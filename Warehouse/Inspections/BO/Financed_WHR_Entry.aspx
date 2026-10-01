<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="Financed_WHR_Entry.aspx.cs" Inherits="Inspections_BO_Financed_WHR_Entry" %>

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
    <script type="text/javascript">

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
    <div runat="server" style="background-color: #FDFAF7; width: 100%;">
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
            <tr>
                <td style="text-align: right; width: 175px;">
                    <asp:Label ID="Label3" runat="server" Text="गोदाम का नाम : "></asp:Label>
                </td>
                <td style="width: 175px;" colspan="5">
                    <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="false" class="form-control">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 176px;">
                    <asp:Label ID="Label13" runat="server" Text=" वेयर हाउस रशीद क्रमांक: "></asp:Label>
                </td>
                <td style="width: 176px;">
                    <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 175px;">
                    <asp:Label ID="Label11" runat="server" Text="दिनाँक (DD/MM/YYYY): "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                </td>
                <td style="text-align: right; width: 175px;">
                    <asp:Label ID="Label12" runat="server" Text="स्कंध : "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:DropDownList ID="ddlCommodity" runat="server" AutoPostBack="false" class="form-control">
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="text-align: right; width: 176px;">
                    <asp:Label ID="Label1" runat="server" Text=" बोरे : "></asp:Label>
                </td>
                <td style="width: 176px;">
                    <asp:TextBox ID="txtBags" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 175px;">
                    <asp:Label ID="Label2" runat="server" Text="बजन की मात्रा : "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:TextBox ID="txtWeight" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="10" onkeypress="return NumberOnly(event);"></asp:TextBox>

                </td>
                <td style="text-align: right; width: 175px;">
                    <asp:Label ID="Label5" runat="server" Text="लयन पत्र की तिथि (DD/MM/YYYY) : "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:TextBox ID="txtLetterDate" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtLetterDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtLetterDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                </td>

            </tr>
            <tr>
                <td style="text-align: right; width: 175px;">
                    <asp:Label ID="Label4" runat="server" Text="बैंक/ निजी व्यक्ति का नाम जिसके पास रहन रखी गई हो : "></asp:Label>
                </td>
                <td style="width: 175px;" colspan="5">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
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
                                <asp:TemplateField HeaderText="गोदाम नम्बर">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGodown_No" runat="server" Text='<%# Eval("Godown_No") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="गोदाम का नाम">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="वेयर हाउस रशीद क्रमांक">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWH_Receipt_No" runat="server" Text='<%# Eval("WH_Receipt_No") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="दिनाँक">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="स्कंध">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCommodity_Name" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="बोरे">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBags" runat="server" Text='<%# Eval("Bags") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="बजन की मात्रा">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="बैंक/ निजी व्यक्ति का नाम जिसके पास रहन रखी गई हो">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBank_Self_Name" runat="server" Text='<%# Eval("Bank_Self_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="लयन पत्र की तिथि">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLetter_Date" runat="server" Text='<%# Eval("Letter_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit Details" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnfilloverallinsp" Text="Edit" runat="server" CommandName="Overallinsp" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remove">
                                    <ItemTemplate>
                                        <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="DeleteRecord" CssClass="btneditstyle" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

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
                alert("गोदाम का नाम चुनें");
                document.getElementById("<%=ddl_gdwn.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtReceiptNo.ClientID %>").value == "") {
                alert("वेयर हाउस रशीद क्रमांक रिक्त नहीं हो सकता है");
                document.getElementById("<%=txtReceiptNo.ClientID %>").focus();
                return false;
            }

            if (document.getElementById("<%=txtDate.ClientID %>").value == "") {
                alert("दिनाँक रिक्त नहीं हो सकता है");
                document.getElementById("<%=txtDate.ClientID %>").focus();
                return false;
            }

            if (document.getElementById("<%=ddlCommodity.ClientID%>").value == "") {
                alert("स्कंध चुनें");
                document.getElementById("<%=ddlCommodity.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtBags.ClientID%>").value == "") {
                alert("बोरे रिक्त नहीं हो सकता है");
                document.getElementById("<%=txtBags.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtWeight.ClientID%>").value == "") {
                alert("बजन की मात्रा रिक्त नहीं हो सकता है");
                document.getElementById("<%=txtWeight.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtLetterDate.ClientID%>").value == "") {
                alert("लयन पत्र की तिथि रिक्त नहीं हो सकता है");
                document.getElementById("<%=txtLetterDate.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtName.ClientID%>").value == "") {
                alert("बैंक/ निजी व्यक्ति का नाम जिसके पास रहन रखी गई हो रिक्त नहीं हो सकता है");
                document.getElementById("<%=txtName.ClientID%>").focus();
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






