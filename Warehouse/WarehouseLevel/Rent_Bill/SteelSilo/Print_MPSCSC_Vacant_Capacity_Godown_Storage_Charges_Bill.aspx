<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_MPSCSC_Vacant_Capacity_Godown_Storage_Charges_Bill.aspx.cs" Inherits="WarehouseLevel_Rent_Bill_SteelSilo_Print_MPSCSC_Vacant_Capacity_Godown_Storage_Charges_Bill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/css/Table-MultiDesigns.css" rel="stylesheet" type="text/css" />
<script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>" type="text/javascript"></script>
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
                    <td style="text-align: left;"><a href="Field_Godown_Rent_Bill.aspx">Back</a></td>
                    <td style="text-align: right;"><a href="javascript:void(0);" onclick="javascript:window.print();">Print</a></td>
                </tr>
            </table>

            <div style="width: 100%; background-repeat: no-repeat; background-position: center;">
                <div style="position: relative; border-bottom: 3px solid black;">
                    <p style="text-align: center;">
                        <strong>M.P. WAREHOUSING AND LOGISTICS CORPORATION, HEAD OFFICE - BHOPAL</strong>
                        <br />
                        <strong>STOARGE CHARGES BILL OF STEEL SILO</strong>
                        <br />
                        <strong>Add. : Office Complex, Block - A, Gautam Nagar, Bhopal </strong>
                        <br />

                    </p>
                </div>
                <table border="1" width="100%">
                    <tr>
                        <td align="left" colspan="2">
                            <strong>BILL NO./DATE &nbsp;&nbsp;<asp:Label ID="Invoice_No" runat="server" Text="......"></asp:Label><br />
                                NAME OF LOCATION:-  &nbsp;&nbsp;<asp:Label ID="DepotName" runat="server" Text="......"></asp:Label><br />
                                NAME OF AGENCY:-  &nbsp;&nbsp;<asp:Label ID="Godown_Name" runat="server" Text="......"></asp:Label><br />
                                <asp:Label ID="Commodity" runat="server" Text="......"></asp:Label>
                            </strong>
                        </td>
                        <td align="left">
                            <strong>Storage Charges Bill No. &nbsp;&nbsp;<asp:Label ID="lblBillNo" runat="server" Text="......"></asp:Label><br />
                                W.P.I. Rate/MT. @<asp:Label ID="Commodity_Rate" runat="server" Text="......"></asp:Label>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td align="left"><strong>PERIOD :- &nbsp;&nbsp;<asp:Label ID="Period" runat="server" Text="......"></asp:Label></strong></td>
                        <td align="center"><strong>TYPE OF BILL :-</strong> VACANT CAPACITY</td>
                        <td align="right"><strong>Days &nbsp;&nbsp;<asp:Label ID="Days" runat="server" Text="......"></asp:Label></strong></td>
                    </tr>
                </table>
                <div style="border-bottom: 1px solid black; position: relative;">
                    <div>
                        <div id="dvgrid" style="width: 100%; margin-top: 1%;">
                            <asp:Label ID="lblmsg" runat="server" BackColor="yellow" ForeColor="red" Style="font-weight: 700; font-size: large; font-family: calibri"></asp:Label>
                            <table border="0" width="100%">
                                <tr>
                                    <td align="right"><strong>Empty Space</strong>  </td>
                                    <td align="right"><strong>(Qty in MT)</strong></td>
                                </tr>
                            </table>
                            <div style="min-height: 600px;">
                                <asp:GridView runat="server" ID="gvIStorageCharge" EnableTheming="False" CssClass="EU_DataTable"
                                    Width="100%" AutoGenerateColumns="False" BorderStyle="Solid" RowStyle-BorderWidth="1px"
                                    BorderWidth="1px" ShowFooter="True" AllowSorting="True" PageSize="50" AllowPaging="True"
                                    HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                                    <FooterStyle HorizontalAlign="Center" BackColor="#DAEDFA" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <PagerStyle HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.no">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Date" HeaderText="Date">
                                            <ItemStyle HorizontalAlign="center" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Opening_Weight" HeaderText="Opening">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Receive_Weight" HeaderText="Deposit" />
                                        <asp:BoundField DataField="Issue_Weight" HeaderText="Delivery" />
                                        <asp:BoundField DataField="Closing_Weight" HeaderText="Closing Balance" />
                                        <asp:BoundField DataField="AgreementCapacity" HeaderText="Capicity For Fixed chg Calculation as per Concession Agreement" />
                                        <asp:BoundField DataField="Chargeable_Closing_Weight" HeaderText="Vacant Capacity in MT" />
                                        <asp:BoundField DataField="Per_Day_Rate" HeaderText="Rate per Day">
                                            <ItemStyle Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total_Charges" HeaderText="Amount per day">
                                            <ItemStyle Font-Size="Small" VerticalAlign="Middle" HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle HorizontalAlign="Center" BackColor="#DAEDFA" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <PagerStyle HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center"></RowStyle>
                                </asp:GridView>
                            </div>
                            <asp:Label ID="LblMessage" runat="server"></asp:Label>
                            <br />
                            <br />
                            <table border="0" width="100%">
                                <tr>
                                    <td style="font-size: 10pt;">&nbsp;</td>
                                    <td style="font-size: 10pt;">&nbsp;</td>
                                    <td style="font-size: 10pt;">Internal Auditor</td>
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
