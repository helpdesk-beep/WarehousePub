<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Tariff.aspx.cs" Inherits="Tariff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="banner" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
<section class="content_wrapper">
    <div class="container">
    <!-- Example row of columns -->
        <div class="row">

        <div class="col-md-12">
              
            <div class="row-fluid">
               <h3 class="red" style="letter-spacing:1px;">TARIFF</h3> <hr class="line-red"/>
                <div class="">
                    
                    <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#rate">RATES / REBATE </a></li>
                        <li><a data-toggle="tab" href="#bot">BOT Proposed Branches</a></li>
                        <li><a data-toggle="tab" href="#tech">Purchase Material of technical section </a></li>
                        <li><a data-toggle="tab" href="#weigh">Weighbridges</a></li>
                    </ul>
                
                    <div class="tab-content">
                        <div id="rate" class="tab-pane fade in active">
                             <p>
                                 <iframe scrolling="auto" frameborder="0" allowtransparency="true" width="100%" height="700px" src="PDF/StorageCharges.pdf" runat="server"></iframe>
                        
                                 <strong><a href="PDF/StorageCharges.pdf" target="_blank" class="red">Storage Charges</a></strong>
                             </p>
                        </div>
                        <div id="bot" class="tab-pane fade">
                            <p>
                                <iframe id="Iframe1" scrolling="auto" frameborder="0" allowtransparency="true" width="100%" height="700px" src="PDF/BOT.pdf" runat="server"></iframe>
                        
                                <strong><a href="PDF/BOT.pdf" target="_blank" class="red">BOT Proposed Branches</a></strong>
                            </p>
                        </div>
                        <div id="tech" class="tab-pane fade">
                            <p>
                                <iframe id="Iframe2" scrolling="auto" frameborder="0" allowtransparency="true" width="100%" height="700px" src="PDF/PurchaseMaterial_TA.pdf" runat="server"></iframe>
                        
                                <strong><a href="PDF/PurchaseMaterial_TA.pdf" target="_blank" class="red">Purchase Material of technical section </a></strong>
                            </p>
                        </div>
                        <div id="weigh" class="tab-pane fade">
                            <p>
                                <iframe id="Iframe3" scrolling="auto" frameborder="0" allowtransparency="true" width="100%" height="700px" src="PDF/Weighbridges.pdf" runat="server"></iframe>
                        
                                <strong><a href="PDF/Weighbridges.pdf" target="_blank" class="red">Weighbridges</a></strong>
                          </p>
                        </div>
                    </div>


                </div>
               <hr />
            </div>
         </div>
     
     </div>

             </div> <!-- /container -->
   </section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="widget" Runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

