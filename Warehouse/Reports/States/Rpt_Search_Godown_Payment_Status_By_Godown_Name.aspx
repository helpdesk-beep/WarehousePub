<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Reports/States/Rpt_Search_Godown_Payment_Status_By_Godown_Name.aspx.cs" Inherits="Reports_State_Rpt_Search_Godown_Payment_Status_By_Godown_Name" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title style="color: white;">Godown Wise Payment Status (in Lakh)</title>

   <%-- <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/dataTables.bootstrap4.min.css" rel="stylesheet" />--%>
    <link href="../../assets/css/style.css" rel="stylesheet" />
   <%-- <script src="scripts/dataTables.bootstrap4.min.js"></script>
    <script src="scripts/jquery.dataTables.min.js"></script>--%>
     <script src="../../JS/gridviewscroll.js"></script>
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
            Width: 90px;
            height: 20px;
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

    <style>
        .view-button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            /*padding: 10px 15px;*/
            text-align: center;
            text-decoration: none;
           /* display: inline-block;*/
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            border-radius: 5px;
        }

        .view-button:hover {
            background-color: #45a049; /* Darker green */
        }
    </style>
      <script type="text/javascript">
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
    </script>
</head>
<body>
    <form id="form1" runat="server" style="height:auto;">
        <div>
            <div style="text-align: center; font-size: large;">
                <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
                <h4 class="header">Godown Wise Payment Summary (IN LAKH)</h4>
            </div>
            <div style="text-align: center; font-size: large;">



                 <asp:Label ID="Label2" runat="server" Text="Godown Name : "></asp:Label>
               <asp:TextBox ID="txtgodownname" runat="server" Width="500px" Height="30px" CssClass="form-control"></asp:TextBox>

                 <asp:Button class="btn-info" ID="btn_Search" runat="server" Text="Search"
                            TabIndex="11" Width="150px" Height="30px" OnClick="btn_Search_Click"></asp:Button>

               
                <br />
                <br />
               
            </div>
             
            <div style="height:500px;">
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowCommand="GridView1_RowCommand"
                            CssClass="Grid GridViewScrollHeader" AlternatingRowStyle-CssClass="alt" AllowPaging="false" PagerStyle-CssClass="pgr" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Regionnm" HeaderText="Region Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="DepotName" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="TotalStorageBill" HeaderText="Total Storage Bill" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="Amount" HeaderText="Storage Bill Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="TotalSubmittedBill" HeaderText="No of Storage Bill Submitted to MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%" />
                                <asp:BoundField DataField="SubmittedBillAmount" HeaderText="Storage Bill Amount Submitted to MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>
                                 <asp:BoundField DataField="PendingForSubmission" HeaderText="No of Storage Bill Pending For Submission to MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%"/>
                                <asp:BoundField DataField="PendingForSubmissionAmount" HeaderText="Storage Bill Amount Pending For Submission to MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>
                                
                                
                                <asp:BoundField DataField="TotalBillReceivedPayment" HeaderText="Total Bill Received Payment" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%"/>
                                <asp:BoundField DataField="Gross_Amount" HeaderText="Gross Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>
                                <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Amt Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>
                                <asp:BoundField DataField="OtherDeduction" HeaderText="Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>
                                <asp:BoundField DataField="Payable_Amount" HeaderText="Amount Credited to MPWLC Account after all Deduction" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>
                                <asp:BoundField DataField="NoofBillPendingatMPSCSC" HeaderText="No of Bill Pending at MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%"/>
                                <asp:BoundField DataField="NoofBillAmountPendingatMPSCSC" HeaderText="Bill Amount Pending at MPSCSC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>


                                <asp:BoundField DataField="TotalRentBill" HeaderText="Total Rent Bill" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%"/>
                                <asp:BoundField DataField="RentBillAmt" HeaderText="Rent Bill Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>
                                <asp:BoundField DataField="ReceivedRentBill" HeaderText="No of Rent Bill Against Received Storage Bills" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%"/>
                                <asp:BoundField DataField="ReceivedRentBillAmount" HeaderText="Rent Bill Amount Against Received Storage Bills Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>
                               
                                
                                <%--<asp:BoundField DataField="Total" HeaderText="Rent Bill Deduction From MPWLC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>--%>
                                <asp:BoundField DataField="PayBilltoGodownOwner" HeaderText="Pay Bill to Godown Owner" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%"/>
                                <asp:BoundField DataField="PaytoGodownOwner" HeaderText="Payment Credited to Godown Owner Account After Deduction" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>
                                <asp:BoundField DataField="PendingBillatMPWLC" HeaderText="Pending Bills at MPWLC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="2%"/>
                                <asp:BoundField DataField="PendingBillAmountatMPWLC" HeaderText="Pending Amount at MPWLC" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%"/>
                                
                                <asp:TemplateField HeaderText="View Godown Wise Bill Details" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnView" runat="server" Text="Views" CssClass="view-button" CommandName="View" CommandArgument='<%# Eval("Godown_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="District Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="sdffsft" runat="server" NavigateUrl='<%#"#"%>'
                                            title="District Name" Text=' <%# Eval("District_Name") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>    --%>
                                <%-- <asp:BoundField DataField="Region" HeaderText="Region" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="District" HeaderText="District" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Branch" HeaderText="Branch" ItemStyle-HorizontalAlign="Left" />--%>
                                <%-- <asp:TemplateField HeaderText="District Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/WarehouseLevel/Rpt_Godown_Bill_Wise_Payment_Status.aspx?GodownID="+ (Eval("Godown_ID").ToString())%>'
                                            title="Godown Name" Text=' <%# Eval("godown_Name") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>
                                <%--<asp:BoundField DataField="godown_Name" HeaderText="godown_Name" ItemStyle-HorizontalAlign="Left"/>--%>
                                <%--<asp:BoundField DataField="noofgdwn" HeaderText="Total No. of JVS Godown" ItemStyle-HorizontalAlign="Right"/>--%>
                                <%-- <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalStorageBill" HeaderText="TotalStorageBill" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoOfSUBBill" HeaderText="Total No. of Submitted Bill" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="SUBBillAmt" HeaderText="Total Submitted Bill Amount" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PendingBillForSubmisionatBranch" HeaderText="Pending Bill For Submision at Branch" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PendingBillAmountForSubmision" HeaderText="Pending Bill Amount For Submision" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoofbillPaymentReceivedFromMPSCSC" HeaderText="Total Received Bill From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoofbillPaymentAmountReceivedFromMPSCSC" HeaderText="Total Received Bill Amount From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="PaymentDecuctionbyMPSCSC" HeaderText="Payment Deducted From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalNoofPendingBillatMPSCSC" HeaderText="Total No. of Pending Bill at MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="TotalNoofPendingBillAmountatMPSCSC" HeaderText="Total No. of Pending Bill Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoOfBillPayment" HeaderText="Total No. of Bill Pay to Godown Owner" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="BillAmtPTG" HeaderText="Total No. of Bill Amount Pay to Godown Owner" ItemStyle-HorizontalAlign="Right" />--%>
                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="Black" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
                </div>
        </div>
        <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
        <script src="../../JS/table2excel.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "GodownwisePaymentSummary.xls"
                });
            });
        </script>
    </form>
</body>
</html>
