<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="Holder_Register_Entry.aspx.cs" Inherits="Inspections_BO_Holder_Register_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.3/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>" type="text/javascript"></script>
    <style type="text/css">
        .modal-dialog {
            width: 1000px;
            margin: 30px auto;
        }

        .btn-info {
            color: #fff;
            background-color: #5bc0de;
            border-color: #46b8da;
        }

        .btn {
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            background-color: #D69758;
            color: #fff;
        }

        .btn-info:hover {
            color: black;
            background-color: #31b0d5;
            border-color: #269abc;
        }

        .btn.active, .btn:active {
            background-image: none;
            outline: 0;
            -webkit-box-shadow: inset 0 3px 5px rgba(0,0,0,.125);
            box-shadow: inset 0 3px 5px rgba(0,0,0,.125);
        }

        .td {
            width: 175px;
        }
    </style>
    <div runat="server" style="background-color: #FDFAF7; width: 100%;">
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
            <tr>
                <td style="text-align: right; width: 175px;">
                    <asp:Label ID="Label3" runat="server" Text="Godown No. : "></asp:Label>
                </td>
                <td style="width: 175px;" colspan="3">
                    <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="false" class="form-control" Width="60%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="Label11" runat="server" Text="Name of the Original Depositor: "></asp:Label>
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtDepositorName" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="150"></asp:TextBox>
                </td>

                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="Label12" runat="server" Text="Addresss of the Original Depositor: "></asp:Label>
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtDepositorAddress" runat="server" CssClass="form-control" Height="80px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="Label13" runat="server" Text="Depositors Ledger Folio : "></asp:Label>
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtLedgerFolio" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </td>

                <td style="text-align: right; width: 20%;">Name of Subsequent Holders who<br />
                    have sent Intimation to the Manager :
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtSubsequentName" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">Address of Subsequent Holders who<br />
                    have sent Intimation to the Manager  :
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtSubsequentAddr" runat="server" CssClass="form-control" Height="80px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                </td>

                <td style="text-align: right; width: 20%;">Date of Intimation for Registration under Rule 30 :
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtIntimationRegDate" runat="server" CssClass="form-control" MaxLength="10" Width="50%"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtIntimationRegDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIntimationRegDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">Reference to the instrument<br />
                    or document transferring possession : 
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtReference" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </td>

                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="Label15" runat="server" Text="Date of Intimation given under Rule 30 : "></asp:Label>
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtIntimationDate" runat="server" CssClass="form-control" MaxLength="10" Width="50%"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtIntimationDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtIntimationDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="Label16" runat="server" Text="Other Particular : "></asp:Label>
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtOtherPerticular" runat="server" CssClass="form-control" Height="80px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                </td>

                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="Label21" runat="server" Text="Remarks : "></asp:Label>
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Height="80px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <center>
                  <asp:Button class="btn" ID="btn_addnewoff" runat="server" Text="Submit"  OnClick="btnsaveprofile_Click"
                                    TabIndex="11" Width="150px" Height="30px" OnClientClick=" return validate()"></asp:Button>
                                &nbsp;
                                 <asp:Button class="btn-info" ID="btn_clear" runat="server" Text="Clear"
                                     TabIndex="11" Width="150px" Height="30px" OnClick="btn_clear_Click"></asp:Button>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div style="overflow: scroll;">
                        <asp:GridView runat="server" ID="GrdOfficerPreviousInsp" OnRowCommand="GrdOfficerPreviousInsp_RowCommand"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowDataBound="GrdOfficerPreviousInsp_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="1%" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGodown_No" runat="server" Text='<%# Eval("Godown_No") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name of the Original Depositor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Depositor_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Addresss of the Original Depositor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositer_Address" runat="server" Text='<%# Eval("Depositer_Address") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Depositors Ledger Folio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositors_Ledger_Folio" runat="server" Text='<%# Eval("Depositors_Ledger_Folio") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name of Subsequent Holders who have sent Intimation to the Manager">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName_of_Subsequent_Holder" runat="server" Text='<%# Eval("Name_of_Subsequent_Holder") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address of Subsequent Holders who have sent Intimation to the Manager">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress_of_Subsequent_Holder" runat="server" Text='<%# Eval("Address_of_Subsequent_Holder") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of Intimation for Registration under Rule 30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate_of_Intimation_Registration" runat="server" Text='<%# Eval("Date_of_Intimation_Registration") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reference to the instrument or document transferring possession">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReference_Instrument_Document_Transferring" runat="server" Text='<%# Eval("Reference_Instrument_Document_Transferring") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of Intimation given under Rule 30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate_of_Intimation" runat="server" Text='<%# Eval("Date_of_Intimation") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Particular">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOther_Particular" runat="server" Text='<%# Eval("Other_Particular") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit Details" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnfilloverallinsp" Text="Edit" runat="server" CommandName="Overallinsp" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>

                            <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                                Height="20px" Font-Size="10pt" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
        <div>
        </div>
        <asp:HiddenField ID="hdnHID" runat="server" Value="0" />

    </div>
    <script language="javascript" type="text/javascript">  
        function validate() {
            if (document.getElementById("<%=ddl_gdwn.ClientID%>").value == "0") {
                alert("Select Godown Name");
                document.getElementById("<%=ddl_gdwn.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtDepositorName.ClientID %>").value == "") {
                alert("Name of the Original Depositor can not be blank");
                document.getElementById("<%=txtDepositorName.ClientID %>").focus();
                return false;
            }

            if (document.getElementById("<%=txtDepositorAddress.ClientID %>").value == "") {
                alert("Addresss of the Original Depositor can not be blank");
                document.getElementById("<%=txtDepositorAddress.ClientID %>").focus();
                return false;
            }

            if (document.getElementById("<%=txtLedgerFolio.ClientID%>").value == "") {
                alert("Depositors Ledger Folio can not be blank");
                document.getElementById("<%=txtLedgerFolio.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtSubsequentName.ClientID%>").value == "") {
                alert("Name of Subsequent Holders who have sent Intimation to the Manager can not be blank");
                document.getElementById("<%=txtSubsequentName.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtSubsequentAddr.ClientID%>").value == "") {
                alert("Address of Subsequent Holders who have sent Intimation to the Manager can not be blank");
                document.getElementById("<%=txtSubsequentAddr.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtIntimationRegDate.ClientID%>").value == "") {
                alert("Date of Intimation for Registration under Rule 30 can not be blank");
                document.getElementById("<%=txtIntimationRegDate.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtReference.ClientID%>").value == "") {
                alert("Reference to the instrument or document transferring possession can not be blank");
                document.getElementById("<%=txtReference.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtIntimationDate.ClientID%>").value == "") {
                alert("Date of Intimation given under Rule 30 can not be blank");
                document.getElementById("<%=txtIntimationDate.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtOtherPerticular.ClientID%>").value == "") {
                alert("Other Particular can not be blank");
                document.getElementById("<%=txtOtherPerticular.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtRemarks.ClientID%>").value == "") {
                alert("Remarks can not be blank");
                document.getElementById("<%=txtRemarks.ClientID%>").focus();
                return false;
            }
            return true;
        }
        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode;
            if ((charCode >= 48 && charCode <= 57)) {
                return true;
            }
            if (charCode == 46) { return true; }
            if (charCode == 8) { return true; }
            if (charCode == 9) { return true; }
            else { return false; }
        }
    </script>

    <%--<script>
        $(document).ready(function () {
            $("[id$=txtReceiptDate]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
            $("[id$=txtBillDate]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>--%>
</asp:Content>




