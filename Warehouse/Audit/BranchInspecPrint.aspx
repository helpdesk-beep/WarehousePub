<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchInspecPrint.aspx.cs" Inherits="Inspection_BranchInspecPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Branch audit</title>
      <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
     <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

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


.tb6 {
	border: 3px double #CCCCCC;
	width: 230px;
}

.collapse {
	border-collapse: collapse;
	border: 1px solid gray;
}

@page {
    size: A4;
    margin: 0;
    font-size:smaller;
}
@media print {
    .page {
        margin: 0;
        border: initial;
        border-radius: initial;
        width: initial;
        min-height: initial;
        box-shadow: initial;
        background: initial;
        page-break-after: always;
        font-size:smaller;
    }
}
@media print {
  html, body {
    width: 210mm;
    height: 297mm;
    font-size:smaller;
  }
  /* ... the rest of the rules ... */
}
/*td{font-size:smaller;}*/
page[size="A4"] {
  background: white;
  width: 21cm;
  height: 29.7cm;
  display: block;
  margin: 0 auto;
  margin-bottom: 0.5cm;
  box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
}
@media print {
  body, page[size="A4"] {
    margin: 0;
    box-shadow: 0;
    font-size:smaller;
  }
}

td
{
	font-size:15px;
}
      </style>




</head>
<body>
      <script type="text/javascript">
          function PrintDiv() {
              var divContents = document.getElementById("ReportDiv").innerHTML;
              var printWindow = window.open('', '', 'height=200,width=400');
              //   printWindow.document.write('<html><head><title>WHR</title>');
              printWindow.document.write('</head><body >');
              printWindow.document.write(divContents);
              printWindow.document.write('</body></html>');
              printWindow.document.close();
              printWindow.print();
              printWindow.close();

              window.location = "GodownInspection.aspx";
          }
    </script>

    <form id="form1" runat="server">
        <center>
            <div style="background-color:aqua">
            <asp:LinkButton ID="LinkButton2" Font-Bold="true" Font-Size="Medium"
                         runat="server" onclick="LinkButton2_Click" >Home</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input id="Button1" name="Print" type="button" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;" value="Print"  onclick="PrintDiv();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Font-Bold="True" Font-Underline="True" ForeColor="#CC0000">Log Out</asp:LinkButton>
        </div>
                 </center>
            <div id="ReportDiv">
    <div id="bg">
		<div class="wrap">
            <table width="100%">
                <tr>

                    <td colspan="2" align="center">
                        मध्यप्रदेश वेयरहाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन-मुख्यालय भोपाल<br />शाखाओं का अंकेक्षण प्रतिवेदन
                    </td>
                </tr>
                <tr>
                    <td>
                        1.अंकेक्षणकर्ता अधिकारी का नाम/पद
                    </td>
                    <td>
                        <asp:Label ID="lblauditornamepost" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                  <tr>
                    <td>
                        2.शाखा का नाम/अंकेक्षण संपादन की दिनांक
                    </td>
                    <td>
                        <asp:Label ID="lblbranchauditdate" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        3.शाखा प्रबन्धक का नाम/पद
                    </td>
                    <td>
                        <asp:Label ID="lblbmpost" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        4.शाखा प्रबन्धक शाखा मे कब से पदस्थ है
                    </td>
                    <td>
                        <asp:Label ID="lblpostingdate" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        5.शाखा में पदस्थ कर्मचारियों के नाम/पद
                    </td>
                    <td>
                        
                        <asp:GridView ID="gvempdtl" runat="server"  AutoGenerateColumns="False" EnableModelValidation="True">
                            <Columns>
                                <asp:BoundField DataField="EmpName" HeaderText="Name" />
                                <asp:BoundField DataField="EmpPost" HeaderText="Post" />
                                <asp:BoundField DataField="EMPPostingdate" HeaderText="Period From" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        6.शाखा की भंडारण क्षमता एवं उपयोगिता:-
                    </td>
                    <td>
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        (अ).स्वनिर्मित क्षमता:<asp:Label ID="lblowncap" runat="server" Text="Label"></asp:Label>मी॰टन
                    </td>
                    <td>
                        उपयोगिता:<asp:Label ID="lblownuse" runat="server" Text="Label"></asp:Label>मी॰टन/प्रतिशत:<asp:Label ID="lblownper" runat="server" Text="Label"></asp:Label></td>
                </tr>
                  <tr>
                    <td>
                        केप क्षमता:<asp:Label ID="lblcapcap" runat="server" Text="Label"></asp:Label>मी॰टन
                    </td>
                    <td>
                        उपयोगिता:<asp:Label ID="lblcapuse" runat="server" Text="Label"></asp:Label>मी॰टन/प्रतिशत:<asp:Label ID="lblcapper" runat="server" Text="Label"></asp:Label></td>
                </tr>
                  <tr>
                    <td>
                        (ब).किराये की क्षमता:<asp:Label ID="lblhiredcap" runat="server" Text="Label"></asp:Label>मी॰टन
                    </td>
                    <td>
                        उपयोगिता:<asp:Label ID="lblhireduse" runat="server" Text="Label"></asp:Label>मी॰टन/प्रतिशत:<asp:Label ID="lblhiredper" runat="server" Text="Label"></asp:Label></td>
                </tr>
                  <tr>
                    <td>
                        <br />
                        7.कुल स्थिति:-<asp:Label ID="lbltotalcap" runat="server" Text="Label"></asp:Label></td>
                    <td>
                        <br />
                       उपयोगिता:<asp:Label ID="lblbtotaluses" runat="server" Text="Label"></asp:Label>मी॰टन/प्रतिशत:<asp:Label ID="lbltotalper" runat="server" Text="Label"></asp:Label></td>
                </tr>
                  <tr>
                    <td>
                        <br />
                        8.व्यवसाय
                    </td>
                    <td>
                      
                    </td>
                </tr>

                <tr>
                    <td>
                        (1)प्राप्त व्यवसाय
                    </td>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="lblfoundbsn"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        (2)प्राप्त हो सक्ने वाले व्यवसाय की जानकारी
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server"></asp:Label>
                    </td>
                </tr>
                   <tr>
                    <td>
                        (3)शाखा प्रबन्धक द्वारा व्यवसाय हेतु किए गए  प्रयास का विवरण
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        9.आय-व्यय
                    </td>
                    <td>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        (1)शाखा का लक्ष्य
                    </td>
                    <td>
                        <asp:Label ID="Label20" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        (2)अंकेक्षण माह तक आनुपातिक लक्ष्य
                    </td>
                    <td>
                        <asp:Label ID="Label21" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        (3)प्राप्त आय
                    </td>
                    <td>
                        <asp:Label ID="Label22" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        (4)व्यय
                    </td>
                    <td>
                        <asp:Label ID="Label23" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        (5)लक्ष्य प्राप्ति का प्रतिशत
                    </td>
                    <td>
                        <asp:Label ID="Label24" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        (6)लक्ष्य से कम/अधिक प्राप्ति का विवरण
                    </td>
                    <td>
                        <asp:Label ID="Label25" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <br />
                       अंकेक्षणकर्ता अधिकारी विगत वर्ष की आय एवं अंकेक्षित वर्ष के आय-व्यय का विवरण भी दें
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <br />
                      10. हस्थरेखा रोकड़ (अंकेक्षण दिनांक को)
                    </td>
                </tr>
                <tr>
                    <td>
                        (1) केश बुक बैलेन्स
                    </td>
                    <td>
                        <asp:Label ID="lblCBB" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        (2) इम्प्रेस्ट केश बुक
                    </td>
                    <td>
                        <asp:Label ID="lblICB" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        (3) निर्माण इम्प्रेस्ट केश बुक
                    </td>
                    <td>
                        <asp:Label ID="lblNICB" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        (4) स्पेशल रिपेयर्स कार्य संबंधी क्षे.का.से प्रेषित राशि का शाखा मे लेखा संधारण से परीक्षण
                    </td>
                    <td>
                        <asp:Label ID="lblSRK" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        (5) पोस्टेज स्टेम्प
                    </td>
                    <td>
                        <asp:Label ID="lblPS" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        (6) रेवेन्यू स्टेम्प
                    </td>
                    <td>
                        <asp:Label ID="lblRS" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <br />
                               इम्प्रेस्ट केश का इम्पेस्ट एडवांस से मिलान कर रोके गए/अमान्य व्हाउचर की राशि
तुरंत इम्पेस्ट मे जमा करायी जाये।
स्पेशल रेपेयर वर्क के लिए भेजी गयी राशि का लेखा संधारण, निर्देशित नियमो के   अनूशार व्यय देयकों का परीक्षण तथा शाखा मद मे लंबित राशि के विषय मे टीप।
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <br />
                       11. बैंक रोकड़
                    </td>
                </tr>
                 <tr>
                    <td>
                               (1) अंकेक्षण दिनांक को बैंक मे जमा राशि का बैंक स्टेटमेंट ले-राशि विवरण
                    </td>
                    <td>
                        <asp:Label ID="lblBST" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                               (2) प्राप्त भंडारण शुल्क की राशि समय पर जमा की गयी/या नहीं
                    </td>
                    <td>
                        <asp:Label ID="lblDS" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                               (3) भंडारण शुल्क का प्रस्तुत देयकों से मिलान किया जाये इसका विवरण दे।
                    </td>
                    <td>
                        <asp:Label ID="lblBSPD" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <br />
                               बैंक से बैंक स्टेटमेंट लिया जावे, बैंक मे जमा राशि, क्षे.का./मुख्यालय मे हस्तांतरित
राशि से प्राप्त आय का मिलान किया जाये उस पर टीप दी जावे।

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                       12. भंडारण शुल्क:
                    </td>
                </tr>
                <tr>
                    <td>
                               (1) अंकेक्षण दिनांक तक (नियमानुसार माह के अंत के भंडारण शुल्क क देयक प्रस्तुत किए गए या नहीं)
                    </td>
                    <td>
                        <asp:Label ID="lblBS1" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                               (2) पूर्व के देयक नहीं बनाए गए हो उनका विवरण
                    </td>
                    <td>
                        <asp:Label ID="lblPD" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                               (3) चालू वर्ष के प्रस्तुत देयकों से प्राप्त भंडारण शुल्क राशि एवं विवरण
                    </td>
                    <td>
                        <asp:Label ID="lblCYDP" runat="server"></asp:Label>
                    </td>
                </tr>
                  <tr>
                    <td>
                              (4) पूर्व वर्ष की लंबित भंडारण शुल्क की राशि एवं विवरण
                    </td>
                    <td>
                        <asp:Label ID="lblLB" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                              (5) पूर्व वर्ष के देयकों की लंबित राशि विवरण सहित
                    </td>
                    <td>
                        <asp:Label ID="lblPVKD" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                              (6) राशि लंबित रहने के कारण
                    </td>
                    <td>
                        <asp:Label ID="lblRLKR" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        (7)जमाकर्ताओं द्वारा रोकी गयी/काटी गयी राशि  का विवरण एवं कारण
                    </td>
                    <td>
                        <asp:Label ID="Label26" runat="server"></asp:Label>
                        </td>
                        </tr>
                        
                       <tr>
                       <td>
            (8)ओवर एण्ड एवव के देयकों के प्रस्तुति विवरण  तैयार कराया जाये राशि एवं विवरण
                   </td> 
                    <td>
                        <asp:Label ID="Label27" runat="server"></asp:Label>
                    </td>
                </tr>

               
                                <tr>
                    <td colspan="2">
                        <br />
                        <span lang="en-us"></span>13. टीडीएस. का विवरण:-
                    </td>
                </tr>
               
                           <tr>
                    <td>
                        <br />
                        (1) टीडीएस का नियमानुसार कटौत्रा एवं राशि नियमानुसार जमा की गयी है अथवा नहीं? आयकर विवरणिका को निर्धारित अवधि मे जमा किया गया है अथवा नहीं इसकी जांच की जावे।  
                    </td>
                    <td>
                        <asp:Label ID="tds1" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        (2) जमाकर्ताओ द्वारा जो राशि टीडीएस॰ के तहत काटी गयी है वह राशि 26 ए॰एस॰ मे प्रदर्शित हो रही है अथवा नहीं?  
                    </td>
                    <td>
                        <asp:Label ID="tds2" runat="server"></asp:Label>
                    </td>
                </tr>
                       <tr>
                    <td>
                        <br />
                        14॰  धन राशि के संग्रहण एवं हस्तांतरण नियमानुसार किया गया है अथवा नहीं इसकी जांच की जावे।  
                    </td>
                    <td>
                        <asp:Label ID="dr1" runat="server"></asp:Label>
                    </td>
                </tr>
               <%-- <tr>
                    <td colspan="2">
                        <br />
                        <span lang="en-us"></span>13. शाखा पर इम्प्रेस्ट व्ययो का विवरण :-
                    </td>
                </tr>--%>
                   <tr>
                    <td>
                        <br />
                        15॰  वेअरहाउस चार्जेज के देयक समय पर प्रस्तुत किए जा रहे है अथवा नहीं ? वसूली की स्थती सही दर्शायी जा रही है अथवा नहीं?  
                    </td>
                    <td>
                        <asp:Label ID="WC" runat="server"></asp:Label>
                    </td>
                </tr>
                   <tr>
                    <td>
                        <br />
                        16॰ कंटेजेन्सी के तहत लेबर चार्जज का भुगतान नियमानुसार बैंक के माध्यम से किया जा रहा है अथवा नहीं?  
                    </td>
                    <td>
                        <asp:Label ID="lc" runat="server"></asp:Label>
                    </td>
                </tr>
                  <tr>
                    <td>
                        <br />
                        17॰  जे॰व्ही॰एस॰ के तहत लिए गये गोदामो का भुगतान नियमानुसार निरंतर आरटीजीएस के माध्यम से किया जा रहा है अथवा नहीं?
                    </td>
                    <td>
                        <asp:Label ID="jvs1" runat="server"></asp:Label>
                    </td>
                </tr>
                   <tr>
                    <td>
                        <br />
                        18॰  जे॰व्ही॰एस॰ के अंतर्गत लिए गये गोदामो का अनुबंध नियमानुसार निर्धारित किये  गये है अथवा नहीं?
                    </td>
                    <td>
                        <asp:Label ID="jvs2" runat="server"></asp:Label>
                    </td>
                </tr>
                </table>
                <div style="page-break-after:always"></div>
  <div>&nbsp;
  <table>
                  <tr>
                    <td colspan="2">
                        <br />
                        <span lang="en-us"></span>19. शाखा पर इम्प्रेस्ट व्ययो का विवरण :-
                    </td>
                </tr>
                <tr>
                    <td style=" font-size:large;" align="center" colspan="4">
                       IMPREST RECUPMENT SHEET- <asp:Label ID="lblBnch" runat="server"></asp:Label>&nbsp;&nbsp;YEAR: <asp:Label ID="lblFy" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvImprest" runat="server" Width="100%" 
                            AutoGenerateColumns="False" EnableModelValidation="True" ShowFooter="true"
                            onrowdatabound="gvImprest_RowDataBound">
                            <Columns>
                               <%-- <asp:BoundField HeaderText="Month" DataField="Month" ItemStyle-Width="20" />--%>
                                <asp:TemplateField HeaderText="Month">
                <FooterTemplate>
                    <asp:Label ID="Month" runat="server" Text="Total"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("Month") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                               <%-- <asp:BoundField  HeaderText="Opening Balance" DataField="OpeningBalance"/>--%>
                    <asp:TemplateField HeaderText="Opening Balance">
                <FooterTemplate>
                    <asp:Label ID="OpeningBalance" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblOpeningBalance" runat="server" Text='<%# Eval("OpeningBalance") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Recupment Amount" DataField="RecupmentAmount" />--%>
                                <asp:TemplateField HeaderText="Recupment Amount">
                <FooterTemplate>
                    <asp:Label ID="RecupmentAmount" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblRecupmentAmount" runat="server" Text='<%# Eval("RecupmentAmount") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Deposit By BM" DataField="DepositByBM"/>--%>
                                <asp:TemplateField HeaderText="Deposit By BM">
                <FooterTemplate>
                    <asp:Label ID="DepositByBM" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblDepositByBM" runat="server" Text='<%# Eval("DepositByBM") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Transffer from cash book" DataField="TFCashBook"/>--%>
                                <asp:TemplateField HeaderText="Transffer from cash book">
                <FooterTemplate>
                    <asp:Label ID="TFCashBook" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblTFCashBook" runat="server" Text='<%# Eval("TFCashBook") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                              <%--  <asp:BoundField HeaderText="Transfer from cons imprest" DataField="TFConstIimprest"/>--%>
                              <asp:TemplateField HeaderText="Transfer from cons imprest">
                <FooterTemplate>
                    <asp:Label ID="TFConstIimprest" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblTFConstIimprest" runat="server" Text='<%# Eval("TFConstIimprest") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Total" DataField="Total"/>--%>
                                <asp:TemplateField HeaderText="Total">
                <FooterTemplate>
                    <asp:Label ID="Total" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                               <%-- <asp:BoundField HeaderText="Imprest for Pass" DataField="ImprestForPass"/>--%>
                               <asp:TemplateField HeaderText="Imprest for Pass">
                <FooterTemplate>
                    <asp:Label ID="ImprestForPass" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblImprestForPass" runat="server" Text='<%# Eval("ImprestForPass") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Imprest Pass" DataField="ImprestPass"/>--%>
                                <asp:TemplateField HeaderText="Imprest Pass">
                <FooterTemplate>
                    <asp:Label ID="ImprestPass" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblImprestPass" runat="server" Text='<%# Eval("ImprestPass") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Witheld Amount" DataField="WitheldAmount"/>--%>
                                <asp:TemplateField HeaderText="Witheld Amount">
                <FooterTemplate>
                    <asp:Label ID="WitheldAmount" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblWitheldAmount" runat="server" Text='<%# Eval("WitheldAmount") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Disalloud Amount" DataField="DisalloudAmount"/>--%>
                                <asp:TemplateField HeaderText="Disalloud Amount">
                <FooterTemplate>
                    <asp:Label ID="DisalloudAmount" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblDisalloudAmount" runat="server" Text='<%# Eval("DisalloudAmount") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                               <%-- <asp:BoundField HeaderText="Return to BM" DataField="ReturnToBM"/>--%>
                               <asp:TemplateField HeaderText="Return to BM">
                <FooterTemplate>
                    <asp:Label ID="ReturnToBM" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblReturnToBM" runat="server" Text='<%# Eval("ReturnToBM") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                               <%-- <asp:BoundField HeaderText="Return to cash book" DataField="ReturnToCashBook"/>--%>
                               <asp:TemplateField HeaderText="Return to cash book">
                <FooterTemplate>
                    <asp:Label ID="ReturnToCashBook" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblReturnToCashBook" runat="server" Text='<%# Eval("ReturnToCashBook") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                               <%-- <asp:BoundField HeaderText="Return to const. imp." DataField="ReturnToConstImp"/>--%>
                               <asp:TemplateField HeaderText="Return to const. imp.">
                <FooterTemplate>
                    <asp:Label ID="ReturnToConstImp" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblReturnToConstImp" runat="server" Text='<%# Eval("ReturnToConstImp") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                               <%-- <asp:BoundField HeaderText="Closing Balance" DataField="ClosingBalance"/>--%>
                               <asp:TemplateField HeaderText="Closing Balance">
                <FooterTemplate>
                    <asp:Label ID="ClosingBalance" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblClosingBalance" runat="server" Text='<%# Eval("ClosingBalance") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                            
                            </Columns>
                        </asp:GridView>
                      
                    </td>
                </tr>
                </table>
                 <div style="page-break-after:always"></div>
  <div>&nbsp;
                <table>
                  <tr>
                    <td colspan="2">
                        <br />
                        <span lang="en-us"></span>20.भंडारित स्कन्ध:-
                    </td>
                </tr>
                  <tr>
                    <td>
                        (1)भंडारित स्कन्ध की गोदामवार,वस्तुवार(कोमोडिटीवाइस) जानकारी मूल्य सहित
                                
                    </td>
                    <td>
                        <asp:Label ID="Label28" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        (2)जारी एवं शेष वेयरहाउस रसीदों की जानकारी ली जाये। विवरण लगाएँ
                    </td>
                    <td>
                        <asp:Label ID="Label29" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        (3)जारी वेयरहाउस रसीदों पर जमकर्ताओं द्वारा लिए ऋण विवरण ,बैंक प्रमाणपत्र सहित लिया जाये। जानकारी मूल्य सहित
                    </td>
                    <td>
                        <asp:Label ID="Label30" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        (4)शाखा में भंडारित स्कन्ध का सम्पूर्ण भौतिक सत्यापन किया जाये। स्टेकिंग रख-रखाव एवं वैज्ञानिक भंडारण पर संक्षिप्त टीप भी  दिया जाये।अवलोकित कमियों का उल्लेख हो।
                    </td>
                    <td>
                        <asp:Label ID="Label31" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        (5)स्कन्ध मे दर्शित कमी/अधिकता की जानकारी दी जाये।
                    </td>
                    <td>
                        <asp:Label ID="Label32" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        (6)स्पेलेज एवं अधिक समय से भंडारित स्कन्ध के विषय में टीप दी जाये। स्पेलेज की रजिस्टर पृविष्टि से परीक्षण किया जाना टीप में लिया जाये । अधिक समय से जमा स्कन्ध जमाकर्ता को उठाने  के लिए शाखा प्रबन्धक ने क्या कार्यवाही की उसका स्पष्ट उल्लेख हो।
                    </td>
                    <td>
                        <asp:Label ID="Label33" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        21 लंबित भुगतान:-
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td>
                        विभिन्न मदों जैसे-गोदाम किराया,वेतन,चिकित्सा,यात्रा,बोनस  देयक,बिजली,पानी,दूरभाष,लीज़रेंट,संपत्तिकर आदि के लंबित देयकों के भुगतान का विवरण
                    </td>
                    <td>
                        <asp:Label ID="Label35" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        भूमि लीज़रेंट प्रीमियम,संपत्तिकर निर्धारित हो चुके हैं या नहीं  संबंधी जानकारी शाखा पर कितना संपत्तिकर,लीज़रेंट  प्रस्तावित है तदसंबंधी जानकारी।
                    </td>
                    <td>
                        <asp:Label ID="Label36" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        22.अग्रिम कर्मचारियों/अन्य को दिये गए सभी तरह के अग्रिमों का विवरण
                    </td>
                    <td>
                        <asp:Label ID="lblagrim" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        23.लंबित दावों का विवरण:
                    </td>
                    <td>
                        <asp:Label ID="lbllambitdabe" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        24. विविध स्टाक-<br />स्टाक पंजी अनुसार शाखा में स्टाक का सत्यापन हो आइटमवार मटेरियल जैसे-धूम्रीकरण सामग्री,डनेज,पलीथिन कवर,दवाइयाँ, नमी मापक यंत्र आदि का मिलान एवं विवरण लिया जाये।  
                    </td>
                    <td>
                        <asp:Label ID="lblvividhstock" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        25.किराये की गोदाम की गुणवत्ता,औचित्य,किराया सक्षम स्वीकृति  विषयक टीप:
                    </td>
                    <td>
                        <asp:Label ID="lblgunbatta" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        26. शाखा में संचालित निजी वेयरहाउस की जानकारी:-<br />
                         
                    </td>
                    <td>
                        
                    </td>
                </tr>
                 <tr>
                    <td align="right">
                        <br />
                        (अ) संख्या
                    </td>
                    <td>
                        <br />
                        <asp:Label ID="lblnijigodamno" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td align="right">
                        <br />
                        (ब) क्षमता
                    </td>
                    <td>
                        <br />
                        <asp:Label ID="lblnijigodamcap" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        27. सुरक्षा उपाय उपकरण-अग्निशमन की जानकारी: 
                    </td>
                    <td>
                        <asp:Label ID="lblsurakchaupkaran" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        28.निर्माण संबंधी:-
                    </td>
                    <td>
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        (अ) स्वनिर्मित गोदामों की संख्या 
                    </td>
                    <td>
                        <asp:Label ID="lblowngdn" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        
                        (ब) वार्षिक रख-रखाव 
                    </td>
                    <td>
                        <asp:Label ID="lblvarsikrakh" runat="server"></asp:Label>
                    </td>
                </tr>
                    <tr>
                    <td>
                        
                        (स) विशेष रख-रखाव 
                    </td>
                    <td>
                        <asp:Label ID="lblvisheshrakh" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        
                        (द) गोदामों की छत,फर्श,फेंसिंग,बाउंड्रीवाल की समस्याओं की जानकारी: 
                    </td>
                    <td>
                        <asp:Label ID="lblvaundriwall" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        29. चल रहे न्यायलयीन विभागीय जांच,जमाकर्ता द्वारा प्रस्तुत दावों का नीराकरण,लंबित रहने संबंधी अधतन स्थिति ,वेब्रिज स्थापना संबंधी बायबिलीय विषयक जानकारी 
                    </td>
                    <td>
                        <asp:Label ID="Label37" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        30.शाखाओ में संधारित रेकॉर्ड देखा जाये,सही संधारित हो रहा  है या नहीं
                    </td>
                    <td>
                        <asp:Label ID="Label38" runat="server"></asp:Label>
                    </td>
                </tr>
                
                  <tr>
                    <td>
                        <br />
                        31.शाखाओ में पदस्थ स्टाफ का उपयुक्तता,कमी/अधिकता के  विषय में स्पस्ट आकलन टीप दी जाये तथा इस सम्बंध मे समय-समय पर मुख्यालय द्वारा जारी निर्देशों के अनुरूप पदस्थ कंटेजेन्सी स्टाफ कम है या अधिक है?
                    </td>
                    <td>
                        <asp:Label ID="Label39" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        32.अंकेक्षणकर्ता अधिकारी किए गए पूर्व  अंकेक्षण बिन्दुओं, आक्षेपों की अनुपालन कार्यवाही का अवलोकन करें उनकी कार्यवाही सुनिश्चित की जाये,क्षे॰प्र॰ द्वारा किए गए निरीक्षण टीप उनके निर्देशों के अनुपालन उल्लेख अंकेक्षण प्रतिवेदन में  सम्मिलित किया जाये
                    </td>
                    <td>
                        <asp:Label ID="Label40" runat="server"></asp:Label>
                    </td>
                </tr>
                  <tr>
                    <td>
                        <br />
                        33.जमाकर्ता को वेयरहाउस रसीद के विरुद्ध बैंक द्वारा स्वीकृत ऋण पर प्राप्त कमिशन की राशि रुपये
                    </td>
                    <td>
                        <asp:Label ID="Label41" runat="server"></asp:Label>
                    </td>
                </tr>
                  <tr>
                    <td>
                        <br />
                        34.अन्य टीप
                    </td>
                    <td>
                        <asp:Label ID="Label42" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        नोट:-अंकेक्षणकर्ता अपनी अंकेक्षण प्रतिवेदन शाखा में शाखा प्रबन्धक को पावती प्राप्त कर देवें जिसकी एक प्रति संबन्धित क्षेत्रीय प्रबन्धक को अनुपालन कार्यवाही कर 15 दिवस के अंदर उनके अभिमत सहित मुख्यालय प्रेषित करें ।
                    </td>
                </tr>
            </table>

            <div style="page-break-after:always"></div>
  <div>&nbsp;
</div>

            <table>
                <tr>
                    <td>
                        त्रुटि पत्रक:-
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:Label ID="Label43" runat="server"></asp:Label>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <div style="page-break-after:always"></div>
  <div>&nbsp;
</div>
            <table>
                <tr>
                    <td colspan="2" align="center">
                        <h4>म॰प्र॰ राज्य भंडारण गृह निगम-क्षेत्रीय कार्यालय भोपाल</h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        शाखा का नाम:<asp:Label ID="lblbranch2" runat="server"></asp:Label></td>
                    <td align="center">
                        दिनांक:<asp:Label ID="lblauditdate2" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        निजी जमकर्ताओं के स्कन्ध हेतु जारी की गई भंडारगृह रसीदों के संबंध में 
                    </td>
                </tr>
                 <tr>
                    <td colspan="2" align="center">
                        <h4> शाखा प्रबन्धक का प्रमाण पत्र </h4>
 प्रमाणित किया जाता है की मेरे द्वारा विभिन्न जमकर्ताओं हेतु जारी की गई विभिन्न भंडारगृह रसीद में से निम्न भंडरगृह रसीदें उनके सन्मुख दर्शाये गए बैंक/निजी व्यक्तियों के पास आज दिनांक को रहन रखी गई हैं जिसकी सूचना हमे समय-समय पर प्राप्त हुई है तथा संबन्धित नस्ती में उक्त सूचना पत्र सुरक्षित रखा गया है:-  </p>
                    </td>
                </tr>
                  <tr>
                    <td colspan="2">
                       <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvwhrdtl" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True">
                            <Columns>
                                <asp:BoundField HeaderText="क्र॰" DataField="Row" />
                                <asp:BoundField  HeaderText="वेयरहाउस रसीद" DataField="Depositor_WHR_Id"/>
                                <asp:BoundField DataField="Depositor_Name" HeaderText="जमाकर्ता" />
                                <asp:BoundField HeaderText="दिनांक" DataField="whrdate"/>
                                <asp:BoundField HeaderText="स्कन्ध" DataField="comm"/>
                                <asp:BoundField HeaderText="स्कन्ध की मात्रा" DataField="Total_Qty_Received"/>
                                <asp:BoundField HeaderText="वोरे" DataField="TotalBags_Received"/>
                                <asp:BoundField HeaderText="वजन की मात्रा" DataField="Total_Qty_Received"/>
                                <asp:BoundField HeaderText="बेंक/निजी व्यक्ति का नाम जिसके पास रहन रखी गई हो" DataField="BankName"/>
                                <asp:BoundField HeaderText="लियन पत्र की तिथि" DataField="LiyanPatraDate"/>
                            </Columns>
                        </asp:GridView>
                      
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <p align="justify">
                        यह प्रमाणित किया जाता है उपरोक्त के अतिरिक्त कोई भी भंडरगृह रसीद किसी व्यक्ति या बैंक के पास रहन होने की सूचना इस कार्यालय को प्राप्त नहीं हुई। उक्त जानकारी का इंद्राज शाखा पर संधारित होल्डर आफ वेयरहाउस रसीद में मेरे द्वारा कर लिया गया है।
                   ूचना इस कार्यालय को प्राप्त नहीं हुई। उक्त जानकारी का इंद्राज शाखा पर संधारित होल्डर आफ वेयरहाउस रसीद में मेरे द्वारा कर लिया गया है।
                   </p>
                             </td>
                </tr>
                <tr>
                    <td></td>
                    <td align="right">मध्य प्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कोर्पो<br />शाखा:<asp:Label ID="lblbranch6" runat="server"></asp:Label></td>
                </tr>
            </table>
            <div style="page-break-after:always">

            </div>
  <div>&nbsp;
</div>
            <table>
                <tr>
                    <td colspan="2" align="center">
                        <h4>म॰प्र॰ राज्य भंडारण गृह निगम-क्षेत्रीय कार्यालय भोपाल</h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        शाखा का नाम:<asp:Label ID="lblbranch3" runat="server"></asp:Label></td>
                    <td align="center">
                        दिनांक:<asp:Label ID="lblauditdate3" runat="server"></asp:Label></td>
                </tr>
                 <tr>
                    <td colspan="2" align="center">
                      शाखा पर निरीक्षण की दिनांक तक जमा करने हेतु प्राप्त समस्त स्कन्ध के संबंध में प्रमाण पत्र।
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                         <p align="justify">
                     प्रमाणित किया जाता है की आज दिनांक <asp:Label ID="lbldate7" runat="server"></asp:Label>&nbsp;तक विभिन्न जमाकर्ताओं से जो स्कन्ध जमा करने हेतु इस भंडरगृह में प्राप्त हुआ उससे संबन्धित भंडरगृह रसीदें मेने जारी कर दी हैं तथा उनका इंद्राज शाखा के डी॰एल॰/एस॰आर॰ तथा अन्य संबन्धित रजिस्टर में कर लिया है मेरी जानकारी के अनुसार ऐसा कोई स्कन्ध आज दिनांक <asp:Label ID="lbldate9" runat="server"></asp:Label> को शेष नहीं है जिसकी भंडरगृह रसीद जारी किया जाना शेष है ।
                    </p>
                             </td>
                </tr>
               <tr>
                    <td></td>
                    <td align="right">शाखा प्रबन्धक <br />म॰प्र॰ राज्य भंडारगृह निगम<br />शाखा:<asp:Label ID="lblbranch4" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <br />
                        <br />
                        शाखा पर निरीक्षण की दिनांक भुगतान किए गए समस्त स्कन्ध के संबंध में
                        <br />
                    </td>
                    
                </tr>
                 <tr>
                    <td colspan="2">
                        <p align="justify">
                        यह प्रमाणित किया जाता है कि आज दिनांक <asp:Label ID="lbldate8" runat="server"></asp:Label> &nbsp;तक विभिन्न जमाकर्ताओं का जो स्कन्ध वेयरहाउस से भुगतान [डिलिवरी] किया गया है उसके पूर्ण भुगतान/आंशिक भुगतान का इंद्राज संबन्धित वेयरहाउस रसीदों कि मूल प्रति एवं कार्यालयीन प्रति में मेरे द्वारा कर लिया गया है । समस्त डेलेवेरी ऑर्डर का इंद्राज भी शाखा के डी॰एल॰/एस॰आर॰ में कर लिया गया है । मेरी जानकारी के अनुसार ऐसा कोई भुगतान आज दिनांक <asp:Label ID="lbldate10" runat="server" Text="Label"></asp:Label> को शेष नहीं है जिसका इंद्राज संबन्धित दस्तावेजों में किया जाना शेष हो ।
                             
                   </p>
                             </td>
                </tr>
                <tr>
                    <td></td>
                    <td align="right">शाखा प्रबन्धक <br />म॰प्र॰ राज्य भंडरगृह निगम <br />शाखा:<asp:Label ID="lblbrnach5" runat="server"></asp:Label></td>
                </tr>
            </table>

               <div style="page-break-after:always">

            </div>
  <div>&nbsp;
</div>

            <%--<table runat="server" id="godowndtl">
                <tr>
                    <td colspan="02" align="center">
                        <span>म॰ प्र. वेअरहाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन,शाखा:- <asp:Label ID="lblbranch44" runat="server" Text="Label"></asp:Label></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="02" align="center">
                        <span>-:भौतिक सत्यापन गणना पत्रक:-</span>
                    </td>
                </tr>
                <tr>
                    <td align="left">गोदाम क्र.<asp:Label ID="lblgodown" runat="server" Text="Label"></asp:Label></td>
                    <td align="right">भौतिक सत्यापन दिनांक: <asp:Label ID="lbldategdn" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ListView ID="ListView1" runat="server">
                       <LayoutTemplate>
                        <table style="border: thin solid #000000; border-collapse:collapse" >
                            <tr align="center" style="border: thin solid #000000">
                                <td style="border-style: solid; border-width: thin" align="center">
                                    जमकर्ता का नाम
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    स्कंध का नाम
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    स्टेक क्र.
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                   बिछान लं
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    बिछान चौ.
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    अति.
                                </td>
                                 <td style="border-style: solid; border-width: thin" align="center">
                                    योग
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    बोरों के लेयर की उचाई
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    ब्लॉक क्र./संख्या
                                </td>
                                <td  style="border-style: solid; border-width: thin" align="center">
                                    बोरियों की संख्या<br />(7*8*9)
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    अतिरिक्त पाई गई बोरियों की संख्या ऊपर
                                </td>
                                  <td style="border-style: solid; border-width: thin" align="center">
                                    अतिरिक्त पाई गई बोरियों की संख्या नीचे
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    भौ.स. में पाये कुल बोरों की संख्या<br />(10+11+12) 
                                </td>
                            </tr>
                            <tr align="center">
                                <td style="border-style: solid; border-width: thin" align="center">
                                    1
                                </td>
                                 <td style="border-style: solid; border-width: thin" align="center">
                                    2
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    3
                                </td>
                                 <td style="border-style: solid; border-width: thin" align="center">
                                    4
                                </td>
                                 <td style="border-style: solid; border-width: thin" align="center">
                                    5
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    6
                                </td>

                                 <td style="border-style: solid; border-width: thin" align="center">
                                    7
                                </td>
                                 <td style="border-style: solid; border-width: thin" align="center">
                                    8
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    9
                                </td>
                                 <td style="border-style: solid; border-width: thin" align="center">
                                    10
                                </td>
                                 <td style="border-style: solid; border-width: thin" align="center">
                                    11
                                </td>
                                <td style="border-style: solid; border-width: thin" align="center">
                                    12
                                </td>
                                 <td style="border-style: solid; border-width: thin" align="center">
                                    13
                                </td>
                            </tr>
                             <tr id="itemPlaceholder" runat="server" style="border: thin solid #000000;" align="center"></tr>
                            </table>
                             </LayoutTemplate>
                             <ItemTemplate>
                         <td style="border-style: solid; border-width: thin" align="center">
   <%# Eval("DeposioterName")%>
    
    </td>
     <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("Commid")%>
    </td>
    <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("stack")%>
    </td >
     <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("BichanLambai")%>
    </td >
     <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("BichanChodai")%>
    </td >
     <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("Atirict")%>
    </td >
      <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("yog")%>
    </td >
     <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("LayerKiUchai")%>
    </td >
     <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("Block")%>
    </td >
    
     <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("boriyonkisankhaya")%>
    </td >
     <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("atririktboriupper")%>
    </td >
                                  <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("atiriktborineeche")%>
    </td >
                                  <td style="border-style: solid; border-width: thin" align="center">
     <%# Eval("kulbore")%>
    </td >
    
    </td >
    </tr>

    </ItemTemplate>
    </asp:ListView>
                    </td>
                   
                </tr>
                <tr>
                    
                     <td align="left">
                         <br />
                    <br />
                    <br />
                        हस्ताक्षर शाखा प्रबन्धक
                    </td>
                    <td align="right">
                         <br />
                    <br />
                    <br />
                        भौतिक सत्यापनकर्ता अंकेक्षण अधिकारी
                    </td>
                </tr>
            </table>--%>

            </div>
        </div>
             </div>

    </form>
</body>
</html>
