<%@ Page Title="" Language="C#" MasterPageFile="~/Special_PV/State/StateMaster_SP.master" AutoEventWireup="true" CodeFile="~/Special_PV/State/Special_PV_Inspection_By_HO_Officer.aspx.cs" Inherits="Special_PV_State_Special_PV_Inspection_By_HO_Officer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        window.history.forward();

        function noBack() { window.history.forward(); }
    </script>
    <script type="text/javascript">
        Cufon.replace('h1,h2,h3,h4,h5,#menu,#copy,.blog-date');
    </script>
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
            <legend>Verified By Ho Inspection Officer</legend>
            <div class="row">
                <div class="col-md-2">
                    <label>Region</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>District</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Branch</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </fieldset>
        <fieldset id="divGodown" runat="server" visible="false">
            <legend>Details</legend>
            <div class="row">
                <div class="col-md-2" style="margin-top: 8PX">
                    <label>Inspection Officer Name</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtOfficerName" Font-Bold="true" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 8PX">
                    <label>Mobile NO.</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtmobile" Font-Bold="true" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 8PX">
                    <label>Inspection/PV Date</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txt_InspDate" runat="server" class="text" type="text" CssClass="form-control"
                        onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txt_InspDate">
                    </cc1:CalendarExtender>
                </div>
            </div>
            <div class="row" style="margin-top: 20PX">
                <div class="table-responsive">
                    <asp:GridView runat="server" ID="grdGodown" ShowFooter="true"
                        AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Godown Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdnGodown_ID" Value='<%#Eval("Godown_ID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Depositor_Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDepositor_Name" Text='<%# Eval("Depositor_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CropYear">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCropYear" Text='<%# Eval("CropYear") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCommodity" Text='<%# Eval("Commodity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clossing Bags As per 31/12/2024" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblClossing_Bags_As_per_31_12_2024" runat="server" Text='<%# Eval("Clossing_Bags_As_per_31_12_2024") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Blance As Per PV By Branch" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblBlance_As_Per_PV_By_Branch" runat="server" Text='<%# Eval("Blance_As_Per_PV_By_Branch") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Diffirence Online and PV" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiffirenceOnlineandPV" runat="server" Text='<%# Eval("DiffirenceOnlineandPV") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rm Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblRm_Remark" runat="server" Text='<%# Eval("Rm_Remark") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jama Bags After 31/12/2024">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJama_Bags_31_12_2024" runat="server" CssClass="form-control" Text='<%# Eval("Jama_Bags_31_12_2024") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paid Bags After 31/12/2024">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPaid_Bags_31_12_2024" runat="server" Width="125" CssClass="form-control" Text='<%# Eval("Paid_Bags_31_12_2024") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No. of Bags As per PV At Inspection Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNo_of_Bags_As_perPV" runat="server" Width="125" CssClass="form-control" Text='<%# Eval("Paid_Bags_31_12_2024") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark By HO Inspection Officer">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemark_By_HO_Inspection_Officer" runat="server" Width="125" CssClass="form-control" Text='<%# Eval("Remark_By_HO_Inspection_Officer") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>Data Already Submited</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            <div class="row" style="margin-top: 20px">
                <div class="col-md-2"></div>
                <div class="col-md-1" style="margin-top: 15px">
                    <label>Remark</label>
                </div>
                <div class="col-md-5">
                    <asp:TextBox runat="server" ID="txthoremark" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="button button2" TabIndex="11"
                        Width="150px" Height="30px" Enabled="true" OnClick="btnSubmit_Click" />
                </div>
            </div>
            <%--   <div class="col-md-1" style="margin-top: 10px">
                    <asp:Button class="button button2" ID="btn_addnewoff" runat="server" Text="Submit"
                        TabIndex="11" Width="150px" Height="30px" OnClick="btn_addnewoff_Click"></asp:Button>
                </div>
            </div>--%>
        </fieldset>
    </div>
</asp:Content>

