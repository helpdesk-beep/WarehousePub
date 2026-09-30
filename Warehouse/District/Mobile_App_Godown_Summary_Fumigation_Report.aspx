<%@ Page Title="Godown Summary Fumigation Report"
    Language="C#"
    MasterPageFile="~/MasterPage/CollectorMasterPage.master"
    AutoEventWireup="true"
    CodeFile="Mobile_App_Godown_Summary_Fumigation_Report.aspx.cs"
    Inherits="District_Mobile_App_Godown_Summary_Fumigation_Report" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"
    rel="stylesheet" />

<style>

.report-box{
    background:#fff;
    padding:15px;
    border-radius:5px;
    box-shadow:0 2px 8px rgba(0,0,0,.15);
}

.Grid{
    width:100%;
    border-collapse:collapse;
}

.Grid th{
    background:#1a5276 !important;
    color:#fff !important;
    text-align:center;
    border:1px solid #000;
    font-size:11px;
    padding:5px;
}

.Grid td{
    border:1px solid #ccc;
    font-size:11px;
    text-align:center;
    padding:5px;
}

.footer-style td{
    background:#f2f2f2 !important;
    font-weight:bold;
}

@media print{

    @page{
        size:A4 landscape;
        margin:8mm;
    }

    .no-print{
        display:none !important;
    }

    .Grid thead{
        display:table-header-group;
    }
}
</style>

<script type="text/javascript">

    function PrintGrid() {

        var printContents =
            document.getElementById("printArea").cloneNode(true);

        var links = printContents.getElementsByTagName("a");

        while (links.length > 0) {

            var txt =
                document.createTextNode(links[0].innerText);

            links[0].parentNode.replaceChild(txt, links[0]);
        }

        var WinPrint =
            window.open('', '', 'width=1400,height=900');

        WinPrint.document.write('<html><head>');
        WinPrint.document.write('<title>Godown Summary Fumigation Report</title>');
        WinPrint.document.write('<style>');
        WinPrint.document.write('@page{size:A4 landscape;margin:8mm;}');
        WinPrint.document.write('table{width:100%;border-collapse:collapse;}');
        WinPrint.document.write('th,td{border:1px solid #000;padding:4px;text-align:center;}');
        WinPrint.document.write('</style>');
        WinPrint.document.write('</head><body>');
        WinPrint.document.write(printContents.innerHTML);
        WinPrint.document.write('</body></html>');
        WinPrint.document.close();

        setTimeout(function () {
            WinPrint.print();
        }, 500);
    }
</script>

<div class="container-fluid mt-2">

    <div class="text-right mb-2 no-print">

        

        <asp:Button ID="btnExport"
            runat="server"
            Text="Export To Excel"
            CssClass="btn btn-success btn-sm"
            OnClick="btnExport_Click" />

        <asp:Button ID="btnPrint"
            runat="server"
            Text="Print / PDF"
            CssClass="btn btn-danger btn-sm"
            OnClientClick="PrintGrid();return false;" />

    </div>

    <div id="printArea" class="report-box">

        <h3 class="text-center text-primary">
            M.P. Warehousing & Logistics Corporation
        </h3>

        <h5 class="text-center">
            Godown Summary Fumigation Report
        </h5>

        <div class="text-right mb-2">

            Date :
            <asp:Label ID="lblDate" runat="server"></asp:Label>

            &nbsp;&nbsp;&nbsp;

            Total Records :
            <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>

        </div>

        <asp:GridView ID="gvReport"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered Grid"
            ShowFooter="True"
            EmptyDataText="No Record Found"
            OnRowDataBound="gvReport_RowDataBound">

            <Columns>

                <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>

				<asp:BoundField DataField="Godown_Name"
                    HeaderText="Godown Name" /> 
					
                <asp:BoundField DataField="Total_Stack"
                    HeaderText="Total Stack" />

                <asp:BoundField DataField="Financial_year"
                    HeaderText="Financial Year" />

                <asp:BoundField DataField="Months"
                    HeaderText="Month" />

                <asp:BoundField DataField="Commodity_Name"
                    HeaderText="Commodity" />

                <asp:BoundField DataField="Fumigation_Date"
                    HeaderText="Fumigation Date"
                    DataFormatString="{0:dd-MM-yyyy}" />

                <asp:BoundField DataField="Fumigation_Status"
                    HeaderText="Fumigation Status" />

                <asp:BoundField DataField="MedicineBrand"
                    HeaderText="Medicine Brand" />

                <asp:BoundField DataField="Opened"
                    HeaderText="Opened" />

                <asp:BoundField DataField="Open_Status"
                    HeaderText="Open Status" />

                <asp:BoundField DataField="Open_Date"
                    HeaderText="Open Date"
                    DataFormatString="{0:dd-MM-yyyy}" />

            </Columns>

            <FooterStyle CssClass="footer-style" />

        </asp:GridView>

    </div>

</div>

</asp:Content>