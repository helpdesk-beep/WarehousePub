<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Region_FY_DepositorWise_PaymentStatus_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Region_FY_DepositorWise_PaymentStatus_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <style>
    table.dataTable thead th,
    table.table thead th,
    table.table th,
    .Grid th {
        background-color: #00AAD2 !important;
        color: #ffffff !important;
        text-align: center !important;
        vertical-align: middle !important;
        white-space: normal !important;
        word-break: break-word;
        font-size: 14px;
        font-weight: bold;
    }

    /* Remove Bootstrap override */
    .table > :not(caption) > * > * {
        background-color: transparent;
    }

    /* GridView Header fallback */
    asp\:GridView th {
        background-color: #00AAD2 !important;
        color: #fff !important;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <div style="text-align: center; font-size: large;">
            <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h4 class="header">Report For Review of Region,&nbsp; Financial Year Wise, Depositor wise Payment Position Report (Amount in Cr Rs) </h4>
        </div>
        <div class="col-md-12">
            <div class="form-group">
                <div class="col-sm-8 col-sm-offset-4">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hfId" Value="0" runat="server" />
                    <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
                </div>
            </div>
            <div>
                <div class="row">
                    <div class="col-md-1">
                        <asp:Label ID="lblRegion" Font-Bold="true" runat="server">संभाग:</asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlRegion" CssClass="form-control" runat="server" selectionmode="Multiple" AutoPostBack="true">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblFY" Font-Bold="true" runat="server" ForeColor="Navy">वित्‍तीय वर्ष:</asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlFinancialYear" CssClass="form-control" runat="server" selectionmode="Multiple">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="2010-11">2010-11</asp:ListItem>
                            <asp:ListItem Value="2011-12">2011-12</asp:ListItem>
                            <asp:ListItem Value="2012-13">2012-13</asp:ListItem>
                            <asp:ListItem Value="2013-14">2013-14</asp:ListItem>
                            <asp:ListItem Value="2014-15">2014-15</asp:ListItem>
                            <asp:ListItem Value="2015-16">2015-16</asp:ListItem>
                            <asp:ListItem Value="2016-17">2016-17</asp:ListItem>
                            <asp:ListItem Value="2017-18">2017-18</asp:ListItem>
                            <asp:ListItem Value="2018-19">2018-19</asp:ListItem>
                            <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                            <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                            <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                            <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                            <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                            <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblDepositorName" Font-Bold="true" runat="server" ForeColor="Navy">जमाकर्ता का नाम:</asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlDepositor" CssClass="form-control" runat="server" selectionmode="Multiple">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" CssClass="btn btn-success show-loader" ValidationGroup="A"
                            runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div class="form-group"></div>
            </div>
            <div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
            <div class="col-md-12">
                <div class="table-responsive">
                    <asp:Panel ID="pnlGrandTotal" runat="server" Visible="false" CssClass="card shadow-sm mb-3">
                        <div class="card-header text-center fw-bold"
                            style="background: #2f6f9f; color: white; font-size: 16px;">
                            GRAND TOTAL SUMMARY (₹ In Crore)
                        </div>

                        <div class="card-body p-2">
                            <div class="row text-center fw-bold">

                                <div class="col-md-3 col-sm-6 mb-2">
                                    <div class="border rounded p-2 bg-light">
                                        <div class="text-muted">Total Bills</div>
                                        <div class="text-primary fs-5">
                                            <asp:Label ID="lblGTBills" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3 col-sm-6 mb-2">
                                    <div class="border rounded p-2 bg-light">
                                        <div class="text-muted">Amount Presented</div>
                                        <div class="text-success fs-5">
                                            <asp:Label ID="lblGTBillAmt" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3 col-sm-6 mb-2">
                                    <div class="border rounded p-2 bg-light">
                                        <div class="text-muted">Amount Received</div>
                                        <div class="text-info fs-5">
                                            <asp:Label ID="lblGTReceived" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3 col-sm-6 mb-2">
                                    <div class="border rounded p-2 bg-light">
                                        <div class="text-muted">Remaining Amount</div>
                                        <div class="text-danger fs-5">
                                            <asp:Label ID="lblGTRemaining" runat="server" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>


                    <div style="overflow-x: scroll;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover GridViewScrollHeader"
                            EnableModelValidation="True"
                            CellPadding="4" rules="all" ForeColor="#333333" ShowFooter="true"
                            OnRowDataBound="GV_FYDepositorWisePayment_OnRowDataBound" OnDataBound="GV_FYDepositorWisePayment_OnDataBound"
                            OnRowCreated="GV_FYDepositorWisePayment_OnRowCreated"
                            OnPreRender="GridView1_PreRender">
                            <RowStyle BackColor="White" CssClass="ADFieldsGridText table-responsive" HorizontalAlign="Left" />
                            <Columns>

                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="संभाग">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("RegionName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="वित्‍तीय वर्ष">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFinancialYear" runat="server" Height="21px" Text='<%# Eval("FinancialYear") %>'> 
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="जमाकर्ता का नाम">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositor" runat="server" Text='<%# Eval("DepositorName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="वित्‍तीय वर्ष में प्रस्तुत देयकों की संख्या" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalBillPresentedInFY" runat="server" Text='<%# Eval("TotalBillPresentedinFY") %>'>0</asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="वित्‍तीय वर्ष में प्रस्तुत देयकों की राशि (राशि Cr.रुपये में)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalBillAmountPresented" runat="server" Text='<%# Eval("TotalBillAmountPresented") %>'>0</asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="प्राप्त राशि का विवरण (राशि Cr.रुपये में)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAmountReceivedInFY" runat="server" Text='<%# Eval("TotalAmountReceivedInFY") %>'>0</asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="शेष लंबित राशि (राशि Cr.रुपये में)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemainingAmountFromDepositor" runat="server" Text='<%# Eval("RemainingAmountFromDepositor") %>'>0</asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="20px" Font-Size="12pt" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
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

            // Initialize DataTable
            BindDatatable($('#GridView1'));
        });

        function BindDatatable(gv) {
            gv.DataTable({
                paging: true,
                searching: true,
                ordering: true,
                info: true,
                lengthMenu: [[10, 20, 30, 50, -1], ['10', '20', '30', '50', 'All']],
                pageLength: 10,
                scrollX: true,
                dom: '<"top d-flex justify-content-between align-items-center"Bfl>rt<"bottom"ip><"clear">',
                destroy: true,
                autoWidth: false,
                buttons: [
                    { extend: 'excelHtml5', title: 'Godown_Wise_Report', text: 'Export to Excel 💾' },
                    { extend: 'pdfHtml5', title: 'Godown_Wise_Report', text: 'Export to PDF 📄', orientation: 'landscape', pageSize: 'A3' },
                    { extend: 'csvHtml5', text: 'Export CSV' },
                    { extend: 'print', text: 'Print' }
                ],
                initComplete: function () {
                    const wrapper = $(this).closest('.dataTables_wrapper');
                    wrapper.find('select').addClass('form-select form-select-sm');
                    wrapper.find('input[type=search]').addClass('form-control form-control-sm').attr('placeholder', 'Search...');
                }
            });
        }
    </script>
</asp:Content>