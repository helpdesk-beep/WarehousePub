<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/WareHouseMaster.master"
    AutoEventWireup="true"
    CodeFile="Rpt_Godown_Wise_Payment_Status_For_Owned_Godown_New.aspx.cs"
    Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Godown_Wise_Payment_Status_For_Owned_Godown_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="server">

    <style>
        /* DataTable Header Style */
        table.dataTable thead th {
            background-color: #00AAD2 !important;
            color: #fff !important;
            text-align: center;
            vertical-align: middle;
            white-space: normal !important;   /* WRAP HEADER */
            word-break: break-word;
            font-size: 14px;
        }

        /* Body cell wrap */
        table.dataTable tbody td {
            white-space: normal !important;   /* WRAP DATA */
            word-break: break-word;
            font-size: 13px;
        }

        .report-title {
            text-align: center;
            margin-bottom: 15px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="server">

    <div class="container-fluid">

        <div class="report-title">
            <h3 class="fw-bold">M.P. WAREHOUSING & LOGISTICS CORPORATION</h3>
            <h5>Owned Godown Wise Payment Status</h5>
        </div>

        <div class="table-responsive">

            <asp:GridView ID="GridView1"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped w-100"
                ClientIDMode="Static"
                ShowFooter="true"
                UseAccessibleHeader="true"
                OnRowDataBound="GridView1_RowDataBound"
                OnPreRender="GridView1_PreRender">

                <Columns>

                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Region" HeaderText="Region" />
                    <asp:BoundField DataField="District" HeaderText="District" />
                    <asp:BoundField DataField="Branch" HeaderText="Branch" />
                    <asp:BoundField DataField="godown_Name" HeaderText="Godown Name" />

                    <asp:BoundField DataField="NoOfGenerateBill" HeaderText="Generated Bills" />
                    <asp:BoundField DataField="BillAmt" HeaderText="Bill Amount" />
                    <asp:BoundField DataField="NoOfSUBBill" HeaderText="Submitted Bills" />
                    <asp:BoundField DataField="SUBBillAmt" HeaderText="Submitted Amount" />

                    <asp:BoundField DataField="PendingBillForSubmisionatBranch" HeaderText="Pending Bills (Branch)" />
                    <asp:BoundField DataField="PendingBillAmountForSubmision" HeaderText="Pending Amount (Branch)" />

                    <asp:BoundField DataField="NoofbillPaymentReceivedFromMPSCSC" HeaderText="Received Bills (MPSCSC)" />
                    <asp:BoundField DataField="NoofbillPaymentAmountReceivedFromMPSCSC" HeaderText="Received Amount (MPSCSC)" />

                    <asp:BoundField DataField="PaymentDecuctionbyMPSCSC" HeaderText="Deduction" />
                    <asp:BoundField DataField="TotalNoofPendingBillatMPSCSC" HeaderText="Pending Bills (MPSCSC)" />
                    <asp:BoundField DataField="TotalNoofPendingBillAmountatMPSCSC" HeaderText="Pending Amount (MPSCSC)" />

                </Columns>

                <FooterStyle Font-Bold="true" />

            </asp:GridView>

        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageScript" runat="server">

    <script>
        $(document).ready(function () {

            if ($.fn.DataTable.isDataTable('#GridView1')) {
                $('#GridView1').DataTable().destroy();
            }

            BindDatatable($('#GridView1'));
        });
    </script>

</asp:Content>
