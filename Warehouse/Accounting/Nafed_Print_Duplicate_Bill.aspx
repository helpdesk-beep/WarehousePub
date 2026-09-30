<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nafed_Print_Duplicate_Bill.aspx.cs" Inherits="Nafed_Print_Duplicate_Bill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Nafed Storage Bill (Original & Duplicate)</title>
    <link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/css/Table-MultiDesigns.css" rel="stylesheet" type="text/css" />
    <script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/jQuery%20Package/jquery-1.10.2/jquery-1.10.2.js" type="text/javascript"></script>
    <link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/Resources/js/Alerts/sweetalert.css" rel="stylesheet" type="text/css" />
    <script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/Resources/js/Alerts/sweetalert-dev.js"></script>

    <style type="text/css">
        table {
            opacity: 0.95;
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
            font-family: 'Segoe UI', Arial, sans-serif;
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
    <style type="text/css" media="print">
        @page {
            size: auto;
            margin: 10mm;
        }
        form {
            border: none !important;
        }
        body {
            border: none !important;
        }
    </style>
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
                    <td style="text-align: left;"><a href="Nafed_Print_Storage_Bill_State.aspx">Back</a></td>
                    <td style="text-align: right;"><a href="javascript:void(0);" onclick="javascript:window.print();">Print</a></td>
                </tr>
            </table>

            <div style="width: 100%; background-repeat: no-repeat; background-position: center;">
                <div style="position: relative; border-bottom: 3px solid black;">
                    <p style="text-align: center;">
                        <strong>
                            M.P. Warehousing & Logistics Corporation - Bhopal<br style="margin-top: 10px" />
                            STORAGE BILL (ORIGINAL & DUPLICATE)
                        </strong>
                        <br />
                        <span style="margin-left: 5px; padding: 5px;" class="ui-widget">GST :</span>
                        <strong><asp:Label ID="Label1" runat="server" Text="23AADCM7742B3ZS"></asp:Label></strong>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <span style="margin-left: 5px; padding: 5px;" class="ui-widget">PAN :</span>
                        <strong><asp:Label ID="Label2" runat="server" Text="AADCM7742B"></asp:Label></strong>
                        <br />
                        <span style="margin-left: 5px; margin-top: 10px; padding: 5px;" class="ui-widget">Region Name :</span>
                        <strong><asp:Label ID="lblRegion" runat="server" Text="......"></asp:Label></strong>
                        &nbsp;&nbsp;
                        <span style="margin-left: 5px; margin-top: 10px; padding: 5px;" class="ui-widget">District Name :&nbsp;</span>
                        <strong><asp:Label ID="lbldist" runat="server" Text="......"></asp:Label></strong>
                        &nbsp;&nbsp;
                        <span style="margin-left: 5px; margin-top: 10px; padding: 5px;" class="ui-widget">Branch Name :</span>
                        <strong><asp:Label ID="lblBranch" runat="server" Text="......"></asp:Label></strong>
                    </p>
                </div>
            </div>

            <table border="1" width="100%" style="border-collapse: collapse; margin-top: 5px;">
                <tr>
                    <td align="center" colspan="4">Warehouse Name: &nbsp;&nbsp;
                        <strong>
                            <asp:Label ID="lblAcGdwnName" runat="server" Text="......"></asp:Label>
                            (<asp:Label ID="lblgodownid" runat="server" Text="......"></asp:Label>)
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        Month:- <strong>&nbsp;&nbsp;<asp:Label ID="lbldatefromto" runat="server" Text="......"></asp:Label></strong><br />
                        Billing Date :- <strong>&nbsp;&nbsp;<asp:Label ID="lblbillingdate" runat="server" Text="......"></asp:Label></strong><br />
                        Bill Number :- <strong>&nbsp;&nbsp;<asp:Label ID="lblbillno_Actual" runat="server" Text="......"></asp:Label></strong><br />
                    </td>
                    <td align="left" colspan="2">
                        Depositor Name :- <strong>&nbsp;&nbsp;<asp:Label ID="lbldepositor" runat="server" Text="NAFED-BHOPAL"></asp:Label></strong><br />
                        Commodity :- <strong>&nbsp;&nbsp;<asp:Label ID="lblcmd_ac" runat="server" Text="......"></asp:Label></strong><br />
                        Charges :- <strong>&nbsp;&nbsp;<asp:Label ID="lblrate" runat="server" Text="......"></asp:Label></strong><br />
                    </td>
                </tr>
            </table>

            <div style="border-bottom: 1px solid black; position: relative;">
                <div id="dvgrid" style="width: 100%; margin-top: 1%;">
                    <asp:Label ID="lblmsg" runat="server" BackColor="yellow" ForeColor="red" Style="font-weight: 700; font-size: large; font-family: calibri"></asp:Label>
                    <div>
                        <asp:GridView runat="server" ID="GD2" EnableTheming="False" CssClass="EU_DataTable"
                            Width="100%" AutoGenerateColumns="False" BorderStyle="Solid" RowStyle-BorderWidth="1px"
                            BorderWidth="1px" ShowFooter="True" AllowSorting="True" AllowPaging="false"
                            HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                            <FooterStyle HorizontalAlign="Center" BackColor="#DAEDFA" Font-Bold="true" />
                            <HeaderStyle HorizontalAlign="Center" BackColor="#F0F0F0" Font-Bold="true" />
                            <PagerStyle HorizontalAlign="Center" />
                            <RowStyle HorizontalAlign="Center"></RowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="S. No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnTotal_Charges" runat="server" Value='<%# Eval("Total_Charges") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Bill_Type" HeaderText="Type" ItemStyle-Font-Bold="true" />
                                <asp:BoundField DataField="Bill_Number" HeaderText="Bill No" />
                                <asp:BoundField DataField="Commodity" HeaderText="Commodity" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Bill_Month" HeaderText="Bill Month" />
                                <asp:BoundField DataField="Dates_Period" HeaderText="Dates Period" />
                                <asp:BoundField DataField="Opening_Balance" HeaderText="Opening Balance" />
                                <asp:BoundField DataField="Receive_Bags" HeaderText="Receive Bags" />
                                <asp:BoundField DataField="Issue_Bags" HeaderText="Issue Bags" />
                                <asp:BoundField DataField="Closing_Balance" HeaderText="Closing Balance" />
                                <asp:BoundField DataField="Reserve_Bags" HeaderText="Reserve Bags" />
                                <asp:BoundField DataField="Chargable_Bags" HeaderText="Chargable Bags" />
                                <asp:BoundField DataField="Total_Charges" HeaderText="Total Charges" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="LblMessage" runat="server"></asp:Label>
                    <br />
                    
                    <div style="width: 100%; background-repeat: no-repeat; background-position: center;">
                        <div style="position: relative; border-bottom: 2px solid black; padding-bottom: 5px;">
                            <p style="text-align: start; margin: 0;">
                                <strong style="color: red">* Rupees :-
                                    <asp:Label ID="lblam" runat="server">...</asp:Label>
                                </strong>
                                <br />
                                <strong style="color: red">* THE ABOVE STORED STOCK ARE KEPT IN GOOD CONDITION WITH PROPER FUMIGATION & SCIENTIFIC STORAGE BY MPWLC !!!
                                </strong>
                            </p>
                        </div>
                    </div>
                    <br />

                    <table border="0" width="100%">
                        <tr>
                            <td align="right" colspan="2" style="width: 100%;">
                                <table style="width: 100%;">
                                    <tr class="fountcolor">
                                        <td align="center" style="width: 50%;">
                                            <asp:Image ID="Image2" runat="server" Height="30px" Visible="false" ImageUrl="~/images/dsc1.png" Width="80px" />
                                        </td>
                                        <td align="center" style="width: 50%;">
                                            <asp:Image ID="Image1" runat="server" Height="30px" Visible="false" ImageUrl="~/images/dsc1.png" Width="80px" />
                                        </td>
                                    </tr>
                                    <tr class="fountcolor">
                                        <td align="center">
                                            <asp:Label ID="lblICSerialNo" runat="server" Text="" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblBSerialNo" runat="server" Text="" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="fountcolor">
                                        <td align="center">
                                            <asp:Label ID="lblICHoldername" runat="server" Text="" Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblBHolderName" runat="server" Text="" Font-Bold="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="fountcolor">
                                        <td align="center">
                                            <asp:Label ID="lblICCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblBCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="fountcolor">
                                        <td align="center">
                                            <asp:Label ID="lblICIp" runat="server" Text="" Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblBIP" runat="server" Text="" Font-Bold="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="fountcolor">
                                        <td align="center" style="padding-top: 10px;">
                                            <asp:Label ID="Label51" runat="server" Text="Signature Of Regional Manager" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="center" style="padding-top: 10px;">
                                            <asp:Label ID="Label50" runat="server" Text="Signature Of Branch Manager" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:HiddenField ID="hdnQR" runat="server" />
                <asp:HiddenField ID="hdnAppid" runat="server" Value="0" />
            </div>

            <div style="width: 100%; background-repeat: no-repeat; background-position: center; margin-top: 10px;">
                <div style="position: relative;">
                    <p style="text-align: center;">
                        <strong style="color: red">
                            * This bill is digitally signed, therefore, it does not require any stamp & sign !!!
                        </strong>
                    </p>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            window.location.hash = "no-back-button";
            window.location.hash = "Again-No-back-button";
            window.onhashchange = function () { window.location.hash = "no-back-button"; }
            function noBack() {
                window.history.forward();
            }
            noBack();
            window.onload = noBack;
            window.onpageshow = function (evt) { if (evt.persisted) noBack(); }
            window.onunload = function () { void (0); }

            $(document).ready(function () {
                $(this).bind("contextmenu", function (e) {
                    e.preventDefault();
                });
                $('input,body').bind("cut copy paste drop contextmenu", function (e) {
                    e.preventDefault();
                });
            });
        </script>
    </form>
</body>
</html>