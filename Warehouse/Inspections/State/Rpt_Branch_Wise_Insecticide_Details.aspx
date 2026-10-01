<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="~/Inspections/State/Rpt_Branch_Wise_Insecticide_Details.aspx.cs" Inherits="Inspections_State_Rpt_Branch_Wise_Insecticide_Details" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend>Branch Wise Insecticide Details</legend>
            <div class="row" style="align-content: center; margin-top: 10px" runat="server">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="grdinsecticide" ShowFooter="true"  OnRowDataBound="grdinsecticide_RowDataBound"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:BoundField DataField="Region_Name" HeaderText="Region Name" />
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

