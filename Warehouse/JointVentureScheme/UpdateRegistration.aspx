<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateRegistration.aspx.cs" Inherits="JointVentureScheme_RegistrationUpdate" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Update Registration</title>
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


    <script type="text/javascript" language="javascript" >
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
            height: 22px;
        }

    </style>
</head>
<body>

<div id="bg" style="background-color:White">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
       
        <div id="PrintDiv">
                            <form id="form1" runat="server">
                                 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>

                                <center>
                    <table style="color: Black;">
<%--                        <tr >
                            
                            <td  colspan="4" style="background-color: #66CCFF"> 
                                <p style="font-size: medium; color: #008080;"><asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/WarehouseHome.aspx"></asp:LinkButton>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Log out</asp:LinkButton></p>
                            </td>
                        </tr>--%>
                        
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
                        <tr>                      <td colspan="4">
                          <marquee direction="left" behavior="alternate"> <p style="font-size: 16px; color: #008080; font-weight:bold ">
                         Update Warehouse Registration</p></marquee>
                      </td>  </tr>                        
                        
                         <tr>
                      <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:#66CCFF" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                          Warehouse Owner Details <span style="color: #FF0000; font-size:x-large;">*</span> </p>
                      </td>                      

                  </tr>
                  <tr>
                    <td>
                            &nbsp;&nbsp;Registration ID:
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblregid" runat="server"></asp:Label>
                    </td>
                    <td class="style2">
                            &nbsp;Registration Date:
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblregdate" runat="server"></asp:Label>
                    </td>
                    </tr>                  
                  <tr>
                    <td Width="230px">
                            &nbsp;&nbsp;Registered Email ID:
                    </td>
                    <td style="font-weight:bold; word-wrap:break-word; ">
                        <asp:Label ID="lblEmail" runat="server" Width="230px"></asp:Label>
                    </td>
                    <td class="style2">
                            &nbsp;Mobile Number:
                    </td>
                    <td style="font-weight:bold;">
                        <asp:Label ID="lblMob" runat="server"></asp:Label>
                    </td>
                    </tr>
                                                <tr>
                    <td>
                   &nbsp;&nbsp;Type of Applicant:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAppType" runat="server" AutoPostBack="true" 
                            Width="230px" Height="25px"
                                            >                          
                        </asp:DropDownList></td>
                    <td>
                  &nbsp;District of Resident:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="false" 
                            Width="230px" Height="27px">                         
                        </asp:DropDownList></td>                        
                      
                    </tr>
                    <tr>
                   
                    <td>
                  &nbsp;&nbsp;Warehouse Owner:
                    </td>
                    <td>
                        <input id="txtAuthPerson" name="rname" class="text" runat="server"  type="text" tabindex="1" />
                   </td>
                    <td>
                      &nbsp;Category :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCast" runat="server" AutoPostBack="false" Width="230px" Height="27px" Enabled="true">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">SC</asp:ListItem>
                            <asp:ListItem Value="2">ST</asp:ListItem>
                            <asp:ListItem Value="3">OBC</asp:ListItem>
                            <asp:ListItem Value="4">GEN</asp:ListItem>
                        </asp:DropDownList>
                    </td>                   
                    </tr>
                  <tr>
                                       <td>&nbsp;&nbsp;PAN No:</td>
                    <td>
                    

                    <input id="txtPAN" type="text" name="PAN" runat="server" class="text" size="10" tabindex="5" />


                    </td>
                                       <td> &nbsp;Aadhar No:</td>
                    <td>
                    

                    <input id="txtAadharNo" type="text" name="AadharNo" runat="server" class="text" size="10" tabindex="5" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;" />
                    </td>                  
                  </tr>
                  
                                                      
                  <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         Warehouse Registration Details <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>                                            

                  </tr>
                  
                  <tr>
                    <td>
                        &nbsp;&nbsp;Warehouse Name : <br /><br /><br />
                        &nbsp;&nbsp;Office Contact No./Mobile No:
                    </td>
                    <td>
                        <input id="txtWarehouseName" name="rname" runat="server" class="text" style="text-transform: uppercase;" type="text"/><br /><br />
                                                <asp:TextBox ID="txtWareContactNo" runat="server" class="text" type="text" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtWareContactNo"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                          <td>
                            &nbsp;Warehouse/Office Address with Postal<br /> &nbsp;Address:
                    </td>
                    <td>
                        
                        <textarea id="txtWareAddress" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                    </tr>
 
                    <tr>
                          <td>
                            &nbsp;&nbsp;Landmark Near Warehouse:
                    </td>
                    <td>
                        <input id="txtLandmark" name="rname" runat="server" class="text" type="text" />
                    </td>                   
                          <td>
                            &nbsp;District :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWarDistrict" runat="server" AutoPostBack="true" 
                            Width="230px" Height="25px" 
                            onselectedindexchanged="ddlWarDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>

                    </tr>
                    

                    <tr>
                       <td>
                                                &nbsp;&nbsp;Tehsil :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBlock" runat="server" 
                            Width="230px" Height="25px" Enabled="true" onselectedindexchanged="ddlBlock_SelectedIndexChanged"
                                            >
                        </asp:DropDownList>
                    </td>                    
                    <td>
                            &nbsp;Block :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlblocknew" runat="server" Width="230px" Height="25px" AutoPostBack="True" 
                            onselectedindexchanged="ddlblocknew_SelectedIndexChanged" >
                        
                        </asp:DropDownList>
                    </td>
                    </tr>                    
                    
                    <tr>
                 
                          <td>
                            &nbsp;&nbsp;Nearest Branch of MPWLC :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="false" Enabled="true"
                            Width="230px" Height="25px">
                        </asp:DropDownList>
                    </td>
                      <td>
                       &nbsp;Distance from nearest branch of <br /> &nbsp;MPWLC (in KM):
                    </td>
                    <td>
                       <%--<input id="txtDistance" name="rname" runat="server" class="text" type="text"/>--%>
                           <asp:TextBox ID="txtDistance" runat="server" class="text" type="text" style="width:90px" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtDistance"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                    </tr>
                   <%-- -------------------Licence Row ------------------%>
                    <%--<tr> 
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         License Detail</p>
                      </td>                                              

                  </tr>
                    <tr>
                    <td colspan="2">
                       &nbsp;&nbsp;Does it has Present Validity?(State Licence):
&nbsp;&nbsp;&nbsp;&nbsp;
                        Yes <asp:RadioButton ID="rdoYes" runat="server" GroupName="StateLicence" 
                            Width="50px" Checked="true" oncheckedchanged="rdoYes_CheckedChanged" AutoPostBack="true"/>
                         NO <asp:RadioButton ID="rdoNo" runat="server" GroupName="StateLicence" 
                             oncheckedchanged="rdoNo_CheckedChanged" AutoPostBack="true"/>
                        
                    </td>
                    </tr>
                    <tr id="TrStateL" runat="server">
                    <td>
                    &nbsp;&nbsp;Warehouse License No.:
                    </td>
                    <td>
                        <input id="txtWLicNo" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             &nbsp;Valid Upto Date (dd/mm/yyyy):
                    </td>
                    <td>
                        <asp:TextBox ID="txtSLDate" runat="server" CssClass="text" 
                            onkeydown="javascript:preventInput(event);" onpaste="return false;" 
                           ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtSLDate"></cc1:CalendarExtender>
                    </td>
                    </tr>
                      <tr>
                    <td colspan="2">
                       &nbsp;&nbsp;Does it has Present Validity?(WDRA Licence) :
&nbsp;&nbsp;
                        Yes <asp:RadioButton ID="rdoYesW" runat="server" GroupName="WDRALicence" Width="50px" Checked="true" 
                            oncheckedchanged="rdoYesW_CheckedChanged" AutoPostBack="true"/>
                         NO <asp:RadioButton ID="rdoNoW" runat="server" GroupName="WDRALicence" 
                         oncheckedchanged="rdoNoW_CheckedChanged" AutoPostBack="true"/>
                    </td>
                    </tr>--%>
                    
                    <%-- -------------------Licence Row --------------------%>
                    
<%--                    <tr id="TWDRAL" runat="server">
                    <td>
                    &nbsp;&nbsp;WDRA registration No at present :
                    </td>
                    <td>
                        <input id="txtWDRALicenceNo" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             &nbsp;Valid Upto Date(dd/mm/yyyy) :
                    </td>
                    <td>
                        <asp:TextBox ID="txtWDRALDate" runat="server" CssClass="text" onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender3" runat="server"  Format="dd/MM/yyyy"
                        TargetControlID="txtWDRALDate"></cc1:CalendarExtender>
                    </td>
                    </tr>--%>
                 
                  <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Warehouse Incharge/Manager Details <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>                                               

                  </tr>
                  <tr>                
                    <td>
                        &nbsp;&nbsp;Authorised Person :<br /> <br /> <br />&nbsp;&nbsp;Designation :
                    </td>
                    <td>
                        <input id="txtIncharge" name="rname" runat="server" class="text" type="text"/><br /><br />
                        <input id="txtDesign" name="rname" runat="server" class="text" type="text"/>
                    </td>
                     <td>
                             &nbsp;
                             Authorised Person Postal Address:
                    </td>
                    <td>
                        
                        <textarea id="txtInchAdd" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                    </tr>
                    <tr>
                    <td>
                        &nbsp;&nbsp;Email ID :
                    </td>
                    <td>
                        <input id="txtInchargeEmail" name="rname" runat="server" class="text" type="text" />
                    </td>                    

                          <td>
                              &nbsp;
                              Mobile No. :
                    </td>
                    <td>
                        <%--<input id="txtInchMob" name="rname" runat="server" class="text" type="text"/>--%>
                        <asp:TextBox id="txtInchMob" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtInchMob"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                    </tr>
                         <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Godown Capacity and Licence Detais <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>                                             
                         </tr>

                              <tr>
                      <td colspan="4" style=" width:10px;">
                      </td>                                             
                      

                  </tr>  
                        <tr>   
                            <td colspan="4" style="text-align:center; width:100%; " align="center">
                            <asp:GridView ID="gvGodown"  runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False" Width="100%" Font-Size="10pt" 
                            Font-Bold="true" BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px" CellPadding="2" CellSpacing="2" >
                            <Columns>
                            <asp:BoundField DataField="Godown_No" HeaderText="Godown No." />
                            <asp:BoundField DataField="G_Length" HeaderText="Length in Feet" />
                            <asp:BoundField DataField="G_Width" HeaderText="Width in Feet" />
                            <asp:BoundField DataField="G_Height" HeaderText="Height in Feet" />
                            <asp:BoundField DataField="G_ScientificCapacity" HeaderText="Capacity (MT)" />
                               
                                <asp:TemplateField HeaderText="Licence Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlgdwntype" runat="server" Height="25px"> 
                                            <asp:ListItem >--Select--</asp:ListItem>
                                            <asp:ListItem Value="68" Text="WDRA"></asp:ListItem>
                                            <asp:ListItem Value="63" Text="NON WDRA"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="APPLIED For WDRA"></asp:ListItem>
                                            <asp:ListItem Value="00" Text="APPLIED For NON WDRA"></asp:ListItem>                                    
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                               
                                <asp:TemplateField HeaderText="Licence No./Application No." HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Gtxtlicno" runat="server" Width="200px"  Font-Bold="true" Height="19px" align="Center" placeholder="Enter Licence Number"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lic. Issue Date/Application Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GtxtLicIssuedate" runat="server" Width="100px" Font-Bold="true" Height="19px"  placeholder="Issue Date" onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                  <cc1:CalendarExtender ID="CalendarExtender3" runat="server"  Format="dd/MM/yyyy"
                                      TargetControlID="GtxtLicIssuedate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>                                 
                                <asp:TemplateField HeaderText="Lic. Expiry Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GtxtLicExpdate" runat="server" Width="100px" Font-Bold="true" Height="19px" placeholder="Expiry Date" onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server"  Format="dd/MM/yyyy"
                        TargetControlID="GtxtLicExpdate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" />                                                                                                                                                                                                                                                  
                            </Columns>
            
                                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="15px" Font-Size="8pt" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#008CBA" Font-Bold="True" ForeColor="White" HorizontalAlign="center" BorderColor="White" BorderStyle="Solid"
                                Height="20px" Font-Size="10pt" /> <AlternatingRowStyle BackColor="#eeeeee" />
                             </asp:GridView>
                            </td>      
                        </tr> 
                                                
                              <tr>
                      <td colspan="4" style=" width:10px;">
                      </td>                                             
                  </tr>    
                  <tr>
                            <td colspan="4" style="color:Red;"> Note : <br />
                                <p style="color:Red;">
                                   1)गोदामो की केपेसिटि से संबन्धित कोई भी शंसोधन कराना हो तो email ID पर मेल करे | <br />
                                   2) Formula for Capacity = [Length*Breadth*(Height-3)/80]                                    
                                </p>
                            </td>
                  </tr>                
                                             

                         <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Bank Account Detail <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>                                             
                  </tr>
                  <tr>
                      <td colspan="4" style="background-color:none; font-size:small;">                                         
                          <p>&nbsp;&nbsp;क्रप्या अपना बैंक लोन अकाउंट डीटेल प्रविष्ट करे, यदि बैंक लोन नहीं है तब अन्य बैंक अकाउंट जिसमे गोदाम किराया प्राप्त करना चाहते है वह प्रविष्ट करे।</p>
                      </td>

                  </tr>
                          <tr>
                                      <td>
                                            &nbsp;&nbsp;अकाउंट नंबर

                                        </td>
                                        <td>
                                         
             
                        <asp:TextBox id="txtAccNo" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtAccNo"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>   
                         
                    <td >
                        
                    &nbsp;&nbsp;IFSC कोड<span lang="hi"> </span>:
                    
                    </td>
                    
                   
                    <td>
                        <input id="txtIFSC" name="ryear" type="text" class="text" runat="server"/>
                    
                    </td>
                                            </tr>            
                                     <tr>
 
                      
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Nearest Distance of Warehouse From <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>                       

                  </tr>
                  <tr>
                    <td colspan="3">
                    &nbsp;&nbsp;1. Nearest Distance of Warehouse From National Highway/State Highway (in KM) 
                    </td>
                    <td align="right">
                         <asp:TextBox id="txtHighway" name="ryear" type="text" class="text" runat="server" style="width:90px"></asp:TextBox> &nbsp;
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtHighway"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                          
                    </tr>
                    <tr>
                    <td colspan="3">
                    &nbsp;&nbsp;2. Nearest Distance of Warehouse From Railway Station (in KM)
                    </td>
                    <td align="right">
                        <asp:TextBox id="txtRailway" name="ryear" type="text" class="text" runat="server" style="width:90px"></asp:TextBox> &nbsp;
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtRailway"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                          
                    </tr>
                     <tr>
                    <td colspan="3">
                    &nbsp;&nbsp;3. Nearest Distance of Warehouse From Mandi (in KM)
                    </td>
                    <td align="right">
                         <asp:TextBox id="txtMandi" name="ryear" type="text" class="text" runat="server" style="width:90px"> </asp:TextBox> &nbsp;
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtMandi"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                          
                    </tr>
                    <tr>
                    <td colspan="3">
                    &nbsp;&nbsp;4. Nearest Distance of Warehouse From Goods Shed (in KM)
                    </td>
                    <td align="right">
                       <asp:TextBox id="txtGS" name="ryear" type="text" class="text" runat="server" style="width:90px"></asp:TextBox> &nbsp;
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtGS"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                          
                    </tr>
                    
                    <%--__________________________________--%>

     <tr>
        <td colspan="4">
            <table style="width: 100%;">                      
                     
                     <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Warehouse Geogrophical Information <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>                       
                  </tr>
                      
                  <tr>
                    <td style="width:335px;">
                       &nbsp&nbsp Warehouse Latitude (Ex. 23.606454) :
                    </td>
                    <td style="width:80px;">
                       <%-- <input id="txtlat" name="rname" runat="server" class="text" type="text" style="width:90px"/>--%>
                   <asp:TextBox ID="txtlat" runat="server" class="text" type="text" style="width:90px" ></asp:TextBox><br /> 
                    
                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtlat"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender></td>
                     <td style="width:370px;">
                     Warehouse Longitude (Ex. 23.606454) :
                    </td>
                    <td style="width:80px;">
                       <%-- <input id="txtlong" name="rname" runat="server" class="text" type="text" style="width:90px" />--%>
                        <asp:TextBox ID="txtlong" runat="server" class="text" type="text" style="width:90px"></asp:TextBox><br />
                    
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtlong"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender></td>
                  </tr>
                                     <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         Warehouse Additional Facilities Information <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>                        
                  </tr> 
    
    
<%--Start Additional Facility--%>                      

    <tr>
                     <td>
                       &nbsp;&nbsp;Motorable Approach Road Type :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlRoadType" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">BT</asp:ListItem>
                            <asp:ListItem Value="2">CC</asp:ListItem>
                            <asp:ListItem Value="3">WBM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td>
                       Width of Road (In Meters) :
                    </td>
                     <td>
                       
                         <asp:TextBox ID="txtRoadWidth" runat="server" class="text" type="text" style="width:90px" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRoadWidth"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                  </tr>                                      
                  <tr>
                     <td>
                      &nbsp;&nbsp;Availability of Shutter/Jali/Chanel Gate in Each Godown :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlGateType" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td>
                       Number of Gate in Warehouse :
                    </td>
                     <td>
                        
                        <asp:TextBox ID="txtNoGate" runat="server" class="text" type="text" style="width:90px" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtNoGate"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      &nbsp;&nbsp;Power Supply (In Phase) :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlPowersuply" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="0">No Supply</asp:ListItem>
                            <asp:ListItem Value="1">1 Phase</asp:ListItem>
                            <asp:ListItem Value="2">2 Phase</asp:ListItem>
                            <asp:ListItem Value="3">3 Phase</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                           Is Warehouse free from Passing over of any tension electric line :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlTentionline" runat="server"
                            Width="100px" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                  <tr>
                    <td>
                     &nbsp;&nbsp;Water Facility :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlWaterFac" runat="server"
                            Width="100px" Height="27px"  >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                             Water Facility for spray and other usage :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlWaterSpry" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr> 
                  <tr>
                    <td>
                       &nbsp;&nbsp;CCTV Camera :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlCCTV" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                            Availability Of Fumigation & Pest Control Equipment :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlFumigation" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>                                     
                  <tr>
                    <td>
                     &nbsp;&nbsp;Guard With Guard Room :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlGuard" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                              Availability of Wooden Planks/Dunnage:
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlPlanks" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr> 
                    
                  <tr>
                    <td>
                      &nbsp;&nbsp;Availability fo Fire Buckets :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlFireBuc" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                               Availability of Fire extinguisher with fire hydrants :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlFireExt" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                    
                  <tr>
                    <td>
                      &nbsp;&nbsp;Warehouse Boundary Covered by  :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlboundrytype" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Boundary Wall</asp:ListItem>
                            <asp:ListItem Value="2">channeling Fencing</asp:ListItem>
                            <asp:ListItem Value="4">Barbed Wire Fancing</asp:ListItem>
                             <asp:ListItem Value="5">Stone Wall</asp:ListItem>
                            <asp:ListItem Value="3">Other</asp:ListItem>
                           
                        </asp:DropDownList>
                    </td>
                    </tr>                     
                    
<%--End Additional Information  --%>                  
                                         
                   <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         Warehouse Hardware Information <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>                         
                      
                  </tr> 
                                                                                                           
                  <tr>
                    <td>
                     &nbsp;&nbsp;Electronic Weighbridge :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlElectWeigh" runat="server"
                            Width="100px" Height="27px" AutoPostBack="True" onselectedindexchanged="ddlElectWeigh_SelectedIndexChanged"
                            >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                    <td><asp:Label ID="lblWB" runat="server" Text="Weighbridge calibration validity date :" Visible="false"></asp:Label></td>

                    <td>
                         <asp:TextBox ID="txtWB_calibrationdate" runat="server" class="text" Visible="false" type="text"  Width="90px"  onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
                          TargetControlID="txtWB_calibrationdate"></cc1:CalendarExtender>                    
                    </td>
                    </tr>
                    <tr>
                                        <td>
                       &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="WB is Certified By Controler :" Visible="false"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlWeighCertified" runat="server" Visible="false"
                            Width="100px" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                       <asp:Label ID="Label1" runat="server" Text="Weighbridge Capacity(In M.T) >30 MT :" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeighCpt" runat="server" class="text" Text="0" type="text" Visible="false" style="width:90px" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtWeighCpt"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>

                    </tr>
                    <tr id="tr_W_Machine" runat="server" visible="false">
                    <td>
                       &nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Text="Weighing Machine :"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddl_W_Machine" runat="server"
                            Width="100px" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                       <asp:Label ID="Label7" runat="server" Text="Weighing Machine Capacity(In K.G) :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWMCpt" runat="server" class="text" Text="0" type="text" style="width:90px" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtWMCpt"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>

                    </tr>                    
                    
                  <tr>
                    <td>
                        &nbsp;&nbsp;Internet Connectivity :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlInternetCon" runat="server"
                            Width="100px" Height="27px" AutoPostBack="true" onselectedindexchanged="ddlInternetCon_SelectedIndexChanged" 
                            >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Connectivity Type :" Visible="false"></asp:Label></td>
                    <td >
                        <asp:DropDownList ID="ddlConType" runat="server"
                            Width="100px" Height="27px" Visible="false">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Fixed Broadband Connections</asp:ListItem>
                            <asp:ListItem Value="2">Mobile Internet</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    </tr>  
                  <tr>
                    <td>
                      &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Visible="false">Availability of Computer & Required Hardware :</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlHardwareAvl" runat="server"
                            Width="100px" Height="27px" Visible="false">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>   
                    <td >
                        <asp:Label ID="Label5" runat="server" Text="Year Of Installation :" Visible="false"></asp:Label> 
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlInstallationyear" runat="server"
                            Width="100px" Height="27px" Visible="false">
                            <asp:ListItem>--Select--</asp:ListItem>
                             <asp:ListItem Value="2018">2022</asp:ListItem>
                            <asp:ListItem Value="2017">2021</asp:ListItem>
                            <asp:ListItem Value="2016">2020</asp:ListItem>
                            <asp:ListItem Value="2015">2019</asp:ListItem>
                            <asp:ListItem Value="2018">2018</asp:ListItem>
                            <asp:ListItem Value="2017">2017</asp:ListItem>
                            <asp:ListItem Value="2016">2016</asp:ListItem>
                            <asp:ListItem Value="2015">2015</asp:ListItem>
                            <asp:ListItem Value="2014">2014</asp:ListItem>
                            <asp:ListItem Value="2013">2013</asp:ListItem>
                            <asp:ListItem Value="2012">2012</asp:ListItem>
                            <asp:ListItem Value="2011">2011</asp:ListItem>
                            <asp:ListItem Value="2010">2010</asp:ListItem>
                        </asp:DropDownList>
                    </td>                                      
     </tr>            
            </table>
        </td>
     </tr>
     <%----------------------------------------------%>
                             <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium;">
                          Self Declaration :</p>
                      </td>

                  </tr>
                        <tr> 
                            <td colspan="4" style="height:40px;">
                                <p>
                                    &nbsp;&nbsp;<asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>     All the informations furnished by me are true to the best of my knowledge and belief, further if any discrepancy found in above I will be responsible for that.   
<br />
                                </p>
                                </td>
                                </tr>
                                  
                       
                             <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium;">
                          Note :</p>
                      </td>

                  </tr>
                                <tr> 
                            <td colspan="4">
                               <%-- <p style="color:Red;">
                                   1)गोदाम की ऊंचाई 14 से 18 फीट के बीच होना चाहिए(Godown height should be between 14 ft to 18 ft.) 
 <br />
                                </p>

                                 <p style="color:Red;">
                                   2) 'A' श्रेणी की संयुक्त भागीदारी योजना के अंतर्गत उन्ही गोदाम संचालको को शामिल किया जाएगा जो अपनी गोदामों में निम्न अर्हताऐं रखते हों :- <br />
                                      &nbsp;&nbsp; (i) WDRA अथवा आयुक्त,खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश का वैध पंजीयन/लायसेंस हो | &nbsp;&nbsp;  (ii) गोदाम में डामरीकृत/सी. सी. रोड़/WBM हो |<br/></बीआर> &nbsp;&nbsp;(iii) गोदाम परिसर में इलेक्ट्राॅनिक वेब्रिज हो | &nbsp;&nbsp; (iv) गोदाम में प्रत्येक गेट पर जालीदार शटर/गेट हो | &nbsp;&nbsp;(v) गोदाम परिसर की बाउण्ड्री बाउण्ड्रीबाल/चैनलिंग फेंसिंग/बार्बेड वायर फेंसिंग से कवर्ड हो |
 <br />
                                </p>    
                                 <p style="color:Red;">
                                   3) ऐसे शेष गोदाम जो 'A' श्रेणी की संयुक्त भागीदारी योजना हेतु निर्धारित अर्हताए नहीं रखते हैं, उन्हें 'B' श्रेणी की संयुक्त भागीदारी योजना में सम्मिलित किया जाएगा | 
 <br />
                                </p> --%>
                            </td>

                        </tr>
                                      <tr>
                  
                    <td align="center" colspan="4">
                  
                        <asp:Button ID="btnsubmit" runat="server" Text="Update" class="submit" OnClick="btnsubmit_Click" ></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <%--<input id="Button1" name="Print" type="button" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;" value="Print"  onclick="PrintDiv();" />--%>

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

