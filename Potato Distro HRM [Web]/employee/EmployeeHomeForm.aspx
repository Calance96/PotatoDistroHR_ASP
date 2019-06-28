<%@ Page Title="" Language="C#" MasterPageFile="~/template/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeHomeForm.aspx.cs" Inherits=" Potato_Distro_HRM__Web_.EmployeeHomeForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container{
            display:table-cell;
            vertical-align:middle;
        }
        .overlay {
            transition: all 0.5s;
            opacity:0.8;
            padding:50px;
            background-color:rgba(160,160,160,0.5);
        }
        .overlay:hover {
            opacity:1;
            padding-left: 60px;
            padding-right:40px;
            background-color:rgba(0,0,0,0.5);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron-fluid" style="background:url(../resources/images/jumbo_background.jpg); height:400px; margin-bottom:40px;">
        <div class="overlay" style="height:100%; width:100%;  display:table;">
            <div class="container">
                <h1 class="display-4" style="color:white">Welcome, <asp:Label ID="empName" runat="server" Text=""></asp:Label> !</h1>
                <p class="lead" style="color:white">Nice to have you back at Potato Distro! We hope you have a productive day ahead.</p>
            </div>
        </div>
    </div>
    <div class="jumbotron-fluid" style="background:url(../resources/images/jumbo_background_2.jpg); height:400px; margin-bottom:40px;">
        <div class="overlay" style="height:100%; width:100%;  display:table;">
            <div class="container">
                <h1 class="display-4" style="color:white">Potato Distro, always ready to deliver.</h1>
                <p class="lead" style="color:white">We pride ourselves on being the best software distributor within a ten kilometre radius.</p>
            </div>
        </div>
    </div>
    <div class="jumbotron-fluid" style="background:url(../resources/images/jumbo_background_3.jpg); height:400px; margin-bottom:40px;">
        <div class="overlay" style="height:100%; width:100%;  display:table;">
            <div class="container">
                <h1 class="display-4" style="color:white">Peace</h1>
                <p class="lead" style="color:white">Here at Potato Distro, our employees' are always well-rested and happy. Unlike the students of Xiamen University.</p>
            </div>
        </div>
    </div>
    <%--<asp:Button ID="LeaveBtn" runat="server" Text="Leave" OnClick="LeaveBtn_Click" />
    <asp:Button ID="ChangePwdBtn" runat="server" Text="Change Password" OnClick="ChangePwdBtn_Click" />--%>
</asp:Content>
