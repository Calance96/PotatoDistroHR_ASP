using Npgsql;
using Potato_Distro_HRM__Web_.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        int currentMonth;
        int currentYear;
        Dictionary<int, int> idDays;
        protected void Page_Load(object sender, EventArgs e)
        {

            currentMonth = ((int[])Session["context"])[1];
            currentYear = ((int[])Session["context"])[0];
            idDays = new Dictionary<int, int>() { { 1, 5 }, { 2, 3 }, { 3, 31 } };
            idDays = (Dictionary<int, int>)Session["idLeaveDays"];
            List<int> emplist = new List<int>(idDays.Keys);

            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString);
            conn.Open();

           

            NpgsqlCommand cmd;
            cmd = new NpgsqlCommand(
                "SELECT employee.id, (fname || ' ' || lname) AS name, department.name AS dept, salary " +
                "FROM employee, department " +
                "WHERE employee.id = ANY(:intlist) AND employee.dept = department.id " +
                "ORDER BY employee.id;", conn);
                cmd.Parameters.Add(new NpgsqlParameter("intlist",emplist));

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Repeater1.DataSource = dataTable;
            Repeater1.DataBind();


        }

        protected void printBtn_Click(object sender, EventArgs e)
        {
            Session["ctrl"] = Panel1;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintPreview.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label payPeriod = (Label)e.Item.FindControl("payPeriod");
            payPeriod.Text = "1/" + currentMonth + "/" + currentYear + " - " + DateTime.DaysInMonth(currentYear, currentMonth) + "/" + currentMonth + "/" + currentYear;

            DataRowView rowview = (DataRowView)e.Item.DataItem;
            int countedLeaveDays = idDays[(int)rowview["id"]];

            double salary = (double)rowview["salary"];
            double leaveAmount = countedLeaveDays * salary / DateTime.DaysInMonth(currentYear, currentMonth);

            Label leave = (Label)e.Item.FindControl("leave");
            leave.Text = Math.Round(leaveAmount, 2).ToString();

            Label netSalary = (Label)e.Item.FindControl("netSalary");
            netSalary.Text = Math.Round((salary - leaveAmount), 2).ToString();
        }
    }
}