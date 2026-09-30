<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master"
AutoEventWireup="true"
CodeFile="Mobile_App_Godown_Detail_Fumigation_Report_BO.aspx.cs"
Inherits="Inspections_BO_Mobile_App_Godown_Detail_Fumigation_Report_BO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style>
    .main-box {
        width: 99%;
        margin: 10px auto;
        background: #fff;
        border: 1px solid #ddd;
        border-radius: 8px;
        padding: 10px;
    }

    .title {
        background: #0d6efd;
        color: white;
        text-align: center;
        padding: 10px;
        font-size: 22px;
        font-weight: bold;
        border-radius: 5px;
        margin-bottom: 10px;
    }

    .grid {
        width: 100%;
        border-collapse: collapse;
        font-size: 12px;
    }

    .grid th {
        background: #0d6efd;
        color: white;
        border: 1px solid #ccc;
        padding: 8px;
        text-align: center;
    }

    .grid td {
        border: 1px solid #ccc;
        padding: 6px;
        text-align: center;
    }

    .grid tr:nth-child(even) {
        background: #f5f5f5;
    }

    .btn {
        padding: 8px 15px;
        border: none;
        color: White;
        font-weight: bold;
        border-radius: 4px;
        cursor: pointer;
    }

    .btnBack {
        background: #6c757d;
    }

    .btnExcel {
        background: #198754;
    }
</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="main-box">

<div style="margin-bottom:10px;">

    <asp:Button ID="btnBack"
        runat="server"
        Text="Back"
        CssClass="btn btnBack"
        OnClick="btnBack_Click" />

    <asp:Button ID="btnExcel"
        runat="server"
        Text="Export Excel"
        CssClass="btn btnExcel"
        OnClick="btnExcel_Click" />

</div>

<div class="title">
    Godown Detail Fumigation Report
</div>

<asp:GridView ID="gvReport"
    runat="server"
    AutoGenerateColumns="false"
    CssClass="grid">

    <Columns>

        <asp:TemplateField HeaderText="S.No">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="Stack_ID"
            HeaderText="Stack ID" />

        <asp:BoundField DataField="Stack_Name"
            HeaderText="Stack Name" />

        <asp:BoundField DataField="Financial_year"
            HeaderText="Financial Year" />

        <asp:BoundField DataField="Months"
            HeaderText="Month" />

        <asp:BoundField DataField="Commodity_Name"
            HeaderText="Commodity" />

        <asp:BoundField DataField="Fumigation_Date"
            HeaderText="Fumigation Date"
            DataFormatString="{0:dd-MM-yyyy}" />

        <asp:BoundField DataField="Fumigation_Status"
            HeaderText="Fumigation Status" />

        <asp:BoundField DataField="MedicineBrand"
            HeaderText="Medicine Brand" />

        <asp:BoundField DataField="Opened"
            HeaderText="Opened" />

        <asp:BoundField DataField="Open_Status"
            HeaderText="Open Status" />

        <asp:BoundField DataField="Open_Date"
            HeaderText="Open Date"
            DataFormatString="{0:dd-MM-yyyy}" />

    </Columns>

</asp:GridView>

</div>

</asp:Content>
