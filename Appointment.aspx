<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Appointment.aspx.cs" Inherits="Appointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
     <section class="content_wrapper">
        <div class="container">
        <!-- Example row of columns -->
          <div class="row">

          <div class="col-md-12">
              <nav aria-label="breadcrumb">
                  <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Default.aspx">Home</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Appointment</li>
                  </ol>
                </nav>
            </div>

    <div class="col-md-12">
              
        <div class="row-fluid">
           <h3 class="red" style="letter-spacing:1px;">APPOINTMENT</h3> <hr class="line-red"/>

           <!-- Error Message -->
            <strong><asp:Label ID="lblErrorMsg" CssClass="red" runat="server"></asp:Label></strong>


          
            <div class="text_content">
                <ul class="text_content">
                    <asp:Repeater ID="rptAppnt" runat="server">
                
                        <ItemTemplate>
                            <li> 
                                <i class="fa fa-chevron-circle-right red"></i> &nbsp;
                                <a href='<%# "Admin/appointment_file/" + Eval("FileName")%>' class="red"> 
                                    <%#Eval("Title") %>
                                </a>
                            </li>
                        </ItemTemplate>
                
                    </asp:Repeater>
                </ul>

               
           </div>
           <hr />
        </div>
     </div>
     
          </div>

         </div> <!-- /container -->
   </section>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

