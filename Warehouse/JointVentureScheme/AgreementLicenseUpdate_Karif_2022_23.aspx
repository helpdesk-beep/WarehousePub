<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgreementLicenseUpdate_Karif_2022_23.aspx.cs" Inherits="JointVentureScheme_AgreementLicenseUpdate_Karif_2022_23" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Update License Status </title>
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
        .style7
        {
            height: 15px;
        }
        .style8
        {
            height: 20px;
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
    border: 2px solid #008CBA;
}

.button1:hover {
    background-color: #008CBA;
    color: white;
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
        .auto-style1 {
            width: 449px;
        }
        .auto-style2 {
            height: 10px;
            width: 449px;
        }
        .auto-style3 {
            width: 922px;
        }
    </style>
    

</head>
<body onload="noBack();" style="background-color:White">
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
            var printWindow = window.open('', '', 'height=200,width=400');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
       <div id="ReportDiv" style="width:100%;" >
      
		<div class="wrap">
                 <form id="Form1"   runat="server">
                  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
                 <img src="../images/CH.jpg" style="width: 100%" alt="" height="150" />
          
                

  <div id="PrintDiv" style="font-size:medium;">

        <div style="width:100%;">

                    <table style="width:100%;">
                        
<%----------------------%>

                         <tr >
                            <td colspan="4" style="font-size: medium; width: 1000px;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
<asp:LinkButton ID="link1" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/DistrictWiseJVSOffer.aspx" ForeColor="White"></asp:LinkButton>                             
                            </td>
                            <td colspan="2" style="background-color: #008CBA ;font-size: medium; color: White; width:100px" align="center" >
                            Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                            <td style="background-color: #008CBA; width:70px;" align="center" >
                            <asp:LinkButton ID="LinkButton7" runat="server" ForeColor="White" 
                                    onclick="LinkButton7_Click">Log out</asp:LinkButton></td>
                            </tr>
                            </table>
                            </td>
                        </tr> 


 <tr>
                      <td colspan="4" style=" border-color:#008CBA; border-style:solid; border-width:2px; background-color:White ; width: 100%;" align="center">
                          <p style="font-size: 14px; color: Black;">
                          Update License Status</p>
                      </td>
                  </tr>
                  
                  <tr>
                    <td class="style7"> 
                    
                    </td>
                  </tr>

                   <tr>
                   
                    <td align="center" colspan="4">
                        
                Inspection ID : &nbsp;&nbsp;
                        <asp:TextBox ID="txtInsID" runat="server"  style="width:150px; height:25px;"></asp:TextBox>
                        <br/>
                          &nbsp;&nbsp; <asp:Button ID="btnSearch"  class="button button1" runat="server" Text="Search" OnClick="btnSearch_Click" Height="25px" Width="100px" />
                   <br /></td>
                       
                    </tr>
                         <tr>
                      <td colspan="4" style=" background-color:#66CCFF; height:25px" align="center">
                          <p style="font-size: 14px; color: Black; font-weight:bold" class="auto-style3">  License Status Details</p>
                      </td>                       
                         </tr>                    

 <tr>
                <td colspan="4" id="GVGodowns" runat="server" visible="true" align="center" style="overflow:scroll;">
               
                        <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" 
                             EnableModelValidation="True" BackColor="White" BorderColor="Black" 
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                             Font-Size="9pt" onselectedindexchanged="gvGodown_SelectedIndexChanged" Height="55px" Width="675px" >
                            <AlternatingRowStyle Font-Size="9pt" />
                            <Columns>
                            <asp:TemplateField HeaderText = "SNo." ItemStyle-Width="100">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>

<ItemStyle Width="25px"></ItemStyle>
    </asp:TemplateField>
                            <asp:BoundField DataField="TId" HeaderText="TId" ></asp:BoundField>
                           <asp:BoundField DataField="Inspection_Id" HeaderText="Inspection Id" />
                                 <asp:BoundField DataField="Insp_Date" HeaderText="Inspection Date" />
                                   <asp:BoundField DataField="CreatedDate" HeaderText="Create Date" />
                           <asp:BoundField DataField="Registration_Id" HeaderText="Registration Id" />
                            <asp:BoundField DataField="WMS_GodownID" HeaderText="Godown ID" ItemStyle-HorizontalAlign="Left" > 
                            <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                               <asp:BoundField DataField="Warehouse_Name" HeaderText="Godown Name" />
                                <asp:BoundField DataField="Godown_No" HeaderText="Godown No" />

                                <asp:BoundField DataField="Present_Validity_WDRA" HeaderText="WDRA License Type" />
                                <asp:BoundField DataField="WDRA_LicenseNo" HeaderText="WDRA LicenseNo" />
                                <asp:BoundField DataField="WDRA_LIssueDate" HeaderText="WDRA IssueDate" />
                                <asp:BoundField DataField="WDRA_LicenseDate" HeaderText="WDRA LicenseDate" />

                                <asp:BoundField DataField="WDRAL_Present_Validity" HeaderText="NON WDRA License Type" />
                                <asp:BoundField DataField="Warehouse_LicenseNo" HeaderText="NON WDRA LicenseNo" />
                                <asp:BoundField DataField="Warehouse_LIssueDate" HeaderText="NON WDRA IssueDate" />
                                <asp:BoundField DataField="Warehouse_LicenseDate" HeaderText="NON WDRA LicenseDate" />
                                <asp:CommandField SelectText="Update" HeaderText="Change" ShowSelectButton="True" >
                                <ControlStyle Font-Bold="True" ForeColor="Red" />
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" Height="30px" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                            <SelectedRowStyle BackColor="#669999" ForeColor="White" />
                        </asp:GridView>

                    </td>            
            </tr>
            <tr>
            <td class="style8">
            
            </td>
            </tr>
            
            <tr id="TRHide" visible="false" runat="server">
                <td>
                    <table style="width: 100%; font-size:12px;" >
                   
            <tr id="gv" runat="server" visible="false">
            <td colspan="4">
            <table style="width:100%;">
                <tr>
                  
                <td class="auto-style4" colspan="8" style="text-align:center; padding-top:20PX;">
                    <asp:Label Font-Size="11pt" Font-Bold="true" ForeColor="navy" ID="Label2" runat="server" Text="License Type"></asp:Label>&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlGodownType"  Class="form-control" runat="server" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">WDRA</asp:ListItem>
                        <asp:ListItem Value="2">NON WDRA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                     <tr>
                <td class="auto-style2"> 
                </td>
            </tr>
                     <tr>
                <td class="auto-style2"> 
                </td>
            </tr>
                </tr>
                    <tbody id="wdra" runat="server" visible="false">
                    <td align="center" class="auto-style1">&nbsp;&nbsp;
                        <asp:Label ID="Label1" runat="server" Text="WDRA License No : "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtLiNo" runat="server"  style=" height:25px; width:100px;"></asp:TextBox>
                        </td>
                         <td align="center" class="auto-style1">&nbsp;&nbsp;
                        <asp:Label ID="Label4" runat="server" Text="WDRA Issue Date : "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtisd" runat="server"  style="height:25px; width:100px;"></asp:TextBox>
                        </td>
                     <td align="center" class="auto-style1">&nbsp;&nbsp;
                        <asp:Label ID="Label9" runat="server" Text="Expiry Date : "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtexd" runat="server"  style="height:25px; width:100px;"></asp:TextBox>
                        </td>
                        </tbody>
                </tr>
                    <tbody id="nonwdra" runat="server" visible="false">
                   <td align="center">&nbsp;&nbsp;
                        <asp:Label ID="Label7" runat="server" Text="NON WDRA License No : "></asp:Label>&nbsp;&nbsp;
                        <asp:TextBox ID="txtNLiNo" runat="server"  style=" height:25px; width:100px;"></asp:TextBox>
                        </td>
                    <td align="center">&nbsp;&nbsp;
                        <asp:Label ID="Label8" runat="server" Text="NON WDRA Issue Date : "></asp:Label>&nbsp;&nbsp;
                        <asp:TextBox ID="txtNisd" runat="server"  style="height:25px; width:100px;"></asp:TextBox>
                        </td>
                     <td align="center">&nbsp;&nbsp;
                        <asp:Label ID="Label10" runat="server" Text="NON Expiry Date : "></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtNexd" runat="server"  style="height:25px; width:100px;"></asp:TextBox>
                        </td>
                        </tbody>
                </tr>
                
                
                <tr>
                <td class="auto-style2"> 
                </td>
            </tr>
            <tr id="btnu" runat="server" visible="false">
                         <td align="center" colspan="4">
            <asp:Button ID="btnUpdateCpt"  class="button button1" runat="server" Text="Update" Height="25px" Width="100px" 
                    onclick="btnUpdateCpt_Click" ></asp:Button>
            </td>
            </tr>
                     </table>
            </td>
            </tr>
    </table>
    </td></tr></table>
            <asp:HiddenField ID="hdnTid" runat="server" Value="0" />
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
                              Successfully Update Capacity
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
               </div>
    <div style="background-image: url('../images/div_bg.png')"></div>
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
                  </form>
               </div>
        </div>
</body>
</html>
