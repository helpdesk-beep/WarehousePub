<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GodownInspection.aspx.cs" Inherits="Inspection_GodownInspection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPWLC Warehouse Audit</title>
     <link rel="stylesheet" href="css/main.css" type="text/css" />
</head> 
<body>
    <form id="form1" runat="server">
    <div id="bg">
		<div class="wrap">
             <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
            <div style="background-color: #66CCFF">
                 <p style="font-size: medium; color: #008080;">;<asp:LinkButton ID="LinkButton2" Font-Bold="true" Font-Size="Medium" 
                         runat="server" onclick="LinkButton2_Click" >Home</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;गोदामो का भौतिक सत्यापन: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >Log out</asp:LinkButton></p>
            </div>
          <table>
                       <tr>
                    <td>

                       भौतिक सत्यापन हेतु गोदाम ::</td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlgodown" runat="server" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    
                    <td colspan="4" id="GVGAUDIT" runat="server" visible="true">
                        <asp:GridView ID="gvstackdtl" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:BoundField DataField="DepositorName" HeaderText="जमकर्ता का नाम" />
                                <asp:BoundField DataField="commname" HeaderText="स्कन्ध का नाम" />
                                <asp:BoundField DataField="stackname" HeaderText="स्टेक क्र." />
                                <asp:BoundField DataField="Stack_ID" HeaderText="stackid" />
                                <asp:TemplateField HeaderText="Stack बिछान लंबाई">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbichanlambai" runat="server" Width="50px">0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" stack बिछान चौड़ाई">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbichanchodai" runat="server" Width="50px">0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="अतिरिक्त">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtatirikt" runat="server" Width="50px">0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="बोरों के लेयर की उचाई">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtleyaruchai" runat="server" Width="50px">0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ब्लॉक क्र./संख्या">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtblocknum" runat="server" Width="50px">0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="अतिरिक्त पाई गई बोरियाँ ऊपर">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtatiriktboriupper" runat="server" Width="50px">0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="अतिरिक्त पाई गई बोरियाँ नीचे">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtatiriktboriniche" runat="server" Width="50px">0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>

                    </td>
                </tr>
                <tr>
                <td align="center" colspan="4">
                <asp:Button ID="btnsubmit" Visible="false" runat="server" Text="Submit" class="submit" style=" color:Maroon; background-color:Silver; font-size:large; font-family:Times New Roman Baltic;"
                        onclick="btnsubmit_Click"></asp:Button>
                </td>
                </tr>
          </table>
            </div>
        </div
    </form>
</body>
</html>
