<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckValidity_For_Offer.aspx.cs" Inherits="StatePages_CheckValidity_For_Offer" %>

<!DOCTYPE html>
<html lang="hi">
<head runat="server">
    <title>Licence Validity Check</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { background-color: #f8f9fa; padding: 20px; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        .card { border-radius: 10px; box-shadow: 0 4px 8px rgba(0,0,0,0.1); }
        .header-bg { background-color: #0d6efd; color: white; border-radius: 10px 10px 0 0; padding: 15px; }
        .status-badge { padding: 5px 12px; border-radius: 20px; font-size: 0.9em; font-weight: bold; }
        .status-Y { background-color: #d1e7dd; color: #0f5132; border: 1px solid #badbcc; } /* Green */
        .status-N { background-color: #f8d7da; color: #842029; border: 1px solid #f5c2c7; } /* Red */
    </style>
</head>
<body>
<form id="form1" runat="server" class="container">

    <div class="row justify-content-center">
        <div class="col-md-12 mt-4">
            <div class="card">
                <div class="header-bg">
                    <h3 class="mb-0">Licence Validity For Offer (Rabi 2026-27)</h3>
                </div>
                
                <div class="card-body">
                    <div class="row g-3 align-items-center mb-4">
                        <div class="col-auto">
                            <label class="col-form-label fw-bold">Registration ID:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRegistrationID" runat="server" CssClass="form-control" placeholder="Enter Registration ID"></asp:TextBox>
                        </div>
                        <div class="col-auto">
                            <asp:Button ID="btnSearch" runat="server" Text="Check Status" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                        </div>
                    </div>

                    <hr />

                    <div class="table-responsive">
                        <asp:GridView ID="gvLicence" runat="server" AutoGenerateColumns="false" 
                            CssClass="table table-hover table-bordered align-middle" GridLines="None">
                            <HeaderStyle CssClass="table-dark" />
                            <Columns>
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown_Name" />
                                <asp:BoundField DataField="JVS_RegNo" HeaderText="Registration ID" />
                                <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                                <asp:BoundField DataField="LicNum" HeaderText="Licence No" />
                                <asp:BoundField DataField="LicDate" HeaderText="Issue Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:BoundField DataField="Valid_date" HeaderText="Valid Till" DataFormatString="{0:dd-MMM-yyyy}" />
                                
                                <asp:TemplateField HeaderText="Days Remaining">
                                    <ItemTemplate>
                                        <span class='fw-bold <%# Convert.ToInt32(Eval("Days_Until_Expiry")) < 15 ? "text-danger" : "text-dark" %>'>
                                            <%# Eval("Days_Until_Expiry") %> Days
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Offer Eligibility">
                                    <ItemTemplate>
                                        <span class='status-badge <%# Eval("Offer_Status_Flag").ToString() == "Y" ? "status-Y" : "status-N" %>'>
                                            <%# Eval("Offer_Status_Flag").ToString() == "Y" ? "ELIGIBLE ✅" : "NOT ELIGIBLE ❌" %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-warning text-center">No Record Found for the entered Registration ID.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
           <%-- <p class="text-muted mt-2 small text-center">* Validity must be 15 days or more from today to participate in offers.</p>--%>
        </div>
    </div>

</form>
</body>
</html>