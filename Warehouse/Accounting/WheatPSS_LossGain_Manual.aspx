<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="WheatPSS_LossGain_Manual.aspx.cs" Inherits="Accounting_WheatPSS_LossGain_Manual" Title="Wheat-PSS Loss Gain" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <style type="text/css">
        .style1
        {
            height: 26px;
        }
     .style2
     {
         height: 14px;
     }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow:scroll; height: 550px; overflow-x:hidden">
                                                           <table cellpadding="2" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="4" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="13pt" ForeColor="WhiteSmoke"
                                        Text="उपार्जित गेहु के भंडारण/भुगतान उपरांत मासांत पर शेष स्कंध की मात्रा एवं प्राप्त/परिलक्षित कुल आधिक्य/कमी की जानकारी का विवरण"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           
                                                           <tr>
    <td class="style1">
<asp:Label ID="lblmob" runat="server" Visible="true" Text="जमाकर्ता का नाम" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td class="style1">
<asp:DropDownList ID="ddldepos_name" runat="server" TabIndex="1" Height="25px" 
            Width="200px" Font-Size="10pt"
            AutoPostBack="false" 
            >
            <asp:ListItem Value="129">MPSCSC</asp:ListItem>
        </asp:DropDownList>
</td>
       <td align="left">
                                            <asp:Label ID="lblCommodity" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="स्कंध का नाम"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCommodity" runat="server" Width="155px" Height="25px" TabIndex="3">
                                            <asp:ListItem Value="22">Wheat-PSS</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
    </tr>
    <tr>

</td>
<td class="style1">
            <asp:Label ID="lblEmail" Visible="true" runat="server" Text="भंडारण क्षमता का प्रकार" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td class="style1">
          <asp:DropDownList ID="ddlStrType" runat="server" TabIndex="1" Height="25px"
            Width="200px" Font-Size="10pt"
            AutoPostBack="true" onselectedindexchanged="ddlStrType_SelectedIndexChanged" 
            >
            <asp:ListItem Value="1">Godown</asp:ListItem>
              <asp:ListItem Value="2">Cap</asp:ListItem>
        </asp:DropDownList>
 </td>
 

    </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5"><asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Blue"
                                        Text="कुल जमा स्कंध का विवरण (क्विंटल मे)"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                           <th colspan="2">फसल वर्ष</th>
                                                            
                                                             <th colspan="2">वजन</th>
                                                            
                                                           </tr>
                                                            <tr>
                                                          <td align="center" colspan="2">
                                                           
                                        <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="true" Width="200px" Height="25px" onselectedindexchanged="ddlcropyear_SelectedIndexChanged"
                                                                >
                                                              
                                                            </asp:DropDownList>
           
                                                          </td>
                                                          
                                                          <td align="center" colspan="2">
                                                           <asp:TextBox ID="txtRWeight" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtRWeight"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                   
                                                         
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5"><asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Blue"
                                        ></asp:Label></td>
                                                           </tr>
                                                                <tr>
                                                         
                                        <td align="center" colspan="5">
                                        <asp:Label ID="Label3" Visible="true" runat="server" Text="भुगतान अवधि(वर्ष)" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlIssuecropyear" runat="server" AutoPostBack="false" Width="200px" Height="25px"
                                                                >
                                                              
                                                            </asp:DropDownList></td>
                                                           </tr>
                                                           <tr>
                                                          
                                                            <th>जून (Closing Weight)</th>
                                                             <th>जुलाई (Closing Weight)</th>
                                                              <th>दिसम्बर (Closing Weight)</th>
                                                               <th colspan="2">मार्च (Closing Weight)</th>
                                                               
                                                           </tr>
                                                            <tr>
                                                          
                                                          <td align="center">
                                                           <asp:TextBox ID="txtClose1" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtClose1"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="txtClose2" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtClose2"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         <td align="center">
                                                          <asp:TextBox ID="txtClose3" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtClose3"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         <td align="center">
                                                           <asp:TextBox ID="txtClose4" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtClose4"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                        
                                                           </tr>
                                                           </table>
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         <td colspan="5" align="center">
                                                         <asp:Button ID="btnAdd" runat="server" Text="ADD" Visible="true" 
                                                                CssClass="BTNBLUE" onclick="btnAdd_Click"/>
                                                         </td>
                                     
                                                           </tr>
                                                           <tr id="trGridHead" runat="server" visible="false">
                                                         <td colspan="5" align="center">
                                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="#990000"
                                        ></asp:Label>
                                                         </td>
                                     
                                                           </tr>
                                                                <tr>
                                                         <td colspan="5" align="center">
                                                         <asp:GridView ID="gvMonthClosing" runat="server" AutoGenerateDeleteButton="True"
                                                                CellPadding="4" ForeColor="#333333" GridLines="None" 
                                                                 onrowdeleting="gvMonthClosing_RowDeleting">
                                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>
                                                         </td>
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5"><asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Blue" 
                                        ></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                               <th colspan="2" class="style2">कुल प्राप्त आधिक्य(Gain) की मात्रा(क्विंटल मे)</th>
                                                                <th class="style2">यदि आधिक्य प्राप्त नहीं हुआ है तो एसी स्थति मे परिलक्षित कमी(Shortage) की कुल मात्रा (क्विंटल मे)</th>
                                                           </tr>
                                                            <tr>
                                                          <td align="center" colspan="2">
                                                           <asp:TextBox ID="txtTGain" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtTGain"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                          
                                                          <td align="center" colspan="2">
                                                           <asp:TextBox ID="txtTLoss" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtTLoss"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                   
                                                         
                                                           </tr>
                                                                                              <tr>
                                                         
                                                   
                                                         <td align="center" colspan="3">
                                                            <asp:Label ID="Label4" Visible="true" runat="server" Text="टिप्पणी (Remarks if any.)" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                          <asp:TextBox ID="txtResion" runat="server" BackColor="LemonChiffon" AutoPostBack="false" TextMode="MultiLine" Text="" Enabled="true"
        TabIndex="9" CssClass="tb6" Width="400px" Height="30px"  
          ></asp:TextBox>
           
                                                          </td>
                                                        
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                             
                                                            <asp:Button ID="btnPSubmit" runat="server" Text="Submit" Visible="true" 
                                                                CssClass="BTNBLUE" onclick="btnPSubmit_Click"/>
                                                                <asp:Button ID="btnPCancel" runat="server" Text="Cancel" Visible="true" 
                                                                CssClass="BTNBLUE" onclick="btnPCancel_Click"/>
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
</asp:Content>

