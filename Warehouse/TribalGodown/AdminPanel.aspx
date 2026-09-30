<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminPanel.aspx.cs" Inherits="TribalGodown_AdminPanel" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Panel</title>
       <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
     <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
	 <script type="text/javascript" language="javascript" >
	     function FillReason() {
	         var Res;
	         Res = document.getElementById('<%= txtReason.ClientID %>');
	         if (Res.value == "" || Res.value == null) {
	             alert("Please Fill Reason");
	             return false;
	         }
	     }
    </script>
     <script type="text/javascript">
         (function() {
             var fn = function() {
               $get("ctl00_ToolkitScriptManager1_HiddenField").value = '';Sys.Application.remove_init(fn);};Sys.Application.add_init(fn);})();(function() {var fn = function() {Sys.Extended.UI.ModalPopupBehavior.invokeViaServer('ctl00_ContentPlaceHolder1_ModalPopupExtender1', true); Sys.Application.remove_load(fn);};Sys.Application.add_load(fn);})();Sys.Application.initialize();
               Sys.Application.add_init(function() {
               $create(Sys.Extended.UI.ModalPopupBehavior, {"BackgroundCssClass":"popup","CancelControlID":"ctl00_ContentPlaceHolder1_x","PopupControlID":"ctl00_ContentPlaceHolder1_pnllogin","dynamicServicePath":"/Warehouse/Branch_Welcome.aspx","id":"ctl00_ContentPlaceHolder1_ModalPopupExtender1"}, null, null, $get("ctl00_ContentPlaceHolder1_new"));
             });
Sys.Application.add_init(function() {
    $create(Sys.Extended.UI.Animation.AnimationBehavior, {"OnClick":"{\"AnimationName\":\"Parallel\",\"AnimationTarget\":\"ctl00_ContentPlaceHolder1_pnllogin\",\"Duration\":\"0.4\",\"Fps\":\"10\",\"AnimationChildren\":[{\"AnimationName\":\"FadeIn\",\"AnimationChildren\":[]}]}","id":"ctl00_ContentPlaceHolder1_popupAnimation"}, null, null, $get("ctl00_ContentPlaceHolder1_new"));
});
</script>
<script src="js/jquery.elevatezoom.js" type="text/javascript"></script>
     <script type="text/javascript">
         $(function() {
         $("#GVApp").find("[id$=zoomjq]").elevateZoom({
                cursor: 'inner',
                tint:true,
                tintColour:'#F90',
                tintOpacity:0.5,
             });
                $("#GVApp").find("[id$=zoomjqd]").elevateZoom({
                cursor: 'inner',
                tint:true,
                tintColour:'#F90',
                tintOpacity:0.5,
             });
         });
</script>
    <style>
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

.modalpop
{
	background-color:#f3f3f3;
	border-radius:10px 10px 10px 10px;
	position:absolute;
	z-index:90000;
	margin-left:50%;
	top:50%;
	box-shadow: 0 2px 5px #000;
}

.popup{
width: 100%;
margin: 0 auto;
position: fixed;
z-index: 101;
}
.pop{
min-width: 220px;
width: 220px;
min-height: 100px;
margin: 10px auto;
background: #f3f3f3;
position: relative;
z-index: 103;
padding: 10px;
border-radius: 5px;
box-shadow: 0 2px 5px #000;
}
.pop p{
color: #555555;
text-align: justify;
font-size:medium;
}
.pop p a{
color: #d91900;
}
.x{
float: right;
height: 35px;
left: 22px;
position: relative;
top: -20px;
width: 35px;
}

.DownloadPop
{
	background-color:#f3f3f3;
	position:absolute;
	z-index:90000;
	margin-left:50%;
	top:50%;
	box-shadow: 0 2px 5px #000;

}
        .style1
        {
            background-color: #f3f3f3;
            border-radius: 10px 10px 10px 10px;
            position: absolute;
            z-index: 90000;
            margin-left: 50%;
            top: 50%;
            box-shadow: 0 2px 5px #000;
            left: -462px;
        }
    </style>
</head>
<body>
<asp:ScriptManager ID="sm1" runat="server">
</asp:ScriptManager>
    <form id="form1" runat="server">
    <div >
<div >
<img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
<table style="width: 100%;">
                         <tr >
                            <td colspan="4" style="font-size: medium;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #008CBA; width:70PX ; " align="center"> 
                             <asp:LinkButton ID="lnkHome" runat="server"  ForeColor="White" onclick="lnkHome_Click" Text="Home"></asp:LinkButton>
                            </td>
                            <td colspan="2" style="background-color: #008CBA ;font-size: medium; color: White; width:100px" align="center" >
                            <asp:Label runat="server" ID="lblPP" Text=""></asp:Label>
                            Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                            <td style="background-color: #008CBA; width:70px;" align="center" >
                            <asp:LinkButton ID="lnkLogOut" runat="server" OnClick="lnkLogOut_Click" ForeColor="White">Log out</asp:LinkButton></td>
                            </tr>
                            </table>
                            </td>
                        </tr>
</table>
<%--            <p style="font-size: medium; color: #008080; background-color:Aqua;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton 
                    ID="lnkHome" runat="server" onclick="lnkHome_Click">Home</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblPP" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkLogOut" runat="server" onclick="lnkLogOut_Click">Log out</asp:LinkButton></p>--%>
              <br/>
                         <center><asp:Label runat="server" Font-Size="Medium" ID="lblPhase" Text="Application Phases"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:DropDownList ID="ddlphase" runat="server" Width="100px" Height="23" 
                                 onselectedindexchanged="ddlphase_SelectedIndexChanged" AutoPostBack="true">
                          <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                         <asp:ListItem Text="Phase 1" Value="1"></asp:ListItem>
                         <asp:ListItem Text="Phase 2" Value="2"></asp:ListItem>
                         <asp:ListItem Text="Phase 3" Value="3"></asp:ListItem>
                         <asp:ListItem Text="Phase 4" Value="4"></asp:ListItem>
                         <asp:ListItem Text="Phase 5" Value="5"></asp:ListItem>
                         <asp:ListItem Selected="True" Text="Phase 6" Value="6"></asp:ListItem>
                         </asp:DropDownList></center>
            
             <center><asp:Label Width="100%" ForeColor="Maroon" Visible="false" Font-Bold="true" BackColor="Aqua" Font-Size="Medium" ID="lblheading" runat="server" Text="Pending Application List"></asp:Label></center>
<table>

            <tr id="trPendList" visible="false" runat="server">
<td colspan="4">
                                                    <center>
                                                           <div>
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Pending Applications List"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td>
                                                           <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                                        Font-Size="10pt"></asp:Label>
            <br />
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                             <asp:GridView ID="GVApp" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                CellPadding="4" EnableModelValidation="True" ForeColor="#333333" 
                GridLines="None" OnSelectedIndexChanged="GVApp_SelectedIndexChanged" 
                onrowcommand="GVApp_RowCommand" onpageindexchanging="GVApp_PageIndexChanging" PageSize="10">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                <asp:BoundField HeaderText="Registration Date" DataField="DateOfReg"/>
                <asp:TemplateField HeaderText="Application No.">
                        <ItemTemplate>
                         <asp:Label ID="lblTAID" runat="server" Text='<%#Bind("TAID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Warehouse District" DataField="WDistrict" />
                    <asp:BoundField HeaderText="Warehouse Block" DataField="WBlock" />
                    <asp:BoundField HeaderText="Applicant Name" DataField="FullName"/>     
                    <asp:BoundField HeaderText="Qualification" DataField="Education"/>
                     <asp:BoundField HeaderText="Rojgar No." DataField="Rojgarnum"/>
                     <asp:BoundField HeaderText="Voter Id No" DataField="VoterId"/>
                     <asp:BoundField HeaderText="Aadhar Card No" DataField="AdharCard"/>
                     <asp:BoundField HeaderText="PAN No" DataField="PaNNo"/>
                     <asp:BoundField HeaderText="Warehouse Distance" DataField="DistFromTO"/>
                     <asp:BoundField HeaderText="Land Qty" DataField="QuantityOfForm"/>
                   <asp:TemplateField HeaderText="Photo">
                    <ItemTemplate> 
                    <asp:ImageButton ID="zoomjq" runat="server" ImageUrl='<%# "ShowImage.ashx?TAID="+ Eval("TAID").ToString() %>' OnClick="btn_click"
                         Height="30px" Width="30px" data-zoom-image='<%# "ShowImage.ashx?TAID="+ Eval("TAID").ToString() %>'/>  
                   </ItemTemplate>
                </asp:TemplateField>
            <%-- <asp:TemplateField HeaderText="Document">
                    <ItemTemplate>
                     <asp:HyperLink ID="hp3" runat="server">
                            <asp:Image ID="zoomjqd" runat="server" ImageUrl='<%# "ShowDocument.ashx?TAID="+ Eval("TAID").ToString() %>' Height="30px" Width="30px" data-zoom-image='<%# "ShowDocument.ashx?TAID="+ Eval("TAID").ToString() %>'/>
                             </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:ButtonField CommandName="Approve" HeaderText="Approve" ButtonType="Button" Text="Approve" />
                    <asp:ButtonField CommandName="Reject" HeaderText="Reject" ButtonType="Button" Text="Reject" />
                      <asp:ButtonField HeaderText="Download" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ButtonType="Link" CommandName="ViewFile" Text="Download" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
                                                           </td>
                                                           </tr>
                                                           </table>
                                                             
                                                            </div>
                                                          
                                                            </center>
                                                            </td>
</tr>

<tr id="trSummary" visible="false" runat="server">
<td colspan="4">
                                                    <center>
                                                           <div>
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Application Summary"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                             <div style=" width:100%" id="Div1" runat="server" visible="true">

<asp:GridView ID="GridTotalStatus" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                CellPadding="4" EnableModelValidation="True" ForeColor="#333333" 
                GridLines="None" onrowdatabound="GridTotalStatus_RowDataBound" ShowFooter="true"
               >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                  <%--  <asp:BoundField HeaderText="District" DataField="WarDist" />
                    <asp:BoundField HeaderText="Block" DataField="WarBlock" />--%>
                    <asp:TemplateField HeaderText="District">
                <FooterTemplate>
                    <asp:Label ID="District" runat="server" Text=""></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("WarDist") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Block">
                <FooterTemplate>
                    <asp:Label ID="Block" runat="server" Text="Total"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblBlock" runat="server" Text='<%# Eval("WarBlock") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Application">
                <FooterTemplate>
                    <asp:Label ID="Total" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Approved">
                <FooterTemplate>
                    <asp:Label ID="Approved" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblApproved" runat="server" Text='<%# Eval("Approved") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Rejected">
                <FooterTemplate>
                    <asp:Label ID="Rejected" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblRejected" runat="server" Text='<%# Eval("Rejected") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Pending">
                <FooterTemplate>
                    <asp:Label ID="Pending" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPending" runat="server" Text='<%# Eval("Pending") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                  <%--  <asp:BoundField HeaderText="Total Application" DataField="Total" />
                    <asp:BoundField HeaderText="Approved" DataField="Approved" />
                   <asp:BoundField HeaderText="Rejected" DataField="Rejected" />
                   <asp:BoundField HeaderText="Pending" DataField="Pending" />--%>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
</div>
<center><asp:Button BackColor="Aqua" Font-Bold="true" ID="btnSS" runat="server" 
           Text="Export" onclick="btnSS_Click" 
                 /></center>
</td>
</tr>
</table>
           
            </div>
                                                   </center>
                                                           </td>
                                                           </tr>
 
 <tr id="trMerit" visible="false" runat="server">
<td colspan="4">
                                                    <center>
                                                           <div>
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Selected Applicants Merit List"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                       

<div id="DivExport" runat="server">

<asp:GridView ID="GvReport" runat="server" AutoGenerateColumns="False"
                CellPadding="4" EnableModelValidation="True" ForeColor="#333333"
                GridLines="None"
               >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                  <asp:BoundField HeaderText="Registration Date" DataField="DateOfReg"/>
                <asp:TemplateField HeaderText="Application No.">
                        <ItemTemplate>
                         <asp:Label ID="lblTAID" runat="server" Text='<%#Bind("TAID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Warehouse District" DataField="WDistrict" />
                    <asp:BoundField HeaderText="Warehouse Block" DataField="WBlock" />
                    <asp:BoundField HeaderText="Applicant Name" DataField="FullName"/>     
                    <asp:BoundField HeaderText="Qualification" DataField="Education"/>
                     <asp:BoundField HeaderText="Rojgar No." DataField="Rojgarnum"/>
                     <asp:BoundField HeaderText="Voter Id No" DataField="VoterId"/>
                     <asp:BoundField HeaderText="Aadhar Card No" DataField="AdharCard"/>
                     <asp:BoundField HeaderText="PAN No" DataField="PaNNo"/>
                     <asp:BoundField HeaderText="Warehouse Distance" DataField="DistFromTO"/>
                     <asp:BoundField HeaderText="Land Qty" DataField="QuantityOfForm"/>
                     <asp:TemplateField HeaderText="Photo">
                    <ItemTemplate> 
                    <asp:ImageButton ID="zoomjq" runat="server" ImageUrl='<%# "ShowImage.ashx?TAID="+ Eval("TAID").ToString() %>' OnClick="btn_click"
                         Height="30px" Width="30px" data-zoom-image='<%# "ShowImage.ashx?TAID="+ Eval("TAID").ToString() %>'/>  
                   </ItemTemplate>
                </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
           
            </div>
            
            
            <center><asp:Button BackColor="Aqua" Font-Bold="true" ID="btnPrint" runat="server" Text="Export" 
                onclick="btnPrint_Click" /></center>
                                                           </td>
                                                           </tr>
                                                           
    
                                                           </table>
                                                             
                                                            </div>
                                                          
                                                            </center>
                                                            </td>
</tr>
<tr id="trApprovedList" visible="false" runat="server">
<td colspan="4">
                                                    <center>
                                                           <div>
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Approved Applicants List"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                       

<div id="Div3" runat="server">

<asp:GridView ID="gvAproved" runat="server" AutoGenerateColumns="False"
                CellPadding="4" EnableModelValidation="True" ForeColor="#333333"
                GridLines="None" onrowcommand="gvAproved_RowCommand"
               >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField HeaderText="Registration Date" DataField="DateOfReg"/>
                <asp:TemplateField HeaderText="Application No.">
                        <ItemTemplate>
                         <asp:Label ID="lblTAID" runat="server" Text='<%#Bind("TAID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Warehouse District" DataField="WDistrict" />
                    <asp:BoundField HeaderText="Warehouse Block" DataField="WBlock" />
                    <asp:BoundField HeaderText="Applicant Name" DataField="FullName"/>     
                    <asp:BoundField HeaderText="Qualification" DataField="Education"/>
                     <asp:BoundField HeaderText="Rojgar No." DataField="Rojgarnum"/>
                     <asp:BoundField HeaderText="Voter Id No" DataField="VoterId"/>
                     <asp:BoundField HeaderText="Aadhar Card No" DataField="AdharCard"/>
                     <asp:BoundField HeaderText="PAN No" DataField="PaNNo"/>
                     <asp:BoundField HeaderText="Warehouse Distance" DataField="DistFromTO"/>
                     <asp:BoundField HeaderText="Land Qty" DataField="QuantityOfForm"/>
                      <asp:TemplateField HeaderText="Photo">
                    <ItemTemplate> 
                    <asp:ImageButton ID="zoomjq" runat="server" ImageUrl='<%# "ShowImage.ashx?TAID="+ Eval("TAID").ToString() %>' OnClick="btn_click"
                         Height="30px" Width="30px" data-zoom-image='<%# "ShowImage.ashx?TAID="+ Eval("TAID").ToString() %>'/>  
                   </ItemTemplate>
                </asp:TemplateField>
                      <asp:ButtonField HeaderText="Download" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ButtonType="Link" CommandName="ViewFile2" Text="Download" />
                      <asp:ButtonField CommandName="PrintF" HeaderText="Print App Form" ButtonType="Button" Text="Print" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
           
            </div>
            
            
            <center><asp:Button BackColor="Aqua" Font-Bold="true" ID="Button2" runat="server" 
                    Text="Export" onclick="Button2_Click" 
                 /></center>
                                                           </td>
                                                           </tr>
                                                           
    
                                                           </table>
                                                             
                                                            </div>
                                                          
                                                            </center>
                                                            </td>
</tr>
   <tr id="trRejected" visible="false" runat="server">
<td colspan="4">
                                                    <center>
                                                           <div>
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Rejected Applicants List"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                       

<div id="Div2" runat="server">

<asp:GridView ID="gvRejected" runat="server" AutoGenerateColumns="False"
                CellPadding="4" EnableModelValidation="True" ForeColor="#333333"
                GridLines="None"
               >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField HeaderText="Registration Date" DataField="DateOfReg"/>
                <asp:TemplateField HeaderText="Application No.">
                        <ItemTemplate>
                         <asp:Label ID="lblTAID" runat="server" Text='<%#Bind("TAID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Warehouse District" DataField="WDistrict" />
                    <asp:BoundField HeaderText="Warehouse Block" DataField="WBlock" />
                    <asp:BoundField HeaderText="Applicant Name" DataField="FullName"/>     
                    <asp:BoundField HeaderText="Qualification" DataField="Education"/>
                     <asp:BoundField HeaderText="Rojgar No." DataField="Rojgarnum"/>
                     <asp:BoundField HeaderText="Voter Id No" DataField="VoterId"/>
                     <asp:BoundField HeaderText="Aadhar Card No" DataField="AdharCard"/>
                     <asp:BoundField HeaderText="PAN No" DataField="PaNNo"/>
                     <asp:BoundField HeaderText="Warehouse Distance" DataField="DistFromTO"/>
                     <asp:BoundField HeaderText="Land Qty" DataField="QuantityOfForm"/>
                    <asp:BoundField HeaderText="Rejection Reason" DataField="Remark" />
                    <asp:TemplateField HeaderText="Photo">
                    <ItemTemplate> 
                    <asp:ImageButton ID="zoomjq" runat="server" ImageUrl='<%# "ShowImage.ashx?TAID="+ Eval("TAID").ToString() %>' OnClick="btn_click"
                         Height="30px" Width="30px" data-zoom-image='<%# "ShowImage.ashx?TAID="+ Eval("TAID").ToString() %>'/>  
                   </ItemTemplate>
                </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
           
            </div>
            
            
            <center><asp:Button BackColor="Aqua" Font-Bold="true" ID="Button1" runat="server" 
                    Text="Export" onclick="Button1_Click" 
               /></center>
                                                           </td>
                                                           </tr>
                                                           
    
                                                           </table>
                                                             
                                                            </div>
                                                          
                                                            </center>
                                                            </td>
</tr>                                                        
    
                                                           </table>
                                                             
                                                            </div>
                                                                                                            

               

   
           <br />

   <asp:Panel ID="pnllogin" runat="server" Visible="false">
    <table id="modalpop" class="modalpop">
    <tr>
    <td colspan="2" align="center"  
            valign="top">
    <h4>रिजेक्ट करने का कारण लिखे</h4>
    </td>
    </tr>
    <tr>
   
    <td colspan="2">
        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>
      
&nbsp;&nbsp;
          <asp:Button ID="btnSubmitR" runat="server" Text="Submit" onclick="btnSubmitR_Click" OnClientClick="return FillReason()"
            />
    </td>
    <td>
        <asp:Button ID="btnCancelR" runat="server" Text="Cancel" 
            onclick="btnCancelR_Click" />
    </td>
    </tr>
    </table>
    </asp:Panel>
     <br />
<asp:Panel ID="Panel1" runat="server" Visible="false">
 <table width="16%" class="DownloadPop">
 <tr>
 <td colspan="2" align="right">
<%-- <img src="~/images/close-icon.png" alt="quit" runat="server" class="x" id="x" />--%>
 <asp:ImageButton ImageUrl="~/images/close-icon.png" alt="quit" runat="server" 
         class="x" id="x" onclick="x_Click"/>
 </td>
 </tr>
    <tr>
   <td colspan="2" align="center"  
            valign="top">
    <h4>Download करने के लिए क्लिक करे</h4>
    </td>
    </tr>
   
    <tr>
   
    <td align="right">
         <asp:Button ID="bynImg" CssClass="submit" runat="server" Text="Photo" Width="100px"
             onclick="bynImg_Click"/>
    </td>
      <td>
         <asp:Button ID="btnDoc" CssClass="submit" runat="server" Text="Employment" Width="100px"
              onclick="btnDoc_Click"/>
    </td>
    </tr>
     <tr>
   
    <td align="right">
         <asp:Button ID="btnEducation" CssClass="submit" runat="server" Text="Education" 
             Width="100px" onclick="btnEducation_Click" 
             />
    </td>
      <td>
         <asp:Button ID="btnCast" CssClass="submit" runat="server" Text="Cast" 
              Width="100px" onclick="btnCast_Click"
             />
    </td>
    </tr>
    </table>
<br/>
</asp:Panel>  
            </div>
        </div>
</form>
</body>
</html>
