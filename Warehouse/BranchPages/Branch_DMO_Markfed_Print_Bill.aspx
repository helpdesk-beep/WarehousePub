<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Branch_DMO_Markfed_Print_Bill.aspx.cs" Inherits="BranchPages_Branch_DMO_Markfed_Print_Bill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>DMO_Markfed Storage Bill</title>
    <link href="http://mpsc.mp.nic.in/Warehouse/Administration/assets/css/Table-MultiDesigns.css" rel="stylesheet" type="text/css" />
    <script src="http://mpsc.mp.nic.in/Warehouse/Administration/assets/jQuery%20Package/jquery-1.10.2/jquery-1.10.2.js" type="text/javascript"></script>
    <style>
        body {
            font-family: "Segoe UI", Calibri, Arial, sans-serif;
            background: #fff;
            margin: 0;
            padding: 0;
            color: #000;
            font-size: 13px;
        }

        #printWrapper {
            width: 950px;
            margin: 15px auto;
            padding: 18px;
            border: 2px solid #000;
            border-radius: 6px;
            box-sizing: border-box;
        }

        /* Top Nav */
        .top-nav {
            margin-bottom: 12px;
            font-size: 13px;
            font-weight: 600;
        }

            .top-nav a {
                text-decoration: none;
                color: #0056b3;
            }

        /* Header */

        /* Info Box */
        .info-box {
            background: #f3f3f3;
            border: 2px solid #000;
            border-bottom: 4px solid #000;
            padding: 12px 10px 14px;
            margin-bottom: 20px;
        }

        .info-table {
            width: 100%;
        }

            .info-table td {
                padding: 6px 4px;
            }

        /* Field & Value */
        .field {
            color: #555;
            font-size: 12.5px;
        }

        .value {
            font-weight: 700;
            font-size: 13.5px;
            margin-left: 4px;
            color: #000;
        }

        /* Grid */
        .EU_DataTable {
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;
            font-size: 12.5px;
        }

            .EU_DataTable th {
                background: #e9eef3;
                font-weight: 700;
                border: 1px solid #000;
                padding: 6px 4px;
            }

            .EU_DataTable td {
                border: 1px solid #000;
                padding: 5px 4px;
                text-align: center;
            }

            .EU_DataTable tr:nth-child(even) td {
                background: #f7fbff;
            }

        /* Amount */
        .total-amount-box {
            margin-top: 18px;
            padding: 10px 14px;
            border: 2px solid #000;
            background: #fff2f2;
            font-size: 14px;
            font-weight: 600;
        }

        .total-amount {
            font-size: 18px;
            font-weight: 800;
            color: #b10000;
            margin-left: 6px;
        }

        /* Signature */
        .signature-box {
            margin-top: 25px;
            padding: 12px;
            border: 2px solid #000;
            border-radius: 6px;
        }

            .signature-box table {
                width: 100%;
            }

            .signature-box td {
                width: 50%;
                text-align: center;
                font-size: 12.5px;
            }

        .signature-title {
            display: block;
            margin-top: 8px;
            font-weight: 700;
        }

        /* Notice */
        .notice-box {
            margin-top: 20px;
            padding-top: 8px;
            border-top: 3px solid #000;
            text-align: center;
            font-size: 12.5px;
            font-weight: 700;
            color: #c00000;
        }

        /* Print */
        @media print {
            .top-nav {
                display: none;
            }

            table, tr, td, th {
                page-break-inside: avoid;
            }
        }

        @page {
            size: A4 portrait;
            margin: 10mm;
        }

        .invoice-header {
            background: #f3f3f3;
            border: 2px solid #000;
            border-bottom: 4px solid #000;
            padding: 12px 10px 14px;
            margin-bottom: 20px;
        }

        /* Table */
        .header-table {
            width: 100%;
            margin-bottom: 6px;
        }

        .logo-cell {
            width: 80px;
            text-align: left;
        }

            .logo-cell img {
                height: 60px;
            }

        .title-cell {
            text-align: center;
        }

        /* Titles */
        .org-name {
            font-size: 21px;
            font-weight: 700;
            letter-spacing: 0.6px;
        }

        .bill-title {
            font-size: 16px;
            font-weight: 700;
            margin-top: 2px;
        }

        /* Info Rows */
        .header-row {
            text-align: center;
            margin-top: 8px;
            font-size: 13px;
        }

        /* Field & Value */
        .field {
            color: #555;
            margin-right: 4px;
            font-weight: bold;
        }

        .value-box {
            display: inline-block;
            padding: 2px 8px;
            margin-right: 2px;
            border: 1px solid #000;
            background: #fff;
            font-weight: 700;
            font-size: 14px;
        }

        .divider {
            margin: 0 10px;
            font-weight: 700;
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

                <table class="header-table">
                    <tr>
                        <!-- Logo -->

                        <td class="logo-cell">
                            <img src="<%= ResolveUrl("../images/favicon.ico") %>" alt="Logo" />
                        </td>

                        <!-- Title -->
                        <td class="title-cell">
                            <div class="org-name">
                                M.P. Warehousing & Logistics Corporation – Bhopal
                            </div>
                            <div class="bill-title">
                                STORAGE BILL
                            </div>
                        </td>
                    </tr>
                </table>

                <!-- GST / PAN -->
                <div class="header-row">
                    <span class="field">GST No.</span>
                    <span class="value-box">23AADCM7742B3ZS</span>

                    <span class="divider">|</span>

                    <span class="field">PAN</span>
                    <span class="value-box">AADCM7742B</span>
                </div>

                <!-- Location -->
                <div class="header-row">
                    <span class="field">Region :</span>
                    <asp:Label CssClass="value" ID="lblRegion" runat="server" />
                    <span class="divider">|</span>

                    <span class="field">District :</span>
                    <asp:Label ID="lbldist" CssClass="value" runat="server" />
                    <span class="divider">|</span>

                    <span class="field">Branch :</span>
                    <asp:Label ID="lblBranch" CssClass="value" runat="server" />
                </div>

            </div>

            <div class="info-box">
                <table class="info-table">
                    <tr>
                        <td colspan="3">
                            <span class="field">Warehouse Name :</span>
                            <span class="value">
                                <asp:Label ID="lblAcGdwnName" runat="server" />
                                (<asp:Label ID="lblgodownid" runat="server" />)
                            </span>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <span class="field">Month :</span>
                            <span class="value">
                                <asp:Label ID="lbldatefromto" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="field">Billing Date :</span>
                            <span class="value">
                                <asp:Label ID="lblbillingdate" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="field">Bill Number :</span>
                            <span class="value">
                                <asp:Label ID="lblbillno_Actual" runat="server" />
                            </span>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <span class="field">Depositor Name :</span>
                            <span class="value">
                                <asp:Label ID="lbldepositor" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="field">Commodity :</span>
                            <span class="value">
                                <asp:Label ID="lblcmd_ac" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="field">Charges :</span>
                            <span class="value">
                                <asp:Label ID="lblrate" runat="server" />
                            </span>
                        </td>
                    </tr>
                </table>

            </div>
            <!-- GridView -->
            <asp:GridView runat="server" ID="GD2" CssClass="EU_DataTable" AutoGenerateColumns="False" ShowFooter="True"
                AllowSorting="True" AllowPaging="false" OnRowDataBound="GD2_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="S. No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                            <asp:HiddenField ID="hdnTotal_Charges" runat="server" Value='<%# Eval("Total_Charges") %>' />
                            <asp:HiddenField ID="hdnTotal_Closing" runat="server" Value='<%# Eval("Chargable_Bags") %>' />
                            <asp:HiddenField ID="hdnClosing_Balance" runat="server" Value='<%# Eval("Closing_Balance") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Commodity" HeaderText="Commodity" />
                    <asp:BoundField DataField="Bill_Month" HeaderText="Bill Month" />
                    <asp:BoundField DataField="Dates_Period" HeaderText="Dates Period" />
                    <asp:BoundField DataField="Opening_Balance" HeaderText="Opening Balance" />
                    <asp:BoundField DataField="Receive_Bags" HeaderText="Receive Bags" />
                    <asp:BoundField DataField="Issue_Bags" HeaderText="Issue Bags" />
                    <asp:BoundField DataField="Closing_Balance" HeaderText="Closing Balance" FooterStyle-Font-Bold="true"
                        FooterStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Reserve_Bags" HeaderText="Reserve Bags" />
                    <%--<asp:BoundField DataField="Chargable_Bags" HeaderText="Chargable Bags" />--%>
                    <asp:BoundField DataField="Chargable_Bags" HeaderText="Chargable Bags" FooterStyle-Font-Bold="true"
                        FooterStyle-HorizontalAlign="Right" />
                    <%--<asp:BoundField DataField="Total_Charges" HeaderText="Total Charges" />--%>
                    <asp:BoundField DataField="Total_Charges" HeaderText="Total Charges" ItemStyle-HorizontalAlign="Right"
                        FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" />
                </Columns>
            </asp:GridView>

            <!-- Amount -->
            <div class="total-amount-box">
                Total Amount (₹) :
                 <span class="total-amount">
                     <asp:Label ID="lblam" runat="server" />
                 </span>
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
