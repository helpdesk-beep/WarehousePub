<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteJVSOffer_Rabi_2026_27.aspx.cs" Inherits="JointVentureScheme_DeleteJVSOffer_Rabi_2026_27" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JVS | Admin Dashboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <style>
        body { background-color: #f0f2f5; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        .navbar-custom { background: linear-gradient(135deg, #008CBA 0%, #005f7a 100%); padding: 1rem 2rem; box-shadow: 0 4px 12px rgba(0,0,0,0.1); }
        .main-card { border: none; border-radius: 15px; box-shadow: 0 10px 30px rgba(0,0,0,0.05); margin-top: -30px; }
        .card-header-custom { background: white; border-bottom: 1px solid #eee; padding: 1.5rem; border-radius: 15px 15px 0 0 !important; }
        
        /* Professional Grid Styling */
        .custom-grid { border: none !important; border-collapse: separate !important; border-spacing: 0 8px !important; width: 100% !important; }
        .custom-grid th { background-color: #8086bd  !important; color: #ffffff !important; font-weight: 600; text-transform: uppercase; font-size: 0.75rem; letter-spacing: 0.05em; padding: 15px !important; border: none !important; }
        .custom-grid td { background-color: white !important; padding: 15px !important; border-top: 1px solid #f1f5f9 !important; border-bottom: 1px solid #f1f5f9 !important; font-size: 0.9rem; vertical-align: middle !important; }
        .custom-grid tr:hover td { background-color: #f1f5f9 !important; transition: 0.2s; }
        .custom-grid td:first-child { border-left: 1px solid #f1f5f9 !important; border-radius: 10px 0 0 10px; }
        .custom-grid td:last-child { border-right: 1px solid #f1f5f9 !important; border-radius: 0 10px 10px 0; }
        
        .footer-summary { background-color: #e2e8f0 !important; font-weight: bold; }
        .search-container { background: white; padding: 2rem; border-radius: 15px; box-shadow: 0 4px 6px rgba(0,0,0,0.02); }
        .badge-total { padding: 0.5rem 1rem; border-radius: 8px; font-weight: 600; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
        
        <nav class="navbar navbar-custom d-flex justify-content-between align-items-center text-white mb-5">
            <asp:LinkButton ID="LinkButton10" runat="server" PostBackUrl="~/JointVentureScheme/DistrictWiseJVSOffer.aspx" CssClass="text-white text-decoration-none fw-bold">
                <i class="fa-solid fa-house-chimney me-2"></i>Dashboard
            </asp:LinkButton>
            <div class="d-flex align-items-center gap-4">
                <span><i class="fa-solid fa-user-circle me-1"></i>Welcome, <asp:Label ID="lbluser" runat="server" Font-Bold="true"></asp:Label></span>
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton1_Click" CssClass="btn btn-sm btn-light text-primary fw-bold px-3 rounded-pill">Logout</asp:LinkButton>
            </div>
        </nav>

        <div class="container pb-5">
            <div class="card main-card">
                <div class="card-header-custom text-center">
                    <h4 class="text-dark fw-bold mb-1">Capacity Management</h4>
                    <p class="text-muted small mb-0">Rabi Season 2026-27 | Data Deletion Portal</p>
                </div>
                
                <div class="card-body p-4">
                    <div class="row justify-content-center mb-5">
                        <div class="col-md-7">
                            <div class="input-group shadow-sm rounded-pill overflow-hidden border">
                                <span class="input-group-text bg-white border-0 ps-4"><i class="fa-solid fa-search text-muted"></i></span>
                                <asp:TextBox ID="txtRegID" runat="server" CssClass="form-control border-0 py-3" placeholder="Enter Registration ID..."></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Text="Search Records" CssClass="btn btn-primary px-4 fw-bold" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>

                    <div id="toexport" runat="server" class="table-responsive mb-4">
                        <asp:GridView ID="RegGrid" runat="server" AutoGenerateColumns="False" 
                            CssClass="custom-grid" GridLines="None"
                            DataKeyNames="Registration_Id" ShowFooter="true" OnRowDeleting="RegGrid_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                        <asp:HiddenField ID="hdnOffer_Id" runat="server" Value='<%# Eval("Offer_Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Warehouse_Name" HeaderText="Warehouse" />
                                <asp:BoundField DataField="Auth_Person" HeaderText="Person" />
                                <asp:BoundField DataField="MobileNo" HeaderText="Contact" />
                                <asp:BoundField DataField="Registration_Id" HeaderText="Reg ID" />
                                <asp:BoundField DataField="RegCapacity" HeaderText="Reg. Cap (MT)" DataFormatString="{0:N2}" />
                                <asp:BoundField DataField="Offer_Capacity" HeaderText="Offered (MT)" DataFormatString="{0:N2}" />
                                <asp:BoundField DataField="OfferedDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" CssClass="text-danger fs-5"
                                            OnClientClick="return confirm('Security Check: Proceed with record deletion?');">
                                            <i class="fa-solid fa-trash-arrow-up"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="footer-summary" />
                        </asp:GridView>
                    </div>

                    <div class="d-flex justify-content-center flex-wrap gap-3 mb-4">
                        <asp:Label ID="Label2" runat="server" CssClass="text-secondary mt-1" Text="Total Warehouses:" Visible="false"></asp:Label>
                        <asp:Label ID="Label3" runat="server" CssClass="badge-total bg-info text-white" Visible="false"></asp:Label>
                        <asp:Label ID="Label4" runat="server" CssClass="text-secondary mt-1 ms-3" Text="Total Offered (MT):" Visible="false"></asp:Label>
                        <asp:Label ID="Label5" runat="server" CssClass="badge-total bg-success text-white" Visible="false"></asp:Label>
                        <asp:Button ID="Button1" runat="server" Text="Download Excel" CssClass="btn btn-outline-success btn-sm ms-3 rounded-pill px-4" Visible="false" OnClick="Button1_Click1" />
                    </div>
                </div>
            </div>

            <div id="divDetail" runat="server" visible="false" class="card mt-4 border-0 shadow-sm rounded-4 overflow-hidden">
              <div class="card-header bg-secondary text-white py-3" style="background-color: rgb(145 195 138) !important;">
                    <h6 class="mb-0 fw-bold"><i class="fa-solid fa-list-ul me-2"></i>Part-Wise Capacity Breakdown</h6>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CssClass="table table-hover align-middle mb-0" GridLines="None" ShowFooter="true">
                        <HeaderStyle CssClass="bg-light text-secondary small text-uppercase" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN"><ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate></asp:TemplateField>
                            <asp:BoundField DataField="Registration_Id" HeaderText="Reg ID" />
                            <asp:BoundField DataField="Godown_No" HeaderText="Godown Number" />
                            <asp:BoundField DataField="G_OfferCapacity" HeaderText="Offered (MT)" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="Offer_Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                        </Columns>
                        <FooterStyle CssClass="table-active fw-bold" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>