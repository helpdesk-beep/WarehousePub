<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="~/Reports/NewBillingReports/Rpt_Godown_Wise_Payment_Status_From_MPSCSC_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_Godown_Wise_Payment_Status_From_MPSCSC_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend align="center">Division,District,Branch,Godown Wise Payment Status
                <label style="color: red">(In Lakh)</label></legend>

            <div class="Row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Division Name</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddldivision" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>District Name</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <div class="form-group">
                            <asp:DropDownList CssClass="form-control select2" ID="ddldistrict" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Branch Name</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddlbranch" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="Row" style="margin-top: 10px">

                <div class="col-md-2" style="margin-top: 5px">
                    <label>Godown Type :</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList CssClass="form-control select2" ID="ddlgodowntype" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlgodowntype_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Private Warehouse" Value="Private"></asp:ListItem>
                            <asp:ListItem Text="Owned" Value="Owned"></asp:ListItem>
                            <asp:ListItem Text="Tribal Scheme" Value="Tribal Scheme"></asp:ListItem>
                            <asp:ListItem Text="PVT.PEG" Value="PVT.PEG"></asp:ListItem>
                            <asp:ListItem Text="CAP" Value="CAP-PMS"></asp:ListItem>
                            <asp:ListItem Text="BOT" Value="BOT"></asp:ListItem>
                            <asp:ListItem Text="Steel Silo" Value="Steel Silo"></asp:ListItem>
                            <asp:ListItem Text="Hired" Value="Hired"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>


            </div>
            <div class="Row">
                <div class="col-md-2" style="margin-top: 5px">
                    <label>Financial Year</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlcropyear" runat="server" CssClass="form-control" AutoPostBack="true" selectionmode="Multiple" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-2" style="margin-top: 5px">
                    <label>Commodity</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlcommodity" runat="server" CssClass="form-control" AutoPostBack="true" selectionmode="Multiple" OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-11 text-md-end text-center">
                    <asp:Button ID="btnshow" runat="server" Text="Show Details"
                        CssClass="btn btn-success show-loader px-4"
                        OnClick="btnshow_Click" />
                </div>
            </div>

        </fieldset>
        <fieldset id="divdivision" runat="server" visible="false">
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="grddivision" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader"
                        AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Division" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepotName" runat="server" Text='<%# Eval("DepotName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Godown" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblGodown" runat="server" Text='<%# Eval("Godown") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No. of Storage Bill" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblStorageBillNumber" runat="server" Text='<%# Eval("TotalBill") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Storage Bill Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblStorageAmount" runat="server" Text='<%# Eval("BillAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="No. of Storage Bill Received From HO MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblHOMPSCSCBillPaymentReceived" runat="server" Text='<%# Eval("TotalReceivedBill") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Storage Bill Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblGross_Amount" runat="server" Text='<%# Eval("Gross_Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount Received From MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPayable_Amount" runat="server" Text='<%# Eval("Payable_Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TDS Amtount Deduction from MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTDS_Amt" runat="server" Text='<%# Eval("TDS_Amt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Other Deduction from MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblOtherDeduction" runat="server" Text='<%# Eval("OtherDeduction") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No Of Pending Bills at MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingatMPSCSC" runat="server" Text='<%# Eval("PendinbillatMPSCSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pending Bills Amtount at MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblPendingatMPSCSC" runat="server" Target="_blank" NavigateUrl='<%#"~/Reports/NewBillingReports/Godown_Bill_Wise_Payment_Status_From_MPSCSC_New.aspx?Godown_ID="+ (Eval("Godown_ID").ToString())%>'
                                        title="Pending Bills Amtount at MPSCSC" Text=' <%# Eval("PendinbillAmountatMPSCSC") %>' ForeColor="Blue"></asp:HyperLink>
                                    <%--<asp:Label ID="lblPendingatMPSCSC" runat="server" Text='<%# Eval("PendinbillAmountatMPSCSC") %>'></asp:Label>--%>
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
        var grid = $('#<%= grddivision.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>

