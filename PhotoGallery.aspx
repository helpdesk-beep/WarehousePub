<%@ Page Language="C#" AutoEventWireup="true" Debug="true" MasterPageFile="~/main.master" CodeFile="PhotoGallery.aspx.cs" Inherits="PhotoGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
     <link href="../Admin_New/css/style.css" rel="stylesheet" />
    <script src='../Admin_New/js/jquery.min.js'></script>
    <script src='../Admin_New/js/font-size.js'></script>
    <script src="../Admin_New/js/bootstrap.min.js"></script>
    <script src='../Admin_New/js/superfish.js'></script>
    <script src='../Admin_New/js/styleswitcher.js'></script>
    <script src='../Admin_New/js/jquery.meanmenu.js'></script>
    <script src='../Admin_New/js/imagelightbox.min.js'></script>
    <script src='../Admin_New/js/light-box.js'></script>
    <script src='../Admin_New/js/jquery.flexisel.js'></script>
    <script src='../Admin_New/js/custom.js'></script>
    <link rel="stylesheet" href="../Admin_New/css/font-awesome.min.css">

    <div class="container">
        <div class="clearfix"></div>
        <div class="row pt20">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="content-area">
                    <div class="row">
                        <asp:Repeater ID="rptCarousel" runat="server">
                            <ItemTemplate>
                                <div class='col-md-4 col-xs-12 pb30'>
                                    <a runat="server" href='<%# Eval("image") %>' data-imagelightbox='f'>
                                        <img runat="server" src='<%# Eval("image") %>' class='photo-gallery' alt='<%# Eval("imageCaption") %>' title='<%# Eval("imageCaption") %>'></a>
                                    <div class='photo_heading'>
                                        <p class='heading3' style="font-size: small;"><%# Eval("imageCaption") %> - <b style="font-size: x-small; color: cadetblue;"><%# Eval("imageDate")%> </b></p>
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
