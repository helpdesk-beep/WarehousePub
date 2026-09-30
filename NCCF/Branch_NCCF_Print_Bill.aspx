<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/NCCF/Branch_NCCF_Print_Bill.aspx.cs" Inherits="NCCF_Branch_NCCF_Print_Bill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NCCF Storage Bill</title>

    <link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/css/Table-MultiDesigns.css" rel="stylesheet" type="text/css" />
    <script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/jQuery%20Package/jquery-1.10.2/jquery-1.10.2.js" type="text/javascript"></script>

    <style>
        body {
            font-family: 'Segoe UI', Calibri, sans-serif;
            background: #fff;
            margin: 0;
            padding: 0;
        }

        #printWrapper {
            width: 950px;
            margin: 20px auto;
            padding: 15px;
            border: 2px solid #000;
            border-radius: 6px;
            box-sizing: border-box;
        }

        .top-nav {
            margin-bottom: 15px;
            font-size: 14px;
            font-weight: bold;
        }

            .top-nav a {
                text-decoration: none;
                color: #0066cc;
                font-weight: bold;
            }

        .invoice-header {
            text-align: center;
            border-bottom: 3px solid #000;
            padding-bottom: 15px;
            margin-bottom: 20px;
        }

            .invoice-header img {
                display: inline-block;
                vertical-align: middle;
                height: 60px;
                margin-right: 10px;
            }

            .invoice-header strong {
                font-size: 22px;
                text-transform: uppercase;
            }

            .invoice-header div {
                margin-top: 5px;
                font-size: 14px;
            }

        .info-box {
            width: 95%;
            border: 2px solid #000;
            padding: 10px 15px;
            margin-bottom: 20px;
            background: #fafafa;
            border-radius: 6px;
            font-size: 14px;
        }

            .info-box table {
                width: 100%;
                border-collapse: collapse;
            }

            .info-box td {
                padding: 5px;
                vertical-align: top;
            }

            .info-box strong {
                font-weight: 600;
            }

        .EU_DataTable {
            border: 1px solid #333;
            font-size: 13px;
            width: 100%;
            margin-top: 10px;
            border-collapse: collapse;
            table-layout: fixed;
        }

            .EU_DataTable th, .EU_DataTable td {
                border: 1px solid #333 !important;
                padding: 6px;
                text-align: center;
            }

            .EU_DataTable tr:nth-child(even) td {
                background: #f4faff;
            }

        .amount-box {
            margin-top: 25px;
            padding: 12px;
            border: 2px solid #e40000;
            background: #fff4f4;
            color: #e40000;
            font-weight: bold;
            border-radius: 6px;
            font-size: 15px;
        }

        .signature-box {
            width: 100%;
            margin-top: 30px;
            padding: 12px;
            border: 2px solid #000;
            border-radius: 6px;
            box-sizing: border-box;
        }

            .signature-box table {
                width: 100%;
                table-layout: fixed;
            }

            .signature-box td {
                text-align: center;
                vertical-align: top;
            }

        .signature-title {
            font-weight: bold;
            font-size: 14px;
            margin-top: 10px;
            display: block;
        }

        .notice-box {
            margin-top: 30px;
            padding: 10px;
            text-align: center;
            color: red;
            border-top: 3px solid #000;
            font-weight: bold;
            font-size: 14px;
        }

        /* Print Styles */
        @media print {
            body {
                margin: 0;
                padding: 0;
            }

            #printWrapper {
                width: 950px;
                margin: 0 auto;
                border: 2px solid #000;
                padding: 15px;
                border-radius: 6px;
                box-sizing: border-box;
            }

            .top-nav {
                display: none !important;
            }

            table, th, td {
                border-collapse: collapse !important;
                page-break-inside: avoid;
            }

            img {
                max-width: 100%;
                height: auto;
            }
        }

        @page {
            size: A4 portrait;
            margin: 10mm;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div id="printWrapper">
            <div class="top-nav">
                <a href="Nafed_Print_Storage_Bill_State.aspx">Back</a> | 
                <a href="javascript:void(0);" onclick="window.print();">Print</a>
            </div>

            <!-- Invoice Header -->
            <div class="invoice-header">
                <strong>M.P. Warehousing & Logistics Corporation - Bhopal<br />
                    STORAGE BILL</strong>
                <div>
                    <span>GST: </span><strong>
                        <asp:Label ID="Label1" runat="server" Text="23AADCM7742B3ZS"></asp:Label></strong>
                    &nbsp;&nbsp;
                    <span>PAN: </span><strong>
                        <asp:Label ID="Label2" runat="server" Text="AADCM7742B"></asp:Label></strong>
                </div>
                <div>
                    <span>Region: </span><strong>
                        <asp:Label ID="lblRegion" runat="server"></asp:Label></strong>
                    &nbsp;&nbsp;
                    <span>District: </span><strong>
                        <asp:Label ID="lbldist" runat="server"></asp:Label></strong>
                    &nbsp;&nbsp;
                    <span>Branch: </span><strong>
                        <asp:Label ID="lblBranch" runat="server"></asp:Label></strong>
                </div>
            </div>

            <!-- Info Box -->
            <div class="info-box">
                <table>
                    <tr>
                        <td>Warehouse Name: <strong>
                            <asp:Label ID="lblAcGdwnName" runat="server"></asp:Label>
                            (<asp:Label ID="lblgodownid" runat="server"></asp:Label>)</strong></td>
                        <td>Branch Name: <strong>
                            <asp:Label ID="lbllogbranch" runat="server"></asp:Label></strong></td>
                    </tr>
                    <tr>
                        <td>Month: <strong>
                            <asp:Label ID="lbldatefromto" runat="server"></asp:Label></strong></td>
                        <td>Billing Date: <strong>
                            <asp:Label ID="lblbillingdate" runat="server"></asp:Label></strong></td>
                        <td>Bill Number: <strong>
                            <asp:Label ID="lblbillno_Actual" runat="server"></asp:Label></strong></td>
                    </tr>
                    <tr>
                        <td>Depositor Name: <strong>
                            <asp:Label ID="lbldepositor" runat="server" Text="NCCF"></asp:Label></strong></td>
                        <td>Commodity: <strong>
                            <asp:Label ID="lblcmd_ac" runat="server"></asp:Label></strong></td>
                        <td>Charges: <strong>
                            <asp:Label ID="lblrate" runat="server"></asp:Label></strong></td>
                    </tr>
                </table>
            </div>

            <!-- GridView -->
            <asp:GridView runat="server" ID="GD2" CssClass="EU_DataTable" AutoGenerateColumns="False" ShowFooter="True" AllowSorting="True" AllowPaging="false">
                <Columns>
                    <asp:TemplateField HeaderText="S. No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                            <asp:HiddenField ID="hdnTotal_Charges" runat="server" Value='<%# Eval("Total_Charges") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Commodity" HeaderText="Commodity" HtmlEncode="true" />
                    <asp:BoundField DataField="Bill_Month" HeaderText="Bill Month" HtmlEncode="true" />
                    <asp:BoundField DataField="Dates_Period" HeaderText="Dates Period" HtmlEncode="true" />
                    <asp:BoundField DataField="Opening_Balance" HeaderText="Opening Balance" HtmlEncode="true" />
                    <asp:BoundField DataField="Receive_Bags" HeaderText="Receive Bags" HtmlEncode="true" />
                    <asp:BoundField DataField="Issue_Bags" HeaderText="Issue Bags" HtmlEncode="true" />
                    <asp:BoundField DataField="Closing_Balance" HeaderText="Closing Balance" HtmlEncode="true" />
                    <asp:BoundField DataField="Reserve_Bags" HeaderText="Reserve Bags" HtmlEncode="true" />
                    <asp:BoundField DataField="Chargable_Bags" HeaderText="Chargable Bags" HtmlEncode="true" />
                    <asp:BoundField DataField="Total_Charges" HeaderText="Total Charges" HtmlEncode="true" />
                </Columns>
            </asp:GridView>

            <!-- Amount -->
            <div class="amount-box">
                * Rupees:
                <asp:Label ID="lblam" runat="server">...</asp:Label>
            </div>

            <!-- Signature -->
            <div class="signature-box">
                <table>
                    <tr>
                        <td>
                            <div style="text-align: center;">
                                <asp:Image ID="Image2" runat="server" Height="30px" Visible="false" ImageUrl="~/images/dsc1.png" Width="80px" />
                            </div>
                            <asp:Label ID="lblICSerialNo" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                            <asp:Label ID="lblICHoldername" runat="server" Text=""></asp:Label><br />
                            <asp:Label ID="lblICCreatedDate" runat="server" Text=""></asp:Label><br />
                            <asp:Label ID="lblICIp" runat="server" Text=""></asp:Label><br />
                            <span class="signature-title">Signature of Regional Manager</span>
                        </td>
                        <td>
                            <div style="text-align: center;">
                                <asp:Image ID="Image1" runat="server" Height="30px" Visible="false" ImageUrl="~/images/dsc1.png" Width="80px" />
                            </div>
                            <asp:Label ID="lblBSerialNo" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                            <asp:Label ID="lblBHolderName" runat="server" Text=""></asp:Label><br />
                            <asp:Label ID="lblBCreatedDate" runat="server" Text=""></asp:Label><br />
                            <asp:Label ID="lblBIP" runat="server" Text=""></asp:Label><br />
                            <span class="signature-title">Signature of Branch Manager</span>
                        </td>
                    </tr>
                </table>
            </div>

            <!-- Notice -->
            <div class="notice-box">
                * THE ABOVE STORED STOCKS ARE KEPT IN GOOD CONDITION WITH PROPER FUMIGATION & SCIENTIFIC STORAGE BY MPWLC.<br />
                * This bill is digitally signed, therefore it does not require any stamp & signature.
            </div>
        </div>
    </form>
</body>
</html>