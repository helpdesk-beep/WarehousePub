<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="~/Reports/NewStorageCapacityReport/Acceptance_Wise_WHR_Details_For_All_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Acceptance_Wise_WHR_Details_For_All_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid py-3">
        <!-- Header Section -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center border-bottom pb-2">
                    <div>
                        <h1 class="h4 text-primary mb-0">M.P. WAREHOUSING & LOGISTICS CORPORATION</h1>
                        <h2 class="h5 text-dark mb-0">Acceptance Wise WHR Details</h2>
                        <div class="mt-2">
                            <span class="badge bg-info fs-6 p-2">Quantity in Quintals</span>
                        </div>
                    </div>
                    <div class="text-end">
                        <div class="badge bg-light text-dark p-2">
                            <strong>Date:</strong>
                            <asp:Label ID="labelName" runat="server" CssClass="fw-bold ms-1"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Filter Section - First Row -->
        <div class="row mb-3">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Division Name</label>
                                <asp:DropDownList CssClass="form-control show-loader-ddl" ID="ddldivision" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">District Name</label>
                                <asp:DropDownList CssClass="form-control show-loader-ddl" ID="ddldistrict" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Branch Name</label>
                                <asp:DropDownList CssClass="form-control show-loader-ddl" ID="ddlbranch" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Session</label>
                                <asp:DropDownList ID="ddlcropyear" runat="server" CssClass="form-control" AutoPostBack="false">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="Rabi202526">Rabi 2025-26</asp:ListItem>
                                    <asp:ListItem Value="Rabi202425">Rabi 2024-25</asp:ListItem>
                                    <asp:ListItem Value="Kharif202425">Kharif 2024-25</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Commodity</label>
                                <asp:ListBox ID="ddlcommodity" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control">
                                    <asp:ListItem Value="22">Wheat-PSS</asp:ListItem>
                                    <asp:ListItem Value="63">GRAM</asp:ListItem>
                                    <asp:ListItem Value="64">LENTIL</asp:ListItem>
                                    <asp:ListItem Value="33">Mustard-Sarason</asp:ListItem>
                                    <asp:ListItem Value="92">Moong</asp:ListItem>
                                    <asp:ListItem Value="27">Urad</asp:ListItem>
                                    <asp:ListItem Value="26">Soya-Beans</asp:ListItem>
                                    <asp:ListItem Value="13">Paddy-Common</asp:ListItem>
                                    <asp:ListItem Value="129">Fortified_Rice</asp:ListItem>
                                </asp:ListBox>
                            </div>
                            <div class="col-md-2"style="padding-top: 28px;">
                                <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-success btn-sm show-loader" OnClick="btnshow_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Status Message -->
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div class="row mb-3">
                <div class="col-12">
                    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-warning d-flex align-items-center mb-0"></asp:Label>
                </div>
            </div>
        </asp:Panel>

        <!-- Data Grid Section -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                                OnRowDataBound="GridView1_RowDataBound"
                                OnRowCreated="GridView1_RowCreated"
                                OnDataBound="OnDataBound"
                                AutoGenerateColumns="false"
                                CssClass="table table-bordered table-striped table-hover mb-0">

                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name">
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID">
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Acceptance No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAN" runat="server" Text='<%# Eval("Acceptance_No") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="WHR Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHR_Id" runat="server" Text='<%# Eval("WHR_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acceptance Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAD" runat="server" Text='<%# Eval("Acceptance_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="WHR Issue Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWID" runat="server" Text='<%# Eval("WHR_Issue_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acceptance Bags">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAB" runat="server" Text='<%# Eval("No_of_Bags") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="WHR Bags">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWB" runat="server" Text='<%# Eval("TotalBags_Received") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acceptance Qty (Qtl)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("Rec_Qty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="WHR Qty (Qtl)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWQ" runat="server" Text='<%# Eval("Total_Qty_Received") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>
                                </Columns>

                                <HeaderStyle CssClass="bg-primary text-white" />
                                <FooterStyle CssClass="bg-light fw-bold" />
                                <RowStyle CssClass="align-middle" />
                                <AlternatingRowStyle CssClass="bg-light" />
                                <SelectedRowStyle CssClass="table-warning" />
                                <PagerStyle CssClass="pagination justify-content-center my-3" />

                                <EmptyDataTemplate>
                                    <div class="alert alert-info text-center m-3">
                                        <i class="bi bi-info-circle me-2"></i>No records found for the selected criteria.
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
                        <li class="mb-1"><i class="bi bi-dot"></i>All quantities are displayed in Quintals (Qtl)</li>
                        <li class="mb-1"><i class="bi bi-dot"></i>Acceptance vs WHR comparison report</li>
                        <li class="mb-1"><i class="bi bi-dot"></i>Filter by Division, District, Branch, Session and Commodity</li>
                        <li><i class="bi bi-dot"></i>Export options available for PDF and Excel formats</li>
                    </ul>
                </div>
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
