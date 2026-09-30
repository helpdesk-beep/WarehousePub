<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_Wise_NAFED_Payment_Summary_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_District_Wise_NAFED_Payment_Summary_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">

<div class="container-fluid mt-3">

    <!-- ===== TITLE ===== -->
    <fieldset class="border p-3">
        <legend class="w-auto px-3 text-center fw-bold  align-items-center">
            Region, District, Commodity Wise NAFED Payment Status 
            <span class="text-danger">(In M.T.)</span>
        </legend>

        <!-- ===== ACTION BUTTONS ===== -->
        <%--<div class="card mb-3">
            <div class="card-body">
                <div class="row align-items-center">
                    <div class="col-md-6">
                        <asp:Button ID="Button2" runat="server"
                            Text="Export To PDF"
                            CssClass="btn btn-danger me-2"
                            OnClientClick="printGrid()" />

                        <input type="button"
                            id="btnExport"
                            value="Export To Excel"
                            class="btn btn-success" />
                    </div>
                </div>
            </div>
        </div>--%>

        <!-- ===== FILTER SECTION ===== -->
        <div class="card mb-3">
            <div class="card-body">
                <div class="row align-items-center justify-content-center">
                    <div class="col-md-2 fw-bold">
                        Commodity
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlcommodity" runat="server"
                            CssClass="form-control"
                            AutoPostBack="false">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-3">
                        <asp:Button ID="btnshow" runat="server"
                            Text="Show Details"
                            CssClass="btn btn-primary show-loader"
                            OnClick="btnshow_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- ===== GRID SECTION ===== -->
        <div id="divdivision" runat="server" visible="false" class="mt-3">
            <div class="card">
                <div class="card-body table-responsive">

                    <asp:GridView ID="GridView1" runat="server"
                        AutoGenerateColumns="false"
                        ShowFooter="true"
                        CssClass="table table-bordered table-striped table-hover"
                        OnRowDataBound="GridView1_RowDataBound"
                        OnRowCreated="GridView1_RowCreated">

                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Region" HeaderText="Region" />
                            <asp:BoundField DataField="Region_ID" HeaderText="Region ID" />
                            <asp:BoundField DataField="District_Name" HeaderText="District" />

                            <asp:BoundField DataField="AmountRecivedFromNAfed"
                                HeaderText="SC Amount Received"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="TotalRentBillAmountD"
                                HeaderText="Total Rent Bill"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="NoofBillAmountPassedbyRM"
                                HeaderText="Passed Amt. by AM"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="TotalAmountPassedbyRMAfterAllDeduction"
                                HeaderText="After Deduction"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="PendingatRM"
                                HeaderText="Pending at RM"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="TDS_Detuction_Amount"
                                HeaderText="TDS Deduction"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="TotalDeductionbyRM"
                                HeaderText="Total Deduction"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="PaytogodownOwner"
                                HeaderText="Pay to Godown Owner"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="PendingatRMForPaytogodownOwner"
                                HeaderText="Pending for Payment"
                                ItemStyle-HorizontalAlign="Right" />
                        </Columns>

                        <FooterStyle BackColor="#f1f1f1" Font-Bold="true" />
                    </asp:GridView>

                </div>
            </div>
        </div>

    </fieldset>
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


