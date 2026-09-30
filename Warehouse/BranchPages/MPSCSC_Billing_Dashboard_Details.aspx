<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MPSCSC_Billing_Dashboard_Details.aspx.cs" Inherits="BranchPages_MPSCSC_Billing_Dashboard_Details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing_Dashboard</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../sdmenu/sdmenu.css" />
    <link href="../css/style_new.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="../JS/ValdationsClientSide.js"></script>--%>
    <script type="text/javascript" src="../../JS/popcalendar.js"></script>
    <script type="text/javascript" src="../../JS/ShowCalender.js"></script>
    <script type="text/javascript" src="../../JS/allFormValidations.js"></script>
    <script type="text/javascript" src="../JS/allFormValidations.js"></script>

    <script language="javascript" type="text/javascript">

        function PrintPage() {

            var printContent = document.getElementById('<%= GVGeneratedBill.ClientID %>');

        var printWindow = window.open("All Records", "Print Panel", 'left=50000,top=50000,width=0,height=0');

        printWindow.document.write(printContent.innerHTML);

        printWindow.document.close();

        printWindow.focus();

        printWindow.print();

    }

    </script>

</head>

<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr class="inner-top">
                                <td>
                                    <table width="788" border="0" cellspacing="0" cellpadding="0">
                                        <tr class="inner-top">
                                            <td></td>
                                        </tr>
                                        <tr class="inner-top">
                                            <td style="height: 56px"></td>
                                            <td style="width: 349px; height: 56px;" align="center" class="whiteTxtBold">Integrated Information System for Foodgrains Management</td>
                                            <td colspan="2" style="height: 56px">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 16px">&nbsp;</td>
                                                        <td align="center" style="width: 222px">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td style="height: 30x; width: 16px;">
                                                            <img src="../images/welcome-left-cut.jpg" hspace="0" vspace="0" border="0" height="30" style="width: 35px"></td>
                                                        <td align="center" class="welcomeBg" style="height: 30px; width: 222px;">Welcome at Branch,&nbsp;
                    <asp:Label ID="UxUserName" runat="server" Text="User Name" Width="82px" Font-Bold="True" ForeColor="Green"></asp:Label><span class="blackTxt">&nbsp;&nbsp;|&nbsp;&nbsp;</span>
                                                            <%--  <a href="../Default2.aspx?Logout=true" target="_top">Sign Out</a>--%>
                                                            <%--  <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default2.aspx">Sign Out</asp:HyperLink>--%>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Back</asp:LinkButton>
                                                        </td>
                                                        <td style="height: 30px">
                                                            <img src="../images/welcome-right-cut.jpg" hspace="0" vspace="0" border="0" height="30" style="width: 34px"></td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td style="height: 56px"></td>
                                            <td align="center" class="whiteTxtBold" style="height: 56px"></td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>

                <td colspan="3" align="center" style="background-color: #0bb6e6; height: 25px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="#000099"
                        Text="MPSCSC STORAGE BILL DETAIL FOR BRANCH  "></asp:Label>
                    <asp:Label ID="lbl_Brchname" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="#000099"></asp:Label>
                </td>

                <tr>
                    <td colspan="3" align="center" valign="top">
                        <div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" valign="top" style="width: 200px">&nbsp;</td>
                                    <asp:GridView ID="Gv_cmdt" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found">
                                        <Columns>

                                            <asp:BoundField HeaderText="Commodity Id" HeaderStyle-ForeColor="Red" HeaderStyle-BackColor="Wheat" DataField="Commodity_Id" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Commodity Name" HeaderStyle-ForeColor="Red" HeaderStyle-BackColor="Wheat" DataField="Commodity_Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Commodity Rate" HeaderStyle-ForeColor="Red" HeaderStyle-BackColor="Wheat" DataField="Commodity_Rate" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </tr>
                            </table>

                            <br />


                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="center" valign="top" style="width: 200px">
                                        <asp:GridView ID="GVGeneratedBill" runat="server" AutoGenerateColumns="false" ShowFooter="True" EmptyDataText="No Record Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Red" HeaderStyle-BackColor="Wheat">
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Godown Id" HeaderStyle-ForeColor="Red" HeaderStyle-BackColor="Wheat" ItemStyle-Width="100px" DataField="Godown_Id" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Godown Name" HeaderStyle-ForeColor="Red" HeaderStyle-BackColor="Wheat" ItemStyle-Width="300px" DataField="Godown_Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>

                                                <asp:BoundField HeaderText="Crop Year" HeaderStyle-ForeColor="Red" HeaderStyle-BackColor="Wheat" ItemStyle-Width="100px" DataField="Crop_Year" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="MONTH" HeaderStyle-ForeColor="Red" HeaderStyle-BackColor="Wheat" ItemStyle-Width="100px" DataField="MONTH" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Min Weight" HeaderStyle-ForeColor="Red" HeaderStyle-BackColor="Wheat" ItemStyle-Width="100px" DataField="Min_Weight" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                 <asp:BoundField HeaderText="Max Weight" HeaderStyle-ForeColor="Red" HeaderStyle-BackColor="Wheat" ItemStyle-Width="100px" DataField="Max_Weight" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Net Amount" HeaderStyle-ForeColor="Red" HeaderStyle-BackColor="Wheat" ItemStyle-Width="100px" DataField="Net_Amount" ItemStyle-HorizontalAlign="Right">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="#FFFF66" ForeColor="Red" Font-Bold="true" />
                                        </asp:GridView>
                                        <br />
                                    </td>

                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td valign="top">
                        <table>
                            <tr>
                                <td style="width: 182px" rowspan="2">&nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/niclogo.gif" /></td>
                                <td valign="top">
                                    <%-- <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="100%" Width="100%">
                             
                                </asp:Panel>--%>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
         <div style="width:100%; text-align:right">
                    <table>
                        <tr align="right">
                            <td align="right" valign="top" style="width: 1200px">
                                <asp:Button ID="btn_prt" runat="server" Text="Print" OnClientClick="return PrintPage();" Visible="False" />
                                <asp:Button ID="btn_exp_els" runat="server" Text="Export to Excel" OnClick="btn_exp_els_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
    </form>
</body>
</html>
