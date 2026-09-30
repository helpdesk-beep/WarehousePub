<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Inspections/Inspection_Officer/GodownWiseSummary_For_Mobile_Inspection.aspx.cs" Inherits="Inspections_Inspection_Officer_GodownWiseSummary_For_Mobile_Inspection" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Inspection Summary | Smart Warehouse</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;600;700&display=swap" rel="stylesheet">

    <style>
        body {
            background-color: #f3f4f7;
            font-family: 'Inter', sans-serif;
            color: #2d3436;
        }

        .page-header {
            background: linear-gradient(135deg, #0984e3, #74b9ff);
            padding: 30px 0 60px 0;
            color: white;
            text-align: center;
            border-bottom-left-radius: 40px;
            border-bottom-right-radius: 40px;
            box-shadow: 0 10px 25px rgba(9, 132, 227, 0.2);
        }

        .page-header h2 {
            font-weight: 700;
            margin-bottom: 5px;
        }

        .action-wrapper {
            margin-top: -35px;
            margin-bottom: 25px;
        }

        .glass-card {
            background: rgba(255, 255, 255, 0.98);
            backdrop-filter: blur(10px);
            border-radius: 20px;
            box-shadow: 0 15px 35px rgba(0,0,0,0.06);
            padding: 20px;
            border: 1px solid rgba(255, 255, 255, 0.3);
        }

        .search-box {
            border-radius: 12px;
            padding: 10px 20px;
            border: 1px solid #dfe6e9;
            transition: all 0.3s;
        }

        .search-box:focus {
            box-shadow: 0 0 0 4px rgba(9, 132, 227, 0.1);
            border-color: #0984e3;
        }

        .btn-export {
            border-radius: 12px;
            padding: 10px 24px;
            font-weight: 600;
            text-decoration: none;
            transition: transform 0.2s;
        }

        .btn-export:hover {
            transform: translateY(-2px);
        }

        .main-card {
            background: white;
            border-radius: 20px;
            overflow: hidden;
            border: none;
            box-shadow: 0 10px 30px rgba(0,0,0,0.03);
        }

        .table {
            margin-bottom: 0;
        }

        .table thead {
            background-color: #f8f9fa;
            border-bottom: 2px solid #edf2f7;
        }

        .table thead th {
            padding: 15px;
            font-size: 12px;
            text-transform: uppercase;
            color: #636e72;
            font-weight: 700;
            text-align: center;
        }

        .table tbody td {
            padding: 15px;
            vertical-align: middle;
            text-align: center;
            border-bottom: 1px solid #f1f2f6;
            font-size: 14px;
        }

        .badge-count {
            padding: 6px 12px;
            border-radius: 8px;
            font-weight: 700;
        }

        .text-godown {
            color: #0984e3;
            text-decoration: none;
            transition: color 0.2s;
        }

        .text-godown:hover {
            color: #074b81;
            text-decoration: underline;
        }

        @media (max-width: 768px) {
            .page-header {
                border-radius: 0 0 25px 25px;
                padding: 20px 0 50px 0;
            }
            .search-box {
                margin-bottom: 10px;
                width: 100% !important;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header class="page-header">
            <div class="container">
                <h2>Godown Inspection Summary</h2>
                <p class="opacity-75">
                    Records Found: <asp:Label ID="lblCount" runat="server" CssClass="fw-bold" Text="0"></asp:Label>
                </p>
            </div>
        </header>

        <main class="container-fluid px-md-5">
            <div class="action-wrapper">
                <div class="glass-card">
                    <div class="row align-items-center">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtSearch" runat="server"
                                CssClass="form-control search-box w-75"
                                placeholder="🔍 Search by Godown, Officer or District..."
                                onkeyup="SearchGrid()" />
                        </div>
                        <div class="col-md-6 text-md-end mt-3 mt-md-0">
                            <asp:Button ID="btnExport" runat="server"
                                Text="📥 Export to Excel"
                                CssClass="btn btn-success btn-export shadow-sm"
                                OnClick="btnExport_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="main-card mb-5">
                <div class="table-responsive">
                    <asp:GridView ID="gvGodown" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-hover align-middle"
                        ShowFooter="true"
                        GridLines="None"
                        OnRowDataBound="gvGodown_RowDataBound"
                        OnRowCommand="gvGodown_RowCommand"
                        EmptyDataText="No Inspection Records Found">

                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <span class="text-muted"><%# Container.DataItemIndex + 1 %></span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Officer_Name" HeaderText="Officer" />
                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                            <asp:BoundField DataField="DepotName" HeaderText="Depot" />

                            <asp:TemplateField HeaderText="Godown Name">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkGodown" runat="server"
                                        Text='<%# Eval("Godown_Name") %>'
                                        CommandName="ViewStack"
                                        CommandArgument='<%# Eval("GodownID") + "|" + Eval("BranchId") %>'
                                        CssClass="text-godown fw-bold">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div class="text-center fw-bold">Grand Total</div>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Online Stack">
                                <ItemTemplate>
                                    <span class="badge bg-success-subtle text-success border border-success-subtle badge-count">
                                        <%# Eval("Online_Stack") %>
                                    </span>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalOnlineStack" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Online Bags">
                                <ItemTemplate><%# Eval("OnlineBags") %></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalOnlineBags" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Insp. Stack">
                                <ItemTemplate>
                                    <span class="badge bg-primary-subtle text-primary border border-primary-subtle badge-count">
                                        <%# Eval("Insp_Stack") %>
                                    </span>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalInspStack" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="Bags (PV)">
                                <ItemTemplate><%# Eval("TotalBags_AsPerPV") %></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalPV" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Spillage">
                                <ItemTemplate>
                                    <span class="text-danger fw-bold"><%# Eval("SpillageBags_AsPerPV") %></span>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalSpillage" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Diff.">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowDiff" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalDiff" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="Godown_Submit_date" HeaderText="Godown Submit date" />

                            <asp:TemplateField HeaderText="Without Image">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkGodownStack" runat="server"
                                        Text="Godown Wise Stack Without Image"
                                        CommandName="ViewGodownWiseStack"
                                        CommandArgument='<%# Eval("GodownID") + "|" + Eval("BranchId") %>'
                                        CssClass="text-godown fw-bold">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </main>
    </form>

    <script>
        function SearchGrid() {
            var input = document.getElementById('<%= txtSearch.ClientID %>');
            var filter = input.value.toLowerCase();
            var table = document.getElementById('<%= gvGodown.ClientID %>');
            if (!table) return;

            var tr = table.getElementsByTagName("tr");

            for (var i = 1; i < tr.length; i++) {
                if (tr[i].parentElement.tagName.toLowerCase() === 'tfoot') continue;

                var td = tr[i].getElementsByTagName("td");
                var found = false;

                for (var j = 0; j < td.length; j++) {
                    if (td[j] && td[j].innerText.toLowerCase().indexOf(filter) > -1) {
                        found = true;
                        break;
                    }
                }
                tr[i].style.display = found ? "" : "none";
            }
        }
    </script>
</body>
</html>