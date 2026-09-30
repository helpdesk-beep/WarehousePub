<%@ Page Title="" Debug="true" EnableEventValidation="true" ViewStateEncryptionMode="Always"  Language="C#" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="DeleteTourProgram.aspx.cs" Inherits="Admin_DeleteTourProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" Runat="Server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <div class="alert-warning img-thumbnail" style="margin-bottom:10px!important;">
        <b style="font-size:large; font-family:'Times New Roman', Times, serif">Delete Tour Program</b> 
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
                <asp:TemplateField HeaderText="Tour Program in English" ControlStyle-Width="700px">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Caption" runat="server" Text='<%#Eval("tour_prog_e") %>'></asp:Label>  
                    </ItemTemplate>                     
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Tour Program in Hindi">  
                    <ItemTemplate>  
                        <asp:Label ID="lbltour" runat="server" Text='<%#Eval("tour_prog_h") %>'></asp:Label>  
                    </ItemTemplate>                   
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Tour Date">  
                <ItemTemplate>
                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("tour_date") %>'></asp:Label>
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

