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
    public partial class AddDepartment : System.Web.UI.Page
    {
        private const string ALERT_ERROR_CLASS = "alert alert-danger alert-dismissible";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            deptMessagePanel.Visible = false;

            if (AreInputsValid())
            {
                bool success = InsertDepartment();
                if (success)
                {
                    deptMessageLabel.Text = "Department added successfully";
                    deptMessagePanel.CssClass = "alert alert-success alert-dismissible";
                    deptMessagePanel.Visible = true;
                    ClearAllFields();
                }
                else
                {
                    deptMessageLabel.Text = "An error has occurred. Please try again later.";
                    deptMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                    deptMessagePanel.Visible = true;
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
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

        private void ClearAllFields()
        {
            deptName.Text = "";
            hotline.Text = "";
            managerDropDownList.SelectedValue = "";
        }

        private bool InsertDepartment()
        {
            string query = "INSERT INTO department(name, hotline, manager) values(:name, :hotline, :manager)";
            int managerId = 0;

            if (managerDropDownList.SelectedValue != "")
                managerId = Convert.ToInt32(managerDropDownList.SelectedValue);

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(query, conn))
                {
                    conn.Open();
                    sqlCommand.Parameters.Add(new NpgsqlParameter("name", deptName.Text));
                    sqlCommand.Parameters.Add(new NpgsqlParameter("hotline", hotline.Text));
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Department Insertion Failure", "alert('" + ecx.Message + "')", true);
            }
            return false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/admin/EmployeeList.aspx");
        }

        protected void managerNameValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int managerId = Department.GetManagerId(args.Value);

            if (managerId == 0 && args.Value != "N/A" && args.Value != "n/a" && args.Value != "")
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
}