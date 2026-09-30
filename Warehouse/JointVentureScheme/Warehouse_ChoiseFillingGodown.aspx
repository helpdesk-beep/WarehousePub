<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Warehouse_ChoiseFillingGodown.aspx.cs" Inherits="JointVentureScheme_Warehouse_ChoiseFillingGodown" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Warehouse Payment Status</title>
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
          
          <table>
    <tr>
                        
       
                        <td  style="background-color: #008CBA; width:100PX ; " align="center"> 
                             <asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/UserReg.aspx" ForeColor="White"></asp:LinkButton>
                            </td>
                            <td style="background-color: #008CBA; width:70px;" align="center" >
                            <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="White" 
                                 OnClick="LinkButton2_Click">Log out</asp:LinkButton></td>
                  </tr>
              
    </table>
          <div style="color:red; text-align:center; font-size:25px">JVS OFFIER  की वह श्रेणीयाँ जो गलत सम्मलित हो गए हे उन्हे सही करे</div>
          
           <br />

    <asp:Panel ID="StoreGrid" runat="server" >
             <asp:GridView ID="GridView1"  CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" OnRowUpdating="GridView1_RowUpdating" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                         <HeaderTemplate>  
                                        
                                                     <th style="text-align: center;">क्र.</th>
                                                    <th style="text-align: center;">region</th>
                                                    <th style="text-align: center;">District_Name</th>  
                                              <th style="text-align: center;">Branch_Name</th>  
                                                    <th style="text-align: center;">Reg_ID</th>  
                                                    <th style="text-align: center;">Godown_Name</th> 
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
                        <td style="text-align: center;"><asp:Label ID="Label1" runat="server" Text='<%#Eval("Warehouse_Name") %>'/> </td>
                          <td style="text-align: center;"><asp:Label ID="Textbox1" runat="server" Text='<%#Eval("WareHouse_Cap") %>'/> </td>
                           <td style="text-align: center;"><asp:Label ID="backcapacity" runat="server" Text='<%#Eval("Cho") %>'></asp:Label> </td>
                          <td style="text-align: center;"><asp:Label ID="lable1" runat="server" Text='<%#Eval("Insert_Date","{0:M-dd-yyyy}") %>'></asp:Label> </td>
                                         <td style="text-align: center;">
                                 <asp:DropDownList ID="ddlflag" runat="server" >
                                      <asp:ListItem Text="Select-श्रेणी" Value="0"></asp:ListItem>
                                     <asp:ListItem Text="अ " Value="A"></asp:ListItem>
                                      <asp:ListItem Text="ब " Value="B"></asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                        <td style="text-align: center;"><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox> </td>
                                              
                         <td style="text-align: center;"><asp:Button ID="btn_Update" CssClass="btn btnred" Visible="false" runat="server" Text="Update" OnClientClick="return confirm('Do you want to Update this श्रेणी?');" CommandName="Update"/>  </td>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
    </asp:Panel>
          
      <div></div>
 </form>
  </div>
</body>
</html>