<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfferedJvs.aspx.cs" Inherits="JointVentureScheme_OfferedJvs" %>

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
          .style4
          {
              height: 20px;
          }
          .style5
          {
              height: 46px;
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
    .style6
    {
        color: #FF3300;
        font-weight: bold;
    }
</style>         
      
</head>
<body>
    <form id="form1" runat="server">
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
                         Category Choice Filling For JVS Scheme 2022-23 </p>
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
                          श्रेणी चयन हेतु  आवश्यक निर्देश:- :</p>
                   </td>
             </tr>
             <tr>
             <td style="width:30px" class="style5">&nbsp;&nbsp </td>
                    <td colspan="3" >
                          <p>
                        1. &nbsp;&nbsp   श्रेणी ’अ’-: पंजीकृत गोदाम संचालक जो अपने गोदाम MPWLC को संयुक्त भागीदारी योजना में देने हेतु सहमत होकर अपने गोदामों का संचालन स्वतः करना चाहते हैं, उन्हें भारत सरकार से मिलने वाले भण्डारण शुल्क राशि का अनुपातिक 80%  देय होगा।    
                         </p>
                        <p style="font-weight:600">
                          &nbsp;&nbsp  ’अ’ श्रेणी में गोदाम संचालकों का दायित्व निम्नानुसार होगा -:
                        </p>
                        <p>
                          भण्डारित स्कंध का रख-रखाव :- गोदाम में भण्डारित होने वाले स्कंध के वैज्ञानिक भण्डारण व्यवस्था हेतु आवश्यक सामग्री एवं संसाधन यथा-डनेज शीट, फ्यूमीगेशन कवर, माइश्चर मीटर, सेण्ड स्नेक, अग्निशमन यंत्र, फायर बकेट्स एवं गोदामों/परिसर की साफ-सफाई, सुरक्षा आदि का दायित्व गोदाम संचालक का होगा ।  
                        </p>

                         <p >
                       * &nbsp;&nbsp   भण्डारित स्कंध में कीटोपचार 
                        </p>

                     <%--       <p>
                         फ्यूमीगेशन/कीटोपचार हेतु ऐसे गोदाम संचालक जिनके द्वारा ऑनलाईन ऑफर के दौरान स्वयं अथवा ’’आऊट सोर्सिंग’’ से फ्यूमीगेशन/कीटोपचार कार्य करने का विकल्प चुना है, उन्हें स्वतः फ्यूमीगेशन/कीटोपचार कार्य करने हेतु एल्युमीनियम फास्फाईड, मेलाथियॉन तथा डेल्टामेथरीन की व्यवस्था करनी होगी शेष गोदाम संचालकों  द्वारा निर्धारित दर पर एल्युमीनियम फास्फाईड, मेलाथियॉन तथा डेल्टामेथरीन उपलब्ध कराई जावेगी। दोनों स्थितियों में अनिवार्य रूप से  संबंधित शाखा प्रबंधक/प्रतिनिधि की उपस्थिति में प्रावधानों एवं मापदण्डों के अनुसार फ्यूमीगेशन/कीटोपचार कार्य निष्पादित करते हुए रिकार्ड संधारण करते हुए सुरक्षित रखेगा |
                        </p>--%>

                        
                         <p >
                            * &nbsp;&nbsp   भण्डारण कमी/गेन का दायित्व 
                        </p>

                              <p >
                           * &nbsp;&nbsp   भण्डारित स्कंध का बीमा, गोदाम का बीमा, GST , विद्युत बिल, प्रापर्टी टेक्स आदि जो भी शासकीय/अर्द्धशासकीय टेक्सेस हैं, वे गोदाम संचालक द्वारा समय पर भुगतान किया जावेगा ।  
                        </p>

                                     <p >
                            * &nbsp;&nbsp  भण्डारित स्कंध के जमा फार्म के विरूद्ध निगम द्वारा समय-समय पर निर्धारित प्रावधान/प्रक्रिया अनुसार वेअरहाउस रसीद गोदाम संचालक द्वारा जारी की जावेगी, जो भंडारित स्कंध के डिपाजिट फार्म ’’ऑनलाईन’’ जारी होने के पश्चात त्रुटियुक्त न होने की दशा में गोदाम संचालक द्वारा MPWLC के WHMS माड्यूल में अपनी Login ID से 48 घंटे (कार्य घंटे) की अवधि में ’’ऑनलाईन WHR जारी करनी होगी ।
                        </p>
                    </td>
            </tr>             
             <tr>
             <td style="width:30px" > </td>
                    <td colspan="3">
                          <p>
                          2.&nbsp;&nbsp   श्रेणी ’ब’-:  पंजीकृत गोदाम संचालक जो अपने गोदाम MPWLC को संयुक्त भागीदारी योजना में देने हेतु सहमत होकर अपने गोदामों का संचालन स्वतः नहीं करते हुए MPWLC द्वारा नियुक्त अधिकृत PMS एजेंसी के माध्यम से कराना चाहते हैं, उन्हें भारत सरकार से मिलने वाले भण्डारण शुल्क राशि का अनुपातिक 60% देय होगा।  
                         </p>


                         <p style="font-weight:600">
                         2.1   &nbsp;&nbsp  ’ब’ श्रेणी में PMS  एजेंसी के दायित्व निम्नानुसार होंगे   -:
                        </p>

                             <p>
                         I.    भण्डारित स्कंध का रख-रखाव :- गोदाम में भण्डारित होने वाले स्कंध के वैज्ञानिक भण्डारण व्यवस्था हेतु आवश्यक सामग्री एवं संसाधन यथा-डनेज शीट, फ्यूमीगेशन कवर, माइश्चर मीटर, सेण्ड स्नेक, अग्निशमन यंत्र, फायर बकेट्स एवं गोदामों/परिसर की साफ-सफाई, सुरक्षा आदि का दायित्व च्डै एजेंसी का होगा ।  
                        </p>
                        <p>
                          II.  वेअरहाउस रसीद जारी करना |
                        </p>

                        <p>
                          III.  भण्डारित स्कंध में कीटोपचार 
                        </p>

                           <p >
                            IV.   भण्डारण कमी/गेन का दायित्व 
                        </p>

                        <p>V. उपार्जित गेहूॅ भंडारित होने पर भारत शासन द्वारा निर्धारित गेन अथवा शासन द्वारा निर्धारित गेन की अनिवार्यता का दायित्व एजेंसी को वहन करना होगा।</p>

                            <p>VI. उपार्जित गेहू भंडारित होने पर भारत शासन द्वारा निर्धारित गेन अथवा शासन द्वारा निर्धारित गेन की अनिवार्यता का दायित्व एजेंसी को वहन करना होगा।</p>
                        <p>VII.  उपरोक्त स्कंध के अलावा समस्त दलहन तिलहन एवं अन्य स्कंध आदि भंडारित होने की दशा में भंडारण कमी/आधिक्य के संबंध में शासन द्वारा निर्धारित मापदण्ड च्डै एजेंसी के लिए बंधनकारी होंगे।</p>


                        
                        <p style="font-weight:600">
                          2.2 &nbsp;&nbsp  ’ब’  श्रेणी में गोदाम संचालक के दायित्व निम्नानुसार होंगे  :-</p>
                            <p>
I.  	गोदाम परिसर में सुरक्षा व्यवस्था हेतु एक कर्मी 24 घंटे उपलब्ध कराना होगा ।</p>
                           
<p>II.	स्कंध के जमा/भुगतान हेतु आने वाले ट्रकों/वाहनों के सुगम आवागमन हेतु एप्रोच रोड एवं परिसर की इंटरनल रोड सदैव दुरूस्त रखना होगी ।</p>
<p>III.	गोदामों की बिल्डिंग का बीमा कराते हुए गोदाम परिसर में ट्रांजिट के दौरान होने वाली टूट-फूट को तत्काल दुरूस्त करावेगा ।</p>
<p>IV.	गोदाम से संबंधित समस्त प्रकार के टेक्सेस जो एप्पलीकेबिल होंगे, उनका भुगतान समय पर करना होगा ।</p>


                          <p style="font-weight:600">
                      2.3  &nbsp;&nbsp  श्रेणी ’ब’ संयुक्त भागीदारी योजना की अन्य शर्तें   -:
                        </p>
                        <p> I.  आगामी सीजन में समस्त jvs  गोदाम संचालकों को उपरोक्त श्रेणियों में से किसी एक श्रेणी पर अपनी सहमति व्यक्त करना अनिवार्य होगा ।</p>
                         <p> II. नवीन निर्मित होने वाली भण्डारण क्षमता के लिए आगामी संयुक्त भागीदारी योजना में उपरोक्त श्रेणियों का प्रावधान रखा जावेगा, जिसमें संबंधित गोदाम संचालक तत्समय अपनी गोदाम का JVS में ऑफर दे सकेगा ।</p>
                         <p> III. गोदाम में भण्डारित स्कंध का संयुक्त रूप से रिकार्ड संधारण करना होगा ।</p>


                
                        
                    </td>
            </tr>
        <%--     <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 3.</td>
                    <td colspan="3">
                          <p>
                             PDS की वार्षिक आवश्यकता वाले स्थानों हेतु एक परिसर की न्यूनतम भण्डारण क्षमता 500 मे.टन एवं शेष स्थानों के लिए न्यूनतम भंडारण क्षमता 1000 में.टन होगी । 
                             </p>
                    </td> 
            </tr>
             <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp </td>
                    <td colspan="3">
                          <p>
                             3.1. आंशिक क्षमता के गोदाम नहीं लिए जावेंगे किन्तु ऐसे गोदाम जो पूर्व से ही MPWLC कि सयुक्त भागीदारी योजना मे संचालित है उनकी न्यूनतम रिक्त   
                             </p>
                    </td> 
            </tr>
            <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp </td>
                    <td colspan="3">
                          <p>
                             क्षमता एक पूर्ण स्टेक होने पर JVS मे लिया जावेगा । 
                             </p>
                    </td> 
            </tr>
             <tr>
             <td> &nbsp;&nbsp 4.</td>
                    <td colspan="3">
                          <p>
                            गोदाम तक पहुंचने हेतु न्यूनतम WBM स्तर की बारहमासी सडक(All Weather Approach Road) हों ।<br />
                         </p>
                    </td>
                    
            </tr> 
             <tr>
             <td>&nbsp;&nbsp 5.</td>
                    <td colspan="3">
                          <p>
                            गोदाम में प्रत्येक गेट पर जालीदार शटर/गेट हो । <br />
                         </p>
                    </td>
                    
            </tr>                         
                         <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 6.</td>
                    <td colspan="3">
                          <p>
                             गोदाम निर्माणाधीन नहीं होना चाहिए ।
                    </td> 
            </tr> 
                         <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 7.</td>
                    <td colspan="3">
                          <p>
                             गोदाम विवादग्रस्त यथा गोदाम के मालिकाना हक, देनदारियां एवं माननीय न्यायालयों में प्रकरण लंबित/विचाराधीन नहीं होना चाहिए ।
                    </td> 
            </tr> 
                         <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 8.</td>
                    <td colspan="3">
                          <p>
                             गोदाम क्षतिग्रस्त नहीं होना चाहिए ।
                    </td> 
            </tr> --%>
 <%--                        <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 9.</td>
                    <td colspan="3">
                          <p>
                             गोदाम में शासकीय योजनाओं के अतिरिक्त अन्य स्कंध भण्डारित नहीं होना चाहिए ।
                    </td> 
            </tr> --%>
        <%--                 <tr>
             <td style="vertical-align:top"> &nbsp;&nbsp 10.</td>
                    <td colspan="3">
                          <p>
                             गोदाम 'ब्लेक लिस्टेड' नहीं होना चाहिए ।
                    </td> 
            </tr>    --%>                                             

        <%--     <tr>
             <td> &nbsp;&nbsp 11.</td>
                    <td colspan="3">
                          <p>
                            खरीदी केंद्र स्थापित करने हेतु सहमत / असहमत(असहमति के बावजूद भी जिला प्रसासन द्वारा आवश्यकता अनुसार खरीदी केंद्र स्थापित किया जा सकेगा।) :  &nbsp;
                                        <asp:DropDownList ID="ddlProcCenter" runat="server" Height="25px" Width="100px"> 
                                            <asp:ListItem >--Select--</asp:ListItem>
                                            <asp:ListItem Value="Y">सहमत</asp:ListItem>
                                            <asp:ListItem Value="N">असहमत</asp:ListItem>                                         
                                        </asp:DropDownList>
                            <br />
                         </p>
                    </td>
                    
            </tr> --%>
            
    <%--         <tr>
             <td> &nbsp;&nbsp 12.</td>
                    <td colspan="3">
                          <p>
                            गोदाम संचालक कीटोपचार/धूम्रीकरण हेतु कीटनाशक औषधिया यथा एल्यूमिनियम फास्फाइड, मेलाथियान, डेल्टामेथ्रिन   &nbsp;&nbsp
                                       
                            <br />
                         </p>
                    </td>
                    
            </tr>    --%>   
      <%--       <tr>
             <td></td>
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
                    
            </tr>  --%>                      
           <tr><td colspan="4"></td></tr>
           <tr><td colspan="4"></td></tr>
             <tr> 
             <td></td>
                            <td colspan="3">
                                <p style=" font-size:medium;color:red">
                                    <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox> मेरे द्वारा रबी विपणन वर्ष 2022-23 की संयुक्त भागीदारी योजना के गोदामों का पीएमएस एजेंसी से सुपरवीजन कराने के संबंध मे विस्तृत विवरण एवं सेवा शर्तों को भाली -भाती पड़ लिया गया हैं |
<br />
                                </p>
                                </td>
                                </tr>
            <tr>
                    <td style="height:10px" >
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
                        Warehouse Name :<asp:Label ID="lblWName" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblRegNo" runat="server" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Total Capacity -:<asp:Label ID="Label2" runat="server" Font-Bold="true"></asp:Label></td>
                  
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
                           <%-- <asp:BoundField DataField="G_ScientificCapacity" HeaderText="Capacity (MT)" />                           
                                <asp:TemplateField HeaderText="Full Capacity">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="ckstack" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="chkParCpt" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Offered Capacity (MT)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCapacity" runat="server" Width="100px" Enabled="false">0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Scheme<br>(Rs 78/83)">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlScheme" runat="server"> 
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="78">B(Rs. 78)</asp:ListItem>
                                            <asp:ListItem Value="83">A(Rs. 83)</asp:ListItem>                                         
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
                        <br />
                    <%--    <asp:Button class="button button2" ID="btnCalCpt" runat="server" Text="Calculate Fee" Width="130px" Height="28px"
                            onclick="btnCalCpt_Click" />--%>
                            
                                                   
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
                        <%--    <asp:BoundField DataField="AvlOfferedCpt" HeaderText="Available Capacity (MT)" />                 
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
                                <asp:TemplateField HeaderText="Scheme<br>(Rs 78/83)">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlScheme" runat="server"> 
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="78">B(Rs. 78)</asp:ListItem>
                                            <asp:ListItem Value="83">A(Rs. 83)</asp:ListItem>                                         
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
            
                    </td>            
            </tr>            
            
       <tr>
         
           <asp:HiddenField ID="GodCAp" runat="server" />
            <asp:HiddenField ID="GodId" runat="server" />
           <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="RedioHiddenField" runat="server" />
            <asp:HiddenField ID="Totalcaphdn" runat="server" />

 <%--          <td colspan="4" align="center">  श्रेणी छुने 
           <asp:RadioButtonList ID="RadioButtonList1" runat="server">
               <asp:ListItem Text="A" Value="A"></asp:ListItem>
                <asp:ListItem Text="B" Value="B"></asp:ListItem>
           </asp:RadioButtonList>


           </td>--%>

           </tr>
        <br />   <br />   <br />
                   <tr>
                      <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White" align="Center">
                          <p style="font-size:16px; color: red; font-weight: 700;">
                         श्रेणी का चयन   करे   </p>
                      </td>

                  </tr> 
           <tr>

           <td colspan="4" align="center">
         <%--   <p style="font-weight:600; font-size:20px; color:blue;">  श्रेणी का चयन   करे  </p>--%>  <asp:RadioButton ID="RadioButton1" runat="server"   GroupName="S" />  <asp:Label ID="lvl1"  runat="server" style="font-weight:600; font-size:15px; color:green;">श्रेणी "अ"</asp:Label>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
            <asp:RadioButton ID="RadioButton2" runat="server"   GroupName="S" /> <asp:Label ID="Label1"  runat="server" style="font-weight:600; font-size:15px; color:green;">श्रेणी "ब"</asp:Label>
               </td>
             </tr>

            <tr>
            <td colspan="4" align="center">
            <asp:Button class="button button2"  ID="btnsubmit" runat="server" Text="Submit"  Width="130px" Height="28px"
                  OnClick="btnsubmit_Click"  ></asp:Button>
                    &nbsp&nbsp;&nbsp&nbsp;
                              &nbsp&nbsp;&nbsp&nbsp;
            <asp:Button class="button button2"  ID="btnPrint" runat="server" Text="Print"  
                    Width="130px" Height="28px" Visible="false" OnClick="btnPrint_Click1"></asp:Button>
            </td>
            </tr>  
            <tr>
            <td></td>
            <td style="font-weight:bold ; color:Red; border-color:Red;" colspan="3">
           <p style="color:Blue; font-size:medium; text-decoration: underline;">
                                  Note : ऑफर संबंधी जानकारी को सबमिट करने के बाद उसका प्रिंट अनिवार्य रूप से प्राप्त कर रखे | 
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
