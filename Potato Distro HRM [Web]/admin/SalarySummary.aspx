<%@ Page Title="" Language="C#" MasterPageFile="~/template/Admin.Master" AutoEventWireup="true" CodeBehind="SalarySummary.aspx.cs" Inherits="Potato_Distro_HRM__Web_.admin.SalarySummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>Employee ID: </td>
            <td>: <asp:Label ID="empid" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Name</td>
            <td>: <asp:Label ID="empname" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Department: </td>
            <td>: <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Monthly Salary</td>
            <td>: <asp:Label ID="monthlySalary" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Total leave days</td>
            <td>: <asp:Label ID="leaveDays" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
</asp:Content>
