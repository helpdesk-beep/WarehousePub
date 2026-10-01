<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_BM_Generated_Bill_For_DMO_Markfed.aspx.cs" Inherits="Accounting_Print_BM_Generated_Bill_For_DMO_Markfed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/css/Table-MultiDesigns.css" rel="stylesheet" type="text/css" />
<script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>" type="text/javascript"></script>
<link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/Resources/js/Alerts/sweetalert.css" rel="stylesheet" type="text/css" />
<script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/Resources/js/Alerts/sweetalert-dev.js"></script>
<style type="text/css">
    * {
        box-sizing: border-box;
        font-family: Cambria, serif;
    }

    body {
        border: 3px solid black;
        padding: 8px;
        min-width: 900px;
        max-width: 1000px;
        margin: 0 auto;
        background: #fff;
        text-transform: capitalize;
    }

    /* ❌ dashed border removed (double border issue fix) */
    /*form {
        margin: 0;
        padding: 0;
        border: none;
    }*/
    form {
        min-height: auto;
    }


    /* ❌ opacity removed (text clear ho jayega) */
    table {
        width: 100%;
        border-collapse: collapse;
        opacity: 1;
    }

    td, th {
        padding: 2px 4px;
        vertical-align: middle;
        font-size: 13px;
    }

    /* ===== Header section ===== */
    #MainContainer p {
        margin: 6px 0;
        line-height: 22px;
    }

    #MainContainer strong {
        font-weight: bold;
    }

    /* ===== Warehouse / Bill info table ===== */
    /*table[border="1"] td {
        line-height: 20px;
    }*/

    /* ===== GridView alignment ===== */
    .EU_DataTable th {
        background-color: #f2f2f2;
        font-weight: bold;
        font-size: 13px;
        text-align: center;
    }

    .EU_DataTable td {
        font-size: 12px;
        text-align: center;
    }

    .EU_DataTable {
        margin-top: 5px;
    }

    /* ===== Amount summary (GST / Net Amount) ===== */
    #trSilo td {
        padding-top: 10px;
        padding-bottom: 10px;
        font-weight: bold;
        font-size: 12px;
    }

    /* ===== Signature block ===== */
    .fountcolor td {
        padding-top: 18px;
        font-size: 12px;
        text-align: right;
    }

    /* ===== Print view ===== */
    /*  @media print {
        a {
            display: none;
        }

        body {
            border: 2px solid black;
        }
    }*/
</style>

<style type="text/css">
    /* ===== A4 PRINT PERFECT ===== */
    @page {
        size: A4;
        margin: 8mm;
    }

    @media print {
        body {
            width: 210mm;
            min-height: 297mm;
            margin: auto;
            border: 2px solid #000;
        }

        a {
            display: none !important;
        }
    }

    /* ===== WATERMARK ===== */
    #MainContainer {
        position: relative;
    }

        #MainContainer::before {
            content: "MPWLC - DMO MARKFED";
            position: absolute;
            top: 40%;
            left: 50%;
            transform: translate(-50%, -50%) rotate(-30deg);
            font-size: 60px;
            color: rgba(0, 0, 0, 0.07);
            font-weight: bold;
            z-index: 0;
            white-space: nowrap;
            pointer-events: none;
        }

        /* content watermark ke upar rahe */
        #MainContainer > * {
            position: relative;
            z-index: 1;
        }

    /* ===== Footer totals thoda clear ===== */
    #trSilo {
        border-top: 1px solid #000;
    }

        #trSilo td {
            text-align: right;
            padding-right: 10px;
        }
</style>
<head runat="server">
    <title>Print DMO-Markfed Storage Bill</title>
</head>
<body>
    <form id="form1" runat="server">
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
                            DMO-MARKFED STORAGE CHARGES
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
                                    HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center"
                                    RowStyle-Height="18px" HeaderStyle-Height="20px">
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
                    </div>
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
