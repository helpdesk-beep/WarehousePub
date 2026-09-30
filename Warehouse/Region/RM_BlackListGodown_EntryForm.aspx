<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Region/RM_BlackListGodown_EntryForm.aspx.cs" Inherits="RM_BlackListGodown_EntryForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Inspections/Assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Administration/assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/datatable/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="assets/datatable/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/custome.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
    <script type="text/javascript">
        function PrintDiv_det() {
            var divContents = document.getElementById("PrintDiv_Det").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
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
    </style>
    <style>
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
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
            /*transform: translate3d(0px, 0px, 0px) !important;*/
            padding: 2px !important;
            max-height: 10em !important;
            overflow-y: auto !important;
            /*#ec8712*/
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }

        .form-controlSearchBox {
            display: block;
            width: 100%;
            height: calc(2.25rem + 2px);
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            /*border-radius: 0.25rem;*/
            box-shadow: inset 0 0 0 transparent;
            /*transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;*/
        }

        .multiselect-native-select button {
            border-radius: 0.5em;
            border-color: #767b83 !important;
        }

        .form-check {
            margin-top: -3px;
            margin-bottom: -4px;
            padding-left: 0;
        }

            .form-check .form-check-label {
                margin-left: 0.5rem;
            }


        .row {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            margin-right: 0px;
            margin-left: -15px;
        }
    </style>
    <style type="text/css">
        ul.svertical {
            width: 220px; /* width of menu */
            overflow: auto;
            background: #f4f4f4; /* background of menu */
            margin: 0;
            padding: 0;
            padding-top: 7px; /* top padding */
            list-style-type: none;
        }

            ul.svertical li {
                text-align: right; /* right align menu links */
            }

                ul.svertical li a {
                    position: relative;
                    display: inline-block;
                    text-indent: 5px;
                    overflow: hidden;
                    background: rgb(1, 138, 180); /* initial background color of links */
                    font: bold 16px Germand;
                    text-decoration: none;
                    padding: 5px;
                    margin-bottom: 5px; /* spacing between links */
                    color: White;
                    -moz-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8); /* inner right shadow added to each link */
                    -webkit-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    -moz-transition: all 0.2s ease-in-out; /* CSS3 transition of hover properties */
                    -webkit-transition: all 0.2s ease-in-out;
                    -o-transition: all 0.2s ease-in-out;
                    -ms-transition: all 0.2s ease-in-out;
                    transition: all 0.2s ease-in-out;
                }

                    ul.svertical li a:hover {
                        padding-right: 30px; /* add right padding to expand link horizontally to the left */
                        color: Black;
                        background: rgb(153,249,75);
                        -moz-box-shadow: inset -3px 0 2px rgba(114,114,114, 0.8); /* contract inner right shadow */
                        -webkit-box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                        box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                    }

                    ul.svertical li a:before { /* CSS generated content: slanted right edge */
                        content: "";
                        position: absolute;
                        left: 0;
                        top: 0;
                        border-style: solid;
                        border-width: 70px 0 0 20px; /* Play around with 1st and 4th value to change slant degree */
                        border-color: transparent transparent transparent #f4f4f4; /* change black to match the background color of the menu UL */
                    }

        /*print*/


        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }

        .page {
            width: 21cm;
            min-height: 29.7cm;
            padding: 2cm;
            margin: 1cm auto;
            border: 1px #D3D3D3 solid;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

        .subpage {
            padding: 1cm;
            border: 5px red solid;
            height: 237mm;
            outline: 2cm #FFEAEA solid;
        }

        @page {
            size: A4;
            margin: 0;
            font-size: smaller;
        }

        @media print {
            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
                font-size: smaller;
            }
        }

        @media print {
            html, body {
                width: 210mm;
                height: 297mm;
                font-size: smaller;
            }
            /* ... the rest of the rules ... */
        }
        /*td{font-size:smaller;}*/
        page[size="A4"] {
            background: white;
            width: 21cm;
            height: 29.7cm;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
        }

        @media print {
            body, page[size="A4"] {
                margin: 0;
                box-shadow: 0;
                font-size: smaller;
            }
        }

        .style7 {
            height: 15px;
        }

        .style8 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset>
        <legend style="text-align: center">BlackList Godown Entry Form</legend>
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    <span class="fa-pull-right">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Select Inspection Quarter" ID="RequiredFieldValidator6" Display="Dynamic" InitialValue="0" ControlToValidate="ddlDistrict" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Select Select !'></i>" ForeColor="Red"></asp:RequiredFieldValidator>
                    </span>
                    <asp:DropDownList ID="ddlDistrict" CssClass="form-control" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblbranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    <asp:DropDownList CssClass="form-control" ID="ddlBranch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblGodown_Name" runat="server" Text="Godown Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    <asp:DropDownList CssClass="form-control" ID="ddlGodownName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGodownName_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblRegistration_ID" runat="server" Text="Godown Registration ID" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    <asp:TextBox runat="server" ID="lblGodownRegId" ReadOnly="true" CssClass="form-control" ForeColor="Red"
                        Font-Bold="true"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 10px">
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblregistrationId" runat="server" Text="Enter Godown Registration ID" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    <asp:TextBox runat="server" ID="txtGodownRegId" AutoComplete="off" MaxLength="15" Placeholder="Enter Godown Registration ID" CssClass="form-control" ForeColor="Red"
                        Font-Bold="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGodownRegId"
                        Display="Dynamic" ErrorMessage="Godown Registration ID is required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblBlackListDate" runat="server" Text="BlackList करने की दिनांक" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    <asp:TextBox ID="txtBlackListedDate" runat="server" placeholder="dd/mm/yyyy"
                        CssClass="form-control" autocomplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBlackListedDate"
                        Display="Dynamic" ErrorMessage="Enter Currect Date" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        CssClass="fa-pull-right mt-1" ErrorMessage="Enter Currect Date" ControlToValidate="txtBlackListedDate" ValidationGroup="a" Display="Dynamic" ForeColor="Red"
                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblBlackListOrderNo" runat="server" Text="BlackList करने का आदेश क्र." Font-Size="10pt" Font-Bold="true"></asp:Label>
                    <asp:TextBox runat="server" ID="txtBlackListOrderNo" AutoComplete="off" MaxLength="15" Placeholder="Enter BlackList Order Number" CssClass="form-control "
                        ForeColor="Red"
                        Font-Bold="true"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblBlackListTillDate" runat="server" Text="BlackList की अवधी(कब तक के लिए BlackList किया गया है.):" Font-Size="10pt" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtBlackListTillDate" runat="server" placeholder="dd/mm/yyyy"
                    CssClass="form-control dateAdd" autocomplete="off" data-provide="datepicker" data-date-format="dd/mm/yyyy"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBlackListTillDate"
                    Display="Dynamic" ErrorMessage="Enter Currect Date" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                    CssClass="fa-pull-right mt-1" ErrorMessage="Enter Currect Date" ControlToValidate="txtBlackListTillDate" ValidationGroup="a" Display="Dynamic" ForeColor="Red"
                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="row" style="margin-top: 10px">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblRemark" runat="server" Font-Size="Medium" Font-Bold="True"
                        Text="BlackList किये जाने का कारण"></asp:Label>
                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4" style="margin-top: 35px">
                <asp:Button class="button button2" ID="btnSubmit" CssClass="btn btn-success btn-primary"
                    runat="server" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
            </div>
        </div>
    </fieldset>
</asp:Content>

