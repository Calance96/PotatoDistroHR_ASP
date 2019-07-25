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
    public partial class ViewDepartment : System.Web.UI.Page
    {
        private Dictionary<int, string> departments;

        private const string QUERY_DEPARTMENT = "SELECT * FROM department WHERE id!=0 ORDER by id";
        private const string DELETE_DEPARTMENT = "DELETE FROM department WHERE id=:id";
        private const string QUERY_DEPARTMENT_BY_NAME = "SELECT * FROM department WHERE name ILIKE '%' || :name || '%'";

        protected void Page_Load(object sender, EventArgs e)
        {
            departments = new Dictionary<int, string>();

            DataView departmentDv = (DataView)DepartmentDataSource.Select(DataSourceSelectArguments.Empty);
            for (int i = 0; i < departmentDv.Count; ++i)
            {
                departments.Add((int)departmentDv[i][0], (string)departmentDv[i][1]);
            }
            GridViewBindData();
        }

        private void GridViewBindData()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
            using (NpgsqlCommand cmd = new NpgsqlCommand(QUERY_DEPARTMENT, conn))
            {
                conn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                departmentGridView.DataSource = ds.Tables[0].DefaultView;
                departmentGridView.DataBind();
            }
        }

        protected void DepartmentGridView_DataBound(object sender, EventArgs e)
        {
            TranslateGridViewFields();

            if (departmentGridView.Rows.Count == 0)
            {
                zeroResultPanel.Visible = true;
            }
        }

        private void TranslateGridViewFields()
        {
            for (int i = 0; i < departmentGridView.Rows.Count; ++i)
            {
                Label managerName = (Label)departmentGridView.Rows[i].FindControl("managerName");

                if (managerName.Text != "")
                    managerName.Text = Department.GetManagerName(Convert.ToInt32(managerName.Text));
                else
                    managerName.Text = "N/A";
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

            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                switch (searchCriteria.SelectedValue)
                {
                    case "1":
                        cmd.CommandText = QUERY_DEPARTMENT_BY_NAME;
                        cmd.Parameters.Add(new NpgsqlParameter("name", searchBox.Text.ToString().Trim()));
                        break;
                }

                using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    departmentGridView.DataSource = ds.Tables[0].DefaultView;
                    departmentGridView.DataBind();
                    btnClearSearch.Enabled = true;
                    btnClearSearch.CssClass = "btn btn-info";
                }
            }
        }

        protected void EditDepartment(object sender, CommandEventArgs e)
        {
            Response.Redirect("~/department/EditDepartment.aspx?id=" + e.CommandArgument);
        }

        protected void DeleteDepartment(object sender, CommandEventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
            using (NpgsqlCommand delete_dept = new NpgsqlCommand(DELETE_DEPARTMENT, conn))
            {
                conn.Open();
                delete_dept.Parameters.Add(new NpgsqlParameter("id", Convert.ToInt32(e.CommandArgument)));
                
                deptMessageLabel.Text = "Department " + Department.GetDepartmentName(Convert.ToInt32(e.CommandArgument)) + " has been deleted successfully";
                deptMessagePanel.CssClass = "alert alert-success alert-dismissible";
                deptMessagePanel.Visible = true;
                delete_dept.ExecuteNonQuery();
                GridViewBindData();
            }
        }

        protected void departmentGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortColumns(e.SortExpression);
        }

        private void SortColumns(string sortExpression)
        {
            DataView dv = (DataView)departmentGridView.DataSource;

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
            departmentGridView.DataSource = dv;
            departmentGridView.DataBind();
        }
    }
}