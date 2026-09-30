<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_NCCF_Godown_Wise_Payment_Status_For_HO.aspx.cs" Inherits="StatePages_Rpt_NCCF_Godown_Wise_Payment_Status_For_HO" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        .header-gen {
            background-color: #e9ecef;
        }
        /* Neutral */
        .header-sub {
            background-color: #fff3cd;
        }
        /* Warning/Pending */
        .header-pay {
            background-color: #d1e7dd;
        }
        /* Success/Paid */
        .table-sticky {
            position: sticky;
            top: 0;
            background: white;
            z-index: 10;
        }

        .amt-font {
            font-family: 'Courier New', Courier, monospace;
            font-weight: bold;
        }

        .grid-container {
            max-height: 1000px;
            overflow-y: auto;
            overflow-x: auto;
            background: #fff;
        }

            .grid-container thead th {
                position: sticky;
                top: 0;
                z-index: 20;
                background-color: #212529 !important; /* table-dark जैसा */
                color: white;
            }

            .grid-container::-webkit-scrollbar {
                width: 6px;
            }

            .grid-container::-webkit-scrollbar-thumb {
                background: #888;
                border-radius: 5px;
            }

        .grid-container {
            max-height: 1000px;
            overflow: auto;
        }

            .grid-container table thead th {
                position: sticky;
                top: 0;
                z-index: 50;
                background-color: #212529 !important;
                color: white;
            }

            /* optional: header border fix */
            .grid-container table th {
                border: 1px solid #dee2e6;
            }

            .grid-container table {
                border-collapse: separate;
            }
    </style>
</head>
<body class="bg-light">
    <form runat="server">
        <div class="container-fluid py-4">
            <div class="d-flex justify-content-between align-items-center mb-4">
                <h2>NCCF PVT Godown Bill Status </h2>
                <asp:Button ID="btnExport" runat="server" Text="Export to Excel"
                    CssClass="btn btn-success" OnClick="btnExport_Click" />
            </div>
            <div class="row mb-4">
                <div class="col-md-2">
                    <div class="card border-primary">
                        <div class="card-body">
                            <h6 class="text-muted">Total Generated Amount</h6>
                            <h3>₹ <span id="totalGen" runat="server"></span></h3>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card border-warning">
                        <div class="card-body">
                            <h6 class="text-muted">Pending at Branch</h6>
                            <h3>₹ <span id="totalPendBranch" runat="server"></span></h3>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card border-info">
                        <div class="card-body">
                            <h6 class="text-muted">RM Submitted to NCCF</h6>
                            <h3>₹ <span id="totalRMSub" runat="server"></span></h3>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card border-secondary">
                        <div class="card-body">
                            <h6 class="text-muted">Pending at RM</h6>
                            <h3>₹ <span id="totalRMPending" runat="server"></span></h3>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card border-danger">
                        <div class="card-body">
                            <h6 class="text-muted">Pending at NCCF</h6>
                            <h3>₹ <span id="totalPendNCCF" runat="server"></span></h3>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card border-success">
                        <div class="card-body">
                            <h6 class="text-muted">Payment Received</h6>
                            <h3>₹ <span id="totalRec" runat="server"></span></h3>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card shadow-sm">
                <div class="grid-container shadow-sm">
                    <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        CssClass="table table-bordered table-sm mb-0" HeaderStyle-CssClass="text-center align-middle"
                        OnRowDataBound="GridView1_RowDataBound" OnPreRender="GridView1_PreRender">--%>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                        CssClass="table table-bordered table-sm mb-0" HeaderStyle-CssClass="text-center align-middle"
                        OnRowDataBound="GridView1_RowDataBound" OnPreRender="GridView1_PreRender">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Region" HeaderText="Region" />
                            <asp:BoundField DataField="District" HeaderText="District" />
                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                            <asp:TemplateField HeaderText="Godown Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkGodown" runat="server" Target="_blank"
                                        NavigateUrl='<%#"~/StatePages/Rpt_Godown_Bill_Wise_Payment_Status_NCCF_For_HO.aspx?GodownID="+ Eval("Godown_ID")%>'
                                        Text='<%# Eval("godown_Name") %>' Font-Bold="true"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NoOfGenerateBill" HeaderText="Total No. of Bill Generation" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="BillAmt" HeaderText="Total Bill Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="NoOfSUBBill" HeaderText="Total No. of Submitted Bill" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="SUBBillAmt" HeaderText="Total Submitted Bill Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingBillForSubmisionatBranch" HeaderText="Pending Bill For Submision at Branch" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingBillAmountForSubmision" HeaderText="Pending Bill Amount For Submision" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="RMsubmitbilltonccf" HeaderText="RM Total Submitted Bill To NCCF" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="RMsubmitbillAmttonccf" HeaderText="RM Total Submitted Amount To NCCF" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingBillForSubmisionatRM" HeaderText="RM Pending Bill Count" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingBillAmountForSubmisionatRM" HeaderText="RM Pending Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="NoofbillPaymentReceivedFromNCCF" HeaderText="Total Received Bill From NCCF" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="NoofbillPaymentAmountReceivedFromNCCF" HeaderText="Total Recv Amt (NCCF)" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaymentDecuctionbyNCCF" HeaderText="Payment Deducted From NCCF" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TotalNoofPendingBillatNCCF" HeaderText="Total Pending Bill at NCCF" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TotalNoofPendingBillAmountatNCCF" HeaderText="Total Pending Amt at NCCF" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="NoOfBillPayment" HeaderText="No Of Bill Payment to godown Owner" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="BillAmtPTG" HeaderText="Bill Amount Pay to Godown Owner" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
