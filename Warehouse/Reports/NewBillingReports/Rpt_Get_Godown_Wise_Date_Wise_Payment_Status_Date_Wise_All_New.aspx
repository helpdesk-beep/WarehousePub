<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Get_Godown_Wise_Date_Wise_Payment_Status_Date_Wise_All_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_Get_Godown_Wise_Date_Wise_Payment_Status_Date_Wise_All_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
    <style>
    /* Hide toggle button */
    #toggleBtn {
        display: none !important;
    }

    /* Hide sidebar completely */
    #sidebar {
        display: none !important;
    }

    /* Remove margin from main body to make it full width */
    #mainBody {
        margin-left: 0 !important;
        width: 100% !important;
    }

    /* Adjust navbar brand margin when toggle button is hidden */
    .navbar-brand {
        margin-left: 15px !important;
    }

    /* Make content area full width */
    .content {
        width: 100% !important;
    }

    /* Adjust container fluid */
    .container-fluid {
        padding-left: 15px !important;
        padding-right: 15px !important;
        max-width: 100% !important;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
<div class="container-fluid py-3">

     <!-- Header Section -->
 <div class="row mb-4">
     <div class="col-12">
         <div class="d-flex justify-content-center align-items-center border-bottom pb-2">
             <div>
                 <h1 class="h4 text-primary mb-0">M.P. Warehousing & Logistics Corporarion</h1>
                 <h2 class="h5 text-dark mb-0">Region Wise Payment Received from MPSCSC (From August 2020)</h2>
             </div>
         </div>
     </div>
 </div>
    <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
        <tr>
            <td style="text-align: left;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                    OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                    CssClass="table table-bordered table-striped table-hover mb-0 Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                    <Columns>

                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Regionnm" HeaderText="Region" />                               
                        <asp:BoundField DataField="District_Name" HeaderText="District" />                               
                        <asp:BoundField DataField="DepotName" HeaderText="Branch" />                               
                        <asp:BoundField DataField="Godown" HeaderText="Godown" />                               
                        <asp:BoundField DataField="Bill_Number" HeaderText="Bill_Number" />                               
                        <asp:BoundField DataField="Crop_Year" HeaderText="Crop_Year" />                               
                        <asp:BoundField DataField="Financial_Year" HeaderText="Financial_Year" />                               
                        <asp:BoundField DataField="Month_Name" HeaderText="Month" />                               
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />                               
                       
                    </Columns>
                    <FooterStyle Font-Bold="True" ForeColor="Black" />
                </asp:GridView>
            </td>
        </tr>
    </table>
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
        $(document).ready(function () {
            // 1. Hide toggle button
            $('#toggleBtn').hide();

            // 2. Collapse/Close sidebar
            $('#sidebar').hide();

            // 3. Adjust main content to full width
            if ($('#main-content').length) {
                $('#main-content').css({
                    'margin-left': '0',
                    'width': '100%'
                });
            }

            // 4. Disable the toggleNav function
            window.toggleNav = function () {
                return false;
            };

            // 5. Force close any open collapsible menus in sidebar
            $('.sidebar-nav .collapse').removeClass('show');

            // 6. Remove click events from sidebar links
            $('.sidebar-link').off('click');

            // 7. Also close sidebar if there's a close function
            if (typeof closeSidebar === 'function') {
                closeSidebar();
            }

            // 8. Override Bootstrap collapse events
            $('[data-bs-toggle="collapse"]').each(function () {
                $(this).attr('data-bs-toggle', '');
                $(this).off('click');
            });
        });

        var grid = $('#<%= GridView1.ClientID %>');
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);
    </script>

</asp:Content>

