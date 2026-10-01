<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true"
    CodeFile="~/Region/Reports/Rpt_Pending_Bill_For_Generation.aspx.cs" Inherits="Region_Reports_Rpt_Pending_Bill_For_Generation" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <link href="../Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        /* === Fieldset & Legend Cleanup === */
        fieldset {
            border: 2px solid #2095A1;
            border-radius: 6px;
            padding: 15px 20px 25px 20px;
            margin: 15px 0;
            background-color: #f9f9f9;
            position: relative;
        }

        legend {
            font-size: 18px;
            font-weight: bold;
            color: #0b5a5e;
            border: 2px solid #2095A1;
            border-radius: 10px;
            background: #fff;
            padding: 4px 12px;
            width: auto;
        }

        /* === Buttons === */
        .btn-export {
            margin-right: 8px;
            font-weight: 600;
        }

        /* === GridView Styling === */
        .table th {
            text-align: center;
            background-color: #cfe2f3;
        }

        .table td {
            color: #000;
        }

        /* === Pagination Cleanup === */
        .GridPager {
            text-align: center !important;
        }

            .GridPager table {
                margin: 10px auto;
            }

            .GridPager a,
            .GridPager span {
                display: inline-block;
                padding: 4px 10px;
                margin: 2px;
                border-radius: 5px;
                border: 1px solid #007bff;
                color: #007bff;
                text-decoration: none;
            }

            .GridPager span {
                background-color: #007bff;
                color: #fff;
            }

        /* === Print View Cleanup (Hide Footer + Pager) === */
        @media print {
            .btn, .GridPager, tfoot, .grid-footer, tr:last-child {
                display: none !important;
                visibility: hidden !important;
            }

            body {
                margin: 0;
            }

            table {
                border-collapse: collapse !important;
                width: 100%;
            }

            th, td {
                border: 1px solid black !important;
                padding: 4px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend>Pending Bill For Generation</legend>

            <div class="mb-2">
                <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-success btn-export" Text="Export to Excel" OnClick="btnExportExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary btn-export" Text="Print" OnClientClick="return printGrid();" />
            </div>

            <div class="table-responsive">
                <asp:GridView ID="grdbill" runat="server" CssClass="table table-bordered table-hover"
                    AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true" PageSize="20"
                    OnPageIndexChanging="grdbill_PageIndexChanging" OnRowCommand="grdbill_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# ((grdbill.PageIndex * grdbill.PageSize) + Container.DataItemIndex + 1) %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        <asp:BoundField DataField="Branch" HeaderText="Branch Name" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                        <asp:BoundField DataField="FinalBillNotGenerated" HeaderText="Final Bill Not Generated" />
                        <asp:BoundField DataField="FinalBillAmountNotGenerated" HeaderText="Final Bill Amount Not Generated" />
                        <%--<asp:BoundField DataField="TotalBillnotsubmittedtoMPSCSC" HeaderText="Total Bill not submitted to MPSCSC" />--%>
                        <asp:TemplateField HeaderText="Total Bill not submitted to MPSCSC">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkTotalBillNotSubmitted" runat="server"
                                    Text='<%# Eval("TotalBillnotsubmittedtoMPSCSC") %>'
                                    CommandName="ViewDetails"
                                    CommandArgument='<%# Eval("Godown_ID") %>'
                                    ForeColor="Blue"
                                    Font-Bold="true"
                                    ToolTip="Click to view bill details"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TotalBillAmountnotsubmittedtoMPSCSC" HeaderText="Total Bill Amount not submitted to MPSCSC" />
                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                </asp:GridView>
            </div>
        </fieldset>
    </div>

    <script type="text/javascript">
        // 🖨 Print functionality for GridView
        function printGrid() {
            var grid = document.getElementById('<%= grdbill.ClientID %>');
            var printWindow = window.open('', '', 'height=700,width=900');
            printWindow.document.write('<html><head><title>Pending Bill For Generation</title>');
            printWindow.document.write('<style>table{border-collapse:collapse;width:100%;}th,td{border:1px solid black;padding:6px;text-align:center;}th{background:#cfe2f3;}tfoot,tr:last-child{display:none;}</style>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<h3 style="text-align:center;">Pending Bill For Generation</h3>');
            printWindow.document.write(grid.outerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            return false;
        }
    </script>
</asp:Content>
