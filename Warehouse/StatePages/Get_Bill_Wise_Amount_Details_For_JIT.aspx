<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/StatePages/Get_Bill_Wise_Amount_Details_For_JIT.aspx.cs" Inherits="StatePages_Get_Bill_Wise_Amount_Details_For_JIT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        <asp:Label runat="server" Visible="false" ID="lblmsg"></asp:Label>
        <div class="content-wrapper">
            <fieldset>
                <legend>Bill Wise Amount Details</legend>
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-1" style="margin-top: 8px">
                        <label>Bill Number</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtbillnumber" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                    <div class="col-md-2" style="text-align: center; margin-top: 4px">
                        <asp:Button runat="server" CssClass="btn btn-success btn-sm" ValidationGroup="a" Text="SEARCH" ID="btnsearch" OnClick="btnsearch_Click" autopostback="true" />
                    </div>
                </div>
            </fieldset>
            <fieldset>
                <legend>Institution Bill Details</legend>
                <div class="row" style="align-content: center; margin-top: 10px" runat="server" id="divinsititution" visible="false">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="grdbill" CssClass="table table-bordered table-hover datatable" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Number" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblBill_Number" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commodity" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblcommodity_Name" Text='<%# Eval("commodity_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Year" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCrop_Year" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMonth" Text='<%# Eval("Month") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Type" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblBill_Type" Text='<%# Eval("Bill_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Amount" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </fieldset>
            <fieldset>
                <legend>Deleted Bill Details</legend>
                <div class="row" style="align-content: center; margin-top: 10px" runat="server" id="divlog" visible="false">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="grdbilllog" CssClass="table table-bordered table-hover datatable" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Number" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblBill_Number" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commodity" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblcommodity_Name" Text='<%# Eval("commodity_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Year" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCrop_Year" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMonth" Text='<%# Eval("Month") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Type" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblBill_Type" Text='<%# Eval("Bill_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Amount" HeaderStyle-BackColor="LightBlue">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>'></asp:Label>
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
