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
    public partial class ManageDepartmentEmployee : System.Web.UI.Page
    {
        private string deptId = "";
        private const string REMOVE_DEPARTMENT_EMPLOYEE = "Update employee SET dept ='0' WHERE id=:id";
        private const string FIELDS = "id,fname,lname,bdate,address,sex,contact,supervisor,end_date,salary";
        private const string QUERY_DEPARTMENT_EMPLOYEE = "SELECT " + FIELDS + " FROM emp_super_dept_view WHERE dept=:dept ORDER by id";
        private const string QUERY_EMPLOYEE_BY_FNAME = "SELECT " + FIELDS + " FROM emp_super_dept_view WHERE dept=:dept and fname ILIKE '%' || :fname || '%' ORDER BY id";
        private const string QUERY_EMPLOYEE_BY_LNAME = "SELECT " + FIELDS + " FROM emp_super_dept_view WHERE dept=:dept and lname ILIKE '%' || :lname || '%' ORDER BY id";
        private const string QUERY_EMPLOYEE_BY_SUPER = "SELECT " + FIELDS + " FROM emp_super_dept_view WHERE dept=:dept and supervisor ILIKE '%' || :superName || '%' ORDER BY id";

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
            using (NpgsqlCommand cmd = new NpgsqlCommand(QUERY_DEPARTMENT_EMPLOYEE, conn))
            {
                conn.Open();
                cmd.Parameters.Add(new NpgsqlParameter("dept", Department.GetDepartmentName(Convert.ToInt32(deptId))));
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                employeeGridView.DataSource = ds.Tables[0].DefaultView;
                employeeGridView.DataBind();
            }
        }

        protected void EmployeeGridView_DataBound(object sender, EventArgs e)
        {
            TranslateGridViewFields();

            if (employeeGridView.Rows.Count == 0)
            {
                zeroResultPanel.Visible = true;
            }
        }

        private void TranslateGridViewFields()
        {
            for (int i = 0; i < employeeGridView.Rows.Count; ++i)
            {
                Label birthDate = (Label)employeeGridView.Rows[i].FindControl("birthDate");
                Label endDate = (Label)employeeGridView.Rows[i].FindControl("endDate");
                Label supervisor = (Label)employeeGridView.Rows[i].FindControl("supervisor");

                birthDate.Text = birthDate.Text.Substring(0, birthDate.Text.IndexOf(' '));
                endDate.Text = string.IsNullOrEmpty(endDate.Text.ToString()) ? "-" : endDate.Text.Substring(0, endDate.Text.IndexOf(' '));
                supervisor.Text = string.IsNullOrEmpty(supervisor.Text) ? "-" : supervisor.Text;
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
                String deptName = Department.GetDepartmentName(Convert.ToInt32(deptId));

                switch (searchCriteria.SelectedValue)
                {
                    case "1":
                        cmd.CommandText = QUERY_EMPLOYEE_BY_FNAME;
                        cmd.Parameters.Add(new NpgsqlParameter("fname", searchBox.Text.ToString().Trim()));
                        cmd.Parameters.Add(new NpgsqlParameter("dept", deptName));
                        break;
                    case "2":
                        cmd.CommandText = QUERY_EMPLOYEE_BY_LNAME;
                        cmd.Parameters.Add(new NpgsqlParameter("lname", searchBox.Text.ToString().Trim()));
                        cmd.Parameters.Add(new NpgsqlParameter("dept", deptName));
                        break;
                    case "3":
                        cmd.CommandText = QUERY_EMPLOYEE_BY_SUPER;
                        cmd.Parameters.Add(new NpgsqlParameter("superName", searchBox.Text.ToString().Trim()));
                        cmd.Parameters.Add(new NpgsqlParameter("dept", deptName));
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

        protected void RemoveEmployee(object sender, CommandEventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
            using (NpgsqlCommand removeEmployee = new NpgsqlCommand(REMOVE_DEPARTMENT_EMPLOYEE, conn))
            {
                conn.Open();
                removeEmployee.Parameters.Add(new NpgsqlParameter("id", Convert.ToInt32(e.CommandArgument)));
                removeEmployee.ExecuteNonQuery();
                GridViewBindAllEmployee();
                deptMessageLabel.Text = "Employee removed successfully from " + Department.GetDepartmentName(Convert.ToInt32(deptId)) + " department";
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/department/AddDepartmentEmployee.aspx?id=" + deptId);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/department/EditDepartment.aspx?id=" + deptId);
        }
    }
}