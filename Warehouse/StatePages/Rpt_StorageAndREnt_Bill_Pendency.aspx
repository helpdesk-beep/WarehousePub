<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/Rpt_StorageAndREnt_Bill_Pendency.aspx.cs" Inherits="StatePages_Rpt_StorageAndREnt_Bill_Pendency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
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

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <fieldset>
            <legend>Storage And Rent Bill Pendding Status</legend>
            <div class="row">
                <div class="col-md-2">
                    <label>Financial Year</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlfinancial" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfinancial_SelectedIndexChanged">
                         <asp:ListItem Text="Select"></asp:ListItem>
                        <asp:ListItem Text="2018-2019"></asp:ListItem>
                        <asp:ListItem Text="2019-2020"></asp:ListItem>
                        <asp:ListItem Text="2020-2021"></asp:ListItem>
                        <asp:ListItem Text="2021-2022"></asp:ListItem>
                        <asp:ListItem Text="2022-2023"></asp:ListItem>
                        <asp:ListItem Text="2023-2024"></asp:ListItem>
                        <asp:ListItem Text="2024-2025"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="GV_StockReport" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Regionnm" HeaderText="Region Name" />
                            <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                            <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" />
                            <asp:BoundField DataField="TotalSCBillGenerated" HeaderText="Total Storage Bill Generated" />
                            <asp:BoundField DataField="TotalRentBillGenerated" HeaderText="Total Rent Bill Generated" />
                            <asp:BoundField DataField="PendingRentBillForGenerate" HeaderText="Pending Rent Bill For Generate" />
                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

