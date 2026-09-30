<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_Wise_Payment_status_Godown_Type_Wise_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_District_Wise_Payment_status_Godown_Type_Wise_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div style="padding-left: 10px;">
        <div style="text-align: center; font-size: large;">
            <h2 class="header">Godown Type wise Payment Status (All Amount in Cr.)&nbsp;&nbsp;<h3>
                <asp:Label ID="lbldate" runat="server"></asp:Label></h3>
            </h2>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-10 p-4 rounded" style="border-color: #e3e3e8;">
                <div class="row align-items-center">

                    <div class="col-md-3 text-md-end text-center mb-2 mb-md-0">
                        <asp:Label ID="Label1" runat="server" Text="Godown Type :" Font-Size="20pt" Font-Bold="true"></asp:Label>
                    </div>

                    <div class="col-md-4 text-center mb-2 mb-md-0">
                        <asp:ListBox ID="ddlGodownType" runat="server" SelectionMode="Multiple"
                            CssClass="checkbox-multiselect form-control"></asp:ListBox>
                    </div>

                    <div class="col-md-3 text-md-start text-center">
                        <asp:Button ID="btnshow" runat="server" Text="Show Details"
                            CssClass="btn btn-success px-4"
                            OnClick="btnshow_Click" />
                    </div>

                </div>
            </div>
        </div>

        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

            <tr>
                <td style="text-align: left;">
                    <asp:GridView ID="GridView1"
                        runat="server"
                        AutoGenerateColumns="False"
                        ShowFooter="true"
                        ClientIDMode="Static"
                        CssClass="table table-bordered table-striped w-100"
                        UseAccessibleHeader="true"
                        OnPreRender="GridView1_PreRender">

                        <Columns>

                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Region" HeaderText="Region" />
                            <asp:BoundField DataField="District" HeaderText="District" />

                            <asp:BoundField DataField="NoOfGenerateBill" HeaderText="Total Bill Generated" />
                            <asp:BoundField DataField="BillAmt" HeaderText="Total Bill Amount" />
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        $(document).ready(function () {

            if ($.fn.DataTable.isDataTable('#GridView1')) {
                $('#GridView1').DataTable().destroy();
            }

            BindDatatable($('#GridView1'));
        });
    </script>
</asp:Content>


