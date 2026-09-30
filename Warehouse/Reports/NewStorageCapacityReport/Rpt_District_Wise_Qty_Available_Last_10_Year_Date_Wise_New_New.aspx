<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_New_New.aspx.cs" Inherits="Reports_NewStorageCapacityReport_Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_New_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">

    <div class="container-fluid">
        <fieldset class="border p-3">
            <legend class="w-auto px-2 text-center">District, Commodity, Date Wise Stock Position 
            <label style="color: red">(In M.T.)</label>
            </legend>

            <!-- 🔹 FILTER ROW -->
            <div class="row align-items-end">

                <!-- Date -->
                <div class="col-md-2">
                    <asp:Label ID="lblDistrict" runat="server" CssClass="font-weight-bold">
                    Date 
                    </asp:Label>
                    <asp:TextBox ID="txtpaymentdate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                </div>

                <!-- Commodity -->
                <div class="col-md-3">
                    <asp:Label ID="Label1" runat="server" CssClass="font-weight-bold">Commodity</asp:Label>
                    <asp:ListBox ID="ddlComodity" SelectionMode="Multiple" runat="server" CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>

                <!-- Depositor -->
                <div class="col-md-3">
                    <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold">Depositor</asp:Label>
                    <asp:ListBox ID="ddlDepositor" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>

                <!-- Button -->
                <div class="col-md-2 text-left">
                    <asp:Button ID="btnshow" runat="server" Text="Show Details"
                        CssClass="btn btn-primary btn-block mt-4"
                        OnClick="btnshow_Click" />
                </div>

            </div>

            <!-- 🔹 GRID -->
            <div class="row mt-4">
                <div class="col-md-12">
                    <div class="table-responsive">

                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                            ShowFooter="true"
                            CssClass="table table-bordered table-hover text-center"
                            AlternatingRowStyle-CssClass="table-secondary">

                            <Columns>

                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="District Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="sdffsft" runat="server" Target="_blank"
                                            NavigateUrl='<%#"~/Inspections/Reports/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_New.aspx?District_Id="+ Eval("District_Id") %>'
                                            Text='<%# Eval("District") %>'
                                            ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="A" HeaderText="[2016-17]" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="B" HeaderText="[2017-18]" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="C" HeaderText="[2018-19]" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="D" HeaderText="[2019-20]" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="E" HeaderText="[2020-21]" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="F" HeaderText="[2021-22]" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="G" HeaderText="[2022-23]" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="H" HeaderText="[2023-24]" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="I" HeaderText="[2024-25]" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />

                            </Columns>

                            <FooterStyle Font-Bold="True" ForeColor="Black" />
                        </asp:GridView>

                    </div>
                </div>
            </div>

        </fieldset>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageScript" runat="Server">
</asp:Content>

