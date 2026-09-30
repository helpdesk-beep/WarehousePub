<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Welcome_DashBoard.aspx.cs" Inherits="StatePages_Welcome_DashBoard" %>

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

<%--<script language="JavaScript"> var message = 'Right Click is disabled';
function clickIE() { if (event.button == 2) { alert(message); return false; } }
function clickNS(e) {
if (document.layers || (document.getElementById && !document.all)) {
if (e.which == 2 || e.which == 3) { alert(message); return false; }
}
}
if (document.layers) { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
else if (document.all && !document.getElementById) { document.onmousedown = clickIE; }
document.oncontextmenu = new Function('alert(message);return false') </script>--%>
    
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
                <span style="color: #FFFFFF; font-weight: bolder; font-size: 20px; font-family:Arial;">Stock Position Dashboard In (Lakh M.T.)</span>
                </td>
            </tr>
         
            <tr>
                        <td style="height:30px; font-size:14px" colspan="3" align="right" >
                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" 
                                onclick="LinkButton1_Click" >Quantity In Lakh M.T</asp:LinkButton>                       
                        <div style="width:100%;">
                        <img id="Img1" src="../Images/line.png" height="25px" width="100%" alt="" />
                        </div>
                        </td>
            </tr> 
            
            
            <tr>
                <td  colspan="3" align="center" >
                       <table width="95%">
                           <tr>
                                <td colspan="5" style="color:Black; font-size:20px; font-weight:bold ;text-align:Center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Wheat PSS Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                           <tr>
                               <td colspan="5" style="width:5px"></td>
                           </tr>
                            <tr>
                                <td style="background-color:#7bd943; height:160px; width:32%; border-radius: 10px; " class="td" align="center">

                                    <asp:Label ID="Wheat" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />
                                    <asp:Label ID="Label2" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Stock"></asp:Label><br />
                                </td>
                                <td style="width:5px">
                                </td>                                
                                <td style="background-color:#7bd943; height:160; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="WheatCovered" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true"  runat="server" Text="000"></asp:Label><br /><br />
                                    <asp:Label ID="Label6" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true"  runat="server" Text="Stock in Covered Godown"></asp:Label><br />
                                </td> 
                                <td style="width:5px">
                                </td>                                 
                                <td style="background-color:#7bd943; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="WheatCap" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label5" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Cap Storage"></asp:Label><br />
                                </td>                                                                                                 
                            </tr> 
                            
                            <tr><td style="height:20px;"></td></tr>
                            <tr>
                                <td colspan="5" style="color:Black; font-size:20px; font-weight:bold ;text-align:Center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Paddy Common Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                           <tr>
                               <td colspan="5" style="width:5px"></td>
                           </tr>
                            <tr>

                                <td style="background-color:#F5B041; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    
                                     <asp:Label ID="Paddy" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />  
                                    <asp:Label ID="Label14" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Stock"></asp:Label><br />
                                </td>    
                                <td style="width:5px">
                                </td>
                                <td style="background-color:#F5B041; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                     <asp:Label ID="PaddyCovered" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />
                                    <asp:Label ID="Label17" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Covered Godown"></asp:Label><br />
                                </td> 
                                <td style="width:5px">
                                </td>                                 
                                <td style="background-color:#F5B041; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="PaddyCap" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label9" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Cap Storage"></asp:Label><br />
                                </td>
                            </tr>
                            <tr><td style="height:20px;"></td></tr>
                           <tr>
                                <td colspan="5" style="color:Black; font-size:20px; font-weight:bold ;text-align:Center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Rice Raw Common Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                           <tr>
                               <td colspan="5" style="width:5px"></td>
                           </tr>
                            <tr>
                            
                                <td style="background-color:#ff5542; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="Rice" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label8" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Stock"></asp:Label><br />
                                </td>    
                                <td style="width:5px">
                                </td>                                                          
                            
                                <td style="background-color:#ff5542; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="RiceCovered" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />  
                                    
                                    <asp:Label ID="Label11" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Covered Godown"></asp:Label><br />
                                </td>
                           <td style="width:5px">
                                </td>                                 
                                <td style="background-color:#ff5542; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="RiceCap" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label13" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Cap Storage"></asp:Label><br />
                                </td>
                            
                            </tr>       
                           
                           <tr><td style="height:20px;"></td></tr>
                           <tr>
                                <td colspan="5" style="color:Black; font-size:20px; font-weight:bold ;text-align:Center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Maize Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                           <tr>
                               <td colspan="5" style="width:5px"></td>
                           </tr>
                            <tr>
                            
                                <td style="background-color:#ff9900; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="Maze" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label15" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Stock"></asp:Label><br />
                                </td>    
                                <td style="width:5px">
                                </td>                                                          
                            
                                <td style="background-color:#ff9900; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="MazeCovered" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />  
                                    
                                    <asp:Label ID="Label18" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Covered Godown"></asp:Label><br />
                                </td>
                           <td style="width:5px">
                                </td>                                 
                                <td style="background-color:#ff9900; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="MazeCap" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label21" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Cap Storage"></asp:Label><br />
                                </td>
                            
                            </tr>    

                           <tr><td style="height:20px;"></td></tr>
                           <tr>
                                <td colspan="5" style="color:Black; font-size:20px; font-weight:bold ;text-align:Center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Bajra Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                           <tr>
                               <td colspan="5" style="width:5px"></td>
                           </tr>
                            <tr>
                            
                                <td style="background-color:#003300; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="Bajra" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label22" Font-Size="16px" Font-Names="Arial" ForeColor="White"  runat="server" Text="Total Stock"></asp:Label><br />
                                </td>    
                                <td style="width:5px">
                                </td>                                                          
                            
                                <td style="background-color:#003300; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="BajraCovered" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />  
                                    
                                    <asp:Label ID="Label23" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Covered Godown"></asp:Label><br />
                                </td>
                           <td style="width:5px">
                                </td>                                 
                                <td style="background-color:#003300; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="BajraCap" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label25" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Cap Storage"></asp:Label><br />
                                </td>
                            
                            </tr>    

                           <tr><td style="height:20px;"></td></tr>
                           <tr>
                                <td colspan="5" style="color:Black; font-size:20px; font-weight:bold ;text-align:Center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Jowar Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                           <tr>
                               <td colspan="5" style="width:5px"></td>
                           </tr>
                            <tr>
                            
                                <td style="background-color:#003366; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="Jowar" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label26" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Stock"></asp:Label><br />
                                </td>    
                                <td style="width:5px">
                                </td>                                                          
                            
                                <td style="background-color:#003366; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="JowarCovered" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />  
                                    
                                    <asp:Label ID="Label27" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Covered Godown"></asp:Label><br />
                                </td>
                           <td style="width:5px">
                                </td>                                 
                                <td style="background-color:#003366; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                   <asp:Label ID="JowarCap" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label29" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Cap Storage"></asp:Label><br />
                                </td>
                            
                            </tr>    

                           <tr><td style="height:20px;"></td></tr>
                           <tr>
                                <td colspan="5" style="color:Black; font-size:20px; font-weight:bold ;text-align:Center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Gram Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                           <tr>
                               <td colspan="5" style="width:5px"></td>
                           </tr>
                            <tr>
                            
                                <td style="background-color:#666699; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="Gram" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label30" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Stock"></asp:Label><br />
                                </td>    
                                <td style="width:5px">
                                </td>                                                          
                            
                                <td style="background-color:#666699; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                   <asp:Label ID="GramCovered" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />  
                                    
                                    <asp:Label ID="Label31" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Covered Godown"></asp:Label><br />
                                </td>
                           <td style="width:5px">
                                </td>                                 
                                <td style="background-color:#666699; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="GramCap" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label33" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Cap Storage"></asp:Label><br />
                                </td>
                            
                            </tr>    

                           <tr><td style="height:20px;"></td></tr>
                           <tr>
                                <td colspan="5" style="color:Black; font-size:20px; font-weight:bold ;text-align:Center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Lentil Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                           <tr>
                               <td colspan="5" style="width:5px"></td>
                           </tr>
                            <tr>
                            
                                <td style="background-color:#009999; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="Lentil" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label34" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Total Stock"></asp:Label><br />
                                </td>    
                                <td style="width:5px">
                                </td>                                                          
                            
                                <td style="background-color:#009999; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="LentilCovered" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />  
                                    
                                    <asp:Label ID="Label35" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Covered Godown"></asp:Label><br />
                                </td>
                           <td style="width:5px">
                                </td>                                 
                                <td style="background-color:#009999; height:160px; width:32% ;border-radius: 10px;" class="td" align="center">
                                    <asp:Label ID="LentilCap" Font-Size="28px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="000"></asp:Label><br /><br />                                     
                                    
                                    <asp:Label ID="Label37" Font-Size="16px" Font-Names="Arial" ForeColor="White" Font-Bold="true" runat="server" Text="Stock in Cap Storage"></asp:Label><br />
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
                    <table style="width:98%;">
<tr>
                <td style="vertical-align:top; width:49%; font-family:Arial" class="pop" >
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:Center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Wheat PSS Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD1" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True" 
                                        onselectedindexchanged="GD1_SelectedIndexChanged"  >
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
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
                <td style="vertical-align:top; width:49%; font-family:Arial" class="pop" >
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:Center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Wheat PSS Stock in Covered Godown Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD2" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True" 
                                        onselectedindexchanged="GD2_SelectedIndexChanged"  >
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>                                      
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true" />
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td>                
            </tr> 
            <tr>
                        <td style="height:15px;">
                        </td>            
            </tr>
<tr>

                <td style="vertical-align:top; width:49%;font-family:Arial" class="pop">
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Wheat PSS Stock in Cap Storage Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD3" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True" 
                                        onselectedindexchanged="GD3_SelectedIndexChanged"  >
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>                                                                             
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true"/>
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td> 
                <td style="vertical-align:top; width:10px;">
                
                </td>            
                <td style="vertical-align:top; width:49%; font-family:Arial" class="pop">
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Paddy Common Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD4" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True" 
                                        onselectedindexchanged="GD4_SelectedIndexChanged">
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>                                       
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center"  VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true"/>
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td>  
            </tr>     
            <tr>
                        <td style="height:15px;">
                        </td>            
            </tr> 
                    
            <tr>
                <td style="vertical-align:top; width:49%; font-family:Arial" class="pop">
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Paddy Common Stock in Covered Godown Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD5" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True"  
                                        onselectedindexchanged="GD5_SelectedIndexChanged">
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>                                       
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true"/>
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td>             
                            <td style="vertical-align:top; width:5px;">
                
                </td>  
                <td style="vertical-align:top; width:49%; font-family:Arial" class="pop">
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Paddy Common Stock in Cap Storage Position As On <%=hdnDate.Value %> 
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD6" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True"  
                                        onselectedindexchanged="GD6_SelectedIndexChanged">
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>                                       
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center"  VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true"/>
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td>  
            </tr>   
            <tr>
                        <td style="height:15px;">
                        </td>            
            </tr>
            <tr>
                            <td style="vertical-align:top; width:49%; font-family:Arial" class="pop">
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Rice Raw Common Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD7" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True" 
                                        onselectedindexchanged="GD7_SelectedIndexChanged">
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>                                      
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true"/>
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td> 
                <td style="vertical-align:top; width:5px;">
                
                </td>  
                <td style="vertical-align:top; width:49%; font-family:Arial" class="pop">
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Maze Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD8" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True" 
                                        onselectedindexchanged="GD8_SelectedIndexChanged">
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>                                    
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center"  VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true"/>
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td>  
            </tr>   
                        <tr>
                        <td style="height:15px;">
                        </td>            
            </tr>
            <tr>
                            <td style="vertical-align:top; width:49%; font-family:Arial" class="pop">
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Bajra Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD9" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True"
                                        onselectedindexchanged="GD9_SelectedIndexChanged">
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>                                     
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true"/>
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td> 
                <td style="vertical-align:top; width:5px;">
                
                </td>  
                <td style="vertical-align:top; width:49%; font-family:Arial" class="pop">
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Jowar Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD10" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True"
                                        onselectedindexchanged="GD10_SelectedIndexChanged">
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                           <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>                                      
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center"  VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true"/>
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td>  
            </tr> 
                        <tr>
                        <td style="height:15px;">
                        </td>            
            </tr>
            <tr>
                            <td style="vertical-align:top; width:49%; font-family:Arial" class="pop">
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Gram Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD11" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True"
                                        onselectedindexchanged="GD11_SelectedIndexChanged">
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>                                       
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true"/>
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td> 
                <td style="vertical-align:top; width:5px;">
                
                </td>  
                <td style="vertical-align:top; width:49%; font-family:Arial" class="pop">
                          <table width="100%" style="background-color:White; height:250px">
                            <tr>
                                <td style="color:Black; font-size:20px; font-weight:bold ;text-align:center; height:25px; border-bottom:solid 1px #CCCCCC; background-color:#ffe14e" >Lentil Stock Position As On <%=hdnDate.Value %>
                                </td>                            
                            </tr>
                            <tr>
                                <td align="center" class="td">
                                    <asp:GridView ID="GD12" runat="server" AutoGenerateColumns="False" width="90%" 
                                        DataKeyNames="CropYear" BorderStyle="Double" CellPadding="8" CellSpacing="10" 
                                        Font-Size="Small" BorderColor="#CCCCCC" ShowFooter="True"
                                        onselectedindexchanged="GD12_SelectedIndexChanged">
                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StockPosition" HeaderText="Available Stock">                                                           
                                                        <ItemStyle Width="50%"></ItemStyle>
                                            </asp:BoundField>                                     
                                        </Columns>                                    
                                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center"  VerticalAlign="Middle" />
                                        <FooterStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Bold="true"/>
                                    </asp:GridView>
                                </td>
                            </tr>
                          </table>                 
                </td>  
            </tr> 
                    </table>
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
    <asp:HiddenField ID="hdnDate" runat="server" Value="" />
</asp:Content>


