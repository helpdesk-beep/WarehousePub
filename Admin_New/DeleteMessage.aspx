<%@ Page Title="" Debug="true" Language="C#" ViewStateEncryptionMode="Always" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="DeleteMessage.aspx.cs" Inherits="Admin_DeleteNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
        <b style="font-size: large; font-family: 'Times New Roman', Times, serif">Delete Message</b>
    </div>
    <br />
    <div>
        <center>
          <asp:Label ID="lblErr" runat="server" CssClass="alert-danger" Font-Bold="true" Font-Size="Large"></asp:Label>  </center>
    </div>
    <br />
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6" AllowPaging="true" PageSize="30"
            OnPageIndexChanging="GridView1_PageIndexChanging"
            CssClass="alert-heading">
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
                        <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("releaseDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="../<%#Eval("FileNameH") %>">View</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btn_Delete" CssClass="btn-danger" OnClick="Delete" CommandArgument='<%# Eval("Id") %>' runat="server" Text="Delete" CommandName="Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="alert-warning" />
            <AlternatingRowStyle CssClass=" alert-danger" />
        </asp:GridView>
    </div>
</asp:Content>

