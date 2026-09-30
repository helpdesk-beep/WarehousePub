<%@ Page Title="" Language="C#" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="DeleteVideo.aspx.cs" Inherits="Admin_DeleteVideo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" Runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <div class="alert-warning img-thumbnail" style="margin-bottom:10px!important;">
        <b style="font-size:large; font-family:'Times New Roman', Times, serif">Delete Video</b> 
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
                <asp:TemplateField HeaderText="Caption" ControlStyle-Width="700px">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Caption" runat="server" Text='<%#Eval("videoCaption") %>'></asp:Label>  
                    </ItemTemplate>                     
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Date">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("videoDate") %>'></asp:Label>  
                    </ItemTemplate>                   
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Video">  
                  <ItemTemplate>  
                            <video width="150" height="150" controls="controls">  
                                <source src='<%#Eval("video")%>' type="video/mp4">  
                            </video>  
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

