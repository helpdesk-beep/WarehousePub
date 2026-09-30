<%@ Page Title="Stack Master" Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="NewStackMaster_W.aspx.cs" Inherits="WarehouseLevel_NewStackMaster_W" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" class="auto-style1">
        <tr>
            <td width="1050px">
                <table cellpadding="0" cellspacing="0" class="auto-style1">
                    <tr>
                        <td width="25px">&nbsp;</td>
                        <td width="1000px">
                            <table cellpadding="0" cellspacing="0" class="auto-style1">
                                <tr>
                                    <td align="center" height="30px" style="font-size: medium; font-weight: bolder; font-style: normal; color: #FFFFFF; background-color: #0066FF" width="1000px">Stack Master</td>
                                </tr>
                                <tr>
                                    <td align="center" height="30px" width="1000px">
                                         <asp:Panel ID="panelContainer" runat="server" Height="400px" ScrollBars="Vertical">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" ShowFooter="true" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" EnableModelValidation="True" Width="600px" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="Godown_ID" OnRowCommand="GridView1_RowCommand">
                                            <Columns>
                                                   <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>.
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />                                                   
                                                    <ControlStyle Width="20px" />
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Godown Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_godownname" runat="server" Text='<%#Eval("Godown_Name") %>' CommandName="a"
                                                                            ForeColor="Black"   ></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                        <asp:Label ID="lbl_head" runat="server" Text="Total Count" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                                       </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Font-Size="10pt"/>
                                                                        <ControlStyle Width="400px" />
                                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Stack">
                                                                         <ItemTemplate>
                                                                            <asp:LinkButton ID="lnktotalstack" runat="server" Text='<%# Eval("countstak") %>' CommandName="b"
                                                                                Font-Underline="true" ForeColor="Blue" ToolTip="Click Me"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_Stackcount" runat="server" Font-Bold="true"></asp:Label></FooterTemplate>                                                                       
                                                                        <ItemStyle HorizontalAlign="center" Font-Size="10pt"/>
                                                                        <ControlStyle Width="150px" />
                                                                    </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                            <RowStyle BackColor="White" ForeColor="#003399" />
                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        </asp:GridView></asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" height="30px" width="1000px">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td width="25px">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

