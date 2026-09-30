<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true"
    CodeFile="BusinessReport.aspx.cs" Inherits="BusinessReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="banner" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    
    <section class="content_wrapper">
    <div class="container">
    <!-- Example row of columns -->
        <div class="row">
            <div class="col-md-12">
              <nav aria-label="breadcrumb">
                  <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Default.aspx">Home</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Business Report</li>
                  </ol>
                </nav>
            </div>


            <div class="col-md-12">
              
            <div class="row-fluid">
                <h3 class="red" style="letter-spacing:1px;">Monthly Business Report</h3> <hr class="line-red"/>

                 <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#monthlyReport">Monthly Business Report </a></li>
                        <%--<li><a data-toggle="tab" href="#busSecInfo">Business Section Information</a></li>
                        <li><a data-toggle="tab" href="#hiringGodown">Hiring &amp; Dehiring of Godown </a></li>--%>
                    </ul>
                
                    <div class="tab-content">
                        <div id="monthlyReport" class="tab-pane fade in active">
                             
                         <ul class="red list-unstyled text_content">
                          <asp:Repeater ID="rptMonRep" runat="server">
                                <ItemTemplate>
                                     <li><i class="fa fa-chevron-circle-right"></i> &nbsp; <a href='<%# "Admin/business_report_file/" + Eval("Filename")%>' target="_blank" class="heading"><%#Eval("Titlemonthyear") %></a></li>

                                </ItemTemplate>
                          </asp:Repeater>
                        </ul>
                            
                        </div>
                        <div id="busSecInfo" class="tab-pane fade">
                            <p>
                                <iframe id="Iframe1" scrolling="auto" frameborder="0" allowtransparency="true" width="100%" height="700px" src="PDF/StorageCharges.pdf" runat="server"></iframe>
                        
                            </p>
                        </div>
                        <div id="hiringGodown" class="tab-pane fade">
                            <p>
                                  <iframe id="Iframe2" scrolling="auto" frameborder="0" allowtransparency="true" width="100%" height="700px" src="PDF/Hiring Dehiring of Godown.pdf" runat="server"></iframe>
                        

                             </p>
                        </div>
                        
                    </div>

                
            </div>
            </div>
     
        </div>

        </div> <!-- /container -->
</section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="widget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" runat="Server">
</asp:Content>
