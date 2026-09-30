<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Godown_Owners_Account.aspx.cs" Inherits="Accounting_frm_Godown_Owners_Account" Title="Account Detail" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
        <div>
<table style="width: 100%;">
<tr id="msg">
<td colspan="4"><asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
</tr>
 <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center">
            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Godown Owners Account Detail"></asp:Label></td>
    </tr>
    
    <tr>
   <td>
     <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="Godown Name"></asp:Label>
    </td>
<td valign="middle"> 
       <asp:DropDownList ID="ddlGodown" runat="server" AutoPostBack="false" 
           TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
            >
                                                            </asp:DropDownList></td>
<td> 
    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Account Holder Name"></asp:Label></td>
    <td>
        <asp:TextBox ID="txtGOwnerName" runat="server" Height="22px" Width="200px"></asp:TextBox></td>
</tr>
<tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
   <td>
     <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="बैंक शाखा जिला(District)"></asp:Label>
    </td>
<td valign="middle"> 
       <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" 
           TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" onselectedindexchanged="ddlDistrict_SelectedIndexChanged" 
            >
                                                            </asp:DropDownList></td>
<td> 
    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label3" runat="server" Text="Bank Name"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddlBankName" runat="server" AutoPostBack="True" 
                TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" onselectedindexchanged="ddlBankName_SelectedIndexChanged"
              >
            </asp:DropDownList></td>
</tr>
<tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
<tr>
   <td>
     <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="Bank Branch"></asp:Label>
    </td>
<td valign="middle"> 
       <asp:DropDownList ID="ddlBBranch" runat="server" AutoPostBack="True" 
           TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" onselectedindexchanged="ddlBBranch_SelectedIndexChanged" 
            >
                                                            </asp:DropDownList></td>
<td>
<asp:Label ID="lblcrate" Visible="true" runat="server" Text="IFSC Code" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtIFSC" Visible="true" ReadOnly="true" AutoPostBack="false" BackColor="LemonChiffon" Text=""  
        TabIndex="9" CssClass="tb6" Height="22px" Width="200px"
        ></asp:TextBox>
</td>
    </tr>
    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
    <tr>
                                                            <td>
    <asp:Label ID="lbltod" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Account No."></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtAccNo" runat="server" Height="22px" Width="200px"></asp:TextBox>
   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtAccNo" ValidChars="0123456789"
                                        >
                                    </cc1:FilteredTextBoxExtender></td>
                                                            <td>
    <asp:Label ID="Label4" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Re Type Account No."></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtAccNo2" runat="server" Height="22px" Width="200px"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAccNo2" ValidChars="0123456789"
                                       >
                                    </cc1:FilteredTextBoxExtender>
   </td>
    </tr>
    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
        <tr>
                                                            <td>
    <asp:Label ID="Label6" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="PAN No."></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtPAN" runat="server" Height="22px" Width="200px"></asp:TextBox>
   
</td>
                                                            <td>
    <asp:Label ID="Label7" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="GST No."></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtGSTNo" runat="server" Height="22px" Width="200px">

   </asp:TextBox> 
   </td>
    </tr>
        <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
    <tr>
                                                            <td>
    <asp:Label ID="Label5" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Mobile Number"></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtMobile" runat="server" Height="22px" Width="200px"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtMobile" ValidChars="0123456789"
                                        >
                                    </cc1:FilteredTextBoxExtender>
</td>
                                                            <td>
    <asp:Label ID="Label9" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Email Id"></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtEmailId" runat="server" Height="22px" Width="200px"></asp:TextBox>
   </td>
    </tr>

     
    
        <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
          <tr>
                                                            <td>
    <asp:Label ID="Label11" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Aadhar No."></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtAadharNo" runat="server" Height="22px" Width="200px"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtAadharNo" ValidChars="0123456789"
                                        >
                                    </cc1:FilteredTextBoxExtender>
</td>

    </tr>
        <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
    <tr>
                                                            <td>
    <asp:Label ID="Label10" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Account Holder Address"></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtAdd1" runat="server" Height="22px" Width="200px" placeholder="Enter Address Line-1"></asp:TextBox></td>
                                                            <td>
    <asp:TextBox ID="txtAdd2" runat="server" Height="22px" Width="200px" placeholder="Enter Address Line-2"></asp:TextBox>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtAddCity" runat="server" Height="22px" Width="200px" placeholder="Enter City/District"></asp:TextBox>
   </td>
    </tr>
    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
<tr>
<td align="center" colspan="4">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" Width="100px" 
        CssClass="BTNBLUE" onclick="btnSubmit_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" 
        CssClass="BTNBLUE" onclick="btnCancel_Click"/>
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

