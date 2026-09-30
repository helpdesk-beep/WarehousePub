<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master"
    AutoEventWireup="true"
    CodeFile="District_wise_paid_One_RS_payment_From_MPSCSC_New.aspx.cs"
    Inherits="Reports_NewBillingReports_District_wise_paid_One_RS_payment_From_MPSCSC_New" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageHead" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageBody" runat="server">

    <div class="content-wrapper">
        <div class="container-fluid">

            <!-- ================= FILTER SECTION ================= -->
            <div class="card mb-3">
                <div class="text-center mb-4">
                    <h3 class="fw-bold">Godown Wise Details – One Rupee Payment From MPSCSC to MPWLC</h3>
                </div>

                <div class="card-body">
                    <div class="row align-items-end justify-content-center">
                        <div class="col-md-2 col-lg-2">
                            <div class="form-group">
                                <label>District Name</label>
                            </div>
                        </div>
                        <div class="col-md-4 col-lg-3">
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

        <!-- ================= GRID SECTION ================= -->
        <asp:Panel ID="grdone" runat="server" Visible="false">
            <div class="card">

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GrdBills"
                            runat="server"
                            AutoGenerateColumns="False"
                            DataKeyNames="Bill_Number"
                            CssClass="table table-bordered table-striped table-hover"
                            ShowFooter="true">

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

                                <asp:TemplateField HeaderText="Amount Received">
                                    <ItemTemplate>
                                        <%# Eval("Payable_Amount") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="HO Deduction">
                                    <ItemTemplate>
                                        <%# Eval("Deduction_by_HO_MPSCSC") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TDS Deduction">
                                    <ItemTemplate>
                                        <%# Eval("TDS_Deduction_by_HO_MPSCSC") %>
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

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageWidget" runat="server" />
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GrdBills.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>


