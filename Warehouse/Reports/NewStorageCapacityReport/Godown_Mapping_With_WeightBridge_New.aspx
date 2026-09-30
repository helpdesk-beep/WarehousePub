<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Godown_Mapping_With_WeightBridge_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Godown_Mapping_With_WeightBridge_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
   <div class="container-fluid py-3">
    <!-- Header Section -->
    <div class="row mb-4">
        <div class="col-12">
            <div class="d-flex justify-content-between align-items-center border-bottom pb-2">
                <div>
                    <h1 class="h4 text-primary mb-0">Godown Mapping With Weight Bridge</h1>
                    <h2 class="h5 text-dark mb-0">Region & District Wise Status</h2>
                </div>
            </div>
        </div>
    </div>

    <!-- Filter Section -->
    <div class="row mb-4">
        <div class="col-12">
            <div class="card shadow-sm">
                <div class="card-body">
                    <div class="row g-3 align-items-end">
                        <div class="col-md-2">
                            <label class="form-label fw-bold text-secondary mb-1">Select Region</label>
                            <asp:DropDownList ID="ddlRegion" runat="server" 
                                CssClass="form-control" 
                                AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <label class="form-label fw-bold text-secondary mb-1">Select District</label>
                            <asp:DropDownList ID="ddlDistrict" runat="server" 
                                CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2 d-flex align-items-end">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                CssClass="btn btn-primary w-100 show-loader" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Data Grid Section -->
    <div class="row">
        <div class="col-12">
            <div class="card shadow-sm">
                <div class="card-body p-0">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" 
                            CssClass="table table-bordered table-striped table-hover mb-0"
                            AutoGenerateColumns="False" 
                            OnPreRender="GridView1_PreRender" 
                            OnRowDataBound="GridView1_RowDataBound"
                            GridLines="None">
                            
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                    <ItemStyle CssClass="text-center align-middle" Width="5%" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="Regionnm" HeaderText="Region">
                                    <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                    <ItemStyle CssClass="align-middle" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="District_Name" HeaderText="District">
                                    <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                    <ItemStyle CssClass="align-middle" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="DepotName" HeaderText="Depot">
                                    <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                    <ItemStyle CssClass="align-middle" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="Total Godown" HeaderText="Total Godown">
                                    <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                    <ItemStyle CssClass="text-center align-middle" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="Entry By BM" HeaderText="Entry By BM">
                                    <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                    <ItemStyle CssClass="text-center align-middle" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="Pending" HeaderText="Pending">
                                    <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                    <ItemStyle CssClass="text-center align-middle" />
                                </asp:BoundField>
                            </Columns>
                            
                            <HeaderStyle CssClass="bg-primary text-white" />
                            <RowStyle CssClass="align-middle" />
                            <AlternatingRowStyle CssClass="bg-light" />
                            <SelectedRowStyle CssClass="table-warning" />
                            <PagerStyle CssClass="pagination justify-content-center my-3" />
                            
                            <EmptyDataTemplate>
                                <div class="alert alert-info text-center m-3">
                                    <i class="bi bi-info-circle me-2"></i>Select region and district, then click "Search" to view mapping status.
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Summary Information -->
    <div class="row mt-3">
        <div class="col-12">
            <div class="alert alert-secondary">
                <strong class="d-block mb-2">Note:</strong> 
                <ul class="list-unstyled mb-0">
                    <li class="mb-1"><i class="bi bi-dot"></i> Shows mapping status of godowns with weight bridges</li>
                    <li class="mb-1"><i class="bi bi-dot"></i> "Total Godown": Number of godowns in the area</li>
                    <li class="mb-1"><i class="bi bi-dot"></i> "Entry By BM": Number of godowns mapped by Branch Manager</li>
                    <li><i class="bi bi-dot"></i> "Pending": Number of godowns pending for mapping</li>
                </ul>
            </div>
        </div>
    </div>
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
</asp:Content>

