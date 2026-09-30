<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FCI_Master.master" AutoEventWireup="true" CodeFile="~/FCI/Region_Wise_Agreement_And_Non_Agreement_Capacity.aspx.cs" Inherits="FCI_Region_Wise_Agreement_And_Non_Agreement_Capacity" %>

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
            background: #2095A1 !important;
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
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content_wrapper">
        <fieldset>
            <legend>Offered/Inspection/Agreement Godown Summary Report</legend>
            <div class="row">
                <div class="col-md-1" style="margin-top: 8px">
                    <label>Season</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddl_session" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                        <asp:ListItem Value="Rab2024_25">JVS Rabi 2024-25</asp:ListItem>
                        <%-- <asp:ListItem Value="JVS2022_23">JVS 2022-23</asp:ListItem>
                        <asp:ListItem Value="JVS2021_22">JVS 2021-22</asp:ListItem>
                        <asp:ListItem Value="JVS2020_21">JVS 2020-21</asp:ListItem>
                        <asp:ListItem Value="Rabi1920">Rabi 2019-20</asp:ListItem>
                        <asp:ListItem Value="Kharif1920">Kharif 2019-20</asp:ListItem>
                        <asp:ListItem Value="Rabi1819">Rabi 2018-19</asp:ListItem>
                        <asp:ListItem Value="Kharif1819">Kharif 2018-19</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                <div class="col-md-6"></div>
                <div class="col-md-2">
                    <asp:Button ID="Button1" runat="server" Text="Export In Excel" class="button button2" Width="120px" Height="28px"
                        OnClick="Button1_Click1" />
                </div>
            </div>
        </fieldset>
        <fieldset>
            <div class="row" id="toexport" runat="server">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="AgreeGrid" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="District" CssClass="table table-bordered table-hover datatable"
                            Font-Size="10pt" ShowFooter="true">
                            <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." HeaderStyle-BackColor="#669999">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Region Name" HeaderStyle-BackColor="#669999">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/WarehouseLevel/Rpt_Godown_Bill_Wise_Payment_Status.aspx?GodownID="+ (Eval("Region_ID").ToString())%>'
                                            title="Region Name" Text=' <%# Eval("Regionnm") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="#669999">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/WarehouseLevel/Rpt_Godown_Bill_Wise_Payment_Status.aspx?GodownID="+ (Eval("District_Id").ToString())%>'
                                            title="District Name" Text=' <%# Eval("District") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Regionnm" HeaderText="Region"  />--%>
                                <%--<asp:BoundField DataField="District" HeaderText="District" HeaderStyle-BackColor="#669999" />--%>
                                <asp:BoundField DataField="NoOfOfferedGodown" HeaderText="No Of Offered Godown" SortExpression="NoOfOfferedGodown" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#669999" />
                                <asp:BoundField DataField="OfferedCapacity" HeaderText="Offered Capacity" SortExpression="OfferedCapacity" HeaderStyle-BackColor="#669999" ItemStyle-HorizontalAlign="Right" />
                                <%--<asp:BoundField DataField="FIT" HeaderText="FIT" SortExpression="FIT" HeaderStyle-BackColor="#669999" />--%>
                                <%--<asp:BoundField DataField="FITCapacity" HeaderText="FIT Capacity" SortExpression="FITCapacity" HeaderStyle-BackColor="#669999" />--%>
                                <%-- <asp:BoundField DataField="UNFIT" HeaderText="UNFIT" SortExpression="UNFIT" HeaderStyle-BackColor="#669999" />
                                <asp:BoundField DataField="UNFITCapacity" HeaderText="UNFIT Capacity" SortExpression="UNFITCapacity" HeaderStyle-BackColor="#669999" />--%>
                                <asp:BoundField DataField="Agreement_Capacity" HeaderText="Agreement Capacity" SortExpression="Agreement_Capacity" HeaderStyle-BackColor="#669999" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Remainning_QTY" HeaderText="Remainning Capacity" SortExpression="Remainning_QTY" HeaderStyle-BackColor="#669999" ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                            <FooterStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Height="25px" Font-Size="10pt" />
                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="30px" Font-Size="10pt" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

