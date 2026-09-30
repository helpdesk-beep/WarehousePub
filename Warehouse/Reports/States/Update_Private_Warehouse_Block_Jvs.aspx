<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Update_Private_Warehouse_Block_Jvs.aspx.cs" Inherits="Reports_States_Update_Private_Warehouse_Block_Jvs" Title="Update_Private_Warehouse_Block_Jvs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                                    <span style="color: White; font-size: 12pt; font-weight: bold">Update Warehouse Related Information</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>

                                            <tr>
                                                <td align="left" colspan="4">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4" align="center">&nbsp;</td>
                                            </tr>


                                            <tr>
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" colspan="4">
                                                    <asp:GridView ID="godown_GridView" runat="server" AutoGenerateColumns="False"
                                                        CellPadding="2" Width="100%" AllowSorting="True" OnRowCommand="godown_GridView_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.N.">
                                                                <%-- <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1%>
                                                                </ItemTemplate>--%>
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1%>
                                                                    <asp:HiddenField ID="hdnregid" runat="server" Value='<%#Eval("Godown_Id") %>' />

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                            </asp:TemplateField>

                                                            <%--<asp:BoundField DataField="SerialNumber" HeaderText="Serial Number" />--%>
                                                            <asp:BoundField DataField="Region" ItemStyle-HorizontalAlign="Center" HeaderText="Region" />
                                                            <asp:BoundField DataField="District_Name" ItemStyle-HorizontalAlign="Center" HeaderText="District" />
                                                            <asp:BoundField DataField="Branch_Name" ItemStyle-HorizontalAlign="Center" HeaderText="Branch Name" />
                                                            <asp:BoundField DataField="Godown_Id" ItemStyle-HorizontalAlign="Center" HeaderText="Godown Id" />
                                                           <%-- <asp:BoundField DataField="Registration_ID" ItemStyle-HorizontalAlign="Center" HeaderText="Registration Id" />--%>
                                                            <asp:TemplateField HeaderText="Registration_ID">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRegistration_ID" runat="server" class="form-control" Text='<%# Eval("Registration_ID") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="Godown_Name" ItemStyle-HorizontalAlign="Center" HeaderText="Warehouse Name" />
                                                            <asp:BoundField DataField="Obstruction" ItemStyle-HorizontalAlign="Center" HeaderText="Obstruction" />
                                                            <asp:BoundField DataField="Remark" ItemStyle-HorizontalAlign="Center" HeaderText="Obstruction Remark" />
                                                            <asp:BoundField DataField="Date_Of_Obstruction" ItemStyle-HorizontalAlign="Center" HeaderText="Date Of Obstruction" />
                                                            <asp:BoundField DataField="Warehouse_Infested_Status" ItemStyle-HorizontalAlign="Center" HeaderText="Warehouse Infested Status" />
                                                            <asp:BoundField DataField="Infested_Remark" ItemStyle-HorizontalAlign="Center" HeaderText="Infested Remark" />
                                                            <asp:BoundField DataField="Warehouse_Infested_Date" ItemStyle-HorizontalAlign="Center" HeaderText="Warehouse Infested Date" />
                                                            <asp:BoundField DataField="Is_allowed" ItemStyle-HorizontalAlign="Center" HeaderText="Is Allowed" />
                                                            <asp:TemplateField HeaderText="Is Allowed">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlIsallowed" runat="server" AutoPostBack="true">
                                                                        <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                                                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnRemove" Text="Update" runat="server" CommandName="RemoveRow" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
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
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>

                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>
                    </tr>
                </table>
            </div>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </fieldset>
</asp:Content>
