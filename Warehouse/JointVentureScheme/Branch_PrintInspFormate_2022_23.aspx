<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Branch_PrintInspFormate_2022_23.aspx.cs" Inherits="JointVentureScheme_Branch_PrintInspFormate_2022_23" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Inspection Form</title>
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
        .style2
        {
            height: 35px;
        }
        
        </style>
</head>
<body >
    <script type="text/javascript">
        function PrintDiv() {
            // document.getelementbyiD("Button1").style.display = 'none';
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
       <div id="ReportDiv" >
      
		<div class="wrap">
                 <form id="Form1" runat="server">
<%--                                                <p style="font-size: medium;color: #008080; background-color:#66CCFF;">  &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inspection Form&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                  &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp 
                                                    <asp:LinkButton ID="lnkLogout" runat="server" onclick="lnkLogout_Click" >Log out</asp:LinkButton>
                                                 </p>--%>
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr style="height:25px;">
                            <td  style="background-color: #008CBA; width:50PX ; " align="center"> 
                             <asp:LinkButton ID="LinkButton10" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx" ForeColor="White"></asp:LinkButton>
                            </td>
                            <td style="background-color: #008CBA ;font-size: medium; color: White; width:600px" align="center" >
                            Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                            <td style="background-color: #008CBA; width:50px;" align="center" >
                            <asp:LinkButton ID="lnkLogout" runat="server" onclick="lnkLogout_Click" ForeColor="White">Log out</asp:LinkButton></td>
                            </tr>
                  <tr>
                      <td colspan="3" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White ; width: 100%;" align="center">
                          <p style="font-size: 14px; color: Black;">
                          Print Godown Inspection Form</p>
                      </td>
                  </tr>                            
                            </table>                                                 
                                                 
               <br />
                
          <div>
          <table>
          <tr>
          <td align="center">
                                      Season &nbsp&nbsp
                        <asp:DropDownList ID="ddl_session" runat="server" Width="150px"  Height="25px" 
                                          onselectedindexchanged="ddl_session_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                             <asp:ListItem Value="JVS2022_23">JVS 2022-23</asp:ListItem>
                            <asp:ListItem Value="JVS2021_22">JVS 2021-22</asp:ListItem>
                        <asp:ListItem Value="JVS2020_21">JVS 2020-21</asp:ListItem>
                        <asp:ListItem Value="Kharif1920">Kharif 2019-20</asp:ListItem>
                        <asp:ListItem Value="Rabi1920">Rabi 2019-20</asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp&nbsp&nbsp;&nbsp&nbsp   <asp:Label ID="lblWarehouse" runat="server" Text="Warehouse Name "></asp:Label>
                   &nbsp;&nbsp;&nbsp;  <asp:DropDownList ID="ddlWarName" runat="server" AutoPostBack="true"
                            Width="230px" Height="25px" 
                         onselectedindexchanged="ddlWarName_SelectedIndexChanged">
                        </asp:DropDownList> 
                        
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblgodown" runat="server" Text="Godown No."></asp:Label>
                      &nbsp;&nbsp;&nbsp;  <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="true" 
                            Width="100px" Height="25px" onselectedindexchanged="ddlgodown_SelectedIndexChanged" 
                            >
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button1" runat="server" Text="Print" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;"  
                      OnClientClick="PrintDiv();" onclick="Button1_Click" />
          </td>
          </tr>
          </table>
          </div>
          <br />


  <div id="PrintDiv" style="font-size:small;" runat="server" visible="false" >

                    <table >
  
                      <tr>
                       <td colspan="4"  class="style2" align="center" style="width:954px">
                          <p style="font-size:14px; color:Black; font-weight:bold; text-align:center">
                         मध्यप्रदेश वेअरहाउस एंड लॉजिस्टिक कार्पोरेशन <br />
                         खरीफ विपणन वर्ष 2022-23 में संयुक्त भागीदारी योजना के तहत निजी/सहकारी क्षेत्र/संस्थागत गोदाम संचालक द्वारा <br />
                              ऑनलाईन आफर्ड की गयी क्षमता हेतु गोदाम उपयुक्तता/सुविधा एवं संसाधन उपलब्धता का संयुक्त निरीक्षण प्रतिवेदन <br />
                              क्षेत्रीय कार्यालय -&nbsp;<asp:Label ID="lblRegion" runat="server"></asp:Label></p>
                         <hr />
                      </td>
                      </tr>
                      <tr>
                      <td>
                     &nbsp;&nbsp;  Registration ID &nbsp;&nbsp;<input id="txtRegNo"  class="text" runat="server" style="width:150px; height:25px;"  readonly="readonly"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Godown ID (As Per WMS) &nbsp;&nbsp; <input id="Text1"  class="text" runat="server"  style="width:150px; height:25px;" /> <asp:Label ID="lblgdwnofrid" runat="server" Visible="false"></asp:Label>
                      <br /></td>
                      </tr>
                      
                      <tr>
                        <td colspan="2"> <br />(1) निरीक्षण दिनांक  :&nbsp;&nbsp; <asp:Label ID="lblNriDate" runat="server" Text="______________"></asp:Label> </td>
                    </tr>
                  <tr>
                      <td colspan="4">(2) जिले का नाम : &nbsp;&nbsp;<asp:Label ID="lbldistname" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       शाखा का नाम :&nbsp;&nbsp;<asp:Label ID="lblbranch" runat="server" Text="" Font-Underline="True"></asp:Label></td>
                  </tr>
             
                  <%-- <tr>
                       <td colspan="4">
                           (3) निरीक्षणकर्ता अधिकारिय का विवरण :- <br />
                           <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>MPWLC के शाखा प्रबंधक का नाम &nbsp;&nbsp; <asp:Label ID="lblbmname" runat="server" Font-Underline="True"></asp:Label>
                       </td>
                    </tr>--%>
                    <tr>
                   
                    <td colspan="4"><br />

                      (3) वेअरहाउस का नाम : <asp:Label ID="lblWarehouseName" runat="server" Text="" Font-Underline="True"></asp:Label>  &nbsp;&nbsp;&nbsp;ऑफर गोदाम का  नंबर  :&nbsp <asp:Label ID="lblGdwnNo" runat="server" Text="" Font-Underline="True"></asp:Label> <br />
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;वेअरहाउस का पता :&nbsp; <asp:Label ID="lblWareAddress" runat="server" Text="" Font-Underline="True"></asp:Label><br />
  
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ज़िला : &nbsp;<asp:Label ID="lblDistrict2" runat="server" Text="" Font-Underline="True"></asp:Label>
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;तहसील :&nbsp;<asp:Label ID="lblTehsil" runat="server" Text="" Font-Underline="True"></asp:Label>
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;विकासखंड का नाम :&nbsp;<asp:Label ID="lblblocktxt" runat="server" Text="" Font-Underline="True"></asp:Label><br />
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;अक्षांश :&nbsp;<asp:Label ID="lbllongitude" runat="server" Text="" Font-Underline="True"></asp:Label>
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;देशान्त : &nbsp;<asp:Label ID="lbllatitude" runat="server" Text="" Font-Underline="True"></asp:Label></td>
                    </tr>
                        <tr>
                            <td>
 &nbsp;&nbsp;&nbsp;&nbsp; वेअरहाउस परिसर का कुल क्षेत्रफल : &nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Text="______________________"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />

                            </td>
                        </tr>
                        <tr>
                   
                    <td>
&nbsp;&nbsp;&nbsp;&nbsp; वेअरहाउस कार्यालय का मोबाइल नंबर : &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblOfficeMob" runat="server" Text="" Font-Underline="True"></asp:Label><br/>
&nbsp;&nbsp;&nbsp;&nbsp; वेअरहाउस प्रभारी का मोबाइल नंबर :  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblInchMob" runat="server" Text="" Font-Underline="True"></asp:Label><br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;वेअरहाउस की निकटतम शाखा का नाम  :&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblnearbranch" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; गोदाम से दूरी(km) :&nbsp;<asp:Label ID="lblnearbranchDist" runat="server" Text="" Font-Underline="True"></asp:Label><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;वेअरहाउस से निकटतम रेल्वे रेक पॉइंट  का नाम : &nbsp;<asp:Label ID="Label9" runat="server" Text="_________________"></asp:Label>&nbsp;&nbsp;दूरी(km) :&nbsp;&nbsp;<asp:Label ID="Label8" runat="server" Text="______"></asp:Label></td>
                    </tr>
                      <%--<tr>
                   
                    <td colspan="4" class="style6">
                          <p style="font-size: medium; color: #008080;font-weight:bold;  ">

                      (4) वेअरहाउस का नाम <asp:Label ID="Label11" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
  वेअरहाउस का पता <asp:Label ID="Label12" runat="server" Text="" Font-Underline="True"></asp:Label><br />
  तहसील<asp:Label ID="Label13" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ज़िला<asp:Label ID="Label14" runat="server" Text="" Font-Underline="True"></asp:Label><br />
  अक्षांश<asp:Label ID="Label15" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; देशान्त<asp:Label ID="Label16" runat="server" Text="" Font-Underline="True"></asp:Label>
                              </p>      
                    </td>
                    </tr>  --%>
                        <tr>
                        <td ><br />
                              (4) वेअरहाउस  संचालक  का नाम :&nbsp;<asp:Label ID="lblAuthPerson" runat="server" Text="" Font-Underline="True"></asp:Label><br />
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;वेअरहाउस  संचालक  का पता : &nbsp;<asp:Label ID="Label18" runat="server" Text="___________________________" ></asp:Label><br />

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PAN : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpan" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Aadhar No. : &nbsp;<asp:Label ID="lblaadhar" runat="server" Text="" Font-Underline="True"></asp:Label><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;मोबाइल नंबर :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmob" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblemail" runat="server" Text="" Font-Underline="True"></asp:Label><br />

 
                               </td>
                    </tr>
                        <tr>
                        <td ><br />
(5) JVS में ऑनलाइन ऑफर दिनांक :&nbsp;<asp:Label ID="lblofferdt" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; समय :&nbsp; <asp:Label ID="lbltime" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(ऑनलाइन ऑफर अनुसार)<br />
                             
                               </td>
                    </tr>
                        <tr>
                        <td colspan="4">
(6) JVS में ऑनलाइन ऑफर&nbsp; गोदाम अथवा गैर WDRA में से जो उल्लेख हो
    लिखा जाये :&nbsp; <asp:Label ID="lblOferscheme" runat="server" Text="" Font-Underline="True"></asp:Label>

                               </td>
                    </tr>
                        <tr>
                        <td >
(7) ऑफर की गयी भंडारण क्षमता का प्रकार - (आंशिक/पूर्ण ) :&nbsp;<asp:Label ID="lbloffertype" runat="server" Text="" Font-Underline="True"></asp:Label><br />

                               </td>
                    </tr>
                        <tr>
                        <td >
(8) JVS में ऑफर की गयी क्षमता :&nbsp; <asp:Label ID="lbloffercpt" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;में टन <br />

                              
                               </td>
                    </tr>
                      
                  <tr>
                   
                    <td ><br />
                       
<b>(9)निरीक्षण में पाई गयी जानकारी का विवरण</b>
                    </td>
                      
                    </tr>
                   </table>
                        <table border="1" >
                            <tbody>
                                <th>SN
                                <th>Discription
                                <th>Godown Details
                                <th>Remarks
                            </tbody>
                            <tr>
                            <td>1</td>                           
                            <td> लंबाई मापन(फीट)</td>
                            <td><asp:Label ID="lblLength" runat="server" Text="" Font-Underline="True"></asp:Label></td>
                            <td></td>
                                </tr>
                            <tr>
                                <td>2</td>                           
                            <td> चौडाई  मापन(फीट)</td>
                            <td><asp:Label ID="lblWeidth" runat="server" Text="" Font-Underline="True"></asp:Label></td>
                            <td></td>
                            </tr>
                            <tr>
                            <td>3</td>                           
                            <td> ऊँचाई मापन(फीट)(अधिकतम 18 फिट)</td>
                            <td><asp:Label ID="lblHeight" runat="server" Text="" Font-Underline="True"></asp:Label></td>
                            <td></td>
                                </tr>
                            <tr>
                                <td>4</td>                           
                            <td> संगणित भण्डारण क्षमता(में टन )-सूत्र (LxWx(H-3)/80) अनुसार भण्डारण क्षमता</td>
                            <td><asp:Label ID="lblTotalCpt" runat="server" Text="" Font-Underline="True"></asp:Label></td>
                            <td></td>
                            </tr>
                            <tr>
                            <td>5</td>                           
                            <td> वर्तमान में गोदाम में संग्रहित स्कंधो का नाम </td>
                            <td></td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>6</td>                           
                            <td> गोदाम में संग्रहित स्कंध जमाकर्ता का नाम (शासकीय-MPSCSC/Markfed/Nafed,Private,Other) </td>
                            <td></td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>7</td>                           
                            <td> वर्तमान में गोदाम की रिक्त क्षमता (मे.टन) (Vacant Capacity) </td>
                            <td></td>
                            <td></td>
                                </tr>
                            </table>
                    <table>
                    <tr>
                    <td  ><br /><br />
                      (10) <b>गोदाम लायसेंस का विवरण :</b><br />
                        
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  लायसेंस प्रकार  <asp:Label ID="lbllictype" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp; लायसेंस नंबर  <asp:Label ID="lblstateLic" runat="server"  Font-Underline="True"></asp:Label><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; जारी दिनांक 
                                <asp:Label ID="lblissuedate" runat="server" Font-Underline="True"></asp:Label>&nbsp;&nbsp;वैधता दिनांक  <asp:Label ID="lblStateLicExpDate" runat="server"  Font-Underline="True"></asp:Label><br />

  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; यदि लायसेंस वैध नहीं है तो क्या इसे नवीनीकरण हेतु आवेदन किया गया है?तो उसका <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; आवेदन लायसेंस का प्रकार <asp:Label ID="lblaplliedtxttype" runat="server"  Font-Underline="True"></asp:Label> आवेदन क्रमांक <asp:Label ID="lblapplied" runat="server"  Font-Underline="True"></asp:Label> <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; आवेदन दिनांक <asp:Label ID="lblapplieddate" runat="server" Font-Underline="True"></asp:Label>
      (छायाप्रति संलग्न)         <br />
    
                    </td>
                                                                             
                  </tr>
                    <tr>
                     
                    <td ><br /> 
                       (11) <b>JVS गोदामों में उपलब्ध सुविधाओं का परीक्षण  :-</b><br/>
                    </td>
                   
                    </tr>
                    </table>
                        <table border="1">
                           <tr>
                                <td style="width:20px;">1</td>
                                <td>क्या WDRA का वैध लायसेंस/राज्य शासन का वैध वेअरहाउस लायसेंस है ?</td>
                                <td style="width:30px;">हाँ</td>
                                <td style="width:30px;">नहीं</td>
                            </tr>

 
                            <tr>
                            <td>2</td>
                            <td>क्या गोदाम में डामरीकृत रोड/सीसी.रोड/WBM है ?</td>
                                <td style="width:30px;">हाँ</td>
                                <td style="width:30px;">नहीं</td>

                                </tr>
                            
                            <tr>
                            <td>3</td>
                            <td>क्या गोदाम के प्रत्येक गेट/शटर पर ’’जालीदार गेट/शटर’’ है ? </td>
                            <td>हाँ</td>
                            <td>नहीं</td>
                            </tr>                                
                            
                            <tr>
                                <td>4</td>
                                <td>क्या गोदाम परिसर में मानक क्षमता का चालू हालत में इलेक्ट्रानिक वेब्रिज है ?</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr> 
                            
                            <tr>
                                <td>5</td>
                                <td>क्या गोदाम परिसर को सुरक्षा व्यवस्था हेतु बाउण्ड्रीवाॅल, चैनलिंग फेंसिंग अथवा बार्बेड वायर फेंसिंग से कवर्ड है ? </td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr> 
                             <tr>
                                <td>6</td>
                                <td>क्या गोदाम स्तरीय खरीदी केन्द्र बनाने हेतु गोदाम परिसर में पर्याप्त स्थान, सुविधाऐं उपलब्ध हैं? </td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr> 
                           
                                                                                                         
                        </table>
                      
                        <table>
                  <tr>
                    <br /> 
                      <td>
                       
(12) <b> गोदाम अपात्र होने संबंधी बिन्दुओं का परीक्षण :- </b><br />
<table border="1">
    <tr>
                            <td style="width:20px">1</td>
                            <td>क्या गोदाम निर्माणाधीन है ?</td>
                            <td style="width:40px">हाँ</td>
                            <td style="width:40px">नहीं</td>
        </tr>
                            <tr>
                                <td>2</td>
                                <td>क्या क्या गोदाम संचालक के विरूद्ध MPWLC द्वारा स्कंध के जमा/भुगतान, स्कंध की गुणवत्ता विपरीत रूप से प्रभावित करने के कारण विवाद होकर उनके विरूद्ध कार्यवाही की गयी है ?</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
    <tr>
                            <td>3</td>
                            <td>क्या गोदाम क्षतिग्रस्त है ? </td>
                            <td>हाँ</td>
                            <td>नहीं</td>
        </tr>
                            <tr>
                                <td>4</td>
                                <td>क्या गोदाम में शासकीय योजनाओं के अतिरिक्त अन्य स्कंध भण्डारित है ?  </td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
    <tr>
                            <td>5</td>
                            <td>क्या गोदाम ’’ब्लेक लिस्टेड’’ है ?   </td>
                            <td>हाँ</td>
                            <td>नहीं</td>
        </tr>
    <tr>
                            <td>6</td>
                            <td>क्या गोदाम की भंडारण क्षमता एक परिसर में न्यूनतम 500 मे.टन से कम है ?</td>
                            <td>हाँ</td>
                            <td>नहीं</td>
        </tr>
        
<%--    <tr>
                            <td>7</td>
                            <td>क्या निजी गोदाम, निजी फर्म, कंपनी, पार्टनरशिप फर्म, सहकारी संस्थाऐं अथवा अन्य संस्थाओं के उक्त गोदाम MPWLC के ऑनलाईन पोर्टल पर अनुबंध अवधि तक पंजीकृत है ?</td>
                            <td>हाँ</td>
                            <td>नहीं</td>
        </tr>--%>
      
        
        <tr>
                            <td>7</td>
                            <td>गोदाम वैज्ञानिक भण्डारण की दृष्टि से वायुसंचरण हेतु रोशनदान के साथ-साथ गोदाम की उंचाई 14 फिट से कम है ?</td>
                            <td>हाँ</td>
                            <td>नहीं</td>
        </tr>  
                            <tr>
                                <td>8</td>
                                <td>क्या निजी गोदाम, निजी फर्म, कंपनी, पार्टनरशिप फर्म, सहकारी संस्थाऐं अथवा अन्य संस्थाओं के उक्त गोदाम MPWLC के ऑनलाईन पोर्टल पर अनुबंध अवधि तक पंजीकृत है ?</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>                        
                </table>
      <table>
        <br /> 
          <tr>
                              <td>
 <b>13. सभी गोदाम परिसर हेतु निम्नानुसार सुविधाऐं अनिवार्य रूप से उपलब्ध कराना होंगी :-  </b>   
                               </td>
              </tr>
            <tr>
                              <td >
  i. कार्यालयीन व्यवस्था हेतु फर्नीचरयुक्त कक्ष एवं विद्युत व्यवस्था ।                   
                               </td>
              </tr>
           <tr>
                              <td >
     ii एक डाटा एन्ट्री ऑपरेटर जो ई-उपार्जन साफ्टवेयर तथा WHMS साफ्टवेयर में कार्य कर सके । उपार्जन अवधि में पूर्णकालिक ऑपरेटर उपलब्ध कराना होगा ।         
                               </td>
              </tr>
           <tr>
                              <td >
    iii     ऐसे गोदाम जिनकी भंडारण क्षमता 5000 मे.टन या अधिक है उन्हें गोदामों के संचालन एवं वैज्ञानिक भण्डारण हेतु एक अनुभवी तकनीकी कर्मचारी(शैक्षणिक योग्यता- बीएससी अथवा तकनीकी अनुभव न्यूनतम 02 वर्ष) उपलब्ध कराना होगा ।     
                               </td>
              </tr>
               <tr>
                              <td >
   iv. एक-एक नग डिजीटल नमी मापक यंत्र एवं पावर स्प्रेयर पंप (ISI Mark) उपलब्ध 
             कराना होगा।        
                               </td>
              </tr>
          <tr>
                              <td>
    v. उच्च गुणवत्ता के CCTV कैमरे (Night Vision सुविधा सहित) जिनकी मेमोरी न्यूनतम                
             1 माह की हो,  जिन्हें हमेशा चालू हालत में रखना अनिवार्य होगा, साथ ही रिकार्डिंग को 
         सुरक्षित रखेगा एवं प्रथम पक्षकार द्वारा चाहे जाने पर रिकार्डिंग उपलब्ध करायेगा ।  
                               </td>
              </tr>
          
          </table>
                          <br />
           <table>
                  <tr>
                      <td >
 <b>14. संयुक्त भागीदारी योजना वर्ष 2022-23 अंतर्गत गोदाम संचालकों द्वारा उपलब्ध करायी जाने वाली 
   अनिवार्य संसाधन सामग्री/सुविधाओं का विवरण:-</b>
                      </td>
                 </tr>
               <tr>
                     <td align="center" colspan="4">
             <div id="toexport" runat="server">
                         <p>
    <asp:GridView ID="AgreeGrid" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        Width="940px" OnDataBound="OnDataBound"
                    Font-Size="10pt" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="क्र.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="Facilities" HeaderText="विवरण" />
                                            <asp:BoundField DataField="Till_2K_MT" HeaderText="2000 मे.टन तक"/>
                                            <asp:BoundField DataField="Till_5K_MT" HeaderText="5000 मे.टन तक"/>
                                            <asp:BoundField DataField="Till_10K_MT" HeaderText="10000 मे.टन तक"/>                                    <asp:BoundField DataField="Till_20K_MT" HeaderText="20000 मे.टन तक"/>
                                            <asp:BoundField DataField="Is_Available" HeaderText="क्षमता अनुसार सामग्री उपलब्ध है/नहीं है"/>                                            
                                        </Columns>
                                        <FooterStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Height="25px" Font-Size="10pt" />
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="30px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                    </asp:GridView>
                                     </p></div>
                                    </td>
                 </tr>
</table>
       <tr>
                              <td>
   नोट:- 1. 20000 मे.टन क्षमता से अधिक वाले गोदामों में उपरोक्तानुसार वर्णित मात्रा अनुसार 
       2000मे.टन क्षमता के अनुपात में अतिरिक्त आवश्यक सामग्री एवं सुविधायें उपलब्ध कराना 
       अनिवार्य है ।
 
                               </td>
              </tr>
              <tr>
                              <td>
   <tr>
                              <td>
  2.     प्रत्येक परिसर हेतु पृथक-पृथक सामग्री/संसाधन अनिवार्यत: उपलब्ध कराना होगी । प्रमाणित किया जाता है कि मेरे द्वारा उल्लेखित बिन्दुओं का निरीक्षण किया गया ।
 
                               </td>
              </tr>
 
                               </td>
              </tr>
            

         

      <br />
               
                  <table>               
                                                      
                  <tr>
                   <td  >प्रमाणित किया जाता है कि निरीक्षण प्रतिवेदन में उल्लेखित बिन्दुओं का निरीक्षण पश्चात उक्त निजी/संस्थागत गोदाम को उपार्जित स्कंध के भण्डारण हेतु JVS गोदाम के लिए <asp:Label ID="Label46" runat="server" Text="___________________"></asp:Label>
                       &nbsp;(पात्र/अपात्र) किया जाता है।<br />
<br />
                  </td>                                 
                  </tr>
                  
                  <tr>
                   <td  >
          नोट :- यदि गोदाम अपात्र होता हे तो अपात्र के कारणों की फोटोग्राप्स रिकॉर्ड हेतु लिया जाएँ | लिया जाएँ | <br />
<br />
                  </td>                                 
                  </tr>                  

</table>
<table>   
    <tr>
<td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;स्थानीय शाखा प्रबंधक MPWLC</td><td style="width:200px">____________________</td>
    <td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;जिला प्रबंधक MPSCSC के प्रतिनिधि</td><td style="width:200px">____________________</td>
    <td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;उपपंजीयक के प्रतिनिधि</td><td style="width:200px">____________________</td>
</tr>
    <tr>
<td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नाम</td><td style="width:200px">____________________</td>
    <td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नाम</td><td style="width:200px">____________________</td>
    <td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नाम</td><td style="width:200px">____________________</td>
</tr>
<tr>
<td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;पद</td><td style="width:200px">____________________</td>
    <td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;पद</td><td style="width:200px">____________________</td>
    <td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;पद</td><td style="width:200px">____________________</td>
</tr>
    <tr></tr>
     <tr></tr>
     <tr></tr>
     <tr></tr>
     <tr></tr>
     <tr></tr>
        <tr>
<td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;स्थानीय ASO/JSO खाद्य विभाग</td>
            <td style="width:200px">____________________</td><td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;एस.डी.एम. के प्रतिनिधि</td><td style="width:200px">____________________</td>
</tr>
    <tr>
<td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नाम</td><td style="width:200px">____________________</td>
    <td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नाम</td><td style="width:200px">____________________</td>
</tr>
<tr>
<td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;पद</td><td style="width:200px">____________________</td>
    <td align="right" style="width:500px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;पद</td><td style="width:200px">____________________</td>
</tr>
</table>

               </div>  
                    <div><center>
                   <%-- <input id="Button1" name="Print" type="button" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;" value="Print"  onclick="PrintDiv();" onserverclick="Submit_Click" />--%>

                   </center></div>
                  </form>
               </div>
</body>
</html>
