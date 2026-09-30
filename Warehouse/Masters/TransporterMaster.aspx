<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master"
    AutoEventWireup="true" CodeFile="TransporterMaster.aspx.cs" Inherits="Masters_TransporterMaster"
    Title="Transporter Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="2" align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="lblTransporterMaster" runat="server" Style="position: static" Text="Transporter Master"
                                                        Font-Bold="true" ForeColor="whitesmoke" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center" valign="top">
                                                    <fieldset style="width: 500px; border: 1px solid navy;">
                                                        <center>
                                                            <div>
                                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                                        <td colspan="2" align="center">
                                                                            <asp:Label ID="Label5" runat="server" Style="position: static" Text="Available Transporter Details"
                                                                                Font-Bold="true" ForeColor="whitesmoke" Font-Size="12pt"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" colspan="2">
                                                                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                                                                Font-Size="10pt"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="center" valign="top">
                                                                            <asp:GridView ID="transport_GridView" runat="server" AutoGenerateColumns="False"
                                                                                GridLines="Both" Font-Size="9pt" CellPadding="2" DataKeyNames="Transporter_Id"
                                                                                Width="600px" OnPageIndexChanging="transport_GridView_PageIndexChanging" OnRowDataBound="transport_GridView_RowDataBound"
                                                                                OnRowDeleting="transport_GridView_RowDeleting" OnSelectedIndexChanged="transport_GridView_SelectedIndexChanged"
                                                                                PageSize="10">
                                                                                <Columns>
                                                                                    <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" ItemStyle-ForeColor="red" />
                                                                                    <asp:CommandField ShowSelectButton="True" HeaderText="Edit" ItemStyle-ForeColor="blue" />
                                                                                    <asp:TemplateField HeaderText="Serial Number" InsertVisible="False">
                                                                                        <EditItemTemplate>
                                                                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                                        </EditItemTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Transpoter_Name" HeaderText="Transporter Name" SortExpression="Transpoter_Name" />
                                                                                    <asp:BoundField DataField="Transporter_Id">
                                                                                        <HeaderStyle Font-Size="0pt" />
                                                                                        <ItemStyle Font-Size="0pt" ForeColor="White" />
                                                                                    </asp:BoundField>
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
                                                                        <td style="height: 10px" colspan="2" align="center">
                                                                            <asp:Label ID="lbl_msg" runat="server" Font-Size="10pt" ForeColor="red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </center>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2">
                        </td>
                    </tr>
                    <tr id="Panel_Transpoter" runat="server" visible="False">
                        <td colspan="2" align="center" valign="top">
                            <fieldset style="width: 600px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="lbl_Head" runat="server" Style="position: static" Text="" Font-Bold="true"
                                                        ForeColor="whitesmoke" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="Label3" runat="server" Text="Transporter Name -" Font-Bold="True"
                                                        ForeColor="navy" Font-Size="8pt"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="txtTransporter" runat="server" Width="400px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" ControlToValidate="txtTransporter"
                                                        Display="Dynamic" ErrorMessage="Name field cannot be empty" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btninsert" runat="server" OnClick="btninsert_Click" Text="Insert"
                                                        CssClass="BTNBLUE" Width="100px" />
                                                    <asp:Button ID="btncancel" runat="server" OnClick="btncancel_Click" Text="Cancel"
                                                        CssClass="BTNBLUE" Width="100px" CausesValidation="False" /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnaddnew" runat="server" CssClass="BTNBLUE" Width="100px" OnClick="btnaddnew_Click"
                                Text="Add New" CausesValidation="False" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_Can" runat="server" Text="Close" CssClass="BTNBLUE" Width="100px"
                                CausesValidation="false" OnClick="btn_Can_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2">
                            <asp:ValidationSummary ID="transpoter_validations" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>
