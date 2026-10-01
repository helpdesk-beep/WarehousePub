<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchInspectionAudit.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Inspections_Audit_BranchInspectionAudit" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mpwlc Branch Inspection</title>
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
            control.makeTransliteratable(['txt_bm']);
            control.makeTransliteratable(['txt_bm_post']);
            control.makeTransliteratable(['txtempname']);
            control.makeTransliteratable(['txtemppost']);
            control.makeTransliteratable(['Textarea10']);
            control.makeTransliteratable(['Textarea9']);
            control.makeTransliteratable(['Textarea8']);
            control.makeTransliteratable(['Textarea7']);
            control.makeTransliteratable(['Textarea4']); 
            control.makeTransliteratable(['Textarea3']);
            control.makeTransliteratable(['Text33']); 
//            control.makeTransliteratable(['Textarea2']);
            control.makeTransliteratable(['Textarea1']);
            control.makeTransliteratable(['txtCaddress']);
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
                 <p style="font-size: medium; color: #008080;">
                     <asp:LinkButton ID="LinkButton2" Font-Bold="true" Font-Size="Medium"
                         runat="server" onclick="LinkButton2_Click" >Home</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;शाखाओं का अंकेक्षण: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Welcome&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;<a href="#">Change Password</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >Log out</asp:LinkButton></p>
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
                        शाखा का नाम:
                    </td>
                    <td>
                        <input id="txt_BranchName" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             अंकेक्षण संपादन की दिनांक:
                    </td>
                    <td>
                       
                        <asp:TextBox ID="TextBox1" runat="server" class="text"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBox1" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    </tr>
                   <tr>
                    <td>
                        शाखा प्रबंधक का नाम:
                    </td>
                    <td>
                        <input id="txt_bm" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             पद:
                    </td>
                    <td>
                        <input id="txt_bm_post" name="rname" runat="server" class="text" type="text" />
                    </td>
                    </tr>
                  <tr>
                    <td>
                        शाखा प्रबंधक शाखा में कब से पदस्थ है:
                    </td>
                    <td>
                       
                        <asp:TextBox ID="TextBox2" runat="server" class="text"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBox2" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                          <td>
                            
                    </td>
                    <td>
                       
                    </td>
                    </tr>
             
                 <tr>
                    <td>
                        शाखा में पदस्थ कर्मचारियों के नाम:
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
                        कर्मचारी शाखा में कब से पदस्थ है:</td>
                    <td>
                       
                        <asp:TextBox ID="TextBox3" runat="server" class="text"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox3_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBox3" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                          <td>
                              &nbsp;</td>
                    <td>
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
                        शाखा की भंडारण क्षमता एवं उपयोगिता:</p>
                            </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        स्वनिर्मित क्षमता(मी॰टन): 
                    </td>
                    <td>
                        <%-- <input id="txt_ownCap" name="rname" runat="server" class="text" type="text" />--%>
                        <asp:TextBox ID="txt_ownCap" runat="server" class="text"></asp:TextBox>
                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_ownCap"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                          <td>
                             उपयोगिता(मी॰टन):
                    </td>
                    <td>
                        <%--<input id="txt_ownuse" name="rname" runat="server" class="text" type="text" />--%>
                         <asp:TextBox ID="txt_ownuse" runat="server" class="text"></asp:TextBox>
                       <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_ownuse"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                </tr>

                 <tr>
                    <td>
                        केप क्षमता(मी॰टन) : 
                    </td>
                    <td>
                        <%--<input id="txt_capcap" name="rname" runat="server" class="text" type="text" />--%>
                        <asp:TextBox ID="txt_capcap" runat="server" class="text"></asp:TextBox>
                       <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_capcap"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                          <td>
                             उपयोगिता(मी॰टन):
                    </td>
                    <td>
                        <%--<input id="txt_capuse" name="rname" runat="server" class="text" type="text" />--%>
                          <asp:TextBox ID="txt_capuse" runat="server" class="text"></asp:TextBox>
                       <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txt_capuse"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                       
                    </td>
                </tr>
                  <tr>
                    <td>
                        किराये की क्षमता(मी॰टन) : 
                    </td>
                    <td>
                        <%--<input id="txt_hiredcap" name="rname" runat="server" class="text" type="text" />--%>
                         <asp:TextBox ID="txt_hiredcap" runat="server" class="text"></asp:TextBox>
                       <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txt_hiredcap"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                    </td>
                          <td>
                             उपयोगिता(मी॰टन):
                    </td>
                    <td>
                        <%--<input id="txt_hiredUSe" name="rname" runat="server" class="text" type="text" />--%>
                         <asp:TextBox ID="txt_hiredUSe" runat="server" class="text"></asp:TextBox>
                       <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txt_hiredUSe"
                                        ValidChars="0123456789.">
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
                        प्राप्त व्यवसाय: 
                    </td>
                    <td>
                        <input id="txt_ObtnBsns" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             प्राप्त हो सकने वाले व्यवसाय:
                    </td>
                    <td>
                        <input id="txt_ObtnableBsns" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                  <tr>
                    <td>
                        शाखा प्रबन्धक द्वारा व्यवसाय हेतु किए गए <br /> प्रयास का विवरण : 
                    </td>
                    <td>
                        <input id="txt_AttemntfrmBMForBsns" name="rname" runat="server" class="text" type="text" />
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
                        शाखा का लक्ष: 
                    </td>
                    <td>
                        <input id="Text17" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             अंकेक्षण माह तक आनुपातिक लक्ष :
                    </td>
                    <td>
                        <input id="Text18" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        प्राप्त आय: 
                    </td>
                    <td>
                        <input id="Text19" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                            व्यय :
                    </td>
                    <td>
                        <input id="Text20" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        लक्ष प्राप्ति का प्रतिशत: 
                    </td>
                    <td>
                        <input id="Text21" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                            लक्ष से कम/अधिक प्राप्ति का विवरण :
                    </td>
                    <td>
                        <input id="Text22" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                 
                <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        हस्थरेखा रोकड़ (अंकेक्षण दिनांक को) :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        केश बुक बैलेन्स: 
                    </td>
                    <td>
                        <input id="txtCBB" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             इम्प्रेस्ट केश बुक :
                    </td>
                    <td>
                        <input id="txtICB" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        निर्माण इम्प्रेस्ट केश बुक: 
                    </td>
                    <td>
                        <input id="txtNICB" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                            स्पेशल रिपेयर्स कार्य संबंधी क्षे.का.:
                    </td>
                    <td>
                        <input id="txtSRK" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        पोस्टेज स्टेम्प : 
                    </td>
                    <td>
                        <input id="txtPS" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                            रेवेन्यू स्टेम्प :
                    </td>
                    <td>
                        <input id="txtRS" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                             <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        बैंक रोकड़ :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        अंकेक्षण दिनांक को बैंक मे जमा राशि का बैंक स्टेटमेंट ले-राशि विवरण: 
                    </td>
                    <td>
                        <input id="txtBST" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             प्राप्त भंडारण शुल्क की राशि समय पर जमा की गयी/या नहीं :
                    </td>
                    <td>
                        <input id="txtDS" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        भंडारण शुल्क का प्रस्तुत देयकों से मिलान किया जाये इसका विवरण दे।: 
                    </td>
                    <td>
                        <input id="txtBSPD" name="rname" runat="server" class="text" type="text" />
                    </td>
                      
                </tr>  
                <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        भंडारण शुल्क :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        अंकेक्षण दिनांक तक (नियमानुसार माह के अंत के भंडारण शुल्क क देयक प्रस्तुत किए गए या नहीं): 
                    </td>
                    <td>
                        <input id="txtBS1" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             पूर्व के देयक नहीं बनाए गए हो उनका विवरण :
                    </td>
                    <td>
                        <input id="txtPD" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        चालू वर्ष के प्रस्तुत देयकों से प्राप्त भंडारण शुल्क राशि एवं विवरण: 
                    </td>
                    <td>
                        <input id="txtCYDP" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                            पूर्व वर्ष की लंबित भंडारण शुल्क की राशि एवं विवरण:
                    </td>
                    <td>
                        <input id="txtLB" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        पूर्व वर्ष के देयकों की लंबित राशि विवरण सहित : 
                    </td>
                    <td>
                        <input id="txtPVKD" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                            राशि लंबित रहने के कारण :
                    </td>
                    <td>
                        <input id="txtRLKR" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr> 
                <tr>
                    <td>
                        जमाकर्ता द्वारा रोकी गई/काटी गई<br /> राशि का विवरण: 
                    </td>
                    <td>
                        <input id="Text23" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                           ओवर एण्ड अवव के देयकों का विवरण :
                    </td>
                    <td>
                        <input id="Text24" name="rname" runat="server" class="text" type="text" />
                       
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
                        शाखा पर इम्प्रेस्ट व्ययो का विवरण :</p>
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
                        भंडारित स्कन्ध :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        भंडारित स्कन्ध की गोदामबार,<br />बस्तुवार जानकारी: 
                    </td>
                    <td>
                        <textarea id="Textarea10" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                              किसी गोडाउन का टेस्ट किया जाए।<br />स्टेकिंग,रख-रखाव एवं वेज्ञानिक भंडारण पर<br /> टीप :
                    </td>
                    <td>
                        <textarea id="Textarea9" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>

                  <tr>
                    <td>
                        स्कन्ध में दर्शित कमी/अधिकता की <br />जानकारी : 
                    </td>
                    <td>
                        <textarea id="Textarea8" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                           स्पेलेज एवं अधिक समय से भंडारित स्कन्ध<br />   के विषय में टीप।अधिक समय से जमा<br /> स्कन्ध उठाने के लिए शाखा प्रबन्धक <br /> द्वारा की गई कार्यवाही का विवरण :
                    </td>
                    <td>
                        <textarea id="Textarea7" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>

                 <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        लंबित भुगतान :</p>
                            </div>
                    </td>
                </tr>
                   <tr>
                    <td>
                       विभिन्न मदों जैसे गोदाम किराया,वेतन, <br />चिकित्सा,यात्रा,बोनस देयक,बिजली,पानी<br />दूरभाषा,लीज़रेंट,संपत्तिकर आदि के लंबित <br /> देयकों के भुगतान का विवरण: 
                    </td>
                    <td>
                       <textarea id="Textarea6" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                           भूमि लीजरेंट, प्रीमियम,संपत्तिकर  <br /> निर्धारित हो चुके हैं या नहीं संबंधी  <br />जानकारी शाखा पर कितना संपत्तिकर, <br />लीज़रेंट प्रस्तावित है तदसंबन्धित जानकारी :
                    </td>
                    <td>
                       <textarea id="Textarea5" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>

                  <tr>
                    <td>
                       कर्मचारियों/अन्य को दिये गये सभी तरह<br /> के अग्रिमों का विवरण:
                    </td>
                    <td>
                       <textarea id="Textarea11" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                          लंबित दावों का विवरण:
                    </td>
                    <td>
                       <textarea id="Textarea12" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>
                  <tr>
                    <td>
                       स्टाक पंजी अनुसार शाखा में स्टाक का सत्यापन हो आइटमवार<br /> मटेरियल जैसे-धूम्रीकरण सामग्री,डनेज,पलीथिन कवर,दवाइयाँ,<br />नमी मापक यंत्र आदि का मिलान एवं विवरण लिया जाये।  
                    </td>
                    <td>
                       <textarea id="Textarea13" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                          किराये की गोदाम की गुणवत्ता,औचित्य,किराया सक्षम स्वीकृति <br /> विषयक टीप:
                    </td>
                    <td>
                       <textarea id="Textarea14" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>
              
                   <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        शाखा में संचालित निजी वेयरहाउसों की जानकारी :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                      (अ)संख्या
                    </td>
                    <td>
                     <input id="Text1" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                        (ब)क्षमता
                    </td>
                    <td>
                    <input id="Text2" name="rname" runat="server" class="text" type="text" />
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                      सुरक्षा उपाय उपकरण-अग्निशमन की जानकारी:-
                    </td>
                    <td>
                       <textarea id="Textarea17" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                       
                    </td>
                    <td>
                      
                       
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        निर्माण संबंधी :</p>
                            </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                      (अ) स्वनिर्मित गोदामों की संख्या
                    </td>
                    <td>
                    <input id="Text3" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                        (ब) वार्षिक रख-रखाव
                    </td>
                    <td>
                    <input id="Text4" name="rname" runat="server" class="text" type="text" />
                    </td>
                </tr>
                 <tr>
                    <td>
                      (स) विशेष रख-रखाव
                    </td>
                    <td>
                       <textarea id="Textarea20" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                         (द) गोदामों की छत,फर्श,फेंसिंग,बाउंड्रीवाल की समस्याओं की<br /> जानकारी:
                    </td>
                    <td>
                       <textarea id="Textarea21" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>
                  <tr>
                    <td>
                       चल रहे न्यायालीन विभागीय जांच,जमकर्ता  <br />द्वारा प्रस्तुत दावों का निराकरन,लंबित<br />रहने संबन्धित अधतन स्थिति,वेब्रिज <br /> संबंधी विषयक जानकारी: 
                    </td>
                    <td>
                        <textarea id="Textarea4" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                       शाखाओं में संधारित रेकॉर्ड देखा जाए<br />सही संधारित हो रहा है या नहीं: 
                    </td>
                    <td>
                       <textarea id="Textarea3" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>
                
                  <tr>
                    <td>
                        शाखा में पदस्थ स्टाफ का उपयुक्तता<br />
                        कमी अधिकता के विषय में टीप: 
                    </td>
                    <td>
                        <%--<input id="Text33" name="rname" runat="server" class="text" type="text" />--%>
                        <textarea id="Text33" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                              <p align="justify">
                         क्षे॰प्र॰ द्वारा किए गए निरीक्षण टीप उनके<br /> निर्देशों के अनुपालन उल्लेख अंकेक्षण <br />प्रतिवेदन मे सम्मिलित किए जाए।<br /> निरीक्षण शाखा द्वारा सामयिक निरीक्षण<br /> प्रतिवेदन की अनुपालन कार्यवाही पर टीप:
                    </p>
                                  </td>
                    <td>
                        <textarea id="TextaT" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>
                   <tr>
                    <td>
                      जमाकर्ता को वेयरहाउस रसीद के<br />विरुद्ध बेंक द्वारा स्वीक्र्त ऋण पर <br /> प्राप्त कमीशन की राशि रुपये: 
                    </td>
                    <td>
                        <input id="Text35" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             
                        अन्य टीप:
                    </td>
                    <td>
                        <textarea id="Textarea1" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                       
                    </td>
                </tr>
                  <tr>
                    <td>
                      त्रुटि पत्रक: 
                    </td>
                    <td>
                        <textarea id="txtCaddress" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                          <td>
                            
                    </td>
                    <td>
                        
                       
                    </td>
                </tr>
                 <tr>
                    <td colspan="4">
                        <div style="background-color: #66CCFF">
                        <p style="color: #008080; font-size: small">
                        भंडारग्रह रसीद की जानकारी :</p>
                            </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        वेयरहाउस रसीद नंबर:
                    </td>
                    <td>
                         <input id="txtwhrnum" name="rname" runat="server" class="text" type="text" />
                    </td>
                    <td>
                       बैंक/निजी व्यक्ति जिसके पास रहन रखी गई है 
                    </td>
                    <td>
                        <input id="txtrahanbank" name="rname" runat="server" class="text" type="text" />
                    </td>
                </tr>
                <tr>
                     <td>
                        रहन की तिथि:
                    </td>
                    <td>
                        <asp:TextBox ID="txtrahandate" runat="server" class="text"></asp:TextBox>
                        <asp:CalendarExtender ID="txtrahandate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtrahandate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="Add" class="submit" OnClick="Button2_Click" ></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="3">वेयरहाउस रसीदों को जोड़ने के लिए वेयरहाउस रसीद नंबर,बैंक/व्यक्ति का नाम व रहन तिथि लिख कर add बटन कर क्लिक करें।</td>
                </tr>
                <tr>
                    <td>

                    </td>
                    <td colspan="3">
                        <asp:GridView ID="gdwrdtl" runat="server" AutoGenerateDeleteButton="True"
                                                        CellPadding="4" ForeColor="#333333" GridLines="None"
                                                        OnRowCreated="gdwrdtl_RowCreated" OnRowDeleting="gdwrdtl_RowDeleting">
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
                    <td>
                    
                    </td>
                    <td>
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="submit" OnClick="btnsubmit_Click" ></asp:Button>
                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    &nbsp;
                       <%-- <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Inspection/OldBranchInsp.aspx">Old Audits</asp:LinkButton--%>
                    </td>
                </tr>
                
            </table>


            </div>
         </div>
    </form>
</body>
</html>
