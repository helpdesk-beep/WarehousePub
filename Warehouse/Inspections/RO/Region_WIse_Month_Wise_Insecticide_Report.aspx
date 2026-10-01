<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="~/Inspections/RO/Region_WIse_Month_Wise_Insecticide_Report.aspx.cs" Inherits="Inspections_RO_Region_WIse_Month_Wise_Insecticide_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <link href="../../Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="../../Assets/js/bootstrap-datepicker.js"></script>
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
        /* Search box + button wrapper */
        .search-box {
            display: flex;
            justify-content: center;
            align-items: center;
            margin: 20px;
        }

            /* Input box */
            .search-box input[type="text"] {
                padding: 10px 15px;
                border: 2px solid #4CAF50;
                border-radius: 25px 0 0 25px;
                outline: none;
                font-size: 14px;
                width: 250px;
                transition: 0.3s;
            }

                .search-box input[type="text"]:focus {
                    border-color: #008CBA;
                }

            /* Button */
            .search-box button {
                padding: 10px 20px;
                border: 2px solid #4CAF50;
                background-color: #4CAF50;
                color: white;
                font-size: 14px;
                font-weight: bold;
                border-radius: 0 25px 25px 0;
                cursor: pointer;
                transition: 0.3s;
            }

                .search-box button:hover {
                    background-color: #45a049;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend>Region/Month Wise Insecticide report</legend>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-1" style="margin-top:6px">
                    <label>Insecticide</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlinsecticide" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">All</asp:ListItem>
                        <asp:ListItem Value="1">Alluminium Phosphide</asp:ListItem>
                        <asp:ListItem Value="2">Malathion</asp:ListItem>
                        <asp:ListItem Value="3">Deltamethrin</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1" style="margin-top:6px">
                    <label>Month</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlmonth" Height="35px" CssClass="form-control"
                        runat="server">
                        <asp:ListItem Value="0">All</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                     <asp:Button class="btn btn-info" ID="btnsearch" ValidationGroup="a" runat="server" 
                         Text="SEARCH" OnClick="btnsearch_Click"></asp:Button>
                        
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Details</legend>
            <div class="row" style="align-content: center; margin-top: 10px" runat="server">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="grdinsecticide" ShowFooter="true"  OnRowDataBound="grdinsecticide_RowDataBound"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                <asp:BoundField DataField="Branch_Name" HeaderText="Branch Name" />
                                <asp:BoundField DataField="Insecticide_Name" HeaderText="Insecticide Name" />
                                <asp:TemplateField HeaderText="Received quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceipt_Balance_quantity" runat="server" Text='<%# Eval("Receipt_Balance_quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received quantity market value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceipt_Balance_market_value" runat="server" Text='<%# Eval("Receipt_Balance_market_value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Received value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceipt_Balance_value" runat="server" Text='<%# Eval("Receipt_Balance_value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Consumption quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConsumption_Balance_quantity" runat="server" Text='<%# Eval("Consumption_Balance_quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Consumption quantity market value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConsumption_Balance_market_value" runat="server" Text='<%# Eval("Consumption_Balance_market_value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Consumption value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConsumption_Balance_value" runat="server" Text='<%# Eval("Consumption_Balance_value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transfer quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransfer_Balance_quantity" runat="server" Text='<%# Eval("Transfer_Balance_quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transfer quantity market value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransfer_Balance_market_value" runat="server" Text='<%# Eval("Transfer_Balance_market_value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Transfer value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransfer_Balance_value" runat="server" Text='<%# Eval("Transfer_Balance_value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClosingBalance" runat="server" Text='<%# Eval("ClosingBalance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

