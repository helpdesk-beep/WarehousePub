<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GodownInspecPrint.aspx.cs" Inherits="Inspection_GodownInspecPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Warehouse Audit</title>  
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
     <link rel="stylesheet" href="css/main.css" type="text/css" />
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

      </style>


</head>
<body>
    <script type="text/javascript">
          function PrintDiv() {
              var divContents = document.getElementById("P").innerHTML;
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

    <form id="form1" runat="server">
        <center>
            <div style="background-color:aqua">
            
            <asp:LinkButton ID="LinkButton2" Font-Bold="true" Font-Size="Medium"
                         runat="server" onclick="LinkButton2_Click" >Back</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input id="Button1" name="Print" type="button" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;" value="Print"  onclick="PrintDiv();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Font-Bold="True" Font-Underline="True" ForeColor="#CC0000">Log Out</asp:LinkButton>
        </div>
                 </center>
                 
            <div id="ReportDiv">
    <div id="bg">
    <table style=" margin-left:500px">
    <tr id="trDD" runat="server" visible="false">
                    <td>

                       भौतिक सत्यापन हेतु गोदाम ::</td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlgodown_SelectedIndexChanged">
                        </asp:DropDownList>

                    </td>
                </tr>
    </table>
		<div class="wrap">
              <%-- <div style="page-break-after:always">--%>
              <div id="P">
               <table runat="server" id="godowndtl">
             
               
                <tr id="tr1" runat="server">
                    <td colspan="02" align="center">
                        <span>म॰ प्र. वेअरहाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन,शाखा:- <asp:Label ID="lblbranch44" runat="server" Text="Label"></asp:Label></span>
                    </td>
                </tr>
                <tr id="tr2" runat="server">
                    <td colspan="02" align="center">
                        <span>-:भौतिक सत्यापन गणना पत्रक:-</span>
                    </td>
                </tr>
               <tr id="tr3" runat="server">
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
            <tr id="tr4" runat="server">
                    
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
            </table>
              </div>
            </div>
  <div>
</div>

           

            </div>
        </div> 

    </form>

</body>
</html>
