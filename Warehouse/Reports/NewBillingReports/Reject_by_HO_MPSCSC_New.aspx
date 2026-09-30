<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master"
    AutoEventWireup="true"
    CodeFile="Reject_by_HO_MPSCSC_New.aspx.cs"
    Inherits="Reports_NewBillingReports_Reject_by_HO_MPSCSC_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="server">

    <div class="content-wrapper">
        <div class="container-fluid">

            <!-- ================= FILTER SECTION ================= -->
            <div class="card mb-3">
                <div class="card-header bg-primary text-white">
                    <h5 class="mb-0">Godown Wise Rejected Bills by HO MPSCSC
                    </h5>
                </div>

                <div class="card-body">
                    <div class="row align-items-center justify-content-center">

                        <div class="col-md-4 col-lg-4">
                            <div class="form-group d-flex align-items-center">
                                <label class="mb-0 me-2" style="min-width: 120px;">
                                    District Name
                                </label>

                                <asp:DropDownList ID="ddldistrict"
                                    runat="server"
                                    CssClass="form-control show-loader-ddl"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>

            </div>

            <!-- ================= GRID SECTION ================= -->
            <asp:Panel ID="grdone" runat="server" Visible="false">
                <div class="card">
                    <div class="card-header d-flex justify-content-between align-items-center">
                        <h6 class="mb-0">Rejected Bill Details</h6>

                        <asp:Button ID="btnExport"
                            runat="server"
                            Text="Export To Excel"
                            CssClass="btn btn-success btn-sm"
                            OnClick="btnExport_Click" />
                    </div>

                    <div class="card-body">
                        <div class="table-responsive">

                            <asp:GridView ID="GrdBills"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames="Bill_Number"
                                CssClass="table table-bordered table-striped table-hover"
                                ShowFooter="true"
                                FooterStyle-Font-Bold="true">

                                <Columns>

                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Region">
                                        <ItemTemplate>
                                            <%# Eval("Regionnm") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="District Name">
                                        <ItemTemplate>
                                            <%# Eval("District_Name") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Branch Name">
                                        <ItemTemplate>
                                            <%# Eval("DepotName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Godown Name">
                                        <ItemTemplate>
                                            <%# Eval("Godown_Name") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill No">
                                        <ItemTemplate>
                                            <%# Eval("Bill_Number") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <ItemTemplate>
                                            <%# Eval("Net_Amount") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </asp:Panel>

        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="server" />
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GrdBills.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>
