<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/Update_Financial_Year.aspx.cs" Inherits="StatePages_Update_Financial_Year" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Update Acceptance Note</title>
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset style="width: 90%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
            <legend>Update Bill Details</legend>
            <div class="row">
                <div class="col-md-2" style="margin-top: 8px">
                    <label style="font-size: 14px">Depositor Name</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlDepositor" runat="server"
                        CssClass="form-control" AutoPostBack="false">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="129">MPSCSC</asp:ListItem>
                        <asp:ListItem Value="10535">NAFED</asp:ListItem>
                        <asp:ListItem Value="15478">NCCF</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 8px">
                    <label style="font-size: 14px">Bill Number :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtacceptanceno" runat="server" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-1">
                    <asp:Button runat="server" ID="btnsearch" CssClass="btn btn-info btn-block" ValidationGroup="a" Text="SEARCH" OnClick="btnsearch_Click" />
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Details</legend>
            <div class="table-responsive">
                <asp:GridView runat="server" ID="GridView1"
                    CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDistrict_Name" Width="100%" Text='<%# Eval("District_Name")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch Name" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDepotName" Width="100%" Text='<%# Eval("DepotName")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Godown ID" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Number" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBill_Number" Width="100%" Text='<%# Eval("Bill_Number")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Type" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBillType" Width="100%" Text='<%# Eval("Bill_Type")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Commodity" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAcceptanceQty" Width="100%" Text='<%# Eval("Commodity_Name")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Crop Year" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCropYear" Width="100%" Text='<%# Eval("Crop_Year")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Financial Year" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFinancial_Year" Width="100%" Text='<%# Eval("Financial_Year")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Month" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRecd_Bags" Width="100%" Text='<%# Eval("Month_Name")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <%-- <asp:TemplateField HeaderText="TC Number" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTC_Number" Width="100%" Text='<%# Eval("TC_Number")%>'></asp:Label>
                            </ItemTemplate>--%>
                        <%-- <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Update Bill Details" HeaderStyle-BackColor="LightBlue">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Update" OnClick="Display"></asp:LinkButton>
                            </ItemTemplate>
                            <ControlStyle Font-Bold="True" ForeColor="Red" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </fieldset>
        <asp:Panel ID="pnllogin" class="popup" runat="server">
            <div class="pop" style="background-color: white; margin-top: 180px; min-height: 300PX; max-height: 500px; width: 1000px; border: #008CBA; border-style: solid; border-width: 10px;">
                <div id="divNewInsp" runat="server" visible="false" style="width: 100%;">
                    <fieldset style="width: 90%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
                        <div class="row" style="margin-top: 20px">
                            <div class="col-md-2" style="margin-top: 8px">
                                <label>Godown Name :</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="lblgodownname" runat="server" ReadOnly="true" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 20px">
                            <div class="col-md-2" style="margin-top: 8px">
                                <label>Bill No:</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtbill" runat="server" ReadOnly="true" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 20px">

                            <div class="col-md-2" style="margin-top: 8px">
                                <label>Financial Year:</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtfinyear" runat="server" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2" style="margin-top: 8px">
                                <label>Crop Year</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtcrpyear" runat="server" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class='row' style="margin-top: 20px">
                            <div class="col-md-4"></div>
                            <div class="col-md-1">
                                <asp:Button class="btn btn-success" ID="btnUpdate" runat="server"
                                    Text="Update" OnClick="btnUpdate_Click"></asp:Button>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1">
                                <asp:Button class="btn btn-danger" ID="btnClose" runat="server" Text="Close"></asp:Button>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
            <img alt="" src="images" id="new" runat="server" />
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin">
        </asp:ModalPopupExtender>
    </div>
</asp:Content>

