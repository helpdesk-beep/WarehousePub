<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Godown_insurance_JVS21_22.aspx.cs" Inherits="JointVentureScheme_Godown_insurance_JVS21_22" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Warehouse Inspection</title>
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
            function preventInput(evnt) {
                //Checked In IE9,Chrome,FireFox
                if (evnt.which != 9) evnt.preventDefault();
           }



           function NumberOnly(e) {
               var charCode = (e.which) ? e.which : e.keyCode;
               if ((charCode >= 48 && charCode <= 57)) {
                   return true;
               }
               if (charCode == 46) { return true; }
               if (charCode == 8) { return true; }
               if (charCode == 9) { return true; }
               else { return false; }
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
            height: 10px;
        }

        </style>
<style type="text/css">
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        width: 80%;
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding:0
      
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
</style>         
</head>
<body>

<div id="bg" style="background-color:White; color:Black">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
       
        <div>
                            <form id="form1" runat="server">
                              <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager> 
                                <center>
                                
          <div>

          </div>       
                                   
                    <table style="width:100%">
                         <tr >
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
                        </tr>                        
                    <tr>
                      <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White" align="center">
                          <p style="font-size: 14px; color: Black;">
                         Insurance For Offered Godown(JVS 2021-22) </p>
                      </td>

                  </tr>                        
                        <tr>
                        <td colspan="4" align="center">
                                  <table>
                             <%--     <tr>
                                  <td colspan="4"> <p style=" color:Red;">महत्वपूर्ण नोट :-<br />
                                   कृपया समस्त जानकारी अँग्रेजी भाषा मे टाइप करे। <br />
                                  यदि WHMS ड्रॉपडाउन मे गोदाम का नाम नहीं दिख रहा हे ओर वह गोदाम पूर्व मे भी आफ़र किया गया हे एवं WHR जारी करने के लिए लॉगिन भी उपलब्ध कराया गया तो पहले शुनिश्चित करे की वह गोदाम Verified/Existance List मे हो।
                                  </p>
                                  </td>
                                  </tr>--%>
          <tr>
          <td>
                   <asp:Label ID="lblWarehouse" runat="server" Text="Warehouse Name "></asp:Label>
                   &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlWarName" runat="server"  OnSelectedIndexChanged="ddlWarName_SelectedIndexChanged" AutoPostBack="true"
                            Width="200px" Height="25px" 
                        >
                        </asp:DropDownList> 
                        
            
          </td>

              <td>
                <asp:Label ID="hname" runat="server" Text="क्या गोदाम में रवि गोदाम का इंश्योरेंस  हैं ?"></asp:Label>
                  <asp:DropDownList runat="server" ID="ddlinsurance" OnSelectedIndexChanged="ddlinsurance_SelectedIndexChanged" AutoPostBack="true">
                          <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                      <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                       <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                  </asp:DropDownList>
              </td>
          </tr>
          </table>
                        </td>
                        </tr>                        
                      
             </table>

                                 <br />  
                      <table id="GAll" runat="server" visible="false">    
                          <tr>
                              <td></td>
                               <td></td>
                               <td><p style="color:red; font-size:15px;">Note-:Insurance Capacity मे गोदाम की क्षमता दर्ज करे |</p></td>
                               <td></td>
                          </tr>
                    
                          
                          <tr>

                                             <td >                     
                           <asp:Label ID="Date" Text="Date Of Insurance " runat="server"></asp:Label>   </td> 
                                      <td>
                        <asp:TextBox ID="txtSLDate" runat="server" CssClass="text"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtSLDate"></cc1:CalendarExtender>
                    </td>   



                                                                <td >                     
                           <asp:Label ID="Label4" Text="Validity  Of Insurance Date " runat="server"></asp:Label>   </td> 
                                      <td>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="text"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="TextBox1"></cc1:CalendarExtender>
                    </td>  
                          </tr>

                      <tr >
                
                               
            <td >                     

                <asp:Label ID="Label1" Text="Insurance Capacity" runat="server"></asp:Label> </td> 
                <td>   <asp:TextBox ID="txtInsuranceCapacity"  onkeypress="return NumberOnly(event)" runat="server"></asp:TextBox>                                            
                  
                     </td>  
                          
                              <td >                  

                           <asp:Label ID="Label2" Text="Value Of Stock" runat="server"></asp:Label> </td>  
                            <td >   <asp:TextBox ID="txtvaluestk"  onkeypress="return NumberOnly(event)" runat="server"></asp:TextBox>                                            
                  
                     </td>  

               </tr> 
                  <tr>

                    

                 <td >             

                           <asp:Label ID="Label3" Text="Premium " runat="server"></asp:Label> </td> 
                            <td >    <asp:TextBox ID="txtPremium"  onkeypress="return NumberOnly(event)" runat="server"></asp:TextBox>                                             
                  
                     </td> 
                                      </tr>

                          <tr>
                              <td colspan="2" style="text-align:center">
                                  <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Save" CssClass="active" style=" margin-left: 315px; width: 39%; height: 28px; background: darkgreen; color: #fff;" />
                              </td>
                          </tr>
                                    </table>






                                         <table id="Table1" runat="server" visible="false">    
                          
                

   

                          <tr>
                              <td colspan="2" style="text-align:center">
                                  <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save" CssClass="active" style=" margin-left: 115px; width: 39%; height: 28px; background: darkgreen; color: #fff;" />
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
                            </div>
    </div>
    </div>
</body>
</html>