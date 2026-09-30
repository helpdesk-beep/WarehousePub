<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="UpdateJVSGodownLicNoExpDate.aspx.cs" Inherits="Region_UpdateJVSGodownLicNoExpDate" Title="Untitled Page" %>
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
                    <tr>
                        <td colspan="4" align="left"> <p style=" color:Red; font-size:14px">नोट :- <br />1.वेयरहाउस मॉडुल में जिस गोदाम में लायसेंस अपडेट करना हे क्रिप्या ड्रॉपडाउन मे गोदाम का प्रकार सेलेक्ट करें । <br />
                        </p>
                        </td>
                    </tr>
                                    
                <tr>
                    <td align="center" style="font-size:14px">
                       Godown Type : &nbsp;&nbsp;<asp:DropDownList 
                                                        ID="ddlgdwntype" runat="server" 
                                                        Height="25px" Width="180px"  
                            AutoPostBack="true" onselectedindexchanged="ddlgdwntype_SelectedIndexChanged" >
                                                        <asp:ListItem Text="--Select--" Value="--Select--" > </asp:ListItem>
                                                        <asp:ListItem Text="JVS/WDRA" Value="JVS" > </asp:ListItem>
                                                         <asp:ListItem Text="Other Then JVS/WDRA" Value="OWN" > </asp:ListItem>
                                                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height:15px">
                    
                    </td>
                </tr>                
                    <%--      ----------Start  JVS Lic -----------------------%>
                    <tr id="trjvslic" runat="server" visible="false">
                        <td align="center" valign="top">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center" >
                                                    <asp:Label ID="lblGodownMaster" runat="server" Text="Update JVS/WDRA Godown Licence Number and Validity" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                    <tr>
                        <td colspan="4" align="left"> <p style=" color:Red; font-size:14px">नोट :- <br />1.वेयरहाउस मॉडुल में जिस ब्रांच क़े गोदाम में यह  लायसेंस अपडेट करना हे क्रिप्या ड्रॉपडाउन मे वह ब्रांच एवम्‌ गोदाम आईडी सेलेक्ट करें । <br />
                        2.जिन  गोदाम का JVS में निरीक्षण कर लिया गया है वहि रजिस्ट्रेशन आईडी प्रदर्शित होगी |</p>
                        </td>
                    </tr>                                            
                                            
  
                                                                                        <tr>
                                                <td style="height: 50px ; font-size:14px" colspan="4" align="center">
                                                    
                                                  &nbsp;&nbsp;&nbsp;&nbsp  Branch : &nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList 
                                                        ID="ddlBranch" runat="server" AutoPostBack="true"
                                                        Height="25px" Width="168px" onselectedindexchanged="ddlBranch_SelectedIndexChanged"> </asp:DropDownList>
                                                    
                                                 &nbsp;&nbsp;&nbsp; JVS Registration ID : &nbsp;&nbsp;<asp:DropDownList 
                                                        ID="ddlRegID" runat="server" 
                                                        Height="25px" Width="168px"  AutoPostBack="true" 
                                                        onselectedindexchanged="ddlRegID_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr> 
                                            
                                            <tr>
                                                <td style="height: 5px" colspan="4"></td>
                                            </tr>                                       
                                                                                        
      <tr>
        <td colspan="4" valign="top" align="center">
  <%--        <asp:GridView ID="Depositor_Gridview"  runat="server"
            DataKeyNames="Godown_ID" 
            AutoGenerateColumns="False" Width="70%"  BackColor="White" 
                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" 
    CellSpacing="2" PageSize="50" 
                onselectedindexchanged="Depositor_Gridview_SelectedIndexChanged">
            <Columns>
              <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" ReadOnly="True" SortExpression="Godown_ID"/>
              <asp:BoundField DataField="Godown_Name" HeaderText="Godown_Name" ReadOnly="True" SortExpression="Godown_Name" />
              <asp:BoundField DataField="Hired_Type" HeaderText="Hired_Type" ReadOnly="True" SortExpression="Hired_Type" />
              <asp:BoundField DataField="Storage_Type" HeaderText="Storage_Type" ReadOnly="True" SortExpression="Storage_Type" />
              <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Godown_Scientific_Capacity" ReadOnly="True" SortExpression="Godown_Scientific_Capacity" />
              <asp:BoundField DataField="Premise_capacity" HeaderText="Premise_capacity" ReadOnly="True" SortExpression="Premise_capacity" />
              <asp:BoundField DataField="Closing_Balance" HeaderText="Closing_Balance" ReadOnly="True" SortExpression="Closing_Balance" />
             
              <asp:CommandField SelectText="Select" HeaderText="Delete" ShowSelectButton="True" >
                   <ControlStyle Font-Bold="True" ForeColor="Red" />
             </asp:CommandField>
            </Columns>
            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
          </asp:GridView>
          
          --%>
          
          
          
          <asp:GridView ID="Depositor_Gridview"  runat="server" DataKeyNames="Warehouse_name" AutoGenerateColumns="False" Width="90%"  BackColor="White" 
                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" 
    CellSpacing="2" PageSize="50" onselectedindexchanged="Depositor_Gridview_SelectedIndexChanged">
            <Columns>
              <asp:BoundField DataField="Warehouse_name" HeaderText="Warehouse Name" ReadOnly="True" SortExpression="Warehouse_name" />
              <asp:BoundField DataField="Godown_No" HeaderText="Godown No" ReadOnly="True" SortExpression="Godown_No" />
              <asp:BoundField DataField="Present_Validity_WDRA" HeaderText="WDRA Lic. Validity" ReadOnly="True" SortExpression="Present_Validity_WDRA" />
              <asp:BoundField DataField="WDRA_LicenseNo" HeaderText="WDRA Licencse No" ReadOnly="True" SortExpression="WDRA_LicencseNo" />
              <asp:BoundField DataField="WDRA_LicenseDate" HeaderText="WDRA Lic. Expiry Date" ReadOnly="True" SortExpression="WDRA_LicenseDate" ItemStyle-Width="100px"/>
              <asp:BoundField DataField="WDRAL_Present_Validity" HeaderText="State Lic Validity" ReadOnly="True" SortExpression="WDRAL_Present_Validity" />
              <asp:BoundField DataField="Warehouse_LicenseNo" HeaderText="State Licence No" ReadOnly="True" SortExpression="Waregouse_LicenseMo" />
              <asp:BoundField DataField="Warehouse_licenseDate" HeaderText="State Lic. Expiry Date" ReadOnly="True" SortExpression="Warehouse_licenseDate" />              
              <asp:CommandField SelectText="Select" HeaderText="Select" ShowSelectButton="True" >
                   <ControlStyle Font-Bold="True" ForeColor="Red" />
             </asp:CommandField>             

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

                                                    <tr id="tr1" runat="server" visible="false">
                                                    
                                                <td style="height: 50px ; font-size:14px" colspan="4" align="center">
                                                    <asp:Label ID="Label2" runat="server" Text="Licence No. : "></asp:Label> 
                                                    &nbsp&nbsp&nbsp 
                                                    <asp:TextBox ID="txtlicno" runat="server"  ReadOnly="true"
                                                        Width="150px" Height="20px" MaxLength="10"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;
                                                   Licence Validity Date : &nbsp&nbsp&nbsp 
                                                    <asp:TextBox ID="txtlicexpdate" runat="server"  ReadOnly="true"
                                                        Width="150px" Height="20px" MaxLength="10"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                                    

        
                                                    <tr id="trmobtxt" runat="server" visible="false">
                                                    
                                                <td style="height: 60px ; font-size:14px" colspan="4" align="center">
                                                    <asp:Label ID="Label1" runat="server" Text="WHMS Godown ID: "></asp:Label> 
                                                    <asp:DropDownList 
                                                        ID="ddlWHMSGdwnID" runat="server" 
                                                        Height="25px" Width="300px"  AutoPostBack="true"
                                                        onselectedindexchanged="ddlWHMSGdwnID_SelectedIndexChanged">
                                                    </asp:DropDownList> &nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="lblgdid" runat="server"></asp:Label> 
                                                </td>
                                                
                                            </tr>  
                                     <tr>
                                          <td style="height: 5px" colspan="4">
                                          </td>
                                     </tr>        
        <tr id="trbtnhide" runat="server" visible="false">
        
           <td align="Right">
<asp:Button class="button button1" id="btnAddCompany" style="width:100px" runat="server" 
                   Text="Update" Height="29px" onclick="btnAddCompany_Click" 
                                       ></asp:Button>&nbsp&nbsp&nbsp&nbsp
          </td>  
                                   <td align="left" >
                            
<asp:Button class="button button2" id="btnGenerateBill" style="width:100px" runat="server" Text="Close" Height="29px" 
                                           onclick="btnGenerateBill_Click"></asp:Button></td>      
        </tr>

</table>
</div>
</center>

      </td>
      </tr>
      
      <%--      ----------End JVS Lic -----------------------%>
      
<%--      ----------Update Owned Lic -----------------------%>
      
<tr id="trownlic" runat="server" visible="false">
                        <td align="center" valign="top">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center" >
                                                    <asp:Label ID="Label3" runat="server" Text="Update Other Then JVS and WDRA Godown Licence Number and Validity" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                    <tr>
                        <td colspan="4" align="left"> <p style=" color:Red; font-size:14px">नोट :- <br />1.वेयरहाउस मॉडुल में जिस जिला एवम्‌ ब्रांच में गोदाम का  लायसेंस अपडेट करना हे क्रिप्या वह जिला एवम्‌ ब्रांच  सेलेक्ट करें । <br />
                        2.लायसेंस नंबर मे , जिले में जारी वेयरहाउस  लायसेंस की सूची उप्लब्ध है । 
                        </p>
                        </td>
                    </tr>                                            
                                            
  
                                                                                        <tr>
                                                <td style="height: 50px ; font-size:14px" colspan="4" align="center">
                                                    &nbsp;&nbsp;&nbsp;&nbsp  District : &nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList 
                                                        ID="DDLDistrict" runat="server" 
                                                        Height="25px" Width="168px"  AutoPostBack="true"
                                                        onselectedindexchanged="DDLDistrict_SelectedIndexChanged1"> </asp:DropDownList>
                                                    
                                                  &nbsp;&nbsp;&nbsp;&nbsp  Branch : &nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList 
                                                        ID="ddlbranchOther" runat="server" 
                                                        Height="25px" Width="168px"  AutoPostBack="true"
                                                        onselectedindexchanged="ddlbranchOther_SelectedIndexChanged"> </asp:DropDownList>
                                                    
                                                 &nbsp;&nbsp;&nbsp; Licence No : &nbsp;&nbsp;<asp:DropDownList 
                                                        ID="ddlOtherJvs" runat="server" 
                                                        Height="25px" Width="168px"  AutoPostBack="true" 
                                                        onselectedindexchanged="ddlOtherJvs_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                </td>
                                            </tr> 
                                            
                                            <tr>
                                                <td style="height: 5px" colspan="4"></td>
                                            </tr> 
                                                                                  
                                                                                        
      <tr>
        <td colspan="4" valign="top" align="center">
          <asp:GridView ID="GridView1"  runat="server" AutoGenerateColumns="False" 
                Width="90%"  BackColor="White" 
                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" 
                CellPadding="5"  CellSpacing="2" PageSize="50" 
                onselectedindexchanged="GridView1_SelectedIndexChanged1">
            <Columns>
              <asp:BoundField DataField="Whr_Name" HeaderText="Whr_Name" ReadOnly="True" SortExpression="Whr_Name" />
              <asp:BoundField DataField="Name_of_Owner" HeaderText="Name_of_Owner" ReadOnly="True" SortExpression="Name_of_Owner" />
              <asp:BoundField DataField="Whr_ID" HeaderText="Whr_ID" ReadOnly="True" SortExpression="Whr_ID" />
              <asp:BoundField DataField="ExpDate" HeaderText="ExpDate" ReadOnly="True" SortExpression="ExpDate" />
              <asp:BoundField DataField="Whr_Address" HeaderText="Whr_Address" ReadOnly="True" SortExpression="Whr_Address" />
              <asp:BoundField DataField="Total_capicity" HeaderText="Total_capicity" ReadOnly="True" SortExpression="Total_capicity" />
              
              <asp:CommandField SelectText="Select" HeaderText="Select" ShowSelectButton="True" >
                   <ControlStyle Font-Bold="True" ForeColor="Red" />
             </asp:CommandField>             

            </Columns>
            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center" Wrap="true"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
          </asp:GridView>
        </td>
        </tr> 

                                                    <tr id="tr3" runat="server" visible="false">
                                                    
                                                <td style="height: 50px ; font-size:14px" colspan="4" align="center">
                                                    <asp:Label ID="Label4" runat="server" Text="Licence No. : "></asp:Label> 
                                                    &nbsp&nbsp&nbsp 
                                                    <asp:TextBox ID="txtlicnoother" runat="server"  ReadOnly="true"
                                                        Width="150px" Height="20px" MaxLength="10"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;
                                                   Licence Validity Date : &nbsp&nbsp&nbsp 
                                                    <asp:TextBox ID="txtlicexpother" runat="server" 
                                                        Width="150px" Height="20px" MaxLength="10" onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
                         TargetControlID="txtlicexpother"></cc1:CalendarExtender>                                                        
                                                </td>
                                                
                                            </tr>
                                                    

        
                                                    <tr id="tr4" runat="server" visible="false">
                                                    
                                                <td style="height: 50px ; font-size:14px" colspan="4" align="center">
                                                    <asp:Label ID="Label5" runat="server" Text="WHMS Godown ID: "></asp:Label> 
                                                    <asp:DropDownList 
                                                        ID="ddlwhmsOthergdwn" runat="server" 
                                                        Height="25px" Width="320px"  AutoPostBack="true" 
                                                        onselectedindexchanged="ddlwhmsOthergdwn_SelectedIndexChanged">
     
                                                    </asp:DropDownList>
           &nbsp&nbsp&nbsp&nbsp <asp:Label ID="lblothergdwnid" runat="server"></asp:Label> 
                                                </td>
                                                
                                            </tr>  
                                     <tr>
                                          <td style="height: 5px" colspan="4">
                                          </td>
                                     </tr>        
        <tr id="tr5" runat="server" visible="false">
        
           <td align="Right">
<asp:Button class="button button1" id="Button1" style="width:100px" runat="server" 
                   Text="Update" Height="29px" onclick="Button1_Click"
                                       ></asp:Button>&nbsp&nbsp&nbsp&nbsp
          </td>  
                                   <td align="left" >
                            
<asp:Button class="button button2" id="Button2" style="width:100px" runat="server" Text="Close" Height="29px" 
                                           onclick="btnGenerateBill_Click"></asp:Button></td>      
        </tr>

</table>
</div>
</center>

      </td>
      </tr>
      
<%--      ---------------End Owned Lic Update ------------     --%> 
                                       <tr>
                                          <td style="height: 5px" colspan="4">
                                          </td>
               
                                     </tr>
                                     
                                     
    </table>     
      
                                    </div>
                                </center>
    </fieldset>

</asp:Content>

