<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Reports/Region/Rpt_Region_Stack_Wise_Moisture_As_Per_System.aspx.cs" Inherits="Reports_Region_Rpt_Region_Stack_Wise_Fumigation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../assets/New/css/style.css" rel="stylesheet" />
    <!-- Bootstrap core CSS -->
    <%--  <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />--%>
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <%-- <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/custome.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../../CSS/style.css" rel="stylesheet" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>Stack Wise Moisture as per online Report  </legend>
            <div class="row">

                <div class="col-md-2">
                    <div class="form-group ">
                        <label style="margin-top: 10px">District Name</label>
                        <asp:DropDownList CssClass="form-control" ID="ddldistrict" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group ">
                        <label style="margin-top: 10px">Branch Name</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlbranch" runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </fieldset>
        <fieldset>
            <%--<legend>Bills Details</legend>--%>
            <%-- <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-2" style="text-align: center; margin-top: 35px">
                    <asp:Button runat="server" CssClass="btn button2 btn-sm" Text="Exporttoexcel" ID="btnExport" OnClick="btnExport_Click" />
                </div>
            </div>--%>
            <div class="row" style="align-content: center" runat="server" id="DivRegion" visible="false">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="GrdRegion" ShowFooter="true"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                                <asp:BoundField DataField="DistrictName" HeaderText="District" />
                                <asp:BoundField DataField="DepotName" HeaderText="Branch" />
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                                <asp:TemplateField HeaderText="Stack Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("Stack_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WHR बनाते समय ली गई औसत नमी">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Moisture") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

