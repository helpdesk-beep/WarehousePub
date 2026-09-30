<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Godown_Wise_Repush_Bill_Details_New.aspx.cs" Inherits="Reports_NewBillingReports_Godown_Wise_Repush_Bill_Details_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
<div>

    <!-- ===== HEADER ===== -->
    <div class="text-center mb-4">
        <h2 class="fw-bold">Godown Wise Details Paid Zero Payment</h2>
        <h5 class="text-muted">From MPSCSC to MPWLC</h5>
    </div>

    <!-- ===== FILTER ===== -->
    <fieldset class="mb-4 p-3">
        <div class="row justify-content-center">
            <div class="col-md-2">
                <label class="form-label">District Name</label>
                </div>
            <div class="col-md-3">
                <asp:DropDownList ID="ddldistrict" runat="server" CssClass="form-control show-loader-ddl"
                                  OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="0">All</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </fieldset>

    <!-- ===== DETAILS GRID ===== -->
    <div id="grdone" runat="server" visible="false" class="p-3">

        <!-- GridView -->
        <div class="table-responsive">
            <asp:GridView ID="GrdBills" runat="server" AutoGenerateColumns="False" ShowFooter="true" 
                FooterStyle-Font-Bold="true" DataKeyNames="Bill_Number" CssClass="table table-bordered table-hover datatable">
                
                <Columns>
                    <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Region" HeaderStyle-BackColor="LightBlue">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblRegionnm" Text='<%# Eval("Regionnm") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Branch Name" HeaderStyle-BackColor="LightBlue">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDepotName" Text='<%# Eval("DepotName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Bill" HeaderStyle-BackColor="LightBlue">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblBill_Number" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Bill Amount" HeaderStyle-BackColor="LightBlue">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Bill Amount Received From MPSCSC" HeaderStyle-BackColor="LightBlue">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPayable_Amount" Text='<%# Eval("Payable_Amount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Deduction by HO MPSCSC" HeaderStyle-BackColor="LightBlue">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDeduction_by_HO_MPSCSC" Text='<%# Eval("Deduction_by_HO_MPSCSC") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="TDS Deduction by HO MPSCSC" HeaderStyle-BackColor="LightBlue">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTDS_Deduction_by_HO_MPSCSC" Text='<%# Eval("TDS_Deduction_by_HO_MPSCSC") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GrdBills.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>

