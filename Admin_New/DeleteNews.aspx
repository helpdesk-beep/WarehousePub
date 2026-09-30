<%@ Page Title="" Debug="true" Language="C#" ViewStateEncryptionMode="Always" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="DeleteNews.aspx.cs" Inherits="Admin_DeleteNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" Runat="Server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <link href="../NEW_CSS/css/jquery-ui.css" rel="stylesheet" />
  <script src="../NEW_CSS/js/jquery-1.12.4.js"></script>
  <script src="../NEW_CSS/js/jquery-ui.js"></script>
  <script>
      $(function () {
          $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy' });
      });
  </script>
     <div class="alert-warning img-thumbnail" style="margin-bottom:10px!important;">
        <b style="font-size:large; font-family:'Times New Roman', Times, serif">Delete News</b> 
     </div> 
     <br />
     <div>
         <center>
             <asp:Label ID="lblErr" runat="server" CssClass="alert-danger" Font-Bold="true" Font-Size="Large"></asp:Label>
         </center>
    </div>
    <br />  
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6" AllowPaging="true" PageSize ="30" 
            OnPageIndexChanging="GridView1_PageIndexChanging" CssClass ="alert-heading" >  
            <Columns>               
                <asp:TemplateField HeaderText="ID">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="News Title" ControlStyle-Width="400px">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Caption" runat="server" Text='<%#Eval("newsTitle") %>'></asp:Label>  
                    </ItemTemplate>                     
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="News Date">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("newsDate") %>'></asp:Label>  
                    </ItemTemplate>                   
                </asp:TemplateField> 
                <asp:BoundField DataField="fileName" HeaderText="File Name"/>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                            CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                        <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField>  
                   <ItemTemplate>  
                        <asp:Button ID="btn_Delete" CssClass="btn-danger" OnClick="Delete" CommandArgument='<%# Eval("Id") %>' runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Delete?')"/>  
                    </ItemTemplate>                     
                </asp:TemplateField>  
            </Columns>  
            <HeaderStyle CssClass="alert-warning" />   
            <AlternatingRowStyle CssClass="alert-danger" />        
        </asp:GridView>        
    </div>   
</asp:Content>