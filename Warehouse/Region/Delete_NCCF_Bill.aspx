<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Region/Delete_NCCF_Bill.aspx.cs" Inherits="Region_Delete_NCCF_Bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Delete NCCF Bill</title>
    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style type="text/css">
        .page-title {
            font-weight: 600;
        }

        .table thead th {
            white-space: nowrap;
        }

        .msg {
            font-weight: 600;
        }
    </style>
    <script type="text/javascript">
        function showLoader() {
            document.getElementById("loader").style.display = "block";
        }

        function hideLoader() {
            document.getElementById("loader").style.display = "none";
        }

        function showAlert(msg, type) {
            Swal.fire({
                icon: type,        // success | error | warning | info
                title: 'Message',
                text: msg,
                confirmButtonText: 'OK'
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid mt-3">
        <!-- Card -->
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h5 class="mb-0 page-title">Delete NCCF Bill</h5>
            </div>
            <div class="card-body">
                <!-- Search Row -->
                <div class="row g-3 align-items-end">
                    <!-- Dropdown Added Before Bill Number -->
                    <div class="col-md-3">
                        <label class="form-label fw-semibold">Bill Type</label>
                        <asp:DropDownList ID="ddlBillType" runat="server" CssClass="form-select">
                            <asp:ListItem Value="0">Selete</asp:ListItem>
                            <asp:ListItem Value="FD">Storage Bill</asp:ListItem>
                            <asp:ListItem Value="GR">Rent Bill</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label class="form-label fw-semibold">Bill Number</label>
                        <asp:TextBox ID="txtBillNo" runat="server" AutoComplete="Off" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary w-100"
                            OnClick="btnSearch_Click" OnClientClick="showLoader();" />
                    </div>
                    <div id="loader" class="text-center mt-3" style="display: none;">
                        <div class="spinner-border text-primary" role="status">
                            <span class="visually-hidden">Loading...</span>
                        </div>
                        <div class="mt-2 fw-semibold">Please wait, data loading...</div>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblMessage" runat="server" CssClass="msg text-danger"></asp:Label>
                    </div>
                </div>
                <hr />
                <!-- Grid -->
                <div class="table-responsive">
                    <asp:GridView ID="gvBill" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-bordered table-striped table-hover align-middle text-center"
                        DataKeyNames="Bill_Number"
                        OnRowCommand="gvBill_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Branch_Name" HeaderText="Branch" />
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                            <asp:BoundField DataField="Bill_Number" HeaderText="Bill No" />
                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />
                            <asp:BoundField DataField="Financial_Year" HeaderText="FY" />
                            <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" />
                            <asp:BoundField DataField="Month" HeaderText="Month" />
                            <asp:BoundField DataField="Net_Amount" HeaderText="Amount" DataFormatString="{0:N2}" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="btnDelete" runat="server"
                                        Text="Delete"
                                        CssClass="btn btn-sm btn-danger"
                                        CommandName="DeleteBill"
                                        CommandArgument='<%# Eval("Bill_Number") %>'
                                        OnClientClick="return confirm('क्या आप इस Bill को delete करना चाहते हैं?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>