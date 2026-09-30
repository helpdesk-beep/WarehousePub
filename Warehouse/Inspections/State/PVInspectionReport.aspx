<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Inspections/State/PVInspectionReport.aspx.cs" Inherits="Inspections_State_PVInspectionReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PV / Inspection Report</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            font-size: 12px;
            background: #fff;
        }

        table {
            border-collapse: collapse;
            width: 100%;
            font-size: 12px;
        }

        th, td {
            border: 1px solid #000;
            padding: 6px;
            text-align: center;
            vertical-align: middle;
        }

        th {
            font-weight: bold;
            background-color: #f2f2f2;
        }

        td {
            font-weight: 500;
        }

        .remark-wrap {
            white-space: normal;
            word-break: break-word;
            text-align: left;
            min-width: 220px;
        }

        .summary-row td {
            font-weight: bold;
            background-color: #e9ecef;
            text-align: center;
        }

        .green { color: green; font-weight: bold; }
        .red { color: red; font-weight: bold; }

        .report-title {
            font-size: 16px;
            font-weight: bold;
            text-align: center;
            margin-bottom: 5px;
        }

        .report-date {
            text-align: right;
            font-size: 11px;
            margin-bottom: 10px;
        }

        .export-bar {
            text-align: right;
            margin-bottom: 8px;
        }
    </style>
</head>

<body>
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" />

    <div class="container-fluid">

        <div class="export-bar">
            <button type="button" class="btn btn-danger btn-sm" onclick="exportPDF()">Export PDF</button>
            <button type="button" class="btn btn-success btn-sm" onclick="exportExcel()">Export Excel</button>
        </div>
        <div class="row mb-3 align-items-end">

    <div class="col-md-3">
        <label class="font-weight-bold">Branch</label>
        <asp:DropDownList ID="ddlBranch" runat="server"
            CssClass="form-control form-control-sm"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
        </asp:DropDownList>
    </div>

    <div class="col-md-3">
        <label class="font-weight-bold">Godown</label>
        <asp:DropDownList ID="ddlGodown" runat="server"
            CssClass="form-control form-control-sm"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged"
            >
        </asp:DropDownList>
    </div>

    <div class="col-md-2">
        <asp:Button ID="btnFilter" runat="server"
            Text="Apply Filter"
            CssClass="btn btn-primary btn-sm"
            OnClick="btnFilter_Click" />
    </div>

</div>

        <div id="reportArea">

            <div class="report-title">PV / Inspection Report</div>
         <div class="report-date">
    Generated on :
    <asp:Label ID="lblGeneratedOn" runat="server"></asp:Label>
</div>

           <asp:GridView ID="gvReport" runat="server"
    AutoGenerateColumns="False"
    CssClass="table table-bordered table-sm"
    OnRowDataBound="gvReport_RowDataBound">

    <Columns>

        <asp:TemplateField HeaderText="S.No">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField HeaderText="Godown ID" DataField="Godown_ID" />
        <asp:BoundField HeaderText="Godown Name" DataField="Godown_Name" />
        <asp:BoundField HeaderText="Godown Capacity" DataField="Godown_Capacity" />

        <asp:BoundField HeaderText="Stack ID" DataField="Stack_ID" />
        <asp:BoundField HeaderText="Stack Name" DataField="Stack_Name" />

        <asp:BoundField HeaderText="Depositer Name" DataField="Depositer_Name" />
        <asp:BoundField HeaderText="Crop Year" DataField="Crop_Year" />
        <asp:BoundField HeaderText="Commodity Name" DataField="Commodity_Name" />

        <asp:BoundField HeaderText="Bags (Online)" DataField="BagBalance" />
        <asp:BoundField HeaderText="Weight" DataField="Avail_Weight" />
        <asp:BoundField HeaderText="Spillage Bags" DataField="Spillage_Bags" />
        <asp:BoundField HeaderText="Bags(PV)" DataField="TotalBags_AsPerPV" />

        <asp:BoundField HeaderText="Difference Bags(Online-PV)" DataField="Difference_Bags" />
        <asp:BoundField HeaderText="PV Date" DataField="Godown_Submit_date" />

        <asp:TemplateField HeaderText="Remark">
            <ItemTemplate>
                <div class="remark-wrap">
                    <%# Eval("Remark") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>

        </div>
    </div>
</form>

<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf-autotable/3.5.29/jspdf.plugin.autotable.min.js"></script>

<script>
    function exportPDF() {

        var gv = document.getElementById('<%= gvReport.ClientID %>');

        if (!gv || gv.rows.length <= 1) {
            alert('No data to export');
            return;
        }

        const { jsPDF } = window.jspdf;

        var doc = new jsPDF('l', 'pt', 'a4');

        doc.setFontSize(16);
        doc.text("PV / Inspection Report", 400, 30);

        doc.setFontSize(10);
        doc.text("Generated on : <%= DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") %>", 650, 50);

    doc.autoTable({
        html: '#' + '<%= gvReport.ClientID %>',
        startY: 70,
        styles: {
            fontSize: 7,
            cellPadding: 3
        },
        headStyles: {
            fillColor: [220, 220, 220],
            fontStyle: 'bold'
        },
        theme: 'grid'
    });

        doc.save("PV_Inspection_Report.pdf");
    }

</script>

<script>
    function exportExcel() {

        var gv = document.getElementById('<%= gvReport.ClientID %>');

    if (!gv || gv.rows.length <= 1) {
        alert('No data to export');
        return;
    }

    var html =
        "<html xmlns:o='urn:schemas-microsoft-com:office:office' " +
        "xmlns:x='urn:schemas-microsoft-com:office:excel'>" +
        "<head><meta charset='UTF-8'></head><body>";

    html += "<h3 style='text-align:center;'>PV / Inspection Report</h3>";
    html += "<div style='text-align:right;'>Generated on : <%= DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") %></div><br/>";

    html += gv.outerHTML;

    html += "</body></html>";

    var blob = new Blob([html], { type: "application/vnd.ms-excel" });

    var link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.download = "PV_Inspection_Report.xls";
    link.click();
}</script>

</body>
</html>
