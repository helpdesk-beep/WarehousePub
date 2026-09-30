<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteMultipleWHR.aspx.cs" Inherits="StatePages_DeleteMultipleWHR" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Dispatch Management</title>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
    
    <script src="https://code.jquery.com/jquery-3.7.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>

    <style>
        tfoot input { width: 100%; box-sizing: border-box; padding: 3px; }
        .container-fluid { padding: 20px; }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            // Setup - add a text input to each footer cell
            $('#gvDispatch tfoot th').each(function (i) {
                if (i > 0) { // Skip the checkbox column
                    var title = $(this).text();
                    $(this).html('<input type="text" placeholder="Search ' + title + '" />');
                }
            });

            // Initialize DataTable
            var table = $('#gvDispatch').DataTable({
                "pageLength": 10,
                "columnDefs": [{ "orderable": false, "targets": 0 }],
                "dom": '<"top"f>rt<"bottom"lp><"clear">', // Position global search
                initComplete: function () {
                    // Apply the search
                    this.api().columns().every(function () {
                        var that = this;
                        $('input', this.footer()).on('keyup change clear', function () {
                            if (that.search() !== this.value) {
                                that.search(this.value).draw();
                            }
                        });
                    });
                }
            });

            // "Select All" functionality
            $('#chkHeader').on('click', function () {
                var rows = table.rows({ 'search': 'applied' }).nodes();
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });

            // Delete Logic
            $('#btnDelete').on('click', function () {
                var selectedIds = [];
                $('.chkRow:checked').each(function () {
                    selectedIds.push($(this).val());
                });

                if (selectedIds.length === 0) {
                    alert("Please select at least one record to delete.");
                    return;
                }

                if (confirm("Are you sure you want to delete " + selectedIds.length + " records?")) {
                    $.ajax({
                        type: "POST",
                        url: "DeleteMultipleWHR.aspx/DeleteSelected", // Updated to match current file
                        data: JSON.stringify({ ids: selectedIds }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            alert(response.d);
                            location.reload();
                        },
                        error: function () {
                            alert("Error occurred while deleting records.");
                        }
                    });
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <h3>Dispatch Records (Unmatched in Storage)</h3>
            <hr />
            <button type="button" id="btnDelete" class="btn btn-danger mb-3">Delete Selected Records</button>

            <asp:GridView ID="gvDispatch" runat="server" AutoGenerateColumns="False" 
                ClientIDMode="Static" CssClass="table table-striped table-bordered" ShowFooter="true">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input type="checkbox" id="chkHeader" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" class="chkRow" value='<%# Eval("Acceptance_No") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DispatchId" HeaderText="Dispatch ID" FooterText="Dispatch ID" />
                    <asp:BoundField DataField="Commodity" HeaderText="Commodity" FooterText="Commodity" />
                    <asp:BoundField DataField="Season" HeaderText="Season" FooterText="Season" />
                    <asp:BoundField DataField="DispatchCreatedDate" HeaderText="Created Date" DataFormatString="{0:dd/MM/yyyy}" FooterText="Date" />
                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" FooterText="Vehicle" />
                    <asp:BoundField DataField="DispatchQuantity" HeaderText="Qty" FooterText="Qty" />
                    <asp:BoundField DataField="DispatchBags" HeaderText="Bags" FooterText="Bags" />
                    <asp:BoundField DataField="CenterName" HeaderText="Center" FooterText="Center" />
                    <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No" FooterText="Acceptance No" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>