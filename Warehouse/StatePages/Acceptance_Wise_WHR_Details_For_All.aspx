<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/StatePages/Acceptance_Wise_WHR_Details_For_All.aspx.cs" Inherits="StatePages_Acceptance_Wise_WHR_Details_For_All" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />

    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="../assets/New/js/select2.min.js"></script>

    <script>
        $(function () {
            $("[id*=ddldivision]").select2();
            $("[id*=ddldistrict]").select2();
            $("[id*=ddlbranch]").select2();
            $("[id*=ddlcropyear]").select2();
            $("[id*=ddlcommodity]").select2();
        });
    </script>

    <!-- GRID SEARCH -->
    <script type="text/javascript">
        function filterGrid() {

            var input = document.getElementById("<%= txtSearch.ClientID %>");
            var filter = input.value.toLowerCase();

            var table = document.getElementById("<%= GridView1.ClientID %>");
            var trs = table.getElementsByTagName("tr");

            for (var i = 1; i < trs.length; i++) { // skip header row
                var display = false;
                var tds = trs[i].getElementsByTagName("td");

                for (var j = 0; j < tds.length; j++) {
                    var cell = tds[j];
                    if (cell && cell.textContent.toLowerCase().indexOf(filter) > -1) {
                        display = true;
                        break;
                    }
                }

                trs[i].style.display = display ? "" : "none";
            }
        }
    </script>
    <%--Search End--%>
    <!-- PRINT -->
    <script>
        function printGrid() {
            var gridData = document.getElementById('<%= GridView1.ClientID %>');
            var prtWindow = window.open('', 'print', 'width=1000,height=1000');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
        }
    </script>



    <!-- ✅ LOADER STYLE -->
    <style>
        #loader {
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background: rgba(255,255,255,0.9);
            z-index: 9999;
            display: none;
            align-items: center;
            justify-content: center;
            font-size: 22px;
            font-weight: bold;
            color: #008CBA;
        }
    </style>

    <script>
        function showLoader() {
            document.getElementById("loader").style.display = "flex";
        }
        function hideLoader() {
            document.getElementById("loader").style.display = "none";
        }
    </script>

    <!-- BUTTON STYLE -->
    <style>
        .button {
            background-color: #4CAF50;
            border: none;
            color: white;
            font-size: 12px;
            font-weight: bold;
            cursor: pointer;
            width: 110px;
            height: 28px;
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

        .filter-box {
            border: 5px solid #e3e3e8;
            width: 80%;
            margin: 30px auto;
            padding: 20px;
            background: #fafafa;
        }

        .row-flex {
            display: flex;
            gap: 15px;
            align-items: center;
            margin-bottom: 15px;
        }

        .col-label {
            width: 20%;
        }

        .col-input {
            width: 30%;
        }

        .search-box {
            border: 5px solid #e3e3e8;
            padding: 10px;
            margin-bottom: 10px;
        }

        .auto-style1 {
            margin-left: 300px;
            margin-right: 20px;
            margin-top: 20px;
            margin-bottom: 20px;
        }
    </style>
    <style>
        /* ✅ Overall Page Font 1px Increase */
        body {
            font-size: 14px; /* pehle 13px jaisa lag raha hai */
        }

        /* ✅ Grid Font Size Proper */
        .Grid, .Grid th, .Grid td {
            font-size: 14px !important;
        }

        /* ✅ Header Title Font Increase */
        .auto-style1,
        .filter-box,
        .search-box {
            font-size: 15px;
        }

        /* ✅ Dropdown, Textbox Font */
        select, input[type=text] {
            font-size: 14px !important;
        }

        /* ✅ Button Font 1px Increase */
        .button {
            font-size: 13px;
            border-radius: 8px;
        }

        /* ✅ Grid Header Bold & Slight Bigger */
        .Grid th {
            font-size: 15px !important;
            font-weight: bold;
        }
    </style>
    <style>
        .table th {
            font-size: 15px;
            font-weight: 700;
            white-space: nowrap;
        }

        .table td {
            font-size: 14px;
            white-space: nowrap;
        }

        .search-box {
            background: #f8f9fa;
            padding: 12px;
            border-radius: 8px;
            box-shadow: 0px 0px 6px rgba(0,0,0,0.1);
        }
    </style>
    <style>
        .table-responsive thead th {
            position: sticky;
            top: 0;
            z-index: 5;
            background: #212529;
            color: #fff;
        }
    </style>
    <style>
        .table-responsive {
            position: relative;
        }

            .table-responsive thead th {
                position: sticky;
                top: 0;
                z-index: 10;
                background-color: #212529; /* Bootstrap dark */
                color: #fff;
            }
    </style>




</head>

<body>
    <!-- ✅ LOADER DIV -->
    <div id="loader">Processing... Please wait</div>
    <form id="form1" runat="server">

        <!-- ================= HEADER ================= -->
        <div style="display: flex; justify-content: space-between; align-items: center; background: lightyellow; padding: 10px;">
            <div>
                <img src="../images/mpwlc.png" width="75" />
            </div>

            <div style="text-align: center;">
                <span style="font-size: 39px; font-weight: bold;">M.P. WAREHOUSING & LOGISTICS CORPORATION</span><br />
                <span style="font-size: 30px; font-weight: bold;">Acceptance Wise WHR Details</span>
            </div>

            <div style="text-align: right;">
                Date:
                <asp:Label ID="labelName" runat="server"></asp:Label><br />
                <br />
                <b>Qty In Qtl.</b>
            </div>
        </div>

        <!-- ✅ EXPORT BUTTONS -->
        <div class="auto-style1">
            <asp:Button ID="Button2" runat="server" Text="ExportToPDF"
                CssClass="button button2"
                OnClientClick="return printGrid();" />

            <input type="button" id="btnExport"
                value="Exporttoexcel"
                class="button button2" />
        </div>

        <!-- ================= FILTER SECTION ================= -->
        <div class="filter-box">

            <div class="row-flex">
                <div class="col-label">Division</div>
                <div class="col-input">
                    <asp:DropDownList ID="ddldivision" runat="server" CssClass="form-control" AutoPostBack="true"
                        OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                        <asp:ListItem Value="0">All</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-label">District</div>
                <div class="col-input">
                    <asp:DropDownList ID="ddldistrict" runat="server" CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                        <asp:ListItem Value="0">All</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row-flex">
                <div class="col-label">Branch</div>
                <div class="col-input">
                    <asp:DropDownList ID="ddlbranch" runat="server" CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                        <asp:ListItem Value="0">All</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-label">Session</div>
                <div class="col-input">
                    <asp:DropDownList ID="ddlcropyear" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="Rabi202627">Rabi 2026-27</asp:ListItem>
                        <asp:ListItem Value="Kharif202526">Kharif 2025-26</asp:ListItem>
                        <asp:ListItem Value="Rabi202526">Rabi 2025-26</asp:ListItem>
                        <asp:ListItem Value="Rabi202425">Rabi 2024-25</asp:ListItem>
                        <asp:ListItem Value="Kharif202425">Kharif 2024-25</asp:ListItem>
                        <asp:ListItem Value="RabiANB202627">ANB 2026-27</asp:ListItem>
                        <asp:ListItem Value="RabiANB202526">ANB 2025-26 Tuar</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row-flex">
                <div class="col-label">Commodity</div>
                <div class="col-input">
                    <asp:DropDownList ID="ddlcommodity" runat="server" CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="22">Wheat-PSS</asp:ListItem>
                        <asp:ListItem Value="63">GRAM</asp:ListItem>
                        <asp:ListItem Value="64">LENTIL</asp:ListItem>
                        <asp:ListItem Value="33">Mustard-Sarason</asp:ListItem>
                        <asp:ListItem Value="92">Moong</asp:ListItem>
                        <asp:ListItem Value="27">Urad</asp:ListItem>
                        <asp:ListItem Value="26">Soya-Beans</asp:ListItem>
                        <asp:ListItem Value="13">Paddy-Common</asp:ListItem>
                        <asp:ListItem Value="120">Chana Dal</asp:ListItem>
                        <asp:ListItem Value="129">Fortified_Rice</asp:ListItem>
                        <asp:ListItem Value="134">KODO</asp:ListItem>
                        <asp:ListItem Value="135">Kutki</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <!-- ================= SEARCH ================= -->
        <div class="search-box1 container-1 mt-3">
            <div class="row">
                <div class="col-md-4" style="padding-bottom: 10px;">
                    <%--<asp:TextBox ID="txtSearch" runat="server" 
                CssClass="form-control shadow-sm"
                placeholder="Search here..."
                onkeyup="filterGrid();" Visible="false" />--%>
                    <asp:TextBox ID="txtSearch" runat="server"
                        placeholder="Search here..."
                        Style="width: 380px; height: 36px; margin-left: 88px; padding: 0 15px; font-size: 15px; border: 1px solid #000; border-radius: 8px; outline: none; transition: all 0.25s ease; box-shadow: 0 2px 6px rgba(0,0,0,0.08);"
                        onkeyup="filterGrid();" Visible="false" />
                </div>
            </div>
        </div>


        <!-- ================= GRID WITH SCROLLER ================= -->
        <div class="container-fluid mt-3">
            <div class="table-responsive" style="max-height: 480px; overflow-y: auto; overflow-x: auto;">

                <asp:GridView runat="server" ID="GridView1"
                    AutoGenerateColumns="false"
                    ShowFooter="true"
                    CssClass="table table-bordered table-striped table-hover text-center align-middle"
                    HeaderStyle-CssClass="table-dark"
                    OnRowDataBound="GridView1_RowDataBound"
                    OnRowCreated="GridView1_RowCreated"
                    OnDataBound="OnDataBound">

                    <Columns>

                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                        <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />

                        <asp:TemplateField HeaderText="Acceptance No">
                            <ItemTemplate>
                                <asp:Label ID="lblAN" runat="server" Text='<%# Eval("Acceptance_No") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="WHR Id">
                            <ItemTemplate>
                                <asp:Label ID="lblWHR_Id" runat="server" Text='<%# Eval("WHR_Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acceptance Date">
                            <ItemTemplate>
                                <asp:Label ID="lblAD" runat="server" Text='<%# Eval("Acceptance_Date") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="WHR Issue Date">
                            <ItemTemplate>
                                <asp:Label ID="lblWID" runat="server" Text='<%# Eval("WHR_Issue_Date") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acceptance Bags">
                            <ItemTemplate>
                                <asp:Label ID="lblAB" runat="server" Text='<%# Eval("No_of_Bags") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="WHR Bags">
                            <ItemTemplate>
                                <asp:Label ID="lblWB" runat="server" Text='<%# Eval("TotalBags_Received") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acceptance Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("Rec_Qty") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="WHR Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblWQ" runat="server" Text='<%# Eval("Total_Qty_Received") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>
        </div>


        <br />
        <asp:Label ID="lblMsg" runat="server" BackColor="Red" Font-Size="Large"></asp:Label>

        <!-- ================= EXPORT ================= -->
        <%--<script src="../../JS/table2excel.js"></script>
<script>
    $("#btnExport").click(function () {
        $("[id*=GridView1]").table2excel({
            filename: "State_Report.xls"
        });
    }); 
</script>--%>
        <!-- ✅ EXCEL EXPORT SCRIPT WITH LOADER -->
        <script src="../../JS/table2excel.js"></script>

        <script>
            $("#btnExport").click(function () {

                showLoader();

                var table = $("[id*=GridView1]");
                if (table.length == 0) {
                    alert("No data found in Grid.");
                    hideLoader();
                    return;
                }

                table.table2excel({
                    filename: "State_Report.xls"
                });

                setTimeout(function () {
                    hideLoader();
                }, 800);
            });
        </script>

    </form>
</body>
</html>
