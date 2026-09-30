<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckCapacity.aspx.cs" Inherits="StatePages_GodownCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Godown Stock details" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 50px;" colspan="8" align="center">&nbsp;&nbsp;&nbsp;&nbsp  Registration Id : &nbsp;&nbsp;<%--<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                                                Height="25px" Width="168px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtRegistrationID" ></asp:TextBox>

                                                &nbsp;&nbsp;&nbsp;&nbsp   : &nbsp;&nbsp;<%--<asp:DropDownList ID="ddlBranch" runat="server"
                                                    Height="25px" Width="168px" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                </asp:DropDownList>--%>
                                                <asp:Button runat="server" Text="Check" ID="btnCheck" OnClick="btnCheck_Click" Height="25px" Width="168px"/>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td align="center" style="width: 200px">
                                                <asp:Button ID="Button1" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                                                    CssClass="BTNBLUE" OnClick="Button1_Click" />
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDistrict_Name" Width="100%" Text='<%# Eval("DistrictID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"/>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Branch ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDepotName" Width="100%" Text='<%# Eval("BranchID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"/>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_ID" Text='<%# Eval("GodownID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"/>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JVS RegistrationID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblRegistrationID" Width="100%" Text='<%# Eval("JVS_RegNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("GodownName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"/>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Capacity" Text='<%# Eval("GodownCapacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"/>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Weight Balance">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Width="100%" Text='<%# Eval("WeightBalance")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"/>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Percentage">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Width="100%" Text='<%# Eval("Percentage")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"/>
                                                        </asp:TemplateField>

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
                                    </table>
                                </div>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>

                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
