<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/WDRACompliance/WDRA_Proforma_WeighBridge.aspx.cs" Inherits="WDRACompliance_WDRA_Proforma_WeighBridge" %>

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
            <legend>WDRA Proforma For Weighbridge </legend>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWBCapacity" Font-Bold="true" runat="server" ForeColor="Navy">Weighbridge Capacity:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox CssClass="form-control select2" ID="txtWeighbridgeCapacity" Placeholder="Enter Weighbridge Capacity in MT" runat="server">
                    </asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWBMake" Font-Bold="true" runat="server" ForeColor="Navy">Make of Weighbridge:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWBMake" runat="server" CssClass="form-control" Placeholder="Enter Weighbridge Make"></asp:TextBox>
                </div>
                &nbsp;
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWBInstallation" Font-Bold="true" runat="server" ForeColor="Navy">Date of Installation of Weighbridge(dd/MM/yyyy):</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtInstallationDate" runat="server" CssClass="form-control" Placeholder="Enter Installation Date of Weighbridge" AutoPostBack="true"></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="txtInstallationDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWBStampingDate" Font-Bold="true" runat="server" ForeColor="Navy">Weighbridge Last Stamping Date:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWBStampingDate" runat="server" CssClass="form-control" Placeholder="Enter Last Stamping Date of Weighbridge"></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox3_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="txtWBStampingDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWBLorryType" Font-Bold="true" runat="server" ForeColor="Navy">Type of Lorry Weighbridge:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:RadioButtonList CssClass="form-control select2" ID="rdlWBLorryType" runat="server">
                        <asp:ListItem Text="Internal" Value="Internal" />
                        <asp:ListItem Text="External" Value="External" />
                    </asp:RadioButtonList>
                </div>
                <div class="col-md-2">
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWBOwnerName" Font-Bold="true" runat="server" ForeColor="Navy">Name of Weighbridge Owner:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWBOwnerName" runat="server" CssClass="form-control" Placeholder="Enter Weighbridge Owner Name"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWBName" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Name of Weighbridge:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWBName" runat="server" CssClass="form-control" Placeholder="Enter Weighbridge Name"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblDistance" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Distance of Weighbridge from Warehouse:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtDistance" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="Enter Weighbridge Distance from Warehouse"></asp:TextBox>
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
        <div class="row" id="grdentry" style="margin-top: 20px">
            <fieldset>
                <legend>Weighbridge Details</legend>
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
                                    <asp:TemplateField HeaderText="Weighbridge Capacity (InMT)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWBCapacity" Enabled="false" runat="server" Text='<%# Eval("WeighbridgeCapacityInMT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weighbridge Make">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWBMake" Enabled="false" runat="server" Text='<%# Eval("WeighbridgeMake") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weighbridge Date of Installation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWBInstallDate" Enabled="false" runat="server" Text='<%# Eval("WeighbridgeDateOfInstallation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weighbridge Last Stamping Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWBLastStampingDate" Enabled="false" runat="server" Text='<%# Eval("WeighbridgeDateOfDateLastStamping") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weighbridge Type of Lorry">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWBLorryType" Enabled="false" runat="server" Text='<%# Eval("WBTypeOfLorry") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weighbridge Owner Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWBOwnerName" Enabled="false" runat="server" Text='<%# Eval("WBOwnerName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weighbridge Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWBName" Enabled="false" runat="server" Text='<%# Eval("WBName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weighbridge Distance from Godown">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWBDistance" Enabled="false" runat="server" Text='<%# Eval("WBDistanceFromGodown") %>'></asp:Label>
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
