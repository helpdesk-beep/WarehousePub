<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="MenualWhr.aspx.cs" Inherits="IssueCenterLevel_Storage_MenualWhr" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" EnableModelValidation="True" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="WHR_Request" HeaderText="DepositerForm" />
            <asp:BoundField DataField="Acceptance_Date" HeaderText="Date of Deposit" />
            <asp:BoundField DataField="Recd_Qty" HeaderText="Qty" />
            <asp:BoundField DataField="Recd_Bags" HeaderText="Bags" />
            <asp:BoundField DataField="Godown" HeaderText="Godown" />
            <asp:TemplateField HeaderText="WHR No">
                <ItemTemplate>
                    <asp:TextBox ID="txtwhr" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="WHR Date">
                <ItemTemplate>
                    <asp:TextBox ID="txtwhrdate" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtwhrdate_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="txtwhrdate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField HeaderText="Submit" SelectText="Submit" 
                ShowSelectButton="True" />
            <asp:BoundField DataField="GodownID" HeaderText="GodownID" />
            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />
        </Columns>
    </asp:GridView>
</asp:Content>

