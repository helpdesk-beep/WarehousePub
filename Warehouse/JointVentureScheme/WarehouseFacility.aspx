<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseFacility.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="TribalGodown_Registration" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

        .style2
        {
            height: 11px;
        }

        .style3
        {
            height: 26px;
        }

        </style>
</head>
<body>

<div id="bg">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
       
        <div>
                            <form id="form1" runat="server">
                                 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
                                <center>
                    <table>
                        <tr >
                            
                            <td  colspan="4" style="background-color: #66CCFF"> 
                                <p style="font-size: medium; color: #008080;">&nbsp&nbsp&nbsp<asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/Neeti.aspx"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server">Log out</asp:LinkButton></p>
                            </td>
                        </tr>
                         <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                         &nbsp&nbsp Warehouse Owner Details </p>
                      </td>

                  </tr>
                                       <tr>
                    <td></td>
                    </tr>
                  <tr>
                    <td style="height:20px">
                       &nbsp&nbsp&nbsp Authorised Person:
                    </td>
                    <td>
                        <asp:Label ID="lblAuthPerson" runat="server"></asp:Label>
                    </td>
                          <td>
                           &nbsp&nbsp&nbsp   Registered Email ID:
                    </td>
                    <td>
                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                    <td style="height:20px">
                       &nbsp&nbsp&nbsp Date of Birth:
                    </td>
                    <td class="style2">
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </td>
                          <td class="style2">
                            &nbsp&nbsp&nbsp  Mobile Number:
                    </td>
                    <td class="style2">
                        <asp:Label ID="lblMob" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                    <td style="height:20px">
                        &nbsp&nbsp&nbsp    Type of Applicant/Entity  :
                    </td>
                    <td>
                        <asp:Label ID="lblAppType" runat="server"></asp:Label>
                    </td>
                          <td>
                            &nbsp&nbsp&nbsp  State-District :
                    </td>
                    <td>
                        <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                    </td>
                    </tr>
                                         <tr>
                    <td></td>
                    </tr>
                   <tr>
                      <td colspan="4" style="background-color: #66CCFF;" class="style2">
                          <p style="font-size: medium; color: #008080; width: 954px;">
                         &nbsp&nbsp Warehouse Geogrophical Information </p>
                      </td>
                  </tr>
                     <tr>
                    <td></td>
                    </tr> 
                  <tr>
                    <td>
                       &nbsp&nbsp&nbsp Warehouse Latitude  :
                    </td>
                    <td class="style3" >
                       <%-- <input id="txtlat" name="rname" runat="server" class="text" type="text" style="width:90px"/>--%>
                   <asp:TextBox ID="txtlat" runat="server" class="text" type="text" style="width:90px" ></asp:TextBox>
                    </td>
                     <td>
                      &nbsp&nbsp&nbsp  Warehouse Longitude  :
                    </td>
                    <td class="style3">
                       <%-- <input id="txtlong" name="rname" runat="server" class="text" type="text" style="width:90px" />--%>
                        <asp:TextBox ID="txtlong" runat="server" class="text" type="text" style="width:90px" ></asp:TextBox>
                    </td>
                  </tr>
                     <tr>
                    <td></td>
                    </tr>                    
                   <tr>
                      <td colspan="4" style="background-color: #66CCFF;" class="style2">
                          <p style="font-size: medium; color: #008080; width: 954px;">
                         &nbsp&nbsp Warehouse Additional Facilities Information </p>
                      </td>
                  </tr> 
                     <tr>
                    <td></td>
                    </tr> 
<tr>
                     <td>
                       &nbsp&nbsp&nbsp  Motorable Approach Road Type :
                    </td>
                    <td class="style3">
                         <asp:DropDownList ID="ddlRoadType" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">BT</asp:ListItem>
                            <asp:ListItem Value="2">CC</asp:ListItem>
                            <asp:ListItem Value="3">WBM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td>
                       &nbsp&nbsp&nbsp Width of Road (In Meters) :
                    </td>
                     <td class="style3">
                       <%-- <input id="txtRoadWidth" name="rname" runat="server" class="text" type="text" style="width:90px" />--%>
                         <asp:TextBox ID="txtRoadWidth" runat="server" class="text" type="text" style="width:90px" ></asp:TextBox>
                    </td>
                  </tr>                                      
                  <tr>
                     <td>
                       &nbsp&nbsp&nbsp  Shutter/Jali/Chanel Gate in Godown :
                    </td>
                    <td class="style3">
                         <asp:DropDownList ID="ddlGateType" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td>
                       &nbsp&nbsp&nbsp Number of Gate in Godown :
                    </td>
                     <td class="style3">
                        <%--<input id="txtNoGate" name="rname" runat="server" class="text" type="text" style="width:90px" />--%>
                        <asp:TextBox ID="txtNoGate" runat="server" class="text" type="text" style="width:90px" ></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      &nbsp&nbsp&nbsp  Power Supply (In Phase) :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlPowersuply" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="0">No Supply</asp:ListItem>
                            <asp:ListItem Value="1">1 Phase</asp:ListItem>
                            <asp:ListItem Value="2">2 Phase</asp:ListItem>
                            <asp:ListItem Value="3">3 Phase</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                            &nbsp&nbsp&nbsp     Is Godown free from Passing over of any tension electric line :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlTentionline" runat="server"
                            Width="100px" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                  <tr>
                    <td>
                      &nbsp&nbsp&nbsp  Water Facility :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlWaterFac" runat="server"
                            Width="100px" Height="27px"  >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                             &nbsp&nbsp&nbsp   Water Facility for spray and other usage :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlWaterSpry" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr> 
                  <tr>
                    <td>
                      &nbsp&nbsp&nbsp  CCTV Camera :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlCCTV" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                           &nbsp&nbsp&nbsp     Availability Of Fumigation & Pest Control Equipment :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlFumigation" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>                                     
                  <tr>
                    <td>
                      &nbsp&nbsp&nbsp  Guard With Guard Room :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlGuard" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                            &nbsp&nbsp&nbsp    Availability of Wooden Planks/Dunnage:
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlPlanks" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr> 
                    
                  <tr>
                    <td>
                      &nbsp&nbsp&nbsp Availability fo Fire Buckets :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlFireBuc" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                            &nbsp&nbsp&nbsp    Availability of Fire extinguisher with fire hydrants :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlFireExt" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr> 
                     <tr>
                    <td></td>
                    </tr>                    
                   <tr>
                      <td colspan="4" style="background-color: #66CCFF;" class="style2">
                          <p style="font-size: medium; color: #008080; width: 954px;">
                         &nbsp&nbsp Warehouse Hardware Information </p>
                      </td>
                  </tr> 
                     <tr>
                    <td></td>
                    </tr>                                                                                             
                  <tr>
                    <td>
                      &nbsp&nbsp&nbsp  Electronic Weighbridge :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlElectWeigh" runat="server"
                            Width="100px" Height="27px" AutoPostBack="True"
                            onselectedindexchanged="ddlElectWeigh_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                                        <td>
                       &nbsp&nbsp&nbsp&nbsp<asp:Label ID="Label2" runat="server" Text="Weighbridge is Certified By Controler :" Visible="false"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlWeighCertified" runat="server" Visible="false"
                            Width="100px" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp&nbsp&nbsp  <asp:Label ID="Label1" runat="server" Text="Weighbridge Capacity(In M.T) :" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeighCpt" runat="server" class="text" type="text" Visible="false" style="width:90px" ></asp:TextBox>
                       
                    </td>

                    </tr>
                  <tr>
                    <td>
                      &nbsp&nbsp&nbsp  Internet Connectivity :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlInternetCon" runat="server"
                            Width="100px" Height="27px" AutoPostBack="true" 
                            onselectedindexchanged="ddlInternetCon_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp&nbsp&nbsp<asp:Label ID="Label4" runat="server" Text="Connectivity Type :" Visible="false"></asp:Label></td>
                    <td >
                        <asp:DropDownList ID="ddlConType" runat="server"
                            Width="100px" Height="27px" Visible="false">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Fixed Broadband Connections</asp:ListItem>
                            <asp:ListItem Value="2">Mobile Internet</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    </tr>  
                  <tr>
                    <td class="style3">
                      &nbsp&nbsp&nbsp <asp:Label ID="Label3" runat="server" Text="Availability of Computer & Required Hardware :" Visible="false"></asp:Label> 
                    </td>
                    <td class="style3">
                        <asp:DropDownList ID="ddlHardwareAvl" runat="server"
                            Width="100px" Height="27px" Visible="false">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>   
                    <td class="style3">
                      &nbsp&nbsp&nbsp  <asp:Label ID="Label5" runat="server" Text="Year Of Installation :" Visible="false"></asp:Label> 
                    </td>
                    <td class="style3">
                        <asp:DropDownList ID="ddlInstallationyear" runat="server"
                            Width="100px" Height="27px" Visible="false">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="2018">2018</asp:ListItem>
                            <asp:ListItem Value="2017">2017</asp:ListItem>
                            <asp:ListItem Value="2016">2016</asp:ListItem>
                            <asp:ListItem Value="2015">2015</asp:ListItem>
                            <asp:ListItem Value="2014">2014</asp:ListItem>
                            <asp:ListItem Value="2013">2013</asp:ListItem>
                            <asp:ListItem Value="2012">2012</asp:ListItem>
                            <asp:ListItem Value="2011">2011</asp:ListItem>
                            <asp:ListItem Value="2010">2010</asp:ListItem>
                        </asp:DropDownList>
                    </td>                                      
                    </tr>   
                    <tr>
                    <td style="height:5px"></td>
                    </tr>                                   
                                      <tr>
                  
                    <td align="center" colspan="4">
                  
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="submit" 
                            onclick="btnsubmit_Click" ></asp:Button>

                    </td>
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
                                            <b>© 2015 &nbsp;National Informatics Centre.All Rights Reserved
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
