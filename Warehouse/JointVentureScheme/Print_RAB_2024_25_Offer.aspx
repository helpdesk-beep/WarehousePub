<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_RAB_2024_25_Offer.aspx.cs" Inherits="JointVentureScheme_Print_RAB_2024_25_Offer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Offer</title>
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
            var printWindow = window.open('', '', 'height=600,width=700');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
       <div id="ReportDiv">
      
		<div class="wrap">
                 <form id="Form1"   runat="server">
<%--                                                <p style="font-size: medium;color: #008080; background-color:#66CCFF">&nbsp;&nbsp;&nbsp<asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/WarehouseHome.aspx"></asp:LinkButton>  &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Offered Detail &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp; <asp:LinkButton ID="lnkLogout" runat="server" onclick="lnkLogout_Click">Log out</asp:LinkButton>
                                                 </p>--%>
          
                

  <div id="PrintDiv" style="font-size:medium; ">

        <div >

                    <table >
                        
<%----------------------%>



                      <tr>
                      <td colspan="4"  class="style2" align="center" style="width:954px">
                          <p style="font-size:large; color: #008080; height:50px; font-weight:bold; text-align:center">
                         &nbsp&nbsp Warehouse Offered Capacity Under Joint Venture Scheme </p>
                         <hr />
                      </td>
                      </tr>
                  <tr>
                   
                      <td colspan="4" class="style6" >
                          <p style="font-size: medium; color: #008080;font-weight:bold; background-color:#66CCFF; text-decoration: underline;">
                        Registration Details </p>
                      </td>
                    
                  </tr>
                   <tr>
                   
                    <td >
                       Registered Id :
                    </td>
                    <td >
                        <asp:Label ID="lblRegId" runat="server"></asp:Label>
                    </td>
                    </tr>
                  <tr>
                   
                    <td >
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
                     Block :
                  </td>
                    <td >
                        <asp:Label ID="lblblocknew" runat="server"></asp:Label>
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
                    </tr>                   
                  <tr>
                   
                      <td colspan="4" class="style2">
                          <p style="font-size: medium; color: #008080; font-weight:bold; background-color:#66CCFF;text-decoration: underline;">
                        Offered Godown's Capacity Details </p>
                      </td>
                     
                  </tr>                                     
                    <tr>

                           <td style="height:10px" >
                                Offered Date :   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblofrdate" runat="server" Text=""></asp:Label>
                           
                           </td>                    
                    </tr> 
 <tr>
                <td colspan="4" id="GVGodowns" runat="server" visible="true" align="center">
                        <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" 
                            Width="60%" EnableModelValidation="True" BackColor="White" BorderColor="Black" 
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3">
                            <Columns>
                            <asp:BoundField DataField="Godown_No" HeaderText="Godown No." />
                            <asp:BoundField DataField="G_OfferCapacity" HeaderText="Offered Capacity" >

                                </asp:BoundField>
                            <asp:BoundField DataField="Offer_Category" HeaderText="Offered Scheme" />
                            <asp:BoundField DataField="Capacity_Type" HeaderText="Capacity Type" ItemStyle-HorizontalAlign="Left" > 
                                
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                        <br />                 

                    </td>            
            </tr>
<tr>
                        
                           <td>
                                Total Offered Capacity :

                                 &nbsp;&nbsp;&nbsp; <asp:Label ID="lblOfrCpt" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td></tr>
<tr>
                             <td>
                                Total Amount :
                                 &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; <asp:Label ID="lbltotalamt" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>                            
</tr>
                        

<%-------------------%>                    
                        
                        <tr>
                        
                           <td>
                               <br />
                               <br />
                               Print Date :   &nbsp;&nbsp;&nbsp; <asp:Label ID="lblAppDate" runat="server" Text=""></asp:Label>
                           </td>
                        </tr>       
                    </table>
                    </div>
               </div>
                    <center><input id="Button1" name="Print" type="button" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;" value="Print"  onclick="PrintDiv();" /></center>
                 
                  </form>
               </div>
        </div>
</body>
</html>
