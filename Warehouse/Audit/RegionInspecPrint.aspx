<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegionInspecPrint.aspx.cs" Inherits="Inspection_RegionInspecPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Region Audit</title>
      <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
     <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
<script type="text/javascript">
    function PrintDivR() {
        var divContents = document.getElementById("bgPrint").innerHTML;
        var printWindow = window.open('', '', 'height=200,width=400');
        printWindow.document.write('</head><body >');
        printWindow.document.write(divContents);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
        printWindow.close();
    }
    </script>
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

      </style>
</head>

<body>
    <form id="form1" runat="server">
    <div id="bg">
    
		<div class="wrap">
		<div style="background-color: #66CCFF">
                 <p style="font-size: medium; color: #008080;">क्षेत्रीय कार्यालय का अंकेक्षण: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome<span 
                         lang="en-us"> to</span>&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton 
                         ID="LinkButton1" runat="server" onclick="LinkButton1_Click" >Log out</asp:LinkButton></p>
            </div>
            <div id="bgPrint">
            <table width="100%">
                <tr>

                    <td colspan="2" align="center">
                        मध्यप्रदेश वेयरहाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन-मुख्यालय भोपाल<br />क्षेत्रीय कार्यालय का अंकेक्षण
                    </td>
                </tr>
                <tr>
                    <td>
                        1.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;अंकेक्षणकर्ता अधिकारी का नाम/पद
                    </td>
                    <td>
                        <asp:Label ID="lblauditornamepost" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                  <tr>
                    <td>
                        2.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;क्षेत्रीय प्रबन्धक का नाम/पद
                    </td>
                    <td>
                        <asp:Label ID="lblRMandPost" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        3.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;क्षेत्रीय कार्यालय का नाम
                    </td>
                    <td>
                        <asp:Label ID="lblROName" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span lang="en-us">4</span>.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;क्षेत्रीय प्रबन्धक कब से पदस्थ है
                    </td>
                    <td>
                        <asp:Label ID="lblRMPostingDate" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <span lang="en-us">5</span>.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;क्षेत्रीय कार्यालय में पदस्थ अधिकारियों/कर्मचारियो के नाम/पद :
                    </td>
                   <%-- <td>
                        <asp:Label ID="lblEmpNamenPost" runat="server" Text="Label"></asp:Label>
                    </td>--%>
                </tr>
                 <tr>
                 <td colspan="2" align="center">
                 
                 <asp:GridView ID="GVE" runat="server" AutoGenerateColumns="false" Width="500px">
                 <Columns>
                 <asp:BoundField HeaderText="S.N." DataField="SN" ItemStyle-Width="30px" />
                  <asp:BoundField HeaderText="Employee Name" DataField="EmpName" />
                  <asp:BoundField HeaderText="Employee Post" DataField="EmpPost" />
                  <asp:BoundField HeaderText="Period From" DataField="EMPPostingdate" />
                  </Columns>
                 </asp:GridView>
                 </td>
                 </tr>
                 <tr>
                    <td>
                        <br />
                        6.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;क्षेत्र की भंडारण क्षमता/उपयोगिता:-
                    </td>
                    <td>
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(अ) 1. स्वनिर्मित क्षमता:<asp:Label ID="lblowncap" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;मी॰टन
                    </td>
                    <td>
                        उपयोगिता:<asp:Label ID="lblownuse" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;मी॰टन/प्रतिशत:<asp:Label ID="lblownper" runat="server" Text="Label"></asp:Label></td>
                </tr>
                  <tr>
                    <td>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2. केप क्षमता:<asp:Label ID="lblcapcap" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;मी॰टन
                    </td>
                    <td>
                        उपयोगिता:<asp:Label ID="lblcapuse" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;मी॰टन/प्रतिशत:<asp:Label ID="lblcapper" runat="server" Text="Label"></asp:Label></td>
                </tr>
                  <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3. किराये की क्षमता:<asp:Label ID="lblhiredcap" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;मी॰टन
                    </td>
                    <td>
                        उपयोगिता:<asp:Label ID="lblhireduse" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;मी॰टन/प्रतिशत:<asp:Label ID="lblhiredper" runat="server" Text="Label"></asp:Label></td>
                </tr>
                  <tr>
                    <td>
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कुल क्षमता:-<asp:Label ID="lblTotalCapacity" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;मी॰टन
                    </td>
                    <td>
                        <br />
                       उपयोगिता&nbsp;<asp:Label ID="lblTotalUtilization" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;मी॰टन/प्रतिशत&nbsp;<asp:Label ID="lblTotalUtilizationPer" runat="server" Text="Label"></asp:Label></td>
                </tr>
                  <tr>
                    <td>
                        <br />
                        7.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;व्यवसाय :-
                    </td>
                    <td>
                      
                    </td>
                </tr>

                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(1)क्षेत्रीय कार्यालय की आय का लक्ष्य रु०:
                    </td>
                    <td>
                        <asp:Label ID="lblTargetIncome" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(2)आनुपातिक लक्ष्य(निरीक्षण माह तक) रु०:
                    </td>
                    <td>
                        <asp:Label ID="lblTargetRatio" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                   <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(3)लक्ष्य प्राप्ति रु०: 
                    </td>
                    <td>
                        <asp:Label ID="lblTargetAchiveR" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(4)लक्ष्य प्राप्ति का प्रतिशत रु०: 
                    </td>
                    <td>
                      <asp:Label ID="lblTargetAchiveP" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(5)अंकेक्षणकर्ता की टीप: 
                    </td>
                    <td>
                        <asp:Label ID="lblAuditerRemark" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(6)विगत वर्षो की तुलनात्मक स्थति: 
                    </td>
                    <td>
                        <asp:Label ID="lblRecentYearsComp" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(7)व्यवसाय व्रद्धि के लिए किए गए क्षे०प्र० के प्रयास की जानकारी: 
                    </td>
                    <td>
                        <asp:Label ID="lblBusinessDevelopInfo" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        8.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;अंकेक्षण दिनांक तक क्षे०का० के कुल देयको की प्रस्तुति उनकी वसूली से प्राप्त आय विवरण : 
                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(विगत वर्ष की आय प्रथक से दर्शित हो )
                    </td>
                    <td>
                        <asp:Label ID="lblIncomeDetailUptoAuditD" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        9.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;क्षेत्रीय कार्यालय से मासिक पत्रक आदि समय पर भेजे जाते है या नहीं 
                    </td>
                    <td>
                        <asp:Label ID="lblROInfo1" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        10.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1. भंडारण शुल्क रजिस्टर का परीक्षण कर देयक प्रविष्ट की गयी है या नहीं यह देखा जाये: 
                    </td>
                    <td>
                        <asp:Label ID="lblROInfo2" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2. समस्त जमाकर्ताओ के देयक समय पर नियमित रूप से प्रस्तुत किए जाते है या नहीं  :
                         <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;भंडारण शुल्क वसूली विलंब के कारण  
                    </td>
                    <td>
                        <asp:Label ID="lblROInfo3" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                       
                      11.&nbsp;&nbsp;&nbsp;&nbsp;(1) चालू वर्ष की कुल अर्जित आय:
                    </td>
                     <td>
                        <asp:Label ID="lblAchiveIncomeCurrentY" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(2) चालू वर्ष की कुल प्राप्ति: 
                    </td>
                    <td>
                        <asp:Label ID="lblReceivedIncomeCurrentY" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(3)पूर्व वर्ष की लंबित आय: 
                    </td>
                    <td>
                        <asp:Label ID="lblPendingIncomeRecentY" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                  <tr>
                    <td>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(4) पूर्व वर्ष की प्राप्त आय :
                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(अंकेक्षण दिनांक तक देखा जाये)
                    </td>
                    <td>
                        <asp:Label ID="lblReceivedIncomeRecentY" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <br />
                        <span lang="en-us"></span>12. &nbsp;&nbsp;&nbsp;&nbsp;टीडीएस. का विवरण:-
                    </td>
                </tr>
               
                           <tr>
                    <td>
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(1) टीडीएस का नियमानुसार कटौत्रा एवं राशि नियमानुसार जमा की गयी है अथवा नहीं? आयकर विवरणिका को निर्धारित अवधि मे जमा किया गया है अथवा नहीं इसकी जांच की जावे।  
                    </td>
                    <td>
                        <asp:Label ID="tds1" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(2) जमाकर्ताओ द्वारा जो राशि टीडीएस॰ के तहत काटी गयी है वह राशि 26 ए॰एस॰ मे प्रदर्शित हो रही है अथवा नहीं?  
                    </td>
                    <td>
                        <asp:Label ID="tds2" runat="server"></asp:Label>
                    </td>
                </tr>
                       <tr>
                    <td>
                        <br />
                        13॰ &nbsp;&nbsp;&nbsp;&nbsp;धन राशि के संग्रहण एवं हस्तांतरण नियमानुसार किया गया है अथवा नहीं इसकी जांच की जावे।  
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
                        14॰ &nbsp;&nbsp;&nbsp;&nbsp;वेअरहाउस चार्जेज के देयक समय पर प्रस्तुत किए जा रहे है अथवा नहीं ? वसूली की स्थती सही दर्शायी जा रही है अथवा नहीं?  
                    </td>
                    <td>
                        <asp:Label ID="WC" runat="server"></asp:Label>
                    </td>
                </tr>
                   <tr>
                    <td>
                        <br />
                        15॰ &nbsp;&nbsp;&nbsp;&nbsp;कंटेजेन्सी के तहत लेबर चार्जज का भुगतान नियमानुसार बैंक के माध्यम से किया जा रहा है अथवा नहीं?  
                    </td>
                    <td>
                        <asp:Label ID="lc" runat="server"></asp:Label>
                    </td>
                </tr>
                  <tr>
                    <td>
                        <br />
                        16॰ &nbsp;&nbsp;&nbsp;&nbsp;जे॰व्ही॰एस॰ के तहत लिए गये गोदामो का भुगतान नियमानुसार निरंतर आरटीजीएस के माध्यम से किया जा रहा है अथवा नहीं?
                    </td>
                    <td>
                        <asp:Label ID="jvs1" runat="server"></asp:Label>
                    </td>
                </tr>
                   <tr>
                    <td>
                        <br />
                        17॰ &nbsp;&nbsp;&nbsp;&nbsp;जे॰व्ही॰एस॰ के अंतर्गत लिए गये गोदामो का अनुबंध नियमानुसार निर्धारित किये  गये है अथवा नहीं?
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
                        <span lang="en-us"></span>18. शाखा पर इम्प्रेस्ट व्ययो का विवरण :-
                    </td>
                </tr>
                <tr>
                    <td style=" font-size:large;" align="center" colspan="4">
                       IMPREST RECUPMENT SHEET- <asp:Label ID="lblRgn" runat="server"></asp:Label>&nbsp;&nbsp;YEAR: <asp:Label ID="lblFy" runat="server"></asp:Label>
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
                    <td colspan="4">
                        19.&nbsp;&nbsp;&nbsp;&nbsp;पत्रको के प्रमाणीकरण एवं भौतिक सत्यापन :
                    </td>
                   
                </tr>
                 <tr>
                    <td colspan="4">
                        &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(1) क्षे०का० अंतर्गत संधारित स्कंध रजिस्टर, प्रस्तुत भंडारण शुल्क के देयकों की प्रस्तुति एवं वसूली संबंधी परीक्षण व केन्द्रवार स्कंध पंजी से इनके मिलान का प्रमाणन दिया जाये ।
                    </td>
                    
                </tr>
                 <tr>
                    <td colspan="4">
                        &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(2) समस्त बैंक समाधान पत्रको की जांच और उनका प्रमाणन । शाखाओ के बैंक खातो से क्षे०का० /मुख्यालय बुलायी गयी/भेजी गयी राशि समय पर भेजी गयी है या नहीं देखा जाये ।
                    </td>
                  
                </tr>
                 <tr>
                    <td colspan="4">
                       &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(3) स्थायी सम्पत्ति रजिस्टर, डैड स्टाक पंजी का सत्यापन किया जाये । स्कन्ध कमी/दावो की जांच एवं उनके पूर्ण विवरण दर्शित हो ।
                    </td>
                   
                </tr>
                 <tr>
                    <td colspan="4">
                        &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(4) सभी तरह के दस्तावेजो,रजिस्टरो, वेयरहाउस रसीद, रेकॉर्ड पंजी, फ्यूमिगेशन मटेरियल दवाइयो की रजिस्टर प्रविष्ट उनकी आवश्यकता एवं पूर्ति के विषय मे जांच कर प्रमाणन किया जाये ।
                    </td>
                    
                </tr>
                 <tr>
                    <td>
                    20.&nbsp;&nbsp;&nbsp;सामान्य लेखा परीक्षण :-
                    </td>
                    <td>
                        
                    </td>
                </tr>
                 <tr>
                    <td>
                        <br />
                        केश-   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(1) केशबुक बैलेन्स :-
                    </td>
                    <td>
                        <asp:Label ID="lblCashbookBalance" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(2) इम्प्रेस्ट केश बैलेन्स :                    </td>
                    <td>
                        <asp:Label ID="lblImprestCashBalance" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        बैंक बैलेन्स-   1/ बैंकों मे संधारित बैंक अनुसार बैंक बैलेन्स की स्थति : 
                    </td>
                    <td>
                        <asp:Label ID="lblBankBalance" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2/ निर्धारित सीमा से क्षे०का० के बैंकों मे अधिक राशि बैलेन्स मे नहीं रहे। 
                    </td>
                    <td>
                        
                    </td>
                </tr>
                 <tr>
                    <td colspan="4">
  
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3/ बैंक स्टेटमेंट लिया जाकर प्राप्त आय,जमा कराये जाने की प्रविष्ट का मिलान किया जाये ।
                    </td>
                    
                </tr>
                
                  <tr>
                    <td colspan="4">
                    <p>21.&nbsp;&nbsp;&nbsp;&nbsp;अग्रिम :</p>
                       <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(1) क्षे०का० से प्रदत्त सभी तरह के अग्रिमों की स्थति, अँकेक्षण टीप मे स्पष्ट की जाये ।  </p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(2) शाखाओ पर वेतन, टी०ए० बिल, मेडिकल बिल, गोदाम किराया बिल, अन्य भुगतान समय पर भेजे जाते है या नहीं ।</p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(3) 100% प्रमार्णन एवं बाउचिंग । समस्त आय-व्यय मुद्दो मे कोई रेवेन्यु लीकेज न हो इसका बाउचिंग के दौरान सपोर्टिंग पपेर्स देखा जाये ।</p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(4) लेखा अभिलेखा की प्रमाणिकता के लिहाज से समस्त आवश्यक प्रामाणिक सपोर्टों का परीक्षण हो ।</p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(5) आंतरिक अँकेक्षण के दौरान जो आपत्तिया ली गई हो उनमे से जो क्षे०का० से ही सुस्पष्ट एवं अनुपालन की जा सकती है उन्हे उसी स्तर पर सुधार करवा ली जाये गंभीर आपत्तियो को मुख्यालय प्रतिवेदन के रूप मे प्रस्तुत हो और उनपर कार्यवाही अनुशंसा भी प्रस्तावित हो ।</p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(6) विगत वर्ष की आंतरिक अँकेक्षण/निरीक्षण प्रतिवेदनो की अनुपालन कार्यवाही यदि नहीं की गयी हो तो सुनिश्चित करे । निराकरण मे कोताही बरती गई है तो कार्यवाही अनुशंसित की जाये । गंभीर बिन्दु पर परिपालन लंबित हो तो उसका स्पष्ट उल्लेख अपने अँकेक्षण प्रतिवेदन मे लिया जाये ।</p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(7) विभिन्न लेनदारी/देनदारी का रिकॉर्ड सत्यापन किया जाये तथा एडवाइज, कारणो का उल्लेख भीलिया जाये ।</p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(8) वैधानिक दायित्व, टी०डी०एस० आयकर, बोनस, सर्विस टैक्स आदि की जांच कर स्थति स्पष्ट करायी जाये उसके भुगतान तिथियो का प्रमाणन टैक्स ऑडिट रिपोर्ट मे अनिवार्यतः इस तरह से सुनिश्चित करे ताकि आयकर विवरणी दाखिल करते समय सही स्थति दर्ज हो सके व मुख्यालय के कार्य मे सुविधाहो ।</p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;(9) आंतरिक अँकेक्षण, क्षे०का० के अँकेक्षण के दौरान यह भी सुनिश्चित करे की निगम के वैधानिक व आंतरिक अँकेक्षण लेखा समापन कार्य समयबद्ध पूर्व हो जाये समस्त आय-व्ययों का लेखांकन/प्रावधान पूर्णरूपेण सही प्रमाण लिया गया हो,ताकि लेखा संकलन कार्य मे सुगमता हो ।</p>
                    </td>
                   
                </tr>
                 <tr>
                    
                    <td colspan="4">
    <p>22.&nbsp;&nbsp;&nbsp;&nbspकिराये की गोदामो की किराया स्वीकृति एवं उसके औचित्य की जांच ।</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;अँकेक्षणकर्ता अधिकारी क्षे०का० अंतर्गत शाखाओ मे किराये मे ली गयी गोदामो की आवश्यकता का </p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;औचित्य, उनकी गुणवत्ता एवं स्वीकृति का परीक्षण करेंगे तथा केन्द्रवार किराये की गोदामो का विवरण,</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;उनकी किराया राशि, देखेंगे । किराये की गोदामो की संख्या क्षे०प्र० को प्रदत्त अधिकारी के अंतर्गत</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;स्वीकृत गोदामो की जानकारी, क्षे०प्र० के अधिकार क्षेत्र से अधिक प्रतिशत वाले गोदामो की जानकारी,</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;किराये के गोदामो के लंबित प्रस्तावो की जानकारी अपने प्रतिवेदन मे दिया जाये ।</p>
    
                    </td>
                </tr>
                  <tr>
                    
                    <td colspan="4">
                       <p>23.&nbsp;&nbsp;&nbsp;&nbsp;निरीक्षण प्रतिवेदनो मे ली गयी टीपो की कार्यवाही रिपोर्ट की अनुपालन कार्यवाही जानकारी ।</p>
    
                    </td>
                </tr>
                  <tr>
                   
                    <td colspan="4">
                        <p>24.&nbsp;&nbsp;&nbsp;&nbsp;तकनीकी शाखा द्वारा खरीदी गयी भंडारण सामाग्री, फ्यूमिगेशन मटेरियल, दवाईयो उनकी आवश्यकता तथा</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;क्रय औचित्य की जांच सहित प्राप्त मटेरियल की यथोचित प्रविष्ट की गई है या नहीं तथा उसकी</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;आवश्यकता भंडारण नियमो के अनुसार समुचित उपयोग किया जा रहा है या नहीं देखा जाये और</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;प्रतिवेदन मे टीप दी जाये ।</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.क्षेत्रान्तर्गत नये निर्माण कार्यो की जानकारी, स्वनिर्मित गोदामो की दशा, कितनी शाखाओ मे वार्षिक</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रख-रखाव कराया गया, विशेष रख-रखाव कराया गया ।</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2. निर्माण संबंधी देयकों की जांच, स्वीकृति कार्य के रेकॉर्ड, एम०बी० अरई कर टेस्ट परीक्षण कर अपना </p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;प्रतिवेदन, कार्य स्वीकृत अनुसार कार्य किया जाना सुनिश्चित किया जाये । कार्य विवरण पंजिया</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;अनुबंध अनुसार कार्यो की क्रियान्वयन की अधतन स्थती ।</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3. क्षेत्रीय कार्यालय अंतर्गत स्वीकृत कार्यो की स्थति प्रस्तावित कार्यो का विवरण, भूमि संबंधी प्रस्तावो पर</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;टीप एवं बजट के अनुसार कार्य क्रियान्वयन अवलोकित हो ।</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4. गोदाम रिपेयर्स, विशेष रिपेयर्स कार्यो की अनुमानित स्वीकृति, निविदाये तथा कार्य स्वीकृति के अनुसार</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;कार्य संचालन, तत्संबंधी क्षे०का० स्तर पर उपयुक्त लेखांकन आदि देखा जाये ।</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;विशेष रेपेयर्स कार्य संबंधी देयकों तथा कार्यो की नियमानुसार कार्यवाही संपुष्टि की जाये यदि</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नियमानुसार कार्यवाही नहीं की गई हो तो अँकेक्षण प्रतिवेदन मे टीप ली जाये । विशेष रिपेयर्स हेतु</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;अग्रिम/भुगतान का समायोजन किया जाये । राशि शाखा मे अनावश्यक नहीं पड़ी रहे यह देखा जाये</p>
    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5. आंचलिक अभियंता द्वारा रुपये 50000/- तक रिपेयर्स कार्य की स्वीकृति की जानकारी ।</p>
                    </td>
                </tr> 
                <tr>
                <td colspan="4">संचालित निजी वेयरहाउस की स्थिति :</td>
                </tr>
                 <tr>
                <td colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;क्षेत्रीय कार्यालय अंतर्गत संचालित निजी वेयरहाउसो की जानकारी ।</td>
                </tr>
                <tr>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(1) संख्या : </td>
                <td>
                        <asp:Label ID="lblRNoOfGodown" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(2) क्षमता : </td>
                <td>
                        <asp:Label ID="lblRCapacityOfGodown" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                
                 <tr>
                <td colspan="4">आंतरिक अँकेक्षण कराये जाने की जानकारी :</td>
                </tr>
                <tr>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(अ) मुख्यालय स्तर से :  </td>
                <td>
                        <asp:Label ID="lblHOLevelAudit" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(ब) क्षेत्रीय कार्यालय स्तर से : </td>
                <td>
                        <asp:Label ID="lblROLevelAudit" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                 <tr>
                <td colspan="4">क्षेत्रान्तर्गत कितनी शाखाओ के लीजरेन्ट, प्रीमियम, सम्पत्तिकरण निर्धारण होना लंबित है- </td>
             
                </tr>
                 <tr>
                <td colspan="4">स्थापना - व्यय, स्वीकृति अवकाश, सर्विस प्रविष्ठियों का परीक्षण एवं टीप ।</td>
             
                </tr>
                  </table>
                <div style="page-break-after:always"></div>
                <table>
                <tr>
                <td colspan="4">25. त्रुटि पत्रक :</td>
                </tr>
                <tr>
                <td colspan="4">
               <asp:Label ID="lblTruti_Patrak" runat="server" Text="Label"></asp:Label>
                </td>
                </tr>
                <tr>
                <td colspan="4" align="center">
               <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="PrintR();" />--%>
                </td>
                </tr>
            </table>
           </div>
           <%-- <div style="page-break-after:always"></div>--%>

           
            </div>
        </div>
    <div>
    </div>
    </form>
        <center><input id="Button1" name="Print" type="button" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;" value="Print"  onclick="PrintDivR();" /></center>

</body>
</html>
