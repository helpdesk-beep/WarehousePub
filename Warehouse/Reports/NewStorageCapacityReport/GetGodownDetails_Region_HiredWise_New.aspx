<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="GetGodownDetails_Region_HiredWise_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_GetGodownDetails_Region_HiredWise_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <div style="text-align: center; font-size: large;">
            <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h4 class="header">Report For Review of Region,&nbsp; Hired Type Wise (Capacity in MT) </h4>
        </div>
        <div class="col-md-12">
            <div class="form-group">
                <div class="col-sm-8 col-sm-offset-4">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hfId" Value="0" runat="server" />
                    <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
                </div>
            </div>
            <div>
                <div class="row py-2">
                    <div class="col-sm-1">
                        <asp:Label ID="lblRegion" Font-Bold="true" runat="server" ForeColor="Navy" Visible="false">Region:</asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlRegion" runat="server" selectionmode="Multiple" AutoPostBack="true" Visible="false">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1">
                        <asp:Label ID="lblHiredType" Font-Bold="true" runat="server" ForeColor="Navy">Hired Type:</asp:Label>
                    </div>
                    <div class="col-sm-2">
                        <%--<asp:DropDownList CssClass="form-control select2" ID="ddlGodownType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>--%>
                        <asp:ListBox ID="ddlGodownType" runat="server" SelectionMode="Multiple"
                            CssClass="checkbox-multiselect form-control"></asp:ListBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="Button2" CssClass="btn btn-success btn-sm show-loader" ValidationGroup="A"
                            runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
            <div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
            <div class="col-md-12">
                <div class="table-responsive">

                    <div style="overflow-x: scroll;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                            EnableModelValidation="True"
                            CellPadding="4" rules="all" ForeColor="#333333" ShowFooter="true"
                            OnRowDataBound="GV_FYDepositorWisePayment_OnRowDataBound" OnDataBound="GV_FYDepositorWisePayment_OnDataBound"
                            OnRowCreated="GV_FYDepositorWisePayment_OnRowCreated"
                            CssClass="table table-bordered table-hover">
                            <RowStyle BackColor="White" CssClass="ADFieldsGridText table-responsive" HorizontalAlign="Left" />
                            <Columns>

                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Region">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("RegionName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hired Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHiredType" runat="server" Height="21px" Text='<%# Eval("HiredType") %>'> 
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Number of Godowns">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOFGodown" runat="server" Text='<%# Eval("NumberOfGodown") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Maximum Capacity in MT" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGdnMaxCapacity" runat="server" Text='<%# Eval("MaxCapacityinMT") %>'>0</asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Scientific Capacity in MT" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGdnScientificCapacity" runat="server" Text='<%# Eval("ScientificCapacityinMT") %>'>0</asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="20px" Font-Size="12pt" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                        </asp:GridView>
                    </div>
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

