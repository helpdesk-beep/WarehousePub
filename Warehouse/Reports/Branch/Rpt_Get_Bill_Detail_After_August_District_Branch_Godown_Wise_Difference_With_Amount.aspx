<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true"
    CodeFile="Rpt_Get_Bill_Detail_After_August_District_Branch_Godown_Wise_Difference_With_Amount.aspx.cs" Inherits="IssueCenterLevel_Storage_Reports_Rpt_Get_Bill_Detail_After_August_District_Branch_Godown_Wise_Difference_With_Amount" Title="Storage Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            padding: 0px 0
            text- lign: center;
            text-decor tion: displa : inl fnt-s font-weight: bold;
            margi -webkit-t ansit 0.4s;
            / Safari tran ition-;
            p W heigh : 50px .bu ackroundcolor: w lack;
            id #4CAF50;
        }


        n1:hov r {
            olor: #4 A color: wh te;
            .bu ackroundcolor: w lack;
            id #008CBA;
        }


        n2:hov r {
            nd-color: #008CBA;
            color: white;
    </style>
    <script>
        var doc = new jsPDF();

        function saveDiv(divId, title) {
            doc.fromHTML(`<html><head><title>${title}</title></head><body>` + document.getElementById(divId).innerHTML + `</body></html>`);
            doc.save('div.pdf');
        }

        function printDiv(divId,
          title) {

            let mywindow = window.open('', 'PRINT', 'height=650,width=900,top=100,left=150');

            mywindow.document.write(`<html><head><title>${title}</title>`);
            mywindow.document.write('</head><body >');
            mywindow.document.write(document.getElementById(divId).innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            mywindow.close();

            return true;
        }
    </script>
    <script type="text/javascript">
    function PrintGrid(html, css) {
        var printWin = window.open('', '', 'left=0,top=0,width=400,height=300,scrollbars=1');
        printWin.document.write('<style type = "text/css">' + css + '</style>');
        printWin.document.write(html);
        printWin.document.close();
        printWin.focus();
        printWin.print();
        printWin.close();
    };
</script>
    <div>
        <div class="container py-4">
            <div class="card">

                <div class="card-body">
                    <button onclick="printDiv('pdf','Title')">print div</button>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExportToExcel" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="ExportToExcel" OnClick="Button1_Click" />
                   
                  
                </div>
            </div>

        <div id="pdf">
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
            <%--<tr>
                <td style="padding-top: 20px; padding-bottom: 20px; background-color: skyblue; font-family: 'Times New Roman'; font-size: 20pt; color: white; text-align: center;">District Branch Wise<br />
                    M.P. Warehousing & Logistics Corporarion<br />
                    PVT Godown Storage Charges Bill(After August)
                </td>
            </tr>--%>
            <tr>
                <td style="text-align: left;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>

                            <asp:BoundField DataField="District" HeaderText="1" />
                            <%--<asp:BoundField DataField="District_Id" HeaderText="District_Id" />--%>
                            <asp:BoundField DataField="Branch" HeaderText="2" />
                            <asp:TemplateField HeaderText="3" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblqty" runat="server" Text='<%# Eval("noofgdwn") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="4" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblqty1" runat="server" Text='<%# Eval("NoOfGenerateBill") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="5" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblNoofBillGenerateAmt" runat="server" Text='<%# Eval("NoofBillGenerateAmt") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="6" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblqty2" runat="server" Text='<%# Eval("NoOfDSCSingBill") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty3" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="7" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblNoofBillAmtDSC" runat="server" Text='<%# Eval("NoofBillAmtDSC") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty4" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="8=(4-6)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblBilldiffDSC" runat="server" Text='<%# Eval("BilldiffDSC") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty5" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="9=(5-7)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblBilldiffAmt" runat="server" Text='<%# Eval("BilldiffAmt") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty6" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="10" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblqty3" runat="server" Text='<%# Eval("NoOfSUBBill") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty7" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="11" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblSUBBillAmt" runat="server" Text='<%# Eval("SUBBillAmt") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty8" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="12=(6-10)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDSCsignedbyBM" runat="server" Text='<%# Eval("DSCsignedbyBM") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty9" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="13=(7-11)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblDSCsignedbyBMAmt" runat="server" Text='<%# Eval("DSCsignedbyBMAmt") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty10" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="14" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblqty4" runat="server" Text='<%# Eval("NoOfSignBill_CSMS") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty11" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="15" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblSUBBillAmt" runat="server" Text='<%# Eval("NoofCSMS_BillAmt") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty12" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="16=(10-14)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblBMsubmited" runat="server" Text='<%# Eval("BMsubmited") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty13" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="17=(11-15)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblBMAmtsubmited" runat="server" Text='<%# Eval("BMAmtsubmited") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty14" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="18" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblqty5" runat="server" Text='<%# Eval("NoOfGdwnRM") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty15" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="19" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblROBillAmt" runat="server" Text='<%# Eval("Approved_By_AGM_Acc_of_RO_Leval_With_Amount") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty16" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="20=(14-18)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblSignedbyICM" runat="server" Text='<%# Eval("SignedbyICM") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty17" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="21=(15-19)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmtSignedbyICM" runat="server" Text='<%# Eval("AmtSignedbyICM") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty18" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="22" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblqty6" runat="server" Text='<%# Eval("NoOfRMDSC") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty19" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="23" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblNoOfRMDSCAmt" runat="server" Text='<%# Eval("NoOfRMDSCAmt") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty20" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="24=(18-22)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblApprovedbyAGM" runat="server" Text='<%# Eval("ApprovedbyAGM") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty21" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="25=(19-23)" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblApprovedAmtbyAGM" runat="server" Text='<%# Eval("ApprovedAmtbyAGM") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty22" runat="server" Font-Bold="true" />
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
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="../../JS/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GridView1]").table2excel({
                filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
            });
        });
    </script>
</asp:Content>
