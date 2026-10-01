<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Reports/Region/Rpt_Pending_Bill_Status_at_RM.aspx.cs" Inherits="Reports_Region_Rpt_Godown_Bill_Wise_Payment_Status" %>

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
    <script src="../../JS/gridviewscroll.js"></script>
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">  
        $(document).ready(function () {
            $("#EmployeeGridViewList").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
    <style>
        /* Search box */
        .search-box {
            width: 320px;
            padding: 8px 12px;
            border: 1px solid #ccc;
            border-radius: 8px;
            outline: none;
            font-size: 14px;
            transition: all 0.3s ease;
            margin: 0 15px;
        }

            .search-box:focus {
                border-color: #007bff;
                box-shadow: 0 0 6px rgba(0, 123, 255, 0.4);
            }
        /* Button */
        .BTNBLUE {
            background: linear-gradient(135deg, #007bff, #0056b3);
            border: none;
            color: #fff !important;
            padding: 8px 25px;
            font-size: 14px;
            font-weight: bold;
            border-radius: 8px;
            cursor: pointer;
            transition: 0.3s ease-in-out;
        }

            .BTNBLUE:hover {
                background: linear-gradient(135deg, #0056b3, #00408a);
                transform: translateY(-2px);
                box-shadow: 0px 4px 8px rgba(0,0,0,0.15);
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

        /* Use a media query to add a breakpoint at 800px: */
        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
        }
    </style>
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

        .button3 {
            background-color: white;
            color: black;
            border: 2px solid #f44336;
        }

            .button3:hover {
                background-color: #f44336;
                color: white;
            }

        .button6 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button6:hover {
                background-color: #008CBA;
                color: white;
            }

        .style3 {
            width: 200px;
            height: 11px;
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
            background: white !important;
            color: black !important;
            /*color: white !important;*/
        }

        .GridViewHeader th {
            color: white !important; /* header text white */
            background-color: #4CAF50 !important; /* optional background */
            text-align: center;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            /*color: black !important;*/
            color: white !important;
            /*font-size:14px;*/
        }

        element.style {
            font-size: medium !important;
        }

        .auto-style3 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 50%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
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

    <%-- <script type="text/javascript">
         var gridViewScroll = null;
         window.onload = function () {
             gridViewScroll = new GridViewScroll({
                 elementID: "GridView1",
                 width: 'auto',
                 //element.style.width = 'auto';
                 height: 900,
                 freezeColumn: true,
                 freezeFooter: true,
                 freezeColumnCssClass: "GridViewScrollItemFreeze",
                 // freezeFooterCssClass: "GridViewScrollFooterFreeze",
                 //freezeHeaderRowCount: 2,
                 //freezeColumnCount: 3,
                 onscroll: function (scrollTop, scrollLeft) {
                     console.log(scrollTop + " - " + scrollLeft);
                 }
             });
             gridViewScroll.enhance();
         }
         function getScrollPosition() {
             var position = gridViewScroll.scrollPosition;
             alert("scrollTop: " + position.scrollTop + ", scrollLeft: " + position.scrollLeft);
         }
         function setScrollPosition() {
             var scrollPosition = { scrollTop: 50, scrollLeft: 50 };

             gridViewScroll.scrollPosition = scrollPosition;
         }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 500px;">
            <div class="container py-4">
                <div class="card">

                    <div class="card-body">
                        <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                    </div>
                </div>
            </div>
            <br />
            <br />
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                <tr>
                    <td>
                        <asp:TextBox ID="txtSearch" runat="server"
                            CssClass="search-box"
                            Width="250px"
                            placeholder="Search by Godown Details..."
                            onkeyup="filterGrid()" />
                        <asp:HiddenField ID="hdnSearchValue" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="10">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnDataBound="OnDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Bill_Month" HeaderText="Bill Month" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Bill_Number" HeaderText="Storage Bill Number" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Amount" HeaderText="Storage Bill Amount" ItemStyle-HorizontalAlign="Right" />
                                <%--<asp:BoundField DataField="TotalBillReceivedPayment" HeaderText="Total Bill Received Payment" ItemStyle-HorizontalAlign="Right" />--%>
                                <asp:BoundField DataField="Gross_Amount" HeaderText="Gross Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Amt Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="OtherDeduction" HeaderText="Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Payable_Amount" HeaderText="Amount Credit to MPWLC After All Deduction" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalRentBill" HeaderText="Rent Bill Number" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="RentBillAmt" HeaderText="Rent Bill Amount" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Total" HeaderText="Rent Bill Deduction From MPWLC" ItemStyle-HorizontalAlign="Right" />
                                <%--<asp:BoundField DataField="PayBilltoGodownOwner" HeaderText="Pay Bill to Godown Owner" ItemStyle-HorizontalAlign="Right" />--%>
                                <asp:BoundField DataField="PaytoGodownOwner" HeaderText="Payment Credit to Godown Owner Account After Deduction" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="Black" />
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
                    filename: "Godown_WisePayment_Status.xls"
                });
            });
        </script>
        <script type="text/javascript">
            function filterGrid() {
                var input = document.getElementById('<%= txtSearch.ClientID %>');
                var filter = input.value.toLowerCase();
                var table = document.getElementById('<%= GridView1.ClientID %>');
                var trs = table.getElementsByTagName("tr");

                // hidden field me textbox ka value set karo
                document.getElementById('<%= hdnSearchValue.ClientID %>').value = input.value;

                for (var i = 1; i < trs.length; i++) { // skip header row
                    var tds = trs[i].getElementsByTagName("td");
                    var show = false;
                    for (var j = 0; j < tds.length; j++) {
                        if (tds[j].innerText.toLowerCase().indexOf(filter) > -1) {
                            show = true;
                            break;
                        }
                    }
                    trs[i].style.display = show ? "" : "none";
                }
            }
        </script>
        <script type="text/javascript">
            function fnChkEmptyData() {
                if (document.getElementById(preid + "txtSearch").value == "") {
                    alert("Godown Id is required.");
                    document.getElementById(preid + "txtSearch").focus();
                    validSubmit = 0;
                    return returnFalse();
                }
            }

        </script>
    </form>
</body>
</html>
