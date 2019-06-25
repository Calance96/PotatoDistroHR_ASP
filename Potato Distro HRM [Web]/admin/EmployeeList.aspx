<%@ Page Title="" Language="C#" MasterPageFile="~/template/Admin.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="Potato_Distro_HRM__Web_.admin.EmployeeList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #accordion {
            margin-bottom: 10px;
        }

        .ui-accordion-header {
            background: #5D7B9D;
            color: white;
        }

        .my-auto {
            padding: 5px;
        }

        button {
            padding: 0;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="accordion">
        <h3>Search</h3>
        <div>
            <div class="row">
                <div class="col-md-2 my-auto">
                    <asp:Label runat="server" Text="Search Input" />
                </div>
                <div class="col-md-5 my-auto">
                    <asp:TextBox ID="searchBox" runat="server" placeholder="Type search term here..." CssClass="form-control"/>
                </div>
                <div class="col-md-1 my-auto">
                    <button runat="server" id="btnSearch" class="btn btn-primary" onserverclick="SearchBtn_OnClick">
                        <i class="fas fa-search" style="color:white"></i>
                    </button> 
                    <asp:LinkButton ID="btnClearSearch" runat="server" CssClass="btn btn-dark" ToolTip="Clear Searches" Enabled="False" OnClick="ClearSearchBtn_onClick">
                        <i class="far fa-trash-alt" style="color:white"></i>
                    </asp:LinkButton>
                </div>
                <div class="col-md-1 my-auto">
                    <div class="text-truncate">Search by</div>
                </div>
                <div class="col-md-2 my-auto">
                    <asp:DropDownList ID="searchCriteria" runat="server" CssClass="form-control">
                        <asp:ListItem Text="First name" Value="1" />
                        <asp:ListItem Text="Last name" Value="2" />
                        <asp:ListItem Text="Department" Value="3" />
                        <asp:ListItem Text="Supervisor" Value="4" />
                        <asp:ListItem Text="Status" Value="5" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="zeroResultPanel" runat="server" CssClass="text-center" Visible="false"><span class="h4 text-secondary">No results found.</span></asp:Panel>
    <div id="gridview">
        <asp:GridView ID="employeeGridView" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" DataKeyNames="id"  OnDataBound="EmployeeGridView_DataBound" AllowSorting="True" OnSorting="employeeGridView_Sorting">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="id" >
                    <ItemTemplate>
                        <asp:Label ID="empId" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="First Name" SortExpression="fname">
                    <ItemTemplate>
                        <asp:Label ID="fname" runat="server" Text='<%# Bind("fname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name" SortExpression="lname">
                    <ItemTemplate>
                        <asp:Label ID="lname" runat="server" Text='<%# Bind("lname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Birthdate" SortExpression="bdate">
                    <ItemTemplate>
                        <asp:Label ID="birthDate" runat="server" Text='<%# Bind("bdate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact" SortExpression="contact">
                    <ItemTemplate>
                        <asp:Label ID="contact" runat="server" Text='<%# Bind("contact") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supervisor" SortExpression="supervisor">
                    <ItemTemplate>
                        <asp:Label ID="supervisor" runat="server" Text='<%# Bind("supervisor") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Starts On" SortExpression="start_date">
                    <ItemTemplate>
                        <asp:Label ID="startDate" runat="server" Text='<%# Bind("start_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ends On" SortExpression="end_date">
                    <ItemTemplate>
                        <asp:Label ID="endDate" runat="server" Text='<%# Bind("end_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salary" SortExpression="salary">
                    <ItemTemplate>
                        <asp:Label ID="salary" runat="server" Text='<%# Bind("salary") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department" SortExpression="dept">
                    <ItemTemplate>
                        <asp:Label ID="department" runat="server" Text='<%# Bind("dept") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="editBtn" runat="server" CssClass="btn btn-success btn-sm" CommandArgument='<%#Eval("id") %>' OnCommand="EditCustomer">
                            <i class="fas fa-user-edit"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="deleteBtn" runat="server" CssClass="btn btn-danger btn-sm">
                            <i class="fas fa-user-slash"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </div>

    <asp:SqlDataSource ID="SupervisorDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:potato_dbConnectionString %>" ProviderName="<%$ ConnectionStrings:potato_dbConnectionString.ProviderName %>" SelectCommand="select id, (fname || ' ' || lname) as name FROM Employee"></asp:SqlDataSource>

    <asp:SqlDataSource ID="DepartmentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:potato_dbConnectionString %>" ProviderName="<%$ ConnectionStrings:potato_dbConnectionString.ProviderName %>" SelectCommand="SELECT &quot;id&quot;, &quot;name&quot; FROM &quot;department&quot;"></asp:SqlDataSource>

    <script>
        $(document).ready(function () {
            $(function () {
                $("#accordion").accordion({
                    heightStyle: "content"
                });
            });

            $('#<%=searchBox.ClientID%>').keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13') {
                    $("#btnSearch").click();
                }
                return true;
            });
        });

        


    </script>
</asp:Content>
