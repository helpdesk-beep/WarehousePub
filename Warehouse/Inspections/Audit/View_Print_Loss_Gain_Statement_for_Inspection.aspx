<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Account_Audit.master" AutoEventWireup="true" CodeFile="View_Print_Loss_Gain_Statement_for_Inspection.aspx.cs" Inherits="Inspections_Audit_View_Print_Loss_Gain_Statement_for_Inspection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

        table {
            border-spacing: 1em .5em;
            padding: 0 2em 1em 0;
            border: 1px solid orange;
        }

        td {
            width: 1.5em;
            height: 1.5em;
            padding: 6px;
            /*background: #d2d2d2;*/
            text-align: center;
            vertical-align: middle;
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
            window.location.reload(); // Refresh the parent page
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div>
            <div class="container py-4">
                <div class="card">

                    <div class="card-body">
                     <%-- <h2 style="text-align:center;">M.P. Warehousing and Logistics Corporation</h2>
                        <br />
                        <h4 style="text-align:center;"> LOSS / Gain Statement </h4>--%>
                        <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                        <%--&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                        &nbsp;&nbsp;&nbsp;&nbsp;--%>
                        <%--<asp:Button ID="btnUpdate" runat="server" CssClass="button button2" Text="Update Pending Months" Visible="false" OnClick="btnUpdate_Click" />--%>
                    </div>
                </div>

            </div>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central; padding-top: 10px;">
                <%--<tr>
                    <td style="padding: 6px;">
                        <asp:Label ID="Label3" runat="server" Text="Godown : "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGodown" runat="server" AutoPostBack="false" class="form-control">
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                <tr>

                    <td style="width: 175px;">
                        <asp:Label ID="Label1" runat="server" Text="Inspection From Date (DD/MM/YY): "></asp:Label>
                    </td>
                    <td style="width: 175px;">
                        <asp:Label ID="txtFDate" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label>
                    </td>
                    <td style="width: 175px;">
                        <asp:Label ID="Label2" runat="server" Text="Inspection To Date (DD/MM/YY): "></asp:Label>
                    </td>
                    <td style="width: 175px;">
                        <asp:Label ID="txtTDate" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label>
                    </td>

                </tr>
                <%--<tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Hired Type : "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlhiredType" runat="server" AutoPostBack="false" class="form-control">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Storage Type : "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlstoragetype" runat="server" AutoPostBack="false" class="form-control">
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                <%-- <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Commodity : "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlcommodity" runat="server" AutoPostBack="false" class="form-control">
                        </asp:DropDownList>
                    </td>
                </tr>--%>
               
                <tr>
                    <td style="text-align: left;" colspan="8" id="grddetails" runat="server" visible="false">

                        <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <%--<asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                                <asp:BoundField DataField="BranchID" HeaderText="BranchID" />--%>
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" />
                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" />

                                <asp:TemplateField HeaderText="Bags">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnGodown_ID" Value='<%# Eval("Godown_ID") %>' />
                                        <asp:HiddenField runat="server" ID="hdnCommodity_Id" Value='<%# Eval("Commodity_Id") %>' />
                                        <asp:Label ID="lblOpeningBags" runat="server" Text='<%# Eval("OpeningBags") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOpeningQty" runat="server" Text='<%# Eval("OpeningQty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Average">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOPAverage" runat="server" Text='<%# Eval("OPAverage") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bags">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecBags" runat="server" Text='<%# Eval("RecBags") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecQty" runat="server" Text='<%# Eval("RecQty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Average">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecAverage" runat="server" Text='<%# Eval("Average") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bgas">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOPRecBags" runat="server" Text='<%# Eval("OPRecBags") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOPRecQty" runat="server" Text='<%# Eval("OPRecQty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Average">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAverage" runat="server" Text='<%# Eval("OPRECAvg") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bags">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDelBags" runat="server" Text='<%# Eval("DelBags") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDelQty" runat="server" Text='<%# Eval("DelQty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Average">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDelAverage" runat="server" Text='<%# Eval("DelAverage") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDOW" runat="server" Text='<%# Eval("CalculateWeight") %>'></asp:Label>
                                        <%--<asp:Label ID="lblActualWeightDel" runat="server" Text='<%# Eval("CalculateWeight") %>'></asp:Label>--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="txtLoss" runat="server" Text='<%# Eval("Loss") %>'></asp:Label>
                                        <%-- <asp:Label ID="lblLoss" runat="server" Text='<%# Eval("Loss") %>'></asp:Label>--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGain" runat="server" Text='<%# Eval("Gain") %>'></asp:Label>
                                        <%-- <asp:Label ID="lblLoss" runat="server" Text='<%# Eval("Loss") %>'></asp:Label>--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Percentage(%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLossPercentage" runat="server" Text='<%# Eval("LossPercentage") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gain">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGain" runat="server" Text='<%# Eval("Gain") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Percentage(%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGainPercentage" runat="server" Text='<%# Eval("GainPercentage") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>--%>

                                <%--  <asp:TemplateField HeaderText="Bag">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBagBalance" runat="server" Text='<%# Eval("BalanceBags") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeightBalance" runat="server" Text='<%# Eval("BalanceWeight") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>--%>
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
    
</asp:Content>

