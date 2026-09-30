<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Update_Godown_Block.aspx.cs" Inherits="BranchPages_Update_Godown_Block" Title="Update Godowns Block" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset  style=" width:1000px; border:2px solid navy">
<center>
<div>
<table width="1000px">
<tr id="msg">
<td><asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
</tr>
<tr style="background-color: #0bb6e6; height: 25px">
 <td align="center">
            <asp:Label ID="lblheading" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Update Godowns Block"></asp:Label></td>
</tr>
<tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
<tr id="trJVSGodownRent" visible="true" runat="server">
<td>
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 50px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                         
                                                           <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                           <tr>
<td>
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lblgodown" runat="server" Text="Godown"></asp:Label>
            </td>
            <td>
            <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="false" 
                    Height="25px" Width="200px" CssClass="tb6" Font-Size="10pt"
               >
            </asp:DropDownList></td>
            <td>
            <asp:Label ID="lblcommodity" runat="server" Text="Block Name" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td><asp:DropDownList ID="ddlBlock" runat="server" Width="200px" Height="25px" 
                AutoPostBack="false"
                >
            </asp:DropDownList>
            </td>
</tr>
 <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
 <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
        
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center">
                                                       <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
             CssClass="BTNBLUE" Enabled="true" onclick="btnSubmit_Click"
                />
            <asp:Button ID="brnCancel" runat="server" Text="Close"
              CssClass="BTNBLUE"/>
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                            </td>
</tr>
    </table>
</div>
</center>
</fieldset>
</asp:Content>

