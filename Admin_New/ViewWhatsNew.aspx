<%@ Page Title="" Language="C#" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="ViewWhatsNew.aspx.cs" Inherits="Admin_ViewWhatsNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
        <b style="font-size: large; font-family: 'Times New Roman', Times, serif">View What's New?</b>
    </div>
    <asp:Label ID="lblErr" runat="server"></asp:Label>
    <div>
        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1" style="float: right;">Select Document Type:</td>
                <td class="style2">
                    <asp:DropDownList ID="ddlDocType" runat="server" Width="180px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rvDocType" runat="server"
                        ControlToValidate="ddlDocType" ErrorMessage="Please Select Document Type" ForeColor="#CC3300"
                        ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvHindi" runat="server" AutoGenerateColumns="False" CellPadding="6"
            OnPageIndexChanging="gvHindi_PageIndexChanging" AllowPaging="true" PageSize="30">
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("id") %>'></asp:HiddenField>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" ControlStyle-Width="500px">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TitleHn" runat="server" Text='<%#Eval("title") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Release Date">
                    <ItemTemplate>
                        <asp:Label ID="releaseDate" runat="server" Text='<%#Eval("releaseDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expire Date">
                    <ItemTemplate>
                        <asp:Label ID="expireDate" runat="server" Text='<%#Eval("expireDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href='<%#Eval("viewfile") %>' target="_blank"><%#Eval("viewfileName") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="alert-warning" />
            <AlternatingRowStyle CssClass=" alert-danger" />
        </asp:GridView>
     

    </div>
</asp:Content>

