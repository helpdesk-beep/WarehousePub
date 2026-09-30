<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="State_DashBoard_DistWise_DrilDown.aspx.cs" Inherits="StatePages_State_DashBoard_DistWise_DrilDown" Title="Dashboard Dist Wise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
       
    .popbag
    {
    background-color:gray;
    filter:alpha(opacity=90);
    opacity:0.8;
    z-index:10000;
    }
    .modalpop
    {
    background-color:#FFFFFF;
    border-width:3px;
    border-color:Black;
    padding-top:10px;
    padding-left:10px;
    width:700px;
    height:500px;
    border-radius: 5px;
    text-shadow:yellow;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
    background-color: #4CAF50; 
    color: white; 
    border: 2px solid #4CAF50;
}

.button1:hover {
    background-color: #4CAF50;
    color: white;
}
.button2 {
    background-color: #008CBA; 
    color: white; 
    border: 2px solid #008CBA;
}

.button2:hover {
    background-color: #008CBA;
    color: white;
}

.button3 {
    background-color: #f44336; 
    color: white; 
    border: 2px solid #f44336;
}

.button3:hover {
    background-color: #f44336;
    color: white;
}
.button6 {
    background-color: #E47D21;
    color: white;
    border: 2px solid #E47D21;
}

.button6:hover {
    background-color: #E47D21;
    color: white;
}

</style>

<style type="text/css">
#popupwin {
position:fixed;
top: 0;
left: 0;
width: 90%;
height: 90%;
background-color: #000;
filter:alpha(opacity=65);
-moz-opacity:0.7;
display: none;
opacity: 0.7;
z-index: 100;

}
.pop{
margin: 0px auto;
background: #FFFFFF;
z-index: 100;
padding: 10px;
border-radius: 5px;
box-shadow: 0 3px 5px #000;
}
</style>

<script language="JavaScript"> var message = 'Right Click is disabled';
function clickIE() { if (event.button == 2) { alert(message); return false; } }
function clickNS(e) {
if (document.layers || (document.getElementById && !document.all)) {
if (e.which == 2 || e.which == 3) { alert(message); return false; }
}
}
if (document.layers) { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
else if (document.all && !document.getElementById) { document.onmousedown = clickIE; }
document.oncontextmenu = new Function('alert(message);return false') </script>
    
<style type="text/css">

.td:hover /* Highlight Current Cell*/
{
    box-shadow: 0 5px 10px #000;
     font-weight:bold;
}
</style>

<fieldset style="height:100% ; width:98%; border: 0px solid navy; margin-left:5px; margin-right:5px; background-color:#E7E7E7;">
     <center>
        <div>
          <table style="width:98%;">
<%--            <tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px; background-color:#5D6D7E; height:25px;" colspan="3">
                <span style="color: #FFFFFF; font-weight: bolder; font-size: 16px; font-family:Arial;">Wheat-PSS Stock Position Dashboard</span>
                </td>
            </tr>--%>
         

                               
            <tr id="tr_trasondate01042019" runat="server" visible="false">
                <td style="vertical-align:top; width:100%;  font-family:Arial" align="Center" class="pop" >

                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:12pt; font-weight:bold ;text-align:Center; height:30px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Wheat Stock Position As On 01/04/2019
                                </td>                            
                            </tr>
            <tr>
                        <td style="height:30px; font-size:14px" colspan="3" align="right" >
                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" 
                                >Quantity In M.T</asp:LinkButton>                       
                        <div style="width:100%;">
                        <img id="Img1" src="../Images/line.png" height="25px" width="100%" alt="" />
                        </div>
                        </td>
            </tr>                             
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="GD1" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="District_Name" BorderStyle="Double" CellPadding="5" CellSpacing="7" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True"  >
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="District_Name" HeaderText="District">
                                                        <ItemStyle Width="40%" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AvlBal16" HeaderText="Available Stock 2016-17">                                                                                                                   
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AvlBal17" HeaderText="Available Stock 2017-18">                                                           
                                            </asp:BoundField>   
                                            <asp:BoundField DataField="AvlBal18" HeaderText="Available Stock 2018-19">                                                           
                                            </asp:BoundField>      
                                            <asp:BoundField DataField="AvlBal19" HeaderText="Available Stock 2019-20">                                                           
                                            </asp:BoundField>                
                                            <asp:BoundField DataField="AvlOnDate01042019" HeaderText="Stock Position As On 01/04/2019 ">                                                           
                                            </asp:BoundField>                                                                                                                                                                                                             
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true" />
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td>
                <td style="vertical-align:top; width:10px;">
                
                </td>
            </tr>
            
            
<tr id="tr_trasondate" runat="server" visible="false">
                <td style="vertical-align:top; width:100%;  font-family:Arial" align="Center" class="pop" >

                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:12pt; font-weight:bold ;text-align:Center; height:28px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Wheat Stock Position As On Date
                                </td>                            
                            </tr>
            <tr>
                        <td style="height:30px; font-size:14px" colspan="3" align="right" >
                            <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" 
                                >Quantity In M.T</asp:LinkButton>                       
                        <div style="width:100%;">
                        <img id="Img2" src="../Images/line.png" height="25px" width="100%" alt="" />
                        </div>
                        </td>
            </tr>                             
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="GD6" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="District_Name" BorderStyle="Double" CellPadding="5" CellSpacing="7" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True" 
                                        onselectedindexchanged="GD6_SelectedIndexChanged"  >
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="District_Name" HeaderText="District">
                                                        <ItemStyle Width="40%" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AvlBal16" HeaderText="Available Stock 2016-17">                                                                                                                   
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AvlBal17" HeaderText="Available Stock 2017-18">                                                           
                                            </asp:BoundField>   
                                            <asp:BoundField DataField="AvlBal18" HeaderText="Available Stock 2018-19">                                                           
                                            </asp:BoundField>      
                                            <asp:BoundField DataField="AvlBal19" HeaderText="Available Stock 2019-20">                                                           
                                            </asp:BoundField>                
                                            <asp:BoundField DataField="AvlOnDateDate" HeaderText="Stock Position As On Date">                                                           
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="District_ID" HeaderText="District_ID`">                                                           
                                            </asp:BoundField>                                            
                                            
                                            <asp:CommandField SelectText="View" HeaderText="Detail " ShowSelectButton="True" >
                                            <ControlStyle Font-Bold="True" ForeColor="#008CBA" />
                                            </asp:CommandField>                                                                                                                                                                                                    
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true" />
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td>
                <td style="vertical-align:top; width:10px;">
                
                </td>
            </tr>
            
<tr id="tr_RD_ThisM" runat="server" visible="false">
                <td style="vertical-align:top; width:100%;  font-family:Arial" align="Center" class="pop" >

                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:12pt; font-weight:bold ;text-align:Center; height:30px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Receipts During The Month
                                </td>                            
                            </tr>
            <tr>
                        <td style="height:30px; font-size:14px" colspan="3" align="right" >
                            <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" 
                                >Quantity In M.T</asp:LinkButton>                       
                        <div style="width:100%;">
                        <img id="Img3" src="../Images/line.png" height="25px" width="100%" alt="" />
                        </div>
                        </td>
            </tr>                             
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="GD3" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="District_Name" BorderStyle="Double" CellPadding="5" CellSpacing="7" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True"  >
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="District_Name" HeaderText="District">
                                                        <ItemStyle Width="40%" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AvlBal16" HeaderText="Receipts 2016-17">                                                                                                                   
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AvlBal17" HeaderText="Receipts 2017-18">                                                           
                                            </asp:BoundField>   
                                            <asp:BoundField DataField="AvlBal18" HeaderText="Receipts 2018-19">                                                           
                                            </asp:BoundField>      
                                            <asp:BoundField DataField="AvlBal19" HeaderText="Receipts 2019-20">                                                           
                                            </asp:BoundField>                
                                            <asp:BoundField DataField="AvlOnDate01042019" HeaderText="Total Receipts During The Month">                                                           
                                            </asp:BoundField>                                                                                                                                                                                                             
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true" />
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td>
                <td style="vertical-align:top; width:10px;">
                
                </td>
            </tr>                                 
          </table>
        </div>
     </center>
</fieldset>
</asp:Content>
