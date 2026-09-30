<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="CheckGdwnLink_JVSRegID.aspx.cs" Inherits="Reports_Branch_CheckGdwnLink_JVSRegID" Title="Untitled Page" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

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
    width:250px;
    height:120px;
     border-radius: 25px;
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
</style>


<fieldset style="width: 1000px; border: 2px solid navy; margin-left: 10px ; margin-right:10px">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
               
                    <%--      ----------Start  JVS Lic -----------------------%>
                    <tr id="trjvslic" runat="server">
                        <td align="center" valign="top">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center" >
                                                    <asp:Label ID="lblGodownMaster" runat="server" Text="View Private Godown link to JVS Registration ID" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                    <tr>
                        <td colspan="4" align="left"> <p style=" color:Red; font-size:14px">नोट :- <br />1.यदि WHR बनाते समय यदि यह मेसेज आ रहा हे (You Cannot Deposite Greater Then Allow Capacity) कृपया उक्त गोदाम  RM लोगिन से लिंक कराएँ  ।<br />
                        2.यदि  एग्रीमेंट छमता क़े विरुध 125% तक WHR बनाई जा चुकी हे  तो उक्त गोदाम से WHR बनाना संभव नहीं हैं | यदि गलत गोदाम लिंक हे तो HO से unlink कराए । <br />
                        3.ड्रॉपडाउन मे वह रजिस्ट्रेशन आईडी सेलेक्ट करें । <br />
                        4.जिन  गोदाम का JVS में निरीक्षण एवम्‌ एग्रीमेंट अपडेट कर लिया गया है वहि रजिस्ट्रेशन आईडी प्रदर्शित होगी ।</p>
                        </td>
                    </tr>                                            
                                            
  
                                                                                        <tr>
                                                <td style="height: 50px ; font-size:14px" colspan="4" align="center">
                                                    
                                                    
                                                 &nbsp;&nbsp;&nbsp; JVS Registration ID : &nbsp;&nbsp;<asp:DropDownList 
                                                        ID="ddlRegID" runat="server" 
                                                        Height="25px" Width="300px"  AutoPostBack="true" 
                                                        onselectedindexchanged="ddlRegID_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr> 
                                            
                                            <tr>
                                                <td style="height: 5px" colspan="4"></td>
                                            </tr>
<tr id="trhide1" runat="server" visible="false">
    <td colspan="4" align="center">
      <table cellpadding="0" cellspacing="0" style="width: 100%">                                            
                                           <%-- ----------Start New Table Here---------------%>
                                               <tr >
                                                <td colspan="4" align="center" >
                                                    <asp:Label ID="lblgdrowcount" runat="server" Font-Size="10pt" ></asp:Label>
                                                </td>
                                            </tr> 
        <tr>
        <td style="height: 10px ">
        
        </td>
        </tr>                                                                                                                           
                                                                                        
      <tr>
        <td colspan="4" valign="top" align="center">
          <asp:GridView ID="Depositor_Gridview"  runat="server" DataKeyNames="Godownid" AutoGenerateColumns="False" Width="70%"  BackColor="White" 
                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" 
    CellSpacing="2" PageSize="50">
            <Columns>
              <asp:BoundField DataField="Godownid" HeaderText="Godown ID" ReadOnly="True" SortExpression="Godownid" />
              <asp:BoundField DataField="Godown_name" HeaderText="Godown Name" ReadOnly="True" SortExpression="Godown_name" />
              <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" ReadOnly="True" SortExpression="Hired Type" />
              <asp:BoundField DataField="WHRQty" HeaderText="WHRQty" ReadOnly="True" SortExpression="WHRQty" />             

            </Columns>
            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center" Wrap="true"
                                                            Height="20px" Font-Size="11px" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
          </asp:GridView>
        </td>
        </tr> 
        <tr>
        <td style="height: 20px ">
        
        </td>
        </tr>

                                                    <tr id="tr1" runat="server">
                                                    
                                                <td style="height: 30px ; font-size:14px" colspan="4" align="left">
                                                    <asp:Label ID="Label2" runat="server" Text="उक्त रजिस्ट्रेशन  आईडि क़े विरुध कुल ऑनलाइन एग्रीमेंट क्षमता (In M.T): "></asp:Label> 
                                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp 
                                                    <asp:TextBox ID="txtagrcpt" runat="server"  ReadOnly="true"
                                                        Width="150px" Height="20px" MaxLength="10"></asp:TextBox>

                                                </td>
                                                
                                            </tr>
                                            
                                            
                                            
                                            <tr id="tr22" runat="server">
                                               <td style="height: 30px ; font-size:14px" colspan="4" align="left">
                                                    
                                                   उक्त रजिस्ट्रेशन आईडि सें लिंक WHMS गोडाउन आईडि से जारी कुल WHR की क्षमता (M.T) : &nbsp&nbsp&nbsp 
                                                    <asp:TextBox ID="txtWHRCpt" runat="server"  ReadOnly="true"
                                                        Width="150px" Height="20px" MaxLength="10"></asp:TextBox>
                                               </td>
                                            </tr>
                                                                
                                                      
                                             
        
        
        <%-----------------End New Table Here----------------%>

      
      </table>
    </td>
</tr>

</table>
</div>
</center>

      </td>
      </tr>

                                     <tr>
                                          <td style="height: 5px" colspan="4">
                                          </td>
                                     </tr>
                                     
                                     
    </table>     
      
                                    </div>
                                </center>
    </fieldset>

</asp:Content>
