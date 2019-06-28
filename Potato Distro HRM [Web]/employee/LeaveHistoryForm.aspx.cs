using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_
{
    public partial class LeaveHistoryForm : System.Web.UI.Page
    {
        private Status.Type currentStatus;
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.InsertParameters["empid"].DefaultValue = Session["UserID"].ToString();
            SqlDataSource1.SelectParameters.Add("userId", System.Data.DbType.Int32, Session["UserID"].ToString());
            SqlDataSource1.SelectParameters.Add("status", System.Data.DbType.Int32, "1");
            SqlDataSource1.SelectCommand = "SELECT LEAVE.id, LEAVE.start, LEAVE.days, LEAVE.reason, LEAVE.status from LEAVE WHERE empid = @userId /*AND status = @status*/ ORDER BY LEAVE.start DESC";
        }

        //private enum Status
        //{
        //    Pending = 1, Accepted = 2, Rejected = 3, Cancelled = 4
        //}

        public void ClearFilter(Object sender, EventArgs e)
        {
            LblStatus.Text = "";
            SqlDataSource1.SelectCommand = "SELECT LEAVE.id, LEAVE.start, LEAVE.days, LEAVE.reason, LEAVE.status from LEAVE WHERE empid = @userId ORDER BY LEAVE.start DESC";
            Repeater1.DataBind();
        }

        public void ShowList(Object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            SqlDataSource1.SelectCommand = "SELECT LEAVE.id, LEAVE.start, LEAVE.days, LEAVE.reason, LEAVE.status from LEAVE WHERE empid = @userId AND status = @status ORDER BY LEAVE.start DESC";
            void switchView(Status.Type status, string text)
            {
                LblStatus.Text = text.ToUpper();
                currentStatus = status;
                SqlDataSource1.SelectParameters["status"].DefaultValue = ((int)status).ToString();
                Repeater1.DataBind();
            }
            switch (btn.CommandArgument)
            {
                case "pending":
                    switchView(Status.Type.Pending, "Pending");
                    break;
                case "approved":
                    switchView(Status.Type.Approved, "Approved");
                    break;
                case "rejected":
                    switchView(Status.Type.Rejected, "Rejected");
                    break;
                case "cancelled":
                    switchView(Status.Type.Cancelled, "Cancelled");
                    break;
                default:
                    break;
            }
        }
        
        public void CancelBtnClicked(Object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int leaveId = Convert.ToInt32(btn.CommandArgument);
            if (Status.ChangeLeaveStatus(Status.Type.Cancelled, leaveId))
                Repeater1.DataBind();
            //OnCancelLeave(sender, e, Convert.ToInt32(btn.CommandArgument));
        }

        //private void OnCancelLeave(object sender, EventArgs e, int leaveId)
        //{
        //    NpgsqlConnection conn = Database.GetConnection();
        //    conn.Open();

        //    NpgsqlCommand cmd = conn.CreateCommand();
        //    cmd.CommandText = "UPDATE LEAVE SET Status = 3 WHERE Id = :id";
        //    cmd.Parameters.Add(new NpgsqlParameter("id", leaveId));
        //    int success = cmd.ExecuteNonQuery();
        //    conn.Close();

        //    if (success > 0)
        //        DataList1.DataBind();
        //    else
        //        Response.Write("Update failed");

        //}

        //protected void DataList1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    Repeater rep = (Repeater)sender;
        //    Control span = e.Item.FindControl("status-icon");
        //    e.Item.
        //    DataList dl = (DataList)sender;
        //    TableCell cell = (TableCell)e.Item.FindControl("CancelCell");
        //    //if (dl.Items.Count != 0)
        //    //{
        //    if (currentStatus == Status.Type.Pending)
        //        cell.Visible = true;
        //    else
        //        cell.Visible = false;
        //    //}
        //}

        public static Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;

            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);
                if (FoundCtl != null)
                    return FoundCtl;
            }

            return null;
        }

        protected void FormView_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            SubmittedLabel.Text = "Leave application submitted successfully!";
            Repeater1.DataBind();
        }

        protected void CalendarValidation(object source, ServerValidateEventArgs args)
        {
            if(((Calendar)LeaveHistoryForm.FindControlRecursive(this.Master,"StartInput")).SelectedDate == DateTime.MinValue)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label icon = (Label)e.Item.FindControl("statusIcon");
            int status = (int)((DataRowView)e.Item.DataItem)["status"];
            switch (status)
            {
                case 1:
                    icon.CssClass += " pficon-restart";
                    break;
                case 2:
                    icon.CssClass += " pficon-ok";
                    break;
                case 3:
                    icon.CssClass += " pficon-error-circle-o";
                    break;
                case 4:
                    icon.CssClass += " pficon-delete";
                    break;
                default:
                    break;

            }
        }
    }
}