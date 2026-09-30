<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mobile_InspectionRegion_Wise_Summry_Report_For_HO.aspx.cs" Inherits="Inspections_State_Mobile_InspectionRegion_Wise_Summry_Report_For_HO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inspection Report</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: linear-gradient(to right, #eef2f7, #e3ebf6);
            overflow-x: hidden;
        }

        .full-width-container {
            width: 90% !important;
            margin: 0 auto;
            max-width: 100% !important;
        }

        .header-box {
            padding: 15px;
            border-radius: 18px;
            background: linear-gradient(135deg, #4e73df, #6f42c1);
            color: white;
            box-shadow: 0 8px 25px rgba(0,0,0,0.15);
            position: relative;
            overflow: hidden;
        }

        .summary-card-v2 {
            padding: 15px;
            border-radius: 15px;
            background: rgba(255,255,255,0.8);
            backdrop-filter: blur(10px);
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
            transition: 0.3s;
        }

            .summary-card-v2 .value {
                font-size: 22px;
                font-weight: 700;
            }

            .summary-card-v2 .title {
                font-size: 13px;
                color: #555;
            }

        .grid-container {
            height: 650px;
            overflow-y: auto;
            overflow-x: hidden;
            background: #fff;
            border-radius: 10px;
        }

        .custom-grid {
            width: 100% !important;
            table-layout: fixed;
            font-size: 11px;
            margin-bottom: 0 !important;
        }

            .custom-grid thead th {
                position: sticky;
                top: 0;
                z-index: 20;
                background-color: #3f51b5 !important;
                color: white;
                padding: 8px 4px;
                text-align: center;
                font-weight: 600;
                border: 1px solid #dee2e6;
            }

            .custom-grid td {
                padding: 6px 4px !important;
                vertical-align: middle;
                word-wrap: break-word;
                overflow-wrap: break-word;
                text-align: center;
            }

                .custom-grid th:nth-child(1), .custom-grid td:nth-child(1) {
                    width: 40px;
                }

                .custom-grid th:nth-child(2), .custom-grid td:nth-child(2) {
                    width: 80px;
                }

                .custom-grid th:nth-child(3), .custom-grid td:nth-child(3) {
                    width: 90px;
                }

                .custom-grid th:nth-child(4), .custom-grid td:nth-child(4) {
                    width: 110px;
                }

                .custom-grid th:nth-child(5), .custom-grid td:nth-child(5),
                .custom-grid th:nth-child(6), .custom-grid td:nth-child(6),
                .custom-grid th:nth-child(7), .custom-grid td:nth-child(7) {
                    width: 65px;
                }

                .custom-grid th:nth-child(8), .custom-grid td:nth-child(8) {
                    width: 75px;
                }

                .custom-grid th:nth-child(9), .custom-grid td:nth-child(9) {
                    width: 100px;
                }

                .custom-grid th:nth-child(10), .custom-grid td:nth-child(10) {
                    width: 100px;
                }

                .custom-grid th:nth-child(11), .custom-grid td:nth-child(11) {
                    width: 70px;
                }

                .custom-grid th:nth-child(12), .custom-grid td:nth-child(12) {
                    width: 85px;
                }

        .border1 {
            border-left: 5px solid #4e73df;
        }

        .border2 {
            border-left: 5px solid #1cc88a;
        }

        .border3 {
            border-left: 5px solid #f6c23e;
        }

        .border4 {
            border-left: 5px solid #e74a3b;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="full-width-container mt-3">
            <div class="header-box text-center mb-3">
                <div class="d-flex align-items-center justify-content-between">
                    <asp:LinkButton ID="btnBack" runat="server"
                        CssClass="btn btn-light btn-sm fw-bold"
                        OnClick="btnBack_Click">← Back</asp:LinkButton>
                    <h2 class="m-0 text-center flex-grow-1">📊 Mobile App Inspection Summary Report</h2>
                    <div style="width: 80px;"></div>
                </div>
            </div>
            <div class="row g-3 mb-3">
                <div class="col-md-3">
                    <div class="summary-card-v2 border1">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <div class="title">Total Branch</div>
                                <asp:Label ID="lblBranch" runat="server" CssClass="value" />
                            </div>
                            <div class="fs-3">🏢</div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="summary-card-v2 border2">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <div class="title">Total Godown</div>
                                <asp:Label ID="lblGodown" runat="server" CssClass="value" />
                            </div>
                            <div class="fs-3">🏬</div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="summary-card-v2 border3">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <div class="title">Total Stack</div>
                                <asp:Label ID="lblStack" runat="server" CssClass="value" />
                            </div>
                            <div class="fs-3">📦</div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="summary-card-v2 border4">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <div class="title">Total Bags</div>
                                <asp:Label ID="lblBags" runat="server" CssClass="value" />
                            </div>
                            <div class="fs-3">🧺</div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="grid-container shadow-sm">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-hover table-bordered custom-grid"
                    HeaderStyle-CssClass="grid-header" RowStyle-CssClass="grid-row" OnRowCommand="gvReport_RowCommand"
                    OnPreRender="gvReport_PreRender" ItemStyle-Font-Size="Medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                <asp:HiddenField runat="server" ID="hdnRegion_ID" Value='<%# Eval("Region_ID") %>' />
                                <asp:HiddenField runat="server" ID="hdninsp_type_id" Value='<%# Eval("Quater_Type") %>' />
                                <asp:HiddenField runat="server" ID="hdnVerificationType" Value='<%# Eval("Verification_Type") %>' />
                                <asp:HiddenField runat="server" ID="hdnFinancial_Year" Value='<%# Eval("Financial_year") %>' />

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBranch" runat="server"
                                    Text='<%# Eval("Regionnm") %>'
                                    CommandName="BranchClick"
                                    CommandArgument='<%# Eval("Region_ID") + "|" + Eval("Financial_year") + "|" + Eval("Quater_Type") +"|" + "|" + Eval("Verification_Type") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Total_Branch" HeaderText="Total Branch" />
                        <asp:BoundField DataField="Total_Godown" HeaderText="Total Godowns" />
                        <asp:BoundField DataField="Total_Stack" HeaderText="Total Stacks" />
                        <asp:BoundField DataField="Total_Bags" HeaderText="Total Bags" />
                        <asp:BoundField DataField="Financial_year" HeaderText="Financial year" />
                        <asp:BoundField DataField="VerificationType" HeaderText="Inspection Type" />
                        <asp:BoundField DataField="Inspection_Status" HeaderText="Inspection Period" />
                        <asp:BoundField DataField="Inspection_Complete_Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
