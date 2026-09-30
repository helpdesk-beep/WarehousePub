<%@ Page Title="" Language="C#" MasterPageFile="~/Warehouse/MasterPage/StateMasterNafed.master" AutoEventWireup="true" CodeFile="~/Accounting/ApprovedByNafedAccountByMonth.aspx.cs" Inherits="Accounting_ApprovedByNafedAccountByMonth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <style>
        /*fieldset {
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
        }*/

        /* Container */
        .report-container {
            max-width: 95%;
            margin: 30px auto;
            padding: 20px;
            background-color: #fdfdfd;
            border-radius: 12px;
            box-shadow: 0 5px 20px rgba(0,0,0,0.1);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        /* Header */
        .report-header h3 {
            color: #2c3e50;
            font-weight: 600;
            margin-bottom: 5px;
        }

        .report-header hr {
            border: 0;
            height: 2px;
            background: #3498db;
            border-radius: 2px;
            margin-bottom: 25px;
        }

        /* Form */
        .filter-box {
            margin-bottom: 25px;
            text-align: center;
        }

            .filter-box label {
                font-weight: 600;
                margin-bottom: 5px;
                display: block;
                color: #34495e;
            }

            .filter-box select {
                width: 200px;
                padding: 8px 12px;
                border-radius: 6px;
                border: 1px solid #ccc;
                font-size: 14px;
                transition: all 0.3s ease;
            }

                .filter-box select:hover {
                    border-color: #3498db;
                    box-shadow: 0 0 5px rgba(52, 152, 219, 0.5);
                }

        /* GridView */
        .custom-grid {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

            .custom-grid th, .custom-grid td {
                padding: 12px 15px;
                border: 1px solid #e0e0e0;
                text-align: center;
            }

            .custom-grid th {
                background-color: #3498db;
                color: white;
                font-weight: 600;
                text-transform: uppercase;
            }

            .custom-grid tr:nth-child(even) {
                background-color: #f9f9f9;
            }

            .custom-grid tr:hover {
                background-color: #eaf2fb;
                transition: 0.3s;
            }

            .custom-grid a {
                color: #2980b9;
                font-weight: 500;
                text-decoration: none;
            }

                .custom-grid a:hover {
                    text-decoration: underline;
                }

        /* Responsive */
        @media (max-width: 768px) {
            .filter-box select {
                width: 100%;
            }

            .custom-grid th, .custom-grid td {
                padding: 10px;
            }
        }
    </style>

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

        .custom-grid th, .custom-grid td {
            padding: 12px 15px;
            border: 1px solid #e0e0e0;
            text-align: center;
            font-size: 16px; /* <-- Set font size */
        }
    </style>
    <div class="content-wrapper">
        <!-- Header -->
        <%--<div class="report-header text-center">
            <h3>Approved By Nafed Account - Date Wise Report</h3>
            <hr />
        </div>--%>
        <fieldset>
            <legend>Approved By Nafed Account - Month Wise Report</legend>
            <!-- Month Filter -->
            <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-3" style="margin-top: 8PX">
                    <label style="font-size: medium">Select Account Approval Months</label>
                </div>
                <div class="col-md-2">

                    <asp:DropDownList CssClass="form-control"
                        ID="ddlMonth"
                        runat="server"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                    </asp:DropDownList>

                    <%--<asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server" AutoPostBack="true" OnClientClick="ShowLoader();"
                        OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select Year</asp:ListItem>
                        <asp:ListItem Value="2022">2022</asp:ListItem>
                        <asp:ListItem Value="2023">2023</asp:ListItem>
                        <asp:ListItem Value="2024">2024</asp:ListItem>
                        <asp:ListItem Value="2025">2025</asp:ListItem>
                        <asp:ListItem Value="2026">2026</asp:ListItem>
                        <asp:ListItem Value="2027">2027</asp:ListItem>
                        <asp:ListItem Value="2028">2028</asp:ListItem>
                        <asp:ListItem Value="2029">2029</asp:ListItem>
                        <asp:ListItem Value="2030">2030</asp:ListItem>
                    </asp:DropDownList>--%>
                </div>
            </div>

            <!-- GridView -->
            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false"
                CssClass="custom-grid"
                EmptyDataText="No Records Found">
                <Columns>
                    <asp:TemplateField HeaderText="SN" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %> 
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Month" DataField="Bill_Month_Name"
                        ItemStyle-Width="120px" HeaderStyle-Width="120px" />

                    <asp:BoundField HeaderText="Month Number" DataField="Bill_Month"
                        ItemStyle-Width="80px" HeaderStyle-Width="80px" />

                    <asp:BoundField HeaderText="Total Bill Amount" DataField="TotalBillAmount"
                        DataFormatString="₹ {0:N2}" />

                    <asp:BoundField HeaderText="Financial Year" DataField="Financial_Year"
                        ItemStyle-Width="120px" HeaderStyle-Width="120px" />

                    <asp:BoundField HeaderText="Commodity Name" DataField="Commodity_Name"
                        ItemStyle-Width="120px" HeaderStyle-Width="120px" />

                    <asp:TemplateField HeaderText="Total Approve" ItemStyle-Width="120px" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkApprove" runat="server"
                                Text='<%# Eval("Total_Bill") %>'
                                NavigateUrl='<%# "ApprovedByNafedAccountBySelectMonth.aspx?month=" 
                                    + Eval("Bill_Month") 
                                    + "&fy=" 
                                    + Eval("Financial_Year") 
                                    + "&Commodity=" 
                                    + Eval("Commodity_Id") 
                                    %>
                                '>
                                    
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
    <div id="myspindiv" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(255, 255, 255, 0.7); z-index: 9999; text-align: center;">
        <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <img src="../images/mpwlc3.gif" alt="Loading..." />
        </div>
    </div>


    <script type="text/javascript">
        // Show loader on full postback
        function showLoader() {
            document.getElementById("myspindiv").style.display = "block";
        }

        // For AJAX requests (UpdatePanel, ModalPopup, etc.)
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
            showLoader();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            document.getElementById("myspindiv").style.display = "none";
        });

        // For normal postbacks
        window.onload = function () {
            var theForm = document.forms[0];
            if (theForm.attachEvent) {
                theForm.attachEvent("onsubmit", showLoader);
            } else {
                theForm.addEventListener("submit", showLoader, false);
            }
        };
    </script>

    <script type="text/javascript">
        window.onload = function () {
            var loader = document.getElementById('loader');
            if (loader) {
                loader.style.display = 'none'; // Hide loader after full page load
            }
        };
    </script>


    <script type="text/javascript">
        function ShowLoader() {
            document.getElementById('loader').style.display = 'block';
        }
    </script>

</asp:Content>


