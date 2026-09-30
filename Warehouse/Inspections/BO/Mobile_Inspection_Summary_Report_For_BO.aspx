<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Inspections/BO/Mobile_Inspection_Summary_Report_For_BO.aspx.cs" Inherits="Inspections_BO_Mobile_Inspection_Summary_Report_For_BO" %>

<!DOCTYPE html>
<html>
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
                <h2>📊 Mobile App Inspection Summary Report</h2>
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

                <asp:GridView ID="gvReport" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table table-hover table-bordered custom-grid"
                    HeaderStyle-CssClass="grid-header"
                    RowStyle-CssClass="grid-row"
                    ItemStyle-Font-Size="Medium"
                    Width="100%"
                    OnRowCommand="gvReport_RowCommand"
                    OnPreRender="gvReport_PreRender">

                    <Columns>

                        <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="5%">
                            <ItemTemplate>

                                <%# Container.DataItemIndex + 1 %>

                                <asp:HiddenField ID="hdnbranchid" runat="server"
                                    Value='<%# Eval("Branch_Id") %>' />

                                <asp:HiddenField ID="hdnemployeeid" runat="server"
                                    Value='<%# Eval("Employee_ID") %>' />

                                <asp:HiddenField ID="hdninsp_type_id" runat="server"
                                    Value='<%# Eval("Quater_Type") %>' />

                                <asp:HiddenField ID="hdnVerificationType" runat="server"
                                    Value='<%# Eval("Verification_Type") %>' />

                                <asp:HiddenField ID="hdnFinancial_Year" runat="server"
                                    Value='<%# Eval("Financial_year") %>' />

                                <asp:HiddenField ID="hdnOrder_no" runat="server"
                                    Value='<%# Eval("Order_No") %>' />

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="District_Name"
                            HeaderText="District Name"
                            HeaderStyle-Width="12%" />

                        <asp:TemplateField HeaderText="Branch" HeaderStyle-Width="15%">
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkBranch"
                                    runat="server"
                                    Text='<%# Eval("Branch_Name") %>'
                                    CommandName="BranchClick"
                                    Style="word-break: break-word;"
                                    CommandArgument='<%# Eval("Branch_Id") + "|" + Eval("Financial_year") + "|" + Eval("Employee_ID") + "|" + Eval("Quater_Type") + "|" + Eval("Order_No") + "|" + Eval("Verification_Type") %>'>
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="OfficerName"
                            HeaderText="Officer Name"
                            HeaderStyle-Width="12%" />

                        <asp:BoundField DataField="Total_Godown"
                            HeaderText="Total Godowns"
                            HeaderStyle-Width="7%" />

                        <asp:BoundField DataField="Total_Stack"
                            HeaderText="Total Stacks"
                            HeaderStyle-Width="7%" />

                        <asp:BoundField DataField="Total_Bags"
                            HeaderText="Total Bags"
                            HeaderStyle-Width="7%" />

                        <asp:BoundField DataField="Financial_year"
                            HeaderText="Financial Year"
                            HeaderStyle-Width="8%" />

                        <asp:BoundField DataField="Order_No"
                            HeaderText="Order No"
                            HeaderStyle-Width="7%" />

                        <asp:BoundField DataField="VerificationType"
                            HeaderText="Inspection Type"
                            HeaderStyle-Width="10%" />

                        <asp:BoundField DataField="Inspection_Status"
                            HeaderText="Inspection Period"
                            HeaderStyle-Width="10%" />

                        <asp:BoundField DataField="Inspection_Complete_Date"
                            HeaderText="Date"
                            DataFormatString="{0:dd/MM/yyyy}"
                            HeaderStyle-Width="8%" />

                        <%--<asp:TemplateField HeaderText="All Stock" HeaderStyle-Width="10%">
                            <ItemTemplate>

                                <asp:LinkButton ID="lnkAllStock"
                                    runat="server"
                                    Text="See All Stock"
                                    CssClass="btn btn-primary btn-sm"
                                    CommandName="AllStock"
                                    CommandArgument='<%# Eval("Branch_Id") + "|" + Eval("Financial_year") + "|" + Eval("Employee_ID") + "|" + Eval("Quater_Type") + "|" + Eval("Order_No") + "|" + Eval("Verification_Type") %>'>
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>--%>

                             <asp:TemplateField HeaderText="See All Stock Without Image" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkGodownStack" runat="server"
                                        Text="All Stock Without Image"
                                        CommandName="ViewAllGodownWiseStack"
                                        CommandArgument='<%# Eval("Branch_Id") + "|" + Eval("Financial_year") + "|" + Eval("Employee_ID") + "|" + Eval("Quater_Type") + "|" + Eval("Order_No") + "|" + Eval("Verification_Type") %>'
                                        CssClass="text-godown fw-bold">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                    </Columns>

                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
