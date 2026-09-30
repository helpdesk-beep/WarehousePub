<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="GodownType.aspx.cs" Inherits="Reports_DashboardPages_GodownType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
    <div style="width: 100%; margin-bottom: 15px;">
    <div style="background: linear-gradient(135deg, #1e3c72, #2a5298); color: white; padding: 14px; border-radius: 8px; text-align: center; font-size: 20px; font-weight: 600; box-shadow: 0 4px 8px rgba(0,0,0,0.2);">
        🏬 Godown LIST BY TYPE
    <div style="font-size: 13px; margin-top: 4px; opacity: 0.9;">
        Filtered by Selected Godown Type
    </div>
    </div>
</div>


<div class="header-bar">
<asp:Label ID="lblType" runat="server" Font-Bold="true" ForeColor="#780000"></asp:Label>
    <asp:Button ID="btnBack" runat="server" Text="⬅ Back"
        CssClass="back-btn show-loader"
        OnClick="btnBack_Click" />
</div>

<asp:GridView ID="gvGodown" runat="server"
    CssClass="table table-bordered table-hover"
    AutoGenerateColumns="false" Width="100%" GridLines="Both"
    HeaderStyle-BackColor="#2c3e50" HeaderStyle-ForeColor="White"
    RowStyle-BackColor="#f9f9f9" AlternatingRowStyle-BackColor="#e6f2ff"
    Font-Size="12px">

    <Columns>

        <asp:TemplateField HeaderText="S. No">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="BranchId" HeaderText="Branch ID" />
        <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
        <asp:BoundField DataField="Godown_Capacity" HeaderText="Capacity" />
        <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />
        <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" />
        <asp:BoundField DataField="Godown_Mobile" HeaderText="Mobile" />

    </Columns>
</asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= gvGodown.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>

