<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Delete_Digital_SignfromWHR.aspx.cs" Inherits="E_WHR_Delete_Digital_SignfromWHR" Title="Delete E WHR" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset style="width: 100%; border: 2px solid navy;">
        <center>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="6" align="center" valign="top">
                                    <fieldset style="width: 100%; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Delete Digital Sign from e-WHR</span>
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
                                                        <td style="height: 10px" colspan="4" align="center">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 150px" align="left">
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" Height="35px" Width="155px" AutoPostBack="True"
                                                                CssClass="tb6" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                      <td style="width: 100px" align="left">
                                                            <asp:Label ID="lblBranch" runat="server" Text="Enter WHR No." Font-Size="10pt" Font-Bold="true" Visible="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 400px" align="left">
                                                            <asp:TextBox ID="txtSearchWHR" class="text" runat="server"  style="width:250px; height:25px;"></asp:TextBox>&nbsp;
                                                            <asp:Button ID="btnSerachWHR" runat="server" Text="Search" Width="100px" CssClass="BTNBLUE"
                                CausesValidation="false" onclick="btnSerachWHR_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td align="center" valign="top" colspan="4">
                                                    <asp:GridView ID="godown_GridView" runat="server" DataKeyNames="WHR_Id" AutoGenerateColumns="false"
                                                        CellPadding="2" Width="100%" AllowSorting="True"
                                                        
                                                        Font-Size="9pt" onrowdeleting="godown_GridView_RowDeleting" 
                                                           >
                                                        <Columns>
                                                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" 
                                                                ItemStyle-ForeColor="red" >
<ItemStyle ForeColor="Red"></ItemStyle>
                                                            </asp:CommandField>
                                                          
                                                            <asp:TemplateField HeaderText="S.N.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Godown" HeaderText="Godown" />
                                                            <asp:BoundField DataField="WHR_Id" HeaderText="WHR Id"/>
                                                            <asp:BoundField DataField="Signing_Person" HeaderText="Signing Person" />
                                                            <asp:BoundField DataField="Serial_No" HeaderText="Serial No" />
                                                            <asp:BoundField DataField="Signing_Date" HeaderText="Signing Date" />
                                                            <asp:BoundField DataField="Signing_Ip" HeaderText="Signing Ip" />
                                                            <asp:BoundField DataField="Bags" HeaderText="Bags" />
                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                             <asp:BoundField DataField="Date_of_Deposit" HeaderText="Date of Deposit" />
                                                             <asp:BoundField DataField="User_Type" HeaderText="User Type" />
                                                             <asp:BoundField DataField="Commodity_Id" HeaderText="Commodity" />
                                                        </Columns>
                                                        <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                                    </asp:GridView>
                                                    <asp:Label ID="Label3" runat="server" Font-Size="X-Small" ForeColor="#400040"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                          
                                                    <tr>
                                                    <td>
                                                    &nbsp;
                                                    </td>
                                                    </tr>
                                                    <tr>
                        <td colspan="4" align="center">
                            <%--<asp:Button ID="btnupdate" runat="server" Text="Save" Width="100px" CssClass="BTNBLUE"
                                ValidationGroup="validate" onclick="btnupdate_Click" />--%>
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                CausesValidation="false" onclick="btn_Close_Click" />
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
                        </table>
                    </div>
              <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </fieldset>
</asp:Content>

