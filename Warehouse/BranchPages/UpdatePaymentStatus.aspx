<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePaymentStatus.aspx.cs" Inherits="JointVentureScheme_UpdatePaymentStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Payment Status | JVS</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" />
    <style>
        body { background-color: #f0f2f5; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        .navbar { background: #1a237e; color: white; margin-bottom: 30px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .card { border: none; border-radius: 10px; box-shadow: 0 4px 6px rgba(0,0,0,0.05); }
        .card-header { background-color: #fff; border-bottom: 1px solid #eee; font-weight: bold; color: #1a237e; }
        .btn-primary { background-color: #1a237e; border: none; }
        .gv-container { background: white; border-radius: 10px; padding: 15px; }
        .table th { background-color: #f8f9fa; color: #333; white-space: nowrap; font-size: 0.9rem; }
        .table td { vertical-align: middle; font-size: 0.85rem; }
        .modalBackground { background-color: rgba(0,0,0,0.5); }
        .modalPopup { background: white; border-radius: 8px; padding: 20px; text-align: center; border: 1px solid #ccc; }
        .icon-btn { font-size: 1.3rem; vertical-align: middle; }
    </style>
    <script type="text/javascript">
        function filterGrid() {
            var input = document.getElementById('<%= txtGridSearch.ClientID %>');
            var filter = input.value.toUpperCase();
            var table = document.getElementById('<%= gvGodown.ClientID %>');
            if (!table) return;
            var tr = table.getElementsByTagName("tr");
            for (var i = 1; i < tr.length; i++) {
                var display = false;
                var td = tr[i].getElementsByTagName("td");
                for (var j = 0; j < td.length; j++) {
                    if (td[j] && (td[j].textContent || td[j].innerText).toUpperCase().indexOf(filter) > -1) {
                        display = true; break;
                    }
                }
                tr[i].style.display = display ? "" : "none";
            }
        }
    </script>
</head>
<body onload="window.history.forward();">
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="tsm1" runat="server"></cc1:ToolkitScriptManager>

        <nav class="navbar p-3">
            <div class="container-fluid d-flex justify-content-between align-items-center">
                <div>
                    <asp:LinkButton ID="btnHome" runat="server" CssClass="text-white me-3" OnClick="btnHome_Click" ToolTip="Home">
                        <i class="bi bi-house-door-fill icon-btn"></i>
                    </asp:LinkButton>
                    <span class="h5 m-0 text-white">Update Payment Status</span>
                </div>
                <div class="text-white">
                    Welcome, <b><asp:Label ID="lbluser" runat="server"></asp:Label></b> | 
                    <asp:LinkButton ID="LinkButton7" runat="server" CssClass="text-white ms-2" OnClick="LinkButton7_Click" ToolTip="Logout">
                        <i class="bi bi-box-arrow-right icon-btn"></i>
                    </asp:LinkButton>
                </div>
            </div>
        </nav>

        <div class="container-fluid px-4">
            <div class="card mb-4">
                <div class="card-header">Search Database</div>
                <div class="card-body">
                    <div class="row g-3">
                        <div class="col-md-5">
                            <label class="form-label small fw-bold">Registration ID</label>
                            <asp:TextBox ID="txtRegID" runat="server" CssClass="form-control" placeholder="Enter Reg ID"></asp:TextBox>
                        </div>
                        <div class="col-md-5">
                            <label class="form-label small fw-bold">Bank Reference No</label>
                            <asp:TextBox ID="txtBankRefNo" runat="server" CssClass="form-control" placeholder="Enter Bank Ref No"></asp:TextBox>
                        </div>
                        <div class="col-md-2 d-flex align-items-end">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary w-100" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlGridArea" runat="server" Visible="false">
                <div class="gv-container card">
                    <div class="card-header d-flex justify-content-between align-items-center bg-light">
                        <div class="d-flex align-items-center w-50">
                            <span class="me-3 fw-bold">Results</span>
                            <div class="input-group input-group-sm w-75">
                                <span class="input-group-text bg-white"><i class="bi bi-search"></i></span>
                                <asp:TextBox ID="txtGridSearch" runat="server" CssClass="form-control" placeholder="Quick Filter grid..." onkeyup="filterGrid()"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Button ID="btnReset" runat="server" Text="Clear" CssClass="btn btn-sm btn-outline-secondary" OnClick="Button3_Click" />
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" 
                            CssClass="table table-hover mt-3" GridLines="None" 
                            DataKeyNames="TId" OnSelectedIndexChanged="gvGodown_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No."><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField>
                                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" />
                                <asp:BoundField DataField="BankReferenceNo" HeaderText="BankReference No" />
                                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:N2}" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:BoundField DataField="REGISTRATIONID" HeaderText="Registration ID" />
                                <asp:BoundField DataField="NAMEOFDEPOSITOR" HeaderText="Name of Depositor" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Select" CssClass="btn btn-sm btn-info text-white" ToolTip="Edit">
                                            <i class="bi bi-pencil-square"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate><div class="text-center p-4">No records found.</div></EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="TRHide" runat="server" Visible="false" CssClass="card mt-4 border-success">
                <div class="card-header bg-success text-white">Modify Record Details</div>
                <div class="card-body">
                    <div class="row g-2">
                        <div class="col-md-3">
                            <label class="form-label small fw-bold">Category</label>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select">
                                <asp:ListItem Text="REGISTRATION FEE" Value="REGISTRATION FEE"></asp:ListItem>
                                <asp:ListItem Text="OFFER FEES" Value="OFFER FEES"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label small fw-bold">Registration ID</label>
                            <asp:TextBox ID="txtRegistrationID" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label small fw-bold">Bank Reference</label>
                            <asp:TextBox ID="txtBankRefEdit" runat="server" CssClass="form-control bg-light" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label small fw-bold">Amount / Fees</label>
                            <asp:TextBox ID="txtFees" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="text-center mt-3">
                        <asp:Button ID="btnUpdateCpt" runat="server" Text="Update Status" CssClass="btn btn-success px-5" OnClick="btnUpdateCpt_Click" />
                    </div>
                </div>
            </asp:Panel>
        </div>

        <asp:Label ID="Label5" runat="server" style="display:none;"></asp:Label>
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlofferpopup" TargetControlID="Label5" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
        <asp:Panel ID="pnlofferpopup" runat="server" CssClass="modalPopup" style="display:none; width:400px;">
            <div class="mb-3"><h4 class="text-success">Success!</h4></div>
            <p>Record has been updated successfully.</p>
            <asp:Button ID="ButtonRefresh" runat="server" Text="OK" CssClass="btn btn-primary" OnClick="Button3_Click" />
        </asp:Panel>
        <asp:HiddenField ID="hdnTid" runat="server" Value="0" />
    </form>
</body>
</html>