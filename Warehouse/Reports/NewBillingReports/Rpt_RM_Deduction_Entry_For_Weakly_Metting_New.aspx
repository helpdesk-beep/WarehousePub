<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_RM_Deduction_Entry_For_Weakly_Metting_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_RM_Deduction_Entry_For_Weakly_Metting_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid mt-3">

        <!-- ================= HEADER ================= -->
        <div class="text-center mb-4">
            <h2 class="mt-2 fw-bold">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h5 class="mt-1">
                क्षेत्रीय कार्यालय स्तर से 1% आधिक्य के विरुद्ध<br />
                गोदाम संचालको के देयकों से 20% रोकी गई राशि की जानकारी अपडेट करना
            </h5>
            <div class="mt-2">
                <strong>Date:</strong> <asp:Label ID="labelName" runat="server"></asp:Label>
            </div>
        </div>

        <!-- ================= FILTERS ================= -->
        <div class="card mb-4">
            <div class="card-body">
                <div class="row align-items-center g-3">
                    <div class="col-md-2 text-end">
                        <label for="ddlfinancial" class="fw-bold">Select Crop Year:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlfinancial" CssClass="form-control" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlfinancial_SelectedIndexChanged">
                            <asp:ListItem Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2 text-end">
                        <label for="ddlcommodity" class="fw-bold">Select Commodity:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlcommodity" CssClass="form-control" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged">
                            <asp:ListItem Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <!-- ================= GRID ================= -->
        <div class="table-responsive mb-5">
            <asp:GridView ID="GridView1" runat="server" ShowFooter="true"
                OnRowDataBound="GridView1_RowDataBound"
                OnRowCreated="GridView1_RowCreated"
                OnDataBound="OnDataBound"
                AutoGenerateColumns="false"
                CssClass="table table-bordered table-hover table-striped text-nowrap">

                <Columns>
                    <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                    <asp:BoundField DataField="Region_ID" HeaderText="Region_ID" />
                    <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" />
                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" />

                    <asp:TemplateField HeaderText="क्षेत्रीय कार्यालय द्वारा गोदाम संचालको के देयकों से 20% रोकी गई राशि">
                        <ItemTemplate>
                            <asp:Label ID="lblDeductionAmount" runat="server" Text='<%# Eval("DeductionAmount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="क्षेत्रीय कार्यालय द्वारा गोदाम संचालको के देयकों से 20% रोकी गई राशि का भुगतान">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountQ" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="मूल वजन में कमी एवं गेन में कमी के विरुद्ध काटी गई राशि">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount_DALG" runat="server" Text='<%# Eval("Amount_DALG") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="अन्य कारणों से काटी गई राशि">
                        <ItemTemplate>
                            <asp:Label ID="lblOther_Deduction" runat="server" Text='<%# Eval("Other_Deduction") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>
                </Columns>

                <EmptyDataTemplate>
                    <div class="text-center text-danger fw-bold p-2">No Record Found</div>
                </EmptyDataTemplate>

                <FooterStyle CssClass="fw-bold bg-light" />
                <PagerStyle CssClass="d-flex justify-content-end mt-2" />
            </asp:GridView>
        </div>

        <!-- ================= MESSAGE LABEL ================= -->
        <asp:Label ID="lblMsg" runat="server" CssClass="p-2 fw-bold text-white d-block"></asp:Label>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">

    <script>
        $(document).ready(function () {

            // ================= Live Date & Time =================
            function updateDateTime() {
                var now = new Date();

                var day = String(now.getDate()).padStart(2, '0');
                var month = String(now.getMonth() + 1).padStart(2, '0');
                var year = now.getFullYear();

                var hours = now.getHours();
                var minutes = String(now.getMinutes()).padStart(2, '0');
                var seconds = String(now.getSeconds()).padStart(2, '0');
                var ampm = hours >= 12 ? 'PM' : 'AM';
                hours = hours % 12;
                hours = hours ? hours : 12;
                hours = String(hours).padStart(2, '0');

                var formattedDateTime = day + '-' + month + '-' + year + ' ' + hours + '.' + minutes + '.' + seconds + ' ' + ampm;

                $('#<%= labelName.ClientID %>').text(formattedDateTime);
            }
            updateDateTime(); // call immediately
            setInterval(updateDateTime, 1000); // every second

            // ================= DataTable Bind =================
            var grid = $('#<%= GridView1.ClientID %>');

            // Convert first row to THEAD only if not already
            if (grid.find('thead').length === 0) {
                grid.find('tr:first').wrap('<thead></thead>');
                grid.find('tr:first').nextAll().wrapAll('<tbody></tbody>');
            }

            // Initialize DataTable
            grid.DataTable({
                paging: true,
                searching: true,
                ordering: true,
                info: true,
                autoWidth: false,
                lengthMenu: [5, 10, 25, 50],
                pageLength: 10
            });
        });
    </script>
</asp:Content>



