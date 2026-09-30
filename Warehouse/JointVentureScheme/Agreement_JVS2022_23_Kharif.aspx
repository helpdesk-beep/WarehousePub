<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Agreement_JVS2022_23_Kharif.aspx.cs" Inherits="JointVentureScheme_Agreement_JVS2022_23_Kharif" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Godown Agreement</title>
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

        .style2
        {
            height: 11px;
        }

        .style4
        {
            height: 8px;
        }

        .style5
        {
            width: 954px;
            height: 33px;
        }

        </style>
        <script type="text/javascript">
            function preventInput(evnt) {
                //Checked In IE9,Chrome,FireFox
                if (evnt.which != 9) evnt.preventDefault();
            }
        </script>

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

<div id="bg" style="background-color:White">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
       
        <div>
                            <form id="form1" runat="server">
                              <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager> 
                                <center>
                                
          <div>

          </div>                                
                    <table style="width: 100%">
<%--                        <tr >
                            <td  colspan="4" style="background-color: #66CCFF"> 
                                <p style="font-size: medium; color: #008080; width: 953px;">&nbsp&nbsp&nbsp<asp:LinkButton 
                                        ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Agreement For Inspected Godown&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/JointVentureScheme/Logins.aspx">Log out</asp:LinkButton></p>
                            </td>
                        </tr>--%>
                        
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
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="White">Log out</asp:LinkButton></td>
                            </tr>
                            </table>
                            </td>
                        </tr>                          
                  <tr>
                      <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White ; width: 100%;" align="center">
                          <p style="font-size: 14px; color: Black;">
                          Agreement For Inspected Godown JVS 2022-23</p>
                      </td>
                  </tr>                        
                        <tr>
                        <td colspan="4" align="center">
                                  <table><tr><td class="style4"></td></tr>
          <tr>
          <td>

                        Agreement

                        Season &nbsp&nbsp&nbsp&nbsp
                        <asp:DropDownList ID="ddl_session" runat="server" Width="150px"  Height="25px" 
                                          onselectedindexchanged="ddl_session_SelectedIndexChanged" AutoPostBack="true" Enabled="false" >
                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                        <asp:ListItem Value="JVS2022_23_Kharif" Selected="True">JVS 2022-23 Kharif</asp:ListItem>
                       
                        </asp:DropDownList>                                  

          &nbsp&nbsp&nbsp&nbsp
          
          
                   <asp:Label ID="lblWarehouse" runat="server" Text="Warehouse Name "></asp:Label>
                   &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlWarName" runat="server"
                            Width="200px" Height="25px" AutoPostBack="true"
                         onselectedindexchanged="ddlWarName_SelectedIndexChanged">
                        </asp:DropDownList> 
                        
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblgodown" runat="server" Text="Godown No."></asp:Label>&nbsp;&nbsp;<asp:DropDownList 
                       ID="ddlgodown" runat="server" 
                            Width="100px" Height="25px" 
                       AutoPostBack="true" 
                       onselectedindexchanged="ddlgodown_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          </td>
          </tr>
          <tr><td class="style4"></td></tr>
          </table>
                        </td>
                        </tr>
                        </table>
                        <div id="divOwner" runat="server">
                        <table style="width: 100%; font-size:13px; color:Black">
                   <tr>
                      <td colspan="4" style=" background-color:#66CCFF; height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold"> A) Godown Link As per WHMS System</p>
                      </td>                      

                  </tr><tr><td class="style4"></td></tr>
                  <tr>
                  
                  <td colspan="4" align="center">District &nbsp;&nbsp;
                  <asp:DropDownList ID="DDLDistrict" runat="server"  AutoPostBack="true"
                            Width="150" Height="27px" onselectedindexchanged="DDLDistrict_SelectedIndexChanged" 
                            >
                        </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                  Branch &nbsp;&nbsp;<asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true"
                            Width="150" Height="27px" 
                          onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                    </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                 Godown(As per WHMS) &nbsp;&nbsp;<asp:DropDownList ID="ddlGodownWHMS" runat="server" AutoPostBack="true"
                            Width="200" Height="27px" >
                    </asp:DropDownList> 
                  
                  </td>
                  </tr>
                            
                        <tr><td class="style4"></td></tr>
                         <tr>
                      <td colspan="4" style=" background-color:#66CCFF; height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold"> B) Warehouse Owner Details</p>
                      </td>                      

                  </tr>
                    <tr><td class="style4"></td></tr>
                  <tr>
                    <td style="height:20px;width:225px">
                        Authorised Person :
                    </td>
                    <td>
                        <asp:Label ID="lblAuthorised" runat="server" Text=""></asp:Label>

                    </td >
                          <td>
                             Registered EmailId :
                    </td>
                    <td >
                     <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>


                    </td>
                    </tr>
                    <tr>
                    <td style="height:20px">
                        Office Contact No/Mobile No :
                    </td>
                    <td class="style2">
                     <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label>

                    </td>
                          <td class="style2">
                             Inspected Vacant Capacity :
                    </td>
                    <td class="style2">
                    <asp:Label ID="lblVacant" runat="server" Text=""></asp:Label>

               
                    </td>
                    </tr>
                    <tr>
                    <td style="height:20px">
                           District  :
                    </td>
                    <td>
                       <asp:Label ID="lblDistrict" runat="server" Text=""></asp:Label>

                    </td>
                          <td>
                              Tehsil :
                    </td>
                    <td>
                                         <asp:Label ID="lblTehsil" runat="server" Text=""></asp:Label>

                    </td>
                    </tr>
                    

                    
                    <tr>
                    <td style="height:20px">
                           Offer Category  :
                    </td>
                    <td>
                       <asp:Label ID="lblofferscheme" runat="server" Text=""></asp:Label>

                    </td>
                          <td>
                             Inspected Category :
                    </td>
                    <td>
                                         <asp:Label ID="lblinspscheme" runat="server" Text=""></asp:Label>

                    </td>
                    </tr>
                    
                    <tr>
                    <td style="height:20px">
                           Licence Type  :
                    </td>
                    <td>
                       <asp:Label ID="lbllictype" runat="server" Text=""></asp:Label>

                    </td>
                    </tr>                     
                    
                    <tr>
                    
                    
                    <td style="height:20px">
                           Licence No  :
                    </td>
                    <td>
                       <asp:Label ID="lbllicno" runat="server"></asp:Label>

                    </td>
                          <td>
                              Licence Validity Date :
                    </td>
                    <td>
                                         <asp:Label ID="lbllicexpdate" runat="server"></asp:Label>

                    </td>
                    </tr>
                                                                                 
                        <tr><td class="style4"></td></tr>
                        </table>
                                    <table style="width: 100% ; font-size:13px; color:Black">
                                    
                   <tr>
                      <td colspan="4" style=" background-color:#66CCFF; height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold"> C) Agreement Details</p>
                      </td>
                  </tr>
                  
                  
                  
                  <tr><td class="style4"></td></tr>
                     <tr>
                    <td></td>
                    </tr> 
                                         <tr>
                    <td style="height:20px">
                        Agreement Signing date : 
                    </td>
                    <td>
                    <asp:TextBox ID="txtAgreementSign" runat="server" class="text" type="text" 
                            onkeydown="javascript:preventInput(event);" onpaste="return false;" 
                           ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtAgreementSign"></cc1:CalendarExtender>
                    </td >
                    
                          <td>
                            Agreement Duration(in Months) :
                    </td>
                    <td >
                     <asp:TextBox ID="txtAgreementDur"   runat="server" class="text" type="text" ></asp:TextBox>
                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAgreementDur"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>

                    </td>
                    </tr>
                 <tr>
                    <td style="height:20px;width:225px">
                        Agreement End date :
                    </td>
                    <td>
                    <asp:TextBox ID="txtAgreeEnd" runat="server" class="text" type="text"  
                            onkeydown="javascript:preventInput(event);" onpaste="return false;" 
                            ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender3" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtAgreeEnd"></cc1:CalendarExtender>

                    </td >
                          <td>
                             Agreemented Capacity :
                    </td>
                    <td >
                     <asp:TextBox ID="txtInspectedCpt" runat="server" class="text" type="text"   ></asp:TextBox>
<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtInspectedCpt"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>

                    </td>
                    </tr>
                    <tr>
                    <td>
                             Value of Stamp paper in Rs. :
                    </td>
                    <td >
                     <asp:TextBox ID="txtStamp" runat="server" class="text" type="text"   >1000</asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtStamp"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>

                    </td>
                    </tr>
                                        
                                        <tr>
                    <td style="height:20px">
                        Purchase Date of Stamp paper :
                    </td>
                    <td>
                    
                        <asp:TextBox ID="txtDateOfPurchase" runat="server" class="text" type="text"  onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender4" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtDateOfPurchase"></cc1:CalendarExtender>
                    </td >
                                              <td>
                             Stamp Code/Id :
                    </td>
                    <td >
                     <asp:TextBox ID="txtStampCode" runat="server" class="text" type="text"  ></asp:TextBox>
                           


                    </td>
                                            </tr>
                                        <tr>
                          <td>
                            Name of first Party on Stamp Paper :
                    </td>
                    <td >
                     <asp:TextBox ID="txtFirstPart" runat="server" class="text" type="text" 
                            text="MPWLC" ReadOnly="True" >MPWLC</asp:TextBox>


                    </td>
                                            <td style="height:20px">
                       Name of Second Party on Stamp Paper :
                    </td>
                    <td>
                    <asp:TextBox ID="txtSecondPart" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                    </tr>
<tr><td class="style4"></td></tr>
<tr><td class="style4"></td></tr>
<tr><td class="style4"></td></tr>
                    
                    
                                         <tr>
                    <td colspan="4"> <p> <asp:CheckBox ID="chkDeclaration" runat="server"></asp:CheckBox>  We hereby declare and affirm that the information provided by us is true and correct to the best of our knowledge and found during the joint inspection by the team.</p><br />

</td>

                    </tr>
                    <tr>
                    <td colspan="4"> <p style=" color:Red;">नोट :- <br />1.गोदाम के अनुबंध संबन्धित समस्त महत्वपूर्ण दस्तावेज़ आगामी निर्देश तक कार्यालय मे संग्रहीत करके रखे । <br />
                    2. WDRA अथवा आयुक्त,खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश का वैध लायसेंस होना अनिवार्य हे ।<br />
                    3. यदि आयुक्त,खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश का लायसेंस हे तो WHMS लॉगिन मे Joint Venture का प्रयोग करे |<br />
                    4. यदि WDRA का  लायसेंस हे तो WHMS लॉगिन मे WDRA का प्रयोग करे |<br />
                    5. यदि गोदाम पहली बार आफर हुआ हे तो WHMS मे लॉगिन के लिए डिफ़ाल्ट पासवर्ड wlc2015 का प्रयोग करे |
                    </p>
                    
</td>

                    </tr>
                                                            <tr>
                    
                        
                                                           
                                 <td align="center" colspan="4">
                                 <br /> <br /> 
                                 
                        <asp:Button ID="btnSubmit" runat="server" class="submit" Text="Submit" align="right" Width="75px"
                                         onclick="btnSubmit_Click" />
                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                         
                       <asp:Button ID="Button1" runat="server" class="submit" Text="New" align="right" Width="75px" 
                                         onclick="Button1_Click"/>
                                         
                      </td> 
                    </tr> 
                    
                                        </table>
<asp:Label ID="Label5" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlofferpopup" TargetControlID="Label5"
   BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlofferpopup" runat="server" CssClass="modalPopup" Height="150px" Width="250px" >
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td style="color:White; font-weight:bold" align="center">Confirmation Message</td>
                   <td> </td>
            </tr> 
                                         
        </table> 
    </div>
    <div class="body">
<table align="center" style="width: 100%;border:#008CBA; border-style:solid ; border-width:0px;">
<tr><td style="height:10px;"></td></tr>
 
                        <tr>
                        <td align="center" >
                              Successfully Update Agreement
                        </td>
                        </tr>
                    <tr><td style="height:10px;"></td></tr>  
                    <tr>
                            <td align="center" >
                                       <asp:Button class="button button2" Width="100px" Height="30px" ID="Button3" 
                                        runat="server" Text="Ok" align="Center" onclick="Button3_Click"/>
                                                                            
                            </td>
                    </tr>                                                                                                                                                                                                 
</table>                   
    </div>                        
</asp:Panel> 
                                        
                                        </div>
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
                                            <b>© 2021 &nbsp;National Informatics Centre.All Rights Reserved
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
