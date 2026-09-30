<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="~/Accounting/Nafed_Comodity_Wise_Stock_Position_For_MArketing.aspx.cs" Inherits="Accounting_Nafed_Comodity_Wise_Stock_Position_For_MArketing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title style="color: white;">Nafed Commodity Wise Stock Position</title>
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript">  
        $(document).ready(function () {
            $("#EmployeeGridViewList").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=GrdStock.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }
    </script>
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= GrdStock.ClientID %>');
            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
                'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
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

        .auto-style1 {
            height: 10px;
            width: 558px;
        }

        .auto-style2 {
            width: 558px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="content-wrapper">
                <fieldset>
                    <legend>Stock Certificate</legend>
                    <div class="row">
                        <div class="col-md-1" style="margin-top: 4px; text-align: center">
                            <asp:Label runat="server" ID="lbldate" Font-Bold="true" Font-Size="Medium">As on</asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtdate" ReadOnly="true" Placeholder="../../...."></asp:TextBox>
                        </div>
                        <div class="col-md-5"></div>
                        <div class="col-md-4">
                            <asp:Button ID="Button2" runat="server" Text="Export To PDF" CssClass="btn button2" OnClientClick="printGrid()" />
                            &nbsp;&nbsp;
                    <asp:Button runat="server" CssClass="btn button2" Text="Exporttexcel" ID="btnExport" OnClick="btnExport_Click" />
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView runat="server" ID="GrdStock" ShowFooter="true" FooterStyle-Font-Bold="true"
                                    CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Regional Office" HeaderStyle-BackColor="LightBlue">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District" HeaderStyle-BackColor="LightBlue">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch" HeaderStyle-BackColor="LightBlue">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("DepotName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="COMMODITY" HeaderStyle-BackColor="LightBlue">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCommodity_Name" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CROP YEAR" HeaderStyle-BackColor="LightBlue">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCrop_Year" Text='<%# Eval("CropYear") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BAGS" HeaderStyle-BackColor="LightBlue">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBAGS" Text='<%# Eval("Available_Bags") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY IN QUINTA" HeaderStyle-BackColor="LightBlue">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQTY_IN_QUINTA" Text='<%# Eval("Available_Qty") %>'></asp:Label>
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
        </div>
    </form>
</body>
</html>
