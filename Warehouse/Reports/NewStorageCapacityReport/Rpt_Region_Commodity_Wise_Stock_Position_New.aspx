<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Region_Commodity_Wise_Stock_Position_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Region_Commodity_Wise_Stock_Position_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <fieldset>
            <legend align="center">District,Commodity,Date Wise Stock Position<label style="color: red">(In M.T.)</label></legend>
            <div class="row" style="text-align: center; font-size: large;">
                <div class="col-md-2" style="margin-top: 5px">
                    <asp:Label ID="lblDistrict" runat="server">Date (DD-MM-YYYY) :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtpaymentdate" AutoComplete="off" CssClass="datepicker form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" >Region :</asp:Label>
                </div>
                <div class="col-md-2">
                    <%--<asp:DropDownList ID="ddlRegion" runat="server" selectionmode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                    <asp:ListItem Value="0">All</asp:ListItem>
                </asp:DropDownList>--%>
                    <asp:ListBox ID="ddlRegion" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-success btn-sm show-loader" OnClick="btnshow_Click" />
                </div>
            </div>
            <div class="row" id="divRegion" runat="server" visible="false" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="grdRegion" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdRegion_RowDataBound"
                        OnRowCreated="grdRegion_RowCreated" PagerStyle-CssClass="table-bordered table-hover pgr">
                        <Columns>

                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepositor" runat="server" Text='<%# Eval("Region") %>'>0</asp:Label>
                                    <%-- <asp:HyperLink ID="lblDepositor" runat="server" Target="_blank" NavigateUrl='<%#"~/Inspections/Reports/Rpt_District_Commodity_Wise_Stock_Position.aspx?District_Id="+ (Eval("District_Id").ToString())%>'
                                    title="District" Text=' <%# Eval("District") %>' ForeColor="Blue"></asp:HyperLink>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("Commodity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CropYear [2016-17]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY1" runat="server" Text='<%# Eval("[2016-17]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2017-18]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY2" runat="server" Text='<%# Eval("[2017-18]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2018-19]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY3" runat="server" Text='<%# Eval("[2018-19]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2019-20]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY4" runat="server" Text='<%# Eval("[2019-20]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2020-21]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY5" runat="server" Text='<%# Eval("[2020-21]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2021-22]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY6" runat="server" Text='<%# Eval("[2021-22]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Crop Year [2022-23]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY7" runat="server" Text='<%# Eval("[2022-23]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2023-24]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY8" runat="server" Text='<%# Eval("[2023-24]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2024-25]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY9" runat="server" Text='<%# Eval("[2024-25]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2025-26]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY10" runat="server" Text='<%# Eval("[2025-26]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                </ItemTemplate>
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
            <div class="row" id="divdistirct" runat="server" visible="false" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="GridView1_OnRowDataBound"
                        OnRowCreated="GridView1_OnRowCreated" PagerStyle-CssClass="pgr">
                        <Columns>

                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblDepositor" runat="server" Text='<%# Eval("District") %>'>0</asp:Label>--%>
                                    <asp:HyperLink ID="lblDepositor" runat="server" Target="_blank" NavigateUrl='<%#"~/Inspections/Reports/Rpt_District_Commodity_Wise_Stock_Position.aspx?District_Id="+ (Eval("District_Id").ToString())%>'
                                        title="District" Text=' <%# Eval("District") %>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("Commodity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CropYear [2016-17]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY1" runat="server" Text='<%# Eval("[2016-17]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2017-18]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY2" runat="server" Text='<%# Eval("[2017-18]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2018-19]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY3" runat="server" Text='<%# Eval("[2018-19]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2019-20]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY4" runat="server" Text='<%# Eval("[2019-20]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2020-21]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY5" runat="server" Text='<%# Eval("[2020-21]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2021-22]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY6" runat="server" Text='<%# Eval("[2021-22]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Crop Year [2022-23]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY7" runat="server" Text='<%# Eval("[2022-23]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2023-24]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY8" runat="server" Text='<%# Eval("[2023-24]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2024-25]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY9" runat="server" Text='<%# Eval("[2024-25]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crop Year [2025-26]" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCY10" runat="server" Text='<%# Eval("[2025-26]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                </ItemTemplate>
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
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= grdRegion.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>

