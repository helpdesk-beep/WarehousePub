<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_NAFED_Payment_Status_All_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_NAFED_Payment_Status_All_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <style>
        .report-header {
            background: linear-gradient(135deg, #1e90ff 0%, #00bfff 100%);
            color: white;
            padding: 20px;
            border-radius: 8px;
            margin-bottom: 20px;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
        }

        .search-panel {
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 8px;
            margin-bottom: 20px;
            border: 1px solid #dee2e6;
        }

        .grid-container {
            background: white;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            overflow: hidden;
        }

        .grid-header {
            background-color: #343a40;
            color: white;
            padding: 15px;
            border-bottom: 1px solid #dee2e6;
        }

        .amount-cell {
            text-align: right;
            font-family: 'Courier New', monospace;
            font-weight: 500;
        }

        .total-row {
            background-color: #e8f4fd !important;
            font-weight: bold;
        }

        .region-cell {
            font-weight: 600;
            color: #2c3e50;
        }

        .btn-custom {
            min-width: 100px;
        }

        @media (max-width: 768px) {
            .form-control {
                margin-bottom: 10px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid">
        <!-- Header Section -->
        <!-- Report Header -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="report-header text-center">
                    <h2 class="mb-2">Region, District, Commodity Wise NAFED Payment Status</h2>
                    <h4 class="mb-0">From 1 April 2024 <span class="text-danger">(In M.T.)</span></h4>
                </div>
            </div>
        </div>

        <!-- Filter Section -->
        <div class="filter-section card-style justify-content-center">
            <div class="row align-items-end">
                <!-- Report Type -->
                <div class="col-md-3 col-sm-6">
                    <div class="form-group">
                        <label class="form-label">
                            <i class="fas fa-chart-bar mr-1"></i>
                            Report Type
                        </label>
                        <asp:DropDownList ID="ddldistrict" runat="server"
                            CssClass="form-control"
                            AutoPostBack="false">
                            <asp:ListItem Text="-- Select Report Type --" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Region Wise" Value="1"></asp:ListItem>
                            <asp:ListItem Text="District Wise" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Branch Wise" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Commodity -->
                <div class="col-md-3 col-sm-3">
                    <div class="form-group">
                        <label class="form-label">
                            <i class="fas fa-seedling mr-1"></i>
                            Commodity
                        </label>
                        <asp:DropDownList ID="ddlcommodity" runat="server"
                            CssClass="form-control"
                            AutoPostBack="false"
                            SelectionMode="Multiple">
                            <asp:ListItem Text="All Commodities" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Action Buttons -->
                <div class="col-md-6 col-sm-6">
                    <div class="d-flex align-items-end h-100">
                        <div class="btn-group" role="group">
                            <asp:Button ID="btnshow" runat="server"
                                Text="Show Report" Style="margin-bottom: 15px;"
                                CssClass="btn btn-success button-custom mr-2"
                                OnClick="btnshow_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Report Grid Section -->
        <div id="divdivision" runat="server" visible="false" class="grid-container">
            <div class="card card-style">
                <div class="card-header bg-light">
                    <h5 class="mb-0">
                        <i class="fas fa-table mr-2"></i>
                        Report Details
                        <small class="float-right text-muted" id="recordCount" runat="server"></small>
                    </h5>
                </div>
                <div class="card-body p-0">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server"
                            AutoGenerateColumns="false"
                            ShowFooter="true"
                            CssClass="table table-bordered table-hover table-striped mb-0"
                            OnRowDataBound="GridView1_RowDataBound"
                            OnRowCreated="GridView1_RowCreated"
                            HeaderStyle-CssClass="grid-header"
                            FooterStyle-CssClass="grid-footer"
                            RowStyle-CssClass="grid-row-hover"
                            AlternatingRowStyle-CssClass="grid-row-alt"
                            GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="60px">
                                    <ItemTemplate>
                                        <span class="badge1 badge-secondary">
                                            <%# Container.DataItemIndex + 1 %>
                                        </span>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="Region" HeaderText="Region">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Region_ID" HeaderText="Region ID">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Commodity" HeaderText="Commodity">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TotalSCBillReceivedFromNAFED"
                                    HeaderText="Total SC Bill Received"
                                    DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="AmountRecivedFromNAfed"
                                    HeaderText="Amount Received"
                                    DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="RentBillNumber"
                                    HeaderText="Rent Bills Generated"
                                    DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TotalRentBillAmount"
                                    HeaderText="Rent Bill Amount"
                                    DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="NoofBillDeductionbyBM"
                                    HeaderText="Deduced by BM"
                                    DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="NoofBillSenttoRMbyBM"
                                    HeaderText="Sent to RM"
                                    DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="NoofBillApprovedbyRM"
                                    HeaderText="Approved by RM"
                                    DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="NoofBillPassedbyRM"
                                    HeaderText="Passed by RM"
                                    DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="NoofBillAmountPassedbyRM"
                                    HeaderText="Amount Passed"
                                    DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TDS_Detuction_Amount"
                                    HeaderText="TDS Deduction"
                                    DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TotalDeductionbyRM"
                                    HeaderText="Total Deduction"
                                    DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="PaytogodownOwner"
                                    HeaderText="Pay to Owner"
                                    DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" Font-Bold="true" CssClass="text-success" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>

                            <EmptyDataTemplate>
                                <div class="text-center py-5">
                                    <i class="fas fa-database fa-3x text-muted mb-3"></i>
                                    <h5 class="text-muted">No data available</h5>
                                    <p class="text-muted">Select filters and click "Show Report" to generate data</p>
                                </div>
                            </EmptyDataTemplate>

                            <PagerSettings Mode="NumericFirstLast"
                                Position="Bottom"
                                PageButtonCount="5" />

                            <PagerStyle CssClass="pagination justify-content-center mt-3" />
                        </asp:GridView>
                    </div>
                </div>

                <!-- Action Buttons for Report -->
                <div class="card-footer bg-white border-top">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <span class="text-muted">
                                <i class="fas fa-info-circle mr-1"></i>
                                Report generated on:
                                <asp:Label ID="lblReportDate" runat="server" Text=""></asp:Label>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- No Data Message -->
        <div id="divNoData" runat="server" visible="false" class="text-center py-5">
            <div class="alert alert-info">
                <i class="fas fa-info-circle fa-2x mb-3"></i>
                <h5>No Report Generated</h5>
                <p>Please select report type and commodity, then click "Show Report"</p>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageScript" runat="Server">
</asp:Content>
