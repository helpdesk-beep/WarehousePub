<%@ Page Title="" Language="C#" ViewStateEncryptionMode="Always" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="ViewImage.aspx.cs" Inherits="Admin_ViewImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" Runat="Server">
    <link href="../NEW_CSS/Gallery/family.css" rel="stylesheet" />
    <link href="../NEW_CSS/Gallery/baguetteBox.min.css" rel="stylesheet" />
    <link href="../NEW_CSS/Gallery/thumbnail-gallery.css" rel="stylesheet" />
    <style type="text/css">
      .gallery-container {
             margin-left:0px!important;
        padding-left:0px!important; 
        }
        .tz-gallery {
             margin-left:0px!important;
        padding-left:0px!important;

        }
        .row {
        margin-left:0px!important;
        padding-left:0px!important;        

        }
        .col-md-4 {
        margin-left:0px!important;
        padding-left:0px!important;
        padding-right:40px!important;
        
        }
        .thumbnail {
         text-align:center;
        }
    </style>
    
    <div class="container gallery-container">       
        <div class="alert-warning img-thumbnail" style="margin-bottom:10px!important;">
            <b style="font-size:large; font-family:'Times New Roman', Times, serif">View Image Gallery</b> 
        </div> 

        <div class="tz-gallery">            
            <div class="row" >
             <asp:Repeater ID="rptCarousel" runat="server">
                <ItemTemplate>
                <div class="col-sm-6 col-md-4">
                    <div class="thumbnail">                    
                             <a class="lightbox" href='<%# Eval("image") %>' runat="server">
                            <img src='<%# Eval("image") %>' style="height:220px;" alt="RAJBHAVAN" runat="server"/>
                        </a>                                                
                        <div class="caption">                      
                            <p><%# Eval("imageCaption") %> - <b style=" font-size:x-small; color:cadetblue;"> <%# Eval("imageDate")%> </b> </p>
                        </div>
                    </div>
                </div>
                  </ItemTemplate>
            </asp:Repeater>
            </div>               
        </div>
      </div>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/baguettebox.js/1.8.1/baguetteBox.min.js"></script>
            <script>
                baguetteBox.run('.tz-gallery');
            </script>
</asp:Content>

