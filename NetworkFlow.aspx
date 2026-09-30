<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="NetworkFlow.aspx.cs" Inherits="NetworkFlow" %>

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
          <h4 class="list-group-item active">CONTACT INFORMATION</h4>
          <a href="Contact.aspx" class="list-group-item">CORPORATE OFFICE </a>
          <a href="RegionalContact.aspx" class="list-group-item">REGIONAL OFFICES</a>
          <a href="BranchContact.aspx" class="list-group-item">BRANCH OFFICES</a>
          <a href="NetworkFlow.aspx" class="list-group-item">NETWORK HIERARCHY </a>
        </div>
    </div>
    <div class="col-md-9">
        <div class="row-fluid">
            <h3 class="red" style="letter-spacing:1px;">MPWLC NETWORK HIERARCHY</h3> <hr class="line-red"/>
            
            <img class="img-responsive" src="assets/img/networkflow.png" />
        </div>
     </div>
     
          </div>

         </div> <!-- /container -->
   </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="widget" Runat="Server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" Runat="Server"></asp:Content>

