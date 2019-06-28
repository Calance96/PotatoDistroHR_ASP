<%@ Page Title="" Language="C#" MasterPageFile="~/template/Employee.Master" AutoEventWireup="true" CodeBehind="LeaveHistoryForm.aspx.cs" Inherits="Potato_Distro_HRM__Web_.LeaveHistoryForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .text-danger{
            font-size:12px;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/patternfly/3.24.0/css/patternfly.min.css">
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/patternfly/3.24.0/css/patternfly-additions.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div id="applyLeaveDiv" style="float:right; position:sticky; top:50px;">
            <div style="margin:0 auto; text-align:center">
                <div class="h4">New Leave Application</div>
                <asp:Label ID="SubmittedLabel" runat="server" Text="" CssClass="text-danger"></asp:Label>
            </div>
            <asp:FormView runat="server" OnItemInserted="FormView_ItemInserted" DataSourceID="SqlDataSource1" DefaultMode="Insert">
                <InsertItemTemplate>
                    <table>
                        <tr>
                            <td style="text-align:center;">
                                <asp:Calendar ID="StartInput" runat="server" SelectedDate='<%# Bind("start") %>' BackColor="White" 
                                              BorderColor="Transparent" 
                                              BorderStyle="None" 
                                              CellSpacing="1" 
                                              Font-Names="Verdana" 
                                              Font-Size="9pt" 
                                              ForeColor="Black" 
                                              Height="250px" 
                                              Width="300px" 
                                              NextPrevFormat="ShortMonth" 
                                              SelectionMode="Day">
                                   <SelectedDayStyle BackColor="#4D6D9A" 
                                                     ForeColor="White" />
                                   <DayStyle BackColor="White"/>
                                   <NextPrevStyle Font-Bold="True" 
                                                  Font-Size="8pt" 
                                                  ForeColor="White" />
                                   <DayHeaderStyle Font-Bold="True" 
                                                   Font-Size="8pt" 
                                                   ForeColor="#333333" 
                                                   Height="8pt" />
                                   <TitleStyle BackColor="#4D6D9A" 
                                               BorderStyle="None" 
                                               Font-Bold="True" 
                                               Font-Size="12pt"
                                   ForeColor="White" Height="12pt" />
                                   <OtherMonthDayStyle BackColor="White" 
                                                       Font-Bold="False" 
                                                       ForeColor="DarkGray"/>

                                </asp:Calendar>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="This field is required" OnServerValidate="CalendarValidation" CssClass="text-danger"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="DaysInput" runat="server" TextMode="Number" Text='<%# Bind("days") %>' CssClass="form-control" placeholder="Total Days"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This field is required" ControlToValidate="DaysInput" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="ReasonInput" runat="server" TextMode="MultiLine" Text='<%# Bind("reason") %>' Height="100px" CssClass="form-control" placeholder="Reason"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This field is required" ControlToValidate="ReasonInput" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                                <asp:Button ID="SubmitBtn" runat="server" Text="Submit" CausesValidation="true" CommandName="Insert" CssClass="btn btn-primary"/>
                                <asp:Button runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel" CssClass="btn btn-secondary"/>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
        </div>

        <nav>
            Filter by: 
            <asp:LinkButton ID="LinkPending" runat="server" CommandArgument='pending' CausesValidation="false" OnClick="ShowList">Pending</asp:LinkButton>
            <asp:LinkButton ID="LinkApproved" runat="server" CommandArgument='approved' CausesValidation="false" OnClick="ShowList">Approved</asp:LinkButton>
            <asp:LinkButton ID="LinkRejected" runat="server" CommandArgument='rejected' CausesValidation="false" OnClick="ShowList">Rejected</asp:LinkButton>
            <asp:LinkButton ID="LinkCancelled" runat="server" CommandArgument='cancelled' CausesValidation="false" OnClick="ShowList">Cancelled</asp:LinkButton>
        </nav>
        <asp:LinkButton ID="LinkClear" runat="server" CausesValidation="false" OnClick="ClearFilter">Clear filter</asp:LinkButton>

        <div style="width: 60%; margin-top:40px;">
            <div>
                <asp:Label ID="LblStatus" runat="server" Text="" CssClass="h4"></asp:Label>
                <div class="container-fluid container-cards-pf">
                    <div class="row row-cards-pf">
                        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1" OnItemDataBound="Repeater1_ItemDataBound" >
                            <ItemTemplate>
                                <div class="col-xs-12 col-sm-12 col-md-4" style="max-height:200px; ">
                                    <div class="card-pf card-pf-view card-pf-view-xs" style="position:relative; height:90%;">
                                        <asp:Panel runat="server" class="CancelCell" Visible='<%#bool.Parse(((int)Eval("status") == 1).ToString())%>' style="position:absolute; right:5px; top:5px;">
                                            <asp:LinkButton runat="server" CausesValidation="false" CssClass="pficon pficon-close" CommandArgument='<%# Eval("id") %>' CommandName="LeaveId" OnClick="CancelBtnClicked"></asp:LinkButton>
                                        </asp:Panel>
                                        <div class="card-pf-body" style="height:80%; overflow: hidden;">
                                            <div class="card-pf-heading-kebab">
                                                <h2 class="card-pf-title" style="margin-top:0">
                                                    <asp:Label ID="statusIcon" runat="server" CssClass="pficon"></asp:Label>
                                                    <span class="h4"><b><asp:Label ID="startLabel" runat="server" Text='<%# Eval("start", "{0:d}") %>' /></b></span>
                                                    <div class="h6"><asp:Label ID="daysLabel" runat="server" Text='<%# Eval("days") + " days" %>' /></div>
                                                </h2>
                                            </div>
                                            <span class="h5"><asp:Label ID="reasonLabel" runat="server" Text='<%# Eval("reason") %>' Style="padding:1px 0;"/></span>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>                        
                    </div>
                </div>
            </div>
            
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:potato_dbConnectionString %>" ProviderName="<%$ ConnectionStrings:potato_dbConnectionString.ProviderName %>"
                InsertCommand="insert into LEAVE (empid, start, days, reason, status) values (@empid, @start, @days, @reason, @status)">
            <InsertParameters>
                <asp:Parameter Name="empid" Type="Int32" />
                <asp:Parameter Name="start" Type="DateTime" />
                <asp:Parameter Name="days" Type="Int32" />
                <asp:Parameter Name="reason" Type="String" />
                <asp:Parameter Name="status" Type="Int32" DefaultValue="1"/>
            </InsertParameters>
        </asp:SqlDataSource>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/patternfly/3.24.0/js/patternfly.min.js"></script>
</asp:Content>
