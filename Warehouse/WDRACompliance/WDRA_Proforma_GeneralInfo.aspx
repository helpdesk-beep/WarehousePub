<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/WDRACompliance/WDRA_Proforma_GeneralInfo.aspx.cs" Inherits="WDRACompliance_WDRA_Proforma" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../assets/css/style.css" rel="stylesheet" />

    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-datepicker.css" rel="stylesheet" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label runat="server" ID="lblMsg"></asp:Label>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>WDRA Proforma - Warehouse General Information</legend>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWHYoC" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Year Of Construction:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox CssClass="form-control select2" ID="txtWHYoC" runat="server">
                    </asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWHLengthInMt" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Length (In Meters):</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWHLength" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="Please Warehouse Length"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWHBreadthInMt" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Breadth (In Meters):</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWHBreadth" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="Please Warehouse Breadth"></asp:TextBox>
                    <br />
                    <br />
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWHHeightInMt" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Height (In Meters):</asp:Label><strong style="color: red">*
                            </strong>
                    </div>
                </div>
                <div class="col-md-2">

                    <asp:TextBox ID="txtWHHeight" runat="server" CssClass="form-control" Placeholder="Please Warehouse Height"></asp:TextBox>
                    <br />
                    <br />
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblPlinthHeight" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Plinth Height (In centimeters):</asp:Label><strong style="color: red">*</strong>
                </div>
            </div>
            <div class="col-md-2">

                <asp:TextBox ID="txtPlinthHeight" runat="server" CssClass="form-control" Placeholder="Please Enter Warehouse Plinth Height"></asp:TextBox>
            </div>


            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWHCapacity" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Capacity (In MT):</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">

                    <asp:TextBox ID="txtWHCapacity" runat="server" CssClass="form-control" Placeholder="Please Enter Warehouse Capacity"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWHCapacityGodownWise" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Capacity Godownwise:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">

                    <asp:TextBox ID="txtWHCapacityGodownWise" runat="server" CssClass="form-control" Placeholder="Please Enter Warehouse Capacity Godownwise"></asp:TextBox>
                </div>
            </div>


            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button ID="btnClkBack" runat="server" Text="Back" CssClass="btn-danger" OnClick="btnClkBack_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-success" Enabled="true" OnClick="btnSubmit_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnClkNext" runat="server" Text="Next" CssClass="btn-danger" OnClick="btnClkNext_Click" />
                </div>
            </div>
        </fieldset>
    </div>
    <div class="content-wrapper">
        <div class="row" id="grdentry" style="margin-top: 20px">
            <fieldset>
                <legend>Warehouse General Information (गोदाम प्रबंधक द्वारा दर्ज की गई जानकारी)</legend>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GV_EntryDone" CellPadding="5" OnRowCommand="GV_EntryDone_RowCommand"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" autopostback="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SN" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WH Year of Construction">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHYoC" Enabled="false" runat="server" Text='<%# Eval("WH_YearOfConstruction") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WH Length(in mts) ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHLength" Enabled="false" runat="server" Text='<%# Eval("WH_LengthInMt") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WH Breadth(in mts)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHBreadth" Enabled="false" runat="server" Text='<%# Eval("WH_BreadthInMt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WH Height(in mts)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHHeight" Enabled="false" runat="server" Text='<%# Eval("WH_HeightInMt") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WH Plinth Height(in cmts)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHPlinthHt" Enabled="false" runat="server" Text='<%# Eval("WH_PlinthHeightInCM") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WH Capacity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHCapacityInMT" Enabled="false" runat="server" Text='<%# Eval("WH_Capacity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WH Capacity Godown Wise">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHCapacityGodownWise" Enabled="false" runat="server" Text='<%# Eval("WH_CapacityGodownWise") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="RemoveRow" CssClass="btn-danger" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div align="center">No records found.</div>
                                </EmptyDataTemplate>
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
            </fieldset>
        </div>
    </div>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 32 && (charCode < 46 || charCode == 47 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript" src="../../assets/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript">
        $(".dateAdd").datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            changemonth: true,
            changeyear: true
        });
    </script>
</asp:Content>
