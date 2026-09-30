<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_Nafed_Bill_For_RM.aspx.cs" Inherits="Region_Print_Nafed_Bill_For_RM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<link href="../assets/css/style.css" rel="stylesheet" />
<!-- font awesome -->
<link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
<link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
<link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
<link href="../CSS/style.css" rel="stylesheet" />
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
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
<style type="text/css">
    .left, .right {
        float: left;
        width: 20%; /* The width is 20%, by default */
    }

    .main {
        float: left;
        width: 60%; /* The width is 60%, by default */
    }

    @media screen and (max-width: 800px) {
        .left, .main, .right {
            width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
        }
    }
</style>
<style type="text/css">
    fieldset {
        border: 1px solid #2095A1;
        padding: 0.35em 0.625em 0.75em;
        margin: 10px;
        border-radius: 5px;
        padding-left: 20px;
    }

    legend {
        padding: 2px 8px;
        border-radius: 10px;
        width: auto;
        border: 1px solid #2095A1;
        font-size: 17px;
        font-weight: bold;
        color: #030203;
    }

    .content-wrapper {
        padding: 1.75rem 1.25rem;
    }

    .table-bordered th, .table-bordered td {
        border: 1px solid #030203;
    }

    .form-control {
        border: 1px solid #767B83;
        border-radius: 8px;
    }

    .table th {
        text-align: center;
    }

    .form-inline {
        display: block !important;
    }

    th.sorting, th.sorting_asc, th.sorting_desc {
        background: #647e68 !important;
        color: black !important;
    }

    .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
        padding: 8px 5px;
        color: black !important;
    }

    element.style {
        font-size: medium !important;
    }
</style>
<style type="text/css">
    .modalBackground {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }

    .modalPopup {
        background-color: #FFFFFF;
        width: 80%;
        border: 3px solid #0DA9D0;
        border-radius: 6px;
        padding: 0
    }

        .modalPopup .header {
            background-color: #2FBDF1;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
            border-top-left-radius: 6px;
            border-top-right-radius: 6px;
        }

        .modalPopup .body {
            min-height: 50px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }

        .modalPopup .footer {
            padding: 6px;
        }

        .modalPopup .yes, .modalPopup .no {
            height: 23px;
            color: White;
            line-height: 23px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            border-radius: 4px;
        }

        .modalPopup .yes {
            background-color: #2FBDF1;
            border: 1px solid #0DA9D0;
        }

        .modalPopup .no {
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }
</style>
<script type="text/javascript">
    function PrintDiv() {
        var divContents = document.getElementById("PrintDiv").innerHTML;
        var printWindow = window.open('', '', 'height=700,width=1000');
        //   printWindow.document.write('<html><head><title>WHR</title>');
        printWindow.document.write('</head><body >');
        printWindow.document.write(divContents);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
        printWindow.close();
    }
</script>
<script type="text/javascript">
    function PrintDiv_det() {
        var divContents = document.getElementById("PrintDiv_Det").innerHTML;
        var printWindow = window.open('', '', 'height=700,width=1000');
        //   printWindow.document.write('<html><head><title>WHR</title>');
        printWindow.document.write('</head><body >');
        printWindow.document.write(divContents);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
        printWindow.close();
    }
</script>
<script type="text/javascript">
    function PrintDiv_Actual() {
        var divContents = document.getElementById("printActualBill").innerHTML;
        var printWindow = window.open('', '', 'height=700,width=1000');
        //   printWindow.document.write('<html><head><title>WHR</title>');
        printWindow.document.write('</head><body >');
        printWindow.document.write(divContents);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
        printWindow.close();
    }
</script>
<script type="text/javascript">
    function PrintDiv_Epo() {
        var divContents = document.getElementById("PrintDivEpo").innerHTML;
        var printWindow = window.open('', '', 'height=700,width=1000');
        //   printWindow.document.write('<html><head><title>WHR</title>');
        printWindow.document.write('</head><body >');
        printWindow.document.write(divContents);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
        printWindow.close();
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

    .auto-style3 {
        height: 17px;
    }
</style>
<head runat="server">
    <title>Print Nafed Rent Bill</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        <div>
                            <table cellspacing="1" cellpadding="3" style="width: 100%;">
                                <tr>
                                    <td align="center" width="100%">
                                        <div id="PrintDiv">
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Label ID="Label1" runat="server" Text="मध्य प्रदेश वेयर हाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन, क्षेत्रीय कार्यालय -" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="lblP_regionnm" runat="server" Font-Size="14px" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Label ID="Label2" runat="server" Text="-: वास्तविक भंडारित मात्रा अंतर्गत J.V. योजना मे लिए गए गोदामो के देयक :-" Font-Size="14px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px;"></td>
                                                </tr>
                                                <tr>
                                                    <td width="60%" align="left">
                                                        <asp:Label ID="Label5" runat="server" Text="शाखा का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblbranch" runat="server" Text=" " Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td width="40%" align="left">
                                                        <asp:Label ID="Label3" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblbillmonth" runat="server" Text=" " Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="Label9" runat="server" Text="वेयरहाउस का नाम :-  " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblgdwnname" runat="server" Text=" " Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="Label7" runat="server" Text="गोदाम क्र. :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblGdnum" runat="server" Text="" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="Label11" runat="server" Text="बिल क्रमांक :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblbillno" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="Label13" runat="server" Text="भंडारित स्कंध का नाम :- " Font-Size="12px"></asp:Label>&nbsp;
                                            <asp:Label ID="lblcmd" runat="server" Text="माह :- " Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td align="left" class="auto-style3">
                                                        <asp:Label ID="Label15" runat="server" Text="GSTN : 23AADCM7742B1ZU" Font-Size="12px"></asp:Label>&nbsp;
                                                    </td>
                                                    <td align="left" class="auto-style3">
                                                        <asp:Label ID="Label17" runat="server" Text="मात्रा :- मेट्रिक टन मे" Font-Size="12px"></asp:Label>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="Label21" runat="server" Text="PAN : AADCM7742B" Font-Size="12px"></asp:Label>&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="Label4" runat="server" Text="जमाकर्ता :- NAFED" Font-Size="12px"></asp:Label>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px;"></td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:GridView ID="GD1" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Arial"
                                                            DataKeyNames="Date" BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                                            Font-Size="11px" BorderColor="#CCCCCC">
                                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="क्र." ItemStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Date" HeaderText="दिनाँक"></asp:BoundField>

                                                                <asp:BoundField DataField="Opening_Weight" HeaderText="प्रारंभिक मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="Receive_Weight" HeaderText="जमा मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="Issue_Weight" HeaderText="भुगतान मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="Closing_Weight" HeaderText="शेष मात्रा" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="Per_Day_Rate" HeaderText="शुल्क दर प्रति दिन" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="Total_Charges" HeaderText="राशि (6x7)" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <FooterStyle Font-Bold="True" ForeColor="#C70039" HorizontalAlign="center" Height="15px" Font-Size="11px" />
                                                            <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 30px;"></td>
                                                </tr>

                                                <%-----------------DSC Div-----------   --%>
                                                <%--    <tr>
                                                    <td align="right" colspan="2" style="width: 100%;">

                                                        <table style="width: 100%;">

                                                            <tr class="fountcolor">
                                                                <td align="right">
                                                                    <asp:Image ID="Image5B" runat="server" Height="30px" Visible="false"
                                                                        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Image ID="Image5" runat="server" Height="30px" Visible="false"
                                                                        ImageUrl="~/images/dsc1.png" Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr class="fountcolor">
                                                                <td align="right">
                                                                    <asp:Label ID="lbldscTB" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                <td align="right">
                                                                    <asp:Label ID="lbldscT" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            </tr>
                                                            <tr class="fountcolor">
                                                                <td align="right">
                                                                    <asp:Label ID="lblDSC_HolderB" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblDSC_Holder" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            </tr>
                                                            <tr class="fountcolor">
                                                                <td align="right">
                                                                    <asp:Label ID="lblSigningDateB" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblSigningDate" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            </tr>
                                                            <tr class="fountcolor">
                                                                <td align="right">
                                                                    <asp:Label ID="lblIpAddB" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblIpAdd" runat="server" Text="" Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            </tr>
                                                            <tr class="fountcolor">
                                                                <td colspan="2">
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr class="fountcolor">
                                                                <td align="right">
                                                                    <asp:Label ID="Label43" runat="server" Text="शाखा प्रबंधक(MPWLC) के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label44" runat="server" Text="माल गोदामपाल के हस्ताक्षर" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            </tr>

                                                        </table>

                                                    </td>
                                                </tr>--%>
                                                <%------------DSC DIv END----------------- --%>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 50px">
                                        <asp:Button class="button button2" Width="150px" Height="30px" ID="btncloseconfrm"
                                            runat="server" Text="Close" align="Center" />
                                        &nbsp &nbsp &nbsp 
     
                            
                        <input id="Button1" name="Print" type="button" style="height: 30px; width: 150px;"
                            class="button button2" value="Print" onclick="PrintDiv();" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="col-md-3"></div>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
