<%@ Page Title="" Debug="true" ViewStateEncryptionMode="Always" Language="C#" MasterPageFile="~/MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="adminDefault.aspx.cs" Inherits="Admin_adminDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" Runat="Server">

  <div class="card" style="height:650px;">
           <div class="card-header deep-orange lighten-1 white-text">
              
                        <b>Latest News</b>
                    </div>
          <div>
              <marquee behavior="alternate" scrollamount="1" direction="up" onmouseover="this.stop();" onmouseout="this.start();" class="m14" height="600px!important" >
		    
		     <div class="col-lg-12 d14">
		        

                   <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
		         <div role="listitem" class="col-lg-12" style="font-size:14px;"">
                     <ul >
                        <li>
                           <asp:LinkButton ID="lnkView" runat="server" Text='<%# Eval("newsTitle") %>' OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton><b style=" font-size:x-small; color:cadetblue;"> <%# Eval("newsDate")%> </b></li>
                          </ul>

		         </div>
		         </ItemTemplate>
                       </asp:Repeater>
		     </div>
		      
		 </marquee></div>
      </div>
</asp:Content>

