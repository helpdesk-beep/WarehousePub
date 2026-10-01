<%@ Page Title="" Language="C#" MasterPageFile="../MasterPages/adminMaster.master" EnableViewState="true" EnableSessionState="True" AutoEventWireup="true" CodeFile="UpdateAchalSampatti.aspx.cs" Inherits="Admin_UpdateAchalSampatti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="../NEW_CSS/js/jquery-1.12.4.js"></script>
    <script src="../NEW_CSS/js/jquery-ui.js"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>


    <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
        <b style="font-size: large; font-family: 'Times New Roman', Times, serif">Update Achal Sampatti</b>
    </div>
    <asp:Label ID="lblErr" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <div>
        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label2" runat="server" Font-Bold="true"></asp:Label>
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
                    <asp:HiddenField runat="server" ID="hdddlYear" />
                </td>
                <td></td>
            </tr>
        </table>
    </div>
    <br />
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6" AllowPaging="true" PageSize="30"
            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnPageIndexChanging="GridView1_PageIndexChanging"
            CssClass="alert-heading" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" />
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("id") %>'></asp:HiddenField>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name in Hindi">
                    <ItemTemplate>
                        <asp:Label ID="lbl_NameH" runat="server" Text='<%#Eval("empNameH") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtNameH" Text='<%#Eval("empNameH") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name in English">
                    <ItemTemplate>
                        <asp:Label ID="lbl_NameE" runat="server" Text='<%#Eval("empNameE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtNameE" Text='<%#Eval("empNameE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation in Hindi">
                    <ItemTemplate>
                        <asp:Label ID="lbl_DesignationH" runat="server" Text='<%#Eval("designationH") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtDesignationH" Text='<%#Eval("designationH") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation in English">
                    <ItemTemplate>
                        <asp:Label ID="lbl_DesignationE" runat="server" Text='<%#Eval("designationE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtDesignationE" Text='<%#Eval("designationE") %>'></asp:TextBox>
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

