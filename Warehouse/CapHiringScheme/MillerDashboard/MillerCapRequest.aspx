<%@ Page Title="" Language="C#" MasterPageFile="~/CapHiringScheme/MillerDashboard/Miller.master" AutoEventWireup="true" CodeFile="MillerCapRequest.aspx.cs" Inherits="JVSMiller_MillerCapRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <!-- page-wrapper -->
        <div id="page-wrapper">
            <div class="row">
                <div class="col-md-12">
                    
                    <div class="panel box-primary">
                        <div class="panel-header with-border">
                          <h3 class="panel-title"></h3>
                          
                        </div><!-- /.box-header -->
                        <div class="panel-body">
                          <div class="row">
                            
                            
                            <asp:HiddenField ID="hdnMRegId" runat="server" />
                             
                              <div class="form-group col-xs-4 col-md-5">
                                <label class="col-sm-4 text-right">District</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlDistrict" class="form-control" AutoPostBack="true"  
                                      runat="server" onselectedindexchanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                      </div>
                              </div>

                              <div class="form-group col-xs-4 col-md-5">
                                 <label class="col-sm-4 text-right">MPWLC Branch</label>
                                 <div class="col-sm-8">
                               <asp:DropDownList ID="ddlBranch" class="form-control"
                                      runat="server" ></asp:DropDownList>
                                      </div>
                              </div>
                                
                            <div class="form-group col-xs-4 col-md-2">
                                <div class="col-sm-12">
                                <asp:LinkButton ID="lnkbtnSearch" class="btn btn-info btn-block" runat="server" 
                                    onclick="lnkbtnSearch_Click">Search</asp:LinkButton>
                                </div>
                            </div>

                           
                          </div><!-- /.row -->



                        </div><!-- /.box-body -->
                       
                      </div>

                </div>
             </div>

             <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvGodownCap" EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red" class="table table-bordered" AutoGenerateColumns="false" runat="server">
                            
                            <Columns>
                            
                                <asp:TemplateField HeaderText="" ItemStyle-Width="5%">
                                    <ItemTemplate >
                                        <asp:CheckBox ID="chkGodown" runat="server" />
                                        <asp:HiddenField ID="hdnGodownId" Value='<%#Eval("Godown_ID") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SN" ItemStyle-Width="5%" >
                                    <ItemTemplate ><%#Container.DataItemIndex + 1 %></ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Cap Name" DataField="Godown_Name" /> 
                               
                                <asp:TemplateField HeaderText="Total Cap (MT)">
                                    <ItemTemplate>
                                       <asp:Literal ID="litGodwonCap" Text='<%#Eval("Godown_Capacity", "{0:#.0}") %>' runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Utilized Cap (MT)">
                                    <ItemTemplate>
                                       <asp:Literal ID="litBookedCap" Text='<%#Eval("Utilized_capacity") %>' runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Vacanct Cap (MT)">
                                    <ItemTemplate>
                                       <asp:Literal ID="litAvailCap" Text='<%#Eval("Vacant_capacity", "{0:#.0}")%>' runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Full/Partial">
                                   <ItemTemplate>
                                       <asp:RadioButtonList ID="radListFullPartial" AutoPostBack="true" OnSelectedIndexChanged="radListFullPartial_OnSelectedIndexChanged" runat="server">
                                            <asp:ListItem Text="Full" Value="F"></asp:ListItem>
                                            <asp:ListItem Text="Partial" Value="P"></asp:ListItem>
                                       </asp:RadioButtonList>
                                   </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reserve Cap (MT)" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbookingCap" autocomplete="off" Text="0" CssClass="allow-numeric" AutoPostBack="true" OnTextChanged="txtbookingCap_OnTextChanged" runat="server"></asp:TextBox>
                                    
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount" >
                                    <ItemTemplate>
                                       <%-- <asp:TextBox ID="txtbookingAmount" Text="0" ReadOnly="true" runat="server"></asp:TextBox>--%>

                                        <asp:Label ID="lblbookingAmount" Text="0" ReadOnly="true" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                
                            
                            </Columns>

                            
                        </asp:GridView>
                    </div>
                </div>
            </div>

            
            <div class="row" id="divCalculate" visible="false" runat="server">
                <div class="col-md-2 center-block text-center">
                <br />
                    <asp:button ID="btnCalculate" class="btn btn-default" Text="Calculate" 
                        runat="server" onclick="btnCalculate_Click" />

                        <h5 class="text-left"><b>Total Capacity</b></h5> <asp:TextBox ID="txtTotalBookCap" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                    <h5 class="text-left"><b> Total Amount</b></h4> <asp:TextBox ID="txtTotalAmount" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>

                    <br />
                    <asp:button ID="btnSubmit" class="btn btn-info " Visible="false" Text="Submit" runat="server" 
                        onclick="btnSubmit_Click" />
                </div>

                <div class="col-md-6 text-left">
                    

                </div>

                </div>

                 <br /><h4 class="panel-title text-center red">  अमानती राशि-प्रत्येक राईस मिलर को निगम की स्वनिर्मित कैप क्षमता ऑनलाइन आवेदन करने के दौरान राशि रुपए १०/- प्रति मे.टन अमानती राशि के रूप में जमा करनी होगी |  </h4><br />
            </div>
        </div>


     

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">

   <script type="text/javascript">

       $(document).ready(function () {
           $(".allow-numeric").bind("keypress", function (e) {
               var keyCode = e.which ? e.which : e.keyCode

               if (!(keyCode >= 48 && keyCode <= 57)) {
                   // $(".error").css("display", "inline");
                   return false;
               } else {
                   //  $(".error").css("display", "none");
               }
           });
       });
     
</script>
</asp:Content>

