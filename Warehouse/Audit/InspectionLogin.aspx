<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InspectionLogin.aspx.cs" Inherits="Inspection_InspectionLogin" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPWLC Audit</title>
      <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
     <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

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

    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });



    </script>
</head>
<body>
    <form id="form1" runat="server">
        
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>

    <div id="bg">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
          
            <br />
            
             <center>
            <div  style="width: 100%;">
                                           
                                   <br /> 
                                 
                    <table align="center" style="width: 500px; border: 10px solid #cb4e48; background-color: #EBEBEB;">       
                          <tr >
                              <td align="center" colspan="2" valign="middle" style="background-color: #007ACC">
                                                        <span style="color: White; font-weight: bolder; font-size: 15pt">Login</span>
                                                    </td>
                                                </tr>
                                                            <tr >
                              <td align="center" colspan="2">
                                  <br />
                                                      <asp:RadioButton ID="rbbranch" runat="server" Text="Branch Inspection" Checked="True" GroupName="rb" Font-Bold="True" ForeColor="#ADCCCC" AutoPostBack="True" OnCheckedChanged="rbbranch_CheckedChanged"></asp:RadioButton>&nbsp;&nbsp;&nbsp; <asp:RadioButton ID="rbrm" runat="server" Text="Region Inspection" GroupName="rb" Font-Bold="True" ForeColor="#ADCCCC" AutoPostBack="True" OnCheckedChanged="rbrm_CheckedChanged"></asp:RadioButton>
                                                    </td>
                                                </tr>     
                        <tr>
                            <td colspan="2">
                                ----------------------------------------------------------------------------------------------------------------------
                            </td>
                        </tr>        
                        <tr runat="server" id="trregion">
                            <td>Region:</td>
                            <td>

                                                        <asp:DropDownList ID="ddlregion" runat="server"
                                                            CssClass="tb6" Font-Bold="true" ForeColor="Navy" Height="30px" Visible="True" 
                                                            Width="205px" >
                                                        </asp:DropDownList>

                            </td>
                        </tr>
                        <tr runat="server" id="trdest">
                            <td>Distric:</td>
                            <td>

                                                        <asp:DropDownList ID="DDL_Dist" runat="server" AutoPostBack="True" 
                                                            Width="205px" Height="30px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged">
                                                        </asp:DropDownList>

                            </td>
                        </tr>
                        <tr runat="server" id="trbranch">
                            <td>

                                Branch:
                            </td>
                            <td>

                                                        <asp:DropDownList ID="DDL_Depot" runat="server" Width="205px" Height="30px" Font-Bold="true"
                                                            ForeColor="Navy" CssClass="tb6">
                                                        </asp:DropDownList>

                            </td>

                        </tr>
                        <tr>
                               <td>
                            Password:
                        </td>
                        <td>
                             <input id="txtlogpwd" name="rname" class="text" runat="server" placeholder="Password" type="password" tabindex="10" />
                            <span style="color: #FF0000; font-size: medium;">*</span>
                        </td>
                        </tr>

                    <tr>
                        <td></td>
                         <td>
                        <%--<input id="Submit1" type="Submit" class="submit"  value="Submit"  />--%>
                             <asp:Button ID="btnlogin" runat="server" Text="Login" class="submit"  TabIndex="11" ValidationGroup="l" OnClick="btnlogin_Click"></asp:Button>
                    </td>
                  </tr>
                 
                 
                    <%--<tr>
                        <td colspan="2"><input id="Button1" type="button" class="submit" value="Forget Password" tabindex="16" />

                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2" align="center">
                           <p style="color:Red; font-family:Times New Roman; font-size:medium;">For Audit Application login Password Contact : 7552600505</p>
                        </td>
                    </tr>
                    <tr>
                                                    <td align="center" colspan="2">
                                                        
                                                        <img src="../images/pdflogo.png" style="width: 21px" alt="" height="21px" />
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Documents/Branch_Audit_Doc.pdf"
                                                            Font-Size="10pt" Font-Bold="true" ForeColor="Navy">Branch Audit Doc</asp:HyperLink>
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        <img src="../images/pdflogo.png" style="width: 21px" alt="" height="21px" />
                                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Documents/Region_Audit_Doc.pdf"
                                                            Font-Size="10pt" Font-Bold="true" ForeColor="Navy">Region Audit Doc</asp:HyperLink>
                                                    </td>
                                                </tr>

                    <tr>
                        <td colspan="2" rowspan="2">
                                   <span lang="en-us"></span><div id="Panel1" runat="server">Email:<input id="txtpdwrec" name="rpass" type="text" runat="server" class="text" tabindex="12" /><br /><br />
                                <asp:Button ID="btngetpwd" CssClass="submit" runat="server" Text="Submit"  TabIndex="13"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btncancel" runat="server" CssClass="submit" Text="Cancel" TabIndex="14"></asp:Button> 
                            </div>
                        </td>
                    </tr>
                    </table>
                                 
                   
                                 <br/>
                 <br />             
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

                 </center>
         </div>
  <br />
                 </div>
    </form>
</body>
</html>
