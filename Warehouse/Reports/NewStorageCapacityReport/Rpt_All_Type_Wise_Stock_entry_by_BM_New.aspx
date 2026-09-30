<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_All_Type_Wise_Stock_entry_by_BM_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_All_Type_Wise_Stock_entry_by_BM_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend align="center">District,Commodity,Date Wise Stock Position<label style="color: red">(In M.T.)</label></legend>
            <div class="Row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Division Name</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddldivision" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>District Name</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <div class="form-group">
                            <asp:DropDownList CssClass="form-control select2" ID="ddldistrict" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Branch Name</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddlbranch" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="Row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>WDRA Compliant</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlwdra" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlwdra_SelectedIndexChanged">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Godown Type :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddlgodowntype" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlgodowntype_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Godown Name :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddlGodown" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>
            <div class="Row">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Crop Year</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlcropyear" runat="server" CssClass="form-control" AutoPostBack="true" selectionmode="Multiple" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Depositor Type</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlDepositorType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepositorType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Depositor Name</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlDepositor" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="Row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Commodity Type</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlCommoditytype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCommoditytype_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Commodity</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <%--<asp:DropDownList ID="ddlcommodity" runat="server" CssClass="form-control" AutoPostBack="true" selectionmode="Multiple" OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>--%>
                        <asp:ListBox ID="ddlcommodity" runat="server" SelectionMode="Multiple"
                            CssClass="checkbox-multiselect form-control"></asp:ListBox>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-success btn-sm show-loader" OnClick="btnshow_Click" />
                </div>
            </div>
        </fieldset>
        <fieldset id="divdivision" runat="server" visible="false">
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="grddivision" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader"
                        AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grddivision_RowDataBound"
                        OnRowCreated="grddivision_RowCreated"
                        PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Division" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("Region") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("Commodity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                </ItemTemplate>
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
        </fieldset>

        <fieldset id="divregion" runat="server" visible="false">
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader"
                        AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="GridView1_OnRowDataBound"
                        OnRowCreated="GridView1_OnRowCreated"
                        PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="District" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("District") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("Commodity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                </ItemTemplate>
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
        </fieldset>
        <fieldset id="divdistrict" runat="server" visible="false">
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader"
                        AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="GridView2_RowDataBound"
                        OnRowCreated="GridView2_RowCreated"
                        PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("Commodity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                </ItemTemplate>
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
        </fieldset>

        <fieldset id="DivGodown" runat="server" visible="false">
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="grdgdn" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader"
                        AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdgdn_RowDataBound"
                        OnRowCreated="grdgdn_RowCreated"
                        PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("Godown") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("Commodity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                </ItemTemplate>
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
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        function initializeAllDataTables() {
            // Array of grid IDs
            var gridIds = [
            '<%= GridView1.ClientID %>',
            '<%= GridView2.ClientID %>',
            '<%= grdgdn.ClientID %>',
            '<%= grddivision.ClientID %>'
            ];

            gridIds.forEach(function (gridId) {
                var grid = $('#' + gridId);
                if (grid.length > 0 && grid.find('tbody tr').length > 0) {
                    try {
                        // Destroy existing DataTable if exists
                        if ($.fn.DataTable.isDataTable(grid)) {
                            grid.DataTable().destroy();
                            grid.find('thead').remove();
                        }

                        // Prepare for DataTable if not already prepared
                        if (grid.find('thead').length === 0) {
                            var headerRow = grid.find('tr:first');
                            if (headerRow.length > 0) {
                                var thead = $('<thead></thead>');
                                thead.append(headerRow.clone());
                                grid.prepend(thead);
                                headerRow.hide();
                            }
                        }

                        // Initialize DataTable with basic configuration
                        grid.DataTable({
                            "paging": true,
                            "pageLength": 25,
                            "lengthChange": false,
                            "searching": true,
                            "ordering": true,
                            "info": true,
                            "autoWidth": false,
                            "responsive": true,
                            "language": {
                                "search": "Search:",
                                "lengthMenu": "Show _MENU_ entries",
                                "info": "Showing _START_ to _END_ of _TOTAL_ entries",
                                "infoEmpty": "Showing 0 to 0 of 0 entries",
                                "infoFiltered": "(filtered from _MAX_ total entries)",
                                "zeroRecords": "No matching records found",
                                "paginate": {
                                    "first": "First",
                                    "last": "Last",
                                    "next": "Next",
                                    "previous": "Previous"
                                }
                            }
                        });

                    } catch (e) {
                        console.error("Error initializing DataTable for " + gridId + ": ", e);
                    }
                }
            });
        }

        // Execute when page loads
        $(document).ready(function () {
            initializeAllDataTables();
        });

        // Re-initialize after ASP.NET AJAX postback
        if (typeof (Sys) !== 'undefined') {
            Sys.Application.add_load(function () {
                initializeAllDataTables();
            });
        }
    </script>
</asp:Content>

