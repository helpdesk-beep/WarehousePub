<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Pending_Bill_For_Generation_State_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_Pending_Bill_For_Generation_State_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid mt-3">

        <!-- ================= HEADER ================= -->
        <fieldset class="border rounded p-3">
            <legend class="px-2 fw-bold">Pending Bill For Generation</legend>

            <!-- ================= GRID ================= -->
            <div class="table-responsive">
                <asp:GridView ID="grdbill" runat="server" 
                    CssClass="table table-bordered table-hover table-striped text-nowrap"
                    AutoGenerateColumns="false" 
                    ShowFooter="true"
                    OnRowCommand="grdbill_RowCommand">

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

                        <asp:BoundField DataField="TotalBillAmountnotsubmittedtoMPSCSC" 
                            HeaderText="Total Bill Amount not submitted to MPSCSC" />
                    </Columns>

                    <EmptyDataTemplate>
                        <div class="text-center text-danger fw-bold p-2">No Record Found</div>
                    </EmptyDataTemplate>

                    <FooterStyle CssClass="fw-bold bg-light" />
                    <PagerStyle CssClass="d-flex justify-content-end mt-2" />
                </asp:GridView>
            </div>
        </fieldset>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        $(document).ready(function () {
            var grid = $('#<%= grdbill.ClientID %>');

            // Convert first row to THEAD for DataTable
            grid.prepend($("<thead></thead>").append(grid.find("tr:first")));

            BindDatatable(grid);
        });
    </script>
</asp:Content>

