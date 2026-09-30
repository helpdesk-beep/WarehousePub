<%@ Page Title="JIT Payment Status Report For Region" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Reports/Region/Get_JIT_Payment_Status_MPWLC_To_Godown_Owner_For_Region.aspx.cs" Inherits="Reports_Region_Get_JIT_Payment_Status_MPWLC_To_Godown_Owner_For_Region" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        
        /* FIXED PRINT HEADER BLOCK CONTAINER */
        .print-header-block { 
            display: none; 
            text-align: center !important; 
            width: 100% !important;
            margin-bottom: 20px; 
            font-family: 'Segoe UI', Arial, sans-serif; 
        }
        
        /* Table Header Formatting Forced */
        html body .container-fluid .report-panel table.table tr th,
        table.table thead tr th, .table th {
            background: #2563eb !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 11px !important;
            border: 1px solid #1e40af !important; padding: 8px !important; white-space: normal !important;
        }

        /* Base Data Alignment Rule: Wrapping forced to prevent grid rupture */
        .table td {
            vertical-align: middle !important; border: 1px solid #e2e8f0 !important; font-size: 11px !important;
            text-align: left !important; padding: 6px 8px !important; white-space: normal !important;
            word-break: break-word !important;
        }

        /* Prevent text squishing inside dynamic grid boundaries */
        .col-text-wrap-lg { min-width: 180px !important; max-width: 250px !important; }
        .col-text-wrap-sm { min-width: 100px !important; max-width: 130px !important; }

        /* High Priority Specificity Rules for Right & Center Alignments */
        html body .container-fluid .report-panel table.table tr td.text-right-align,
        .table td.text-right-align, .text-right-align { text-align: right !important; padding-right: 10px !important; white-space: nowrap !important; }
        html body .container-fluid .report-panel table.table tr td.text-center-align,
        .table td.text-center-align, .text-center-align { text-align: center !important; white-space: nowrap !important; }

        .report-panel { background: #fff; padding: 15px; border-radius: 5px; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); margin-bottom: 25px; }
        .filter-section { background: #f8fafc; border: 1px solid #e2e8f0; padding: 15px; border-radius: 5px; margin-bottom: 15px; }
        .btn-area { text-align: right; }

        /* HORIZONTAL SCROLLER CONTAINER DEPLOYMENT LOCK */
        .grid-scroller-container {
            width: 100% !important;
            max-width: 100% !important;
            overflow-x: auto !important;
            overflow-y: hidden !important;
            margin-bottom: 15px !important;
            border: 1px solid #e2e8f0 !important;
            border-radius: 4px !important;
            display: block !important;
        }

        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { position: absolute; left: 0; top: 0; width: 100%; background: none; padding: 0; margin: 0; zoom: 55%; }
            .btn-area, .page-header, .filter-section { display: none !important; }
            
            /* FIXED FOR PRINT HEADER LAYOUT */
            .print-header-block { display: block !important; text-align: center !important; width: 100% !important; }
            .print-header-block * { text-align: center !important; }
            
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; }
            .table { border-collapse: collapse !important; width: 100% !important; table-layout: auto !important; }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }
            
            .grid-scroller-container { overflow-x: visible !important; max-width: none !important; border: none !important; display: inline !important; }
            .table td { white-space: normal !important; word-break: break-word !important; }
            .table td.text-right-align { text-align: right !important; padding-right: 10px !important; white-space: nowrap !important; }
            .table td.text-center-align { text-align: center !important; white-space: nowrap !important; }

            html body #printZone .report-panel table.table thead tr th,
            html body #printZone .report-panel table.table tr th {
                background-color: #2563eb !important; color: #ffffff !important; font-weight: bold !important;
                text-align: center !important; vertical-align: middle !important; border: 1px solid #1e40af !important;
                -webkit-print-color-adjust: exact !important; print-color-adjust: exact !important;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid" style="margin-left:50px;width:1690px;">
    <div id="printZone" class="container-fluid mt-3">

        <div class="page-header">
            JIT Payment Credit Status Report (Regional Office View)
        </div>

        <div class="print-header-block">
            <h2 style="margin:0 auto; font-size:24px; font-weight:bold; color:#1e3a8a; text-align:center !important;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin:6px auto; font-size:16px; color:#475569; text-align:center !important;">Regional JIT Payment Status Summary Report</h4>
            <p style="margin:5px auto 0 auto; font-size:12px; font-weight:bold; text-align:center !important; color:#334155;">Generated On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></p>
            <hr style="border:1px solid #1e3a8a; margin-top:10px; margin-bottom:20px; width:100%;"/>
        </div>

        <div class="filter-section">
            <div class="row align-items-center g-3">
                <div class="col-md-3">
                    <label class="fw-bold mb-1">From Date:</label>
                    <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label class="fw-bold mb-1">To Date:</label>
                    <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 mt-4 pt-2">
                    <asp:Button ID="btnSearch" runat="server" Text="View Records" CssClass="btn btn-primary w-100" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-4 btn-area mt-4 pt-2 ms-auto">
                    <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
                </div>
            </div>
        </div>

        <div class="report-panel">
            <div class="grid-scroller-container">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-striped table-hover mb-0" ShowFooter="false"
                    EmptyDataText="No transaction logs discovered inside selected operational calendar frame."
                    OnRowDataBound="gvReport_RowDataBound" OnDataBound="gvReport_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle Width="40px" CssClass="text-center-align" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="District_Name" HeaderText="District Name" ItemStyle-CssClass="col-text-wrap-sm" />
                        <asp:BoundField DataField="DepotName" HeaderText="Depot Name" ItemStyle-CssClass="col-text-wrap-sm" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-CssClass="col-text-wrap-lg" />
                        <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="Account_No" HeaderText="Account No" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="IFSC_Code" HeaderText="IFSC Code" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="JVS_Bill_No" HeaderText="JVS Bill No" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="SC_Bill_No" HeaderText="SC Bill No" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="Commodity" HeaderText="Commodity" ItemStyle-CssClass="col-text-wrap-sm" />
                        <asp:BoundField DataField="Bill_Month" HeaderText="Bill Month" ItemStyle-CssClass="text-center-align" />
                        
                        <asp:BoundField DataField="Per_Month_Rate" HeaderText="Rate/Month" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Rent_Bill_AMT" HeaderText="Rent Bill Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Gain_Detuction_Amount" HeaderText="Gain Deduct" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Other_Detuction_Amt" HeaderText="Other Deduct" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="BM_Deduction" HeaderText="BM Deduct" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="Total_Deduction_AMT" HeaderText="Total Deduct" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PayToGO" HeaderText="Pay To Owner" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="PAYtoMPWLC" HeaderText="Pay To WLC" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="StorageCharBillAMt" HeaderText="Storage Bill Amt" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        
                        <asp:BoundField DataField="RO_Approve_Date" HeaderText="RO Approve Date" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="MPWLCPAYToGodown" HeaderText="Paid To Owner" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                        <asp:BoundField DataField="BranchBill_BankUTRNo" HeaderText="UTR Number" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="Payment_Date" HeaderText="Payment Date" ItemStyle-CssClass="text-center-align" />
                        <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" ItemStyle-CssClass="col-text-wrap-sm" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
        </div>
</asp:Content>