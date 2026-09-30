<%@ Page Title="NAFED Payment Details Breakdown" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Get_Nafed_Payment_Detail_For_HO.aspx.cs" Inherits="StatePages_Get_Nafed_Payment_Detail_For_HO" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />

    <style type="text/css">
        .main-card {
            background-color: #ffffff;
            border: 1px solid #e3e6f0;
            border-radius: 0.35rem;
            box-shadow: 0 0.15rem 1.75rem 0 rgba(58, 59, 69, 0.15);
            padding: 1.5rem;
            margin-top: 1rem;
        }

        .page-header-title {
            font-size: 1.4rem;
            font-weight: 700;
            color: #4e73df;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            border-bottom: 2px solid #eaecf4;
            padding-bottom: 0.75rem;
            margin-bottom: 1.5rem;
        }

        .filter-section {
            background-color: #f8f9fc;
            border: 1px solid #e3e6f0;
            border-radius: 6px;
            padding: 15px;
            margin-bottom: 20px;
        }

        .search-container-box {
            background-color: #f8fafc;
            border: 1px dashed #cbd5e1;
            border-radius: 6px;
            padding: 10px 15px;
            margin-bottom: 15px;
        }

        .info-panel {
            background-color: #e7f3fe;
            border-left: 6px solid #2196F3;
            padding: 12px;
            margin-bottom: 15px;
            font-weight: 600;
            color: #1e3a8a;
            border-radius: 0 4px 4px 0;
        }

        /* Premium Grid Framework Width Fixes */
        .custom-nafed-grid {
            width: 100%;
            border-collapse: collapse !important;
            table-layout: auto;
        }

            .custom-nafed-grid th {
                background: linear-gradient(180deg, #4e73df 0%, #224abe 100%) !important;
                color: #ffffff !important;
                font-weight: 600;
                text-align: center;
                vertical-align: middle !important;
                padding: 10px 5px !important;
                border: 1px solid #cbd5e1 !important;
                font-size: 0.8rem;
                text-transform: uppercase;
                white-space: nowrap;
            }

            .custom-nafed-grid td {
                padding: 8px 8px !important;
                vertical-align: middle !important;
                font-size: 0.82rem;
                border: 1px solid #cbd5e1 !important;
                color: #2d3748;
            }

            .custom-nafed-grid tr:nth-of-type(even) {
                background-color: #f8f9fc;
            }

        /* Sub Total & Grand Total Rows */
        .subtotal-row td {
            background-color: #f1f5f9 !important;
            font-weight: bold !important;
            color: #1e3a8a !important;
            border-top: 1.5px solid #cbd5e1 !important;
            border-bottom: 1.5px solid #cbd5e1 !important;
            font-size: 0.85rem !important;
        }

        .grandtotal-row td {
            background-color: #eff6ff !important;
            border-top: 2px solid #4e73df !important;
            border-bottom: 2px double #224abe !important;
            font-weight: bold !important;
            color: #1e3a8a !important;
            font-size: 0.88rem !important;
        }

        .text-right-align {
            text-align: right !important;
            padding-right: 12px !important;
            font-weight: bold;
        }

        .print-only-header {
            display: none;
        }

        @media print {
            @page {
                size: landscape;
                margin: 12mm 8mm;
            }

            body * {
                visibility: hidden;
            }

            .print-wrapper, .print-wrapper * {
                visibility: visible;
            }

            .print-wrapper {
                position: absolute;
                left: 0;
                top: 0;
                width: 100%;
                padding: 0;
                margin: 0;
                box-shadow: none;
                border: none;
            }

            .filter-section, .search-container-box, .action-area, .btn, .back-btn {
                display: none !important;
            }

            .print-only-header {
                display: block !important;
                border-bottom: 3px double #1e3a8a;
                padding-bottom: 10px;
                margin-bottom: 20px;
            }

            .page-header-title {
                display: none !important;
            }

            .custom-nafed-grid {
                border-collapse: collapse !important;
                width: 100% !important;
                page-break-inside: auto;
            }

                .custom-nafed-grid tr {
                    page-break-inside: avoid !important;
                    page-break-after: auto !important;
                }

                .custom-nafed-grid thead {
                    display: table-header-group !important;
                }

                .custom-nafed-grid th {
                    background-color: #4e73df !important;
                    color: #ffffff !important;
                    -webkit-print-color-adjust: exact !important;
                    print-color-adjust: exact !important;
                }

            .subtotal-row td {
                background-color: #f1f5f9 !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }

            .grandtotal-row td {
                background-color: #eff6ff !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }
        }
    </style>

    <script type="text/javascript">
        function performGridSearch() {
            var input = document.getElementById('txtClientSearch');
            var filter = input.value.toUpperCase();
            var grid = document.getElementById('<%= gvNafedDetails.ClientID %>');
            if (!grid) return;

            var tr = grid.getElementsByTagName('tr');
            for (var i = 1; i < tr.length; i++) {
                if (tr[i].className.indexOf('subtotal-row') > -1 || tr[i].className.indexOf('grandtotal-row') > -1) {
                    continue;
                }
                var showRow = false;
                var cellsToSearch = [1, 2, 3];

                for (var j = 0; j < cellsToSearch.length; j++) {
                    var td = tr[i].cells[cellsToSearch[j]];
                    if (td) {
                        var textValue = td.textContent || td.innerText;
                        if (textValue.toUpperCase().indexOf(filter) > -1) {
                            showRow = true;
                            break;
                        }
                    }
                }
                tr[i].style.display = showRow ? "" : "none";
            }
        }

        function CallPrint() {
            window.print();
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid print-wrapper main-card">

        <!-- PRINT BANNER HEADER -->
        <div class="print-only-header text-center">
            <h2 style="font-weight: bold; color: #1e3a8a; margin: 0; font-size: 22px;">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h4 style="color: #475569; margin: 5px 0 0 0; font-size: 15px;">NAFED Storage Payment Details Breakdown</h4>
            <p style="font-size: 12px; font-weight: bold; margin-top: 5px;">
                Target Payment Date: <%= !string.IsNullOrEmpty(txtPaymentDate.Text) ? DateTime.Parse(txtPaymentDate.Text).ToString("dd-MM-yyyy") : "" %> | Printed On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %>
            </p>
        </div>

        <div class="page-header-title">
            <i class="fas fa-file-invoice-dollar mr-2 text-primary"></i>NAFED Storage Payment Detail Breakdown
        </div>

        <div class="filter-section shadow-sm">
            <div class="form-row align-items-end justify-content-center">
                <div class="form-group col-md-4 m-0">
                    <label class="font-weight-bold text-dark"><i class="far fa-calendar-alt mr-1 text-primary"></i>Select Payment Date:</label>
                    <asp:TextBox ID="txtPaymentDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary font-weight-bold btn-block" OnClick="btnSearch_Click">
                        <i class="fas fa-search mr-1"></i> Load Breakdown
                    </asp:LinkButton>
                </div>
            </div>
        </div>

        <div class="search-container-box shadow-sm row no-gutters align-items-center justify-content-between action-area">
            <div class="col-md-6 d-flex align-items-center">
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text bg-white border-right-0 text-muted"><i class="fas fa-filter"></i></span>
                    </div>
                    <input type="text" id="txtClientSearch" class="form-control border-left-0" placeholder="Type District, Depot, or Godown Name to search instantly..." onkeyup="performGridSearch();" autocomplete="off" />
                </div>
            </div>
            <div class="col-md-5 text-right mt-2 mt-md-0">
                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" CssClass="btn btn-success btn-sm font-weight-bold shadow-sm mr-2">
                    <i class="fas fa-file-excel mr-1"></i> Export Excel
                </asp:LinkButton>
                <asp:LinkButton ID="btnPrint" runat="server" OnClientClick="return CallPrint();" CssClass="btn btn-secondary btn-sm font-weight-bold shadow-sm">
                    <i class="fas fa-print mr-1"></i> Print Breakdown
                </asp:LinkButton>
            </div>
        </div>

        <div class="info-panel action-area shadow-sm">
            <i class="fas fa-info-circle mr-1"></i>Active Record State Context Date: 
            <span class="badge badge-dark p-2" style="font-size: 13px;">
                <%= !string.IsNullOrEmpty(txtPaymentDate.Text) ? DateTime.Parse(txtPaymentDate.Text).ToString("dd-MM-yyyy") : "" %>
            </span>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gvNafedDetails" runat="server" AutoGenerateColumns="False"
                CssClass="table table-bordered custom-nafed-grid" Width="100%"
                GridLines="Both" BorderColor="#e3e6f0" BorderStyle="Solid" BorderWidth="1px"
                OnRowDataBound="gvNafedDetails_RowDataBound" OnRowCreated="gvNafedDetails_RowCreated" OnDataBound="gvNafedDetails_DataBound">
                <Columns>
                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="text-center">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="District_Name" HeaderText="District Name" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="DepotName" HeaderText="Depot Name" ItemStyle-HorizontalAlign="Left" />
                    <%--<asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" />--%>
                    <%-- CHANGED: Godown Name ko Hyperlink banaya detail page par redirect karne ke liye --%>
                    <asp:TemplateField HeaderText="Godown Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlGodown" runat="server" Target="_blank"
                                NavigateUrl='<%# String.Format("Get_Nafed_Payment_Detail_By_Godown.aspx?GodownID={0}&CommodityID={1}&Date={2}", Eval("Godown_ID"), Eval("Commodity_Id"), txtPaymentDate.Text) %>'
                                Text='<%# Eval("Godown_Name") %>' ForeColor="#4e73df">
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CropYear" HeaderText="Crop Year" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" ItemStyle-HorizontalAlign="Left" />

                    <%-- FIXED: Mapped explicit N2 string formatting configurations for exact accounting decimals structure alignment safely --%>
                    <asp:BoundField DataField="Payment" HeaderText="Payment Amt" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" />
                    <asp:BoundField DataField="TDS" HeaderText="TDS (PSS)" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-secondary" />
                    <asp:BoundField DataField="TDS1" HeaderText="TDS (PSF)" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-secondary" />
                    <asp:BoundField DataField="NetAmount" HeaderText="Net Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-success font-weight-bold" />
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-warning text-center font-weight-bold m-0" role="alert">
                        <i class="fas fa-exclamation-triangle mr-1"></i>Is Payment Date par koi records available nahi hain.
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
