<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WareHouseMaster.master" CodeFile="Rpt_DistrictWise_HiredTypeWise_VacantGodownCapacity.aspx.cs" Inherits="StatePages_Rpt_DistrictWise_HiredTypeWise_VacantGodownCapacity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/2.4.1/css/buttons.dataTables.min.css" />

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

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div class="row justify-content-center">
        <div class="col-md-12">
            <div class="p-3 mb-4 bg-light shadow-sm rounded text-center">
                <h3 class="text-danger mb-0">District Hired Type Wise Godown Vacant Capacity</h3>
                <small>(Qty In M.T.)</small>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-md-4">
                <label class="fw-bold">District</label>
                <asp:DropDownList ID="ddldistrict" runat="server" CssClass="form-control show-loader-ddl" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="fw-bold">Branch</label>
                <asp:DropDownList ID="ddlbranch" runat="server" CssClass="form-control show-loader-ddl" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="fw-bold">Range</label>
                <asp:DropDownList ID="ddlCapacityRange" runat="server" CssClass="form-control show-loader-ddl" AutoPostBack="true" OnSelectedIndexChanged="ddlCapacityRange_SelectedIndexChanged">
                    <asp:ListItem Text="--Select Range--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="10% - 20%" Value="10-20"></asp:ListItem>
                    <asp:ListItem Text="20% - 30%" Value="20-30"></asp:ListItem>
                    <asp:ListItem Text="30% - 40%" Value="30-40"></asp:ListItem>
                    <asp:ListItem Text="40% - 50%" Value="40-50"></asp:ListItem>
                    <asp:ListItem Text="50% - 60%" Value="50-60"></asp:ListItem>
                    <asp:ListItem Text="60% - 70%" Value="60-70"></asp:ListItem>
                    <asp:ListItem Text="70% - 80%" Value="70-80"></asp:ListItem>
                    <asp:ListItem Text="80% - 90%" Value="80-90"></asp:ListItem>
                    <asp:ListItem Text="90% - 100%" Value="90-100"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="table-responsive">
            <asp:GridView runat="server" ID="GridView1" ShowFooter="true" AutoGenerateColumns="false" CssClass="Grid">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                    <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                    <asp:BoundField DataField="TypeofGodown" HeaderText="Type of Godown" />
                    <asp:BoundField DataField="GodownCapacity" HeaderText="Godown Capacity" />
                    <asp:BoundField DataField="Quantityofstockstoredinwarehouse" HeaderText="Stock Stored" />
                    <asp:BoundField DataField="VacantCapacity" HeaderText="Vacant Capacity" />
                    <asp:BoundField DataField="VacantPercentage" HeaderText="Vacant %" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.html5.min.js"></script>

    <script type="text/javascript">
        function ApplyDataTables() {
            var grid = $('#<%= GridView1.ClientID %>');
            if (grid.length > 0 && grid.find('tr').length > 1) {
                // Prepare Header for DataTables [cite: 34]
                if (grid.find('thead').length === 0) {
                    grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
                }

                grid.DataTable({
                    "destroy": true,
                    "dom": 'Bfrtip',
                    "buttons": [
                        { extend: 'excelHtml5', title: 'Godown Capacity Report', footer: true },
                        { extend: 'pdfHtml5', title: 'Godown Capacity Report', orientation: 'landscape', footer: true }
                    ],
                    "ordering": false, // Keep rows in current order [cite: 39]
                    "paging": false,
                    "drawCallback": function (settings) {
                        var api = this.api();
                        var rows = api.rows({ page: 'current' }).nodes();
                        var last = null;
                        var subCap = 0, subStock = 0, subVac = 0;

                        // Group by "Type of Godown" (Column index 1)
                        api.column(1, { page: 'current' }).data().each(function (group, i) {
                            var cap = parseFloat(api.cells(i, 4).data()[0]) || 0;
                            var stock = parseFloat(api.cells(i, 5).data()[0]) || 0;
                            var vac = parseFloat(api.cells(i, 6).data()[0]) || 0;

                            if (last !== group) {
                                if (last !== null) {
                                    $(rows).eq(i - 1).after(
                                        '<tr class="subtotal-row"><td colspan="4" align="right">Sub Total (' + last + ')</td>' +
                                        '<td align="right">' + subCap.toFixed(2) + '</td>' +
                                        '<td align="right">' + subStock.toFixed(2) + '</td>' +
                                        '<td align="right">' + subVac.toFixed(2) + '</td><td></td></tr>'
                                    );
                                    subCap = 0; subStock = 0; subVac = 0;
                                }
                                last = group;
                            }
                            subCap += cap; subStock += stock; subVac += vac;

                            // Add subtotal row for the final group
                            if (i === api.column(1).data().length - 1) {
                                $(rows).eq(i).after(
                                    '<tr class="subtotal-row"><td colspan="4" align="right">Sub Total (' + group + ')</td>' +
                                    '<td align="right">' + subCap.toFixed(2) + '</td>' +
                                    '<td align="right">' + subStock.toFixed(2) + '</td>' +
                                    '<td align="right">' + subVac.toFixed(2) + '</td><td></td></tr>'
                                );
                            }
                        });
                    }
                });
            }
        }
        $(document).ready(ApplyDataTables);
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm) { prm.add_endRequest(ApplyDataTables); } // Fix for UpdatePanel

    </script>
</asp:Content>
