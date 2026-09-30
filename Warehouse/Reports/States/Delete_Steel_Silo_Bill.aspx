<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Delete_Steel_Silo_Bill.aspx.cs" Inherits="Reports_States_Delete_Steel_Silo_Bill" Title="Delete Steel Silo Bill" %>

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
                                                    <span style="color: White; font-size: 12pt; font-weight: bold">Delete Rent Bill Steel Silo</span>
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
                                            <%--   <tr>
                            <td style="text-align: right;">
                                <asp:Label ID="lvlRegion" runat="server" Text="Region:"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlregion" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width: 300px;">
                                <asp:Label ID="lbldistrict" runat="server" Text="District : "></asp:Label>
                            </td>
                            <td style="text-align: left; width: 300px;">
                                <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                             <td style="text-align: right; width: 300px;">
                                <asp:Label ID="lblbranch" runat="server" Text="Branch:"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 300px;">
                                <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true" class="form-control" >
                                </asp:DropDownList>
                            </td>
                                                       </tr>--%>
                                            <tr>

                                                <%--<td style="width:150px; font-size:20px;" align="center" >Insurance Type: &nbsp;&nbsp;
                                                            <asp:DropDownList ID="ddlFlag" runat="server" Height="25px" Width="155px" AutoPostBack="True" OnSelectedIndexChanged="ddlFlag_SelectedIndexChanged"
                                                                CssClass="tb6">
                                                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="YES" Value="YES" ></asp:ListItem>
                                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>


                                                            </asp:DropDownList>
                                                        </td>--%>
                                                <td style="width: 150px; font-size: 20px;">District: &nbsp;&nbsp;
                                                            <asp:DropDownList ID="ddldistrict" runat="server" Height="25px" Width="155px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack="True"
                                                                CssClass="tb6">
                                                            </asp:DropDownList>
                                                </td>
                                                <td style="width: 150px; font-size: 20px;">Branch: &nbsp;&nbsp;
                                                            <asp:DropDownList ID="ddlbranch" runat="server" Height="25px" Width="155px" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" AutoPostBack="True"
                                                                CssClass="tb6">
                                                            </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" colspan="4">
                                                    <asp:GridView ID="godown_GridView" runat="server" AutoGenerateColumns="False"
                                                        CellPadding="2" Width="100%" AllowSorting="True" OnRowUpdating="godown_GridView_RowUpdating" Height="190px">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.N.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1%>
                                                                    <asp:HiddenField ID="BillNumberID" runat="server" Value='<%#Eval("Bill_Number") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                            </asp:TemplateField>

                                                            <%--<asp:BoundField DataField="SerialNumber" HeaderText="Serial Number" />--%>

                                                            <%--<asp:BoundField DataField="Regionnm" ItemStyle-HorizontalAlign="Center"  HeaderText="Region"/>--%>
                                                            <asp:BoundField DataField="Bill_Number" ItemStyle-HorizontalAlign="Center" HeaderText="Bill Number" />
                                                            <asp:BoundField DataField="Bill_Type" ItemStyle-HorizontalAlign="Center" HeaderText="Bill Type" />
                                                            <asp:BoundField DataField="Godown_Name" ItemStyle-HorizontalAlign="Center" HeaderText="Godown Name" />
                                                            <asp:BoundField DataField="Commodity_Name" ItemStyle-HorizontalAlign="Center" HeaderText="Commodity Name" />
                                                            <%--<asp:BoundField DataField="InsuranceCapacity" ItemStyle-HorizontalAlign="Center" HeaderText="Insurance Capacity" />
                                                            <asp:BoundField DataField="valuestk" ItemStyle-HorizontalAlign="Center" HeaderText="Insurance Value" />
                                                            <asp:BoundField DataField="Idate" ItemStyle-HorizontalAlign="Center" HeaderText="Insurance Date" />
                                                            <asp:BoundField DataField="Idatevaladity" ItemStyle-HorizontalAlign="Center" HeaderText="Insurance Validity Date" />
                                                             <asp:BoundField DataField="Jvs_Flag" ItemStyle-HorizontalAlign="Center" HeaderText="Insurance Type" />   
                                                            --%>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnRemove" Text="Delete" runat="server" CssClass="btneditstyle" CommandName="Update" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                                                                </ItemTemplate>
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
