<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteGodownAgreement_For_Rabi_2023_24.aspx.cs" Inherits="JointVentureScheme_DeleteGodownAgreement_For_Rabi_2023_24" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Godown Agreement Management</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .navbar-custom {
            background-color: #0056b3;
            color: white;
        }

        .card-header-custom {
            background-color: #719cb6;
            color: white;
            font-weight: bold;
        }

        .btn-icon {
            background: none;
            border: none;
            padding: 0;
            color: inherit;
            cursor: pointer;
        }

        .grid-header th {
            background-color: #719cb6 !important;
            color: white !important;
            text-align: center;
        }

        .footer-logo {
            max-height: 50px;
        }
    </style>
</head>
<body onload="noBack();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>

        <nav class="navbar navbar-custom shadow-sm mb-4">
            <div class="container-fluid d-flex justify-content-between">
                <div>
                    <asp:LinkButton ID="linkHome" runat="server" CssClass="text-white text-decoration-none me-3" PostBackUrl="~/JointVentureScheme/DistrictWiseJVSOffer.aspx">
                        <i class="fas fa-home"></i> Home
                    </asp:LinkButton>
                </div>
                <div class="text-white">
                    <i class="fas fa-user-circle"></i>Welcome:
                    <asp:Label ID="lblUser" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div>
                    <asp:LinkButton ID="lnkLogout" runat="server" CssClass="text-white text-decoration-none" OnClick="lnkLogout_Click">
                        <i class="fas fa-sign-out-alt"></i> Logout
                    </asp:LinkButton>
                </div>
            </div>
        </nav>

        <div class="container-fluid">
            <div class="card shadow">
                <div class="card-header card-header-custom text-center">
                    <h5 class="mb-0">View & Manage Godown Agreement Status</h5>
                </div>
                <div class="card-body">
                    <div class="row g-3 align-items-center justify-content-center mb-4">
                        <div class="col-auto">
                            <label class="form-label fw-bold">Season:</label>
                            <asp:DropDownList ID="ddl_session" runat="server" CssClass="form-select d-inline-block w-auto"
                                OnSelectedIndexChanged="ddl_session_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                <asp:ListItem Value="Previous">Previous</asp:ListItem>
                                <asp:ListItem Value="Rab2026_27" Selected="True">Rab2026_27</asp:ListItem>
                                <asp:ListItem Value="Rab2025_26">Rab2025_26</asp:ListItem>
                                <asp:ListItem Value="Kharif2024_25">Kharif2024_25</asp:ListItem>
                                <asp:ListItem Value="Rab2024_25">Rab2024_25</asp:ListItem>
                                <asp:ListItem Value="Kharif2023_24">Kharif2023_24</asp:ListItem>
                                <asp:ListItem Value="Rab2023_24">Rabi 2023-24</asp:ListItem>
                                <asp:ListItem Value="Kharif_2022_23">Kharif_2022_23</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-auto">
                            <label class="form-label fw-bold">Registration ID:</label>
                            <asp:TextBox ID="txtSearch" runat="server"
                                CssClass="form-control d-inline-block w-auto"
                                placeholder="Enter ID..."
                                onkeydown="return clickButton(event, 'btnSearch');"></asp:TextBox>
                        </div>
                        <div class="col-auto">
                            <div class="col-auto">
        <asp:LinkButton ID="btnSearch" runat="server" 
            CssClass="btn btn-primary d-flex align-items-center justify-content-center" 
            style="height: 38px; padding: 0 60px;" 
            OnClick="btnSearch_Click">
            <i class="fas fa-search me-2"></i> Search
        </asp:LinkButton>
    </div>

                        <script type="text/javascript">
                            function clickButton(e, buttonid) {
                                var evt = e ? e : window.event;
                                var bt = document.getElementById(buttonid);
                                if (bt) {
                                    if (evt.keyCode == 13) {
                                        bt.click();
                                        return false;
                                    }
                                }
                            }
                        </script>
                    </div>

                    <div class="table-responsive" style="max-height: 400px;">
                        <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover text-center align-middle"
                            OnSelectedIndexChanged="gvGodown_SelectedIndexChanged">
                            <HeaderStyle CssClass="grid-header" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnAgreementID" runat="server" Value='<%# Eval("Agreement_Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Inspection_Id" HeaderText="Inspection ID" />
                                <asp:BoundField DataField="Registration_Id" HeaderText="Registration ID" />
                                <asp:BoundField DataField="GodownId" HeaderText="Godown ID" />
                                <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse Name" />
                                <asp:BoundField DataField="Godown_No" HeaderText="Godown No" />
                                <asp:BoundField DataField="G_OfferCapacity" HeaderText="Offer Cap" />
                                <asp:BoundField DataField="Vacant_Capacity" HeaderText="Vacant Cap" />
                                <asp:BoundField DataField="Fit_Unfit" HeaderText="Status" />
                                <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                <asp:BoundField DataField="Agree_Capacity" HeaderText="Agreement Cap" />
                                <asp:BoundField DataField="Agreement_Id" HeaderText="Agreement ID" />
                                <asp:BoundField DataField="Agree_Sign_Date" HeaderText="Date" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Select" CssClass="text-danger" OnClientClick="return confirm('Are you sure you want to delete?');">
                                            <i class="fas fa-trash-alt fa-lg"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="card mt-4 mb-5 shadow-sm">
                <div class="card-header bg-secondary text-white">
                    <h6 class="mb-0">Registration Year Details</h6>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="grdjvsyear" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-striped text-center">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo.">
                                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Registration_Id" HeaderText="Registration ID" />
                                <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                                <asp:BoundField DataField="JVS_Year" HeaderText="JVS Year" />
                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <footer class="bg-light border-top pt-4">
            <div class="container text-center">
                <div class="row align-items-center">
                    <div class="col-md-3">
                        <img src="../Images/NIC-logo.png" class="footer-logo" alt="NIC" />
                    </div>
                    <div class="col-md-6 small text-muted">
                        <strong>© 2026 National Informatics Centre. All Rights Reserved.</strong><br />
                        Developed by NIC Madhya Pradesh
                    </div>
                    <div class="col-md-3">
                        <img src="../Images/natindialogo.png" class="footer-logo me-2" alt="India" />
                        <img src="../Images/di.png" class="footer-logo" alt="Digital India" />
                    </div>
                </div>
            </div>
        </footer>
    </form>
    <script type="text/javascript">
        function noBack() { window.history.forward(); }
    </script>
</body>
</html>
