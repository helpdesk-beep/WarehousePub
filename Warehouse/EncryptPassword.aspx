<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EncryptPassword.aspx.cs" Inherits="EncryptPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="JavaScript" src="js/MD5.js"></script>
    <script type="text/javascript">
        function MDS(button) {
            var rowId = button.id.substring("gvPwdEN_ctl".length, button.id.length - "_btnUpdate".length);
            var password = document.getElementById("gvPwdEN_ctl" + rowId + "_txtPwd");
            var encrypted = document.getElementById("gvPwdEN_ctl" + rowId + "_txtEncrypted");
            if (password && encrypted) {
                encrypted.value = MD5(password.value);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gvPwdEN" runat="server" CellPadding="0" ForeColor="#FFC080" BorderColor="Silver" HorizontalAlign="left" BackColor="Silver" TabIndex="8" AutoGenerateColumns="False">
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Encrypted">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEncrypted" runat="server" ></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                  
                                    <asp:TextBox ID="txtEncrypted"  runat="server" ></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="pwd">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPwd" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                   
                                    <asp:TextBox ID="txtPwd" runat="server" Text='<%# Bind("pwd") %>'></asp:TextBox>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="login_id">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUID" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                   
                                    <asp:TextBox ID="txtUID" runat="server" Text='<%# Bind("login_id") %>'></asp:TextBox>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User_Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                   
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("User_Name") %>'></asp:TextBox>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MEncrypted">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMEncrypted" runat="server" ></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                  
                                    <asp:TextBox ID="txtMEncrypted"  runat="server" ></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="EnCrypt" OnClientClick="javascript:MDS(this);" />
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            
                        </Columns>
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" BorderColor="#FFC080" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" BorderColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" BorderColor="Silver"  />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblStatus" runat="server" ForeColor="Red" /></td>
            </tr>
            <tr>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" /></td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
