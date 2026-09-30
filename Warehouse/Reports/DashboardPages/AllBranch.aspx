<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="~/Reports/DashboardPages/AllBranch.aspx.cs" Inherits="Reports_DashboardPages_AllBranch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <style>
        .depot-link a {
            color: #1565c0; /* strong blue */
            text-decoration: none; /* ❌ underline removed */
            font-weight: 600;
        }

            .depot-link a:hover {
                color: #0d47a1;
                background-color: #e3f2fd; /* light highlight */
                padding: 3px 6px;
                border-radius: 4px;
                text-decoration: none;
            }
    </style>
    <style>
        .header-bar {
            display: flex;
            justify-content: space-between; /* left + right */
            align-items: center;
            margin-bottom: 10px;
        }

        .back-btn {
            background: #2c3e50;
            color: white;
            border: none;
            padding: 6px 14px;
            border-radius: 5px;
            font-size: 13px;
            cursor: pointer;
            box-shadow: 0 2px 5px rgba(0,0,0,0.2);
        }

            .back-btn:hover {
                background: #1a252f;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">

    <div style="width: 100%; margin-bottom: 15px;">
        <div style="background: linear-gradient(135deg, #1e3c72, #2a5298); color: white; padding: 14px 10px; border-radius: 8px; text-align: center; font-size: 22px; font-weight: 600; letter-spacing: 1px; box-shadow: 0 4px 8px rgba(0,0,0,0.2);">
            🏭 All Branch 

        <div style="font-size: 13px; font-weight: 400; margin-top: 4px; opacity: 0.9;">
            All Warehouse Depot Details Overview
        </div>
        </div>
    </div>
    <div class="header-bar">
        <asp:Label ID="lblDepot" runat="server" Font-Bold="true" ForeColor="#780000">All Branches</asp:Label>

        <asp:Button ID="btnBack" runat="server" Text="⬅ Back" title="GO TO DASHBOARD"
            CssClass="back-btn show-loader"
            OnClick="btnBack_Click" />
    </div>

    <asp:GridView ID="gvDepot" runat="server" CssClass="table table-bordered table-hover Grid"
        AutoGenerateColumns="false" Width="100%" GridLines="Both"
        HeaderStyle-BackColor="#2c3e50" HeaderStyle-ForeColor="White"
        RowStyle-BackColor="#f9f9f9" AlternatingRowStyle-BackColor="#e6f2ff"
        Font-Size="12px">

        <Columns>

            <asp:BoundField DataField="DepotID" HeaderText="S. No" />
            <%--<asp:BoundField DataField="DepotName" HeaderText="Depot Name" />--%>
            <asp:HyperLinkField DataTextField="DepotName" HeaderText="Branch Name" DataNavigateUrlFields="BranchId" DataNavigateUrlFormatString="AllGodownByDepotId.aspx?BranchId={0}" ItemStyle-CssClass="depot-link show-loader" />
            <asp:BoundField DataField="DistrictId" HeaderText="District" />
            <asp:BoundField DataField="TehsilName" HeaderText="Tehsil" />
            <asp:BoundField DataField="PhoneNo" HeaderText="Phone" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="DepoCapaty" HeaderText="Capacity" />
            <asp:BoundField DataField="DepoBelongs" HeaderText="Belongs To" />
            <asp:BoundField DataField="LincenseNo" HeaderText="License No" />
            <asp:BoundField DataField="LicenceDate" HeaderText="License Date" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="OperatorName" HeaderText="Operator" />

        </Columns>
    </asp:GridView>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= gvDepot.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>


