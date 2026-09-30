<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeChoiceFilling.aspx.cs" Inherits="TribalGodown_ChangeChoiceFilling" %>

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

          
                  

 
         
              
            <fieldset style="width: 920px;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">                    
                                                           <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="Label2" runat="server" Text="क्या आप श्रेणी बदलना चाहते हैं ?" Font-Bold="true"
                                                                Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                            &nbsp;&nbsp&nbsp;&nbsp  &nbsp;&nbsp&nbsp;&nbsp
                                                            <asp:Button id="btnback" runat="server" Text="BACK" OnClick="btnback_Click"  />
                                                        </td>
                                                    </tr>
                                               
                                            
                                                   
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            

                                                            <asp:Button ID="btn" runat="server" CssClass="BTNBLUE" style="width: 200px; margin-top: 20px; height: 34px; background: #0bb6e6;  color: #fff;  font-size: 16px;  font-weight: 600;" Text="श्रेणी ब से अ में परिवर्तित करे " OnClick="btn_Click"  />                                                            
                                                        </td>
                                                    </tr>
                                                    
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
            
          
            
       
        </div>
        </div>
    </form>
</body>
</html>
