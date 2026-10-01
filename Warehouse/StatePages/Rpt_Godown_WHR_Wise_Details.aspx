<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Godown_WHR_Wise_Details.aspx.cs"
    Inherits="StatePages_Rpt_Godown_WHR_Wise_Details" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>State Level Report</title>

    <!-- CSS FILES -->
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../../assets/New/css/bootstrap-multiselect.css" rel="stylesheet" />

    <!-- JS FILES -->
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="../assets/New/js/select2.min.js"></script>
    <script src="../../assets/New/js/bootstrap-multiselect.js"></script>

    <!-- INITIALIZATION -->
    <script>
        $(function () {
            $("#ddlCropYear").select2();
            $("#ddldepositor").select2();

            $('[id*=Godownchk]').multiselect({
                includeSelectAllOption: true
            });
        });

        function printGrid() {
            var grid = document.getElementById('<%= GrdGodown.ClientID %>');
            var win = window.open('', '', 'width=900,height=700');
            win.document.write('<html><head></head><body>');
            win.document.write(grid.outerHTML);
            win.document.close();
            win.print();
            win.close();
        }
    </script>
      <!-- GRID SEARCH -->
<script type="text/javascript">
    function filterGrid() {

        var input = document.getElementById("<%= txtSearch.ClientID %>");
        var filter = input.value.toLowerCase();

        var table = document.getElementById("<%= GrdGodown.ClientID %>");
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

    <!-- CUSTOM GRID CSS -->
    <style>
        .grid-container {
            width: 100%;
            border: 1px solid #ccc;
            padding: 5px;
        }

        /* Header Color */
        .grid-header th {
            background-color: #336699 !important;
            color: white !important;
            text-align: center !important;
            font-size: 14px !important;
            font-weight: bold !important;
        }

        /* Footer – Single Total Row */
        .grid-footer td {
            background-color: #336699 !important;
            color: white !important;
            font-weight: bold !important;
            text-align: right !important;
        }

        #container {
            display: flex;
            justify-content: space-between;
            background-color: lightyellow;
            padding: 10px;
        }

        #container div {
            height: 130px;
        }
    </style>

</head>

<body>
<form id="form1" runat="server">

    <!-- PAGE HEADER -->
    <div id="container">
        <div>
            <img src="../../images/mpwlc.png" width="75px" />
        </div>

        <div style="text-align:center;">
            <span style="font-size: 32px; font-weight:bold;">
                M.P. WAREHOUSING & LOGISTICS CORPORATION
            </span><br />
            <span style="font-size:24px; font-weight:bold;">
                Godown WHR Wise Details in Qtl.
            </span>
        </div>

        <div>
            <span>Date: </span><asp:Label ID="labelName" runat="server"></asp:Label>
            <br /><br /><br />
            <span style="font-size:18px; font-weight:bold;">Qty In Qtl.</span>
        </div>
    </div>

    <br />

    <!-- FILTER SECTION -->
    <div class="container">
        <div class="row">

            <div class="col-md-3">
                <label>Crop Year</label>
                <asp:DropDownList ID="ddlCropYear" runat="server" CssClass="form-control"
                    Width="100%" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div class="col-md-3">
                <label>Depositor</label>
                <asp:DropDownList ID="ddldepositor" runat="server" CssClass="form-control"
                    Width="100%" AutoPostBack="true"
                    OnSelectedIndexChanged="ddldepositor_SelectedIndexChanged"></asp:DropDownList>
            </div>

        </div>
    </div>

    <br />
    <!-- ================= SEARCH ================= -->
<div class="search-box1 container-1 mt-3">
    <div class="row">
        <div class="col-md-4" style="padding-bottom:10px;">
            <%--<asp:TextBox ID="txtSearch" runat="server" 
                CssClass="form-control shadow-sm"
                placeholder="Search here..."
                onkeyup="filterGrid();" Visible="false" />--%>
            <asp:TextBox ID="txtSearch" runat="server"
    placeholder="Search here..."
    Style="width: 380px; height: 36px;margin-left: 88px; padding: 0 15px; font-size: 15px; border: 1px solid #000; border-radius: 8px; outline: none; transition: all 0.25s ease; box-shadow: 0 2px 6px rgba(0,0,0,0.08);"
    onkeyup="filterGrid();"/>
        </div>
    </div>
</div>
    <!-- GRIDVIEW SECTION -->
    <div class="grid-container">
        <div class="table-responsive" style="height:670px;">

            <asp:GridView ID="GrdGodown" runat="server"
                CssClass="table table-bordered table-hover"
                AutoGenerateColumns="false"
                ShowFooter="true"
                Font-Size="10pt"
                HeaderStyle-CssClass="grid-header"
                FooterStyle-CssClass="grid-footer">

                <Columns>

                    <asp:BoundField DataField="SNO" HeaderText="S.No." />

                    <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="WHR No." />

                    <asp:BoundField DataField="WHRDATE" HeaderText="WHR Date" />

                    <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor" />

                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />

                    <asp:BoundField DataField="CropYear" HeaderText="CropYear" />

                    <asp:BoundField DataField="TotalBags_Received" HeaderText="Total Bags Received"
                        ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="Total_Qty_Received" HeaderText="Total Qty. Received"
                        ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="No_Of_Bags" HeaderText="Bags Delivered"
                        ItemStyle-HorizontalAlign="Right" />

                    <asp:TemplateField HeaderText="Weight Delivered">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnk" runat="server" Target="_blank"
                                NavigateUrl='<%# "~/StatePages/Rpt_WHRwiseGetpassDetail.aspx?WHRNo=" + Eval("Depositor_WHR_Id") %>'
                                Text='<%# Eval("Bags_Weight") %>' ForeColor="Blue"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="AvailableBags" HeaderText="Available Bags"
                        ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="AvailableQty" HeaderText="Available Qty."
                        ItemStyle-HorizontalAlign="Right" />

                </Columns>

            </asp:GridView>

        </div>
    </div>

    <br />

    <asp:Label ID="lblMsg" runat="server" BackColor="Red"
        ForeColor="White" Font-Size="Large"></asp:Label>

</form>
</body>
</html>
