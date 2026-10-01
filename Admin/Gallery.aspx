<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Gallery.aspx.cs" Inherits="Admin_Gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <h4>Add Gallery</h4><hr />

    <div class="form-horizontal col-md-6" >

          <div class="form-group">
            <div class="col-sm-8 col-sm-offset-4">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <asp:HiddenField ID="hfId" Value="0" runat="server" />
                <asp:HiddenField ID="hfImageName" Value="0" runat="server" />

            </div>
          </div>
        
          <div class="form-group">
            <label class="col-sm-4 control-label">Title</label>
            <div class="col-sm-8">
            
                <asp:TextBox ID="txtTitle" placeholder="Title"  CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTitle" ValidationGroup="A" ErrorMessage="* required"  runat="server"></asp:RequiredFieldValidator>
                
            </div>
          </div>

          <div class="form-group">
            <label class="col-sm-4 control-label">Description</label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtDescription" placeholder="Description" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
          </div>

          <div class="form-group">
            <label class="col-sm-4 control-label">Choose Images</label>
            <div class="col-sm-8">
               <asp:FileUpload ID="upImageFile" runat="server" />
               <small class="help-block">Please upload only PNG,JPG,JPEG,GIF extension file only</small>
            </div>
          </div>

          <div class="form-group">
            <div class="col-sm-offset-4 col-sm-8">     
                <asp:Button ID="btnSave" CssClass="btn btn-info" ValidationGroup="A" runat="server" Text="SAVE" 
                    onclick="btnSave_Click" />

                <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" 
                    Text="Cancel" onclick="btnCancel_Click" />
            </div>
         </div>

   </div>

   <div class="col-md-12">
        <div class="table-responsive">
            
             <asp:GridView ID="gvGalleryList" AutoGenerateColumns="false" 
                 CssClass="table table-bordered" runat="server" 
                 onrowcommand="gvGalleryList_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="SN">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Title" DataField="Title" />
                    <asp:BoundField HeaderText="Description" DataField="Description" />

                    <asp:BoundField HeaderText="Image Name" DataField="ImageName" />

                    <asp:BoundField HeaderText="Upload Date" DataFormatString="{0:dd/MM/yyyy}"  DataField="Date" />

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                             <asp:Button ID="btnEdit" CssClass="btn btn-default btn-xs"  runat="server" Text="Edit" CommandName="EditRecord" CommandArgument='<%#Eval("Id") %>'/>
                             
                             <asp:Button ID="btnDelete" CssClass="btn btn-danger btn-xs"  runat="server" Text="Delete" CommandName="DeleteRecord" CommandArgument='<%#Eval("Id") %>' OnClientClick='return confirm("Are you sure you want to be delete this ?");'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
             </asp:GridView>
        </div>
   </div>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contbootm" Runat="Server">
     <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=txtTitle]').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });
        });
        $(function () {
            $('[id*=txtDescription]').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });
        });
    </script>
</asp:Content>

