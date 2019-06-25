using Npgsql;
using Potato_Distro_HRM__Web_.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_ {
    public partial class EditEmployee : System.Web.UI.Page {

        private const string ALERT_ERROR_CLASS = "alert alert-danger alert-dismissible";
        private string empId = "";

        protected void Page_Load(object sender, EventArgs e) {
            if (Request["id"] == null) {
                empMessageLabel.Text = "Employee ID not specified";
                empMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                empMessagePanel.Visible = true;
                DisableAllControls();

            } else {
                empId = Request["id"];
                if (!IsPostBack)
                    PreFillForm();
            }
        }

        private void DisableAllControls() {
            firstName.Enabled = false;
            lastName.Enabled = false;
            contact.Enabled = false;
            birthDate.Disabled = true;
            address.Enabled = false;
            rbMale.Enabled = false;
            rbFemale.Enabled = false;
            dropdownDept.Enabled = false;
            startDate.Disabled = true;
            dropdownSuper.Enabled = false;
            endDate.Disabled = true;
            salary.Enabled = false;
            empStatus.Enabled = false;
        }

        private void PreFillForm() {
            if (!string.IsNullOrEmpty(empId)) {
                string select_query = "SELECT * FROM employee WHERE id=" + empId;

                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                using (NpgsqlCommand cmd = new NpgsqlCommand(select_query, conn)) {
                    conn.Open();
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read()) {
                        firstName.Text = dr.GetString(1);
                        lastName.Text = dr.GetString(2);
                        birthDate.Value = dr.GetDateTime(3).ToShortDateString();
                        address.Text = dr.GetString(4);
                        rbFemale.Checked = dr.GetString(5) == "F" ? true : false;
                        contact.Text = dr.GetString(6);
                        dropdownSuper.SelectedValue = dr.IsDBNull(7) ? "" : dr.GetInt32(7).ToString();
                        startDate.Value = dr.GetDateTime(8).ToShortDateString();
                        endDate.Value = dr.IsDBNull(9) ? null : dr.GetDateTime(9).ToShortDateString();
                        empStatus.SelectedValue = dr.IsDBNull(9) ? "1" : "0";
                        salary.Text = dr.GetDouble(10).ToString();
                        dropdownDept.SelectedValue = dr.GetInt32(11).ToString();
                    }
                }

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect("~/admin/EmployeeList.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            empMessagePanel.Visible = false;
            jobMessagePanel.Visible = false;
 
            if (AreInputsValid()) {
                bool updated = UpdateEmployee();
                if (updated) {
                    empMessageLabel.Text = "Employee updated successfully";
                    empMessagePanel.CssClass = "alert alert-success alert-dismissible";
                    empMessagePanel.Visible = true;
                } else {
                    empMessageLabel.Text = "An error has occurred. Please try again later.";
                    empMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                    empMessagePanel.Visible = true;
                }
            }
        }

        private bool AreInputsValid() {
            bool valid = true;

            if (string.IsNullOrEmpty(firstName.Text.Trim())
                || string.IsNullOrEmpty(lastName.Text.Trim())
                || string.IsNullOrEmpty(contact.Text.Trim())
                || string.IsNullOrEmpty(address.Text.Trim())
                || string.IsNullOrEmpty(birthDate.Value)) {

                empMessageLabel.Text = "Missing required field(s)!";
                empMessagePanel.CssClass = ALERT_ERROR_CLASS;
                empMessagePanel.Visible = true;
                valid = false;
            }

            if (string.IsNullOrEmpty(startDate.Value)
                || string.IsNullOrEmpty(salary.Text)) {
                jobMessageLabel.Text = "Missing required field(s)!";
                jobMessagePanel.CssClass = ALERT_ERROR_CLASS;
                jobMessagePanel.Visible = true;
                valid = false;
            }
            return valid;
        }

        private bool UpdateEmployee() {
            Employee employee = CreateUpdatedEmployee();
            string update_command = "Update employee SET fname=:fname, lname=:lname, bdate=:bdate, address=:address, sex=:sex, contact=:contact, super_id=:super_id, start_date=:start_date, end_date=:end_date, salary=:salary, dept=:dept WHERE id=:id";

            try {
                using (NpgsqlConnection conn = DatabaseConnection.GetConnection())
                using (NpgsqlCommand command = new NpgsqlCommand(update_command, conn)) {
                    conn.Open();
                    command.Parameters.Add(new NpgsqlParameter("fname", employee.fname));
                    command.Parameters.Add(new NpgsqlParameter("lname", employee.lname));
                    command.Parameters.Add(new NpgsqlParameter("bdate", DateTime.Parse(employee.birthdate)));
                    command.Parameters.Add(new NpgsqlParameter("address", employee.address));
                    command.Parameters.Add(new NpgsqlParameter("sex", employee.gender));
                    command.Parameters.Add(new NpgsqlParameter("contact", employee.contact));
                    command.Parameters.Add(new NpgsqlParameter("start_date", DateTime.Parse(employee.startDate)));
                    command.Parameters.Add(new NpgsqlParameter("salary", employee.salary));
                    command.Parameters.Add(new NpgsqlParameter("dept", employee.department));

                    if (string.IsNullOrEmpty(employee.superId)) {
                        command.Parameters.Add(new NpgsqlParameter("super_id", DBNull.Value));
                    } else {
                        command.Parameters.Add(new NpgsqlParameter("super_id", Convert.ToInt32(employee.superId)));
                    }

                    if (string.IsNullOrEmpty(employee.endDate)) {
                        command.Parameters.Add(new NpgsqlParameter("end_date", DBNull.Value));
                    } else {
                        command.Parameters.Add(new NpgsqlParameter("end_date", DateTime.Parse(employee.endDate)));
                    }

                    command.Parameters.Add(new NpgsqlParameter("id", Convert.ToInt32(empId)));

                    int updated = command.ExecuteNonQuery();
                    return Convert.ToBoolean(updated);
                }
            } catch (NpgsqlException ecx) {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Employee Insertion Failure", "alert('" + ecx.Message +  "')", true);
            }
            return false;
        }

        private Employee CreateUpdatedEmployee() {
            Employee employee = new Employee();
            employee.fname = firstName.Text.ToString().Trim();
            employee.lname = lastName.Text.ToString().Trim();
            employee.contact = contact.Text.ToString().Trim();
            employee.address = address.Text.ToString().Trim();
            employee.birthdate = DateTime.Parse(birthDate.Value.ToString()).ToShortDateString();
            employee.gender = rbMale.Checked ? 'M' : 'F';
            employee.department = Convert.ToInt32(dropdownDept.SelectedValue);
            employee.salary = Convert.ToDouble(salary.Text);
            employee.startDate = DateTime.Parse(startDate.Value.ToString()).ToShortDateString();
            employee.endDate = empStatus.SelectedValue == "0" ? DateTime.ParseExact(endDate.Value, "MM/dd/yyyy", null).ToShortDateString() : "";
            employee.superId = dropdownSuper.SelectedValue;

            return employee;
        }
    }
}