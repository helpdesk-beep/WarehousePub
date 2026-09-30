<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_Vacant_Godown_Rent_Bill.aspx.cs" Inherits="WarehouseLevel_Rent_Bill_SteelSilo_Print_Vacant_Godown_Rent_Bill" %>

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
                    <td style="text-align: left;"><a href="Field_Godown_Rent_Bill.aspx">Back</a></td>
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
                            <asp:Label ID="pGodown" runat="server" Text="......"></asp:Label></strong>
                        <br />
                        <span style="margin-left: 5px; padding: 5px;" class="ui-widget">Branch :
                        </span><strong>
                            <asp:Label ID="lblBranch" runat="server" Text="......"></asp:Label></strong>
                        <span style="margin-left: 5px; padding: 5px;" class="ui-widget">District:&nbsp;</span>
                        <strong>
                            <asp:Label ID="lbldist" runat="server" Text="......"></asp:Label></strong><br />
                        <strong>
                            <asp:Label ID="lblOffceAddress" runat="server" Text="......"></asp:Label></strong><br />
                        <strong>
                            <asp:Label ID="lblCorpOfficeAddress" runat="server" Text="......"></asp:Label></strong><br />

                    </p>
                </div>
                <table border="1" width="100%">
                    <tr>
                        <td align="center" colspan="4">
                            <strong>CIN No. &nbsp;&nbsp;<asp:Label ID="lblCINNO" runat="server" Text="......"></asp:Label>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <strong>Invoice No. &nbsp;&nbsp;<asp:Label ID="Invoice_No" runat="server" Text="......"></asp:Label><br />
                                NAME OF LOCATION:-  &nbsp;&nbsp;<asp:Label ID="DepotName" runat="server" Text="......"></asp:Label><br />
                                NAME OF AGENCY:-  &nbsp;&nbsp;<asp:Label ID="Godown_Name" runat="server" Text="......"></asp:Label><br />
                                City : &nbsp;&nbsp;<asp:Label ID="District_Name" runat="server" Text="......"></asp:Label><br />
                                State : &nbsp;&nbsp;<asp:Label ID="State" runat="server" Text="......"></asp:Label><br />
                                GST No. : &nbsp;&nbsp;<asp:Label ID="GSTNo" runat="server" Text="......"></asp:Label><br />
                                PAN : &nbsp;&nbsp;<asp:Label ID="PanNo" runat="server" Text="......"></asp:Label><br />
                                <asp:Label ID="Commodity" runat="server" Text="......"></asp:Label>

                            </strong>
                        </td>
                        <td align="left">
                            <br />
                            <strong>Name : M.P. Warehousing & Logistics Corp.
                            <br />
                                Add. : Office Complex, Block - A, Gautam Nagar, 
                            <br />
                                City : Bhopal 
                            <br />
                                State : Madhya Pradesh<br />
                                GST No. : 23AADCM7742BIZU
                            <br />
                                PAN : AADCM7742B
                            <br />
                                Bill No. &nbsp;&nbsp;<asp:Label ID="lblBillNo" runat="server" Text="......"></asp:Label><br />
                                Rate/MT. @<asp:Label ID="Commodity_Rate" runat="server" Text="......"></asp:Label>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td align="left"><strong>PERIOD :- &nbsp;&nbsp;<asp:Label ID="Period" runat="server" Text="......"></asp:Label></strong></td>
                        <td align="center"><strong>TYPE OF BILL :-</strong> ACTUAL</td>
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
                         <%--   <table border="0" width="100%">
                                <tr>
                                    <td style="font-size: 10pt;">Branch Manager(MPWLC) </td>
                                    <td style="font-size: 10pt;">D. M.(MPSCSC)</td>
                                    <td style="font-size: 10pt;">Authorised Signatory</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="font-size: 10pt;">Regional Manager(MPWLC) </td>
                                    <td style="font-size: 10pt;">Regional Manager(MPSCSC)</td>
                                </tr>
                            </table>--%>
                                <table border="0" width="100%">
                                <tr>
                                    <td align="right" colspan="2" style="width: 100%;">

                                        <table style="width: 100%;">

                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Image ID="Image3" runat="server" Height="30px" Visible="false"
                                                        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:Image ID="Image1" runat="server" Height="30px" Visible="false"
                                                        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:Image ID="Image2" runat="server" Height="30px" Visible="false"
                                                        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:Image ID="Image4" runat="server" Height="30px" Visible="false"
                                                        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>

                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblGOMHoldername" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBMMHoldername" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                <td align="right">
                                                    <asp:Label ID="lblBHolderName" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblNanHolderName" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblGOMSerialNo" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBMMSerialNo" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                <td align="right">
                                                    <asp:Label ID="lblBSerialNo" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblNanSerialNo" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblGOMCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBMMCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                <td align="right">
                                                    <asp:Label ID="lblBCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblNanCreatedDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="lblGOMIp" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblBMMIp" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                <td align="right">
                                                    <asp:Label ID="lblBIP" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="lblNanIP" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                            </tr>


                                            <tr class="fountcolor">
                                                <td colspan="3">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr class="fountcolor">
                                                <td align="right">
                                                    <asp:Label ID="Label51" runat="server" Text="Incharge(Steel Silo)" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="Label50" runat="server" Text="Branch Manager(MPWLC)" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

                                                <td align="right">
                                                    <asp:Label ID="Label9" runat="server" Text="Regional Manager(MPWLC)" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td align="right">
                                                    <asp:Label ID="Label1" runat="server" Text="District Mangar(Mpscsc)" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
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
