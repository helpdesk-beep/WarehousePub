<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertPaymentStatus.aspx.cs" Inherits="JointVentureScheme_InsertPaymentStatus" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Payment Status Entry</title>
    <style>
        body { font-family: 'Segoe UI', Arial, sans-serif; background: #f4f4f4; padding: 20px; }
        .form-container { background: #fff; padding: 25px; border-radius: 8px; box-shadow: 0 0 15px rgba(0,0,0,0.1); max-width: 1250px; margin: auto; }
        .grid-view { margin-top: 20px; width: 100%; border-collapse: collapse; font-size: 13px; }
        .grid-view th { background: #007bff; color: white; padding: 12px; text-align: left; }
        .grid-view td { padding: 10px; border: 1px solid #ddd; }
        .input-row { display: grid; grid-template-columns: repeat(4, 1fr); gap: 15px; margin-bottom: 20px; }
        input, select { padding: 8px; width: 100%; box-sizing: border-box; border: 1px solid #ccc; border-radius: 4px; }
        .btn { padding: 8px 15px; cursor: pointer; border: none; border-radius: 4px; color: white; font-weight: bold; margin: 2px; }
        .btn-add { background: #28a745; width: 100%; grid-column: span 4; font-size: 16px; height: 40px; }
        .btn-save { background: #007bff; width: 100%; margin-top: 20px; font-size: 16px; height: 45px; }
        .btn-edit { background-color: #007bff !important; }
        .btn-delete { background-color: #dc3545 !important; }
        .btn-update { background-color: #28a745 !important; }
        .btn-cancel { background-color: #6c757d !important; }
    </style>
</head>
<body>   
    <form id="form1" runat="server">
       <div class="form-container">
            <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
                 <div style="display: flex; gap: 10px;">
     <asp:Button ID="Button1" runat="server" Text="Home" CssClass="btn btn-update" OnClick="btnHome_Click" CausesValidation="false" />
</div>
                <h2 style="margin: 0;">Payment Details Entry</h2>
                <div style="display: flex; gap: 10px;">
                   <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-cancel" OnClick="btnLogout_Click" CausesValidation="false" />
                </div>
            </div>

            <div class="input-row">
                <asp:DropDownList ID="ddlCategory" runat="server">
                    <asp:ListItem Value="">--Select Category--</asp:ListItem>
                    <asp:ListItem>OFFER FEES</asp:ListItem>
                    <asp:ListItem>REGISTRATION FEE</asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList ID="ddlMode" runat="server">
                    <asp:ListItem Value="">--Payment Mode--</asp:ListItem>
                    <asp:ListItem>NETBANKING</asp:ListItem>
                    <asp:ListItem>RUPAYCARD</asp:ListItem>
                    <asp:ListItem>CREDITCARD</asp:ListItem>
                    <asp:ListItem>OTHERDCARD</asp:ListItem>
                    <asp:ListItem>SBDEBIT</asp:ListItem>
                    <asp:ListItem>BILLDESK</asp:ListItem>
                    <asp:ListItem>UPI</asp:ListItem>
                </asp:DropDownList>
    
                <asp:TextBox ID="txtBankRef" runat="server" placeholder="Bank Ref No"></asp:TextBox>
                <asp:TextBox ID="txtRegID" runat="server" placeholder="Registration ID"></asp:TextBox>
                <asp:TextBox ID="txtDepositor" runat="server" placeholder="Depositor Name"></asp:TextBox>
                <asp:TextBox ID="txtContact" runat="server" placeholder="Contact No"></asp:TextBox>
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Email ID"></asp:TextBox>
                <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount"></asp:TextBox>
                <asp:TextBox ID="txtFee" runat="server" placeholder="Fee"></asp:TextBox>
                <asp:TextBox ID="txtDate" runat="server" TextMode="Date"></asp:TextBox>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="">--Status--</asp:ListItem>
                    <asp:ListItem>Completed Successfully</asp:ListItem>
                    <asp:ListItem>Pending</asp:ListItem>
                    <asp:ListItem>Failed</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtRemarks" runat="server" placeholder="Remarks"></asp:TextBox>
                
                <asp:Button ID="btnAdd" runat="server" Text="Add to List" CssClass="btn btn-add" OnClick="btnAdd_Click" />
            </div>

            <asp:GridView ID="gvPayments" runat="server" AutoGenerateColumns="False" CssClass="grid-view"
                OnRowDeleting="gvPayments_RowDeleting" OnRowEditing="gvPayments_RowEditing" 
                OnRowUpdating="gvPayments_RowUpdating" OnRowCancelingEdit="gvPayments_RowCancelingEdit"
                OnRowDataBound="gvPayments_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate><%# Eval("CategoryName") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="editCategory" runat="server" SelectedValue='<%# Bind("CategoryName") %>'>
                                <asp:ListItem>OFFER FEES</asp:ListItem>
                                <asp:ListItem>REGISTRATION FEE</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mode">
                        <ItemTemplate><%# Eval("PaymentMode") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="editMode" runat="server">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem>NETBANKING</asp:ListItem>
                                <asp:ListItem>RUPAYCARD</asp:ListItem>
                                <asp:ListItem>CREDITCARD</asp:ListItem>
                                <asp:ListItem>OTHERDCARD</asp:ListItem>
                                <asp:ListItem>SBDEBIT</asp:ListItem>
                                <asp:ListItem>BILLDESK</asp:ListItem>
                                <asp:ListItem>UPI</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ref No">
                        <ItemTemplate><%# Eval("BankReferenceNo") %></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="editRefNo" runat="server" Text='<%# Bind("BankReferenceNo") %>'></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate><%# Eval("Amount") %></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="editAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><%# Eval("Status") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="editStatus" runat="server" SelectedValue='<%# Bind("Status") %>'>
                                <asp:ListItem>Completed Successfully</asp:ListItem>
                                <asp:ListItem>Pending</asp:ListItem>
                                <asp:ListItem>Failed</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Reg ID">
                        <ItemTemplate><%# Eval("REGISTRATIONID") %></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="editRegID" runat="server" Text='<%# Bind("REGISTRATIONID") %>'></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-edit" />
                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-delete" 
                                OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" CssClass="btn btn-update" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-cancel" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Button ID="btnFinalSubmit" runat="server" Text="Final Save to Database" CssClass="btn btn-save" OnClick="btnFinalSubmit_Click" Visible="false" />
        </div>
    </form>
</body>
</html>