<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Region/Reports/Pending_WB_For_Updation.aspx.cs" Inherits="Region_Reports_Pending_WB_For_Updation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Weighbridge Null Name Report</title>
    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        function PrintGrid() {
            var grid = document.getElementById('<%= gvReport.ClientID %>');
            var win = window.open('', '', 'height=700,width=900');
            win.document.write('<html><head><title>Print</title>');
            win.document.write('<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">');
            win.document.write('</head><body>');
            win.document.write('<h4 class="text-center my-3">Weighbridge Report (WB_Name is NULL)</h4>');
            win.document.write(grid.outerHTML);
            win.document.write('</body></html>');
            win.document.close();
            win.print();
        }
    </script>
    <style type="text/css">
        .table tfoot tr td {
            font-weight: bold;
            background-color: #f8f9fa;
        }

        .subtotal-row td {
            font-weight: bold;
            background-color: #e9ecef; /* Light gray */
        }

        .btn-export {
            margin-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-5">
        <h3 class="text-center mb-4 fw-bold">Pending Weighbridge Name For Updation</h3>
        <div class="mb-3 text-end">
            <asp:Button ID="btnExcel" runat="server" Text="Export to Excel"
                CssClass="btn btn-success btn-sm btn-export"
                OnClick="btnExcel_Click" />
            <button type="button" class="btn btn-primary btn-sm" onclick="PrintGrid();">
                Print
            </button>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvReport" runat="server"
                CssClass="table table-bordered table-striped table-hover align-middle"
                AutoGenerateColumns="False"
                ShowFooter="True"
                OnRowDataBound="gvReport_RowDataBound"
                EmptyDataText="No Record Found">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                    <asp:BoundField DataField="DepotName" HeaderText="Name of Block/Tehsil of Weighbridge" />
                    <asp:BoundField DataField="WBCount" HeaderText="Weighbridge Count"
                        ItemStyle-HorizontalAlign="Right"
                        FooterStyle-HorizontalAlign="Right" />
                </Columns>
                <FooterStyle CssClass="table-secondary fw-bold" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

