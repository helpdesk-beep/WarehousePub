<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="~/Inspections/BO/Godown_Wise_Inspection_Details_BO.aspx.cs" Inherits="Inspections_BO_Godown_Wise_Inspection_Details_BO" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Godown Inspection Summary</title>
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
        }

        .summary-card-v2 {
            padding: 15px;
            border-radius: 15px;
            background: rgba(255,255,255,0.9);
            backdrop-filter: blur(10px);
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
            transition: 0.3s;
            margin-bottom: 15px;
        }

            .summary-card-v2 .value {
                font-size: 22px;
                font-weight: 700;
                display: block;
            }

            .summary-card-v2 .title {
                font-size: 13px;
                color: #555;
                text-transform: uppercase;
                letter-spacing: 1px;
            }

        .grid-container {
            height: 600px;
            overflow-y: auto;
            background: #fff;
            border-radius: 10px;
            padding: 10px;
        }

        .custom-grid {
            width: 100% !important;
            font-size: 12px;
        }

            .custom-grid thead th {
                position: sticky;
                top: 0;
                z-index: 20;
                background-color: #3f51b5 !important;
                color: white;
                text-align: center;
                padding: 10px;
            }

        /* Border colors for cards like the target design */
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

        .search-section {
            background: white;
            padding: 15px;
            border-radius: 15px;
            margin-bottom: 20px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="full-width-container mt-3">

            <%-- Header --%>
            <div class="header-box text-center mb-4">
                <h2>📊 Godown Wise PV Inspection Details</h2>
            </div>
            <%-- Search and Export --%>
            <div class="search-section">
                <div class="row align-items-center">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                            placeholder="🔍 Search by Godown, Officer or District..." onkeyup="SearchGrid()" />
                    </div>
                    <div class="col-md-6 text-end">
                        <asp:Button ID="btnExport" runat="server" Text="📥 Export to Excel"
                            CssClass="btn btn-success shadow-sm" OnClick="btnExport_Click" />
                    </div>
                </div>
            </div>

            <%-- GridView --%>
            <div class="grid-container shadow-sm">
                <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-hover table-bordered custom-grid"
                    ShowFooter="true" OnRowCommand="gvGodown_RowCommand"
                    OnRowDataBound="gvGodown_RowDataBound" EmptyDataText="No Inspection Records Found">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Officer_Name" HeaderText="Officer" />
                        <asp:BoundField DataField="District_Name" HeaderText="District" />
                        <asp:BoundField DataField="DepotName" HeaderText="Depot" />

                        <asp:TemplateField HeaderText="Godown Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkGodown" runat="server"
                                    Text='<%# Eval("Godown_Name") %>' CommandName="ViewStack"
                                    CommandArgument='<%# Eval("GodownID") %>'
                                    CssClass="text-godown fw-bold">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate><b>Grand Total</b></FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Online Stack">
                            <ItemTemplate><%# Eval("Online_Stack") %></ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalOnlineStack" runat="server" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Online Bags">
                            <ItemTemplate><%# Eval("OnlineBags") %></ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalOnlineBags" runat="server" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Insp. Stack">
                            <ItemTemplate>
                                <span class="badge bg-primary-subtle text-primary border border-primary-subtle">
                                    <%# Eval("Insp_Stack") %>
                                </span>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalInspStack" runat="server" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bags (PV)">
                            <ItemTemplate><%# Eval("TotalBags_AsPerPV") %></ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalPV" runat="server" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Spillage">
                            <ItemTemplate><span class="text-danger fw-bold"><%# Eval("SpillageBags_AsPerPV") %></span></ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalSpillage" runat="server" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Diff.">
                            <ItemTemplate>
                                <%# Convert.ToInt32(Eval("TotalBags_AsPerPV") ?? 0) - Convert.ToInt32(Eval("OnlineBags") ?? 0) %>
                            </ItemTemplate> 
                            <FooterTemplate>
                                <asp:Label ID="lblTotalDiff" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Diff.">
                            <ItemTemplate>
                                <%# 
                                    Convert.ToInt32(Eval("TotalBags_AsPerPV") == DBNull.Value ? 0 : Eval("TotalBags_AsPerPV")) - 
                                    Convert.ToInt32(Eval("OnlineBags") == DBNull.Value ? 0 : Eval("OnlineBags")) 
                                %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalDiff" runat="server" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Godown_Submit_date" HeaderText="Godown Submit date" />
                        <asp:TemplateField HeaderText="Without Image">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkGodownStack" runat="server"
                                    Text="Godown Wise Stack Without Image"
                                    CommandName="ViewGodownWiseStack"
                                    CommandArgument='<%# Eval("GodownID") %>'
                                    CssClass="text-godown fw-bold">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
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
                        found = true; break;
                    }
                }
                tr[i].style.display = found ? "" : "none";
            }
        }
    </script>
</body>
</html>
