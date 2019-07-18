<%@ Page Title="" Language="C#" MasterPageFile="~/template/Admin.Master" AutoEventWireup="true" CodeBehind="ManagePayroll.aspx.cs" Inherits="Potato_Distro_HRM__Web_.admin.ManagePayroll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker3.min.css"/>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <style>
        .optionBtn {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            var today = new Date()
            var date_input = $('input[name="date"]'); //our date input has the name "date"
            var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
            var options = {
                format: "MM yyyy",
                minViewMode: 1,
                maxViewMode: 2,
                autoclose: true,
                container: container,
                defaultViewDate: { year: today.getFullYear(), month: today.getMonth(), day: 1 }
            };
            date_input.datepicker(options);
        })

        function displayMessage(printContent) {
            var inf = printContent;
            win = window.open("print.htm", 'popup', 'toolbar = no, status = no');
            win.document.write(inf);
            win.document.close(); // new line 
        } 
    </script>
    <div id="gridview">
<%--        <asp:Button ID="Button1" runat="server" Text="Button" Onclick="Button1_Click"/>--%>
        <asp:Button ID="printAllBtn" runat="server" Text="Print all" OnClick="printAllBtn_Click"/>
        <div class="form-group">
            <!-- Date input -->
            <input class="form-control" style="width:200px; display:inline" id="date" name="date" placeholder="Choose Month" type="text" />
            <asp:Button ID="monthBtn" runat="server" Text="Submit" CausesValidation="false" OnClick="monthBtn_Click"/>
        </div>
        <div class="form-group" style="position: absolute; right: 10%; width:20%;">
            <b>Filter by </b>
            <br /><br />
            Name: <asp:TextBox ID="NameTb" runat="server" CssClass="form-control" placeholder="Employee Name"></asp:TextBox>
            <br /><br />
            Department: <asp:DropDownList ID="DeptDdl" runat="server" CssClass="form-control"></asp:DropDownList>
            <br /><br />
            <asp:Button ID="filterBtn" runat="server" Text="Filter" CausesValidation="false" OnClick="filterBtn_Click"/>
            <asp:Button ID="clearFilterBtn" runat="server" Text="Clear filter" CausesValidation="false" OnClick="clearFilterBtn_Click"/>
        </div>
        <div style="font-weight:bold; padding: 20px;"><asp:Label ID="monthLbl" runat="server" Text=""></asp:Label></div>
        <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound" OnDataBound="ListView1_DataBound" >
            <ItemTemplate>
                <div style="padding:20px; background-color:white; width:60%; margin:5px; box-shadow:rgba(128, 128, 128, 0.5) 3px 3px 5px">
                    <table style="width:100%">
                        <tr>
                            <td style="width:70%">
                                <p>
                                    <b>
                                        <asp:Label runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                        <asp:Label runat="server" Text='<%# " (" + Eval("id") + ")" %>'></asp:Label>
                                        <asp:Label runat="server" Text='<%# " - " + Eval("dept") %>'></asp:Label>
                                    </b>
                                </p>
                                <p>Monthly Salary: <asp:Label ID="Label1" runat="server" Text='<%# Eval("salary") %>'></asp:Label></p>
            <%--                    <p>counted leave days: <asp:Label ID="Label3" runat="server" Text='<%# Eval("total_leave_days") %>'></asp:Label></p>--%>
                                <p>Net Salary: <span style="color:forestgreen"> RM<asp:Label ID="salary" runat="server" Text=""></asp:Label></span></p>
                            </td>
                            <td style="width:20%">
                                <asp:LinkButton ID="detailsBtn" runat="server" CssClass="optionBtn" CommandArgument='<%# Eval("id") + "," + Eval("total_leave_days") %>' OnClick="detailsBtn_Click" CausesValidation="False">
                                    <asp:Image ID="Image1" Height="20px" runat="server" Style="padding-right:5px;" ImageUrl="~/resources/images/details_icon.png" />
                                    See Details
                                </asp:LinkButton>
                                <br />
                                <%--<asp:LinkButton ID="printBtn" runat="server" CssClass="optionBtn" CommandArgument='<%# Eval("id") %>' OnClientClick="displayMessage(printarea.innerHTML)" CausesValidation="False">
                                    <asp:Image ID="Image2" Height="20px" runat="server" Style="padding-right:5px;" ImageUrl="~/resources/images/print_icon.png" />
                                    Print
                                </asp:LinkButton>--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
