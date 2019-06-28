<%@ Page Title="" Language="C#" MasterPageFile="~/template/Admin.Master" AutoEventWireup="true" CodeBehind="ManageLeaveForm.aspx.cs" Inherits="Potato_Distro_HRM__Web_.ManageLeaveForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/patternfly/3.24.0/css/patternfly.min.css">
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/patternfly/3.24.0/css/patternfly-additions.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid container-cards-pf">
        <div class="row row-cards-pf">
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1" >
                <ItemTemplate>
                    <div class="col-xs-12 col-sm-12 col-md-6" style="max-height:200px; ">
                        <div class="card-pf card-pf-view card-pf-view-xs" style="position:relative; height:90%;">
                            <div class="card-pf-body" style="height:80%; overflow: hidden;">
                                <div style="float:right">
                                    <asp:LinkButton ID="Button1" runat="server" CssClass="pficon pficon-ok" Font-Size="X-Large" CommandArgument='<%# Eval("id") %>' CommandName="LeaveId" OnClick="ApproveBtnClicked"/>
                                    <br /><br />
                                    <asp:LinkButton ID="Button2" runat="server" CssClass="pficon pficon-error-circle-o" Font-Size="X-Large" CommandArgument='<%# Eval("id") %>' CommandName="LeaveId" OnClick="RejectBtnClicked"/>
                                </div>
                                <h2 class="card-pf-title" style="margin-top:0">
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("fullname") %>'></asp:Label>
                                    <asp:Label ID="Label7" runat="server" Text='<%# " (" + Eval("empid") + ") " %>'></asp:Label>
                                </h2>
                                <p class="card-pf-info">
                                    <strong>Start</strong>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("start", "{0:d}") %>'></asp:Label>
                                    <br/>
                                    <strong>Days</strong>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("days") + " days" %>' />
                                    <br />
                                    <strong>Reason</strong>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("reason") %>' Style="padding:1px 0;"/>
                                </p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="EmptyLbl" runat="server" Text="No more leave applications to manage" Visible='<%#bool.Parse((Repeater1.Items.Count==0).ToString())%>'></asp:Label>
                </FooterTemplate>
            </asp:Repeater>                        
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:potato_dbConnectionString %>" ProviderName="<%$ ConnectionStrings:potato_dbConnectionString.ProviderName %>" 
        SelectCommand="select LEAVE.empid, LEAVE.id, T.fullname, LEAVE.start, LEAVE.days, LEAVE.reason 
            FROM LEAVE INNER JOIN 
            (
	            select EMPLOYEE.id, concat(EMPLOYEE.fname, ' ', EMPLOYEE.lname) AS fullname from EMPLOYEE
            ) AS T
            ON LEAVE.empid = T.id
            WHERE LEAVE.status=1 
            ORDER BY LEAVE.start DESC">

    </asp:SqlDataSource>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/patternfly/3.24.0/js/patternfly.min.js"></script>
</asp:Content>
