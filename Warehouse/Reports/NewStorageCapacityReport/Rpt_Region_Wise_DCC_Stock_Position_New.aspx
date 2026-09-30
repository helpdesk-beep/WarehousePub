<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Region_Wise_DCC_Stock_Position_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Region_Wise_DCC_Stock_Position_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <fieldset>
            <legend align="center">क्षेत्रीय कार्यालय वार डीसीसी ,कीटग्रस्‍त स्‍कंध ,आटा फारमेशन,फारेन मेटर एवं  खराब बारदाने में भंडारित स्‍कंध की शेष  मात्रा 
            <label style="color: red">(मे. टन में )</label></legend>
            <div class="row" style="text-align: center; font-size: large;">
                <div class="col-md-2"></div>
                <div class="col-md-1">
                    <asp:Label ID="Label2" runat="server" Font-Size="10pt" Font-Bold="true">Commodity :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ddlComodity" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="true">Crop Year :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlcropyear" runat="server" CssClass="form-control" selectionmode="Multiple" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged1">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-success btn-sm show-loader" OnClick="btnshow_Click" />
                </div>
            </div>
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="क्षेत्रीय कार्यालय">
                                <ItemTemplate>
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Inspections/Reports/Rpt_District_Wise_DCC_Stock_Position_New.aspx?Region_ID="+ (Eval("Region_ID").ToString())%>'
                                        title="Region Name" Text=' <%# Eval("Regionnm") %>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="डीसीसी स्टॉक">
                                <ItemTemplate>
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Inspections/Reports/Rpt_Godown_Wise_DCC_Stock_Position.aspx?Region_ID="+ (Eval("Region_ID").ToString())%>'
                                        title="DCC_Stock" Text=' <%# Eval("DCC_Stock") %>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="कीटग्रस्‍त स्‍कंध की मात्रा">
                                <ItemTemplate>
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Inspections/Reports/Rpt_Godown_Wise_Infested_Stock_Position.aspx?Region_ID="+ (Eval("Region_ID").ToString())%>'
                                        title="infestedstock" Text=' <%# Eval("infestedstock") %>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="आटा फारमेशन की मात्रा">
                                <ItemTemplate>
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Inspections/Reports/Rpt_Godown_Wise_doughformation_Stock_Position.aspx?Region_ID="+ (Eval("Region_ID").ToString())%>'
                                        title="doughformation" Text=' <%# Eval("doughformation") %>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="फारेन मेटर युक्‍त स्‍कंध की मात्रा">
                                <ItemTemplate>
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Inspections/Reports/Rpt_Godown_Wise_FM_Stock_Position.aspx?Region_ID="+ (Eval("Region_ID").ToString())%>'
                                        title="foreignmatter" Text=' <%# Eval("foreignmatter") %>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="खराब बारदाने में भंडारित स्‍कंध की मात्रा">
                                <ItemTemplate>
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Inspections/Reports/Rpt_Godown_Wise_badsacks_Stock_Position.aspx?Region_ID="+ (Eval("Region_ID").ToString())%>'
                                        title="badgunnybags" Text=' <%# Eval("badgunnybags") %>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
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
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>

