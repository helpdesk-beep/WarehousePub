<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OldBranchInsp.aspx.cs" Inherits="Inspection_OldBranchInsp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    

     <title>Old Branch Audit</title>
      <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
     <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

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


.tb6 {
	border: 3px double #CCCCCC;
	width: 230px;
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

      </style>

</head>
<body>
    <form id="form1" runat="server">
    <div id="bg">
		<div class="wrap">
             <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
            <div style="background-color: #66CCFF">
                 <p style="font-size: medium; color: #008080;">शाखाओं के अंकेक्षण की पुरानी जानकारी: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;<asp:LinkButton 
                         ID="LinkButton3" runat="server" onclick="LinkButton3_Click" >Change Password</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >Log out</asp:LinkButton></p>
            </div>
            <table width="100%">
            <tr>
            <td align="center">
               <asp:GridView ID="gvolddates" runat="server" AutoGenerateColumns="False" DataKeyNames="AuId"
                 CellPadding="4" EnableModelValidation="True" ForeColor="#333333" 
                 GridLines="None" OnSelectedIndexChanged="gvolddates_SelectedIndexChanged" 
                 onrowcommand="gvolddates_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField HeaderText="Date" DataField="Auditdate" />
                    <asp:BoundField HeaderText="Audit Id" DataField="AuId" />
                     <asp:BoundField HeaderText="Auditer Name" DataField="AuditerName" />
                      <asp:BoundField HeaderText="Auditer Post" DataField="AuditerPost" />
                    <asp:CommandField HeaderText="Branch Audit Print" ShowSelectButton="True" />
                    <asp:TemplateField HeaderText="Godown Audit">
                                                                         <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkBillNo" runat="server" Text='गोदाम का भौतिक अंकेक्षण' CommandName="GAUDIT"
                                                                                Font-Underline="true" ForeColor="Blue" ToolTip="Click Me"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                                                                                           
                                                                        <ItemStyle HorizontalAlign="center" Font-Size="10pt"/>
                                                                        <ControlStyle Width="150px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Godown Audit Print">
                                                                         <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkBillNo2" runat="server" Text='गोदाम अंकेक्षण के प्रिंट ' CommandName="GAUDIT2"
                                                                                Font-Underline="true" ForeColor="Blue" ToolTip="Click Me"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                                                                                           
                                                                        <ItemStyle HorizontalAlign="center" Font-Size="10pt"/>
                                                                        <ControlStyle Width="150px" />
                                                                    </asp:TemplateField>
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
            <tr>
            <td align="center">
            <asp:Button ID="btnsubmit" Visible="true" runat="server" Text="New Audit" 
                    class="submit" 
                    style=" color:Maroon; background-color:Silver; font-size:medium; font-family:Times New Roman Baltic;" onclick="btnsubmit_Click"
                        ></asp:Button>
            </td>
            </tr>
            </table>
         
            </div>
        </div>
    </form>
</body>
</html>
