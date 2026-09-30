<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Agreemented_Godown_Report.aspx.cs" Inherits="JointVentureScheme_Agreemented_Godown_Report" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Agreement Report</title>
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
                 <td  colspan="4" style="background-color: #66CCFF" align="left"> 
                     <p style="font-size: medium; color: #008080; width: 954px;"><asp:LinkButton ID="link1" Text="Home" runat="server"></asp:LinkButton>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="LinkButton1" Text="Agreement Report" runat="server"></asp:LinkButton>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="LinkButton2" runat="server"  align="left">Log out</asp:LinkButton></p>
                 </td>
             </tr>
              <tr>
                    <td style="height:10px">
                    </td>
                    <td align="center">

                    </td>
            </tr>
<%--             <tr>
                <td colspan="4"   align="center">
                    <p style="font-size: medium; color: #008080; width: 954px;">&nbsp&nbsp Warehouse Registration Report </p>
                </td>
            </tr>--%>
<%--            <tr>
                <td colspan="4" style="background-color: #66CCFF">
                    <p style="font-size: medium; color: #008080;">&nbsp&nbsp Warehouse Details </p>
                </td>
            </tr>--%>
            
            <tr id="trAgreeGrid" runat="server">
            <td align="center" colspan="4">
             <div style ="height:280px; width:950px; overflow:auto;" id="toexport" runat="server">
                         <p>
    <asp:GridView ID="AgreeGrid" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        Width="940px" DataKeyNames="Registration_Id" 
                    Font-Size="10pt" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="District" HeaderText="District" />
                                            <asp:BoundField DataField="Branch" HeaderText="Branch" SortExpression="Branch" />
                                            <asp:BoundField DataField="Warehouse_Name" HeaderText="Warehouse Name" SortExpression="Warehouse_Name" />
                                            <asp:BoundField DataField="Registration_Id" HeaderText="RegistrationId" SortExpression="Registration_Id" />
                                            <asp:BoundField DataField="Godown_No" HeaderText="Godown No" SortExpression="Godown_No" />
                                            <asp:BoundField DataField="Issue_Date" HeaderText="Agreement Issue Date" SortExpression="Issue_Date"/>                                            
                                            <asp:BoundField DataField="Expiry_Date" HeaderText="Agreement End Date" SortExpression="Expiry_Date"/>
                                           <%-- <asp:BoundField DataField="RegAmt" HeaderText="Registration Amount" SortExpression="RegAmt"/>--%>
                                            <asp:BoundField DataField="G_OfferCapacity" HeaderText="Offered Capacity" SortExpression="G_OfferCapacity"/>
                                            <%--<asp:BoundField DataField="OfferAmt" HeaderText="Offered Amount" SortExpression="OfferAmt"/>--%>
                                            <asp:BoundField DataField="Insp_Capacity" HeaderText="Inspected Capacity" SortExpression="Insp_Capacity"/>
                                            <asp:BoundField DataField="Agree_Capacity" HeaderText="Agreemented Capacity" SortExpression="Agree_Capacity"/>
                                            
                                        </Columns>
                                        <FooterStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Height="25px" Font-Size="10pt" />
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="30px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                    </asp:GridView>
                                     </p></div>
                                    </td>
                                    </tr>
                                    <tr><td></td>
                     <td align="right" >
                         <asp:Button ID="Button1" runat="server" Text="Export In Excel" 
                             onclick="Button1_Click1" />   
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
                                                      <br />Developed By : National Informatics Centre
                            <br />Madhya Pradesh, Ministry of Communications and Information Technology</b>
                        </td>
                        <td style="width: 1%" align="center">
                            <img id="Logo" src="/../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                        </td>
                        <td style="width: 20%" align="center">
                 <table>
                    <tr>    
                         <td><a href="http://india.gov.in/">
                              <img src="../Images/natindialogo.png" width="100px" height="50px" alt="" />
                              </a>
                         </td>
                         <td><a href="http://www.digitalindia.gov.in/">
                             <img src="../Images/di.png" width="100px" height="50px" alt="" />
                             </a>
                         </td>
                     </tr>                   
                 </table>

