<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="NewsDetails.aspx.cs" Inherits="NewsDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
<section class="content_wrapper">
      <div class="container">
        <!-- Example row of columns -->
          <div class="row">
            <div class="col-md-12">
                <div class="row-fluid">
                    <h3 class="red" style="letter-spacing:1px;">NEWS DETAILS</h3> <hr class="line-red"/>
                     <div class="text_content">
                        <h4><strong><asp:Label ID="lblTitle" runat="server"></asp:Label></strong></h4>
                        <p><asp:Label ID="lblDesc" runat="server"></asp:Label></p>
                        
                        <%-- <asp:HyperLink ID="hplDownload" Visible="false" Target="_blank" runat="server" CssClass="btn btn-info" Text="Download Now"></asp:HyperLink>
                         --%>
                         <iframe id="frFileViewer" scrolling="auto" frameborder="0" allowtransparency="true"   runat="server"></iframe>
                        




                    </div>
                </div>
             </div>
          </div>
       </div> <!-- /container -->
   </section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

