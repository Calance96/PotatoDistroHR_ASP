using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Potato_Distro_HRM__Web_ {
    public partial class Admin : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            switch (Session["Tab"])
            {
                case "home":
                    TitleLbl.Text = "Home";
                    RemoveCurrentTabClass();
                    HomeBtn.CssClass += " currentTab";
                    break;
                case "leave":
                    TitleLbl.Text = "Manage Leave Applications";
                    RemoveCurrentTabClass();
                    LeaveBtn.CssClass += " currentTab";
                    break;
                case "employee":
                    TitleLbl.Text = "Manage Employees";
                    RemoveCurrentTabClass();
                    EmployeesBtn.CssClass += " currentTab";
                    break;
                case "dept":
                    TitleLbl.Text = "Manage Departments";
                    RemoveCurrentTabClass();
                    DeptBtn.CssClass += " currentTab";
                    break;
                case "payroll":
                    TitleLbl.Text = "Manage Payroll";
                    RemoveCurrentTabClass();
                    PayrollBtn.CssClass += " currentTab";
                    break;
                default:
                    break;
            }
        }
        private void RemoveCurrentTabClass()
        {
            EmployeesBtn.CssClass.Replace("currentTab", "").Trim();
            DeptBtn.CssClass.Replace("currentTab", "").Trim();
            LeaveBtn.CssClass.Replace("currentTab", "").Trim();
            PayrollBtn.CssClass.Replace("currentTab", "").Trim();
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/LoginForm.aspx");
        }

        protected void LeaveBtn_Click(object sender, EventArgs e)
        {
            Session["Tab"] = "leave";
            Response.Redirect("~/admin/ManageLeaveForm.aspx");
        }

        protected void HomeBtn_Click(object sender, EventArgs e)
        {
            Session["Tab"] = "home";
            Response.Redirect("~/admin/AdminHomeForm.aspx");
        }

        protected void EmployeesBtn_Click(object sender, EventArgs e)
        {
            Session["Tab"] = "employee";
            Response.Redirect("~/admin/EmployeeList.aspx");
        }
    }
}