<%@ Page Title="Mapping For Moisture / Fumigation" Language="C#"
    MasterPageFile="~/MasterPage/Gdwn.master"
    AutoEventWireup="true"
    CodeFile="~/BranchPages/MappingForMostureFumigation.aspx.cs"
    Inherits="BranchPages_MappingForMostureFumigation"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />

    <style>
        .select2-container--default .select2-selection--single {
            height: 35px;
            border: 1px solid #ced4da;
            border-radius: 5px;
        }

        fieldset {
            border: 1px solid #007bff;
            border-radius: 8px;
            padding: 10px;
        }

        legend {
            font-weight: bold;
            color: #007bff;
        }

        .disabled-field {
            background-color: #e9ecef !important;
            opacity: 0.8;
            cursor: not-allowed;
        }

        /* Search box styles */
        .search-container {
            margin-bottom: 15px;
            float: right;
        }

            .search-container input {
                padding: 8px 12px;
                border: 1px solid #ddd;
                border-radius: 4px;
                width: 500px;
                font-size: 14px;
            }

                .search-container input:focus {
                    outline: none;
                    border-color: #007bff;
                    box-shadow: 0 0 5px rgba(0,123,255,0.3);
                }

            .search-container i {
                position: relative;
                left: -25px;
                color: #888;
                cursor: pointer;
            }

        .clear-search {
            position: relative;
            left: -30px;
            color: #888;
            cursor: pointer;
            font-size: 14px;
        }

            .clear-search:hover {
                color: #dc3545;
            }

        .no-records {
            text-align: center;
            padding: 20px;
            color: #666;
            font-style: italic;
        }

        .table-responsive {
            overflow-x: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper col-md-12">
        <fieldset>
            <legend>Mapping For Moisture / Fumigation</legend>

            <asp:HiddenField ID="hfID" runat="server" />
            <asp:HiddenField ID="hfEmployeeValue" runat="server" />
            <asp:HiddenField ID="hfGodownValue" runat="server" />
            <asp:HiddenField ID="hfEditMode" runat="server" Value="false" />

            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblName" runat="server" Text="Employee:" Font-Bold="true"></asp:Label>
                    <asp:DropDownList
                        ID="ddlEmployee"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <asp:Label ID="lblMobile" runat="server" Text="Mobile No:" Font-Bold="true"></asp:Label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <asp:Label ID="lblDesignation" runat="server" Text="Designation:" Font-Bold="true"></asp:Label>
                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblType" runat="server" Text="Type:" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Moisture" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Fumigation" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Both" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-3">
                    <asp:Label ID="lblFinancial_Year" runat="server" Text="Financial Year:" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="ddlFinancial_Year" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancial_Year_SelectedIndexChanged">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="2024-25" Value="2024-25"></asp:ListItem>
                        <asp:ListItem Text="2025-26" Value="2025-26"></asp:ListItem>
                        <asp:ListItem Text="2026-27" Value="2026-27"></asp:ListItem>
                        <asp:ListItem Text="2027-28" Value="2027-28"></asp:ListItem>
                        <asp:ListItem Text="2028-29" Value="2028-29"></asp:ListItem>
                        <asp:ListItem Text="2029-30" Value="2029-30"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <asp:Label ID="lblMonth" runat="server" Text="Financial Month:" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                        <asp:ListItem Text="--Select Month--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="January" Value="January"></asp:ListItem>
                        <asp:ListItem Text="February" Value="February"></asp:ListItem>
                        <asp:ListItem Text="March" Value="March"></asp:ListItem>
                        <asp:ListItem Text="April" Value="April"></asp:ListItem>
                        <asp:ListItem Text="May" Value="May"></asp:ListItem>
                        <asp:ListItem Text="June" Value="June"></asp:ListItem>
                        <asp:ListItem Text="July" Value="July"></asp:ListItem>
                        <asp:ListItem Text="August" Value="August"></asp:ListItem>
                        <asp:ListItem Text="September" Value="September"></asp:ListItem>
                        <asp:ListItem Text="October" Value="October"></asp:ListItem>
                        <asp:ListItem Text="November" Value="November"></asp:ListItem>
                        <asp:ListItem Text="December" Value="December"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <asp:Label ID="lblGodown" runat="server" Text="Godown:" Font-Bold="true"></asp:Label>
                    <asp:DropDownList CssClass="form-control select2" ID="ddlGodown" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row" style="margin-top: 15px">
                <div class="col-md-2">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" OnClientClick="return fnChkEmpty();" />
                </div>
                <%--<div class="col-md-2">
                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdate_Click" Visible="false" OnClientClick="return fnChkEmpty();" />
                </div>--%>
                <div class="col-md-2">
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="btnCancel_Click" Visible="false" />
                </div>
            </div>
        </fieldset>

        <fieldset style="margin-top: 20px;">
            <legend>Existing Records
                <div class="search-container">
                    <input type="text" id="txtSearch" placeholder="Search by Employee, Mobile, Godown, Type..." onkeyup="searchGridView()" />
                    <span class="clear-search" onclick="clearSearch()" style="display: none;">✕</span>
                </div>
                <div style="clear: both"></div>
            </legend>

            <div class="table-responsive">
                <asp:GridView ID="gvMapping" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-hover"
                    OnRowCommand="gvMapping_RowCommand"
                    ClientIDMode="Static">

                    <HeaderStyle BackColor="#007bff" ForeColor="White" Font-Bold="True" />

                    <RowStyle Wrap="False" />
                    <AlternatingRowStyle BackColor="#f8f9fa" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                <asp:HiddenField ID="HDNBranch_ID" runat="server" Value='<%# Eval("BranchId") %>' />
                                <asp:HiddenField ID="HDNEmployeeId" runat="server" Value='<%# Eval("EmployeeId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="FumigationName" HeaderText="Employee" ItemStyle-Width="15%">
                            <ItemStyle CssClass="searchable" />
                        </asp:BoundField>

                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" ItemStyle-Width="12%">
                            <ItemStyle CssClass="searchable" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Designation" HeaderText="Designation" ItemStyle-Width="12%">
                            <ItemStyle CssClass="searchable" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown" ItemStyle-Width="15%">
                            <ItemStyle CssClass="searchable" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-Width="10%">
                            <ItemStyle CssClass="searchable" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" ItemStyle-Width="10%">
                            <ItemStyle CssClass="searchable" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Month" HeaderText="Month" ItemStyle-Width="10%">
                            <ItemStyle CssClass="searchable" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Width="10%"></asp:BoundField>

                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="11%">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="btnEdit" runat="server"
                                    CommandName="EditRecord"
                                    CommandArgument='<%# Eval("FumigationId") %>'
                                    CssClass="btn btn-sm btn-info">
                                    <i class="fa fa-edit"></i> Edit
                                </asp:LinkButton>--%>
                                &nbsp;
                                <asp:LinkButton ID="btnDelete" runat="server"
                                    CommandName="DeleteRecord"
                                    CommandArgument='<%# Eval("FumigationId") %>'
                                    CssClass="btn btn-sm btn-danger"
                                    OnClientClick="return confirm('Are you sure you want to delete this record?');">
                                    <i class="fa fa-trash"></i> Delete
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>

    <script type="text/javascript">
        $(".select2").select2({
            placeholder: "--Select Godown--",
            allowClear: true
        });

        function fnChkEmpty() {
            var employee = $('#<%= ddlEmployee.ClientID %>').val();
            var mobile = $('#<%= txtMobile.ClientID %>').val().trim();
            var designation = $('#<%= txtDesignation.ClientID %>').val().trim();
            var godown = $('#<%= ddlGodown.ClientID %>').val();
            var type = $('#<%= ddlType.ClientID %>').val();
            var year = $('#<%= ddlFinancial_Year.ClientID %>').val();
            var month = $('#<%= ddlMonth.ClientID %>').val();

            if (employee == "0" || employee == "" || employee == null || employee == "--Select--") {
                alert("Please select Employee");
                return false;
            }
            if (mobile == "") {
                alert("Mobile No is required");
                return false;
            }
            if (designation == "") {
                alert("Designation is required");
                return false;
            }
            if (godown == "0" || godown == "" || godown == null || godown == "--Select--" || godown == "Select") {
                alert("Please select Godown");
                return false;
            }
            if (type == "0") {
                alert("Please select Type");
                return false;
            }
            if (year == "0") {
                alert("Please select Financial Year");
                return false;
            }
            if (month == "0") {
                alert("Please select Month");
                return false;
            }
            return true;
        }

        function enableDisableFields(isEditMode) {
            if (isEditMode) {
                $('#<%= ddlFinancial_Year.ClientID %>').prop('disabled', true);
                $('#<%= ddlMonth.ClientID %>').prop('disabled', true);
                $('#<%= ddlGodown.ClientID %>').prop('disabled', true);

                $('#<%= ddlFinancial_Year.ClientID %>').addClass('disabled-field');
                $('#<%= ddlMonth.ClientID %>').addClass('disabled-field');
                $('#<%= ddlGodown.ClientID %>').addClass('disabled-field');
            } else {
                $('#<%= ddlType.ClientID %>').prop('disabled', false);
                $('#<%= ddlFinancial_Year.ClientID %>').prop('disabled', false);
                $('#<%= ddlMonth.ClientID %>').prop('disabled', false);
                $('#<%= ddlGodown.ClientID %>').prop('disabled', false);

                $('#<%= ddlType.ClientID %>').removeClass('disabled-field');
                $('#<%= ddlFinancial_Year.ClientID %>').removeClass('disabled-field');
                $('#<%= ddlMonth.ClientID %>').removeClass('disabled-field');
                $('#<%= ddlGodown.ClientID %>').removeClass('disabled-field');
            }
        }

        // GridView Search Function
        function searchGridView() {
            var input, filter, table, tr, td, i, j, txtValue, found;
            input = document.getElementById("txtSearch");
            filter = input.value.toUpperCase();
            table = document.getElementById("<%= gvMapping.ClientID %>");

            if (!table) {
                console.log("GridView not found");
                return;
            }

            tr = table.getElementsByTagName("tr");
            var visibleCount = 0;

            // Loop through all table rows (skip header row)
            for (i = 1; i < tr.length; i++) {
                found = false;
                // Get all cells in the row
                td = tr[i].getElementsByTagName("td");

                if (td.length > 0) {
                    // Loop through each cell (skip action column which is last)
                    for (j = 0; j < td.length - 1; j++) {
                        if (td[j]) {
                            txtValue = td[j].textContent || td[j].innerText;
                            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                                found = true;
                                break;
                            }
                        }
                    }

                    if (found) {
                        tr[i].style.display = "";
                        visibleCount++;
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }

            // Show/hide clear button
            var clearBtn = document.querySelector('.clear-search');
            if (filter.length > 0) {
                clearBtn.style.display = 'inline';
            } else {
                clearBtn.style.display = 'none';
            }

            // Show no records message if needed
            showNoRecordsMessage(visibleCount, table);
        }

        function showNoRecordsMessage(visibleCount, table) {
            // Remove existing no records message if any
            var existingMsg = document.getElementById("noRecordsMsg");
            if (existingMsg) {
                existingMsg.remove();
            }

            // If no visible rows, show message
            if (visibleCount === 0) {
                var tbody = table.getElementsByTagName("tbody")[0];
                if (tbody) {
                    var row = tbody.insertRow(0);
                    row.id = "noRecordsMsg";
                    var cell = row.insertCell(0);
                    cell.colSpan = table.rows[0].cells.length;
                    cell.className = "no-records";
                    cell.innerHTML = '<i class="fas fa-info-circle"></i> No matching records found';
                }
            }
        }

        function clearSearch() {
            document.getElementById("txtSearch").value = "";
            searchGridView();
            document.getElementById("txtSearch").focus();
        }

        // Add debounce for better performance
        var searchTimeout;
        if (document.getElementById("txtSearch")) {
            document.getElementById("txtSearch").addEventListener("keyup", function () {
                clearTimeout(searchTimeout);
                searchTimeout = setTimeout(function () {
                    searchGridView();
                }, 300);
            });
        }

        // Export to Excel functionality
        function exportToExcel() {
            var table = document.getElementById("<%= gvMapping.ClientID %>");
            var html = table.outerHTML;
            var url = 'data:application/vnd.ms-excel,' + encodeURIComponent(html);
            var downloadLink = document.createElement("a");
            downloadLink.href = url;
            downloadLink.download = 'Mapping_Report.xls';
            document.body.appendChild(downloadLink);
            downloadLink.click();
            document.body.removeChild(downloadLink);
        }

        // Print functionality
        function printGrid() {
            var printContents = document.getElementById("<%= gvMapping.ClientID %>").outerHTML;
            var originalTitle = document.title;
            document.title = "Mapping Report";
            var printWindow = window.open('', '_blank');
            printWindow.document.write('<html><head><title>Mapping Report</title>');
            printWindow.document.write('<link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />');
            printWindow.document.write('<style>table { width: 100%; border-collapse: collapse; } th, td { border: 1px solid #ddd; padding: 8px; text-align: left; } th { background-color: #007bff; color: white; }</style>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(printContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            document.title = originalTitle;
        }

        // Add keyboard shortcut (Ctrl+F) to focus search
        document.addEventListener('keydown', function (e) {
            if (e.ctrlKey && e.key === 'f') {
                e.preventDefault();
                document.getElementById("txtSearch").focus();
            }
        });
    </script>
</asp:Content>
