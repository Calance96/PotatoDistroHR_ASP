<%@ Page Title="" Language="C#" MasterPageFile="~/template/Admin.Master" AutoEventWireup="true" CodeBehind="SalarySummary.aspx.cs" Inherits="Potato_Distro_HRM__Web_.admin.SalarySummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style>
    th {
        text-align: center;
        border-bottom: 2px solid black;
        border-top: 2px solid black;
    }
    tr{
        height: 40px;
    }
    .empDetailsLabelCol {
        width: 20%;
    }
    .empDetailsValueCol{
        width:30%;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background-color: white; padding:20px;">
        <table style="width:100%;">
            <tr>
                <td colspan="4" style="text-align:center; font-weight:bold">PAYSLIP FOR THE PERIOD OF <asp:Label ID="payPeriod" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <th colspan="4">EMPLOYEE DETAILS</th>
            </tr>
            <tr>
                <td class="empDetailsLabelCol">Employee ID </td>
                <td class="empDetailsValueCol">: <asp:Label ID="empid" runat="server" Text=""></asp:Label></td>
                <td class="empDetailsLabelCol">Department </td>
                <td class="empDetailsValueCol">: <asp:Label ID="dept" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="empDetailsLabelCol">Name</td>
                <td class="empDetailsValueCol">: <asp:Label ID="empname" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr></tr>
            </table>
        <table style="width:100%">
            <tr>
                <th colspan="2">SALARY DETAILS</th>
            </tr>
            <tr>
                <td>Monthly Salary</td>
                <td>: RM<asp:Label ID="monthlySalary" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>DEDUCTIONS</td><td></td>
            </tr>
            <tr>
                <td>Leave</td>
                <td>: RM<asp:Label ID="leave" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr style="border-top: 1px solid black;">
                <td><b>Net Salary</b></td>
                <td><b>: RM<asp:Label ID="netSalary" runat="server" Text=""></asp:Label></b></td>
            </tr>

        </table>
    </div>
    
</asp:Content>
