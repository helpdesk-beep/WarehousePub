<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WareHouseMaster.master" CodeFile="Rpt_DistrictWise_HiredTypeWise_GodownCapacity_New.aspx.cs" Inherits="Rpt_DistrictWise_HiredTypeWise_GodownCapacity_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <%-- DataTables ColReorder CSS --%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/colreorder/1.7.0/css/colReorder.dataTables.min.css" />
    <style>
        .btnMargin { margin-bottom: 10px !important; }
        .table-responsive { width: 100% !important; overflow-x: auto; }
        /* Prevent width jumping during reorder */
        table.dataTable { width: 100% !important; margin: 0 auto; border-collapse: collapse !important; }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="row justify-content-center">
        <div class="col-md-12 ml-2">
            <div class="p-3 mb-4 bg-light shadow-sm rounded">
                <h3 class="text-center text-danger mb-0">
                    <i class="fas fa-warehouse me-2"></i>District Hired Type Wise Godown Capacity and Avl. Stock Position in MT 
                </h3>
            </div>
        </div>

        <div class="row mb-3 align-items-center">
            <div class="col-md-2 fw-bold text-end">Godown Type</div>
            <div class="col-md-2">
                <asp:ListBox ID="ddlGodownType" runat="server" SelectionMode="Multiple" CssClass="checkbox-multiselect form-control"></asp:ListBox>
            </div>
            <div class="col-md-2 fw-bold text-end">Storage Type</div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlstorageType" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info show-loader" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div class="col-md-12">
            <div class="table-responsive">
               <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                    OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated"
                    AutoGenerateColumns="false" CssClass="table table-bordered table-hover display nowrap" 
                    UseAccessibleHeader="true">
                    <Columns>
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        <asp:BoundField DataField="District_Id" HeaderText="District Id" />
                        <asp:BoundField DataField="TypeofGodown" HeaderText="Type of Godown" />
                        <asp:TemplateField HeaderText="Total Godown">
                            <ItemTemplate><asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalGodown") %>'></asp:Label></ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Godown Capacity">
                            <ItemTemplate><asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("GodownCapacity") %>'></asp:Label></ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock Stored">
                            <ItemTemplate><asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Quantityofstockstoredinwarehouse") %>'></asp:Label></ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vacant Capacity">
                            <ItemTemplate><asp:Label ID="lblCVC" runat="server" Text='<%# Eval("currentlyvacantcapacity") %>'></asp:Label></ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <%-- Load ColReorder JS --%>
    <script type="text/javascript" src="https://cdn.datatables.net/colreorder/1.7.0/js/dataTables.colReorder.min.js"></script>

    <%--<script type="text/javascript">
        function BindDatatable() {
            var gridId = '#<%= GridView1.ClientID %>';
            var grid = $(gridId);

            // Only initialize if the grid exists and has data rows
            if (grid.length > 0 && grid.find('tbody tr').length > 0) {
                
                // 1. Destroy existing instance if it exists
                if ($.fn.DataTable.isDataTable(gridId)) {
                    grid.DataTable().destroy();
                }

                // 2. Standard ASP.NET GridView fix: move first row to <thead>
                if (grid.find("thead").length === 0) {
                    grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
                }

                // 3. Initialize DataTable
                var table = grid.DataTable({
                    paging: true,
                    searching: true,
                    ordering: true,
                    info: true,
                    scrollX: true,
                    autoWidth: false,
                    colReorder: true, // MOVABLE COLUMNS
                    lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                    dom: '<"top d-flex justify-content-between align-items-center"Bfl>rt<"bottom"ip><"clear">',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Show / Hide Columns 👁️',
                            className: 'btn btn-sm btn-outline-secondary'
                        },
                        { 
                            extend: 'excelHtml5', 
                            title: 'District_Godown_Report', 
                            text: 'Excel 💾',
                            className: 'btn btn-sm btn-success'
                        },
                        { 
                            extend: 'pdfHtml5', 
                            title: 'District_Godown_Report', 
                            text: 'PDF 📄', 
                            orientation: 'landscape',
                            className: 'btn btn-sm btn-danger'
                        },
                        { 
                            extend: 'print', 
                            text: 'Print 🖨️',
                            className: 'btn btn-sm btn-primary'
                        }
                    ],
                    language: {
                        searchPlaceholder: "Search records...",
                        search: ""
                    }
                });

                // Correct the layout after initialization
                table.columns.adjust();
            }
        }

        // Initialize on load
        $(document).ready(function () {
            BindDatatable();
        });

        // Handle AJAX UpdatePanel PostBacks
        if (typeof (Sys) !== 'undefined') {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                BindDatatable();
            });
        }
    </script>--%>
    <script type="text/javascript">
    function BindDatatable(gv) {
        if ($.fn.DataTable.isDataTable(gv)) {
            gv.DataTable().destroy();
        }

        var table = gv.DataTable({
            paging: true,
            searching: true,
            ordering: true,
            info: true,
            colReorder: true, // Enables movable columns functionality
            scrollX: true,
            autoWidth: false,
            dom: '<"top d-flex justify-content-between align-items-center"Bfl>rt<"bottom"ip><"clear">',
            buttons: [
                {
                    extend: 'colvis',
                    text: 'Show / Hide Columns 👁️'
                },
                { extend: 'excelHtml5', title: 'Godown_Report', text: 'Excel 💾' },
                { extend: 'pdfHtml5', title: 'Godown_Report', text: 'PDF 📄', orientation: 'landscape' },
                { extend: 'print', text: 'Print' }
            ]
        });
    }

    $(document).ready(function () {
        var grid = $('#<%= GridView1.ClientID %>');
        BindDatatable(grid);
    });d: 'pdfHtml5', title: 'Godown_Rort', orientation: 'landscape' },
                    { extend: 'print' }
                ]
            });
        }

        $(document).ready(function () {
            BindDatatable();
        });
    </script>
</asp:Content>
