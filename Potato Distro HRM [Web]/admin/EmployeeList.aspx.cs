﻿using Npgsql;
using Potato_Distro_HRM__Web_.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_.admin {
    public partial class EmployeeList : System.Web.UI.Page {

        private Dictionary<int, string> supervisors;
        private Dictionary<int, string> departments;

        private const string QUERY_ALL_EMPLOYEE_QUERY = "SELECT * FROM emp_super_dept_view ORDER by id, dept";
        private const string QUERY_EMPLOYEE_BY_FNAME = "SELECT * FROM emp_super_dept_view WHERE fname ILIKE '%' || :fname || '%' ORDER BY id, dept";
        private const string QUERY_EMPLOYEE_BY_LNAME = "SELECT * FROM emp_super_dept_view WHERE lname ILIKE '%' || :lname || '%' ORDER BY id, dept";
        private const string QUERY_EMPLOYEE_BY_DEPT = "SELECT * FROM emp_super_dept_view WHERE dept ILIKE '%' || :deptName || '%' ORDER BY id, dept";
        private const string QUERY_EMPLOYEE_BY_SUPER = "SELECT * FROM emp_super_dept_view WHERE supervisor ILIKE '%' || :superName || '%' ORDER BY id, dept";
        private const string QUERY_EMPLOYEE_BY_STATUS_OK = "SELECT * FROM emp_super_dept_view WHERE end_date IS NULL ORDER BY id, dept";
        private const string QUERY_EMPLOYEE_BY_STATUS_NOT_OK = "SELECT * FROM emp_super_dept_view WHERE end_date IS NOT NULL ORDER BY id, dept";
        
        protected void Page_Load(object sender, EventArgs e) {
            supervisors = new Dictionary<int, string>();
            departments = new Dictionary<int, string>();
            
            DataView supervisorDv = (DataView)SupervisorDataSource.Select(DataSourceSelectArguments.Empty);
            for (int i = 0; i < supervisorDv.Count; ++i) {
                supervisors.Add((int)supervisorDv[i][0], (string)supervisorDv[i][1]);
            }

            DataView departmentDv = (DataView)DepartmentDataSource.Select(DataSourceSelectArguments.Empty);
            for (int i = 0; i < departmentDv.Count; ++i) {
                departments.Add((int)departmentDv[i][0], (string)departmentDv[i][1]);
            }
            GridViewBindAllEmployee();
        }

        private void GridViewBindAllEmployee() {
            using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
            using (NpgsqlCommand cmd = new NpgsqlCommand(QUERY_ALL_EMPLOYEE_QUERY, conn)) {
                conn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                employeeGridView.DataSource = ds.Tables[0].DefaultView;
                employeeGridView.DataBind();
            }
        }

        protected void EmployeeGridView_DataBound(object sender, EventArgs e) {
            TranslateGridViewFields();

            if (employeeGridView.Rows.Count == 0) {
                zeroResultPanel.Visible = true;
            }
        }

        private void TranslateGridViewFields() {
            for (int i = 0; i < employeeGridView.Rows.Count; ++i) {
                Label birthDate = (Label)employeeGridView.Rows[i].FindControl("birthDate");
                Label startDate = (Label)employeeGridView.Rows[i].FindControl("startDate");
                Label endDate = (Label)employeeGridView.Rows[i].FindControl("endDate");
                Label supervisor = (Label)employeeGridView.Rows[i].FindControl("supervisor");

                birthDate.Text = birthDate.Text.Substring(0, birthDate.Text.IndexOf(' '));
                startDate.Text = startDate.Text.Substring(0, startDate.Text.IndexOf(' '));
                endDate.Text = string.IsNullOrEmpty(endDate.Text.ToString()) ? "-" : endDate.Text.Substring(0, endDate.Text.IndexOf(' '));
                supervisor.Text = string.IsNullOrEmpty(supervisor.Text) ? "-" : supervisor.Text;
            }
        }

        protected void ClearSearchBtn_onClick(object sender, EventArgs e) {
            btnClearSearch.Enabled = false;
            btnClearSearch.CssClass = "btn btn-dark";
            searchBox.Text = "";
            zeroResultPanel.Visible = false;
        }

        protected void SearchBtn_OnClick(object sender, EventArgs e) {
            zeroResultPanel.Visible = false;
            if (string.IsNullOrEmpty(searchBox.Text)) {
                return;
            }

            bool valid = true;
            
            using (NpgsqlCommand cmd = new NpgsqlCommand()) {              
                switch (searchCriteria.SelectedValue) {
                    case "1":
                        cmd.CommandText = QUERY_EMPLOYEE_BY_FNAME;
                        cmd.Parameters.Add(new NpgsqlParameter("fname", searchBox.Text.ToString().Trim()));
                        break;
                    case "2":
                        cmd.CommandText = QUERY_EMPLOYEE_BY_LNAME;
                        cmd.Parameters.Add(new NpgsqlParameter("lname", searchBox.Text.ToString().Trim()));
                        break;
                    case "3":
                        cmd.CommandText = QUERY_EMPLOYEE_BY_DEPT;
                        cmd.Parameters.Add(new NpgsqlParameter("deptName", searchBox.Text.ToString().Trim()));
                        break;
                    case "4":
                        cmd.CommandText = QUERY_EMPLOYEE_BY_SUPER;
                        cmd.Parameters.Add(new NpgsqlParameter("superName", searchBox.Text.ToString().Trim()));
                        break;
                    case "5":
                        if (searchBox.Text == "In service (1)")
                            cmd.CommandText = QUERY_EMPLOYEE_BY_STATUS_OK;
                        else if (searchBox.Text == "Not in service (2)")
                            cmd.CommandText = QUERY_EMPLOYEE_BY_STATUS_NOT_OK;
                        else
                            valid = false;
                        break;
                }

                if (valid) {
                    using (NpgsqlConnection conn = DatabaseConnection.GetConnection()) {
                        conn.Open();
                        cmd.Connection = conn;
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        employeeGridView.DataSource = ds.Tables[0].DefaultView;
                        employeeGridView.DataBind();
                        btnClearSearch.Enabled = true;
                        btnClearSearch.CssClass = "btn btn-info";
                    }
                }
            }
        }

        protected void EditCustomer(object sender, CommandEventArgs e) {
            Response.Redirect("~/admin/EditEmployee.aspx?id=" + e.CommandArgument);
        }

        protected void employeeGridView_Sorting(object sender, GridViewSortEventArgs e) {
            SortColumns(e.SortExpression);
        }

        private void SortColumns(string sortExpression) {
            DataView dv = (DataView)employeeGridView.DataSource;

            // By default, when a column header is first clicked on, assume it as ascending order
            if (ViewState[sortExpression] == null) { 
                ViewState[sortExpression] = "ASC";
            }

            // If the column is currently in descending order
            if (ViewState[sortExpression].ToString() == "ASC") { 
                ViewState[sortExpression] = "DESC"; // Set the column to descending order
            } else {
                ViewState[sortExpression] = "ASC"; // Else set the column to asecnding order
            }

            // Let's sort and view the update in UI!
            dv.Sort = sortExpression + " " + ViewState[sortExpression];
            employeeGridView.DataSource = dv;
            employeeGridView.DataBind();
        }
    }


}