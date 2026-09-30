<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehousePayment.aspx.cs" Inherits="JointVentureScheme_WarehousePayment" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Warehouse Facility</title>
      <meta charset="urf-8"/>
    <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
	<script type="text/javascript">
	    Cufon.replace('h1,h2,h3,h4,h5,#menu,#copy,.blog-date');
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

        .style3
        {
            height: 22px;
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
    border: 2px solid #008CBA;
}

.button6:hover {
    background-color: #008CBA;
    color: white;
}
    </style>        
</head>
<body>

<div id="bg" style="background-color:White">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="150" />
       
        <div>
                            <form id="form1" runat="server">
                                 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
                                <center>
                    <table style="width:100%; font-size:14px;">
<%--                        <tr >
                            
                            <td  colspan="4" style="background-color: #66CCFF" align="right"> 
                                <p style="font-size: medium; color: #008080;">&nbsp&nbsp&nbsp<asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/WarehouseHome.aspx"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton 
                                        ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Log out</asp:LinkButton>&nbsp;&nbsp;</p>
                            </td>
                        </tr>--%>
                        
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

                      <td colspan="4"   align="center">
                          <p style="font-size: medium; font-weight:bold; color: #008080; width:100%;">
                         &nbsp&nbsp Payment for Registration of Warehouse</p>
                      </td>
                                                </tr>
<tr>
<%--                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                         &nbsp&nbsp  </p>
                      </td>--%>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="Letf">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         Warehouse Details</p>
                      </td>                       

                  </tr>
                                       <tr>
                    <td ></td>
                    </tr>
                  <tr>
                    <td >
                       &nbsp&nbsp&nbsp Warehouse Name:
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblAuthPerson" runat="server"></asp:Label>
                    </td>
                          <td >
                              &nbsp;&nbsp;&nbsp; Registration No. :
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblRegNo" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr> 
                          <td >
                            &nbsp&nbsp&nbsp  Mobile Number:
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblMob" runat="server"></asp:Label>
                    </td>
                      <td >
                        &nbsp&nbsp&nbsp    Type of Applicant/Entity  :
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblAppType" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                  
                          <td >
                            &nbsp&nbsp&nbsp  District :
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                    </td>
                    </tr>
                                         <tr>
                    <td ></td>
                    </tr>
                   <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="Letf">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Warehouse Capacity & Payment Details</p>
                      </td>                       
                  </tr>
                     <tr>
                    <td ></td>
                    </tr> 
                  <tr>
                    <td >
                       &nbsp&nbsp&nbsp Total Capacity (In M.T) :
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblCapt" runat="server"></asp:Label>
                    </td>                     
                  </tr>
                  <tr>
                  
                   <td >
                       &nbsp&nbsp&nbsp Payable Registration Fee Rs :
                  </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblRegFee" runat="server"></asp:Label>
                    </td>                   
                  </tr>
                  <tr>
                   <td >
                       &nbsp&nbsp&nbsp Total charges Rs :
                  </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblTotalfee" runat="server"></asp:Label>
                    </td>                   
                  </tr> 
                                    <tr runat="server" visible="false">
                   <td class="style3" >
                       &nbsp&nbsp&nbsp Payment Status :
                  </td>
                    <td class="style3" >
                        <asp:Label ID="lblFeestatus" runat="server"></asp:Label>
                    </td>                   
                  </tr> 
                  <tr>

                    <td align="right" colspan="4">
                        <asp:Button ID="Button1" runat="server" Text="Click Here to Proceed Payment" 
                            class="button button2" Width="250px" Height="30px" onclick="Button1_Click" ></asp:Button>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    </td>
                  </tr>                                    
                     <tr>
                    <td style="color:Red" >&nbsp&nbsp Note :</td>
                    </tr> 
                    <tr>
                      <td colspan="4">
                          <p color:#008080;" style="color:Red">
                         &nbsp&nbsp 1. ऑनलाइन भुगतान करते समय रजिस्ट्रेशन आईडी एवं रजिस्ट्रेशन फीस की जानकारी सही प्रविष्टि करे । </p>
                         <p color:#008080;" style="color:Red">
                         &nbsp&nbsp 2. देय पंजीकरण शुल्क 40 पैसा/मेट्रिक टन । </p>
                        
                         <p color:#008080;" style="color:Red">
                         &nbsp&nbsp 3. रजिस्ट्रेशन फीस का पुस्टिकरण कार्यालीन दिवस के 24 घंटे में किया जाएगा । </p>                         
                         
                      </td>
                    </tr>                     
                   
                                
                    <tr>
                    <td ></td>
                    </tr>                                   
                    </table>
                                </center>
                                </form>
                    </div>
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
                                            <b>© 2018 &nbsp;National Informatics Centre.All Rights Reserved
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
</body>
</html>
