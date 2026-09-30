<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Neeti.aspx.cs" Inherits="TribalGodown_Neeti" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Godown cunstruction Neeti</title>
      <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
<%--<link href="css/bootstrap.css" rel="stylesheet"/>
<link href="css/bootstrap-responsive.css" rel="stylesheet"/>--%>
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
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

</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="bg">
		<div class="wrap">

            <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
            <div style="background-color: #66CCFF">
                 <p style="font-size: medium; color: #008080;"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Log out</asp:LinkButton></p>
            </div>
               <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">  <asp:LinkButton ID="link1" Text="Warehouse Registration" runat="server" PostBackUrl="~/JointVentureScheme/WarehouseRegistration.aspx"></asp:LinkButton>
                          </p>
                        
                      </td>

                  </tr>
                     <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                          <asp:LinkButton ID="LinkButton2" Text="Warehouse other Information" runat="server" PostBackUrl="~/JointVentureScheme/WarehouseFacility.aspx"></asp:LinkButton> </p>
                      </td>

                  </tr>
                     <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                         <asp:LinkButton ID="LinkButton3" Text="Payment for Warehouse Registration " runat="server" PostBackUrl="https://www.onlinesbi.com/sbicollect/payment/listinstitution.htm"></asp:LinkButton> </p>
                      </td>

                  </tr>
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                              <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium;">
                          Note :</p>
                      </td>

                  </tr>
                  <hr />
                  <hr />
                  
                                <tr> 
                            <td colspan="4">
                                <p style="color:Red;">
                                   1.कृपया Warehouse Registration के लिए Warehouse Registration लिंक पर click करें|     
 <br />
                                </p>
                                 <p style="color:Red;">
                                   2.भुगतान ना करने की स्थति में Payment for Registration of Warehouse लिंक पर क्लिक करे।
 <br />
                                </p>
                               <%-- <p style="color:Red;">
                                   3.डुप्लीकेट रसीद की प्राप्ति हेतु कृपया लिंक "Duplicate Receipt for Warehouse Registration  " पर क्लिक करे। 
 <br />
                                </p>
                                <p style="color:Red;">
                                   4.JV/Rental scheme आवेदन हेतु लिंक "Application Form For JV Scheme" पर क्लिक करे।
 <br />
                                </p>--%>
                            </td>

                        </tr>
          <img src="line.png" />
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
               <br />
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
               <br />
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
               <br />
              <br />
           
            <img src="line.png" /
         
              
            
            
          
            
            <asp:Button ID="Button1" runat="server" CssClass="submit" Text="Submit" OnClick="Button1_Click" Visible="false" />
        </div>
        </div>
    </form>
</body>
</html>
