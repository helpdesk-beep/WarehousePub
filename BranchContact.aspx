<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="BranchContact.aspx.cs" Inherits="BranchContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
         #map{
      height: 400px;
      width: 100%;
    }
    </style>
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
        </div>
    </div>
    <div class="col-md-9">
        <div class="row-fluid">
            <h3 class="red" style="letter-spacing:1px;">BRANCH OFFICE ADDRESS</h3> <hr class="line-red"/>
            <div class="well well-sm well-danger">
                <label>SELECT REGIONAL OFFICES </label> &nbsp;
                <asp:DropDownList ID="ddlRO" runat="server" 
                    onselectedindexchanged="ddlRO_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                    &nbsp;&nbsp;
               <label> SELECT DISTRICT </label>&nbsp;
                <asp:DropDownList ID="ddlDst" runat="server" AutoPostBack="true"></asp:DropDownList>
                <asp:Button ID="btnSearch" CssClass="pull-right btn btn-warning btn-sm badge " style="letter-spacing:1px;" Text="Search" runat="server" 
                    onclick="btnSearch_Click" />
            </div>

           
            
            <asp:Repeater ID="rptBranch" runat="server">
                <HeaderTemplate>
                    <h4 class="red text-center">BRANCH LIST</h4><hr />
                </HeaderTemplate>
                <ItemTemplate>
                <%# Container.ItemIndex % 3 == 0 ? "<div class='row'>" : ""%>
                 
                    <div class="col-md-4 branch_add_box">
                        <div class="panel panel-info" >
                          <div class="panel-heading" style="letter-spacing:1px;"> <span class="text-uppercase text-primary"><%#Eval("DepotName")%></span></div>
                          <div class="panel-body">
                            <h4 class="text-uppercase"> <%#Eval("NodalOfficeName")%> </h4>
                            <p class="text-capitalize"> <i class="fa fa-map-marker"></i> <%#Eval("DepotAddress")%> </p> 
                            <p> <i class="fa fa-phone"></i> <%#Eval("PhoneNo")%> </p> 
                            <p> <i class="fa fa-envelope-o"></i> <%#Eval("Email")%> </p>
                          </div>
                          <div class="panel-footer">
                            District : <span class="text-capitalize"> <%#Eval("District_Name")%> </span>
                          </div>
                        </div>
                    </div>

                     <%# (Container.ItemIndex + 1) % 3 == 0 ? "</div>" : ""%>
                


                </ItemTemplate>
            </asp:Repeater> 
             
        </div>
     </div>

     
          </div>

         </div> <!-- /container -->
   </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="widget" Runat="Server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" Runat="Server"></asp:Content>

