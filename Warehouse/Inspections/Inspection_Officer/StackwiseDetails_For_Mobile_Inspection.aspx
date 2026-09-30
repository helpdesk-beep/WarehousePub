<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StackwiseDetails_For_Mobile_Inspection.aspx.cs" Inherits="Inspections_Inspection_Officer_StackwiseDetails_For_Mobile_Inspection" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StackwiseDetails_For_Mobile_Inspection.aspx.cs" Inherits="Inspections_Inspection_Officer_StackwiseDetails_For_Mobile_Inspection" EnableEventValidation="false" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Inspection Report | Smart Warehouse</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;600;700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />

    <style>
        body {
            background-color: #f3f4f7;
            font-family: 'Inter', sans-serif;
            color: #2d3436;
        }

        .page-header {
            background: linear-gradient(135deg, #6c5ce7, #a29bfe);
            padding: 30px 0 60px 0;
            color: white;
            text-align: center;
            border-bottom-left-radius: 50px;
            border-bottom-right-radius: 50px;
            box-shadow: 0 10px 20px rgba(108, 92, 231, 0.2);
            position: relative;
        }

        /* Back Button Style */
        .btn-back {
            position: absolute;
            left: 20px;
            top: 30px;
            background: rgba(255, 255, 255, 0.2);
            color: white;
            border: none;
            padding: 8px 15px;
            border-radius: 12px;
            backdrop-filter: blur(5px);
            text-decoration: none;
            font-weight: 600;
            transition: 0.3s;
        }

            .btn-back:hover {
                background: white;
                color: #6c5ce7;
            }

        .info-wrapper {
            margin-top: -40px;
            margin-bottom: 30px;
        }

        .glass-card {
            background: rgba(255, 255, 255, 0.95);
            backdrop-filter: blur(10px);
            border-radius: 20px;
            box-shadow: 0 15px 35px rgba(0,0,0,0.05);
            padding: 25px;
        }

        /* Export Buttons Group */
        .export-container {
            display: flex;
            justify-content: flex-end;
            gap: 10px;
            margin-bottom: 15px;
        }

        .btn-export {
            border-radius: 10px;
            font-weight: 600;
            padding: 8px 18px;
            border: none;
            transition: 0.3s;
        }

        .btn-excel {
            background-color: #1D6F42;
            color: white;
        }

        .btn-pdf {
            background-color: #E74C3C;
            color: white;
        }

        .btn-export:hover {
            transform: translateY(-2px);
            opacity: 0.9;
            color: white;
        }

        .detail-item {
            padding: 10px;
            border-right: 1px solid #eee;
        }

            .detail-item:last-child {
                border-right: none;
            }

        .label-text {
            display: block;
            font-size: 11px;
            font-weight: 700;
            color: #b2bec3;
            text-transform: uppercase;
        }

        .value-text {
            font-size: 15px;
            font-weight: 600;
            color: #2d3436;
        }

        .custom-table-card {
            border-radius: 20px;
            overflow: hidden;
            background: white;
            box-shadow: 0 10px 30px rgba(0,0,0,0.03);
        }

        .table thead {
            background-color: #f8f9fa;
        }

            .table thead th {
                padding: 15px;
                font-size: 12px;
                text-transform: uppercase;
                color: #636e72;
                border: none;
                text-align: center;
            }

        .table tbody td {
            padding: 15px;
            vertical-align: middle;
            border-bottom: 1px solid #f1f2f6;
            text-align: center;
        }

        .badge-diff {
            padding: 6px 12px;
            border-radius: 50px;
            font-weight: 700;
            font-size: 13px;
            display: inline-block;
        }

        .diff-danger {
            background: #fff5f5;
            color: #ff7675;
        }

        .diff-success {
            background: #f0fff4;
            color: #55efc4;
        }

        .img-thumb {
            width: 50px;
            height: 50px;
            object-fit: cover;
            border-radius: 10px;
            cursor: pointer;
            transition: 0.3s;
        }

            .img-thumb:hover {
                transform: scale(1.1);
            }

        @media (max-width: 768px) {
            .btn-back {
                top: 15px;
                padding: 5px 10px;
                font-size: 12px;
            }

            .detail-item {
                border-right: none;
                border-bottom: 1px solid #eee;
            }

            .export-container {
                justify-content: center;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header class="page-header">
            <div class="container">
                <h2>Stack Wise Inspection</h2>
                <p class="opacity-75">Detailed Warehouse Inspection Report</p>
            </div>
        </header>
        <main class="container">
            <div class="info-wrapper">
                <div class="glass-card">
                    <div class="row g-3">
                        <div class="col-6 col-md-3 detail-item">
                            <span class="label-text">Officer Incharge</span>
                            <asp:Label ID="lblOfficer" runat="server" CssClass="value-text"></asp:Label>
                        </div>
                        <div class="col-6 col-md-3 detail-item">
                            <span class="label-text">District</span>
                            <asp:Label ID="lblDistrict" runat="server" CssClass="value-text"></asp:Label>
                        </div>
                        <div class="col-6 col-md-3 detail-item">
                            <span class="label-text">Active Depot</span>
                            <asp:Label ID="lblDepot" runat="server" CssClass="value-text"></asp:Label>
                        </div>
                        <div class="col-6 col-md-3 detail-item">
                            <span class="label-text">Godown ID</span>
                            <asp:Label ID="lblGodown" runat="server" CssClass="value-text"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="export-container">
                <asp:LinkButton ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" CssClass="btn-export btn-excel shadow-sm">
                    <i class="fa-regular fa-file-excel me-1"></i> Excel
                </asp:LinkButton>
                <%--  <asp:LinkButton ID="btnExportPDF" runat="server" OnClick="btnExportPDF_Click" CssClass="btn-export btn-pdf shadow-sm">
                    <i class="fa-regular fa-file-pdf me-1"></i> PDF
                </asp:LinkButton>--%>
            </div>
            <div class="custom-table-card mb-5">
                <div class="table-responsive">
                    <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table table-hover" GridLines="None" OnRowDataBound="gvReport_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stack Name">
                                <ItemTemplate><%# Eval("Stack_Name") %></ItemTemplate>
                                <FooterTemplate>
                                    <div class="text-end">Grand Total</div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Stack_ID" HeaderText="Stack ID" />--%>
                            <asp:TemplateField HeaderText="Online Bags">
                                <ItemTemplate><%# Eval("OnlineBags") %></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalOnlineBags" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PV Bags">
                                <ItemTemplate><%# Eval("PV_Bags") %></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalPV" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Spillage">
                                <ItemTemplate>
                                    <span class="text-danger fw-bold"><%# Eval("SpillageBags") %></span>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalSpillage" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Godown Submit Date">
                                <ItemTemplate><%# Eval("Godown_Submit_Date") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Difference">
                                <ItemTemplate>
                                    <div class='<%# Convert.ToInt32(Eval("Difference")) > 0 ? "badge-diff diff-danger" : "badge-diff diff-success" %>'>
                                        <%# Eval("Difference") %>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalDiff" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemark" runat="server"
                                        CssClass="form-control form-control-sm"
                                        Text='<%# Eval("Remark") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stack Image">
                                <ItemTemplate>
                                    <img src='<%# Eval("StackImageBase64") %>' class="img-thumb shadow-sm"
                                        onclick='<%# "showImage(\"" + Eval("StackImageBase64") + "\")" %>'
                                        data-bs-toggle="modal" data-bs-target="#imgModal"
                                        onerror="this.src='https://placehold.co/60x60?text=NA'" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Commodity Image">
                                <ItemTemplate>
                                    <img src='<%# Eval("CommodityImageBase64") %>' class="img-thumb shadow-sm"
                                        onclick='<%# "showImage(\"" + Eval("CommodityImageBase64") + "\")" %>'
                                        data-bs-toggle="modal" data-bs-target="#imgModal"
                                        onerror="this.src='https://placehold.co/60x60?text=NA'" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </main>
        <div class="modal fade" id="imgModal" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow-lg" style="border-radius: 20px;">
                    <div class="modal-header border-0">
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body p-0 text-center">
                        <img id="modalImage" class="img-fluid" style="border-radius: 0 0 20px 20px; max-height: 100vh;" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        function showImage(src) {
            document.getElementById("modalImage").src = src;
        }
    </script>
</body>
</html>
