<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillStatusTracking.aspx.cs" Inherits="StatePages_BillStatusTracking" %>

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
                                                <asp:Label ID="lblBillNo" runat="server" Text="Bill Status Tracking " Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 50px;" colspan="8" align="center">
                                                 <asp:Label ID="lblAgencyType" runat="server" Font-Bold="true" Text="Agency Type:"></asp:Label>
                                                &nbsp;&nbsp;&nbsp; 
                                                <asp:DropDownList ID="ddlAgencyType" runat="server" AutoPostBack="true" Height="25px" OnSelectedIndexChanged="ddlAgencyType_SelectedIndexChanged" Width="150px">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblBillNumber" runat="server" Font-Bold="true" Text="Bill Number: "></asp:Label>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:TextBox runat="server" ID="txtBillNumber"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblBillType" runat="server" Font-Bold="true" Text="Bill Type:"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlBillType" runat="server" AutoPostBack="true" Height="25px" OnSelectedIndexChanged="ddlBillType_SelectedIndexChanged" Width="150px">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="AD">Storage Charges</asp:ListItem>
                                                    <asp:ListItem Value="FBN">Final Bill Number</asp:ListItem>
                                                    <asp:ListItem Value="GR">Godown Rent</asp:ListItem>
                                                    <asp:ListItem Value="HG">HG</asp:ListItem>
                                                    <asp:ListItem Value="FD">NAFED SC Bill</asp:ListItem>
                                                    <asp:ListItem Value="PM">PMS</asp:ListItem>
                                                    <asp:ListItem Value="SS">Steel Silo</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button runat="server" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" Height="25px" Width="168px" Font-Bold="true"  />
                                                    
                                            </td>

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
                                                        <asp:TemplateField HeaderText="Bill Number">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBillNo" Width="100%" Text='<%# Eval("BillNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Final Bill Number">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblFinBillNo" Width="100%" Text='<%# Eval("FinalBillNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDistrict_Name" Width="100%" Text='<%# Eval("DistrictName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Branch Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDepotName" Width="100%" Text='<%# Eval("BranchName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Name/ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownNameID" Width="100%" Text='<%# Eval("GodownName-ID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crop Year">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCropYear" Width="100%" Text='<%# Eval("CropYear")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Month">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBillMonth" Width="100%" Text='<%# Eval("BillMonth")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Creation Date">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBillCreatedDate" Width="100%" Text='<%# Eval("CreatedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBillType" Width="100%" Text='<%# Eval("BillType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>                                                        
    
                                                        <asp:TemplateField HeaderText="Commodity Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCommodityName" Width="100%" Text='<%# Eval("CommodityName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%-- <ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BO Approval Status">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBOApprovalStatus" Width="100%" Text='<%# Eval("BOApprovalStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RO Approval Status">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblROApprovalStatus" Width="100%" Text='<%# Eval("ROApprovalStatus")%>'></asp:Label>
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
                                                <asp:Label ID="lblBillTxndtls" runat="server" Text="Bill Details" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>


                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="GV_BillAmountDetails" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" Font-Size="15pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Number">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBillNo" Width="100%" Text='<%# Eval("BillNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBillAmount" Width="100%" Text='<%# Eval("BillAmount")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            
                                                        </asp:TemplateField>
                                                         
                                                        <asp:TemplateField HeaderText="Financial Year">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Vacant_Capacity" Width="100%" Text='<%# Eval("FinancialYear")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crop Year">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCropYear" Width="100%" Text='<%# Eval("CropYear")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        

                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White"/>
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

        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin-top: 50px">
                                        <tr style="background-color: #5da7a7; height: 25px; margin-top: 50px;">
                                            <td colspan="8" align="center ">
                                                <asp:Label ID="lblBillDSCDtls" runat="server" Text="Bill DSC Details" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        

                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="GV_BillDSCDetails" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" Font-Size="15pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bill Number">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBillNo" Width="100%" Text='<%# Eval("BillNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown DSC Holder Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGMName" Width="100%" Text='<%# Eval("GodownDSCHolderName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Godown DSC Approval Date">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGMDSCApprovalDate" Width="100%" Text='<%# Eval("GodownDSCApprovalDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Branch DSC Holder Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBMName" Width="100%" Text='<%# Eval("BranchDSCHolderName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Branch DSC Approval Date">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBMDSCApprovalDate" Width="100%" Text='<%# Eval("BranchDSCApprovalDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="RM DSC Holder Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblRMName" Width="100%" Text='<%# Eval("RMDSCHolderName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RM DSC Approval Date">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblRMDSCApprovalDate" Width="100%" Text='<%# Eval("RMDSCApprovalDate")%>'></asp:Label>
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
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4">
                            <br />
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td align="center">
                                                <asp:Label ID="lblBillKatotraTxndtls" runat="server" Text="Bill Katotra Details" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px"></td>
                                        </tr>


                                        <tr>
                                            <td valign="top" align="center">
                                                <asp:GridView ID="GV_BillKatotraDetails" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" Font-Size="15pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Number">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBillNos" Width="100%" Text='<%# Eval("BillNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownName" Width="100%" Text='<%# Eval("GodownName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownID" Width="100%" Text='<%# Eval("GodownID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Branch ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBranchID" Width="100%" Text='<%# Eval("BranchID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reference Bill Number">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblRefBillNo" Width="100%" Text='<%# Eval("ReferenceBillNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Number">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBillNo" Width="100%" Text='<%# Eval("BillNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBillAmount0" Width="100%" Text='<%# Eval("BillAmount")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Deduction Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDeductionAmt" Width="100%" Text='<%# Eval("DeductionAmount")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                   

                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White"/>
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="12pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                    </tr>
                </table>

            </div>
        </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
