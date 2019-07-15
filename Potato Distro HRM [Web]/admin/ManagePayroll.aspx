<%@ Page Title="" Language="C#" MasterPageFile="~/template/Admin.Master" AutoEventWireup="true" CodeBehind="ManagePayroll.aspx.cs" Inherits="Potato_Distro_HRM__Web_.admin.ManagePayroll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="gridview">
        <asp:Button ID="Button1" runat="server" Text="Button" Onclick="Button1_Click"/>
        <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
            <ItemTemplate>
                <div style="padding:20px; background-color:white; margin:5px; box-shadow:rgba(128, 128, 128, 0.5) 3px 3px 5px">
                    <p>
                        <asp:Label runat="server" Text='<%# Eval("name") %>'></asp:Label>
                        <asp:Label runat="server" Text='<%# " (" + Eval("id") + ")" %>'></asp:Label>
                    </p>
                    <p>monthly salary: <asp:Label ID="Label1" runat="server" Text='<%# Eval("salary") %>'></asp:Label></p>
                    <p>counted leave days: <asp:Label ID="Label3" runat="server" Text='<%# Eval("total_leave_days") %>'></asp:Label></p>
                    <p>Salary: <asp:Label ID="salary" runat="server" Text=""></asp:Label></p>
                    <asp:Button ID="detailsBtn" runat="server" Text="See Details" CommandArgument='<%# Eval("id") %>' OnClick="detailsBtn_Click"/>
                    <asp:Button ID="printBtn" runat="server" Text="Print" CommandArgument='<%# Eval("id") %>' OnClick="printBtn_Click" />

                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:potato_dbConnectionString %>" ProviderName="<%$ ConnectionStrings:potato_dbConnectionString.ProviderName %>" 
        SelectCommand="SELECT employee.id, (fname || ' ' || lname) as name, 
            dept, salary, SUM(days) as leave_days, start_date, end_date 
            FROM employee, leave 
            WHERE employee.id = leave.empid and leave.status = 2 and leave.start
            GROUP BY employee.id;">
    </asp:SqlDataSource>
</asp:Content>
