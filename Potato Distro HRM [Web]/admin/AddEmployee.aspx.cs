using Npgsql;
using Potato_Distro_HRM__Web_.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_ {
    public partial class AddEmployee : System.Web.UI.Page {

        private const string ALERT_ERROR_CLASS = "alert alert-danger alert-dismissible";
        int insertionId = -1;

        protected void Page_Load(object sender, EventArgs e) {
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            empMessagePanel.Visible = false;
            jobMessagePanel.Visible = false;
            
                if (AreInputsValid()) {
                    bool added = InsertEmployeeToDatabase();
                    if (added) {
                        empMessageLabel.Text = "Employee added successfully with ID " + insertionId;
                        empMessagePanel.CssClass = "alert alert-success alert-dismissible";
                        empMessagePanel.Visible = true;
                        ClearAllFields();
                    } else {
                        empMessageLabel.Text = "An error has occurred. Please try again later.";
                        empMessagePanel.CssClass = "alert alert-danger alert-dismissible";
                        empMessagePanel.Visible = true;
                    }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e) {
            ClearAllFields();
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

        private void ClearAllFields() {
            firstName.Text = "";
            lastName.Text = "";
            contact.Text = "";
            birthDate.Value = "";
            address.Text = "";
            startDate.Value = "";
            salary.Text = "";
            dropdownSuper.SelectedValue = "";
        }

        private bool InsertEmployeeToDatabase() {
            Employee employee = CreateNewEmployee();
            string insert_command = "INSERT INTO employee(fname, lname, bdate, address, sex, contact, super_id, start_date, salary, dept) VALUES(:fname, :lname, :bdate, :address, :sex, :contact, :super_id, :start_date, :salary, :dept) RETURNING id";
            string insert_account_command = "INSERT INTO account VALUES(:id, :password)";

            try {
                using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString))
                using (NpgsqlCommand emp_command = new NpgsqlCommand(insert_command, conn))
                using (NpgsqlCommand acc_command = new NpgsqlCommand(insert_account_command, conn)) {
                    conn.Open();
                    emp_command.Parameters.Add(new NpgsqlParameter("fname", employee.fname));
                    emp_command.Parameters.Add(new NpgsqlParameter("lname", employee.lname));
                    emp_command.Parameters.Add(new NpgsqlParameter("bdate", DateTime.Parse(employee.birthdate)));
                    emp_command.Parameters.Add(new NpgsqlParameter("address", employee.address));
                    emp_command.Parameters.Add(new NpgsqlParameter("sex", employee.gender));
                    emp_command.Parameters.Add(new NpgsqlParameter("contact", employee.contact));
                    emp_command.Parameters.Add(new NpgsqlParameter("start_date", DateTime.Parse(employee.startDate)));
                    emp_command.Parameters.Add(new NpgsqlParameter("salary", employee.salary));
                    emp_command.Parameters.Add(new NpgsqlParameter("dept", employee.department));

                    if (string.IsNullOrEmpty(employee.superId)) {
                        emp_command.Parameters.Add(new NpgsqlParameter("super_id", DBNull.Value));
                    } else {
                        emp_command.Parameters.Add(new NpgsqlParameter("super_id", Convert.ToInt32(employee.superId)));
                    }

                    insertionId = Convert.ToInt32(emp_command.ExecuteScalar());
                    acc_command.Parameters.Add(new NpgsqlParameter("id", insertionId));
                    acc_command.Parameters.Add(new NpgsqlParameter("password", "123456"));
                    int success = acc_command.ExecuteNonQuery();
                    return Convert.ToBoolean(success);
                }
            } catch (NpgsqlException ecx) {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Employee Insertion Failure", "alert('" + ecx.Message +  "')", true);
            }
            return false;
        }

        private Employee CreateNewEmployee() {

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
            employee.superId = dropdownSuper.SelectedValue;

            return employee;
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect("~/admin/EmployeeList.aspx");
        }
    }
}