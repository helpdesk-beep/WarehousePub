<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master"
    AutoEventWireup="true"
    CodeFile="Godown_Wise_Capacity_Classification.aspx.cs"
    Inherits="StatePages_Godown_Wise_Capacity_Classification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

     <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
 <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
 <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
     <script type="text/javascript">
         $(function () {
             $("[id*=ddlRegion]").select2();
         });
     </script>
<script type="text/javascript">
    $(function () {
        $("[id*=ddlDistrict]").select2();
    });
</script>
<script type="text/javascript">
    $(function () {
        $("[id*=ddlDepot]").select2();
    });
</script>
<script type="text/javascript">
    $(function () {
        $("[id*=ddlGodown]").select2();
    });
</script>

    <style type="text/css">

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
            background: #0d6efd;
        }

        .btnPrint {
            background: #dc3545;
        }

        .filterddl {
            width: 180px;
            padding: 6px;
        }

        .searchbox {
            width: 100%;
            padding: 7px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
        }

        .grid th {
            background: #0d6efd;
            color: white;
            padding: 8px;
            border: 1px solid #d0d0d0;
            text-align: center;
        }

        .grid td {
            padding: 7px;
            border: 1px solid #d0d0d0;
        }

        .grid tr:nth-child(even) {
            background: #f5f5f5;
        }

        .subtotal {
            background: #ffe8a3 !important;
            font-weight: bold;
        }

        .grandtotal {
            background: #c3f3cb !important;
            font-weight: bold;
        }

        @media print {

            .noprint {
                display: none;
            }

            body {
                -webkit-print-color-adjust: exact !important;
            }

            .grid th {
                background: #0d6efd !important;
                color: white !important;
            }
        }

    </style>

    <script type="text/javascript">

        function PrintPanel() {

            var panel = document.getElementById('<%= pnlData.ClientID %>');

            var printWindow = window.open('', '', 'height=700,width=1200');

            printWindow.document.write('<html><head><title>Print</title></head><body>');

            printWindow.document.write(panel.innerHTML);

            printWindow.document.write('</body></html>');

            printWindow.document.close();

            setTimeout(function () {

                printWindow.print();

            }, 500);

            return false;
        }

        function SearchGrid() {

            var input = document.getElementById("txtSearch");

            var filter = input.value.toLowerCase();

            var grid = document.getElementById('<%= gvReport.ClientID %>');

            var rows = grid.getElementsByTagName("tr");

            for (var i = 1; i < rows.length; i++) {

                var txtValue = rows[i].textContent || rows[i].innerText;

                if (txtValue.toLowerCase().indexOf(filter) > -1) {

                    rows[i].style.display = "";

                }
                else {

                    rows[i].style.display = "none";
                }
            }
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-box">

        <div class="title">
            Godown Wise Capacity Classification Report
        </div>

        <div class="noprint">

            <table style="width:100%; margin-bottom:10px;">

                <tr>

                    <td>
                        <asp:DropDownList ID="ddlRegion"
                            runat="server"
                            CssClass="filterddl"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlDistrict"
                            runat="server"
                            CssClass="filterddl"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlDepot"
                            runat="server"
                            CssClass="filterddl"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>

                    <td>
                        <asp:DropDownList ID="ddlGodown"
                            runat="server"
                            CssClass="filterddl"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>

                </tr>

                <tr>

                    <td colspan="5" style="padding-top:10px;">

                        <input type="text"
                            id="txtSearch"
                            class="searchbox"
                            placeholder="Search in Grid..."
                            onkeyup="SearchGrid()" />

                    </td>

                    <td colspan="2" align="right" style="padding-top:10px;">

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

                    </td>

                </tr>

            </table>

        </div>

        <asp:Panel ID="pnlData" runat="server">

            <div style="overflow:auto;">

                <asp:GridView ID="gvReport"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="grid"
                    ShowFooter="true"
                    OnRowDataBound="gvReport_RowDataBound">

                    <Columns>

                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Regionnm"
                            HeaderText="Region" />

                        <asp:BoundField DataField="District_Name"
                            HeaderText="District" />

                        <asp:BoundField DataField="DepotName"
                            HeaderText="Depot Name" />

                        <asp:BoundField DataField="Godown_Name"
                            HeaderText="Godown Name" />

                        <asp:BoundField DataField="Godown_ID"
                            HeaderText="Godown ID" />

                        <asp:BoundField DataField="Godown Type"
                            HeaderText="Godown Type" />

                        <asp:BoundField DataField="Latitude"
                            HeaderText="Latitude" />

                        <asp:BoundField DataField="Longitude"
                            HeaderText="Longitude" />

                        <asp:BoundField DataField="Godown Capcity"
                            HeaderText="Capacity (MT)"
                            DataFormatString="{0:N2}" />

                        <asp:BoundField DataField="Capacity_Classification"
                            HeaderText="Capacity Classification" />

                    </Columns>

                </asp:GridView>

            </div>

        </asp:Panel>

    </div>

</asp:Content>