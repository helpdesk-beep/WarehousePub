<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Achivement.aspx.cs" Inherits="Achivement" %>

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
          <h4 class=" list-group-item active">PROFILE INFORMATION</h4>
          <a href="OrgStructure.aspx" class="list-group-item">ORGANISATION STRUCTURE</a>
          <a href="InfoProfile.aspx" class="list-group-item">AIMS / OBJECTIVES</a>
          <a href="Achivement.aspx" class="list-group-item">ACHIEVEMENTS</a>
        </div>
    </div>
    <div class="col-md-9">
              
        <div class="row-fluid">
            <h3 class="red" style="letter-spacing:1px;">ACHIEVEMENTS</h3> <hr class="line-red"/>
            <img src="assets/img/Achievements_and_Awards.jpg" class="img-rounded" style="width:100%;height:300px;"  />
            <hr />
            <h4>Corporation's Recent Achievement</h4>

            <ul class="text_content">
                <li>The corporation received the Warehousing sector 2000-01 productivity award “Certificate of Merit” from the National Productivity Council of India on the 16th day of February 2004 in New Delhi.</li>
                <li>The corporation received the Warehousing sector 2001-02 award “Second Best Productivity Performance” from the National Productivity Council of India on the 16th day of February 2004 in New Delhi.</li>
                <li>The corporation's two branches namely Itarsi & Dewas received ISO 9001:2000 certification in 2001.</li>
                <li>The corporation received the "Manthan Award" from the World Committee & Board of Directors Dr. Professor Patel(A Brook) for better performance on "Online Application for Registration of Private Warehouses in the year 2013."</li>
            </ul>
            <hr />
         </div>
     </div>
     </div>
     </div>
     </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="widget" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" Runat="Server">
   
</asp:Content>

