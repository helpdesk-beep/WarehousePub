<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/UpdateDepositerNameinWHR.aspx.cs" Inherits="StatePages_UpdateDepositerNameinWHR" Title="Update WHR Depositer Name" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
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
    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
    </script>
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
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #D69758;
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
    <style type="text/css">
        #popupwin {
            position: fixed;
            top: 0;
            left: 0;
            width: 90%;
            height: 90%;
            background-color: #000;
            filter: alpha(opacity=65);
            -moz-opacity: 0.7;
            display: none;
            opacity: 0.7;
            z-index: 100;
        }

        .pop a {
            text-decoration: none;
        }

        .popup {
            width: 100%;
            height: 98%;
            margin: 0 auto;
            position: fixed;
            z-index: 101;
            padding-left: 90px;
        }

        .pop {
            /*min-width: 900px;*/
            width: 80%;
            min-height: 150px;
            margin: 0px auto;
            background: #FFFFFF;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 5px 10px #000;
            /*margin-top:200px;*/
        }

            .pop p {
                color: #555555;
                text-align: justify;
                font-size: medium;
            }

                .pop p a {
                    color: #d91900;
                }

            .pop .x {
                float: right;
                height: 35px;
                /*left: 22px;*/
                position: relative;
                /*top: -20px;*/
                width: 35px;
            }
    </style>
    <script type="text/javascript">
        $(function () {
            $("[id*=DropDownList1]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlBranch]").select2();
        });
    </script>
    <script type="text/javascript">
                 $(function () {
                     $("[id*=ddlcropyear]").select2();
                 });
    </script>
    <fieldset style="width: 100%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
        <legend>Update WHR Depositer Name</legend>
        <%--<div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-1">
                <label>District :</label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                    CssClass="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-1">
                <label>Branch :</label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlBranch" runat="server"
                    CssClass="form-control" AutoPostBack="false">
                </asp:DropDownList>
            </div>
            <div class="col-md-1">
                <label>CropYear :</label>
            </div>
            <div class="col-md-2" style="align-content: initial">
                <asp:DropDownList ID="ddlcropyear" runat="server"
                    CssClass="form-control" AutoPostBack="false">
                    <asp:ListItem Value="" Text="---Select Year---"></asp:ListItem>
                     <asp:ListItem Value="2026-27" Text="2026-27"></asp:ListItem>
                    <asp:ListItem Value="2025-26" Text="2025-26"></asp:ListItem>
                     <asp:ListItem Value="2024-25" Text="2024-25"></asp:ListItem>
                    <asp:ListItem Value="2023-24" Text="2023-24"></asp:ListItem>
                     <asp:ListItem Value="2022-23" Text="2022-23"></asp:ListItem>
                    <asp:ListItem Value="2021-22" Text="2021-22"></asp:ListItem>
                    <asp:ListItem Value="2020-21" Text="2020-21"></asp:ListItem>
                     <asp:ListItem Value="2019-20" Text="2019-20"></asp:ListItem>
                    <asp:ListItem Value="2018-19" Text="2018-19"></asp:ListItem>
                    <asp:ListItem Value="2017-18" Text="2017-18"></asp:ListItem>
                    <asp:ListItem Value="2016-17" Text="2016-17"></asp:ListItem>
                    <asp:ListItem Value="2015-16" Text="2015-16"></asp:ListItem>
                    <asp:ListItem Value="2014-15" Text="2014-15"></asp:ListItem>
                   <asp:ListItem Value="2013-14" Text="2013-14"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>--%>
        <div class="row" style="margin-top: 10px">
            <div class="col-md-3"></div>
            <div class="col-md-1">
                <label>WHR No. :</label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txttwhrno" runat="server" AutoPostBack="false" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <asp:Button ID="Button1" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                    CssClass="BTNBLUE" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </fieldset>
    <fieldset>
        <legend>Details</legend>
        <div class="table-responsive">
            <asp:GridView ID="Depositor_Gridview" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False"
                CssClass="table table-bordered table-hover datatable" OnRowDataBound="Depositor_Gridview_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-Font-Size="Medium">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                            <asp:HiddenField ID="hdngodownid" runat="server" Value='<%# Eval("Godown_ID") %>' />
                            <asp:HiddenField ID="hdndepositerid" runat="server" Value='<%# Eval("DepositorID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="District_Name" HeaderText="District Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Medium" />
                    <asp:BoundField DataField="DepotName" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="Medium" />
                    <asp:TemplateField HeaderText="Godown Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Whr_No">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblWhr_No" Text='<%# Eval("Whr_No")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Crop Year">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCropYear" Text='<%# Eval("CropYear")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Depositor Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDepositor_Name" Text='<%# Eval("Depositor_Name")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bags">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Text='<%# Eval("TotalBags_Received")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblGodown_Capacity" Text='<%# Eval("Total_Qty_Received")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Print Status">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPrintStatus" Text='<%# Eval("PrintStatus")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WHR Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblMktvalue" runat="server" Text='<%# Eval("MktValue_of_Commodity")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Update">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBtnEdit" runat="server" CssClass="btn btn-info btn-success" Text="Update"
                                OnClick="Display"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="True" ForeColor="White" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>
        </div>
    </fieldset>
    <asp:Panel ID="pnllogin" class="popup" runat="server">
        <div class="pop" style="background-color: white; min-height: 300PX; max-height: 500px; width: 1500px; border: #008CBA; border-style: solid; border-width: 10px;">
            <%-- <div class="col-sm-12 col-md-12 col-xs-12">--%>
            <div id="divNewInsp" runat="server" visible="true" style="width: 100%;">
                <div class="row" style="margin-top: 30px">
                    <div class="col-md-4"></div>
                    <div class="col-md-2" style="margin-top: 6px">
                        <label id="Label2" runat="server">Godown Name :</label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblgodownname" runat="server" BorderStyle="None" Font-Bold="true" Font-Size="Medium" BackColor="Transparent"></asp:Label>
                        <%--<label ID="lblgodownname" runat="server"></label>--%>
                    </div>
                </div>
                <div class="row" style="margin-top: 25px">
                    <div class="col-md-2" style="margin-top: 6px">
                        <label id="Label1" runat="server">Godown ID :</label>
                    </div>
                    <div class="col-md-2" style="align-content: start">
                        <asp:TextBox ID="txtGdwnID" runat="server" ReadOnly="true"
                            CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2" style="margin-top: 6px">
                        <label id="Label3" runat="server">Depositor Name in WHR</label>
                    </div>
                    <div class="col-md-2" style="align-content: start">
                        <asp:DropDownList ID="ddlDepositorfill" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="false">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="margin-top: 6px">
                        <label id="Label4" runat="server">Depositor Name</label>
                    </div>
                    <div class="col-md-2" style="align-content: start">
                        <asp:DropDownList ID="ddlDepositor" runat="server" CssClass="form-control" AutoPostBack="false">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px">
                    <div class="col-md-2" style="margin-top: 6px">
                        <label id="Label5" runat="server">WHR No.</label>
                    </div>
                    <div class="col-md-2" style="align-content: start">
                        <asp:TextBox ID="txtwhrno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2" style="margin-top: 6px">
                        <label id="Label6" runat="server">Check</label>
                    </div>
                    <div class="col-md-2" style="align-content: start">
                        <asp:CheckBox ID="chkbox" runat="server" AutoPostBack="true" OnCheckedChanged="chkbox_CheckedChanged" />
                    </div>
                    <div class="col-md-2" style="margin-top: 6px">
                        <label id="Label7" runat="server">WHR Rate</label>
                    </div>
                    <div class="col-md-2" style="align-content: start">
                        <asp:TextBox ID="txtWHRRate" runat="server" Enabled="false"
                            CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px">
                    <div class="col-md-4"></div>
                    <div class="col-md-2">
                        <asp:Button class="button button1" ID="btnAddCompany" Style="width: 100px" runat="server"
                            Text="Update" Height="29px" OnClick="btnAddCompany_Click1"></asp:Button>
                    </div>
                    <div class="col-md-2">
                        <asp:Button class="button button2" ID="btnGenerateBill" Style="width: 100px" runat="server" Text="Close" Height="29px"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <img alt="New" src="images/new6.gif" id="new" runat="server" />
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin">
    </asp:ModalPopupExtender>
</asp:Content>

