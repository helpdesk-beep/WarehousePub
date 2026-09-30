<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchJvsChoiceFilling.aspx.cs" Inherits="JointVentureScheme_BranchJvsChoiceFilling" %>

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
<%--              <tr >
                 <td  colspan="4" style="background-color: #66CCFF" align="left"> 
                     <p style="font-size: medium; color: #008080; width: 954px;"><asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp<asp:LinkButton ID="LinkButton1" Text="Inspection Report" runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp&nbsp<asp:LinkButton 
                             ID="LinkButton2" runat="server"  align="left" onclick="LinkButton2_Click">Log out</asp:LinkButton></p>
                 </td>
             </tr>--%>
<%--             <tr>
                <td colspan="4"   align="center">
                    <p style="font-size: medium; color: #008080; width: 954px;">&nbsp;&nbsp Warehouse Registration Report </p>
                </td>
            </tr>--%>
<%--            <tr>
                <td colspan="4" style="background-color: #66CCFF">
                    <p style="font-size: medium; color: #008080;">&nbsp;&nbsp Warehouse Details </p>
                </td>
            </tr>--%>
                  <%--       <tr >
                            <td colspan="4" style="font-size: medium; width: 1000px;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                             <asp:LinkButton ID="LinkButton10" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx" ForeColor="White"></asp:LinkButton>
                            </td>
                            <td colspan="2" style="background-color: #008CBA ;font-size: medium; color: White; width:100px" align="center" >
                            Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                            <td style="background-color: #008CBA; width:70px;" align="center" >
                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="White" 
                                    onclick="LinkButton1_Click">Log out</asp:LinkButton></td>
                            </tr>
                            </table>
                            </td>
                        </tr>   --%>       
                    <tr>
                      <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White" align="center">
                          <p style="font-size: 14px; color: Black;">
                     Branch Jvs Choice Filling 2022-23 </p>
                      </td>

                  </tr>

              <tr id="GAll" runat="server">
                     <td align="center" colspan="4">
                         <div style ="height:270px; width:950px; overflow:auto;" id="toexport" runat="server" >
                         <p>
                                    
                         <asp:GridView ID="GridView1" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" CellPadding="2"
                                        Width="940px" DataKeyNames="Registration_Id" Font-Size="10pt" OnRowUpdating="GridView1_RowUpdating">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        
                        <th style="text-align: center;">क्र.</th>
                        <th style="text-align: center;">Registration ID</th>
                        <th style="text-align: center;">Warehouse Name</th>
                        <th style="text-align: center;">Mobile No</th>
                        <th style="text-align: center;">Warehouse Capacity</th>
                        <th style="text-align: center;">Warehouse License No</th>
                        <th style="text-align: center;">Incharge Name</th>
                        <th style="text-align: center;">चयन करे</th>
                        <th style="text-align: center;">Save</th>
                        </tr>
                        
                    </HeaderTemplate>
                    <ItemTemplate>

                        <td style="text-align: center;"><%# Container.DataItemIndex + 1 %></td>
                        <td style="text-align: center;">
                   
                                 <asp:HiddenField ID="hdnregid" runat="server" Value='<%#Eval("Registration_Id") %>'/>
                                 <asp:HiddenField ID="HiddenDist" runat="server" Value='<%#Eval("DistrictId") %>'/>
                            
                            <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Registration_Id") %>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Warehouse_Name") %>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Mobile_No") %>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Warehouse_Capacity") %>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("Warehouse_LicenseNo") %>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("Incharge_Name") %>' />
                        </td>
                         <td style="text-align: center;">
                            <asp:DropDownList runat="server" ID="ddlchouse" >
                                <asp:ListItem Text="चयन करे" Value="0"></asp:ListItem>
                       <%--         <asp:ListItem Text="यह Registration उपयोग में नही है" Value="यह Regitration उपयोग में नही है"></asp:ListItem>
                                <asp:ListItem Text="संचालक Offer नही करना चहता  हैं" Value="संचालक Offer नही करना चाह रहा हैं"></asp:ListItem>
                                <asp:ListItem Text="गोदाम भरा हैं" Value="गोदाम भरा हैं"></asp:ListItem>--%>


                                         <asp:ListItem Text="गोदाम का Regitration उपयोग मे नही है" Value="गोदाम का Regitration उपयोग मे नही है"></asp:ListItem>
                                <asp:ListItem Text="गोदाम विक्रय उपरांत अन्य नाम से संचालित हैं" Value="गोदाम विक्रय उपरांत अन्य नाम से संचालित हैं"></asp:ListItem>
                                <asp:ListItem Text="प्राइवेट स्कंध भंडारित हैं" Value="प्राइवेट स्कंध भंडारित हैं"></asp:ListItem>
                                <asp:ListItem Text="अन्‍य" Value="अन्‍य"></asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td style="text-align: center;">
                            <asp:Button ID="btn_Update" runat="server" Visible="false" CssClass="yelloCell" Text="Save" OnClientClick="return confirm('Do you want to Choice Filling?');" CommandName="Update" />
                        </td>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>                       
                         </p></div>
                     </td>                
               </tr> 
               
      <%--         <tr>
                     <td align="right" >
                         <asp:Button ID="Button1" runat="server" Text="Export In Excel" 
                             onclick="Button1_Click1" />   
                     </td>
               </tr>--%>
              <%--<tr visible="false" id="GDist" runat="server">
                     <td align="center" colspan="4">
                         <div style ="height:300px; width:900px; overflow:auto;" id="toexportDist" runat="server">
                         <p>
                                    <asp:GridView ID="GridDist" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        Width="00px" DataKeyNames="District_Name" Font-Size="10pt" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                                            <asp:BoundField DataField="NoOfRegistration" HeaderText="Number Of Registration" SortExpression="NoOfRegistration" />
                                            <asp:BoundField DataField="RegCapacity" HeaderText="Registered Capacity" SortExpression="RegCapacity" />
                                            <asp:BoundField DataField="Offer_Capacity" HeaderText="Offered Capacity" SortExpression="Offer_Capacity" />
                                        </Columns>
                                        <FooterStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Height="25px" Font-Size="10pt" />
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="30px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                    </asp:GridView>                         
                         </p></div>
                     </td>                
               </tr>--%>  
<%--<tr visible="false" id="GBranch" runat="server">
                     <td align="center" colspan="4">
                         <div style ="height:300px; width:900px; overflow:auto;" id="toexportBranch" runat="server" >
                         <p>
                                    <asp:GridView ID="GridBranch" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        Width="00px" DataKeyNames="District_Name" Font-Size="10pt" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                                            <asp:BoundField DataField="DepotName" HeaderText="Branch" />
                                            <asp:BoundField DataField="NoOfRegistration" HeaderText="Number Of Registration" SortExpression="NoOfRegistration" />
                                            <asp:BoundField DataField="RegCapacity" HeaderText="Registered Capacity" SortExpression="RegCapacity" />
                                            <asp:BoundField DataField="Offer_Capacity" HeaderText="Offered Capacity" SortExpression="Offer_Capacity" />
                                        </Columns>
                                        <FooterStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Height="25px" Font-Size="10pt" />
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="30px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                    </asp:GridView>                         
                         </p></div>
                     </td>                
               </tr>--%>                             
              
               <tr>
                     <td align="center" colspan="4" style="font-weight:bold">
 <%--                    <p>
                         <asp:Label ID="Label2" runat="server" Text="Total No. Of Warehouse Offered : " Visible="false"></asp:Label>&nbsp
                         <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label> &nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp
                         <asp:Label ID="Label4" runat="server" Visible="false" Text="Total Offered Capacity (In M.T) :"></asp:Label>&nbsp
                         <asp:Label ID="Label5" runat="server" Visible="false"></asp:Label>
                     </p>--%>
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

