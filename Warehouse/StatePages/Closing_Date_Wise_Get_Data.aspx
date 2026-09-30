<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/Closing_Date_Wise_Get_Data.aspx.cs" Inherits="StatePages_Closing_Date_Wise_Get_Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
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
    <style type="text/css">
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
            background: white !important;
            color: black !important;
        }

        .GridViewHeader th {
            color: white !important; /* header text white */
            background-color: #4CAF50 !important; /* optional background */
            text-align: center;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            color: black !important;
        }

        element.style {
            font-size: medium !important;
        }

        .auto-style3 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 50%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
    <style>
        body {
            background: #f4f6f9;
            font-family: 'Segoe UI', sans-serif;
        }

        .datepicker-card {
            background: #fff;
            border-radius: 1rem;
            border: 2px solid #dee2e6;
            box-shadow: 0 4px 12px rgba(0,0,0,0.08);
            padding: 2rem;
            transition: all 0.3s ease;
        }

            .datepicker-card:hover {
                box-shadow: 0 6px 18px rgba(0,0,0,0.12);
                border-color: #0d6efd;
            }

        .input-group-text {
            border-radius: 0.75rem 0 0 0.75rem;
            border: 2px solid #0d6efd;
            border-right: none;
            background: #0d6efd;
            color: #fff;
        }

        .form-control {
            border-radius: 0 0.75rem 0.75rem 0;
            border: 2px solid #0d6efd;
            border-left: none;
        }

            .form-control:focus {
                box-shadow: none;
                border-color: #198754;
            }

        .datepicker-dropdown {
            border-radius: 1rem !important;
            padding: 10px !important;
            border: 2px solid #0d6efd !important;
        }

        /* 🔹 Month & Year Header */
        .datepicker .datepicker-switch {
            font-weight: 600;
            color: #0d6efd !important;
            background: #e9f2ff;
            border-radius: 8px;
            padding: 6px;
        }

        /* 🔹 Day Names (Sun, Mon, Tue...) */
        .datepicker thead th.dow {
            color: #198754 !important;
            font-weight: 600;
            background: #e9f9f0;
            border-radius: 6px;
            padding: 5px;
        }

        /* 🔹 Today's date */
        .datepicker table tr td.today {
            background: #0d6efd !important;
            color: #fff !important;
            border-radius: 50%;
        }

        /* 🔹 Active (selected) date */
        .datepicker table tr td.active,
        .datepicker table tr td.active:hover {
            background: #198754 !important;
            color: #fff !important;
            border-radius: 50%;
        }
    </style>
    <!-- Bootstrap DatePicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <!-- Bootstrap DatePicker JS -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true,

            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlemp]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlbranch]").select2();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend>Closing Date Wise Get Data </legend>
            <div class="row">
                <div class="col-md-2" style="text-align: center; margin-top: 7px;">
                    <label>Branch</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control" ID="ddlbranch" AutoPostBack="true" runat="server">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="text-align: center; margin-top: 7px;">
                    <label>Financial Year</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlfinancialyear" runat="server" AutoPostBack="false" Width="210px"
                        Height="28px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                        <asp:ListItem Value="0">Select Financial Year</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="text-align: center; margin-top: 7px;">
                    <label>Employees</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlemp" AutoPostBack="true" runat="server">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 20px">
                <div class="col-md-2" style="text-align: center; margin-top: 7px;">
                    <label>Inspection Closing Date</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtdob" placeholder="DD/MM/YYYY" MaxLength="10" autocomplete="off" Width="210px"
                        Height="28px" onpaste="return false ;" CssClass="tb6 datepicker" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtdob" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtdob" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                </div>
                <div class="col-md-2" style="text-align: center; margin-top: 7px;">
                    <label>Inspection Type</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlverification" runat="server" AutoPostBack="false" Width="210px"
                        Height="28px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="1">General Inspection</asp:ListItem>
                        <asp:ListItem Value="2">Physical Verification</asp:ListItem>
                        <asp:ListItem Value="3">Both</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="text-align: center; margin-top: 7px;">
                    <label>Quarter</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlquater" runat="server" AutoPostBack="false" Width="210px"
                        Height="28px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="1">1st Quarter</asp:ListItem>
                        <asp:ListItem Value="2">2nd Quarter</asp:ListItem>
                        <asp:ListItem Value="3">3rd Quarter</asp:ListItem>
                        <asp:ListItem Value="4">4th Quarter</asp:ListItem>
                        <asp:ListItem Value="5">Half Yearly Inspection</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="Row" style="margin-top: 20px">
                <div class="col-md-5"></div>
                <div class="col-md-2" style="text-align: center; margin-top: 17px">
                    <asp:Button runat="server" CssClass="btn btn-success btn-lg " ValidationGroup="A" Font-Italic="true" Text="SUBMIT" ID="btnupdatereg" OnClick="btnupdatereg_Click" />
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

