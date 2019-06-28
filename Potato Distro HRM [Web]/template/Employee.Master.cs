using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_
{
    public partial class EmployeeMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Session["Tab"])
            {
                case "home":
                    TitleLbl.Text = "Home";
                    ChangePwdBtn.CssClass.Replace("currentTab", "").Trim();
                    LeaveBtn.CssClass.Replace("currentTab","").Trim();
                    HomeBtn.CssClass += " currentTab";
                    break;
                case "leave":
                    TitleLbl.Text = "Apply For Leave";
                    ChangePwdBtn.CssClass.Replace("currentTab","").Trim();
                    HomeBtn.CssClass.Replace("currentTab","").Trim();
                    LeaveBtn.CssClass += " currentTab";
                    //LeaveBtn.Attributes.Add("class", "currentTab");
                    break;
                case "changePwd":
                    TitleLbl.Text = "Change Password";
                    HomeBtn.CssClass.Replace("currentTab", "").Trim();
                    LeaveBtn.CssClass.Replace("currentTab","").Trim();
                    ChangePwdBtn.CssClass += " currentTab";
                    //ChangePwdBtn.Attributes.Add("class", "currentTab");
                    break;
                default:
                    break;
            }
        }
        protected void HomeBtn_Click(object sender, EventArgs e)
        {
            Session["Tab"] = "home";
            Response.Redirect("EmployeeHomeForm.aspx");
        }

        protected void LeaveBtn_Click(object sender, EventArgs e)
        {
            Session["Tab"] = "leave";
            Response.Redirect("LeaveHistoryForm.aspx");
        }

        protected void ChangePwdBtn_Click(object sender, EventArgs e)
        {
            Session["Tab"] = "changePwd";
            Response.Redirect("ChangePasswordForm.aspx");
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/LoginForm.aspx");
        }

    }
}