<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Get_Branch_Wise_JVS_Payment_Against_Received_Payment_Pending_Rent_Bill_For_Generation_New.aspx.cs" Inherits="Reports_NewBillingReports_Get_Branch_Wise_JVS_Payment_Against_Received_Payment_Pending_Rent_Bill_For_Generation_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid mt-3">

        <!-- ===== PAGE HEADER ===== -->
        <div class="card mb-4">
            <div class="card-body text-center bg-primary text-white">
                <h4 class="mb-1">Branch Wise JVS Payment Status</h4>
                <small>Against Received Payment From MPSCSC (From August 2020)</small>
            </div>
        </div>

        <!-- ===== FILTER SECTION ===== -->
        <div class="card mb-4">
            <div class="card-header fw-bold bg-light">
                Search Criteria
            </div>
            <div class="card-body">
                <div class="row g-3 align-items-end justify-content-center">

                    <!-- Payment Date From -->
                    <div class="col-md-3">
                        <label class="fw-bold">Received Payment Date From</label>
                        <asp:TextBox ID="txtdatefrom" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                    </div>

                    <!-- Payment Date To -->
                    <div class="col-md-3">
                        <label class="fw-bold">Received Payment Date To</label>
                        <asp:TextBox ID="txtdateto" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                    </div>

                    <!-- Search Button -->
                    <div class="col-md-2">
                        <asp:Button ID="btnDateSearch" runat="server" CssClass="btn btn-primary w-100 show-loader"
                            Text="Search" OnClick="btnDateSearch_Click" />
                    </div>

                    <!-- OR separator -->
                    <div class="col-md-12 text-center fw-bold mt-2 mb-2">OR</div>


                </div>

                <div class="row g-3 align-items-end justify-content-center">
                    <!-- UTR Number -->
                    <div class="col-md-3 offset-md-3">
                        <label class="fw-bold">UTR Number</label>
                        <asp:TextBox ID="txtUTRNo" runat="server" onkeypress="return NumberOnly(event);" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        <asp:Button ID="btnUTRSearch" runat="server" CssClass="btn btn-primary w-100"
                            Text="Search" OnClick="btnUTRSearch_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- ===== GRID SECTION ===== -->
        <div class="card">
            <div class="card-header fw-bold bg-light">
                Branch Wise JVS Payment Pending Report
            </div>
            <div class="card-body table-responsive">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                    OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                    CssClass="table table-bordered table-striped table-hover" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">

                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                        <asp:BoundField DataField="District_Name" HeaderText="District" />
                        <asp:BoundField DataField="DepotName" HeaderText="Branch" />

                        <asp:TemplateField HeaderText="No of Bills" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblUTR2BillNo" runat="server" Text='<%# Eval("UTR2BillNo") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <div class="text-end">
                                    <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                </div>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="No of Rent Bills Pending For Generation" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblUTR9RBillNo" runat="server" Text='<%# Eval("UTR9RBillNo") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <div class="text-end">
                                    <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                </div>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="No of Rent Bill Amount Pending" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblUTR9R_Amount" runat="server" Text='<%# Eval("UTR9R_Amount") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <div class="text-end">
                                    <asp:Label ID="lblTotalqty3" runat="server" Font-Bold="true" />
                                </div>
                            </FooterTemplate>
                        </asp:TemplateField>

                    </Columns>

                    <FooterStyle Font-Bold="True" BackColor="#f1f1f1" ForeColor="Black" />
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>

