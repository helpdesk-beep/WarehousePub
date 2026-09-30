<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_BM_Generated_Bill.aspx.cs" Inherits="Accounting_Print_BM_Generated_Bill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/css/Table-MultiDesigns.css" rel="stylesheet" type="text/css" />
<script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/jQuery%20Package/jquery-1.10.2/jquery-1.10.2.js" type="text/javascript"></script>
<link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/Resources/js/Alerts/sweetalert.css" rel="stylesheet" type="text/css" />
<script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/Resources/js/Alerts/sweetalert-dev.js"></script>
<style type="text/css">
    table {
        opacity: 0.65;
    }

    .title {
        margin-bottom: 1%;
        margin-top: 1%;
    }

    form {
        margin-top: 0;
        border: 2px dashed black;
        padding: 5px;
    }

    body {
        border: 4px solid black;
        padding: 5px;
        min-width: 900px;
        max-width: 1000px;
        margin: 0 auto;
        text-transform: capitalize;
    }

    strong span {
        color: black;
        font-family: Cambria;
    }

    #HeaderText strong {
        border: 1px solid black;
        padding: 2px;
        min-width: 150px;
    }
</style>
<head id="Head1" runat="server">
    <title>Print Steel Silo Bill</title>
</head>
<body>
    <form id="form1" runat="server" style="min-height: 800px;">
        <asp:HiddenField runat="server" ID="hdnXMLSigned" Value="" />
        <asp:HiddenField runat="server" ID="hdnXML4Sign" Value="" />
        <asp:HiddenField runat="server" ID="hdnDSCUserName" Value="0" />
        <asp:HiddenField runat="server" ID="hdnrefid" Value="0" />
        <asp:HiddenField runat="server" ID="hdnmessage" Value="0" />
        <asp:HiddenField runat="server" ID="hdnref" Value="0" />
        <asp:HiddenField runat="server" ID="hdnXSGVal" Value="0" />
        <div id="MainContainer" runat="server">
            <table border="0" width="100%">
                <tr>
                    <td style="text-align: left;"><a href="BM_PrintGeneratedBill_DSC.aspx">Back</a></td>
                    <td style="text-align: right;"><a href="javascript:void(0);" onclick="javascript:window.print();">Print</a></td>
                </tr>
            </table>
            <%--<div >
                
            </div>
            <div >
                
            </div>--%>
            <div style="width: 100%; background-repeat: no-repeat; background-position: center;">
                <div style="position: relative; border-bottom: 3px solid black;">
                    <p style="text-align: center;">
                        <strong>
                            <%--<asp:Label ID="pGodown" runat="server" Text="......"></asp:Label>--%>
                            M.P. Warehousing & Logistics Corporation - Bhopal<br />
                            STORAGE CHARGES
                        </strong>
                        <br />
                        <span style="margin-left: 5px; padding: 5px;" class="ui-widget">शाखा का नाम :
                        </span><strong>
                            <asp:Label ID="lblBranch" runat="server" Text="......"></asp:Label></strong>
                        <span style="margin-left: 5px; padding: 5px;" class="ui-widget">जिला का नाम :&nbsp;</span>
                        <strong>
                            <asp:Label ID="lbldist" runat="server" Text="......"></asp:Label></strong><br />
                    </p>
                </div>
                <table border="1" width="100%">
                    <tr>
                        <td align="center" colspan="4">वेयरहाउस का नाम: &nbsp;&nbsp;<strong><asp:Label ID="lblAcGdwnName" runat="server" Text="......"></asp:Label>
                        </strong>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">माह:- <strong>&nbsp;&nbsp;<asp:Label ID="lbldatefromto" runat="server" Text="......"></asp:Label></strong><br />
                            बिल क्रमांक :-  <strong>&nbsp;&nbsp;<asp:Label ID="lblbillno_Actual" runat="server" Text="......"></asp:Label></strong><br />
                            GST :-  <strong>&nbsp;&nbsp;23AADCM7742B3ZS</strong><br />
                            PAN :- <strong>&nbsp;&nbsp;AADCM7742B</strong><br />

                        </td>
                        <td align="left">
                            <br />
                            जमाकर्ता :- <strong>&nbsp;&nbsp;<asp:Label ID="lbldepositor" runat="server" Text="......"></asp:Label></strong>
                            <br />
                            स्कन्द का नाम :- <strong>&nbsp;&nbsp;<asp:Label ID="lblcmd_ac" runat="server" Text="......"></asp:Label></strong>
                            <br />
                            मात्रा :-  <strong>मेट्रिक टन मे </strong>
                            <br />
                            दर  : <strong>&nbsp;&nbsp;<asp:Label ID="lblCropyear" runat="server" Text="......"></asp:Label></strong><br />

                        </td>
                    </tr>
                </table>
                <div style="border-bottom: 1px solid black; position: relative;">
                    <div>
                        <div id="dvgrid" style="width: 100%; margin-top: 1%;">
                            <asp:Label ID="lblmsg" runat="server" BackColor="yellow" ForeColor="red" Style="font-weight: 700; font-size: large; font-family: calibri"></asp:Label>
                            <div style="min-height: 600px;">
                                <asp:GridView runat="server" ID="GD2" EnableTheming="False" CssClass="EU_DataTable"
                                    Width="100%" AutoGenerateColumns="False" BorderStyle="Solid" RowStyle-BorderWidth="1px"
                                    BorderWidth="1px" ShowFooter="True" AllowSorting="True" AllowPaging="false"
                                    HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                                    <FooterStyle HorizontalAlign="Center" BackColor="#DAEDFA" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <PagerStyle HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="क्र.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1%>
                                                <asp:HiddenField ID="hdnTotal_Charges" runat="server" Value='<%# Eval("Total_Charges") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Date" HeaderText="दिनाँक">
                                            <ItemStyle HorizontalAlign="center" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Opening_Weight" HeaderText="प्रारंभिक मात्रा">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Receive_Weight" HeaderText="जमा मात्रा" />
                                        <asp:BoundField DataField="Issue_Weight" HeaderText="भुगतान मात्रा" />
                                        <asp:BoundField DataField="Closing_Weight" HeaderText="शेष मात्रा" />
                                        <asp:BoundField DataField="Per_Day_Rate" HeaderText="शुल्क दर प्रति दिन" />
                                        <asp:BoundField DataField="Total_Charges" HeaderText="राशि (6x7)" />
                                    </Columns>
                                    <FooterStyle HorizontalAlign="Center" BackColor="#DAEDFA" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <PagerStyle HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center"></RowStyle>
                                </asp:GridView>
                                <table width="100%">
                                    <tr id="trSilo" runat="server" visible="false" style="text-align: right;">
                                        <td colspan="2" style="height: 30px;">
                                            <asp:Label ID="lblSp" runat="server" Text="Supervison Charges :" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblSpAmt" runat="server" Text="00.00" Font-Size="12px"></asp:Label>&nbsp;</br>
                                    <asp:Label ID="lblGs" runat="server" Text="GST(18%) Amount :" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblGSTAmt" runat="server" Text="00.00" Font-Size="12px"></asp:Label>&nbsp;</br>
                                    <asp:Label ID="lblBillNet" runat="server" Text="Bill Net Amount :" Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblBillNetAmount" runat="server" Text="00.00" Font-Size="12px"></asp:Label>&nbsp;

                                            

                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Label ID="LblMessage" runat="server"></asp:Label>
                            <br />
                            <br />
                            <table border="0" width="100%">
                                <tr>
                                    <td align="right" colspan="2" style="width: 100%;">

                                        <table style="width: 100%;">

                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Image ID="Image2" runat="server" Height="30px" Visible="false"
                                                        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:Image ID="Image1" runat="server" Height="30px" Visible="false"
                                                        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>

                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblICSerialNo" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBSerialNo" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblICHoldername" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBHolderName" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblICCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblICIp" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBIP" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                            </tr>
                                            <tr class="fountcolor">
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="Label51" runat="server" Text="प्रदाय केन्द्र प्रभारी के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="Label50" runat="server" Text="शाखा प्रबंधक(MPWLC) के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            </tr>

                                        </table>

                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <%--<div style="text-align: center;">
                            <a href="javascript:void(0);" onclick="javascript:window.print();">Print</a>
                        </div>--%>
                    </div>
                    <%-- <script type="text/javascript">        window.print(); </script>--%>
                    <asp:HiddenField ID="hdnQR" runat="server" />
                    <asp:HiddenField ID="hdnAppid" runat="server" Value="0" />
                </div>
            </div>
        </div>
        <script>
            window.location.hash = "no-back-button";
            window.location.hash = "Again-No-back-button"; //again because google chrome don't insert first hash into history
            window.onhashchange = function () { window.location.hash = "no-back-button"; }
            function noBack() {
                window.history.forward()
            }
            noBack();
            window.onload = noBack;
            window.onpageshow = function (evt) { if (evt.persisted) noBack(); }
            window.onunload = function () { void (0); }
            $(document).ready(function () {
                $(function () {
                    $(this).bind("contextmenu", function (e) {
                        Message("", "This facility has been disabled !", "info");
                        e.preventDefault();
                    });
                });
                $('input,body').bind("cut copy paste drop contextmenu", function (e) {
                    Message("", "This facility has been disabled !", "info");
                    e.preventDefault();
                });
            });
        </script>
    </form>
</body>
</html>
