<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Password_Change.aspx.cs" Inherits="JointVentureScheme_Password_Change" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Change Password</title>
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
	
<script type="text/javascript">
    function checksqlkey_psw(e, tx) {
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        var num = tx.value;

        //alert(AsciiCode);              
        if (AsciiCode == 59 || AsciiCode == 32) {
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
        }
        else if (AsciiCode == "36" || AsciiCode == "37" || AsciiCode == "38" || AsciiCode == "40" || AsciiCode == "41" || AsciiCode == "43" || AsciiCode == "92" || AsciiCode == "124" || AsciiCode == "34" || AsciiCode == "39" || AsciiCode == "60" || AsciiCode == "62" || AsciiCode == "44" || AsciiCode == "64" || AsciiCode == "61") {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
            return false;
        }
        else if (num.length > 9) {
            alert('Password Length Maximum 10 Characters ...');
            return false;
        }
    }

    // For Copy/Paste Checking

    function checksqlkey_special(e, tx) {
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        var num = tx.value;
        //alert(AsciiCode);         
        if (AsciiCode == 17) {
            alert('Copy/Paste Not Allowed ...');
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

        .style2
        {
            height: 11px;
        }

        .style4
        {
            height: 8px;
        }

        </style>
        <script type="text/javascript">
            function preventInput(evnt) {
                //Checked In IE9,Chrome,FireFox
                if (evnt.which != 9) evnt.preventDefault();
            }

            
        </script>
        
</head>
<body>

<div id="bg">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
			 <form id="Form1"   runat="server">
       
        <div>
          <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%> 
                          
                                
          <div>

          </div>                                
                    <table>
                        <tr >
                            <td  colspan="4" style="background-color: #66CCFF"> 
                                <p style="font-size: medium; color: #008080; width: 953px;">&nbsp&nbsp&nbsp<asp:LinkButton 
                                        ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Change Password&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/JointVentureScheme/Logins.aspx">Log out</asp:LinkButton></p>
                            </td>
                        </tr>
                        
                        <tr>
                        <td colspan="4" align="center">
                                  <table><tr><td class="style4"></td></tr>
           <tr>
        <td colspan="4">
        <center><h5 style="color:Red">लॉगिन करने के पश्चात सर्वप्रथम अपने पासवर्ड को बदले</h5></center>
       
        </td>
        </tr>
          <tr style="border-collapse: collapse; border: solid 1px white" class="HeadingBlue">
                <td colspan="4" style="background-color: dimgray; height: 15px;">
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 18px" colspan="4">
                    <asp:Label ID="lblChangePassword" runat="server" 
                        Text="Change Login Password" Font-Bold="True"
                        Style="position: static" Width="152px"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" style="height: 18px" colspan="4">
                    <asp:Label ID="Uxmsg" runat="server" Font-Bold="True" ForeColor="Red" Style="position: static"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 23px; height: 18px;" align="left">
                    <asp:Label ID="lblOldPassword" runat="server" Font-Bold="True" Text="Old Password"
                        Width="120px"></asp:Label></td>
                <td style="width: 46px; height: 18px;">
                    <asp:TextBox ID="txt_Old_Pass" runat="server" TextMode="Password" 
                        ValidationGroup="bl"></asp:TextBox></td>
                <td style="width: 8px; height: 18px">
                </td>
                <td style="width: 12px; height: 18px;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Old_Pass"
                        ErrorMessage="Provide Old Password" ValidationGroup="bl">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 23px" align="left">
                    <asp:Label ID="lblNewPassword" runat="server" Font-Bold="True" Text="New Password"
                        Width="136px"></asp:Label></td>
                <td style="width: 46px">
                    <asp:TextBox ID="txt_Pass_New" runat="server" TextMode="Password" MaxLength="15"
                        ValidationGroup="bl"></asp:TextBox></td>
                <td style="width: 8px">
                </td>
                <td style="width: 12px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_Pass_New"
                        ErrorMessage="Provide New Password" ValidationGroup="bl">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 23px" align="left">
                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" Font-Bold="True"
                        Width="128px"></asp:Label></td>
                <td style="width: 46px">
                    <asp:TextBox ID="txt_Pass_Confirm" runat="server" TextMode="Password" MaxLength="15" 
                        ValidationGroup="bl"></asp:TextBox></td>
                <td style="width: 8px">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_Pass_New"
                        ControlToValidate="txt_Pass_Confirm" 
                        ErrorMessage="New Password and Confirm  password Must be same" 
                        ValidationGroup="bl">*</asp:CompareValidator></td>
                <td style="width: 12px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Pass_Confirm"
                        ErrorMessage="Confirm Password" ValidationGroup="bl">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btn_Change_Pass" runat="server" Text="Change" 
                        Font-Size="X-Small" class="submit" 
                         Style="position: static" 
                        ValidationGroup="bl" onclick="btn_Change_Pass_Click" />
                    <asp:Button ID="btn_Cancle" runat="server" Text="Close" Font-Size="X-Small" 
                        Style="position: static" class="submit" 
                         CausesValidation="false" onclick="btn_Cancle_Click" />
                </td>
            </tr>
          
            <tr style="border-collapse: collapse; border: solid 1px white" class="HeadingBlue">
                <td colspan="4" style="background-color: dimgray; height: 15px;">
                </td>
            </tr>
          <tr><td class="style4"></td></tr>
          </table>
                        </td>
                        </tr>
                        </table>
                        <div id="divOwner" runat="server">
                        <table>
                   <tr>
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         </p>
                      </td>

                  </tr><tr><td class="style4"></td></tr>
                  
                       
                        <tr><td class="style4"></td></tr>
                        </table>
                                    <table>
                                    
                   
                                </table>
                            </div>
    </div>
    </form>
    </div>
    </div>
</body>
</html>