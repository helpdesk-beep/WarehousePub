<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Inspections/Inspection_Officer/GodownWise.aspx.cs" Inherits="Inspection_GodownWise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Godown Wise Inspection Report</title>

<link href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" rel="stylesheet" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf-autotable/3.5.29/jspdf.plugin.autotable.min.js"></script>
<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>

<style>
    body {
        background: #f4f6f9;
        font-family: "Segoe UI", Arial, sans-serif;
    }

    .page-card {
        width: 95%;
        margin: 20px auto;
        background: #ffffff;
        border-radius: 12px;
        box-shadow: 0 10px 25px rgba(0,0,0,0.08);
        padding: 20px;
    }

    .report-title {
        font-size: 26px;
        font-weight: 600;
        color: #212529;
    }

    .report-subtitle {
        color: #6c757d;
        font-size: 14px;
    }

    .table thead th {
        background: #f1f3f5;
        font-weight: 600;
        white-space: nowrap;
        text-align: left;
    }

    .table td, .table th {
        vertical-align: middle;
        text-align: left;
    }

    .diff-positive {
        color: #28a745;
        font-weight: 600;
    }

    .diff-negative {
        color: #dc3545;
        font-weight: 600;
    }

    .zero-online-row {
        background-color: #f8d7da !important;
    }
</style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="page-card">
         <asp:ScriptManager ID="ScriptManager1" runat="server" />
         <div class="row mb-3">
             <div class="col-12">
                 <div class="report-title">Stack Wise Inspection Report</div>
                 <div class="report-subtitle">
                     <%--<span id="lblBranch" runat="server"></span> |--%>
                     <span id="lblGodown" runat="server"></span>
                 </div>
             </div>
         </div>

         <div class="row mb-3">
             <div class="col-md-6">
             </div>
             <div class="col-md-6 text-right">
                 <asp:HiddenField ID="hfHasData" runat="server" />
                 <asp:Button ID="btnExcel" runat="server"
                     Text="Export Excel"
                     CssClass="btn btn-success btn-sm"
                     OnClientClick="return downloadExcel();" />

                 <asp:Button ID="btnPdf" runat="server"
                     Text="Export PDF"
                     CssClass="btn btn-danger btn-sm ml-2"
                     OnClientClick="return downloadPdf();" />
             </div>
         </div>

         <div class="table-responsive">
             <asp:GridView ID="gvReport" runat="server"
                 AutoGenerateColumns="false"
                 CssClass="table table-bordered table-hover table-sm"
                 GridLines="None"
                 RowStyle-CssClass="table-light"
                 AlternatingRowStyle-CssClass="table-secondary"
                 OnRowDataBound="gvReport_RowDataBound"
                 OnRowCommand="gvReport_RowCommand">

                 <Columns>

                     <asp:TemplateField HeaderText="S.No">
                         <ItemTemplate>
                             <%# Container.DataItemIndex + 1 %>
                         </ItemTemplate>
                     </asp:TemplateField>

                     <asp:BoundField DataField="Stack_Name" HeaderText="Stack Name" />

                     <asp:BoundField DataField="TotalBags_AsOnline"
                         HeaderText="Bags (Online)" />

                     <asp:BoundField DataField="TotalBags_AsPerPV"
                         HeaderText="Bags (PV)" />
                     <asp:BoundField DataField="SpillageBags_AsPerPV"
    HeaderText="Spillage Bags (PV)" />
                                      
                     <asp:TemplateField>
                         <HeaderTemplate>
                             <div style="text-align: left;">
                                 <div class="font-weight-bold">Difference</div>
                                 <div class="text-muted small">PV – Online</div>
                             </div>
                         </HeaderTemplate>

                         <ItemTemplate>
                             <span style="text-align: left; display: inline-block;"
                                 class='<%# (Convert.ToInt32(Eval("TotalBags_AsPerPV")) - Convert.ToInt32(Eval("TotalBags_AsOnline")) >= 0)
                     ? "text-success" : "text-danger" %>'>
                                 <%# Convert.ToInt32(Eval("TotalBags_AsPerPV")) - Convert.ToInt32(Eval("TotalBags_AsOnline")) %>
                             </span>
                         </ItemTemplate>
                     </asp:TemplateField>
                                                           <asp:BoundField DataField="Remark"
HeaderText="Stack Remark" />
                     <asp:TemplateField HeaderText="Image">
                         <ItemTemplate>
                             <asp:LinkButton ID="lnkRemarks"
                                 runat="server"
                                 Text="View"
                                 CommandName="Remarks"
                                 CommandArgument='<%# Eval("Stack_ID") + "|" + Eval("Stack_Name") %>' />
                         </ItemTemplate>
                     </asp:TemplateField>
                  
                 </Columns>
             </asp:GridView>
         </div>
         <div class="modal fade" id="remarksModal" tabindex="-1">
             <div class="modal-dialog modal-lg">
                 <div class="modal-content">

                     <div class="modal-header text-center d-block">
                         <h5 class="modal-title mb-0">Stack Name -
                 <asp:Label ID="lblStackName" runat="server"></asp:Label>
                         </h5>

                         <small class="text-muted">Details
                         </small>

                         <button type="button"
                             class="close position-absolute"
                             style="right: 15px; top: 15px;"
                             data-dismiss="modal">
                             &times;</button>
                     </div>

                     <div class="modal-body">

                        <%-- <div class="mb-4">
                             <h6 class="font-weight-bold text-primary text-center">Remarks
                             </h6>

                             <div class="border rounded p-3 text-center bg-light">
                                 <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                             </div>
                         </div>--%>

                         <div>
                             <h6 class="font-weight-bold text-primary text-center mb-3">Images
                             </h6>

                             <div class="row text-center">

                                 <div class="col-md-6 mb-3">
                                     <div class="font-weight-bold mb-2">
                                         Stack Image
                                     </div>

                                     <asp:Image ID="imgStack"
                                         runat="server"
                                         CssClass="img-fluid img-thumbnail"
                                         AlternateText="Stack Image"
                                         Width="250px"
                                         Height="200px"
                                          Style="object-fit:contain; cursor:pointer;"
                                          onclick="showImage(this.src)"
                                         Visible="false" />
                                 </div>

                                 <div class="col-md-6 mb-3">
                                     <div class="font-weight-bold mb-2">
                                         Commodity Image
                                     </div>
 <div class="text-muted small mb-2">
        <asp:Label ID="lblCommodityName" runat="server"></asp:Label>
    </div>
                                     <asp:Image ID="imgCommodity"
                                         runat="server"
                                         CssClass="img-fluid img-thumbnail"
                                         AlternateText="Commodity Image"
                                         Width="250px"
                                         Height="200px"
                                           Style="object-fit:contain; cursor:pointer;"
                                           onclick="showImage(this.src)"
                                         Visible="false" />
                                 </div>

                             </div>
                         </div>

                     </div>

                 </div>
             </div>
         </div>


     </div>
<div class="modal fade" id="imgModal" tabindex="-1">
    <div class="modal-dialog" style="max-width:80%;">
        <div class="modal-content">
            <div class="modal-body text-center">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <img id="modalImg"  style="width:50vw; height:70vh; object-fit:contain; border-radius:5px;"  />
            </div>
        </div>
    </div>
</div>
    </form>
    <script>
        function showImage(src) {
            document.getElementById("modalImg").src = src;
            $('#imgModal').modal('show');
        }
    </script>
     <script type="text/javascript">
     function hasData() {
         var flag = document.getElementById('<%= hfHasData.ClientID %>').value;
         if (flag !== "1") {
             alert('No data available to export.');
             return false;
         }
         return true;
     }
 </script>
 <script type="text/javascript">
     function hasData() {
         var flag = document.getElementById('<%= hfHasData.ClientID %>').value;
         if (flag !== "1") {
             alert('No data available to export.');
             return false;
         }
         return true;
     }
 </script>

<script type="text/javascript">
    function downloadExcel() {

        if (!hasData()) return false;

        var table = getTableWithoutRemarks();
        if (!table) {
            alert('No data available to export.');
            return false;
        }

        var html = '';
        html += '<html><head><meta charset="utf-8">';
        html += '<style>';
        html += 'table{border-collapse:collapse;width:100%;font-family:Arial;}';
        html += 'th,td{border:1px solid #000;padding:6px;text-align:center;}';
        html += 'th{background:#E9ECEF;font-weight:bold;}';
        html += '</style></head><body>';

        html += "<h2 style='text-align:center;'>Godown Wise Inspection Report</h2>";
        html += "<div style='text-align:center;color:gray;margin-bottom:10px;'>";
        html += "<%= lblGodown.InnerText %>";
        html += "</div>";

        html += "<div style='text-align:right;font-size:12px;margin-bottom:20px;'>";
        html += "Generated on: " + new Date().toLocaleString();
        html += "</div>";

        html += table.outerHTML;
        html += '</body></html>';

        var blob = new Blob([html], {
            type: 'application/vnd.ms-excel;charset=utf-8;'
        });

        var fileName = 'GodownWiseInspection_' + new Date().getTime() + '.xls';

        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = fileName;

        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);

        return false;
    }
</script>


<%--<script type="text/javascript">
    function downloadPdf() {

        if (!hasData()) return false;

        var table = getTableWithoutRemarks();
        if (!table) {
            alert('No data available to export.');
            return false;
        }

        var jsPDF = window.jspdf.jsPDF;
        var doc = new jsPDF('l', 'pt', 'a4');

        doc.setFontSize(16);
        doc.text('Godown Wise Inspection Report', 420, 40, { align: 'center' });

        doc.setFontSize(11);
        doc.text('<%= lblGodown.InnerText %>', 420, 60, { align: 'center' });

        doc.setFontSize(9);
        doc.text('Generated on: ' + new Date().toLocaleString(), 760, 80);

        doc.autoTable({
            html: table,
            startY: 100,
            theme: 'grid',
            styles: {
                fontSize: 8,
                halign: 'center'
            },
            headStyles: {
                fillColor: [233, 236, 239]
            },
            alternateRowStyles: {
                fillColor: [249, 249, 249]
            }
        });

        var fileName = 'GodownWiseInspection_' + new Date().getTime() + '.pdf';
        doc.save(fileName);

        return false;
    }
</script>--%>
    <script type="text/javascript">
        function downloadPdf() {

            if (!hasData()) return false;

            var table = getTableWithoutRemarks();
            if (!table) {
                alert('No data available to export.');
                return false;
            }

            var jsPDF = window.jspdf.jsPDF;
            var doc = new jsPDF('l', 'pt', 'a4');

            // Page width & height
            var pageWidth = doc.internal.pageSize.getWidth();
            var pageHeight = doc.internal.pageSize.getHeight();

            /* ================= HEADER ================= */

            // Title
            doc.setFontSize(16);
            doc.setFont(undefined, 'bold');
            doc.text('Godown Wise Inspection Report', pageWidth / 2, 40, { align: 'center' });

            // Godown name
            doc.setFontSize(11);
            doc.setFont(undefined, 'normal');
            doc.text('<%= lblGodown.InnerText %>', pageWidth / 2, 60, { align: 'center' });

            // Generated on (RIGHT aligned – NO CUTTING)
            doc.setFontSize(9);
            doc.text(
                'Generated on: ' + new Date().toLocaleString(),
                pageWidth - 40,
                80,
                { align: 'right' }
            );

            /* ================= TABLE ================= */

            doc.autoTable({
                html: table,
                startY: 100,
                margin: { left: 30, right: 30 }, // prevent left/right cut
                theme: 'grid',
                styles: {
                    fontSize: 8,
                    halign: 'center',
                    valign: 'middle',
                    overflow: 'linebreak'
                },
                headStyles: {
                    fillColor: [233, 236, 239],
                    textColor: 20,
                    fontStyle: 'bold'
                },
                alternateRowStyles: {
                    fillColor: [249, 249, 249]
                },
                didDrawPage: function (data) {

                    // Footer - page number
                    var pageCount = doc.internal.getNumberOfPages();
                    doc.setFontSize(9);
                    doc.text(
                        'Page ' + pageCount,
                        pageWidth / 2,
                        pageHeight - 20,
                        { align: 'center' }
                    );
                }
            });


            var fileName = 'GodownWiseInspection_' + new Date().getTime() + '.pdf';
            doc.save(fileName);

            return false;
        }
    </script>

 <script type="text/javascript">
     function getTableWithoutRemarks() {

         var originalTable = document.getElementById('<%= gvReport.ClientID %>');
         if (!originalTable || originalTable.rows.length <= 1) {
             return null;
         }

         var table = originalTable.cloneNode(true);

         //for (var i = 0; i < table.rows.length; i++) {
         //    table.rows[i].deleteCell(table.rows[i].cells.length - 1);
         //}
         var links = table.getElementsByTagName("a");
         while (links.length > 0) {
             links[0].outerHTML = links[0].innerText;
         }

         return table;
     }
 </script>

</body>
</html>
