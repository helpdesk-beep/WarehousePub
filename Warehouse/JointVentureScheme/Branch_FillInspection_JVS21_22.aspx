<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Branch_FillInspection_JVS21_22.aspx.cs" Inherits="JointVentureScheme_Branch_FillInspection_JVS21_22" %>

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
                         Inspection For Offered Godown(JVS 2021-22) </p>
                      </td>

                  </tr>                        
                        <tr>
                        <td colspan="4" align="center">
                                  <table>
                                  <tr>
                                  <td colspan="4"> <p style=" color:Red;">महत्वपूर्ण नोट :-<br />
                                   कृपया समस्त जानकारी अँग्रेजी भाषा मे टाइप करे। <br />
                                  यदि WHMS ड्रॉपडाउन मे गोदाम का नाम नहीं दिख रहा हे ओर वह गोदाम पूर्व मे भी आफ़र किया गया हे एवं WHR जारी करने के लिए लॉगिन भी उपलब्ध कराया गया तो पहले शुनिश्चित करे की वह गोदाम Verified/Existance List मे हो।
                                  </p>
                                  </td>
                                  </tr>
          <tr>
          <td>
                   <asp:Label ID="lblWarehouse" runat="server" Text="Warehouse Name "></asp:Label>
                   &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlWarName" runat="server" AutoPostBack="true" 
                            Width="200px" Height="25px" 
                         onselectedindexchanged="ddlWarName_SelectedIndexChanged">
                        </asp:DropDownList> 
                        
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblgodown" runat="server" Text="Godown No."></asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="true" 
                            Width="100px" Height="25px" onselectedindexchanged="ddlgodown_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Godown ID (AS Per WMS)"></asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="ddlWMSGodownID" runat="server"
                            Width="200px" Height="25px"> </asp:DropDownList>
          </td>
          </tr>
          </table>
                        </td>
                        </tr>
                        
                         <tr>
<%--                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                          A) Inspection Officer Detail  </p>
                      </td>--%>
                      <td colspan="4" style=" background-color:#66CCFF; height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold"> A) Inspection Officer Detail</p>
                      </td>                       

                  </tr>
             </table>
                                 
                        
              <table style="width:100%">
                    <tr>
                    <td>
                        शाखा प्रबन्धक का नाम :
                    </td>
                    <td>
                     <asp:TextBox ID="txtBranchBM" runat="server" class="text" type="text" ReadOnly="true" ></asp:TextBox>

                    </td>
                          <td >
                             निरीक्षण दिनांक :
                    </td>
                    <td>
                    <asp:TextBox ID="txtInspDate" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtInspDate"></cc1:CalendarExtender>
                    </td>
                    </tr>              
                   <tr>
                      <td colspan="4" style="background-color:#66CCFF;height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold"> B)	Inspected Warehouse Campus Details</p>
                      </td>                       
                  </tr>
                     <tr>
                    <td></td>
                    </tr> 
                                         <tr>
                    <td style="height:20px">
                        जिले का नाम :
                    </td>
                    <td>
                    <asp:DropDownList ID="DDLDistrict" runat="server" Enabled="false"
                            Width="232px" Height="27px">
                        </asp:DropDownList>
                    </td >
                    
                          <td>
                            तहसील का नाम :
                    </td>
                    <td >
                     <asp:DropDownList ID="DDLTehsil" runat="server" Enabled="false"
                            Width="232px" Height="27px">
                     </asp:DropDownList>

                    </td>
                    </tr>
                     <tr>
                    <td>
                             विकासखंड का नाम  :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlblocknew" runat="server" Enabled="false"
                            Width="232px" Height="27px">
                    </asp:DropDownList>
                    </td>
                                                          <td style="height:20px">
                       गाँव का नाम :
                    </td>
                    <td>
                     <asp:DropDownList ID="ddlvillage" runat="server" 
                            Width="232px" Height="27px">
                    </asp:DropDownList>

                    </td >
                    </tr>                      
                     <tr>
                    <td>
                             वेअरहाउस की निकटतम शाखा का नाम  :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlNearBranch" runat="server" Enabled="false"
                            Width="232px" Height="27px">
                    </asp:DropDownList>
                    </td>
                                                          <td style="height:20px">
                       गोदाम से दूरी(km) :
                    </td>
                    <td>
                    <asp:TextBox ID="txtDistance" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                    </tr>                    
                 <tr>
                    <td style="height:20px;width:225px">
                        वेअरहाउस/गोदाम का नाम :
                    </td>
                    <td>
                    <asp:TextBox ID="txtWareName" runat="server" class="text" type="text"></asp:TextBox>

                    </td >
                          <td>
                             वेअरहाउस/गोदाम का  नंबर :
                    </td>
                    <td >
                     <asp:TextBox ID="txtGodNo" runat="server" class="text" type="text" ReadOnly="true"   ></asp:TextBox>


                    </td>
                    </tr>
                                        
                                        <tr>
                    <td style="height:20px">
                        वेअरहाउस का पता :
                    </td>
                    <td>
                    
                        <textarea id="txtGodAdd" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td >
                                              <td>
                             वेअरहाउस परिसर का कुल क्षेत्रफल (in Hect.):
                    </td>
                    <td >
                     <asp:TextBox ID="txtWareArea" runat="server" class="text" type="text"  ></asp:TextBox>
                           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtWareArea"
                                        ValidChars="0123456789.">
                         </cc1:FilteredTextBoxExtender>


                    </td>
                                            </tr>
                                        <tr>
                          <td>
                             अक्षांश :
                    </td>
                    <td >
                     <asp:TextBox ID="txtLatitude" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
                                            <td style="height:20px">
                        देशान्त :
                    </td>
                    <td>
                    <asp:TextBox ID="txtLongitude" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                    </tr>
                                                            <tr>
                    
                    <td>
                             वेअरहाउस कार्यालय का मोबाइल नंबर : 
                    </td>
                    <td >
                     <asp:TextBox ID="txtGodContact" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
                                                                <td style="height:20px">
                        वेअरहाउस प्रभारी का मोबाइल नंबर :
                    </td>
                    <td>
                    <asp:TextBox ID="txtInchMbNo" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                    </tr>

                    
                    <tr>
                    
                          <td>
                             वेअरहाउस की निकटतम रेल्वे रेक पॉइंट  का नाम  :
                    </td>
                    <td>
                    <asp:TextBox ID="txtrackpoint" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                                                          <td style="height:20px">
                       गोदाम से दूरी(km) :
                    </td>
                    <td>
                    <asp:TextBox ID="txtrackpintdist" runat="server" class="text" type="text" ></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtrackpintdist"
                                        ValidChars="0123456789.">
                         </cc1:FilteredTextBoxExtender>

                    </td >
                    </tr>
                     <tr>
                    <td></td>
                    </tr> 
                                        </table>
                                    <table>
                   <tr>
                      <td colspan="4" style="background-color:#66CCFF;height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold"> C)	Warehouse Owner Details </p>
                      </td>                      
                  </tr>
                     <tr>
                    <td></td>
                    </tr> 
                 <tr>
                    <td style="height:20px;width:225px">
                        वेअरहाउस  संचालक  का नाम :
                    </td>
                    <td>
                    <asp:TextBox ID="txtOwnerName" runat="server" class="text" type="text" ></asp:TextBox>

                    </td >
                          <td style="height:20px;width:250px">
                             वेअरहाउस  संचालक  का पता : 
                    </td>
                    <td >
                     <asp:TextBox ID="txtOwnerAdd" runat="server" class="text" type="text" ></asp:TextBox>


                    </td>
                    </tr>
                                         <tr>
                    <td style="height:20px">
                        वेअरहाउस  संचालक  का मोबाइल नंबर :
                    </td>
                    <td>
                    <asp:TextBox ID="txtOwnerMob" runat="server" class="text" type="text" ReadOnly="true"></asp:TextBox>
                    </td >
                          <td>
                            वेअरहाउस  संचालक  का email ID :
                    </td>
                    <td >
                     <asp:TextBox ID="txtOwnerEmail" runat="server" class="text" type="text"  ReadOnly="true"></asp:TextBox>


                    </td>
                    </tr>
                                        <tr>
                    <td style="height:20px">
                        PAN No.:
                    </td>
                    <td>
                    <asp:TextBox ID="txtPanNo" runat="server" class="text" type="text"  ></asp:TextBox>
                        
                    </td >
                          <td>
                             Aadhaar No.   :
                    </td>
                    <td >
                     <asp:TextBox ID="txtAadharNo" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
                    </tr>
                                               
                     <tr>
                    <td></td>
                    </tr> 
                   <tr>

                      <td colspan="4" style="background-color:#66CCFF;height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold"> D)	Inspected Warehouse / Godown  Details</p>
                      </td>                      
                  </tr>
                     <tr>
                    <td></td>
                    </tr> 
                    <tr>
                    <td style="height:20px">
                        लंबाई मापन(फीट) :
                    </td>
                    <td>
                    <asp:TextBox ID="txtLength" runat="server" class="text" type="text"></asp:TextBox>

                    </td >
                          <td>
                             चौडाई  मापन(फीट)  :
                    </td>
                    <td >
                     <asp:TextBox ID="txtWidth" runat="server" class="text" type="text"></asp:TextBox>


                    </td>
                    </tr>
                                         <tr>
                    <td style="height:20px">
                        ऊँचाई मापन(फीट)(अधिकतम 18 फिट) :
                    </td>
                    <td>
                    <asp:TextBox ID="txtHeight" runat="server" class="text" type="text"></asp:TextBox>

                    </td >
                          <td>
                             संगणित भण्डारण क्षमता(में टन )-सूत्र (LxWx(H-3)/80) अनुसार भण्डारण क्षमता  :
                    </td>
                    <td >
                     <asp:TextBox ID="txtMaxCpt" runat="server" class="text" type="text"></asp:TextBox>
                    </td>
                    </tr> <tr>
                    <td style="height:20px">
                        निर्माण वर्ष :
                    </td>
                    <td>
                    <asp:TextBox ID="txtConsYear" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                          <td>
                             गोदाम की एक दिन में अनुमानित उतराई क्षमता (मे.टन):
                    </td>
                    <td >
                     <asp:TextBox ID="txtUnloadCpt" runat="server" class="text" type="text"  ></asp:TextBox>
                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtUnloadCpt"
                                        ValidChars="0123456789.">
                         </cc1:FilteredTextBoxExtender>

                    </td>
                    </tr> <tr>
                    <td style="height:20px">
                       भण्डारण प्रकार :
                    </td>
                    <td>
                   <asp:DropDownList ID="ddlStorageType" runat="server"
                            Width="100" Height="27px" Enabled="false">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Covered </asp:ListItem>
                            <asp:ListItem Value="2">Permanent(CAP)</asp:ListItem>
                            <asp:ListItem Value="3">Temporary (CAP) </asp:ListItem>
                            <asp:ListItem Value="4">Silo Bag  </asp:ListItem>
                            <asp:ListItem Value="5">Steel Silo </asp:ListItem>
                        </asp:DropDownList>

                    </td >
                          <td>
                             वर्तमान में गोदाम में संग्रहित स्कंधो का नाम :
                    </td>
                    <td >
                     <asp:TextBox ID="txtCommodityName" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
                    </tr>
                           <tr>
                    <td style="height:20px">
                        गोदाम में संग्रहित स्कंध जमाकर्ता का नाम (MPSCSC/Markfed/Nafed/Private/Other)
                    </td>
                    <td>
                    <asp:TextBox ID="txtDepositorName" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                          <td>
                            वर्तमान में गोदाम में संग्रहित स्कंध की क्षमता (मे.टन)  :
                    </td>
                    <td >
                     <asp:TextBox ID="txtCurrentStoredComm" runat="server" class="text" type="text"  Text="0"></asp:TextBox>
                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtCurrentStoredComm"
                                        ValidChars="0123456789.">
                         </cc1:FilteredTextBoxExtender>                     

                    </td>
                    </tr>  
                                        <tr>
                    <td style="height:20px">
                        वर्तमान में गोदाम की रिक्त क्षमता (मे.टन) (Vacant Capacity) :
                    </td>
                    <td>
                    <asp:TextBox ID="txtvacantcpt" runat="server" class="text" type="text" ></asp:TextBox>

                    </td >
<%--                    <td>
                    गोदाम संचालक स्वतः अथवा आउट सोर्स के माध्यम से फ्यूमीगेशन कार्य करने हेतु सहमत / असहमत :
                    </td>
                    <td>
                                        <asp:DropDownList ID="ddlfumigation" runat="server" Height="25px" Width="232px"> 
                                            <asp:ListItem >--Select--</asp:ListItem>
                                            <asp:ListItem Value="Y">सहमत</asp:ListItem>
                                            <asp:ListItem Value="N">असहमत</asp:ListItem>
                                                                                   
                                        </asp:DropDownList>                    
                    </td>--%>
                    </tr>                                             
 
                   <tr>

                      <td colspan="4" style="background-color:#66CCFF;height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold"> E)	Warehouse/Godown Licence Details</p>
                      </td>                      
                  </tr>
                  <tr>
                        <td colspan="3">
                        क्या वेअरहाउस लायसेंस की वर्तमान वेधता है ?(WDRA अथवा आयुक्त,खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश का)
                        </td>
                        <td>
                                        <asp:DropDownList ID="ddllicchk" runat="server" Height="25px" Width="232px" 
                                            onselectedindexchanged="ddllicchk_SelectedIndexChanged" AutoPostBack="true"> 
                                            <asp:ListItem >--Select--</asp:ListItem>
                                            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="No"></asp:ListItem>                                
                                        </asp:DropDownList>                            
                        </td>
                  </tr>
                  <tr ID="trlic1" runat="server">
                        <td>
                        लायसेंस का प्रकार
                        </td>
                        <td>
                                        <asp:DropDownList ID="ddlgdwntype" runat="server" Height="25px" Width="232px"> 
                                            <asp:ListItem >--Select--</asp:ListItem>
                                            <asp:ListItem Value="68" Text="WDRA"></asp:ListItem>
                                            <asp:ListItem Value="63" Text="NON WDRA"></asp:ListItem>                                
                                        </asp:DropDownList>                            
                        </td>
                        <td>
                        लायसेंस नंबर 
                        </td>
                        <td>
                        <asp:TextBox ID="txtlicno" runat="server" class="text" type="text"></asp:TextBox>
                        </td>                                                
                  </tr>
                  
                  <tr ID="trlic2" runat="server">
                        <td>
                        जारी दिनांक
                        </td>
                        <td>
                        <asp:TextBox ID="txtlicissuedate" runat="server" class="text" type="text" ></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender4" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtlicissuedate"></cc1:CalendarExtender>                             
                        </td>
                        <td>
                        वैधता दिनांक
                        </td>
                        <td>
                        <asp:TextBox ID="txtlicExpdate" runat="server" class="text" type="text" ></asp:TextBox>
                                
                         <cc1:CalendarExtender ID="CalendarExtender3" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtlicExpdate"></cc1:CalendarExtender>                                
                        </td>                                                
                  </tr>
                  
                 <tr ID="trlicapplied1" runat="server">
                        <td colspan='2'>
                       यदि लायसेंस वैध नहीं है तो क्या इसे नवीनीकरण हेतु आवेदन किया गया है?तो उसका
                        </td>

                        <td>
                        आवेदन लायसेंस का प्रकार
                        </td>
                        <td>
                                        <asp:DropDownList ID="ddlappliedtype" runat="server" Height="25px"> 
                                            <asp:ListItem >--Select--</asp:ListItem>
                                            <asp:ListItem Value="0" Text="APPLIED For WDRA"></asp:ListItem>
                                            <asp:ListItem Value="00" Text="APPLIED For NON WDRA"></asp:ListItem>                                    
                                        </asp:DropDownList>                        
                        </td>                                                
                  </tr>
                  <tr ID="trlicapplied2" runat="server">
                        <td>
                        आवेदन क्रमांक
                        </td>
                        <td>
                        <asp:TextBox ID="txtlicappliedno" runat="server" class="text" type="text"></asp:TextBox>
                        </td>
                        <td>
                        आवेदन दिनांक
                        </td>
                        <td>
                        <asp:TextBox ID="txtlicappliceddate" runat="server" class="text" type="text"></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtlicappliceddate"></cc1:CalendarExtender>
                        </td>                                                
                  </tr> 
                  <tr>
                    <td align="center" colspan="4"><br />
                    <asp:Label ID="lbllicmessage" runat="server" ForeColor="Red"></asp:Label><br />
                    </td>
                  </tr>
                  
                  <tr>
                    <td colspan="4" align="right">
                    
                        <asp:LinkButton ID="lnklicdetail" Font-Underline="true" ForeColor="Blue"  
                            Font-Bold="true" Font-Size="11px" Text="जिले अनुसार जारी लायसेंस/पंजीयन धारको की सूची देखने के लिए क्लिक करे" 
                            runat="server" onclick="lnklicdetail_Click"></asp:LinkButton>
                    </td>
                  </tr>
                                    <tr>
                  
                    <td align="center" colspan="4">
                        <asp:Label ID="lblchklicnovalid" runat="server" Visible="false"></asp:Label>
                        
                        <asp:Button ID="btnvalidatelicno" runat="server" Text="Check Licence" class="submit" 
                            Width="150px" Height="30px" onclick="btnvalidatelicno_Click"  ></asp:Button>
                    </td>
                    </tr>
                     
                     <tr>
                    <td colspan="4"> <p style="color:Red;">नोट :- <br />1. दुर्ज किये गए गोदाम लायसेंस/पंजीयन नंबर को चेक करने के बाद ही गोदाम का निरीक्षण पुर्ण किया जाना संभव हे ।<br />
                    1. यदि गोदाम लायसेंस/पंजीयन नंबर गलत हे तो WDRA/खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश जिले अनुसार धारको की सूची देखने के लिए दी गई लिंक पर क्लिक करे ।
                    </p>
</td>

                    </tr>                                                                                          

                   <tr>

                      <td colspan="4" style="background-color:#66CCFF;height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold"> F)	Godown Offer Details</p>
                      </td>                      
                  </tr>
                     <tr>
                    <td></td>
                    </tr> 
                 <tr>
                    <td style="height:20px;width:225px">
                        JVS में ऑनलाइन ऑफर दिनांक और समय :
                    </td>
                    <td>
                    <asp:TextBox ID="txtOnlineOffer" runat="server" class="text" type="text" 
                            ReadOnly="True"  ></asp:TextBox>

                    </td >
                          <td>
                            श्रेणी ऑनलाइन ऑफर अनुसार :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlJVCategory" runat="server"
                            Width="232px" Height="27px" Enabled="False">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="78">B(Rs. 78)</asp:ListItem>
                                            <asp:ListItem Value="83">A(Rs. 83)</asp:ListItem> 
                            
                        </asp:DropDownList>

                    </td>
                    </tr>
                                         <tr>
                    <td style="height:20px">
                        ऑफर की गयी भंडारण क्षमता का प्रकार - (आंशिक/पूर्ण ) :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlCptOffer" runat="server"
                            Width="232px" Height="27px" Enabled="False">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="P">Partial</asp:ListItem>
                            <asp:ListItem Value="F">Full</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                             JVS में ऑफर की गयी क्षमता :
                    </td>
                    <td >
                     <asp:TextBox ID="txtjvsofrcpt" runat="server" class="text" type="text" ReadOnly="true" ></asp:TextBox>

                    </td>
                    </tr>

                    </table>
                    <table>
                    
                   <tr>
                      <td colspan="4" style="background-color:#66CCFF;height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">G)	Important aspect of Godown to become Category/Scheme &nbsp;&nbsp;&nbsp; 'A (Rs. 83)' &nbsp;or &nbsp;'B (Rs. 78)' </p>
                      </td>                         
                  </tr>
                  <tr>
                    <td style="width:400px">
                     क्या गोदाम परिसर में मानक क्षमता का चालू हालत में इलेक्ट्रानिक वेब्रिज है <asp:Label ID="lblelectronicWeib" Visible="false" runat="server" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlElectWeigh" runat="server"
                            Width="100px" Height="27px" AutoPostBack="True" 
                            onselectedindexchanged="ddlElectWeigh_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td>
                      क्या गोदाम के प्रत्येक गेट/शटर पर 'जालीदार गेट/शटर' है <asp:Label ID="lblgatechk" runat="server" Visible="false" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlGateType" runat="server"
                            Width="100" Height="27px" AutoPostBack="True" 
                             onselectedindexchanged="ddlGateType_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                  </tr>
                  <tr>
                    <td>
                     क्या गोदाम परिसर को सुरक्षा व्यवस्था हेतु बाउण्ड्री बाउण्ड्रीवाॅल / चैनलिंग फेंसिंग / बार्बेड वायर फेंसिंग/स्टोन वाल से कवर्ड है <asp:Label ID="lblboundry" Visible="false" runat="server" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlboundrytype" runat="server"
                            Width="100px" Height="27px" AutoPostBack="true"
                            onselectedindexchanged="ddlboundrytype_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Boundary Wall</asp:ListItem>
                            <asp:ListItem Value="2">channeling Fencing</asp:ListItem>
                            <asp:ListItem Value="4">Barbed Wire Fancing</asp:ListItem>
                            <asp:ListItem Value="3">Other</asp:ListItem>
                            <asp:ListItem Value="5">Stone Wall</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td>
                       क्या गोदाम में डामरीकृत रोड/सीसी.रोड/WBM है <asp:Label ID="lblrode" Visible="false" runat="server" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlRoadType" runat="server"
                            Width="100" Height="27px" AutoPostBack="true"
                             onselectedindexchanged="ddlRoadType_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">BT</asp:ListItem>
                            <asp:ListItem Value="2">CC</asp:ListItem>
                            <asp:ListItem Value="3">WBM</asp:ListItem>
                            <asp:ListItem Value="4">Other</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    </tr>
                    
                  <tr>
<%--                    <td>
                     क्या वेअरहाउस लायसेंस की वर्तमान वेधता है यदि नहीं हे तो क्या लायसेंस के लिए आवेदन किया गया हे
                    </td>
                    <td>
                      <asp:DropDownList ID="ddllicvalidapplied" runat="server"
                            Width="100px" Height="27px"  AutoPostBack="true"
                            onselectedindexchanged="ddllicvalidapplied_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>--%> 
                    
                         <td style="background-color:#F8CF6A; font-weight:bold ; font-size:14px;">
                           निरीक्षण में पाई गई श्रेणी (निम्न अर्हताऐं अनुसार)
                        </td>
                        <td style="background-color:#F8CF6A;">
                             <asp:DropDownList ID="ddlinspectionScheme" runat="server"
                                Width="100" Height="27px" Enabled="False">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="63">B(Rs. 78)</asp:ListItem>
                                            <asp:ListItem Value="68">A(Rs. 83)</asp:ListItem> 
                            </asp:DropDownList>
                        </td>                                       
                    </tr> 
<tr>
                      <td colspan="4" style="color:Red;">
                          
                          Note :
                      </td>

                  </tr>
                                <tr> 
                            <td colspan="4">

                                 <p style="color:Red;">
                                   1) उक्त प्रदर्शित जानकारी रजिस्ट्रेशन के समय / अपडेट करते समय भरी गई हे | भरी गई जानकारी के अनुसार ही श्रेणी का चयन किया गया हे |
 <br />
                                </p> 
                                 <p style="color:Red;">
                                   2) 'A' श्रेणी की संयुक्त भागीदारी योजना की निम्न अर्हताऐं :- <br />
                                      &nbsp;&nbsp;&nbsp; (i) WDRA अथवा आयुक्त,खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश का वैध पंजीयन/लायसेंस हो | &nbsp;&nbsp;  (ii) गोदाम में डामरीकृत/सी.सी./WBM  रोड़ हो|  <br /> &nbsp;&nbsp;&nbsp;(iii) गोदाम परिसर में इलेक्ट्राॅनिक वेब्रिज हो | &nbsp;&nbsp;(iv) गोदाम में प्रत्येक गेट पर जालीदार शटर/गेट हो |  &nbsp;&nbsp;(v) गोदाम परिसर की बाउण्ड्री बाउण्ड्रीबाल/चैनलिंग फेंसिंग/बार्बेड वायर फेंसिंग/स्टोन वाल से कवर्ड हो |
 <br />
                                </p>    
                                 <p style="color:Red;">
                                   3) ऐसे गोदाम जो 'A' श्रेणी की संयुक्त भागीदारी योजना हेतु निर्धारित अर्हताए नहीं रखते हैं, उन्हें 'B' श्रेणी की संयुक्त भागीदारी योजना में लिया जाए | 
 <br />
                                </p> 
                            </td>

                        </tr>                                                                                          

                                        </table>


                                    <table style="width:100%">
                   <tr>
                      <td colspan="6" style="background-color:#66CCFF;height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">H)	Important aspect of Warehouse to become UNFIT </p>
                      </td>                         
                  </tr>
                     <tr>
                    <td></td>
                    </tr> 
                 <tr>
                     <td>
                            1.
                     </td>
                    <td style="height:20px ;">
                        क्या गोदाम निर्माणाधीन है ?
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlWareConstruct" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlWareConstruct_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                     <td>
                            2.
                     </td>                    
                          <td>
                            	क्या गोदाम विवादग्रस्त यथा गोदाम के मालिकाना हक, देनदारियां एवं माननीय न्यायालयों में प्रकरण लंबित/विचाराधीन हैं ?
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlWarelitigation" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlWarelitigation_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>
                                         <tr>
                     <td>
                            3.
                     </td>                                         
                    <td style="height:20px">
                        क्या गोदाम क्षतिग्रस्त है ?
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlWareDamage" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlWareDamage_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                     <td>
                            4.
                     </td>                    
                    <td>
          	क्या गोदाम में पूर्व से शासकीय स्कंध के अतिरिक्त अन्य जमाकर्ता का स्कंध भण्डारित है ?
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlWarePrivateDepositor" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlWarePrivateDepositor_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>
                                        <tr>
                     <td>
                            5.
                     </td>                                        
                    <td style="height:20px">
                        क्या गोदाम 'ब्लेक लिस्टेड' है ?
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlBlackList" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlBlackList_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                     <td>
                            6.
                     </td>                    
                          <td>
                            	क्या गोदाम की भण्डारण क्षमता एक परिसर में न्यूनतम 500 मे.टन से कम है ?
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlcptless" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlcptless_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
              <tr>
                     <td>
                            7.
                     </td>             
                    <td style="width:350px">
                         क्या गोदाम वैज्ञानिक भंडारण हेतु अयोग्य हे ( यदि गोदाम योग्य हे तो यह भी सुनिश्चित करे की वायुसंचरण हेतु रोशनदान हो तथा गोदाम की ऊंचाई कम से कम 14 फिट है)?
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlFacilities" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlFacilities_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                     <td>
                            8.
                     </td>             
                    <td style="width:350px">
                         क्या ऑनलाइन ऑफर संबंधी जानकारी गलत दी गयी है ?
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlofrinfo" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlofrinfo_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >                    

                    </tr>
                    
                    
                                        <tr>
                    <td></td>
                    </tr>
                                <tr> 
                            <td colspan="6" style="color:Red;">
Note : <br />
                                 <p >
                                   1) यदि उपरोक्त बिन्दुओं में से कोई भी एक बिन्दु की जानकारी ’हाॅं’ होती है तो वह गोदाम उपार्जित स्कंध के भण्डारण हेतु अपात्र होगा ।
                                </p>    
                            </td>

                        </tr>                     
                                        </table>
                                    
                                    
                                                 
                                     <table style="width:100%">
                   <tr>
                      <td colspan="4" style="background-color:#66CCFF;height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">I)	Declaration of Inspection Officer / Branch Manager </p>
                      </td>                       
                  </tr>
                     <tr>
                    <td></td>
                    
                    </tr>
                    
                    <tr>
                    <td > According To System FIT / UNFIT &nbsp&nbsp<asp:TextBox ID="txtSystemFitunfit"  Width="90px" runat="server" ReadOnly="true" class="text" type="text"  ></asp:TextBox></td>

                    <td colspan="3" style="margin-top:0px; text-align:center">Remark For Recommendation :&nbsp
                        <textarea id="txtRemark" maxlength="200" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:250px;"
                        ></textarea>
                        </td>
                    </tr>
                    
                    <tr>
                    
                    <td></td>
                    </tr>
                    
                    
                                         <tr>
                    <td colspan="4"> <p> <asp:CheckBox ID="chkDeclaration" runat="server"></asp:CheckBox>  I hereby declare and affirm that the information provided by us is true and correct to the best of our knowledge and found during the inspection by me.</p><br />

<p>Also this is to certify that after inspection based on the points mentioned in inspection report format, that (<asp:Label ID="lblDecWareName" runat="server" ></asp:Label>), has been found <asp:Label ID="lblDecFitUnfit" runat="server" ></asp:Label> for storage of commodity</p><br />
Date :-  <asp:Label ID="lblDate" runat="server"></asp:Label> 
</td>

                    </tr>
                    
                     <tr>
                    <td colspan="4"> <p style=" color:Red;">नोट :- <br />1. यदि गोदाम अपात्र होता हे तो अपात्र के कारणों की फोटोग्राप्स रिकॉर्ड हेतु लिया जाएँ ।<br />
                   <%-- 2. यदि गोदाम अपात्र होता हे तो अपात्र के कारणों कै साथ वेयरहाउस औंनर को अग्वत कराए ।<br />--%>
                    2. यदि गोदाम अपात्र होता हे तो अपात्र के कारणों कै साथ वेयरहाउस औंनर को सूचित करें एवं   अगवत कराए की 5 दिवस के भीतर उनके लॉगिन से अपील किया जाना संभव हे ।<br />
                    3. गोदाम के निरीक्षण संबन्धित समस्त महत्वपूर्ण दस्तावेज़ आगामी निर्देश तक कार्यालय मे संग्रहीत करके रखे।
                   
                    </p>
</td>

                    </tr>
 
                                      
                                    <tr>
                  
                    <td align="center" colspan="4">
                  
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="submit" 
                            Width="140px" onclick="btnsubmit_Click" ></asp:Button>

                    </td>
                    </tr>
                    <tr ID="trlblHide" runat="server" visible="false">
                    <td colspan="4">
                    <asp:Label ID="lblDistID" runat="server" ></asp:Label><asp:Label ID="lblBranchID" runat="server" ></asp:Label>
                    <asp:Label ID="lblOfferID" runat="server" ></asp:Label><asp:Label ID="lblGodownOfferID" runat="server" ></asp:Label>
                    <asp:Label ID="lblRegionID" runat="server" ></asp:Label>
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

 <%-- Start First Div Popup Data--%>
 <asp:Label ID="Label2" runat="server"></asp:Label>
     <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" CancelControlID="btncancelpwd" TargetControlID="Label2"
     BackgroundCssClass="modalBackground" PopupControlID="pnllogin">
    </cc1:ModalPopupExtender>
  <asp:Panel ID="pnllogin" runat="server" ScrollBars="Vertical" class="modalpop" Height="600px" Width="80%" >
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td align="center" valign="top" style="color:White; font-weight:bold; background-color:#008CBA; height:25px; width:400px; font-size:16px; border-radius:2px">खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश का पंजीयन/लायसेंस धारको का विवरण |</td>
                   <td align="center" valign="top" style="color:White; font-weight:bold; background-color:#008CBA; height:25px; width:30px; font-size:16px; border-radius:2px">
                       <asp:Button ID="btncancelpwd" runat="server" Text="Close"/>
                    </td>
            </tr> 
                                         
        </table> 
    </div>  
    <table id="modalpop" align="center" style="width: 100%;">
    <tr>
        <td >
                <asp:GridView ID="GD_Paddy"  runat="server" DataKeyNames="Whr_ID" AutoGenerateColumns="False" Width="100%" Height="300px" Font-Size="8pt" 
                Font-Bold="true" BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"  HeaderStyle-BorderColor="White"
                 CellPadding="2" CellSpacing="2" ShowFooter="true" >
    
                            <Columns>
    <asp:TemplateField HeaderText = "SNO." ItemStyle-Width="40px">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>                            
                                <asp:BoundField DataField="District" HeaderText="DISTRICT" ReadOnly="True" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="Whr_Name" HeaderText="WAREHOUSE NAME" ReadOnly="True" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="Whr_ID" HeaderText="LICENCE NO." ReadOnly="True" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Name_of_Owner" HeaderText="OWNER NAME" ReadOnly="True" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="Whr_Address" HeaderText="WAREHOUSE ADDRESS" ReadOnly="True" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="Total_capicity" HeaderText="CAPACITY" ReadOnly="True" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="ExpDate" HeaderText="EXP. DATE" ReadOnly="True" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                                                                                          
                            </Columns>
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="15px" Font-Size="8pt" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                 <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle BackColor="#008CBA" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="30px" Font-Size="12PX" /> <AlternatingRowStyle BackColor="#eeeeee" />
                             </asp:GridView>            
        </td>
    </tr>
    <tr>
        <td align="center">
            
        </td>
    </tr>
    </table>
    </asp:Panel>

    
<%--    <cc1:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="Label2">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </cc1:AnimationExtender> --%>
    
 <%-- End Of First Div Popup Data--%>

<%-- Start Decond Div Popup Data--%>
 <asp:Label ID="Label3" runat="server"></asp:Label>
     <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" CancelControlID="Button1" TargetControlID="Label3"
     BackgroundCssClass="modalBackground" PopupControlID="Panel1">
    </cc1:ModalPopupExtender>
  <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" class="modalpop" Height="600px" Width="80%" >
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td align="center" valign="top" style="color:White; font-weight:bold; background-color:#008CBA; height:25px; width:400px; font-size:16px; border-radius:2px"> मध्यप्रदेश के WDRA पंजीयन/लायसेंस धारको का विवरण (28/08/2018) तक  |</td>
                   <td align="center" valign="top" style="color:White; font-weight:bold; background-color:#008CBA; height:25px; width:30px; font-size:16px; border-radius:2px">
                       <asp:Button ID="Button1" runat="server" Text="Close"/>
                    </td>
            </tr> 
                                         
        </table> 
    </div>  
    <table id="Table1" align="center" style="width: 100%;">
    <tr>
        <td >
                <asp:GridView ID="GridView1"  runat="server" DataKeyNames="WHCode" AutoGenerateColumns="False" Width="100%" Height="300px" Font-Size="8pt" 
                Font-Bold="true" BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"  HeaderStyle-BorderColor="White"
                 CellPadding="2" CellSpacing="2" ShowFooter="true" >
    
                            <Columns>
    <asp:TemplateField HeaderText = "SNO." ItemStyle-Width="40px">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>                            
                                
                                <asp:BoundField DataField="NameandAddress" HeaderText="WAREHOUSE NAME & ADDRESS" ReadOnly="True" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="WHCode" HeaderText="LICENCE NO." ReadOnly="True" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="WarehousemanName" HeaderText="OWNER NAME" ReadOnly="True" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="Mobile" HeaderText="MOBILE NO" ReadOnly="True" HeaderStyle-Width="80px"  ItemStyle-HorizontalAlign="Left"/>
                                <asp:BoundField DataField="Capacity" HeaderText="CAPACITY" ReadOnly="True" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="ExpDate" HeaderText="EXP. DATE" ReadOnly="True" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                                                                                          
                            </Columns>
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="15px" Font-Size="8pt" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                 <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle BackColor="#008CBA" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="30px" Font-Size="12PX" /> <AlternatingRowStyle BackColor="#eeeeee" />
                             </asp:GridView>            
        </td>
    </tr>
    <tr>
        <td align="center">
            
        </td>
    </tr>
    </table>
    </asp:Panel>

    
<%--    <cc1:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="Label2">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </cc1:AnimationExtender> --%>
    
 <%-- End Of Second Div Popup Data--%>

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