<%@ Page Title="" Language="C#" MasterPageFile="~/Special_PV/State/StateMaster_SP.master" AutoEventWireup="true" CodeFile="~/Special_PV/State/Add_Inspection_Officer_For_SP.aspx.cs" Inherits="Special_PV_State_Add_Inspection_Officer_For_SP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
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

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend>Add Inspection Officer</legend>
            <div class="row">
                <div class="col-md-2" style="margin-top: 8px">
                    <label>Inspection Officer Name :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtofficername" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 8px">
                    <label>Personal Mobile No :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtMob" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                </div>
                <div class="col-md-2" style="margin-top: 8px">
                    <label>Designation :</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddl_Desig" runat="server" AutoPostBack="false" Font-Bold="true" ForeColor="Navy" CssClass="form-control">
                        <asp:ListItem Selected="True">--Select--</asp:ListItem>
                        <asp:ListItem Value="AGM">AGM</asp:ListItem>
                        <asp:ListItem Value="DGM">DGM</asp:ListItem>
                        <asp:ListItem Value="AQC">AQC</asp:ListItem>
                        <asp:ListItem Value="AQC(C)">AQC(C)</asp:ListItem>
                        <asp:ListItem Value="QC">QC</asp:ListItem>
                        <asp:ListItem Value="GM QC">GMQC</asp:ListItem>
                        <asp:ListItem Value="Manager(QC)">Manager(QC)</asp:ListItem>
                        <asp:ListItem Value="Manager(General)">Manager(General)</asp:ListItem>
                        <asp:ListItem Value="Assistant accountant">Assistant accountant</asp:ListItem>
                        <asp:ListItem Value="Senior assistant">Senior assistant</asp:ListItem>
                        <asp:ListItem Value="Junior Assistant">Junior Assistant</asp:ListItem>
                        <asp:ListItem Value="Stenographer">Stenographer</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top:20px">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button class="button button2" ID="btn_addnewoff" runat="server" Text="Submit"
                        TabIndex="11" Width="150px" Height="30px"  OnClick="btn_addnewoff_Click"></asp:Button>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

