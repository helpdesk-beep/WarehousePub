<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DMJvsChoiceFilling.aspx.cs" Inherits="JointVentureScheme_DMJvsChoiceFilling" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>JVS Inspection Report</title>
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

            function ConfirmOnDelete() {
                if (confirm("Are you sure want to Delete This Inspection ?") == true)
                    return true;
                else
                    return false;
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

        .style1
        {
            height: 10px;
        }

        </style>
</head>
<body>

<div id="bg" style="background-color:White">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
       
    <div>
      <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
          <center>
            <table>

                    <tr>
                         <td colspan="2" style="background-color: #008CBA ;font-size: medium; color: White; width:100px" align="center" >
                            Welcome District&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                        <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                             <asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/JVSDistrictofferReport.aspx" ForeColor="White"></asp:LinkButton>
                            </td>
                            <td style="background-color: #008CBA; width:70px;" align="center" >
                            <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="White" 
                                    onclick="LinkButton2_Click">Log out</asp:LinkButton></td>
                  </tr>
                 <tr>
                  <td>
                <label id="Label2"  runat="server">Select Branch</label>
                <asp:DropDownList ID="ddlbranchname" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="searchid" runat="server" Text="Search" OnClick="searchid_Click" />
            </td>
        </tr>


              <tr id="GAll" runat="server">
                     <td align="center" colspan="4">
                         <div style ="height:270px; width:950px; overflow:auto;" id="toexport" runat="server" >
                         <p>
                                    
                            <asp:GridView ID="GridView1"  CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" OnRowUpdating="GridView1_RowUpdating" Width="100%"  >
                                    <Columns>
                                        <asp:TemplateField>
                                         <HeaderTemplate>  
                                        
                                               <th style="text-align: center;">क्र.</th>
                                               <th style="text-align: center;">region</th>
                                               <th style="text-align: center;">District_Name</th>  
                                              <th style="text-align: center;">Branch_Name</th>  
                                              <th style="text-align: center;">Reg_ID</th>  
                                              <th style="text-align: center;">Total_Godown_Capacity </th> 
                                               <th style="text-align: center;">श्रेणी</th>  
                                                <th style="text-align: center;">Date</th>  
                                               <th style="text-align: center;">श्रेणी चुने  </th>
                                             <th style="text-align: center;">Remark</th>
                                               <th style="text-align: center;">Update श्रेणी </th> 
                                             
                                                </tr>
                        
                                            </HeaderTemplate>
                                            <ItemTemplate>                                                    
                       
                        <td style="text-align: center;"><%# Container.DataItemIndex + 1 %></td>
                        <asp:HiddenField ID="hdngodownid" runat="server" Value='<%#Eval("Reg_ID") %>'/>
                                              
                        <td style="text-align: center;"><asp:Label ID="lblComment" runat="server" Text='<%#Eval("region") %>'/> </td>
                        <td style="text-align: center;"><asp:Label ID="Label16" runat="server" Text='<%#Eval("District_Name") %>'/> </td>
                          <td style="text-align: center;"><asp:Label ID="Godownmcap" runat="server" Text='<%#Eval("DepotName") %>'/> </td>
                           <td style="text-align: center;"><asp:Label ID="GodownScientificcap" runat="server" Text='<%#Eval("Reg_ID") %>'/> </td>
                          <td style="text-align: center;"><asp:Label ID="Textbox1" runat="server" Text='<%#Eval("WareHouse_Cap") %>'/> </td>
                           <td style="text-align: center;"><asp:Label ID="backcapacity" runat="server" Text='<%#Eval("Cho") %>'></asp:Label> </td>
                          <td style="text-align: center;"><asp:Label ID="lable1" runat="server" Text='<%#Eval("Insert_Date","{0:dd-MM-yyyy}") %>'></asp:Label> </td>
                                         <td style="text-align: center;">
                                 <asp:DropDownList ID="ddlflag" runat="server" >
                                     <%-- <asp:ListItem Text="Select-श्रेणी" Value="0"></asp:ListItem>--%>
                                    <%-- <asp:ListItem Text="अ " Value="A"></asp:ListItem>--%>
                                      <asp:ListItem Text="ब " Value="B"></asp:ListItem>
                                 </asp:DropDownList>
                                             <%--<td>
                                                 <asp:DropDownList ID="ddlRemark" runat="server">
                                                     <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                     <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                     <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                     <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                 </asp:DropDownList>
                                             </td>--%>
                                             <td>
                                                 <asp:TextBox runat="server" ID="txtRemark"  TextMode="MultiLine"></asp:TextBox>
                                             </td>
                             </td>
                         <td style="text-align: center;"><asp:Button ID="btn_Update" runat="server" Text="Update" OnClientClick="return confirm('Do you want to Update this श्रेणी?');" CommandName="Update"/>  </td>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>                      
                         </p></div>
                     </td>                
               </tr> 
               
                            
              
               <tr>
                     <td align="center" colspan="4" style="font-weight:bold">
 
                     </td>                                                               
               </tr>                                                 
               <tr>
                     <td >
                    
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

                        </td>
                    </tr>
             </table>
         </div>
      </div>
  </div>
</body>
</html>

