<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WLCPendingGatePassOfDelivery.aspx.cs"
    Inherits="IssueCenterLevel_Storage_WLCPendingGatePassOfDelivery" MasterPageFile="~/MasterPage/Gdwn.master"
    Title="Pending Delivery Gatepass ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function OpenWindow(Sno) {
            window.open("Gate_Pass.aspx?src=RO&vu=" + Sno, "_new", "height=800,width=780");
        }  
    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 950px">
                    <tr>
                        <td colspan="6" align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblPendingGatePAssList" runat="server" Font-Bold="True" Font-Size="12pt"
                                                        ForeColor="whitesmoke" Text="Pending Delivery Gate Pass List"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblmsg" runat="server" ForeColor="red" Visible="False" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 200px" align="left">
                                                    <asp:Label ID="lblDepositorType" runat="server" Text="Depositor Type" Font-Bold="true"
                                                        Font-Size="10pt" ForeColor="navy"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddldepositortype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldepositortype_SelectedIndexChanged"
                                                        TabIndex="1" Width="155px" Height="25px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 150px" align="left">
                                                    <asp:Label ID="lblDepositorName" runat="server" Text="Depositor Name" Font-Bold="true"
                                                        Font-Size="10pt" ForeColor="navy"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlDepositor" runat="server" AutoPostBack="True" Height="25px"
                                                        Width="155px" TabIndex="2">
                                                    </asp:DropDownList>
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
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lbl_Empty" runat="server" Text="" Font-Bold="true" Font-Size="12pt"
                                ForeColor="red" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4">
                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" colspan="4">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div style="overflow: scroll; height: 200px; overflow-x: hidden">
                                        <asp:GridView ID="gdGPHelp" runat="server" CellPadding="2" CellSpacing="1" TabIndex="7"
                                            Width="100%" Font-Size="10pt" ForeColor="Navy" AutoGenerateColumns="False" DataKeyNames="gatepass_no"
                                            EnableModelValidation="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select " ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <a href="#" onclick='OpenWindow(<%# DataBinder.Eval(Container.DataItem,"GatePass_No")%>);'>
                                                            <font color="blue" face="bold"><u>Issue Gate Pass</u></font></a>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="gatepass_no" HeaderText="GatePass No.">
                                                    <ItemStyle Width="100px" HorizontalAlign="left" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="depo" HeaderText="Depositor">
                                                    <ItemStyle Width="100px" HorizontalAlign="left" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                                    <ItemStyle Width="100px" HorizontalAlign="left" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Vehicle_No" HeaderText="Vehicle No.">
                                                    <ItemStyle Width="80px" HorizontalAlign="left" />
                                                    <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Bags" HeaderText="Bags">
                                                    <ItemStyle Width="50px" HorizontalAlign="right" />
                                                    <HeaderStyle Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Weight" HeaderText="Weight">
                                                    <ItemStyle Width="50px" HorizontalAlign="right" />
                                                    <HeaderStyle Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Issue_Date" HeaderText="Issue Date">
                                                    <ItemStyle Width="100px" HorizontalAlign="center" />
                                                    <HeaderStyle Width="60px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="20px" Font-Size="10pt" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
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
                        <td style="height: 10px" colspan="6" align="center">
                            <asp:Label ID="lbl_notfound" runat="server" Font-Size="10pt" ForeColor="Red" Font-Bold="true"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4">
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
