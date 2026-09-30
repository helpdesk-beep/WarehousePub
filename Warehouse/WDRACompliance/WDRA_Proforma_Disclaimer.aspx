<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/WDRACompliance/WDRA_Proforma_Disclaimer.aspx.cs" Inherits="WDRACompliance_WDRA_Proforma_Disclaimer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../assets/css/style.css" rel="stylesheet" />

    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
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
        .left, .right {
            float: left;
            width: 20%; /* The width is 20%, by default */
        }

        .main {
            float: left;
            width: 60%; /* The width is 60%, by default */
        }

        /* Use a media query to add a breakpoint at 800px: */
        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
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
        .menu {
            width: 25%;
            float: left;
        }

        .main {
            width: 75%;
            float: left;
        }

        .auto-style3 {
            position: relative;
            left: 0px;
            top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label runat="server" ID="lblMsg"></asp:Label>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>WDRA Proforma Disclaimer</legend>
            <div class="row">
                <div class="row" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblStatement" Font-Bold="true" runat="server" ForeColor="Navy">Disclaimer Statement:</asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblDisclaimer" Font-Bold="true" runat="server" ForeColor="Navy">Disclaimer:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:CheckBox ID="chkDisclaimer" runat="server" CssClass="form-control"></asp:CheckBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblAckNo" Font-Bold="true" runat="server" ForeColor="Navy">Acknowledgement Number:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtAckNo" runat="server" CssClass="form-control" Enabled="false" Visible="true"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWDRADisclaimerDate" Font-Bold="true" runat="server" ForeColor="Navy">Disclaimer Date (dd/MM/yyyy):</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtDisclaimerDate" runat="server" CssClass="form-control" Placeholder="Disclaimer Date" AutoPostBack="true"></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="txtDisclaimerDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblRemarks" Font-Bold="true" runat="server" ForeColor="Navy">Remarks:</asp:Label>
                    </div>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button ID="btnClkBack" runat="server" Text="Back" CssClass="btn-danger" OnClick="btnClkBack_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-success" Enabled="true" OnClick="btnSubmit_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn-danger" />
                </div>
            </div>
        </fieldset>

    </div>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 32 && (charCode < 46 || charCode == 47 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript" src="../../assets/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript">
        $(".dateAdd").datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            changemonth: true,
            changeyear: true
        });
    </script>
</asp:Content>
