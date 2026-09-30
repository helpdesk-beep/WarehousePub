<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Neeti.aspx.cs" Inherits="TribalGodown_Neeti" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Godown cunstruction Neeti</title>
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
</head>
<body>
    <form id="form1" runat="server">
    <div id="bg">
<div class="wrap">

            <img src="../images/CH.jpg" style="width: 100%" alt="" height="150" />
<%--            <div style="background-color: #66CCFF">
                 <p style="font-size: medium; color: #008080;">आवश्यक निर्देश  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Log out</asp:LinkButton></p>
            </div>--%>
            <table>
                         <tr >
                            <td colspan="4" style="font-size: medium; width: 1000px;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                             
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
                      <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White ; width: 100%;" align="center">
                          <p style="font-size: 14px; color: Black;">
                          आवश्यक निर्देश</p>
                      </td>
                  </tr>                                     
            </table>
            
            <table>
                <tr>
                    <td style=" width:10px;">
                    </td>
                    <td style="font-size: medium; color: #CC0000;">
                    प्रदेश मे वेयरहाउसिंग एंड लॉजिस्टिक्स के अंतर्गत अनुसूचित जनजाति बाहुल्य क्षेत्रों में सार्वजनिक वितरण प्रणाली के अंतर्गत भंडारण क्षमता के निर्माण एवं संचालन हेतु निर्धारित विकासखंडों में 1800 में. टन क्षमता के गोदाम निर्माण हेतु अनुसूचित जनजाति के बेरोजगार उधमियों से ऑनलाइन प्रस्ताव प्राप्त किये जाने हेतु निम्नानुसार नवीन एकजाई निर्देश निर्धारित किए जाते है :-
                    </td>
                    <td style=" width:10px;">
                    </td>
                </tr>
                <tr>
                    <td>1.</td>
                    <td>आवेदन अनुसूचित जनजाति बाहुल्य अनुसूचित क्षेत्रों के अनुसूचित जनजाति के उद्धमियों से ही प्राप्त किए जावेंगे ।</td>
                    <td></td>
                </tr>
                <tr>
                    <td>2.</td>
                    <td>राज्य शासन द्वारा मध्यप्रदेश वेयरहाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन के माध्यम से उद्धमियों को 7 वर्ष की व्यावसायिक गारंटी प्रदान की जावेगी ।</td>
                    <td></td>
                </tr>
                <tr>
                    <td>3.</td>
                    <td>उद्धमियों के आवेदन जिला स्तर पर प्राप्त किए जावेंगे । आवेदन प्राप्त होने के उपरान्त भूमि के उपयोगी एवं आवेदक के वित्तीय प्रबंधन के संबंध मे निम्नलिखित समिति द्वारा आवेदक के चयन के संबंध मे निर्णय लिया जावेगा :-</td>
                    <td></td>
                </tr>                                
                <tr>
                    <td></td>
                    <td>(क) संबन्धित जिले के कलेक्टर &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; - अध्यक्ष</td>

                </tr>
                <tr>
                    <td></td>
                    <td>(ख) जिला आपूर्ति नियंत्रक/अधिकारी &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- सदस्य</td>
                </tr>                                                
                <tr>
                    <td></td>
                    <td>(ग) क्षेत्रीय प्रबन्धक, म॰ प्र॰  वेयरहाउसिंग एंड लॉजिस्टिक्स कार्पोरेशन  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- सचिव सह संयोजक</td>
                </tr>  
                <tr>
                    <td></td>
                    <td>(घ) क्षेत्रीय प्रबन्धक, म॰ प्र॰ स्टेट सिविल सप्लाईस कार्पोरेशन &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- सदस्य</td>
                </tr>
                <tr>
                    <td></td>
                    <td>(ड) जिले का लीड बैंक अधिकारी &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;- सदस्य</td>
                </tr>
                <tr>
                    <td>4.</td>
                    <td>आवेदक को निम्नलिखित प्रपत्र संबंधित क्षेत्रीय कार्यालय मे जमा करना आवश्यक है।</td>
                </tr>
                <tr>
                    <td></td>
                    <td>(अ) अनुसूचित जनजाति होने का जाति प्रमाण पत्र ।</td>
                </tr>
                <tr>
                    <td></td>
                    <td>(ब) रोजगार कार्यालय का पंजीयन क्रमांक ।</td>
                </tr>
                <tr>
                    <td></td>
                    <td>(स) शैक्षणिक योग्यता संबंधी दस्तावेज़ ।</td>
                </tr>
                <tr>
                    <td></td>
                    <td>(द) भूमि का डायवर्सन, नगर एवं ग्राम निवेश अंतर्गत भवन निर्माण संबंधी दस्तावेज़ ।
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>(ई) वित्तीय पोषण संबंधी दस्तावेज़ ।</td>
                </tr> 
                <tr>
                    <td></td>
                    <td>(फ) भूमि स्वामित्व संबंधी दस्तावेज़ ।</td>
                </tr>
                <tr>
                    <td>5.</td>
                    <td>किसी विकासखण्ड मे एक से अधिक आवेदन प्राप्त होने की दशा में समिति द्वारा निम्नानुसार आवेदनो का मूल्यांकन किया जावेगा :-</td>
                </tr>
                <tr>
                    <td></td>
                    <td>(I) (क) आवेदक के पास स्वयं के नाम पर विकासखंड मे न्यूनतम 0.72 एकड भूमि का होना आवश्यक है।</td>
                </tr>
                <tr>
                    <td></td>
                    <td>(ख) आवेदक द्वारा आवेदन के साथ वित्तीय प्रबंधन का विवरण भी प्रस्तुत किया जावेगा।</td>
                </tr> 
                <tr>
                    <td></td>
                    <td>(ग) विकासखंड मे यदि प्रदाय केंद्र स्थित है तो दूरी प्रदाय केंद्र से 10 किलो मीटर के अंदर तथा यदि विकासखंड मे प्रदाय केंद्र स्थापित नहीं है तो विकासखंड के भीतर भूमि प्रदाय केंद्र स्थापना हेतु उपयुक्त स्थान पर हो सकती है ।</td>
                </tr>
                <tr>
                    <td></td>
                    <td>(घ) साक्षात्कार एवं अंको के आधार पर चयन का प्रावधान समाप्त किया है ।</td>
                </tr> 
                <tr>
                    <td></td>
                    <td>(II) समिति द्वारा स्थान की उपयुक्तता एवं प्राथमिकता के बारे मे संबन्धित विकासखंड की आवश्यकता को ध्यान मे रखते हुए निर्णय लिया जावेगा एवं भूमि के चयन के समय यह ध्यान रखा जावेगा की उस स्थान पर भंडारण सामग्री की आवाजाही मे किसी प्रकार की कोई दिक्कत न हो एवं सड़क के समीप हो।</td>
                </tr> 
                <tr>
                    <td>6.</td>
                    <td>भंडारण की दर समय समय पर शासन द्वारा सार्वजनिक वितरण प्रणाली के स्कंध भंडारण हेतु निर्धारित दर अनुसार उद्धमियों को भुगतान किया जावेगा ।</td>
                </tr> 
                <tr>
                    <td>7.</td>
                    <td> निजी उद्धमियों द्वारा प्रस्ताव प्रस्तुत करते समय स्वयं की भूमि का विवरण एवं स्वामित्व संबंधी प्रपत्र प्रस्ताव प्रस्तुत करते समय अथवा 4 माह के अंतर्गत भूमि क्रय संबंधी कार्यवाही पूर्ण करके प्रस्तुत करने हेतु शपथ पत्र प्रस्तुत करना होगा ।</td>
                </tr> 
                <tr>
                    <td>8.</td>
                    <td>इस योजना के अंतर्गत अनुसूचित जनजाति के बेरोजगार उद्धमियों से ही आवेदन प्राप्त किये जायेंगे। अतः इस हेतु जाति प्रमाण पत्र एवं रोजगार कार्यालय में पंजीयन का विवरण भी आवेदन पत्र के साथ प्रस्तुत करना आवश्यक होगा ।</td>
                </tr> 
                <tr>
                    <td>9.</td>
                    <td>आवेदक की न्यूनतम शैक्षणिक योग्यता 12 वी कक्षा उत्तीर्ण होना आवश्यक है।</td>
                </tr> 
                <tr>
                    <td>10.</td>
                    <td>आवेदन प्रस्तुत करने के 4 माह के अंतर्गत भूमि का डायवर्सन, नगर एवं ग्राम निवेश अंतर्गत भवन निर्माण संबंधी समस्त नियमों का पूर्ण पालन करना होगा । चयन के उपरान्त एक माह की अवधि में अनुबंध निष्पादित करना होगा ।</td>
                </tr> 
                <tr>
                    <td>11.</td>
                    <td>अनुबंध निष्पादन के उपरांत एक वर्ष की अवधि में निर्माण कार्य पूर्ण करना आवश्यक होगा । एक वर्ष की अवधि में निर्माण कार्य पूर्ण नहीं होता है तो व्यावसायिक गारंटी मे आनुपातिक कमी की जावेगी, परंतु निर्माण कार्य अधिकतम दो वर्ष की अवधि में पूर्ण करना होगा, अन्यथा व्यावसायिक गारंटी प्रदान नहीं की जावेगी ।</td>
                </tr> 
                <tr>
                    <td>12.</td>
                    <td>आवेदनकर्ता को गोदाम निर्माण हेतु चयन के 4 माह के अंतर्गत समस्त प्रपत्र एवं प्रोजेक्ट रिपोर्ट, गोदाम निर्माण हेतु वित्त पोषण का पूर्ण विवरण प्रस्तुत करना होगा ।</td>
                </tr>
                <tr>
                    <td>13.</td>
                    <td> स्थानों का चयन करते समय इस बात का ध्यान रखा जावे कि गोदाम का स्थान निचली भूमि में स्थित न हो, निर्माण स्थल के ऊपर हाईटेंशन लाइन न हो तथा उस स्थान पर  भंडारण सामग्री कि आवाजाही में किसी प्रकार की कोई दिक्कत न हो, अर्थात सड़क के समीप हो ।</td>
                </tr>
                <tr>
                    <td>14.</td>
                    <td>गोदाम निर्माण हेतु वही समस्त मापदंड अपनाए जावेंगे जो कि गोदाम निर्माण हेतु मध्यप्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स नीति 2012 के तहत निर्धारित किए गए है किन्तु इस योजना के अंतर्गत पूंजी अनुदान या ब्याज अनुदान का प्रावधान नहीं होगा। Specification हेतु
                       <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/TribalGodown/TribalGodownstructure.pdf" Target="_blank" ForeColor="#003399">यहाँ क्लिक करें</asp:HyperLink></td>
                </tr>
                <tr>
                    <td>15.</td>
                    <td>गोदाम निर्माण हेतु भूमि की उपलब्धता एवं गोदाम निर्माण के उपरांत निवेशक के द्वारा निर्माण की स्थिति से मध्यप्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कार्पोरेशन के संबंधित क्षेत्रीय प्रबन्धक को सूचित किया जावेगा । क्षेत्रीय प्रबन्धक सूचना प्राप्त होने के एक सप्ताह के अंदर समिति द्वारा गोदाम का निरीक्षण किया जावेगा । समिति मे निम्नानुसार सदस्य होंगे :- </td>
                </tr>
                <tr>
                    <td></td>
                    <td>1 क्षेत्रीय प्रबन्धक,मध्यप्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कार्पोरेशन । </td>
                </tr> 
                <tr>
                    <td></td>
                    <td>2 सहायक यंत्री/आंचलिक अभियंता, मध्यप्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कार्पोरेशन । </td>
                </tr> 
                <tr>
                    <td></td>
                    <td>3 जिला आपूर्ति अधिकारी/जिला आपूर्ति नियंत्रक ।</td>
                </tr>
                <tr>
                    <td>16</td>
                    <td>गोदाम निर्माण पूर्ण होने के उपरांत वास्तविक स्कंध के भंडारण, रख रखाव और भुगतान आदि की वही प्रक्रिया होगी जो मध्यप्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कार्पोरेशन द्वारा सार्वजनिक वितरण प्रणाली के खाद्यान भंडारण के लिए अपनाई जाती है ।</td>
                </tr>
                <tr>
                    <td>17</td>
                    <td>यह सुनिश्चित किया जायेगा कि योजना अंतर्गत निर्मित गोदामो का औसत वार्षिक उपयोग गारंटी अवधि मे 75% से कम न हो। गोदाम की क्षमता से कम भंडारण की स्थिति मे रिक्त क्षमता के देयकों के सत्यापन के उपरान्त देयकों का भुगतान मध्यप्रदेश वेयरहाउसिंग एंड लॉंजिस्टिक्स कार्पोरेशन द्वारा किया जावेगा, ऐसे व्ययो की प्रतिपूर्ति राज्य शासन द्वारा की जाएगी।</td>
                </tr>
                <tr>
                    <td></td>
                    <td style="font-size: medium; color: #CC0000;">आवेदन भरने के पूर्व यह सुनिश्चित कर लें कि आपके पास scanned पासपोर्ट साइज फोटो(100 KB से कम साइज़ मे), हायर सेकेन्डरी पास सर्टिफिकेट, जाति प्रमाण पत्र एवं रोजगार कार्यालय पंजीयन की scanned कॉपी(400 KB से कम साइज़ मे) <span 
                              lang="en-us">JPG/PNG Format मे उपलब्ध हो।</span><br />
                               </td>
                </tr>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
            </table>
            
<%--            <p align="justify" style="font-size: medium; color: #CC0000;">प्रदेश मे वेयरहाउसिंग एंड लॉजिस्टिक्स के अंतर्गत अनुसूचित जनजाति बाहुल्य क्षेत्रों में सार्वजनिक वितरण प्रणाली के अंतर्गत भंडारण क्षमता के निर्माण एवं संचालन हेतु निर्धारित विकासखंडों में 1800 में. टन क्षमता के गोदाम निर्माण हेतु अनुसूचित जनजाति के बेरोजगार उधमियों से ऑनलाइन प्रस्ताव प्राप्त किये जाने हेतु निम्नानुसार नवीन एकजाई निर्देश निर्धारित किए जाते है :-</p>
          <img src="line.png" />--%>
              <%--<ul style=" list-style-type:none;">
                <li>
                   1. आवेदन अनुसूचित जनजाति बाहुल्य अनुसूचित क्षेत्रों के अनुसूचित जनजाति के उद्धमियों से ही प्राप्त किए जावेंगे ।

                </li>
                <li>
                    <br />
                   2. राज्य शासन द्वारा मध्यप्रदेश वेयरहाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन के माध्यम से उद्धमियों को 7 वर्ष की व्यावसायिक गारंटी प्रदान की जावेगी ।
                </li>
               <li>
                    <br />
                    3. उद्धमियों के आवेदन जिला स्तर पर प्राप्त किए जावेंगे । आवेदन प्राप्त होने के उपरान्त भूमि के उपयोगी एवं आवेदक के वित्तीय प्रबंधन के संबंध मे निम्नलिखित समिति द्वारा आवेदक के चयन के संबंध मे निर्णय लिया जावेगा :- 
                </li>
                <li>
                    <br />
                    (क) संबन्धित जिले के कलेक्टर &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; - अध्यक्ष
                </li>
                 <li>
                    <br />
                  (ख) जिला आपूर्ति नियंत्रक/अधिकारी  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- सदस्य
                </li>
                 <li>
                    <br />
                    (ग) क्षेत्रीय प्रबन्धक, म॰ प्र॰  वेयरहाउसिंग एंड लॉजिस्टिक्स कार्पोरेशन  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- सचिव सह संयोजक
                </li>
                <li>
                    <br />
                    (घ) क्षेत्रीय प्रबन्धक, म॰ प्र॰ स्टेट सिविल सप्लाईस कार्पोरेशन  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- सदस्य                  </li>
                <li>
                    <br />
                   (ड) जिले का लीड बैंक अधिकारी  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;- सदस्य
                 </li>
                  <li>
                    <br />
                    4. आवेदक को निम्नलिखित प्रपत्र संबंधित क्षेत्रीय कार्यालय मे जमा करना आवश्यक है।
                 </li>
                 <li>
                    <br /> 
                           (अ) अनुसूचित जनजाति होने का जाति प्रमाण पत्र ।
                 </li>
                   <li>
                    <br />
                           (ब) रोजगार कार्यालय का पंजीयन क्रमांक ।
                 </li>
                  <li>
                    <br />
                           (स) शैक्षणिक योग्यता संबंधी दस्तावेज़ ।
                 </li>
                  <li>
                    <br />
                           (द) भूमि का डायवर्सन, नगर एवं ग्राम निवेश अंतर्गत भवन निर्माण संबंधी दस्तावेज़ ।
                 </li>
                  <li>
                    <br />
                           (ई) वित्तीय पोषण संबंधी दस्तावेज़ ।
                 </li>
                 <li>
                    <br />
                           (फ) भूमि स्वामित्व संबंधी दस्तावेज़ ।
                 </li>
                <li>
                    <br />
                    5. किसी विकासखण्ड मे एक से अधिक आवेदन प्राप्त होने की दशा में समिति द्वारा निम्नानुसार आवेदनो का मूल्यांकन किया जावेगा :-  
                 </li>
                 <li>
                    <br />
                       (I) (क) आवेदक के पास स्वयं के नाम पर विकासखंड मे न्यूनतम 0.72 एकड भूमि का होना आवश्यक है। 
                </li>
                 <li>
                    <br />
                          (ख) आवेदक द्वारा आवेदन के साथ वित्तीय प्रबंधन का विवरण भी प्रस्तुत किया जावेगा। 
                </li>
                 <li>
                    <br />
                          (ग) विकासखंड मे यदि प्रदाय केंद्र स्थित है तो दूरी प्रदाय केंद्र से <span lang="hi">10</span> किलो मीटर के अंदर तथा यदि विकासखंड मे प्रदाय केंद्र स्थापित नहीं है तो विकासखंड के भीतर भूमि प्रदाय केंद्र स्थापना हेतु उपयुक्त स्थान पर हो सकती है । 
                </li>
                <li>
                    <br />
                          (घ) साक्षात्कार एवं अंको के आधार पर चयन का प्रावधान समाप्त किया है ।
                </li>
                <li>
                    <br />
                       (II) समिति द्वारा स्थान की उपयुक्तता एवं प्राथमिकता के बारे मे संबन्धित विकासखंड की आवश्यकता को ध्यान मे रखते हुए निर्णय लिया जावेगा एवं भूमि के चयन के समय यह ध्यान रखा जावेगा की उस स्थान पर भंडारण सामग्री की आवाजाही मे किसी प्रकार की कोई दिक्कत न हो एवं सड़क के समीप हो।
                </li>
                
                <li>
                    <br />
                   6. भंडारण की दर समय समय पर शासन द्वारा सार्वजनिक वितरण प्रणाली के स्कंध भंडारण हेतु निर्धारित दर अनुसार उद्धमियों को भुगतान किया जावेगा ।
                </li>
                <li>
                    <br />
                    7. निजी उद्धमियों द्वारा प्रस्ताव प्रस्तुत करते समय स्वयं की भूमि का विवरण एवं स्वामित्व संबंधी प्रपत्र प्रस्ताव प्रस्तुत करते समय अथवा 4 माह के अंतर्गत भूमि क्रय संबंधी कार्यवाही पूर्ण करके प्रस्तुत करने हेतु शपथ पत्र प्रस्तुत करना होगा । 

                </li>
                <li>
                    <br />
                   8. इस योजना के अंतर्गत अनुसूचित जनजाति के बेरोजगार उद्धमियों से ही आवेदन प्राप्त किये जायेंगे। अतः इस हेतु जाति प्रमाण पत्र एवं रोजगार कार्यालय में पंजीयन का विवरण भी आवेदन पत्र के साथ प्रस्तुत करना आवश्यक होगा । 
                </li>
                 <li>
                    <br />
                   9. आवेदक की न्यूनतम शैक्षणिक योग्यता 12 वी कक्षा उत्तीर्ण होना आवश्यक है।
                </li>
                <li>
                    <br />
                   10. आवेदन प्रस्तुत करने के 4 माह के अंतर्गत भूमि का डायवर्सन, नगर एवं ग्राम निवेश अंतर्गत भवन निर्माण संबंधी समस्त नियमों का पूर्ण पालन करना होगा । चयन के उपरान्त एक माह की अवधि में अनुबंध निष्पादित करना होगा ।
                </li>
                  <li>
                    <br />
                    11. अनुबंध निष्पादन के उपरांत एक वर्ष की अवधि में निर्माण कार्य पूर्ण करना आवश्यक होगा । एक वर्ष की अवधि में निर्माण कार्य पूर्ण नहीं होता है तो व्यावसायिक गारंटी मे आनुपातिक कमी की जावेगी, परंतु निर्माण कार्य अधिकतम दो वर्ष की अवधि में पूर्ण करना होगा, अन्यथा व्यावसायिक गारंटी प्रदान नहीं की जावेगी । 
                </li>
                   <li>
                    <br />
                     12. आवेदनकर्ता को गोदाम निर्माण हेतु चयन के 4 माह के अंतर्गत समस्त प्रपत्र एवं प्रोजेक्ट रिपोर्ट, गोदाम निर्माण हेतु वित्त पोषण का पूर्ण विवरण प्रस्तुत करना होगा ।
                       </li>
                   <li>
                    <br />
                       13. स्थानों का चयन करते समय इस बात का ध्यान रखा जावे कि गोदाम का स्थान निचली भूमि में स्थित न हो, निर्माण स्थल के ऊपर हाईटेंशन लाइन न हो तथा उस स्थान पर भंडारण सामग्री कि आवाजाही में किसी प्रकार की कोई दिक्कत न हो, अर्थात सड़क के समीप हो ।
                       </li>
                   <li>
                    <br />
                       14. गोदाम निर्माण हेतु वही समस्त मापदंड अपनाए जावेंगे जो कि गोदाम निर्माण हेतु मध्यप्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स नीति 2012 के तहत निर्धारित किए गए है किन्तु इस योजना के अंतर्गत पूंजी अनुदान या ब्याज अनुदान का प्रावधान नहीं होगा। Specification हेतु
                       <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/TribalGodown/TribalGodownstructure.pdf" Target="_blank" ForeColor="#003399">यहाँ क्लिक करें</asp:HyperLink>
                       
                       </li>
                   <li>
                    <br />
                       15. गोदाम निर्माण हेतु भूमि की उपलब्धता एवं गोदाम निर्माण के उपरांत निवेशक के द्वारा निर्माण की स्थिति से मध्यप्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कार्पोरेशन के संबंधित क्षेत्रीय प्रबन्धक को सूचित किया जावेगा ।  क्षेत्रीय प्रबन्धक सूचना प्राप्त होने के एक सप्ताह के अंदर समिति द्वारा गोदाम का निरीक्षण किया जावेगा । समिति मे निम्नानुसार सदस्य होंगे :-
                       <br />
                       1 क्षेत्रीय प्रबन्धक,मध्यप्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कार्पोरेशन।<br />
                       2 सहायक यंत्री/आंचलिक अभियंता, मध्यप्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कार्पोरेशन<br />
                       3 जिला आपूर्ति अधिकारी/जिला आपूर्ति नियंत्रक ।

                       </li>
                  <li>
                      <br />
                     16. गोदाम निर्माण पूर्ण होने के उपरांत वास्तविक स्कंध के भंडारण, रख रखाव और भुगतान आदि की वही प्रक्रिया होगी जो मध्यप्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कार्पोरेशन द्वारा सार्वजनिक वितरण प्रणाली के खाद्यान भंडारण के लिए अपनाई जाती है । 
                  </li>
                 <li>
                      <br />
                     17. यह सुनिश्चित किया जायेगा कि योजना अंतर्गत निर्मित गोदामो का औसत वार्षिक उपयोग गारंटी अवधि मे 75% से कम न हो। गोदाम की क्षमता से कम भंडारण की स्थिति मे रिक्त क्षमता के देयकों के सत्यापन के उपरान्त देयकों का भुगतान मध्यप्रदेश वेयरहाउसिंग एंड लॉंजिस्टिक्स कार्पोरेशन द्वारा किया जावेगा, ऐसे व्ययो की प्रतिपूर्ति राज्य शासन द्वारा की जाएगी।
                  </li>
                  
                  <li>
                      <p style="color: #FF0000"><br/>
                     आवेदन भरने के पूर्व यह सुनिश्चित कर लें कि आपके पास scanned पासपोर्ट साइज फोटो(100 KB से कम साइज़ मे), हायर सेकेन्डरी पास सर्टिफिकेट, जाति प्रमाण पत्र एवं रोजगार कार्यालय पंजीयन की scanned कॉपी(400 KB से कम साइज़ मे) <span 
                              lang="en-us">JPG/PNG/GIF Format मे उपलब्ध हो।</span>  
                          </p>
                  </li>
                   <li>
                      <p style="color: #FF0000">
                     यदि आप किसी कारण से आवेदन फॉर्म अभी नहीं भर पा रहे हों तो दिनाँक 28/05/2016 सायं 5.00 के पूर्व दुबारा से लॉगिन करके आवेदन पूरा कर सकते हैं ।
                           </p>
                  </li>
                  
            </ul>--%>
            <img src="line.png" /
            <p style="color: #009933"> <asp:CheckBox ID="CheckBox1" runat="server" /> मैं सत्यापित करता/करती हूं / I Agree कि मैंने विज्ञापन(नीति 
                <span lang="hi">संबंधी</span>) में दी गई समस्त जानकारियां एवं शर्तें अच्छी तरह पढ़ ली हैं और स्वयं को इसके लिए पात्र समझता<span 
                    lang="hi">/समझती</span> हूँ ।</p> <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           
            <asp:Button ID="Button1" runat="server" CssClass="submit" Text="Submit" Width="100px" OnClick="Button1_Click" /><br />
            <br />
        </div>
        </div>
    </form>
</body>
</html>
