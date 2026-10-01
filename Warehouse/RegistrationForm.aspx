<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrationForm.aspx.cs" Inherits="QCRegistration_RegistrationForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="assets/datatable/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="assets/datatable/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="assets/datatable/css/jquery.dataTables.min.css" rel="stylesheet" />
    <!-- Bootstrap -->
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link href="../Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../Inspections/Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <%--<script type="text/javascript" src="../Assets/js/bootstrap-datepicker.js"></script>--%>
    <script type="text/javascript" src="../Inspections/Assets/js/bootstrap-datepicker.js"></script>
    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }

        .auto-style1 {
            height: 10px;
            width: 558px;
        }

        .auto-style2 {
            width: 558px;
        }
    </style>
    <style type="text/css">
        .left, .right {
            float: left;
            width: 20%; /* The width is 20%, by default */
        }

        .main {
            float: left;
            width: 60%; /* The width is 60%, by default */
        }

        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
        }
    </style>
    <style type="text/css">
        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
        }

        .content-wrapper {
            padding: 1.75rem 1.25rem;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #030203;
        }

        .form-control {
            border: 1px solid #767B83;
            border-radius: 8px;
        }

        .table th {
            text-align: center;
        }

        .form-inline {
            display: block !important;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #647e68 !important;
            color: black !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            color: black !important;
        }

        element.style {
            font-size: medium !important;
        }
    </style>
    <script type="text/javascript">
        $(".dateAdd").datepicker({
            format: 'yyyy/mm/dd',
            autoclose: true,
            changemonth: true,
            changeyear: true
        });
    </script>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 32 && (charCode < 46 || charCode == 47 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function previewUserImage() {
            debugger
            let preview = document.querySelector('#<%=imgUpload.ClientID %>');
            let file = document.querySelector('#<%=Nurseryphoto.ClientID %>').files[0];
            let reader = new FileReader();

            reader.onloadend = function () {

                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
        function previewUserImage1() {
            debugger
            let preview = document.querySelector('#<%=Image1.ClientID %>');
            let file = document.querySelector('#<%=FileUpload1.ClientID %>').files[0];
            let reader = new FileReader();

            reader.onloadend = function () {

                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="content-wrapper">
            <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
                <legend>स्कंध की गुणवत्ता जांच के लिए मैनपावर हेतु आवेदन पत्र</legend>
                <div class="row">
                    <div class="col-md-4">
                        <label>आवेदक/आवेदिक का पूरा नाम<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txtname" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="आवेदक/आवेदिक का पूरा नाम दर्ज करें" ErrorMessage="आवेदक/आवेदिक का पूरा नाम दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtname" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>आवेदक/आवेदिक के पिता/पति का पूरा नाम<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtFather" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="आवेदक/आवेदिक के पिता/पति का पूरा नाम" ErrorMessage="आवेदक/आवेदिक के पिता/पति का पूरा नाम" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtFather" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>आवेदक/आवेदिक के माता का नाम<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtMother" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="आवेदक/आवेदिक के माता का नाम" ErrorMessage="आवेदक/आवेदिक के माता का नाम" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtMother" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px">
                    <div class="col-md-4">
                        <label>जन्म तिथि(10+2 के प्रमाण पत्र के आधार पर)<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtDate" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="जन्म तिथि(10+2 के प्रमाण पत्र के आधार पर)" ErrorMessage="जन्म तिथि(10+2 के प्रमाण पत्र के आधार पर)" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDate" runat="server" placeholder="yyyy/mm/dd"
                            CssClass="form-control dateAdd" autocomplete="off" data-provide="datepicker"
                            onpaste="return false ;" onkeypress="return false;" data-date-format="yyyy/mm/dd"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>लिंग<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                            ErrorMessage="लिंग चुने" ToolTip="लिंग चुने" Text="<i class='fa fa-exclamation-circle' title='लिंग चुने !'></i>"
                            ControlToValidate="ddlgender" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                        </asp:RequiredFieldValidator>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlgender" runat="server">
                            <asp:ListItem Value="0">चुने</asp:ListItem>
                            <asp:ListItem Value="1">महिला</asp:ListItem>
                            <asp:ListItem Value="2">पुरुष</asp:ListItem>
                            <asp:ListItem Value="3">ट्रांसजेंडर</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        <label>जाति<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                            ErrorMessage="जाति चुने" ToolTip="जाति चुने" Text="<i class='fa fa-exclamation-circle' title='जाति चुने !'></i>"
                            ControlToValidate="ddlcast" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                        </asp:RequiredFieldValidator>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlcast" runat="server">
                            <asp:ListItem Value="0">चुने</asp:ListItem>
                            <asp:ListItem Value="1">सामान्य</asp:ListItem>
                            <asp:ListItem Value="2">अनुसूचित जाति (एससी)</asp:ListItem>
                            <asp:ListItem Value="3">अनुसूचित जनजाति (एसटी)</asp:ListItem>
                            <asp:ListItem Value="4">अन्य पिछड़ा वर्ग (ओबीसी)</asp:ListItem>
                            <asp:ListItem Value="5">आर्थिक रूप से कमज़ोर वर्ग (EWS)</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>राष्ट्रीयता<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                            ErrorMessage="राष्ट्रीयता चुने" ToolTip="राष्ट्रीयता चुने" Text="<i class='fa fa-exclamation-circle' title='राष्ट्रीयता चुने !'></i>"
                            ControlToValidate="ddlNationality" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                        </asp:RequiredFieldValidator>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlNationality" runat="server">
                            <asp:ListItem Value="0">चुने</asp:ListItem>
                            <asp:ListItem Value="1">भारतीय</asp:ListItem>
                            <asp:ListItem Value="2">अन्य</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>कार्या हेतु ज़िला चुने<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                            ErrorMessage="ज़िला चुने" ToolTip="ज़िला चुने" Text="<i class='fa fa-exclamation-circle' title='ज़िला चुने !'></i>"
                            ControlToValidate="ddldistrict" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                        </asp:RequiredFieldValidator>
                        <asp:DropDownList CssClass="form-control select2" ID="ddldistrict" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px">
                    <div class="col-md-6">
                        <label>वर्तमान डाक का पता<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator8" Display="Dynamic" ControlToValidate="txtpresentadd" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="वर्तमान डाक का पता दर्ज करें" ErrorMessage="वर्तमान डाक का पता दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtpresentadd" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label>स्थायी पता<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator9" Display="Dynamic" ControlToValidate="txtparmanentAdd" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="स्थायी पता दर्ज करें" ErrorMessage="स्थायी पता दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtparmanentAdd" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px">
                    <div class="col-md-3">
                        <label>आवेदक/आवेदिक का मोबाइल न.<i style="color: red;">*</i></label>
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" Display="Dynamic" ControlToValidate="txtmobile" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="आवेदक/आवेदिक का मोबाइल न. दर्ज करें" ErrorMessage="आवेदक/आवेदिक का मोबाइल न. दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ControlToValidate="txtmobile" Display="Dynamic" ForeColor="Red" ValidationGroup="a" ErrorMessage="<i class='fa fa-exclamation-circle' title='मोबाइल न. सही दर्ज करें !'></i>"
                                ValidationExpression="^[6-9]\d{9}$"></asp:RegularExpressionValidator>
                        </span>
                        <asp:TextBox ID="txtmobile" runat="server" onkeypress="return isNumber()" autocomplete="off" MaxLength="10" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>ईमेल आईडी<i style="color: red;">*</i></label>
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" Display="Dynamic" ControlToValidate="txtemail" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="ईमेल आईडी दर्ज करें" ErrorMessage="ईमेल आईडी दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revemail" runat="server"
                                ControlToValidate="txtemail" ValidationGroup="a" ForeColor="Red" ErrorMessage="<i class='fa fa-exclamation-circle' title='ईमेल आईडी सही दर्ज करें !'></i>"
                                ValidationExpression="^[a-z0-9]+(?!.*(?:\+{2,}|\-{2,}|\.{2,}))(?:[\.+\-]{0,1}[a-z0-9])*@gmail\.com$" Display="Dynamic"></asp:RegularExpressionValidator>
                        </span>
                        <asp:TextBox ID="txtemail" runat="server" placeholder="Enter Email-Id" autocomplete="off" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>वैवाहिक स्थिति<i style="color: red;">*</i></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="a"
                            ErrorMessage="वैवाहिक स्थिति चुने" ToolTip="वैवाहिक स्थिति चुने" Text="<i class='fa fa-exclamation-circle' title='वैवाहिक स्थिति चुने !'></i>"
                            ControlToValidate="ddlmarried" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                        </asp:RequiredFieldValidator>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlmarried" runat="server">
                            <asp:ListItem Value="0">चुने</asp:ListItem>
                            <asp:ListItem Value="1">विवाहित</asp:ListItem>
                            <asp:ListItem Value="2">अविवाहित</asp:ListItem>
                            <asp:ListItem Value="3">तलाकशुदा</asp:ListItem>
                            <asp:ListItem Value="4">अन्य</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px">
                    <fieldset>
                        <legend>वांछित शैक्षणिक योग्यता </legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>शैक्षणिक योग्यता<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator ToolTip="शैक्षणिक योग्यता दर्ज करे" ErrorMessage="शैक्षणिक योग्यता दर्ज करे" ControlToValidate="txt10th" ID="RequiredFieldValidator13" CssClass="fa-pull-right" runat="server" ForeColor="Red" ValidationGroup="a" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' Title='शैक्षणिक योग्यता दर्ज करे'></i>"> </asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txt10th" Text="10Th" ReadOnly="true" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>बोर्ड/विश्वविद्यालय<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator14" Display="Dynamic" ControlToValidate="txtbu" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="बोर्ड/विश्वविद्यालय दर्ज करें" ErrorMessage="बोर्ड/विश्वविद्यालय दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtbu" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>पूर्णांक<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator18" Display="Dynamic" ControlToValidate="txtpurnank" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="पूर्णांक दर्ज करें" ErrorMessage="पूर्णांक दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtpurnank" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>प्राप्तांक<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator15" Display="Dynamic" ControlToValidate="txtPraptank" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="प्राप्तांक दर्ज करें" ErrorMessage="प्राप्तांक दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtPraptank" CssClass="form-control" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtPraptank_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>प्रतिशत<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator17" Display="Dynamic" ControlToValidate="txtpercentage" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="प्रतिशत दर्ज करें" ErrorMessage="प्रतिशत दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtpercentage" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>शैक्षणिक योग्यता<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator ToolTip="शैक्षणिक योग्यता दर्ज करे" ErrorMessage="शैक्षणिक योग्यता दर्ज करे" ControlToValidate="txtbse" ID="RequiredFieldValidator16" CssClass="fa-pull-right" runat="server" ForeColor="Red" ValidationGroup="a" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' Title='शैक्षणिक योग्यता दर्ज करे'></i>"> </asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtbse" Text="BSC(Agriculture)" ReadOnly="true" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>बोर्ड/विश्वविद्यालय<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator20" Display="Dynamic" ControlToValidate="txtbseBoard" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="बोर्ड/विश्वविद्यालय दर्ज करें" ErrorMessage="बोर्ड/विश्वविद्यालय दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtbseBoard" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>पूर्णांक<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator21" Display="Dynamic" ControlToValidate="txtbseTotal" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="पूर्णांक दर्ज करें" ErrorMessage="पूर्णांक दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtbseTotal" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>प्राप्तांक<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator22" Display="Dynamic" ControlToValidate="txtbsepra" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="प्राप्तांक दर्ज करें" ErrorMessage="प्राप्तांक दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtbsepra" CssClass="form-control" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtbsepra_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>प्रतिशत<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator23" Display="Dynamic" ControlToValidate="txtbsePer" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="प्रतिशत दर्ज करें" ErrorMessage="प्रतिशत दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtbsePer" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>शैक्षणिक योग्यता<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator ToolTip="शैक्षणिक योग्यता दर्ज करे" ErrorMessage="शैक्षणिक योग्यता दर्ज करे" ControlToValidate="txtComputer" ID="RequiredFieldValidator24" CssClass="fa-pull-right" runat="server" ForeColor="Red" ValidationGroup="a" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' Title='शैक्षणिक योग्यता दर्ज करे'></i>"> </asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtComputer" ReadOnly="true" Text="Computer Deploma" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>बोर्ड/विश्वविद्यालय<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator25" Display="Dynamic" ControlToValidate="txtcseboard" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="बोर्ड/विश्वविद्यालय दर्ज करें" ErrorMessage="बोर्ड/विश्वविद्यालय दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtcseboard" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>पूर्णांक<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator26" Display="Dynamic" ControlToValidate="txtcsetotal" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="पूर्णांक दर्ज करें" ErrorMessage="पूर्णांक दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtcsetotal" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>प्राप्तांक<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator27" Display="Dynamic" ControlToValidate="txtcserece" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="प्राप्तांक दर्ज करें" ErrorMessage="प्राप्तांक दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtcserece" CssClass="form-control" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtPraptank_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>प्रतिशत<i style="color: red">*</i></label>
                                    <asp:RequiredFieldValidator runat="server" CssClass="fa fa-pull-right" ID="RequiredFieldValidator28" Display="Dynamic" ControlToValidate="txtcseperce" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="प्रतिशत दर्ज करें" ErrorMessage="प्रतिशत दर्ज करें" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" ID="txtcseperce" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="Row" style="margin-top: 10px">
                    <label>
                        कृपया कैंडिडेट यहाँ फोटो और हस्ताक्षर अपलोड करें :(केवल: .JPG,.PNG एवं .JPEG फोर्मेट मैं ही फोटो अपलोड करें)(500kb से कम साइज़ मैं ही अपलोड करें )
                        <br />
                        Candidates kindly upload your photo And Signature here: (Only use: .JPG,.PNG &.JPEG format)(Image size should not be more than 500kb)<i style="color: red">*</i></label>
                </div>
                <div class="row form-group" style="margin-top: 10px">
                    <div class="col-md-3" style="margin-top: 10px">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" ID="rfvNurseryimage" Display="Dynamic" ControlToValidate="Nurseryphoto" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Upload JPG Photo" ErrorMessage=" Upload JPG Photo" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:HyperLink runat="server" ID="docs" Visible="false" Text="View Image" Style="color: red; font-weight: 600" Target="_blank" CssClass="label label-orange"></asp:HyperLink>
                        </span>
                        <asp:Image ID="imgUpload" ClientIDMode="Static" runat="server" BorderStyle="Solid" Width="125" Height="149" src="../warehouse/images/Dummy.jpg" />
                        <asp:FileUpload ID="Nurseryphoto" onchange="previewUserImage()" runat="server" CssClass="form-control mt-2" Style="margin-top: 10px" />
                        <label runat="server" id="lblUserPhotoErrorMsg"></label>
                        <asp:HiddenField runat="server" ID="hfdNurseryImage" />
                    </div>
                    <div class="col-md-3" style="margin-top: 110px">
                        <span class="fa-pull-right">
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator19" Display="Dynamic" ControlToValidate="Nurseryphoto" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ToolTip="Upload Signature JPG Photo" ErrorMessage=" Upload Signature JPG Photo" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:HyperLink runat="server" ID="HyperLink1" Visible="false" Text="View Image" Style="color: red; font-weight: 600" Target="_blank" CssClass="label label-orange"></asp:HyperLink>
                        </span>
                        <asp:Image ID="Image1" ClientIDMode="Static" runat="server" Width="150" Height="50" src="../warehouse/images/Signature.jpg" />
                        <asp:FileUpload ID="FileUpload1" onchange="previewUserImage1()" runat="server" CssClass="form-control mt-2" Style="margin-top: 10px" />
                        <label runat="server" id="Label1"></label>
                        <asp:HiddenField runat="server" ID="HiddenField1" />
                    </div>
                </div>
                <div class="row">
                    <asp:CheckBox ID="Ckeckbox" runat="server" CssClass="chkitems" OnCheckedChanged="Checked_CheckedChanged" AutoPostBack="true" />
                    <asp:Label runat="server" ID="lblcheck">मैं घोषणा करता हूँ/करती हूँ कि उपरोक्त समस्त जानकारी जो मेरे द्वारा दी गई है वह मेरे विवेक एवं भान से पूर्णतः सत्य है। जानकारी असत्य पाये जाने पर मेरा आवेदन निरस्त माना जावेगा, जिसका उत्तरदायित्व स्वयं मेरा होगा।<i style="color: red">*</i></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-5"></div>
                    <div class="col-md-2" style="margin-top: 25px">
                        <asp:Button runat="server" ID="btnsave" Visible="false" CssClass="btn btn-info btn-block" AutoPostBack="true" OnClick="btnsave_Click" ValidationGroup="a" Text="Submit" />
                    </div>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
