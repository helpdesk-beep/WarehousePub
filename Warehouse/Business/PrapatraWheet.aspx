<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrapatraWheet.aspx.cs" Inherits="StatePages_PrapatraWheet" %>

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
            var prtGrid = document.getElementById('<%=gvCol3.ClientID %>');
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
            var gridData = document.getElementById('<%= gvCol3.ClientID %>');
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

        .auto-style1 {
            width: 84px;
        }

        .auto-style2 {
            width: 150px;
        }
    </style>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
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
             <table style="border: solid 5px #e3e3e8; width: 70%; vertical-align: central; margin-left:200px;">
                   <tr>
                    <td style="width: 150px" align="right">
                        <asp:Label ID="lblDistrict" runat="server" Text="Date (DD-MM-YYYY)" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 150px; margin-left: 10px;" align="left">
                        <asp:TextBox ID="txtpaymentdate" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 150px" align="right">
                        <asp:Label ID="Label9" runat="server" Text="Region:"></asp:Label>
                    </td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:DropDownList ID="ddlregion" runat="server" AutoPostBack="true" CssClass="form-control" >
                        </asp:DropDownList>
                    </td>
                    <td style="width: 100px" align="right">
                        <asp:Button ID="Button1" Text="Show Details" runat="server" CssClass="button button2" OnClick="Button1_Click" />
                    </td>
                </tr>
                </table>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                <tr>
                    <td style="padding-top: 20px; padding-bottom: 20px; background-color: skyblue; font-family: 'Times New Roman'; font-size: 20pt; color: white; text-align: center;">मध्यप्रदेश वेअरहाउससिंग ए ड लॉजिस्टिक्स कार्पोरेशन, क्षेत्रीय कार्यालय<br />
                        निगम की  स्वयं निर्मित/ JVS   एवं गोदाम में भंडारित गेहूँ की कुल क्षमता एवं भंडारित मात्रा की जानकारी दिनांक 26.11.2021 की स्थिति [M.T.] में
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="gvCol3" runat="server" AutoGenerateColumns="false" Visible="true"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <tr style="text-align: center;">
                                            <th colspan="6" style="width: 0px"></th>
                                            <th colspan="10" style="width: 200px">कवर्ड गोडाम एवं कैप की दिनांक ----------------- की स्थिति में कुल रिक्त भण्डारण क्षमता(में.टन)</th>
                                            <th colspan="10" style="text-align: center;">दिनांक ----------------- के बाद आज दिनांक ----------------- तक मिली गोदाम / कैप की कुल रिक्त भण्डारण क्षमता(स्कंध के उठाव तथा नविन प्राप्त क्षमता)(में.टन) </th>
                                            <th colspan="10" style="text-align: center;">आज दिनांक तक उपलब्ध कुल प्रगतिशील रिक्त क्षमता (में.टन)</th>

                                        </tr>
                                        <tr>
                                            <th colspan="6" style="width: 0px"></th>
                                            <th colspan="8" style="text-align: center;">GODOWN CAPACITY</th>
                                            <th colspan="6" style="text-align: center;">CAP CAPACITY</th>
                                            <th colspan="8" style="text-align: center;">GODOWN CAPACITY</th>
                                            <th colspan="6" style="text-align: center;">CAP CAPACITY</th>
                                            <th colspan="8" style="text-align: center;">GODOWN CAPACITY</th>
                                            <th colspan="6" style="text-align: center;">CAP CAPACITY</th>
                                        </tr>
                                        <tr>
                                            <th colspan="2" style="width: 0px"></th>
                                            <th colspan="4" style="text-align: center;">अनुमानित उपार्जन</th>
                                            <th colspan="3" style="text-align: center;">MPWLC</th>
                                            <th colspan="5" style="text-align: center;"></th>
                                            <th colspan="3" style="text-align: center;">MPWLC</th>
                                            <th colspan="3" style="text-align: center;"></th>
                                            <th colspan="3" style="text-align: center;">MPWLC</th>
                                            <th colspan="5" style="text-align: center;"></th>
                                            <th colspan="3" style="text-align: center;">MPWLC</th>
                                            <th colspan="3" style="text-align: center;"></th>
                                            <th colspan="3" style="text-align: center;">MPWLC</th>
                                            <th colspan="5" style="text-align: center;"></th>
                                            <th colspan="3" style="text-align: center;">MPWLC</th>
                                            <th colspan="3" style="text-align: center;"></th>
                                        </tr>
                                        <tr>
                                            <th></th>
                                            <th style="text-align: center;">District Name</th>
                                            <th style="text-align: center;">Branch</th>
                                            <th style="text-align: center;">धान</th>
                                            <th style="text-align: center;">मोटा अनाज</th>
                                            <th style="text-align: center;">कुल अनुमानित उपार्जन</th>
                                            <th style="text-align: center;">भण्डारण हेतु कुल आवश्यक क्षमता</th>

                                            <th style="text-align: center;">MPWLC OWN</th>
                                            <th style="text-align: center;">JVS</th>
                                            <th style="text-align: center;">Hired + Adhigrahan</th>
                                            <th style="text-align: center;">Agrementedd PVT PEG Godown</th>
                                            <th style="text-align: center;">CWC</th>
                                            <th style="text-align: center;">Markfed</th>
                                            <th style="text-align: center;">Oilfed</th>
                                            <th style="text-align: center;">Total Godown Capacity (8 to 14)</th>
                                            <th style="text-align: center;">MPWLC OWN</th>
                                            <th style="text-align: center;">Mandi CAP</th>
                                            <th style="text-align: center;">Mandi Shed</th>
                                            <th style="text-align: center;">Markfed</th>
                                            <th style="text-align: center;">Pvt. PEG CAP</th>
                                            <th style="text-align: center;">Total CAP Capacity (16 to 20)</th>

                                            <th style="text-align: center;">MPWLC OWN</th>
                                            <th style="text-align: center;">JVS</th>
                                            <th style="text-align: center;">Hired + Adhigrahan</th>
                                            <th style="text-align: center;">Agrementedd PVT PEG Godown</th>
                                            <th style="text-align: center;">CWC</th>
                                            <th style="text-align: center;">Markfed</th>
                                            <th style="text-align: center;">Oilfed</th>
                                            <th style="text-align: center;">Total Godown Capacity (8 to 14)</th>
                                            <th style="text-align: center;">MPWLC OWN</th>
                                            <th style="text-align: center;">Mandi CAP</th>
                                            <th style="text-align: center;">Mandi Shed</th>
                                            <th style="text-align: center;">Markfed</th>
                                            <th style="text-align: center;">Pvt. PEG CAP</th>
                                            <th style="text-align: center;">Total CAP Capacity (16 to 20)</th>

                                            <th style="text-align: center;">MPWLC OWN</th>
                                            <th style="text-align: center;">JVS</th>
                                            <th style="text-align: center;">Hired + Adhigrahan</th>
                                            <th style="text-align: center;">Agrementedd PVT PEG Godown</th>
                                            <th style="text-align: center;">CWC</th>
                                            <th style="text-align: center;">Markfed</th>
                                            <th style="text-align: center;">Oilfed</th>
                                            <th style="text-align: center;">Total Godown Capacity (8 to 14)</th>
                                            <th style="text-align: center;">MPWLC OWN</th>
                                            <th style="text-align: center;">Mandi CAP</th>
                                            <th style="text-align: center;">Mandi Shed</th>
                                            <th style="text-align: center;">Markfed</th>
                                            <th style="text-align: center;">Pvt. PEG CAP</th>
                                            <th style="text-align: center;">Total CAP Capacity (16 to 20)</th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <td><%# Container.DataItemIndex + 1 %></td>
                                        <td><%# Eval("District_Name") %></td>
                                        <asp:HiddenField ID="hdngodownid" runat="server" Value='<%#Eval("District_Id") %>' />
                                        <td>
                                            <asp:Label ID="lblPaddy1" runat="server" Text='<%# Eval("paddy") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMotaanaj1" runat="server" Text='<%# Eval("Fat_grain_Weight") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotal_Estimated_Earnings" runat="server" Text='<%# Eval("Total_Estimated_Earnings") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotal_storage_capacity" runat="server" Text='<%# Eval("Total_storage_capacity") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_OWN1" runat="server" Text='<%# Eval("MPWLC_OWN1") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_JVS1" runat="server" Text='<%# Eval("MPWLC_JVS1") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_HA1" runat="server" Text='<%# Eval("MPWLC_HA1") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblArremented_PVT_PEG1" runat="server" Text='<%# Eval("Arremented_PVT_PEG1") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_CWC1" runat="server" Text='<%# Eval("MPWLC_CWC1") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMarkfed1" runat="server" Text='<%# Eval("Markfed1") %>'></asp:Label>
                                        </td>
                                        <asp:Label ID="lblOILFED1" runat="server" Text='<%# Eval("OILFED1") %>'></asp:Label>

                                        <td>
                                            <asp:Label ID="lblTotal_Godown_Capacity1" runat="server" Text='<%# Eval("Total_Godown_Capacity1") %>'></asp:Label>

                                        </td>
                                        <asp:Label ID="lblMPWLC_OWN2" runat="server" Text='<%# Eval("MPWLC_OWN2") %>'></asp:Label>

                                        <td>
                                            <asp:Label ID="lblMPWLC_Mandi_Cap2" runat="server" Text='<%# Eval("MPWLC_Mandi_Cap2") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_Mandi_Shed2" runat="server" Text='<%# Eval("MPWLC_Mandi_Shed2") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMarkfed2" runat="server" Text='<%# Eval("Markfed2") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPvt_PEG_Cap2" runat="server" Text='<%# Eval("Pvt_PEG_Cap2") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotal_Cap_Capacity2" runat="server" Text='<%# Eval("Total_Cap_Capacity2") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_OWN3" runat="server" Text='<%# Eval("MPWLC_OWN3") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_JVS3" runat="server" Text='<%# Eval("MPWLC_JVS3") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_HA3" runat="server" Text='<%# Eval("MPWLC_HA3") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblArremented_PVT_PEG3" runat="server" Text='<%# Eval("Arremented_PVT_PEG3") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_CWC3" runat="server" Text='<%# Eval("MPWLC_CWC3") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMarkfed3" runat="server" Text='<%# Eval("Markfed3") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOILFED3" runat="server" Text='<%# Eval("OILFED3") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotal_Godown_Capacity3" runat="server" Text='<%# Eval("Total_Godown_Capacity3") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_OWN4" runat="server" Text='<%# Eval("MPWLC_OWN4") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_Mandi_Cap4" runat="server" Text='<%# Eval("MPWLC_Mandi_Cap4") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_Mandi_Shed4" runat="server" Text='<%# Eval("MPWLC_Mandi_Shed4") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMarkfed4" runat="server" Text='<%# Eval("Markfed4") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPvt_PEG_Cap4" runat="server" Text='<%# Eval("Pvt_PEG_Cap4") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotal_Cap_Capacity4" runat="server" Text='<%# Eval("Total_Cap_Capacity4") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_OWN5" runat="server" Text='<%# Eval("MPWLC_OWN5") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_JVS5" runat="server" Text='<%# Eval("MPWLC_JVS5") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_HA5" runat="server" Text='<%# Eval("MPWLC_HA5") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblArremented_PVT_PEG5" runat="server" Text='<%# Eval("Arremented_PVT_PEG5") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_CWC5" runat="server" Text='<%# Eval("MPWLC_CWC5") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMarkfed5" runat="server" Text='<%# Eval("Markfed5") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOILFED5" runat="server" Text='<%# Eval("OILFED5") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotal_Godown_Capacity5" runat="server" Text='<%# Eval("Total_Godown_Capacity5") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_OWN6" runat="server" Text='<%# Eval("MPWLC_OWN6") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_Mandi_Cap5" runat="server" Text='<%# Eval("MPWLC_Mandi_Cap5") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMPWLC_Mandi_Shed5" runat="server" Text='<%# Eval("MPWLC_Mandi_Shed5") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMarkfed6" runat="server" Text='<%# Eval("Markfed6") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPvt_PEG_Cap5" runat="server" Text='<%# Eval("Pvt_PEG_Cap5") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotal_Cap_Capacity5" runat="server" Text='<%# Eval("Total_Cap_Capacity5") %>'></asp:Label>
                                        </td>

                                    </ItemTemplate>

                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
        <script src="../../JS/table2excel.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
                });
            });
        </script>
        <script>
            function AllowAlphabet(e) {
                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : event.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
                    return true;
                else {
                    alert('Please Enter Only Character values.');
                    return false;
                }
            }

            function AllowNumber(evt) {
                if ((evt.which != 46 || self.val().indexOf('.') != -1) && (evt.which < 48 || evt.which > 57)) {
                    alert("Allow Only Numbers");
                    return false;
                }
            }

        </script>
    </form>
</body>
</html>
