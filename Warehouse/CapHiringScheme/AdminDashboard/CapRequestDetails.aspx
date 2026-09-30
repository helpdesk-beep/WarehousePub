<%@ Page Title="" Language="C#" MasterPageFile="~/CapHiringScheme/AdminDashboard/Admin.master"
    AutoEventWireup="true" CodeFile="CapRequestDetails.aspx.cs" Inherits="CapHiringScheme_AdminDashboard_CapRequestDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <!-- page-wrapper -->
    <div id="page-wrapper">
        <div class="row">
            <div class="col-md-12">
                <asp:HiddenField ID="hdnMillId" runat="server" />
                <asp:Repeater ID="rptPersonalDetails" runat="server">
                    <ItemTemplate>
                        <div class="panel box-primary">
                            <div class="panel-header with-border">
                                <h3 class="panel-title">
                                    Miller Details</h3>
                                <hr />
                            </div>
                            <!-- /.box-header -->
                            <div class="panel-body">
                                <div class="row">
                                    <asp:HiddenField ID="hdnMRegId" runat="server" />
                                    <div class="form-group col-md-4">
                                        <label>
                                            Registration Id :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("Registration_ID")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Miller Name :</label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("Miller")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Mobile No. :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("Mobile")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Email :</label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("Email")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Aadhar No. :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("Aadhar")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Pan No. :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("Pan")%></label>
                                        </span>
                                    </div>
                                </div>
                                <!-- /.row -->
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>



            </div>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-md-12">
                
                <div class="panel box-primary">
                            <div class="panel-header with-border">
                                <h3 class="panel-title">
                                    Cap Request Details</h3>
                                <hr />
                            </div>
                            <!-- /.box-header -->
                            <div class="panel-body">
                             <asp:Repeater ID="rptBookCap" runat="server">
                                <ItemTemplate>
                                <div class="row">
                                    <asp:HiddenField ID="hdnMRegId" runat="server" />
                                    <div class="form-group col-md-4">
                                        <label>
                                            Region :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("Regionnm")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            District Name:
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("District_Name")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Branch Name :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("BranchName")%></label>
                                        </span>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <label>
                                            Total Request Capacity :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("TotalCapacity", "{0:#}")%></label>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Total Amount :</label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("TotalAmount", "{0:#}")%></label> /-
                                        </span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>
                                            Request Date :
                                        </label>
                                        <span class="text-primary">
                                            <label class="text-primary">
                                                <%# Eval("RequestDate", "{0:dd/M/yyyy hh:mm tt}")%></label>
                                        </span>
                                    </div>
                                    
                                </div>
                                <!-- /.row -->
                                </ItemTemplate>
                               </asp:Repeater> 
                                <div class="row">
                                    <asp:GridView ID="gvCapGodownBookDetails" AutoGenerateColumns="false" class="table table-bordered"
                                    runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SN" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1 %>
                                                <asp:HiddenField ID="hdnGodownId" Value='<%# Eval("GodownID") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Cap Name" DataField="Godown_Name" />
                                        <asp:TemplateField HeaderText="Cap Type">
                                            <ItemTemplate>
                                                <%# Eval("BookCapacityType").ToString() == "F" ? "Full" : "Partial"%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="Reserve Cap (MT)" DataField="BookGodownCapacity" DataFormatString="{0:#}" />
                                        <asp:BoundField HeaderText="Amount" DataField="BookGodownAmount" DataFormatString="{0:#}" />
                                    </Columns>
                                </asp:GridView>
                                </div>
                            </div>
                        </div>


                
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>
