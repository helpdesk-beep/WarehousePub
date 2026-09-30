<%@ Page Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Circular.aspx.cs" Inherits="Circular" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
 <section class="content_wrapper">
        <div class="container">
        <!-- Example row of columns -->
          <div class="row">

          <div class="col-md-12">
              <nav aria-label="breadcrumb">
                  <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Default.aspx">Home</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Orders,Letters & Circulars</li>
                  </ol>
                </nav>
            </div>

    <div class="col-md-12">
              
              <div class="row-fluid">

              <h3 class="red caps" style="letter-spacing:1px;">Orders, Letters & Circulars</h3>
              <hr class="line-red">

                <div class="important">
                  <h2>HO Level Orders, Letters & Circulars</h2>

                    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true" style="padding:1em;">

                   
                    <asp:Repeater ID="rptEst" Visible="false" runat="server">
                    <HeaderTemplate>
                      <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="headingOne">
                          <h4 class="panel-title">
                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                              Establishment Section
                            </a>
                          </h4>
                        </div>
                        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                          <div class="panel-body">

                          </HeaderTemplate>
                              <ItemTemplate>
                                <h5 class="red"> 
                                 <i class="fa fa-chevron-circle-right"></i> &nbsp;
                                   <a href='<%# "Admin/lettercircular_file/" + Eval("FileName")%>' class="red"> 
                                        <%#Eval("Title") %>
                                    </a>
                                 </h5>
                              </ItemTemplate>
                              <FooterTemplate>
                          </div>
                        </div>
                      </div>
                      </FooterTemplate>
                      </asp:Repeater>



                      <asp:Repeater ID="rptSecr" Visible="false" runat="server">
                      <HeaderTemplate>
                      <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="headingTwo">
                          <h4 class="panel-title">
                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                              Secretariat Section
                            </a>
                          </h4>
                        </div>
                        <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                          <div class="panel-body">
                          </HeaderTemplate>
                          <ItemTemplate>
                            <h5 class="red"> 
                                 <i class="fa fa-chevron-circle-right"></i> &nbsp;
                                   <a href='<%# "Admin/lettercircular_file/" + Eval("FileName")%>' class="red"> 
                                        <%#Eval("Title") %>
                                    </a>
                                 </h5>
                          </ItemTemplate>
                          <FooterTemplate>
                          </div>
                        </div>
                      </div>
                      </FooterTemplate>
                      </asp:Repeater>


                      <asp:Repeater ID="rptBus" Visible="false" runat="server">
                      <HeaderTemplate>
                      <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="headingThree">
                          <h4 class="panel-title">
                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                              Business Section
                            </a>
                          </h4>
                        </div>
                        <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                          <div class="panel-body">
                          </HeaderTemplate>
                          <ItemTemplate>
                            <h5 class="red"> 
                                 <i class="fa fa-chevron-circle-right"></i> &nbsp;
                                   <a href='<%# "Admin/lettercircular_file/" + Eval("FileName")%>' class="red"> 
                                        <%#Eval("Title") %>
                                    </a>
                                 </h5>
                          </ItemTemplate>
                          <FooterTemplate>
                          </div>
                        </div>
                      </div>
                      </FooterTemplate>
                      </asp:Repeater>


                      <asp:Repeater ID="rptTech" Visible="false" runat="server">
                      <HeaderTemplate>
                      <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="headingFour">
                          <h4 class="panel-title">
                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseFour" aria-expanded="false" aria-controls="collapseThree">
                              Technical Section
                            </a>
                          </h4>
                        </div>
                        <div id="collapseFour" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingFour">
                          <div class="panel-body">
                          </HeaderTemplate>
                          <ItemTemplate>
                            <h5 class="red"> 
                                 <i class="fa fa-chevron-circle-right"></i> &nbsp;
                                   <a href='<%# "Admin/lettercircular_file/" + Eval("FileName")%>' class="red"> 
                                        <%#Eval("Title") %>
                                    </a>
                                 </h5>
                          </ItemTemplate>
                          <FooterTemplate>
                          </div>
                        </div>
                      </div>
                      </FooterTemplate>
                      </asp:Repeater>


                      <asp:Repeater ID="rptAcc" Visible="false" runat="server">
                      <HeaderTemplate>
                      <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="headingFive">
                          <h4 class="panel-title">
                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseFive" aria-expanded="false" aria-controls="collapseThree">
                              Accounts/Audit Section
                            </a>
                          </h4>
                        </div>
                        <div id="collapseFive" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingFive">
                          <div class="panel-body">
                          </HeaderTemplate>
                          <ItemTemplate>
                            <h5 class="red"> 
                                 <i class="fa fa-chevron-circle-right"></i> &nbsp;
                                   <a href='<%# "Admin/lettercircular_file/" + Eval("FileName")%>' class="red"> 
                                        <%#Eval("Title") %>
                                    </a>
                                 </h5>
                         </ItemTemplate>
                         <FooterTemplate>
                          </div>
                        </div>
                      </div>
                       </FooterTemplate>
                      </asp:Repeater>


                      <asp:Repeater ID="rptCon" Visible="false" runat="server">
                      <HeaderTemplate>
                      <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="headingSix">
                          <h4 class="panel-title">
                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseSix" aria-expanded="false" aria-controls="collapseThree">
                              Construction Section
                            </a>
                          </h4>
                        </div>
                        <div id="collapseSix" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingSix">
                          <div class="panel-body">
                          </HeaderTemplate>
                          <ItemTemplate>
                            <h5 class="red"> 
                                 <i class="fa fa-chevron-circle-right"></i> &nbsp;
                                   <a href='<%# "Admin/lettercircular_file/" + Eval("FileName")%>' class="red"> 
                                        <%#Eval("Title") %>
                                    </a>
                                 </h5>
                         </ItemTemplate>
                         <FooterTemplate>
                          </div>
                        </div>
                      </div>
                       </FooterTemplate>
                      </asp:Repeater>

                    </div>
                </div>
              </div>
              
              
           </div>
           
          </div>

         </div> <!-- /container -->
   </section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

