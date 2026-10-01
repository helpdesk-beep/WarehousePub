<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Rpt_Search_Godown_Wise_Pending_Bill_Details_Final_Bill.aspx.cs" Inherits="Region_Reports_Rpt_Search_Godown_Wise_Pending_Bill_Details_Final_Bill" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            var prtGrid = document.getElementById('<%=divshowdetails.ClientID %>');
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
            var gridData = document.getElementById('<%= divshowdetails.ClientID %>');
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

    <div>
        <div id="divshowdetails" runat="server" visible="false">
            <div class="container py-4">
                <div class="card">
                    <div class="card-body">
                        <asp:Button ID="Button2" runat="server" Text="Print To PDF" CssClass="button button2" OnClientClick="printGrid()" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Export To Excel" class="button button2" />

                    </div>
                </div>

            </div>
            <br />

            <table class="table table-bordered" style="margin: auto; margin-top: 10px; margin-bottom: 5px;"
                width="100%">

                <table class="table table-bordered" style="margin: auto; margin-top: 10px; margin-bottom: 5px;" width="100%">

                    <tr id="tr3" runat="server" align="center">
                        <td style="width: 100%" colspan="2" class="list-group-item-danger">
                            <asp:Label ID="Label4" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Verification for Issue Center "></asp:Label>
                            <asp:Panel ID="Panel3" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                                <asp:GridView ID="grd_details" runat="server" AutoGenerateColumns="False"
                                    EmptyDataRowStyle-BackColor="pink"
                                    EmptyDataText="Record Not Found"
                                    CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                                    <columns>
                                        <asp:TemplateField>
                                            <itemtemplate><%#Container.DataItemIndex+1 %> </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BillNo" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                        <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                        <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                        <asp:BoundField DataField="Crop_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                        <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Financial_Year" SortExpression="Date" />
                                        <asp:BoundField DataField="Commodity_Name" ControlStyle-BorderWidth="50px" HeaderText="Commodity_Name" SortExpression="Date" />
                                        <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month" SortExpression="Date" />
                                        <asp:BoundField DataField="WearHouse_NetAmt" ControlStyle-BorderWidth="50px" HeaderText="WharHouse Amount" SortExpression="Date" />
                                        <asp:BoundField DataField="CSMS_NetAmt" ControlStyle-BorderWidth="50px" HeaderText="Csms Amount" SortExpression="Date" />
                                        <asp:BoundField DataField="Godown_Type" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />

                                    </columns>
                                    <emptydatarowstyle backcolor="Pink" />
                                </asp:GridView>
                            </asp:Panel>

                        </td>
                    </tr>

                    <tr id="tr1" runat="server" align="center">
                        <td style="width: 100%" colspan="2" class="list-group-item-danger">
                            <asp:Label ID="Label1" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Digital Sign at Issue Center level "></asp:Label>
                            <asp:Panel ID="Panel1" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                                <asp:GridView ID="grd_digital_sign" runat="server" AutoGenerateColumns="False"
                                    EmptyDataRowStyle-BackColor="pink"
                                    EmptyDataText="Record Not Found"
                                    CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                                    <columns>
                                        <asp:TemplateField>
                                            <itemtemplate><%#Container.DataItemIndex+1 %> </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                        <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                        <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                        <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Financial Year" SortExpression="Date" />
                                        <asp:BoundField DataField="Crop_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                        <asp:BoundField DataField="Commodity_Name" ControlStyle-BorderWidth="50px" HeaderText="Commodity" SortExpression="Date" />
                                        <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month" SortExpression="Date" />
                                        <asp:BoundField DataField="Net_Amount" ControlStyle-BorderWidth="50px" HeaderText="WharHouse Amount" SortExpression="Date" />
                                        <asp:BoundField DataField="Sub_Amount" ControlStyle-BorderWidth="50px" HeaderText="Csms Amount" SortExpression="Date" />

                                    </columns>
                                    <emptydatarowstyle backcolor="Pink" />
                                </asp:GridView>
                            </asp:Panel>

                        </td>
                    </tr>

                    <tr id="tr4" runat="server" align="center">
                        <td style="width: 100%" colspan="2" class="list-group-item-danger">
                            <asp:Label ID="Label3" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bill at DM Level "></asp:Label>
                            <asp:Panel ID="Panel4" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                                <asp:GridView ID="grdDMlevel" runat="server" AutoGenerateColumns="False"
                                    EmptyDataRowStyle-BackColor="pink"
                                    EmptyDataText="Record Not Found"
                                    CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                                    <columns>
                                        <asp:TemplateField>
                                            <itemtemplate><%#Container.DataItemIndex+1 %> </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />
                                        <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                        <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                        <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                        <asp:BoundField DataField="Commodity_Name" ControlStyle-BorderWidth="50px" HeaderText="Commodity" SortExpression="Date" />
                                        <asp:BoundField DataField="Crop_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                        <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Financial Year" SortExpression="Date" />
                                        <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month " SortExpression="Date" />
                                        <asp:BoundField DataField="CSMS_Net_Amount" ControlStyle-BorderWidth="50px" HeaderText="Net Amount" SortExpression="Date" />

                                    </columns>
                                    <emptydatarowstyle backcolor="Pink" />
                                </asp:GridView>
                            </asp:Panel>

                        </td>
                    </tr>

                    <tr id="tr2" runat="server" align="center">
                        <td style="width: 100%" colspan="2" class="list-group-item-danger">
                            <asp:Label ID="Label2" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Digital Signature at DM Level "></asp:Label>
                            <asp:Panel ID="Panel2" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                                <asp:GridView ID="grdDMlevelDigital" runat="server" AutoGenerateColumns="False"
                                    EmptyDataRowStyle-BackColor="pink"
                                    EmptyDataText="Record Not Found"
                                    CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                                    <columns>
                                        <asp:TemplateField>
                                            <itemtemplate><%#Container.DataItemIndex+1 %> </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />
                                        <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                        <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                        <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                        <asp:BoundField DataField="Commodity_Name" ControlStyle-BorderWidth="50px" HeaderText="Commodity" SortExpression="Date" />
                                        <asp:BoundField DataField="Crop_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                        <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Financial Year" SortExpression="Date" />
                                        <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month" SortExpression="Date" />
                                        <asp:BoundField DataField="Net_Amount" ControlStyle-BorderWidth="50px" HeaderText="Net Amount" SortExpression="Date" />

                                    </columns>
                                    <emptydatarowstyle backcolor="Pink" />
                                </asp:GridView>
                            </asp:Panel>

                        </td>
                    </tr>

                    <tr id="tr5" runat="server" align="center">
                        <td style="width: 100%" colspan="2" class="list-group-item-danger">
                            <asp:Label ID="Label5" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bill Payed Via NEFT "></asp:Label>
                            <asp:Panel ID="Panel5" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                                <asp:GridView ID="grdNeft" runat="server" AutoGenerateColumns="False"
                                    EmptyDataRowStyle-BackColor="pink"
                                    EmptyDataText="Record Not Found"
                                    CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                                    <columns>
                                        <asp:TemplateField>
                                            <itemtemplate><%#Container.DataItemIndex+1 %> </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />
                                        <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                        <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                        <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                        <asp:BoundField DataField="Commodity_Name" ControlStyle-BorderWidth="50px" HeaderText="Commodity" SortExpression="Date" />
                                        <asp:BoundField DataField="Crop_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                        <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Financial Year" SortExpression="Date" />
                                        <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month" SortExpression="Date" />
                                        <asp:BoundField DataField="Net_Amount" ControlStyle-BorderWidth="50px" HeaderText="Net Amount" SortExpression="Date" />
                                        <asp:BoundField DataField="DistrictLotId" ControlStyle-BorderWidth="50px" HeaderText="DistrictLotId" SortExpression="Date" />
                                        <asp:BoundField DataField="HoLotID" ControlStyle-BorderWidth="50px" HeaderText="HoLotID" SortExpression="Date" />

                                    </columns>
                                    <emptydatarowstyle backcolor="Pink" />
                                </asp:GridView>
                            </asp:Panel>

                        </td>
                    </tr>

                    <tr id="tr6" runat="server" align="center">
                        <td style="width: 100%" colspan="2" class="list-group-item-danger">
                            <asp:Label ID="Label6" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bills Neft Response "></asp:Label>
                            <asp:Panel ID="Panel6" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                                <asp:GridView ID="gridNeftUTR" runat="server" AutoGenerateColumns="False"
                                    EmptyDataRowStyle-BackColor="pink"
                                    EmptyDataText="Record Not Found"
                                    CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                                    <columns>
                                        <asp:TemplateField>
                                            <itemtemplate><%#Container.DataItemIndex+1 %> </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Ref_Bill_No" SortExpression="Date" />
                                        <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                        <asp:BoundField DataField="Branch_Name" ControlStyle-BorderWidth="50px" HeaderText="Branch_Name" SortExpression="Date" />
                                        <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                        <asp:BoundField DataField="Payable_Amount" ControlStyle-BorderWidth="50px" HeaderText="Net Amount" SortExpression="Date" />
                                        <asp:BoundField DataField="BranchBill_BankUTRNo" ControlStyle-BorderWidth="50px" HeaderText="UTR No" SortExpression="Date" />
                                        <asp:BoundField DataField="BranchBillPaymentDate" ControlStyle-BorderWidth="50px" HeaderText="Payment Date" SortExpression="Date" />


                                    </columns>
                                    <emptydatarowstyle backcolor="Pink" />
                                </asp:GridView>
                            </asp:Panel>

                        </td>
                    </tr>

                </table>
            </table>
        </div>
    </div>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="../../JS/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GridView1]").table2excel({
                filename: "Rpt_Get_Payment_Credit_From_MPWLC_To_Godown_Wise.xls"
            });
        });
    </script>
</asp:Content>
