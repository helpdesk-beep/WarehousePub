<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="PaddyStorageLossDetail.aspx.cs" Inherits="Accounting_PaddyStorageLossDetail" Title="Paddy Loss Detail" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow:scroll; height: 350px; overflow-x:hidden">
                                                           <table cellpadding="2" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="4" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="स्कंध मे परिलक्षित भंडारण कमी की जानकारी"></asp:Label>
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
        </asp:DropDownList>
</td>
       <td align="left">
                                            <asp:Label ID="lblCommodity" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                                Text="स्कंद का नाम"></asp:Label></td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCommodity" runat="server" Width="155px" Height="25px" TabIndex="3">
                                            </asp:DropDownList>
                                        </td>
    </tr>
    <tr>

</td>
<td class="style1">
            <asp:Label ID="lblEmail" Visible="true" runat="server" Text="वर्ष" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td class="style1">
          <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="false" Width="200px"
                                                                >
                                                              
                                                            </asp:DropDownList>
 </td>
 

    </tr>
                                                           <tr>
                                                           <td colspan="4">
                                                           <table width="100%" border="1px" cellspacing="0px" cellpadding="0px">
                                                           <tr>
                                                         
                                        <td align="center" colspan="5"><asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Blue"
                                        Text="स्कंध जमा का विवरण"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                           <th>गोदाम क्र॰</th>
                                                            <th>प्रथम जमा दिनांक</th>
                                                             <th>वजन</th>
                                                              <th>औसत नमी</th>
                                                               <th>तौल की विधि</th>
                                                           </tr>
                                                            <tr>
                                                          <td align="center">
                                                           <asp:TextBox ID="txtGodown" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text=""
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           
                                                          </td>
                                                          <td align="center">
                                                            <asp:TextBox ID="txtFirstDate" runat="server" BackColor="LemonChiffon"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" ontextchanged="txtFirstDate_TextChanged"
              ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtFirstDate"></cc1:CalendarExtender>
      
                                    
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="txtRWeight" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtRWeight"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         <td align="center">
                                                          <asp:TextBox ID="txtAvgMos" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtAvgMos"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         <td align="center">
                                                           <asp:DropDownList ID="ddlRWeighmentMode" runat="server" Width="155px" Height="25px"
                                                TabIndex="14">
                                                <asp:ListItem>10%</asp:ListItem>
                                                <asp:ListItem>100%</asp:ListItem>
                                            </asp:DropDownList>
           
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
                                        Text="स्कंध भुगतान का विवरण"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                          
                                                            <th>स्कंध निरंक अंतिम दिनांक</th>
                                                             <th>वजन</th>
                                                              <th>औसत नमी</th>
                                                               <th colspan="2">तौल की विधि</th>
                                                               
                                                           </tr>
                                                            <tr>
                                                          
                                                          <td align="center">
                                                            <asp:TextBox ID="txtLastDate" runat="server" BackColor="LemonChiffon"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" ontextchanged="txtLastDate_TextChanged"
              ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtLastDate"></cc1:CalendarExtender>
      
                                    
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="txtIWeight" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtIWeight"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         <td align="center">
                                                          <asp:TextBox ID="txtIAvgMos" runat="server" BackColor="LemonChiffon" AutoPostBack="false" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtIAvgMos"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>
                                                         <td align="center" colspan="2">
                                                           <asp:DropDownList ID="ddlIWeighmentMode" runat="server" Width="155px" Height="25px"
                                                TabIndex="14">
                                                <asp:ListItem>10%</asp:ListItem>
                                                <asp:ListItem>100%</asp:ListItem>
                                            </asp:DropDownList>
          
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
                                        Text="भंडारण की मात्रा मे कमी का कारण"></asp:Label></td>
                                                           </tr>
                                                           <tr>
                                                             <%--<th>माह</th>
                                                              <th>दिन</th>--%>
                                                               <th colspan="2">कमी का कारण</th>
                                                               
                                                           </tr>
                                                            <tr>
                                                          
                             <%--                             <td align="center">
                                                            <asp:TextBox ID="txtMonth" runat="server" BackColor="LemonChiffon"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" Text="0"
              ></asp:TextBox>
               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtMonth"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
      
                                    
                                                          </td>
                                                          <td align="center">
                                                           <asp:TextBox ID="txtDay" runat="server" BackColor="LemonChiffon" AutoPostBack="true" Text="0"
        TabIndex="9" CssClass="tb6" Width="100px" Height="20px" 
          ></asp:TextBox>
           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtDay"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                                                          </td>--%>
                                                   
                                                         <td align="center" colspan="3">
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
                                                             <asp:Button ID="btnNew" runat="server" Text="New Entry" Visible="false" 
                                                                CssClass="BTNBLUE" onclick="btnNew_Click"/>
                                                            <asp:Button ID="btnPSubmit" runat="server" Text="Submit" Visible="true" 
                                                                CssClass="BTNBLUE" onclick="btnPSubmit_Click"/>
                                                                <asp:Button ID="btnPCancel" runat="server" Text="Cancel" Visible="true" 
                                                                CssClass="BTNBLUE" onclick="btnPCancel_Click" />
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
</asp:Content>
