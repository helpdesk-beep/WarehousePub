<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Rpt_Get_RO_GRent_PassingOrder_Detail_From_Aug_Date_Wise.aspx.cs" Inherits="Region_Reports_Rpt_Get_RO_GRent_PassingOrder_Detail_From_Aug_Date_Wise" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
            Width: 150px;
            height: 40px;
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
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding-top: 20px; padding-bottom: 20px; background-color: skyblue; font-family: 'Times New Roman'; font-size: 20pt; color: white; text-align: center;" colspan="8">Passing Order Report For Income Tax Date Wise
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">Passing Order From Date:
     
         <asp:TextBox ID="txtdatefrom" runat="server" Height="25px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtdatefrom_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtdatefrom" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    &nbsp;&nbsp;&nbsp;
     Passing Order To Date:
         <asp:TextBox ID="txtdateto" runat="server" Height="25px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtdateto_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtdateto" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <div style="text-align: center;">
            <asp:Button ID="btnsubmit" runat="server" CssClass="button button2" Text="Show Details" OnClick="btnsubmit_Click" />
        </div>
        <div id="divshowdetails" runat="server" visible="false">
            <div class="container py-4">
                <div class="card">
                    <div class="card-body">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="Print" />
                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="ExportToExcel" />
                    </div>
                </div>

            </div>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central; overflow: auto;">
                <tr>
                    <td style="padding-top: 20px; padding-bottom: 20px; background-color: skyblue; font-family: 'Times New Roman'; font-size: 20pt; color: white; text-align: center;">Region WIse<br />
                        M.P. Warehousing & Logistics Corporarion<br />
                        Passing Order Details From Aug.
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                <asp:TemplateField HeaderText="Branch Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch_Name" runat="server" Text='<%# Eval("DepotName") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGodown_ID" runat="server" Text='<%# Eval("Godown_ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="JVS Bill No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJVS_Bill_No" runat="server" Text='<%# Eval("JVS_Bill_No") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SC Bill No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSC_Bill_No" runat="server" Text='<%# Eval("SC_Bill_No") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccount_No" runat="server" Text='<%# Eval("Account_No") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IFSC Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIFSC_Code" runat="server" Text='<%# Eval("IFSC_Code") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBill_Month" runat="server" Text='<%# Eval("Bill_Month") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCrop_Year" runat="server" Text='<%# Eval("Crop_Year") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Financial Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFinancial_Year" runat="server" Text='<%# Eval("Financial_Year") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commodity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("Commodity") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="JVS Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPer_Month_Rate" runat="server" Text='<%# Eval("Per_Month_Rate") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rent Bill AM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRent_Bill_AMT" runat="server" Text='<%# Eval("Rent_Bill_AMT") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TDS Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTDS_Amt" runat="server" Text='<%# Eval("TDS_Amt") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gain Detuction Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGain_Detuction_Amount" runat="server" Text='<%# Eval("Gain_Detuction_Amount") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Detuctio Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOther_Detuction_Amt" runat="server" Text='<%# Eval("Other_Detuction_Amt") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resource Deduction" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBM_Deduction" runat="server" Text='<%# Eval("BM_Deduction") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Deduction AMT" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal_Deduction_AMT" runat="server" Text='<%# Eval("Total_Deduction_AMT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pay To GO" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPayToGO" runat="server" Text='<%# Eval("PayToGO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PAY To MPWLC" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPAYtoMPWLC" runat="server" Text='<%# Eval("PAYtoMPWLC") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SC Bill AMT" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStorageCharBillAMt" runat="server" Text='<%# Eval("StorageCharBillAMt") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RO Approve Date" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRO_Approve_Date" runat="server" Text='<%# Eval("RO_Approve_Date") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MPWLC PAY To Godown" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLCPAYToGodown" runat="server" Text='<%# Eval("MPWLCPAYToGodown") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UTR No." ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranchBill_BankUTRNo" runat="server" Text='<%# Eval("BranchBill_BankUTRNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Date" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPayment_Date" runat="server" Text='<%# Eval("Payment_Date") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="Black" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JS/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GridView1]").table2excel({
                filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
            });
        });
    </script>
</asp:Content>
