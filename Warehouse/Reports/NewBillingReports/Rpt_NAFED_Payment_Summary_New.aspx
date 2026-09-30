<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master"
    AutoEventWireup="true"
    CodeFile="Rpt_NAFED_Payment_Summary_New.aspx.cs"
    Inherits="Reports_NewBillingReports_Rpt_NAFED_Payment_Summary_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <style type="text/css">
        /* Custom Styles */
        .legend-header {
            background-color: #f8f9fa;
            border-bottom: 2px solid #dee2e6;
            padding: 15px;
            margin-bottom: 20px;
            border-radius: 5px;
        }

        .filter-section {
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            margin-bottom: 25px;
        }

        .form-label {
            font-weight: 600;
            color: #495057;
            margin-bottom: 8px;
        }

        .button-custom {
            padding: 8px 25px;
            font-weight: 600;
            border-radius: 4px;
            min-width: 150px;
        }

        .btn-show {
            background-color: #28a745;
            color: white;
            border: none;
        }

            .btn-show:hover {
                background-color: #218838;
            }

        /* Grid Styling */
        .grid-container {
            margin-top: 20px;
            overflow-x: auto;
        }

        .grid-header {
            background-color: #343a40 !important;
            color: white !important;
        }

        .grid-footer {
            background-color: #495057 !important;
            color: white !important;
            font-weight: bold;
        }

        .grid-row-alt {
            background-color: #f8f9fa;
        }

        .grid-row-hover:hover {
            background-color: #e9ecef;
        }

        /* Responsive adjustments */
        @media (max-width: 768px) {
            .filter-section .col-md-2 {
                margin-bottom: 15px;
            }

            .button-custom {
                width: 100%;
            }
        }

        /* Required field indicator */
        .required-field::after {
            content: " *";
            color: #dc3545;
        }

        /* Card style for sections */
        .card-style {
            border: 1px solid #dee2e6;
            border-radius: 5px;
            overflow: hidden;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid">
        <div class="row legend-header">
            <div class="col-12">
                <h4 class="mb-0 text-center">
                    <i class="fas fa-file-invoice-dollar mr-2"></i>
                    Region, District, Commodity Wise NAFED Payment Status From 1 April 2024
                    <span style="color: red">(Only Pvt. Warehouse – In M.T.)</span>
                </h4>
            </div>
        </div>

        <!-- Filter Section -->
        <div class="filter-section card-style">
            <div class="row align-items-end">
                <div class="col-md-2">
                    <label>Select Report Type</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddldistrict" runat="server"
                        CssClass="form-control select2"
                        AutoPostBack="false"
                        OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Region" Value="1" />
                        <asp:ListItem Text="District" Value="2" />
                        <asp:ListItem Text="Branch" Value="3" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-1">
                    <label>Commodity</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlcommodity" runat="server"
                        CssClass="form-control"
                        AutoPostBack="false">
                        <asp:ListItem Text="All" Value="0" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 text-center">
                    <asp:Button ID="btnshow"
                        runat="server"
                        Text="Show Details"
                        CssClass="btn btn-primary show-loader"
                        OnClick="btnshow_Click" />
                </div>

            </div>
        </div>


        <!-- REGION GRID -->
        <div id="divdivision" runat="server" visible="false" class="row mt-4">
            <div class="col-md-12">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server"
                        AutoGenerateColumns="false"
                        ShowFooter="true"
                        CssClass="table table-bordered table-hover"
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
                            <asp:BoundField DataField="Commodity" HeaderText="Commodity" />

                            <asp:BoundField DataField="AmountRecivedFromNAfed"
                                HeaderText="SC Amount Received From NAFED"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="TotalRentBillAmountD"
                                HeaderText="Total Rent Bill Amount"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="NoofBillAmountPassedbyRM"
                                HeaderText="Passed Rent Amount by AM"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="TotalAmountPassedbyRMAfterAllDeduction"
                                HeaderText="After Deduction Passed Amount"
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

                        <FooterStyle BackColor="#f2f2f2" Font-Bold="true" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- DISTRICT GRID -->
        <div id="divdistrict" runat="server" visible="false" class="row mt-4">
            <div class="col-md-12">
                <div class="table-responsive">
                    <asp:GridView ID="GridView2" runat="server"
                        AutoGenerateColumns="false"
                        ShowFooter="true"
                        CssClass="table table-bordered table-hover"
                        OnRowDataBound="GridView2_RowDataBound"
                        OnRowCreated="GridView2_RowCreated">

                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                            <asp:BoundField DataField="District_Id" HeaderText="District ID" />
                            <asp:BoundField DataField="Commodity" HeaderText="Commodity" />

                            <asp:BoundField DataField="AmountRecivedFromNAfed"
                                HeaderText="SC Amount Received From NAFED"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="TotalRentBillAmountD"
                                HeaderText="Total Rent Bill Amount"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="NoofBillAmountPassedbyRM"
                                HeaderText="Passed Rent Amount by AM"
                                ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="TotalAmountPassedbyRMAfterAllDeduction"
                                HeaderText="After Deduction Passed Amount"
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

                        <FooterStyle BackColor="#f2f2f2" Font-Bold="true" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageScript" runat="Server">
</asp:Content>
