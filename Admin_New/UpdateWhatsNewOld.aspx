<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/adminMaster.master" EnableViewState="true" EnableSessionState="True" EnableEventValidation="false" AutoEventWireup="true" CodeFile="UpdateWhatsNewOld.aspx.cs" Inherits="Admin_UpdateWhatsNew" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
   <%-- <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>--%>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
<script src="../NEW_CSS/js/jquery-1.12.4.js"></script>
<script src="../NEW_CSS/js/jquery-ui.js"></script>
<script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>

   <script type="text/javascript">

        $(function () {
            $(".cal").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                minDate: new Date("01/23/2018")
            });
        });
    </script>

    <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
        <b style="font-size: large; font-family: 'Times New Roman', Times, serif">Update What's New?</b>
    </div>
    <asp:Label ID="lblErr" runat="server"></asp:Label>
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
                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <table>
                            <tr class="alert-success">
                                <td>
                                    <asp:Label ID="lbl_TitleHn" runat="server" Text='<%#Eval("titleInHn") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr class="alert-info">
                                <td>
                                    <asp:Label ID="lbl_TitleEn" runat="server" Text='<%#Eval("titleInEn") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <textarea runat="server" class="alert-success" type="text" id="txtTitleHn" style="width: 200px; height: 80px;" value='<%#Eval("titleInHn") %>'></textarea>
                        <textarea runat="server" class="alert-info" type="text" id="txtTitleEn" style="width: 200px; height: 80px;" value='<%#Eval("titleInEn") %>'></textarea>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Release Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ReleaseDate" runat="server" Text='<%#Eval("releaseDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <input type="text" runat="server" id="releaseDate" name="releaseDate" maxlength="10" value='<%#Eval("releaseDate","{0:d}") %>' class="form-control cal"/>

                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expire Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ExpireDate" runat="server" Text='<%#Eval("expireDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <input type="text" runat="server" id="expireDate" maxlength="10" value='<%#Eval("expireDate","{0:d}") %>' class="form-control cal" />

                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="File">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkViewH" runat="server" Text='<%# Eval("fileNameHn") %>' OnClick="ViewH" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkViewE" runat="server" Text='<%# Eval("fileNameEn") %>' OnClick="ViewE" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:FileUpload ID="FileUpload2" runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>

            </Columns>
            <HeaderStyle CssClass="alert-warning" />
            <AlternatingRowStyle CssClass="alert-success" />
        </asp:GridView>
    </div>
</asp:Content>

