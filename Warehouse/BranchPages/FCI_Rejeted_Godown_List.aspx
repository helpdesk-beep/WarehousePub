<%@ Page Title="FCI Rejected Godown List" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/FCI_Rejeted_Godown_List.aspx.cs" Inherits="BranchPages_FCI_Rejeted_Godown_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .grid-container {
            padding: 20px;
            background-color: #fff;
            border-radius: 8px;
        }

        .error-text {
            color: red;
            font-size: 11px;
            display: block;
        }

        .disabled-btn {
            opacity: 0.5;
            cursor: not-allowed !important;
            background-color: gray !important;
        }

        .table-style {
            width: 100%;
            border-collapse: collapse;
        }
    </style>
    <script type="text/javascript">
        function validateRow(btnId) {
            var btn = document.getElementById(btnId);
            if (!btn) return;
            var row = btn.closest('tr');
            var inputs = row.querySelectorAll('.required-input');
            var isValid = true;
            inputs.forEach(function (input) {
                if (input.value.trim() === "") isValid = false;
            });
            if (isValid) {
                btn.disabled = false;
                btn.classList.remove('disabled-btn');
            } else {
                btn.disabled = true;
                btn.classList.add('disabled-btn');
            }
        }
    </script>

    <script type="text/javascript">
        function Search_Gridview(strKey) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvRejectedGodowns.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="grid-container">
        <h3 class="BTNBLUE" style="width: 100%; padding: 10px; box-sizing: border-box;">FCI Rejected Godown List</h3>
        Search :
            <asp:TextBox ID="txtSearch" runat="server" Font-Size="20px" onkeyup="Search_Gridview(this)"></asp:TextBox><br />
        <asp:GridView ID="gvRejectedGodowns" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" 
            DataKeyNames="ID" OnRowCommand="gvRejectedGodowns_RowCommand"
            CssClass="table-style" CellPadding="4">
            <Columns>
                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GodownName" HeaderText="Godown Name" ReadOnly="true" />
                <asp:BoundField DataField="Godown_Id" HeaderText="Godown Id" ReadOnly="true" />
                <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" ReadOnly="true" />

                <asp:BoundField DataField="Rejected_QTY" HeaderText="Rejected QTY" ReadOnly="true" />
                <asp:BoundField DataField="Reason_For_Rejection" HeaderText="Reason For Rejection" ReadOnly="true" />



                <asp:TemplateField HeaderText="Upgrade Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtUpgradeQty" runat="server" Text='<%# Eval("Upgrade_Qty") %>' CssClass="required-input" onkeyup="validateRow(this.closest('tr').querySelector('.update-btn').id)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtUpgradeQty" ErrorMessage="Required" Display="Dynamic" CssClass="error-text" ValidationGroup="vgUpdate" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Lift Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtLiftQty" runat="server" Text='<%# Eval("Lift_Qty") %>' CssClass="required-input" onkeyup="validateRow(this.closest('tr').querySelector('.update-btn').id)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtLiftQty" ErrorMessage="Required" Display="Dynamic" CssClass="error-text" ValidationGroup="vgUpdate" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Pending Upgrade">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPendingUp" runat="server" Text='<%# Eval("Pending_For_Upgradation") %>' CssClass="required-input" onkeyup="validateRow(this.closest('tr').querySelector('.update-btn').id)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtPendingUp" ErrorMessage="Required" Display="Dynamic" CssClass="error-text" ValidationGroup="vgUpdate" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Pending Lift Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPendingLift" runat="server" Text='<%# Eval("Pending_For_Lift_Upgradation_Qty") %>' CssClass="required-input" onkeyup="validateRow(this.closest('tr').querySelector('.update-btn').id)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtPendingLift" ErrorMessage="Required" Display="Dynamic" CssClass="error-text" ValidationGroup="vgUpdate" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Upload Document">
                    <ItemTemplate>
                        <asp:FileUpload ID="fuDoc" runat="server" />
                        <asp:RegularExpressionValidator ID="revFile" runat="server" ControlToValidate="fuDoc"
                            ErrorMessage="Only PDF/Images allowed" ValidationExpression="^.*\.(jpg|jpeg|png|pdf|JPG|JPEG|PNG|PDF)$"
                            Display="Dynamic" CssClass="error-text" ValidationGroup="vgUpdate" />

                        <div style="margin-top: 5px;">
                            <%-- Added OnClientClick to open in new tab --%>
                            <asp:LinkButton ID="lnkView" runat="server"
                                Visible='<%# Eval("Upload_Document") != DBNull.Value %>' Text="View Doc"
                                CommandName="ViewDoc" CommandArgument='<%# Container.DataItemIndex %>'
                                OnClientClick="window.document.forms[0].target='_blank';">
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="UpdateRecord"
                            CommandArgument='<%# Container.DataItemIndex %>' ValidationGroup="vgUpdate"
                            CssClass="BTNBLUE update-btn disabled-btn" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
