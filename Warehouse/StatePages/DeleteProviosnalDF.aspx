<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="DeleteProviosnalDF.aspx.cs" Inherits="StatePages_DeleteProviosnalDF" Title="Delete Provisionla DF" %>

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
<fieldset style="width: 980px; border: 2px solid navy; margin-left: 10px ; margin-right:10px">
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
                                                    <asp:Label ID="lblGodownMaster" runat="server" Text="Delete Proviosnal Depositor Form" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="height: 50px ; font-size:14px" colspan="4" align="center">
                                                   Provisonal Depositor Form No.&nbsp&nbsp&nbsp <asp:TextBox ID="txtDFNo" runat="server"  
                                                        Width="200px" Height="20px"></asp:TextBox>
                                                     &nbsp&nbsp&nbsp
                                                    <asp:Button class="button button2" id="Button1" style="width:100px" 
                                                        runat="server" Text="Search" Height="29px" onclick="Button1_Click"></asp:Button>
                                                </td>
                                                
                                            </tr>    
                                                                                        <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>                                        
                                                                                        
      <tr>
        <td colspan="4" valign="top" align="center">
          <asp:GridView ID="Depositor_Gridview"  runat="server"
            DataKeyNames="TC_Number" 
            AutoGenerateColumns="False" Width="100%"  BackColor="White" 
                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" 
    CellSpacing="2" PageSize="50">
            <Columns>
              <asp:BoundField DataField="ChkQ_ID" HeaderText="ChkQ_ID" ReadOnly="True" SortExpression="ChkQ_ID" Visible="false"/>
              <asp:BoundField DataField="District" HeaderText="District" ReadOnly="True" SortExpression="District" />
              <asp:BoundField DataField="Branch" HeaderText="Branch" ReadOnly="True" SortExpression="Branch" />
              <asp:BoundField DataField="Godown" HeaderText="Godown" ReadOnly="True" SortExpression="Godown" />
              <asp:BoundField DataField="TC_Number" HeaderText="TC Number" ReadOnly="True" SortExpression="TC_Number" />
              <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No" ReadOnly="True" SortExpression="Acceptance_No" />
              <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" ReadOnly="True" SortExpression="Acceptance_Date" />
              <asp:BoundField DataField="Recd_Bags" HeaderText="Recd Bags" SortExpression="Recd_Bags" />
              <asp:BoundField DataField="NetWeight" HeaderText="Net Weight" SortExpression="NetWeight" />
              <asp:BoundField DataField="Society_Name" HeaderText="Society Name" SortExpression="Society_Name" />
              <asp:BoundField DataField="Qualitychk_Status" HeaderText="Qualitychk Status" SortExpression="Qualitychk_Status" />
              <asp:BoundField DataField="Rejection_Status" HeaderText="Rejection Status" SortExpression="Rejection_Status" />
              
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

