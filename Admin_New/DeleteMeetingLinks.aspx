<%@ Page Title="" Language="C#" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="DeleteMeetingLinks.aspx.cs" Inherits="Admin_DeleteMeetingLinks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
        <b style="font-size: large; font-family: 'Times New Roman', Times, serif">Delete old Meeting Link</b>
    </div>
    <br />
    <div>
        <center>
          <asp:Label ID="lblErr" runat="server" CssClass="alert-danger" Font-Bold="true" Font-Size="Large"></asp:Label>  </center>
    </div>
    <br />
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
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6" AllowPaging="true" PageSize="30"
            OnPageIndexChanging="GridView1_PageIndexChanging"
            CssClass="alert-heading">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" ControlStyle-Width="400px">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TitleHn" runat="server" Text='<%#Eval("title") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Release Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ReleaseDate" runat="server" Text='<%#Eval("releaseDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expire Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ExpireDate" runat="server" Text='<%#Eval("expireDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Meeting Link">
                        <ItemTemplate>
                            <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%# Eval("Meeting_Link") %>'
                                title="Meeting Link" Text=' <%# Eval("Meeting_Link") %>' ForeColor="Blue"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btn_Delete" CssClass="btn-danger" OnClick="Delete" CommandArgument='<%# Eval("Id") %>' runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="alert-warning" />
            <AlternatingRowStyle CssClass=" alert-danger" />
        </asp:GridView>
    </div>
</asp:Content>

