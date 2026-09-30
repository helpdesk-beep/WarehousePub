<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeScheme.aspx.cs" Inherits="JointVentureScheme_ChangeScheme" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Change scheme </title>
        <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
	 <script type="text/javascript">
	     window.history.forward();

	     function noBack() { window.history.forward(); }
    </script>
	<script type="text/javascript">
	    Cufon.replace('h1,h2,h3,h4,h5,#menu,#copy,.blog-date');
	</script>
    <script type="text/javascript">
        function validateForm() {
            var x = document.forms["email_form_with_php"]["rname"].value;
            if (x == null || x == "") {
                alert("Name must be filled out");
                return false;
            }

            var x = document.forms["email_form_with_php"]["remail"].value;
            if (x == null || x == "") {
                alert("Email: must be filled out");
                return false;
            }

            var x = document.forms["email_form_with_php"]["remail"].value;
            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");
            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                alert("Not a valid e-mail address");
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

/*print*/

 
* {
    box-sizing: border-box;
    -moz-box-sizing: border-box;
}
.page {
    width: 21cm;
    min-height: 29.7cm;
    padding: 2cm;
    margin: 1cm auto;
    border: 1px #D3D3D3 solid;
    border-radius: 5px;
    background: white;
    box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
}
.subpage {
    padding: 1cm;
    border: 5px red solid;
    height: 237mm;
    outline: 2cm #FFEAEA solid;
}

@page {
    size: A4;
    margin: 0;
    font-size:smaller;
}
@media print {
    .page {
        margin: 0;
        border: initial;
        border-radius: initial;
        width: initial;
        min-height: initial;
        box-shadow: initial;
        background: initial;
        page-break-after: always;
        font-size:smaller;
    }
}
@media print {
  html, body {
    width: 210mm;
    height: 297mm;
    font-size:smaller;
  }
  /* ... the rest of the rules ... */
}
/*td{font-size:smaller;}*/
page[size="A4"] {
  background: white;
  width: 21cm;
  height: 29.7cm;
  display: block;
  margin: 0 auto;
  margin-bottom: 0.5cm;
  box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
}
@media print {
  body, page[size="A4"] {
    margin: 0;
    box-shadow: 0;
    font-size:smaller;
  }
}
        .style2
        {
            height: 35px;
        }
        
        .style6
        {
            height: 39px;
        }
        
    </style>

    

</head>
<body onload="noBack();">
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
            var printWindow = window.open('', '', 'height=200,width=400');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>

      
		<div class="wrap">
                 <form id="Form1"   runat="server">
                  <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
                 <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
                                                <p style="font-size: medium;color: #008080; background-color:#66CCFF">&nbsp;&nbsp;&nbsp<asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/WarehouseHome.aspx"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Welcome
                                                                                               <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp; <asp:LinkButton ID="lnkLogout" runat="server" onclick="lnkLogout_Click">Log out</asp:LinkButton>--%>
                                                 </p>
          
                

  

        <div >

                    <table>
                        
<%----------------------%>



                      <tr>
                      <td colspan="4"  class="style2" align="center" style="width:954px">
                          <p style="font-size:large; color: #008080; height:30px; font-weight:bold; text-align:center">
                         &nbsp&nbsp Change/Update Scheme</p>
                      </td>
                      </tr>
                  <tr>
                   
                      <td colspan="4" class="style6" >
                          <p style="font-size: medium; color: #008080;font-weight:bold; background-color:#66CCFF; text-decoration: underline;">
                        Warehouse Registration Details </p>
                      </td>
                    
                  </tr>
                   <tr>
                   
                    <td align="center" colspan="4">
                   Registration ID : &nbsp;&nbsp;
                        <asp:TextBox ID="txtSearch" class="text" runat="server"  style="width:150px; height:25px;"></asp:TextBox>
                       <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtSearch"
                                        ValidChars="0123456789">
                         </cc1:FilteredTextBoxExtender>--%>
                          &nbsp;&nbsp; <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" style="height:25px;" />
                   <br /><br /> </td>
                       
                    </tr>
                  <%--<tr>--%>
                   
                   <%-- <td >
                       Registered Email Address :
                    </td>
                    <td >
                        <asp:Label ID="lblRegEmail" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                     
                    <td  >
                      Mobile Number :
                    </td>
                     <td >
                        <asp:Label ID="lblRegMobile" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                     
                    <td  > 
                       Type of Applicant/Entity  :
                    </td>
                    <td>
                        <asp:Label ID="lblAppType" runat="server"></asp:Label>
                    </td>
                    </tr>
                  <tr>
                   
                      <td colspan="4" class="style2" >
                          <p style="font-size: medium; color: #008080; font-weight:bold; background-color:#66CCFF; text-decoration: underline;">
                        Warehouse Details </p>
                      </td>
                     
                  </tr>                    
                  <tr>
                       
                    <td  >
                       Warehouse Name :
                    </td>
                    <td  >
                        <asp:Label ID="lblWName" runat="server" Width="350px" Style="word-wrap: break-word"></asp:Label>
                    </td>               
                  </tr>
                  <tr>
                    <td  >
                       Office Contact No./Mobile No :
                    </td>
                    <td >
                        <asp:Label ID="lblWMobile" runat="server"></asp:Label>
                    </td>                  
                  </tr>
                  <tr>
                  
                    <td   >
                       Warehouse Owner :
                    </td>
                    <td >
                        <asp:Label ID="lblWOwn" runat="server"></asp:Label>
                    </td>                   </tr>                                   
                  <tr>
                   
                   <td  >
                     District :
                  </td>
                    <td >
                        <asp:Label ID="lblWDist" runat="server"></asp:Label>
                    </td>                                      
                  </tr>
                  <tr>
                   <td  >
                     Tehsil :
                  </td>
                    <td >
                        <asp:Label ID="lblWtehsil" runat="server"></asp:Label>
                    </td>                    
                  </tr>
                  
                  <tr>
                   
                   <td  >
                       Nearest Branch of MPWLC :
                  </td>
                    <td>
                        <asp:Label ID="lblWBranch" runat="server"></asp:Label>
                    </td>                                       
                  </tr> 
                  <tr>
                   <td  >
                       Distance from Nearest Branch of MPWLC (in KM) :
                  </td>
                    <td >
                        <asp:Label ID="lblWBranchDist" runat="server"></asp:Label>
                    </td>                  
                  </tr>
                    <tr  >
                    <td >
                       Warehouse/Office Address with Postal Address :
                    </td>
                    <td>
                        <asp:Label ID="lblWAddres" runat="server"  Width="350px" Style="word-wrap: break-word" ></asp:Label>
                    </td>                     
                    </tr>                   --%>
                 <%-- <tr>
                   
                      <td colspan="4" class="style2">
                          <p style="font-size: medium; color: #008080; font-weight:bold; background-color:#66CCFF;text-decoration: underline;">
                        Offered Godown's Capacity Details </p>
                      </td>
                     
                  </tr>   --%>                                  
                   <%-- <tr>

                           <td style="height:10px" >
                                Offered Date :   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblofrdate" runat="server" Text=""></asp:Label>
                           
                           </td>                    
                    </tr> --%>
 <tr>
 
                <td colspan="4" id="GVGodowns" runat="server" visible="true" align="center">
                        <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" 
                             EnableModelValidation="True" BackColor="White" BorderColor="Black" 
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                             Font-Size="Small" onselectedindexchanged="gvGodown_SelectedIndexChanged">
                            <AlternatingRowStyle Font-Size="Small" />
                            <Columns>
                            <asp:TemplateField HeaderText = "SNo." ItemStyle-Width="100">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>

<ItemStyle Width="25px"></ItemStyle>
    </asp:TemplateField>
                            
                                <asp:BoundField DataField="Registration_Id" HeaderText="RegistrationId" />
                                <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse Name" />
                                <asp:BoundField DataField="Godown_Id" HeaderText="Godown Id" />
                                <asp:BoundField DataField="Godown_No" HeaderText="Godown No" />
                                <asp:BoundField DataField="G_OfferCapacity" HeaderText="Offer Capacity" />
                                <asp:BoundField DataField="G_Scheme" HeaderText="Scheme" />
                                
                                <asp:CommandField SelectText="Update" HeaderText="Change" ShowSelectButton="True" >
                                <ControlStyle Font-Bold="True" ForeColor="Red" />
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="Black" Height="30px" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <SelectedRowStyle BackColor="#669999" ForeColor="White" />
                        </asp:GridView>
                        <br />                 

                    </td>   
                       
            </tr>
<%--<tr>
                        
                           <td>
                                Total Offered Capacity :

                                 &nbsp;&nbsp;&nbsp; <asp:Label ID="lblOfrCpt" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td></tr>
<tr>
                             <td>
                                Total Amount :
                                 &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; <asp:Label ID="lbltotalamt" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>                            
</tr>--%>
                        

<%-------------------%>                    
                        
                        <tr id="gv" runat="server" visible="false">
                        
                           <td align="center">
                               <asp:Label ID="Label1" runat="server" Text="Scheme"></asp:Label>
&nbsp;&nbsp;
                               <asp:DropDownList ID="ddlScheme" runat="server" Height="25px" Width="130px" 
                                    >
                               <asp:ListItem Value="55" Text="NON-WDRA"></asp:ListItem>
                               <asp:ListItem Value="60" Text="WDRA"></asp:ListItem>
                               </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               <br /><br />
                                </td>
                               
                               
                               
                           
                        </tr> 
                        <tr>
                        <td colspan="4"><center><asp:Button ID="btnUpdate" runat="server" Text="Update" 
                                Height="25px" Width="75px"  Visible="false" onclick="btnUpdate_Click"></asp:Button></center>

                        </td></tr>      
                    </table>
                    </div>
        

             <div style="background-image: url('../images/div_bg.png')"></div>
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
                                            <b>© 2018 &nbsp;National Informatics Centre.All Rights Reserved
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
                  </form>
               </div>
        </div>
</body>
</html>


