<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="TribalGodown_Registration" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Godown Registration</title>
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
  <script language="javascript" type="text/javascript">
      $(function() {
          var _URL = window.URL;

          $("#fileuploadimage").change(function(e) {

          var fileUpload = document.getElementById("fileuploadimage");
          if (typeof (fileuploadimage.files) != "undefined") {
              var size = parseFloat(fileuploadimage.files[0].size / 1024).toFixed(2);
              if (size > 100) {
                  alert("फोटो का साइज़ 100 KB से कम होना चाहिए!");
                  document.getElementById("fileuploadimage").value = '';    
              }
          } else {
              alert("This browser does not support HTML5.");
          }
          });
      });

      $(function() {
          var _URL = window.URL;
         
          $("#fileuploadDoc").change(function(e) {

          var fileUpload = document.getElementById("fileuploadDoc");
          if (typeof (fileuploadDoc.files) != "undefined") {
              var size = parseFloat(fileuploadDoc.files[0].size / 1024).toFixed(2);
                  if (size > 400) {
                      alert("रोजगार कार्यालय पंजीयन का साइज़ 400 KB से कम होना चाहिए!");
                      document.getElementById("fileuploadDoc").value = '';
                  }
              } else {
                  alert("This browser does not support HTML5.");
              }
          });
      });


</script>   

  <script language="javascript" type="text/javascript">

      $(function() {
          var _URL = window.URL;
       
          $("#fileuploadCert").change(function(e) {

          var fileUpload = document.getElementById("fileuploadCert");

          if (typeof (fileuploadCert.files) != "undefined") {
              var size = parseFloat(fileuploadCert.files[0].size / 1024).toFixed(2);
                  if (size > 400) {
                     
                      alert("शैक्षणिक प्रमाण पत्र का साइज़ 400 KB से कम होना चाहिए!");
                      document.getElementById("fileuploadCert").value = '';
                  }
              } else {
                  alert("This browser does not support HTML5.");
              }
          });
      });

      $(function() {
          var _URL = window.URL;

          $("#fileuploadCast").change(function(e) {

              var fileUpload = document.getElementById("fileuploadCast");
              if (typeof (fileuploadCast.files) != "undefined") {
                  var size = parseFloat(fileuploadCast.files[0].size / 1024).toFixed(2);
                  if (size > 400) {
                      alert("जाति प्रमाण पत्र का साइज़ 400 KB से कम होना चाहिए!");
                      document.getElementById("fileuploadCast").value = '';
                  }
              } else {
                  alert("This browser does not support HTML5.");
              }
          });
      });
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
            height: 62px;
        }

    </style>
</head>
<body>

<div id="bg">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="150" />
       
        <div>
                            <form id="form1" runat="server">
                                 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
                               <script type="text/javascript">
                                   function validate() {
                                       var uploadcontrol = document.getElementById('<%=fileuploadimage.ClientID%>').value;
                                       //Regular Expression for fileupload control.
                                       var reg = /^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpeg|.JPEG|.gif|.GIF| .png|.PNG)$/;
                                       if (uploadcontrol.length > 0) {
                                           //Checks with the control value.
                                           if (reg.test(uploadcontrol)) {
                                               return true;
                                           }
                                           else {
                                               //If the condition not satisfied shows error message.
                                               alert("Only JPG,PNG and GIF files are allowed!");
                                               return false;
                                           }
                                       }
                                   } //End of function validate.
</script>
                                <center>
                    <table>
                        <%--<tr >
                            
                            <td  colspan="4" style="background-color: #66CCFF"> 
                                <p style="font-size: medium; color: #008080;">आवेदक की जानकारी  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Log out</asp:LinkButton></p>
                            </td>
                        </tr>--%>
                         <tr >
                            <td colspan="4" style="font-size: medium; width: 1000px;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                             <%--<asp:LinkButton ID="LinkButton10" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx" ForeColor="White"></asp:LinkButton>--%>
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
                    <td>
                        &nbsp;प्रथम नाम:
                    </td>
                    <td>
                        <input id="txtfname" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             मध्य नाम:
                    </td>
                    <td>
                        <input id="txtmname" name="rname" runat="server" class="text" type="text" />
                    </td>
                    </tr>
                         <tr>
                    <td>
                        &nbsp;उपनाम :
                    </td>
                    <td>
                        <input id="txtlname" name="rname" runat="server" class="text" type="text" />
                    </td>
                               <td>
                                   माता का नाम:
                    </td>
                    <td>
                        <input id="txtMotherName" name="rname" runat="server" class="text" type="text" />
                    </td>
                    </tr>
   <tr>
                    <td>
                        &nbsp;पिता का नाम  :
                    </td>
                    <td>
                        <input id="txtFatherName" name="rname" runat="server" class="text" type="text" />
                    </td>
         <td>
                                            जाति:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlcaste" runat="server" Enabled="false">
                            <asp:ListItem Selected="True">ST</asp:ListItem>
                            <asp:ListItem>SC</asp:ListItem>
                            <asp:ListItem>OBC</asp:ListItem>
                            <asp:ListItem>General</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                        <tr>
                            <td>
                                &nbsp;ई-मेल:
                            </td>
                            <td>
                                <input id="txtemail" runat="server" name="remail" readonly="true" class="text" type="text" runat="server" />
                            </td>
                              <td>
                        मोबाइल नंबर:
                    </td>
                    <td>
                        <%--<input id="txtmobile" name="ryear" runat="server" type="text" class="text" />--%>
                        <asp:TextBox id="txtmobile" name="ryear" runat="server" ReadOnly="true" 
                            type="text" class="text" MaxLength="10"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtmobile"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                        </tr>

                    <tr>
                                        <td>&nbsp;जन्मतिथि:</td>
                    <td>
                    

                 <%--   <input id="text3" type="text" name="rage" class="text" size="10px" />--%>
                  
                      <asp:TextBox ID="txtDOB" runat="server" CssClass="text"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtDOB"></cc1:CalendarExtender>
                    </td>
                          <td>
                       कुल परिवार की आय
                    </td>
                    <td>
                        <%--<input id="txtFIncome" name="ryear" type="text" class="text" runat="server" />--%>
                        <asp:TextBox id="txtFIncome" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtFIncome"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                  </tr>
                   <tr>
                 
                   <td>
                   &nbsp;लिंग:
                   </td>
                   <td>
                      पुरुष: <asp:RadioButton ID="rdomale" runat="server" GroupName="gender" Width="70px" Checked="true"/>
                      स्त्री: <asp:RadioButton ID="rdofemale" runat="server" GroupName="gender" Width="70px" />
                   </td>

                         <td>
                       कुल परिवार के सदस्यों की संख्या:
                    </td>
                    <td>
                        <%--<input id="txtFMember" runat="server" name="ryear" type="text" class="text"/>--%>
                        <asp:TextBox id="txtFMember" runat="server" name="ryear" type="text" 
                            class="text" MaxLength="2"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFMember"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                   </tr>
                   
                 
                   

                    <tr>
                    <td>
                        <span data-dobid="hdw"><span lang="en-us"></span>&nbsp;व्यवसाय</span>: </td>
                    <td>
                        <%--<input id="txtOccupation" name="ryear" type="text" class="text" runat="server"/>--%>
                        <asp:DropDownList ID="ddlEmp" runat="server" AutoPostBack="true"  Height="25px"
                            Width="232px" onselectedindexchanged="ddlEmp_SelectedIndexChanged"
                                            >
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="Employeed">Employeed</asp:ListItem>
                            <asp:ListItem>Unemployment</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                            <td>
                       कुल परियोजना लागत (रू):
                    </td>
                    <td>
                        <%--<input id="txtTPV" name="ryear" type="text" class="text" runat="server"/>--%>
                        <asp:TextBox id="txtTPV" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtTPV"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                    </tr>

               
                   
                      <tr>
                    <td>
                        &nbsp;रोजगार कार्यालय नंबर :
                    </td>
                    <td>
                        <input id="txtRKN" name="membershipno" type="text" class="text" runat="server"/>
                    </td>
                              <td>
                       वित्त <span lang="hi">का</span> स्रोत(प्रमाण पत्र प्रस्तुत करे):
                    </td>
                    <td>
                        <input id="txtSOI" name="ryear" type="text" class="text" placeholder="उदाहरण: बैंक,स्वयं" runat="server"/>
                    </td>
                    </tr>
                    

                    <tr>
                    <td >
                        &nbsp;वर्तमान पता:
                    
                    </td>
                    
                   
                    <td>
                        <textarea id="txtCaddress" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    
                    </td>
                         <td >
                        स्थाई पता:
                    
                    </td>
                    
                   
                    <td>
                        <textarea id="txtPaddress" runat="server" cols="20" maxlength="150" rows="2" name="radr" class="text" style=" width:223px;"></textarea>
                    
                    </td>


                    </tr>
                         <tr>
                                        <td>&nbsp;पिन कोड :</td>
                    <td>
                    <%--<input id="txtCPIN" type="text" runat="server" name="rage" class="text"/>--%>
                    <asp:TextBox id="txtCPIN" type="text" runat="server" name="rage" class="text" 
                            MaxLength="6"></asp:TextBox>
                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCPIN"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                          <td>
                      पिन कोड :
                    </td>
                    <td>
                        <%--<input id="txtPPIN" name="ryear" runat="server" type="text" class="text" />--%>
                        <asp:TextBox id="txtPPIN" type="text" runat="server" name="rage" class="text" 
                            MaxLength="6"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtPPIN"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                  </tr>
                          <tr>
                                        <td>&nbsp;जिला :</td>
                    <td>
                    

                    <asp:DropDownList ID="ddlCDistrict" runat="server" AutoPostBack="true"  Height="25px"
                            Width="232px" onselectedindexchanged="ddlCDistrict_SelectedIndexChanged"
                                            >
                        </asp:DropDownList>
                    </td>
                          <td>
                     जिला :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlPDistrict" runat="server" AutoPostBack="true" Height="25px" 
                             Width="232px" onselectedindexchanged="ddlPDistrict_SelectedIndexChanged"
                                            >
                        </asp:DropDownList>
                    </td>
                  </tr>
                          <tr>
                                        <td>&nbsp;विकास खंड :</td>
                    <td>
                    

                     <asp:DropDownList ID="ddlCBlock" runat="server" AutoPostBack="false" Width="232px" Height="25px"
                            Enabled="false" onselectedindexchanged="ddlCBlock_SelectedIndexChanged"
                                            >
                        </asp:DropDownList>
                    </td>
                          <td>
                      विकास खंड :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlPBlock" runat="server" AutoPostBack="true" Width="232px" Enabled="false" Height="25px"
                                            >
                        </asp:DropDownList>
                    </td>
                  </tr>
                  
                          <tr>
                                        <td>&nbsp;शिक्षा:</td>
                   <%-- <td>
                      
                         शिक्षित:   <asp:RadioButton ID="rdoEducate" runat="server" GroupName="Educate" Width="70px" Checked="true"/>
                         अशिक्षित:<asp:RadioButton ID="rdoNonEducate" runat="server" GroupName="Educate" Width="70px" />
                   
                    </td>--%>
                    <td>
                         <asp:DropDownList ID="ddlEduc1" runat="server" AutoPostBack="true"  Height="25px"
                            Width="110px" onselectedindexchanged="ddlEduc1_SelectedIndexChanged" 
                                            >
                            <%-- <asp:ListItem>12 th</asp:ListItem>
                             <asp:ListItem>Graduate</asp:ListItem>
                             <asp:ListItem>Post-Graduate</asp:ListItem>--%>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlEduc2" Enabled="true" runat="server" AutoPostBack="true" Height="25px"
                            Width="100px" 
                                            >
                            
                        </asp:DropDownList>
                    </td>
                        <td>वोटर आई डी नंबर :</td>
                    <td>
                    

                    <input id="txtVID" type="text" name="rage" class="text" runat="server"/>
                    </td>  
                  </tr>
                        <tr>
                                        
                          <td>
                     &nbsp;आधार कार्ड नंबर :
                    </td>
                    <td>
                        <input id="txtAno" name="ryear" type="text" class="text" runat="server" 
                            maxlength="12"/>
                    </td>
                    <td>पैन कार्ड नंबर :</td>
                    <td>
                    

                    <input id="txtPAN" type="text" name="rage" class="text" runat="server" 
                            maxlength="10" />
                    </td>
                  </tr>
                  <tr>
                      <td colspan="4" style="background-color: #008CBA">
                          <p style="font-size: medium; color: White;">
                          &nbsp;&nbsp;वेयरहाउस संचालन जिस जगह करना है उसका विवरण:</p>
                      </td>

                  </tr>
                          <tr>
                                        <td>
                                            &nbsp;जिला

                                        </td>
                                        <td>
                                         
                        <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true" Width="232px" Enabled="false" Height="25px"
                                                onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                                      <td>
                                            विकास खंड

                                        </td>
                                        <td>
                                         
                        <asp:DropDownList ID="ddlBlock" runat="server" Enabled="false" Width="232px"  Height="25px"
                                                >
                        </asp:DropDownList>
                    </td>   
                                    </tr>
                           <tr>
                    <td >
                        
                    &nbsp;पता :
                    
                    </td>
                    
                   
                    <td>
                        <textarea id="txtWAddr" runat="server" maxlength="200" cols="20" rows="2" name="radr" class="text" style=" width:223px;"></textarea>
                    
                    </td>
                      <td>
                     उपलब्ध भूमि की विकासखंड से दूरी (KM) :
                    </td>
                    
                     <td>
                       <%-- <input id="txtDFTO" name="ryear" type="text" class="text" runat="server"/>--%>
                         <asp:TextBox id="txtDFTO" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtDFTO"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                                            </tr>
                                            <tr>
                                                 <td>
                     &nbsp;उपलब्ध भूमि की मात्रा (एकड़ मे) :
                    </td>
                    
                     <td>
                      <%--  <input id="txtQofForm" name="ryear" type="text" class="text" runat="server"/>--%>
                       <asp:TextBox id="txtQofForm" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtQofForm"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                                            </tr>

                         <tr>
                      <td colspan="4" style="background-color: #008CBA">
                          <p style="font-size: medium; color: White ">
                          &nbsp;&nbsp;बैंक संवन्धित जानकारी:</p>
                      </td>

                  </tr>
                          <tr>
                                        <td>
                                            &nbsp;बैंक का नाम

                                        </td>
                                        <td>
                                         
                        <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="true" Width="232px" Height="25px" onselectedindexchanged="ddlBank_SelectedIndexChanged"
                                              >
                        </asp:DropDownList>
                    </td>
                                      <td>
                                            अकाउंट नंबर

                                        </td>
                                        <td>
                                         
             
                        <asp:TextBox id="txtAccNo" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtAccNo"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>   
                                    </tr>
                           <tr>
                         <td>
                                            
                                            &nbsp;<asp:Label runat="server" ID="lblOBank" Text="बैंक शाखा" Visible="true"></asp:Label>

                                        </td>
                                        <td>
                                         
 
                        <asp:DropDownList ID="ddlBBranch" runat="server" AutoPostBack="true" Width="232px" Height="25px" onselectedindexchanged="ddlBBranch_SelectedIndexChanged"
                                              >
                        </asp:DropDownList>
                   
                    </td>
                    <td >
                        
                    IFSC कोड<span lang="hi"> </span>:
                    
                    </td>
                    
                   
                    <td>
                        <input id="txtIFSC" name="ryear" type="text" class="text" runat="server" />
                    
                    </td>
                                            </tr>

                         <tr>
                      <td colspan="4" style="background-color: #008CBA">
                          <p style="font-size: medium; color: White ">
                          &nbsp;&nbsp;किन्ही 2 परिचित व्यक्तियों की जानकारी:</p>
                      </td>

                  </tr>
                          <tr>
                                        <td>
                                            &nbsp;1 व्यक्ति का नाम

                                        </td>
                                        <td>
                                         
                        <input id="txtP1" name="ryear" type="text" class="text" runat="server" />
                    </td>
                                      <td>
                                            पता

                                        </td>
                                        <td>
                                         
                        <input id="txtP1Add" name="ryear" type="text" class="text" runat="server" />
                    </td>   
                                    </tr>
                           <tr>
                    <td >
                        
                    &nbsp;मोबाइल नं<span lang="hi"> </span>:
                    
                    </td>
                    
                   
                    <td>
                        <%--<input id="txtP1Mob" name="ryear" type="text" class="text" runat="server" />--%>
                        <asp:TextBox id="txtP1Mob" name="ryear" type="text" class="text" runat="server" 
                            MaxLength="10"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtP1Mob"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    
                    </td>
                                <td >
                        
                    ई मेल(यदि कोई हो):
                    
                    </td>
                    
                   
                    <td>
                        <input id="txtP1Email" name="ryear" type="text" class="text" runat="server"/>
                    
                    </td>
                                            </tr>
                         <tr>
                    <td >
                        
                    &nbsp;संबंध:
                    
                    </td>
                    
                   
                    <td>
                        <input id="txtP1Rel" name="ryear" type="text" class="text" runat="server"/>
                    
                    </td>
                                            </tr>
                        <tr>
                                        <td>
                                            &nbsp;2 व्यक्ति का नाम

                                        </td>
                                        <td>
                                         
                        <input id="txtP2" name="ryear" type="text" class="text" runat="server" />
                    </td>
                                      <td>
                                            पता

                                        </td>
                                        <td>
                                         
                        <input id="txtP2Add" name="ryear" type="text" class="text" runat="server" />
                    </td>   
                                    </tr>
                        <tr>
                    <td >
                        
                    &nbsp;मोबाइल नं:
                    
                    </td>
                    
                   
                    <td>
                        <%--<input id="txtP2Mob" name="ryear" type="text" class="text" runat="server"/>--%>
                        <asp:TextBox id="txtP2Mob" name="ryear" type="text" class="text" runat="server" 
                            MaxLength="10"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtP2Mob"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    
                    </td>
                                <td >
                        
                    ई मेल(यदि कोई हो):
                    
                    </td>
                    
                   
                    <td>
                        <input id="txtP2Email" name="ryear" type="text" class="text" runat="server"/>
                    
                    </td>
                                            </tr>
                         <tr>
                    <td >
                        
                    &nbsp;संबंध:
                    
                    </td>
                    
                   
                    <td>
                        <input id="txtP2Rel" name="ryear" type="text" class="text" runat="server"/>
                    
                    </td>
                                            </tr>
                                                   <tr>
                      <td colspan="4" style="background-color: #008CBA">
                          <p style="font-size: medium; color: White ">
                          &nbsp;&nbsp;दस्तावेज़ जमा करने हेतु संबंधित क्षेत्रीय कार्यालय की जानकारी:</p>
                      </td>

                  </tr>
                          <tr>
                                        <td colspan="4">
                                           
                       <asp:Label ID="lblRegionAdd" runat="server" Text="" name="ryear" type="text" class="text"></asp:Label>
                                        </td>
               
                                    </tr>
                                     <tr>
                                        <td colspan="4">
                                           
                       <asp:Label ID="lblRegionAdd2" runat="server" Text="" name="ryear" type="text" class="text"></asp:Label>
                                        </td>
               
                                    </tr>

                             <tr>
                      <td colspan="4" style="background-color: #008CBA">
                          <p style="font-size: medium; color: White">
                           &nbsp;&nbsp;डॉकयुमेंट अपलोड करें :</p>
                      </td>

                  </tr>
                          <tr>
                                        <td>
                                            आवेदक की फोटो

                                        </td>
                                        <td>
                                         <asp:FileUpload ID="fileuploadimage" OnUploadedComplete="FileUploadComplete" 
                                                runat="server" onload="FileUploadComplete"></asp:FileUpload>
                                        
                                 </td>
                                      <td>
                                            रोजगार कार्यालय पंजीयन

                                        </td>
                                        <td>
                                         
                    
                                            <asp:FileUpload ID="fileuploadDoc" runat="server"></asp:FileUpload>
                    </td>   
                                    </tr>
                                    <tr>
                                    <td>
                                           शैक्षणिक प्रमाण पत्र

                                        </td>
                                        <td>
                                         
                    
                                            <asp:FileUpload ID="fileuploadCert" runat="server"></asp:FileUpload>
                    </td>
                    
                    <td>
                                           जाति प्रमाण पत्र

                                        </td>
                                        <td>
                                         
                    
                                            <asp:FileUpload ID="fileuploadCast" runat="server"></asp:FileUpload>
                    </td>
                                    </tr>
                       
                             <tr>
                      <td colspan="4" style="background-color:  #008CBA">
                          <p style="font-size: medium; color:White">
                          &nbsp;&nbsp;घोषणा:</p>
                      </td>

                  </tr>
                        <tr> 
                            <td colspan="4">
                                <p>
                                    1. <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>मै घोषणा करता /करती हूं कि मेरे द्वारा दी गयी उपरोक्त जानकारी मेरे ज्ञान के अनुसार सत्य है, मैंने उसमें कुछ भी छुपाया नहीं है। मुझे यह ज्ञात है कि मेरे द्वारा असत्य या भ्रामक जानकारी देने पर मेरे विरुद्ध आपराधिक दण्डात्मक कार्यवाही की जा सकती है। साथ ही मुझे प्राप्त समस्त लाभों को भी वापस किया जाएगा। <br />
                                    2. <asp:CheckBox ID="CheckBox2" runat="server"></asp:CheckBox>चयन के किसी भी स्तर पर अपात्र पाये जाने पर मेरा आवेदन निरस्त किया जा सकेगा। 
                                </p>
                            </td>

                        </tr>
                                      <tr>
                  
                    <td align="center" colspan="4">
                  
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="submit" OnClick="btnsubmit_Click" ></asp:Button>

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
