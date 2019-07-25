using Npgsql;
using Potato_Distro_HRM__Web_.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_.department
{
    public partial class EditDepartment : System.Web.UI.Page
    {
        private const string ALERT_ERROR_CLASS = "alert alert-danger alert-dismissible";
        private String deptId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["id"] == null)
            {
                deptMessageLabel.Text = "Department ID not specified";
                deptMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                deptMessagePanel.Visible = true;
                deptName.Enabled = false;
                hotline.Enabled = false;
                managerDropDownList.Enabled = false;

            }
            else
            {
                deptId = Request["id"];
                if (!IsPostBack && !string.IsNullOrEmpty(deptId))
                {
                    string query = "SELECT * FROM department WHERE id=" + deptId;

                    using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        conn.Open();
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            deptName.Text = dr.GetString(1);
                            hotline.Text = dr.GetString(2);

                            if (dr.IsDBNull(3))
                                managerDropDownList.SelectedValue = "N/A";
                            else
                                managerDropDownList.SelectedValue = dr.GetInt32(3).ToString();
                        }
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/department/DepartmentList.aspx");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            deptName.Text = "";
            hotline.Text = "";
            managerDropDownList.SelectedValue = "N/A";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            deptMessagePanel.Visible = false;

            if (AreInputsValid())
            {
                bool success = UpdateDepartment();
                if (success)
                {
                    deptMessageLabel.Text = "Department update successfully";
                    deptMessagePanel.CssClass = "alert alert-success alert-dismissible";
                    deptMessagePanel.Visible = true;
                }
                else
                {
                    deptMessageLabel.Text = "An error has occurred. Please try again later.";
                    deptMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                    deptMessagePanel.Visible = true;
                }
            }
        }

        private bool AreInputsValid()
        {
            bool valid = true;

            if (string.IsNullOrEmpty(deptName.Text.Trim())
                || string.IsNullOrEmpty(hotline.Text.Trim()))
            {
                deptMessageLabel.Text = "Missing required field(s)!";
                deptMessagePanel.CssClass = ALERT_ERROR_CLASS;
                deptMessagePanel.Visible = true;
                valid = false;
            }
            return valid;
        }

        private bool UpdateDepartment()
        {
            string query = "Update department SET name=:name, hotline=:hotline, manager=:manager WHERE id=:id";
            int managerId = 0;

            if (managerDropDownList.SelectedValue != "N/A")
                managerId = Convert.ToInt32(managerDropDownList.SelectedValue);

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(query, conn))
                {
                    conn.Open();
                    sqlCommand.Parameters.Add(new NpgsqlParameter("name", deptName.Text));
                    sqlCommand.Parameters.Add(new NpgsqlParameter("hotline", hotline.Text));
                    sqlCommand.Parameters.Add(new NpgsqlParameter("id", Convert.ToInt32(deptId)));
                    if (managerId != 0)
                        sqlCommand.Parameters.Add(new NpgsqlParameter("manager", managerId));
                    else
                        sqlCommand.Parameters.Add(new NpgsqlParameter("manager", DBNull.Value));

                    int success = sqlCommand.ExecuteNonQuery();
                    return Convert.ToBoolean(success);
                }
            }
            catch (NpgsqlException ecx)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Department Update Failure", "alert('" + ecx.Message + "')", true);
            }
            return false;
        }

        protected void managerNameValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int managerId = Department.GetManagerId(args.Value);

            if (managerId == 0 && args.Value != "N/A" && args.Value != "n/a" && args.Value != "")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void btnManageDeptEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/department/ManageDepartmentEmployee.aspx?id=" + deptId);
        }
    }
}