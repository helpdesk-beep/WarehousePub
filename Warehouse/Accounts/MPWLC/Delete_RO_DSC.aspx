<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Account_MPWLC_Master.master" AutoEventWireup="true" CodeFile="Delete_RO_DSC.aspx.cs" Inherits="Accounting_Delete_RO_DSC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Bootstrap 5 CSS & JS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    <!-- jQuery & Select2 -->
    <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
    <link href="../../assets/New/css/select2.min.css" rel="stylesheet" />
    <script src="../../assets/New/js/select2.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $("[id*=ddlDistrict]").select2();
        });
        $(function () {
            $("[id*=ddlDepotList]").select2();
        });
        $(function () {
            $("[id*=ddlParty]").select2();
        });
    </script>
    <script type="text/javascript">

        function filterGrid() {
            var input = document.getElementById("<%= txtSearch.ClientID %>");
            var filter = input.value.toLowerCase();
            var table = document.getElementById("<%= gv.ClientID %>");
            var trs = table.getElementsByTagName("tr");

            for (var i = 1; i < trs.length; i++) {
                var display = false;
                var tds = trs[i].getElementsByTagName("td");
                for (var j = 0; j < tds.length; j++) {
                    if (tds[j] && tds[j].textContent.toLowerCase().indexOf(filter) > -1) {
                        display = true;
                        break;
                    }
                }
                trs[i].style.display = display ? "" : "none";
            }
        }
    </script>

    <div class="container1 my-4" style="padding: 0px 50px 0px 50px;">

        <!-- Card Header -->
        <div class="card border-primary">
            <div class="card-header bg-primary1 text-white text-center" style="background: #5897fb;">
                <h5>Delete RM/Manager A/C Bill's DSC Details</h5>
            </div>

            <div class="card-body">

                <p class="text-primary fw-bold">
                    Note: Record will be deleted on the basis of Bill No. This will remove all records that belong to selected Bill No.
                </p>

                <!-- Dropdowns Section -->
                <div class="row g-3 mb-3">
                    <div class="row mt-4">
                        <div class="col-md-1">
                            <label class="form-label fw-bold">District</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-2"></div>
                        <div class="col-md-1">
                            <label class="form-label fw-bold">Branch</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlDepotList" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDepotList_SelectedIndexChanged1"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div class="col-md-2">
                            <label class="form-label fw-bold">Select Beneficiary Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlParty" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <!-- Search & Button Section -->
                <div class="row mb-3">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSerachWHR" runat="server" Text="Search"
                            CausesValidation="false"
                            OnClick="btnSerachWHR_Click"
                            Style="background-color: #66b3ff; border: 1px solid #4da3ff; padding: 8px 20px; border-radius: 5px; color: white; font-weight: 600; cursor: pointer;" />

                    </div>
                </div>
                <hr />
                <div class="row mb-3">
                    <div class="col-md-4">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search here..." onkeyup="filterGrid();" Visible="false" />
                    </div>
                    <div class="col-md-6"></div>
                    <div class="col-md-2">

                        <!-- Row Count & Not Found -->
                        <div class="d-flex justify-content-between mb-3">
                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record:" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_notfound" runat="server" ForeColor="red" Font-Bold="true" Font-Size="15pt" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>

                <!-- GridView Section -->
                <div class="table-responsive mb-3">
                    <asp:Panel ID="panelContainer" runat="server" Height="380px" ScrollBars="Vertical" Visible="false">
                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Ref_Bill_No" CssClass="table table-bordered table-striped table-hover">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_Delete" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Ref_Bill_No" HeaderText="Bill No." />
                                <asp:BoundField DataField="Godown" HeaderText="Godown Name" />
                                <asp:BoundField DataField="Godown_Id" HeaderText="Godown Id" />
                                <asp:BoundField DataField="Commodity" HeaderText="Commodity Name" />
                                <asp:BoundField DataField="Net_Amount" HeaderText="Net Amount" />
                                <asp:BoundField DataField="Month_No" HeaderText="Month No." />
                                <asp:BoundField DataField="party_Name" HeaderText="Party Name" />
                                <asp:BoundField DataField="Account_No" HeaderText="Account No" />
                                <asp:BoundField DataField="IFSC_Code" HeaderText="IFSC Code" />
                                <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" />
                                <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" />
                                <asp:BoundField DataField="RM" HeaderText="DSC Holder Name" />
                                <asp:BoundField DataField="Acc_Mgr" HeaderText="DSC Holder Name" />
                                <asp:BoundField DataField="NEFT" HeaderText="NEFT Status" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>


                <!-- Action Buttons -->
                <div class="d-flex justify-content-center gap-3">
                    <asp:Button ID="Btn_Delete"
                        runat="server"
                        Text="Delete Record"
                        Visible="false"
                        OnClick="Btn_Delete_Click"
                        OnClientClick="return confirm('Are you sure you want to delete this record?');"
                        Style="background-color: #ff9999; border: 1px solid #ff8080; padding: 8px 20px; border-radius: 5px; color: #FFF; font-weight: 600; cursor: pointer;" />

                    <asp:Button ID="btn_Close" runat="server" Text="Close" CssClass="btn btn-secondary" Visible="false" OnClick="btn_Close_Click" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>
