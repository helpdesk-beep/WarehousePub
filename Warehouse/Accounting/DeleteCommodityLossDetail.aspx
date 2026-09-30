<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="DeleteCommodityLossDetail.aspx.cs" Inherits="Accounting_DeleteCommodityLossDetail" Title="Delete Commodity Loss" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="6" align="center" valign="top">
                                    <fieldset style="width: 1100px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Delete Commodity Loss/Gain Details</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td align="left" colspan="4">
                                                            &nbsp;</td>
                                                    </tr>
                                                   <tr>
                                                        <td colspan="2" style="width: 150px" align="right">
                                                            <asp:Label ID="Label1" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="width: 150px" align="left">
                                                            <asp:DropDownList ID="ddlCmdType" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                                CssClass="tb6" onselectedindexchanged="ddlCmdType_SelectedIndexChanged">
                                                                <asp:ListItem Value="1">Wheat</asp:ListItem>
                                                                <asp:ListItem Value="2">Paddy</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                      
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 150px" align="left">
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                                CssClass="tb6" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 100px" align="left">
                                                            <asp:Label ID="lblBranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 200px" align="left">
                                                            <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                                 CssClass="tb6" 
                                                                onselectedindexchanged="ddlDepotList_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                  
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="right">
                                    <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Text="Total Record "
                                        Font-Size="10pt" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Label ID="lbl_notfound" runat="server" Text="" Font-Bold="True" Font-Size="15pt"
                                        ForeColor="red" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trPaddy" runat="server" visible="false">
                                <td colspan="4" align="center" valign="top">
                                    <asp:Panel ID="panelContainer" runat="server" Height="380px" ScrollBars="Vertical"
                                        Width="100%" BorderColor="navy" BorderWidth="1px">
                                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="10pt"
                                            DataKeyNames="Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_Delete" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                        Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                    <ControlStyle Width="15px" />
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="Id" HeaderText="ID">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="DepositorName" HeaderText="Depositor Name">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="Commodity" HeaderText="Commodity">
                                                    <ItemStyle Width="50px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="50px" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="CropYear" HeaderText="Crop Year">
                                                    <ItemStyle Width="200px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Godown" HeaderText="Godown">
                                                    <ItemStyle Width="150px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DepositDate" HeaderText="Deposit Date">
                                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                      <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                             
                                             
                                                <asp:BoundField DataField="LossQty" HeaderText="Loss Qty">
                                                    <ItemStyle HorizontalAlign="center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IssueDate" HeaderText="Issue Date">
                                                    <ItemStyle HorizontalAlign="center" Width="50px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="20px" Font-Size="10pt" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr id="trWheat" runat="server" visible="false">
                                <td colspan="4" align="center" valign="top">
                                    <asp:Panel ID="panel1" runat="server" Height="380px" ScrollBars="Vertical"
                                        Width="100%" BorderColor="navy" BorderWidth="1px">
                                        <asp:GridView ID="GridViewWheat" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="10pt"
                                            DataKeyNames="WLG_Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_Delete" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                        Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                    <ControlStyle Width="15px" />
                                                </asp:TemplateField>
                                            
                                                 <asp:BoundField DataField="WLG_Id" HeaderText="WLG_Id">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Storage_Type" HeaderText="Storage_Type">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                  <asp:BoundField DataField="Deposit_CropYear" HeaderText="Deposit_CropYear">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="Deposit_Weight" HeaderText="Deposit_Weight">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                    <asp:BoundField DataField="Total_Gain" HeaderText="Total_Gain">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                     <asp:BoundField DataField="Total_Loss" HeaderText="Total_Loss">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="20px" Font-Size="10pt" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 15px" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="Btn_Delete" runat="server" Text="Delete Record" Width="100px"
                                        CssClass="BTNBLUE" onclick="Btn_Delete_Click" />
                                    <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px"
                                        CssClass="BTNBLUE" onclick="btn_Close_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
           
        </center>
    </fieldset>
</asp:Content>

