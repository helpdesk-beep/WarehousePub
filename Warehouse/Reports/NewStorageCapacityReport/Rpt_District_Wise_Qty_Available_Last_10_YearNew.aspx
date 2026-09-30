<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WareHouseMaster.master" CodeFile="~/Reports/NewStorageCapacityReport/Rpt_District_Wise_Qty_Available_Last_10_YearNew.aspx.cs" Inherits="Rpt_District_Wise_Qty_Available_Last_10_YearNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <%--    <script src="../../JS/gridviewscroll.js"></script>--%>
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
    <div class="row justify-content-center">
        <div class="col-md-12 ml-2">
            <div class="p-3 mb-4 bg-light shadow-sm rounded">
                <h3 class="text-center text-danger mb-0">
                    <i class="fas fa-warehouse me-2"></i>
                    District Wise Last 10 Year Stock Position<label style="color: red">(In M.T.)</label>
                </h3>
            </div>

        </div>

        <div class="form-group">
            <div class="col-md-2 fw-bold text-end align-middle">
                <asp:Label ID="Label2" runat="server" Text="Select Commodity"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:ListBox ID="drpDwnCommodity" runat="server" SelectionMode="Multiple"
                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info show-loader" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div class="col-md-12 ml-12">
            <div class="table table-condensed table-responsive">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ShowFooter="true"
                    CssClass="table-bordered table-hover" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
<HeaderStyle CssClass="GridViewheader" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle Font-Bold="True" ForeColor="Black" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <%--<script src="../../JS/table2excel.js"></script>--%>
    <%--<script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>--%>
    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);
    </script>
    <%-- <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GridView1]").table2excel({
                filename: "District_Branch_and_Godown_Wise_Stock_Position.xls"
            });
        });

    </script>--%>
</asp:Content>
