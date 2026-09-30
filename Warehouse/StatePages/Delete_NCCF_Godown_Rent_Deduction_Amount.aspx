<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/Delete_NCCF_Godown_Rent_Deduction_Amount.aspx.cs" Inherits="StatePages_Delete_NCCF_Godown_Rent_Deduction_Amount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Modern responsive styling -->
    <style type="text/css">
        :root {
            --primary-color: #0bb6e6;
            --success-color: #28a745;
            --danger-color: #dc3545;
            --border-color: #dcdcdc;
            --text-dark: #333333;
        }

        .form-container {
            max-width: 1150px;
            margin: 20px auto;
            padding: 20px;
            background-color: #ffffff;
            border: 1px solid var(--border-color);
            border-radius: 8px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.08);
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
        }

        .page-header {
            background-color: var(--primary-color);
            color: #ffffff;
            padding: 12px 20px;
            border-radius: 6px;
            text-align: center;
            margin-bottom: 20px;
        }

        .filter-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
            gap: 20px;
            padding: 15px;
            background-color: #f9f9f9;
            border-radius: 6px;
            border: 1px solid var(--border-color);
            margin-bottom: 25px;
            align-items: flex-end;
        }

        .filter-group {
            display: flex;
            flex-direction: column;
            gap: 6px;
        }

            .filter-group label, .filter-group b {
                font-size: 13px;
                font-weight: 600;
                color: var(--text-dark);
            }

        .custom-select, .search-box {
            height: 36px;
            padding: 6px 12px;
            font-size: 14px;
            border: 1px solid #cccccc;
            border-radius: 4px;
            width: 100%;
            box-sizing: border-box;
            background-color: #ffffff;
        }

            .custom-select:focus, .search-box:focus {
                border-color: var(--primary-color);
                outline: none;
                box-shadow: 0 0 0 2px rgba(11, 182, 230, 0.2);
            }

        .btn-check-search {
            background: linear-gradient(135deg, #007bff, #0056b3);
            border: none;
            color: #fff !important;
            height: 36px;
            padding: 0 25px;
            font-size: 14px;
            font-weight: bold;
            border-radius: 4px;
            cursor: pointer;
            transition: 0.2s ease-in-out;
            width: 100%;
        }

            .btn-check-search:hover {
                background: linear-gradient(135deg, #0056b3, #00408a);
                box-shadow: 0px 4px 8px rgba(0,0,0,0.15);
            }

        .alert-message {
            padding: 10px;
            color: var(--danger-color);
            font-weight: bold;
            text-align: center;
            margin-bottom: 15px;
        }

        .data-panel {
            border: 1px solid var(--border-color);
            border-radius: 6px;
            overflow: hidden;
            margin-top: 20px;
        }

        .data-panel-header {
            background-color: var(--success-color);
            color: #ffffff;
            padding: 10px 15px;
            font-weight: bold;
            font-size: 14px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            flex-wrap: wrap;
            gap: 10px;
        }

        .grid-wrapper {
            max-height: 400px;
            overflow: auto;
            width: 100%;
        }

        .modern-grid {
            width: 100% !important;
            border-collapse: collapse !important;
            border: none !important;
        }

            .modern-grid th {
                background-color: #f2f2f2 !important;
                color: var(--text-dark) !important;
                padding: 12px 10px !important;
                font-weight: 600 !important;
                border-bottom: 2px solid var(--border-color) !important;
            }

            .modern-grid td {
                padding: 10px !important;
                border-bottom: 1px solid var(--border-color) !important;
            }

            .modern-grid tr:hover {
                background-color: #f5fafd !important;
            }

        .action-container {
            padding: 20px;
            text-align: center;
            background-color: #f9f9f9;
            border-top: 1px solid var(--border-color);
            display: flex;
            justify-content: center;
            gap: 15px;
        }

        .btn {
            height: 38px;
            padding: 0 24px;
            font-size: 14px;
            font-weight: 600;
            border-radius: 4px;
            cursor: pointer;
            border: 1px solid transparent;
            transition: all 0.2s ease-in-out;
        }

        .btn-delete {
            background-color: #ffffff;
            color: var(--danger-color);
            border-color: var(--danger-color);
        }

            .btn-delete:hover {
                background-color: var(--danger-color);
                color: #ffffff;
            }

        .btn-close {
            background-color: #ffffff;
            color: #6c757d;
            border-color: #6c757d;
        }

            .btn-close:hover {
                background-color: #6c757d;
                color: #ffffff;
            }

        .msg-label {
            display: block;
            margin-top: 10px;
            font-weight: 600;
            color: #0056b3;
            text-align: center;
        }

        .modal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            left: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            display: none;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67C5EF;
            width: 200px;
            height: 110px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>

    <!-- Scripts Bindings -->
    <script type="text/javascript" src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />

    <!-- SweetAlert2 Added Here -->
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script type="text/javascript">
        // Progress bar / Loader implementation
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                modal.show();
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

        function showLoader() {
            ShowProgress();
        }

        // Apply loader on forms postback bindings
        $(document).ready(function () {
            $('form').live("submit", function () {
                ShowProgress();
            });

            // Initialize Select2 dropdown elements cleanly
            $("[id*=ddlDistrict]").select2();
            $("[id*=ddlbranch]").select2();
            $("[id*=ddlGodown]").select2();
        });

        // Grid Management Checkboxes Counters
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            var grid = document.getElementById('<%= this.gvBOBillApp.ClientID %>');
            TotalChkBx = grid ? parseInt('<%= this.gvBOBillApp.Rows.Count %>') : 0;
            Counter = 0;
        }

        function ChildClick(CheckBox, HCheckBox) {
            var HeaderCheckBox = document.getElementById(HCheckBox);
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
        }

        function HeaderClick(CheckBox) {
            var TargetBaseControl = document.getElementById('<%= this.gvBOBillApp.ClientID %>');
            var TargetChildControl = "chk_Sum";
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            Counter = CheckBox.checked ? TotalChkBx : 0;
            calculate();
        }

        function calculate() {
            var txtTotalcharges = 0;
            var CheckCount = 0;
            var grid = document.getElementById("<%= gvBOBillApp.ClientID%>");
            if (!grid) return;
            for (var i = 0; i < grid.rows.length - 1; i++) {
                var txtcharges = $("input[id*=txtcharges]");
                var checkBoxes = $("input[id*=chk_Sum]");
                if (checkBoxes[i] && checkBoxes[i].checked == true) {
                    if (txtcharges[i] && txtcharges[i].value) {
                        txtTotalcharges = txtTotalcharges + parseFloat(txtcharges[i].value);
                    }
                    CheckCount = CheckCount + 1;
                }
            }
        }

        // Client-side quick filter logic
        function filterGrid() {
            var input = document.getElementById('<%= txtSearch.ClientID %>');
            var filter = input.value.toLowerCase();
            var table = document.getElementById('<%= gvBOBillApp.ClientID %>');
            if (!table) return;
            var trs = table.getElementsByTagName("tr");

            document.getElementById('<%= hdnSearchValue.ClientID %>').value = input.value;

            for (var i = 1; i < trs.length; i++) {
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

        function fnChkEmptyData() {
            var searchTxt = document.getElementById('<%= txtSearch.ClientID %>').value;
            if (searchTxt.trim() == "") {
                showAlert("Godown Id / Bill Detail is required.", "warning");
                document.getElementById('<%= txtSearch.ClientID %>').focus();
                return false;
            }
            return true;
        }

        function PrintDiv_Actual() {
            var divContents = document.getElementById("printActualBill").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            printWindow.document.write('<html><head><title>Print</title></head><body>');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }

        // SweetAlert Helper Function
        function showAlert(msg, type) {
            Swal.fire({
                icon: type,        // success | error | warning | info
                title: 'Notification',
                text: msg,
                confirmButtonText: 'OK'
            });
        }

        // SweetAlert Confirmation Logic for Delete File Button
        var isConfirmed = false;
        function confirmDelete(btn) {
            if (isConfirmed) {
                isConfirmed = false; // reset flag
                return true;
            }

            // Check if any checkbox is selected before showing confirmation
            var hasSelection = false;
            $("input[id*=chk_Sum]").each(function () {
                if ($(this).is(':checked')) {
                    hasSelection = true;
                    return false; // break loop
                }
            });

            if (!hasSelection) {
                showAlert("Please select at least one record to delete.", "warning");
                return false;
            }

            Swal.fire({
                title: 'Are you sure?',
                text: "Do you want to Delete Deduction?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#dc3545',
                cancelButtonColor: '#6c757d',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    isConfirmed = true;
                    showLoader(); // Run loader screen
                    btn.click();  // Trigger postback dynamically
                }
            });
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-container">

        <!-- Loading Progress Bar Div Placeholder -->
        <div class="loading" align="center">
            <br />
            <img src="../Images/loader.gif" alt="Loading..." /><br />
            Please wait...
        </div>

        <!-- Header View Section -->
        <div class="page-header">
            <asp:Label ID="lblDepositDetail" runat="server" Text="View NCCF Godown Rent Deduction Amount Detail" Font-Size="17px" Font-Bold="true"></asp:Label>
        </div>

        <!-- System Alerts Box -->
        <asp:Panel ID="pnlAlert" runat="server">
            <div class="alert-message">
                <asp:Label ID="lblMsg" runat="server" Visible="False"></asp:Label>
            </div>
        </asp:Panel>

        <!-- Dynamic Responsive Dropdown & Search Filter Area -->
        <div class="filter-grid">
            <div class="filter-group">
                <asp:Label ID="Label2" runat="server" Text="District Name"></asp:Label>
                <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="custom-select">
                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="filter-group">
                <asp:Label ID="Label1" runat="server" Text="Branch Name"></asp:Label>
                <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true" CssClass="custom-select" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="filter-group">
                <asp:Label ID="Label3" runat="server" Text="Godown Name"></asp:Label>
                <asp:DropDownList ID="ddlGodown" runat="server" AutoPostBack="true" CssClass="custom-select" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>
        <div class="filter-grid">
            <div class="filter-group">
                <b>Bill Details / Search:</b>
                <asp:TextBox runat="server" CssClass="search-box" placeholder="Search by Bill Details..." onkeyup="filterGrid()" ID="txtSearch"></asp:TextBox>
            </div>
            <div class="filter-group">
                <asp:Button runat="server" CssClass="btn-check-search" Text="Check" ID="btnCheck"
                    OnClientClick="return fnChkEmptyData();" OnClick="btnCheck_Click" />
                <asp:HiddenField ID="hdnSearchValue" runat="server" />
            </div>
        </div>
        <!-- Dynamic GridView Results Block container wrapper -->
        <div id="trnewproc" runat="server" visible="false" class="data-panel">

            <div class="data-panel-header">
                <div>
                    <span>Total Record: </span>
                    <asp:Label ID="lblNoofAC" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <span>NCCF Godown Rent Deduction Amount Detail </span>
                    <asp:Label ID="lblcropyr" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="grid-wrapper" id="toexportDist" runat="server">
                <asp:GridView ID="gvBOBillApp" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="Ref_Bill_No" AllowPaging="False" Width="100%"
                    CssClass="modern-grid" TabIndex="4" CellPadding="4">
                    <Columns>
                        <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" SortExpression="Godown_ID">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" SortExpression="Godown_Name">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Ref_Bill_No" HeaderText="Ref Bill No" SortExpression="Ref_Bill_No">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Bill_No" HeaderText="Bill No" SortExpression="Bill_No">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TResources_Deduct_Amt" HeaderText="Resources Deduct Amt" SortExpression="TResources_Deduct_Amt">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TBill_Amount" HeaderText="Bill Amount" SortExpression="TBill_Amount">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Select">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkBxHeader" Text="All" onclick="javascript:HeaderClick(this);" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Sum" runat="server" onclick="javascript:ChildClick(this, 'chkBxHeader');" />
                                <asp:HiddenField ID="hdnRef_Bill_No" runat="server" Value='<%# Eval("Ref_Bill_No") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                            <ControlStyle Width="15px" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="center" />
                </asp:GridView>
            </div>

            <!-- Dynamic Submittals Actions Buttons Tray -->
            <div id="tblbtn" runat="server" visible="false" class="action-container">
                <asp:Button ID="btnDelete" runat="server" Text="Delete File" class="btn btn-delete"
                    Visible="false" TabIndex="13" ValidationGroup="SaveValid" OnClick="btnDelete_Click"
                    OnClientClick="return confirmDelete(this);" />

                <asp:Button ID="btn_Close" runat="server" Text="Cancel"
                    class="btn btn-close" CausesValidation="false" OnClick="btn_Close_Click" />
            </div>

            <div style="padding: 10px; text-align: center;">
                <asp:Label ID="lblRespMsg" runat="server" CssClass="msg-label"></asp:Label>
                <asp:Label ID="lblRespMsgNo" runat="server" CssClass="msg-label"></asp:Label>
            </div>
        </div>

        <!-- Retained baseline hidden tracking labels -->
        <asp:Label ID="Label8" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Label24" runat="server" Visible="false"></asp:Label>
    </div>
</asp:Content>
