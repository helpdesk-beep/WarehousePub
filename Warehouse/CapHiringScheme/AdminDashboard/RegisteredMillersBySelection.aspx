<%@ Page Title="" Language="C#" MasterPageFile="~/CapHiringScheme/AdminDashboard/Admin.master"
    AutoEventWireup="true" CodeFile="RegisteredMillersBySelection.aspx.cs" Inherits="CapHiringScheme_AdminDashboard_RegisteredMillers"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <!-- page-wrapper -->
    <div id="page-wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="panel box-primary">
                    <div class="panel-header with-border">
                        <h3 class="panel-title">
                            Registered Millers List</h3>
                        <hr />
                    </div>
                    <!-- /.box-header -->
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group col-xs-3 col-md-3" id="divRegion" runat="server">
                                <label>
                                    Region</label>
                                <asp:DropDownList ID="ddlRegion" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-xs-3 col-md-3" id="divDistrict" runat="server">
                                <label>
                                    District</label>
                                <asp:DropDownList ID="ddlDistrict" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-xs-3 col-md-3">
                                <label>
                                    Branch
                                </label>
                                <asp:DropDownList ID="ddlBranch" CssClass="form-control" runat="server" >
                                    <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-xs-3 col-md-3">
                               <br />
                               <asp:Button ID="btnSearch" CssClass="btn btn-info" Text="Search" runat="server" 
                                    onclick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvRegMillerList" EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red"
                                        AutoGenerateColumns="false" class="table table-bordered" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SN" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Registration ID" DataField="Registration_ID" />
                                            <asp:BoundField HeaderText="Region" DataField="Regionnm" />
                                            <asp:BoundField HeaderText="District" DataField="District_Name" />
                                            <asp:BoundField HeaderText="Branch" DataField="BranchName" />
                                            <asp:BoundField HeaderText="Miller Name" DataField="Miller" />
                                            <asp:BoundField HeaderText="Mill" DataField="Mill_Name" />
                                           
                                            <asp:BoundField HeaderText="Registration Date" DataField="CreateOn" DataFormatString="{0:dd/M/yyyy}" />
                                            <asp:TemplateField HeaderText="" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <a class="btn btn-info btn-sm" href="<%# String.Format("RegisteredMillerDetails.aspx?Mill_Id={0}", Eval("Mill_Id")) %>">
                                                        VIEW</a>
                                                    <asp:HiddenField ID="hdnMillId" Value='<%#Eval("Mill_Id") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Button ID="btnExportExcel" Text="Export To Excel" CssClass="btn btn-success"
                                    Visible="false" runat="server" OnClick="btnExportExcel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>
