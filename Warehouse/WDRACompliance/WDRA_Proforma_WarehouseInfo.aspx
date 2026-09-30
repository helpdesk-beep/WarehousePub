<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/WDRACompliance/WDRA_Proforma_WarehouseInfo.aspx.cs" Inherits="WDRACompliance_WDRA_Proforma_WarehouseInfo" %>

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
            <legend>WDRA Proforma Warehouse and Owner Information</legend>
            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWHYoC" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Year of Construction:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWHYoC" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="Please Enter Warehouse Year of Construction"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWHName" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Name:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWHName" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="Please Enter Warehouse Name"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWHAddress" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Address:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWHAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
                    <legend>Warehouse Owner Details</legend>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Label ID="lblWHOwnerName" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Owner Name:</asp:Label><strong style="color: red">*</strong>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtWHOwnerName" runat="server" CssClass="form-control" Placeholder="Please Enter Warehouse Owner Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Label ID="lblWHOwnerContact" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Owner Contact Number:</asp:Label><strong style="color: red">*</strong>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtWHOwnerContact" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="Please Enter Warehouse Owner Contact"></asp:TextBox>
                        <br />
                    </div>
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Label ID="lblAltContact" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Owner Alternate Contact Number:</asp:Label><strong style="color: red">*</strong>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtWHAltContact" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="Please Enter Warehouse Owner Alternate Contact"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Label ID="lblWHOwnerAddress" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Owner Address:</asp:Label><strong style="color: red">*</strong>
                        </div>
                    </div>
                    <div class="col-md-2">

                        <asp:TextBox ID="txtWHOwnerAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>

                </fieldset>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWDRARegNo" Font-Bold="true" runat="server" ForeColor="Navy">WDRA Registration No:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWDRARegNo" runat="server" CssClass="form-control" Placeholder="Please Enter WDRA Registration No"></asp:TextBox>
                </div>
                <div class="col-md-2">
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWDRALicValidity" Font-Bold="true" runat="server" ForeColor="Navy">Licence Validity Date (dd/MM/yyyy):</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtValidityDate" runat="server" CssClass="form-control" Placeholder="Please Enter Licence Validity Date" AutoPostBack="true"></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="txtValidityDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWHLicIssuingDate" Font-Bold="true" runat="server" ForeColor="Navy">Issuing Date:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWHLicenseIssueDate" runat="server" CssClass="form-control" Placeholder="Please Enter Licence Issue Date" AutoPostBack="true"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                        Enabled="True" TargetControlID="txtWHLicenseIssueDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWHOwnerPoI" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Owner Aadhar No:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWHOwnerAadhar" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWHOwnerEmail" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Owner Email</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWHOwnerEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <%--    <div class="row" style="margin-top: 10px">
        <div class="col-md-2">
            <div class="form-group">
                <asp:Label ID="lblRemarks" Font-Bold="true" runat="server" ForeColor="Navy">Remarks:</asp:Label>
            </div>
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>--%>

            <div class="row">
                <div class="col-md-5"></div>
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
                <legend>Warehouse Information as per Godown Manager</legend>
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
                                    <asp:TemplateField HeaderText="Warehouse Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl" Enabled="false" runat="server" Text='<%# Eval("WH_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WH Year of Construction">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYoC" Enabled="false" runat="server" Text='<%# Eval("WH_YoC") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Owner Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHOwnerName" Enabled="false" runat="server" Text='<%# Eval("WH_OwnerName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Owner Contact">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHContact" Enabled="false" runat="server" Text='<%# Eval("WH_OwnerContact1") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alternate Contact">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHOwnerContact2" Enabled="false" runat="server" Text='<%# Eval("WH_OwnerContact2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Owner Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHOwnerEmail" Enabled="false" runat="server" Text='<%# Eval("WH_OwnerEmail") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Owner Aadhar No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHOwnerAadhar" Enabled="false" runat="server" Text='<%# Eval("WH_OwnerPOI") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Owner Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHOwnerAddress" Enabled="false" runat="server" Text='<%# Eval("WH_OwnerAddress") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warehouse Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHAddress" Enabled="false" runat="server" Text='<%# Eval("WH_Address") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WDRA License No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWDRALicNo" Enabled="false" runat="server" Text='<%# Eval("WH_WDRALicenceNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WDRA License Validity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHLicValidityDate" Enabled="false" runat="server" Text='<%# Eval("WH_WDRALicenceValidityDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WDRA License Issue Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHLicIssueDate" Enabled="false" runat="server" Text='<%# Eval("WH_WDRALicenceIssueDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" Enabled="false" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
