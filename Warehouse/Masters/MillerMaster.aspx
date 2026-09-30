<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master"
    AutoEventWireup="true" CodeFile="MillerMaster.aspx.cs" Inherits="Masters_MillerMaster"
    Title="मिलर मास्टर ::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete()
        {
          if (confirm("Are you sure want to Delete?")==true)
            return true;
          else
            return false;
        }
    </script>

    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td align="center">
                                                    <asp:Label ID="lblMilerMaster" runat="server" Text="Miller Master " ForeColor="whitesmoke"
                                                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <asp:GridView ID="Millergrid" runat="server" DataKeyNames="Miller_Id" AutoGenerateColumns="False"
                                                        CellPadding="4" Width="600px" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="Millergrid_SelectedIndexChanged"
                                                        OnPageIndexChanging="Millergrid_PageIndexChanging" PageSize="5" GridLines="Both"
                                                        Font-Size="9pt" OnRowCommand="Millergrid_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk_Delete" runat="server" CommandName="Deletes" ForeColor="red"
                                                                        Text="Delete" OnClientClick="return ConfirmOnDelete();"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowSelectButton="True" HeaderText="Edit" ItemStyle-ForeColor="blue" />
                                                            <asp:BoundField HeaderText="Miller Name" DataField="Miller_Name" ApplyFormatInEditMode="True">
                                                                <ItemStyle ForeColor="Blue" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Licence_No" HeaderText="Licence No" SortExpression="Licence_No">
                                                                <ItemStyle ForeColor="Blue" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks">
                                                                <ItemStyle ForeColor="Blue" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Miller_Id" DataField="Miller_Id">
                                                                <ItemStyle ForeColor="White" Font-Size="0pt" Width="0px" />
                                                                <HeaderStyle Font-Size="0pt" ForeColor="DarkRed" Width="0px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#CCCC99" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <asp:Label ID="lbl_count" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="10pt"
                                                        Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr id="PanelMiller" runat="server" visible="False">
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td align="center" colspan="2">
                                                    <asp:Label ID="lbl_Head" runat="server" Text="Add New Miller Details " ForeColor="whitesmoke"
                                                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" colspan="2">
                                                    <fieldset style="width: 500px; border: 1px solid navy;">
                                                        <center>
                                                            <div>
                                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 100px" align="left">
                                                                            <asp:Label ID="Label1" runat="server" Text="Miller Name" ForeColor="navy" Font-Bold="true"
                                                                                Font-Size="8pt"></asp:Label></td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtMillerName" runat="server" Width="250px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" ControlToValidate="txtMillerName"
                                                                                Display="Dynamic" ErrorMessage="Name field cannot be empty" SetFocusOnError="True"
                                                                                Font-Bold="true">*</asp:RequiredFieldValidator></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 5px" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 150px" align="left">
                                                                            <asp:Label ID="Label2" runat="server" Text="License No" ForeColor="navy" Font-Bold="true"
                                                                                Font-Size="8pt"></asp:Label></td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtLicenceNo" runat="server" Width="250px"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 5px" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 150px" align="left" valign="top">
                                                                            <asp:Label ID="Label3" runat="server" Text="Remarks" ForeColor="navy" Font-Bold="true"
                                                                                Font-Size="8pt"></asp:Label></td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="250px" Height="50px"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 5px" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="2">
                                                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CausesValidation="False"
                                                                                OnClick="btncancel_Click" CssClass="BTNBLUE" Width="100px" />
                                                                            &nbsp; &nbsp; &nbsp; &nbsp;
                                                                            <asp:Button ID="btninsert" runat="server" Text="Insert" CssClass="BTNBLUE" Width="100px"
                                                                                OnClick="btninsert_Click" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 5px" colspan="2" align="center">
                                                                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="10pt"></asp:Label>
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
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnaddnew" runat="server" Text="Add New" CssClass="BTNBLUE" Width="100px"
                                OnClick="btnaddnew_Click" />
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                CausesValidation="false" OnClick="btn_Close_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
    <asp:ValidationSummary ID="stack_sdsvalidations" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
</asp:Content>
