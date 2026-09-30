<%@ Page Title="" Language="C#"
    MasterPageFile="~/MasterPage/StateMaster.master"
    AutoEventWireup="true"
    CodeFile="~/StatePages/Godown_Wise_Report_Sub_Total.aspx.cs"
    Inherits="StatePages_Godown_Wise_Report_Sub_Total" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="Server">

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript" src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
     <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
     <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    
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

        body {
            background: #f5f7fb;
        }

        .main-box {
            background: #ffffff;
            border-radius: 10px;
            padding: 15px;
            box-shadow: 0px 0px 10px #dcdcdc;
        }

        .report-title {
            background: #0d6efd;
            color: white;
            padding: 12px;
            border-radius: 8px;
            font-size: 24px;
            font-weight: bold;
            text-align: center;
            margin-bottom: 15px;
        }

        .filter-box {
            background: #f8f9fa;
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 15px;
            border: 1px solid #dee2e6;
        }

        .grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 11px;
        }

        .grid th {
            background: #0d6efd;
            color: white;
            padding: 5px;
            border: 1px solid #dcdcdc;
            text-align: center;
            white-space: normal !important;
            word-wrap: break-word;
            min-width: 70px;
        }

        .grid td {
            padding: 4px;
            border: 1px solid #dcdcdc;
            white-space: normal !important;
            word-wrap: break-word;
        }

        .grid tr:nth-child(even) {
            background: #f9f9f9;
        }

        .table-responsive {
            overflow-x: auto;
        }


        .subtotal {
            background: #b0c4e1 !important;
            font-weight: bold;
            text-align:center;
        }

        .grandtotal {
            background: #b6d7a8 !important;
            font-weight: bold;
        }

        .right {
            text-align: right;
        }

        .center {
            text-align: center;
        }

        .btn-custom {
            min-width: 140px;
            font-weight: bold;
        }

        .select2-container {
            width: 100% !important;
        }

        @media print {

            .noprint {
                display: none !important;
            }
        }

    </style>

    <script type="text/javascript">
        function PrintPanel() {

            var panel = document.getElementById('<%= pnlData.ClientID %>');

            var printWindow = window.open('', '', 'height=700,width=1200');

            printWindow.document.write('<html><head><title>Godown Wise Report</title>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');

            printWindow.document.close();
            printWindow.focus();

            setTimeout(function () {
                printWindow.print();
                printWindow.close();
            }, 500);

            return false;
        }
    </script>

</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">

    <div class="mt-2">

        <div class="main-box">

            <div class="report-title">
                Godown Wise Report With Sub Total
            </div>

            <div class="filter-box noprint">

                <div class="row">

                    <div class="col-md-3 mb-3">

                        <label>
                            Region
                        </label>

                        <asp:DropDownList ID="ddlRegion"
                            runat="server"
                            CssClass="form-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>

                    <div class="col-md-3 mb-3">

                        <label>
                            District
                        </label>

                        <asp:DropDownList ID="ddlDistrict"
                            runat="server"
                            CssClass="form-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>

                    <div class="col-md-3 mb-3">

                        <label>
                            Branch
                        </label>

                        <asp:DropDownList ID="ddlDepot"
                            runat="server"
                            CssClass="form-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>

                    <div class="col-md-3 mb-3">

                        <label>
                            Godown
                        </label>

                        <asp:DropDownList ID="ddlGodown"
                            runat="server"
                            CssClass="form-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>

                </div>

                <div class="row">

                    <div class="col-md-12 text-end">

                        <asp:Button ID="btnExcel"
                            runat="server"
                            Text="Export Excel"
                            CssClass="btn btn-success btn-custom"
                            OnClick="btnExcel_Click" />

                        <asp:Button ID="btnPrint"
                            runat="server"
                            Text="Print"
                            CssClass="btn btn-danger btn-custom"
                            OnClientClick="return PrintPanel();" />

                    </div>

                </div>

            </div>

            <asp:Panel ID="pnlData"
                runat="server">

                <div class="table-responsive">

                    <asp:GridView ID="gvReport"
    runat="server"
    AutoGenerateColumns="false"
    CssClass="grid table table-bordered table-striped"
    ShowFooter="true"
    OnRowDataBound="gvReport_RowDataBound">

    <%--<Columns>

        <asp:BoundField DataField="S.No"
            HeaderText="S.No">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:BoundField DataField="Regionnm"
            HeaderText="Region" />

        <asp:BoundField DataField="District_Name"
            HeaderText="District" />

        <asp:BoundField DataField="DepotName"
            HeaderText="Branch" />

        <asp:BoundField DataField="Godown_Name"
            HeaderText="Godown Name" />

        <asp:BoundField DataField="Godown_Capacity"
            HeaderText="Capacity"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2015-16"
            HeaderText="2015-16"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2016-17"
            HeaderText="2016-17"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2017-18"
            HeaderText="2017-18"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2018-19"
            HeaderText="2018-19"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2019-20"
            HeaderText="2019-20"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2020-21"
            HeaderText="2020-21"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2021-22"
            HeaderText="2021-22"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2022-23"
            HeaderText="2022-23"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2023-24"
            HeaderText="2023-24"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2024-25"
            HeaderText="2024-25"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2025-26"
            HeaderText="2025-26"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="2026-27"
            HeaderText="2026-27"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>

        <asp:BoundField DataField="Total"
            HeaderText="Total"
            DataFormatString="{0:N2}">
            <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
        
        <asp:BoundField DataField="Depositor_Name"
            HeaderText="Depositor Name" />
        
        <asp:BoundField DataField="Commodity_Name"
            HeaderText="Commodity Name" />

    </Columns>--%>

                        <Columns>

    <asp:BoundField DataField="S.No" HeaderText="S.No" />

    <asp:BoundField DataField="Regionnm"
        HeaderText="Region" />

    <asp:BoundField DataField="District_Name"
        HeaderText="District" />

    <asp:BoundField DataField="DepotName"
        HeaderText="Branch" />

    <asp:BoundField DataField="Godown_Name"
        HeaderText="Godown Name" />

    <asp:BoundField DataField="Depositor_Name"
        HeaderText="Depositor Name" />

    <asp:BoundField DataField="Commodity_Name"
        HeaderText="Commodity Name" />

    <asp:BoundField DataField="Godown_Capacity"
        HeaderText="Capacity"
        DataFormatString="{0:N2}">
        <ItemStyle HorizontalAlign="Right" />
    </asp:BoundField>

    <asp:BoundField DataField="2015-16" HeaderText="2015-16" DataFormatString="{0:N2}" />
    <asp:BoundField DataField="2016-17" HeaderText="2016-17" DataFormatString="{0:N2}" />
    <asp:BoundField DataField="2017-18" HeaderText="2017-18" DataFormatString="{0:N2}" />
    <asp:BoundField DataField="2018-19" HeaderText="2018-19" DataFormatString="{0:N2}" />
    <asp:BoundField DataField="2019-20" HeaderText="2019-20" DataFormatString="{0:N2}" />
    <asp:BoundField DataField="2020-21" HeaderText="2020-21" DataFormatString="{0:N2}" />
    <asp:BoundField DataField="2021-22" HeaderText="2021-22" DataFormatString="{0:N2}" />
    <asp:BoundField DataField="2022-23" HeaderText="2022-23" DataFormatString="{0:N2}" />
    <asp:BoundField DataField="2023-24" HeaderText="2023-24" DataFormatString="{0:N2}" />
    <asp:BoundField DataField="2024-25" HeaderText="2024-25" DataFormatString="{0:N2}" />
    <asp:BoundField DataField="2025-26" HeaderText="2025-26" DataFormatString="{0:N2}" />
    <asp:BoundField DataField="2026-27" HeaderText="2026-27" DataFormatString="{0:N2}" />

    <asp:BoundField DataField="Total"
        HeaderText="Total"
        DataFormatString="{0:N2}" />

</Columns>

</asp:GridView>            

                </div>

            </asp:Panel>

        </div>

    </div>

</asp:Content>