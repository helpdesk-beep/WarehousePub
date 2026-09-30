<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/GodownWise_PaymentStatusInformation.aspx.cs" Inherits="BranchPages_GodownWise_PaymentStatusInformation" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label runat="server" ID="lblMsg"></asp:Label>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>निजी गोदामों के भुगतान की स्थिति (प्रोफोर्मा-ब) </legend>
            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblFinancialYear" Font-Bold="true" runat="server" ForeColor="Navy">वित्‍तीय वर्ष:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlFinancialYear" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="2010-11">2010-11</asp:ListItem>
                        <asp:ListItem Value="2011-12">2011-12</asp:ListItem>
                        <asp:ListItem Value="2012-13">2012-13</asp:ListItem>
                        <asp:ListItem Value="2013-14">2013-14</asp:ListItem>
                        <asp:ListItem Value="2014-15">2014-15</asp:ListItem>
                        <asp:ListItem Value="2015-16">2015-16</asp:ListItem>
                        <asp:ListItem Value="2016-17">2016-17</asp:ListItem>
                        <asp:ListItem Value="2017-18">2017-18</asp:ListItem>
                        <asp:ListItem Value="2018-19">2018-19</asp:ListItem>
                        <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                        <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                        <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                        <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                        <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                        <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                        <asp:ListItem Value="2025-26">2025-26</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblGodownType" Font-Bold="true" runat="server" ForeColor="Navy">गोदाम का प्रकार:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlGodownType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblGodownName" Font-Bold="true" runat="server" ForeColor="Navy">गोदाम का नाम:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlGodownName" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblAgrCapacity" Font-Bold="true" runat="server" ForeColor="Navy">अनुबंधित क्षमता:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">

                    <asp:TextBox ID="txtAgreementCapacity" runat="server" CssClass="form-control" Placeholder="अनुबंधित क्षमता दर्ज करे" onkeypress="return isNumber()" Text='<%# Eval("GodownAgreementCapacity(InMT)") %>'></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblAgrDate" Font-Bold="true" runat="server" ForeColor="Navy">अनुबंध दिनांक (dd/MM/yyyy):</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtAgreementDate" runat="server" CssClass="form-control" Placeholder="अनुबंध दिनांक(01/01/2024)दर्ज करे" AutoPostBack="true" Text='<%# Eval("GodownAgreementDate") %>'></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblRentinFY" Font-Bold="true" runat="server" ForeColor="Navy">वित्‍तीय वर्ष में किराये की कुल राशि:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtRentAmountInFY" runat="server" CssClass="form-control" onkeypress="return isNumber()" Text='<%# Eval("TotalAmountInRentInFY") %>'></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblPaymentFY" Font-Bold="true" runat="server" ForeColor="Navy">वित्‍तीय वर्ष में भुगतान राशि:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtFYPaymentAmount" runat="server" CssClass="form-control" onkeypress="return isNumber()" Text='<%# Eval("TotalAmountPaidToGodownInFY") %>'></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblGodownOwnerRemAmt" Font-Bold="true" runat="server" ForeColor="Navy">गोदाम संचालक की शेष लंबित राशि:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtGodownOwnerRemainingAmt" runat="server" CssClass="form-control" onkeypress="return isNumber()" Text='<%# Eval("RemainingAmountOfGodownOwner") %>'></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblAgrEndDate" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">अनुबंध समाप्ति दिनांक (dd/MM/yyyy):</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtAgreementEndDate" runat="server" CssClass="form-control" Placeholder="अनुबंध समाप्ति दिनांक(01/01/2024)दर्ज करे" Text='<%# Eval("AgreementEndDate") %>'></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblRemarks" Font-Bold="true" runat="server" ForeColor="Navy">Remarks:</asp:Label>
                    </div>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 15px">
                <div style="width: 100%; background-repeat: no-repeat;">
                    <div style="position: relative;">
                        <p style="text-align: start;">
                            <strong style="color: red">* क्षमता मे.टन में एवं राशि Cr. में प्रविष्ट करे :-
                            </strong>
                            <br />
                        </p>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-success" Enabled="true" OnClick="btnSubmit_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn-danger" />
                </div>
            </div>
        </fieldset>
        <div class="row" id="grdentry" style="margin-top: 20px">
            <fieldset>
                <legend>शाखा प्रबंधक द्वारा दर्ज  की गई जानकारी </legend>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GV_EntryDone" CellPadding="5" OnRowCommand="GV_EntryDone_RowCommand"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" autopostback="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष">
                                        <ItemTemplate>
                                            <asp:Label ID="txtFY" Enabled="false" runat="server" Text='<%# Eval("FinancialYear") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="गोदाम का नाम">
                                        <ItemTemplate>
                                            <asp:Label ID="txtEVGodownName" Enabled="false" runat="server" Text='<%# Eval("GodownName") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="गोदाम का प्रकार">
                                        <ItemTemplate>
                                            <asp:Label ID="txtGodownType" Enabled="false" runat="server" Text='<%# Eval("GodownType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अनुबंधित क्षमता">
                                        <ItemTemplate>
                                                                           
                                            <asp:Label ID="lblAgreementCapacity" Enabled="false" runat="server" Text='<%# Eval("GodownAgreementCapacity") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अनुबंध दिनांक">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAgreementDate" Enabled="false" runat="server" Text='<%# Eval("GodownAgreementDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष में किराये की कुल राशि">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_GodownRentAmount" Enabled="false" runat="server" Text='<%# Eval("TotalAmountInRentInFY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष में भुगतान राशि">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_AmountPaymentToGodown" Enabled="false" runat="server" Text='<%# Eval("TotalAmountPaidToGodownInFY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="गोदाम संचालक की शेष लंबित राशि">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGodownOwnerPendingAmount" Enabled="false" runat="server" Text='<%# Eval("RemainingAmountOfGodownOwner") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अनुबंध समाप्ति दिनांक">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAgreementEndDate" Enabled="false" runat="server" Text='<%# Eval("AgreementEndDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" Enabled="false" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
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
