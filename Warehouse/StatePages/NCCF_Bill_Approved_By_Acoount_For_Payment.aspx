<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Nccf_Master.master" AutoEventWireup="true" CodeFile="~/NCCF/NCCF_Bill_Approved_By_Acoount_For_Payment.aspx.cs" Inherits="NCCF_NCCF_Bill_Approved_By_Acoount_For_Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <style>
        .custom-grid {
            width: 100%;
            border-collapse: collapse;
            background: #fff;
            font-size: 14px;
        }
        .custom-grid th {
            background: #8ea9c5 !important;
            color: white;
            text-align: center;
            padding: 10px !important;
            font-weight: 600;
        }
        .custom-grid td {
            padding: 8px !important;
            vertical-align: middle !important;
        }
        .custom-grid tr:nth-child(even) {
            background: #f2f6fc;
        }
        .custom-grid tr:hover {
            background: #d9ebff;
            transition: 0.2s;
        }
        .readonly-box {
            background-color: #f0f0f0;
            border: 1px solid #ccc;
            cursor: not-allowed;
        }
        .highlight-total {
            background-color: #ffc107;
            color: #000;
            font-weight: bold;
            padding: 5px 10px;
            border-radius: 5px;
            border: 1px solid #ff9800;
        }
        #searchBox {
            padding: 8px;
            width: 250px;
            margin-bottom: 12px;
            border-radius: 4px;
            border: 1px solid #aaa;
        }
        .payment-card {
            background: #f8f9fa;
            border: 1px solid #ddd;
            border-radius: 8px;
            padding: 15px;
            margin-top: 15px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.05);
        }
    </style>
    <script type="text/javascript">
        function initSelect2() {
            $('.select2').select2({
                allowClear: true,
                width: '100%'
            });
            $(document).on('select2:open', function () {
                setTimeout(function () {
                    let searchBox = document.querySelector('.select2-container--open .select2-search__field');
                    if (searchBox) {
                        searchBox.focus();
                    }
                }, 10);
            });
        }
        $(document).ready(function () {
            initSelect2();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            initSelect2();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="content_wrapper">
        <asp:Label ID="lblrmsg" runat="server" Visible="false" ForeColor="#339933" Font-Bold="true" Font-Size="Large"></asp:Label>
        
        <fieldset>
            <legend>Bill Pending for payment approval</legend>
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px; font-weight: bold;">Commodity</label>
                        <asp:DropDownList CssClass="form-control" ID="ddlcommodity" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group ">
                        <label style="margin-top: 10px; font-weight: bold;">Crop Year</label>
                        <asp:DropDownList CssClass="form-control" ID="ddlcropyear" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group ">
                        <label style="margin-top: 10px; font-weight: bold;">District Name</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddldistrict" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group ">
                        <label style="margin-top: 10px; font-weight: bold;">Branch Name</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlbranch" runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group ">
                        <label style="margin-top: 10px; font-weight: bold;">Godown Name</label>
                        <asp:DropDownList CssClass="form-control select2" ID="ddlGodown" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px; font-weight: bold;">Financial Year</label>
                        <asp:DropDownList CssClass="form-control" ID="ddlFinancialyear" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                            <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                            <asp:ListItem Value="2025-26">2025-26</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label style="margin-top: 10px; font-weight: bold;">Month</label>
                        <asp:DropDownList CssClass="form-control" ID="ddlmonth" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-2" style="text-align: center; margin-top: 40px">
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" />
                </div>
            </div>
        </fieldset>

        <fieldset style="align-content: center" runat="server" id="grdbill" visible="false">
            <legend>Bills Details</legend>
            
            <asp:UpdatePanel ID="UP_GridAndCalculations" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-1">
                            <label style="margin-top: 10px; font-weight: bold;">Search:</label>
                        </div>
                        <div class="col-md-2">
                            <input type="text" id="searchBox" placeholder="Search..." oninput="searchGrid()" />
                        </div>
                        <div class="col-md-3"></div>
                        <div class="col-md-1">
                            <label style="margin-top: 10px; font-weight: bold;">Total Bills:</label>
                        </div>
                        <div class="col-md-1" style="margin-top: 10px;">
                            <asp:Label ID="litTotalBills" runat="server" CssClass="highlight-total"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <label style="margin-top: 10px; font-weight: bold;">Total Amount:</label>
                        </div>
                        <div class="col-md-1" style="margin-top: 10px;">
                            <asp:Label ID="litTotalAmount" runat="server" CssClass="highlight-total"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:Panel ID="pnlGrid" runat="server">
                                    <asp:GridView ID="GrdBills" runat="server" AutoGenerateColumns="False" CssClass="custom-grid"
                                        OnDataBound="GrdBills_DataBound" OnRowCommand="GrdBills_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Region Name" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch Name" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnGodownId" runat="server" Value='<%# Eval("Godown_ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Number" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblBill_Number" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Commodity Name" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCommodity_Name" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnCommodityId" runat="server" Value='<%# Eval("Commodity_ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Crop Year" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCrop_Year" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblMonth" Text='<%# Eval("Month") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Amount" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblNet_Amount" CssClass="net-amount" Text='<%# Eval("Net_Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TDS Deduction" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTDS_Deduction" runat="server" ReadOnly="true" CssClass="tds-deduction"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Deduction" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOther_Deduction" runat="server" CssClass="Other_Deduction" Text="0"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Deduction Remark" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDeduction_Remark" runat="server" CssClass="Deduction_Remark" Text='<%# Eval("Deduction_Remark") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pass Amount" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtpass" runat="server" CssClass="pass-amount"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PSF Amount" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtpsf" runat="server" CssClass="psf-amount" Text="0"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bussiness Approved Date" HeaderStyle-BackColor="LightBlue">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtMarketingApprovedDate" runat="server" Text='<%# Eval("Marketing_Approve_Date") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkBxHeader" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Checked" runat="server" OnCheckedChanged="Checked_CheckedChanged" AutoPostBack="true" />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center" Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                <ControlStyle Width="15px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <div class="payment-card" id="allcount" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-3" style="margin-top: 10px;">
                                <label id="lbltotalsebils" runat="server" style="font-weight: bold;">Total Bills:</label>
                                <asp:Label ID="lblSelectedCount" runat="server" CssClass="highlight-total"></asp:Label>
                            </div>
                            <div class="col-md-3" style="margin-top: 10px;">
                                <label id="lblselecAmount" runat="server" style="font-weight: bold;">Total Amount:</label>
                                <asp:Label ID="lblSelectedAmount" runat="server" CssClass="highlight-total"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group" style="margin-bottom:0;">
                                    <label style="font-weight: bold; color: #cc0000;">UTR Number *</label>
                                    <asp:TextBox ID="txtUTRNumber" runat="server" CssClass="form-control" autocomplete="off" placeholder="Enter Reference UTR"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group" style="margin-bottom:0;">
                                    <label style="font-weight: bold; color: #cc0000;">Check Date *</label>
                                    <asp:TextBox ID="txtPaymentDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        
                        <div class="row" style="margin-top: 20px">
                            <div class="col-md-5"></div>
                            <div class="col-md-2" style="text-align: center;">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>

    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            localStorage.setItem('gridScrollY', window.scrollY || document.documentElement.scrollTop);
            localStorage.setItem('gridScrollX', window.scrollX || document.documentElement.scrollLeft);
        });

        prm.add_endRequest(function () {
            var sy = localStorage.getItem('gridScrollY');
            var sx = localStorage.getItem('gridScrollX');
            if (sy && sx) {
                window.scrollTo(parseInt(sx), parseInt(sy));
                localStorage.removeItem('gridScrollY');
                localStorage.removeItem('gridScrollX');
            }
            initializeGridCalculation();
        });

        // Event Listeners ko Other_Deduction aur psf-amount dono classes ke liye handle kiya gaya hai
        document.addEventListener('input', function (e) {
            if (e.target && (e.target.classList.contains('Other_Deduction') || e.target.classList.contains('psf-amount'))) {
                calculateRow(e.target);
            }
        });

        document.addEventListener('keyup', function (e) {
            if (e.target && (e.target.classList.contains('Other_Deduction') || e.target.classList.contains('psf-amount'))) {
                calculateRow(e.target);
            }
        });

        function calculateRow(input) {
            var row = input.closest("tr");
            if (!row) return;

            var netAmountLabel = row.querySelector(".net-amount");
            var txtTDS = row.querySelector(".tds-deduction");
            var txtOther = row.querySelector(".Other_Deduction");
            var txtPSF = row.querySelector(".psf-amount");
            var txtPass = row.querySelector(".pass-amount");
            var txtRemark = row.querySelector(".Deduction_Remark");

            if (!netAmountLabel || !txtTDS || !txtOther || !txtPSF || !txtPass || !txtRemark) return;

            var net = parseFloat(netAmountLabel.innerText.replace(/,/g, '')) || 0;
            var tds = net * 0.10;
            txtTDS.value = tds.toFixed(2);

            var other = parseFloat(txtOther.value) || 0;
            var psf = parseFloat(txtPSF.value) || 0;

            // Naya Rule: Pass Amount = Net Amount - TDS - Other Deduction - PSF Amount
            var pass = net - tds - other - psf;

            // Agar calculation negative chali jaye toh safety validation check
            if (pass < 0) {
                pass = 0;
                // Jis input field ko edit kiya ja raha hai, usko limit me adjust karne ke liye logic
                if (input.classList.contains('psf-amount')) {
                    psf = net - tds - other;
                    if (psf < 0) psf = 0;
                    txtPSF.value = psf.toFixed(2);
                } else {
                    other = net - tds - psf;
                    if (other < 0) other = 0;
                    txtOther.value = other.toFixed(2);
                }
            }

            txtPass.value = pass.toFixed(2);

            // Remark box mandatory setting logic
            if (other > 0) {
                txtRemark.readOnly = false;
                txtRemark.classList.remove("readonly-box");
            } else {
                txtRemark.readOnly = true;
                txtRemark.value = "";
                txtRemark.classList.add("readonly-box");
            }
        }

        function initializeGridCalculation() {
            var table = document.getElementById("<%= GrdBills.ClientID %>");
            if (table) {
                var rows = table.querySelectorAll("tr");
                rows.forEach(function (row) {
                    var txtOther = row.querySelector(".Other_Deduction");
                    if (txtOther) {
                        calculateRow(txtOther);
                    }
                });
            }
        }

        function searchGrid() {
            var input = document.getElementById("searchBox").value.toLowerCase();
            var table = document.getElementById("<%= GrdBills.ClientID %>");
            if (!table) return;
            var rows = table.getElementsByTagName("tr");
            var visibleCount = 0;
            var totalAmount = 0;

            for (var i = 1; i < rows.length; i++) {
                var show = false;
                var cells = rows[i].getElementsByTagName("td");
                if (cells.length === 0) continue;

                for (var j = 0; j < cells.length; j++) {
                    if (cells[j].innerText.toLowerCase().includes(input)) {
                        show = true;
                        break;
                    }
                }
                rows[i].style.display = show ? "" : "none";

                if (show) {
                    visibleCount++;
                    var amountLabel = rows[i].querySelector(".net-amount");
                    if (amountLabel) {
                        var amtText = amountLabel.innerText.replace(/,/g, '');
                        var amt = parseFloat(amtText) || 0;
                        totalAmount += amt;
                    }
                }
            }
            document.getElementById("<%= litTotalBills.ClientID %>").innerText = visibleCount;
            document.getElementById("<%= litTotalAmount.ClientID %>").innerText = totalAmount.toFixed(2);
        }

        window.onload = function () {
            initializeGridCalculation();
        };
    </script>
</asp:Content>