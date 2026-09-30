<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Inspections/Inspection_Officer/BranchWise.aspx.cs" Inherits="Inspection_BranchWise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Branch Wise Inspection Report</title>

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
     }

     .table td, .table th {
         vertical-align: middle;
     }

     .godown-link {
         color: #007bff;
         font-weight: 500;
         text-decoration: none;
     }

         .godown-link:hover {
             text-decoration: underline;
         }
          .branch-link {
     color: #0d6efd;
     font-weight: 600;
     text-decoration: none;
 }

     .branch-link:hover {
         color: #6610f2;
         text-decoration: underline;
     }
 </style>


</head>
<body>
    <form id="form1" runat="server">
      <div class="page-card">
         <asp:ScriptManager ID="ScriptManager1" runat="server" />
         <div class="row mb-3">
             <div class="col-12">
                 <div class="report-title">Godown Wise Inspection Report</div>
                 <div class="report-subtitle">
                     <span id="lblDistrict" runat="server"></span>|
             <span id="lblBranch" runat="server"></span>
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
                 OnRowCommand="gvReport_RowCommand">

                 <Columns>
                     <asp:TemplateField HeaderText="S.No">
                         <ItemTemplate>
                             <%# Container.DataItemIndex + 1 %>
                         </ItemTemplate>
                     </asp:TemplateField>

                   <%--  <asp:TemplateField HeaderText="Godown">
                         <ItemTemplate>
                             <a class="godown-link"
                                 href='GodownWise.aspx?GodownId=<%# Eval("Godown_ID") +"&Godown_Name="+Eval("Godown_Name") + "&Mobile=" + Request.QueryString["Mobile"] %>'>
                                 <%# Eval("Godown_Name") %>

                             </a>
                         </ItemTemplate>
                     </asp:TemplateField>--%>
                      <asp:TemplateField HeaderText="Godown">
     <ItemTemplate>

         <asp:Label ID="lblGodownName" runat="server"
             Text='<%# Eval("Godown_Name") %>' Visible="false"></asp:Label>
          <asp:Label ID="lbGodownSubmitDate" runat="server"
     Text='<%# Eval("InspectionDate") %>' Visible="false"></asp:Label>

         <asp:LinkButton ID="lnkGodown" runat="server"
             Text='<%# Eval("Godown_Name") %>'
             CommandArgument='<%# Eval("Godown_ID") %>'
             OnClick="lnkGodown_Click"
             CssClass="branch-link">
         </asp:LinkButton>

     </ItemTemplate>
 </asp:TemplateField>

                     <asp:BoundField DataField="Online_Stack" HeaderText="Stack (Online)" />
                     <asp:BoundField DataField="Insp_Stack" HeaderText="Stack (PV)" />
                     <asp:BoundField DataField="TotalBags_AsPerPV" HeaderText="Bags (PV)" />
                       <asp:BoundField DataField="SpillageBags_AsPerPV" HeaderText="Spillage Bags (PV)" />
                     <asp:BoundField DataField="InspectionDate"
                         HeaderText="PV Date"
                         DataFormatString="{0:dd-MM-yyyy}" />

                      <asp:BoundField DataField="Godown_Remark" HeaderText="Godown Remark" />
                    <%-- <asp:TemplateField HeaderText="Remarks">
                         <ItemTemplate>
                             <asp:LinkButton ID="lnkRemarks"
                                 runat="server"
                                 Text="View Remarks"
                                 CommandName="Remarks"
                                 CommandArgument='<%# Eval("Godown_ID") + "|" + Eval("Godown_Name") %>' />
                         </ItemTemplate>
                     </asp:TemplateField>--%>
                 </Columns>
             </asp:GridView>
         </div>
<%--         <div class="modal fade" id="remarksModal" tabindex="-1">
             <div class="modal-dialog modal-lg">
                 <div class="modal-content">

                     <div class="modal-header text-center d-block">
                         <h5 class="modal-title mb-0">Godown Name - 
                 <asp:Label ID="lblGodownName" runat="server"></asp:Label>
                         </h5>

                         <small class="text-muted">Remarks
                         </small>

                         <button type="button" class="close position-absolute"
                             style="right: 15px; top: 15px;"
                             data-dismiss="modal">
                             &times;</button>
                     </div>

                     <div class="modal-body text-center">
                         <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                     </div>

                 </div>
             </div>
         </div>--%>


     </div>

    </form>
     <script type="text/javascript">
     function hasRecords() {
         var flag = document.getElementById('<%= hfHasData.ClientID %>').value;
         if (flag !== "1") {
             alert('No data available to export.');
             return false;
         }
         return true;
     }
 </script>

<%--<script type="text/javascript">
    function downloadPdf() {

        if (!hasRecords()) return false;

        var table = getTableWithoutRemarks();
        if (!table) {
            alert('No data available to export.');
            return false;
        }

        var jsPDF = window.jspdf.jsPDF;
        var doc = new jsPDF('l', 'pt', 'a4');

        doc.setFontSize(16);
        doc.text('Branch Wise Inspection Report', 420, 40, { align: 'center' });

        doc.setFontSize(11);
        doc.text(
    '<%= lblDistrict.InnerText %> | <%= lblBranch.InnerText %>',
    420,
    60,
    { align: 'center' }
);

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
            }
        });

        var fileName = 'Branch_Wise_Inspection_' +
            new Date().toISOString().replace(/[:T]/g, '').substring(0, 12) +
            '.pdf';

        doc.save(fileName);
        return false;
    }

</script>--%>
    <script type="text/javascript">
        function downloadPdf() {

            if (!hasRecords()) return false;

            var table = getTableWithoutRemarks();
            if (!table) {
                alert('No data available to export.');
                return false;
            }

            var jsPDF = window.jspdf.jsPDF;
            var doc = new jsPDF('l', 'pt', 'a4');

            // Get page size dynamically
            var pageWidth = doc.internal.pageSize.getWidth();
            var pageHeight = doc.internal.pageSize.getHeight();

            /* ================= HEADER ================= */

            // Title
            doc.setFontSize(16);
            doc.setFont(undefined, 'bold');
            doc.text('Branch Wise Inspection Report', pageWidth / 2, 40, { align: 'center' });

            // District | Branch
            doc.setFontSize(11);
            doc.setFont(undefined, 'normal');
            doc.text(
            '<%= lblDistrict.InnerText %> | <%= lblBranch.InnerText %>',
            pageWidth / 2,
            60,
            { align: 'center' }
        );

            // Generated on (RIGHT aligned – no cut)
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
                margin: { left: 30, right: 30 }, // prevents edge cutting
                theme: 'grid',
                styles: {
                    fontSize: 8,
                    halign: 'center',
                    valign: 'middle',
                    overflow: 'linebreak'
                },
                headStyles: {
                    fillColor: [233, 236, 239],
                    fontStyle: 'bold'
                },
                didDrawPage: function () {
                    // Footer page number
                    doc.setFontSize(9);
                    doc.text(
                        'Page ' + doc.internal.getNumberOfPages(),
                        pageWidth / 2,
                        pageHeight - 20,
                        { align: 'center' }
                    );
                }
            });

            /* ================= SAVE ================= */

            var fileName = 'Branch_Wise_Inspection_' +
                new Date().toISOString().replace(/[:T]/g, '').substring(0, 12) +
                '.pdf';

            doc.save(fileName);
            return false;
        }
    </script>

<script type="text/javascript">
    function downloadExcel() {

        if (!hasRecords()) return false;

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
        html += '</style>';
        html += '</head><body>';

        html += '<h2 style="text-align:center;">Branch Wise Inspection Report</h2>';
        html += '<div style="text-align:center;color:gray;margin-bottom:10px;">';
        html += '<%= lblDistrict.InnerText %> | <%= lblBranch.InnerText %>';
        html += '</div>';

        html += '<div style="text-align:right;font-size:12px;margin-bottom:15px;">';
        html += 'Generated on: ' + new Date().toLocaleString();
        html += '</div>';

        html += table.outerHTML;
        html += '</body></html>';

        var blob = new Blob([html], {
            type: 'application/vnd.ms-excel;charset=utf-8;'
        });

        var fileName = 'BranchWiseInspection_' + new Date().getTime() + '.xls';

        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = fileName;

        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);

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
