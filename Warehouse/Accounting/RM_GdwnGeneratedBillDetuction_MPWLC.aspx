<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="RM_GdwnGeneratedBillDetuction_MPWLC.aspx.cs" Inherits="Accounting_RM_GdwnGeneratedBillDetuction_MPWLC" Title="Untitled Page" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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

    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script> 
   

<style type="text/css">
.button {
    background-color: #4CAF50; /* Green */
    border: none;
    color: white;
    padding: 0px 0px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 12px;
    font-weight:bold;
    margin: 4px 2px;
    -webkit-transition-duration: 0.4s; /* Safari */
    transition-duration: 0.4s;
    cursor: pointer;
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
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy; margin-left:5px;">
        <center>
        <div>
      
<table width="100%">
<tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center">
            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="WhiteSmoke"
                                        Text="Actual Godown Generated Bill for Payment"></asp:Label></td>
</tr>
<tr>
<td style="height: 50px ; font-size:14px" align="center" colspan="4">
    Branch &nbsp;<asp:DropDownList ID="ddlBranch" runat="server" 
             Height="25px" Width="160px"  AutoPostBack="true"
        onselectedindexchanged="ddlBranch_SelectedIndexChanged"> </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;
     Godown &nbsp; <asp:DropDownList ID="ddlgdwn" runat="server" 
             Height="25px" Width="250px"  AutoPostBack="true"
         onselectedindexchanged="ddlgdwn_SelectedIndexChanged"> </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;
     JVS Bill No. &nbsp;<asp:DropDownList ID="ddlbillno" runat="server" 
             Height="25px" Width="160px"  AutoPostBack="true"
         onselectedindexchanged="ddlbillno_SelectedIndexChanged"> </asp:DropDownList>
</td>
</tr>    

<tr id="trdet" runat="server" visible="false">
    <td>
        <table width="100%">
                               
                               
<tr>
    <td  align="center" colspan="4">
                        <div style="width:100%;">
                        <img id="Img1" src="../Images/line.png" height="25px" width="100%" alt="" />
                        </div>        
    </td>
</tr>                               
                        <tr>
                            <td  align="right" style="font-size:14px;" >
                                <asp:Label ID="Label25" runat="server" Text="Month :- " ></asp:Label>
                            </td>
                            <td  align="left" style="font-size:14px;">
                                &nbsp;&nbsp;<asp:Label ID="lblrmmonth" runat="server" Text="माह :- " ></asp:Label></td>
                            <td  align="right" style="font-size:14px;" >
                                <asp:Label ID="Label14" runat="server" Text="Financial Year :- " ></asp:Label>
                            </td>
                            <td align="left" style="font-size:14px;">                              
                               &nbsp;&nbsp;<asp:Label ID="lblfinancial" runat="server" Text="माह :- " ></asp:Label></td>
                        </tr>    
                        <tr>
                            <td  align="right" style="font-size:14px;">
                                <asp:Label ID="Label6" runat="server" Text="From Date :- " ></asp:Label>
                            </td>
                            <td  align="left" style="font-size:14px;">                              
                               &nbsp;&nbsp;<asp:Label ID="lblfrmdate" runat="server" Text="माह :- " ></asp:Label></td>
                            <td  align="right" style="font-size:14px;">
                                <asp:Label ID="Label10" runat="server" Text="To Date :- "></asp:Label>
                            </td>
                            <td align="left" style="font-size:14px;">                                  
                                &nbsp;&nbsp;<asp:Label ID="lbltodate" runat="server" Text="माह :- " ></asp:Label></td>
                        </tr>
                        <tr>
                            <td  align="right" style="font-size:14px;" >                                
                                <asp:Label ID="Label27" runat="server" Text="Commodity :- "></asp:Label>
                            </td>
                           <td  align="left" style="font-size:14px;">                               
                                &nbsp;&nbsp;<asp:Label ID="lblrmcmd" runat="server" Text="माह :- " ></asp:Label></td>
                            <td  align="right" style="font-size:14px;" >                            
                                <asp:Label ID="Label20" runat="server" Text="Closing Balance :- "></asp:Label>
                            </td>
                            <td align="left" style="font-size:14px;">                                  
                                &nbsp;&nbsp;<asp:Label ID="lblclosingbal" runat="server" ></asp:Label></td>
                        </tr>                                                             

<tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center">
            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="WhiteSmoke"
                                        Text="View Actual Generated Bill Details"></asp:Label></td>
</tr>  

                        <tr>
                            <td colspan="3"  style=" font-size:14px ; height:40px; ">ब्रांच मैनेजर द्वरा बनाएँ गए बिल का विवरण देखने के लिये क्लिक करें |

                            </td>
            <td align="center" colspan="4" >          
               <asp:Button class="button button2" id="btnviewbill" style="width:150px" runat="server" Text="View Bill" Height="29px" >
               </asp:Button>
            </td>                             
                        </tr>  
                        
<%--                        <tr>
                            <td colspan="3" style=" font-size:14px ; height:40px; ">ब्रांच मैनेजर द्वरा बनाएँ गए बिल मे किये गए कतोत्र का विवरण देखने के लिये क्लिक करें |

                            </td>
            <td align="center" colspan="4" >          
               <asp:Button class="button button2" id="btnviewdetuction" style="width:150px" runat="server" Text="View Bill Detuction" Height="29px" >
               </asp:Button>
            </td>                             
                        </tr>   --%>                                                
        </table>
    </td>
</tr>
</table>

<%------------------------------------------------------------------------%>

<cc1:modalpopupextender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlviewbilldetails" TargetControlID="btnviewbill"
 CancelControlID="btncloseconfrm" BackgroundCssClass="modalBackground">
</cc1:modalpopupextender>

<%--<asp:Panel ID="pnlviewbilldetails" runat="server" CssClass="modalPopup" style="width: 70%; height:620px; display: none;"  ScrollBars="Vertical">--%>

<asp:Panel ID="pnlviewbilldetails" runat="server" CssClass="modalPopup" style="width: 900px; height:600px;display: none;" ScrollBars="Vertical" >
    <div >
        <table cellspacing="1" cellpadding="3" style="width:100%;"> 
            <tr>
                    <td align="center" width="100%">
                        <div id="PrintDiv" >
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="Label1" runat="server" Text="M.P. Warehousing & Logistics Corporation -" Font-Size="14px" Font-Bold="true" ></asp:Label>
                                            <asp:Label ID="lblP_regionnm" runat="server" Font-Size="14px" Font-Bold="true" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"  align="center">
                                            <asp:Label ID="Label2" runat="server" Text="STORAGE CHARGES" underline="True" Font-Size="14px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height:10px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  align="left">
                                            <asp:Label ID="Label21" runat="server" Text="शाखा का नाम :-  " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblbranch" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>                                    
                                    
                                    
                                    <tr>
                                        <td  align="left">
                                            <asp:Label ID="Label12" runat="server" Text="वेयरहाउस का नाम :-  " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblgdwnname" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                        </td>
                                        <td  align="left">
                                            <asp:Label ID="Label18" runat="server" Text="गोदाम क्र. :-" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblgdwnNo" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                                                                                
                                    <tr>
                                        <td  align="left">
                                            <asp:Label ID="Label9" runat="server" Text="माह :-  " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lbldatefromto" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                        </td>
                                        <td  align="left">
                                            <asp:Label ID="Label7" runat="server" Text="जमाकर्ता :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblGdnum" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td  align="left">
                                            <asp:Label ID="Label11" runat="server" Text="बिल क्रमांक :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblbillno" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                        </td>
                                        <td  align="left">
                                            <asp:Label ID="Label13" runat="server" Text="स्कंध का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblcmd" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr> 
                                    
                                    <tr>
                                        <td  align="left">
                                            <asp:Label ID="Label15" runat="server" Text="GSTN : 23AADCM7742B1ZU" Font-Size="12px"></asp:Label>&nbsp;
                                        </td>
                                        <td  align="left">
                                            <asp:Label ID="Label17" runat="server" Text=" मात्रा :- मेट्रिक टन मे" Font-Size="12px"></asp:Label>&nbsp;
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="height:10px;">
                                        </td>
                                    </tr>                                                                                                                                                                           
                                
                                    <tr>
                                        <td align="center" colspan="2">
                                        <asp:GridView ID="GD1" runat="server" AutoGenerateColumns="False" width="100%" Font-Names="Arial" 
                                          DataKeyNames="Date" BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                          Font-Size="11px" BorderColor="#CCCCCC"> <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText = "क्र." ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Date" HeaderText="दिनाँक" >
                                                    </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="Opening_Weight" HeaderText="प्रारंभिक मात्रा" ItemStyle-HorizontalAlign="Left" > 
                                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="Receive_Weight" HeaderText="जमा मात्रा" ItemStyle-HorizontalAlign="Left" > 
                                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                                                
                                                    <asp:BoundField DataField="Issue_Weight" HeaderText="भुगतान मात्रा" ItemStyle-HorizontalAlign="Left" > 
                                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="Closing_Weight" HeaderText="शेष मात्रा" ItemStyle-HorizontalAlign="Left" > 
                                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="Per_Day_Rate" HeaderText="शुल्क दर प्रति दिन" ItemStyle-HorizontalAlign="Left" > 
                                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="Total_Charges" HeaderText="राशि (6x7)" ItemStyle-HorizontalAlign="Left" > 
                                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundField>                                                                                                                        
                                                </Columns>
                                            <FooterStyle Font-Bold="True" ForeColor="#C70039" HorizontalAlign="center" Height="15px" Font-Size="11px" />
                                            <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                                         </asp:GridView>
                                        </td>
                                    </tr>
<tr>
    <td style="height:20px;">
        
    </td>
</tr>

<tr>
<td style="height:25px; font-size:14px;" align="right">
    कुळ देयक बिळ राशि    &nbsp;     ('B1') &nbsp;: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</td>
<td>
 <asp:Label ID="lblNetAmount" runat="server" Text="स्कंध का नाम :- " Font-Size="14px" ForeColor="Red"></asp:Label>
</td>
</tr> 

<tr>
<td style="height:25px; font-size:14px;"  align="right">
    गोदाम संचालक को देय शुध राशि     &nbsp;   ('B1') &nbsp;: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</td>
<td>
 <asp:Label ID="lbljvsnetamt" runat="server" Text="स्कंध का नाम :- " Font-Size="14px" ForeColor="Red"></asp:Label>
</td>
</tr>    

<tr>
<td style="height:25px; font-size:14px;"  align="right">
    MPWLC को नेट भुगतान योग्य राशि     &nbsp;   ('B'='B1'-'B2') &nbsp;: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</td>
<td>
 <asp:Label ID="lblmpwlcnetamt" runat="server" Text="स्कंध का नाम :- " Font-Size="14px" ForeColor="Red"></asp:Label>
</td>
</tr>                                  
                                    
                                    
                                    
<tr>
    <td style="height:30px;">
        
    </td>
</tr>                                    


<%-----------------DSC Div-----------   --%>                        
                                    <tr>
<td align="right" colspan="2" style="width:100%;">

<table style="width:100%;" >

<tr class="fountcolor">
<td align="right">
<asp:Image ID="Image5B" runat="server" Height="30px" Visible="false"
        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
<td align="right">
<asp:Image ID="Image5" runat="server" Height="30px" Visible="false"
        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
</tr>
<tr class="fountcolor">
<td align="right">
<asp:Label ID="lbldscTB" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
<td align="right">
<asp:Label ID="lbldscT" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr class="fountcolor">
<td align="right">
<asp:Label ID="lblDSC_HolderB" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
<td align="right">
<asp:Label ID="lblDSC_Holder" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr class="fountcolor">
<td align="right">
<asp:Label ID="lblSigningDateB" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
<td align="right">
<asp:Label ID="lblSigningDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr class="fountcolor">
<td align="right">
<asp:Label ID="lblIpAddB" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
<td align="right">
<asp:Label ID="lblIpAdd" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr class="fountcolor" >
<td colspan="2">
    <br />
</td>
</tr>
<tr class="fountcolor">
<td align="right">
<asp:Label ID="Label43" runat="server" Text="Manager Account" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
<td align="right">
<asp:Label ID="Label44" runat="server" Text="छेत्रिय प्रबंधक(MPWLC) के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>

</table>

</td>                      
                                    </tr>      
                                    
                                    
<%------------DSC DIv END----------------- --%>  
                                                                             
                                </table>
                        </div>                
                    </td>
                </tr>        
            <tr>
                <td align="center" style="height:50px">
                           <asp:Button class="button button2" Width="150px" Height="30px" ID="btncloseconfrm" 
                            runat="server" Text="Close" align="Center" /> &nbsp &nbsp &nbsp 
     
                            
                        <input id="Button1" name="Print" type="button" style="height:30px; width:150px;"
                        class="button button2" value="Print"  onclick="PrintDiv();" />                                                          
                </td>
            </tr>                     
        </table>
     </div>                        
</asp:Panel>   

<%----------------------------------------------------------------------------%>



</div>
</center>
</fieldset>
</asp:Content>


