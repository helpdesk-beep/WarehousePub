<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintReg.aspx.cs" Inherits="TribalGodown_PrintReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Registration </title>
        <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
	 <script type="text/javascript">
	     window.history.forward();

	     function noBack() { window.history.forward(); }
    </script>
	<script type="text/javascript">
	    Cufon.replace('h1,h2,h3,h4,h5,#menu,#copy,.blog-date');
	</script>
    <script type="text/javascript">
        function validateForm() {
            var x = document.forms["email_form_with_php"]["rname"].value;
            if (x == null || x == "") {
                alert("Name must be filled out");
                return false;
            }

            var x = document.forms["email_form_with_php"]["remail"].value;
            if (x == null || x == "") {
                alert("Email: must be filled out");
                return false;
            }

            var x = document.forms["email_form_with_php"]["remail"].value;
            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");
            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                alert("Not a valid e-mail address");
                return false;
            }
        }


    </script>
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

/*print*/

 
* {
    box-sizing: border-box;
    -moz-box-sizing: border-box;
}
.page {
    width: 21cm;
    min-height: 29.7cm;
    padding: 2cm;
    margin: 1cm auto;
    border: 1px #D3D3D3 solid;
    border-radius: 5px;
    background: white;
    box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
}
.subpage {
    padding: 1cm;
    border: 5px red solid;
    height: 237mm;
    outline: 2cm #FFEAEA solid;
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
        .style1
        {
            height: 22px;
        }
    </style>

    

</head>
<body onload="noBack();">
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
            var printWindow = window.open('', '', 'height=200,width=400');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
       <div id="ReportDiv">
      
		<div class="wrap">
                 <form id="Form1"   runat="server">
<%--                                                <p style="font-size: medium; color: #008080; background-color:Aqua;">आवेदक की जानकारी  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                    <asp:LinkButton ID="lnkLogout" runat="server" onclick="lnkLogout_Click" >Log out</asp:LinkButton>
                                                    </p>--%>
                                                    <div>
                                                    <table>
                                                                                 <tr >
                            <td colspan="4" style="font-size: medium; width: 1000px;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                             <%--<asp:LinkButton ID="LinkButton10" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx" ForeColor="White"></asp:LinkButton>--%>
                            </td>
                            <td colspan="2" style="background-color: #008CBA ;font-size: medium; color: White; width:100px" align="center" >
                            Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                            <td style="background-color: #008CBA; width:70px;" align="center" >
                            <asp:LinkButton ID="lnkLogout" runat="server" OnClick="lnkLogout_Click" ForeColor="White">Log out</asp:LinkButton></td>
                            </tr>
                            </table>
                            </td>
                        </tr>
                                                    </table>
                                                    </div>
          
                

  <div id="PrintDiv">

        <div>

                    <table>

                    
                        <tr >
                           
                            <td  colspan="4" style="background-color:none"> 
                                <p style="font-size: large; color:Maroon;">
                                    <img src="images/MpwlcM.PNG" alt="F" height="30" width="30" /> 
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Madhya Pradesh Warehousing and Logistics Corporation
                                </p>
                            </td>
                               
                        </tr>
                        <tr >
                            <center>
                            <td colspan="4" style="background-color: #66CCFF"> 
                                <p style="font-size: medium; color: Black;">अनुसूचित क्षेत्रों में गोदाम निर्माण एवं संचालन हेतु आवेदन पत्र(1800 MT.) सिर्फ आदिवासी हेतु              
                                </p>
                            </td>
                                </center>
                        </tr>
                    <tr>
                    <td>
                     <p style="font-size: small"> 
                       आवेदन क्र.
                         </p>
                    </td>
                    <td>
                      <asp:Label ID="lblAppNo" runat="server" Text=""></asp:Label>
                    </td>
                          <td rowspan="6" align="center">
                               <asp:Image ID="Image1" Width="110px" Height="140px" runat="server" Visible = "false"/>
                      
                    </td>
                   <td rowspan="6">
                       <img src="image022.gif"
                   </td>
                    </tr>
                         <tr>
                    <td>
                      नाम:
                    </td>
                    <td>
                        <asp:Label ID="lblFullName" runat="server" Text=""></asp:Label>
                    </td>
                               
                    </tr>
   <tr>
                    <td>
                         पिता का नाम :</td>
                    <td>
                        <asp:Label ID="lblFather" runat="server" Text=""></asp:Label>
                    </td>
        
                    </tr>
                        <tr>
                            <td>
                      माता का नाम :
                            </td>
                            <td>
                              <asp:Label ID="lblMother" runat="server" Text=""></asp:Label>    
                            </td>
                             
                        </tr>

                    <tr>
                                        <td>जन्म तिथि:</td>
                    <td>
                    

                    <asp:Label ID="lblDOB" runat="server" Text=""></asp:Label>
                    </td>
                         
                  </tr>
                   <tr>
                 
                   <td>
                   लिंग :
                   </td>
                   <td>

                <asp:Label ID="lblSex" runat="server" Text=""></asp:Label>
                   </td>

                        
                   </tr>
                   
                   
                      <tr>
                    <td class="style1">
                       ई-मेल: 
                    </td>
                    <td class="style1">
                       <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                    </td>
                              <td class="style1">
                       वित्त का साधन:
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblSOI" runat="server" Text=""></asp:Label>
                    </td>
                    </tr>
                    

                    <tr>
                    <td >
                        वर्तमान पता:
                    
                    </td>
                    
                   
                    <td>
                         <asp:Label ID="lblCAddress" runat="server" Text=""></asp:Label>
                    
                    </td>
                         <td>
                             मोबाइल नंबर:
                    
                    </td>
                    
                   
                    <td>
                         <asp:Label ID="lblMob" runat="server" Text=""></asp:Label>
                    </td>


                    </tr>
                         <tr>
                                        <td>विकास खण्ड</td>
                    <td>
                    

                     <asp:Label ID="lblCBlock" runat="server" Text=""></asp:Label>
                    </td>
                          <td>
                              शिक्षा: 
                    </td>
                    <td>
                        <asp:Label ID="lblEducation" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                          <tr>
                                        <td>जिला</td>
                    <td>
                    

                    <asp:Label ID="lblCDistrict" runat="server" Text=""></asp:Label>
                    </td>
                            <td>
                        रोजगार कार्यालय नंबर:
                    </td>
                    <td>
                         <asp:Label ID="lblRKN" runat="server" Text=""></asp:Label>
                    </td>  
                  </tr>
                          <tr>
                                        <td>पिन कोड:</td>
                    <td>
                    

                     
                     <asp:Label ID="lblCPIN" runat="server" Text=""></asp:Label>
                    </td>
                          <td>
                              वोटर आई डी नंबर</td>
                    <td>
                    

                      <asp:Label ID="lblVID" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                          <tr>
                                        <td>आधार कार्ड नंबर :</td>
                    <td>
                    

                          <asp:Label ID="lblADHAR" runat="server" Text=""></asp:Label>
                    </td>
                          <td>
                             पैन कार्ड नंबर :</td>
                    <td>
                        <asp:Label ID="lblPAN" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                       
                  <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: Black;">
                          वेयरहाउस संचालन जिस जगह करना है उसका विवरण :-</p>
                      </td>

                  </tr>
                          <tr>
                                        <td>
                                            जिला</td>
                                        <td>
                                         
                        <asp:Label ID="lblWDistrict" runat="server" Text=""></asp:Label>
                    </td>
                                      <td>
                                            विकास खण्ड

                                        </td>
                                        <td>
                                         
                         <asp:Label ID="lblWBlock" runat="server" Text=""></asp:Label>
                    </td>   
                                    </tr>
                           <tr>
                    <td >
                        
                        पता:
                    
                    </td>
                    
                   
                    <td>
                          <asp:Label ID="lblWAddress" runat="server" Text=""></asp:Label>
                    
                    </td>
                    <td >
                        
                        मुख्यालय से दूरी & भूमि की मात्रा:
                    
                    </td>
                    
                   
                    <td>
                          <asp:Label ID="lblDistanceQty" runat="server" Text=""></asp:Label>
                    
                    </td>
                             
                                            </tr>

                         <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color:Black ">
                          बैंक संबन्धित जानकारी :-</p>
                      </td>

                  </tr>
                          <tr>
                                        <td>
                                            बैंक का नाम

                                        </td>
                                        <td>
                                         
                          <asp:Label ID="lblBank" runat="server" Text=""></asp:Label>
                    </td>
                                      <td>
                                            अकाउंट नंबर

                                        </td>
                                        <td>
                                         
                        <asp:Label ID="lblANO" runat="server" Text=""></asp:Label>
                    </td>   
                                    </tr>
                           <tr>
                    <td >
                        
                    आइ एफ एस सी कोड:
                    
                    </td>
                    
                   
                    <td>
                         <asp:Label ID="lblIFSC" runat="server" Text=""></asp:Label>
                    
                    </td>
                                            </tr>

                         <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color:Black ">
                          किन्ही 2 परिचित व्यक्तियों की जानकारी :-</p>
                      </td>

                  </tr>
                          <tr>
                                        <td>
                                            1 व्यक्ति का नाम

                                        </td>
                                        <td>
                                         
                        <asp:Label ID="lblP1" runat="server" Text=""></asp:Label>
                    </td>
                                      <td>
                                            
                                           संबंध:
                                        </td>
                                        <td>
                                         <asp:Label ID="lblP1Rel" runat="server" Text=""></asp:Label>
                         
                    </td>   
                                    </tr>
                           <tr>
                    <td >
                        
                  पता
                    
                    </td>
                    
                   
                    <td>
                       <asp:Label ID="lblP1Add" runat="server" Text=""></asp:Label>  
                    
                    </td>
                                <td >
                        
                    मोबाइल नं. & ई मेल:
                    
                    </td>
                    
                   
                    <td>
                         <asp:Label ID="lblP1Mob" runat="server" Text=""></asp:Label>
                         /
                         <asp:Label ID="lblP1Email" runat="server" Text=""></asp:Label>
                    
                    </td>
                                            </tr>
                
                                        <td>
                                            2 व्यक्ति का नाम

                                        </td>
                                        <td>
                                         
                        <asp:Label ID="lblP2" runat="server" Text=""></asp:Label>
                    </td>
                                      <td>
                                            
                                     संबंध:
                                        </td>
                                        <td>
                                         
                           <asp:Label ID="lblP2Rel" runat="server" Text=""></asp:Label>
                    </td>   
                                    </tr>
                        <tr>
                    <td >
                        
                   पता
                    
                    </td>
                    
                   
                    <td>
                        <asp:Label ID="lblP2Add" runat="server" Text=""></asp:Label>
                    
                    </td>
                                <td >
                        
                     मोबाइल नं.& ई मेल:
                    
                    </td>
                    
                   
                    <td>
                    <asp:Label ID="lblP2Mob" runat="server" Text=""></asp:Label>
                    /
                       <asp:Label ID="lblP2Email" runat="server" Text=""></asp:Label>
                     
                    
                    </td>
                                                    <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color:Black ">
                          दस्तावेज़ जमा करने हेतु संबंधित क्षेत्रीय कार्यालय की जानकारी :-</p>
                      </td>

                  </tr>
                          <tr>
                                        <td colspan="4">
                                           
                       <asp:Label ID="lblRegionAdd" runat="server" Text="" name="ryear" type="text" class="text"></asp:Label>
                                        </td>
               
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                           
                       <asp:Label ID="lblRegionAdd2" runat="server" Text="" name="ryear" type="text" class="text"></asp:Label>
                                        </td>
               
                                    </tr>

                                            </tr>

                         
                             <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color:Black">
                          घोषणा :-</p>
                      </td>

                  </tr>
                        <tr>
                            <td colspan="4">
                                <p>
                                    1. मै घोषणा करता /करती हूं कि मेरे द्वारा दी गयी उपरोक्त जानकारी मेरे ज्ञान के 
                                    अनुसार सत्य है, मैंने उसमें कुछ भी छुपाया नहीं है। मुझे यह ज्ञात है कि मेरे 
                                    द्वारा असत्य या भ्रामक जानकारी देने पर मेरे विरुद्ध आपराधिक दण्डात्मक कार्यवाही 
                                    की जा सकती है। साथ ही मुझे प्राप्त समस्त लाभों को भी वापस <span lang="hi">लिया 
                                    जा सकता है</span>। <br />
                                    2.<span lang="hi"> </span>म॰ प्र॰ शासन नियमानुसार कार्यवाही करने के लिए 
                                    स्वतंत्र <span lang="hi">होगा</span> । 
                                </p>
                            </td>

                        </tr>
                        <tr>
                           <td>
                               <br />
                              
                                दिनांक:
                              
                           </td>
                            <td>
                                <br />
                               
                                 <asp:Label ID="lblAppDate" runat="server" Text=""></asp:Label>
                                
                            </td>
                             <td>
                                <br />
                              
                                
                             
                            </td>
                             <td>
                               
                               <br />
                                 आवेदक के हस्ताक्षर </td>
                        </tr>
                                     
                    </table>
                                </form>
                                  </div>
                    </div>
  
    <center><input id="Button1" name="Print" type="button" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;" value="Print"  onclick="PrintDiv();" /></center>
</body>
</html>
