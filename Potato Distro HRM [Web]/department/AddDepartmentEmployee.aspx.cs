using Npgsql;
using Potato_Distro_HRM__Web_.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_.department
{
    public partial class AddDepartmentEmployee : System.Web.UI.Page
    {
        private string deptId = "";
        private const string DEPARTMENT_ADD_EMPLOYEE = "Update employee SET dept=:deptId WHERE id=:id";
        private const string FIELDS = "id,fname,lname";
        private const string QUERY_FREE_EMPLOYEE = "SELECT " + FIELDS + " FROM employee WHERE dept='0' ORDER by id";
        private const string QUERY_EMPLOYEE_BY_FNAME = "SELECT " + FIELDS + " FROM emp_super_dept_view WHERE dept='No department' and fname ILIKE '%' || :fname || '%' ORDER BY id";
        private const string QUERY_EMPLOYEE_BY_LNAME = "SELECT " + FIELDS + " FROM emp_super_dept_view WHERE dept='No department' and lname ILIKE '%' || :lname || '%' ORDER BY id";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["id"] == null)
            {
                deptMessageLabel.Text = "Department ID not specified";
                deptMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                deptMessagePanel.Visible = true;
            }
            else
            {
                deptId = Request["id"];
                if (!IsPostBack && !string.IsNullOrEmpty(Request["id"]))
                    GridViewBindAllEmployee();
            }
        }

        private void GridViewBindAllEmployee()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
            using (NpgsqlCommand cmd = new NpgsqlCommand(QUERY_FREE_EMPLOYEE, conn))
            {
                conn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                employeeGridView.DataSource = ds.Tables[0].DefaultView;
                employeeGridView.DataBind();
            }
        }

        protected void EmployeeGridView_DataBound(object sender, EventArgs e)
        {
            if (employeeGridView.Rows.Count == 0)
            {
                zeroResultPanel.Visible = true;
            }
        }

        protected void ClearSearchBtn_onClick(object sender, EventArgs e)
        {
            btnClearSearch.Enabled = false;
            btnClearSearch.CssClass = "btn btn-dark";
            searchBox.Text = "";
            zeroResultPanel.Visible = false;
        }

        protected void SearchBtn_OnClick(object sender, EventArgs e)
        {
            zeroResultPanel.Visible = false;
            if (string.IsNullOrEmpty(searchBox.Text))
            {
                return;
            }

            bool valid = true;

            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                switch (searchCriteria.SelectedValue)
                {
                    case "1":
                        cmd.CommandText = QUERY_EMPLOYEE_BY_FNAME;
                        cmd.Parameters.Add(new NpgsqlParameter("fname", searchBox.Text.ToString().Trim()));
                        break;
                    case "2":
                        cmd.CommandText = QUERY_EMPLOYEE_BY_LNAME;
                        cmd.Parameters.Add(new NpgsqlParameter("lname", searchBox.Text.ToString().Trim()));
                        break;
                }

                if (valid)
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
                    {
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

        protected void AddDeptEmployee(object sender, CommandEventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
            using (NpgsqlCommand addEmployee = new NpgsqlCommand(DEPARTMENT_ADD_EMPLOYEE, conn))
            {
                conn.Open();
                addEmployee.Parameters.Add(new NpgsqlParameter("deptId", Convert.ToInt32(deptId)));
                addEmployee.Parameters.Add(new NpgsqlParameter("id", Convert.ToInt32(e.CommandArgument)));
                addEmployee.ExecuteNonQuery();
                GridViewBindAllEmployee();
                deptMessageLabel.Text = "Employee added successfully to " + Department.GetDepartmentName(Convert.ToInt32(deptId)) + " department";
                deptMessagePanel.CssClass = "alert alert-success alert-dismissible";
                deptMessagePanel.Visible = true;
            }
        }

        protected void employeeGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortColumns(e.SortExpression);
        }

        private void SortColumns(string sortExpression)
        {
            DataView dv = (DataView)employeeGridView.DataSource;

            // By default, when a column header is first clicked on, assume it as ascending order
            if (ViewState[sortExpression] == null)
            {
                ViewState[sortExpression] = "ASC";
            }

            // If the column is currently in descending order
            if (ViewState[sortExpression].ToString() == "ASC")
            {
                ViewState[sortExpression] = "DESC"; // Set the column to descending order
            }
            else
            {
                ViewState[sortExpression] = "ASC"; // Else set the column to asecnding order
            }

            // Let's sort and view the update in UI!
            dv.Sort = sortExpression + " " + ViewState[sortExpression];
            employeeGridView.DataSource = dv;
            employeeGridView.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/department/ManageDepartmentEmployee.aspx?id=" + deptId);
        }
    }
}