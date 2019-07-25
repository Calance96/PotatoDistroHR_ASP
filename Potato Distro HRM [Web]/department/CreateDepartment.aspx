<%@ Page Title="" Language="C#" MasterPageFile="~/template/Admin.Master" AutoEventWireup="true" CodeBehind="CreateDepartment.aspx.cs" Inherits="Potato_Distro_HRM__Web_.department.AddDepartment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="add-form">
        <hr /><div class="bold h2">Department Details</div><hr />
        <asp:Panel ID="deptMessagePanel" runat="server" Visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <asp:Label ID="deptMessageLabel" runat="server" />
        </asp:Panel>
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label CssClass="bold" runat="server">Department name</asp:Label>
                <asp:TextBox CssClass="form-control" ID="deptName" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="deptName" ForeColor="Red" ErrorMessage="*Required" runat="server"/>
            </div>
        </div>
        <div class="form-row">
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label CssClass="bold" runat="server">Hotline</asp:Label>
                    <asp:TextBox CssClass="form-control" ID="hotline" runat="server" />
                    <asp:RequiredFieldValidator ControlToValidate="hotline" ForeColor="Red" ErrorMessage="*Required" runat="server"/>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="bold" runat="server">Manager</asp:Label>
                    <asp:DropDownList ID="managerDropDownList" CssClass="form-control" runat="server" DataSourceID="ManagerSource" DataTextField="name" DataValueField="id" AppendDataBoundItems="true">
                        <asp:ListItem Text="[Select a manager]" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br /><hr />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click"/>
        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClear_Click" CausesValidation="false"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" CausesValidation="false"/>
        <asp:SqlDataSource ID="ManagerSource" runat="server" ConnectionString="<%$ ConnectionStrings:potato_dbConnectionString %>" ProviderName="<%$ ConnectionStrings:potato_dbConnectionString.ProviderName %>" SelectCommand="SELECT id, (fname || ' ' || lname) as name FROM Employee;"></asp:SqlDataSource>
    </div>
</asp:Content>
