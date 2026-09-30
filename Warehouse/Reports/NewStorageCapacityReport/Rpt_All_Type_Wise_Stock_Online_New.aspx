<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_All_Type_Wise_Stock_Online_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_All_Type_Wise_Stock_Online_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
    <div class="content-wrapper">
    <fieldset>
        <legend align="center">Division,District,Branch,Godown,Commodity Wise Stock Position(Online)<label style="color: red">(In M.T.)</label></legend>
        
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
                    <asp:DropDownList ID="ddlcommodity" runat="server" CssClass="form-control" AutoPostBack="true" selectionmode="Multiple" OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= grddivision.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);


        var grid = $('#<%= grddivision.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);


        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);


        var grid = $('#<%= GridView2.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

        var grid = $('#<%= grdgdn.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>--%>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        // First, define the BindDatatable function
        function BindDatatable(grid) {
            console.log('Initializing DataTable for:', grid.selector || grid);

            var $grid = $(grid);

            // Check if grid has data
            if ($grid.find('tbody tr').length === 0) {
                console.log('No data in grid');
                return;
            }

            // Destroy existing DataTable if exists
            if ($.fn.DataTable && $.fn.DataTable.isDataTable($grid)) {
                $grid.DataTable().destroy();
                $grid.find('thead').remove();
            }

            // Create THEAD if not exists
            if ($grid.find('thead').length === 0) {
                // Clone first row as header
                var $firstRow = $grid.find('tr:first').clone();
                $grid.prepend($('<thead></thead>').append($firstRow));

                // Remove the cloned row from tbody
                $grid.find('tbody tr:first').remove();
            }

            // Initialize DataTable
            try {
                var dataTable = $grid.DataTable({
                    paging: true,
                    pageLength: 10,
                    lengthChange: true,
                    searching: true,
                    ordering: true,
                    info: true,
                    autoWidth: false,
                    responsive: true,
                    scrollX: true,
                    destroy: true,
                    retrieve: true,
                    dom: 'Bfrtip',
                    buttons: [
                        {
                            extend: 'copy',
                            text: '<i class="fas fa-copy"></i> Copy',
                            className: 'btn btn-secondary btn-sm',
                            exportOptions: {
                                columns: ':visible'
                            }
                        },
                        {
                            extend: 'excel',
                            text: '<i class="fas fa-file-excel"></i> Excel',
                            className: 'btn btn-success btn-sm',
                            title: 'Stock_Report',
                            exportOptions: {
                                columns: ':visible'
                            }
                        },
                        {
                            extend: 'pdf',
                            text: '<i class="fas fa-file-pdf"></i> PDF',
                            className: 'btn btn-danger btn-sm',
                            title: 'Stock_Report',
                            orientation: 'landscape',
                            pageSize: 'A4',
                            exportOptions: {
                                columns: ':visible'
                            },
                            customize: function (doc) {
                                doc.defaultStyle.fontSize = 8;
                                doc.styles.tableHeader.fontSize = 10;
                                doc.styles.title.fontSize = 14;
                            }
                        },
                        {
                            extend: 'print',
                            text: '<i class="fas fa-print"></i> Print',
                            className: 'btn btn-warning btn-sm',
                            title: 'Stock Report',
                            exportOptions: {
                                columns: ':visible'
                            },
                            customize: function (win) {
                                $(win.document.body).find('table')
                                    .addClass('compact')
                                    .css('font-size', '10pt');
                                $(win.document.body).find('h1')
                                    .css('text-align', 'center');
                            }
                        }
                    ],
                    language: {
                        search: "Search:",
                        lengthMenu: "Show _MENU_ entries",
                        info: "Showing _START_ to _END_ of _TOTAL_ entries",
                        infoEmpty: "Showing 0 to 0 of 0 entries",
                        infoFiltered: "(filtered from _MAX_ total entries)",
                        paginate: {
                            first: "First",
                            last: "Last",
                            next: "Next",
                            previous: "Previous"
                        }
                    },
                    initComplete: function () {
                        console.log('DataTable initialized successfully');
                        // Fix column widths
                        this.api().columns.adjust();
                    }
                });

                return dataTable;
            } catch (error) {
                console.error('Error initializing DataTable:', error);
                return null;
            }
        }

        // Function to initialize only visible grid
        function initializeVisibleDataTable() {
            console.log('Looking for visible grid...');

            var gridId = null;

            if ($('#<%= divdivision.ClientID %>').is(':visible')) {
                console.log('Division grid visible');
                gridId = '#<%= grddivision.ClientID %>';
            }
            else if ($('#<%= divregion.ClientID %>').is(':visible')) {
                console.log('Region grid visible');
                gridId = '#<%= GridView1.ClientID %>';
            }
            else if ($('#<%= divdistrict.ClientID %>').is(':visible')) {
                console.log('District grid visible');
                gridId = '#<%= GridView2.ClientID %>';
            }
            else if ($('#<%= DivGodown.ClientID %>').is(':visible')) {
                console.log('Godown grid visible');
                gridId = '#<%= grdgdn.ClientID %>';
            }

            if (gridId && $(gridId).length > 0) {
                console.log('Found grid:', gridId);
                BindDatatable(gridId);
            } else {
                console.log('No visible grid found');
            }
        }

        // Wait for everything to load
        $(document).ready(function () {
            console.log('Page loaded, initializing DataTable...');

            // Wait for GridView to render
            setTimeout(function () {
                initializeVisibleDataTable();
            }, 500);

            // Additional check
            setTimeout(function () {
                if (!$('.dataTable').length) {
                    console.log('DataTable not found, retrying...');
                    initializeVisibleDataTable();
                }
            }, 1000);
        });

        // Handle postbacks
        if (typeof Sys !== 'undefined') {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                console.log('Postback completed');
                setTimeout(function () {
                    initializeVisibleDataTable();
                    $('#pageLoader').hide();
                }, 300);
            });
        } else {
            // Alternative for non-AJAX postbacks
            $(window).on('load', function () {
                setTimeout(initializeVisibleDataTable, 300);
            });
        }
    </script>
</asp:Content>

