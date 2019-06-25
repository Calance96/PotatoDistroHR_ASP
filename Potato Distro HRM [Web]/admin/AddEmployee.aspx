<%@ Page Title="" Language="C#" MasterPageFile="~/template/Admin.Master" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="Potato_Distro_HRM__Web_.AddEmployee" UnobtrusiveValidationMode="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #add-form {
            width: 50%;
            margin: 0 auto;
        }
        
        .bold {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="add-form">
        <hr /><div class="bold h2">Employee Personal Details</div><hr />
        <asp:Panel ID="empMessagePanel" runat="server" Visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <asp:Label ID="empMessageLabel" runat="server" />
        </asp:Panel>
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label CssClass="bold" runat="server">First name</asp:Label>
                <asp:TextBox CssClass="form-control" ID="firstName" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="firstName" ForeColor="Red" ErrorMessage="*Required" runat="server"/>
            </div>
            <div class="form-group col-md-6">
                <asp:Label CssClass="bold" runat="server">Last name</asp:Label>
                <asp:TextBox CssClass="form-control" ID="lastName" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="lastName" ForeColor="Red" ErrorMessage="*Required" runat="server"/>
            </div>
        </div>
        <div class="form-row">
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label CssClass="bold" runat="server">Contact</asp:Label>
                    <asp:TextBox CssClass="form-control" ID="contact" runat="server" />
                    <asp:RequiredFieldValidator ControlToValidate="contact" ForeColor="Red" ErrorMessage="*Required" runat="server"/>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="bold" runat="server">Address</asp:Label>
                    <asp:TextBox CssClass="form-control" ID="address" runat="server" TextMode="MultiLine" />
                    <asp:RequiredFieldValidator ControlToValidate="address" ForeColor="Red" ErrorMessage="*Required" runat="server"/>
                </div>
                <div class="form-group">
                    <div>
                        <asp:Label CssClass="bold" runat="server">Gender</asp:Label>
                    </div>
                    <div class="form-check form-check-inline">
                        <asp:RadioButton ID="rbMale" CssClass="form-check-input" runat="server" GroupName="gender" Checked="true"/>
                        <asp:Label ID="lblMale" CssClass="form-check-label" runat="server" Text="Male" />
                    </div>
                    <div class="form-check form-check-inline">
                        <asp:RadioButton ID="rbFemale" CssClass="form-check-input" GroupName="gender" runat="server"/>
                        <asp:Label ID="lblFemale" CssClass="form-check-label" runat="server" Text="Female" />
                    </div>
                </div>   
            </div>
            <div class="col-md-6">
                <asp:Label CssClass="bold" runat="server">Birthdate</asp:Label>
                <input id="birthDate" runat="server" class="form-control"/>
                <asp:RequiredFieldValidator ControlToValidate="birthDate" ForeColor="Red" ErrorMessage="*Required" runat="server"/>
           </div>
        </div>
        <br /> <hr />   
        <h2 class="bold">Job Details</h2><hr />
        <asp:Panel ID="jobMessagePanel" runat="server" Visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <asp:Label ID="jobMessageLabel" runat="server" />
        </asp:Panel>
        <div class="form-row">
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label CssClass="bold" runat="server">Department</asp:Label>
                    <asp:DropDownList ID="dropdownDept" CssClass="form-control" runat="server" DataSourceID="DepartmentSource" DataTextField="name" DataValueField="id"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="bold" runat="server">Supervisor</asp:Label>
                    <asp:DropDownList ID="dropdownSuper" CssClass="form-control" runat="server" DataSourceID="SupervisorSource" DataTextField="name" DataValueField="id" AppendDataBoundItems="true">
                        <asp:ListItem Text="[Select a supervisor]" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="bold" runat="server">Salary</asp:Label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text">RM</span>
                        </div>
                        <asp:TextBox ID="salary" cssClass="form-control" runat="server" />
                    </div>
                    <asp:RequiredFieldValidator ControlToValidate="salary" ForeColor="Red" ErrorMessage="*Required" runat="server"/>
                    <asp:RangeValidator runat="server" MinimumValue="0" MaximumValue="99999" ControlToValidate="salary" ErrorMessage="Salary cannot be negative!" ForeColor="Red"/>
                </div>
            </div>
            <div class="col-md-6">
                <asp:Label CssClass="bold" runat="server">Start date</asp:Label>
                <input id="startDate" type="text" runat="server" class="form-control"/>
                <asp:RequiredFieldValidator ControlToValidate="firstName" ForeColor="Red" ErrorMessage="*Required" runat="server"/>
            </div>
        </div>
        <br /><hr />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click"/>
        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClear_Click" CausesValidation="false"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" CausesValidation="false"/>
        <asp:SqlDataSource ID="DepartmentSource" runat="server" ConnectionString="<%$ ConnectionStrings:potato_dbConnectionString %>" ProviderName="<%$ ConnectionStrings:potato_dbConnectionString.ProviderName %>" SelectCommand="SELECT * FROM &quot;department&quot;"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SupervisorSource" runat="server" ConnectionString="<%$ ConnectionStrings:potato_dbConnectionString %>" ProviderName="<%$ ConnectionStrings:potato_dbConnectionString.ProviderName %>" SelectCommand="SELECT id, (fname || ' ' || lname) as name FROM Employee;"></asp:SqlDataSource>
    </div>
    <script>
        $(document).ready(function () {
            $(function () {
                $("#<%=birthDate.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    showButtonPanel: true,
                    yearRange: "-60:-16",
                });

                $("#<%=startDate.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    showButtonPanel: true,
                    yearRange: "-50:+1",
                });

                $("#<%=startDate.ClientID%>").datepicker("setDate", new Date());
            });
        });
    </script>
</asp:Content>
