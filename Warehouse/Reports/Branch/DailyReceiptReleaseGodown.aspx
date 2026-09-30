<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="DailyReceiptReleaseGodown.aspx.cs" Inherits="Reports_Branch_DailyReceiptReleaseGodown" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <table  >
<tr>
    
<td>
<asp:Label ID="Label1" runat="server" Text="Date From:"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBox1" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
    </asp:CalendarExtender>
</td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Date To:"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBox2" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
    </asp:CalendarExtender>
</td>
    <td>
        Godown:
    </td>
    <td>
        <asp:DropDownList ID="ddlgodownlist" runat="server" ></asp:DropDownList>
    </td>
<td>
    <asp:Button ID="btnsubmit" runat="server" Text="Submit" 
         OnClientClick="this.disabled = true; this.value='Please wait...'" UseSubmitBehavior="false" CssClass="BTNBLUE" OnClick="btnsubmit_Click" />
</td>
<td>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnexel" runat="server"  Text="Import To Exel" 
        OnClientClick="this.disabled = true; this.value='Importing'" UseSubmitBehavior="false" CssClass="BTNBLUE" OnClick="btnexel_Click" />
</td>
</tr>
</table>


    <div id="toexport" runat="server">
        <div id="dtl" runat="server">
        Branch:<asp:Label ID="lblbranch" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   Date From: <asp:Label ID="lbldatefrom" runat="server" Text="Label"></asp:Label>    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    To Date: <asp:Label ID="lvldateto" runat="server" Text="Label"></asp:Label>
        <br />
        Print Date:<asp:Label ID="lbldateprinted" runat="server" Text="Label"></asp:Label></div>
    <asp:GridView ID="gvrr" runat="server" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="gvrr_RowDataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
        <Columns>
            <asp:BoundField DataField="commodity" HeaderText="Commodity" />
             <asp:TemplateField HeaderText = "Row Number" ItemStyle-Width="100">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>

<ItemStyle Width="100px"></ItemStyle>
    </asp:TemplateField>
            <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="WHR No."/>
            <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor/Receiver"/>
            <asp:BoundField DataField="godownname" HeaderText="Godown ID/Name"/>
            <asp:TemplateField HeaderText="Received Bags">
                <FooterTemplate>
                    <asp:Label ID="recbags" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("recbags") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Received Qty">
                <FooterTemplate>
                    <asp:Label ID="lblrecqty" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("recweght") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="WHR_Issue_Date" HeaderText="Received Date"/>
            <%--<asp:BoundField DataField="delbags" HeaderText="Issued Bags"/>
            <asp:BoundField DataField="delwght" HeaderText="Issued Qty"/>--%>
            <asp:TemplateField HeaderText="Issued Bags">
                <FooterTemplate>
                    <asp:Label ID="lblissubags" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("delbags") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Issued Qty">
                <FooterTemplate>
                    <asp:Label ID="lblissueqty" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("delwght") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DeliveryDate" HeaderText="Issue Date"/>
            <asp:BoundField  HeaderText="Signature of Incharge"/>
            
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>
          </div>
        
</asp:Content>

