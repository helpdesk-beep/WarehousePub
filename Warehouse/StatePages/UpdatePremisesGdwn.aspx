<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="UpdatePremisesGdwn.aspx.cs" Inherits="StatePages_UpdatePremisesGdwn" Title="Update Premises Godown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="Panel3" runat="server" >
<fieldset style="width: 1070px; border: 2px solid navy;">
  <center>
    <div>
      <table cellpadding="0" cellspacing="0" style="width: 100%">
         <tr>
            <td align="center" valign="top">
               <fieldset style="width: 1050px; border: 1px solid navy;">
                  <center>
                     <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                       <tr style="background-color: #0bb6e6; height: 25px; width: 100%">
                                          <td colspan="4" align="center">
                                             <span style="color: White; font-size: 12pt; font-weight: bold">Update Premises Godown Details</span>
                                          </td>
                                       </tr>
                                                   
                                       <tr>
                                          <td style="height: 12px" colspan="4" align="center">
                                          </td>
                                       </tr>
                                        <tr>
                                          <td style="height: 12px" colspan="4" align="Right">
                                              <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Add Premises</asp:LinkButton>
                                          </td>
                                       </tr>                                                    
                                       <tr>
                                          <td style="width:25px" align="left">
                                             <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>
                                          <td style="width: 150px" align="left">
                                             <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="150px" AutoPostBack="True"
                                                 CssClass="tb6" onselectedindexchanged="ddlDistrict_SelectedIndexChanged" >
                                              </asp:DropDownList>
                                          </td>
                                                                                     <td style="width: 50px" align="left">
                                               <asp:Label ID="lblBranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                           </td>
                                           <td style="width: 200px" align="left">
                                               <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="150px" AutoPostBack="True"
                                                CssClass="tb6" onselectedindexchanged="ddlDepotList_SelectedIndexChanged" >
                                               </asp:DropDownList>
                                           </td>
                                        </tr>
                                </table>
                                <table>
                                        <tr>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>                                       
                                           <td align="center" valign="top">
                                              <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
                                               CellPadding="2" Width="100%" AllowSorting="True" Font-Size="8pt" 
                                                   DataKeyNames="P_Godown_Id" 
                                                   onselectedindexchanged="gv_SelectedIndexChanged" 
                                                   onrowdeleting="gv_RowDeleting" >
                                               <Columns>
                                                  <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ItemStyle-ForeColor="red" >
                                                    <ItemStyle ForeColor="Red"></ItemStyle>
                                                  </asp:CommandField>
                                                  <asp:CommandField ShowSelectButton="True" HeaderText="Edit" ItemStyle-ForeColor="blue" >
                                                     <ItemStyle ForeColor="Blue"></ItemStyle>
                                                  </asp:CommandField>
                                                            <asp:BoundField DataField="P_Godown_Id" HeaderText="Premises Godown ID"/>
                                                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name"/>
                                                            <asp:BoundField DataField="GodownNum" HeaderText="Godown No."/>
                                                            <asp:BoundField DataField="Godown_Capacity" HeaderText="Godown Cpt" />
                                                            <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Scientific Cpt" />
                                                            <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />
                                                            <asp:BoundField DataField="Closing_Balance" HeaderText="Closing Bal" />
                                                            <asp:BoundField DataField="Vacant_Capacity" HeaderText="Vacant Cpt" />
                                                            <asp:BoundField DataField="Premises_Name" HeaderText="Premises Name" />
                                                            <asp:BoundField DataField="Premises_No" HeaderText="Premises No." />
                                                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" />
                                                            <asp:BoundField DataField="Whr_Name" HeaderText="Gdwn_name (Software)" />
                                                            <asp:BoundField DataField="Premises_Id" HeaderText="Premises ID" />
                                                            <asp:BoundField DataField="Closing_Balance_Sep2017" HeaderText="Closing Sep 2017" />
                                               </Columns>
                                                        <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="9pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                             </asp:GridView>
                                          </td>
                                       </tr>  
                                                                                         
                                 <tr>
                                   <td style="height: 2px">
                                   </td>
                                </tr>  
                                 <tr>
                                     <td align="center">
                                        <asp:Button ID="Newgdwn_btn" runat="server" Text="New Godown" Width="100px" CssClass="BTNBLUE"
                                          ValidationGroup="validate" Height="36px" onclick="Newgdwn_btn_Click" />
                                          &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="Button2" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                          CausesValidation="false" Height="36px" onclick="Button2_Click" />
                                     </td>
                               </tr>                                  
                                 <tr>
                                   <td style="height: 2px">
                                   </td>
                                </tr>
                          </table>
                       </div>
                  </center>
               </fieldset> 
               <div style="height:4px"></div>     
                         <asp:Panel ID="Panel1" runat="server" Visible="false">
                         <fieldset style="width: 1050px; border: 1px solid navy;">
                         <table  cellpadding="0" cellspacing="0" style="width: 100%">
                                       <tr style="background-color: #0bb6e6; height: 25px; width: 100%">
                                          <td align="center" style="color: White; font-size: 12pt; font-weight: bold">
                                          <asp:Label ID="header_addgdwntxt" runat="server" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                            <%-- <span style="color: White; font-size: 12pt; font-weight: bold">Add New Godown</span>--%>
                                          </td>
                                       </tr> 
                                 <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px">
                                   </td>
                                </tr>                                                                  
                         </table>
                          <table align="Center">                         
                                 <tr id="trgodown" runat="server" style="height:15px">
                                          <td align="left" >
                                            <asp:Label ID="Label4" runat="server" Text="Premises Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>

                                           <td align="left">
                                               <asp:DropDownList ID="ddlPremises" runat="server" Height="25px" Width="200px" 
                                                   AutoPostBack="True" onselectedindexchanged="ddlPremises_SelectedIndexChanged">
                                               </asp:DropDownList>
                                          </td>
                                  </tr>
                                 <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                </tr>                                  
                         <%--        <tr id="tr8" runat="server" style="height:15px">
                                          <td align="left" >
                                            <asp:Label ID="Label9" runat="server" Text="Premises No." Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>

                                           <td align="left">
                                               <asp:DropDownList ID="ddlprmisesno" runat="server" Height="25px" Width="200px" 
                                                   AutoPostBack="True">
                                               </asp:DropDownList>
                                          </td>
                                  </tr>                                  
                                 <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                </tr>  --%>                                                                     
                                     <tr id="tr1" runat="server">
                                         <td align="left">
                                             <asp:Label ID="Label1" runat="server" Text="Godown Name (New)" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                         </td>
                                          <td align="left">
                                               <asp:TextBox ID="gdwnnamenewtxt" runat="server" Width="196px" Height="18px" Font-Size="9pt" AutoComplete="off" ReadOnly="true" ></asp:TextBox>                                             
                                         </td>
                                    </tr> 
                                 <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                </tr>                                     
                                     <tr id="tr9" runat="server">
                                         <td align="left">
                                             <asp:Label ID="Label10" runat="server" Text="Godown No." Font-Size="10pt" Font-Bold="true"></asp:Label>
                                         </td>
                                          <td align="left">
                                               <asp:TextBox ID="gdwnnonewtxt" runat="server" Width="196px" Height="18px" Font-Size="9pt" AutoComplete="off" ></asp:TextBox>                                             
                                         <asp:Label ID="P_GdwnID" runat="server"  Font-Size="10pt" Font-Bold="true" Visible="false"></asp:Label>
                                         </td>
                                    </tr>  
                                 <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                </tr>                                     
                                     <tr id="tr10" runat="server">
                                         <td align="left">
                                             <asp:Label ID="Label11" runat="server" Text="Godown APN." Font-Size="10pt" Font-Bold="true"></asp:Label>
                                         </td>
                                          <td align="left">
                                               <asp:TextBox ID="gdwnAPN" runat="server" Width="196px" Height="18px" Font-Size="9pt" AutoComplete="off" ></asp:TextBox>                                             
                                         
                                         </td>
                                    </tr>                                                                          
                                 <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                </tr> 
                                                                                                   
                                     <tr id="tr2" runat="server">
                                         <td align="left">
                                             <asp:Label ID="Label2" runat="server" Text="Hired Type" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>
                                          <td align="left">
                                               <asp:DropDownList ID="ddlhiredtype" runat="server" Height="25px" Width="200px" 
                                                   AutoPostBack="True" onselectedindexchanged="ddlhiredtype_SelectedIndexChanged">
                                               <asp:ListItem Text="--Select--" Value="0" ></asp:ListItem>
                                               <asp:ListItem Text="MPWLC-OWN" Value="1"></asp:ListItem>
                                               <asp:ListItem Text="MPWLC-PEG" Value="2"></asp:ListItem>
                                               <asp:ListItem Text="PVT.PEG" Value="3"></asp:ListItem>
                                               </asp:DropDownList>                                           
                                          </td>
                                     </tr> 
                                 <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                </tr>                                                                     
                                     <tr id="tr3" runat="server">
                                         <td align="left">
                                             <asp:Label ID="Label3" runat="server" Text="Godown Capacity (In M.T.)" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>
                                          <td align="left">
                                             <asp:TextBox ID="gdwncpttxt" runat="server" Width="196px" Font-Size="9pt" 
                                                  AutoComplete="off" Height="18px" style="margin-left: 0px"></asp:TextBox>                                        
                                          </td>
                                     </tr>   
                                 <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                </tr>                                                                   
                                     <tr id="tr4" runat="server">
                                         <td align="left">
                                             <asp:Label ID="Label5" runat="server" Text="Godown Scientific Capacity (In M.T.)" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>
                                          <td align="left">
                                             <asp:TextBox ID="gdwnsctcpttxt" runat="server" Width="196px" Font-Size="9pt" 
                                                  AutoComplete="off" Height="18px"></asp:TextBox>   
                                          </td>
                                     </tr> 
                                 <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                </tr>                                                                     
                                                                    
                                     <tr id="tr6" runat="server">

                                         <td align="left">
                                             <asp:Label ID="Label6" runat="server" Text="Storage Type" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>                                          
                                          <td align="left">
                                               <asp:DropDownList ID="ddlstoragetype" runat="server" Height="25px" 
                                                   Width="200px" AutoPostBack="True">
                                               <asp:ListItem Text="Covered" Value="1" ></asp:ListItem>

                                               </asp:DropDownList>                                             
                                          </td>
                                     </tr>  
                                     
<tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                   </tr>
                                     <tr id="tr8" runat="server">

                                         <td align="left">
                                             <asp:Label ID="Label9" runat="server" Text="Closing Balance (Previous)" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>                                          
                                          <td align="left">
                                             <asp:TextBox ID="PreClosing" runat="server" Width="196px" Height="18px" 
                                                  Font-Size="9pt" AutoComplete="off"></asp:TextBox>
                                          </td>
                                     </tr>                                      
                                   <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                   </tr>
                                     <tr id="tr7" runat="server">

                                         <td align="left">
                                             <asp:Label ID="Label8" runat="server" Text="Closing Balance (Sep 2017 In M.T)" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>                                          
                                          <td align="left">
                                             <asp:TextBox ID="closingbaltxt" runat="server" Width="196px" Height="18px" 
                                                  Font-Size="9pt" AutoComplete="off"></asp:TextBox>
                                          </td>
                                     </tr>  
                                   <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td> 
                                </tr> 
                                     <tr id="tr5" runat="server">

                                         <td align="left">
                                             <asp:Label ID="Label7" runat="server" Text="Godown Name (In Software)" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>                                          
                                          <td align="left">
                                               <asp:DropDownList ID="gdwn_name_softtxt" runat="server" Height="25px" 
                                                   Width="300px" AutoPostBack="True" 
                                                   onselectedindexchanged="gdwn_name_softtxt_SelectedIndexChanged">
                                               </asp:DropDownList>  
                                                  <asp:Label ID="lbl_gdid" runat="server" Font-Size="6pt"></asp:Label>                                         
                                          
                                          </td>
                                     </tr>                                     
                                 <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                </tr>                                                                    
                               
                                 <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 3px" colspan="2">
                                   </td>
                                </tr>
                                                                                                                 
                            <tr>                       
                        <td colspan="2" align="center">
                            <asp:Button ID="btnupdate" runat="server" Width="100px" CssClass="BTNBLUE"
                                ValidationGroup="validate" onclick="btnupdate_Click"  />
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                CausesValidation="false" />
                        </td>
                        </tr> 
                    
                        </table>
                        </fieldset></asp:Panel>
 
            </td>
         </tr>
         <tr>
           <td style="height: 5px">
           </td>
         </tr>
      </table>
    </div>
  </center>
</fieldset>
</asp:Panel>


<%---------------------Premises Design Here------------------------------%>


<div style="height:6px"></div>  
<asp:Panel ID="Panel2" runat="server" Visible="false" >
<fieldset style="width: 1070px; border: 2px solid navy;">
<center>
    <div>
      <table cellpadding="0" cellspacing="0" style="width: 100%" align="Center">
         <tr>
             <td align="center" valign="top">
                <fieldset style="width: 1050px; border: 1px solid navy;">
                  <center>
                     <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                              <tr style="background-color: #0bb6e6; height: 25px; width: 100%">
                                 <td colspan="4" align="center">
                                     <span style="color: White; font-size: 12pt; font-weight: bold">Update / Add Premises </span>
                                 </td>
                              </tr>
                                       <tr>
                                          <td style="height: 12px" colspan="4" align="center">
                                          </td>
                                       </tr>
                                                    
                                       <tr>
                                           
                                               <td align="left" style="width:25px">
                                                   <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="10pt" 
                                                       Text="District"></asp:Label>
                                               </td>
                                               <td align="left" style="width: 150px">
                                                   <asp:DropDownList ID="prm_ddldist" runat="server" AutoPostBack="True" 
                                                       CssClass="tb6" Height="25px" Width="150px" 
                                                       onselectedindexchanged="prm_ddldist_SelectedIndexChanged">
                                                   </asp:DropDownList>
                                               </td>
                                               <td align="left" style="width: 50px">
                                                   <asp:Label ID="Label13" runat="server" Font-Bold="true" Font-Size="10pt" 
                                                       Text="Branch"></asp:Label>
                                               </td>
                                               <td align="left" style="width: 200px">
                                                   <asp:DropDownList ID="prm_ddlbranch" runat="server" AutoPostBack="True" 
                                                       CssClass="tb6" Height="25px" Width="150px" 
                                                       onselectedindexchanged="prm_ddlbranch_SelectedIndexChanged">
                                                   </asp:DropDownList>
                                               </td>
                                           
                                        </tr>
                         </table>
                         <table align="Center">
                                        <tr>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>                                       
                                           <td align="center" valign="top">
                                              <asp:GridView ID="prm_gv" runat="server" AutoGenerateColumns="False"
                                               CellPadding="2" Width="100%" AllowSorting="True" Font-Size="9pt" 
                                                   onrowdeleting="prm_gv_RowDeleting" DataKeyNames="Premises_Id" >
                                               <Columns>
                                                  <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ItemStyle-ForeColor="red" >
                                                    <ItemStyle ForeColor="Red"></ItemStyle>
                                                  </asp:CommandField>
                                                            <asp:BoundField DataField="Premises_Name" HeaderText="Premises Name" />
                                                            <asp:BoundField DataField="Premises_No" HeaderText="Premises No." />
                                                            <asp:BoundField DataField="Premises_Id" HeaderText="Premises ID" />
                                                            <asp:BoundField DataField="Owner" HeaderText="Owner" />
                                                            <asp:BoundField DataField="Org_Name" HeaderText="Org Name" />
                                                            <asp:BoundField DataField="No_of_GDWN" HeaderText="No Of Godowns" />
                                               </Columns>
                                                        <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                             </asp:GridView>
                                          </td>
                                       </tr>
                                 <tr>
                                   <td style="height: 2px">
                                   </td>
                                </tr>  
                                 <tr>
                                     <td align="center">
                                        <asp:Button ID="prm_newbtn" runat="server" Text="New Premises" Width="100px" CssClass="BTNBLUE"
                                          ValidationGroup="validate" Height="34px" onclick="prm_newbtn_Click" />
                                          &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="prm_closebtn" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                          CausesValidation="false" Height="34px" onclick="Button2_Click" />
                                     </td>
                               </tr>
                                 <tr>
                                   <td style="height: 2px">
                                   </td>
                                </tr> 
     <asp:Panel ID="Panel4" runat="server" Visible="false" >    
                                            <tr style="background-color: #0bb6e6; height: 25px; width: 100%">
                                          <td colspan="4" align="center">
                                             <span style="color: White; font-size: 12pt; font-weight: bold">Add New Premises</span>
                                          </td>
                                       </tr>                          
                                 <tr> 
                                                                                                                                                                                                                                                                       
                                   <td style="height: 1px">
                                   <table><center>
                                   <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                   </tr>
                                     <tr >
                                         <td align="left">
                                             <asp:Label ID="Label18" runat="server" Text="Premises Hired Type" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>
                                          <td align="left">
                                               <asp:DropDownList ID="prm_hiretype" runat="server" Height="25px" Width="200px" 
                                                   AutoPostBack="True" 
                                                   onselectedindexchanged="prm_hiretype_SelectedIndexChanged">
                                               <asp:ListItem Text="--Select--" Value="0" ></asp:ListItem>
                                               <asp:ListItem Text="MPWLC-OWN" Value="1"></asp:ListItem>
                                               <asp:ListItem Text="MPWLC-PEG" Value="2"></asp:ListItem>
                                               <asp:ListItem Text="PVT.PEG" Value="3"></asp:ListItem>
                                               </asp:DropDownList>                                           
                                          </td>
                                          
                                     </tr> 
                                   <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                   </tr>                                                                        
                                     <tr id="tr11" runat="server">

                                         <td align="left">
                                             <asp:Label ID="Label14" runat="server" Text="Premises Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>                                          
                                          <td align="left">
                                             <asp:TextBox ID="Prm_nametxt" runat="server" Width="196px" Height="18px" 
                                                  Font-Size="9pt" AutoComplete="off"></asp:TextBox>
                                          </td>
                                     </tr> 
                                   <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                   </tr>
                                     <tr id="tr12" runat="server">

                                         <td align="left">
                                             <asp:Label ID="Label15" runat="server" Text="Premises No" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>                                          
                                          <td align="left">
                                             <asp:TextBox ID="Prm_notxt" runat="server" Width="196px" Height="18px" 
                                                  Font-Size="9pt" AutoComplete="off"></asp:TextBox>
                                          </td>
                                     </tr> 
                                   <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                   </tr>
                                     <tr id="tr13" runat="server">

                                         <td align="left">
                                             <asp:Label ID="Label16" runat="server" Text=" Owner" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>                                          
                                          <td align="left">
                                             <asp:TextBox ID="Prm_ownertxt" runat="server" Width="196px" Height="18px" 
                                                  Font-Size="9pt" AutoComplete="off"></asp:TextBox>
                                          </td>
                                     </tr> 
                                   <tr>                                                                                                                                                                                                                                       
                                   <td style="height: 1px" colspan="2">
                                   </td>
                                   </tr>
                                     <tr id="tr14" runat="server">

                                         <td align="left">
                                             <asp:Label ID="Label17" runat="server" Text="Organization Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                          </td>                                          
                                          <td align="left">
                                             <asp:TextBox ID="Prm_orgnametxt" runat="server" Width="196px" Height="18px" 
                                                  Font-Size="9pt" AutoComplete="off"></asp:TextBox>
                                          </td>
                                     </tr>   
                                     </center>                                                                                                           
                                   </table>
                                   </td>
                                </tr>  
                                                                  
                                 <tr>
                                   <td style="height: 2px">
                                   </td>
                                </tr>  
                                                                
               
                                
                                   <tr>
                                   <td>
                                   <table>
                                   <tr>
                                        <td align="center">
                                        <asp:Button ID="btn_prmsave" runat="server" Width="100px" CssClass="BTNBLUE"
                                        ValidationGroup="validate" Text="Save" onclick="btn_prmsave_Click" />
                                        &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="Button3" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                        CausesValidation="false" onclick="Button3_Click" />
                                       </td>                                   
                                   </tr>
                                   </table>
                                   </td>
                                   </tr> 
                                      </asp:Panel>                        
                                <tr>
                                   <td style="height: 2px">
                                   </td>
                                </tr>                                
                          </table>
                     </div>
                   </center>
                 </fieldset>
             </td>
         </tr>
      </table>
    </div>
</center>
</fieldset>
</asp:Panel>
</asp:Content>

