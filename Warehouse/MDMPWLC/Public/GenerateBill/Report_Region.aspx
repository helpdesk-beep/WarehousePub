<%@ Page Language="C#" MasterPageFile="~/MDMPWLC/MasterPages/MasterPage2.master"
    AutoEventWireup="true" CodeFile="Report_Region.aspx.cs" Inherits="MSMPWLC_Report_Region"
    Title="Report Pages States" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="row" style="margin-bottom: 10px;">
        <div class="col-md-12 mg-t-10">
            <div id="div20" runat="server" visible="true" style="padding-top: 1%;">
                <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green; background-color:white;"
                    border="1px">

                    <tr style="background-color: #242849; height: 25px">
                        <td colspan="2" align="center">
                            <asp:Label ID="Label14" runat="server" Text="Storage Charges Details" ForeColor="WhiteSmoke" Font-Bold="True" Font-Size="12pt"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 10px" align="center">
                            <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                        <td>
                            <asp:LinkButton ID="LinkButton202" runat="server" ValidationGroup="lik2"
                                ForeColor="navy" Font-Bold="true"
                                Font-Size="10pt" OnClick="LinkButton202_Click">Billing Pendency report at Various Level(From August)</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px" align="center">
                            <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                        <td>
                            <asp:LinkButton ID="LinkButton208" runat="server" ValidationGroup="lik2"
                                ForeColor="navy" Font-Bold="true"
                                Font-Size="10pt" OnClick="LinkButton208_Click">Received Payment from MPSCSC(From August) </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px" align="center">
                            <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                        <td>
                            <asp:LinkButton ID="LinkButton207" runat="server" ValidationGroup="lik2"
                                ForeColor="navy" Font-Bold="true"
                                Font-Size="10pt" OnClick="LinkButton207_Click">Region wise Summary Report from August</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px" align="center">
                            <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                        <td>
                            <asp:LinkButton ID="LinkButton209" runat="server" ValidationGroup="lik2"
                                ForeColor="navy" Font-Bold="true"
                                Font-Size="10pt" OnClick="LinkButton209_Click">Storage Bill After August 2020 Search Godown Wise</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px" align="center">
                            <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                        <td>
                            <asp:LinkButton ID="LinkButton210" runat="server" ValidationGroup="lik2"
                                ForeColor="navy" Font-Bold="true"
                                Font-Size="10pt" OnClick="LinkButton210_Click">Search Storage Bill After August 2020 Status</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px" align="center">
                            <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                        <td>
                            <asp:LinkButton ID="LinkButton211" runat="server" ValidationGroup="lik2"
                                ForeColor="navy" Font-Bold="true"
                                Font-Size="10pt" OnClick="LinkButton211_Click">Pending Storage Bills Report for Payment</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px" align="center">
                            <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                        <td>
                            <asp:LinkButton ID="LinkButton213" runat="server" ValidationGroup="lik2"
                                ForeColor="navy" Font-Bold="true"
                                Font-Size="10pt" OnClick="LinkButton213_Click">Received Payment from MPSCSC Region/District/Branch Wise(After August)</asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 10px" align="center">
                            <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                        <td>
                            <asp:LinkButton ID="LinkButton212" runat="server" ValidationGroup="lik2"
                                ForeColor="navy" Font-Bold="true"
                                Font-Size="10pt" OnClick="LinkButton212_Click">Payment Credit From MPWLC To Godown NEW</asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 10px" align="center">
                            <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                        <td>
                            <asp:LinkButton ID="LinkButton214" runat="server" ValidationGroup="lik2"
                                ForeColor="navy" Font-Bold="true"
                                Font-Size="10pt" OnClick="LinkButton214_Click">Payment August To December</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px" align="center">
                            <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                        <td>
                            <asp:LinkButton ID="LinkButton215" runat="server" ValidationGroup="lik2"
                                ForeColor="navy" Font-Bold="true"
                                Font-Size="10pt" OnClick="LinkButton215_Click">Payment Jan To March</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px" align="center">
                            <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="lik2"
                                ForeColor="navy" Font-Bold="true"
                                Font-Size="10pt" OnClick="LinkButton1_Click1">Pending Payment From MPSCSC</asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="background-color: #242849; height: 25px">
                        <td colspan="2" align="center">
                            <asp:Label ID="Label1" runat="server" Text="Rabi Procurement 2021-22" ForeColor="WhiteSmoke" Font-Bold="True" Font-Size="12pt"></asp:Label>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton21" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton21_Click"> 12. &nbsp;Wheat Procurement 2021-22 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton22" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton22_Click"> 13. &nbsp;Dalhan Procurement 2021-22 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton23" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton23_Click"> 14. &nbsp;Dalhan e-WHR Submission Procurement 2021-22 </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>


