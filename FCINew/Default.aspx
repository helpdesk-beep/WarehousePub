<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FCI_Master_New.master" AutoEventWireup="true" CodeFile="~/FCINew/Default.aspx.cs" Inherits="FCI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="container-fluid">

    <!-- DASHBOARD CARDS -->
    <div class="row">

        <!-- Total Employees -->
        <div class="col-md-3 mb-4">
            <div class="card shadow border-0 text-white" style="background: linear-gradient(135deg,#4e73df,#224abe);">
                <div class="card-body">
                    <h5>Total Employees</h5>
                    <h2><asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label></h2>
                </div>
            </div>
        </div>

        <!-- Active -->
        <div class="col-md-3 mb-4">
            <div class="card shadow border-0 text-white" style="background: linear-gradient(135deg,#1cc88a,#13855c);">
                <div class="card-body">
                    <h5>Active Employees</h5>
                    <h2><asp:Label ID="lblActive" runat="server" Text="0"></asp:Label></h2>
                </div>
            </div>
        </div>

        <!-- Inactive -->
        <div class="col-md-3 mb-4">
            <div class="card shadow border-0 text-white" style="background: linear-gradient(135deg,#e74a3b,#c0392b);">
                <div class="card-body">
                    <h5>Inactive Employees</h5>
                    <h2><asp:Label ID="lblInactive" runat="server" Text="0"></asp:Label></h2>
                </div>
            </div>
        </div>

        <!-- Today -->
        <div class="col-md-3 mb-4">
            <div class="card shadow border-0 text-white" style="background: linear-gradient(135deg,#f6c23e,#dda20a);">
                <div class="card-body">
                    <h5>Today Registered</h5>
                    <h2><asp:Label ID="lblToday" runat="server" Text="0"></asp:Label></h2>
                </div>
            </div>
        </div>

    </div>

    <!-- RECENT EMPLOYEE LIST -->
    <div class="card shadow border-0">
        <div class="card-header bg-dark text-white">
            <h5 class="mb-0"><i class="fa fa-users"></i> Recent Employees</h5>
        </div>
        <div class="card-body p-0">

            <asp:GridView ID="gvRecent" runat="server" AutoGenerateColumns="false"
                CssClass="table table-bordered table-hover mb-0">

                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <%# HttpUtility.HtmlEncode(Eval("Emp_Name") ?? "") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile">
                        <ItemTemplate>
                            <%# HttpUtility.HtmlEncode(Eval("Mobile_No") ?? "") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="District">
                        <ItemTemplate>
                            <%# HttpUtility.HtmlEncode(Eval("District_Name") ?? "") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch">
                        <ItemTemplate>
                            <%# HttpUtility.HtmlEncode(Eval("DepotName") ?? "") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class='badge <%# (Eval("Status") != DBNull.Value && Convert.ToBoolean(Eval("Status"))) ? "bg-success" : "bg-danger" %>'>
                                <%# (Eval("Status") != DBNull.Value && Convert.ToBoolean(Eval("Status"))) ? "Active" : "Inactive" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>

        </div>
    </div>

</div>
</asp:Content>