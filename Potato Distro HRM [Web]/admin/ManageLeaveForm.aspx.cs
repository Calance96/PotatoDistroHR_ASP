using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_
{
    public partial class ManageLeaveForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ApproveBtnClicked(object sender, EventArgs e)
        {
            LinkButton approveBtn = (LinkButton)sender;
            int leaveId = Convert.ToInt32(approveBtn.CommandArgument);

            if (Status.ChangeLeaveStatus(Status.Type.Approved, leaveId))
                Repeater1.DataBind();
        }

        public void RejectBtnClicked(object sender, EventArgs e)
        {
            LinkButton rejectBtn = (LinkButton)sender;
            int leaveId = Convert.ToInt32(rejectBtn.CommandArgument);

            if (Status.ChangeLeaveStatus(Status.Type.Rejected, leaveId))
                Repeater1.DataBind();
        }
    }
}