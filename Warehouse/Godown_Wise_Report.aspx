<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Godown_Wise_Report.aspx.cs" Inherits="Godown_Wise_Report" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Godown Wise Report With Sub Total</title>

    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <!-- Select2 -->
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script src="../assets/New/js/select2.min.js"></script>

    <script>
        $(function () {
            $("#<%=ddlRegion.ClientID%>").select2();
            $("#<%=ddlDistrict.ClientID%>").select2();
            $("#<%=ddlDepot.ClientID%>").select2();
            $("#<%=ddlGodown.ClientID%>").select2();
        });
    </script>

    <style>
        body { background:#f4f6f9; }

        .card-box {
            background:#fff;
            border-radius:10px;
            padding:15px;
            box-shadow:0 0 10px rgba(0,0,0,0.1);
        }

        .title {
            background:#0d6efd;
            color:#fff;
            padding:12px;
            text-align:center;
            font-size:20px;
            font-weight:bold;
            border-radius:8px;
            margin-bottom:15px;
        }

        .grid th {
            background:#0d6efd;
            color:#fff;
            text-align:center;
            font-size:12px;
            white-space:nowrap;
        }

        .grid td {
            font-size:11px;
            text-align:center;
        }

        .subtotal {
            background:#dbe7ff !important;
            font-weight:bold;
        }

        .grandtotal {
            background:#c6efce !important;
            font-weight:bold;
        }

        .right { text-align:right; }

        @media print {
            .noprint { display:none; }
        }
    </style>

    <script>
        function PrintPanel() {
            var panel = document.getElementById('<%= pnlData.ClientID %>');
            var w = window.open('', '', 'width=1200,height=700');
            w.document.write('<html><head></head><body>');
            w.document.write(panel.innerHTML);
            w.document.write('</body></html>');
            w.document.close();
            w.print();
            return false;
        }
    </script>
</head>

<body>
<form id="form1" runat="server">

<div class="container-fluid mt-3">

    <div class="card-box">

        <div class="title">
            Godown Wise Report With Sub Total
        </div>

        <!-- FILTERS -->
        <div class="row noprint mb-3">

            <div class="col-md-3">
                <label>Region</label>
                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-select"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" />
            </div>

            <div class="col-md-3">
                <label>District</label>
                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-select"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" />
            </div>

            <div class="col-md-3">
                <label>Branch</label>
                <asp:DropDownList ID="ddlDepot" runat="server" CssClass="form-select"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged" />
            </div>

            <div class="col-md-3">
                <label>Godown</label>
                <asp:DropDownList ID="ddlGodown" runat="server" CssClass="form-select"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged" />
            </div>

        </div>

        <div class="text-end noprint mb-2">
            <asp:Button ID="btnExcel" runat="server" Text="Excel"
                CssClass="btn btn-success" OnClick="btnExcel_Click" />

            <asp:Button ID="btnPrint" runat="server" Text="Print"
                CssClass="btn btn-danger" OnClientClick="return PrintPanel();" />
        </div>

        <!-- GRID -->
        <asp:Panel ID="pnlData" runat="server">

            <div class="table-responsive">

                <asp:GridView ID="gvReport" runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-bordered table-striped grid"
                    ShowFooter="true"
                    OnRowDataBound="gvReport_RowDataBound">

                    <Columns>

                        <asp:BoundField DataField="S.No" HeaderText="S.No" />
                        <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                        <asp:BoundField DataField="District_Name" HeaderText="District" />
                        <asp:BoundField DataField="DepotName" HeaderText="Branch" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor" />
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />

                        <asp:BoundField DataField="Godown_Capacity" HeaderText="Capacity" DataFormatString="{0:N2}" />

                        <asp:BoundField DataField="2015-16" HeaderText="2015-16" />
                        <asp:BoundField DataField="2016-17" HeaderText="2016-17" />
                        <asp:BoundField DataField="2017-18" HeaderText="2017-18" />
                        <asp:BoundField DataField="2018-19" HeaderText="2018-19" />
                        <asp:BoundField DataField="2019-20" HeaderText="2019-20" />
                        <asp:BoundField DataField="2020-21" HeaderText="2020-21" />
                        <asp:BoundField DataField="2021-22" HeaderText="2021-22" />
                        <asp:BoundField DataField="2022-23" HeaderText="2022-23" />
                        <asp:BoundField DataField="2023-24" HeaderText="2023-24" />
                        <asp:BoundField DataField="2024-25" HeaderText="2024-25" />
                        <asp:BoundField DataField="2025-26" HeaderText="2025-26" />
                        <asp:BoundField DataField="2026-27" HeaderText="2026-27" />

                        <asp:BoundField DataField="Total" HeaderText="Total" />

                    </Columns>

                </asp:GridView>

            </div>

        </asp:Panel>

    </div>
</div>

</form>
</body>
</html>
