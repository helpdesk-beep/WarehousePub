<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fill_Inspection_Annexure_B.aspx.cs" Inherits="Inspection_Fill_Inspection_Annexure_B" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
      <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
     <%--<link rel="stylesheet" href="css/main.css" type="text/css" />--%>
<%--<link rel="stylesheet" href="css/main.css" type="text/css" />--%>
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
	<script type="text/javascript" src="js/menu.js"></script>
	<script type="text/javascript" src="js/slideshow.js"></script>
	<script type="text/javascript" src="js/cufon-yui.js"></script>
	<script type="text/javascript" src="js/Arial.font.js"></script>
    <title>Fill Annexure B</title>
          <style type="text/css">

.wrap { 
	margin: 0 auto; 
	width: 960px;
	
	-moz-box-shadow: 0px 5px 23px #000;

-webkit-box-shadow: 0px 5px 23px #000;

box-shadow: 0px 5px 23px #000;
	
}

input.submit {
	color: #fff;
	padding: 7px 10px;
	border: 0;
	font-weight: bold;
	background: #777;
	border-radius: 25px; 
}

input.text
{

    border: 2px solid rgb(173, 204, 204);
    height: 20px;
    width: 223px;
    font-size: 16px;
    box-shadow: 0px 0px 27px rgb(204, 204, 204) inset;
    transition:500ms all ease;
    padding:3px 3px 3px 3px;

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
    .style1
    {
        height: 30px;
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
        background-color: #D69758;
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

    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>
       <script type="text/javascript">
           function preventInput(evnt) {
               //Checked In IE9,Chrome,FireFox
               if (evnt.which != 9) evnt.preventDefault();
           }
        </script>  
            <script type="text/javascript" language="javascript">
                function ConfirmOnDelete() {
                    if (confirm("Are you sure want to Delete This Inspection ?") == true)
                        return true;
                    else
                        return false;
                }
    </script>
          
</head>
<body style="background-color:#FDFAF7">
<form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager> 

    <div id="bg" >
<div class="wrap">
<img src="../images/insp.jpg" style="width: 100%" alt="" height="140" />
          
             <center>
            <div  style="width: 100%;" >
                    <table align="center" style="width: 100%;">        
                        <tr >
                            <td colspan="4" style="font-size: medium;">
                            <table  style="width: 100%; height:32px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #D69758; width:100PX ; font-size: medium; color: White; " align="center"> 
<asp:LinkButton ID="LinkButton2" runat="server" align="right" ForeColor="White" 
                                       Font-Size="12pt" onclick="LinkButton2_Click">Home</asp:LinkButton>                             
                            </td>
                            <td colspan="2" style="background-color: #D69758 ;font-size: medium; color: White; width:100px" align="center" >
                            Welcome&nbsp;<asp:Label ID="lbl_user" runat="server" ForeColor="White"></asp:Label></td>
                            <td style="background-color: #D69758; width:70px;" align="center" >
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="White">Log out</asp:LinkButton></td>
                            </tr>
                            </table>
                            </td>
                        </tr> 
                        <tr>
                        <td align="center" colspan="3" >
                        <table border="1" width="70%" >
                            <tbody>
                                <th>Inspection ID </th>
                                <th>Inspection Type </th>
                                <th> Inspection Period</th>
                                <th>Branch </th>
                            </tbody>
                            <tr align="center">
                                    <td>
                                     <asp:Label ID="lblinspid" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                     <asp:Label ID="lblinsptype" runat="server" ></asp:Label>
                                    </td>  
                                    <td>
                                     <asp:Label ID="lblInspPeriod" runat="server" ></asp:Label>
                                    </td>                                    
                                    <td>
                                     <asp:Label ID="lblbranch" runat="server" ></asp:Label>
                                    </td>                                                                       
                           </tr>
                          </table>      
                        </td>
                        </tr>                                                                    
                    </table> 
                    <table align="center" style="width: 100%;border:#008CBA; border-style:solid ; border-width:0px;">
                        <tr>
                            <td align="center" style="border:#E6C79D; border-style:solid ; border-width:2px;" colspan="4">
                            <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Fill Godown Inspection ( Annexure 'B' ) </span>
                            </td>                            
                        </tr>
                        <tr>
                                        <td align="center" colspan="2" style="height:50px ; width:50%">
                                           <asp:Label ID="Label9" runat="server" Text="Godown : "></asp:Label>
  &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="true" Width="250px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" 
                                                onselectedindexchanged="ddl_gdwn_SelectedIndexChanged" > </asp:DropDownList>
                                        </td>
                                    <td  align="center" colspan="2" style="height:50px ; width:50%">
                                         <asp:Label ID="Label1" runat="server" Text="Inspection Date : "></asp:Label>
  &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txt_inspdate" runat="server" Width="150px" Height="25px"
                                        onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
                                         TargetControlID="txt_inspdate"></cc1:CalendarExtender>
                                    </td>                               

                        </tr>
                        <tr>
                        <td align="center" colspan="4">
                        <asp:Label ID="Label2" runat="server" Text="Scientific Capacity (In Qntl.) : "></asp:Label>
                        <asp:TextBox ID="txtsci_CPT" runat="server" Width="80px" Height="25px"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" Text="Max. Capacity (In Qntl.) : "></asp:Label>
                        <asp:TextBox ID="txtmaxcpt" runat="server" Width="80px" Height="25px"></asp:TextBox>    
                        <asp:Label ID="Label5" runat="server" Text="Godown Type : "></asp:Label>
                                        <asp:DropDownList ID="ddlhiredtype" runat="server" Width="100px" Height="25px">                      
                                        </asp:DropDownList>                        
                        <asp:Label ID="Label4" runat="server" Text="Storage Type : "></asp:Label>                                            
                                        <asp:DropDownList ID="ddlStorageType" runat="server" Width="100px" Height="25px">
                                                        <asp:ListItem Text="--Select--" ></asp:ListItem>
                                                        <asp:ListItem Text="Covered" Value="Covered"></asp:ListItem>
                                                        <asp:ListItem Text="Permanent(CAP)" Value="Permanent(CAP)"></asp:ListItem>
                                                           <asp:ListItem Text="Temporary(CAP)" Value="Temporary(CAP)"></asp:ListItem>
                                                         <asp:ListItem Text="Silo Bag"  Value="Silo Bag"></asp:ListItem>
                                                        <asp:ListItem Text="Steel Silo"  Value="Steel Silo"></asp:ListItem>                                                              
                                        </asp:DropDownList>                                                         
                       </td>
                        </tr>
                        
                        <tr id="tr_griddata" runat="server" visible="false">
                                <td colspan="4">
                                    <table align="center" style="width: 100%;">
<tr>
                            <td align="center" style="border:#E6C79D; border-style:solid ; border-width:2px;" colspan="4">
                            <span style="color: #cb4e48; font-weight: bold; font-size: 17px"> Stack wise Balance</span>
                            </td>                            
                        </tr>
<tr>   
                            <td colspan="6" valign="top" align="center">
                            <asp:GridView ID="GD_StackBal"  runat="server" DataKeyNames="Stack_ID" 
                                    AutoGenerateColumns="False" Width="95%" Font-Size="9pt" Font-Bold="true" FooterStyle-Wrap="true"
                            BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px" 
                                    CellPadding="2" CellSpacing="2">
    
                            <Columns>
                                <asp:BoundField DataField="Stack_ID" HeaderText="Stack_ID" ReadOnly="True"/>
                                <asp:BoundField DataField="Stack_Name" HeaderText="Stack Name" ReadOnly="True"/>
                                <asp:BoundField DataField="Stack_capacity" HeaderText="Stack Capacity" ReadOnly="True" />
                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" ReadOnly="True" />
                                <asp:BoundField DataField="AvlBags" HeaderText="Avlailable Bags" ReadOnly="True" />
                                <asp:BoundField DataField="AvlQty" HeaderText="Available Quantity" ReadOnly="True" /> 
                                <asp:TemplateField HeaderText="Avlailable Bags As Per PV"  >
                                    <ItemTemplate>
                                        <asp:TextBox ID="GtxtbagsActual" runat="server" Width="70px" align="Center" OnTextChanged="txtQty_TextChanged"
                        AutoPostBack="True" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Difference of Bags" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GtxtDiffBags" runat="server" Width="60px" align="Center" ReadOnly="true" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type of Diff. Bags" HeaderStyle-Width="50px"> 
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlDiffBagstYpe" runat="server" Width="80px"> 
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="Spillage Bags">Spillage Bags</asp:ListItem>
                                            <asp:ListItem Value="WHR Not Issue">WHR Not Issue</asp:ListItem>
                                            <asp:ListItem Value="Stock Not Deliverd">Stock Not Deliverd In System</asp:ListItem>
                                            <asp:ListItem Value="No Difference">No Difference</asp:ListItem>
                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Classification"> 
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlclassifi" runat="server"> 
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="C" Selected="True">C</asp:ListItem>
                                            <asp:ListItem Value="F">F</asp:ListItem>
                                            <asp:ListItem Value="H">H</asp:ListItem>                                         
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stack Open/Covered (Fumigated/CAP)" HeaderStyle-Width="100px"> 
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlStackType" runat="server" Width="80px"> 
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="Open">Open</asp:ListItem>
                                            <asp:ListItem Value="Covered">Covered</asp:ListItem>
                                                                                  
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                              
                                <asp:TemplateField HeaderText="Last Fumigation Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GtxtLastFDate" runat="server" Width="80px" align="Center" ></asp:TextBox>
                                          <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
                                         TargetControlID="GtxtLastFDate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="GtxtRemark" runat="server" Width="120px"  align="Center" Height="25px" TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                              
                           </Columns>
            
                                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="15px" Font-Size="8pt" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#E6C79D" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                                Height="20px" Font-Size="10pt" /> <AlternatingRowStyle BackColor="#eeeeee" />
                             </asp:GridView>
                            </td>      
                        </tr>
                                    <tr align="center">
                                        <td colspan="6" align="center" style="height:50px;">
                                           <asp:Label ID="Label11" runat="server">Total Available Bags (as Per Online) :</asp:Label>&nbsp;&nbsp;&nbsp;
<asp:Label ID="lblTotalBags_O" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label ID="Label7" runat="server" >Total Available Bags (as Per PV)</asp:Label>&nbsp;&nbsp;&nbsp;
<asp:Label ID="lblTptalBags_PV" runat="server" ></asp:Label>                                           
                                        </td>
                                    </tr>                         
                        
                                    <tr>
                                        <td colspan="6" align="center" style="height:50px;">
                                            <asp:Button class="button button6" ID="btn_saveInspDate" runat="server" Text="Submit" 
                                            TabIndex="11" Width="150px" Height="30px" onclick="btn_saveInspDate_Click" ></asp:Button> 
                                            &nbsp&nbsp&nbsp&nbsp
                                            <asp:Button class="button button6" ID="btnclear" runat="server" Text="Clear All" 
                                            TabIndex="12" Width="150px" Height="30px"  ></asp:Button>                                             
                                        </td>
                                    </tr>
                                    
                                                                        
                                    </table>
                                </td>
                        </tr>
                    <tr>
                         <td colspan="6"> 
                            <p style=" color:Red;">नोट :- <br />1.गोदाम में यदि फिजिकल स्कंध भण्डारित है और WHR एवम्‌ स्टेक भी बनी है परन्तु स्टेक ऑनलाइन प्रदर्शित नहि हो रह हे तो सम्पर्क करें । <br />
                            2. यदि ऑनलाइन ओर फिजिकल स्टेक बैलेंस में अंतर हे तो रिमार्क में विवरण अवस्य डालें ।<br />
                            3. यदि फ़्युमिगेसन नहीं किया गया है तो क्रिप्या फ़्युमिगेसन दिनांक ब्लेंक छोड़ें ।
                            </p>              
                        </td>
                   </tr>                           
                                                                                                
                    </table>
<asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlofferpopup" TargetControlID="Label12"
   BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlofferpopup" runat="server" CssClass="modalPopup" Height="150px" Width="250px" Visible="false">
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
                              Successfully Save
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
                                re.All Rights Reserved
                                <br />
                                          Developed By : National Informatics Centre
                                <br />
                                          Madhya Pradesh, Ministry of Communications and Information Technology</b>
                                </td>
                                <td style="width: 1%" align="center">
                                          <img id="Logo" src="/../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                                </td>
                                <td style="width: 20%" align="center">
                                        <table>
                                            <tr>    
                                                <td>  
                                                    <a href="http://india.gov.in/">
                                                    <img src="../Images/natindialogo.png" width="100px" height="50px" alt="" />
                                                    </a>
                                                </td>
                                                <td>
                                                    <a href="http://www.digitalindia.gov.in/">
                                                    <img src="../Images/di.png" width="100px" height="50px" alt="" />
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                         </tr>
                    </table>
               </div>
           </center>
      </div>
    </div>
</form>
</body>
</html>
