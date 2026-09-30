<%@ Page Title="Godown Bill Wise Pending DSC Report" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Get_Godown_Bill_Wise_Pending_DSC_From_DM_MPSCSC_BO.aspx.cs" Inherits="BranchPages_Get_Godown_Bill_Wise_Pending_DSC_From_DM_MPSCSC_BO" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        /* Container and Panel Full Width Fix */
        #printZone, .report-panel {
            width: 100% !important;
            padding: 0 !important;
            margin: 0 !important;
        }

        .report-panel { 
            background: #fff; 
            padding: 15px !important; 
            border-radius: 5px; 
            box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1); 
            margin-bottom: 25px; 
        }

        /* Force Grid/Table to Occupy 100% Width */
        html body .container-fluid .report-panel table.table,
        table.table, 
        #gvReport {
            width: 100% !important;
            max-width: 100% !important;
            table-layout: auto !important;
        }

        .page-header { background: #1e3a8a; color: white !important; padding: 12px; border-radius: 5px; margin-bottom: 15px; text-align: center; font-size: 22px; font-weight: bold; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
        .print-header-block { display: none; text-align: center; margin-bottom: 20px; font-family: 'Segoe UI', Arial, sans-serif; }
        .btn-area { margin-bottom: 15px; text-align: right; }

        /* Grid Header Formatting */
        html body .container-fluid .report-panel table.table tr th,
        table.table thead tr th, .table th {
            background: #2563eb !important; color: #ffffff !important; font-weight: bold !important;
            text-align: center !important; vertical-align: middle !important; font-size: 12px !important;
            border: 1px solid #1e40af !important; padding: 10px !important;
            white-space: nowrap !important;
        }

        .table td {
            vertical-align: middle !important;
            border: 1px solid #e2e8f0 !important;
            font-size: 12px !important;
            text-align: left !important;
            padding: 8px 10px !important;
        }

        html body .container-fluid .report-panel table.table tr td.text-right-align,
        .table td.text-right-align,
        .text-right-align {
            text-align: right !important;
            padding-right: 12px !important;
        }

        html body .container-fluid .report-panel table.table tr td.text-center-align,
        .table td.text-center-align,
        .text-center-align {
            text-align: center !important;
        }

        /* Print Specific Rules */
        @media print {
            @page { size: landscape; margin: 5mm; }
            body *, html * { visibility: hidden; height: auto !important; }
            #printZone, #printZone * { visibility: visible; }
            #printZone { 
                position: absolute; 
                left: 0; 
                top: 0; 
                width: 100% !important; 
                background: none; 
                padding: 0; 
                margin: 0; 
                zoom: 100%; 
            }
            .btn-area, .page-header { display: none !important; }
            .print-header-block { display: block !important; }
            .report-panel { box-shadow: none !important; padding: 0 !important; border: none !important; width: 100% !important; }
            
            .table { 
                border-collapse: collapse !important; 
                width: 100% !important; 
                max-width: 100% !important;
            }
            thead { display: table-header-group !important; }
            tr { page-break-inside: avoid !important; }

            .table td.text-right-align { text-align: right !important; padding-right: 12px !important; }
            .table td.text-center-align { text-align: center !important; }

            html body #printZone .report-panel table.table thead tr th,
            html body #printZone .report-panel table.table tr th {
                background-color: #2563eb !important; 
                color: #ffffff !important;
                font-weight: bold !important;
                text-align: center !important;
                vertical-align: middle !important;
                border: 1px solid #1e40af !important;
                -webkit-print-color-adjust: exact !important; 
                print-color-adjust: exact !important; 
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="printZone" class="container-fluid">

        <div class="page-header">
            Godown Bill Wise Pending DSC From DM MPSCSC Report
        </div>

        <div class="print-header-block">
            <h2 style="margin: 0; font-size: 24px; font-weight: bold; color: #1e3a8a;">MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</h2>
            <h4 style="margin: 6px 0; font-size: 16px; color: #475569;">Pending DSC Bill Summary As On: <%= DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") %></h4>
            <hr style="border: 1px solid #1e3a8a; margin-top: 5px; margin-bottom: 20px;" />
        </div>

        <div class="report-panel">
            <div class="btn-area">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Style="background-color: #16a34a; border-color: #16a34a;" Text="Export To Excel" OnClick="btnExcel_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Style="background-color: #2563eb; border-color: #2563eb;" Text="Print Report" OnClientClick="window.print(); return false;" />
            </div>

            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
                CssClass="table table-bordered table-striped table-hover" ShowFooter="false"
                OnRowDataBound="gvReport_RowDataBound" OnDataBound="gvReport_DataBound">
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle Width="50px" CssClass="text-center-align" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Regionnm" HeaderText="Region Name" />
                    <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                    <asp:BoundField DataField="DepotName" HeaderText="Depot/Branch Name" />
                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                    <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Bill_Month" HeaderText="Bill Month" ItemStyle-CssClass="text-center-align" />
                    <asp:BoundField DataField="Net_Amount" HeaderText="Net Amount (₹)" DataFormatString="{0:N2}" ItemStyle-CssClass="text-right-align" />
                    <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number" ItemStyle-CssClass="text-center-align" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>