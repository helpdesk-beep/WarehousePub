<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="GetGodownDetails_DistrictBranchWise_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_GetGodownDetails_DistrictBranchWise_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <div style="text-align: center; font-size: large;">
            <h2 class="header">District and Branch wise Godown Details</h2>
        </div>

        <div class="col-md-12">
            <div class="col-sm-1">
                <asp:Label ID="lblDistrict" Font-Bold="true" runat="server" ForeColor="Navy">District:</asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true" CssClass="form-control show-loader-ddl"
                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-1">
                <asp:Label ID="lblBranch" Font-Bold="true" runat="server" ForeColor="Navy">Branch:</asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control show-loader-ddl"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="lblGodownType" Font-Bold="true" runat="server" ForeColor="Navy">Godown Type:</asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:ListBox ID="ddlGodownType" runat="server" SelectionMode="Multiple"
                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
            </div>
            <div class="col-md-1">
                <asp:Button ID="btnSearch" CssClass="btn btn-success btn-sm show-loader" ValidationGroup="A"
                    runat="server" Text="Search" OnClick="btnSearch_Click" />
            </div>
        </div>
         <div class="col-md-12">
     <div class="table-responsive">

         <div style="overflow-x: scroll;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                        BorderColor="#CC9966" BorderStyle="Double" ShowFooter="true"
                        OnRowDataBound="Depositor_Gridview_OnRowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GodownID">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("GodownID")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GodownName">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("GodownName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="25%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HiredType">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHired_Type" Width="100%" Text='<%# Eval("HiredType")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Storage Type">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStorage_Type" Width="100%" Text='<%# Eval("StorageType")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Scientific Capacity In MT">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Width="100%" Text='<%# Eval("ScientificCapacityInMT")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Max Capacity In MT">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblGodown_Capacity" Width="100%" Text='<%# Eval("MaxCapacityInMT")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LicNum">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLicNum" Width="100%" Text='<%# Eval("LicNum")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LicDate">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLicDate" Width="100%" Text='<%# Eval("LicDate")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
                </div>
            </div>
        </div>


    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>

