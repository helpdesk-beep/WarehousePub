<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Check_Godown_Status_For_All.aspx.cs" Inherits="StatePages_GodownCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <div>
                                <div class="auto-style1">
                                    <a href="State_Welcome_DashBoard.aspx">Go Back</a>
                                </div>

                                <center>

                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Godown Details" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 50px;" colspan="8" align="center">&nbsp;&nbsp;&nbsp;&nbsp<b>Godown Id : </b>&nbsp;&nbsp;<%--<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                                                Height="25px" Width="168px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtGodownID"></asp:TextBox>

                                                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<%--<asp:DropDownList ID="ddlBranch" runat="server"
                                                    Height="25px" Width="168px" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                </asp:DropDownList>--%>

                                                <%--<asp:DropDownList ID="ddl_session" runat="server" Width="150px"  Height="25px" 
                                                onselectedindexchanged="ddl_session_SelectedIndexChanged" AutoPostBack="true" >
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                <asp:ListItem Value="Previous">Previous</asp:ListItem>
                                                <asp:ListItem Value="Rab2023_24">Rabi_2023_24</asp:ListItem>
                                                <asp:ListItem Value="Kharif_2022_23">Kharif_2022_23</asp:ListItem>
                                                </asp:DropDownList>--%>  
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button runat="server" Text="Check" ID="btnCheck" OnClick="btnCheck_Click" Height="25px" Width="168px" Font-Bold="true" />


                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>

                                        </tr>
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="Label1" runat="server" Text="Godown Details in(Qtl.)" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" Font-Size="15pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDistrict_Name" Width="100%" Text='<%# Eval("District_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Branch Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDepotName" Width="100%" Text='<%# Eval("DepotName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("Godown_ID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Registration No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownReg_ID" Width="100%" Text='<%# Eval("GodownReg_No")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Password">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownPwd" Width="100%" Text='<%# Eval("GodownPassword")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Scientific Capacity in (Qtl.)">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Width="100%" Text='<%# Eval("Godown_Scientific_Capacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Maximum Capacity in (Qtl.)">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Capacity" Width="100%" Text='<%# Eval("Godown_Capacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%-- <ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Hired Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHired_Type" Width="100%" Text='<%# Eval("Hired_Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Active">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblIsActive" Width="100%" Text='<%# Eval("IsActive")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%-- <ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CreatedDate">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCreatedDate" Width="100%" Text='<%# Eval("CreatedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="12pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                            </div>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>

                    </tr>
                </table>

            </div>
        </div>
        <br />
        <br />
        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="lblGodownTxndtls" runat="server" Text="Godown Available Stock Position in(MT)" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>


                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="GV_Capacity" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" ShowFooter="true"
                                                    CellSpacing="2" Font-Size="15pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("Godown_ID")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Godown Vacant Capacity in(MT)">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Vacant_Capacity" Width="100%" Text='<%# Eval("Godown_Vacant_Capacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crop Year">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCropYear" Width="100%" Text='<%# Eval("CropYear")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="WHR Quantity in(MT)">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCropYear" Width="100%" Text='<%# Eval("WHR_Quantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="12pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>

                    </tr>
                </table>

            </div>
        </div>

        <br />
        <br />
        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="Label2" runat="server" Text="Godown All Stock Position Deposite,Delivery and Available Stock in(MT)" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>


                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" ShowFooter="true"
                                                    CellSpacing="2" Font-Size="15pt">
                                                    <Columns>
                                                        <%-- <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/StatePages/Rpt_Godown_WHR_Wise_Details.aspx?GodownID="+ (Eval("GodownID").ToString())%>'
                                                                    title="District Name" Text=' <%# Eval("Godown_Name") %>' ForeColor="Blue"></asp:HyperLink>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="GodownID" HeaderText="GodownID" />
                                                        <asp:BoundField DataField="CropYear" HeaderText="CropYear" />
                                                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor_Name" />
                                                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity_Name" />
                                                        <asp:BoundField DataField="Total_Qty_Received" HeaderText="Total_Qty_Received" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField DataField="DeliveryWeight" HeaderText="DeliveryWeight" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField DataField="AvailableQty" HeaderText="AvailableQty" ItemStyle-HorizontalAlign="Right" />

                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="12pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
                                        </tr>


                                    </table>
                                </div>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>

                    </tr>
                </table>

            </div>
        </div>
        <br />

        <div>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td align="center" valign="top">

                        <center>
                            <div>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr style="background-color: #0bb6e6; height: 25px">
                                        <td colspan="8" align="center">
                                            <asp:Label ID="Label3" runat="server" Text="Godown Payment Status in Lakh" Font-Bold="true"
                                                Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px" colspan="8"></td>
                                    </tr>


                                    <tr>
                                        <td colspan="8" valign="top" align="center">
                                            <asp:GridView ID="GrdGodownBill" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowCommand="GrdGodownBill_RowCommand"
                                                Width="100%" BackColor="White"
                                                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Regionnm" HeaderText="Region Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="District_Name" HeaderText="District Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="DepotName" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                                    <asp:BoundField DataField="TotalStorageBill" HeaderText="Total Storage Bill" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Storage Bill Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="TotalSubmittedBill" HeaderText="No of Storage Bill Submitted to MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%" />
                                                    <asp:BoundField DataField="SubmittedBillAmount" HeaderText="Storage Bill Amount Submitted to MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="PendingForSubmission" HeaderText="No of Storage Bill Pending For Submission to MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%" />
                                                    <asp:BoundField DataField="PendingForSubmissionAmount" HeaderText="Storage Bill Amount Pending For Submission to MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />


                                                    <asp:BoundField DataField="TotalBillReceivedPayment" HeaderText="Total Bill Received Payment" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%" />
                                                    <asp:BoundField DataField="Gross_Amount" HeaderText="Gross Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Amt Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="OtherDeduction" HeaderText="Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="Payable_Amount" HeaderText="Amount Credited to MPWLC Account after all Deduction" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="NoofBillPendingatMPSCSC" HeaderText="No of Bill Pending at MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%" />
                                                    <asp:BoundField DataField="NoofBillAmountPendingatMPSCSC" HeaderText="Bill Amount Pending at MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />


                                                    <asp:BoundField DataField="TotalRentBill" HeaderText="Total Rent Bill" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%" />
                                                    <asp:BoundField DataField="RentBillAmt" HeaderText="Rent Bill Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="ReceivedRentBill" HeaderText="No of Rent Bill Against Received Storage Bills" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%" />
                                                    <asp:BoundField DataField="ReceivedRentBillAmount" HeaderText="Rent Bill Amount Against Received Storage Bills Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />


                                                    <%--<asp:BoundField DataField="Total" HeaderText="Rent Bill Deduction From MPWLC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>--%>
                                                    <asp:BoundField DataField="PayBilltoGodownOwner" HeaderText="Pay Bill to Godown Owner" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%" />
                                                    <asp:BoundField DataField="PaytoGodownOwner" HeaderText="Payment Credited to Godown Owner Account After Deduction" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />
                                                    <asp:BoundField DataField="PendingBillatMPWLC" HeaderText="Pending Bills at MPWLC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%" />
                                                    <asp:BoundField DataField="PendingBillAmountatMPWLC" HeaderText="Pending Amount at MPWLC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />

                                                    <asp:TemplateField HeaderText="View Godown Wise Bill Details" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnView" runat="server" Text="Views" CssClass="view-button" CommandName="View" CommandArgument='<%# Eval("Godown_ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="District Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="sdffsft" runat="server" NavigateUrl='<%#"#"%>'
                                            title="District Name" Text=' <%# Eval("District_Name") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>    --%>
                                                    <%-- <asp:BoundField DataField="Region" HeaderText="Region" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="District" HeaderText="District" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Branch" HeaderText="Branch" ItemStyle-HorizontalAlign="Left" />--%>
                                                    <%-- <asp:TemplateField HeaderText="District Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/WarehouseLevel/Rpt_Godown_Bill_Wise_Payment_Status.aspx?GodownID="+ (Eval("Godown_ID").ToString())%>'
                                            title="Godown Name" Text=' <%# Eval("godown_Name") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>
                                                    <%--<asp:BoundField DataField="godown_Name" HeaderText="godown_Name" ItemStyle-HorizontalAlign="Left"/>--%>
                                                    <%--<asp:BoundField DataField="noofgdwn" HeaderText="Total No. of JVS Godown" ItemStyle-HorizontalAlign="Right"/>--%>
                                                    <%-- <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalStorageBill" HeaderText="TotalStorageBill" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoOfSUBBill" HeaderText="Total No. of Submitted Bill" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="SUBBillAmt" HeaderText="Total Submitted Bill Amount" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PendingBillForSubmisionatBranch" HeaderText="Pending Bill For Submision at Branch" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PendingBillAmountForSubmision" HeaderText="Pending Bill Amount For Submision" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoofbillPaymentReceivedFromMPSCSC" HeaderText="Total Received Bill From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoofbillPaymentAmountReceivedFromMPSCSC" HeaderText="Total Received Bill Amount From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PaymentDecuctionbyMPSCSC" HeaderText="Payment Deducted From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalNoofPendingBillatMPSCSC" HeaderText="Total No. of Pending Bill at MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalNoofPendingBillAmountatMPSCSC" HeaderText="Total No. of Pending Bill Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoOfBillPayment" HeaderText="Total No. of Bill Pay to Godown Owner" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="BillAmtPTG" HeaderText="Total No. of Bill Amount Pay to Godown Owner" ItemStyle-HorizontalAlign="Right" />--%>
                                                </Columns>
                                               <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="12pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                            </asp:GridView>
                                        </td>
                                    </tr>


                                </table>
                            </div>
                        </center>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px" colspan="4"></td>

                </tr>
            </table>

        </div>

    </form>
</body>
</html>
