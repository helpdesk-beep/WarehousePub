<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Region/Delete_DMO_Markfed_Bills.aspx.cs" Inherits="Region_Delete_DMO_Markfed_Bills" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css"
        rel="stylesheet" />
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <!-- SweetAlert2 -->
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        function bindSelect2() {
            $('[id$=ddlDistrict]').select2({ width: '100%' });
            $('[id$=ddlBranch]').select2({ width: '100%' });
            $('[id$=ddlGodown]').select2({ width: '100%' });
            $('[id$=ddlCommodity]').select2({ width: '100%' });
        }

        $(document).ready(function () {
            bindSelect2();
        });

        if (typeof Sys !== 'undefined') {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                bindSelect2();
            });
        }
    </script>
    <script type="text/javascript">
        function confirmDelete(btn) {

            // ✅ agar already confirmed hai → direct postback
            if (btn.dataset.confirmed === "true") {
                btn.dataset.confirmed = "false"; // reset
                return true; // allow postback
            }

            // ❌ pehli baar → popup dikhao
            Swal.fire({
                title: 'Are you sure?',
                text: 'This bill will be permanently deleted!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, delete it',
                cancelButtonText: 'Cancel',
                confirmButtonColor: '#d33'
            }).then((result) => {
                if (result.isConfirmed) {
                    btn.dataset.confirmed = "true";
                    btn.click(); // 🔥 second click → postback
                }
            });

            return false; // pehli baar postback roko
        }
    </script>
    <%--<script type="text/javascript">
        function confirmDelete(billNo, btnUniqueId) {
            Swal.fire({
                title: 'Are you sure?',
                text: "This bill will be permanently deleted!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Yes, delete it',
                cancelButtonText: 'Cancel'
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack(btnUniqueId, billNo);
                }
            });
            return false;
        }
        
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vgShow" CssClass="alert alert-danger" HeaderText="Please Select following errors:" DisplayMode="BulletList" />
    <asp:HiddenField ID="hdnDeleteReason" runat="server" />
    <div class="container-fluid mt-3">
        <!-- Page Header -->
        <div class="card shadow-sm mb-3">
            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">Delete Bills</h5>
            </div>
            <div class="card-body">
                <div class="row g-3">
                    <div class="col-md-2">
                        <label class="form-label fw-bold">District</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server"
                            CssClass="form-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDistrict" runat="server"
                            ControlToValidate="ddlDistrict"
                            InitialValue="0"
                            ErrorMessage="Please select District"
                            ValidationGroup="vgShow"
                            Display="None" />
                    </div>
                    <div class="col-md-2">
                        <label class="form-label fw-bold">Branch</label>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server"
                            ControlToValidate="ddlBranch"
                            InitialValue="0"
                            ErrorMessage="Please select Branch"
                            ValidationGroup="vgShow"
                            Display="None" />
                    </div>
                    <div class="col-md-3">
                        <label class="form-label fw-bold">Godown</label>
                        <asp:DropDownList ID="ddlGodown" runat="server"
                            CssClass="form-select">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvGodown" runat="server"
                            ControlToValidate="ddlGodown"
                            InitialValue="0"
                            ErrorMessage="Please select Godown"
                            ValidationGroup="vgShow"
                            Display="None" />
                    </div>
                    <div class="col-md-2 ">
                        <label class="form-label fw-bold">Commodity</label>
                        <asp:DropDownList ID="ddlCommodity" runat="server" CssClass="form-select"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCommodity" runat="server"
                            ControlToValidate="ddlCommodity"
                            InitialValue="0"
                            ErrorMessage="Please select Commodity"
                            ValidationGroup="vgShow"
                            Display="None" />
                    </div>
                    <div class="col-md-1" style="margin-top: 45px">
                        <asp:Button ID="btnShow" runat="server" Text="Show Bills" ValidationGroup="vgShow" CssClass="btn btn-sm btn-success px-4" OnClick="btnShow_Click" />
                    </div>
                </div>
                <div class="row mt-3">
                </div>
            </div>
        </div>

        <!-- Grid Section -->
        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView ID="gvBills" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover"
                    EmptyDataText="No bills found" DataKeyNames="Bill_Number" OnRowCommand="gvBills_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="District" DataField="District_Name" />
                        <asp:BoundField HeaderText="Branch" DataField="Branch_Name" />
                        <asp:BoundField HeaderText="Godown" DataField="Godown_Name" />
                        <asp:BoundField HeaderText="Commodity Name" DataField="Commodity_Name" />
                        <asp:BoundField HeaderText="Bill No" DataField="Bill_Number" />
                        <asp:BoundField HeaderText="Financial Year" DataField="Financial_Year" />
                        <asp:BoundField HeaderText="Crop Year" DataField="Crop_Year" />
                        <asp:BoundField HeaderText="Month" DataField="Month" />
                        <asp:BoundField HeaderText="Net Amount" DataField="Net_Amount" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server"
                                    Text="Delete"
                                    CssClass="btn btn-sm btn-danger"
                                    CommandName="DeleteBill"
                                    CommandArgument='<%# Eval("Bill_Number") %>'
                                    CausesValidation="false"
                                    UseSubmitBehavior="false"
                                    OnClientClick="return confirmDelete(this);">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

