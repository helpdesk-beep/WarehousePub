<%@ Page Title="" Language="C#" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="ViewGradationList.aspx.cs" Inherits="Admin_ViewGradationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="../NEW_CSS/js/jquery-1.12.4.js"></script>
    <script src="../NEW_CSS/js/jquery-ui.js"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>


    <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
        <b style="font-size: large; font-family: 'Times New Roman', Times, serif">View Gradation List</b>
    </div>
    <asp:Label ID="lblErr" runat="server"></asp:Label>
    <br />
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6" AllowPaging="true" PageSize="30"
            OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="alert-heading">
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("id") %>'></asp:HiddenField>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("title") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtTitle" Width="600" Text='<%#Eval("title") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="File">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkView" runat="server" Text='<%# Eval("fileName") %>' OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="alert-warning" />
            <AlternatingRowStyle CssClass="alert-success" />
        </asp:GridView>
    </div>
</asp:Content>

