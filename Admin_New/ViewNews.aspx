<%@ Page Title="" Debug="true" ViewStateEncryptionMode="Always" Language="C#" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="ViewNews.aspx.cs" Inherits="Admin_ViewNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
        <b style="font-size: large; font-family: 'Times New Roman', Times, serif">View News</b>
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
                <asp:TemplateField HeaderText="News Title" ControlStyle-Width="500px">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Caption" runat="server" Text='<%# HttpUtility.HtmlEncode(Eval("newsTitle") ?? "") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="News Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Date" runat="server" Text='<%# Eval("newsDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fileName" HeaderText="File Name" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                            CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                        <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="alert-warning" />
            <AlternatingRowStyle CssClass=" alert-info" />
        </asp:GridView>
    </div>
</asp:Content>