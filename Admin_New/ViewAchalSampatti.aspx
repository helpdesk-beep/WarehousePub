<%@ Page Title="" Language="C#" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="ViewAchalSampatti.aspx.cs" Inherits="Admin_ViewAchalSampatti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
        <b style="font-size: large; font-family: 'Times New Roman', Times, serif">View Achal Sampatti</b>
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
                <td class="style1" style="float: right;">Select Year:</td>
                <td class="style2">
                    <asp:DropDownList ID="ddlYear" runat="server" Width="180px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rvDocType" runat="server"
                        ControlToValidate="ddlYear" ErrorMessage="Please Select Document Type" ForeColor="#CC3300"
                        ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6"
            OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="true" PageSize="30">
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                        <asp:HiddenField ID="hdnID" runat="server" Value='<%# HttpUtility.HtmlEncode(Convert.ToString(Eval("id"))) %>'></asp:HiddenField>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Name In Hindi" ControlStyle-Width="500px">
                    <ItemTemplate>
                        <asp:Label ID="empNameH" runat="server" Text='<%# HttpUtility.HtmlEncode(Convert.ToString(Eval("empNameH"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Name In English">
                    <ItemTemplate>
                        <asp:Label ID="empNameE" runat="server" Text='<%# HttpUtility.HtmlEncode(Convert.ToString(Eval("empNameE"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation in Hindi">
                    <ItemTemplate>
                        <asp:Label ID="designationH" runat="server" Text='<%# HttpUtility.HtmlEncode(Convert.ToString(Eval("designationH"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation in English">
                    <ItemTemplate>
                        <asp:Label ID="designationE" runat="server" Text='<%# HttpUtility.HtmlEncode(Convert.ToString(Eval("designationE"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="fileName" HeaderText="File Name" HtmlEncode="true" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                            CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                        <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="alert-warning" />
            <AlternatingRowStyle CssClass="alert-info" />
        </asp:GridView>

    </div>
</asp:Content>