<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_AND_Depositer_Wise_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_AND_Depositer_Wise_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="row justify-content-center">
        <div class="col-md-12 ml-2">
            <div class="p-3 mb-4 bg-light shadow-sm rounded">
                <h3 class="text-center text-danger mb-0">
                    <i class="fas fa-warehouse me-2"></i>
                    District,Commodity,Date Wise Stock Position<label style="color: red">(In M.T.)</label>
                </h3>
            </div>

        </div>
        <div class="col-md-12">


            <div class="form-group">
                <div class="col-md-2 fw-bold text-end">
                    <asp:Label ID="lblDistrict" runat="server" Font-Size="10pt" Font-Bold="true">Date (DD-MM-YYYY) :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtpaymentdate" runat="server" CssClass="form-control datepicker" AutoComplete="off"></asp:TextBox>
                </div>

                <div class="col-md-1 fw-bold text-end">
                    <asp:Label ID="Label1" runat="server" Text="Commodity"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ddlComodity" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>
                <div class="col-md-1 fw-bold text-end">
                    <asp:Label ID="Label2" runat="server" Font-Size="10pt" Font-Bold="true">Depositor :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddldepositor" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success btn-sm show-loader" OnClick="btnshow_Click" />
                </div>
            </div>
            <div class="col-md-12 ml-12">
                <div class="form-group">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </div>
            </div>
        </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <%--<script src="../../JS/table2excel.js"></script>--%>
    <%--<script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>--%>
    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);
    </script>
    <%-- <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GridView1]").table2excel({
                filename: "District_Branch_and_Godown_Wise_Stock_Position.xls"
            });
        });

    </script>--%>
</asp:Content>


