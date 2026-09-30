<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="InspOfficer_PVWelcome.aspx.cs" Inherits="Inspections_Inspection_Officer_InspOfficer_PVWelcome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <style type="text/css">

.wrap { 
	margin: 0 auto; 
	width: 960px;
	
	-moz-box-shadow: 0px 5px 23px #000;

-webkit-box-shadow: 0px 5px 23px #000;

box-shadow: 0px 5px 23px #000;
	
}

input.submit {
	color: #fff;
	padding: 7px 10px;
	border: 0;
	font-weight: bold;
	background: #777;
	border-radius: 25px; 
}

input.text
{

    border: 2px solid rgb(173, 204, 204);
    height: 20px;
    width: 223px;
    font-size: 16px;
    box-shadow: 0px 0px 27px rgb(204, 204, 204) inset;
    transition:500ms all ease;
    padding:3px 3px 3px 3px;

}
              </style>
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

.button3 {
    background-color: white; 
    color: black; 
    border: 2px solid #f44336;
}

.button3:hover {
    background-color: #f44336;
    color: white;
}
.button6 {
    background-color: white;
    color: black;
    border: 2px solid #008CBA;
}

.button6:hover {
    background-color: #008CBA;
    color: white;
}
    .style1
    {
        height: 30px;
    }
</style>      

    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>
       <script type="text/javascript">
           function preventInput(evnt) {
               //Checked In IE9,Chrome,FireFox
               if (evnt.which != 9) evnt.preventDefault();
           }
        </script>  
            <script type="text/javascript" language="javascript">
                function ConfirmOnDelete() {
                    if (confirm("Are you sure want to Delete This Inspection ?") == true)
                        return true;
                    else
                        return false;
                }
    </script>
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
        border-radius: 12px;
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
<div>
  
       <%-----------------------end of First Gride----------------%>

                    <table align="center" style="width: 100%;border:#008CBA; border-style:solid ; border-width:0px;">

 
                        <tr>
                        <td align="center" colspan="3" >
                        <table border="1" width="70%" >
                            <tbody>
                                <th>Inspection ID </th>
                                <th>Inspection Type </th>
                                <th> Inspection Period</th>
                                <th>Branch </th>
                            </tbody>
                            <tr align="center">
                                    <td>
                                     <asp:Label ID="lblinspid" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                     <asp:Label ID="lblinsptype" runat="server" ></asp:Label>
                                    </td>  
                                    <td>
                                     <asp:Label ID="lblInspPeriod" runat="server" ></asp:Label>
                                    </td>                                    
                                    <td>
                                     <asp:Label ID="lblbranch" runat="server" ></asp:Label>
                                    </td>                                                                       
                                </tr>
                          </table>      
                        </td>
                        </tr>
                         <tr>
                            <td align="center" style="border:#E6C79D; border-style:solid ; border-width:2px;" colspan="4">
                            <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Operation For Physical Inspection</span>
                            </td>                            
                        </tr>
                        <tr>
                        <td style="height:10px">
                        </td>
                        </tr>
                    <tr>    
                   <td style="width:30px;" align="center">1.
                   </td>
                      <td style="font-size: large; color: #008080;width:300px;">
                             <asp:LinkButton ID="LinkButton9" Text="Fill Annexure A" runat="server" Visible="true" Font-Underline="True"></asp:LinkButton>
                     </td>
                      <td style="font-size:13px; color: #008080;">
                             <asp:LinkButton ID="LinkButton6" Text="Print" runat="server" Visible="true" 
                                 Font-Underline="True" onclick="LinkButton6_Click" ></asp:LinkButton>
                     </td>
<%--                     <td>
                           <asp:Button class="button button2" ID="btnNewReg" runat="server" 
                              Text="Print" Font-Size="15px" Font-Bold="false"  Width="100px" 
                               Height="25px" onclick="btnNewReg_Click"></asp:Button>
&nbsp;&nbsp;&nbsp;&nbsp;
                           <asp:Button class="button button2" ID="Button2" runat="server" 
                              Text="Fill" Font-Size="15px" Font-Bold="false" Width="100px" Height="25px"></asp:Button>
                     </td>--%>                     
                  </tr>
                                      <tr>    
                   <td style="width:30px;" align="center">2.
                   </td>
                      <td style="font-size: large; color: #008080;width:150px;">
                             <asp:LinkButton ID="LinkButton3" Text="Fill Annexure B" runat="server" Visible="true" Font-Underline="True"  
                             PostBackUrl="~/Inspection/Fill_Inspection_Annexure_B.aspx" ></asp:LinkButton>

                     </td>
                      <td style="font-size:13px; color: #008080;">
                             <asp:LinkButton ID="LinkButton7" Text="Print" runat="server" Visible="true" 
                                 Font-Underline="True" onclick="LinkButton7_Click" ></asp:LinkButton>
                     </td>                     
<%--                     <td>
                           <asp:Button class="button button2" ID="Button3" runat="server" 
                              Text="Print" Font-Size="15px" Font-Bold="false"  Width="100px" 
                               Height="25px" onclick="Button3_Click"></asp:Button>
&nbsp;&nbsp;&nbsp;&nbsp;
                           <asp:Button class="button button2" ID="Button4" runat="server" 
                              Text="Fill" Font-Size="15px" Font-Bold="false" Width="100px" Height="25px" 
                               onclick="Button4_Click"></asp:Button>
                     </td> --%>                    
                  </tr>
                                      <tr>    
                   <td style="width:30px;" align="center">3.
                   </td>
                      <td style="font-size: large; color: #008080;width:150px;">
                             <asp:LinkButton ID="LinkButton4" Text="Fill Annexure C" runat="server" Visible="true" Font-Underline="True"  
                              ></asp:LinkButton>
                     </td>
                     <td style="font-size:13px; color: #008080;">
                             <asp:LinkButton ID="LinkButton8" Text="Print" runat="server" Visible="true" 
                                 Font-Underline="True" onclick="LinkButton8_Click" ></asp:LinkButton>
                     </td>
<%--                     <td >
                           <asp:Button class="button button2" ID="Button5" runat="server" 
                              Text="Print" Font-Size="15px" Font-Bold="false"  Width="100px" 
                               Height="25px" onclick="Button5_Click"></asp:Button>
&nbsp;&nbsp;&nbsp;&nbsp;
                           <asp:Button class="button button2" ID="Button6" runat="server" 
                              Text="Fill" Font-Size="15px" Font-Bold="false" Width="100px" Height="25px"></asp:Button>
                     </td>--%>                     
                  </tr>
                                      <tr>    
                   <td style="width:30px;" align="center">4.
                   </td>
                      <td style="font-size: large; color: #008080;width:400px;">
                             <asp:LinkButton ID="LinkButton5" Text="Fill Summary of Inspection" runat="server" Visible="true" Font-Underline="True"  
                             PostBackUrl="~//Inspections/Inspection_Officer/InspOfficer_FillOverall_PVInsp.aspx" ></asp:LinkButton>
                     </td>
                      <td style="font-size:13px; color: #008080;">
                      

                             <asp:LinkButton ID="LinkButton10" Text="Print" runat="server" Visible="true" href="http://www.mpwarehousing.com/useful_file/Insp.pdf"
                                 Font-Underline="True" ></asp:LinkButton>
                     </td>                     
                  </tr>                  
                                          
                   <%--<tr>
                   <td style="width:30px;" align="center">1.
                   </td>
                      <td >
                          <p style="font-size: medium; color: #008080;">  
                             <asp:LinkButton ID="LinkButton9" Text="फ़िल भौतिक सत्यापन" runat="server" Visible="true" Font-Underline="True"  
                             PostBackUrl="~/Inspection/InspOfficer_FillPVInspection.aspx" ></asp:LinkButton>
                         </p>
                     </td>
                  </tr> 
                   <tr>
                   <td style="width:30px;" align="center">2.
                   </td>
                      <td >
                          <p style="font-size: medium; color: #008080;">  
                             <asp:LinkButton ID="LinkButton3" Text="त्रुटि पत्रक" runat="server" Visible="true" Font-Underline="True"  
                             PostBackUrl="~/Inspection/InspOfficer_NirikshanTrutiPatrak.aspx" ></asp:LinkButton>
                         </p>
                     </td>
                  </tr>
                   <tr>
                   <td style="width:30px;" align="center">3.
                   </td>
                      <td >
                          <p style="font-size: medium; color: #008080;">  
                             <asp:LinkButton ID="LinkButton4" Text="गणना पत्रक" runat="server" Visible="true" Font-Underline="True"  
                             PostBackUrl="~/Inspection/InspOfficer_NirikshanTrutiPatrak.aspx" ></asp:LinkButton>
                         </p>
                     </td>
                  </tr>
                        <tr>
                        <td style="height:20px">
                        </td>
                        </tr>                  
                  
                        <tr>
                        <td align="center" colspan="2"><asp:LinkButton ID="lnklbl_final" runat="server" align="right" 
                                ForeColor="Red" Font-Size="12pt" onclick="lnklbl_final_Click">Click Here to Final Submition of All this Operations</asp:LinkButton>
                        </td>
                        </tr> 
                        <tr>
                        <td style="height:20px">
                        </td>
                        </tr> --%>   
                        
                       <tr>
                        <td style="height:20px">
                        </td>
                        </tr>                                                                                                                                                                                     
                </table>                                 
            </div>
<asp:LinkButton ID="lnklbl_final" runat="server" align="right" Visible="false"></asp:LinkButton>            
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlFinalSub" TargetControlID="lnklbl_final"
    CancelControlID="btnNo" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlFinalSub" runat="server" CssClass="modalPopup"  Height="300px" Width="500px" Style="display: none">
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td style="color:White; font-weight:bold" align="center">Godown's Physical Inpection Final Submition by OPT</td>
                   <td style="width:50px"> <asp:Button ID="Button1" runat="server" Text="Close" 
                           CssClass="no" align="left" onclick="Button1_Click"/> </td>
            </tr>         
        </table> 
    </div>
    <div class="body">
                    <table cellspacing="1" cellpadding="3">
                         <tr>
                        <td style="height:20px">
                        </td>
                        </tr>                    
                        <tr>
                                <td style="color:Black; font-weight:normal" align="center">भौतिक सत्यापन अधिकारी का मोबाइल नंबर : 
                                    <asp:Label ID="lblmobno" runat="server" Text="Label" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                </td>
                        </tr>
                                            
                        <tr>   
                            <td valign="top"  style="text-align:center;">
                            <asp:Button class="button button2" Width="150px" Height="30px" 
                                    ID="btno_generateOTP" runat="server" Text="Generate OTP" align="Center" 
                                    onclick="btno_generateOTP_Click"/>
                            </td>      
                        </tr>
                         <tr>
                        <td style="height:20px"><asp:Label ID="lbl_otpnovalidate" runat="server" Font-Bold="true" Font-Size="Medium" Visible="true"></asp:Label>
                        </td>
                        </tr>
                        <tr id="otpentersection" runat="server" visible="false">
                        
                                <td style="color:Black; font-weight:normal" align="center">क्रप्या 6 डिजिट का OTP दर्ज करें : &nbsp;&nbsp;&nbsp;&nbsp
                                 <asp:TextBox ID="txtOTP" runat="server" class="text" type="text" Height="25px" 
                                        Width="100px" placeholder="Enter OTP" MaxLength="6"></asp:TextBox>&nbsp;&nbsp;&nbsp
                                 <asp:Button class="button button2" Width="80px" Height="25px" ID="btnoptsubmit" 
                                        runat="server" Text="Submit" align="Center" onclick="btnoptsubmit_Click"/>
                                </td>
                        </tr>
                         <tr>
                        <td style="height:20px">
                        </td>
                        </tr>                         
                        
                        <tr>
                                <td style="color:red; font-weight:normal" align="center">नोट :- भौतिक सत्यापन अधिकारी का मोबाइल नंबर गलत हे या OTP नहीं आ रहा हे तो RM Office से मोबाइल नंबर बदलवा ले |
                                </td>
                        </tr>                                                

                    </table>        
    </div>
    <div class="footer" align="right">
    </div>
</asp:Panel>   
</div>
</asp:Content>

