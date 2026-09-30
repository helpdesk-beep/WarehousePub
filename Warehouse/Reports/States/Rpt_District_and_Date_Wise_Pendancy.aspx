<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_District_and_Date_Wise_Pendancy.aspx.cs" Inherits="Reports_States_Rpt_District_and_Date_Wise_Pendancy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title style="color: white;">State Level Rerport</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.css" rel="stylesheet" />
    <script src="scripts/dataTables.bootstrap4.min.js"></script>
    <script src="scripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript">  
        $(document).ready(function () {
            $("#EmployeeGridViewList").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
    <style>
        .btnMargin {
            margin-bottom: 10px !important;
        }
    </style>
    <style>
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
            width: 100%;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #00aad2 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #00aad2 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=GridView1.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }
    </script>
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= GridView1.ClientID %>');
            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
                'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
        }
    </script>
    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
            Width: 200px;
            height: 50px;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container py-4">
                <div class="card">

                    <div class="card-body">
                        <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                    </div>
                </div>

            </div>
            <table style="border: solid 5px #e3e3e8; width: 80%; vertical-align: central; margin-left: 10%;">
                <tr>
                    <td style="text-align: center;">From Date (DD/MM/YYYY):&nbsp;&nbsp;
                        <asp:TextBox ID="txtFDate" runat="server" ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        To Date (DD/MM/YYYY):&nbsp;&nbsp;<asp:TextBox ID="txtTDate" runat="server"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick=" return validate()" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                <%--<tr>
                    <td style="padding-top: 20px; padding-bottom: 20px; background-color: skyblue; font-family: 'Times New Roman'; font-size: 20pt; color: white; text-align: center;">District Wise<br />
                        M.P. Warehousing & Logistics Corporarion<br />
                        Godown Storage Charges Bill(From August)
                    </td>
                </tr>--%>
                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Regionnm" HeaderText="1" />
                                <asp:BoundField DataField="District" HeaderText="2" />
                                <asp:TemplateField HeaderText="3" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblnoofgdwn" runat="server" Text='<%# Eval("noofgdwn") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="4" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingBill" runat="server" Text='<%# Eval("PendingBill") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="5" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBilldiffAmt" runat="server" Text='<%# Eval("BilldiffAmt") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty3" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="6" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDSCsignedbyBMAmt" runat="server" Text='<%# Eval("DSCsignedbyBMAmt") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty4" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="7" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCentreInchargeDscPending" runat="server" Text='<%# Eval("CentreInchargeDscPending") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty5" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="8" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingDMAmt" runat="server" Text='<%# Eval("PendingDMAmt") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty6" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="9" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingHOAmt" runat="server" Text='<%# Eval("PendingHOAmt") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty7" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingPassingOrder" runat="server" Text='<%# Eval("PendingPassingOrder") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty8" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="11" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingAGMDSC" runat="server" Text='<%# Eval("PendingAGMDSC") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty9" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="12" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApprovedAmtbyAGM" runat="server" Text='<%# Eval("ApprovedAmtbyAGM") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty10" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="13" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedEPFAmount" runat="server" Text='<%# Eval("PendingCreatedEPF") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty12" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="14" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingNefHOAmount" runat="server" Text='<%# Eval("PendingNefHOAmount") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotalqty11" runat="server" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="Black" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js"></script>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.8.0/css/bootstrap-datepicker.min.css" />
        <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
                });
            });
        </script>
        <script language="javascript" type="text/javascript">
            function validate() {
                if (document.getElementById("<%=txtFDate.ClientID%>").value == "") {
                    alert("From Date can not be blank");
                    document.getElementById("<%=txtFDate.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtTDate.ClientID %>").value == "") {
                    alert("To Date can not be blank");
                    document.getElementById("<%=txtTDate.ClientID %>").focus();
                    return false;
                }

            }
            $(document).ready(function () {
                $("[id$=txtFDate]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                    maxDate: 'today',
                });
            });
            $(document).ready(function () {
                $("[id$=txtTDate]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                    maxDate: 'today',
                });
            });
        </script>
    </form>
</body>
</html>
