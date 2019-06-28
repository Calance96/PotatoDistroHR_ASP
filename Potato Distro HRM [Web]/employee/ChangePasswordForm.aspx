<%@ Page Title="" Language="C#" MasterPageFile="~/template/Employee.Master" AutoEventWireup="true" CodeBehind="ChangePasswordForm.aspx.cs" Inherits=" Potato_Distro_HRM__Web_.ChangePasswordForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .labelCell{
            text-align:right;
            padding-right:10px;
            vertical-align:top;
        }
        td{
            padding:10px;
        }
        .auto-style1 {
            width: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="border-spacing:20px 40px;">
        <tr>
            <td class="labelCell">Current password</td>
            <td class="auto-style1">
                <asp:TextBox ID="oldPasswordInput" runat="server" TextMode="Password" placeholder="Current password" CssClass="form-control"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This field is required" ControlToValidate="oldPasswordInput" CssClass="text-danger" Font-Size="Small"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="labelCell">New password</td>
            <td class="auto-style1">
                <asp:TextBox ID="newPasswordInput" runat="server" TextMode="Password" placeholder="New password" CssClass="form-control"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="This field is required" CssClass="text-danger" Font-Size="Small" ControlToValidate="newPasswordInput"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="labelCell">Repeat new password</td>
            <td class="auto-style1">
                <asp:TextBox ID="repeatPasswordInput" runat="server" TextMode="Password" placeholder="Repeat new password" CssClass="form-control"></asp:TextBox>
            </td>
            <td>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="This field must match new password" CssClass="text-danger" Font-Size="Small" ControlToValidate="repeatPasswordInput" ControlToCompare="newPasswordInput"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
            <td class="auto-style1">
                <asp:Button ID="SubmitBtn" runat="server" Text="Submit" OnClick="SubmitBtn_Click" CssClass="btn btn-primary"/>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="ResultLbl" runat="server" CssClass="text-danger" Font-Size="Small"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
