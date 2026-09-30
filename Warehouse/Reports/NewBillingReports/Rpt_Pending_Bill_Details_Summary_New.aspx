<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master"
    AutoEventWireup="true"
    CodeFile="Rpt_Pending_Bill_Details_Summary_New.aspx.cs"
    Inherits="Reports_NewBillingReports_Rpt_Pending_Bill_Details_Summary_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <style>
        .report-card {
            background: #ffffff;
            border: 1px solid #ccc;
            border-radius: 6px;
            padding: 20px;
            margin-top: 15px;
        }

        .report-title {
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            border-bottom: 2px solid #000;
            padding-bottom: 8px;
            margin-bottom: 20px;
        }

        .grid-wrapper {
            overflow-x: auto;
        }

        .Grid th {
            background-color: #f2f2f2;
            text-align: center;
            font-weight: bold;
            white-space: nowrap;
        }

        .Grid td {
            vertical-align: middle;
        }

        .Grid tr:hover {
            background-color: #f9f9f9;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">

    <div class="container-fluid">
        <div class="report-card">

            <!-- Report Title -->
            <div class="text-center">
                <h4 class="report-title">
                    Pending Bills For Generation at Branch Level (SC)
                </h4>
            </div>

            <!-- Grid -->
            <div class="grid-wrapper">
                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="False"
                    ShowFooter="true"
                    CssClass="table table-bordered table-striped table-hover mb-0 Grid"
                    AlternatingRowStyle-CssClass="alt"
                    PagerStyle-CssClass="pgr">

                    <Columns>

                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Regionnm"
                            HeaderText="Region"
                            ItemStyle-HorizontalAlign="Left"
                            ItemStyle-Width="25%" />

                        <asp:TemplateField HeaderText="No. of Storage Charges Bills Pending">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkPending"
                                    runat="server"
                                    Target="_blank"
                                    NavigateUrl='<%# "~/Reports/States/Godown_Wise_Pending_PAyment_Status.aspx?Region_ID=" + Eval("Region_ID") %>'
                                    Text='<%# Eval("TotalBillPendingForGeneration") %>'
                                    ForeColor="Blue"
                                    Font-Bold="true">
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="30%" />
                        </asp:TemplateField>

                    </Columns>

                    <FooterStyle Font-Bold="True" />
                </asp:GridView>
            </div>

        </div>
    </div>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        $(document).ready(function () {
            var grid = $('#<%= GridView1.ClientID %>');

            // Convert first row to THEAD for DataTable
            grid.prepend($("<thead></thead>").append(grid.find("tr:first")));

            BindDatatable(grid);
        });
    </script>
</asp:Content>
