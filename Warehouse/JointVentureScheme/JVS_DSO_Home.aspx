<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JVS_DSO_Home.aspx.cs" Inherits="JVS_DSO_Home" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>District JVS Report</title>
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
<body style="background-color:White">

<div id="bg">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="140" />
       
    <div>
      <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
          <center>
            <table style="width: 100%;" >
                        <tr >
                            <td colspan="4" style="font-size: medium;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                             <asp:LinkButton ID="LinkButton10" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/JVS_DSO_Home.aspx" ForeColor="White"></asp:LinkButton>
                            </td>
                            <td colspan="2" style="background-color: #008CBA ;font-size: medium; color: White; width:100px" align="center" >
                            Welcome District&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                            <td style="background-color: #008CBA; width:70px;" align="center" >
                            <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="White" 
                                    onclick="LinkButton2_Click">Log out</asp:LinkButton></td>
                            </tr>
                            </table>
                            </td>
                        </tr>
              <tr id="Tr1" runat="server">
                      
                      <td style="font-size: medium; color: #008080;">&nbsp;&nbsp;1. <asp:LinkButton 
                                  ID="LinkButton1" Text="Pre Printed Inspection Form for JVS Inspection" runat="server" ForeColor="#008080"
                                  PostBackUrl="~/JointVentureScheme/DSO_PrintInspFormate_2022_23.aspx" Font-Underline="True"></asp:LinkButton><br /><br />
                      </td>  
                  <td style="font-size: medium; color: #008080;">&nbsp;&nbsp;2. <asp:LinkButton 
                                  ID="LinkButton3" Text="Warehouse Inspection Report Kharif 2022-23" runat="server" ForeColor="#008080"
                                  PostBackUrl="~/JointVentureScheme/BranchInspectionReport2022_23_Kharif_Dso.aspx" Font-Underline="True"></asp:LinkButton><br /><br />
                      </td>         

                  </tr>     
                <tr id="Tr2" runat="server">
                      
                      <td style="font-size: medium; color: #008080;">&nbsp;&nbsp;3. <asp:LinkButton 
                                  ID="LinkButton4" Text="FIFO for Deposit Stock Kharif 2022-23" runat="server" ForeColor="#008080"
                                  PostBackUrl="~/JointVentureScheme/DSO_FIFO_wise_Godown_Inflow.aspx" Font-Underline="True"></asp:LinkButton><br /><br />
                      </td>  
                  <td style="font-size: medium; color: #008080;"><br /><br />
                      </td>         

                  </tr>    

      <tr>
                      <td style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White ; width: 100%;" align="center" colspan="2">
                          <p style="font-size: 14px; color: Black;">
                          Offered Capacity Report (JVS 2022-23)</p>
                      </td>

                  </tr>             
              <tr>
                     <td align="center">
                        Select Report Type :
                          <asp:DropDownList ID="ddl_session" runat="server" Width="150px"  Height="25px" Enabled="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"
                              AutoPostBack="true">
                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                             <asp:ListItem Value="JVSK2223">JVS KHARIF 2022-23</asp:ListItem>
                             <asp:ListItem Value="JVSR2223">JVS Rabi 2022-23</asp:ListItem>
                        </asp:DropDownList>                 
                     </td>
                     <td align="center">                   
                         Branch Wise <asp:RadioButton ID="rdoBranch" runat="server" 
                             GroupName="StateLicence" Width="70px" AutoPostBack="true"
                             oncheckedchanged="rdoBranch_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         Detail Report <asp:RadioButton ID="rdoAll" runat="server" GroupName="StateLicence" AutoPostBack="true"
                             Width="70px" oncheckedchanged="rdoAll_CheckedChanged" />
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Button1" runat="server" Text="Export In Excel" class="button button2" Width="120px" Height="28px"
                             onclick="Button1_Click1" />   
                     </td>
               </tr>
              <tr visible="false" id="GAll" runat="server">
                     <td align="center" colspan="4">
                         <div style ="height:280px; width:900px; overflow:auto;" id="toexport" runat="server" >
                         <p>
                                    <asp:GridView ID="RegGrid" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        Width="900px" DataKeyNames="Registration_Id" Font-Size="10pt" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                                            <asp:BoundField DataField="DepotName" HeaderText="Branch" SortExpression="DepotName" />
                                            <asp:BoundField DataField="Warehouse_Name" HeaderText="Warehouse Name" SortExpression="Warehouse_Name" />
                                            <asp:BoundField DataField="Auth_Person" HeaderText="Authorized Person" SortExpression="Auth_Person" />
                                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No." SortExpression="MobileNo" />
                                            <asp:BoundField DataField="Registration_Id" HeaderText="Registration ID" SortExpression="Registration_Id"/>                                            
                                            <asp:BoundField DataField="RegCapacity" HeaderText="Registered Capacity (in M.T)" SortExpression="RegCapacity"/>
                                           <%-- <asp:BoundField DataField="RegAmt" HeaderText="Registration Amount" SortExpression="RegAmt"/>--%>
                                            <asp:BoundField DataField="Offer_Capacity" HeaderText="Offered Capacity (in M.T)" SortExpression="Offer_Capacity"/>
                                            <%--<asp:BoundField DataField="OfferAmt" HeaderText="Offered Amount" SortExpression="OfferAmt"/>--%>
                                            <asp:BoundField DataField="OfferedDate" HeaderText="Offered Date" SortExpression="OfferedDate"/>
                                        </Columns>
                                        <FooterStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Height="25px" Font-Size="10pt" />
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="30px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                    </asp:GridView>                         
                         </p></div>
                     </td>                
               </tr> 
                
<tr visible="false" id="GBranch" runat="server">
                     <td align="center" colspan="4">
                         <div style ="height:280px; width:900px; overflow:auto;" id="toexportBranch" runat="server" >
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
               </tr>                             
               <tr>
                     <td >

                    </td>
               </tr>                
               <tr>
                     <td align="center" colspan="4" style="font-weight:bold">
                     <p>
                         <asp:Label ID="Label2" runat="server" Text="Total No. Of Warehouse Offered : " Visible="false"></asp:Label>&nbsp
                         <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                         <asp:Label ID="Label4" runat="server" Visible="false" Text="Total Offered Capacity (In M.T) :"></asp:Label>&nbsp
                         <asp:Label ID="Label5" runat="server" Visible="false"></asp:Label>
                     </p>
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
