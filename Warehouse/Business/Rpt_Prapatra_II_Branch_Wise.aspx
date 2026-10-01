<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Prapatra_II_Branch_Wise.aspx.cs" Inherits="Reports_Rpt_Prapatra_II_Branch_Wise" %>


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
            location.reload();
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
                        &nbsp;&nbsp;&nbsp;&nbsp;<%--<asp:Button ID="btnUpdate" runat="server" CssClass="button button2" Text="Update Pending Months" Visible="false" OnClick="btnUpdate_Click" />--%>
                    </div>
                </div>

            </div>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                <tr>
                    <td style="width: 150px" align="right">
                        <asp:Label ID="lblDistrict" runat="server" Text="Date (DD-MM-YYYY)" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 150px; margin-left: 10px;" align="left">
                        <asp:TextBox ID="txtpaymentdate" runat="server"></asp:TextBox>
                    </td>
                   
                </tr>

                <tr>
                    <td colspan="8" style="text-align: center;">
                        <asp:Button ID="btnshow" runat="server" Text="View" CssClass="button button2" OnClick="btnshow_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="8">

                        <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:BoundField DataField="District_Name" HeaderText="District Name">
                                         <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField> 
                                     <asp:BoundField DataField="District_Id" HeaderText="District_Id" />
                                <asp:BoundField DataField="DepotName" HeaderText="Branch Name">
                                         <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name">
                                         <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>

                                <asp:TemplateField HeaderText="वजन">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeight" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="मोटा अनाज">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMotaanaj1" runat="server" Text='<%# Eval("Fat_grain_Weight") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="भण्डारण हेतु कुल आवश्यक क्षमता">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal_A_B" runat="server" Text='<%# Eval("Total_A_B") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MPWLC OWN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_OWN1" runat="server" Text='<%# Eval("MPWLC_OWN1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JVS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_JVS1" runat="server" Text='<%# Eval("MPWLC_JVS1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hired + Adhigrahan">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_HA1" runat="server" Text='<%# Eval("MPWLC_HA1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Agrementedd PVT PEG Godown">
                                    <ItemTemplate>
                                        <asp:Label ID="lblArremented_PVT_PEG1" runat="server" Text='<%# Eval("Arremented_PVT_PEG1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CWC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_CWC1" runat="server" Text='<%# Eval("MPWLC_CWC1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Markfed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarkfed1" runat="server" Text='<%# Eval("Markfed1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oilfed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOILFED1" runat="server" Text='<%# Eval("OILFED1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Godown Capacity (8 to 14)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal_Godown_Capacity1" runat="server" Text='<%# Eval("Total_Godown_Capacity1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MPWLC OWN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_OWN2" runat="server" Text='<%# Eval("MPWLC_OWN2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mandi CAP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_Mandi_Cap2" runat="server" Text='<%# Eval("MPWLC_Mandi_Cap2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mandi Shed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_Mandi_Shed2" runat="server" Text='<%# Eval("MPWLC_Mandi_Shed2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Markfed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarkfed2" runat="server" Text='<%# Eval("Markfed2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pvt. PEG CAP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPvt_PEG_Cap2" runat="server" Text='<%# Eval("Pvt_PEG_Cap2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total CAP Capacity (16 to 20)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal_Cap_Capacity2" runat="server" Text='<%# Eval("Total_Cap_Capacity2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MPWLC OWN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_OWN3" runat="server" Text='<%# Eval("MPWLC_OWN3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JVS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_JVS3" runat="server" Text='<%# Eval("MPWLC_JVS3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hired + Adhigrahan">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_HA3" runat="server" Text='<%# Eval("MPWLC_HA3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agrementedd PVT PEG Godown">
                                    <ItemTemplate>
                                        <asp:Label ID="lblArremented_PVT_PEG3" runat="server" Text='<%# Eval("Arremented_PVT_PEG3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CWC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_CWC3" runat="server" Text='<%# Eval("MPWLC_CWC3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Markfed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarkfed3" runat="server" Text='<%# Eval("Markfed3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oilfed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOILFED3" runat="server" Text='<%# Eval("OILFED3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Godown Capacity (22 to 28)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal_Godown_Capacity3" runat="server" Text='<%# Eval("Total_Godown_Capacity3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MPWLC OWN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_OWN4" runat="server" Text='<%# Eval("MPWLC_OWN4") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mandi CAP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_Mandi_Cap4" runat="server" Text='<%# Eval("MPWLC_Mandi_Cap4") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mandi Shed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_Mandi_Shed4" runat="server" Text='<%# Eval("MPWLC_Mandi_Shed4") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Markfed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarkfed4" runat="server" Text='<%# Eval("Markfed4") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pvt. PEG CAP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPvt_PEG_Cap4" runat="server" Text='<%# Eval("Pvt_PEG_Cap4") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total CAP Capacity (30 to 34)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal_Cap_Capacity4" runat="server" Text='<%# Eval("Total_Cap_Capacity4") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MPWLC OWN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_OWN5" runat="server" Text='<%# Eval("MPWLC_OWN5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JVS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_JVS5" runat="server" Text='<%# Eval("MPWLC_JVS5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hired + Adhigrahan">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_HA5" runat="server" Text='<%# Eval("MPWLC_HA5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agrementedd PVT PEG Godown">
                                    <ItemTemplate>
                                        <asp:Label ID="lblArremented_PVT_PEG5" runat="server" Text='<%# Eval("Arremented_PVT_PEG5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CWC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_CWC5" runat="server" Text='<%# Eval("MPWLC_CWC5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Markfed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarkfed5" runat="server" Text='<%# Eval("Markfed5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oilfed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOILFED5" runat="server" Text='<%# Eval("OILFED5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Godown Capacity (36 to 42)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal_Godown_Capacity5" runat="server" Text='<%# Eval("Total_Godown_Capacity5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MPWLC OWN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_OWN6" runat="server" Text='<%# Eval("MPWLC_OWN6") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mandi CAP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_Mandi_Cap5" runat="server" Text='<%# Eval("MPWLC_Mandi_Cap5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mandi Shed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_Mandi_Shed5" runat="server" Text='<%# Eval("MPWLC_Mandi_Shed5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Markfed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarkfed6" runat="server" Text='<%# Eval("Markfed6") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pvt. PEG CAP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPvt_PEG_Cap5" runat="server" Text='<%# Eval("Pvt_PEG_Cap5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total CAP Capacity (44 to 48)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal_Cap_Capacity5" runat="server" Text='<%# Eval("Total_Cap_Capacity5") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Label ID="lblMsg" runat="server" BackColor="Red" Font-Size="Large"></asp:Label>
        </div>
        <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
        <script src="../../JS/table2excel.js"></script>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.8.0/css/bootstrap-datepicker.min.css" />
        <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
        <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
                });
            });
        </script>
        <script>
            $(document).ready(function () {
                $("[id$=txtpaymentdate]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                });
            });
        </script>
    </form>
</body>
</html>
