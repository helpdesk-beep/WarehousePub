<%@ Page Title="" Debug="true" ViewStateEncryptionMode="Always" Language="C#" MasterPageFile="/MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="UpdateImage.aspx.cs" Inherits="Admin_UpdateImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="../NEW_CSS/js/jquery-1.12.4.js"></script>
    <script src="../NEW_CSS/js/jquery-ui.js"></script>
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>

    <script type="text/javascript">
        $(function () {
            $('.datepicker').datepicker({
                dateFormat: 'dd/mm/yy',
                defaultDate: -1,
                minDate: new Date("01/23/2018"),
                maxDate: 0
            });
        });
    </script>
    <div class="container">
        <div class="alert-warning img-thumbnail" style="margin-bottom: 10px!important;">
            <b style="font-size: large; font-family: 'Times New Roman', Times, serif">Update Image</b>
        </div>
        <div class="img-thumbnail">

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2" AllowPaging="true" PageSize="30"
                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnPageIndexChanging="GridView1_PageIndexChanging"
                CssClass="alert-heading table"
                OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
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
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Caption">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Caption" runat="server" Text='<%#Eval("imageCaption") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <textarea runat="server" type="text" id="txtCaption" style="width:100%" value='<%#Eval("imageCaption") %>'></textarea>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("imageDate") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <input runat="server" type="text" id="datepicker" maxlength="10" style="width:80px;" value='<%#Eval("imageDate") %>' class="datepicker" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%#Eval("image") %>' Width="120px" />
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
    </div>
</asp:Content>

