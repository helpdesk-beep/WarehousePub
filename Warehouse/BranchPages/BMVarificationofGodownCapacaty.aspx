<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="BMVarificationofGodownCapacaty.aspx.cs" Inherits="Accounting_BMVarificationofGodownCapacaty" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
            border-radius: 6px;
            padding: 0
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>
         <script  type="text/javascript">  
         
            function ConfirmOnDelete() {
                if (confirm("Are you sure want to Delete This Inspection ?") == true)
                    return true;
                else
                    return false;
            }
    


             function NumberOnly(e) {
                 var charCode = (e.which) ? e.which : e.keyCode;
                 if ((charCode >= 48 && charCode <= 57)) {
                     return true;
                 }
                 if (charCode == 46) { return true; }
                 if (charCode == 8) { return true; }
                 if (charCode == 9) { return true; }
                 else { return false; }
             }


         
     <%--            $(document).ready(function () {
                     $('#<%=OfferDistType.ClientID%>').change(function () {
                         var vdist = $('#<%=OfferDistType.ClientID%>').val();
                         if (vdist == "S")
                         {

                         }
                         else
                         {
                         }
            
            });   
        });--%>
     
         </script>
    <table>
        <tr>
            <td>
                    <div style="text-align:center"> <h2 style="font-size: 25px;text-transform: uppercase;font-weight: revert;color: blue;"> Godown Storage and Vacant Capacity For Season Rabi 2023-24 </h2>
    </div>
                 <div style="text-align:center"> <h2 style="font-size: 25px;text-transform: uppercase;font-weight: revert;color: red;">Note:- नीचे  दी गई स्क्रीन में प्रदर्शित गोदामों  के latitude,longitude,Vacant कैपेसिटी के आधार पर ही  ऑनलाइन (उपार्जन/मैपिंग/स्टोरेज ) कार्य  किये  जा  रहे हे इन्हे जांचे एवं  अपडेट रखे | </h2>
    </div>
        <div style="text-align:center"> <h2 style="font-size: 20px;text-transform: uppercase;font-weight: revert;color: red;"><asp:Label ID="lblmsg" runat="server"></asp:Label> </h2>
    </div>
            </td>

      <%--      <td>
             
                 <div style="text-align:center"> <asp:Button ID="show" runat="server" style="font-size:20px; color:green; font-weight:800" Text="List Of Selected/Rejected Godown For Mapping" OnClick="show_Click"  />  
                </div>
                     </td>--%>
        </tr>
    </table>

   
    <asp:HiddenField ID="ddd" runat="server" />
         


    <asp:Panel ID="StoreGrid" runat="server"  >
      
       <%--     <table style="text-align:center;margin:0 auto; border:1px solid#000;" >
                <tr>
                    <td>  <asp:DropDownList ID="DdlDist" runat="server" AutoPostBack="true" OnTextChanged="DdlDist_TextChanged"></asp:DropDownList></td>
                  <td>  <asp:DropDownList ID="ddlbranch" runat="server" OnTextChanged="ddlbranch_TextChanged" AutoPostBack="true"></asp:DropDownList></td>
                </tr>
            </table>--%>
           <asp:HiddenField ID="hdngdnid" runat="server"/>
          <asp:HiddenField ID="Hiddendistid" runat="server"/>
          <asp:HiddenField ID="Hiddenbranch" runat="server"/>

         <asp:HiddenField ID="EnterHiddendistid" runat="server"/>
     


             <asp:GridView ID="GridView1"  CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" Font-Size="18px"  Width="100%"  >
                                 
                                                        <Columns>
                                                                    <%--<asp:CommandField HeaderText="Delete" ShowDeleteButton="True"
                                                                ItemStyle-ForeColor="red">
                                                                <ItemStyle ForeColor="Red"></ItemStyle>
                                                            </asp:CommandField>
                                                                    <asp:CommandField ShowSelectButton="True" HeaderText="Edit"
                                                                        ItemStyle-ForeColor="blue">
                                                                        <ItemStyle ForeColor="Blue"></ItemStyle>
                                                                    </asp:CommandField>--%>
                                                            <asp:TemplateField HeaderText="S.N.">
                                                               <ItemTemplate>
                                                                         <%#Container.DataItemIndex+1%>
                                                               <%--    <asp:Label ID="id" runat="server" Text='<%#(Eval(""),"{0:N0}")) %>'></asp:Label>--%>
                                                               </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:BoundField DataField="GodownID"  HeaderText="Godown_ID" />
                                                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown_Name" />
                                                                 <asp:BoundField DataField="Hired_Type" HeaderText="Hired_Type" />

                                                             <asp:TemplateField HeaderText="Godown_Max_Capacity">
                                                               <ItemTemplate>
                                                                      
                                                                   <asp:Label ID="Godown_Max_Capacity" runat="server" Text='<%# (Eval("Godown_Max_Capacity","{0:N0}"))%>'></asp:Label>
                                                               </ItemTemplate>
                                                            </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Godown_Scientific_Capacity">
                                                               <ItemTemplate>
                                                                      
                                                                   <asp:Label ID="Godown_Scientific_Capacity" runat="server" Text='<%# (Eval("Godown_Scientific_Capacity","{0:N0}"))%>'></asp:Label>
                                                               </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Godown Vacant Capacity">
                                                               <ItemTemplate>
                                                                      
                                                                   <asp:Label ID="Godown_Vacant_Capacity" runat="server" Text='<%# (Eval("vacant_Capacity_Dynamic","{0:N0}"))%>'></asp:Label>
                                                               </ItemTemplate>
                                                            </asp:TemplateField>
                                                          <%--  <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Godown_Scientific_Capacity" />--%>
														   <asp:TemplateField HeaderText="Godown Unload Capacity">
                                                               <ItemTemplate>
                                                                      
                                                                   <asp:Label ID="Godown_Unload_Capacity" runat="server" Text='<%# (Eval("Unload_Capacity","{0:N0}"))%>'></asp:Label>
                                                               </ItemTemplate>
                                                            </asp:TemplateField>

                                                         

                                                                 <asp:TemplateField HeaderText="Latitude">
                                                               <ItemTemplate>
                                                                          <asp:Label ID="Latitude" runat="server" Text='<%# (Eval("Latitude","{0:N0}"))%>'></asp:Label>
                                                               
                                                               </ItemTemplate>
                                                            </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Longitude">
                                                               <ItemTemplate>
                                                                      
                                                                   <asp:Label ID="Longitude" runat="server" Text='<%# (Eval("Longitude","{0:N0}"))%>'></asp:Label>
                                                               </ItemTemplate>
                                                            </asp:TemplateField>

                                                     
                                                              <asp:BoundField DataField="Godown_Flagmark" HeaderText="Status" />

                                                          
                                                            
                                                                 

                                                                <%--    <asp:BoundField   DataField="District_Id" HeaderText="District_Id" />
                                                                    <asp:BoundField  DataField="District_Name" HeaderText="District_Name" />
                                                                    <asp:BoundField  DataField="BranchId" HeaderText="BranchId" />
                                                                    <asp:BoundField  DataField="Branch" HeaderText="Branch" />--%>
                                                              
                                                                
                                                        
                                                         
                                                           
                                                           
                                                                 
                                                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="Edit">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" Text="Update" OnClick="Edit"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                 
                 
                 
       
                                </asp:GridView>
    </asp:Panel>





          <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 50%; Height:90%; overflow:auto;" >
                <table align="center">
                    <tr align="center">
                        <td colspan="2"> <asp:Label Font-Bold="true" ID="Label3" style="text-align:center; font-size:25px; text-transform:uppercase; color:red;"  runat="server" Text="Godown Details"></asp:Label> </td>
                    </tr>
                        <tr>
                            <td style="font-size:20px;">
                 Godown_Id -: <asp:Label Font-Bold="true" ID="Label8" runat="server" Text="Godown Details"></asp:Label>
                                      </td> <td style="font-size:20px;">
        Godown_Name -: <asp:Label Font-Bold="true" ID="Label9" runat="server" Text="Godown Details"></asp:Label>
                   </td> </tr>
                            </table>
                            <br />
                            <table align="center">
                          
                                <tr>
                                    <td style="padding: 25px">
                                 <%--    Vacant_Capacity_BY_BM--%>   <asp:Label ID="Label4" runat="server" Text="रबी  2022-23 भण्डारण हेतु  वर्तमान या भविष्य में होने वाली रिक्त क्षमता" Font-Bold="true" ForeColor="navy"
                                            Font-Size="11pt"></asp:Label>(In MT)</td>
                                    <td style="padding: 25px">
                                        <asp:TextBox ID="txtVacantCapacity" runat="server" Width="150px" AutoComplete="off" onkeypress="return NumberOnly(event)"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtVacantCapacity_FilteredTextBoxExtender"
                                            runat="server" TargetControlID="txtVacantCapacity" FilterType="Custom, Numbers" ValidChars=".">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 25px">
                                 <%-- Unload_Capacity --%>      <asp:Label ID="Label1" runat="server" Text="Premise की उतराई /Unload क्षमता" Font-Bold="true" ForeColor="navy"
                                            Font-Size="11pt"></asp:Label>(In  MT)</td>
                                    <td style="padding: 25px">
                                        <asp:TextBox ID="txtUnloadCapacity" runat="server" Width="150px" AutoComplete="off" onkeypress="return NumberOnly(event)"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtUnloadCapacity_FilteredTextBoxExtender"
                                            runat="server" TargetControlID="txtUnloadCapacity" FilterType="Custom, Numbers" ValidChars=".">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>



                                    <tr>
                                    <td style="padding: 25px">
                                 <%-- Unload_Capacity --%>      <asp:Label ID="Label10" runat="server" Text="Latitude" Font-Bold="true" ForeColor="navy"
                                            Font-Size="11pt"></asp:Label>(In  MT)</td>
                                    <td style="padding: 25px">
                                        <asp:TextBox ID="TxtLatitude" runat="server" Width="150px" AutoComplete="off" onkeypress="return NumberOnly(event)"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                            runat="server" TargetControlID="TxtLatitude" FilterType="Custom, Numbers" ValidChars=".">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>


                                    <tr>
                                    <td style="padding: 25px">
                                 <%-- Unload_Capacity --%>      <asp:Label ID="Label11" runat="server" Text="Longitude" Font-Bold="true" ForeColor="navy"
                                            Font-Size="11pt"></asp:Label>(In  MT)</td>
                                    <td style="padding: 25px">
                                        <asp:TextBox ID="TextLongitude" runat="server" Width="150px" AutoComplete="off" onkeypress="return NumberOnly(event)"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                                            runat="server" TargetControlID="TextLongitude" FilterType="Custom, Numbers" ValidChars=".">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>


                                      <tr>
                                    <td style="padding: 25px">
                                        <asp:Label ID="Label2" runat="server" Text="क्या यह  गोदाम मैपिंग हेतु सम्मालित करना चाहते हैं" Font-Bold="true" ForeColor="navy"
                                            Font-Size="11pt"></asp:Label></td>
                                    <td style="padding: 25px">
                                     
                                      <asp:DropDownList ID="Godownflag" runat="server" Width="155px" Height="25px" CssClass="tb6">
                          <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                          <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                           <asp:ListItem Text="NO" Value="N"></asp:ListItem>
                                                      </asp:DropDownList>

                                    </td>
                                </tr>


                                           <tr>
                                    <td style="padding: 25px">
                                        <asp:Label ID="Label5" runat="server" Text="मैपिंग हेतु जिलो का प्रकार" Font-Bold="true" ForeColor="navy"
                                            Font-Size="11pt"></asp:Label></td>
                                    <td style="padding: 25px">
                                     
                                <asp:DropDownList ID="OfferDistType" runat="server" Width="155px" Height="25px" CssClass="tb6" AutoPostBack="true" OnSelectedIndexChanged="OfferDistType_SelectedIndexChanged">
                          <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                          <asp:ListItem Text="Same" Value="S"></asp:ListItem>
                           <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                                      </asp:DropDownList>

                                    </td>
                                </tr>


                                          <tr>
                                    <td style="padding: 25px">
                                        <asp:Label ID="Label6" runat="server" Text="जिला चुने " Font-Bold="true" ForeColor="navy"
                                            Font-Size="11pt"></asp:Label> </td>
                                    <td style="padding: 25px">
                                            <asp:DropDownList ID="ddldistict" runat="server" Width="155px" Height="25px" CssClass="tb6" >
                   
                                                      </asp:DropDownList>
                                    </td>
                                </tr>

                                               <tr>
                                    <td style="padding: 25px">
                                        <asp:Label ID="Label7" runat="server" Text="दिनांक 10/03/2022 की स्थिति मै Closing Balance " Font-Bold="true" ForeColor="navy"
                                            Font-Size="11pt"></asp:Label>(In MT)</td>
                                    <td style="padding: 25px">
                                        <asp:TextBox ID="ClosingBalance" runat="server" Width="150px" AutoComplete="off" onkeypress="return NumberOnly(event)"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="ClosingBalance_FilteredTextBoxExtender1"
                                            runat="server" TargetControlID="ClosingBalance" FilterType="Custom, Numbers" ValidChars=".">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>


                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BTNBLUE" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button button2" OnClientClick="return Hidepopup()" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
         <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                        <asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
                            PopupControlID="pnlAddEdit" TargetControlID="lnkFake"
                            BackgroundCssClass="modalBackground">
                        </asp:ModalPopupExtender>
</asp:Content>

