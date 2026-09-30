<%@ Page Title="" Language="C#" ViewStateEncryptionMode="Always" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="UpdateMediaScan.aspx.cs" Inherits="Admin_UpdateMediaScan" %>

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
        <b style="font-size:large; font-family:'Times New Roman', Times, serif">Update Media Scan</b>  
    </div>
     

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
                <asp:TemplateField HeaderText="Media Title">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Caption" runat="server" Width="500px" Text='<%#Eval("mdTitle") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                         <textarea runat="server" type="text" id="txtCaption" style="width:500px; height:80px;" value='<%#Eval("mdTitle") %>'></textarea>
                    </EditItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Date">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("mdDate") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate> 
                        <input runat="server" type="text" id="datepicker" maxlength="10" value='<%#Eval("mdDate") %>' class="datepicker" /> 
                    </EditItemTemplate>  
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Media Scan">  
                    <ItemTemplate> 
                        <asp:Image runat="server" ImageUrl='<%#Eval("mdImage") %>' Width="50%" />                      
                    </ItemTemplate>  
                    <EditItemTemplate>                       
                       <asp:FileUpload ID="FileUpload1" runat="server" />                          
                    </EditItemTemplate>  
                </asp:TemplateField>   
            </Columns>  
            <HeaderStyle CssClass="alert-warning" />   
            <AlternatingRowStyle CssClass="alert-success" />        
        </asp:GridView>
</asp:Content>

