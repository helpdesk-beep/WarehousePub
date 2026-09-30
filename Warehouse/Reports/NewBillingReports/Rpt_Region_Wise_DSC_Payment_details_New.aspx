<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="~/Reports/NewBillingReports/Rpt_Region_Wise_DSC_Payment_details_New.aspx.cs" Inherits="SRV_Storage_Reports_NewBillingReport_Rpt_Region_Wise_DSC_Payment_details_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend align="center">Division,District,Branch,Godown,Commodity,Financial Year Wise SC Bill,Rent Bill,DSC ,Submission, Payment Status
                <label style="color: red">(In Cr.)</label></legend>
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

                <div class="col-md-1" style="margin-top: 5px">
                    <label>Commodity</label>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:ListBox ID="ddlcommodity" runat="server" SelectionMode="Multiple"
                            CssClass="checkbox-multiselect form-control"></asp:ListBox>
                    </div>
                </div>
                <div class="col-md-1 pt-2">
                    <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-success btn-sm show-loader" OnClick="btnshow_Click" />
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
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Division" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Storage Bill" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblStorageBillNumber" runat="server" Text='<%# Eval("StorageBillNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Storage Bill Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblStorageAmount" runat="server" Text='<%# Eval("StorageAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DSC BY BM(MPWLC) SC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDSC_BY_BM_SC" runat="server" Text='<%# Eval("DSC_BY_BM_SC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Submit SC Bill to DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblSUBMIT_Bill_To_DMMPSCSC" runat="server" Text='<%# Eval("SUBMIT_Bill_To_DMMPSCSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Submit SC Bill Amount to DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblSUBMIT_BillAmount_To_DMMPSCSC" runat="server" Text='<%# Eval("SUBMIT_BillAmount_To_DMMPSCSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SC Bill DSC by DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDMDSC" runat="server" Text='<%# Eval("DMDSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SC Bill Amount DSC by DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDMDSC_Amount" runat="server" Text='<%# Eval("DMDSC_Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pending Bill DSC at DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingBillatDMForDSC" runat="server" Text='<%# Eval("PendingBillatDMForDSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pending Bill Amount DSC at DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingBillAmountatDMForDSC" runat="server" Text='<%# Eval("PendingBillAmountatDMForDSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SC Bill Received From HO MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblHOMPSCSCBillPaymentReceived" runat="server" Text='<%# Eval("HOMPSCSCBillPaymentReceived") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right">
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
                            <asp:TemplateField HeaderText="Pending Amtount at MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingatMPSCSC" runat="server" Text='<%# Eval("PendingatMPSCSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rent Bill NO" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblRentBillNO" runat="server" Text='<%# Eval("RentBillNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rent Bill Amt" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblRentBillAmt" runat="server" Text='<%# Eval("RentBillAmt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DSC BY Godown Owner on Rent Bill" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDSC_BY_MG_GR" runat="server" Text='<%# Eval("DSC_BY_MG_GR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="DSC BY BM on Rent Bill" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDSC_BY_BM_GR" runat="server" Text='<%# Eval("DSC_BY_BM_GR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MPWLC Paid No. of Bill to Godown Owner" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPayBilltoGodownOwner" runat="server" Text='<%# Eval("PayBilltoGodownOwner") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MPWLC Paid Bill Amount to Godown Owner" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaytoGodownOwner" runat="server" Text='<%# Eval("PaytoGodownOwner") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pending Rent Bill Amount at MPWLC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingatMPWLC" runat="server" Text='<%# Eval("PendingatMPWLC") %>'></asp:Label>
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

        <fieldset id="divregion" runat="server" visible="false">
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover"
                        AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Storage Bill" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblStorageBillNumber" runat="server" Text='<%# Eval("StorageBillNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Storage Bill Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblStorageAmount" runat="server" Text='<%# Eval("StorageAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DSC BY BM(MPWLC) SC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDSC_BY_BM_SC" runat="server" Text='<%# Eval("DSC_BY_BM_SC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Submit SC Bill to DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblSUBMIT_Bill_To_DMMPSCSC" runat="server" Text='<%# Eval("SUBMIT_Bill_To_DMMPSCSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Submit SC Bill Amount to DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblSUBMIT_BillAmount_To_DMMPSCSC" runat="server" Text='<%# Eval("SUBMIT_BillAmount_To_DMMPSCSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SC Bill DSC by DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDMDSC" runat="server" Text='<%# Eval("DMDSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SC Bill Amount DSC by DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDMDSC_Amount" runat="server" Text='<%# Eval("DMDSC_Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pending Bill DSC at DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingBillatDMForDSC" runat="server" Text='<%# Eval("PendingBillatDMForDSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pending Bill Amount DSC at DM MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingBillAmountatDMForDSC" runat="server" Text='<%# Eval("PendingBillAmountatDMForDSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SC Bill Received From HO MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblHOMPSCSCBillPaymentReceived" runat="server" Text='<%# Eval("HOMPSCSCBillPaymentReceived") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right">
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
                            <asp:TemplateField HeaderText="Pending Amtount at MPSCSC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingatMPSCSC" runat="server" Text='<%# Eval("PendingatMPSCSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rent Bill NO" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblRentBillNO" runat="server" Text='<%# Eval("RentBillNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rent Bill Amt" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblRentBillAmt" runat="server" Text='<%# Eval("RentBillAmt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DSC BY Godown Owner on Rent Bill" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDSC_BY_MG_GR" runat="server" Text='<%# Eval("DSC_BY_MG_GR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="DSC BY BM on Rent Bill" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDSC_BY_BM_GR" runat="server" Text='<%# Eval("DSC_BY_BM_GR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MPWLC Paid No. of Bill to Godown Owner" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPayBilltoGodownOwner" runat="server" Text='<%# Eval("PayBilltoGodownOwner") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MPWLC Paid Bill Amount to Godown Owner" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaytoGodownOwner" runat="server" Text='<%# Eval("PaytoGodownOwner") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pending Rent Bill Amount at MPWLC" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingatMPWLC" runat="server" Text='<%# Eval("PendingatMPWLC") %>'></asp:Label>
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
        <fieldset id="divdistrict" runat="server" visible="false">
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader"
                        AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="GridView2_RowDataBound"
                        OnRowCreated="GridView2_RowCreated"
                        PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("Commodity") %>'></asp:Label>
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

        <fieldset id="DivGodown" runat="server" visible="false">
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="grdgdn" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader"
                        AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="grdgdn_RowDataBound"
                        OnRowCreated="grdgdn_RowCreated"
                        PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("Godown") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("Commodity") %>'></asp:Label>
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


    <script type="text/javascript">
        $(document).ready(function () {
            initializeDataTables();
        });

        function initializeDataTables() {
            // सभी GridViews के लिए DataTable इनिशियलाइज़ करें
            var grids = [
            '#<%= grddivision.ClientID %>',
            '#<%= GridView1.ClientID %>',
            '#<%= GridView2.ClientID %>',
            '#<%= grdgdn.ClientID %>'
            ];

            $.each(grids, function (index, gridSelector) {
                var grid = $(gridSelector);

                if (grid.length > 0 && grid.find('tbody tr').length > 0) {
                    // THEAD fix for ASP.NET GridView
                    if (grid.find("thead").length == 0) {
                        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
                    }

                    BindDatatable(grid);
                }
            });
        }

        // PostBack के बाद DataTable को री-इनिशियलाइज़ करने के लिए
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            initializeDataTables();
        });
    </script>



</asp:Content>

