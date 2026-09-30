<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="~/Reports/NewStorageCapacityReport/RM_Pending_Report.aspx.cs" Inherits="Reports_NewStorageCapacityReport_RM_Pending_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid mt-3">
        <div class="card p-3">
            <h4 class="mb-3 text-primary fw-bold">RM Pending Amount Report</h4>

            <div class="row mb-3">
                <div class="col-md-3">
                    <label class="form-label fw-bold">Crop Year</label>
                    <asp:DropDownList ID="ddlCropYear" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label class="form-label fw-bold">Region</label>
                    <asp:DropDownList ID="ddlRegion" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label class="form-label fw-bold">Commodity</label>
                    <%--<asp:DropDownList ID="ddlCommodity" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                    <asp:ListBox ID="ddlCommodity" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>

                <div class="col-md-2 d-flex align-items-end">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary w-100" OnClick="btnSearch_Click" />
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server"
                    CssClass="table table-bordered table-striped table-hover"
                    AutoGenerateColumns="false"
                    ShowFooter="true"
                    ShowHeaderWhenEmpty="true"
                    OnRowDataBound="GridView1_RowDataBound"
                    OnPreRender="GridView1_PreRender"
                    EmptyDataText="No records found for the selected criteria."
                    EmptyDataRowStyle-CssClass="alert alert-warning text-center">

                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                        <asp:BoundField DataField="District_Name" HeaderText="District" />
                        <asp:BoundField DataField="DepotName" HeaderText="Depot" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                        <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" HeaderStyle-Width="8%" />
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />

                        <asp:BoundField DataField="DeductionAmount" HeaderText="Deduction (₹)"
                            DataFormatString="{0:N2}" ItemStyle-CssClass="numeric-col"
                            HeaderStyle-CssClass="text-end" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount (₹)"
                            DataFormatString="{0:N2}" ItemStyle-CssClass="numeric-col"
                            HeaderStyle-CssClass="text-end" />
                        <asp:BoundField DataField="Amount_DALG" HeaderText="DALG (₹)"
                            DataFormatString="{0:N2}" ItemStyle-CssClass="numeric-col"
                            HeaderStyle-CssClass="text-end" />
                        <asp:BoundField DataField="Other_Deduction" HeaderText="Other Deduction (₹)"
                            DataFormatString="{0:N2}" ItemStyle-CssClass="numeric-col"
                            HeaderStyle-CssClass="text-end" />
                        <asp:BoundField DataField="PendingatRM" HeaderText="Pending RM (₹)"
                            DataFormatString="{0:N2}" ItemStyle-CssClass="numeric-col"
                            HeaderStyle-CssClass="text-end" />
                    </Columns>

                    <FooterStyle CssClass="grid-footer" Font-Bold="true" />
                    <EmptyDataRowStyle CssClass="alert alert-warning text-center" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>

