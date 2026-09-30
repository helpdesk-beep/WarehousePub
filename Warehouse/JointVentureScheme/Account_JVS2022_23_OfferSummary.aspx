<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Account_JVS2022_23_OfferSummary.aspx.cs" Inherits="JointVentureScheme_Account_JVS2022_23_OfferSummary" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Summary Report</title>
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
    border: 2px solid #E47D21;
}

.button6:hover {
    background-color: #E47D21;
    color: white;
}
</style>
</head>
<body>

<div id="bg" style="background-color:White">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
       
    <div >
      <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
          <center>
            <table>
<%--              <tr >
                 <td  colspan="4" style="background-color: #66CCFF" align="left"> 
                     <p style="font-size: medium; color: #008080; width: 954px;"><asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/DistrictWiseJVSOffer.aspx"></asp:LinkButton>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="LinkButton1" Text="Offered Capacity Summary Report" runat="server"></asp:LinkButton>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="LinkButton2" runat="server"  align="left">Log out</asp:LinkButton></p>
                 </td>
             </tr>--%>
                        <tr >
                            <td colspan="4" style="font-size: medium;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                             <asp:LinkButton ID="LinkButton3" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/HoAccountHome.aspx" ForeColor="White"></asp:LinkButton>
                            </td>
                            <td colspan="2" style="background-color: #008CBA ;font-size: medium; color: White; width:100px" align="center" >
                            Welcome HO&nbsp;</td>
                            <td style="background-color: #008CBA; width:70px;" align="center" >
                            <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="White" 
                                    onclick="LinkButton2_Click">Log out</asp:LinkButton></td>
                            </tr>
                            </table>
                            </td>
                        </tr>
      <tr>
                      <td colspan="4" style=" border-color:#008CBA; height:20px; border-style:solid; border-width:2px; background-color:White; font-size: 14px; color: Black;" align="center">
                          
                          Offered/Inspection/Agreement Godown Summary Report 2022-23
                      </td>

                  </tr>                                      
                                  <tr>
                                  <td align="center" colspan="4">
                                  Season &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:DropDownList ID="ddl_session" runat="server" Width="150px"  Height="25px" 
                                           AutoPostBack="true" Enabled="false" >
                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                 <asp:ListItem Selected="True" Value="JVS2022_23">JVS 2022-23</asp:ListItem>
                         <asp:ListItem  Value="JVS2020_21">JVS 2020-21</asp:ListItem>
                        <asp:ListItem Value="Rabi1920">Rabi 2019-20</asp:ListItem>
                        <asp:ListItem Value="Kharif1819">Kharif 2018-19</asp:ListItem>
                         <asp:ListItem Value="Rabi1819">Rabi 2018-19</asp:ListItem>
                        </asp:DropDownList> 
&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                         <asp:Button ID="Button1" runat="server" Text="Export In Excel" class="button button2" Width="120px" Height="28px"
                             onclick="Button1_Click1" />   
                           
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
                                        Width="90%" DataKeyNames="District" 
                    Font-Size="10pt" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                                            <asp:BoundField DataField="District" HeaderText="District" />
                                            <asp:BoundField DataField="NoOfOfferedGodown" HeaderText="No Of Offered Godown" SortExpression="NoOfOfferedGodown" />
                                            <asp:BoundField DataField="OfferedCapacity" HeaderText="Offered Capacity" SortExpression="OfferedCapacity" />
                                            <asp:BoundField DataField="FIT" HeaderText="FIT" SortExpression="FIT" />
                                            <asp:BoundField DataField="FITCapacity" HeaderText="FIT Capacity" SortExpression="FITCapacity" />
                                            <asp:BoundField DataField="UNFIT" HeaderText="UNFIT" SortExpression="UNFIT"/>                                            
                                            <asp:BoundField DataField="UNFITCapacity" HeaderText="UNFIT Capacity" SortExpression="UNFITCapacity"/>
                                           <%-- <asp:BoundField DataField="RegAmt" HeaderText="Registration Amount" SortExpression="RegAmt"/>--%>
                                            <asp:BoundField DataField="Agreement_Capacity" HeaderText="Agreement Capacity" SortExpression="Agreement_Capacity"/>
                                          <%--  <asp:BoundField DataField="RemForInsp" HeaderText="Remaining For Inspection" SortExpression="RemForInsp"/>--%>
                                            <%--<asp:BoundField DataField="Insp_Capacity" HeaderText="Inspected Capacity" SortExpression="Insp_Capacity"/>
                                            <asp:BoundField DataField="Agree_Capacity" HeaderText="Agreemented Capacity" SortExpression="Agree_Capacity"/>--%>
                                            
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
