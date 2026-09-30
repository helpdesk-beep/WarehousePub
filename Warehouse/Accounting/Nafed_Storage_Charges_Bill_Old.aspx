<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Nafed_Storage_Charges_Bill_Old.aspx.cs" Inherits="Accounting_Nafed_Storage_Charges_Bill" Title="Nafed Bill" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
        <div>
<table style="width: 100%;">
<tr id="msg">
<td colspan="6"><asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
</tr>
 <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center">
            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="NAFED Storage Charges Bill"></asp:Label></td>
    </tr>
    
    <tr>
   <td>
     <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="Year:"></asp:Label>
    </td>
<td valign="middle"> 
       <asp:DropDownList ID="ddlFinYear" runat="server" AutoPostBack="True" 
           TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
            >
                                                            </asp:DropDownList></td>
<td> 
    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Month"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="True" 
                TabIndex="1" Height="25px" Width="200px" Font-Size="10pt"
              >
            </asp:DropDownList></td>
</tr>
<tr>
   <td>
     <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="Crop Year:"></asp:Label>
    </td>
<td valign="middle"> 
       <asp:DropDownList ID="ddlcropyr" runat="server" AutoPostBack="True" 
           TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
            >
                                                            </asp:DropDownList></td>
<td>
<asp:Label ID="lblcrate" Visible="true" runat="server" Text="Commodity Rate(Month/15 Day)" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtcomrate" Visible="true" ReadOnly="false" AutoPostBack="true" BackColor="LemonChiffon" Text="6.20" 
        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" 
        ></asp:TextBox>
        <asp:TextBox runat="server" ID="txtCPRate" Visible="true" ReadOnly="false" AutoPostBack="true" BackColor="LemonChiffon" Text="3.10"
        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" 
        ></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtcomrate"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
</td>
    </tr>
    <tr>
                                                            <td>
    <asp:Label ID="lbltod" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Commodity"></asp:Label>
    </td>
<td valign="middle"> 
    <asp:DropDownList ID="ddlcomodity" runat="server" AutoPostBack="True" 
        TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
        >
         <asp:ListItem Value="-1">--Select--</asp:ListItem>
         <asp:ListItem Value="63">GRAM</asp:ListItem>
         <asp:ListItem Value="64">LENTIL</asp:ListItem>
         <asp:ListItem Value="33">Mustard-Sarason</asp:ListItem>
         <asp:ListItem Value="52">Arahar</asp:ListItem>
         <asp:ListItem Value="27">Urad</asp:ListItem>
         <asp:ListItem Value="92">Moong</asp:ListItem>
         <asp:ListItem Value="123">RAM TIL</asp:ListItem>
         <asp:ListItem Value="31">Ground-Nut</asp:ListItem>
         <asp:ListItem Value="65">Tilli</asp:ListItem>
    </asp:DropDownList></td>

    </tr>
<tr>
<td align="center" colspan="6">
        <asp:Button ID="btnGenerateBill" runat="server" Text="Submit" Visible="true" Width="100px" 
        CssClass="BTNBLUE" onclick="btnGenerateBill_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" 
        CssClass="BTNBLUE" onclick="btnCancel_Click"/>
</td>
</tr>
<tr id="trReportsView" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
<center>
<div style="overflow: scroll; height: 500px; overflow-x: hidden">
  
    <rsweb:ReportViewer ID="ReportViewer_SC" runat="server" Width="100%" >
    </rsweb:ReportViewer>
</div>
</center> 
</fieldset>
</td> 
</tr>
<tr id="trRentBill" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 200px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Storage Bill Detail"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                             <asp:GridView ID="gvNafedStorageCharge" runat="server" AutoGenerateColumns="false">
                                                             <Columns>
                                                             <asp:BoundField HeaderText="Date" dataformatstring="{0:dd/MM/yyyy}" DataField="Deposit_Date" />
                                                             <asp:BoundField HeaderText="Opening Balance Bags" DataField="Opening_Balance" />
                                                             <asp:BoundField HeaderText="Receive Bags" DataField="Receive_Bags" />
                                                             <asp:BoundField HeaderText="Issue Bags" DataField="Issue_Bags" />
                                                             <asp:BoundField HeaderText="Closing Balance Bags" DataField="Closing_Balance" />
                                                             <asp:BoundField HeaderText="Reservation on Bags" DataField="Reserve_Bags" />
                                                             <asp:BoundField HeaderText="Chargable Bags" DataField="Chargable_Bags"/>
                                                             
                                                             <asp:BoundField HeaderText="Rate Per Bag(For 15 Day)" DataField="Per_Day_Rate" />
                                                             <asp:BoundField HeaderText="Amount" DataField="Charges" />
                                                             </Columns>
                                                             </asp:GridView>
                                                           </td>
                                                           </tr>
                                                           
    
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                            <asp:Button ID="btnGenBill" runat="server" Text="Generate Bill" Visible="true" 
                                                                CssClass="BTNBLUE" onclick="btnGenBill_Click" />
                                                                <asp:Button ID="btncancel2" runat="server" Text="Cancel" Visible="true" Width="110px" 
                                                                CssClass="BTNBLUE" onclick="btncancel2_Click" />
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                            </td>
</tr>
<tr id="trReservationBill" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
<center>
<div style="overflow: scroll; height: 170px; overflow-x: hidden">
                                                           
</div>
</center>
</fieldset>
</td>
</tr>
<%--  <tr>
   <td colspan="2" align="right"><asp:Button ID="btnsubmit" Visible="false" 
           runat="server" Text="Submit" CssClass="BTNBLUE" onclick="btnsubmit_Click"/>
   </td>
   <td colspan="2" align="left"><asp:Button ID="btncancel" Visible="false" 
           runat="server" Text="Cancel" CssClass="BTNBLUE" onclick="btncancel_Click"/>
   </td>
   </tr>--%>
</table>
</div>
        </center>
        </fieldset>
</asp:Content>

