using Npgsql;
using Potato_Distro_HRM__Web_.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_
{
    public partial class ChangePasswordForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = DatabaseConnection.GetConnection();
            conn.Open();
            NpgsqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE ACCOUNT " +
                "SET Password = :newpassword " +
                "WHERE ACCOUNT.Id = :id AND ACCOUNT.Password = :oldpassword;";
            cmd.Parameters.Add(new NpgsqlParameter("newpassword", newPasswordInput.Text));
            cmd.Parameters.Add(new NpgsqlParameter("id", Session["UserID"]));
            cmd.Parameters.Add(new NpgsqlParameter("oldpassword", oldPasswordInput.Text));

            int successfulQuery = cmd.ExecuteNonQuery();
            conn.Close();

            if (successfulQuery > 0)
            {
                ResultLbl.Text = "Changed password successfully!";
            }
            else
            {
                ResultLbl.Text = "Old password is incorrect!";
                oldPasswordInput.Text = "";
                newPasswordInput.Text = "";
                repeatPasswordInput.Text = "";
            }
        }
    }
}