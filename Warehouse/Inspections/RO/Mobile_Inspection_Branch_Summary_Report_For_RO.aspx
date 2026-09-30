<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mobile_Inspection_Branch_Summary_Report_For_RO.aspx.cs" Inherits="Inspections_RO_Mobile_Inspection_Branch_Summary_Report_For_RO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inspection Summary Report</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: linear-gradient(to right, #eef2f7, #e3ebf6);
            overflow-x: hidden;
        }

        .full-width-container {
            width: 95% !important;
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
            max-height: 650px;
            overflow-y: auto;
            background: #fff;
            border-radius: 10px;
            padding: 10px;
        }

        .custom-grid {
            width: 100% !important;
            font-size: 11px;
            margin-bottom: 0 !important;
        }

        .custom-grid thead th {
            position: sticky;
            top: 0;
            z-index: 20;
            background-color: #3f51b5 !important;
            color: white;
            padding: 10px 4px;
            text-align: center;
            font-weight: 600;
            border: 1px solid #dee2e6;
        }

        .custom-grid td {
            padding: 8px 4px !important;
            vertical-align: middle;
            text-align: center;
        }
        
        .grid-footer-style {
            background-color: #eaedd1 !important;
            font-weight: bold;
            color: #2c3e50;
            border: 1px solid #dee2e6;
        }

        .border1 { border-left: 5px solid #4e73df; }
        .border2 { border-left: 5px solid #1cc88a; }
        .border3 { border-left: 5px solid #f6c23e; }
        .border4 { border-left: 5px solid #e74a3b; }
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
                                <asp:Label ID="lblBranch" runat="server" CssClass="value" Text="0" />
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
                                <asp:Label ID="lblGodown" runat="server" CssClass="value" Text="0" />
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
                                <asp:Label ID="lblStack" runat="server" CssClass="value" Text="0" />
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
                                <asp:Label ID="lblBags" runat="server" CssClass="value" Text="0" />
                            </div>
                            <div class="fs-3">🧺</div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="grid-container shadow-sm mb-4">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-hover table-bordered custom-grid"
                    GridLines="Both"
                    ShowFooter="true"
                    OnRowCommand="gvReport_RowCommand"
                    OnRowDataBound="gvReport_RowDataBound"
                    OnPreRender="gvReport_PreRender">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_Id") %>' />
                                <asp:HiddenField runat="server" ID="hdnemployeeid" Value='<%# Eval("Employee_ID") %>' />
                                <asp:HiddenField runat="server" ID="hdninsp_type_id" Value='<%# Eval("Quater_Type") %>' />
                                <asp:HiddenField runat="server" ID="hdnVerificationType" Value='<%# Eval("Verification_Type") %>' />
                                <asp:HiddenField runat="server" ID="hdnFinancial_Year" Value='<%# Eval("Financial_year") %>' />
                                <asp:HiddenField runat="server" ID="hdnOrder_no" Value='<%# Eval("Order_No") %>' />
                            </ItemTemplate>
                            <FooterTemplate>Total</FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" NullDisplayText="-" />
                        
                        <asp:TemplateField HeaderText="Branch">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBranch" runat="server"
                                    Text='<%# Eval("Branch_Name") %>'
                                    CommandName="BranchClick"
                                    Font-Bold="true"
                                    CommandArgument='<%# Container.DataItemIndex %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="OfficerName" HeaderText="Officer Name" NullDisplayText="-" />
                        
                        <asp:TemplateField HeaderText="Total Godowns">
                            <ItemTemplate><%# Eval("Total_Godown") %></ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFooterGodowns" runat="server" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Stacks">
                            <ItemTemplate><%# Eval("Total_Stack") %></ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFooterStacks" runat="server" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Bags">
                            <ItemTemplate><%# Eval("Total_Bags") %></ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFooterBags" runat="server" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Financial_year" HeaderText="Financial year" />
                        <asp:BoundField DataField="Order_No" HeaderText="Order" />
                        <asp:BoundField DataField="VerificationType" HeaderText="Inspection Type" />
                        <asp:BoundField DataField="Inspection_Status" HeaderText="Inspection Period" />
                        <asp:BoundField DataField="Inspection_Complete_Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" NullDisplayText="-" />
                        
                        <asp:TemplateField HeaderText="All Stock View">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkGodownStack" runat="server"
                                    Text="All Stock Without Image"
                                    CommandName="ViewAllGodownWiseStack"
                                    CssClass="btn btn-sm btn-outline-secondary font-monospace"
                                    Style="font-size:10px; padding: 2px 5px;"
                                    CommandArgument='<%# Container.DataItemIndex %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="grid-footer-style" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>