<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Prapatra_7.aspx.cs" Inherits="Reports_Rpt_Prapatra_7" %>


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
                    <td style="width: 150px; margin-left:10px;" align="left">
                       <asp:TextBox ID="txtpaymentdate" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 150px" align="right">
                        <asp:Label ID="Label9" runat="server" Text="Region:"></asp:Label>
                    </td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:DropDownList ID="ddlregion" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                   <td style="width: 100px" align="right">
                <asp:Button ID="Button1" Text="Show Details" runat="server" CssClass="button button2" OnClick="btnshow_Click" />
                    </td>
                </tr>

                <tr>
                    <td colspan="8" style="text-align: center;">
                        <asp:Button ID="btnshow" runat="server" Text="View Loss Gain" CssClass="button button2" OnClick="btnshow_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="8">

                        <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:BoundField DataField="Regionnm" HeaderText="District Name" />
                                <asp:BoundField DataField="Region_ID" HeaderText="Region_ID" />
                                <asp:BoundField DataField="DepotName" HeaderText="Brnach Name" />
                              
                                <asp:TemplateField HeaderText="MPWLC+HIRED+JVS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_HIRED_JVS1" runat="server" Text='<%# Eval("MPWLC_HIRED_JVS1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PVT PEG">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPVT_PEG1" runat="server" Text='<%# Eval("PVT_PEG1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MARKFED">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMARKFED1" runat="server" Text='<%# Eval("MARKFED1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="QMANDI BOARD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQMANDI_BOARD1" runat="server" Text='<%# Eval("QMANDI_BOARD1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="OILFED">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOILFED1" runat="server" Text='<%# Eval("OILFED1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CWC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCWC1" runat="server" Text='<%# Eval("CWC1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TOTAL(3 To 8)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal3to8" runat="server" Text='<%# Eval("Total3to8") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>



                                 <asp:TemplateField HeaderText="MPWLC+HIRED+JVS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_HIRED_JVS2" runat="server" Text='<%# Eval("MPWLC_HIRED_JVS2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PVT PEG">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPVT_PEG2" runat="server" Text='<%# Eval("PVT_PEG2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MARKFED">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMARKFED2" runat="server" Text='<%# Eval("MARKFED2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="QMANDI BOARD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQMANDI_BOARD2" runat="server" Text='<%# Eval("QMANDI_BOARD2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="OILFED">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOILFED2" runat="server" Text='<%# Eval("OILFED2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CWC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCWC2" runat="server" Text='<%# Eval("CWC2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TOTAL(3 To 8)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal10to15" runat="server" Text='<%# Eval("Total10to15") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="MPWLC+HIRED+JVS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPWLC_HIRED_JVS3" runat="server" Text='<%# Eval("MPWLC_HIRED_JVS3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PVT PEG">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPVT_PEG3" runat="server" Text='<%# Eval("PVT_PEG3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MARKFED">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMARKFED3" runat="server" Text='<%# Eval("MARKFED3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="QMANDI BOARD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQMANDI_BOARD3" runat="server" Text='<%# Eval("QMANDI_BOARD3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="OILFED">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOILFED3" runat="server" Text='<%# Eval("OILFED3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CWC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCWC3" runat="server" Text='<%# Eval("CWC3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TOTAL(3 To 8)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal17to22" runat="server" Text='<%# Eval("Total17to22") %>'></asp:Label>
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
