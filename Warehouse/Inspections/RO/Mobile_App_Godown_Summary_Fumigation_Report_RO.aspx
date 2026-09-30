<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="Mobile_App_Godown_Summary_Fumigation_Report_RO.aspx.cs" Inherits="Inspections_RO_Mobile_App_Godown_Summary_Fumigation_Report_RO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
        color: White;
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
        color: White;
        padding: 8px;
        border: 1px solid #ccc;
        text-align: center;
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
        color: White;
        font-weight: bold;
        border-radius: 4px;
        cursor: pointer;
    }

    .btnExcel {
        background: #198754;
    }

    .btnPrint {
        background: #dc3545;
    }

</style>

<script type="text/javascript">

    function SearchGrid() {

        var input = document.getElementById("txtSearch");
        var filter = input.value.toUpperCase();

        var table = document.getElementById('<%= gvReport.ClientID %>');
        var tr = table.getElementsByTagName("tr");

        for (var i = 1; i < tr.length; i++) {

            var td = tr[i].getElementsByTagName("td");
            var showRow = false;

            for (var j = 0; j < td.length; j++) {

                if (td[j]) {

                    var txtValue = td[j].textContent || td[j].innerText;

                    if (txtValue.toUpperCase().indexOf(filter) > -1) {

                        showRow = true;
                        break;
                    }
                }
            }

            tr[i].style.display = showRow ? "" : "none";
        }
    }

    function PrintPanel() {

        var panel = document.getElementById('<%= pnlData.ClientID %>');

        var printWindow = window.open('', '', 'height=700,width=1400');

        printWindow.document.write(panel.innerHTML);

        printWindow.document.close();

        setTimeout(function () {
            printWindow.print();
        }, 500);

        return false;
    }

</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

```
<div class="main-box">

    <div style="margin-bottom:10px;">

        <input type="text"
            id="txtSearch"
            placeholder="Search Here..."
            onkeyup="SearchGrid()"
            style="width:300px;padding:8px;" />

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
        Branch Summary Fumigation Report
    </div>

    <asp:Panel ID="pnlData" runat="server">

        <asp:GridView ID="gvReport"
    runat="server"
    AutoGenerateColumns="false"
    CssClass="grid"
    ShowFooter="true"
    OnRowDataBound="gvReport_RowDataBound">

    <Columns>

        <asp:TemplateField HeaderText="S.No">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>

       <asp:TemplateField HeaderText="Godown Name">
    <ItemTemplate>

        <asp:HyperLink ID="lnkGodown"
            runat="server"
            Text='<%# Eval("Godown_Name") %>'
            NavigateUrl='<%# "Mobile_App_Stack_Wise_Fumigation_Report_For_RO.aspx?GodownID="
                + Eval("Godown_ID")
                + "&Commodity_Id="
                + Eval("Commodity_Id") %>'
            Target="_blank">
        </asp:HyperLink>

    </ItemTemplate>
</asp:TemplateField>

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

</asp:GridView>

    </asp:Panel>

</div>

</asp:Content>
