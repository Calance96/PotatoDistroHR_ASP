using Npgsql;
using Potato_Distro_HRM__Web_.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_
{
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString);
            conn.Open();
            NpgsqlCommand cmd = conn.CreateCommand();

            if (ModeInput.Text == "Employee")
            {
                cmd.CommandText = "SELECT EMPLOYEE.id, EMPLOYEE.fname, EMPLOYEE.lname FROM EMPLOYEE, ACCOUNT " +
                    "WHERE ACCOUNT.id = :id AND ACCOUNT.password = :password AND EMPLOYEE.id = ACCOUNT.id;";
            }
            else if (ModeInput.Text == "Admin")
            {
                cmd.CommandText = "SELECT EMPLOYEE.id, EMPLOYEE.fname, EMPLOYEE.lname FROM EMPLOYEE, ACCOUNT_ADMIN " +
                    "WHERE ACCOUNT_ADMIN.id = :id AND ACCOUNT_ADMIN.password = :password AND EMPLOYEE.id = ACCOUNT_ADMIN.id;";
            }

            cmd.Parameters.Add(new NpgsqlParameter("id", Int32.Parse(IdInput.Text)));
            cmd.Parameters.Add(new NpgsqlParameter("password", PwdInput.Text));

            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read()) //valid user
            {
                Session["UserID"] = dr.GetInt32(0);
                Session["UserFullName"] = dr.GetString(1) + " " + dr.GetString(2);
                Session["Tab"] = "home";
                if (ModeInput.Text == "Employee")
                {
                    LoginLbl.Text = "Employee login GO!" + Session["UserFullName"];
                    Response.Redirect("employee/EmployeeHomeForm.aspx");
                    //EmployeeHomeForm empHome = new EmployeeHomeForm();
                    //empHome.Show();
                }
                else if (ModeInput.Text == "Admin")
                {
                    LoginLbl.Text = "Admin login GO!";
                    Response.Redirect("admin/AdminHomeForm.aspx");
                    //AdminHomeForm adminHome = new AdminHomeForm();
                    //adminHome.Show();
                }
                IdInput.Text = string.Empty;
                PwdInput.Text = string.Empty;
            }
            else
            {
                LoginLbl.Text = "Login failed!";
                //MessageBox.Show("User is NOT valid!!");
            }
            conn.Close();
        }
    }
}