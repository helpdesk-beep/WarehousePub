<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GodwonCheck.aspx.cs" Inherits="StatePages_GodwonCheck" %>

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
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Branch Manager details" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 50px;" colspan="8" align="center">&nbsp;&nbsp;&nbsp;&nbsp  Godown Id : &nbsp;&nbsp;<%--<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                                                Height="25px" Width="168px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtGodwonID" ></asp:TextBox>

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
                                                        <asp:TemplateField HeaderText="District Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDistrict_Name" Width="100%" Text='<%# Eval("District_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Branch Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDepotName" Width="100%" Text='<%# Eval("DepotName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Godown ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("Godown_ID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Godown Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Capacity" Width="100%" Text='<%# Eval("Godown_Capacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Godown Scientific Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Width="100%" Text='<%# Eval("Godown_Scientific_Capacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hired Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHired_Type" Width="100%" Text='<%# Eval("Hired_Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Is Active">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblIsActive" Width="100%" Text='<%# Eval("IsActive")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="CreatedDate">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCreatedDate" Width="100%" Text='<%# Eval("CreatedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
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
              <%--  <asp:Panel ID="pnllogin" class="popup" runat="server">
                    <div class="pop" style="background-color: white; min-height: 300PX; max-height: 500px; width: 1000px; border: #008CBA; border-style: solid; border-width: 10px;">
                       


                        <div id="divNewInsp" runat="server" visible="true" style="width: 100%;">

                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 70px; font-size: 14px" colspan="4" align="center">
                                        <asp:Label ID="Label2" runat="server" Text="Godown Name : "></asp:Label>
                                        <asp:TextBox ID="lblgodownname" runat="server" Width="300px" Height="20px"></asp:TextBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr id="trmobtxt" runat="server" visible="true">
                                    <td style="height: 70px; font-size: 14px" colspan="4" align="center">
                                        <asp:Label ID="Label1" runat="server" Text="Godown ID : "></asp:Label>
                                        <asp:TextBox ID="txtGdwnID" runat="server" ReadOnly="true"
                                            Width="150px" Height="20px"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                      &nbsp; Depositor Name &nbsp;
                                                    <asp:DropDownList ID="ddlDepositor" runat="server" Width="155px" Height="25px" AutoPostBack="false">
                                                    </asp:DropDownList>
                                        &nbsp;
                                                <br />
                                    </td>
                                </tr>

                                <tr id="tr2" runat="server" visible="true">
                                    <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp; WHR No. &nbsp;
                                                    <asp:TextBox ID="txtwhrno" runat="server"
                                                        Width="300px" Height="20px"></asp:TextBox>

                                    </td>
                                </tr>


                                <tr>
                                    <td style="height: 5px" colspan="4"></td>
                                </tr>
                             
                            </table>
                            <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

                        </div>


                       
                    </div>
                    <img alt="New" src="images/new6.gif" id="new" runat="server" />

                </asp:Panel>--%>
                <%--<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin">
                </asp:ModalPopupExtender>--%>
            </div>
        </div>
    </form>
</body>
</html>
