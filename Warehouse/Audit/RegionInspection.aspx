<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegionInspection.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Inspection_RegionInspection" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mpwlc Region Audit</title>
      <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
     <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>

      <style type="text/css">

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


.tb6 {
	border: 3px double #CCCCCC;
	width: 230px;
}

      </style>
   <script type="text/javascript" src="https://www.google.com/jsapi">
   </script>
    <script type="text/javascript">

        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                [google.elements.transliteration.LanguageCode.HINDI],
                transliterationEnabled: true
            };

            // Create an instance on TransliterationControl with the required
            // options.
            var control =
            new google.elements.transliteration.TransliterationControl(options);

            // Enable transliteration in the textbox with id
            // 'transliterateTextarea'.
            control.makeTransliteratable(['txt_AuditerName']);
            
            control.makeTransliteratable(['txt_AuditerPost']);

            control.makeTransliteratable(['txtRMName']);

            control.makeTransliteratable(['txtRMpost']); 

            control.makeTransliteratable(['txtempname']);

            control.makeTransliteratable(['txtemppost']);

            control.makeTransliteratable(['txtAuditerRemark']);

            control.makeTransliteratable(['txtRecentYearsComp']);

            control.makeTransliteratable(['txtBusinessDevelopInfo']);

            control.makeTransliteratable(['txtTruti_Patrak']);
            
        }
        google.setOnLoadCallback(onLoad); 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        
          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
                             
     <div id="bg">
		<div class="wrap">

            <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
            <div style="background-color: #66CCFF">
                 <p style="font-size: medium; color: #008080;">क्षेत्रीय कार्यालय का अंकेक्षण: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome<span 
                         lang="en-us"> to</span>&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;<asp:LinkButton 
                         ID="LinkButton3" runat="server" onclick="LinkButton3_Click" >Password</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton 
                         ID="LinkButton1" runat="server" onclick="LinkButton1_Click" >Log out</asp:LinkButton></p>
            </div>
            <table>
                  <tr>
                    <td>
                        अंकेक्षणकर्ता  अधिकारी का नाम:
                    </td>
                    <td>
                        <input id="txt_AuditerName" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             पद:
                    </td>
                    <td>
                        <input id="txt_AuditerPost" name="rname" runat="server" class="text" type="text" />
                    </td>
                    </tr>
                 <tr>
                    <td>
                        क्षेत्रीय कार्यालय का नाम:
                    </td>
                    <td>
                        <input id="txt_ROName" name="rname" runat="server" class="text" type="text" disabled="disabled" />
                    </td>
                          <td>
                             अंकेक्षण संपादन की दिनांक:
                    </td>
                    <td>
                       
                        <asp:TextBox ID="txtAuditDate" runat="server" class="text"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtAuditDate">
                        </asp:CalendarExtender>
                    </td>
                    </tr>
                   <tr>
                    <td>
                        क्षेत्रीय प्रबंधक का नाम:
                    </td>
                    <td>
                        <input id="txtRMName" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             पद:
                    </td>
                    <td>
                        <input id="txtRMpost" name="rname" runat="server" class="text" type="text" />
                    </td>
                    </tr>
                  <tr>
                    <td>
                        क्षेत्रीय प्रबंधक क्षेत्रीय कार्यालय में कब से पदस्थ है:
                    </td>
                    <td>
                       
                        <asp:TextBox ID="txtRMPostingDate" runat="server" class="text"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtRMPostingDate">
                        </asp:CalendarExtender>
                    </td>
                          <td>
                            
                    </td>
                    <td>
                       
                    </td>
                    </tr>
             
                 <tr>
                    <td>
                        क्षेत्रीय कार्यालय में पदस्थ कर्मचारियों के नाम:
                    </td>
                    <td>
                        <input id="txtempname" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             पद:
                    </td>
                    <td>
                        <input id="txtemppost" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                    </tr>
                    
                    <tr>
                    <td>
                        क्षेत्रीय कार्यालय मे कर्मचारी कब से पदस्थ है :
                    </td>
                    <td>
                         <asp:TextBox ID="txtREP_Date" runat="server" class="text"></asp:TextBox>
                         <%--<input id="txtREP_Date" name="rname" runat="server" class="text" type="text" />--%>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtREP_Date">
                        </asp:CalendarExtender>
                    </td>
                         
                   <td colspan="2" align="right">
                    <asp:Button ID="Button1" runat="server" Text="Add" class="submit" OnClick="Button1_Click" ></asp:Button>
                   </td>
                    </tr>
                    
                <tr>
                    <td></td>
                    <td colspan="3">
                        <p style="color: #CC0000">
                        एक से अधिक कर्मचारियों को जोड़ने के लिए कर्मचारी का नाम व पद लिख कर add बटन कर क्लिक करें।</p>

                    </td>
                </tr>

                 <tr>
                    <td></td>
                    <td colspan="3">
                        <asp:GridView ID="gdstackingdetails" runat="server" AutoGenerateDeleteButton="True"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None"
                                                        OnRowCreated="gdstackingdetails_RowCreated" OnRowDeleting="gdstackingdetails_RowDeleting">
                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>

                    </td>
                </tr>
                <tr>
                    <td colspan="4">

                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        क्षेत्र की भंडारण क्षमता एवं उपयोगिता:</p>
                            </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        स्वनिर्मित क्षमता(मी॰टन) : 
                    </td>
                    <td>
                        <asp:TextBox id="txt_ownCap" name="rname" runat="server" class="text" type="text"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_ownCap"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                          <td>
                             उपयोगिता(%):
                    </td>
                    <td> 
                        <asp:TextBox id="txt_ownuse" name="rname" runat="server" class="text" type="text"></asp:TextBox>
                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_ownuse"
                                        ValidChars="0123456789.%">
                                    </asp:FilteredTextBoxExtender>
                       
                    </td>
                </tr>

                 <tr>
                    <td>
                        केप क्षमता(मी॰टन) : 
                    </td>
                    <td>
                       
                          <asp:TextBox id="txt_capcap" name="rname" runat="server" class="text" type="text"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_capcap"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                          <td>
                             उपयोगिता(%):
                    </td>
                    <td>
                       
                        <asp:TextBox id="txt_capuse" name="rname" runat="server" class="text" type="text"></asp:TextBox>
                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txt_capuse"
                                        ValidChars="0123456789.%">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                </tr>
                  <tr>
                    <td>
                        किराये की क्षमता(मी॰टन) : 
                    </td>
                    <td>
   
                          <asp:TextBox id="txt_hiredcap" name="rname" runat="server" class="text" type="text"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txt_hiredcap"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                          <td>
                             उपयोगिता(%):  
                    </td>
                    <td>
              
                          <asp:TextBox id="txt_hiredUSe" name="rname" runat="server" class="text" type="text"></asp:TextBox>
                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txt_hiredUSe"
                                        ValidChars="0123456789.%">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                </tr>
                 <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        व्यवसाय:</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        क्षेत्रीय कार्यालय की आय का लक्ष्य रु०: 
                    </td>
                    <td>
                
                        <asp:TextBox id="txtTargetIncome" name="rname" runat="server" class="text" type="text"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtTargetIncome"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                          <td>
                             आनुपातिक लक्ष्य(निरीक्षण माह तक) रु०:
                    </td>
                    <td>
                        
                        <asp:TextBox id="txtTargetRatio" name="rname" runat="server" class="text" type="text"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtTargetRatio"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                </tr>
                  <tr>
                    <td>
                        लक्ष्य प्राप्ति रु०: 
                    </td>
                    <td>

                           <asp:TextBox id="txtTargetAchiveR" name="rname" runat="server" class="text" type="text"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtTargetAchiveR"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                          <td>
                       लक्ष्य प्राप्ति का प्रतिशत रु०:
                    </td>
                    <td>
                        
                          <asp:TextBox id="txtTargetAchiveP" name="rname" runat="server" class="text" type="text"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtTargetAchiveP"
                                        ValidChars="0123456789.%">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                </tr>
                        <tr>
                    <td>
                       अंकेक्षणकर्ता की टीप: 
                    </td>
                    <td>
                        <textarea id="txtAuditerRemark" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                       विगत वर्षो की तुलनात्मक स्थति:
                    </td>
                    <td>
                        <textarea id="txtRecentYearsComp" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                </tr>
                                       <tr>
                    <td>
                         व्यवसाय व्रद्धि के लिए किए गए क्षे०प्र० के प्रयास की जानकारी:
                    </td>
                    <td>
                        <textarea id="txtBusinessDevelopInfo" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
             
                </tr>
                 <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        आय-व्यय :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        अंकेक्षण दिनांक तक क्षे०का० के कुल देयको की प्रस्तुति उनकी वसूली से प्राप्त आय विवरण (विगत वर्ष की आय प्रथक से दर्शित हो ): 
                    </td>
                    <td>
                      <textarea id="txtIncomeDetailUptoAuditD" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                             क्षेत्रीय कार्यालय से मासिक पत्रक आदि समय पर भेजे जाते है या नहीं :
                    </td>
                    <td>
                         <textarea id="txtROInfo1" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        भंडारण शुल्क रजिस्टर का परीक्षण कर देयक प्रविष्ट की गयी है या नहीं यह देखा जाये: 
                    </td>
                    <td>
                  <textarea id="txtROInfo2" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                           समस्त जमाकर्ताओ के देयक समय पर नियमित रूप से प्रस्तुत किए जाते है या नहीं भंडारण शुल्क वसूली विलंब के कारण :
                    </td>
                    <td>
                        <textarea id="txtROInfo3" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        चालू वर्ष की कुल अर्जित आय: 
                    </td>
                    <td>
                        <input id="txtAchiveIncomeCurrentY" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                            चालू वर्ष की कुल प्राप्ति: 
                    </td>
                    <td>
                        <input id="txtReceivedIncomeCurrentY" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        पूर्व वर्ष की लंबित आय: 
                    </td>
                    <td>
                        <input id="txtPendingIncomeRecentY" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                            पूर्व वर्ष की प्राप्त आय (अंकेक्षण दिनांक तक देखा जाये): 
                    </td>
                    <td>
                        <input id="txtReceivedIncomeRecentY" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
 <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        टीडीएस॰ एवं अन्य विवरण :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        (1) टीडीएस का नियमानुसार कटौत्रा एवं राशि नियमानुसार जमा की गयी है अथवा नहीं? आयकर विवरणिका को निर्धारित अवधि मे जमा किया गया है अथवा नहीं इसकी जांच की जावे : 
                    </td>
                    <td>
                        <textarea id="txttds1" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                             (2) जमाकर्ताओ द्वारा जो राशि टीडीएस॰ के तहत काटी गयी है वह राशि 26 ए॰एस॰ मे प्रदर्शित हो रही है अथवा नहीं :
                    </td>
                    <td>
                        <textarea id="txttds2" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>
                  <tr>
                    <td>
                        धन राशि के संग्रहण एवं हस्तांतरण नियमानुसार किया गया है अथवा नहीं इसकी जांच की जावे : 
                    </td>
                    <td>
                        <textarea id="txtdr" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                             वेअरहाउस चार्जेज के देयक समय पर प्रस्तुत किए जा रहे है अथवा नहीं ? वसूली की स्थती सही दर्शायी जा रही है अथवा नहीं? :
                    </td>
                    <td>
                        <textarea id="txtwc" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>
                      <tr>
                    <td>
                       कंटेजेन्सी के तहत लेबर चार्जज का भुगतान नियमानुसार बैंक के माध्यम से किया जा रहा है अथवा नहीं?  : 
                    </td>
                    <td>
                        <textarea id="txtct" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                            जे॰व्ही॰एस॰ के तहत लिए गये गोदामो का भुगतान नियमानुसार निरंतर आरटीजीएस के माध्यम से किया जा रहा है अथवा नहीं? :
                    </td>
                    <td>
                        <textarea id="txtjvs1" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>
                      <tr>
                    <td>
                       जे॰व्ही॰एस॰ के अंतर्गत लिए गये गोदामो का अनुबंध नियमानुसार निर्धारित किये  गये है अथवा नहीं? : 
                    </td>
                    <td>
                        <textarea id="txtjvs2" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          
                </tr>
                                                        <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        क्षेत्रीय कार्यालय पर इम्प्रेस्ट व्ययो का विवरण :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td align="right" colspan="2">
                      वित्तीय वर्ष :
                    </td>
                    <td align="left" colspan="2">
                     <asp:DropDownList ID="ddlFinncialYear" runat="server"
           TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
>
                                                            </asp:DropDownList>
                    </td>
                          
                </tr>
    <tr>
                    
                    <td colspan="4" id="GVImoprest" runat="server" visible="true">
                        <asp:GridView ID="gvImprest" runat="server" AutoGenerateColumns="False" ShowFooter="true" Width="50%"
                         EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
                             <asp:TemplateField HeaderText="Month">
            <ItemTemplate>
                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Value="-1">Select</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
           
        </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Balance">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOB" runat="server" Width="40px" Text='<%# Eval("OB") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recupment Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRptAmt" runat="server" Width="40px" Text='<%# Eval("RptAmt") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposit By BM">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtDBRM" runat="server" Width="40px" Text='<%# Eval("DBRM") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transffer from cash book">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTFCB" runat="server" Width="40px" Text='<%# Eval("TFCB") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transfer from cons imprest">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTFCImp" runat="server" Width="40px" Text='<%# Eval("TFCImp") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotal" runat="server" Width="40px" Text='<%# Eval("Total") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Imprest for Pass">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtImpFPass" runat="server" Width="40px" Text='<%# Eval("ImpFPass") %>'>0</asp:TextBox>
                                    </ItemTemplate>
           
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Imprest Pass">
                                    <ItemTemplate>
                                       <asp:TextBox ID="txtImpPass" runat="server" Width="40px" Text='<%# Eval("ImpPass") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Witheld Amount">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtWithAmt" runat="server" Width="40px" Text='<%# Eval("WithAmt") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                       <asp:TemplateField HeaderText="Disalloud Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDisAmt" runat="server" Width="40px" Text='<%# Eval("DisAmt") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Return to BM">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtRetTBM" runat="server" Width="40px" Text='<%# Eval("RetTBM") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Return to cash book">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtRetTCB" runat="server" Width="40px" Text='<%# Eval("RetTCB") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Return to const. imp.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRetTCImp" runat="server" Width="40px" Text='<%# Eval("RetTCImp") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing Balance">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCB" runat="server" Width="40px" Text='<%# Eval("CB") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
             <FooterStyle HorizontalAlign="Right" />
            <FooterTemplate>
             <asp:Button ID="ButtonAdd" runat="server" Text="AddNew" Width="40px" 
                    onclick="ButtonAdd_Click" />
            </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
                        
                        

                    </td>
                </tr>
                 <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        सामान्य लेखा परीक्षण :</p>
                            </div>
                    </td>
                </tr>
                   <tr>
                    <td>
                       केशबुक बैलेन्स :
                    </td>
                    <td>
                   <input id="txtCashbookBalance" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                          इम्प्रेस्ट केश बैलेन्स :
                    </td>
                    <td>
                  <input id="txtImprestCashBalance" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>

                  <tr>
                    <td>
                      1. बैंकों मे संधारित बैंक अनुसार बैंक बैलेन्स की स्थति : 
                    </td>
                    <td>
                        <textarea id="txtBankBalance" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                 
                </tr>
                
                  <tr>
                   <td colspan="4">
                   <p>2. निर्धारित सीमा से क्षे०का० के बैंकों मे अधिक राशि बैलेन्स मे नहीं रहे। </p>
                     <p>3. बैंक स्टेटमेंट लिया जाकर प्राप्त आय,जमा कराये जाने की प्रविष्ट का मिलान किया जाये । </p>
                   </td>
                </tr>
                  
                 <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                       क्षेत्रीय कार्यालय अंतर्गत संचालित निजी वेयरहाउस की स्थिति :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        संख्या : 
                    </td>
                    <td>
                      <input id="txtRNoOfGodown" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             क्षमता :
                    </td>
                    <td>
                      <input id="txtRCapacityOfGodown" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
              
                 <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        आंतरिक अँकेक्षण कराये जाने की जानकारी :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        मुख्यालय स्तर से : 
                    </td>
                    <td>
                      <input id="txtHOLevelAudit" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             क्षेत्रीय कार्यालय स्तर से  :
                    </td>
                    <td>
                      <input id="txtROLevelAudit" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                               <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        त्रुटि पत्रक :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        त्रुटि की जानकारी : 
                    </td>
                    <td colspan="3">
                      <textarea id="txtTruti_Patrak" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:500px;"
                        ></textarea>
                    </td>
                          
                </tr>

                   <tr>
         
                    <td colspan="2" align="right">
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="submit" OnClick="btnsubmit_Click" ></asp:Button>
                    </td>
                     <td colspan="2">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="submit" ></asp:Button>
                    </td>
                </tr>
                
            </table>


            </div>
         </div>
    </form>
</body>
</html>
