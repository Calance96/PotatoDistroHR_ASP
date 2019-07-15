using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_.admin
{
    public partial class ManagePayroll : System.Web.UI.Page
    {
        private DateTime currentDate = DateTime.Now;
        private int currentMonth = DateTime.Now.Month;
        DataTable dataTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                GetNewQuery();
            }
            
        }

        private void GetNewQuery()
        {
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(
                "SELECT employee.id, (fname || ' ' || lname) as name, dept, salary, leaves.start as leave_start, leaves.days as leave_days " +
                "FROM employee LEFT OUTER JOIN " +
                "( SELECT * FROM leave WHERE status = 2 ) AS leaves " +
                "ON employee.id = leaves.empid; ", conn);

            //NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(
            //    "SELECT employee.id, (fname || ' ' || lname) as name, " +
            //    "dept, salary, leave.start as leave_start, leave.days as leave_days " +
            //    "FROM employee LEFT OUTER JOIN leave ON employee.id = leave.empid " +
            //    "WHERE employee.id = leave.empid and leave.status = 2 and " +
            //    "((extract(MONTH FROM(leave.start)) < (extract(MONTH FROM(current_date))) AND " +
            //    "EXTRACT(MONTH FROM (leave.start + leave.days * INTERVAL '1 day' )) > (extract(MONTH FROM(current_date)))) " +
            //    "OR extract(MONTH FROM(leave.start)) = (extract(MONTH FROM(current_date))) OR " +
            //    "EXTRACT(MONTH FROM(leave.start + leave.days * INTERVAL '1 day')) = (extract(MONTH FROM(current_date))))", conn);


            dataTable = new DataTable();
            adapter.Fill(dataTable);
            UpdateListView(currentMonth);
        }

        private void UpdateListView(int currentMonth)
        {
            DataTable newDataTable = new DataTable();

            DataColumn column = new DataColumn("id", typeof(int));
            newDataTable.Columns.Add(column);

            column = new DataColumn("name", typeof(string));
            newDataTable.Columns.Add(column);

            column = new DataColumn("dept", typeof(int));
            newDataTable.Columns.Add(column);

            column = new DataColumn("salary", typeof(double));
            newDataTable.Columns.Add(column);

            column = new DataColumn("leave_start", typeof(DateTime));
            newDataTable.Columns.Add(column);

            column = new DataColumn("leave_days", typeof(int));
            newDataTable.Columns.Add(column);

            column = new DataColumn("counted_leave_days", typeof(int));
            newDataTable.Columns.Add(column);

            DataRow datarow;

            foreach(DataRow row in dataTable.Rows)
            {
                int countedLeaveDays = 0;

                if (row["leave_start"] != DBNull.Value)
                {
                    DateTime startDate = Convert.ToDateTime(row["leave_start"]);
                    int days = Convert.ToInt32(row["leave_days"]);
                    DateTime endDate = startDate.AddDays(days);
                    int startMonth = startDate.Month;
                    int endMonth = endDate.Month;

                    if (startMonth < currentMonth && endMonth > currentMonth)
                    {
                        countedLeaveDays = DateTime.DaysInMonth(currentDate.Year, currentMonth);
                    }
                    else if (startMonth == currentMonth)
                    {
                        if (endMonth > currentMonth)
                        {
                            countedLeaveDays = DateTime.DaysInMonth(currentDate.Year, currentMonth) - startDate.Day;
                        }
                        else if (endMonth == currentMonth)
                        {
                            countedLeaveDays = endDate.Day - startDate.Day;
                        }
                    }
                    else if (endMonth == currentMonth)
                    {
                        countedLeaveDays = endDate.Day;
                    }
                }

                
                datarow = newDataTable.NewRow();
                datarow.ItemArray = new object[] { row["id"], row["name"], row["dept"], row["salary"], row["leave_start"], row["leave_days"], countedLeaveDays };
                newDataTable.Rows.Add(datarow);
            }

            

            var totalLeaveDays =
                from entry in newDataTable.AsEnumerable()
                group entry by entry.Field<int>("id") into grp
                select new
                {
                    id = grp.Key,
                    total_leave_days = grp.Sum(r => r.Field<int>("counted_leave_days"))
                };

            var empinfo = 
                (from entry in newDataTable.AsEnumerable()
                select new
                {
                    id = entry.Field<int>("id"),
                    name = entry.Field<string>("name"),
                    dept = entry.Field<int>("dept"),
                    salary = entry.Field<double>("salary")
                }).Distinct();


            var all =
                (from leave in totalLeaveDays
                join emp in empinfo on leave.id equals emp.id
                select new
                {
                    id = leave.id,
                    name = emp.name,
                    dept = emp.dept,
                    salary = emp.salary,
                    total_leave_days = leave.total_leave_days
                }).Distinct();

            ListView1.DataSource = all;
            ListView1.DataBind();
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                dynamic rowView = dataItem.DataItem as dynamic;
                int totalLeaveDays = Convert.ToInt32(rowView.total_leave_days);
                int monthlySalary = Convert.ToInt32(rowView.salary);
                Label salaryLbl = (Label)e.Item.FindControl("salary");
                salaryLbl.Text = (Math.Round(monthlySalary - totalLeaveDays * (monthlySalary * 1.0/ DateTime.DaysInMonth(currentDate.Year, currentMonth)),2)).ToString();
            }
        }

        protected void detailsBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Response.Redirect("SalarySummary.aspx?empid=" + btn.CommandArgument);
        }

        protected void printBtn_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["potato_dbConnectionString"].ConnectionString);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM LEAVE;", conn);
            int success = cmd.ExecuteNonQuery();
            if (success > 0)
            {
                Response.Write("success");
            }
        }

        protected void monthBtn_Click(object sender, EventArgs e)
        {
            currentMonth = DateTime.ParseExact(Request["date"], "MMMM", CultureInfo.CurrentCulture).Month;
            GetNewQuery();

        }
    }
}