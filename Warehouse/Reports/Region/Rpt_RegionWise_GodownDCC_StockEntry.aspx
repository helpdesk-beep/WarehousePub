<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Rpt_RegionWise_GodownDCC_StockEntry.aspx.cs" Inherits="Region_Rpt_RegionWise_GodownDCC_StockEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- Bootstrap core CSS -->
    <link href="../Inspections/Assets/css/bootstrap.min.css" type="text/css" />
    <link href="../assets/New/css/bootstrap-theme.css" rel="stylesheet" type="text/css" />

    <!-- font awesome -->
    <link href="../../Administration/assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../assets/New/css/custome.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="../../assets/New/css/bootstrap.min.css" />
  
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
        <%--<asp:ValidationSummary ID="vds" runat="server" ShowMessageBox="true" ValidationGroup="a" />--%>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>Report for DCC Entry at Region</legend>
            <div class="row">
                <div class="col-md-2" style="margin-top: 8px">
                    <asp:Label ID="lblCropYear" Font-Bold="true" runat="server" ForeColor="Navy">Crop Year:</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlCropYear" runat="server" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
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
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 8px">
                    <div class="form-group">
                        <asp:Label ID="lblDepositor" Font-Bold="true" runat="server" ForeColor="Navy">Depositor Name:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlDepositor" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 8px">
                    <div class="form-group">
                        <asp:Label ID="lblDistrict" runat="server" Font-Bold="true" ForeColor="Navy">District Name:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 15px">
                <div class="col-md-2" style="margin-top: 8px">
                    <div class="form-group">
                        <asp:Label ID="lblBranch" runat="server" Font-Bold="true" ForeColor="Navy">Branch Name:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlDepotList" runat="server" AutoPostBack="false"
                        CssClass="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>
            <div class="row" style="margin-top: 15px">
                <div style="width: 100%; background-repeat: no-repeat;">
                    <div style="position: relative;">
                        <p>
                            <strong style="color: red">* Weight in Qtl:-
                            </strong>
                            <br />
                        </p>
                    </div>
                </div>
                <div class="col-md-4" style="margin-top: 25px">
                    <h5></h5>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="a" Text="Search" CssClass="btn-success" Enabled="true" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </fieldset>
        <div class="row" id="grdentry" style="margin-top: 20px">
            <fieldset>
                <legend>शाखा प्रबंधक द्वारा दर्ज  की गई जानकारी </legend>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GV_EntryDone" CellPadding="5" OnRowCommand="GV_EntryDone_RowCommand" OnRowDataBound="GV_EntryDone_RowDataBound"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" autopostback="true" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SN" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("SN") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RegionName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY" Enabled="false" runat="server" Text='<%# Eval("RegionName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DistrictName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrictName" Enabled="false" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BranchName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranchName" Enabled="false" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GodownName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGodownName" Enabled="false" runat="server" Text='<%# Eval("GodownName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DepositorName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepositorName" Enabled="false" runat="server" Text='<%# Eval("DepositorName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CropYear">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCropYear" Enabled="false" runat="server" Text='<%# Eval("CropYear") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commodity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommodity" Enabled="false" runat="server" Text='<%# Eval("Commodity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total_Bags">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalNoOfBagsAvailable" Enabled="false" runat="server" Text='<%# Eval("Total_Bags") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total_Weight">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalQuantityAvailable" Enabled="false" runat="server" Text='<%# Eval("Total_Weight") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DCCStockInBag">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalDCCBagsAvailable" Enabled="false" runat="server" Text='<%# Eval("DCCStockInBag") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="DCCStockWeightInQtl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalDCCQuantity" Enabled="false" runat="server" Text='<%# Eval("DCCStockWeightInQtl") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="DCCStockWeightInQtl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalDCCQuantity" Enabled="false" runat="server" Text='<%# Eval("TotalWeight") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="UpgradableQtyInQtl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalUpgradableQty" Enabled="false" runat="server" Text='<%# Eval("Upgradable") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DumpingInQtl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalDumpingQty" Enabled="false" runat="server" Text='<%# Eval("Dumping") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    <div align="center">No records found.</div>
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Right" Font-Size="12pt" />
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

</asp:Content>
