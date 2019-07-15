<%@ Page Title="" Language="C#" MasterPageFile="~/template/Admin.Master" AutoEventWireup="true" CodeBehind="SalarySummary.aspx.cs" Inherits="Potato_Distro_HRM__Web_.admin.SalarySummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>Employee ID </td>
            <td>: <asp:Label ID="empid" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Name</td>
            <td>: <asp:Label ID="empname" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Department </td>
            <td>: <asp:Label ID="dept" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="2">Pay Period: <asp:Label ID="payPeriod" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Monthly Salary</td>
            <td>: <asp:Label ID="monthlySalary" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>DEDUCTIONS</td><td></td>
        </tr>
        <tr>
            <td>Leave</td>
            <td>: <asp:Label ID="leave" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Net Salary</td>
            <td>: <asp:Label ID="netSalary" runat="server" Text=""></asp:Label></td>
        </tr>

    </table>
</asp:Content>
