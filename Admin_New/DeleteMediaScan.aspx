<%@ Page Title="" Debug="true" Language="C#" ViewStateEncryptionMode="Always" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="DeleteMediaScan.aspx.cs" Inherits="Admin_DeleteMediaScan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" Runat="Server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <div class="alert-warning img-thumbnail" style="margin-bottom:10px!important;">
        <b style="font-size:large; font-family:'Times New Roman', Times, serif">Delete Media Scan</b> 
        </div> 
         <br /><div>
         <center>
          <asp:Label ID="lblErr" runat="server" CssClass="alert-danger" Font-Bold="true" Font-Size="Large"></asp:Label>  </center>
    </div>
    <br />  
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6"            
   OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="true" PageSize="30" CssClass ="alert-heading" >  
            <Columns>               
                <asp:TemplateField HeaderText="ID">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Media Title" ControlStyle-Width="700px">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Caption" runat="server" Text='<%#Eval("mdTitle") %>'></asp:Label>  
                    </ItemTemplate>                     
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Date">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("mdDate") %>'></asp:Label>  
                    </ItemTemplate>                   
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Media Scan File">  
                    <ItemTemplate> 
                        <asp:Image runat="server" ImageUrl='<%#Eval("mdImage") %>' Width="200px" />                      
                    </ItemTemplate>                    
                </asp:TemplateField>   
                <asp:TemplateField>  
                   <ItemTemplate>  
                        <asp:Button ID="btn_Delete" CssClass="btn-danger" OnClick="Delete" CommandArgument='<%# Eval("Id") %>' runat="server" Text="Delete" CommandName="Delete" />  
                    </ItemTemplate>                      
                </asp:TemplateField>  
            </Columns>  
            <HeaderStyle CssClass="alert-warning" />   
            <AlternatingRowStyle CssClass=" alert-danger" />        
        </asp:GridView>        
    </div>   
</asp:Content>

