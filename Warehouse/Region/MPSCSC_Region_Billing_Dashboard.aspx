<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="MPSCSC_Region_Billing_Dashboard.aspx.cs" Inherits="Region_MPSCSC_Region_Billing_Dashboard"  Title="MPSCSC_Region_Billing_Dashboard"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 232px;
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
            <tr>
                <td align="center" style="border:#FFFFFF ; border-style:solid ; border-width:2px; background-color:#5D6D7E; height:25px;" colspan="3">
                <span style="color: #FFFFFF; font-weight: bolder; font-size: 16px; font-family:Arial;">MPSCSC Billing Dashboard</span>
                </td>
            </tr>
         
            <tr>
                        <td style="height:30px; font-size:14px" colspan="3" align="right" >
                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" 
                                 ></asp:LinkButton>                       
                        <div style="width:100%;">
                        <img id="Img1" src="../Images/line.png" height="25px" width="100%" alt="" />
                        </div>
                        </td>
            </tr> 
            
            
            <tr>
                <td  colspan="3" align="center" >
                       <table width="95%">
                            <tr>
                                <td style="background-color:#7bd943; height:160px; width:32%; border-radius: 10px; " class="td" align="center">

                                    <asp:LinkButton ID="Label1" Font-Size="28px" Font-Names="Arial" 
                                        ForeColor="White" ToolTip="Click To View Details" Font-Underline="false"  
                                        runat="server" Text="" Font-Bold="true" ></asp:LinkButton><br /><br />                                     
                                    <asp:Label ID="Label2" Font-Size="16px" Font-Names="Arial" ForeColor="White"  runat="server" Text="Total Generated Bill"></asp:Label><br />
                                    <asp:Label ID="Label3" Font-Size="14px" Font-Names="Arial" ForeColor="White"  runat="server" Text=""></asp:Label><br />
                                </td>
                                <td style="width:5px">
                                </td>                                
                                <td style="background-color:#2980B9; height:160; width:32% ;border-radius: 10px;" class="td" align="center">

                                    <asp:LinkButton ID="Label21" Font-Size="28px" Font-Names="Arial" 
                                        ForeColor="White" ToolTip="Click To View Details" Font-Underline="false"  
                                        runat="server" Text="" Font-Bold="true" ></asp:LinkButton><br /><br />
                                    <asp:Label ID="Label6" Font-Size="16px" Font-Names="Arial" ForeColor="White"  runat="server" Text="Total Submitted Bills at Branch "></asp:Label><br />
                                   <%-- <asp:Label ID="Label18" Font-Size="14px" Font-Names="Arial" ForeColor="White"  runat="server" Text="Crop Year : 2016-17,2017-18,2018-19,2019-20"></asp:Label><br />--%>
                                </td> 
                                <td style="width:5px">
                                </td>                                 
                                <td style="background-color:#B2BABB; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:LinkButton ID="Label4" Font-Size="28px" Font-Names="Arial" 
                                        ForeColor="White" ToolTip="Click To View Details" Font-Underline="false"  
                                        runat="server" Text="" Font-Bold="true"></asp:LinkButton><br /><br />                                     
                                    
                                    <asp:Label ID="Label5" Font-Size="16px" Font-Names="Arial" ForeColor="White"  runat="server" Text=""></asp:Label><br />
                                    <%--<asp:Label ID="Label6" Font-Size="14px" Font-Names="Arial" ForeColor="White"  runat="server" Text="Crop Year : 2016-17,2017-18,2018-19,2019-20"></asp:Label><br />--%>
                                </td>                                                                                                 
                            </tr> 
                            
                            <tr><td style="height:20px;"></td></tr>
                            
                            <tr>

                                <td style="background-color:#F5B041; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    
                                     <asp:LinkButton ID="Label20" Font-Size="28px" Font-Names="Arial" ForeColor="White" ToolTip="Click To View Details" Font-Underline="false"  runat="server" Text="" Font-Bold="true"></asp:LinkButton><br /><br />  
                                    <asp:Label ID="Label14" Font-Size="16px" Font-Names="Arial" ForeColor="White"  runat="server" Text=""></asp:Label><br />
                                   <%-- <asp:Label ID="Label15" Font-Size="14px" Font-Names="Arial" ForeColor="White"  runat="server" Text="Crop Year : 2016-17,2017-18,2018-19,2019-20"></asp:Label><br />--%>
                                </td>    
                                <td style="width:5px">
                                </td>
                                <td style="background-color:#208c96; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:LinkButton ID="Label16" Font-Size="28px" Font-Names="Arial" ForeColor="White" ToolTip="Click To View Details" Font-Underline="false"  runat="server" Text="" Font-Bold="true"></asp:LinkButton><br /><br />
                                    <asp:Label ID="Label17" Font-Size="16px" Font-Names="Arial" ForeColor="White"  runat="server" Text=""></asp:Label><br />
                                   <%-- <asp:Label ID="Label18" Font-Size="14px" Font-Names="Arial" ForeColor="White"  runat="server" Text="Crop Year : 2016-17,2017-18,2018-19,2019-20"></asp:Label><br />--%>
                                </td>                                                                                              
                            </tr>
                            <tr><td style="height:20px;"></td></tr>
                            <tr>
                            
                                <td style="background-color:#ff5542; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:LinkButton ID="Label7" Font-Size="28px" Font-Names="Arial" ForeColor="White" ToolTip="Click To View Details" Font-Underline="false"  runat="server" Text="" Font-Bold="true"></asp:LinkButton><br /><br />                                     
                                    
                                    <asp:Label ID="Label8" Font-Size="16px" Font-Names="Arial" ForeColor="White"  runat="server" Text=""></asp:Label><br />
                                    
                                </td>    
                                <td style="width:5px">
                                </td>                                                          
                            
                                <td style="background-color:#e44a94; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:LinkButton ID="Label10" Font-Size="28px" Font-Names="Arial" 
                                        ForeColor="White" ToolTip="Click To View Details" Font-Underline="false"  
                                        runat="server" Text="" Font-Bold="true"></asp:LinkButton><br /><br />  
                                    
                                    <asp:Label ID="Label11" Font-Size="16px" Font-Names="Arial" ForeColor="White"  runat="server" Text=""></asp:Label><br />
                                    <asp:Label ID="Label12" Font-Size="14px" Font-Names="Arial" ForeColor="White"  runat="server" Text=""></asp:Label><br />
                                </td>
                           
                            
                            </tr>                                                             
                       </table>
                </td>
            </tr> 
            <tr>
                        <td style="height:10px;" colspan="3">

                        </td>
            </tr>                        
            <tr>
                        <td style="height:40px;" colspan="3">
                        <div style="width:100%;">
                        <img id="Img3" src="../Images/line.png" height="25px" width="100%" alt="" />
                        </div>
                        </td>
            </tr>
            
            <tr>
            <td colspan="3" align="center">
                    
            </td>           
            </tr>
                                   
            
            <tr>
                        <td style="height:15px;">
                        </td>            
            </tr>             
                        
          </table>
        </div>
     </center>
</fieldset>
</asp:Content>