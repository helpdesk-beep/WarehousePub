<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master"
    AutoEventWireup="true" CodeFile="UpdateCropYear.aspx.cs"
    Inherits="StatePages_UpdateCropYear" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- JQuery -->
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>

    <!-- Select2 -->
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script src="../assets/New/js/select2.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=ddlDistrict.ClientID%>").select2();
            $("#<%=ddlbranch.ClientID%>").select2();
            $("#<%=ddlComodity.ClientID%>").select2();
            $("#<%=ddlGodown.ClientID%>").select2();
            $("#<%=ddlCropYear.ClientID%>").select2();
            $("#<%=ddlChangeCropYear.ClientID%>").select2();

        });

    </script>

    <style type="text/css">

        body {
            margin: 0px;
            padding: 0px;
            font-family: Arial, Helvetica, sans-serif;
            background: #f3f6fb;
        }

        .mainbox {
            width: 99%;
            margin: 10px auto;
            border: 1px solid #b7c9dc;
            background: #ffffff;
        }

        .pageheader {
            background: #2f6fa5;
            color: #ffffff;
            padding: 10px;
            font-size: 20px;
            font-weight: bold;
            text-align: center;
            border-bottom: 3px solid #1d4f91;
            letter-spacing: 0.5px;
        }

        .filterbox {
            padding: 12px;
            background: #f7fbff;
            border-bottom: 1px solid #d5e3f0;
        }

        .filtertable {
            width: 100%;
        }

        .filtertable td {
            padding: 7px;
            vertical-align: middle;
        }

        .label {
            font-size: 12px;
            font-weight: bold;
            color: #1d3557;
            white-space: nowrap;
        }

        .ddl {
            width: 220px;
            height: 30px;
            border: 1px solid #9db5d3;
            background: #ffffff;
            border-radius: 2px;
            font-size: 12px;
            padding-left: 3px;
        }

        .select2-container {
            width: 220px !important;
        }

        .select2-container .select2-selection--single {
            height: 30px !important;
            border: 1px solid #9db5d3 !important;
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered {
            line-height: 28px !important;
            font-size: 12px !important;
        }

        .select2-container--default .select2-selection--single .select2-selection__arrow {
            height: 28px !important;
        }

        .updatepanel {
            margin: 10px;
            padding: 10px;
            background: #eef5fc;
            border: 1px solid #c6d7e8;
        }

        .gridsection {
            padding: 10px;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
            border: 1px solid #b7c9dc;
        }

        .gridview th {
            background: #719cb6;
            color: #ffffff;
            padding: 8px;
            text-align: center;
            font-size: 12px;
            border: 1px solid #5d839b;
        }

        .gridview td {
            padding: 7px;
            border: 1px solid #d5dfe9;
            font-size: 12px;
            color: #222222;
        }

        .gridview tr:nth-child(even) {
            background: #f8fbfd;
        }

        .gridview tr:hover {
            background: #e9f3ff;
        }

        .btnsave {
            background: #2f6fa5;
            color: #ffffff;
            border: 1px solid #1d4f91;
            padding: 8px 35px;
            font-size: 13px;
            font-weight: bold;
            cursor: pointer;
            border-radius: 2px;
        }

        .btnsave:hover {
            background: #1d4f91;
        }

        .footerbox {
            text-align: center;
            padding: 15px;
        }

        .headingline {
            background: #dceaf7;
            color: #1d4f91;
            font-weight: bold;
            padding: 8px;
            border: 1px solid #bfd2e5;
            margin-bottom: 10px;
            font-size: 13px;
        }

    </style>

    <div class="mainbox">

        <!-- Heading -->
        <div class="pageheader">
            Update Crop Year
        </div>

        <!-- Filter Panel -->
        <div class="filterbox">

            <div class="headingline">
                Search Details
            </div>

            <table class="filtertable">

                <tr>

                    <td class="label">
                        District Name
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlDistrict"
                            runat="server"
                            CssClass="ddl"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">

                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                    </td>

                    <td class="label">
                        Branch Name
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlbranch"
                            runat="server"
                            CssClass="ddl"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">

                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                    </td>

                    <td class="label">
                        Commodity Name
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlComodity"
                            runat="server"
                            CssClass="ddl"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">

                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                    </td>

                </tr>

                <tr>

                    <td class="label">
                        Godown Name
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlGodown"
                            runat="server"
                            CssClass="ddl"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">

                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                    </td>

                    <td class="label">
                        Crop Year
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlCropYear"
                            runat="server"
                            CssClass="ddl"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">

                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                    </td>

                </tr>

            </table>

        </div>

        <!-- Grid Panel -->
        <table id="grid" runat="server" visible="false" width="100%">

            <tr>

                <td>

                    <div class="updatepanel">

                        <table>

                            <tr>

                                <td class="label">
                                    Crop Year for Update
                                </td>

                                <td>
                                    <asp:DropDownList ID="ddlChangeCropYear"
                                        runat="server"
                                        CssClass="ddl">

                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Value="2026-27">2026-27</asp:ListItem>
                                        <asp:ListItem Value="2025-26">2025-26</asp:ListItem>
                                        <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                                        <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                                        <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                                        <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                                        <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                                        <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                                        <asp:ListItem Value="2018-19">2018-19</asp:ListItem>
                                        <asp:ListItem Value="2017-18">2017-18</asp:ListItem>
                                        <asp:ListItem Value="2016-17">2016-17</asp:ListItem>
                                        <asp:ListItem Value="2015-16">2015-16</asp:ListItem>
                                        <asp:ListItem Value="2014-15">2014-15</asp:ListItem>

                                    </asp:DropDownList>
                                </td>

                            </tr>

                        </table>

                    </div>

                </td>

            </tr>

            <tr>

                <td>

                    <div class="gridsection">

                        <asp:GridView ID="godown_GridView"
                            runat="server"
                            CssClass="gridview"
                            DataKeyNames="Depositor_WHR_Id"
                            AutoGenerateColumns="False"
                            Width="100%"
                            AllowPaging="True"
                            AllowSorting="True"
                            PageSize="100"
                            Font-Size="9pt"
                            GridLines="Both"
                            OnRowDataBound="godown_GridView_RowDataBound"
                            OnPageIndexChanging="godown_GridView_PageIndexChanging">

                            <Columns>

                                <asp:TemplateField HeaderText="Select">

                                    <ItemTemplate>

                                        <div style="text-align: center;">
                                            <asp:CheckBox ID="chkbtn" runat="server" />
                                        </div>

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="S.No">

                                    <ItemTemplate>

                                        <%# Container.DataItemIndex + 1 %>

                                        <asp:HiddenField ID="hdnCropYear"
                                            runat="server"
                                            Value='<%# Eval("CropYear") %>' />

                                        <asp:HiddenField ID="hdnDepositor_WHR_Id"
                                            runat="server"
                                            Value='<%# Eval("Depositor_WHR_Id") %>' />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="Depositor WHR Id" />
                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" />
                                <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name" />
                                <asp:BoundField DataField="Date_of_Deposit" HeaderText="Date of Deposit" />
                                <asp:BoundField DataField="TotalBags_Received" HeaderText="Total Bags Received" />
                                <asp:BoundField DataField="Total_Qty_Received" HeaderText="Total Qty Received" />
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                <asp:BoundField DataField="CropYear" HeaderText="Crop Year" />

                            </Columns>

                            <HeaderStyle BackColor="#719cb6"
                                ForeColor="White"
                                Font-Bold="True"
                                HorizontalAlign="Center" />

                            <PagerStyle BackColor="#eef3f7"
                                ForeColor="#000000"
                                HorizontalAlign="Center" />

                            <AlternatingRowStyle BackColor="#f8fbfd" />

                        </asp:GridView>

                    </div>

                </td>

            </tr>

            <tr>

                <td>

                    <div class="footerbox">

                        <asp:Button ID="btnSave"
                            runat="server"
                            Text="Save"
                            CssClass="btnsave"
                            OnClick="Save" />

                    </div>

                </td>

            </tr>

        </table>

    </div>

</asp:Content>