<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseHome.aspx.cs" Inherits="JointVentureScheme_WarehouseHome" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Warehouse Home</title>
      <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
<%--<link href="css/bootstrap.css" rel="stylesheet"/>
<link href="css/bootstrap-responsive.css" rel="stylesheet"/>--%>
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
      <style>

ul.svertical{
width: 220px; /* width of menu */
overflow: auto;
background: #f4f4f4; /* background of menu */
margin: 0;
padding: 0;
padding-top: 7px; /* top padding */
list-style-type: none;
}

ul.svertical li{
text-align: right; /* right align menu links */
}

ul.svertical li a{
position: relative;
display: inline-block;
text-indent: 5px;
overflow: hidden;
background: rgb(1, 138, 180); /* initial background color of links */
font: bold 16px Germand;
text-decoration: none;
padding: 5px;
margin-bottom: 5px; /* spacing between links */
color:White;
-moz-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8); /* inner right shadow added to each link */
-webkit-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
-moz-transition: all 0.2s ease-in-out; /* CSS3 transition of hover properties */
-webkit-transition: all 0.2s ease-in-out;
-o-transition: all 0.2s ease-in-out;
-ms-transition: all 0.2s ease-in-out;
transition: all 0.2s ease-in-out;
}

ul.svertical li a:hover{
padding-right: 30px; /* add right padding to expand link horizontally to the left */
color:Black;
background: rgb(153,249,75);
-moz-box-shadow: inset -3px 0 2px rgba(114,114,114, 0.8); /* contract inner right shadow */
-webkit-box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
}

ul.svertical li a:before{ /* CSS generated content: slanted right edge */
content: "";
position: absolute;
left: 0;
top: 0;
border-style: solid; 
border-width: 70px 0 0 20px; /* Play around with 1st and 4th value to change slant degree */
border-color: transparent transparent transparent #f4f4f4; /* change black to match the background color of the menu UL */

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
    border: 2px solid #E47D21;
}

.button6:hover {
    background-color: #E47D21;
    color: white;
}
.button4 {
    background-color: white;
    color: black;
    border: 2px solid #FC00B3;
}

.button4:hover {
    background-color: #FC00B3;
    color: white;
}
.button5 {
    background-color: white;
    color: black;
    border: 2px solid #CBE555;
}

.button5:hover {
    background-color: #CBE555;
    color: white;
}
.button7 {
    background-color: white;
    color: black;
    border: 2px solid #AEB6BF;
}

.button7:hover {
    background-color: #AEB6BF;
    color: white;
}
.button8 {
    background-color: white;
    color: black;
    border: 2px solid #F4D03F;
}

.button8:hover {
    background-color: #F4D03F;
    color: white;
}
.button9 {
    background-color: white;
    color: black;
    border: 2px solid #117A65;
}

.button9:hover {
    background-color: #117A65;
    color: white;
}
</style> 
</head>
<body>
    <form id="form1" runat="server">
    <div id="bg" style="background-color:White">
		<div class="wrap">
            <img src="../images/CH.jpg" style="width: 100%" alt="" height="150" />
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
            <table style="width:100% ;  text-align:center;">
                        <tr >
                            <td colspan="4" style="font-size: medium;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                            <%-- <asp:LinkButton ID="LinkButton1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/WarehouseHome.aspx" ForeColor="White"></asp:LinkButton>--%>
                            </td>
                            <td colspan="2" style="background-color: #008CBA ;font-size: medium; color: White; width:100px" align="center" >
                            Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                            <td style="background-color: #008CBA; width:70px;" align="center" >
                            <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton1_Click" ForeColor="White">Log out</asp:LinkButton></td>
                            </tr>
                            </table>
                            </td>
                        </tr>            
            
                    <tr>
                    <td style="height:20px"></td>
                    </tr> 
                    


   <%--            <tr id="TrReg" runat="server">
                      <td colspan="4" >
                          <p style="font-size: medium; color: #008080;">  &nbsp;&nbsp;&nbsp;<asp:LinkButton 
                                  ID="link1" Text="Warehouse Registration" runat="server" 
                                  PostBackUrl="~/JointVentureScheme/WarehouseRegistration.aspx" 
                                   Font-Underline="True"></asp:LinkButton></p>
                        
                      </td>

                  </tr>
                  
                     <tr>
                      <td colspan="4">
                          <p style="font-size: medium; color: #008080;">
                         &nbsp;&nbsp;  <asp:LinkButton ID="LinkButton3" Text="Payment for Warehouse Registration " runat="server" PostBackUrl="~/JointVentureScheme/WarehousePayment.aspx" Font-Underline="True"></asp:LinkButton> </p>
                      </td>

                  </tr>
                    <tr id="TrOffer" runat="server">
                      <td colspan="4" style="text-align:center;">
                          <asp:LinkButton ID="LinkButton2" Text="Offer Capacity" runat="server"  class="button button3" Width="200px" Height="50px"
                                  PostBackUrl="~/JointVentureScheme/Offered.aspx" Font-Underline="False"></asp:LinkButton>
                      </td>

                  </tr>
                   <tr>
                      <td colspan="4">
                          <p style="font-size: medium; color: #008080;">
                         &nbsp;&nbsp;  <asp:LinkButton ID="LinkButton4" Text="Payment for Offered Capacity" 
                                  runat="server"
                                  Font-Underline="True" onclick="LinkButton4_Click"></asp:LinkButton> </p>
                      </td>

                  </tr>
                    <tr id="Tr2" runat="server">
                      <td colspan="4" >
                          <p style="font-size: medium; color: #008080;">  &nbsp;&nbsp;&nbsp;<asp:LinkButton 
                                  ID="LinkButton6" Text="Update Warehouse Registration Detail" runat="server" 
                                  PostBackUrl="~/JointVentureScheme/UpdateRegistration.aspx"
                                   Font-Underline="True"></asp:LinkButton></p>
                        
                      </td>

                     </tr>                  
                  
                    <tr id="Tr1" runat="server">
                      <td colspan="4" >
                          <p style="font-size: medium; color: #008080;">  &nbsp;&nbsp;&nbsp;<asp:LinkButton 
                                  ID="LinkButton5" Text="View / Print Registration & Offer for Current Season" runat="server" 
                                  PostBackUrl="~/JointVentureScheme/UpdatePrintPreview.aspx" 
                                   Font-Underline="True"></asp:LinkButton></p>
                        
                      </td>

                  </tr>--%>
                 <%-- ----------------------------------%>
                 
                 <tr>
                 <td >
                     <h5 style="color:red; font-size:small;">नोट: यदि JVS पोर्टल पर आपके रजिस्ट्रेशन के 3 वर्ष पूर्ण हो चुके है तब कृपया रजिस्ट्रेशन Re-New करने हेतु 40 पैसे प्रति/एमटी दर से रजिस्ट्रेशन Fee का पुनः भुगतान करना होगा। </h5>                 
                 </td>
                 </tr>

                    <tr>
                 <td >
                     <h5 style="color:red; font-size:small;">नोट: रजिस्ट्रेशन और ऑफर की फीस 48 घंटे में अपडेट होने के बाद ही ऑफर वेरीफाई लिस्ट में शो होगा |</h5>                 
                 </td>
                 </tr>

<%--                         <tr>
                 <td style="height:20px;">
                 
                 </td>
                 </tr>--%>
                
                     <tr  runat="server" visible="false">
                 <td>
                                <%-- <asp:Button class="button button7" ID="btnjvs" runat="server" 
                                     Text="Choice Filling For JVS Scheme" Font-Size="15px" Font-Bold="false"
                                 TabIndex="1" Width="260px" Height="60px" OnClick="btnjvs_Click"></asp:Button>    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   --%>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                         <asp:Button class="button button1" ID="btnjvs" runat="server" 
                                     Text="Choice Filling For JVS Scheme 2022-23" Font-Size="15px" Font-Bold="false"
                                 TabIndex="1" Width="350px" Height="60px"  OnClick="btnjvs_Click1" Enabled="true"></asp:Button>    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   
                                
                                        
                 </td>
                 </tr>   

                
                     <tr  runat="server" visible="false" >
                 <td>
                                <%-- <asp:Button class="button button7" ID="btnjvs" runat="server" 
                                     Text="Choice Filling For JVS Scheme" Font-Size="15px" Font-Bold="false"
                                 TabIndex="1" Width="260px" Height="60px" OnClick="btnjvs_Click"></asp:Button>    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   --%>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                         <asp:Button class="button button1" ID="Button7" runat="server" 
                                     Text=" Change Choice Filling For JVS Scheme 2022-23" Font-Size="15px" Font-Bold="false"
                                 TabIndex="1" Width="350px" Height="60px" OnClick="Button7_Click"  Enabled="true"></asp:Button>    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   
                                
                                        
                 </td>
                 </tr>   




                 <tr>
                 <td >
                                 <asp:Button class="button button2" ID="btnNewReg" runat="server" 
                                     Text="Warehouse Registration" Font-Size="15px" Font-Bold="false"
                                 TabIndex="1" Width="260px" Height="60px" onclick="btnNewReg_Click" ></asp:Button>    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   
                                             
                                 <asp:Button class="button button6" ID="btnPaymentReg"
                        runat="server" Text="Payment for Warehouse Registration" Font-Size="15px" Font-Bold="false"   
                                 TabIndex="2" Width="260px" Height="60px" onclick="btnPaymentReg_Click"></asp:Button> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                 
                                 <asp:Button class="button button3" ID="btnupdatereg" runat="server" 
                            Text="Update Registration"   Font-Size="15px" Font-Bold="false"
                                 TabIndex="3" Width="260px" Height="60px" onclick="btnupdatereg_Click"></asp:Button>                   
                 </td>
                 </tr>
                 <tr>
                 <td style="height:20px;">
                 
                 </td>
                 </tr>
                 <tr>
                 <td>
                                 <asp:Button class="button button1" ID="btnoffer" runat="server" Enabled="true"
                                     Text="Offer Capacity" Font-Size="15px" Font-Bold="false"
                                 TabIndex="1" Width="260px" Height="60px" onclick="btnoffer_Click"></asp:Button>    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   
                                             
                                 <asp:Button class="button button4" ID="btnpaymentoffer" 
                        runat="server" Text="Payment for Offered Capacity" Font-Size="15px" Font-Bold="false"   
                                 TabIndex="2" Width="260px" Height="60px" onclick="btnpaymentoffer_Click"></asp:Button> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                 
                                 <asp:Button class="button button5" ID="btnPreviewregofr" runat="server" 
                            Text="View Registration & Offer" Font-Size="15px" Font-Bold="false"
                                 TabIndex="3" Width="260px" Height="60px" onclick="btnPreviewregofr_Click" ></asp:Button>                   
                 </td>
                 </tr>  
                 
                 <tr>
                 <td style="height:20px;">
                 
                 </td>
                 </tr>
                 <tr>
                 <td>
                                 <asp:Button class="button button7" ID="Button1" runat="server" Enabled="false"
                                     Text="Appeal For Unfit Godown" Font-Size="15px" Font-Bold="false"
                                 TabIndex="1" Width="260px" Height="60px" onclick="Button1_Click"></asp:Button>    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   
                                             
                                 <asp:Button class="button button8" ID="btnchangepass" 
                        runat="server" Text="Change Password" Font-Size="15px" Font-Bold="false"   
                                 TabIndex="2" Width="260px" Height="60px"></asp:Button> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                 
                                 <asp:Button class="button button9" ID="Button5" runat="server" 
                            Text="Track Reg./Offer Payment Status" Font-Size="15px" Font-Bold="false"
                                 TabIndex="3" Width="260px" Height="60px" onclick="Button5_Click" ></asp:Button>                   
                 </td>
                 </tr>     

                  <tr>
                 <td>
                                 <%--<asp:Button class="button button5" ID="UpdateGodownAToB" runat="server" 
                                     Text="Choice(A/B) Confirmation For PMS" Font-Size="15px" Font-Bold="false"
                                 TabIndex="1" Width="260px" Height="60px" OnClick="UpdateGodownAToB_Click" ></asp:Button>--%>    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   
                                             
                                 <asp:Button class="button button2" ID="Button8" runat="server" Enabled="true" 
                                     Text=" Offered Warehouse Report" Font-Size="15px" Font-Bold="false"
                                 TabIndex="2" Width="260px" Height="60px" OnClick="Button8_Click" ></asp:Button> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                 
                             <%--    <asp:Button class="button button3" ID="ButtonChoisePMS" runat="server" 
                            Text="Change Choice Filling" Font-Size="15px" Font-Bold="false"
                                 TabIndex="3" Width="260px" Height="60px" OnClick="ButtonChoisePMS_Click" ></asp:Button> --%>                  
                 </td>
                 </tr>     



                <%--<tr>
                    <td>
                          &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                         <asp:Button class="button button1" ID="UpdateGodownAToB" runat="server" 
                                     Text="Choice(A/B) Confirmation For PMS" Font-Size="15px" Font-Bold="false"
                                 TabIndex="1" Width="260px" Height="60px" OnClick="UpdateGodownAToB_Click" ></asp:Button>    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    </td>
                   

                </tr>
                <tr>
                 <td style="height:20px;">
                 
                 </td>
                      </tr>
                <tr>
                    
                      <td>
                          &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                         <asp:Button class="button button2" ID="Button8" runat="server" 
                                     Text=" Offered Warehouse Report" Font-Size="15px" Font-Bold="false"
                                 TabIndex="1" Width="350px" Height="60px" OnClick="Button8_Click" ></asp:Button>    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    </td>
               </tr>--%>
             
                                    
                  </table>
                  <br />
                  <br />
                  

                  <hr />
                  <hr />
<table style="width: 100%">
                              <tr>
                      <td colspan="4" style="background-color: White; width:100%;">
                          <p style="font-size: medium; font-weight:bold;">
                         &nbsp;&nbsp;&nbsp; Note :</p>
                      </td>
                  </tr>                  
                                <tr> 
                            <td colspan="4">

<%--                            <p style="color:Red;">
                                  &nbsp;&nbsp;&nbsp; 1. Warehouse Registration Detail के लिए  Warehouse Registration & Offer Detail लिंक पर click करें|     
 <br />
                                </p>--%>
<%--                                 <p style="color:Red;">
                                  &nbsp;&nbsp;&nbsp; 2. Warehouse Registration Detail Update के लिए  Update Warehouse Registration Detail लिंक पर click करें|     
 <br />
                                </p>
                                <p style="color:Red;">
                                  &nbsp;&nbsp;&nbsp; 3. कृपया Warehouse Registration के लिए Warehouse Registration लिंक पर click करें|     
 <br />
                                </p>
                                 <p style="color:Red;">
                                 &nbsp;&nbsp;&nbsp;  4. भुगतान करने की स्थति में Payment for Registration of Warehouse लिंक पर क्लिक करे।
 <br />
                                </p>--%>
                               <%-- <p style="color:Red;">
                                   3.डुप्लीकेट रसीद की प्राप्ति हेतु कृपया लिंक "Duplicate Receipt for Warehouse Registration  " पर क्लिक करे। 
 <br />
                                </p>
                                <p style="color:Red;">
                                   4.JV/Rental scheme आवेदन हेतु लिंक "Application Form For JV Scheme" पर क्लिक करे।
 <br />
                                </p>--%>
                            </td>

                        </tr>
</table>                        

              
<%-------------------------------Model Registration----------------------------------------------------%>
              
<asp:Label ID="Label8" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
<cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlCofirmmsg" TargetControlID="Label8"
   CancelControlID="btnNo" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlCofirmmsg" runat="server" CssClass="modalPopup" Height="465px" Width="800px" Style="display: none">
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td style="color:White; font-weight:bold; font-size:14px;" align="center">रजिस्ट्रेशन करने के लिए आवश्यक निर्देश</td>
                   <td style="width:50px"> <asp:Button ID="btnNo" runat="server" Text="Close" CssClass="no" align="left"/> </td>
            </tr> 
                                         
        </table> 
    </div>
    <div class="body">
<table align="center" style="width: 100%;border:#008CBA; border-style:solid ; border-width:0px;">
<tr><td style="height:10px;"></td></tr>
 
                        <tr>
                        <td align="center" >
                        <table border="1" width="100%" >
                            <tbody>
                                <tr>
                                    <th style="width:15px;">
                                        क्रमांक</th>
                                    <th>
                                        निर्देश</th>
                                    <th style="width:30px;">
                                        चुने</th>
                                </tr>
                            </tbody>
                             <tr align="center">
                                    <td>
                                     <asp:Label ID="lblinspid" runat="server" >1.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="lblinsptype" runat="server" >(*) वाले कॉलम अनिवार्य हैं।</asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="chkreg1" runat="server" />
                                    </td>                                                                                                          
                              </tr>
                             <tr align="center">
                                    <td>
                                     <asp:Label ID="Label1" runat="server" >2.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="Label2" runat="server" >आवेदक का नाम, संस्था के प्रकार, मोबाइल नंबर एवं अन्य जानकारी सावधानीपूर्वक प्रविष्ट करे, प्रविष्ट की गई जानकारी मे किसी भी प्रकार की त्रुटि के लिए आवेदक स्वयं जिम्मेदार होंगा |</asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="chkreg2" runat="server" />
                                    </td>                                                                                                          
                              </tr>
                             <tr align="center">
                                    <td>
                                     <asp:Label ID="Label3" runat="server" >3.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="Label4" runat="server" > आवेदक मोबाइल नंबर सही प्रविष्ट करे क्योकि सभी जानकारी रैजिस्टर्ड मोबाइल नंबर पर ही भेजी जाएगी ।</asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="chkreg3" runat="server" />
                                    </td>                                                                                                          
                              </tr>
                              
                         <tr align="center">
                                   <td>
                                     <asp:Label ID="Label13" runat="server" >4.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="Label14" runat="server" > एक गोदाम संचालक के रजिस्ट्रेशन मे शामिल किए जाने वाले समस्त गोदाम एक ही परिसर मे स्थित होने चाहिए, अन्य परिसर अथवा अन्य स्थान मे स्थित गोदामो के लिए प्रथक रजिस्ट्रेशन करे।</asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="chkregparisar" runat="server" />
                                    </td>                             
                        </tr>
                         <tr align="center">
                                   <td>
                                     <asp:Label ID="Label15" runat="server" >5.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="Label16" runat="server" >आवेदक के पास  WDRA से अथवा आयुक्त,खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश का’ वैध वेअरहाउस लायसेंस’ उपलब्ध होना चाहिए  अथवा अनुज्ञप्ति हेतु आवेदन किया गया हो तो आवेदित विवरण उपलब्ध कराना होगा तथा स्कंध भण्डारण से पूर्व लायसेंस अनिवार्यत: उपलब्ध कराना होगा ।</asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="chkregtype" runat="server" />
                                    </td>                             
                        </tr>                                                                                                                                         
                          </table>      
                        </td>
                        </tr>
                    <tr><td style="height:15px;"></td></tr>  
                    <tr>
                            <td align="center" >
                                       <asp:Button class="button button2" Width="150px" Height="30px" ID="Button2" 
                                        runat="server" Text="Agree All Conditions" align="Center" 
                                           onclick="Button2_Click" />
                                                                            
                            </td>
                    </tr>                                                                                                                                                                                                 
                </table>                   
    </div>                        
</asp:Panel>
<%------------------------------------------------------------------------%>


<%-------------------------------Model Offer ----------------------------------------------------%>
              
<asp:Label ID="Label5" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlofferpopup" TargetControlID="Label5"
   CancelControlID="btnofrclose" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlofferpopup" runat="server" CssClass="modalPopup" Height="350px" Width="700px" Style="display: none">
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td style="color:White; font-weight:bold" align="center">आफर करने के लिए आवश्यक निर्देश</td>
                   <td style="width:50px"> <asp:Button ID="btnofrclose" runat="server" Text="Close" CssClass="no" align="left"/> </td>
            </tr> 
                                         
        </table> 
    </div>
    <div class="body">
<table align="center" style="width: 100%;border:#008CBA; border-style:solid ; border-width:0px;">
<tr><td style="height:20px;"></td></tr>
 
                        <tr>
                        <td align="center" >
                        <table border="1" width="100%" >
                            <tbody>
                                <tr>
                                    <th style="width:15px;">
                                        S.No.
                                    </th>
                                    <th>
                                        Discription</th>
                                    <th style="width:20px;">
                                        Select</th>
                                </tr>
                            </tbody>
                             <tr align="center">
                                    <td>
                                     <asp:Label ID="Label6" runat="server" >1.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="Label7" runat="server" >आवेदक के पास  Warehousing Development and Regulatory Authority (WDRA) से अथवा आयुक्त,खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश का’ वैध वेअरहाउस लायसेंस’ उपलब्ध होना चाहिए  अथवा अनुज्ञप्ति हेतु आवेदन किया गया हो तो स्कंध भण्डारण से पूर्व लायसेंस अनिवार्यत: उपलब्ध कराना होगा ।</asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="chkofr1" runat="server" />
                                    </td>                                                                                                          
                              </tr>
                             <tr align="center" runat="server" visible="false">
                                    <td>
                                     <asp:Label ID="Label9" runat="server" >2.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="Label10" runat="server" > अपात्र पाये गाये गोदाम आगामी 15 दिवस पस्चात पुनः JVS का आफर कर सकेंगे |</asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="chkofr2" runat="server" />
                                    </td>                                                                                                          
                              </tr>
                             <tr align="center">
                                    <td>
                                     <asp:Label ID="Label11" runat="server" >2.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="Label12" runat="server" >गोदाम तक पहुंचने हेतु न्यूनतम WBM स्तर की बारहमासी सडक(All Weather 	Approach Road) हों । </asp:Label>
                                    </td>  
                                 <%-- <td align="left">
                                     <asp:Label ID="Label26" runat="server" >गोदाम में वैज्ञानिक भण्डारण के दृष्टिगत वायुसंचरण हेतु रोशनदान / वेंटिलेटर के साथ-साथ गोदाम की ऊंचाई न्यूनतम 16 फिट होना आवश्यक है ।</asp:Label>
                                    </td>  --%>
                                    <td>
                                        <asp:CheckBox ID="chkofr3" runat="server" />
                                    </td>                                                                                                          
                              </tr>       
                             <tr align="center">
                                    <td>
                                     <asp:Label ID="Label27" runat="server" >3.</asp:Label>
                                    </td>  
                                  <td align="left">
                                     <asp:Label ID="Label26" runat="server" >गोदाम संचालक ऑनलाइन ऑफर करते समय PMS/SPMS का चयन करते हुए अपने गोदाम का ऑफर कर सकेगे। इन ऑफर्ड गोदामों को PMS/SPMS के माध्‍यम से संचालित कराने हेतु निगम का निर्णय अंतिम होगा ।</asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="CheckBox5" runat="server" />
                                    </td>                                                                                                          
                              </tr>         
                          </table>      
                        </td>
                        </tr>
                    <tr><td style="height:20px;"></td></tr>  
                    <tr>
                            <td align="center" >
                                       <asp:Button class="button button2" Width="150px" Height="30px" ID="Button3" 
                                        runat="server" Text="Agree All Conditions" align="Center" 
                                           onclick="Button3_Click" />
                                                                            
                            </td>
                    </tr>                                                                                                                                                                                                 
                </table>                   
    </div>                        
</asp:Panel>
<%------------------------------------------------------------------------%>



<%-------------------------------Model Offer ----------------------------------------------------%>
              
<asp:Label ID="Label17" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
<cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="Panel1" TargetControlID="Label17"
   CancelControlID="Button4" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Height="350px" Width="700px" Style="display: none">
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td style="color:White; font-weight:bold" align="center">श्रेणी चयन हेतु  आवश्यक निर्देश</td>
                   <td style="width:50px"> <asp:Button ID="Button4" runat="server" Text="Close" CssClass="no" align="left"/> </td>
            </tr> 
                                         
        </table> 
    </div>
    <div class="body">
<table align="center" style="width: 100%;border:#008CBA; border-style:solid ; border-width:0px;">
<tr><td style="height:20px;"></td></tr>
 
                        <tr>
                        <td align="center" >
                        <table border="1" width="100%" >
                            <tbody>
                                <tr>
                                    <th style="width:15px;">
                                        S.No.
                                    </th>
                                    <th>
                                        Discription</th>
                                    <th style="width:20px;">
                                        Select</th>
                                </tr>
                            </tbody>
                             <tr align="center">
                                    <td>
                                     <asp:Label ID="Label18" runat="server" >1.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="Label19" runat="server" >मेरे द्वारा रबी  विपणन वर्ष 2022-23 की संयुक्त भागीदारी योजना के गोदामों का पीएमएस  एजेंसी से सुपरवीजन  कराने  के संबंध मे विस्तृत विवरण एवं सेवा शर्तों को भाली -भाती  पड़  लिया गया  हैं | </asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </td>                                                                                                          
                              </tr>
                             <tr align="center">
                                    <td>
                                     <asp:Label ID="Label20" runat="server" >2.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="Label21" runat="server" > योजना मे शामिल होने हेतु दोनों श्रेणी (A एवं  B) मे से किसी एक का चुनाव किया जाना अनिवार्य हैं |</asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                    </td>                                                                                                          
                              </tr>
                             <tr align="center">
                                    <td>
                                     <asp:Label ID="Label22" runat="server" >3.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="Label23" runat="server" >श्रेणी A  मे शामिल होने  पर संचालक  JVS POLICY  एवं इस  नीती  मे वर्णित  दायित्वों का पालन करना होगा  । </asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="CheckBox3" runat="server" />
                                    </td>                                                                                                          
                              </tr>    
                            
                                 <tr align="center">
                                    <td>
                                     <asp:Label ID="Label24" runat="server" >4.</asp:Label>
                                    </td>
                                    <td align="left">
                                     <asp:Label ID="Label25" runat="server" >श्रेणी B मे का चयन करने पर नीती मे वर्णित दायित्वों का पालन संबंधित गोदाम संचालक एवं  MPWLC द्वारा  नियुक्त पी एम. एस.  एजेसी को करना अनिवार्य होगा  । </asp:Label>
                                    </td>  
                                    <td>
                                        <asp:CheckBox ID="CheckBox4" runat="server" />
                                    </td>                                                                                                          
                              </tr> 
                          </table>      
                        </td>
                        </tr>
                   <%-- <tr><td style="height:20px;"></td></tr>  --%>
                    <tr>
                            <td align="center" >
                                       <asp:Button class="button button2" Width="150px" Height="30px" ID="Button6" 
                                        runat="server" Text="Agree All Conditions" align="Center" 
                                        OnClick="Button6_Click"    />
                                                                            
                            </td>
                    </tr>                                                                                                                                                                                                 
                </table>                   
    </div>                        
</asp:Panel>
<%------------------------------------------------------------------------%>





<cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlchangePass" TargetControlID="btnchangepass" CancelControlID="btncloseconfrm"
    BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlchangePass" runat="server" CssClass="modalPopup" Height="250px" Width="500px">
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td style="color:Black; font-weight:bold; font-size:larger;" align="center">Change Password</td>
            </tr> 
                                         
        </table> 
    </div>
    <div class="body">
                    <table cellspacing="1" cellpadding="3" style="width:100%;">
                         <tr>
                        <td style="height:10px">
                        </td>
                        </tr>

                        <tr>
                        <td align="Right">Enter Current Password :</td>
                       <td>
                           <input id="txtForgotPassword" name="rcpwd"  runat="server" class="text"  />
                       </td>                       
                        </tr>
                        <tr>
                        <td align="Right">New Password :</td>
                       <td>
                           <input id="txtnewpass" name="rcpwd"  runat="server" class="text"  />
                       </td>                       
                        </tr>
                        <tr>
                        <td align="Right">Re-Enter New Password :</td>
                       <td>
                            <input id="txtnewpassre" name="rcpwd"  runat="server" class="text"  />
                       </td>                       
                        </tr>                                                
                         <tr>
                        <td style="height:10px">
                        </td>
                        </tr>
                       
                        <tr>
                            <td align="center" colspan="2">
                                       <asp:Button class="button button2" Width="150px" Height="30px" ID="btncloseconfrm" 
                                        runat="server" Text="Close" align="Center" />
                                        
                                       &nbsp &nbsp &nbsp 
                                       <asp:Button class="button button2" Width="150px" Height="30px" ID="btnChange" 
                                        runat="server" Text="Change Password" align="Center" 
                                           onclick="btnChange_Click" />                                        
                            </td>
                        </tr>                     
                    </table>
    </div>                        
</asp:Panel>   


<%----------------------------------------------------------------------------%>
              
             <div style="background-image: url('../images/div_bg.png')">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="height: 20px;" colspan="5">
                                            <img id="Img2" src="../Images/line.png" height="30px" width="100%" alt="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%" align="center">
                                            <a href="http://www.mp.nic.in/">
                                                <img src="../Images/NIC-logo.png" width="200px" height="50px" alt="" />
                                            </a>
                                        </td>
                                        <td style="width: 1%" align="center">
                                            <img id="Img1" src="../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                        </td>
                                        <td style="color: Navy; font-size: 8pt; width: 58%;" align="center">
                                            <b>© 2020 &nbsp;National Informatics Centre.All Rights Reserved
                                                 of Communications and Information Technologyd Information Technology</b>
                                        </td>
                                        <td style="width: 1%" align="center">
                                            <img id="Logo" src="/../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                        </td>
                                        <td style="width: 20%" align="center">
                                        <table><tr>    
                                        <td>                                        <a href="http://india.gov.in/">
                                                <img src="../Images/natindialogo.png" width="100px" height="50px" alt="" />
                                            </a>
                                            </td>
                                            <td>
                                             <a href="http://www.digitalindia.gov.in/">
                                                <img src="../Images/di.png" width="100px" height="50px" alt="" />
                                            </a>
                                            </td>
                                            </tr></table>

                                        </td>
                                    </tr>
                                </table>
                            </div>              
        </div>
        </div>
    </form>
</body>
</html>