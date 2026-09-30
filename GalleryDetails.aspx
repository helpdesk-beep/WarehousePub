<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="GalleryDetails.aspx.cs" Inherits="GalleryDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" rel="stylesheet" href="assets/css/lightbox.css">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
   
<section class="content_wrapper">
    <div class="container">
        <!-- Example row of columns -->
            <div class="row">
                
            <div class="col-md-12">
              
                <div class="row-fluid">
                    <h3 class="red caps" style="letter-spacing:1px;"><asp:Literal ID="litTitle" runat="server"></asp:Literal></h3> 
                    <hr class="line-red"/>
                        

                        <asp:Repeater ID="rptGallery" runat="server">
                             
                            <ItemTemplate>
                                <%# Container.ItemIndex % 4 == 0 ? "<div class='row'>" : ""%>
                                    
                                    <div class="col-md-3">
                                        <a href='<%# "Admin/image_file/" + Eval("ImageName")%>' data-lightbox="example-set" data-title='<%#Eval("Description") %>'><img class="img-responsive gall-img" src='<%# "Admin/image_file/" + Eval("ImageName")%>' alt='<%#Eval("Description") %>'/></a>
                                    </div>

                                 <%# (Container.ItemIndex + 1) % 4 == 0 ? "</div>" : ""%>
                            </ItemTemplate>

                        </asp:Repeater>
                  </div>
            </div>
            </div>
    </div>
</section>


</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="script" Runat="Server">
     <script src="assets/js/lightbox-plus-jquery.min.js"></script>
</asp:Content>

