<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_Wise_NAFED_Payment_Summary_For_Review_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_District_Wise_NAFED_Payment_Summary_For_Review_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
    <div class="container-fluid mt-3">

        <!-- Header -->
        <div class="row mb-3">
            <div class="col-12 text-center">
                <h4 class="fw-bold">
                    District Wise Pendancy at NAFED From 1 April 2024 (All Godown)
                    <span class="text-danger">(Amount in Cr.)</span>
                </h4>
            </div>
        </div>

        <!-- Grid Section -->
        <div id="divdivision" runat="server" visible="false" class="row">
            <div class="col-12">
                <div class="table-responsive">

                    <asp:GridView ID="GridView1" runat="server"
                        AutoGenerateColumns="false"
                        ShowFooter="true"
                        CssClass="table table-bordered table-hover text-nowrap"
                        AlternatingRowStyle-CssClass="table-secondary">

                        <Columns>

                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Region"
                                HeaderText="Region"
                                ItemStyle-HorizontalAlign="Left" />

                            <asp:BoundField DataField="District_Name"
                                HeaderText="District"
                                ItemStyle-HorizontalAlign="Left" />

                            <asp:BoundField DataField="TotalBillAmountSubmittedtoNAFED"
                                HeaderText="SC Amount Submit to NAFED"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="AmountRecivedFromNAfed"
                                HeaderText="SC Amount Received From NAFED"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="PendingatNafed"
                                HeaderText="Pending at NAFED (Submit - Received)"
                                ItemStyle-HorizontalAlign="Right" />

                        </Columns>

                        <FooterStyle CssClass="fw-bold bg-light" />

                    </asp:GridView>

                </div>
            </div>
        </div>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        $(document).ready(function () {
            var grid = $('#<%= GridView1.ClientID %>');

            // Convert first row to THEAD for DataTable
            grid.prepend($("<thead></thead>").append(grid.find("tr:first")));

            BindDatatable(grid);
        });
    </script>
</asp:Content>

