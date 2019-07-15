using Npgsql;
using Potato_Distro_HRM__Web_.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Potato_Distro_HRM__Web_.admin.ManagePayroll;

namespace Potato_Distro_HRM__Web_.admin
{
    public partial class SalarySummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmpSalaryDetails details = (EmpSalaryDetails)Session["salarydetails"];
            int id = details.id;
            int countedLeaveDays = details.countedLeaveDays;
            int currentMonth = details.month;
            int currentYear = details.year;
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(
                "SELECT employee.id, (fname || ' ' || lname) AS name, department.name AS dept, salary " +
                "FROM employee, department WHERE employee.id=:id AND employee.dept = department.id;", conn);
            cmd.Parameters.Add(new NpgsqlParameter("id", id));

            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                empid.Text = dr[0].ToString();
                empname.Text = dr[1].ToString();
                dept.Text = dr[2].ToString();
                monthlySalary.Text = dr[3].ToString();

                payPeriod.Text = "1/" + currentMonth + "/" + currentYear + " - " + DateTime.DaysInMonth(currentYear, currentMonth) + "/" + currentMonth + "/" + currentYear;

                double salary = (double)dr[3];
                double leaveAmount = countedLeaveDays * salary / DateTime.DaysInMonth(currentYear, currentMonth);
                leave.Text = Math.Round(leaveAmount,2).ToString();
                netSalary.Text = Math.Round((salary - leaveAmount), 2).ToString();
            }


        }
    }
}