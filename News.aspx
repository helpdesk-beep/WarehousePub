<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <section class="content_wrapper">
        <div class="container">
        <!-- Example row of columns -->
          <div class="row">
    <div class="col-md-12">
        <div class="row-fluid">
             <h3 class="red" style="letter-spacing:1px;">NEWS</h3> <hr class="line-red"/>
             <div class="text_content">
             <ul class="list-unstyled">
              <asp:Repeater ID="rptNewsUpdate" runat="server">
                <ItemTemplate>
                    <li><a href='<%# "NewsDetails.aspx?Id=" + Eval("Id")%>'><%#Eval("Title") %></a></li>
                </ItemTemplate>
              </asp:Repeater>
              </ul>
              </div>
         </div>
     </div>
     
          </div>

         </div> <!-- /container -->
   </section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

