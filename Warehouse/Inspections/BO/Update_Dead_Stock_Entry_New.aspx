<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="Update_Dead_Stock_Entry_New.aspx.cs" Inherits="Inspections_BO_Update_Dead_Stock_Entry_New" %>

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
    </style>
    <div runat="server" style="background-color: #FDFAF7; width: 100%;">
        <div>
           <h3> <span style="color:red;">यदि दी गई लिस्ट में आपको आर्टिकल नहीं दिखाई दे रहे हैं तो आप अन्य का ऑप्शन चयन करके उसकी एंट्री कर सकते हैं (सिर्फ आर्टिकल का नाम ही लिखना हैं )</span></h3>
        </div>
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
            <tr>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label33" runat="server" Text="Description of the Articles  : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlArticles" runat="server" AutoPostBack="true"
                        CssClass="form-control" OnSelectedIndexChanged="ddlArticles_SelectedIndexChanged">
                    </asp:DropDownList>
                    <%--<asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" MaxLength="300"></asp:TextBox>--%>
                </td>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label59" runat="server" Text="Quantity of the Articles  : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" MaxLength="4" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td style="font-weight: bold; text-align: right;">Name of the Party from whom
                    <br />
                    articles purchased/ Received : 
                </td>
                <td>
                    <asp:TextBox ID="txtPurchaser" runat="server" CssClass="form-control" Height="80px" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tbody id="otherartical" runat="server" visible="false">
                 <tr>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label1" runat="server" Text="आर्टिकल का नाम : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtarticalname" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            </tbody>
            <tr>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label11" runat="server" Text="Date of Receipt  : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReceiptDate" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtReceiptDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReceiptDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                </td>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label12" runat="server" Text="Bill Number  : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                </td>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label13" runat="server" Text="Bill Date  : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBillDate" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtBillDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtBillDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label14" runat="server" Text="Cost of the Articles : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCost" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label15" runat="server" Text="Accidental Charge : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccidental" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label16" runat="server" Text="Total : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label17" runat="server" Text="Rate of Dep. : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label18" runat="server" Text="Depreciation : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDep" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label19" runat="server" Text="Written down Value : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtWritten" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="font-weight: bold; text-align: right;">
                    <asp:Label ID="Label20" runat="server" Text="Remarks : "></asp:Label>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Height="80px" TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <center>
                  <asp:Button class="btn" ID="btn_addnewoff" runat="server" Text="Submit"  OnClick="btnsaveprofile_Click"
                                    TabIndex="11" Width="150px" Height="30px" ValidationGroup="A" OnClientClick=" return validate()"></asp:Button>
                                &nbsp;
                                 <asp:Button class="btn-info" ID="btn_clear" runat="server" Text="Clear"
                                     TabIndex="11" Width="150px" Height="30px" OnClick="btn_clear_Click"></asp:Button>
                    </center>
                </td>
            </tr>
            
        </table>
        <div>
        </div>

    </div>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
    <script language="javascript" type="text/javascript">  
        function validate() {
            if (document.getElementById("<%=ddlArticles.ClientID%>").value == "") {
                alert("Select Description of the Articles");
                document.getElementById("<%=ddlArticles.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtQty.ClientID %>").value == "") {
                alert("Quantity of the Articles can not be blank");
                document.getElementById("<%=txtQty.ClientID %>").focus();
                return false;
            }

            if (document.getElementById("<%=txtPurchaser.ClientID %>").value == "") {
                alert("Name of the Party from whom articles purchased/ Received can not be blank");
                document.getElementById("<%=txtPurchaser.ClientID %>").focus();
                return false;
            }

            if (document.getElementById("<%=txtReceiptDate.ClientID%>").value == "") {
                alert("Date of Receipt is not valid");
                document.getElementById("<%=txtReceiptDate.ClientID%>").focus();
                return false;
            }

           <%-- if (document.getElementById("<%=txtBillNo.ClientID%>").value == "") {
                alert("Bill Number can not be blank");
                document.getElementById("<%=txtBillNo.ClientID%>").focus();
                return false;
            }--%>

            <%--if (document.getElementById("<%=txtBillDate.ClientID%>").value == "") {
                alert("Bill Date can not be blank");
                document.getElementById("<%=txtBillDate.ClientID%>").focus();
                return false;
            }--%>

            if (document.getElementById("<%=txtCost.ClientID%>").value == "") {
                alert("Cost of the Articles can not be blank");
                document.getElementById("<%=txtCost.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtAccidental.ClientID%>").value == "") {
                alert("Accidental Charge can not be blank");
                document.getElementById("<%=txtAccidental.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtTotal.ClientID%>").value == "") {
                alert("Total can not be blank");
                document.getElementById("<%=txtAccidental.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtRate.ClientID%>").value == "") {
                alert("Rate of Dep. can not be blank");
                document.getElementById("<%=txtRate.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtDep.ClientID%>").value == "") {
                alert("Depreciation can not be blank");
                document.getElementById("<%=txtDep.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtWritten.ClientID%>").value == "") {
                alert("Written down Value can not be blank");
                document.getElementById("<%=txtWritten.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtRemark.ClientID%>").value == "") {
                alert("Remarks can not be blank");
                document.getElementById("<%=txtRemark.ClientID%>").focus();
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
    <script>
        $(document).ready(function () {
            $("[id$=txtReceiptDate]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>

</asp:Content>



