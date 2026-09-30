<%@ Page Title="" Language="C#"
MasterPageFile="~/Inspections/Masters/Inspection_RO.master"
AutoEventWireup="true"
CodeFile="~/Inspections/RO/Mobile_App_District_Wise_Moisture_Report_For_RO.aspx.cs"
Inherits="Inspections_State_Mobile_App_District_Wise_Moisture_Report_For_HO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">

    body {
        font-family: Arial;
    }

    .main-box {
        width: 99%;
        margin: 10px auto;
        background: #fff;
        border: 1px solid #dcdcdc;
        border-radius: 8px;
        padding: 10px;
        box-shadow: 0px 0px 8px #dcdcdc;
    }

    .title {
        background: #0d6efd;
        color: white;
        padding: 12px;
        text-align: center;
        font-size: 22px;
        font-weight: bold;
        border-radius: 5px;
        margin-bottom: 10px;
    }

    .grid {
        width: 100%;
        border-collapse: collapse;
        font-size: 12px;
    }

    .grid th {
        background: #0d6efd;
        color: white;
        padding: 8px;
        border: 1px solid #ccc;
        text-align: center;
        white-space: normal;
        line-height: 18px;
    }

    .grid td {
        padding: 6px;
        border: 1px solid #ccc;
        text-align: center;
    }

    .grid tr:nth-child(even) {
        background: #f5f5f5;
    }

    .grid tr:hover {
        background: #eef5ff;
    }

    .totalRow {
        background: #ffe9b3 !important;
        font-weight: bold;
    }

    .btn {
        padding: 8px 18px;
        border: none;
        color: white;
        font-weight: bold;
        border-radius: 4px;
        cursor: pointer;
        margin-left: 5px;
    }

    .btnExcel {
        background: #198754;
    }

    .btnPrint {
        background: #dc3545;
    }

    @media print {

        .noprint {
            display: none;
        }

        .grid th {
            background-color: #0d6efd !important;
            color: white !important;
            -webkit-print-color-adjust: exact;
        }

        .title {
            background-color: #0d6efd !important;
            color: white !important;
            -webkit-print-color-adjust: exact;
        }
    }

</style>

<script type="text/javascript">

    function PrintPanel() {

        var panel = document.getElementById('<%= pnlData.ClientID %>');

        var printWindow = window.open('', '', 'height=700,width=1400');

        printWindow.document.write('<html><head><title>Print</title>');

        printWindow.document.write('<style>');
        printWindow.document.write('body{font-family:Arial;}');
        printWindow.document.write('table{width:100%;border-collapse:collapse;font-size:12px;}');
        printWindow.document.write('th{background:#0d6efd;color:white;border:1px solid #ccc;padding:8px;text-align:center;}');
        printWindow.document.write('td{border:1px solid #ccc;padding:6px;text-align:center;}');
        printWindow.document.write('</style>');

        printWindow.document.write('</head><body>');

        printWindow.document.write(panel.innerHTML);

        printWindow.document.write('</body></html>');

        printWindow.document.close();

        setTimeout(function () {

            printWindow.focus();

            printWindow.print();

            printWindow.close();

        }, 500);

        return false;
    }

</script>

</asp:Content>

<asp:Content ID="Content2"
ContentPlaceHolderID="ContentPlaceHolder1"
Runat="Server">

<div class="main-box">

    <div class="noprint"
        style="margin-bottom:10px; text-align:right;">

        <asp:Button ID="btnExcel"
            runat="server"
            Text="Export Excel"
            CssClass="btn btnExcel"
            OnClick="btnExcel_Click" />

        <asp:Button ID="btnPrint"
            runat="server"
            Text="Print"
            CssClass="btn btnPrint"
            OnClientClick="return PrintPanel();" />

    </div>

    <div class="title">
        Wheat-PSS 2026-27 District Wise Moisture Report As On <%: DateTime.Now.ToString("dd/MM/yyyy") %>
    </div>

    <asp:Panel ID="pnlData" runat="server">

        <div style="overflow:auto;">

            <asp:GridView ID="gvReport"
                runat="server"
                AutoGenerateColumns="false"
                CssClass="grid"
                ShowFooter="true"
                OnRowDataBound="gvReport_RowDataBound">

                <Columns>

                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="जिले का नाम">
                        <ItemTemplate>

                            <a href='Mobile_App_Branch_Wise_Moisture_Report_For_RO.aspx?DistrictId=<%# Eval("District_Id") %>'
                                target="_blank"
                                style="color:#0d6efd;
                                font-weight:bold;
                                text-decoration:none;">

                                <%# Eval("District Name") %>

                            </a>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Total_Stack"
                        HeaderText="कुल स्टेको की संख्या" />

                    <asp:BoundField DataField="Prev Stack"
                        HeaderText="पूर्व दिनांक तक नमी अंकित किये गये स्टेको की संख्या" />

                    <asp:BoundField DataField="Today Stack"
                        HeaderText="आज दिनांक तक नमी अंकित किये गये स्टेको की संख्या" />

                    <asp:BoundField DataField="Total Moisture Stack"
                        HeaderText="कुल नमी अंकित स्टेको की संख्या" />

                    <asp:BoundField DataField="Pending Stack"
                        HeaderText="शेष स्टेको की संख्या जिसकी नमी अंकित किया जाना है" />

                    <asp:BoundField DataField="Prev Moisture Sent_DM"
                        HeaderText="पूर्व दिनांक तक MPSCSC को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="Today Moisture Sent_DM"
                        HeaderText="आज दिनांक को MPSCSC को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="Total Moisture Sent_DM"
                        HeaderText="कुल MPSCSC को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="Prev Moisture Submit To FCI"
                        HeaderText="पूर्व दिनांक तक FCI को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="Today Moisture Submit To FCI"
                        HeaderText="आज दिनांक को FCI को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="Total Moisture Submit To FCI"
                        HeaderText="कुल FCI को प्रेषित स्टेको की संख्या" />

                    <asp:BoundField DataField="FCI Inspected Stack"
                        HeaderText="FCI द्वारा Cross Check किये गये स्टेको की संख्या" />

                    <asp:BoundField DataField="FCI Inspected Date"
                        HeaderText="FCI Inspection Date"
                        DataFormatString="{0:dd-MM-yyyy}" />

                </Columns>

            </asp:GridView>

        </div>

    </asp:Panel>

</div>

</asp:Content>