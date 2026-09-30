<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/ReportDeliveryForm.aspx.cs" Inherits="BranchPages_ReportDeliveryForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Warehouse Gatepass</title>

    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <link href="../Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="../Assets/js/bootstrap-datepicker.js"></script>
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
        /* Custom date picker colors */
        .datepicker {
            background-color: #f0f8ff; /* Light blue background */
        }

            .datepicker table tbody tr td.active,
            .datepicker table tbody tr td.active:hover {
                background-color: #ff6347; /* Tomato color for selected date */
                color: white; /* White text color for selected date */
            }

            .datepicker table tbody tr td:hover {
                background-color: #87ceeb; /* Sky blue color on hover */
            }

            .datepicker .datepicker-days .datepicker-switch {
                color: #008080; /* Teal color for the month/year switch */
            }

            .datepicker .datepicker-days .prev,
            .datepicker .datepicker-days .next {
                color: #008080; /* Teal arrows */
            }

            .datepicker table {
                border: 2px solid #008080; /* Teal border around the calendar */
            }

                .datepicker table tbody tr td {
                    color: #333; /* Dark text color for the dates */
                }
    </style>

    <!-- Bootstrap DatePicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <!-- Bootstrap DatePicker JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <script>
        $(document).ready(function () {
            var now = new Date();

            //var startOfMonth = new Date(now.getFullYear(), now.getMonth(), 1);
            //var endOfMonth = new Date(now.getFullYear(), now.getMonth() + 1, 0);

            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true,
                //startDate: startOfMonth,
                //endDate: endOfMonth
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="form-container">
            <div class="headingBig text-center">
                एम.पी वेअरहाउसिंग एंड लॉजिस्टिक्स कार्पोरेशन
            <br />
                माल जमा (डिपॉजिट) करने का प्रार्थना पत्र 
           
            </div>

            <hr />

            <div class="form-group">
                <div class="row">
                    <div class="col-md-2 text-right">
                        Branch:
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlBranch" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 text-right">
                        Date: 
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtDate" CssClass="form-control datepicker" runat="server" Text="14/04/2022" />
                    </div>
                </div>
                <div class="row" style="margin-top: 30px;">
                    <div class="col-md-2 text-right">
                        Godown: 
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlGodown" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 text-right">
                        Delivery Order No:
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList CssClass="form-control" ID="ddlDeliveryOrderNo" runat="server" />
                    </div>
                    <%-- <div>
                    Receipt No: <asp:Label ID="lblReceiptNo" runat="server"  /> 
                </div>--%>
                </div>
                <div class="row" style="margin-top: 30px;">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit & View Report" OnClick="btnSubmit_Click" CssClass="btn btn-default" />
                        <asp:Button ID="btnExport" runat="server" Text="Export to PDF" OnClick="btnExport_Click" CssClass="btn-info" Visible="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

