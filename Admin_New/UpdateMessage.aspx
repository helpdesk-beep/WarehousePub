<%@ Page Title="" Language="C#" ViewStateEncryptionMode="Always" Debug="true" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="UpdateMessage.aspx.cs" Inherits="Admin_UpdateNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" Runat="Server">
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
<script src="../NEW_CSS/js/jquery-1.12.4.js"></script>
<script src="../NEW_CSS/js/jquery-ui.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

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

       <div class="alert-warning img-thumbnail" style="margin-bottom:10px!important;">
        <b style="font-size:large; font-family:'Times New Roman', Times, serif">Update News</b>  
    </div><asp:Label ID="lblErr" runat="server"></asp:Label>
    <br />
        <div>     
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6" AllowPaging="true" PageSize ="30" 
            OnRowCancelingEdit="GridView1_RowCancelingEdit"  OnPageIndexChanging="GridView1_PageIndexChanging" 
   CssClass ="alert-heading"
OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">  
            <Columns>  
                <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="ID">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Caption">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Message" runat="server" Text='<%#Eval("titleInHn") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                         <textarea runat="server" type="text" id="txtTitle" style="width:500px; height:80px;" value='<%#Eval("titleInHn") %>'></textarea>
                    </EditItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Date">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("relDate") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate> 
                        <input runat="server" type="text" id="datepicker" maxlength="10" value='<%#Eval("relDate") %>' class="datepicker" /> 
                    </EditItemTemplate>  
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="File">  
                    <ItemTemplate> 
                          <a href="../<%#Eval("FileNameH") %>">View</a>
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

