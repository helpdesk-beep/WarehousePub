<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_MPSCSC_Payment_Receive_and_Payto_Godown_Review_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_MPSCSC_Payment_Receive_and_Payto_Godown_Review_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">

<div class="container-fluid mt-3">

    <!-- ===== PAGE HEADER ===== -->
    <div class="card mb-4">
        <div class="card-body text-center bg-primary text-white">
            <h4 class="mb-1">M.P. Warehousing & Logistics Corporation</h4>
            <h5 class="mb-1">Region Wise JVS Payment Status</h5>
            <small>
                Against Received Payment From MPSCSC (From August 2020)
                <strong>(In Cr.)</strong>
            </small>
        </div>
    </div>

    <!-- ===== FILTER SECTION ===== -->
    <div class="card mb-4">
        <div class="card-body">

            <!-- Date Filter -->
            <div class="row align-items-end mb-3 justify-content-center">
                <div class="col-md-3">
                    <label class="fw-bold">Received Payment Date (From)</label>
                    <asp:TextBox ID="txtdatefrom" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label class="fw-bold">Received Payment Date (To)</label>
                    <asp:TextBox ID="txtdateto" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                </div>

                <div class="col-md-2">
                    <asp:Button ID="btnDateSearch" runat="server"
                        Text="Search"
                        CssClass="btn btn-primary w-100 show-loader"
                        OnClick="btnDateSearch_Click" />
                </div>
            </div>

            <div class="text-center fw-bold mb-3">OR</div>

            <!-- UTR Filter -->
            <div class="row align-items-end justify-content-center">
                <div class="col-md-4">
                    <label class="fw-bold">UTR Number</label>
                    <asp:TextBox ID="txtUTRNo" runat="server"
                        CssClass="form-control"
                        onkeypress="return NumberOnly(event);">
                    </asp:TextBox>
                </div>

                <div class="col-md-2">
                    <asp:Button ID="btnUTRSearch" runat="server"
                        Text="Search"
                        CssClass="btn btn-success w-100 show-loader"
                        OnClick="btnUTRSearch_Click" />
                </div>
            </div>

        </div>
    </div>

    <!-- ===== GRID SECTION ===== -->
    <div class="card">
        <div class="card-header fw-bold bg-light">
            District / Region Wise Payment Summary
        </div>
        <div class="card-body table-responsive">

            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False"
                ShowFooter="true"
                CssClass="table table-bordered table-striped table-hover"
                OnRowDataBound="GridView1_RowDataBound"
                OnRowCreated="GridView1_RowCreated"
                OnDataBound="OnDataBound">

                <Columns>

                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Regionnm" HeaderText="Region" />

                    <asp:TemplateField HeaderText="Received Amt. after Deduction" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblUTR2Payable_Amount" runat="server"
                                Text='<%# Eval("UTR2Payable_Amount") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalqty3" runat="server" Font-Bold="true" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Rent Bill Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblUTR3DEDTBill_Amount" runat="server"
                                Text='<%# Eval("UTR3DEDTBill_Amount") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalqty4" runat="server" Font-Bold="true" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Passed Rent Amt. by AM" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblPOJVS_Bill_Amount" runat="server"
                                Text='<%# Eval("POJVS_Bill_Amount") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalqty5" runat="server" Font-Bold="true" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="After Deduction Passed Amt" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblUTR4POJVS_Net_Amount" runat="server"
                                Text='<%# Eval("UTR4POJVS_Net_Amount") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalqty6" runat="server" Font-Bold="true" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Payment to Godown Owner" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblUTR7PFCredit_Amount" runat="server"
                                Text='<%# Eval("UTR7PFCredit_Amount") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalqty12" runat="server" Font-Bold="true" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Pending at RM" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalPendingatRM" runat="server"
                                Text='<%# Eval("TotalPendingatRM") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalqty13" runat="server" Font-Bold="true" />
                        </FooterTemplate>
                    </asp:TemplateField>

                </Columns>

                <FooterStyle Font-Bold="true" BackColor="#f1f1f1" />
            </asp:GridView>

        </div>
    </div>

</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>

