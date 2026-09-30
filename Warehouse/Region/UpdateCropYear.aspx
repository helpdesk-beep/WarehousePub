<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master"
    AutoEventWireup="true" CodeFile="~/Region/UpdateCropYear.aspx.cs"
    Inherits="Region_UpdateCropYear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <!-- JQuery -->
    <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>

    <!-- Select2 -->
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script src="../assets/New/js/select2.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            bindSelect2();

            // UpdatePanel/PostBack ke baad bhi Select2 chale
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                bindSelect2();
            });

        });

        function bindSelect2() {

            $("#<%=ddlDistrict.ClientID%>").select2();
            $("#<%=ddlbranch.ClientID%>").select2();
            $("#<%=ddlComodity.ClientID%>").select2();
            $("#<%=ddlGodown.ClientID%>").select2();
            $("#<%=ddlCropYear.ClientID%>").select2();
            $("#<%=ddlChangeCropYear.ClientID%>").select2();

        }

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
            box-shadow: 0px 0px 6px rgba(0,0,0,0.10);
        }

        .pageheader {
            background: #2f6fa5;
            color: #ffffff;
            padding: 12px;
            font-size: 22px;
            font-weight: bold;
            text-align: center;
            border-bottom: 3px solid #1d4f91;
            letter-spacing: 0.5px;
        }

        .filterbox {
            padding: 15px;
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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="mainbox">

        <div class="pageheader">
            Update Crop Year
        </div>

        <div class="filterbox">

            <table class="filtertable">

                <tr>

                    <td class="label">
                        <asp:Label ID="lbldist" runat="server" Text="District Name"></asp:Label>
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlDistrict" runat="server"
                            CssClass="ddl"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">

                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                    </td>

                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Branch Name"></asp:Label>
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlbranch" runat="server"
                            CssClass="ddl"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">

                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                    </td>

                    <td class="label">
                        <asp:Label ID="Label4" runat="server" Text="Commodity Name"></asp:Label>
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlComodity" runat="server"
                            CssClass="ddl"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">

                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                    </td>

                </tr>

                <tr>

                    <td class="label">
                        <asp:Label ID="Label2" runat="server" Text="Godown Name"></asp:Label>
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlGodown" runat="server"
                            CssClass="ddl"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">

                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                    </td>

                    <td class="label">
                        <asp:Label ID="Label3" runat="server" Text="Crop Year"></asp:Label>
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlCropYear" runat="server"
                            CssClass="ddl"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">

                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>

                        </asp:DropDownList>
                    </td>

                </tr>

            </table>

        </div>

        <div class="updatepanel">

            <asp:Label ID="lblbranchid" runat="server"></asp:Label>

        </div>

        <table id="grid" runat="server" visible="false" width="100%">

            <tr>
                <td>

                    <div class="headingline">

                        Crop Year for Update :

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

                    </div>

                </td>
            </tr>

            <tr>

                <td class="gridsection">

                    <asp:GridView ID="godown_GridView"
                        runat="server"
                        CssClass="gridview"
                        DataKeyNames="Depositor_WHR_Id"
                        AutoGenerateColumns="False"
                        CellPadding="2"
                        Width="100%"
                        AllowPaging="True"
                        AllowSorting="True"
                        PageSize="100"
                        Font-Size="9pt"
                        OnRowDataBound="godown_GridView_RowDataBound"
                        OnPageIndexChanging="godown_GridView_PageIndexChanging">

                        <Columns>

                            <asp:TemplateField HeaderText="">

                                <HeaderTemplate>
                                    चयन करें
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:CheckBox ID="chkbtn" runat="server" />
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="S.N.">

                                <ItemTemplate>

                                    <%#Container.DataItemIndex+1%>

                                    <asp:HiddenField ID="hdnCropYear"
                                        runat="server"
                                        Value='<%# Eval("CropYear") %>' />

                                    <asp:HiddenField ID="hdnDepositor_WHR_Id"
                                        runat="server"
                                        Value='<%# Eval("Depositor_WHR_Id") %>' />

                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Left" Width="20px" />

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

                        <FooterStyle BackColor="#719cb6"
                            ForeColor="White"
                            Font-Bold="True"
                            HorizontalAlign="Center" />

                        <PagerStyle BackColor="#F7F7DE"
                            ForeColor="Black"
                            HorizontalAlign="Center" />

                        <SelectedRowStyle BackColor="#CE5D5A"
                            Font-Bold="True"
                            ForeColor="White" />

                        <HeaderStyle BackColor="#719cb6"
                            Font-Bold="True"
                            ForeColor="White"
                            HorizontalAlign="center"
                            Height="20px"
                            Font-Size="10pt" />

                        <AlternatingRowStyle BackColor="#eeeeee" />

                    </asp:GridView>

                </td>

            </tr>

            <tr>

                <td class="footerbox">

                    <asp:Button ID="btnSave"
                        runat="server"
                        Text="Save"
                        CssClass="btnsave"
                        OnClick="Save" />

                </td>

            </tr>

        </table>

    </div>

</asp:Content>