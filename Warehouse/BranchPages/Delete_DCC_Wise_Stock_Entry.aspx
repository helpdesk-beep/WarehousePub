<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Delete_DCC_Wise_Stock_Entry.aspx.cs" Inherits="BranchPages_Delete_DCC_Wise_Stock_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- Bootstrap core CSS -->
    <%--  <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />--%>
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <%-- <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/custome.css" rel="stylesheet" type="text/css" />--%>
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
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
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
            border-radius: 6px;
            padding: 0
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>
    <script type="text/javascript">  
        function ConfirmOnDelete() {
            if (confirm("Are you sure want to Delete This ?") == true)
                return true;
            else
                return false;
        }
        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode;
            if ((charCode >= 48 && charCode <= 57)) {
                return true;
            }
            if (charCode == 46) { return true; }
            if (charCode == 8) { return true; }
            if (charCode == 9) { return true; }
            else { return false; }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="ddd" runat="server" />
    <asp:Panel ID="StoreGrid" runat="server">
        <asp:HiddenField ID="hdngdnid" runat="server" />
        <asp:HiddenField ID="Hiddendistid" runat="server" />
        <asp:HiddenField ID="Hiddenbranch" runat="server" />
        <asp:HiddenField ID="EnterHiddendistid" runat="server" />
        <fieldset>
            <legend>DCC Stock Detail</legend>
            <div class="Row" style="margin-top: 10px">
                <div class="col-md-2">
                    <label>Godown Type :</label>
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddlgodowntype" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlgodowntype_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Godown Name :</label>
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddlGodown" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
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
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Commodity Type</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlCommoditytype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCommoditytype_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="Row">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Commodity</label>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlcommodity" runat="server" CssClass="form-control" AutoPostBack="true" selectionmode="Multiple">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 25px">
                    <asp:Button runat="server" ID="btnsearch" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="Search" OnClick="btnsearch_Click" />
                </div>
            </div>
        </fieldset>
        <div class="row" id="grdentry" style="margin-top: 20px">
            <fieldset>
                <legend>शाखा प्रबंधक द्वारा दर्ज की गई जानकारी</legend>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GV_EntryDone" CellPadding="5" OnRowCommand="GV_EntryDone_RowCommand"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" autopostback="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnGodown_ID" runat="server" Value='<%# Bind("Godown_ID") %>' />
                                            <asp:HiddenField ID="hdnDepositor_ID" runat="server" Value='<%# Bind("Depositor_ID") %>' />
                                            <asp:HiddenField ID="hdnCommodity_Id" runat="server" Value='<%# Bind("Commodity_Id") %>' />
                                            <asp:HiddenField ID="hdnCrop_Year" runat="server" Value='<%# Bind("Crop_Year") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GodownID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGodown_ID" Enabled="false" runat="server" Text='<%# Eval("Godown_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Godown Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGodown_Name" Enabled="false" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Depositor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepositor_Name" Enabled="false" runat="server" Text='<%# Eval("Depositor_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crop Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCrop_Year" Enabled="false" runat="server" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commodity Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommodity_Name" Enabled="false" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DCC Stock Bags">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDCC_Stock_Bags" Enabled="false" runat="server" Text='<%# Eval("DCC_Stock_Bags") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dcc Stock Weight">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDcc_Stock_Weight" Enabled="false" runat="server" Text='<%# Eval("Dcc_Stock_Weight") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Total DCC Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotaldccquantity" Enabled="false" runat="server" Text='<%# Eval("totaldccquantity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Upgradable Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpgradableQty" Enabled="false" runat="server" Text='<%# Eval("UpgradableQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Dumping Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDumpingQty" Enabled="false" runat="server" Text='<%# Eval("DumpingQty") %>'></asp:Label>
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
    </asp:Panel>
</asp:Content>

