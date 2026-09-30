
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stack_Report.aspx.cs" Inherits="StatePages_Stack_Report" %>

<!DOCTYPE html>
<html lang="hi">
<head runat="server">
    <title>Godown Stack Report</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.4/css/jquery.dataTables.min.css">
    <style>
        body { background-color: #f4f7f6; font-family: 'Segoe UI', Arial, sans-serif; }
        .card { border: none; border-radius: 10px; }
        .table thead th { vertical-align: middle; background-color: #2c3e50; color: white; font-size: 0.9rem; }
        .table-container { padding: 20px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid table-container">
            <div class="card shadow">
                <div class="card-header bg-white py-3">
                    <h4 class="text-primary fw-bold mb-0">Storage Summary Report</h4>
                </div>
                <div class="card-body">
                    <table id="tblReport" class="table table-bordered table-striped" style="width:100%">
                        <thead>
                            <tr>
                                <th>S.No</th>
                                <th>Godown Name</th>
                                <th id="hPrevDate">दिनांक तक online stack</th>
                                <th id="hCurrDate">आज दिनांक तक कुल स्टैक</th>
                                <th>कुल स्टैक</th>
                                <th>Mobile app के द्वारा लिये गये कुल स्टैक का Moisture</th>
                                <th>DM MPSCSC को Send किए गए कुल स्टैक</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            // Set dynamic dates in headers
            var today = new Date();
            var yesterday = new Date();
            yesterday.setDate(today.getDate() - 1);

            var options = { day: '2-digit', month: '2-digit', year: 'numeric' };
            var fToday = today.toLocaleDateString('en-GB').replace(/\//g, '-');
            var fPrev = yesterday.toLocaleDateString('en-GB').replace(/\//g, '-');

            $("#hPrevDate").text("दिनांक " + fPrev + " तक बनाये गये online stack");
            $("#hCurrDate").text("आज दिनांक " + fToday + " तक बनाये गये कुल स्टैक");

            // Load DataTables
            $('#tblReport').DataTable({
                "ajax": {
                    "url": "Stack_Report.aspx/GetStackData",
                    "type": "POST",
                    "contentType": "application/json; charset=utf-8",
                    "dataSrc": function (json) { return JSON.parse(json.d); }
                },
                "columns": [
                    { "data": "SNo" },
                    { "data": "GodownName" },
                    { "data": "PrevStack" },
                    { "data": "CurrentStack" },
                    { "data": "TotalStack" },
                    { "data": "MobileMoisture" },
                    { "data": "SentToDM" }
                ],
                "columnDefs": [{ "className": "dt-center", "targets": "_all" }]
            });
        });
    </script>
</body>
</html>