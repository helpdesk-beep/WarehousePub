<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WareHouseMaster.master" CodeFile="~/SRV/Storage_Reports/Inspenctions/Rpt_All_Type_Wise_Stock_entry_by_BMNew.aspx.cs" Inherits="Rpt_All_Type_Wise_Stock_entry_by_BMNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <style>
        .btnMargin {
            margin-bottom: 10px !important;
        }
    </style>
    <style>
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
            width: 100%;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #00aad2 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #00aad2 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
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
            Width: 200px;
            height: 50px;
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1" />

    <div class="row justify-content-center">

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-md-12 ml-2">
                    <div class="p-3 mb-4 bg-light shadow-sm rounded">
                        <h3 class="text-center text-danger mb-0">
                            <i class="fas fa-warehouse me-2"></i>District, Commodity, Date Wise Stock Position
                        <label style="color: red">(In M.T.)</label>
                        </h3>
                    </div>
                </div>

                <div class="form-group row">
                    <div class="col-md-2 fw-bold text-end">Select Division</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddldivision" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 fw-bold text-end">Select District</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group row mt-2">
                    <div class="col-md-2 fw-bold text-end">Select Branch</div>
                    <div class="col-md-4">
                        <asp:ListBox ID="ddlbranch" runat="server" SelectionMode="Multiple" AutoPostBack="true" CssClass="checkbox-multiselect form-control" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged"></asp:ListBox>
                    </div>
                    <div class="col-md-2 fw-bold text-end">WDRA Compliant</div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlwdra" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlwdra_SelectedIndexChanged">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group row mt-2">
                    <div class="col-md-2 fw-bold text-end">Godown Type</div>
                    <div class="col-md-2">
                        <asp:DropDownList CssClass="form-control" ID="ddlgodowntype" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlgodowntype_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 fw-bold text-end">Godown Name</div>
                    <div class="col-md-2">
                        <asp:DropDownList CssClass="form-control" ID="ddlGodown" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 fw-bold text-end">Crop Year</div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlcropyear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group row mt-2">
                    <div class="col-md-2 fw-bold text-end">Depositor Type</div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlDepositorType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepositorType_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 fw-bold text-end">Depositor Name</div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlDepositor" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group row mt-2">
                    <div class="col-md-2 fw-bold text-end">Commodity Type</div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlCommoditytype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCommoditytype_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 fw-bold text-end">Commodity</div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlcommodity" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="row mt-4">
                    <div class="table-responsive">
                        <asp:GridView ID="gvStockReport" runat="server" AutoGenerateColumns="true" ShowFooter="true"
                            CssClass="Grid table-bordered table-hover" OnRowDataBound="gvStockReport_RowDataBound">
                            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Right" />
                        </asp:GridView>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddldistrict" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <%-- <script>
        var grid = $('#<%= gvStockReport.ClientID %>');
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);
    </script--%>>
    <script type="text/javascript">
        function ApplyDataTables() {
            var grid = $('#<%= gvStockReport.ClientID %>');
            if (grid.length > 0 && grid.find('tr').length > 1) {
                // Your requested logic
                grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
                BindDatatable(grid);
            }

        }

        $(document).ready(function () {
            ApplyDataTables();
        });

        // Re-apply after UpdatePanel/PostBack
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm) {
            prm.add_endRequest(function () {
                ApplyDataTables();
            });
        }
    </script>
</asp:Content>

