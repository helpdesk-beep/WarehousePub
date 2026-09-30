<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="DeleteInActiveGodown.aspx.cs" Inherits="BranchPages_DeleteInActiveGodown" Title="Delete InActive Godown" %>

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
                                                    <asp:Label ID="lblGodownMaster" runat="server" Text="Delete In-Active Godown Entry" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            
  
                                                                                        <%--<tr>
                                                <td style="height: 50px ; font-size:14px" colspan="4" align="center">
                                                    
                                                  &nbsp;&nbsp;&nbsp;&nbsp  Branch : &nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList ID="ddlBranch" runat="server" 
                                                        Height="25px" Width="168px"> </asp:DropDownList>
                                                    
                                                 &nbsp;&nbsp;&nbsp;   Which data You Want to delete ? : &nbsp;&nbsp;<asp:DropDownList 
                                                        ID="ddlGdwnExixtance" runat="server" 
                                                        Height="25px" Width="168px"  AutoPostBack="true" 
                                                        onselectedindexchanged="ddlGdwnExixtance_SelectedIndexChanged">
                                                        <asp:ListItem Text="--Select--" Value="--Select--" > </asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="Y" > </asp:ListItem>
                                                         <asp:ListItem Text="No" Value="N" > </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>--%> 
                                            
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
          
          
          
          <asp:GridView ID="Depositor_Gridview"  runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False" Width="100%"  BackColor="White" 
                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" 
    CellSpacing="2" PageSize="50" onselectedindexchanged="Depositor_Gridview_SelectedIndexChanged">
            <Columns>
              <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" ReadOnly="True" SortExpression="Godown_ID" />
              <asp:BoundField DataField="Godown_Name" HeaderText="Godown_Name" ReadOnly="True" SortExpression="Godown_Name" />
              <asp:BoundField DataField="Hired_Type" HeaderText="Hired_Type" ReadOnly="True" SortExpression="Hired_Type" />
              <asp:BoundField DataField="Storage_Type" HeaderText="Storage_Type" ReadOnly="True" SortExpression="Storage_Type" />
              <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Godown_Scientific_Capacity" ReadOnly="True" SortExpression="Godown_Scientific_Capacity" ItemStyle-Width="100px"/>
              
              <asp:BoundField DataField="Premise_capacity" HeaderText="Premise_capacity" ReadOnly="True" SortExpression="Premise_capacity" />
              <asp:BoundField DataField="Closing_Balance" HeaderText="Closing_Balance" ReadOnly="True" SortExpression="Closing_Balance" />
              <asp:CommandField SelectText="Select" HeaderText="Delete" ShowSelectButton="True" >
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

        
                                                    <tr id="trmobtxt" runat="server" visible="false">
                                                    
                                                <td style="height: 70px ; font-size:14px" colspan="4" align="center">
                                                    <asp:Label ID="Label1" runat="server" Text="Godown ID : "></asp:Label> 
                                                    <asp:TextBox ID="txtGdwnID" runat="server" ReadOnly="true"
                                                        Width="150px" Height="20px"></asp:TextBox> &nbsp;&nbsp;&nbsp;
                                                   Godown Name &nbsp&nbsp&nbsp 
                                                    <asp:TextBox ID="txtGdwnName" runat="server"  ReadOnly="true"
                                                        Width="300px" Height="20px" MaxLength="10"></asp:TextBox>
                                                </td>
                                                
                                            </tr>  
                                     <tr>
                                          <td style="height: 5px" colspan="4">
                                          </td>
                                     </tr>        
        <tr id="trbtnhide" runat="server" visible="false">
        
           <td align="Right">
<asp:Button class="button button1" id="btnAddCompany" style="width:100px" runat="server" 
                   Text="Delete" Height="29px" onclick="btnAddCompany_Click" 
                                       ></asp:Button>&nbsp&nbsp&nbsp&nbsp
          </td>  
                                   <td align="left">
                            
<asp:Button class="button button2" id="btnGenerateBill" style="width:100px" runat="server" Text="Close" Height="29px" 
                                           onclick="btnGenerateBill_Click"></asp:Button></td>      
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

