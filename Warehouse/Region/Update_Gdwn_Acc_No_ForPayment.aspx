<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Update_Gdwn_Acc_No_ForPayment.aspx.cs" Inherits="Region_Update_Gdwn_Acc_No_ForPayment" Title="Update Account No." %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
      .popbag
    {
    background-color:gray;
    filter:alpha(opacity=90);
    opacity:0.8;
    z-index:10000;
    }
    .modalpop
    {
    background-color:#FFFFFF;
    border-width:3px;
    border-color:Black;
    padding-top:10px;
    padding-left:10px;
    width:250px;
    height:120px;
     border-radius: 25px;
    text-shadow:yellow;	
    }
</style>

<style type="text/css">
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        width: 80%;
        border: 3px solid #0DA9D0;
        border-radius: 6px;
        padding:0
      
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
</style>  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style type="text/css">
        .button {
        border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: #4CAF50; /* Green */
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight:bold;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
            margin-left: 2px;
            margin-right: 2px;
            margin-top: 4px;
        }

.button1 {
    background-color: white; 
    color: black; 
    border: 2px solid #4CAF50;
}

.button1:hover {
    background-color: #4CAF50;
    color: white;
}

.button2 {
    background-color: white; 
    color: black; 
    border: 2px solid #008CBA;
}

.button2:hover {
    background-color: #008CBA;
    color: white;
}
        #Img1
        {
            width: 10%;
        }
        .style1
        {
            width: 35px;
        }
    </style>


<fieldset style="width: 1000px; border: 2px solid navy; margin-left: 10px ; margin-right:10px">
    <center>
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 100%">        
                    <%--      ----------Start  JVS Lic -----------------------%>
                <tr>
                    <td align="center" valign="top">
                <center>
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="4" align="center" >
                                    <asp:Label ID="lblGodownMaster" runat="server" Text="Add/Update/Delete Godown Owners Account Information For Payment" Font-Bold="true" 
                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px" colspan="4"></td>
                            </tr>  
                <tr>
                <td align="left" style="font-size:medium;">
                यदि बैंक का विवरण किसी अन्य प्रदेश का  है या प्रदेश के जिले की सूची में बैंक का नाम व शाखा का नाम नहीं आ रहा है तो कृपया दी गई लिंक से गोदाम का बैंक विवरण दर्ज करें |
                </td>
                <td align="right" colspan="2" >
                <asp:Button class="button button1" id="btn_Add_New" style="width:220px" runat="server" 
                   Text="Add Godown Account Information" Height="30px" ></asp:Button>
                </td>
                </tr>  
                <tr>                
                <td align="left" style="font-size:medium; height:50px;">
                यदि किसी गोदाम का बैंक का विवरण गलत हो गया है तो कृपया दी गई लिंक से डिलीट करें |
                </td>                
                <td align="right">
                <asp:Button class="button button2" id="Button4" style="width:220px" runat="server" 
                        Text="Delete Godown Account Information" Height="30px" onclick="Button4_Click" >
                </asp:Button></td>      
                </tr> 
                            <tr>
                                <td style="height: 20px" colspan="4"></td>
                            </tr>                 
                
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="4" align="center" >
                                    <asp:Label ID="Label11" runat="server" Text="Update Godown Owners Account Information For Payment" Font-Bold="true" 
                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                </td>
                            </tr>                               
                                                                                     
                            <tr>
                                <td style="height: 50px ; font-size:14px" colspan="4" align="center">
                                  &nbsp;&nbsp;&nbsp;&nbsp  Branch : &nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList 
                                        ID="ddlBranch" runat="server" AutoPostBack="true"
                                        Height="25px" Width="200px" 
                                        onselectedindexchanged="ddlBranch_SelectedIndexChanged"> </asp:DropDownList>
                                </td>
                            </tr> 
                            
                            <tr>
                                <td style="height: 5px" colspan="4"></td>
                            </tr>                                       
                                                                        
                              <tr>
                                <td colspan="4" valign="top" align="center">
                                  
                                  <asp:GridView ID="Depositor_Gridview"  runat="server" DataKeyNames="Godown_Id" AutoGenerateColumns="False" Width="100%"  BackColor="White" 
                                        BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" 
                            CellSpacing="2" PageSize="50" onselectedindexchanged="Depositor_Gridview_SelectedIndexChanged">
                                    <Columns>
                                      <asp:BoundField DataField="Godown_Id" HeaderText="Godown ID"/>
                                      <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                      <asp:BoundField DataField="DistrictName" HeaderText="District Name"  />
                                      <asp:BoundField DataField="BankName" HeaderText="Bank Name" />
                                      <asp:BoundField DataField="BranchName" HeaderText="Branch Name"  ItemStyle-Width="100px"/>
                                      <asp:BoundField DataField="Acc_Holder_Name" HeaderText="Account Holder Name"  />
                                      <asp:BoundField DataField="Account_No" HeaderText="Account No." />
                                      <asp:BoundField DataField="IFSC_Code" HeaderText="IFSC Code" /> 
                                      
                                      <asp:BoundField DataField="Bank_District_Id" HeaderText="Bank_District_Id" /> 
                                      <asp:BoundField DataField="Bank_Id" HeaderText="Bank_Id" /> 
                                      <asp:BoundField DataField="Bank_Branch_Id" HeaderText="Bank_Branch_Id" /> 
                                      <asp:BoundField DataField="GO_Approval_Status" HeaderText="Approve/Reject By Godown Owner" /> 
                                      <asp:BoundField DataField="RO_Approval_Status" HeaderText="Approve/Reject By RM" /> 
                                      <asp:BoundField DataField="Beneficiary" HeaderText="Beneficiary" /> 
                                        
                                                 
                                      <asp:CommandField SelectText="Select" HeaderText="Select" ShowSelectButton="True" >
                                           <ControlStyle Font-Bold="True" ForeColor="Red" />
                                     </asp:CommandField>             

                                    </Columns>
                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center" Wrap="true"
                                                                                    Height="20px" Font-Size="11px" />
                                                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                  </asp:GridView>
                                </td>
                                </tr> 
                                
                               

                                <tr id="tr1" runat="server" visible="false">
                                <td colspan="4" align="center" style="width:100%;">
                                   <table style="width:100%;">
                                   
                                   
                            <tr>
                                <td style="height: 5px" colspan="4"></td>
                            </tr>  
                                                               
 <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center">
            <asp:Label ID="Label37" runat="server"  Font-Size="14px" Font-Bold="true" ForeColor="WhiteSmoke"
                                        Text="Godown Owner Account Details"></asp:Label></td>
    </tr>
    
                            <tr>
                                <td style="height: 10px" colspan="4">
                                <asp:Label ID="lblgdwnid" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>      
                                        <tr>
                                            <td style="height: 30px; width:170px ; font-size:14px" align="left">
                                                <asp:Label ID="Label4" runat="server" Text="Godown Name : "></asp:Label> 
                                             </td>
                                             <td style="height: 30px ; width:220px ; font-size:14px"  align="left">
                                                <asp:TextBox ID="txtGdwnName" runat="server"  ReadOnly="true"
                                                    Width="200px" Height="20px" ></asp:TextBox>                                                   
                                             </td >
                                             <td style="height: 30px ; width:170px ; font-size:14px" align="left">
                                               Bank District :
                                             </td>  
                                             <td style="height: 30px ; width:170px ; font-size:14px"  align="left">                                             
                                            <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true"
                                                Height="25px" Width="205px" 
                                                     onselectedindexchanged="ddlDistrict_SelectedIndexChanged"> 
                                            </asp:DropDownList>                                           
                                            </td>
                                        </tr>                                   
                                        <tr>
                                            <td style="height: 30px; width:170px ; font-size:14px" align="left">
                                                <asp:Label ID="Label3" runat="server" Text="Bank Name : "></asp:Label> 
                                             </td>
                                             <td style="height: 30px ; width:170px ; font-size:14px"  align="left">
                                            <asp:DropDownList ID="ddlBankName" runat="server" AutoPostBack="true"
                                                Height="25px" Width="205px" 
                                                     onselectedindexchanged="ddlBankName_SelectedIndexChanged"> 
                                            </asp:DropDownList>                                           
                                             </td >
                                             <td style="height: 30px ; width:170px ; font-size:14px" align="left">
                                               Bank Branch Name :
                                             </td>  
                                             <td style="height: 30px ; width:170px ; font-size:14px"  align="left">                                             
                                            <asp:DropDownList ID="ddlBBranch" runat="server" AutoPostBack="true"
                                                Height="25px" Width="205px" 
                                                     onselectedindexchanged="ddlBBranch_SelectedIndexChanged"> 
                                            </asp:DropDownList>                                                                                      
                                            </td>
                                        </tr>                                   
                                        <tr>
                                            <td style="height: 30px; width:170px ; font-size:14px" align="left">
                                                <asp:Label ID="Label2" runat="server" Text="IFSC Code : "></asp:Label> 
                                             </td>
                                             <td style="height: 30px ; width:170px ; font-size:14px"  align="left">
                                                <asp:TextBox ID="txtIFSC" runat="server" Width="200px" Height="20px" >
                                                </asp:TextBox>
                                             </td >
                                             <td style="height: 30px ; width:170px ; font-size:14px" align="left">
                                               Account Holder Name :
                                             </td>  
                                             <td style="height: 30px ; width:170px ; font-size:14px"  align="left">                                             
                                                <asp:TextBox ID="txtGOwnerName" runat="server" Width="200px" Height="20px" style="text-transform:uppercase" >
                                                </asp:TextBox>                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px; width:170px ; font-size:14px" align="left">
                                                <asp:Label ID="Label1" runat="server" Text="Account No. : "></asp:Label> 
                                             </td>
                                             <td style="height: 30px ; width:170px ; font-size:14px"  align="left">
                                                <asp:TextBox ID="txtAccNo" runat="server" Width="200px" Height="20px"></asp:TextBox>
                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtAccNo" ValidChars="0123456789">
                   </cc1:FilteredTextBoxExtender>                                                    
                                             </td >
                                             <td style="height: 30px ; width:170px ; font-size:14px" align="left">
                                               Re Type Account No. :
                                             </td>  
                                             <td style="height: 30px ; width:170px ; font-size:14px"  align="left">                                             
                                                <asp:TextBox ID="txtAccNo2" runat="server" Width="200px" Height="20px" ></asp:TextBox>  
                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAccNo2" ValidChars="0123456789">
                   </cc1:FilteredTextBoxExtender>                                                                                              
                                            </td>
                                        </tr>                                        
                <tr>

                <td align="right" colspan="2" style="height:70px">
                <asp:Button class="button button1" id="btnupdate" style="width:130px" runat="server" 
                   Text="Update" Height="29px" onclick="btnupdate_Click"
                                       ></asp:Button>&nbsp&nbsp&nbsp&nbsp
                </td>  
                <td align="left" colspan="2">
                            
                <asp:Button class="button button2" id="btnClose" style="width:130px" runat="server" 
                        Text="Close" Height="29px" onclick="btnClose_Click" >
                </asp:Button></td>      
                </tr>                                        
                                   </table>
                                </td>
                            </tr>
                     <tr>
                          <td style="height: 5px" colspan="4">
                          </td>
                     </tr>        

</table>

<%------------------------------------------------------------------------%>

<cc1:modalpopupextender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlviewbilldetails" TargetControlID="btn_Add_New"
 CancelControlID="btncos" BackgroundCssClass="modalBackground">
</cc1:modalpopupextender>

<%--<asp:Panel ID="pnlviewbilldetails" runat="server" CssClass="modalPopup" style="width: 850px; height:620px; display: none;"  ScrollBars="Vertical">--%>

<asp:Panel ID="pnlviewbilldetails" runat="server" CssClass="modalPopup" style="width: 850px; height:250px;" ScrollBars="Vertical">

   <div >
        <table cellspacing="1" cellpadding="3" style="width:100%;"> 
 <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center">
            <asp:Label ID="Label10" runat="server"  Font-Size="14px" Font-Bold="true" ForeColor="WhiteSmoke"
                                        Text="Add Other State / Bank Branch not in List Godown Owner Account Details"></asp:Label></td>
    </tr>
<tr>

   <td style="width:10px;">
    </td>
    </tr>        
        
            <tr>
                <td style="width:200px;">
                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="8pt" 
                        ForeColor="Navy" Text="Branch Name"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:DropDownList ID="ddlAddBranch" runat="server" AutoPostBack="true" 
                        Font-Size="10pt" Height="25px" TabIndex="1" Width="200px" 
                        onselectedindexchanged="ddlAddBranch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="8pt" 
                        ForeColor="Navy" Text="Godown Name"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:DropDownList ID="ddlGodown" runat="server" AutoPostBack="false" 
                        Font-Size="10pt" Height="25px" TabIndex="1" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
        
<tr>
   <td>
     <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" 
           Text="Bank Name"></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtA_BName" runat="server" Height="22px" Width="200px"></asp:TextBox>
   </td>
<td>
<asp:Label ID="Label7" runat="server" Text="Bank Branch Name" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtA_BankBranch" Visible="true" AutoPostBack="false" Text=""  
        TabIndex="9" CssClass="tb6" Height="22px" Width="200px"
        ></asp:TextBox>
</td>
</tr>        
<tr>
   <td>
     <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" 
           Text="Account Holder Name"></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtA_Holder" runat="server" Height="22px" Width="200px"></asp:TextBox>
   </td>
<td>
<asp:Label ID="lblcrate" Visible="true" runat="server" Text="IFSC Code" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtA_IFSC" Visible="true" Text=""  
        TabIndex="9" CssClass="tb6" Height="22px" Width="200px"
        ></asp:TextBox>
</td>
    </tr>

    <tr>
                                                            <td>
    <asp:Label ID="lbltod" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Account No."></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtA_Acc" runat="server" Height="22px" Width="200px"></asp:TextBox>
   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtA_Acc"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender></td>
                                                            <td>
    <asp:Label ID="Label5" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Re Type Account No."></asp:Label>
    </td>
<td valign="middle"> 
   <asp:TextBox ID="txtA_AccRe" runat="server" Height="22px" Width="200px"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtA_AccRe"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
   </td>
    </tr>

            <tr>
                <td align="center" style="height:50px" colspan="4">
                           <asp:Button class="button button2" Width="150px" Height="30px" ID="btnAddSubmit" 
                            runat="server" Text="Submit" align="Center" onclick="btnAddSubmit_Click"/> &nbsp &nbsp &nbsp 
     
                            
                        <input id="btncos" name="Close" type="button" style="height:30px; width:150px;"
                        class="button button2" value="Close" />                                                          
                </td>
            </tr>                     
        </table>
     </div>                        
</asp:Panel>   

<%------------------------------END Of JVS Bill----------------------------------------------%>



<%------------------------------------------------------------------------%>

<cc1:modalpopupextender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel1" TargetControlID="Button4"
 CancelControlID="btndeeletclose" BackgroundCssClass="modalBackground">
</cc1:modalpopupextender>

<%--<asp:Panel ID="pnlviewbilldetails" runat="server" CssClass="modalPopup" style="width: 850px; height:620px; display: none;"  ScrollBars="Vertical">--%>

<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="width: 750px; height:150px;" ScrollBars="Vertical">

   <div >
        <table cellspacing="1" cellpadding="3" style="width:100%;"> 
 <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center">
            <asp:Label ID="Label12" runat="server"  Font-Size="14px" Font-Bold="true" ForeColor="WhiteSmoke"
                                        Text="Delete Godown Owner Account Details"></asp:Label></td>
    </tr>
<tr>

   <td style="width:10px;">
    </td>
    </tr>        
        
            <tr>
                <td style="width:150px;">
                    <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="8pt" 
                        ForeColor="Navy" Text="Branch Name"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:DropDownList ID="ddldeleteBranch" runat="server" AutoPostBack="true" 
                        Font-Size="10pt" Height="25px" TabIndex="1" Width="200px" 
                        onselectedindexchanged="ddldeleteBranch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="8pt" 
                        ForeColor="Navy" Text="Godown Name"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:DropDownList ID="ddlDeleteGodown" runat="server" AutoPostBack="false" 
                        Font-Size="10pt" Height="25px" TabIndex="1" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
   

            <tr>
                <td align="center" style="height:50px" colspan="4">
                           <asp:Button class="button button2" Width="150px" Height="30px" ID="Button1" 
                            runat="server" Text="Delete" align="Center" onclick="Button1_Click"/> &nbsp &nbsp &nbsp 
     
                            
                        <input id="btndeeletclose" name="btndeeletclose" type="button" style="height:30px; width:150px;"
                        class="button button2" value="Close" />                                                          
                </td>
            </tr>                     
        </table>
     </div>                        
</asp:Panel>   

<%------------------------------END Of JVS Bill----------------------------------------------%>
</div>
</center>
      </td>
      </tr>
      
      <%--      ----------End JVS Lic -----------------------%>                  
                </table>     
            </div>
        </center>
    </fieldset>
</asp:Content>
