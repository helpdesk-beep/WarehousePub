<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Network.aspx.cs" Inherits="Network" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="banner" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
<section class="content_wrapper">
        <div class="container">
        <!-- Example row of columns -->
          <div class="row">
    <div class="col-md-3">
       <div class="list-group">
          <h4 class="list-group-item active">NETWORK</h4>
          <a href="Contact.aspx" class="list-group-item">CORPORATE OFFICE </a>
          <a href="RegionalContact.aspx" class="list-group-item">REGIONAL OFFICES</a>
          <a href="BranchContact.aspx" class="list-group-item">BRANCH OFFICES</a>
          <a href="NetworkFlow.aspx" class="list-group-item">NETWORK HIERARCHY </a>
          <a href="GeographicalView.aspx" class="list-group-item">GMAP MPWLC</a>
        </div>
    </div>
    <div class="col-md-9">
        <div class="row-fluid">
            <h3 class="red" style="letter-spacing:1px;">CORPORATE OFFICE ADDRESS</h3> <hr class="line-red"/>
            <address style="font-size:18px;">
                
                Office Complex , Block 'A' Gautam Nagar, Bhopal<br>
                Phone: +91-755-2600509, 510  <br />
                Fax : +91-755-2600384<br />
                Email : <span>hompwlc[at]gmail[dot]com , mpwlchelpdesk[at]gmail[dot]com </span>
               
            </address>
            <hr/>  
        </div>
     </div>
     
          </div>

         </div> <!-- /container -->
   </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="widget" Runat="Server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" Runat="Server"></asp:Content>



