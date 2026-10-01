<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prapatra.aspx.cs" Inherits="StatePages_Prapatra" %>

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
                        <td style="text-align: right;">
                            <asp:Label ID="Label9" runat="server" Text="Region:"></asp:Label>
                        </td>
                        <td style="text-align: left;" class="auto-style1">
                            <asp:DropDownList ID="ddlregion" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label1" runat="server" Text="District : "></asp:Label>
                        </td>
                        <td style="text-align: left;" class="auto-style2">
                            <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label2" runat="server" Text="Branch:"></asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="false" CssClass="form-control">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                        <asp:Label ID="Label3" runat="server" Text="Depositer Name:"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddldepositer" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddldepositer_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">MPSCSC</asp:ListItem>
                            <asp:ListItem Value="2">Nafed</asp:ListItem>
                            <asp:ListItem Value="3">Markfed</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                </table>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">
                <tr>
                    <td style="padding-top: 20px; padding-bottom: 20px; background-color: skyblue; font-family: 'Times New Roman'; font-size: 20pt; color: white; text-align: center;">मध्यप्रदेश वेअरहाउससिंग ए ड लॉजिस्टिक्स कार्पोरेशन, क्षेत्रीय कार्यालय<br />
                        निगम की  स्वयं निर्मित/ JVS   एवं गोदाम में भंडारित धान की कुल क्षमता एवं भंडारित मात्रा की जानकारी दिनांक 26.11.2021 की स्थिति [M.T.] में
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
                                            <th style="width: 0px"></th>
                                            <th style="width: 0px"></th>
                                            <th style="width: 200px">गोदाम/शेड स्थल का नाम</th>
                                            <th colspan="9" style="text-align: center;">दिनांक 22.11.2021 की स्थिति में शेष धान की मात्रा एवं वास्तविक लगे स्टेक की संख्या </th>
                                            <th colspan="1" style="text-align: center;">कुल शेष मात्रा </th>
                                        </tr>
                                        <tr>
                                            <th style="width: 0px"></th>
                                            <th style="width: 0px"></th>
                                            <th style="width: 0px"></th>
                                            <th colspan="3" style="text-align: center;">वर्ष 2018-19</th>
                                            <th colspan="3" style="text-align: center;">वर्ष 2019-20</th>
                                            <th colspan="3" style="text-align: center;">वर्ष 2020-21</th>
                                        </tr>
                                        <tr>
                                            <th></th>
                                            <th style="text-align: center;">S.No(1)</th>
                                            <th style="text-align: center;">Godown Name(2)</th>

                                            <th style="text-align: center;">स्टेको की संख्या(3)</th>
                                            <th style="text-align: center;">बोरे (4) </th>
                                            <th style="text-align: center;">भंडारित मात्रा(5)</th>

                                            <th style="text-align: center;">स्टेको की संख्या(6)</th>
                                            <th style="text-align: center;">बोरे (7)</th>
                                            <th style="text-align: center;">भंडारित मात्रा(8)</th>

                                            <th style="text-align: center;">स्टेको की संख्या(9)</th>
                                            <th style="text-align: center;">बोरे (10) </th>
                                            <th style="text-align: center;">भंडारित मात्रा(11)</th>

                                            <th style="text-align: center;">कुल शेष मात्रा (5+8+11)</th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <td><%# Container.DataItemIndex + 1 %></td>
                                        <td><%# Eval("Godown_Name") %></td>
                                        <asp:HiddenField ID="hdngodownid" runat="server" Value='<%#Eval("Godown_ID") %>' />
                                            <td>
                                                <asp:TextBox class="form-control" runat="server" ID="txtSacks_201819" Text='<%# Eval("Number_Of_Sacks_201819")%>' onkeypress="return AllowNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" runat="server" ID="txtBags_201819" Text='<%# Eval("Number_of_Bags_201819")%>' onkeypress="return AllowNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" runat="server" ID="txtQuantity_201819" Text='<%# Eval("Stored_Quantity_201819")%>' onkeypress="return AllowNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" runat="server" ID="txtSacks_201920" Text='<%# Eval("Number_Of_Sacks_201920")%>' onkeypress="return AllowNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" runat="server" ID="txtBags_201920" Text='<%# Eval("Number_of_Bags_201920")%>' onkeypress="return AllowNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" runat="server" ID="txtQuantity_201920" Text='<%# Eval("Stored_Quantity_201920")%>' onkeypress="return AllowNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" runat="server" ID="txtSacks_202021" Text='<%# Eval("Number_Of_Sacks_202021")%>' onkeypress="return AllowNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" runat="server" ID="txtBags_202021" Text='<%# Eval("Number_of_Bags_202021")%>' onkeypress="return AllowNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" runat="server" ID="txtQuantity_202021" Text='<%# Eval("Stored_Quantity_202021")%>' onkeypress="return AllowNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" runat="server" ID="txttotalbalance" Text='<%# Eval("Total_Balance")%>' onkeypress="return AllowNumber(event)"></asp:TextBox>
                                            </td>

                                    </ItemTemplate>

                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <div style="text-align:center;">
                         <asp:Button runat="server" ID="BtnForCol2To6"  Text="Save" ValidationGroup="A" CssClass="btn btn-success" OnClick="BtnForCol2To6_Click"/>
                        </div>
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
