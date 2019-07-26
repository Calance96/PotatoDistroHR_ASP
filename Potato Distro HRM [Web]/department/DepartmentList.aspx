<%@ Page Title="" Language="C#" MasterPageFile="~/template/Admin.Master" AutoEventWireup="true" CodeBehind="DepartmentList.aspx.cs" Inherits="Potato_Distro_HRM__Web_.department.ViewDepartment" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="add-form">
        <hr /><div class="bold h2">Department Employee Details</div><hr />
        <asp:Panel ID="deptMessagePanel" runat="server" Visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <asp:Label ID="deptMessageLabel" runat="server" />
        </asp:Panel>
    </div>
    <div id="accordion">
        <h3>Search</h3>
        <div>
            <div class="row">
                <div class="col-md-2 my-auto">
                    <asp:Label runat="server" Text="Search Input" />
                </div>
                <asp:Panel CssClass="col-md-6 my-auto" runat="server" DefaultButton="btnSearch">
                    <asp:TextBox ID="searchBox" runat="server" placeholder="Type search term here..." CssClass="form-control w-75 d-inline-block" AutoPostBack="false"/>
                    <asp:LinkButton runat="server" ID="btnSearch" CssClass="btn btn-primary d-inline-block" OnClick="SearchBtn_OnClick">
                        <i class="fas fa-search" style="color:white"></i>
                    </asp:LinkButton> 
                    <asp:LinkButton ID="btnClearSearch" runat="server" CssClass="btn btn-dark d-inline-block" ToolTip="Clear Searches" Enabled="False" OnClick="ClearSearchBtn_onClick">
                        <i class="far fa-trash-alt" style="color:white"></i>
                    </asp:LinkButton>
                </asp:Panel>
                <div class="col-md-1 my-auto">
                    <div class="text-truncate">Search by</div>
                </div>
                <div class="col-md-2 my-auto">
                    <asp:DropDownList ID="searchCriteria" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Name" Value="1" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="zeroResultPanel" runat="server" CssClass="text-center" Visible="false"><span class="h4 text-secondary">No results found.</span></asp:Panel>
    <div id="gridview">
        <asp:GridView ID="departmentGridView" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" DataKeyNames="id"  OnDataBound="DepartmentGridView_DataBound" AllowSorting="True" OnSorting="departmentGridView_Sorting">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Department ID" InsertVisible="False" SortExpression="id" >
                    <ItemTemplate>
                        <asp:Label ID="deptId" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department Name" SortExpression="name">
                    <ItemTemplate>
                        <asp:Label ID="deptName" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hotline" SortExpression="hotline">
                    <ItemTemplate>
                        <asp:Label ID="deptHotline" runat="server" Text='<%# Bind("hotline") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Manager Name" SortExpression="managername">
                    <ItemTemplate>
                        <asp:Label ID="managerName" runat="server" Text='<%# Bind("manager") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="editBtn" runat="server" CssClass="btn btn-success btn-sm" CommandArgument='<%#Eval("id") %>' OnCommand="EditDepartment">
                            <i class="fas fa-user-edit"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="deleteBtn" runat="server" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Are you sure you want to delete this department?')" OnCommand="DeleteDepartment">
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

    <asp:SqlDataSource ID="DepartmentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:potato_dbConnectionString %>" ProviderName="<%$ ConnectionStrings:potato_dbConnectionString.ProviderName %>" SelectCommand="SELECT id, name, hotline, manager FROM department WHERE (id &lt;&gt; 0)"></asp:SqlDataSource>

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
