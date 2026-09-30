<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchPreInspection.aspx.cs" Inherits="JointVentureScheme_BranchPreInspection" %>

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
        
        .style3
        {
            height: 133px;
        }
        
        </style>
</head>
<body >
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
       <div id="ReportDiv" >
      
		<div class="wrap">
                 <form id="Form1" runat="server">
<%--                                                <p style="font-size: medium;color: #008080; background-color:#66CCFF;">  &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inspection Form&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                  &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp 
                                                    <asp:LinkButton ID="lnkLogout" runat="server" onclick="lnkLogout_Click" >Log out</asp:LinkButton>
                                                 </p>--%>
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
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
                          Print Godown Inpection Form</p>
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
                        <asp:ListItem Value="Kharif1819">Kharif 2018-19</asp:ListItem>
                         <asp:ListItem Value="Rabi1819">Rabi 2018-19</asp:ListItem>
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
          </td>
          </tr>
          </table>
          </div>
          <br />


  <div id="PrintDiv" style="font-size:medium;" runat="server" visible="false" >

        <div >

                    <table >
  
                      <tr>
                       <td colspan="4"  class="style2" align="center" style="width:954px">
                          <p style="font-size:14px; color:Black; font-weight:bold; text-align:center">
                         मध्यप्रदेश वेअरहाउस एंड लॉजिस्टिक कार्पोरेशन <br />
                         विपणन वर्ष 2018-19 में संयुक्त भागीदारी योजना के तहत निजी /संस्थागत गोदाम संचालक द्वारा <br />
                              ऑनलाइन आफर्ड की गयी क्षमता का संयुक्त निरीक्षण प्रतिवेदन <br />
                              क्षेत्रीय कार्यालय -&nbsp;<asp:Label ID="lblRegion" runat="server"></asp:Label>
                          </p>
                         <hr />
                      </td>
                      </tr>
                      <tr>
                      <td>
                     &nbsp;&nbsp;  Registration ID &nbsp;&nbsp;<input id="txtRegNo"  class="text" runat="server" style="width:150px; height:25px;"  readonly="readonly"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Godown ID (As Per WMS) &nbsp;&nbsp; <input id="Text1"  class="text" runat="server"  style="width:150px; height:25px;" />
                      <br /></td>
                      </tr>
                      
                      <tr>
                        <td colspan="2"> <br />(1) निरीक्षण दिनांक  :&nbsp;&nbsp; <asp:Label ID="lblNriDate" runat="server" Text="______________"></asp:Label> </td>
                    </tr>
                  <tr>
                      <td colspan="4">(2) जिले का नाम : &nbsp;&nbsp;<asp:Label ID="lbldistname" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      शाखा का नाम :&nbsp;&nbsp;<asp:Label ID="lblbranch" runat="server" Text="" Font-Underline="True"></asp:Label></td>
                  </tr>
             
                   <tr>
                       <td colspan="4" class="style3">
                           (3) निरीक्षणकर्ता अधिकारियों का विवरण :- <br />
                           <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.</b> जिला प्रबंधक MPSCSC/DMO मार्कफेड अथवा उनके प्रतिनिधि नाम  __________________________________
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;पद  _____________<br />
                           <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.</b> जिला आपूर्ति अधिकारी /खाद नियंत्रण अथवा उनके प्रतिनिधि नाम  ____________________________________
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; पद   _____________<br />
                           <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.</b> MPWLC के जिला स्तरीय नोडल अधिकारी अथवा संबंधित शाखा के शाखा प्रबंधक -(संयोजक) <br />
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; नाम   ____________________________________ पद    ________________<br />
                       </td>
                    </tr>
                    <tr>
                   
                    <td colspan="4"><br />

                      (4) वेअरहाउस का नाम : <asp:Label ID="lblWarehouseName" runat="server" Text="" Font-Underline="True"></asp:Label>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ऑफर गोदाम का  नंबर  :&nbsp <asp:Label ID="lblGdwnNo" runat="server" Text="" Font-Underline="True"></asp:Label> <br />
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;वेअरहाउस का पता :&nbsp; <asp:Label ID="lblWareAddress" runat="server" Text="" Font-Underline="True"></asp:Label><br />
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;तहसील :&nbsp;<asp:Label ID="lblTehsil" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ज़िला : &nbsp;<asp:Label ID="lblDistrict2" runat="server" Text="" Font-Underline="True"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;अक्षांश :&nbsp;<asp:Label ID="lbllongitude" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; देशान्त : &nbsp;<asp:Label ID="lbllatitude" runat="server" Text="" Font-Underline="True"></asp:Label></td>
                    </tr>
                        <tr>
                            <td>
 &nbsp;&nbsp;&nbsp;&nbsp; वेअरहाउस परिसर का कुल क्षेत्रफल : &nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Text="______________________"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />

                            </td>
                        </tr>
                        <tr>
                   
                    <td>
&nbsp;&nbsp;&nbsp;&nbsp; वेअरहाउस कार्यालय का मोबाइल नंबर : &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblOfficeMob" runat="server" Text="" Font-Underline="True"></asp:Label><br/>
&nbsp;&nbsp;&nbsp;&nbsp; वेअरहाउस प्रभारी का मोबाइल नंबर :  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblInchMob" runat="server" Text="" Font-Underline="True"></asp:Label><br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;वेअरहाउस की निकटतम शाखा का नाम  :&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblnearbranch" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; गोदाम से दूरी(km) :&nbsp;<asp:Label ID="lblnearbranchDist" runat="server" Text="" Font-Underline="True"></asp:Label> <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;वेअरहाउस से निकटतम रेल्वे रेक पॉइंट  का नाम : &nbsp;<asp:Label ID="Label9" runat="server" Text="_________________"></asp:Label> &nbsp;&nbsp;दूरी(km) :&nbsp;&nbsp;<asp:Label ID="Label8" runat="server" Text="______"></asp:Label>
</td>
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
                              (5) वेअरहाउस  संचालक  का नाम :&nbsp;<asp:Label ID="lblAuthPerson" runat="server" Text="" Font-Underline="True"></asp:Label><br />
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;वेअरहाउस  संचालक  का पता : &nbsp;<asp:Label ID="Label18" runat="server" Text="___________________________" ></asp:Label><br />

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PAN : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpan" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Aadhar No. : &nbsp;<asp:Label ID="lblaadhar" runat="server" Text="" Font-Underline="True"></asp:Label><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;मोबाइल नंबर :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmob" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Email : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblemail" runat="server" Text="" Font-Underline="True"></asp:Label><br />

 
                               </td>
                    </tr>
                        <tr>
                        <td ><br />
(6) JVS में ऑनलाइन ऑफर दिनांक :&nbsp;<asp:Label ID="lblofferdt" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; समय :&nbsp; <asp:Label ID="lbltime" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;(ऑनलाइन ऑफर अनुसार)<br />
                             
                               </td>
                    </tr>
                        <tr>
                        <td colspan="4">
(7) JVS में ऑनलाइन ऑफर WDRA गोदाम अथवा गैर WDRA में से जो उल्लेख हो
    लिखा जाये :&nbsp; <asp:Label ID="lblOferscheme" runat="server" Text="" Font-Underline="True"></asp:Label>

                               </td>
                    </tr>
                        <tr>
                        <td >
(8) ऑफर की गयी भंडारण क्षमता का प्रकार - (आंशिक/पूर्ण ) :&nbsp;<asp:Label ID="lbloffertype" runat="server" Text="" Font-Underline="True"></asp:Label><br />

                               </td>
                    </tr>
                        <tr>
                        <td >
(9) JVS में ऑफर की गयी क्षमता :&nbsp; <asp:Label ID="lbloffercpt" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;में टन <br />

                              
                               </td>
                    </tr>
                      
                  <tr>
                   
                    <td ><br />
                       
(10)निरीक्षण में पाई गयी जानकारी का विवरण
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
                            <td>निर्माण वर्ष </td>
                            <td><asp:Label ID="lblConYear" runat="server" Text="" Font-Underline="True"></asp:Label></td>
                            <td></td>
                                </tr>
<%--                            <tr>
                                <td>6</td>                           
                            <td>  भण्डारण प्रकार (Covered / Permanent (CAP) / Temporary (CAP) / Silo Bag  / Steel Silo/ others)</td>
                            <td></td>
                            <td></td>
                            </tr>--%>
                            <tr>
                            <td>6</td>                           
                            <td> गोदाम की एक दिन में अनुमानित उतराई क्षमता (मे.टन) </td>
                            <td></td>
                            <td></td>
                            
                                </tr>
                            <tr>
                            <td>7</td>                           
                            <td> वर्तमान में गोदाम में संग्रहित स्कंधो का नाम </td>
                            <td></td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>8</td>                           
                            <td> गोदाम में संग्रहित स्कंध जमाकर्ता का नाम (शासकीय-MPSCSC/Markfed/Nafed,Private,Other) </td>
                            <td></td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>9</td>                           
                            <td> वर्तमान में गोदाम में संग्रहित स्कंध की क्षमता (मे.टन) </td>
                            <td></td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>10</td>                           
                            <td> वर्तमान में गोदाम की रिक्त क्षमता (मे.टन) (Vacant Capacity) </td>
                            <td></td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>11</td>                           
                            <td> क्या गोदाम की रिक्त क्षमता offer की गयी क्षमता से कम है?  </td>
                            <td>YES/NO</td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>12</td>                           
                            <td> क्या मौजूदा रिक्त क्षमता (Vacant Capacity)1800(मे.टन)से कम है, और संग्रहीत स्कंध का  जमाकर्ता सरकारी एजेंसी नहीं  है?</td>
                            <td>YES/NO</td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>13</td>                           
                            <td> क्या मौजूदा रिक्त क्षमता (Vacant Capacity)500(मे.टन) से कम है, और संग्रहीत स्कंध का  जमाकर्ता सरकारी एजेंसी है? </td>
                            <td>YES/NO</td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>14</td>                           
                            <td> गोदाम में गेट्स की संख्या </td>
                            <td></td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>15</td>                           
                            <td> गोदाम में स्टैक्स की संख्या की स्वीकार्य सीमा </td>
                            <td></td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>16</td>                           
                            <td> अग्नि हाइड्रंट्स  के साथ आग बुझाने की पर्याप्त उपलब्धता एवं संख्या</td>
                            <td>YES/NO</td>
                            <td></td>
                                </tr>
                            <tr>
                            <td>17</td>                           
                            <td> गोदाम में फायर बकेट की पर्याप्त उपलब्धता एवं संख्या</td>
                            <td>YES/NO</td>
                            <td></td>
                                </tr>
                            </table>
                    <%--<tr>
                     
                    <td  >
                       Date of Birth :
                    </td>
                    <td >
                        <asp:Label ID="lblDOB" runat="server"></asp:Label>
                    </td>
                   
                    </tr>--%>
                    <table>
                    <tr>
                     
                    <td  ><br />
                      (11) गोदाम बैंक खाता विवरण ((ऋण खाता)<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.	बैंक खाता संख्या :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblAccNo" runat="server" Text="" Font-Underline="True"></asp:Label><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.	आई०ऍफ़०एस० कोड  :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblIFSC" runat="server" Text="" Font-Underline="True"></asp:Label><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.	बैंक और शाखा का नाम  : &nbsp;&nbsp;<asp:Label ID="lblBankName" runat="server" Text="" Font-Underline="True"></asp:Label><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.	क्या बैंक विवरण सही हैं (पास बुक विवरण के साथ जांच करें) : &nbsp;<asp:Label ID="Label14" runat="server" Text="" Font-Underline="True"></asp:Label><br />

                    </td>
                     
                    </tr>
                    <tr>
                    <td  ><br />
                      (12) गोदाम लायसेंस का विवरण <br />
                        
(12.1) WDRA लायसेंस:- संयुक्त निरीक्षण टीम द्वारा उक्त वेअरहाउस के WDRA लायसेंस 
का निरीक्षण दिनांक को  अवलोकन किया गया परीक्षणोपरांत विवरण निम्नानुसार है:- <br />
WDRA लायसेंस नंबर  <asp:Label ID="lblWDRALicNo" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;जारी दिनांक 
                                <asp:Label ID="Label1" runat="server" Text="________"></asp:Label>&nbsp;&nbsp;वैधता दिनांक :  <asp:Label ID="lblWDRALicdate" runat="server" Text="" Font-Underline="True"></asp:Label><br />
      (छायाप्रति संलग्न)         <br />
      यदि WDRA लायसेंस वैध नहीं है तो क्या इसे नवीनीकरण हेतु आवेदन किया गया है?तो उसका आवेदन क्रमांक <asp:Label ID="Label3" runat="server" Text="_____________________" ></asp:Label> आवेदन दिनांक <asp:Label ID="Label4" runat="server" Text="_____________" ></asp:Label>
      (छायाप्रति संलग्न)         <br />
      
      
                        (12.2) WDRA अधिकृत एक्रीडेशन एजेंसी का नाम  <asp:Label ID="Label29" runat="server" Text="_____________________________________" ></asp:Label><br />
                        एवं जारी प्रमाण पत्र का क्रमांक  <asp:Label ID="Label30" runat="server" Text="______________" ></asp:Label> जारी दिनांक   <asp:Label ID="Label31" runat="server" Text="______________"></asp:Label>    <br />
                    (12.3) यदि WDRA  लायसेंस नहीं है तो निम्न दस्तावेज/अर्हताऐं का निरीक्षण दिनांक को  अवलोकन किया गया:- संयुक्त निरीक्षण टीम द्वारा वेअरहाउस लायसेंस का परीक्षणोपरांत विवरण निम्नानुसार है:- <br />
                        वेअरहाउस लायसेंस नंबर  <asp:Label ID="lblstateLic" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;जारी दिनांक 
                                <asp:Label ID="Label2" runat="server" Text="_________"></asp:Label>&nbsp;&nbsp;वैधता दिनांक  <asp:Label ID="lblStateLicExpDate" runat="server" Text="" Font-Underline="True"></asp:Label><br />
     (छायाप्रति संलग्न)<br />

  यदि वेअरहाउस लायसेंस वैध नहीं है तो क्या इसे नवीनीकरण हेतु आवेदन किया गया है?तो उसका आवेदन क्रमांक <asp:Label ID="Label5" runat="server" Text="_____________________" ></asp:Label> आवेदन दिनांक <asp:Label ID="Label7" runat="server" Text="_____________" ></asp:Label>
      (छायाप्रति संलग्न)         <br />
    
                    </td>
                                                                             
                  </tr>
                        <tr>
                            <td><br />(13) गोदाम बीमा का विवरण
                                <br />
                                (13.1) गोदाम की बिल्डिंग के बीमा पॉलिसी क्रमांक
                                <asp:Label ID="Label35" runat="server" Text="_______________" ></asp:Label>&nbsp;&nbsp; बीमित राशि रू.
                                <asp:Label ID="Label40" runat="server" Text="__________" ></asp:Label>
    <br />  जारी दिनांक 
                                <asp:Label ID="Label36" runat="server" Text="___________________"></asp:Label>&nbsp;&nbsp;वैधता दिनांक<asp:Label ID="Label41" runat="server" Text="_________________"></asp:Label>
      <br />
      बीमा पॉलिसी जारी करने वाली एजेंसी का नाम
                                <asp:Label ID="Label42" runat="server" Text="_______________________________"></asp:Label><br />

                                (13.2) गोदाम की प्रस्तावित क्षमता में भण्डारित होने वाले स्कंध की बीमा पॉलिसी क्रमांक
                                <asp:Label ID="Label37" runat="server" Text="__________________" ></asp:Label><br />    गोदाम की प्रस्तावित क्षमता में भण्डारित होने वाले स्कंध की बीमित राशि रू.
                                <asp:Label ID="Label43" runat="server" Text="______________"></asp:Label>
     <br /> जारी दिनांक 
                                <asp:Label ID="Label38" runat="server" Text="__________________"></asp:Label>&nbsp;&nbsp;&nbsp;वैधता दिनांक<asp:Label ID="Label44" runat="server" Text="_________________" ></asp:Label>
     <br />
      बीमा पॉलिसी जारी करने वाली एजेंसी का नाम
                                <asp:Label ID="Label45" runat="server" Text="_________________________________" ></asp:Label>
<br />बीमित राशि रू. 20,000 प्रति (मे.टन)(प्रस्तावित क्षमता पर आंकलित) की दर से कम तो नहीं है? पर्याप्त है / पर्याप्त नहीं है
                                <asp:Label ID="Label39" runat="server" Text="________"></asp:Label>(छायाप्रति संलग्न) <br />

      यदि बीमा पॉलिसी नहीं है तो शपथ पत्र जमा कराए जिसमे उल्लेख हो की अग्रीमेंट के समय तक आवश्यक वैध  बीमा पॉलिसी उपलब्ध करा ली जावेगी    (छायाप्रति संलग्न)         <br />       

                            </td>

                        </tr>
                    <tr>
                     
                    <td ><br /> 
                       (14) गोदाम परिसर में उपलब्ध सुविधाओं का विवरण:-<br/>
                    </td>
                   
                    </tr>
                    </table>
                        <table border="1">
                           <tr>
                                <td>1</td>
                                <td>गोदाम के प्रत्येक शटर के अलावा अतिरिक्त रूप से अन्दर की ओर ’’जालीदार शटर’’ है ?</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>2</td>
                            <td>गोदाम परिसर /संलग्न परिसर  में मानक क्षमता का चालू हालत में प्रमाणित इलेक्ट्रानिक वेब्रिज । </td>
                            <td>हाँ</td>
                            <td>नहीं</td>
                                </tr>
                            <tr>
                                <td>3</td>
                                <td>भण्डारित स्कंध के कीटोपचार हेतु संबंधित गोदाम परिसर पर फ्यूमीगेषन कव्हर (IS 14611.1998 or  BIS मानक - Up to date ammendment )सेण्ड स्नेक्स सहित, मानव संसाधन एवं पावर स्प्रेयर पंप आदि उपलब्ध हैं ?  </td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>4</td>
                            <td>प्रत्येक गोदाम परिसर में कम से कम 200 किलो क्षमता तक के प्रमाणित इलेक्ट्रानिक बीम स्केल उपलब्ध कराना होंगे ।   </td>
                            <td>हाँ</td>
                            <td>नहीं</td>
                                </tr>
                            <tr>
                                <td>5</td>
                                <td>गोदाम परिसर में ISI मार्क के डिजिटल नमी मापक यंत्र उपलब्ध है ?</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>6</td>
                            <td>गोदाम में भण्डारित स्कंध की सुरक्षा-व्यवस्था हेतु उच्च गुणवत्ता के CCTV कैमरे (Night Vision सुविधा सहित) जिसकी मेमोरी दो माह तक सुरक्षित  हैं ?   </td>
                            <td>हाँ</td>
                            <td>नहीं</td>
                                </tr>
                            <tr>
                                <td>7</td>
                                <td>गार्ड रूम के साथ सुरक्षा गार्ड की 24X7 उपलब्धता है </td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>8</td>
                            <td>मोटर योग्य सड़क का प्रकार  (BT/CC/WBM)</td>

                                </tr>
                            <tr>
                                <td>9</td>
                                <td>मोटर योग्य सड़क की चौड़ाई (in Meter)</td>
                            </tr>
                            <tr>
                            <td>10</td>
                            <td>सभी मौसमों के लिए रोड की गुणवत्ता संतोषजनक है?</td>
                            <td>हाँ</td>
                            <td>नहीं</td>
                                </tr>
                            <tr>
                                <td>11</td>
                                <td>गोदाम में चारों ओर सीमा में दीवार है और प्रवेश और निकास द्वार है?  </td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>12</td>
                            <td>गोदाम में उपलब्ध खुली जगह (> 25000Sq. Ft)</td>
                                </tr>
                            <tr>
                                <td>13</td>
                                <td>गोदाम पिछले साल खरीद केंद्र के रूप में काम किया है? </td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>14</td>
                            <td>यदि हां, तो पिछले साल  कितनी मात्रा की खरीदी गई(in MT) </td>

                                </tr>
                            <tr>
                                <td>15</td>
                                <td>पर्याप्त लकड़ी के तख्ते / डनेज और अन्य उपयोग योग्य सामान की उपलब्धता है?  </td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>16</td>
                            <td>पेयजल सुविधा उपलब्ध है? </td>
                            <td>हाँ</td>
                            <td>नहीं</td>
                                </tr>
                            <tr>
                                <td>17</td>
                                <td>गोदाम में स्प्रे और अन्य उपयोग के लिए जल की सुविधा उपलब्ध है?</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>18</td>
                            <td>अनुभवी तकनीकी संसाधन (बीएससी और 02 साल का अनुभव ) > 5000MT Godown</td>
                            <td>हाँ</td>
                            <td>नहीं</td>
                                </tr>
                            <tr>
                                <td>19</td>
                                <td>क्या धूमन और कीट नियंत्रण के लिए सुविधाएं/उपकरण मानकों के अनुसार हैं और गोदाम में उपलब्ध हैं?</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>20</td>
                            <td>जब आवश्यक हो तो गोदाम में पर्याप्त श्रमिक(labour) उपलब्ध होगी?</td>
                            <td>हाँ</td>
                            <td>नहीं</td>
                                </tr>
                            <tr>
                                <td>21</td>
                                <td>विद्युत आपूर्ति की उपलब्धता</td>
                                <td>1 Phase</td>
                                <td>3 Phase</td>
                            </tr>
                            <tr>
                            <td>22</td>
                            <td>गोदाम को किसी भी तनाव विद्युत लाइन से गुजरने से मुक्त होना चाहिए और इस तरह की रेखाओं से गुजरने की स्थिति में, भंडारण संरचना की योजना बनाते समय प्रासंगिक विद्युत कोड प्रावधानों को ध्यान में रखा जाना चाहिए। गोदाम गैस / तेल पाइप लाइनों से मुक्त होना चाहिए।</td>
                            <td>हाँ</td>
                            <td>नहीं</td>
                                </tr>
                            <tr>
                                <td>23</td>
                                <td>इलेक्ट्रॉनिक वेब्रिज (तुलाचौकी)</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>24</td>
                            <td>इलेक्ट्रॉनिक वेब्रिज की क्षमता (in MT)</td>
                                </tr>
                            <tr>
                                <td>25</td>
                                <td>क्या इलेक्ट्रॉनिक वेब्रिज  नियंत्रक ( नाप तौल) द्वारा प्रमाणित हैं? कैलिब्रेशन प्रमाणपत्र जांच करें?</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>26</td>
                            <td>कैलिब्रेशन प्रमाणपत्र जारी करने की तिथि प्रविष्ट करें?</td>
                         
                                </tr>
                            <tr>
                                <td>27</td>
                                <td>यदि इलेक्ट्रॉनिक वेब्रिज परिसर में नहीं है, तो कोई दूसरा प्रमाणित इलेक्ट्रॉनिक वेब्रिज गोदाम से लगभग 500 मीटर की दूरी पर उपलब्ध है?</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                            <tr>
                            <td>28</td>
                            <td>इंटरनेट कनेक्टिविटी</td>
                            <td>हाँ</td>
                            <td>नहीं</td>
                                </tr>
                            <tr>
                                <td>29</td>
                                <td>कनेक्टिविटी का प्रकार</td>
                            </tr>
                            <tr>
                            <td>30</td>
                            <td>कंप्यूटर और आवश्यक हार्डवेयर की उपलब्धता</td>
                            <td>हाँ</td>
                            <td>नहीं</td>
                                </tr>
                            <tr>
                                <td>31</td>
                                <td>कंप्यूटर ऑपरेटर की उपलब्धता</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
                        </table>
                        <table>
                  <tr></br/>
                   
                      <td>
                       
(15) <b> गोदाम के ’’अनुपयुक्तता’’ होने के आवश्यक बिन्दु:- </b><br /></br/>
<table border="1">
    <tr>
                            <td style="width:20px">1</td>
                            <td>क्या गोदाम निर्माणाधीन है ?</td>
                            <td style="width:40px">हाँ</td>
                            <td style="width:40px">नहीं</td>
        </tr>
                            <tr>
                                <td>2</td>
                                <td>क्या गोदाम विवादग्रस्त है ?</td>
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
                                <td>क्या गोदाम में शासकीय स्कंध के अतिरिक्त पूर्व से स्कंध भण्डारित है ?  </td>
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
                                <td>क्या ऑनलाइन ऑफर संबंधी जानकारी गलत दी गयी है ?</td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                            </tr>
    <tr>
                            <td>7</td>
                            <td>क्या अनिवार्य सुविधा में मानक स्तर की डनेज शीट, कम्प्यूटर सिस्टम, अग्निशामक यंत्र और फायर बकेट्स है एवं बारहमासी (All Weather Approach Road) पहुंचमार्ग जो कम से कम WBM स्तर का हो, यह सभी अनिवार्य सुविधा उपलब्ध नहीं कराई गई हैं?        </td>
                            <td>हाँ</td>
                            <td>नहीं</td>
        </tr>
                            <tr>
                                <td>8</td>
                                <td>क्या गोदाम खरीफ सीजन 2017-18 में आॅफर होने के वाबजूद आवश्यकता होने पर गोदाम संचालक द्वारा अनुबंधित नहीं हुआ है।   </td>
                                <td>हाँ</td>
                                <td>नहीं</td>
                                </tr>
                </table>
                <br />
                          यदि उपरोक्त 08 बिन्दुओं में से कोई भी एक बिन्दु की जानकारी ’’हाॅं’’ होती है तो वह गोदाम उपार्जित स्कंध के भण्डारण हेतु ’’अनुपयुक्त’’ होगा । <br />
                           
                      </td>
                     
                  </tr>  
                  </table>  
                  <table>               
                  <tr>
                       
                    <td  ><br />
                       (16) 	उपार्जन केन्द्र की भौगोलिक सीमा(अर्थात् उपार्जन केन्द्र से संबंद्ध सहकारी समिति/समितियों की भौगोलिक सीमा)स्थापित खरीदी केन्द्रो का नाम एवं गोदाम से दूरीः-<br /> 
                    </td>
                                  
                  </tr>
                        <table border="1" >
                            <tbody>
                                <th>क्रमांक</th>
                                <th>खरीदी केन्द्र का नाम</th>
                                <th>गोदाम से दूरी(km)</th>
                                
                            </tbody>
                            <td>1</td>                           
                            <td></td>
                            <td></td>
                                                        </tbody>
                            <td>2</td>                           
                            <td></td>
                            <td></td>
                            
                                                        </tbody>
                            <td>3</td>                           
                            <td></td>
                            <td></td>
                           </table> 
                  <tr>
                    <td  ><br />
                      (17)  उपार्जन केन्द्र की भौगोलिक सीमा(अर्थात् उपार्जन केन्द्र से संबंद्ध सहकारी समिति/समितियों की भौगोलिक सीमा) खरीदी केन्द्र नही होने की स्थिति में नजदीकी खरीदी परिधि (उसी जिलें में अथवा नजदीकी जिले) की खरीदी केन्द्र का नाम एवं गोदाम से दूरीः-</br>
                    <table border="1" >
                            <tbody>
                                <th>क्रमांक</th>
                                <th>खरीदी केन्द्र </th>
                                <th>नजदीकी जिला</th>
                                <th>गोदाम से दूरी(km)</th>
                                
                            </tbody>
                            <td>1</td>                           
                            <td></td>
                            <td></td>
                            <td></td>
                            
                                                        </tbody>
                            <td>2</td>                           
                            <td></td>
                            <td></td>
                            <td></td>
                                                        </tbody>
                            <td>3</td>                           
                            <td></td>
                            <td></td>
                            <td></td>

                           </table> 
                    </td>
                                 
                  </tr>
                  <%--<tr>
                  
                    <td   >
                       Email ID :
                    </td>
                    <td >
                        <asp:Label ID="lblWEmail" runat="server"></asp:Label>
                    </td>                   </tr>--%>
                                                      
                  <tr>
                   
                   <td  ><br />
                    (18)  निरीक्षण के दौरान निम्नानुसार बिन्दुओं पर फोटोग्राफ्स लिये जाकर प्रतिवेदन के साथ संलग्न करें <br />
1.	एप्रोच रोड की फोटो<br />
2.	गोदाम बिल्डिंग के अगले भाग की फोटो<br />
3.	गोदाम बिल्डिंग के पीछे के भाग की फोटो<br />
4.	गोदाम के अन्दर से छत का फोटो<br />
5.	गोदाम के फर्श का फोटो<br />
6.	कार्यालय का फोटो<br />
7.	सी.सी.टी.व्ही. कैमरा की फोटो<br /><br />
नोट:- फोटोग्राफ्स इस तरह से लिये जायें, जिसमें समिति सदस्य, गोदाम के प्रतिनिधि, सीसीटीव्ही कैमरे, धर्मकांटा, 
बाउण्ड्रीवाल /फेंसिंग एवं शटर आदि भी प्रदर्शित हो । <br /><br />
प्रमाणित किया जाता है कि संयुक्त निरीक्षण प्रतिवेदन में उल्लेखित बिन्दुओं का गठित टीम द्वारा संयुक्त निरीक्षण पश्चात उल्लेखित समस्त बिन्दुओं के आधार पर उक्त निजी/संस्थागत गोदाम को उपार्जित स्कंध के भण्डारण हेतु JVS गोदाम <asp:Label ID="Label46" runat="server" Text="___________________"></asp:Label>&nbsp;&nbsp;&nbsp;हेतु (उपयुक्त/अनुपयुक्त)<asp:Label ID="Label47" runat="server" Text="____________________"></asp:Label>&nbsp;&nbsp;किया जाता है।<br />

                  </td>
                                                          
                  </tr>
                    <tr>
                    <td style="height:5px"></td>
                    </tr> 

</table>
<table>                  
                <tr><td colspan="4" class="style2" >   <br /> नोट:-
<p>(1)	जो लागू न हो, उसे  क्रॉस(X) कर दिया जाये । </p>
<p>(2)	समिति के सदस्यों द्वारा प्राप्त प्रतिवेदन के प्रत्येक पृष्ठ पर हस्ताक्षर किये जाए। </p>
<p>संलग्न:- उपरोक्तानुसार दस्तावेजों की छायाप्रति एवं फोटोग्राफ्स</p> 
</td></tr>
<tr>
<td style="width:200px">(1)	हस्ताक्षर </td><td style="width:200px">____________________</td><td>(2)	हस्ताक्षर </td><td style="width:200px">____________________</td>
</tr>
<tr>
<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नाम </td><td style="width:200px">____________________</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नाम </td><td style="width:200px">____________________</td>
</tr>
<tr>
<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MPWLC शाखा</td><td style="width:200px">____________________</td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; जिला प्रबंधक/डीएमओ  MPSCSC / Markfed </td>
</tr>
<tr>
<td></td><td style="width:200px"></td><td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;जिला </td><td style="width:200px">____________________</td>
</tr>
<tr>
<td>(3)	हस्ताक्षर </td><td style="width:200px">____________________</td>
</tr>
<tr>
<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नाम </td><td style="width:200px">____________________</td>
</tr>

<tr>
<td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DSC/DSO  अथवा उनका प्रतिनिधि </td><td style="width:200px"></td>
</tr>
<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;स्थान</td><td style="width:200px">____________________<br /></td></tr>
<tr>
<td>(4)	हस्ताक्षर </td><td style="width:200px">____________________</td><td>(5)	हस्ताक्षर </td><td style="width:200px">____________________</td>
</tr>
<tr>
<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नाम </td><td style="width:200px">____________________</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नाम </td><td style="width:200px">____________________</td>
</tr>
<tr>
<td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;गोदाम संचालक के प्रतिनिधि  </td><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;गोदाम पर उपलब्ध कोई अन्य  व्यक्ति</td>
</tr>
<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;स्थान</td><td style="width:200px">____________________<br /></td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;स्थान</td><td style="width:200px">____________________<br /></td></tr>


<%--<tr>
<td>
                       (1)	हस्ताक्षर <asp:Label ID="Label48" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(2) हस्ताक्षर <asp:Label ID="Label51" runat="server" Text="" Font-Underline="True"></asp:Label><br /></td></tr>
<tr><td>	 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;नाम <asp:Label ID="Label49" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; नाम <asp:Label ID="Label52" runat="server" Text="" Font-Underline="True"></asp:Label><br /></td></tr>
	<tr><td> MPWLC  शाखा <asp:Label ID="Label50" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;		   जिला प्रबंधक/डीएमओ  MPSCSC / Markfed <br />   </td></tr>  
                     <tr><td> जिला<asp:Label ID="Label53" runat="server" Text="" Font-Underline="True"></asp:Label>
<tr><td>(3)	हस्ताक्षर <asp:Label ID="Label54" runat="server" Text="" Font-Underline="True"></asp:Label>  </td></tr>           	     
	<tr><td>नाम<asp:Label ID="Label55" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	DSC/DSO  अथवा उनका प्रतिनिधि
	स्थान <asp:Label ID="Label56" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

(4)    हस्ताक्षर <asp:Label ID="Label57" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;              (5) हस्ताक्षर <asp:Label ID="Label60" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
      नाम<asp:Label ID="Label58" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                  नाम<asp:Label ID="Label61" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      गोदाम संचालक के प्रतिनिधि             गोदाम पर उपलब्ध कोई अन्य ’’तटस्थ’’ व्यक्ति
      स्थान <asp:Label ID="Label59" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           	  स्थान <asp:Label ID="Label62" runat="server" Text="" Font-Underline="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;


                  </td>
                                       
                  </tr>--%>
                  </table>
                  
                  <%--<tr>
                   
                   <td  >
                       Nearest Branch of MPWLC :
                  </td>
                    <td>
                        <asp:Label ID="lblWBranch" runat="server"></asp:Label>
                    </td>                                       
                  </tr> 
                  <tr>
                   <td  >
                       Distance from Nearest Branch of MPWLC (in KM) :
                  </td>
                    <td >
                        <asp:Label ID="lblWBranchDist" runat="server"></asp:Label>
                    </td>                  
                  </tr>
                    <tr  >
                    <td >
                       Warehouse/Office Address with Postal Address :
                    </td>
                    <td>
                        <asp:Label ID="lblWAddres" runat="server"  Width="350px" Style="word-wrap: break-word" ></asp:Label>
                    </td>                     
                    </tr>                   
                  <tr>
                   
                      <td colspan="4" class="style2">
                          <p style="font-size: medium; color: #008080; font-weight:bold; background-color:#66CCFF;text-decoration: underline;">
                        Registered Capacity & Payment Details </p>
                      </td>
                     
                  </tr>
                  <tr>
                   
                    <td   >Total Capacity (In M.T) :</td>
                    <td >
                        <asp:Label ID="lblRegCpt" runat="server"></asp:Label>
                    </td>                    
                  </tr>
                  <tr>
                   
                   <td  >Payable Total charges Rs :</td>
                    <td >
                        <asp:Label ID="lblRegFee" runat="server"></asp:Label>
                    </td>                   
                  </tr>
                  <%--<tr>
                   
                   <td >Total charges Rs : </td>
                    <td >
                        <asp:Label ID="lblTotalFee" runat="server"></asp:Label>
                    </td>                   
                  </tr>--%>        
              <%--       <tr>
                     <td class="style3"></td>
                     <td style="color:Red"> Note :</td>
                    </tr> 
                    <tr>
                     
                      <td colspan="4">
                          <p color: #008080;" style="color:Red">
                         &nbsp&nbsp 1)&nbsp देय पंजीकरण शुल्क 40 पैसा /मेट्रिक टन क्षमता अथवा कम से कम पंजीयन शुल्क १००० रूपये (केवल निजी गोदाम संचालको हेतु) । </p>
                      </td>
                    </tr>                     
                    <tr>
                     
                      <td colspan="4">
                          <p color: #008080;" style="color:Red">
                         &nbsp&nbsp 2) &nbspपोर्टल शुल्क 165 रुपये </p>
                      </td>
                    </tr>
                    <tr>
                     
                      <td colspan="4">
                          <p color: #008080;" style="color:Red">
                         &nbsp&nbsp 3)&nbsp Formula for Capacity = [Length*Breadth*(Height-3)/80] All Dimensions for Length,Breadth and height should be in feet </p>
                      </td>
                    </tr> --%>                                       

<%-------------------%>                    
                        
                        
                    </div>
               </div>
                    <center>
                   <%-- <input id="Button1" name="Print" type="button" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;" value="Print"  onclick="PrintDiv();" onserverclick="Submit_Click" />--%>
                    <asp:Button ID="Button1" runat="server" Text="Print" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;"  
                      OnClientClick="PrintDiv();" onclick="Button1_Click" />
                   </center>
                  </form>
               </div>
        </div>
</body>
</html>