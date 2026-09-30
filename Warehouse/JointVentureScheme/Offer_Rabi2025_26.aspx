<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Offer_Rabi2025_26.aspx.cs" Inherits="JointVentureScheme_Offer_Rabi2025_26" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Offer Godown Capacity</title>
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
          ul.svertical {
              width: 220px; /* width of menu */
              overflow: auto;
              background: #f4f4f4; /* background of menu */
              margin: 0;
              padding: 0;
              padding-top: 7px; /* top padding */
              list-style-type: none;
          }

              ul.svertical li {
                  text-align: right; /* right align menu links */
              }

                  ul.svertical li a {
                      position: relative;
                      display: inline-block;
                      text-indent: 5px;
                      overflow: hidden;
                      background: rgb(1, 138, 180); /* initial background color of links */
                      font: bold 16px Germand;
                      text-decoration: none;
                      padding: 5px;
                      margin-bottom: 5px; /* spacing between links */
                      color: White;
                      -moz-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8); /* inner right shadow added to each link */
                      -webkit-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                      box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                      -moz-transition: all 0.2s ease-in-out; /* CSS3 transition of hover properties */
                      -webkit-transition: all 0.2s ease-in-out;
                      -o-transition: all 0.2s ease-in-out;
                      -ms-transition: all 0.2s ease-in-out;
                      transition: all 0.2s ease-in-out;
                  }

                      ul.svertical li a:hover {
                          padding-right: 30px; /* add right padding to expand link horizontally to the left */
                          color: Black;
                          background: rgb(153,249,75);
                          -moz-box-shadow: inset -3px 0 2px rgba(114,114,114, 0.8); /* contract inner right shadow */
                          -webkit-box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                          box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                      }

                      ul.svertical li a:before { /* CSS generated content: slanted right edge */
                          content: "";
                          position: absolute;
                          left: 0;
                          top: 0;
                          border-style: solid;
                          border-width: 70px 0 0 20px; /* Play around with 1st and 4th value to change slant degree */
                          border-color: transparent transparent transparent #f4f4f4; /* change black to match the background color of the menu UL */
                      }

          .style4 {
              height: 20px;
          }

          .style5 {
              height: 46px;
          }
      </style>
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
        border-radius: 12px;
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
        font-weight: bold;
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

    .style6 {
        color: #FF3300;
        font-weight: bold;
    }
</style>         
      
</head>
<body>
    <form id="form1" runat="server">
     <%--   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

    <div id="bg" style="background-color:White">
		<div class="wrap">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
            <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
           
       <table>
                        <tr >
                            <td colspan="4" style="font-size: medium;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                             <asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/WarehouseHome.aspx" ForeColor="White"></asp:LinkButton>
                            </td>
                            <td colspan="2" style="background-color: #008CBA ;font-size: medium; color: White; width:100px" align="center" >
                            Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                            <td style="background-color: #008CBA; width:70px;" align="center" >
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="White">Log out</asp:LinkButton></td>
                            </tr>
                            </table>
                            </td>
                        </tr>                
      <tr>
                      <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White" align="center">
                          <p style="font-size: 16px; " class="style6">
                         Offer for JVS 2025-26</p>

                                  <%--<asp:Button class="button button2"  ID="Button4" runat="server" Text="Print"  
                    Width="130px" Height="28px" OnClick="Button4_Click" ></asp:Button>--%>
                      </td>

          

                  </tr>
               
                  
                  <tr>
                    <td>
                        <asp:Label ID="lblDist" runat="server" visible="false"></asp:Label>
                    </td >
                    <td >
                        <asp:Label ID="lblbranch" runat="server" visible="false"></asp:Label>
                    </td>
                    </tr>                            
        
              <tr>
                   <td colspan="4">
                          <p style="font-size: medium; font-weight: 700; color: #CC00CC; text-decoration: underline;">
                          JVS ऑफर हेतु अन्य अनिवार्यताएं निम्नानुसार है:- :</p>
                   </td>
             </tr>
             <tr>
             <td style="vertical-align:top" class="style5">&nbsp;&nbsp 1.</td>
                    <td colspan="3" class="style5">
                          <p>
                           निजी गोदाम, निजी फर्म, कंपनी, पार्टनशिप फर्म, सहकारी संस्‍थाऐं एवं अन्‍य संस्‍थाओं के गोदाम MPWLC के ऑनलाइन पोर्टल https://mpwarehousing.mp.gov.in/warehouse/JointVentureScheme/JointVentureSchemeApp.aspx  पर पंजीकृत होना चाहिए।।
                         </p>
                    </td>
            </tr>             
             <tr>
             <td style="width:30px" >&nbsp;&nbsp 2.</td>
                    <td colspan="3">
                          <p>
                             आवेदक के पास Warehousing Development and Regulatory Authority(WDRA) से अथवा आयुक्‍त/संचालक खाद्य नागरिक आपूर्ति एवं उपभोक्‍ता संरक्षण, मध्‍यप्रदेश द्वारा जारी वैध वेअरहाउस लॉयसेंस उपलब्‍ध होना चाहिए।
                         </p>
                    </td>
            </tr>
             <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 3.</td>
                    <td colspan="3">
                          <p>
                           आवेदक द्वारा Warehousing Development and Regulatory Authority(WDRA) को अथवा आयुक्‍त/संचालक, खाद्य नागरिक आपूर्ति एवं उपभोक्‍ता संरक्षण, मध्‍यप्रदेश को अनुज्ञप्ति हेतु आवेदन किया गया हो तो ऑफर के समय विवरण उपलब्‍ध कराना होगा, किन्‍तु स्‍कंध भंडारण से पूर्व लॉयसेंस अनिवार्यत: उपलब्‍ध कराना होगा।
                             </p>
                    </td> 
            </tr>
             <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 4.</td>
                    <td colspan="3">
                          <p>
                            जिन गोदाम को ब्लैक लिस्ट किया गया है, वह  ब्लैकलिस्ट किए जाने की अवधि तक के लिए अपात्र होंगे।  
                             </p>
                    </td> 
            </tr>
            <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 5.</td>
                    <td colspan="3">
                          <p>
                            	जिन गोदाम/गोदाम संचालकों के विरुध्द अनियमितताओं के लिए शासन/निगम द्वारा पुलिस प्रकरण दर्ज कराया गया है, वह भी अपात्र होंगे।  
                             </p>
                    </td> 
            </tr>
             <%--<tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 6.</td>
                    <td colspan="3">
                          <p> योजना अंतर्गत ऑनलाइन ऑफ़र दिनांक 10 मार्च 2023 को साँय 5.00 तक निगम के निम्नलिखित पोर्टल पर प्रस्तुत करना होगा- 
https://mpwarehousing.mp.gov.in/warehouse/JointVentureScheme/JointVentureSchemeApp.aspx
<br />
                         </p>
                    </td>
                    
            </tr> --%>
             <tr>
             <td style="vertical-align:top">&nbsp;&nbsp 6.</td>
                    <td colspan="3">
                          <p>
                            ऑनलाइन ऑफ़र के साथ गोदाम के श्रेणीकरण हेतु आवश्यक जानकारी गोदाम संचालक द्वारा प्रस्तुत करना अनिवार्य होगा। यदि निरीक्षण के दौरान यह पाया जाएगा कि गोदाम संचालक द्वारा गोदाम के श्रेणीकरण हेतु आवश्यक जानकारी ग़लत प्रस्तुत की गई है, तो ऐसे ऑफ़र को निरस्त कर दिया जाएगा। <br />
                         </p>
                    </td>
                    
            </tr>                         
                         <%--<tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 8.</td>
                    <td colspan="3">
                          <p>
                             गोदाम निर्माणाधीन नहीं होना चाहिए ।
                    </td> 
            </tr> --%>
                         <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 7.</td>
                    <td colspan="3">
                          <p>
                             गोदाम संचालक को निर्धारित प्रारूप में अनुबंध निष्पादित कर अनुबंध के मुताबिक़ शर्तें और नियम का पालन करना अनिवार्य होगा। 
                    </td> 
            </tr> 
             <tr>
             <td> &nbsp;&nbsp 8.</td>
                    <td colspan="3">
                          <p>
                            खरीदी केंद्र स्थापित करने हेतु सहमत / असहमत(असहमति के बावजूद भी जिला प्रशासन द्वारा आवश्यकता अनुसार खरीदी केंद्र स्थापित किया जा सकेगा।) :  &nbsp;
                                        <asp:DropDownList ID="ddlProcCenter" runat="server" Height="25px" Width="100px"> 
                                            <asp:ListItem >--Select--</asp:ListItem>
                                            <asp:ListItem Value="Y">सहमत</asp:ListItem>
                                            <asp:ListItem Value="N">असहमत</asp:ListItem>                                         
                                        </asp:DropDownList>
                            <br />
                         </p>
                    </td>
                    
            </tr> 
            
             <tr>
             <td> &nbsp;&nbsp 9.</td>
                    <td colspan="3">
                          <p>
                            गोदाम संचालक कीटोपचार/धूम्रीकरण हेतु कीटनाशक औषधिया यथा एल्यूमिनियम फास्फाइड, मेलाथियान, डेल्टामेथ्रिन   &nbsp;&nbsp
                                       
                            <br />
                         </p>
                    </td>
                    
            </tr>    
                   <tr>
             <td>&nbsp;&nbsp</td>
                    <td colspan="3">
                          <p>
                            MPWLC से निर्धारित दरो पर प्राप्त करना चाहते है?(हाँ/नहीं):  &nbsp;&nbsp
                                  <asp:DropDownList ID="ddlfumigation" runat="server" Height="25px" Width="100px"> 
                                            <asp:ListItem >--Select--</asp:ListItem>
                                            <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>
                                                                                   
                                        </asp:DropDownList>     
                            <br />
                         </p>
                    </td>
                    
            </tr>  

                  
             <tr>
             <td style="vertical-align:top" class="style5">&nbsp;&nbsp 10.</td>
                    <td colspan="3">
                          <p>
                              <strong>स्‍कंध का बीमा</strong>:- संयुक्‍त भागीदारी(JVS) योजनान्‍तर्गत अनुबंध निष्‍पादन के दौरान गोदामों में भंडारित होने वाले स्‍कंध का बीमा गोदाम संचालक द्वारा कराया जावेगा,   &nbsp;&nbsp
                                       
                            <br />
                         </p>
                    </td>
                    
            </tr>  
           <tr>
             <td >&nbsp;&nbsp 11.</td>
                    <td colspan="3">
                          <p>
                          उपार्जन केन्‍द्र और भण्‍डारण के लिए अनुबंधित निजी गोदाम का चयन निम्‍नलिखित प्राथमिकता क्रम के आधार पर किया जाएगा:-   &nbsp;&nbsp
                                       
                            <br />
                         </p>
                    </td>
                    
            </tr>  
            <tr>
             <td colspan="4" align="center">
               <table border="1" cellspacing="4" style="border:solid">
                   <tr>
                       <td align="center" style="font:x-large">
                           <b>प्राथमिक क्रम</b>
                       </td>
                        <td align="center"><b>विवरण</b></td>
                   </tr>
                    <tr>
                       <td align="center">प्रथम</td>
                        <td>गोदाम जिनके परिसर में वे-ब्रिज के साथ-साथ क्‍लीनिंग/ग्रेडिंग की मशीन उपलब्‍ध है, ऐसी गोदामों को प्रथम प्राथमिकता दी जावेगी। </td>
                   </tr>
                     <tr>
                       <td align="center">द्वितीय</td>
                        <td>गोदाम, जिनके परिसर में वे-ब्रिज की सुविधा हैं।  </td>
                   </tr>
                     <tr>
                       <td align="center">तृतीय</td>
                        <td>गोदाम, जिनकी 4 किमी की परिधि में वे-ब्रिज की सुविधा हैं।   </td>
                   </tr>
                     <tr>
                       <td align="center">चतुर्थ</td>
                        <td>गोदाम, जो मुख्‍य राष्‍ट्रीय/राज्‍य/जिला सड़क मार्ग पर स्थित हैं।  </td>
                   </tr>
                    <tr>
                       <td align="center">अंतिम</td>
                        <td>शेष गोदाम।   </td>
                   </tr>
               </table>
             </td>  
            </tr>  
          <%-- <tr>
             <td >&nbsp;&nbsp</td>
                    <td colspan="3">
                          <p>
                         नोट- उक्‍त सभी श्रेणियों में जहाँ धान मिलर्स की उपलब्‍धता होगी, उनके निकटतम गोदामों को प्राथमिकता होगी। 
                                       
                            <br />
                         </p>
                    </td>
                    
            </tr>  --%>
          <tr><td colspan="4"></td></tr>
             <tr>
             <td>&nbsp;&nbsp </td>
                    <td colspan="3" style="border-color:#008CBA; border-style:solid; border-width:2px" ; >
                          <p style="font-size:16px; color:darkgreen; font-weight: 700;">
                            आपका गोदाम, उक्त JVS योजना में किस "प्राथमिकता क्रम" की पात्रता रखता है चयन करे :  &nbsp;&nbsp
                                  <asp:DropDownList ID="ddlPrioritySelection" runat="server" Height="30px" Width="100px" OnSelectedIndexChanged="ddlPrioritySelection_SelectedIndexChanged" AutoPostBack="true"> 
                                            <asp:ListItem >--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">प्रथम</asp:ListItem>
                                            <asp:ListItem Value="2">द्वितीय</asp:ListItem>
                                            <asp:ListItem Value="3">तृतीय</asp:ListItem>
                                            <asp:ListItem Value="4">चतुर्थ</asp:ListItem>
                                            <asp:ListItem Value="5">अंतिम</asp:ListItem>                    
                                        </asp:DropDownList>     
                            <br />
                         </p>
                    </td>
                    
            </tr>  
            <tr><td colspan="4"><p style="font-size:12px; color:red; font-weight: 700;">
                            नोट:-उपरोक्त "प्राथमिकता क्रम" में से चुनाव ध्यानपूर्वक करे, इसके डॉक्यूमेंट संबंधित शाखा में जमा करने होंगे| उक्त दस्तावेज निरिक्षण के दौरान गलत पाए जाने पर आपका ऑफर रिजेक्ट अथवा "प्राथमिकता क्रम" में परिवर्तन किया जावेगा|</p></td></tr>

           <tr><td colspan="4"></td></tr>
             <tr>
             <td>&nbsp;&nbsp </td>
                    <td colspan="4" style="border-color:#008CBA; border-style:solid; border-width:2px" ; >
                          <p style="font-size:16px; color:darkgreen; font-weight: 700;">
                            संयुक्त भागीदारी योजना की श्रेणी(A अथवा B) का चयन करे :  &nbsp;&nbsp
                                  <asp:DropDownList ID="ddlCategory" runat="server" Height="30px" Width="100px"  AutoPostBack="false" > 
                                            <asp:ListItem >--Select--</asp:ListItem>
                                            <asp:ListItem Value="A">A</asp:ListItem>
                                            <asp:ListItem Value="B">B</asp:ListItem>
                                   
                                        </asp:DropDownList>     
                            <br />
                         </p>
                    </td>
                    
            </tr>  
                      <tr><td colspan="4"></td></tr>
            <%--<tr><td colspan="4"></td></tr>
                    <tr>
             <td >&nbsp</td>
                    <td colspan="3">
                          <p>
                        II.	यदि दो या अधिक निजी अनुबंधित गोदाम प्राथमिकता क्रम मे एक समान स्थिति में होंगे तो उनके बीच प्राथमिकता क्रम का निर्धारण निम्निलिखित आधार पर किया जाएगा:-
                                       
                            <br />
                         </p>
                    </td>
                    
            </tr>  
            <tr>
             <td colspan="4" align="center">
               <table border="1" cellspacing="4" style="border:solid">
                   <tr>
                       <td align="center" style="font:x-large">
                           <b>प्राथमिक क्रम</b>
                       </td>
                        <td align="center"><b>विवरण</b></td>
                   </tr>
                    <tr>
                       <td align="center">प्रथम</td>
                        <td>उस गोदाम का पहले चयन किया जाये जो PMS (Preservation, Maintenance and Security) का कार्य MPWLC या MPWLC द्वारा अनुबंधित एजेन्‍सी को सौंपने के लिये लिखित रूप से सहमति प्रस्‍तुत करें। </td>
                   </tr>
                     <tr>
                       <td align="center">द्वितीय</td>
                        <td>यदि दो या अधिक निजी अनुबंधित गोदाम PMS की लिखित सहमति प्रस्‍तुत करने की स्थिति में भी समान प्राथमिकता क्रम में हैं तो फिर उस गोदाम को प्राथमिकता दी जाये जिसका लायसेंस सबसे पहले जारी हुआ हो।</td>
                   </tr>
                    
               </table>
             </td>  
            </tr>  --%>
                

          
              <tr>
             <td>&nbsp;&nbsp </td>
                    <td colspan="4" style="border-color:#008CBA; border-style:solid; border-width:2px" ; >
                          <p style="font-size:16px; color:darkgreen; font-weight: 700;">
                            आप अपने गोदाम में भंडारित स्कंध का रख-रखाव स्वयं(SPMS) में चुनाव करे :  &nbsp;&nbsp
                                  <asp:DropDownList ID="ddlPMS" runat="server" Height="30px" Width="100px" AutoPostBack="true"> 
                                            <%--<asp:ListItem >--Select--</asp:ListItem>--%>
                                            <asp:ListItem Value="1">SPMS</asp:ListItem>
                                     <%-- <asp:ListItem Value="2">PMS</asp:ListItem>--%>
                                          <%--  <asp:ListItem Value="0">नहीं</asp:ListItem> --%>             
                                        </asp:DropDownList>     
                            <br />
                         </p>
                    </td>
                    
            </tr>  
             <tr><td colspan="4"><p style="font-size:12px; color:red; font-weight: 700;">
                            नोट:-1. JVS गोदामों में भंडारण शुल्क दर 'A' Category के गोदामों में रूपये '81'&nbsp; प्रति में.टन एवं 'B' Category के गोदामों में रूपये &#39;76&#39; प्रति में.टन प्रतिमाह वास्तविक भंडारित मात्रा के आधार पर किराया देय होगा|
                                 </p>
                 
                 </td></tr>
            <tr>
               <%-- <td colspan="4">
                <p style="font-size:15px; color:red; font-weight: 700;">
                            नोट:- धान हेतु गोदामों की ऑफर दरें इस प्रकार है:-
                                 </p>
                 <p style="font-size:12px; color:red; font-weight: 700;">अ) PMS में A श्रेणी गोदाम हेतु रूपये 45 प्रति मे०टन प्रतिमाह, आ) PMS में B श्रेणी गोदाम हेतु रूपये 40 प्रति मे०टन प्रतिमाह,  इ) SPMS में A श्रेणी गोदाम हेतु रूपये 67 प्रति मे०टन प्रतिमाह,ई) SPMS में B श्रेणी हेतु रूपये 62 प्रति मे०टन प्रतिमाह</p>
                 <p style="font-size:15px; color:red; font-weight: 700;">
                             1. मोटे अनाज हेतु गोदामों की ऑफर दरें इस प्रकार है:-
                                 </p>
                    <p style="font-size:12px; color:red; font-weight: 700;">3) PMS में A श्रेणी गोदाम हेतु रूपये 54 प्रति मे०टन प्रतिमाह, ऊ) PMS में B श्रेणी गोदाम हेतु रूपये 49 प्रति मे०टन प्रतिमाह, ऋ) SPMS में A श्रेणी गोदाम हेतु रूपये 79 प्रति मे०टन प्रतिमाह,ए) SPMS में B श्रेणी हेतु रूपये 74 प्रति मे०टन प्रतिमाह</p>
                <p style="font-size:15px; color:red; font-weight: 700;">
                             2. मोटे अनाज/सोयाबीन हेतु गोदामों की ऑफर दरें इस प्रकार है:-
                                 </p>
                    <p style="font-size:12px; color:red; font-weight: 700;">3) PMS में A श्रेणी गोदाम हेतु रूपये 54 प्रति मे०टन प्रतिमाह, ऊ) PMS में B श्रेणी गोदाम हेतु रूपये 49 प्रति मे०टन प्रतिमाह, ऋ) SPMS में A श्रेणी गोदाम हेतु रूपये 79 प्रति मे०टन प्रतिमाह,ए) SPMS में B श्रेणी हेतु रूपये 74 प्रति मे०टन प्रतिमाह</p>--%>
                <%-- <p style="font-size:12px; color:red; font-weight: 700;"></p>
                 <p style="font-size:12px; color:red; font-weight: 700;"></p>
                 <p style="font-size:12px; color:red; font-weight: 700;"></p>--%>
            <%--</td>--%>
                <%--<td>
                <p style="font-size:12px; color:red; font-weight: 700;">
                             मोटे अनाज हेतु गोदामों की ऑफर दरें इस प्रकार है:-
                                 </p>
                 <p style="font-size:12px; color:red; font-weight: 700;"></p>
                 <p style="font-size:12px; color:red; font-weight: 700;"></p>
                 <p style="font-size:12px; color:red; font-weight: 700;"></p>
 </td>
                <td>
                <p style="font-size:12px; color:red; font-weight: 700;">
                             मोटे अनाज/सोयाबीन हेतु गोदामों की ऑफर दरें इस प्रकार है:-
                                 </p>
                 <p style="font-size:12px; color:red; font-weight: 700;"></p>
                 <p style="font-size:12px; color:red; font-weight: 700;"></p>
                 <p style="font-size:12px; color:red; font-weight: 700;">,</p>
                 
                </td>--%>
            </tr>
           <tr><td colspan="4"></td></tr>
             <tr> 
             <td></td>
   <%--                         <td colspan="3">
                                <p style=" font-size:medium;">
                                    <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>     I agree with all above mentioned Terms and Conditions for this JVS offer. All the informations furnished by me are true to the best of my knowledge and belief, further if any discrepancy found in above I will be responsible for that.      
<br />
                                </p>
                                </td>--%>
                                </tr>
            <tr>
                    <td style="height:10px" >
                    </td>
                    
            </tr>

                    <tr>
                      <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White" align="Center">
                          <p style="font-size:16px; color:darkslateblue; font-weight: 700;">
                          आपके द्वारा गोदाम "रख-रखाव" एवं "प्राथमिकता क्रम" संबंधित चयनित विकल्प -:  &nbsp;&nbsp<asp:Label ID="lbljvschois" runat="server" ></asp:Label> </p>
                      </td>

                  </tr> 
           <tr id="jvsB" runat="server" visible="false">
                  <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White" align="Center">
                      <%--   <a href=""> <p style="font-size:16px; color: #FF3300; font-weight: 700;">
                          क्या आप श्रेणी बदलना चाहते हैं ?</p></a>--%>
                      <asp:Button ID="btnpopup" Visible="false" runat="server" style=" color:green; font-weight:700; border-radius:5px;  border-style:solid; border-width:2px; background-color:#f4f4f4" align="Center" Text="क्या आप श्रेणी बदलना चाहते हैं ?" OnClick="btnpopup_Click" />
                      </td>
           </tr>

               <tr id="jvsA" runat="server" visible="false">
                  <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White" align="Center">
                          <p style="font-size:16px; color: #FF3300; font-weight: 700;">
                    <%--     यदि आप श्रेणी परिवर्तन  करना   चाहते हैं तो RM के माध्यम से  परिवर्तन करा सकते हैं |--%></p>
                      </td>
           </tr>
                      
                            
            <tr>
                      <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White" align="Center">
                          <p style="font-size:16px; color: #FF3300; font-weight: 700;">
                          Registered Godown Detail  </p>
                      </td>

                  </tr> 
            <tr>
                    <td style="height:5px" >
                    </td>
                    
            </tr>  
                    <tr>
                    
                    <td class="style4" colspan="4" align="center">
                        Warehouse Name :<asp:Label ID="lblWName" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblRegNo" runat="server" Font-Bold="true"></asp:Label></td>
                  
                    </tr> 
                <tr>
                    <td style="height:5px" >
                    </td>
                    
            </tr>          
            <tr id="trNewOfr" runat="server" visible="false">
                <td colspan="4" id="GVGodowns" runat="server" visible="true" align="center">
                        <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" 
                            Width="90%" EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID." />
                            <asp:BoundField DataField="Godown_No" HeaderText="Godown No." />
                            <asp:BoundField DataField="G_Length" HeaderText="Length in Feet" />
                            <asp:BoundField DataField="G_Width" HeaderText="Width in Feet" />
                            <asp:BoundField DataField="G_Height" HeaderText="Height in Feet" />
                            <asp:BoundField DataField="G_ScientificCapacity" HeaderText="Capacity (MT)" />                           
                                <asp:TemplateField HeaderText="Full Capacity">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="ckstack" runat="server" AutoPostBack="True" OnCheckedChanged="ckstack_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="chkParCpt" runat="server" AutoPostBack="True" OnCheckedChanged="chkParCpt_CheckedChanged" Enabled="false"/>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Offered Capacity (MT)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCapacity" runat="server" Width="100px" Enabled="false">0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Scheme<br>(Rs 40/35/60/55)" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlScheme" runat="server"> 
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="80">B(Rs. 80)</asp:ListItem>
                                            <asp:ListItem Value="85">A(Rs. 85)</asp:ListItem>  
                                              <asp:ListItem Value="60">A -'ब'(Rs. 60)</asp:ListItem>
                                            <asp:ListItem Value="55">B -'ब'(Rs. 55)</asp:ListItem> 
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
                        <br />
                        <asp:Button class="button button2" ID="btnCalCpt" runat="server" Text="Calculate Fee" Width="130px" Height="28px"
                            onclick="btnCalCpt_Click" />
                            
                                                   
                        &nbsp&nbsp;&nbsp&nbsp;

                    </td>            
            </tr>
            
            <tr id="trparofr" runat="server" visible="false">
                <td colspan="4" id="Td1" runat="server" visible="true" align="center">
                        <asp:GridView ID="GridViewParticialCpt" runat="server" AutoGenerateColumns="False" 
                            Width="90%" EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID." />
                            <asp:BoundField DataField="Godown_No" HeaderText="Godown No." />
                            <asp:BoundField DataField="G_Length" HeaderText="Length in Feet" />
                            <asp:BoundField DataField="G_Width" HeaderText="Width in Feet" />
                            <asp:BoundField DataField="G_Height" HeaderText="Height in Feet" />
                            <asp:BoundField DataField="G_ScientificCapacity" HeaderText="Capacity (MT)" />   
                            <asp:BoundField DataField="AvlOfferedCpt" HeaderText="Available Capacity (MT)" />                 
                                <asp:TemplateField HeaderText="Full Capacity">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="ckstackPar" runat="server" AutoPostBack="True" OnCheckedChanged="ckstackPar_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="chkParCptPar" runat="server" AutoPostBack="True" OnCheckedChanged="chkParCptPar_CheckedChanged" Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Offered Capacity (MT)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCapacity" runat="server" Width="100px" Enabled="false">0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Scheme<br>(Rs 85/80/60/55)" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlScheme" runat="server"> 
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="80">B(Rs. 80)</asp:ListItem>
                                            <asp:ListItem Value="85">A(Rs. 85)</asp:ListItem>   
                                             <asp:ListItem Value="60">A -'ब'(Rs. 60)</asp:ListItem>
                                            <asp:ListItem Value="55">B -'ब'(Rs. 55)</asp:ListItem> 
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
                        <br />
                        <asp:Button class="button button2" ID="Button1" runat="server" Text="Calculate Fee" Width="130px" Height="28px"
                            onclick="btnCalCpt_Click" />
                            
                                                   
                        &nbsp&nbsp;&nbsp&nbsp;

                    </td>            
            </tr>            
            
            <tr style="text-align:center">
            <td></td>
            <td style="font-weight:bold ; color:green; border-color:green;" colspan="3">
            Total Offered Capacity :&nbsp&nbsp;&nbsp&nbsp;&nbsp; <input id="txtTotlcpt" value="0.00" disabled="disabled" name="remail" class="text" runat="server" type="text" style="width:100px; font-weight:bold ; color:green;" />
            </td>
            </tr> 
            <tr style="text-align:center">
            <td></td>
            <td style="font-weight:bold ; color:green; border-color:green;" colspan="3">
            Payable Earnest Amount :&nbsp&nbsp <input id="txtOfferAmt" name="remail" value="0.00" disabled="disabled" class="text" runat="server" type="text" style="width:100px; font-weight:bold ; color:green;" />
            </td>
            </tr>
    
            <tr>
                                         <td colspan="4">
                                <p style=" font-size:medium;">
                                    <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>     I agree with all above mentioned Terms and Conditions for this JVS offer. All the informations furnished by me are true to the best of my knowledge and belief, further if any discrepancy found in above I will be responsible for that.      
<br />
                                </p>
                                </td>

                  </tr> 
              <tr>
            <td colspan="4" align="center">
            <asp:Button class="button button2"  ID="btnsubmit" runat="server" Enabled="true" Text="Submit"  Width="130px" Height="28px"
                    onclick="btnsubmit_Click" Visible="true"></asp:Button>
                    &nbsp&nbsp;&nbsp&nbsp;
            <asp:Button class="button button2"  ID="btnPrint" runat="server" Text="Print"  
                    Width="130px" Height="28px" Visible="false" onclick="btnPrint_Click"></asp:Button>
                    &nbsp&nbsp;&nbsp&nbsp;
                            <asp:Button ID="btnpayment" runat="server" Text="Proceed to Payment" class="button button2" Width="130px" Height="28px" Visible="false" 
                            onclick="btnpayment_Click" ></asp:Button>                    
            </td>
            </tr>  
            <tr>
            <td></td>
            <td style="font-weight:bold ; color:Red; border-color:Red;" colspan="3">
           <p style="color:Blue; font-size:medium; text-decoration: underline;">
                                  Note : ऑफर संबंधी जानकारी को सबमिट करने के बाद उसका प्रिंट अनिवार्य रूप से प्राप्त कर रखे | तदुपरान्त पेमेंट विकल्प पर जाकर अनिवार्य रूप से राशि का भुगतान करे, भुगतान कार्यवाही के दौरान आवश्यक जानकारी तथा राशि का विवरण प्राप्त प्रिंटआउट मे उल्लेखित जानकारी तथा राशि को यथावत दर्ज करे। अन्यथा की दशा मे आपका ऑफर मान्य नहीं होगा। 
 <br />
                                </p>
            </td>
            </tr>                                      
          </table>
             <br />
             
<%----------------------------------------------------%>     
            




<asp:Label ID="Label8" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
<cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlCofirmmsg" TargetControlID="Label8"
   CancelControlID="btnNo" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlCofirmmsg" runat="server" CssClass="modalPopup" Height="330px" Width="700px" >
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td style="color:White; font-weight:bold" align="center">Payment For Offer Fee</td>
                   <td style="width:50px"> <asp:Button ID="btnNo" runat="server" Text="Close" CssClass="no" align="left"/> </td>
            </tr> 
                                         
        </table> 
    </div>
    <div class="body">
                    <table cellspacing="1" style="width:100%;">
                         <tr>
                        <td style="height:15px">
                        </td>
                        </tr>
                         <tr>
                            <td style="font-size:12px; font-family:Arial; font-weight:bold" align="left">
                        <table width="100%" >
<%--                            <tbody>
                                <th>Registration ID</th>
                                <th>Godown Owner Name</th>
                                <th>Email ID</th>
                                <th>Contact No </th>
                                <th>Registered Capacity</th>
                                <th>Registration Fee</th>
                            </tbody>--%>
                            <tr style="height:15px;">
                            <td style="width:130px;">
                            &nbsp;Registration ID :
                            </td>
                                    <td>
                                     <asp:Label ID="lblRegID" runat="server" ></asp:Label>
                                    </td>
                            <td style="width:150px;">
                            Godown Owner Name :
                            </td>                                    
                                    <td>
                                     <asp:Label ID="lblOwn" runat="server" ></asp:Label>
                                    </td>
                            </tr>  
                            <tr style="Height:15px;">
                            <td> &nbsp;Email ID :
                            </td>
                                    <td>
                                    <asp:Label ID="lblemailid" runat="server" ></asp:Label>
                                    </td> 
                                    <td>
                                    Contact No :</td>                                   
                                    <td>
                                     <asp:Label ID="lblcontact" runat="server" ></asp:Label>
                                    </td>
                             
                             </tr> 
                             <tr style="Height:20px;">
                             <td>
                             &nbsp;Offer Capacity :</td>
                                    <td>
                                     <asp:Label ID="lblRegCapacity" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                    Offer Fee :</td> 
                                    <td>
                                     <asp:Label ID="lblRegFee" runat="server" ></asp:Label>
                                    </td>                                                                                                                                               
                           </tr>
                          </table>                            
                            </td>
                         </tr>                       
                         <tr>
                            <td style="color:Red"  align="left">&nbsp;Note :</td>
                         </tr> 
                         <tr>
                            <td align="left">
                            <p color:#008080;" style="color:Red">
                            &nbsp;1) ऑनलाइन भुगतान करते समय उपरोक्त दर्शित सभी जानकारियां सही प्रविष्टि करे । </p>
                            </td>
                        </tr>
                         <tr>
                            <td align="left">
                            <p color:#008080;" style="color:Red">
                            &nbsp;2) देय पंजीकरण शुल्क Rs. 1 / मेट्रिक टन । </p>
                            </td>
                        </tr> 
                         <tr>
                            <td align="left">
                            <p color:#008080;" style="color:Red">
                            &nbsp;3) आफर फीस का पुस्टिकरण कार्यालीन दिवस के 24 घंटे में किया जाएगा । </p>
                            </td>
                        </tr>
                                                
                        <tr>
                        <td style="height:10px;">
                        
                        </td>
                        </tr>                                              
                        <tr>
                            <td align="center">
                                       <asp:Button class="button button2" Width="150px" Height="30px" ID="Button2" 
                                        runat="server" Text="Proceed To Payment" align="Center" onclick="Button2_Click" />
                                                                            
                            </td>                     
                        </tr>                     
                    </table>
    </div>                        
</asp:Panel>  

<asp:Label ID="Label5" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlofferpopup" TargetControlID="Label5"
   BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlofferpopup" runat="server" CssClass="modalPopup" Height="170px" Width="300px" Visible="false" >
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td style="color:White; font-weight:bold" align="center">Alert Message</td>
                   <td> </td>
            </tr> 
                                         
        </table> 
    </div>
    <div class="body">
<table align="center" style="width: 100%;border:#008CBA; border-style:solid ; border-width:0px;">
<tr><td style="height:5px;"></td></tr>
 
                        <tr>
                        <td align="center" >
                              आफ़र केवल बालाघाट एवम्‌ शहडोल जिले क़े लिये चालू किया गाया है
                        </td>
                        </tr>
                    <tr><td style="height:5px;"></td></tr>  
                    <tr>
                            <td align="center" >
                                       <asp:Button class="button button2" Width="100px" Height="30px" ID="Button3" 
                                        runat="server" Text="Ok" align="Center" onclick="Button3_Click"/>
                                                                            
                            </td>
                    </tr>                                                                                                                                                                                                 
</table>                   
    </div>                        
</asp:Panel>  
<%----------------------------------------------------%>             
                
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
                                                <br />
                                                Developed By : National Informatics Centre
                                                <br />
                                                Madhya Pradesh, Ministry of Communications and Information Technology</b>
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
