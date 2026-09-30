<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OtherLoginRegistration.aspx.cs" Inherits="JointVentureScheme_OtherLoginRegistration" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Other Warehouse Registration</title>
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
</head>
<body>

<div id="bg">
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
                    <table>
                        <tr >
                            
                            <td  colspan="4" style="background-color: #66CCFF"> 
                                <p style="font-size: medium; color: #008080;"><asp:LinkButton ID="link1" 
                                        Text="Home" runat="server" 
                                        onclick="link1_Click"></asp:LinkButton>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Log out</asp:LinkButton></p>
                            </td>
                        </tr>
  <%--                       <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                          Warehouse Owner Details </p>
                      </td>

                  </tr>
                  <tr>
                    <td>
                        Authorised Person:
                    </td>
                    <td>
                        <asp:Label ID="lblAuthPerson" runat="server"></asp:Label>
                    </td>
                          <td>
                              Registered Email ID:
                    </td>
                    <td>
                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                   
                          <td class="style2">
                              Mobile Number:
                    </td>
                    <td class="style2">
                        <asp:Label ID="lblMob" runat="server"></asp:Label>
                    </td>
                     <td>
                        
 Type of Applicant/Entity  :
                    </td>
                    <td>
                        <asp:Label ID="lblAppType" runat="server"></asp:Label>
                    </td>
                    </tr>
                    <tr >
                   
                          <td>
                              District :
                    </td>
                    <td>
                        <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                    </td>
                    </tr>
                    --%>
                  <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                          Warehouse Registration Details<span style="color: #FF0000; font-size:x-large;">*</span></p>
                            
                      </td>

                  </tr>
                  <tr>
                    <td>
                        Warehouse Name :
                    </td>
                    <td>
                        <input id="txtWarehouseName" name="rname" runat="server" class="text" type="text"/>
                    </td>
                          <td>
                              Warehouse/Office Address with Postal Address:
                    </td>
                    <td>
                        
                        <textarea id="txtWareAddress" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                    </tr>
                    <tr>
                    <td>
                        Office Contact No./Mobile No:
                    </td>
                    <td>
                        <%--<input id="txtWareContactNo" name="rname" runat="server" class="text" type="text"/>--%>
                        <asp:TextBox ID="txtWareContactNo" runat="server" class="text" type="text" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtWareContactNo"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                        
                    </td>
                          <td>
                              Landmark Near Warehouse:
                    </td>
                    <td>
                        <input id="txtLandmark" name="rname" runat="server" class="text" type="text" />
                    </td>
                    </tr>
                    <%--<tr>
                    <td>
                       PAN Number:
                    </td>
                    <td>
                        <input id="txtPANNo" name="rname" runat="server" class="text" type="text"/>
                    </td>
                          <td>
                             Aadhar No.:
                    </td>
                    <td>
                        <input id="txtAadhar" name="rname" runat="server" class="text" type="text"/>
                    </td>
                    </tr>--%>
                    <tr>
                   
                          <td>
                             District :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWarDistrict" runat="server" AutoPostBack="true" 
                            Width="230px" Height="25px" 
                            onselectedindexchanged="ddlWarDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                       <td>
                      Tehsil :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="false" Width="230px" Height="25px" Enabled="true"
                                            >
                        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                 
                          <td>
                             Nearest Branch of MPWLC :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="false" Enabled="true"
                            Width="230px" Height="25px">
                        </asp:DropDownList>
                    </td>
                      <td>
                      Distance from nearest branch of MPWLC (in KM):
                    </td>
                    <td>
                       <%--<input id="txtDistance" name="rname" runat="server" class="text" type="text"/>--%>
                           <asp:TextBox ID="txtDistance" runat="server" class="text" type="text" style="width:90px" ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtDistance"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                    </tr>
                    
                    <tr>
                 
                          <td>
                             Email ID :
                    </td>
                    <td>
                            <asp:TextBox ID="txtEmailID" runat="server" class="text" type="text"  ></asp:TextBox>
                    </td>
                      <td>
                       <asp:Label ID="lblWType" runat="server" Text="Warehouse Type :" Visible="false"></asp:Label>
                    </td>
                    <td>
                       <%--<input id="txtDistance" name="rname" runat="server" class="text" type="text"/>--%>
                        <asp:DropDownList ID="ddlWType" runat="server" AutoPostBack="false" Enabled="true"  Visible="false"
                            Width="230px" Height="25px">
                            <asp:ListItem Value="1">Owned</asp:ListItem>                          
                            <asp:ListItem Value="2">PEG-HLC</asp:ListItem>
                            <asp:ListItem Value="3">PEG-FCI</asp:ListItem>
                            <asp:ListItem Value="4">Requisition By Collector</asp:ListItem>
                            <asp:ListItem Value="5">Private</asp:ListItem>
                            
                        </asp:DropDownList>
                    </td>
                    </tr>
                    
                    <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                         License Detail<span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>

                  </tr>
                    <tr>
                    <td>
                       Does it has Present Validity?(State Licence):
                    </td>
                    <td>
                        Yes <asp:RadioButton ID="rdoYes" runat="server" GroupName="StateLicence" 
                            Width="70px" Checked="true" oncheckedchanged="rdoYes_CheckedChanged" AutoPostBack="true"/>
                         NO <asp:RadioButton ID="rdoNo" runat="server" GroupName="StateLicence" 
                             oncheckedchanged="rdoNo_CheckedChanged" AutoPostBack="true"/>
                        
                    </td>
                    </tr>
                    <tr id="TrStateL" runat="server">
                    <td>
                    Warehouse License No.:
                    </td>
                    <td>
                        <input id="txtWLicNo" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             Valid Upto Date (dd/mm/yyyy):
                    </td>
                    <td>
                        <asp:TextBox ID="txtSLDate" runat="server" CssClass="text"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtSLDate"></cc1:CalendarExtender>
                    </td>
                    </tr>
                      <tr>
                    <td>
                       Does it has Present Validity?(WDRA Licence) :
                    </td>
                    <td>
                        Yes <asp:RadioButton ID="rdoYesW" runat="server" GroupName="WDRALicence" 
                            Width="70px" Checked="true" 
                            oncheckedchanged="rdoYesW_CheckedChanged" AutoPostBack="true"/>
                         NO <asp:RadioButton ID="rdoNoW" runat="server" GroupName="WDRALicence" Checked="true"
                             oncheckedchanged="rdoNoW_CheckedChanged" AutoPostBack="true"/>
                    </td>
                    </tr>
                    <tr id="TWDRAL" runat="server">
                    <td>
                    WDRA registration No at present :
                    </td>
                    <td>
                        <input id="txtWDRALicenceNo" name="rname" runat="server" class="text" type="text" />
                    </td>
                          <td>
                             Valid Upto Date(dd/mm/yyyy) :
                    </td>
                    <td>
                        <asp:TextBox ID="txtWDRALDate" runat="server" CssClass="text"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender3" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtWDRALDate"></cc1:CalendarExtender>
                    </td>
                    </tr>
                 
                  <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                          Warehouse Incharge/Manager Details <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>

                  </tr>
                  <tr>
                    <td>
                        Incharge/Manager Name :
                    </td>
                    <td>
                        <input id="txtIncharge" name="rname" runat="server" class="text" type="text"/>
                    </td>
                          <td>
                              Incharge/Manager Address with Postal Address:
                    </td>
                    <td>
                        
                        <textarea id="txtInchAdd" maxlength="150" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:223px;"
                        ></textarea>
                    </td>
                    </tr>
                    <tr>
                    <td>
                        Designation :
                    </td>
                    <td>
                        <input id="txtDesign" name="rname" runat="server" class="text" type="text"/>
                    </td>
                          <td>
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
                    <td>
                        Email ID :
                    </td>
                    <td>
                        <input id="txtInchargeEmail" name="rname" runat="server" class="text" type="text" />
                    </td>
                        <%--  <td>
                   लिंग:
                   </td>--%>
                 <%--  <td>
                      पुरुष: <asp:RadioButton ID="RadioButton3" runat="server" GroupName="gender" Width="70px" Checked="true"/>
                      स्त्री: <asp:RadioButton ID="RadioButton4" runat="server" GroupName="gender" Width="70px" />
                   </td>--%>
                    </tr>
                  <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                          Godown Description *  Height should be between 4 ft to 18 ft. <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>

                  </tr>
                   <tr>
                      <td colspan="4" style="background-color:none; font-size:small;">                                         
                          <p style="color:Red;">रजिस्ट्रेशन मे शामिल किए जाने वाले समस्त गोदाम एक ही परिसर मे स्थित होने चाहिए, अन्य परिसर अथवा अन्य स्थान मे स्थित गोदामो के लिए प्रथक रजिस्ट्रेशन करे।</p>
                      </td>

                  </tr>
                 <tr>
                    
                    <td colspan="4" id="GVGodowns" runat="server" visible="true" align="center">
                        <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="true" Width="20%"
                         EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" 
                            onrowdeleting="gvGodown_RowDeleting" AutoGenerateDeleteButton="false">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="Godown No." />
                             
                                <asp:TemplateField HeaderText="Length in Feet">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLenght" runat="server" Width="50px" Text='<%# Eval("Lenght") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Width in Feet">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWidth" runat="server" Width="50px" Text='<%# Eval("Width") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Height in Feet">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtHeight" runat="server" Width="50px" Text='<%# Eval("Height") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select">
                                   <ItemTemplate>
                                     <asp:CheckBox ID="ckstack" runat="server" AutoPostBack="True" OnCheckedChanged="ckstack_CheckedChanged" />
                                    </ItemTemplate>
                                   </asp:TemplateField>
                                <asp:TemplateField HeaderText="Capacity (MT)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCapacity" runat="server" Enabled="false" Width="100px" Text='<%# Eval("Capacity") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Year">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtConstY" runat="server" Width="80px" Text='<%# Eval("ConstY") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Number of Gates in Godown">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGVGate" runat="server" Width="50px" Text='<%# Eval("GVGate") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Estimated Unloading Capacity in a day of Godown (MT)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGVUnloadCpt" runat="server" Width="100px" Text='<%# Eval("GVUnloadCpt") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="Storage Type">
                                    <ItemTemplate>
                                    <%--    <asp:TextBox ID="txtGVStorageType" runat="server" Width="100px" Text='<%# Eval("GVStorageType") %>'></asp:TextBox>--%>
                                   
                                   <asp:DropDownList ID="txtGVddlStorageType" runat="server" Text='<%# Eval("GVStorageType") %>'
                                                 Width="100" Height="27px">
                                                 <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                 <asp:ListItem Value="1" Text="Covered"></asp:ListItem>
                                                 <asp:ListItem Value="2" Text="Permanent (CAP)"></asp:ListItem>
                                                 <asp:ListItem Value="3" Text="Temporary (CAP)"></asp:ListItem>
                                                 <asp:ListItem Value="4" Text="STEEL SILO"></asp:ListItem>
                                                 <asp:ListItem Value="5" Text="SILO BAG"></asp:ListItem> 
                                     </asp:DropDownList>
                                   </ItemTemplate>
                                </asp:TemplateField>                                                                                          
                           
                         
                                <asp:TemplateField HeaderText="">
                                    
                                                                
             <FooterStyle HorizontalAlign="Right" />
            <FooterTemplate>
             <asp:Button ID="ButtonAdd" runat="server" Text="Add Godown" Width="100px" 
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
                         <asp:Label ID="Label6" runat="server" Text="Total Registration Fee:" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label> <asp:Label ID="lblTotalRegAmt" runat="server" Text="0.00" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
                    </td>
                   
                  </tr>
                         <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; ">
                       
                          Bank Account Detail: <span style="color: #FF0000; font-size:x-large;"></span></p>
                      </td>

                  </tr>
                  <tr>
                      <td colspan="4" style="background-color:none; font-size:small;">                                         
                          <p></p>
                      </td>

                  </tr>
                          <tr>
                                        <td>
                                            बैंक का नाम

                                        </td>
                                        <td>
                                         
                        <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="true" Width="190px" onselectedindexchanged="ddlBank_SelectedIndexChanged"
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
                                            
                                            <asp:Label runat="server" ID="lblOBank" Text="बैंक शाखा" Visible="true"></asp:Label>

                                        </td>
                                        <td>
                                         
 
                        <asp:DropDownList ID="ddlBBranch" runat="server" AutoPostBack="true" Width="190px" onselectedindexchanged="ddlBBranch_SelectedIndexChanged"
                                              >
                        </asp:DropDownList>
                   
                    </td>
                    <td >
                        
                    IFSC कोड<span lang="hi"> </span>:
                    
                    </td>
                    
                   
                    <td>
                        <input id="txtIFSC" name="ryear" type="text" class="text" runat="server"/>
                    
                    </td>
                                            </tr>

                      

                           
                                     <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium; color: #008080;">
                          Nearest Distance of Warehouse From <span style="color: #FF0000; font-size:x-large;">*</span> </p>
                      </td>

                  </tr>
                  <tr>
                    <td colspan="3">
                    1. Nearest Distance of Warehouse From National Highway/State Highway (in KM) 
                    </td>
                    <td>
                         <asp:TextBox id="txtHighway" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtHighway"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                          
                    </tr>
                    <tr>
                    <td colspan="3">
                    2. Nearest Distance of Warehouse From Railway Station (in KM)
                    </td>
                    <td>
                        <asp:TextBox id="txtRailway" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtRailway"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                          
                    </tr>
                     <tr>
                    <td colspan="3">
                    3. Nearest Distance of Warehouse From Mandi (in KM)
                    </td>
                    <td>
                         <asp:TextBox id="txtMandi" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtMandi"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                          
                    </tr>
                    <tr>
                    <td colspan="3">
                    4. Nearest Distance of Warehouse From Goods Shed (in KM)
                    </td>
                    <td>
                       <asp:TextBox id="txtGS" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtGS"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                    </tr>
                    <tr>
                    <td colspan="3">
                    5. Nearest Distance of Warehouse From Railway Rack Point (in KM)
                    </td>
                    <td>
                       <asp:TextBox id="txtrackpointDist" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtrackpointDist"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                    </td>
                    </tr>  

                    <tr>
                    <td colspan="3">
                    6. Nearest Railway Rack Point Name From Warehouse :
                    </td>
                    <td>
                       <asp:TextBox id="txtrackpointName" name="ryear" type="text" class="text" runat="server"></asp:TextBox>
                    </td>
                    </tr>
          
                     <tr>
                     
                      <td colspan="4" style="background-color: #66CCFF;" class="style2">
                          <p style="font-size: medium; color: #008080; width: 954px;">
                         &nbsp&nbsp Warehouse Geogrophical Information </p>
                      </td>
                  </tr>
                      
                  <tr>
                    <td>
                       &nbsp&nbsp&nbsp Warehouse Latitude  :
                    </td>
                    <td class="style3" >
                       <%-- <input id="txtlat" name="rname" runat="server" class="text" type="text" style="width:90px"/>--%>
                   <asp:TextBox ID="txtlat" runat="server" class="text" type="text" style="width:90px" ></asp:TextBox>
                    Ex:26.203194
                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtlat"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender></td>
                     <td>
                      &nbsp&nbsp&nbsp  Warehouse Longitude  :
                    </td>
                    <td class="style3">
                       <%-- <input id="txtlong" name="rname" runat="server" class="text" type="text" style="width:90px" />--%>
                        <asp:TextBox ID="txtlong" runat="server" class="text" type="text" style="width:90px"></asp:TextBox>
                    Ex:78.209267
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtlong"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender></td>
                  </tr>
                                     <tr>
                      <td colspan="4" style="background-color: #66CCFF;" class="style2">
                          <p style="font-size: medium; color: #008080; width: 954px;">
                         &nbsp&nbsp Warehouse Additional Facilities Information <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>
                  </tr> 
                      
<tr>
                     <td>
                       Motorable Approach Road Type :
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
                      Shutter/Jali/Chanel Gate in Warehouse :
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
                      Power Supply (In Phase) :
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
                     Water Facility :
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
                       CCTV Camera :
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
                     Guard With Guard Room :
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
                      Availability fo Fire Buckets :
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
                      <td colspan="4" style="background-color: #66CCFF;" class="style2">
                          <p style="font-size: medium; color: #008080; width: 954px;">
                         &nbsp&nbsp Warehouse Hardware Information <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>
                  </tr> 
                                                                                                           
                  <tr>
                    <td>
                     Electronic Weighbridge :
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
                    </tr>
                    <tr>
                                        <td>
                       <asp:Label ID="Label2" runat="server" Text="Weighbridge is Certified By Controler :" Visible="false"></asp:Label></td>
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
                  <tr>
                    <td>
                        Internet Connectivity :
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
                    <td class="style3">
                      <asp:Label ID="Label3" runat="server" Text="Availability of Computer & Required Hardware :" Visible="false"></asp:Label> 
                    </td>
                    <td class="style3">
                        <asp:DropDownList ID="ddlHardwareAvl" runat="server"
                            Width="100px" Height="27px" Visible="false">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>   
                    <td class="style3">
                        <asp:Label ID="Label5" runat="server" Text="Year Of Installation :" Visible="false"></asp:Label> 
                    </td>
                    <td class="style3">
                        <asp:DropDownList ID="ddlInstallationyear" runat="server"
                            Width="100px" Height="27px" Visible="false">
                            <asp:ListItem>--Select--</asp:ListItem>
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
        <%--            </tr>   
                         <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium;">
                           Upload Document :</p>
                      </td>

                  </tr>
                          <tr>
                                        <td>
                                            Applicant Photo

                                        </td>
                                        <td>
                                         <asp:FileUpload ID="fileuploadimage" OnUploadedComplete="FileUploadComplete" 
                                                runat="server" onload="FileUploadComplete"></asp:FileUpload>
                                        
                                 </td>
                                      <td>
                                           Warehouse Plan

                                        </td>
                                        <td>
                                         
                    
                                            <asp:FileUpload ID="fileuploadDoc" runat="server"></asp:FileUpload>
                    </td>   
                                    </tr>--%>
                             <tr>
                      <td colspan="4" style="background-color: #66CCFF">
                          <p style="font-size: medium;">
                          Self Declaration : <span style="color: #FF0000; font-size:x-large;">*</span></p>
                      </td>

                  </tr>
                        <tr> 
                            <td colspan="4">
                                <p>
                                    <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>     All the informations furnished by me are true to the best of my knowledge and belief, further if any discrepancy found in above I will be responsible for that.   
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
                                <p style="color:Red;">
                                   1)गोदाम की ऊंचाई 4 फुट 18 फीट के बीच होना चाहिए(Godown height should be between 4 ft to 18 ft.) 
 <br />
                                </p>

                                <p style="color:Red;">
                                   2) Formula for Capacity = [Length*Breadth*(Height-3)/80] 
 <br />
                                </p>
                                <p style="color:Red;">
                                   All Dimensions for Length,Breadth and height should be in feet 
 <br />
                                </p>
                            </td>

                        </tr>
                                      <tr>
                  
                    <td align="center" colspan="4">
                  
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="submit" OnClick="btnsubmit_Click" ></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
