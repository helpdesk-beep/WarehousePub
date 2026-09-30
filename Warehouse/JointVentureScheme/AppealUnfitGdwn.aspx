<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppealUnfitGdwn.aspx.cs" Inherits="JointVentureScheme_AppealUnfitGdwn" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Appeal Unfit Godown</title>
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
    border: 2px solid #008CBA;
}

.button6:hover {
    background-color: #008CBA;
    color: white;
}
    </style>        
</head>
<body>

<div id="bg" style="background-color:White">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="150" />
       
        <div>
                            <form id="form1" runat="server">
                                 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
                                <center>
                    <table style="width:100%; font-size:14px;">
                        
                        <tr >
                            <td colspan="4" style="font-size: medium;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                             <asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/WarehouseHome.aspx" ForeColor="White"></asp:LinkButton>
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
                                <td colspan="2" style="font-size: medium;" align="center">
                                    <asp:RadioButton ID="RadioButton1" Text="Appeal for Unfit Godown" 
                                        GroupName="StateLicence" runat="server"  AutoPostBack="true"
                                        oncheckedchanged="RadioButton1_CheckedChanged" />    
                                </td>
                                <td colspan="2" style="font-size: medium;" align="center">
                                    <asp:RadioButton ID="RadioButton2" GroupName="StateLicence" AutoPostBack="true" 
                                        Text="Appeal for Scheme/Category Change Godown" runat="server" Enabled="false"
                                        oncheckedchanged="RadioButton2_CheckedChanged" /> 
                                </td>                                
                        </tr>
                     <tr>
                    <td style="color:Red" >&nbsp&nbsp Note :</td>
                    </tr> 
                 <tr>
                      <td colspan="4">
                          <p color:#008080;" style="color:Red">
                         &nbsp;&nbsp; 1. अपात्र गोदामो का अपील निरीक्षण ऑनलाइन अपडेट दिनांक से 5 दिवस के भीतर ही किया जाना संभव हे। </p>
                      </td>
                    </tr>                         
               
            <%--     ------------TR Start for UNFIT ---------------------%>          
                        
                        <tr id="trUnfit" runat="server" visible="false">
                            <td colspan="4">
                                <table style="width: 100%;">
<tr>

                      <td colspan="4"   align="center">
                          <p style="font-size: medium; font-weight:bold; color: #008080; width:100%;">
                         &nbsp&nbsp Appeal for Unfit Godown</p>
                      </td>
                                                </tr>
<tr>
<%--                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                         &nbsp&nbsp  </p>
                      </td>--%>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="Letf">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         UNFIT Godown Details</p>
                      </td>                  

                  </tr>
                  
<tr>
                <td colspan="4" align="center" style="font-size:small;">
                        <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" 
                            Width="80%" EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID." />
                            <asp:BoundField DataField="Godown_No" HeaderText="Godown No." />
                           
                            <asp:BoundField DataField="G_OfferCapacity" HeaderText="Offer Capacity" />
                            <asp:BoundField DataField="G_Scheme" HeaderText="Offer Scheme" />
                            <asp:BoundField DataField="Offer_Date" HeaderText="Offer Date" />
                            <asp:BoundField DataField="Fit_Unfit" HeaderText="Inspection Status" />
                            <asp:BoundField DataField="Insp_Date" HeaderText="Inspection Date" /> 
                            <asp:BoundField DataField="Godown_Offer_Id" HeaderText="Godown_Offer_Id" /> 
                            <asp:BoundField DataField="Inspection_Id" HeaderText="Inspection_Id" />                           
                                <asp:TemplateField HeaderText="Select To Appeal">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="ckstack" runat="server" AutoPostBack="True" OnCheckedChanged="ckstack_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
                    </td>            
            </tr>                                                                  
                                                
<tr>
<%--                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                         &nbsp&nbsp  </p>
                      </td>--%>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="Letf">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         Important aspect of Warehouse to become UNFIT</p>
                      </td>                       

                  </tr>
                  <tr style=" height:30px;">
                    <td style="width:350px;">
                       1.क्या गोदाम निर्माणाधीन है ?  :
                    </td>
                    <td style="font-weight:bold;width:30px;">
                        <asp:Label ID="lblnirman" runat="server"></asp:Label>
                    </td>
                          <td style="width:350px;">
                              2.	क्या गोदाम विवादग्रस्त यथा गोदाम के मालिकाना हक, देनदारियां एवं माननीय न्यायालयों में प्रकरण लंबित/विचाराधीन हैं ?
                    </td>
                    <td style="font-weight:bold;width:30px;">
                        <asp:Label ID="lblvivad" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr style=" height:30px;"> 
                          <td >
                             3.	क्या गोदाम क्षतिग्रस्त है ?  :
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblchatigrast" runat="server"></asp:Label>
                    </td>
                      <td >
                        4.क्या गोदाम में पूर्व से शासकीय अथवा अन्य जमाकर्ता का स्कंध भण्डारित है ?
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblpurwaskandh" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr style=" height:30px;">
                  
                          <td >
                           5.	क्या गोदाम ’’ब्लेक लिस्टेड’’ है ? :
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblblack" runat="server"></asp:Label>
                    </td>
                  
                          <td >
                             6.	क्या गोदाम की भण्डारण क्षमता एक परिसर में न्यूनतम 500 मे.टन से कम है ?
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblnottake" runat="server"></asp:Label>
                    </td>
                    </tr>
                    
                    <tr style=" height:30px;">
                  
                          <td >
                            7. क्या गोदाम वैज्ञानिक भंडारण हेतु अयोग्य हे ( यदि गोदाम योग्य हे तो यह भी सुनिश्चित करे की वायुसंचरण हेतु रोशनदान हो तथा गोदाम की ऊंचाई कम से कम 14 फिट है)?
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lbldanege" runat="server"></asp:Label>
                    </td>

                  
                          <td >
                            8. क्या ऑनलाइन ऑफर संबंधी जानकारी गलत दी गयी है ?
                    </td>
                    <td style="font-weight:bold;">
                        
                        <asp:Label ID="lblwrongdetail" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr style="color:Red;height:30px">
                          <td colspan="2" >
                           अनफ़िट करने का विवरण(रिमार्क) :
                    </td>
                    <td style="font-weight:bold;" colspan="2">
                        <asp:Label ID="lblremark" runat="server"></asp:Label>
                    </td>
                    </tr>                    
                                                                                
                                         <tr>
                    <td ></td>
                    </tr>
                   <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="Letf">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Discription For Appeal</p>
                      </td>                       
                  </tr>
                     <tr>
                    <td ></td>
                    </tr> 
                  <tr>
                    <td >
                       &nbsp&nbsp&nbsp Discription For Appeal :
                    </td>
                    <td colspan="2">
                        <textarea id="txtRemark" maxlength="200" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:250px;"
                        ></textarea>                    
                    </td> 
                  </tr>
                  <tr>

                    <td align="right" colspan="4">
                        <asp:Button ID="btnUnfit" runat="server" Text="Submit" 
                            class="button button2" Width="100px" Height="30px" onclick="btnUnfit_Click" 
                            ></asp:Button>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    </td>
                  </tr>                                    
                     <tr>
                    <td style="color:Red" >&nbsp&nbsp Note :</td>
                    </tr> 
                 <tr>
                      <td colspan="4">
                          <p color:#008080;" style="color:Red">
                         &nbsp;&nbsp; 1. गोदाम संचालक द्वारा अनफ़िट गोदाम के विरुद्ध यदि कार्यवाही कर ली गई हे तो उसका विवरण दर्ज कर Submit करें । <br />
                         &nbsp;&nbsp; 2. यदि विवरण हिन्दी मे टाइप करना हे तो हिन्दी यूनिकोड का उपयोग करें ।
                         
                          </p>
                      </td>
                    </tr>                   
                   
                                
                    <tr>
                    <td ></td>
                    </tr>                                    
                                
                                </table>
                            </td>
                        </tr>
                        
                   <%--     ------------TR End for UNFIT ---------------------%>   
                   
                   
<%--     ------------TR Start for Scheme ---------------------%>          
                        
                        <tr id="trscheme" runat="server" visible="false">
                            <td colspan="4">
                                <table style="width: 100%;">
<tr>

                      <td colspan="4"   align="center">
                          <p style="font-size: medium; font-weight:bold; color: #008080; width:100%;">
                         &nbsp&nbsp Appeal for Change Scheme</p>
                      </td>
                                                </tr>
<tr>
<%--                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                         &nbsp&nbsp  </p>
                      </td>--%>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="Letf">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         Godown Details</p>
                      </td>                  

                  </tr>
                  
<tr>
                <td colspan="4" align="center" style="font-size:small;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            Width="80%" EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                            GridLines="Horizontal">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID." />
                            <asp:BoundField DataField="Godown_No" HeaderText="Godown No." />
                            <asp:BoundField DataField="G_OfferCapacity" HeaderText="Offer Capacity" />
                            <asp:BoundField DataField="G_Scheme" HeaderText="Offer Scheme" />
                            <asp:BoundField DataField="Offer_Date" HeaderText="Offer Date" />
                            <asp:BoundField DataField="Insp_Offer_Scheme" HeaderText="Offer Scheme (After Inspection)" />
                            <asp:BoundField DataField="Fit_Unfit" HeaderText="Inspection Status" />
                            <asp:BoundField DataField="Insp_Date" HeaderText="Inspection Date" />
                            <asp:BoundField DataField="Godown_Offer_Id" HeaderText="Godown_Offer_Id" />                           
                                <asp:TemplateField HeaderText="Select To Appeal">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="chkschemegdwn" runat="server" AutoPostBack="True"  OnCheckedChanged="chkschemegdwn_CheckedChanged"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
                    </td>            
            </tr>                                                                  
                                                
<tr>
<%--                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                         &nbsp&nbsp  </p>
                      </td>--%>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="Letf">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         Important aspect of Godown to become Category/Scheme</p>
                      </td>                       

                  </tr>
                  <tr style=" height:30px;">
                    <td style="width:350px;">
                       1.क्या गोदाम परिसर में मानक क्षमता का चालू हालत में इलेक्ट्रानिक वेब्रिज है :
                    </td>
                    <td style="font-weight:bold;width:30px;">
                        <asp:Label ID="lblWeibrige" runat="server"></asp:Label>
                    </td>
                          <td style="width:350px;">
                              2.क्या गोदाम के प्रत्येक गेट/शटर पर 'जालीदार गेट/शटर' है
                    </td>
                    <td style="font-weight:bold;width:30px;">
                        <asp:Label ID="lblgate" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr style=" height:30px;"> 
                          <td >
                             3.क्या गोदाम परिसर को सुरक्षा व्यवस्था हेतु बाउण्ड्री बाउण्ड्रीवाॅल / चैनलिंग फेंसिंग / बार्बेड वायर फेंसिंग से कवर्ड है
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblboundry" runat="server"></asp:Label>
                    </td>
                      <td >
                        4.क्या गोदाम में डामरीकृत रोड/सीसी.रोड/WBM है
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblroadtype" runat="server"></asp:Label>
                    </td>
                    </tr>                   
                                                                                
                                         <tr>
                    <td ></td>
                    </tr>
                   <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="Letf">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Discription For Appeal</p>
                      </td>                       
                  </tr>
                     <tr>
                    <td ></td>
                    </tr> 
                  <tr>
                    <td >
                       &nbsp&nbsp&nbsp Discription For Appeal :
                    </td>
                    <td colspan="2">
                        <textarea id="Textarea1" maxlength="200" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:250px;"
                        ></textarea>                    
                    </td> 
                  </tr>
                  <tr>

                    <td align="right" colspan="4">
                        <asp:Button ID="Button2" runat="server" Text="Submit" 
                            class="button button2" Width="100px" Height="30px" ></asp:Button>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    </td>
                  </tr>                                    
                     <tr>
                    <td style="color:Red" >&nbsp&nbsp Note :</td>
                    </tr> 
                 <tr>
                      <td colspan="4">
                          <p color:#008080;" style="color:Red">
                         &nbsp;&nbsp; 1. गोदाम संचालक द्वारा आफर की श्रेणी के विरुद्ध यदि कार्यवाही कर ली गई हे तो उसका विवरण दर्ज कर Submit करें । </p>
                      </td>
                    </tr>                   
                   
                                
                    <tr>
                    <td ></td>
                    </tr>                                    
                                
                                </table>
                            </td>
                        </tr>
                        
                   <%--     ------------TR End for Scheme ---------------------%> 
                   
                                                            
                                                       
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
                              Successfully Update Inspection
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
