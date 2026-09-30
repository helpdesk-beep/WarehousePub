<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Owned_Godown_Details_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Owned_Godown_Details_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
   <%-- <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.4.1/css/buttons.dataTables.min.css" />--%>


    <style>
        .Grid {
            background-color: #fff;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            width: 100%;
        }

            .Grid td {
                padding: 4px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 8px 4px;
                color: #fff;
                background: #00aad2;
                border: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

        .subtotal-row {
            background-color: #f2f2f2 !important;
            font-weight: bold;
        }

        .grand-total {
            background-color: #d9edf7 !important;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <div class="card-header1 text-center">
            <h4>M.P. WAREHOUSING & LOGISTICS CORPORATION</h4>
            <h4 class="mb-4">'Owned','PVT.PEG','BOT-AUB','CWC','Steel Silo','Hired','Tribal Schemes' Godown Capacity and available Stock Position in M.T.
        <span class="text-danger">(Qty In M.T.)</span>
                <span style="width: 52.68mm; height: 6.35mm;">Date:-</span>
                <asp:Label ID="labelName" runat="server"></asp:Label>
            </h4>
            <hr />
        </div>
        <div class="table-responsive">
            <%--<asp:GridView ID="GridView1" runat="server"
                OnPreRender="GridView1_PreRender"
                CssClass="table table-bordered table-striped"
                AutoGenerateColumns="false"
                UseAccessibleHeader="true">
                <Columns>
                    <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                    <asp:BoundField DataField="DistrictId" HeaderText="DistrictId" />
                    <asp:BoundField DataField="DepotName" HeaderText="DepotName" />
                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                    <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />

                    <asp:TemplateField HeaderText="Total Capacity in M.T.">
                        <ItemTemplate>
                            <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalCapacityinMT") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Available Quantity">
                        <ItemTemplate>
                            <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("AvailableQty") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vacant Capacity">
                        <ItemTemplate>
                            <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("VacantCapacity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>--%>
            <asp:GridView ID="GridView1" runat="server"
                OnPreRender="GridView1_PreRender"
                CssClass="table table-bordered table-striped"
                AutoGenerateColumns="false"
                ShowFooter="true"
                OnRowDataBound="GridView1_RowDataBound"
                UseAccessibleHeader="true">

                <Columns>

                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                    <asp:BoundField DataField="DistrictId" HeaderText="DistrictId" Visible="false" />
                    <asp:BoundField DataField="DepotName" HeaderText="Depot Name" />
                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                    <asp:BoundField DataField="Hired_Type" HeaderText="Type" />

                    <asp:TemplateField HeaderText="Total Capacity (M.T.)">
                        <ItemTemplate>
                            <asp:Label ID="lblCap" runat="server"
                                Text='<%# Eval("TotalCapacityinMT") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Available Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblAvail" runat="server"
                                Text='<%# Eval("AvailableQty") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Vacant Capacity">
                        <ItemTemplate>
                            <asp:Label ID="lblVacant" runat="server"
                                Text='<%# Eval("VacantCapacity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div>
        <asp:Label ID="lblMsg" runat="server" BackColor="Red" Font-Size="Large"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <%--<script type="text/javascript">
        $(document).ready(function () {
            var tableId = '#<%= GridView1.ClientID %>';

            if ($.fn.DataTable.isDataTable(tableId)) {
                $(tableId).DataTable().destroy();
            }

            $(tableId).DataTable({
                responsive: true,
                paging: true,
                searching: true,
                ordering: true,
                info: true,
                pageLength: 10
            });
        });
    </script>--%>
    <%--<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>

    <script src="https://cdn.datatables.net/buttons/2.4.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.html5.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js"></script>

    <!-- PDF ke liye ye DO line jaruri -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.7/vfs_fonts.js"></script>--%>

    <%--<script type="text/javascript">
        function ApplyDataTables() {

            var tableId = '#<%= GridView1.ClientID %>';

            if ($.fn.DataTable.isDataTable(tableId)) {
                $(tableId).DataTable().destroy();
            }

            $(tableId).DataTable({
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Godown Capacity Report'
                    },
                    {
                        extend: 'pdfHtml5',
                        title: 'Godown Capacity Report',
                        orientation: 'landscape',
                        pageSize: 'A4'
                    }
                ],
                paging: true,
                searching: true,
                ordering: false,
                info: true,
                destroy: true
            });
        }

        $(document).ready(function () {
            setTimeout(function () {
                ApplyDataTables();
            }, 200);
        });

        if (typeof (Sys) !== "undefined") {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                setTimeout(function () {
                    ApplyDataTables();
                }, 200);
            });
        }
    </script>--%>

    <%--<script type="text/javascript">

        function ApplyDataTables() {

            var tableId = '#<%= GridView1.ClientID %>';

            if ($.fn.DataTable.isDataTable(tableId)) {
                $(tableId).DataTable().destroy();
            }

            $(tableId).DataTable({

                dom: 'Bfrtip',

                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Godown Capacity Report',
                        text: 'Export Excel'
                    },
                    {
                        extend: 'pdfHtml5',
                        title: 'Godown Capacity Report',
                        text: 'Export PDF',
                        orientation: 'landscape',
                        pageSize: 'A4'
                    }
                ],

                paging: true,
                searching: true,
                ordering: false,
                info: true,
                destroy: true,
                scrollX: true,
                lengthChange: true,

                // ✅ Show All Option Added
                lengthMenu: [
                    [10, 25, 50, 100, -1],
                    ['10', '25', '50', '100', 'Show All']
                ],

                pageLength: 10

            });
        }

        $(document).ready(function () {
            setTimeout(function () {
                ApplyDataTables();
            }, 200);
        });

        if (typeof (Sys) !== "undefined") {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                setTimeout(function () {
                    ApplyDataTables();
                }, 200);
            });
        }

    </script>--%>

    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>


</asp:Content>

