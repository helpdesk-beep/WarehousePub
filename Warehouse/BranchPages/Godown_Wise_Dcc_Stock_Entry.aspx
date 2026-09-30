<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Godown_Wise_Dcc_Stock_Entry.aspx.cs" Inherits="BranchPages_Godown_Wise_Dcc_Stock_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />

    <link href="../assets/css/style.css" rel="stylesheet" />

    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
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
    </style>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 32 && (charCode < 46 || charCode == 47 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function lettersOnly() {
            var charCode = event.keyCode;
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true;
            else
                return false;
        }

        function hindiOnly() {
            var charCode = event.keyCode;
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == (u + 0020))
                return false;
            else
                return true;
        }
    </script>

    <!-- Bootstrap -->
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
    <!-- Bootstrap -->
    <!-- Bootstrap DatePicker -->
    <link rel="stylesheet" href="../assets/New/css/bootstrap-datepicker.css" type="text/css" />
    <script src="../assets/New/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <!-- Bootstrap DatePicker -->
    <script type="text/javascript">
        $(function () {
            $('[id*=txtDate]').datepicker({
                format: "dd/mm/yyyy",
                language: "tr"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="row">
            <div class="col-12">
                <asp:Label ID="lblmsg" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </div>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>Godown Wise DCC Entry</legend>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Godown Type</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlGodowntype" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGodowntype_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Godown Name :</label>
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddlGodown" AutoPostBack="true" runat="server">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Crop Year</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlcropyear" runat="server" CssClass="form-control" AutoPostBack="true" selectionmode="Multiple">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Depositor Type</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlDepositorType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepositorType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Depositor Name</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlDepositor" runat="server" CssClass="form-control" AutoPostBack="true">
                            <asp:ListItem Text="Select" Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Commodity Type</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlCommoditytype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCommoditytype_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 5px">
                <div class="col-md-2">
                    <label>Commodity</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlcommodity" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Total Bags</label>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txttotalBags"  CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Total Weight</label>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txttotalweight"  CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
            </div>
        </fieldset>
        <fieldset runat="server" id="fielddcc" visible="false">
            <legend>DCC Stock</legend>
            <div class="row" style="margin-top: 10px">
                <div style="width: 100%; background-repeat: no-repeat;">
                    <div style="position: relative;">
                        <div class="col-md-4" style="margin-top: 5px">
                            <label><strong style="color: red; font-size: medium;">DCC  के लिये प्रस्तावित Stock (Quantity In Quintal)</strong></label>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddldccStock" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldccStock_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row" runat="server" id="divdccstock" visible="false">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>DCC Stock Bags</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtdccstock" onkeypress="return isNumberKey(this, event);" oninput="validate(this)" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>DCC Stock Weight</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtweight" onkeypress="return isNumberKey(this, event);" oninput="validate(this)" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 10px" runat="server" id="district" visible="false">
                <div style="width: 100%; background-repeat: no-repeat;">
                    <div style="position: relative;">
                        <div class="col-md-4" style="margin-top: 5px">
                            <label><strong style="color: red; font-size: medium;">जिला प्रभारी को प्रस्तुत किया गया है</strong></label>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddldistrictmanager" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldistrictmanager_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" runat="server" id="divdate" visible="false">
                <div class="col-md-4"></div>
                <div class="col-md-2">
                    <label>Distribution Date</label>
                    <div class="form-group">
                        <asp:TextBox ID="txtDate" runat="server" placeholder="dd/mm/yyyy"
                            CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                            data-date-format="dd/mm/yyyy"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>File Upload</label>
                    <div class="form-group">
                        <asp:FileUpload ID="IdFileUpload" onchange="previewUserImage()" Width="200" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 10px" runat="server" id="divsamiti" visible="false">
                <div class="col-md-4" style="margin-top: 5px">
                    <label><strong style="color: red; font-size: medium;">समिति द्वारा भौतिक सत्यापन किया गया</strong></label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlphysicalverification" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlphysicalverification_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row" runat="server" id="divdisi" visible="false">
                <div class="col-md-2">
                    <label>Decision Date</label>
                    <div class="form-group">
                        <asp:TextBox ID="txtdecision" runat="server" placeholder="dd/mm/yyyy"
                            CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                            data-date-format="dd/mm/yyyy"></asp:TextBox>
                    </div>
                </div>
                <fieldset>
                    <legend>Committee Decision</legend>
                    <div class="row">
                        <div class="col-md-2">
                            <label>DCC Quantity</label>
                            <asp:CheckBox Style="margin-top: 5px" ID="chkDCC_CommiteeDecision" runat="server" AutoPostBack="true" OnCheckedChanged="chkDCC_CommiteeDecision_CheckedChanged"></asp:CheckBox>
                        </div>
                        <div class="col-md-2" runat="server" id="totaldccquantity" visible="false">
                            <label>Total DCC Quantity (In Qntl)</label>
                            <asp:TextBox runat="server" ID="txtDCC" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Upgradable</label>
                            <asp:CheckBox ID="chkUpgradableCommiteeDecision" runat="server" AutoPostBack="true" OnCheckedChanged="chkUpgradableCommiteeDecision_CheckedChanged"></asp:CheckBox>
                        </div>
                        <div class="col-md-2" runat="server" id="totalUpgradable" visible="false">
                            <label>Total Upgradable Quantity (In Qntl)</label>
                            <asp:TextBox runat="server" ID="txtUpgradableQty" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Dumping</label>
                            <asp:CheckBox ID="chkDumping" runat="server" AutoPostBack="true" OnCheckedChanged="chkDumping_CheckedChanged"></asp:CheckBox>
                        </div>
                        <div class="col-md-2" runat="server" id="totalDumping" visible="false">
                            <label>Total Dumping Quantity (In Qntl)</label>
                            <asp:TextBox runat="server" ID="txtDumpingQty" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="row" style="margin-top=10px" runat="server" id="divdccTenderStutes" visible="false">
                <div class="col-md-2">
                    <label>DCC Tender Status</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddltenderstutes" runat="server" CssClass="form-control" AutoPostBack="true">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Tender Date</label>
                    <div class="form-group">
                        <asp:TextBox ID="txtTenderDate" runat="server" placeholder="dd/mm/yyyy"
                            CssClass="form-control dateAdd" data-date-end-date="0d" autocomplete="off" data-provide="datepicker"
                            data-date-format="dd/mm/yyyy"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>File Upload</label>
                    <div class="form-group">
                        <asp:FileUpload ID="TenderFileUpload" onchange="previewUserImage()" Width="200" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Stock Delivered</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlStockDelivered" runat="server" CssClass="form-control" AutoPostBack="true">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </fieldset>
        <div class="row" style="margin-top: 10px" runat="server" visible="false" id="opennull">
            <div style="width: 100%; background-repeat: no-repeat;">
                <div style="position: relative;">
                    <div class="col-md-2"></div>
                    <div class="col-md-8" style="margin-top: 5px">
                        <label><strong style="color: red; font-size: medium;">आपके गोदाम में इस क्रॉप ईयर और इस जमाकर्ता और इस कमोडिटी का कोई स्कंध DCC के लिये उपलब्ध नहीं है!</strong></label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-2">
                <asp:Button runat="server" ID="btnsave" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="SUBMIT" OnClick="btnsave_Click" />
            </div>
        </div>

    </div>
</asp:Content>

