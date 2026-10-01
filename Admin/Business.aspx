<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Business.aspx.cs" Inherits="Admin_Business" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <h4>Add Business Excel Data</h4><hr />

    <div class="form-horizontal col-md-6" >

          <div class="form-group">
            <div class="col-sm-8 col-sm-offset-4">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                

            </div>
          </div>

          <div class="form-group">
            <label class="col-sm-4 control-label">Select File</label>
            <div class="col-sm-8">
               <asp:FileUpload ID="upFile" runat="server" />
               
               <small class="help-block">Please Upload Only xls, xlsx File</small>
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
       
    </script>
</asp:Content>

