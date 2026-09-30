<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/JointVentureScheme/District_Wise_Offerd_Capacity_details_2026_27.aspx.cs" Inherits="JointVentureScheme_District_Wise_Offerd_Capacity_details_2026_27" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Branch Wise Capacity Report | Warehouse Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />

    <style>
        body {
            background-color: #f4f7f6;
            font-family: 'Segoe UI', Roboto, sans-serif;
        }

        .navbar-custom {
            background: #003366;
            color: white;
            padding: 15px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }

        .main-container {
            margin-top: -30px;
            position: relative;
            z-index: 10;
        }

        .header-section {
            background: #003366;
            height: 150px;
            color: white;
            padding-top: 20px;
        }

        .card-custom {
            border: none;
            border-radius: 12px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
            border-left: 5px solid #003366;
        }

        .stat-icon {
            font-size: 2rem;
            opacity: 0.3;
            position: absolute;
            right: 15px;
            top: 15px;
        }

        .grid-holder {
            background: white;
            border-radius: 12px;
            padding: 25px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.08);
        }

        .table thead {
            background-color: #f8f9fa;
            border-bottom: 2px solid #dee2e6;
            text-transform: uppercase;
            font-size: 0.85rem;
            letter-spacing: 1px;
        }

        .table-hover tbody tr:hover {
            background-color: #f1f5f9;
            cursor: pointer;
        }

        .dropdown-box {
            border-radius: 8px;
            border: 1px solid #ced4da;
            padding: 10px;
        }

        .btn-export {
            border-radius: 8px;
            font-weight: 600;
            transition: all 0.3s;
        }

            .btn-export:hover {
                transform: scale(1.05);
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="header-section text-center">
            <h2 class="fw-bold"><i class="fas fa-warehouse me-2"></i>Branch Wise Capacity Report</h2>
            <p class="opacity-75">Warehouse Monitoring System - Rabi 2026-27</p>
        </div>
        <div class="row mb-3">
            <div class="col-md-6">
                <asp:HyperLink ID="btnBack" runat="server"
                    NavigateUrl="~/JointVentureScheme/JVSDistrictofferReport.aspx"
                    CssClass="btn btn-outline-secondary shadow-sm">
                <i class="fas fa-arrow-left me-2"></i>Back to Main Report
            </asp:HyperLink>
            </div>
        </div>
        <div class="container main-container">
            <div class="row mb-4">
                <div class="col-md-4">
                    <div class="card card-custom p-3 bg-white mb-3">
                        <i class="fas fa-code-branch stat-icon text-primary"></i>
                        <h6 class="text-muted">Total Branches</h6>
                        <h2 class="fw-bold">
                            <asp:Literal ID="litTotalBranches" runat="server">0</asp:Literal></h2>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-custom p-3 bg-white mb-3" style="border-left-color: #28a745;">
                        <i class="fas fa-building stat-icon text-success"></i>
                        <h6 class="text-muted">Godowns Count</h6>
                        <h2 class="fw-bold">
                            <asp:Literal ID="litTotalGodowns" runat="server">0</asp:Literal></h2>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-custom p-3 bg-white mb-3" style="border-left-color: #17a2b8;">
                        <i class="fas fa-weight-hanging stat-icon text-info"></i>
                        <h6 class="text-muted">Total Capacity (MT)</h6>
                        <h2 class="fw-bold">
                            <asp:Literal ID="litTotalCapacity" runat="server">0.00</asp:Literal></h2>
                    </div>
                </div>
            </div>

            <div class="grid-holder">
                <div class="table-responsive">
                    <asp:GridView ID="gvBranchReport" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="BranchId" OnRowCommand="gvBranchReport_RowCommand"
                        CssClass="table table-hover align-middle border-0" GridLines="None">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle Width="80px" ForeColor="#666" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                            <%--<asp:BoundField DataField="Branch_Name" HeaderText="Branch (Depot)" HeaderStyle-CssClass="text-primary fw-bold" />--%>
                            <asp:TemplateField HeaderText="Branch (Depot)">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBranch" runat="server"
                                        Text='<%# Eval("Branch_Name") %>'
                                        CommandName="ShowDetails"
                                        CommandArgument='<%# Eval("BranchId") %>'
                                        CssClass="fw-bold text-decoration-none text-primary">
                               </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No. of Godowns">
                                <ItemTemplate>
                                    <span class="badge rounded-pill bg-light text-dark border px-3 py-2">
                                        <%# Eval("Total_Godowns") %>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Total_Offered_Capacity" HeaderText="Total Capacity (MT)"
                                DataFormatString="{0:N2}" ItemStyle-CssClass="fw-bold text-success" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="text-center py-5">
                                <i class="fas fa-folder-open fa-3x text-muted mb-3"></i>
                                <p class="text-muted">Selected district ke liye koi data nahi mila.</p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="modal fade" id="detailsModal" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header bg-primary text-white">
                        <h5 class="modal-title"><i class="fas fa-list me-2"></i>Godown Wise Details</h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <h6 id="modalBranchName" class="text-muted mb-3"></h6>
                        <div class="table-responsive">
                            <asp:GridView ID="gvGodownDetails" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-bordered table-sm small">
                                <Columns>
                                    <asp:BoundField DataField="Warehouse_Name" HeaderText="Warehouse Name" />
                                    <asp:BoundField DataField="Registration_Id" HeaderText="Reg. ID" />
                                    <asp:BoundField DataField="Offer_Capacity" HeaderText="Capacity (MT)" />
                                    <asp:BoundField DataField="OfferedDate" HeaderText="Offered Date" />
                                    <asp:BoundField DataField="Choise_Filling" HeaderText="Choise Filling" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        <script type="text/javascript">
            function openModal() {
                var myModal = new bootstrap.Modal(document.getElementById('detailsModal'));
                myModal.show();
            }
</script>
    </form>
</body>
</html>
