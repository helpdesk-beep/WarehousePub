<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchRe_InspectionForm.aspx.cs" Inherits="JointVentureScheme_BranchRe_InspectionForm" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Branch Inspection</title>
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
	
<%--<script type="text/javascript">
    function AddRow() {
        //Reference the GridView.
        var gridView = document.getElementById("<%=gvGodown.ClientID %>");

        //Reference the TBODY tag.
        var tbody = gridView.getElementsByTagName("tbody")[0];

        //Reference the first row.
        var row = tbody.getElementsByTagName("tr")[1];

        //Check if row is dummy, if yes then remove.
        if (row.getElementsByTagName("td")[0].innerHTML.replace(/\s/g, '') == "") {
            tbody.removeChild(row);
        }

        //Clone the reference first row.
        row = row.cloneNode(true);
        
        //Add the Country value to second cell.
        var txtCountry = document.getElementById("<%=ProcName.ClientID %>");
        SetValue(row, 0, "ProcName", ProcName);

        var txtCountry = document.getElementById("<%=ProcDist.ClientID %>");
        SetValue(row, 1, "ProcDist", TextBox1);

        var txtCountry = document.getElementById("<%=District.ClientID %>");
        SetValue(row, 2, "District", DropDownList1);

        var txtCountry = document.getElementById("<%=Dist_Id.ClientID %>");
        SetValue(row, 3, "Dist_Id", DropDownList1);

        //Add the row to the GridView.
        tbody.appendChild(row);
        return false;
    };

    function SetValue(ProcName, ProcDist, District, Dist_Id) {
    
//        //Reference the Cell and set the value.
//        row.cells[index].innerHTML = textbox.value;

//        //Create and add a Hidden Field to send value to server.
//        var input = document.createElement("input");
//        input.type = "hidden";
//        input.name = name;
//        input.value = textbox.value;
//        row.cells[index].appendChild(input);

        //Clear the TextBox.
        textbox.value = "";
    }
</script>	--%>

 <%--  <script language="javascript" type="text/javascript">
       $(function() {
           var _URL = window.URL;

           $("#UploadWDRALic").change(function(e) {

           var fileUpload = document.getElementById("UploadWDRALic");
               if (typeof (fileuploadimage.files) != "undefined") {
                   var size = parseFloat(fileuploadimage.files[0].size / 1024).toFixed(2);
                   if (size > 100) {
                       alert("WDRA लायसेंस का साइज़ 400 KB से कम होना चाहिए!");
                       document.getElementById("UploadWDRALic").value = '';
                   }
               } else {
                   alert("This browser does not support HTML5.");
               }
           });
       });

       $(function() {
           var _URL = window.URL;

           $("#UploadWarehouseLicense").change(function(e) {

           var fileUpload = document.getElementById("UploadWarehouseLicense");
               if (typeof (fileuploadDoc.files) != "undefined") {
                   var size = parseFloat(fileuploadDoc.files[0].size / 1024).toFixed(2);
                   if (size > 400) {
                       alert("वेअरहाउस लायसेंस का साइज़ 400 KB से कम होना चाहिए!");
                       document.getElementById("UploadWarehouseLicense").value = '';
                   }
               } else {
                   alert("This browser does not support HTML5.");
               }
           });
       });

       $(function() {
           var _URL = window.URL;

           $("#UploadInsuaredPolicy").change(function(e) {

           var fileUpload = document.getElementById("UploadInsuaredPolicy");
               if (typeof (fileuploadimage.files) != "undefined") {
                   var size = parseFloat(fileuploadimage.files[0].size / 1024).toFixed(2);
                   if (size > 100) {
                       alert("बीमा पॉलिसी का साइज़ 400 KB से कम होना चाहिए!");
                       document.getElementById("UploadInsuaredPolicy").value = '';
                   }
               } else {
                   alert("This browser does not support HTML5.");
               }
           });
       });
       $(function() {
           var _URL = window.URL;

           $("#FilechkboxInsPolicy").change(function(e) {

           var fileUpload = document.getElementById("FilechkboxInsPolicy");
               if (typeof (fileuploadimage.files) != "undefined") {
                   var size = parseFloat(fileuploadimage.files[0].size / 1024).toFixed(2);
                   if (size > 100) {
                       alert("बीमा पॉलिसी का शपथ पत्र का साइज़ 400 KB से कम होना चाहिए!");
                       document.getElementById("FilechkboxInsPolicy").value = '';
                   }
               } else {
                   alert("This browser does not support HTML5.");
               }
           });
       });
       $(function() {
           var _URL = window.URL;

           $("#UploadWDRALic").change(function(e) {

               var fileUpload = document.getElementById("UploadWDRALic");
               if (typeof (fileuploadimage.files) != "undefined") {
                   var size = parseFloat(fileuploadimage.files[0].size / 1024).toFixed(2);
                   if (size > 100) {
                       alert("WDRA लायसेंस का साइज़ 400 KB से कम होना चाहिए!");
                       document.getElementById("UploadWDRALic").value = '';
                   }
               } else {
                   alert("This browser does not support HTML5.");
               }
           });
       });
</script>  
 --%>
 
   
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
</head>
<body>

<div id="bg">
		<div class="wrap">
			<img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
       
        <div>
                            <form id="form1" runat="server">
                              <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager> 
                                <center>
                                
          <div>

          </div>                                
                    <table>
                        <tr >
                            <td  colspan="4" style="background-color: #66CCFF"> 
                                <p style="font-size: medium; color: #008080;">&nbsp&nbsp&nbsp<asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/JointVentureScheme/Logins.aspx">Log out</asp:LinkButton></p>
                            </td>
                        </tr>
                         <tr>
                        <td colspan="4" align="center">
                        <h3>Godown Re-Inspection</h3>
                                 </td>
                        <tr>
                        <td colspan="4" align="center">
                                  <table>
                                  <tr>
                                  <td> <p style=" color:Red;">महत्वपूर्ण नोट :- कृपया समस्त जानकारी अँग्रेजी भाषा मे टाइप करे।</p>
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
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                          A)	Inspection Team  / Office Details  </p>
                      </td>

                  </tr>
                                       <tr>
                    <td class="style4"></td>
                    </tr>
                  <tr>
                    <td style="height:20px;width:225px">
                        क्षेत्रीय कार्यालय :
                    </td>
                    <td>
                    <asp:TextBox ID="txtRegion" runat="server" class="text" type="text" ReadOnly="true" ></asp:TextBox>

                    </td >
                          <td>
                             जिले का नाम :
                    </td>
                    <td >
                     <asp:TextBox ID="txtDistrict" runat="server" class="text" type="text" ReadOnly="true"  ></asp:TextBox>


                    </td>
                    </tr>
                    <tr>
                    <td style="height:20px">
                        शाखा का नाम :
                    </td>
                    <td class="style2">
                     <asp:TextBox ID="txtBranch" runat="server" class="text" type="text" ReadOnly="true" ></asp:TextBox>

                    </td>
                          <td class="style2">
                             निरीक्षण दिनांक :
                    </td>
                    <td class="style2">
                    <asp:TextBox ID="txtInspDate" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtInspDate"></cc1:CalendarExtender>
                    </td>
                    </tr>
                    <tr>
                    <td style="height:20px">
                           जिला प्रबंधक MPSCSC के प्रतिनिधि का नाम  :
                    </td>
                    <td>
                    <asp:TextBox ID="txtInspNameMPS" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td>
                          <td>
                              जिला प्रबंधक MPSCSC के प्रतिनिधि का पद :
                    </td>
                    <td>
                                         <asp:TextBox ID="txtDesMPS" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td>
                    </tr>
                        <tr>
                    <td style="height:20px">
                       MPWLC के जिला स्तरीय नोडल अधिकारी अथवा संबंधित शाखा के शाखा प्रबंधक -(संयोजक) नाम :
                    </td>
                    <td>
                    <asp:TextBox ID="txtNameMPW" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                          <td>
                              MPWLC के जिला स्तरीय नोडल अधिकारी अथवा संबंधित शाखा के शाखा प्रबंधक -(संयोजक) पद:
                    </td>
                    <td >
                     <asp:TextBox ID="txtDesMPW" runat="server" class="text" type="text"  ></asp:TextBox>
                    </td>
                    </tr>
                        <tr>
                    <td style="height:20px">
                        जिला प्रबंधक Markfed के प्रतिनिधि का नाम :
                    </td>
                    <td>
                    <asp:TextBox ID="txtNameMark" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                          <td>
                              जिला प्रबंधक Markfed के प्रतिनिधि का पद :
                    </td>
                    <td >
                     <asp:TextBox ID="txtDesMark" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
                    </tr>
                        <tr>
                    <td style="height:20px">
                        जिला प्रबंधक (DSO/ DFC) के प्रतिनिधि का नाम :
                    </td>
                    <td>
                    <asp:TextBox ID="txtNameDSO" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                          <td>
                             जिला प्रबंधक (DSO/ DFC) के प्रतिनिधि का पद:
                    </td>
                    <td >
                     <asp:TextBox ID="txtDesDSO" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
                    </tr>
                                         <tr>
                    <td></td>
                    </tr>
                        </table>
                                    <table>
                   <tr>
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         B)	Inspected Warehouse Campus Details   </p>
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
                            Width="100" Height="27px" onselectedindexchanged="DDLDistrict_SelectedIndexChanged" 
                            >
                        </asp:DropDownList>
                    </td >
                    
                          <td>
                            तहसील का नाम :
                    </td>
                    <td >
                     <asp:DropDownList ID="DDLTehsil" runat="server" Enabled="false"
                            Width="100" Height="27px">
                     </asp:DropDownList>

                    </td>
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
                             वेअरहाउस की निकटतम शाखा का नाम  :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlNearBranch" runat="server" Enabled="false"
                            Width="100" Height="27px">
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
                    
                                                          </tr>
                     <tr>
                    <td></td>
                    </tr> 
                                        </table>
                                    <table>
                   <tr>
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         C)	Warehouse Owner Details     </p>
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
                        Mobile :
                    </td>
                    <td>
                    <asp:TextBox ID="txtOwnerMob" runat="server" class="text" type="text" ReadOnly="true"></asp:TextBox>
                    </td >
                          <td>
                             Email :
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
                                        </table>
                                    <table>
                   <tr>
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         D)	Inspected Warehouse / Godown  Details       </p>
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
                    <asp:TextBox ID="txtOnlineOffer" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                          <td>
                             ऑनलाइन ऑफर अनुसार :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlJVCategory" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="55">55</asp:ListItem>
                            <asp:ListItem Value="60">60</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td>
                    </tr>
                                         <tr>
                    <td style="height:20px">
                        ऑफर की गयी भंडारण क्षमता का प्रकार - (आंशिक/पूर्ण ) :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlCptOffer" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="P">Partial</asp:ListItem>
                            <asp:ListItem Value="F">Full</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                             JVS में ऑफर की गयी क्षमता :
                    </td>
                    <td >
                     <asp:TextBox ID="txtCptJV" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
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
                        गोदाम में संग्रहित स्कंध जमाकर्ता का नाम (शासकीय) :
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
                    <asp:TextBox ID="txtVacantCpt" runat="server" class="text" type="text" ></asp:TextBox>

                    </td >
                          <td>
                            क्या गोदाम की रिक्त क्षमता offer की गयी क्षमता से कम है?  :
                    </td>
                    <td >
                    <asp:DropDownList ID="ddlVacantCptLess" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>
                    </td>
                    </tr>           
                                        <tr>
                    <td style="height:20px">
                        क्या मौजूदा रिक्त क्षमता (Vacant Capacity)1800(मे.टन)से कम है, और संग्रहीत स्कंध का  जमाकर्ता सरकारी एजेंसी नहीं  है? :
                    </td>
                    <td>
                   <asp:DropDownList ID="ddlVanactCptUnused" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                            क्या मौजूदा रिक्त क्षमता (Vacant Capacity)500(मे.टन) से कम है, और संग्रहीत स्कंध का  जमाकर्ता सरकारी एजेंसी है? :
                    </td>
                    <td >
                    <asp:DropDownList ID="ddlGovtDepositor" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>                      
                                        <tr>
                    <td style="height:20px">
                      गोदाम में गेट्स की संख्या :
                    </td>
                    <td>
                    <asp:TextBox ID="txtGateNo" runat="server" class="text" type="text"  ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtGateNo"
                                        ValidChars="0123456789.">
                         </cc1:FilteredTextBoxExtender>

                    </td >
                          <td>
                            गोदाम में स्टैक्स की संख्या की स्वीकार्य सीमा  :
                    </td>
                    <td >
                     <asp:TextBox ID="txtGodStack" runat="server" class="text" type="text"  ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtGodStack"
                                        ValidChars="0123456789.">
                         </cc1:FilteredTextBoxExtender>

                    </td>
                    </tr>                      
                                        <tr>
                    <td style="height:20px">
                        अग्नि हाइड्रंट्स  के साथ आग बुझाने की पर्याप्त उपलब्धता :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlFireExting" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                            गोदाम में फायर बकेट की पर्याप्त उपलब्धता  :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlFireBucket" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>                      
                     <tr>
                    <td></td>
                    </tr> 
                                        </table>
                                                  <table>
                   <tr>
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         E)	Warehouse / Godown  Bank Account Details ( Loan account)     </p>
                      </td>
                  </tr>
                     <tr>
                    <td></td>
                    </tr> 
                 <tr>
                    <td style="height:20px;width:225px">
                        बैंक खाता संख्या :
                    </td>
                    <td>
                    <asp:TextBox ID="txtBankNo" runat="server" class="text" type="text"  ></asp:TextBox>
                    

                    </td >
                          <td>
                             आई०ऍफ़०एस० कोड  :
                    </td>
                    <td >
                     <asp:TextBox ID="txtIFSC" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
                    </tr>
                                         <tr>
                    <td style="height:20px">
                        बैंक और शाखा का नाम  :
                    </td>
                    <td>
                    <asp:TextBox ID="txtBankName" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                          <td>
                             क्या बैंक विवरण सही हैं (पास बुक विवरण के साथ जांच करें) :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlBankDetail" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>
                                       
                                               
                     <tr>
                    <td></td>
                    </tr> 
                                        </table>
                                    <table>
                   <tr>
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         F)	License details of Warehouse / Godown    </p>
                      </td>
                  </tr>
                     <tr>
                    <td></td>
                    </tr> 
                 <tr>
                    <td style="height:20px;width:225px">
                       
क्या इसकी वर्तमान वैधता है?(WDRA) :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlWDRALic" runat="server"
                            Width="100" Height="27px" AutoPostBack="true" 
                            onselectedindexchanged="ddlWDRALic_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                             <asp:Label ID="lblWDRALicNo" runat="server" Text="WDRA लायसेंस नंबर  :"></asp:Label>
                    </td>
                    <td >
                     <asp:TextBox ID="txtWDRALicNo" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
                    </tr>
                                         <tr id="WDRA3" runat="server">
                    <td style="height:20px">
                        
                             <asp:Label ID="lblWDRAstartDate" runat="server" Text="जारी दिनांक :"></asp:Label>
                    </td>
                    <td>
                    <asp:TextBox ID="txtWDRAstartDate" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtWDRAstartDate"></cc1:CalendarExtender>
                    </td >
                          <td>
                             
                             <asp:Label ID="lblWDRAValidDate" runat="server" Text="वैधता दिनांक :"></asp:Label>

                    </td>
                    <td >
                     <asp:TextBox ID="txtWDRAValidDate" runat="server" class="text" type="text"  onkeydown="javascript:preventInput(event);" onpaste="return false;"></asp:TextBox>

                     <cc1:CalendarExtender ID="CalendarExtender3" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtWDRAValidDate"></cc1:CalendarExtender>
                    </td>
                    </tr>
                                        <tr id="WDRA2" runat="server">
                    <%--<td style="height:20px">
                        
                        <asp:Label ID="lvlWDRAAtechFile" runat="server" Text="छायाप्रति संलग्न :"></asp:Label>
                    </td>
                    <td>
                    <asp:FileUpload ID="UploadWDRALic" runat="server" ></asp:FileUpload>

                    </td >--%>
                          <td> <asp:Label ID="lblWDRAAgenct" runat="server" Text="WDRA अधिकृत एक्रीडेशन एजेंसी का नाम   :"></asp:Label>
                             
                    </td>
                    <td >
                     <asp:TextBox ID="txtWDRALicAuthority" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
                    </tr>
                                               <tr id="WDRA1" runat="server">
                    <td style="height:20px">
                        जारी प्रमाण पत्र का क्रमांक :
                    </td>
                    <td>
                    <asp:TextBox ID="txtWDRAAgencyCert" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                          <td>
                             जारी दिनांक   :
                    </td>
                    <td >
                     <asp:TextBox ID="txtWDRADateOfCert" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>

                     <cc1:CalendarExtender ID="CalendarExtender4" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtWDRADateOfCert"></cc1:CalendarExtender>

                    </td>
                    </tr>
                                                            <tr>
                    <td style="height:20px">
                       
क्या इसकी वर्तमान वैधता है?( Warehouse) :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlstateLic" runat="server"
                            Width="100" Height="27px" AutoPostBack="true" 
                            onselectedindexchanged="ddlstateLic_SelectedIndexChanged" >
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >

                    </tr>
                                                      <tr id="trLic2" runat="server"> 
                                                                                <td>
                             
                              <asp:Label ID="lblLicenseNo" runat="server" Text="वेअरहाउस लायसेंस नंबर :"></asp:Label>
                    </td>
                    <td >
                     <asp:TextBox ID="txtLicenseNo" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
                    <td style="height:20px">
                                                     <asp:Label ID="lblDateOfIssuance" runat="server" Text="जारी दिनांक :"></asp:Label>

                    </td>
                    <td>
                    <asp:TextBox ID="txtDateOfIssuance" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender5" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtDateOfIssuance"></cc1:CalendarExtender>
                    </td >
                          
                    </tr>
                                                      <tr id="trLic3" runat="server">
                                                      <td>
                                                     <asp:Label ID="lblExpDate" runat="server" Text="वैधता दिनांक :"></asp:Label>
                             
                    </td>
                    <td >
                     <asp:TextBox ID="txtlicExpdate" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender6" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtlicExpdate"></cc1:CalendarExtender>

                    </td>
<%--                    <td style="height:20px">
                    <asp:Label ID="lblStateLicAtchFile" runat="server" Text="छायाप्रति संलग्न :"></asp:Label>
                    </td>
                    <td>
                    <asp:FileUpload ID="UploadWarehouseLicense" runat="server"></asp:FileUpload>

                    </td >--%>
                    </tr>
                    <tr><td>
                              यदि WDRA लायसेंस या वेअरहाउस लायसेंस वैध नहीं है तो क्या इसे नवीनीकरण हेतु आवेदन किया गया है?  
</td>
                    <td >
                     <asp:DropDownList ID="ddlLicNotPre" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlLicNotPre_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
</tr>
<tr id="trLicNotePre" runat="server" visible="false">
<td>
   <asp:Label ID="Label2" runat="server" Text="आवेदन क्रमांक" ></asp:Label></td><td>
   <asp:TextBox ID="txtLicNotPreset" runat="server" class="text" type="text"  ></asp:TextBox></td>
 <td><asp:Label ID="Label4" runat="server" Text="आवेदन दिनांक" ></asp:Label>
</td>
<td><asp:TextBox ID="txtLicNotPresetDate" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender12" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtLicNotPresetDate"></cc1:CalendarExtender>
</td>
</tr>                                                          
                                                          
                     <tr>
                    <td></td>
                    </tr> 
                                        </table>
                                    <table>
                   <tr>
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         G)	Warehouse / Godown  Insurance Details  </p>
                          <p style="font-size: medium; color: #008080;">
                         G.1) Insurance of  Warehouse Building    </p>
                      </td>
                  </tr>
                     <tr>
                    <td></td>
                    </tr> 
                 <tr>
                    <td style="height:20px">
                        बीमा पॉलिसी जारी करने वाली एजेंसी का नाम  :
                    </td>
                    <td>
                    <asp:TextBox ID="txtInsuarnceName" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                          <td>
                             गोदाम की बिल्डिंग के बीमा पॉलिसी क्रमांक :
                    </td>
                    <td >
                     <asp:TextBox ID="txtPolicyNo" runat="server" class="text" type="text"  ></asp:TextBox>


                    </td>
                    </tr>
                                         <tr>
                    <td style="height:20px">
                        बीमित राशि रू. :
                    </td>
                    <td>
                    <asp:TextBox ID="txtPolicyAmt" runat="server" class="text" type="text"  ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPolicyAmt"
                                        ValidChars="0123456789.">
                         </cc1:FilteredTextBoxExtender>

                    </td >
                          <td>
                             जारी दिनांक :
                    </td>
                    <td >
                                         <asp:TextBox ID="txtPolicyIssue" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender7" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtPolicyIssue"></cc1:CalendarExtender>


                    </td>
                    </tr>
                                        <tr>
                    <td style="height:20px">
                        वैधता दिनांक :
                    </td>
                    <td>
                    <asp:TextBox ID="txtPolicyExpired" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender8" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtPolicyExpired"></cc1:CalendarExtender>
                    </td >
                          
                    </tr>
                                       
                                               
                     <tr>
                    <td></td>
                    </tr> 
                                        <tr>
<td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         G.2) Insurance of Commodity Stored in Warehouse      </p>
                          
                      </td>
                                            </tr>
                                        <tr>
                    <td style="height:20px">
                        बीमा पॉलिसी जारी करने वाली एजेंसी का नाम  :
                    </td>
                    <td>
                    <asp:TextBox ID="txtComWareInsName" runat="server" class="text" type="text"  ></asp:TextBox>

                    </td >
                          <td>
                             गोदाम की प्रस्तावित क्षमता में भण्डारित होने वाले स्कंध की बीमा पॉलिसी क्रमांक :
                    </td>
                    <td >
                                         <asp:TextBox ID="txtComWarePolicyNo" runat="server" class="text" type="text"  ></asp:TextBox>



                    </td>
                    </tr>
                                        <tr>
                    <td style="height:20px">
                        गोदाम की प्रस्तावित क्षमता में भण्डारित होने वाले स्कंध की बीमित राशि रू. :
                    </td>
                    <td>
                    <asp:TextBox ID="txtSumAssured" runat="server" class="text" type="text"  ></asp:TextBox>
                       <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtSumAssured"
                                        ValidChars="0123456789.">
                         </cc1:FilteredTextBoxExtender>

                    </td >
                          <td>
                             जारी दिनांक  :
                    </td>
                    <td >
                                         <asp:TextBox ID="txtComInsuDateIssue" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender9" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtComInsuDateIssue"></cc1:CalendarExtender>


                    </td>
                    </tr>
                                        <tr>
                    <td style="height:20px">
                        वैधता दिनांक :
                    </td>
                    <td>
                    <asp:TextBox ID="txtComInsuDateExpired" runat="server" class="text" type="text" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender10" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtComInsuDateExpired"></cc1:CalendarExtender>
                    </td >
                          <td>
                            बीमित राशि रू. 20,000 प्रति (मे.टन)(प्रस्तावित क्षमता पर आंकलित) की दर से कम तो नहीं है?  :
                    </td>
                    <td >
                                          <asp:DropDownList ID="ddlSumAdequate" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>



                    </td>
                    </tr>
<%--                                        <tr>
                    <td style="height:20px">
                        छायाप्रति संलग्न :
                    </td>
                    <td>
                    <asp:FileUpload ID="UploadInsuaredPolicy" runat="server"></asp:FileUpload>

                    </td >
                          
                    </tr>--%>
                                                               <tr> <td colspan="4">
                 यदि बीमा पॉलिसी नहीं है तो शपथ पत्र जमा कराए जिसमे उल्लेख हो की अग्रीमेंट के समय तक आवश्यक वैध  बीमा पॉलिसी उपलब्ध करा ली जावेगी   
                       <asp:CheckBox ID="chkboxInsPolicy" runat="server" AutoPostBack="true" 
                                oncheckedchanged="chkboxInsPolicy_CheckedChanged"></asp:CheckBox> <asp:Label ID="lblInsPolicyChkbox" runat="server" Visible="false"></asp:Label>
                       
                       
                       </td>
                       </tr>

                                        </table>
                                    <table>
                   <tr>
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         H)	Facilities at Warehouse Campus</p>
                      </td>
                  </tr>
                     <tr>
                    <td></td>
                    </tr> 
                 <tr>
                    <td style="height:20px">
संयुक्त भागीदारी योजना के अनुबंधित गोदाम क्षमता पर राषि रूपये 20,000 प्रति मे.टन की दर से काम्प्रीहेंसिव बीमा है? </td>
                   <td>
                    <asp:DropDownList ID="ddljvsIns" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>

                    </td >
                          <td>
गोदाम के प्रत्येक शटर के अलावा अतिरिक्त रूप से अन्दर की ओर ’’जालीदार शटर’’ है ? </td>
                    <td >
                     <asp:DropDownList ID="ddlShutterGates" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>
                                         <tr>
                    <td style="height:20px">
                        गोदाम परिसर /संलग्न परिसर  में मानक क्षमता का चालू हालत में प्रमाणित इलेक्ट्रानिक वेब्रिज । :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlElecWeighbridge" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
भण्डारित स्कंध के कीटोपचार हेतु संबंधित गोदाम परिसर पर फ्यूमीगेषन कव्हर (IS 14611.1998 or  BIS मानक - Up to date ammendment )सेण्ड स्नेक्स सहित, मानव संसाधन एवं पावर स्प्रेयर पंप आदि उपलब्ध हैं ?                    </td>
                    <td >
                     <asp:DropDownList ID="ddlFumigation" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>
                                        <tr>
                    <td style="height:20px">
                        प्रत्येक गोदाम परिसर में कम से कम 200 किलो क्षमता तक के प्रमाणित इलेक्ट्रानिक बीम स्केल उपलब्ध कराना होंगे । :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlElecBeamScale" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                             गोदाम परिसर में ISI मार्क के डिजिटल नमी मापक यंत्र उपलब्ध है ? :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlMarkedMoisture" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>
                                         <tr>
                    <td style="height:20px">
गोदाम में भण्डारित स्कंध की सुरक्षा-व्यवस्था हेतु उच्च गुणवत्ता के CCTV कैमरे (Night Vision सुविधा सहित) जिसकी मेमोरी दो माह तक सुरक्षित  हैं ?                     </td>
                    <td>
                    <asp:DropDownList ID="ddlCameras" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                             गार्ड रूम के साथ सुरक्षा गार्ड की 24X7 उपलब्धता है :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlGuard" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr> <tr>
                    <td style="height:20px">
                        मोटर योग्य सड़क का प्रकार :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlMotorableRoadType" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">BT </asp:ListItem>
                            <asp:ListItem Value="2">CC</asp:ListItem>
                            <asp:ListItem Value="3">WBM</asp:ListItem>
                            <asp:ListItem Value="4">other</asp:ListItem>
                            
                        </asp:DropDownList>
                    </td >
                          <td>
                             मोटर योग्य सड़क की चौड़ाई (in Meter)  :
                    </td>
                    <td >
                     <asp:TextBox ID="txtMotorableRoadWidth" runat="server" class="text" type="text" Width="90"  ></asp:TextBox>
                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtMotorableRoadWidth"
                                        ValidChars="0123456789.">
                         </cc1:FilteredTextBoxExtender>

                    </td>
                    </tr> <tr>
                    <td style="height:20px">
                       सभी मौसमों के लिए रोड की गुणवत्ता संतोषजनक है? :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlRoadSatisfactory" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                             गोदाम में चारों ओर सीमा में दीवार है और प्रवेश और निकास द्वार है?   :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlBoundaryWall" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>
                           <tr>
                    <td style="height:20px">
                        गोदाम में उपलब्ध खुली जगह (> 25000Sq. Ft) :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlGodownSpace" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                            गोदाम पिछले साल खरीद केंद्र के रूप में काम किया है?   :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlProcurement" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>           
                                        <tr>
                    <td style="height:20px">
                        यदि हां, तो पिछले साल  कितनी मात्रा की खरीदी गई(in MT) :
                    </td>
                    <td>
                    <asp:TextBox ID="txtPreviouseCpt" runat="server" class="text" type="text" Width="90" ></asp:TextBox>
                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtPreviouseCpt"
                                        ValidChars="0123456789.">
                         </cc1:FilteredTextBoxExtender>

                    </td >
                          <td>
                            पर्याप्त लकड़ी के तख्ते / डनेज और अन्य उपयोग योग्य सामान की उपलब्धता है?   :
                    </td>
                    <td >
                        <asp:DropDownList ID="ddlwoodenplank" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>
                    </td>
                    </tr>           
                                        <tr>
                    <td style="height:20px">
                       पेयजल सुविधा उपलब्ध है? :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlwater" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>
                    </td >
                          <td>
                            गोदाम में स्प्रे और अन्य उपयोग के लिए जल की सुविधा उपलब्ध है? :
                    </td>
                    <td >
                         <asp:DropDownList ID="ddlwaterother" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>
                    </td>
                    </tr>                      
                                        <tr>
                    <td style="height:20px">
                       धूमन और कीट नियंत्रण के लिए पर्याप्त उपकरण :
                    </td>
                    <td>
                      <asp:DropDownList ID="ddlFumiEquip" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>
                    </td >
                          <td>
                            अनुभवी तकनीकी संसाधन (बीएससी और 02 साल का अनुभव ) > 5000MT Godown :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlTechResource" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>                      
                                        <tr>
                    <td style="height:20px">
क्या धूमन और कीट नियंत्रण के लिए सुविधाएं/उपकरण मानकों के अनुसार हैं और गोदाम में उपलब्ध हैं?                    </td>
                    <td>
                    <asp:DropDownList ID="ddlFumiPestEquipments" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                            जब आवश्यक हो तो गोदाम में पर्याप्त श्रमिक(labour) उपलब्ध होगी?  :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlManpower" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>                      
                                        <tr>
                    <td style="height:20px">
                       विद्युत आपूर्ति की उपलब्धता :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlPowerSupply" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">1 Phase  </asp:ListItem>
                            <asp:ListItem Value="3">3 Phase </asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
गोदाम को किसी भी तनाव विद्युत लाइन से गुजरने से मुक्त होना चाहिए और इस तरह की रेखाओं से गुजरने की स्थिति में,भंडारण संरचना की योजना बनाते समय प्रासंगिक विद्युत कोड प्रावधानों को ध्यान में रखा जाना चाहिए। गोदाम गैस / तेल पाइप लाइनों से मुक्त होना चाहिए।                    </td>
                    <td >
                     <asp:DropDownList ID="ddlElectTensionLine" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>
                                        <tr>
                    <td style="height:20px">
                       इलेक्ट्रॉनिक वेब्रिज (तुलाचौकी) :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlElectWeighBridge" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                           इलेक्ट्रॉनिक वेब्रिज की क्षमता (in MT) :
                    </td>
                    <td >
                         <asp:TextBox ID="txtCptElectWeigh" runat="server" class="text" type="text" Width="90"  ></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtCptElectWeigh"
                                        ValidChars="0123456789.">
                         </cc1:FilteredTextBoxExtender>
                    </td>
                    </tr>
                                        <tr>
                    <td style="height:20px">
                       क्या इलेक्ट्रॉनिक वेब्रिज  नियंत्रक ( नाप तौल) द्वारा प्रमाणित हैं? कैलिब्रेशन प्रमाणपत्र जांच करें?  :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCalibration" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td >
                          <td>
                           कैलिब्रेशन प्रमाणपत्र जारी करने की तिथि प्रविष्ट करें? :
                    </td>
                    <td >
                                                             <asp:TextBox ID="txtCalib_cert_date" runat="server" class="text" type="text" Width="90" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                                         <cc1:CalendarExtender ID="CalendarExtender11" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtCalib_cert_date"></cc1:CalendarExtender>
                    </td>
                    </tr>
                                        <tr>
                    <td style="height:20px">
                       यदि इलेक्ट्रॉनिक वेब्रिज परिसर में नहीं है, तो कोई दूसरा प्रमाणित इलेक्ट्रॉनिक वेब्रिज गोदाम से लगभग 500 मीटर की दूरी पर उपलब्ध है?  :
                    </td>
                    <td>
                                                            <asp:DropDownList ID="ddlCampusDistance" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td >
  
                    </tr>     
                    
                    
                    <tr>
                                                                <td>
                           इंटरनेट कनेक्टिविटी :
                    </td>
                    <td >
                    <%--  <asp:TextBox ID="txtInternetCon" runat="server" class="text" type="text"  ></asp:TextBox>--%>
                       <asp:DropDownList ID="ddlInternetCon" runat="server"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlInternetCon_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>
                    </td>
                    <td style="height:20px">
                       कंप्यूटर ऑपरेटर की उपलब्धता :
                    </td>
                    <td>
                                                            <asp:DropDownList ID="ddlComputerOperator" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td >
                          
                    </tr>                 
                                        <tr id="trConType" runat="server" visible="false">
                    <td style="height:20px">
                       
                       <asp:Label ID="lblConType" runat="server" Text="कनेक्टिविटी का प्रकार :" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlConType" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Fixed Broadband Connections</asp:ListItem>
                            <asp:ListItem Value="2">Mobile Internet</asp:ListItem>
                        </asp:DropDownList>                    
                    
                    </td >
                          <td>
                          <asp:Label ID="lblHard" runat="server" Text="कंप्यूटर और आवश्यक हार्डवेयर की उपलब्धता :" ></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="ddlHardwareAvl" runat="server"
                            Width="100" Height="27px">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td>
                    </tr>                      
                                                              
                     <tr>
                    <td></td>
                    </tr> 
                                        </table>
                                    <table>
                   <tr>
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         I)	Important aspect of Warehouse to become UNFIT         </p>
                      </td>
                  </tr>
                     <tr>
                    <td></td>
                    </tr> 
                 <tr>
                    <td style="height:20px">
                        1.	क्या गोदाम निर्माणाधीन है ?  :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlWareConstruct" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlWareConstruct_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                            2.	क्या गोदाम विवादग्रस्त है ?  :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlWarelitigation" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlWarelitigation_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>
                                         <tr>
                    <td style="height:20px">
                        3.	क्या गोदाम क्षतिग्रस्त है ?  :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlWareDamage" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlWareDamage_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td ><td>
          4.	क्या गोदाम में शासकीय स्कंध के अतिरिक्त पूर्व से स्कंध भण्डारित है ? :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlWarePrivateDepositor" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlWarePrivateDepositor_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>


                    </td>
                    </tr>
                   <%-- ------------------------------------%>
                    
                    <tr id="trPvtCpt" runat="server" visible="false">
                    <td style="height:20px">
                       
                    </td>
                    <td>
                    </td >
                          <td>
                             शासकीय स्कंध के अतिरिक्त पुर्व से  भण्डारित स्कंध कि स्थिति मे क्या  गोदाम संचालक द्वारा इस भंडारित स्कंध का उठाव  कर आवस्यक 
                              क्षमता उपलब्ध कराई जाएगी :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlPvtCpt" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlPvtCpt_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                    
                    <%-- -------------------------%>
                                        <tr>
                    <td style="height:20px">
                        5.	क्या गोदाम ’’ब्लेक लिस्टेड’’ है ? :
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlBlackList" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlBlackList_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                             6.	क्या ऑनलाइन ऑफर संबंधी जानकारी गलत दी गयी है ?  :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlWareWrongInfo" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlWareWrongInfo_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
              <tr>
              
             
                    <td style="width:400px">
                        7. क्या अनिवार्य सुविधा में मानक स्तर की डनेज शीट, कम्प्यूटर सिस्टम, अग्निशामक यंत्र और फायर बकेट्स है एवं बारहमासी (All Weather Approach Road) पहुंचमार्ग जो कम से कम WBM स्तर का हो, यह सभी अनिवार्य सुविधा उपलब्ध नहीं कराई गई हैं?
                    </td>
                    <td>
                    <asp:DropDownList ID="ddlFacilities" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlFacilities_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            
                        </asp:DropDownList>

                    </td >
                          <td>
                           8. क्या गोदाम खरीफ सीजन 2017-18 में आॅफर होने के वाबजूद आवश्यकता होने पर गोदाम संचालक द्वारा अनुबंधित नहीं हुआ है। :
                    </td>
                    <td >
                     <asp:DropDownList ID="ddlWareSeasonCpt" runat="server" AutoPostBack="true"
                            Width="100" Height="27px" 
                            onselectedindexchanged="ddlWareSeasonCpt_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                        </asp:DropDownList>


                    </td>
                    </tr>
                                        <tr>
                    <td></td>
                    </tr> 
                                        </table>
                                    <table>
                   <tr>
                      <td style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
            J)	Distances  from Warehouse        </p>
                      </td>
                  </tr>
                  <tr>
                  <td><br /> उपार्जन केन्द्र की भौगोलिक सीमा(अर्थात् उपार्जन केन्द्र से संबंद्ध सहकारी समिति/समितियों की भौगोलिक सीमा)स्थापित खरीदी केन्द्रो का नाम एवं गोदाम से दूरीः-<br /> </td>
                  </tr>
                  <tr>
                  <td></td>
                  </tr>
                  <tr>
                  <td colspan="4" id="ProcGodown" runat="server" visible="true" align="center">
                        <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="true" Width="50%"
                         EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" 
                            AutoGenerateDeleteButton="false" OnRowDataBound="gvGodown_OnRowDataBound">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:BoundField DataField="Tid" HeaderText="S.No"  ItemStyle-Width="30px"/>
                                <asp:TemplateField HeaderText="Procurement Centre Name"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                    <%--    <asp:TextBox ID="txtProcName" runat="server" Width="200px" Text='<%# Eval("ProcName") %>'></asp:TextBox>--%>
                     <asp:DropDownList ID="ddltxtProcName" runat="server"
                            Width="200" Height="27px">
                        </asp:DropDownList>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distance From Warehouse (in KM)"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                     <asp:TextBox ID="txtProcDistance" runat="server" Width="100px" Text='<%# Eval("ProcDist") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                            <asp:DropDownList ID="ddlProcDist" runat="server" Text='<%# Eval("District") %>'
                                                 Width="100" Height="27px">
                                                 <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                 <asp:ListItem Value="2351" Text="Agar Malwa"></asp:ListItem>
                                                 <asp:ListItem Value="2349" Text="Alirajpur"></asp:ListItem>
                                                 <asp:ListItem Value="2348" Text="Anuppur"></asp:ListItem>
                                                 <asp:ListItem Value="2346" Text="AshokNagar"></asp:ListItem>
                                                 <asp:ListItem Value="2338" Text="Balaghat"></asp:ListItem>
                                                 <asp:ListItem Value="2343" Text="Barwani"></asp:ListItem>
                                                 <asp:ListItem Value="2331" Text="Betul"></asp:ListItem>
                                                 <asp:ListItem Value="2301" Text="Bhind"></asp:ListItem>
                                                 <asp:ListItem Value="2328" Text="Bhopal"></asp:ListItem>
                                                 <asp:ListItem Value="2347" Text="Burhanpur"></asp:ListItem>
                                                 <asp:ListItem Value="2323" Text="Chhatarpur"></asp:ListItem>
                                                 <asp:ListItem Value="2336" Text="Chhindwara"></asp:ListItem>
                                                 <asp:ListItem Value="2311" Text="Damoh"></asp:ListItem>
                                                 <asp:ListItem Value="2307" Text="Datia"></asp:ListItem>
                                                 <asp:ListItem Value="2320" Text="Dewas"></asp:ListItem>
                                                 <asp:ListItem Value="2322" Text="Dhar"></asp:ListItem>
                                                 <asp:ListItem Value="2339" Text="Dindori"></asp:ListItem>
                                                 <asp:ListItem Value="2306" Text="Guna"></asp:ListItem>
                                                 <asp:ListItem Value="2304" Text="Gwalior"></asp:ListItem>
                                                 <asp:ListItem Value="2341" Text="Harda"></asp:ListItem>
                                                 <asp:ListItem Value="2326" Text="Hoshangabad"></asp:ListItem>
                                                 <asp:ListItem Value="2308" Text="Indore"></asp:ListItem>
                                                 <asp:ListItem Value="2333" Text="Jabalpur"></asp:ListItem>
                                                 <asp:ListItem Value="2321" Text="Jhabua"></asp:ListItem>
                                                 <asp:ListItem Value="2342" Text="Katni"></asp:ListItem>
                                                 <asp:ListItem Value="2313" Text="Khandwa"></asp:ListItem>
                                                 <asp:ListItem Value="2324" Text="Khargone"></asp:ListItem>
                                                 <asp:ListItem Value="2335" Text="Mandla"></asp:ListItem>
                                                 <asp:ListItem Value="2316" Text="Mandsour"></asp:ListItem>
                                                 <asp:ListItem Value="2302" Text="Morena"></asp:ListItem>
                                                 <asp:ListItem Value="2334" Text="Narsinghpur"></asp:ListItem>
                                                 <asp:ListItem Value="2344" Text="Neemuch"></asp:ListItem>
                                                 <asp:ListItem Value="2309" Text="Panna"></asp:ListItem>
                                                 <asp:ListItem Value="2330" Text="Raisen"></asp:ListItem>
                                                 <asp:ListItem Value="2332" Text="Rajgarh"></asp:ListItem>
                                                 <asp:ListItem Value="2317" Text="Ratlam"></asp:ListItem>
                                                 <asp:ListItem Value="2325" Text="Rewa"></asp:ListItem>
                                                 <asp:ListItem Value="2310" Text="Sagar"></asp:ListItem>
                                                 <asp:ListItem Value="2312" Text="Satna"></asp:ListItem>
                                                 <asp:ListItem Value="2329" Text="Sehore"></asp:ListItem>
                                                 <asp:ListItem Value="2337" Text="Seoni"></asp:ListItem>
                                                 <asp:ListItem Value="2314" Text="Shahdol"></asp:ListItem>
                                                 <asp:ListItem Value="2319" Text="Shajapur"></asp:ListItem>
                                                 <asp:ListItem Value="2305" Text="Shivpuri"></asp:ListItem>
                                                 <asp:ListItem Value="2340" Text="Shyopur"></asp:ListItem>
                                                 <asp:ListItem Value="2315" Text="Sidhi"></asp:ListItem>
                                                 <asp:ListItem Value="2350" Text="Singrauli"></asp:ListItem>
                                                 <asp:ListItem Value="2303" Text="Tikamgarh"></asp:ListItem>
                                                 <asp:ListItem Value="2318" Text="Ujjain"></asp:ListItem>
                                                 <asp:ListItem Value="2345" Text="Umariya"></asp:ListItem>
                                                 <asp:ListItem Value="2327" Text="Vidisha"></asp:ListItem>
                                            </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>  
             <asp:TemplateField HeaderText=""  ItemStyle-Width="100px">                                              
             <FooterStyle HorizontalAlign="Right" />
            <FooterTemplate>
             <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" Width="100px" 
                    onclick="ButtonAdd_Click" />
            </FooterTemplate>
                                </asp:TemplateField>
                                                                                          
                            </Columns>
                           
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
                    </td>
                  </tr>

                                        
                                                                               

                                         </table>
                                    
                                                   <%--<table>
                   <tr>
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         K)	Photographs during Inspection        </p>     </td></tr>

                    <tr>
                    <td style="height:20px">
                        एप्रोच रोड की फोटो :
                    </td>
                    <td>
                                        <asp:FileUpload ID="UploadRoad" runat="server"></asp:FileUpload>


                    </td >
                     <td>
                             गोदाम बिल्डिंग के अगले भाग की फोटो :
                    </td>
                    <td >
                                         <asp:FileUpload ID="UploadFrontSide" runat="server"></asp:FileUpload>



                    </td>
                    </tr>

                      <tr>
                    <td style="height:20px">
                        गोदाम बिल्डिंग के पीछे के भाग की फोटो :
                    </td>
                    <td>
                                        <asp:FileUpload ID="UploadBackSide" runat="server"></asp:FileUpload>


                    </td >
                          <td>
                             गोदाम के अन्दर से छत का फोटो :
                    </td>
                    <td >
                                         <asp:FileUpload ID="UploadFloor" runat="server"></asp:FileUpload>



                    </td>
                    </tr>
                                        <tr>
                    <td style="height:20px">
                        गोदाम के फर्श का फोटो :
                    </td>
                    <td>
                                        <asp:FileUpload ID="UploadRoof" runat="server"></asp:FileUpload>


                    </td >
                          <td>
                             कार्यालय का फोटो :
                    </td>
                    <td >
                                         <asp:FileUpload ID="UploadOffice" runat="server"></asp:FileUpload>



                    </td>
                    </tr>
                                         <tr>
                    <td style="height:20px">
                        सी.सी.टी.व्ही. कैमरा की फोटो : 
                    </td>
                    <td>
                                        <asp:FileUpload ID="uploadCamera" runat="server"></asp:FileUpload>


                    </td >
                          
                    </tr>
                                        <tr>
                    <td></td>
                    </tr> 
                                        </table>--%>
                                     <table>
                   <tr>
                      <td  colspan="4" style="background-color: #66CCFF;width:954px">
                          <p style="font-size: medium; color: #008080;">
                         K)	Declaration of Inspection Team        </p>
                      </td>
                  </tr>
                     <tr>
                    <td></td>
                    
                    </tr>
                    
                    <tr>
                    <td > According To System FIT / UNFIT &nbsp&nbsp<asp:TextBox ID="txtSystemFitunfit"  Width="90px" runat="server" ReadOnly="true" class="text" type="text"  ></asp:TextBox></td>
                    
</tr>
                    <tr>
                    <td style="margin-top:0px"><b>FIT / UNFIT As Recommended by Inspection Team </b>&nbsp&nbsp 
                        <asp:DropDownList ID="ddlfitunfit" runat="server"
                            Width="100" Height="27px" AutoPostBack="true" 
                            onselectedindexchanged="ddlfitunfit_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem Value="FIT">FIT</asp:ListItem>
                            <asp:ListItem Value="UNFIT">UNFIT</asp:ListItem>
                        </asp:DropDownList>
                       <b> Remark For Recommendation :</b>
                        &nbsp&nbsp
                        <textarea id="txtRemark" maxlength="200" runat="server" cols="20" rows="2" name="radr" class="text" style=" width:250px;"
                        ></textarea>
                        </td>
                    </tr>
                    
                    <tr>
                    
                    <td></td>
                    </tr>
                    
                    
                                         <tr>
                    <td> <p> <asp:CheckBox ID="chkDeclaration" runat="server"></asp:CheckBox>  We hereby declare and affirm that the information provided by us is true and correct to the best of our knowledge and found during the joint inspection by the team.</p><br />

<p>Also this is to certify that after joint inspection based on the points mentioned in joint inspection report format, that (<asp:Label ID="lblDecWareName" runat="server" ></asp:Label>), has been found <asp:Label ID="lblDecFitUnfit" runat="server" ></asp:Label> for storage of commodity</p><br />
Date :-  <asp:Label ID="lblDate" runat="server"></asp:Label> 
</td>

                    </tr>
                    
                     <tr>
                    <td> <p style=" color:Red;">नोट :- गोदाम के निरीक्षण संबन्धित समस्त महत्वपूर्ण दस्तावेज़ आगामी निर्देश तक कार्यालय मे संग्रहीत करके रखे।</p>
</td>

                    </tr>
 
                                      
                                    <tr>
                  
                    <td align="center" colspan="4">
                  
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="submit" onclick="btnsubmit_Click" 
                            ></asp:Button>

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
