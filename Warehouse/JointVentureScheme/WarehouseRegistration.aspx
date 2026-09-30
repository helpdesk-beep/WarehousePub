<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseRegistration.aspx.cs" Inherits="JointVentureScheme_WarehouseRegistration" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Warehouse Registration</title>
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
	<script type="text/javascript">
	    function disablefield() {
	        if (document.getElementById('rdoNo').checked == 1) {
	            document.getElementById('txtWLicNo').disabled = 'disabled';
	            document.getElementById('txtWLicNo').value = '';
	            document.getElementById('txtSLDate').disabled = 'disabled';
	            document.getElementById('txtSLDate').value = '';
	        } else {
	            document.getElementById('txtWLicNo').disabled = '';
	            document.getElementById('txtWLicNo').value = '';
	            document.getElementById('txtSLDate').disabled = '';
	            document.getElementById('txtSLDate').value = '';
	        }
	    } 
</script>


<script type="text/javascript">
    function disablefield1() {
        if (document.getElementById('rdoNoW').checked == 1) {
            document.getElementById('txtWDRALicenceNo').disabled = 'disabled';
            document.getElementById('txtWDRALicenceNo').value = '';
            document.getElementById('txtWDRALDate').disabled = 'disabled';
            document.getElementById('txtWDRALDate').value = '';
        } else {
            document.getElementById('txtWDRALicenceNo').disabled = '';
            document.getElementById('txtWDRALicenceNo').value = '';
            document.getElementById('txtWDRALDate').disabled = '';
            document.getElementById('txtWDRALDate').value = '';
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

        .style2
        {
            height: 22px;
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
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
       
        <div id="PrintDiv">
                            <form id="form1" runat="server">
                                 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
                               <%--<script type="text/javascript">
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
</script>--%>
                                <center>
                    <table style="width:100%">
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
                      <td colspan="4">
                          <marquee direction="left" behavior="alternate"> <p style="font-size: 16px; color: #008080; font-weight:bold ">
                          Warehouse Registration </p></marquee>
                      </td>  

                  </tr>                         
                         
                         <tr>
                      <td colspan="4" style=" background-color:#66CCFF; height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold ">
                          Owner Details <span style="color: #FF0000; font-size:x-large;">*</span> </p>
                      </td>  
                  </tr>
                  <tr>
                    <td>
                        &nbsp;&nbsp;Owner Name :
                    </td>
                    <td style="font-weight:bold; word-wrap:break-word; color:Black">
                        <asp:Label ID="lblAuthPerson" runat="server"></asp:Label>
                    </td>
                          <td>
                              Registered Email ID:
                    </td>
                    <td style="font-weight:bold; word-wrap:break-word;color:Black ">
                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                   
                          <td class="style2">
                              &nbsp;&nbsp;Mobile Number:
                    </td>
                    <td class="style2" style="font-weight:bold; word-wrap:break-word;color:Black ">
                        <asp:Label ID="lblMob" runat="server"></asp:Label>
                    </td>
                     <td>
                        
 Type of Applicant/Entity  :
                    </td>
                    <td style="font-weight:bold; word-wrap:break-word; color:Black ">
                        <asp:Label ID="lblAppType" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                   
                          <td>
                              &nbsp;&nbsp;District :
                    </td>
                    <td style="font-weight:bold; word-wrap:break-word;color:Black ">
                        <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                    </td>
                    <td>
                      Category :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCast" runat="server" AutoPostBack="false" Width="90px" Height="25px" Enabled="true"
                                            >
                                             <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">SC</asp:ListItem>
                            <asp:ListItem Value="2">ST</asp:ListItem>
                            <asp:ListItem Value="3">OBC</asp:ListItem>
                            <asp:ListItem Value="4">GEN</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                  <tr>

                      <td colspan="4"  style=" border-color:#66CCFF; background-color:#66CCFF;height:20px; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         Warehouse Details <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>                         

                  </tr>
                  <tr>               
                    <td>
                       &nbsp;&nbsp;Warehouse Name : <br /><br /><br />&nbsp;&nbsp;Office Contact No./Mobile No:
                    </td>
                    <td>
                        <input id="txtWarehouseName" name="rname" runat="server" class="text"  style="text-transform: uppercase;" 
                            type="text" tabindex="1"/>
                        <br />
                        <br />
                     
                        <asp:TextBox ID="txtWareContactNo" runat="server" class="text" type="text" 
                            TabIndex="3" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtWareContactNo"
                                        ValidChars="0123456789">
                         </cc1:FilteredTextBoxExtender>                        
                    </td>
                          <td>
                              Warehouse/Office Address with Postal Address:
                    </td>
                    <td>
                        
                        <textarea id="txtWareAddress" maxlength="150" runat="server" cols="20" rows="2" 
                            name="radr" class="text" style=" width:223px;" tabindex="2"
                        ></textarea>
                    </td>
                    </tr>
                    <tr>

                          <td>
                              &nbsp;&nbsp;Landmark Near Warehouse:
                    </td>
                    <td>
                        <input id="txtLandmark" name="rname" runat="server" class="text" type="text" 
                            tabindex="4" />
                    </td>
                          <td>
                             District :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWarDistrict" runat="server" AutoPostBack="true" 
                            Width="232px" Height="25px" 
                            onselectedindexchanged="ddlWarDistrict_SelectedIndexChanged" TabIndex="5">
                        </asp:DropDownList>
                    </td>                    
                    </tr>

                    <tr>
                  
                       <td>
                      &nbsp;&nbsp;Tehsil :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBlock" runat="server" 
                            Width="232px" Height="25px" Enabled="true"  TabIndex="6">
                        </asp:DropDownList>
                    </td>
                    <td>
                            Block :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlblocknew" runat="server" Width="232px" Height="25px" AutoPostBack="true" 
                            TabIndex="7" onselectedindexchanged="ddlblocknew_SelectedIndexChanged" >
                        
                        </asp:DropDownList>
                    </td>                    
                    </tr>
                    <tr>
                 
                          <td>
                             &nbsp;&nbsp;Nearest Branch of MPWLC :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="false" Enabled="true"
                            Width="232px" Height="25px" TabIndex="8">
                        </asp:DropDownList>
                    </td>
                      <td>
                      Distance from nearest branch of MPWLC (in KM):
                    </td>
                    <td>
                       <%--<input id="txtDistance" name="rname" runat="server" class="text" type="text"/>--%>
                           <asp:TextBox ID="txtDistance" runat="server" class="text" type="text" 
                            style="width:90px" TabIndex="9" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtDistance"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                    </tr>
                    
                    <%--<tr>

                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF;height:20px; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         License Detail <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>                        

                  </tr>
                    <tr>
                    <td colspan="2" style="height:25px;">
                       &nbsp;&nbsp;Does it has Present Validity?(State Licence):
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Yes <asp:RadioButton ID="rdoYes" 
                            runat="server" GroupName="StateLicence" 
                            Width="50px" Checked="true" oncheckedchanged="rdoYes_CheckedChanged" 
                            AutoPostBack="true" TabIndex="10"/>
                         NO <asp:RadioButton ID="rdoNo" runat="server" GroupName="StateLicence" 
                             oncheckedchanged="rdoNo_CheckedChanged" AutoPostBack="true" 
                            TabIndex="11"/>
                        
                    </td>
                    </tr>
                    <tr id="TrStateL" runat="server">
                    <td>
                    &nbsp;&nbsp;Warehouse License No.:
                    </td>
                    <td>
                        <input id="txtWLicNo" name="rname" runat="server" class="text" type="text" 
                            tabindex="12" />
                    </td>
                          <td>
                             Valid Upto Date (dd/mm/yyyy):
                    </td>
                    <td>
                        <asp:TextBox ID="txtSLDate" runat="server" CssClass="text" TabIndex="13" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtSLDate"></cc1:CalendarExtender>
                    </td>
                    </tr>
                      <tr>
                    <td colspan="2" style="height:25px;">
                       &nbsp;&nbsp;Does it has Present Validity?(WDRA Licence) :

                      &nbsp;&nbsp;Yes <asp:RadioButton ID="rdoYesW" runat="server" GroupName="WDRALicence" 
                            Width="50px" Checked="true" tabindex="14"
                            oncheckedchanged="rdoYesW_CheckedChanged" AutoPostBack="true"/>
                         NO <asp:RadioButton ID="rdoNoW" runat="server" GroupName="WDRALicence" 
                             oncheckedchanged="rdoNoW_CheckedChanged" AutoPostBack="true" tabindex="15"/>
                    </td>
                    </tr>
                    <tr id="TWDRAL" runat="server">
                    <td>
                    &nbsp;&nbsp;WDRA registration No. :
                    </td>
                    <td>
                        <input id="txtWDRALicenceNo" name="rname" runat="server" class="text" type="text" tabindex="16" />
                    </td>
                          <td>
                             Valid Upto Date(dd/mm/yyyy) :
                    </td>
                    <td>
                        <asp:TextBox ID="txtWDRALDate" runat="server" CssClass="text" tabindex="17" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender3" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtWDRALDate"></cc1:CalendarExtender>
                    </td>
                    </tr>--%>
                 
                  <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF;height:25px; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Warehouse Incharge/Manager Details <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>  

                  </tr>
                  <tr>
                    <td>
                         &nbsp;&nbsp;Authorised Person :<br /><br /><br /> &nbsp;&nbsp;Designation :
                    </td>
                    <td>
                        <input id="txtIncharge" name="rname" runat="server" class="text" type="text" tabindex="18"/><br />
                        <br />
                        <input id="txtDesign" name="rname" runat="server" class="text" type="text" tabindex="19"/>
                    </td>
                          <td>
                              Authorised Person Address with Postal Address:
                    </td>
                    <td>
                        
                        <textarea id="txtInchAdd" maxlength="150" runat="server" cols="20" rows="2" 
                            name="radr" class="text" style=" width:223px;" tabindex="20"
                        ></textarea>
                    </td>
                    </tr>
                    <tr>
                    <td>
                         &nbsp;&nbsp;Email ID :
                    </td>
                    <td>
                        <input id="txtInchargeEmail" name="rname" runat="server" class="text" 
                            type="text" tabindex="21" />
                    </td>                    
                          <td>
                              Mobile No. :
                    </td>
                    <td>
                        <%--<input id="txtInchMob" name="rname" runat="server" class="text" type="text"/>--%>
                        <asp:TextBox id="txtInchMob" name="ryear" type="text" class="text" 
                            runat="server" TabIndex="22"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtInchMob"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                    </tr>
                  <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid;height:25px;border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         Godown Description <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>                         

                  </tr>
                  <tr><td colspan="4" style="color:Red;">Note :</td></tr>
                  
                   <tr>
                      <td colspan="4" style="background-color:none; font-size:small;">                                         
                    &nbsp;&nbsp;1.एक गोदाम संचालक के रजिस्ट्रेशन मे शामिल किए जाने वाले समस्त गोदाम एक ही परिसर मे स्थित होने चाहिए, अन्य परिसर अथवा अन्य स्थान मे स्थित गोदामो के लिए प्रथक रजिस्ट्रेशन करे।
                      </td>
                  </tr>
                  <tr>
                  <td colspan="4">
                  &nbsp;&nbsp;2.Formula for Capacity = [Length*Breadth*(Height-3)/80].
                  </td>
                  </tr>
                  <tr>
                  <td colspan="4">
                  &nbsp;&nbsp;3.गोदाम की ऊंचाई 14 से 18 फीट के बीच होना चाहिए(Godown height should be between 14 ft to 18 ft.). 
                  </td>
                  </tr>
                  <tr>
                  <td style="height:10px;">
                  </td>
                  </tr>
                                    
                 <tr>
                    
                    <td colspan="4" id="GVGodowns" runat="server" visible="true" style="text-align:center; width:100%; " align="center">
                        <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" ShowFooter="true" Width="100%"
                         EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                         GridLines="Horizontal" onrowdeleting="gvGodown_RowDeleting" AutoGenerateDeleteButton="false">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="Godown No." />
                             
                                <asp:TemplateField HeaderText="Length in Feet">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLenght" runat="server" Width="70px" Text='<%# Eval("Lenght") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Width in Feet">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWidth" runat="server" Width="70px" Text='<%# Eval("Width") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Height in Feet">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtHeight" runat="server" Width="70px" Text='<%# Eval("Height") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="ckstack" runat="server" AutoPostBack="True" OnCheckedChanged="ckstack_CheckedChanged" />
                                    </ItemTemplate>
                                   </asp:TemplateField>
                                <asp:TemplateField HeaderText="Capacity (MT)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCapacity" runat="server" Enabled="false" Width="70px" Text='<%# Eval("Capacity") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Year">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtConstY" runat="server" Width="70px" Text='<%# Eval("ConstY") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           
<asp:TemplateField HeaderText="Licence Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlgdwntype" runat="server" Height="21px" Width="70px"> 
                                                <asp:ListItem Value="-1">--Select--</asp:ListItem>                                  
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                               
                                <asp:TemplateField HeaderText="Licence No./Application No." HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="Gtxtlicno" runat="server" Width="150px"  Font-Bold="true" Height="15px" align="Center" Text='<%# Eval("LNo") %>' placeholder="Enter Licence Number"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lic. Issue/Application Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GtxtLicIssuedate" runat="server" Width="70px" Font-Bold="true" Height="15px"  Text='<%# Eval("LIssueDate") %>' onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server"  Format="dd/MM/yyyy"
                        TargetControlID="GtxtLicIssuedate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>                                 
                                <asp:TemplateField HeaderText="Lic. Expiry Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GtxtLicExpdate" runat="server" Width="80px" Font-Bold="true" Height="15px" Text='<%# Eval("LExpDate") %>' onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server"  Format="dd/MM/yyyy"
                        TargetControlID="GtxtLicExpdate"></cc1:CalendarExtender>
                                    </ItemTemplate>
             <FooterStyle HorizontalAlign="Right" />
            <FooterTemplate>
             <asp:Button ID="ButtonAdd" runat="server" Text="Add Godown" Width="80px"  Font-Size="12px"
                    onclick="ButtonAdd_Click" />
            </FooterTemplate>                                    
                                </asp:TemplateField>                          
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
                        
                        

                    </td>
                </tr>

                         <tr>
                      
                           <td colspan="4" align="center">
                        Total Capacity : <asp:Label ID="lblTotalCapacity" runat="server" Text="0.00" Font-Bold="true" ForeColor="Red"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Total Registration Fee: <asp:Label ID="lblTotalRegAmt" runat="server" Text="0.00" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                   
                  </tr>
                         <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF;height:25px; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Bank Account Detail <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td> 

                  </tr>
                  <tr>
                      <td colspan="4" style="background-color:none; font-size:small;">                                         
                          <p> &nbsp;&nbsp;क्रप्या अपना बैंक लोन अकाउंट डीटेल प्रविष्ट करे, यदि बैंक लोन नहीं है तब अन्य बैंक अकाउंट जिसमे गोदाम किराया प्राप्त करना चाहते है वह प्रविष्ट करे।</p>
                      </td>

                  </tr>
                          <tr>
                                        <td>
                                             &nbsp;&nbsp;बैंक का नाम

                                        </td>
                                        <td>
                                         
                        <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="true" Width="232px" Height="25px" 
                        onselectedindexchanged="ddlBank_SelectedIndexChanged" TabIndex="24">
                        </asp:DropDownList>
                    </td>
                                      <td>
                                            अकाउंट नंबर

                                        </td>
                                        <td>
                                         
             
                        <asp:TextBox id="txtAccNo" name="ryear" type="text" class="text" runat="server" TabIndex="26"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtAccNo"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>   
                                    </tr>
                           <tr>
                         <td>
                                            
                                            &nbsp;&nbsp;<asp:Label runat="server" ID="lblOBank" Text="बैंक शाखा" Visible="true"></asp:Label></td>
                                        <td>
                                         
 
                        <asp:DropDownList ID="ddlBBranch" runat="server" AutoPostBack="true" Width="232" Height="25px" 
                        onselectedindexchanged="ddlBBranch_SelectedIndexChanged" TabIndex="25">
                        </asp:DropDownList>
                   
                    </td>
                    <td >
                        
                    IFSC कोड<span lang="hi"> </span>:
                    
                    </td>
                    
                   
                    <td>
                        <input id="txtIFSC" name="ryear" type="text" class="text" runat="server" 
                            tabindex="27"/>
                    
                    </td>
                                            </tr>

                      

                           
                                     <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF;height:25px; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Nearest Distance of Warehouse From <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td> 

                  </tr>
                  <tr>
                    <td colspan="3">
                     &nbsp;&nbsp;1. Nearest Distance of Warehouse From National Highway/State Highway (in KM) 
                    </td>
                    <td align="right">
                         <asp:TextBox id="txtHighway" name="ryear" type="text" class="text" 
                             runat="server" style="width:90px" TabIndex="29"></asp:TextBox>&nbsp;
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
                        <asp:TextBox id="txtRailway" name="ryear" type="text" class="text" 
                            runat="server" style="width:90px" TabIndex="30"></asp:TextBox>&nbsp;
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
                         <asp:TextBox id="txtMandi" name="ryear" type="text" class="text" runat="server" 
                             style="width:90px" TabIndex="31"></asp:TextBox>&nbsp;
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
                       <asp:TextBox id="txtGS" name="ryear" type="text" class="text" runat="server" 
                            style="width:90px" TabIndex="32"></asp:TextBox>&nbsp;
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtGS"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                          
                    </tr>
                    
                    
                    
                     <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF;height:25px; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                        Warehouse Geogrophical Information<span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>   
                  </tr>
                     
     <%-- ---------------------------------------------------------------------------------------------------------------%>
           <tr>
            <td colspan="4">
              <table style="width: 100%">                   
                  <tr>
                    <td>
                       Warehouse Latitude  :
                    </td>
                    <td style="width:100px;">
                       <%-- <input id="txtlat" name="rname" runat="server" class="text" type="text" style="width:90px"/>--%>
                   <asp:TextBox ID="txtlat" runat="server" class="text" type="text" style="width:90px" 
                            TabIndex="33" ></asp:TextBox>
                    Ex:26.203194
                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtlat"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender></td>
                     <td style="width:370px;">
                      Warehouse Longitude  :
                    </td>
                    <td style="width:100px;">
                      
                        <asp:TextBox ID="txtlong" runat="server" class="text" type="text" 
                            style="width:90px" TabIndex="34"></asp:TextBox>
                    Ex:78.209267
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtlong"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender></td>
                  </tr>
                                     <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF; border-style:solid;height:25px; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         Warehouse Additional Facilities Information <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>  
                  </tr> 
                      
<tr>
                     <td>
                       Motorable Approach Road Type :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlRoadType" runat="server"
                            Width="100" Height="27px" TabIndex="35">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">BT</asp:ListItem>
                            <asp:ListItem Value="2">CC</asp:ListItem>
                            <asp:ListItem Value="3">WBM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td>
                       Width of Road (In Meters) :
                    </td>
                     <td >
                       
                         <asp:TextBox ID="txtRoadWidth" runat="server" class="text" type="text" 
                             style="width:90px" TabIndex="36" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRoadWidth"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                  </tr>                                      
                  <tr>
                     <td>
                      Availability of Shutter/Jali/Chanel Gate in Each Godown :
                    </td>
                    <td>
                         <asp:DropDownList ID="ddlGateType" runat="server"
                            Width="100" Height="27px" TabIndex="37">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td>
                       Number of Gate in Warehouse :
                    </td>
                     <td>
                        
                        <asp:TextBox ID="txtNoGate" runat="server" class="text" type="text" 
                             style="width:90px" TabIndex="38" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtNoGate"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      Power Supply (In Phase) :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlPowersuply" runat="server"
                            Width="100px" Height="27px" TabIndex="39" >
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
                            Width="100px" Height="27px" TabIndex="40">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                  <tr>
                    <td>
                     Water Facility :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlWaterFac" runat="server"
                            Width="100px" Height="27px" TabIndex="41"  >
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
                            Width="100px" Height="27px" TabIndex="42" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr> 
                  <tr>
                    <td>
                       CCTV Camera :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlCCTV" runat="server"
                            Width="100px" Height="27px" TabIndex="43" >
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
                            Width="100px" Height="27px" TabIndex="44" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>                                     
                  <tr>
                    <td>
                     Guard With Guard Room :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlGuard" runat="server"
                            Width="100px" Height="27px" TabIndex="46" >
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
                            Width="100px" Height="27px" TabIndex="47" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr> 
                    
                  <tr>
                    <td>
                      Availability fo Fire Buckets :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlFireBuc" runat="server"
                            Width="100px" Height="27px" TabIndex="48" >
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
                            Width="100px" Height="27px" TabIndex="49" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr> 
                  <tr>
                    <td>
                      Warehouse Boundary Covered by  :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlboundrytype" runat="server"
                            Width="100px" Height="27px" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Boundary Wall</asp:ListItem>
                            <asp:ListItem Value="2">channeling Fencing</asp:ListItem>
                            <asp:ListItem Value="3">Other</asp:ListItem>
                            <asp:ListItem Value="4">Barbed Wire Fencing</asp:ListItem>
                            <asp:ListItem Value="5">Stone Wall</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>                       
                                         
                   <tr>
                      <td colspan="4" style=" border-color:#66CCFF; background-color:#66CCFF;height:25px; border-style:solid; border-width:2px;" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold">
                         Warehouse Hardware Information<span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>   
                  </tr> 
                                                                                                           
                  <tr>
                    <td>
                     Electronic Weighbridge :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlElectWeigh" runat="server"
                            Width="100px" Height="27px" AutoPostBack="True" 
                            onselectedindexchanged="ddlElectWeigh_SelectedIndexChanged" TabIndex="50"
                            >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td><asp:Label ID="lblWB" runat="server" Text="Weighbridge calibration validity date :" Visible="false"></asp:Label></td>

                    <td>
                         <asp:TextBox ID="txtWB_calibrationdate" runat="server" class="text" 
                             Visible="false" type="text"  Width="90px"  
                             onkeydown="javascript:preventInput(event);" onpaste="return false;" 
                             TabIndex="51"></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
                          TargetControlID="txtWB_calibrationdate"></cc1:CalendarExtender>                    
                    </td>                    
                    </tr>
                    <tr>
                                        <td>
                       <asp:Label ID="Label2" runat="server" Text="Weighbridge is Certified By Controler :" Visible="false"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlWeighCertified" runat="server" Visible="false"
                            Width="100px" Height="27px" TabIndex="52">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                       <asp:Label ID="Label1" runat="server" Text="Weighbridge Capacity(In M.T) >30 MT :" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeighCpt" runat="server" class="text" Text="0" type="text" 
                            Visible="false" style="width:90px" TabIndex="53" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtWeighCpt"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                    </td>

                    </tr>
                   <tr id="tr_W_Machine" runat="server" visible="false">
                    <td>
                       <asp:Label ID="Label6" runat="server" Text="Weighing Machine :"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddl_W_Machine" runat="server"
                            Width="100px" Height="27px" TabIndex="54">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                       <asp:Label ID="Label7" runat="server" Text="Weighing Machine Capacity(In K.G) :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWMCpt" runat="server" class="text" Text="0" type="text" 
                            style="width:90px" TabIndex="55" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtWMCpt"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>

                    </tr>                     
                    
                  <tr>
                    <td>
                        Internet Connectivity :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlInternetCon" runat="server"
                            Width="100px" Height="27px" AutoPostBack="true" 
                            onselectedindexchanged="ddlInternetCon_SelectedIndexChanged" TabIndex="56" 
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
                            Width="100px" Height="27px" Visible="false" TabIndex="57">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Fixed Broadband Connections</asp:ListItem>
                            <asp:ListItem Value="2">Mobile Internet</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    </tr>  
                  <tr>
                    <td>
                      <asp:Label ID="Label3" runat="server" Text="Availability of Computer & Required Hardware :" Visible="false"></asp:Label> 
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlHardwareAvl" runat="server"
                            Width="100px" Height="27px" Visible="false" TabIndex="58">
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
                            Width="100px" Height="27px" Visible="false" TabIndex="59">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="2018">2019</asp:ListItem>
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
  <%-- --------------------------------------------------------------------------------------------------------%>     
                  <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium;">
                          Self Declaration : <span style="color: #FF0000;height:25px; font-size:x-large;">*</span></p>
                      </td>

                  </tr>
                        <tr> 
                            <td colspan="4">
                                <p>
                                    <asp:CheckBox ID="CheckBox1" runat="server" TabIndex="60"></asp:CheckBox>     All the informations furnished by me are true to the best of my knowledge and belief, further if any discrepancy found in above I will be responsible for that.   
<br />
                                </p>
                                </td>
                                </tr>
                                  
                       
                             <tr>
                      <td colspan="4" style="background-color: #66CCFF;height:25px;">
                          <p style="font-size: medium;">
                          Note :</p>
                      </td>

                  </tr>
                                <tr> 
                            <td colspan="4">
               <%--                  <p style="color:Red;">
                                   1) देय पंजीकरण शुल्क 40 पैसा/मेट्रिक टन क्षमता (केवल निजी गोदाम संचालको हेतु) | 
 <br />
                                </p>
                                
                                 <p style="color:Red;">
                                   2) 'A' श्रेणी की संयुक्त भागीदारी योजना के अंतर्गत उन्ही गोदाम संचालको को शामिल किया जाएगा जो अपनी गोदामों में निम्न अर्हताऐं रखते हों :- <br />
                                      &nbsp;&nbsp; (i) WDRA अथवा आयुक्त,खाद्य नागरिक आपूर्ति एवं उपभोक्ता संरक्षण, मध्यप्रदेश का वैध पंजीयन/लायसेंस हो | &nbsp;&nbsp;  (ii) गोदाम में डामरीकृत/सी. सी. रोड़/WBM हो |<br/> &nbsp;&nbsp;(iii) गोदाम परिसर में इलेक्ट्राॅनिक वेब्रिज हो अथवा गोदाम परिसर से 500 मीटर विजुयल डिस्टेन्स पर(न्यूनतम 30 MT क्षमता का) अनुबंधित प्रमाणित वेब्रिज हो | &nbsp;&nbsp; (iv) गोदाम में प्रत्येक गेट पर जालीदार शटर/गेट हो | &nbsp;&nbsp;(v) गोदाम परिसर की बाउण्ड्री बाउण्ड्रीबाल/चैनलिंग फेंसिंग/बार्बेड वायर फेंसिंग/स्टोन वाल से कवर्ड हो |
 <br />
                                </p>    
                                 <p style="color:Red;">
                                   3) ऐसे शेष गोदाम जो 'A' श्रेणी की संयुक्त भागीदारी योजना हेतु निर्धारित अर्हताए नहीं रखते हैं, उन्हें 'B' श्रेणी की संयुक्त भागीदारी योजना में सम्मिलित किया जाएगा | 
 <br />
                                </p>   --%> 
                                 <p style="color:Blue; font-size:medium; text-decoration: underline;">
                                  पंजीयन संबंधी जानकारी को सबमिट करने के बाद उसका प्रिंट अनिवार्य रूप से प्राप्त कर रखे| तदुपरान्त पेमेंट विकल्प पर जाकर अनिवार्य रूप से राशि का भुगतान करे, भुगतान कार्यवाही के दौरान आवश्यक जानकारी तथा राशि का विवरण प्राप्त प्रिंटआउट मे उल्लेखित जानकारी तथा राशि को यथावत दर्ज करे। अन्यथा की दशा मे आपका पंजीयन मान्य नहीं होगा। 
 <br />
                                </p>                                                                                                                    
 
                            </td>

                        </tr>
                                      <tr>
                  
                    <td align="center" colspan="4">
                  
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="button button2" Width="150px" Height="30px" OnClick="btnsubmit_Click" ></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <%--<input id="Button1" name="Print" type="button" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;" value="Print"  onclick="PrintDiv();" />--%>

                            <asp:Button ID="btnpayment" runat="server" Text="Proceed to Payment" class="button button2" Width="150px" Height="30px" Visible="false" 
                            onclick="btnpayment_Click" ></asp:Button>
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            
                            <asp:Button ID="btnprint" runat="server" Text="Print Registration" Width="150px" Height="30px" Visible="false" 
                            class="button button2" onclick="btnprint_Click" ></asp:Button>
                            
                    </td>
                    </tr>
                    </table>
<asp:Label ID="Label8" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
<cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlCofirmmsg" TargetControlID="Label8"
   CancelControlID="btnNo" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlCofirmmsg" runat="server" CssClass="modalPopup" Height="330px" Width="700px" >
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td style="color:White; font-weight:bold" align="center">Payment For Registration Fee</td>
                   <td style="width:50px"> <asp:Button ID="btnNo" runat="server" Text="Close" CssClass="no" align="left"/> </td>
            </tr> 
                                         
        </table> 
    </div>
    <div class="body">
                    <table cellspacing="1" style="width:100%;">
                         <tr>
                        <td style="height:15px">
                        </td>
                        </tr>
                         <tr>
                            <td style="font-size:12px; font-family:Arial; font-weight:bold" align="left">
                        <table width="100%" >
<%--                            <tbody>
                                <th>Registration ID</th>
                                <th>Godown Owner Name</th>
                                <th>Email ID</th>
                                <th>Contact No </th>
                                <th>Registered Capacity</th>
                                <th>Registration Fee</th>
                            </tbody>--%>
                            <tr style="height:15px;">
                            <td style="width:130px;">
                            &nbsp;Registration ID :
                            </td>
                                    <td>
                                     <asp:Label ID="lblRegID" runat="server" ></asp:Label>
                                    </td>
                            <td style="width:150px;">
                            Godown Owner Name :
                            </td>                                    
                                    <td>
                                     <asp:Label ID="lblOwn" runat="server" ></asp:Label>
                                    </td>
                            </tr>  
                            <tr style="Height:15px;">
                            <td> &nbsp;Email ID :
                            </td>
                                    <td>
                                    <asp:Label ID="lblemailid" runat="server" ></asp:Label>
                                    </td> 
                                    <td>
                                    Contact No :</td>                                   
                                    <td>
                                     <asp:Label ID="lblcontact" runat="server" ></asp:Label>
                                    </td>
                             
                             </tr> 
                             <tr style="Height:20px;">
                             <td>
                             &nbsp;Registered Capacity :</td>
                                    <td>
                                     <asp:Label ID="lblRegCapacity" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                    Registration Fee :</td> 
                                    <td>
                                     <asp:Label ID="lblRegFee" runat="server" ></asp:Label>
                                    </td>                                                                                                                                               
                           </tr>
                          </table>                            
                            </td>
                         </tr>                       
                         <tr>
                            <td style="color:Red"  align="left">&nbsp;Note :</td>
                         </tr> 
                         <tr>
                            <td align="left">
                            <p color:#008080;" style="color:Red">
                            &nbsp;1) ऑनलाइन भुगतान करते समय उपरोक्त दर्शित सभी जानकारियां सही प्रविष्टि करे । </p>
                            </td>
                        </tr>
                         <tr>
                            <td align="left">
                            <p color:#008080;" style="color:Red">
                            &nbsp;2) देय पंजीकरण शुल्क 40 पैसा/मेट्रिक टन | </p>
                            </td>
                        </tr> 
                         <tr>
                            <td align="left">
                            <p color:#008080;" style="color:Red">
                            &nbsp;3) रजिस्ट्रेशन फीस का पुस्टिकरण कार्यालीन दिवस के 24 घंटे में किया जाएगा | </p>
                            </td>
                        </tr>
                                                
                        <tr>
                        <td style="height:10px;">
                        
                        </td>
                        </tr>                                              
                        <tr>
                            <td align="center">
                                       <asp:Button class="button button2" Width="150px" Height="30px" ID="Button2" 
                                        runat="server" Text="Proceed To Payment" align="Center" onclick="Button2_Click" />
                                                                            
                            </td>                     
                        </tr>                     
                    </table>
    </div>                        
</asp:Panel>                      

<asp:Label ID="Label9" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlconfrmreg" TargetControlID="Label9"
    BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlconfrmreg" runat="server" CssClass="modalPopup" Height="150px" Width="420px" Style="display: none">
    <div class="header">
        <table style="width:100%;">
            <tr>
                   <td style="color:White; font-weight:bold" align="center">Aleart Message</td>
            </tr> 
                                         
        </table> 
    </div>
    <div class="body">
                    <table cellspacing="1" cellpadding="3" style="width:100%;">
                         <tr>
                        <td style="height:20px">
                        </td>
                        </tr>
                         <tr>
                        <td align="center" style="font-size:14px; font-weight:bold; height:30px ">आपका रजिस्ट्रेशन हो चुका है कृपया चेक करे...
                        </td>
                        </tr>
                        <tr>
                            <td align="center">
                                       <asp:Button class="button button2" Width="80px" Height="25px" ID="btncloseconfrm" 
                                        runat="server" Text="Ok" align="Center" onclick="btncloseconfrm_Click" />
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
                                            <b>© 2020 &nbsp;National Informatics Centre.All Rights Reserved
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
