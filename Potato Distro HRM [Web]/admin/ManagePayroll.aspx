<%@ Page Title="" Language="C#" MasterPageFile="~/template/Admin.Master" AutoEventWireup="true" CodeBehind="ManagePayroll.aspx.cs" Inherits="Potato_Distro_HRM__Web_.admin.ManagePayroll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker3.min.css"/>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
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
    </script>
    <div id="gridview">
<%--        <asp:Button ID="Button1" runat="server" Text="Button" Onclick="Button1_Click"/>--%>
        <div class="form-group">
            <!-- Date input -->
            <label class="control-label" for="date">Date</label>
            <input class="form-control" id="date" name="date" placeholder="Choose Month" type="text" />
        </div>
        <div class="form-group">
            <!-- Submit button -->
            <asp:Button ID="monthBtn" CssClass="btn btn-primary" runat="server" Text="Submit" CausesValidation="false" OnClick="monthBtn_Click"/>
<%--            <button class="btn btn-primary " name="submit" type="submit">Submit</button>--%>
        </div>
        <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
            <ItemTemplate>
                <div style="padding:20px; background-color:white; margin:5px; box-shadow:rgba(128, 128, 128, 0.5) 3px 3px 5px">
                    <p>
                        <asp:Label runat="server" Text='<%# Eval("name") %>'></asp:Label>
                        <asp:Label runat="server" Text='<%# " (" + Eval("id") + ")" %>'></asp:Label>
                    </p>
                    <p>monthly salary: <asp:Label ID="Label1" runat="server" Text='<%# Eval("salary") %>'></asp:Label></p>
                    <p>counted leave days: <asp:Label ID="Label3" runat="server" Text='<%# Eval("total_leave_days") %>'></asp:Label></p>
                    <p>Salary: <asp:Label ID="salary" runat="server" Text=""></asp:Label></p>
                    <asp:Button ID="detailsBtn" runat="server" Text="See Details" CommandArgument='<%# Eval("id") + "," + Eval("total_leave_days") %>' OnClick="detailsBtn_Click"/>
                    <asp:Button ID="printBtn" runat="server" Text="Print" CommandArgument='<%# Eval("id") %>' OnClick="printBtn_Click" />

                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
