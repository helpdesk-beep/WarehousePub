<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="~/WarehouseLevel/Nafed_Godown_Bill_Wise_Payment_Status.aspx.cs" Inherits="WarehouseLevel_Nafed_Godown_Bill_Wise_Payment_Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <link href="../../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../../assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
    <script type="text/javascript">
        function PrintDiv_det() {
            var divContents = document.getElementById("PrintDiv_Det").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
    <script type="text/javascript">
        function PrintDiv_Actual() {
            var divContents = document.getElementById("printActualBill").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=grpendding.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }
    </script>
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= grpendding.ClientID %>');
            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();
            var prtWindow = window.open(windowUrl, windowName,
                'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
        }
    </script>
    <style>
        .report-box {
            background: white;
            border: 2px solid #2c3e50;
            border-radius: 12px;
            padding: 15px;
        }

        .report-title {
            font-size: 26px;
            font-weight: bold;
            text-align: center;
            color: #2c3e50;
        }

        .sub-title {
            text-align: center;
            font-size: 18px;
            font-weight: bold;
            margin-top: 5px;
        }

        .godown-box {
            background: #f1f7ff;
            border: 2px solid #0d6efd;
            border-radius: 10px;
            padding: 10px;
            text-align: center;
            font-size: 20px;
            font-weight: bold;
            margin-top: 10px;
            margin-bottom: 15px;
        }

        .table thead th {
            background: #2c3e50 !important;
            color: white !important;
            text-align: center;
            vertical-align: middle;
        }

        .table td {
            vertical-align: middle !important;
        }

        .total-footer {
            background: #eaf5ea;
            font-weight: bold;
            font-size: 16px;
        }

        .print-header {
            text-align: center;
            margin-bottom: 10px;
        }

        @media print {
            .no-print {
                display: none;
            }
        }
    </style>
<style>
.auto-width-grid {
    width: 100%;
    table-layout: auto !important;
}

.auto-width-grid th,
.auto-width-grid td {
    white-space: nowrap;
    padding: 6px 10px;
    font-size: 13px;
}

.auto-width-grid th {
    text-align: center;
    background-color: #f1f1f1;
}

.table-responsive {
    overflow-x: auto;
}

.empty-row {
    padding: 20px;
    font-size: 18px;
    color: red;
    font-weight: bold;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="report-box">
            <!-- HEADER -->
            <div class="print-header">
                <div class="report-title">NAFED STORAGE BILL PAYMENT REPORT</div>
                <div class="sub-title">Received Nafed Bill Payment Status</div>
            </div>
            <!-- BUTTONS -->
            <div class="row no-print" style="margin-bottom: 10px;">
                <div class="col-md-6">
                    <asp:Button ID="btnPrint" runat="server" Text="🖨 Print"
                        CssClass="btn btn-primary btn-sm"
                        OnClientClick="printGrid()" />
                    <input type="button" value="⬇ Excel"
                        class="btn btn-success btn-sm" id="btnExport" />
                </div>
            </div>
            <!-- GODOWN HEADER -->
            <div class="godown-box">
                <asp:Label ID="lblGodownHeader" runat="server"></asp:Label>
            </div>
            <!-- GRID -->
            <div class="table-responsive">
                <asp:GridView ID="grpendding" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover"
                    ShowFooter="true">
<RowStyle Wrap="false" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Bill_Number" HeaderText="Storage Bill No" />
                        <asp:BoundField DataField="AmountRecivedFromNAfed" HeaderText="NAFED Amount" />
                        <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" />
                        <asp:BoundField DataField="Financial_Year" HeaderText="Fin Year" />
                        <asp:BoundField DataField="Month" HeaderText="Month" />
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />
                        <asp:BoundField DataField="RentBillNumber" HeaderText="Rent Bill No" />
                        <asp:BoundField DataField="TotalRentBillAmount" HeaderText="Rent Amount" />
                        <asp:BoundField DataField="NoofBillDeductionbyBM" HeaderText="BM Deduction" />
                        <asp:BoundField DataField="NoofBillSenttoRMbyBM" HeaderText="Sent RM" />
                        <asp:BoundField DataField="NoofBillApprovedbyRM" HeaderText="Approved RM" />
                        <asp:BoundField DataField="NoofBillPassedbyRM" HeaderText="Passed RM" />
                        <asp:BoundField DataField="NoofBillAmountPassedbyRM" HeaderText="Passed Amount" />
                        <asp:BoundField DataField="TDS_Detuction_Amount" HeaderText="TDS" />
                        <asp:BoundField DataField="TotalDeductionbyRM" HeaderText="Other Deduction" />
                        <asp:TemplateField HeaderText="Pay To Owner">
                            <ItemTemplate>
                                <span style="font-weight: bold; color: green;">
                                    <%# Eval("PaytogodownOwner") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

