<%@ Page Title="" Debug="true" ViewStateEncryptionMode="Always" Language="C#" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="ViewMessage.aspx.cs" Inherits="Admin_ViewNews" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
        <b style="font-size: large; font-family: 'Times New Roman', Times, serif">View Message</b>
    </div>
    <asp:Label ID="lblErr" runat="server"></asp:Label>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6"
            OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="true" PageSize="30">
            <Columns>
                <asp:TemplateField HeaderText="IndexNo.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Message Title" ControlStyle-Width="500px">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Caption" runat="server" Text='<%#Eval("titleInHn") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="News Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("relDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="../<%#Eval("FileNameH") %>">View</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="alert-warning" />
            <AlternatingRowStyle CssClass=" alert-info" />
        </asp:GridView>
    </div>
</asp:Content>

