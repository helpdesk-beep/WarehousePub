<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="DepositorMaster.aspx.cs" Inherits="Masters_DepositorMaster" Title="DepositorMaster" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <fieldset style="width: 950px; border: 2px solid navy; margin-left: 10px ; margin-right:10px">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width:930px; border: 1px solid navy; margin-top:2px">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                           
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center" >
                                                    <asp:Label ID="lblGodownMaster" runat="server" Text="Depositor Master" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
      <tr>
        <td colspan="4" valign="top" align="center">
          <asp:GridView ID="Depositor_Gridview" AllowSorting="True" AllowPaging="True" runat="server"
            DataKeyNames="Depositor_ID" 
            AutoGenerateColumns="False" Width="100%" SelectedIndex="0"  BackColor="White" 
                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" 
                OnSelectedIndexChanged="Depositor_Gridview_SelectedIndexChanged" 
                OnRowDeleting="Depositor_Gridview_RowDeleting" 
                OnPageIndexChanging="Depositor_Gridview_PageIndexChanging" CellSpacing="2">
            <Columns>
              <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" >
                  <ControlStyle Font-Underline="True" ForeColor="Blue" />
              </asp:CommandField>
              <asp:BoundField DataField="Depositor_ID" HeaderText="Depositor_ID" ReadOnly="True" SortExpression="Depositor_ID" />
              <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name" SortExpression="Depositor_Name" />
              <asp:BoundField DataField="Depositor_Type" HeaderText="Depositor Type" SortExpression="Depositor_Type" />
              <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
              <asp:BoundField DataField="Contact_No" HeaderText="Contact No" SortExpression="Contact_No" />
              
            </Columns>
            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
          </asp:GridView>
          
        </td>
        </tr>
                                     <tr>
                                          <td style="height: 5px" colspan="4">
                                          </td>
                                     </tr>        
        <tr>
           <td align="Right">
                <asp:Button ID="btnnewdp" CssClass="BTNBLUE" runat="server" Height="35px" 
                    Text="Add New" Width="80px" onclick="btnnewdp_Click" />&nbsp&nbsp
           </td>  
                                   <td align="left" >
                            &nbsp&nbsp<asp:Button ID="btnCancel" CssClass="BTNBLUE" runat="server" Height="35px" Text="Close" Width="80px" OnClick="btnCancel_Click" /></td>      
        </tr>

</table>
</div>
</center>
</fieldset>
      </td>
      </tr>
                                       <tr>
                                          <td style="height: 5px" colspan="4">
                                          </td>
               
                                     </tr>
          
          <%-----------First Part------------%>
          
          <tr>
        <td align="center" valign="top">
        <asp:Panel ID="ddltypevisible" runat="server" Visible="false">
         <fieldset style="width: 930px; border: 1px solid navy;">
                <table cellpadding="0" cellspacing="0" style="width: 100%">

                                            <tr style="background-color: #0bb6e6; ">
                                                <td colspan="2" align="center" class="style7" >
                                                    <asp:Label ID="Label8" runat="server" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr> 
                                       <tr>
                                          <td style="height: 10px" colspan="2">
                                          </td>
                                     </tr>                                            
                    <tr>
                        <td align="right" style="width: 400px">
                            <asp:Label ID="Label9" runat="server" Text="Depositor Type" Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label>
                        </td>                            
                        <td >
                           &nbsp&nbsp &nbsp&nbsp<asp:DropDownList ID="ddldptype_select" runat="server" 
                                Width="250px" Height="25px" onselectedindexchanged="ddldptype_select_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                                                                
                </table>
         </fieldset>   
         </asp:Panel>    
         </td>
         </tr>
        
        <%--    Second Part       ----------------------%>
                                    
                                 
                                     <tr>
                                          <td style="height: 5px" colspan="4">
                                          </td>
                                     </tr>                                            
  <%--  <tr>
        <td align="center" valign="top">
        <asp:Panel ID="pnlPass" runat="server" Visible="False">
         <fieldset style="width: 915px; border: 1px solid navy;">
                <table cellpadding="4" cellspacing="4" >
                
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center" >
                                                    <asp:Label ID="Label6" runat="server" Text="Depositor Details" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                    <tr>
                        <td align="left" style="width: 200px">
                            <asp:Label ID="Label2" runat="server" Text="Depositor Type" Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label>
                        </td>                            
                        <td >
                            <asp:DropDownList ID="drp_lstDepositorType" runat="server" Width="250px" Height="25px">
                            </asp:DropDownList>
                        </td>
                    </tr>                                            
                                            
                    <tr>
                        <td align="left" class="style3" >
                            <asp:Label ID="Label1" runat="server" Text="Depositor Name" Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label></td>
                        <td class="style1">
                            <asp:TextBox ID="txtDepositorName" runat="server" Width="250px"></asp:TextBox></td>
                        <td align="left" class="style3">
                            <asp:Label ID="Label3" runat="server" Text="Address"></asp:Label>
                        </td>
                        <td class="style1">
                          <asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server" Width="250px"></asp:TextBox>
                        </td>                        
                    </tr>

                    <tr>
                        <td align="left" style="width: 200px">
                            <asp:Label ID="Label4" runat="server" Text="Contact No" Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label>
                        </td>
                        <td >
                            <asp:TextBox ID="txtContactNo" runat="server" Width="250px" AutoComplete="off" onkeypress="CheckNumeric(event);"></asp:TextBox>
                        </td>
                    </tr>
                    
                  </table>
             </fieldset>
             </asp:Panel>
          </td>
         </tr>
          
          --%>
          
<%--          --------------- Start Cultivetor Design----------       --%>
         <tr>
         <td align="center" valign="top" >
         <asp:Panel ID="pnlnewdp" runat="server" Visible="False">
         <fieldset style="width: 930px; border: 1px solid navy;">
         
         <table style="width:915px;">
                   <tr>
           <td style="height: 5px" colspan="4">
           </td>
          </tr> 
                             <tr>
                             
                        <td align="left">
                            <asp:Label ID="Label22" runat="server" Text="Depositor Type(If Change)" Font-Bold="True" Font-Size="8pt" Visible="false"
                             ForeColor="navy"></asp:Label>
                        </td>                            
                        <td align="left">
                         <asp:DropDownList ID="ddlchangeDP" runat="server" 
                                Width="200px" Height="25px" onselectedindexchanged="ddldptype_select_SelectedIndexChanged" Visible="false">
                         </asp:DropDownList>
                        </td>
                        
                    </tr>
          <tr>
           <td>
           </td>
          </tr>  


                                            <tr>
                                                    <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="dlbldepositor" runat="server" Text="Depositor Name" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left" style="width: 300px"  >
                                                    <asp:TextBox ID="txtdepositor_name" runat="server" Width="200px" ></asp:TextBox>
                                                </td>
                                                        <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="lblemail" runat="server" Text="Father Name" Width="142px" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                   <td align="left" > 
                                                    <asp:TextBox ID="txtf_name" runat="server" Width="200px"
                                                        Font-Names="Verdana" Font-Size="9pt" ></asp:TextBox></td>
                                            </tr>
<%--          <tr>
           <td style="height: 2px" colspan="4">
           </td>
          </tr> --%>                                             
                    <tr>
                        <td align="left" valign="top" class="style5" >
                            <asp:Label ID="Label12" runat="server" Text="Contact No" Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label>
                        </td>
                        <td align="left" valign="top" >
                            <asp:TextBox ID="txtmobile" runat="server" Width="200px" AutoComplete="off" onkeypress="CheckNumeric(event);"></asp:TextBox>
                        </td>
                        <td align="left" valign="top" class="style6">
                            <asp:Label ID="Label14" runat="server" Text="Postal Address With Pin Code"   Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label>
                        </td>
                        <td class="style6" >
                          <asp:TextBox ID="txtaddress" TextMode="MultiLine" runat="server" Width="200px"></asp:TextBox>
                        </td>                          
                    </tr>  
<%--          <tr>
           <td style="height: 2px" colspan="4">
           </td>
          </tr>  --%>                    
                                              
          <tr >
           <td align="left" style="width: 200px" valign="top" >
              <asp:Label ID="lblDepotName" runat="server" Text="Tehsil Name" Font-Size="9pt" Font-Bold="True" ForeColor="navy">
             </asp:Label>
           </td>
           <td align="left">
                  <asp:DropDownList ID="ddltehsil" runat="server" Height="25px" Width="205px" 
                      AutoPostBack="true" onselectedindexchanged="ddltehsil_SelectedIndexChanged">
                  </asp:DropDownList>
           </td>                                                    
           <td  align="left" style="width: 200px" valign="top">
              <asp:Label ID="Label7" runat="server" Text="Village Name" Font-Bold="True" Font-Size="8pt" ForeColor="navy">
              </asp:Label>
           </td>
           <td align="left">
                  <asp:DropDownList ID="ddlVillage" runat="server" Height="25px"  Width="205px">
                  </asp:DropDownList>
           </td>                                                    
         </tr>
<%--          <tr>
           <td style="height: 2px" colspan="4">
           </td>
          </tr> --%>          
         <tr>
                                                    <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label10" runat="server" Text="Patwari Halka No" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left" style="width: 300px"  >
                                                    <asp:TextBox ID="txthlkano" runat="server" Width="200px" ></asp:TextBox>
                                                </td>
                                                        <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label11" runat="server" Text="Rin Pustika No" Width="142px" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                   <td align="left" > 
                                                    <asp:TextBox ID="txtrinpustikano" runat="server" Width="200px"
                                                        Font-Names="Verdana" Font-Size="9pt" ></asp:TextBox></td>
                                            </tr>
<%--          <tr>
           <td style="height: 2px" colspan="4">
           </td>
          </tr>      --%>                                        
                                            <tr>
                                                    <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label13" runat="server" Text="Cast / Category" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left" style="width: 300px"  >
                                                  <asp:DropDownList ID="ddlcast" runat="server" Height="25px" Width="205px" >
                                                  <asp:ListItem Text="--Select--"></asp:ListItem>
                                                  <asp:ListItem Text="ST"></asp:ListItem>
                                                  <asp:ListItem Text="SC"></asp:ListItem>
                                                  <asp:ListItem Text="OBC"></asp:ListItem>
                                                  <asp:ListItem Text="General"></asp:ListItem>
                                                  <asp:ListItem Text="Other"></asp:ListItem>
                                                  
                                                  
                                                  </asp:DropDownList>                                                   
                                                </td>
                                                        <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label15" runat="server" Text="Aadhar No" Width="142px" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                   <td align="left" > 
                                                    <asp:TextBox ID="txtuid" runat="server" Width="200px"
                                                        Font-Names="Verdana" Font-Size="9pt" ></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                        <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label16" runat="server" Text="Samagra ID" Width="142px" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                   <td align="left" > 
                                                    <asp:TextBox ID="txtsamagrah" runat="server" Width="200px"
                                                        Font-Names="Verdana" Font-Size="9pt" ></asp:TextBox></td>                                            
                                            </tr>
          <tr>
           <td style="height: 5px" colspan="4">
           </td>
          </tr>                                           
        </table>
        </fieldset>
       </asp:Panel>
        </td>
       </tr>
       
      <%---------End Cultivetor Design---------%>
      
      <%---------Start Bhavanter Design---------%>
      <tr>
      <td align="center" valign="top">
      <asp:Panel ID="BPanel" runat="server" Visible="False">
      <fieldset style="width: 915px; border: 1px solid navy;">
         
         <table style="width:915px;">

                         <tr>
                <td style="height: 5px" colspan="2">

                </td>
                 <tr>
                <td style="height: 5px" colspan="2" align="left" >
                <asp:Label ID="Label27" runat="server" Text="जिस स्कन्ध के लिए डीपोसीटर जोड़ना चाहते हे उसका भावांतर रजिस्ट्रेशन नंबर डाल कर सर्च करे उसके बाद स्कन्ध का चयन करे फिर खसरा रकबा का चयन कर डाटा सेव करे " Font-Bold="True" Font-Size="10pt" 
                             ForeColor="navy"></asp:Label>
                
                </td>
            </tr> 
                                     <tr>
                <td style="height: 5px" colspan="2">

                </td>
            </tr>
             <tr>
                  <td align="left" style="width: 120px">
                      <asp:Label ID="Label26" runat="server" Text="Registration No." Font-Bold="True" Font-Size="8pt"
                      ForeColor="navy"></asp:Label>
                  </td>
                    <td align="left" style="width: 300px"  >
                       <asp:TextBox ID="BtxtRegiNo" runat="server" Width="200px" ></asp:TextBox>
                       &nbsp&nbsp 
                        <asp:LinkButton ID="SearchRegBtn" runat="server" onclick="SearchRegBtn_Click" 
                            >Search</asp:LinkButton>
                   </td>
                  
             </tr>
             <tr>
                 <td style="height: 10px" colspan="2">
                 </td>
            </tr>  
          
             <tr>
             <td colspan="2" align="center" valign="top">
                                    <asp:GridView ID="FarmerGrid" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        Width="100%" DataKeyNames="Farmer_Id" Font-Size="9pt" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                 <ItemTemplate>
                                                    <asp:CheckBox ID="chk_Insert" runat="server" AutoPostBack="true" OnCheckedChanged="chk_Insert_CheckedChanged" />
                                                 </ItemTemplate>
                                                 <ItemStyle HorizontalAlign="Center" />
                                             </asp:TemplateField>
                                            <asp:BoundField DataField="Farmer_Id" HeaderText="Farmer_Id" />
                                            <asp:BoundField DataField="FarmerName" HeaderText="Farmer Name" />
                                            <asp:BoundField DataField="FatherHusName" HeaderText="Father/Hus Name"  />
                                            <asp:BoundField DataField="Mobileno" HeaderText="Mobile No." />
                                            <asp:BoundField DataField="Tehsil_Name" HeaderText="Tehsil" />
                                            <asp:BoundField DataField="VillageName" HeaderText="Village" />
                                            <asp:BoundField DataField="Samagra_ID" HeaderText="Samagra_ID" />
                                            <asp:BoundField DataField="Farmer_EID_UID_No" HeaderText="Addhar No." />
                                            <asp:BoundField DataField="CategoryName" HeaderText="Category/cast" />
                                            <asp:BoundField DataField="PatwariHalkaNo" HeaderText="PatwariHalkaNo" />
                                            <asp:BoundField DataField="RinPustikaNo" HeaderText="RinPustikaNo" />
                                            <asp:BoundField DataField="Tehsil_Id" HeaderText="Tehsil_Id" />
                                            <asp:BoundField DataField="IFSC" HeaderText="IFSC" />
                                            <asp:BoundField DataField="AcountNo" HeaderText="AcountNo"  />
                                            <asp:BoundField DataField="crpcode" HeaderText="Crop Code"  />
                                            <asp:BoundField DataField="Commodity" HeaderText="Commodity"  />
                                        </Columns>
                                        
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="20px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                    </asp:GridView>
                   </td>
             
               </tr>
               
             <tr>
                 <td style="height: 10px" colspan="2">
                 </td>
            </tr>
                         <tr>
             <td colspan="2" align="center" valign="top">
                                    <asp:GridView ID="BCropGrid" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        Width="100%" DataKeyNames="Farmer_Id" Font-Size="9pt">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                 <ItemTemplate>
                                                    <asp:CheckBox ID="chk_Insert_land" runat="server"  />
                                                 </ItemTemplate>
                                                 <ItemStyle HorizontalAlign="Center" />
                                             </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>                                            
                                            <asp:BoundField DataField="Farmer_Id" HeaderText="Farmer Id" />
                                            <asp:BoundField DataField="crop" HeaderText="Commodity" />
                                            <asp:BoundField DataField="crpcode" HeaderText="Crop Code" />
                                            <asp:BoundField DataField="Rakba" HeaderText="Rakba"  />
                                            <asp:BoundField DataField="KhasaraNo" HeaderText="Khasra No." />
                                            <asp:BoundField DataField="Tehsil_Name" HeaderText="Tehsil" />
                                            
                                        </Columns>
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="20px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                    </asp:GridView>
                   </td>
             
               </tr>
              
            <tr>
                 <td style="height: 5px" colspan="2">
                 </td>
            </tr>  
         </table>
          <table>
            <tr>
                 <td>
                    <asp:Button ID="Bsavebtn" CssClass="BTNBLUE" runat="server" Height="35px" 
                         Text="Save" Width="81px" onclick="Bsavebtn_Click" Visible="false" />
                 </td>
            </tr>
                         <tr>
                 <td style="height: 5px">
                 </td>
            </tr>  
         </table>
       </fieldset>
       </asp:Panel>      
      </td>
      </tr>
           
      
      <%---------End Bhavanter Design---------%>
      

      <%-- -------- Star Other Design---------%>
      
      <tr>
         <td align="center" valign="top" >
      <asp:Panel ID="pnlotherdp" runat="server" Visible="False">
         <fieldset style="width: 930px; border: 1px solid navy;">
         
         <table style="width:915px;">
                                      <tr>
                             
                        <td align="left">
                            <asp:Label ID="Label25" runat="server" Text="Depositor Type(If Change)" Font-Bold="True" Font-Size="8pt" Visible="false"
                             ForeColor="navy"></asp:Label>
                        </td>                            
                        <td align="left">
                         <asp:DropDownList ID="ddlorgcngtype" runat="server" 
                                Width="200px" Height="25px" onselectedindexchanged="ddldptype_select_SelectedIndexChanged" Visible="false">
                         </asp:DropDownList>
                        </td>
                        
                    </tr>
          <tr>
           <td style="height: 5px" colspan="4">
           </td>
          </tr>  


                                            <tr>
                                                <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label1" runat="server" Text="Depositor/Organzation Name" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left" style="width: 300px"  >
                                                    <asp:TextBox ID="txtorgname" runat="server" Width="200px" ></asp:TextBox>
                                                </td>
                                                        <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label2" runat="server" Text="Authorized/Owner Person" Width="142px" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                   <td align="left" > 
                                                    <asp:TextBox ID="txtorgapn" runat="server" Width="200px"
                                                        Font-Names="Verdana" Font-Size="9pt" ></asp:TextBox></td>
                                            </tr>
<%--          <tr>
           <td style="height: 2px" colspan="4">
           </td>
          </tr> --%>                                             
                    <tr>
                        <td align="left" valign="top" class="style5" >
                            <asp:Label ID="Label3" runat="server" Text="Contact No" Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label>
                        </td>
                        <td align="left" valign="top" >
                            <asp:TextBox ID="txtorgmobile" runat="server" Width="200px" AutoComplete="off" onkeypress="CheckNumeric(event);"></asp:TextBox>
                        </td>
                        <td align="left" valign="top" class="style6">
                            <asp:Label ID="Label4" runat="server" Text="Postal Address With Pin Code"   Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label>
                        </td>
                        <td align="left" >
                          <asp:TextBox ID="txtorgaddress" TextMode="MultiLine" runat="server" Width="200px"></asp:TextBox>
                        </td>                          
                    </tr>  
<%--          <tr>
           <td style="height: 2px" colspan="4">
           </td>
          </tr>  --%>                    
                                              
          <tr >
           <td align="left" style="width: 200px" valign="top" >
              <asp:Label ID="Label6" runat="server" Text="Tehsil Name" Font-Size="9pt" Font-Bold="True" ForeColor="navy">
             </asp:Label>
           </td>
           <td align="left">
                  <asp:DropDownList ID="ddlorgtehsil" runat="server" Height="25px" Width="205px" >
                  </asp:DropDownList>
           </td>
            <td align="left" style="width: 200px" valign="top" >
              <asp:Label ID="Label28" runat="server" Text="Licence No." Font-Size="9pt" Font-Bold="True" ForeColor="navy">
             </asp:Label>
           </td> 
                                                              <td align="left" > 
                                                    <asp:TextBox ID="txtlicence" runat="server" Width="200px"
                                                        Font-Names="Verdana" Font-Size="9pt" ></asp:TextBox></td>                                                                                                      
         </tr>
         
         
                  
         <tr>
                                                    <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label23" runat="server" Text="GSTIN No." Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                <td align="left" style="width: 250px"  >
                                                    <asp:TextBox ID="txtgstin" runat="server" Width="200px" ></asp:TextBox>
                                                </td>
                                                        <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label24" runat="server" Text="PAN No." Width="142px" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                   <td align="left" > 
                                                    <asp:TextBox ID="txtorgpan" runat="server" Width="200px"
                                                        Font-Names="Verdana" Font-Size="9pt" ></asp:TextBox></td>
                                            </tr>                                      

          <tr>
           <td style="height: 5px" colspan="4">
           </td>
          </tr>                                           
        </table>
        </fieldset>
    </asp:Panel>
        </td>
       </tr>
      
    
  <%--  ---------End Other Design---------- --%>
      
    <%--  -------Starc Bank Detail---------%>
                                           <tr>
                                          <td style="height: 5px" colspan="2">
                                          </td>
                                     </tr> 
              <tr>
        <td align="center" valign="top">
<asp:Panel ID="pnlbank" runat="server" Visible="false">
         <fieldset style="width: 930px; border: 1px solid navy;">
                <table style="width: 915px">

                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center"  >
                                                    <asp:Label ID="Label17" runat="server" Text="Bank Detail" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr> 
                                       <tr>
                                          <td style="height: 10px" colspan="2">
                                          </td>
                                     </tr>                                            
                    <tr>
                        <td align="Left" >
                            <asp:Label ID="Label18" runat="server" Text="Bank Name" Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label>
                        </td>                            
                        <td align="left" style="width: 300px"  >
                           <asp:DropDownList ID="ddlBank" runat="server" Width="200px" Height="25px"  AutoPostBack="true"
                                onselectedindexchanged="ddlBank_SelectedIndexChanged">
                           </asp:DropDownList>
                        </td>
                         <td align="Left" >
                            <asp:Label ID="Label19" runat="server" Text="Branch Name" Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label>
                        </td>    
                           
                            <td align="left">
                                <asp:DropDownList ID="ddlBankBranch" runat="server" Height="25px"  AutoPostBack="true"
                                    onselectedindexchanged="ddlBankBranch_SelectedIndexChanged" Width="200px">
                                </asp:DropDownList>
                            </td>
                    </tr>
                                       <tr>
                                          <td style="height: 10px" colspan="2">
                                          </td>
                                     </tr>                     
<tr>
                                                        <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label20" runat="server" Text="Account No" Width="142px" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                   <td align="left" > 
                                                    <asp:TextBox ID="txtAccNo" runat="server" Width="200px"
                                                        Font-Names="Verdana" Font-Size="9pt" ></asp:TextBox></td>
                                                        <td align="left" style="width: 200px" valign="top">
                                                    <asp:Label ID="Label21" runat="server" Text="IFSC Code" Width="142px" Font-Size="8pt" Font-Bold="True"
                                                        ForeColor="navy"></asp:Label></td>
                                                   <td align="left" > 
                                                    <asp:TextBox ID="txtifsc" runat="server" Width="200px"
                                                        Font-Names="Verdana" Font-Size="9pt" ></asp:TextBox></td>                                                        
</tr>                    
                                                           <tr>
                                          <td style="height: 10px" colspan="2">
                                          </td>
                                     </tr> 
                                                                
                </table>
         </fieldset>   
         </asp:Panel>
         </td>
         </tr>
         <%----------------For Co-Societies-------------------%>
         <tr>
        <td align="center" valign="top">
<asp:Panel ID="pnlCopSociety" runat="server" Visible="false">
         <fieldset style="width: 930px; border: 1px solid navy;">
                <table style="width: 915px">

                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center"  >
                                                    <asp:Label ID="Label29" runat="server" Text="Add Co-op Society" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr> 
                                       <tr>
                                          <td style="height: 10px" colspan="2">
                                          </td>
                                     </tr> 
                                     <tr>
                        <td align="right">
                            <asp:Label ID="Label31" runat="server" Text="Procurement Type" Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label>
                        </td>                            
                        <td align="left" style="width: 300px"  >
                           <asp:DropDownList ID="ddlProcType" runat="server" Width="200px" Height="25px" 
                                AutoPostBack="true" onselectedindexchanged="ddlProcType_SelectedIndexChanged"
                              >
                              <asp:ListItem Text="Wheat Procurement 2020-21"></asp:ListItem>
                              <asp:ListItem Text="Wheat Procurement 2019-20"></asp:ListItem>
                              <asp:ListItem Text="Paddy Procurement"></asp:ListItem>
                              <asp:ListItem Text="Coarse Grain Procurement"></asp:ListItem>
                              <asp:ListItem Text="Dalhan/Tilhan Procurement"></asp:ListItem>
                           </asp:DropDownList>
                        </td>
                         
                    
                    </tr> 
                       <tr>
                                          <td style="height: 10px" colspan="2">
                                          </td>
                                     </tr>                                           
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label30" runat="server" Text="Society Name" Font-Bold="True" Font-Size="8pt"
                             ForeColor="navy"></asp:Label>
                        </td>                            
                        <td align="left" style="width: 300px"  >
                           <asp:DropDownList ID="ddlDepositor" runat="server" Width="500px" Height="25px" AutoPostBack="true"
                              >
                           </asp:DropDownList>
                        </td>
                         
                    
                    </tr>
                                       <tr>
                                          <td style="height: 10px" colspan="2">
                                          </td>
                                     </tr>                                         
                                                           <tr>
                                          <td style="height: 10px" colspan="2">
                                          </td>
                                     </tr> 
                                                                
                </table>
         </fieldset>   
         </asp:Panel>
         </td>
         </tr>
         <%-------------------End Bank Detail---------------%>
       
       <tr>
       <td>
       <asp:Panel ID="btnapprove" runat="server" Visible="False">
                       <table cellpadding="0" cellspacing="0" style="width: 100%">
                       
                                                                                  <tr>
                                          <td style="height: 5px">
                                          </td>
                                          </tr>
                                          <tr>
                        <td align="center" style="width: 100%">
                            <asp:Button ID="btnEdit" CssClass="BTNBLUE" runat="server" Height="35px" Width="100px" OnClick="btnEdit_Click"  /></td>
                      </tr>
                      </table>
            </asp:Panel>
<%--            </asp:Panel>--%>
        </td>
    </tr>
        <tr>
        <td valign="top">
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></td>
      </tr>

    </table>
                                                    
                                                    <asp:Label ID="Label5" runat="server" Font-Size="X-Small" ForeColor="#400040"></asp:Label>
                                    </div>
                                </center>
                            </fieldset> 


</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

 <script type="text/javascript">
 
     function CheckNumeric(e, tx) {
       var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        if ((AsciiCode < 46 && AsciiCode != 8 && AsciiCode != 9) || (AsciiCode > 57)) {
            alert('Please enter only numbers !');
            return false;
        }

    }

    function isNumberKey(key) {
        //getting key code of pressed key
        var keycode = (key.which) ? key.which : key.keyCode;
        //comparing pressed keycodes

        if (keycode > 31 && (keycode < 48 || keycode > 57) && keycode != 47) {
            alert(" You can enter only characters 0 to 9 ");
            return false;
        }
        else return true;
    }

    function isNumberKey2(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode

        if (charCode == 46) {
            var inputValue = $("#inputfield").val()
            if (inputValue.indexOf('.') < 1) {
                return true;
            }
            return false;
        }
        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
        
    </script>

    <style type="text/css">
        .style5
        {
            width: 200px;
            height: 40px;
        }
        .style6
        {
            height: 40px;
        }
        .style7
        {
            height: 25px;
        }
    </style>

</asp:Content>


