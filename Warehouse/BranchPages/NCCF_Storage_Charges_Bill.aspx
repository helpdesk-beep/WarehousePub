<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/Accounting/NCCF_Storage_Charges_Bill.aspx.cs" Inherits="Accounting_NCCF_Storage_Charges_Bill" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <link href="../Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="../Assets/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
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
            /*text-align: center;*/ /* aligns content inside fieldset */
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
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlGodownType]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlgodown]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlcommodity]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlFinYear]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlmonth]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlcropyr]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlFyear]").select2();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset style="background-color: whitesmoke">
            <legend>NCCF Storage Charges Bill Preparation</legend>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-2" style="margin-top: 5px">
                    <asp:Label ID="Label3" runat="server" Text="Godown Type" Font-Bold="True" Font-Size="12pt"
                        ForeColor="Navy"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlGodownType" runat="server" CssClass="form-control"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">MPWLC Godowns</asp:ListItem>
                        <asp:ListItem Value="2">JVS Godowns</asp:ListItem>
                        <asp:ListItem Value="3">Hired Godowns</asp:ListItem>
                        <asp:ListItem Value="4">Silo Bags</asp:ListItem>
                        <asp:ListItem Value="5">MPWLC Owned Cap</asp:ListItem>
                        <asp:ListItem Value="6">Tribal Scheme</asp:ListItem>
                        <asp:ListItem Value="7">CAP-PMS</asp:ListItem>
                        <asp:ListItem Value="8">BOT</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <asp:Label Font-Size="12pt" Font-Bold="true" ForeColor="navy" ID="lblgodown" runat="server" Text="Godown Name"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True"
                        CssClass="form-control" Font-Size="10pt" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-1"></div>
                <div class="col-md-2">
                    <asp:Label ID="lblcommodity" runat="server" Text="Commodity Name" Font-Bold="True" Font-Size="12pt"
                        ForeColor="Navy"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlcommodity" runat="server" AutoPostBack="True"
                        CssClass="form-control" Font-Size="10pt" OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12pt"
                        ForeColor="Navy" Text="Bill Year:"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlFinYear" runat="server" AutoPostBack="True" CssClass="form-control" Font-Size="10pt">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-1"></div>
                <div class="col-md-2">
                    <asp:Label Font-Size="12pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Bill Month"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="True" CssClass="form-control" Font-Size="10pt">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblcrate" Visible="true" runat="server" Text="Commodity Rate <br/> (Month/15 Day)" Font-Bold="True" Font-Size="12pt"
                        ForeColor="Navy"></asp:Label>
                </div>
                <div class="col-md-1">
                    <asp:TextBox runat="server" ID="txtcomrate" Visible="true" ReadOnly="false" AutoPostBack="true" BackColor="LemonChiffon"
                        TabIndex="9" CssClass="tb6" Width="120px" Height="30px"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtcomrate"
                        ValidChars="0123456789.">
                    </cc1:FilteredTextBoxExtender>
                </div>
                <div class="col-md-1">
                    <asp:TextBox runat="server" ID="txtCPRate" Visible="true" ReadOnly="false" AutoPostBack="true" BackColor="LemonChiffon"
                        TabIndex="9" CssClass="tb6" Width="120px" Height="30px"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-1"></div>
                <div class="col-md-2">
                    <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Navy" Text="Crop Year:"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlcropyr" runat="server" CssClass="form-control" AutoPostBack="True" Font-Size="10pt">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblFyear" runat="server" Text="Financial Year" Font-Bold="True" Font-Size="12pt" ForeColor="Navy"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlFyear" runat="server" CssClass="form-control" AutoPostBack="True" Font-Size="10pt">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 30px">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button ID="btnGenerateBill" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnGenerateBill_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                </div>
            </div>
        </fieldset>
        <fieldset id="trRentBill" visible="false" runat="server" style="background-color: whitesmoke">
            <legend>NCCF Storage Bill Detail</legend>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvnccfStorageCharge" CssClass="table table-bordered table-hover datatable" runat="server" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderStyle-BackColor="WhiteSmoke" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" DataField="Deposit_Date" />
                                <asp:BoundField HeaderStyle-BackColor="WhiteSmoke" HeaderText="Opening Balance Bags" DataField="Opening_Balance" />
                                <asp:BoundField HeaderStyle-BackColor="WhiteSmoke" HeaderText="Receive Bags" DataField="Receive_Bags" />
                                <asp:BoundField HeaderStyle-BackColor="WhiteSmoke" HeaderText="Issue Bags" DataField="Issue_Bags" />
                                <asp:BoundField HeaderStyle-BackColor="WhiteSmoke" HeaderText="Closing Balance Bags" DataField="Closing_Balance" />
                                <asp:BoundField HeaderStyle-BackColor="WhiteSmoke" HeaderText="Reservation on Bags" DataField="Reserve_Bags" />
                                <asp:BoundField HeaderStyle-BackColor="WhiteSmoke" HeaderText="Chargable Bags" DataField="Chargable_Bags" />
                                <asp:BoundField HeaderStyle-BackColor="WhiteSmoke" HeaderText="Rate Per Bag(For 15 Day)" DataField="Per_Day_Rate" />
                                <asp:BoundField HeaderStyle-BackColor="WhiteSmoke" HeaderText="Amount" DataField="Charges" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 20px">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button ID="btnGenBill" runat="server" Text="Generate Bill" Visible="true"
                        CssClass="btn btn-info" OnClick="btnGenBill_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btncancel2" runat="server" Text="Cancel" Width="120px" Visible="true"
                        CssClass="btn btn-danger" OnClick="btncancel2_Click" />
                </div>
            </div>
        </fieldset>
        <fieldset id="divmsg" runat="server" visible="false" style="background-color: whitesmoke; margin-top:20px">
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-6">
                    <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" Font-Size="Large" runat="server"></asp:Label>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

