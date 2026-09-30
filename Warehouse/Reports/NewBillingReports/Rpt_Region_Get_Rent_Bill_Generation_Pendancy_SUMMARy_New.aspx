<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Region_Get_Rent_Bill_Generation_Pendancy_SUMMARy_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_Region_Get_Rent_Bill_Generation_Pendancy_SUMMARy_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">

<div class="container-fluid mt-3">

    <fieldset class="border p-3">
        <legend class="w-auto px-3 text-center fw-bold">
            Region Wise Pending Rent Bill Generation From Aug. 2020
            <span class="text-danger">(In M.T.)</span>
        </legend>

        <!-- ===== FILTER SECTION ===== -->
        <div class="card mb-3">
            <div class="card-body">
                <div class="row align-items-center">

                    <div class="col-md-2 fw-bold">
                        Report Type
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddldistrict" runat="server"
                            CssClass="form-control"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Region" Value="1"></asp:ListItem>
                            <asp:ListItem Text="District" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Branch" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2 fw-bold">
                        Financial Year
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlfinancial" runat="server"
                            CssClass="form-control"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlfinancial_SelectedIndexChanged">
                            <asp:ListItem Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
            </div>
        </div>

        <!-- ===== REGION GRID ===== -->
        <div id="divdivision" runat="server" visible="false" class="mb-4">
            <div class="card">
                <div class="card-header fw-bold bg-light">
                    Region Wise Summary
                </div>
                <div class="card-body table-responsive">
                    <asp:GridView ID="GridView1" runat="server"
                        AutoGenerateColumns="false"
                        ShowFooter="true"
                        CssClass="table table-bordered table-striped table-hover"
                        OnRowDataBound="GridView1_RowDataBound"
                        OnRowCreated="GridView1_RowCreated">

                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                            <asp:BoundField DataField="TotalSCBillGenerated" HeaderText="SC Bill Generated" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TotalRentBillGenerated" HeaderText="Rent Bill Generated" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingRentBillForGenerate" HeaderText="Pending for Generation" ItemStyle-HorizontalAlign="Right" />
                        </Columns>

                        <FooterStyle BackColor="#f1f1f1" Font-Bold="true" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- ===== DISTRICT GRID ===== -->
        <div id="DivDistrict" runat="server" visible="false" class="mb-4">
            <div class="card">
                <div class="card-header fw-bold bg-light">
                    District Wise Summary
                </div>
                <div class="card-body table-responsive">
                    <asp:GridView ID="GrdDistrict" runat="server"
                        AutoGenerateColumns="false"
                        ShowFooter="true"
                        CssClass="table table-bordered table-striped table-hover">

                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                            <asp:BoundField DataField="TotalSCBillGenerated" HeaderText="SC Bill Generated" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TotalRentBillGenerated" HeaderText="Rent Bill Generated" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingRentBillForGenerate" HeaderText="Pending for Generation" ItemStyle-HorizontalAlign="Right" />
                        </Columns>

                        <FooterStyle BackColor="#f1f1f1" Font-Bold="true" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- ===== BRANCH GRID ===== -->
        <div id="DivBranch" runat="server" visible="false">
            <div class="card">
                <div class="card-header fw-bold bg-light">
                    Branch Wise Summary
                </div>
                <div class="card-body table-responsive">
                    <asp:GridView ID="GrdBranch" runat="server"
                        AutoGenerateColumns="false"
                        ShowFooter="true"
                        CssClass="table table-bordered table-striped table-hover"
                        OnRowDataBound="GrdBranch_RowDataBound"
                        OnRowCreated="GrdBranch_RowCreated">

                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                            <asp:BoundField DataField="District_Id" HeaderText="District ID" />
                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                            <asp:BoundField DataField="TotalSCBillGenerated" HeaderText="SC Bill Generated" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TotalRentBillGenerated" HeaderText="Rent Bill Generated" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingRentBillForGenerate" HeaderText="Pending for Generation" ItemStyle-HorizontalAlign="Right" />
                        </Columns>

                        <FooterStyle BackColor="#f1f1f1" Font-Bold="true" />
                    </asp:GridView>
                </div>
            </div>
        </div>

    </fieldset>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>

    <script>
        var grid = $('#<%= GrdDistrict.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>

    <script>
        var grid = $('#<%= GrdBranch.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>

