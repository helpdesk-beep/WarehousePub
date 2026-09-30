<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Reports/States/Rpt_Godown_CropYear_Depositorwise_StockPosition.aspx.cs" Inherits="Region_State_Rpt_Godown_CropYear_Depositorwise_StockPosition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title style="color: white;">Stock Position</title>

    <link href="../../assets/New//css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../../assets/New/CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
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
        <div class="content-wrapper">
            <fieldset>
                <legend style="text-align: center; font-size: large;">Report For Review of District, Depositor, CropYear Wise, Commodity Wise Stock Position Report (Quantity in MT)</legend>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblDistrict" runat="server" Font-Size="10pt" Font-Bold="true">Date (DD-MM-YYYY) :</asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtpaymentdate" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="true">Depositor :</asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddldepositor" runat="server" selectionmode="Multiple">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label2" runat="server" Font-Size="10pt" Font-Bold="true">Commodity :</asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlComodity" runat="server" selectionmode="Multiple">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px">
                    <div class="col-md-5"></div>
                    <div class="col-md-1">
                        <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-info btn-block" OnClick="btnshow_Click" />
                    </div>
                </div>
            </fieldset>
            <%--<div class="row" style="text-align: center; font-size: large;">
                <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
                <h4 class="header">Report For Review of District, Depositor, CropYear Wise, Commodity Wise Stock Position Report (Quantity in MT) </h4>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-sm-8 col-sm-offset-4">
                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        <asp:HiddenField ID="hfId" Value="0" runat="server" />
                        <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
                    </div>
                </div>
            </div>--%>
            <%-- <div class="row" style="text-align: center; font-size: large;">
            </div>--%>
        </div>
        <fieldset>
            <legend>Details</legend>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <div style="overflow-x: scroll;">
                            <asp:GridView ID="GV_StockPositionDetails" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                EnableModelValidation="True"
                                CellPadding="4" rules="all" ForeColor="#333333" ShowFooter="true"
                                OnRowDataBound="GV_StockPositionDetails_OnRowDataBound">
                                <RowStyle BackColor="White" CssClass="ADFieldsGridText table-responsive" HorizontalAlign="Left" />
                                <Columns>

                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Region">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("RegionName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("District") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BranchName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Godown">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGodown" runat="server" Text='<%# Eval("GodownName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Godown Type">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGodownType" runat="server" Text='<%# Eval("GodownType") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                    <%--<asp:TemplateField HeaderText="Crop Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCropYear" runat="server" Height="21px" Text='<%# Eval("CropYear") %>'> 
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Branch Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepotName" runat="server" Text='<%# Eval("DepotName") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Godown Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGodown" runat="server" Text='<%# Eval("Godown") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Year [2017-18]" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY1" runat="server" Text='<%# Eval("[2017-18]") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Year [2018-19]" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY2" runat="server" Text='<%# Eval("[2018-19]") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Year [2019-20]" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY3" runat="server" Text='<%# Eval("[2019-20]") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Year [2020-21]" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY4" runat="server" Text='<%# Eval("[2020-21]") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Crop Year [2021-22]" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY5" runat="server" Text='<%# Eval("[2021-22]") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Year [2022-23]" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY6" runat="server" Text='<%# Eval("[2022-23]") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Year [2023-24]" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY7" runat="server" Text='<%# Eval("[2023-24]") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Year [2024-25]" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY8" runat="server" Text='<%# Eval("[2024-25]") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Crop Year [2025-26]" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY8" runat="server" Text='<%# Eval("[2024-25]") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Crop Year [2021-22]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate >
                                        <asp:Label ID="lblCY1" runat="server" Text='<%# Eval("[2021-22]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year [2022-23]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCY2" runat="server" Text='<%# Eval("[2022-23]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year [2023-24]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCY3" runat="server" Text='<%# Eval("[2023-24]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year [2024-25]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCY4" runat="server" Text='<%# Eval("[2024-25]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="कुल भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                </Columns>
                                <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                    Height="20px" Font-Size="12pt" />
                                <AlternatingRowStyle BackColor="#eeeeee" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "receivedPaymentfromMPSCSCthroughNEFTPaymentSystem.xls"
                });
            });
        </script>
        <script>
            $(document).ready(function () {
                $("[id$=txtfromdate]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                });
            });
        </script>
        <script>
            $(document).ready(function () {
                $("[id$=txttodate]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                });
            });
        </script>
    </form>
    <!--Java Script -->

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
</body>
</html>
