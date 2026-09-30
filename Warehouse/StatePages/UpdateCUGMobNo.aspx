<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="UpdateCUGMobNo.aspx.cs" Inherits="StatePages_UpdateCUGMobNo" Title="Update CUG No." %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
<fieldset style="width: 800px; border: 2px solid navy; margin-left: 10px ; margin-right:10px">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">
                            
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center" >
                                                    <asp:Label ID="lblGodownMaster" runat="server" Text="Update Branch OTP Mobile Number" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            
  
                                                                                        <tr>
                                                <td style="height: 50px ; font-size:14px" colspan="4" align="center">
                                                    District : &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlDistrict" runat="server" 
                                                        Height="25px" Width="168px"  AutoPostBack="true"
                                                        onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr> 
                                            
                                            <tr>
                                                <td style="height: 5px" colspan="4"></td>
                                            </tr>                                       
                                                                                        
      <tr>
        <td colspan="4" valign="top" align="center">
          <asp:GridView ID="Depositor_Gridview"  runat="server"
            DataKeyNames="Branch_Id" 
            AutoGenerateColumns="False" Width="70%"  BackColor="White" 
                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" 
    CellSpacing="2" PageSize="50" 
                onselectedindexchanged="Depositor_Gridview_SelectedIndexChanged">
            <Columns>
              <asp:BoundField DataField="Branch_Id" HeaderText="Branch_ID" ReadOnly="True" SortExpression="Branch_Id"/>
              <asp:BoundField DataField="Branch_Name" HeaderText="Branch Name" ReadOnly="True" SortExpression="Branch_Name" />
              <asp:BoundField DataField="Mobile_No" HeaderText="Mobile_No" ReadOnly="True" SortExpression="Mobile_No" />
              <asp:BoundField DataField="In_Depot_Master" HeaderText="In_Depot_Master" ReadOnly="True" SortExpression="In_Depot_Master" />
              <asp:CommandField SelectText="Edit" HeaderText="Edit" ShowSelectButton="True" >
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
          
        </td>
        </tr> 

        
                                                    <tr id="trmobtxt" runat="server" visible="false">
                                                    
                                                <td style="height: 70px ; font-size:14px" colspan="4" align="center">
                                                    <asp:Label ID="Label1" runat="server" Text="Branch : "></asp:Label> 
                                                    <asp:TextBox ID="txtbranch" runat="server"
                                                        Width="150px" Height="20px"></asp:TextBox> &nbsp;&nbsp;&nbsp;
                                                   Enter Mobile No.&nbsp&nbsp&nbsp 
                                                    <asp:TextBox ID="txtDFNo" runat="server" 
                                                        Width="170px" Height="20px" MaxLength="10"></asp:TextBox>
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
                            
<asp:Button class="button button2" id="btnGenerateBill" style="width:100px" runat="server" Text="Close" Height="29px"></asp:Button></td>      
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

