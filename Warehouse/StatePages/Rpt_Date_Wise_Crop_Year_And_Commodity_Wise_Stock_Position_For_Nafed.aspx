<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Date_Wise_Crop_Year_And_Commodity_Wise_Stock_Position_For_Nafed.aspx.cs" Inherits="StatePages_Rpt_Date_Wise_Crop_Year_And_Commodity_Wise_Stock_Position_For_Nafed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend align="center">Godown Wise Date Wise Stock Position For Nafed
                    <label style="color: red">(qty In qtl.)</label></legend>
                <div class="container py-4" style="margin-bottom: 15px">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                          <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="text-align: center; font-size: large;">
                    <div class="col-md-2">
                        <asp:Label ID="lblfromDate" runat="server" Font-Size="10pt" Font-Bold="true">From Date (DD/MM/YYYY) :</asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtFromDate" placeholder="dd/mm/yyyy" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lbltodate" runat="server" Font-Size="10pt" Font-Bold="true">To Date (DD/MM/YYYY) :</asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txttodate" placeholder="dd/mm/yyyy" autocomplete="off" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="true">Commodity :</asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlComodity" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px">
                    <div class="col-md-2">
                        <asp:Label ID="lblcropyear" runat="server" Font-Size="10pt" Font-Bold="true">Crop Year :</asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList CssClass="form-control" ID="ddlcropyear" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="button button2" OnClick="btnshow_Click" />
                    </div>
                </div>
            </fieldset>
            <fieldset>
                <legend>Details</legend>
                <div class="row" style="align-content: center; margin-top: 10px" runat="server">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                                OnRowDataBound="GridView1_RowDataBound"  OnDataBound="OnDataBound"
                                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                                <Columns>
                                    <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                    <asp:BoundField DataField="Branch_Name" HeaderText="Branch" />
                                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                    <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                                    <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />
                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" />
                                    <asp:TemplateField HeaderText="Opening Bags">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpeningBags" runat="server" Text='<%# Eval("OpeningBags") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpeningQty" runat="server" Text='<%# Eval("OpeningQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Bags">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecBags" runat="server" Text='<%# Eval("RecBags") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecQty" runat="server" Text='<%# Eval("RecQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delevery Bags">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDelBags" runat="server" Text='<%# Eval("DelBags") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delevery Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDelQty" runat="server" Text='<%# Eval("DelQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Bags">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosingBags" runat="server" Text='<%# Eval("ClosingBags") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Weight">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosingWeight" runat="server" Text='<%# Eval("ClosingWeight") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
