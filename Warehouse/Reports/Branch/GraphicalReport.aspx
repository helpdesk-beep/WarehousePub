<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="GraphicalReport.aspx.cs" Inherits="Reports_Branch_GraphicalReport" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h3>Total Godown Utilization(In %)</h3>
    <asp:BarChart ID="BarChart1" runat="server" ChartHeight="200" ChartWidth="900">
    
    </asp:BarChart>
    <h3>Godown wise avilable capacity Commodity wise(In Qntl.)</h3>
    Select Godown:<asp:DropDownList ID="ddlgodown" runat="server" 
        onselectedindexchanged="ddlgodown_SelectedIndexChanged" 
        AutoPostBack="True">
    </asp:DropDownList>
    <asp:AreaChart ID="AreaChart1" runat="server" ChartHeight="200" ChartWidth="900">
    </asp:AreaChart>
    
</asp:Content>

