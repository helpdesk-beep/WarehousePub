<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/CreatePvtGodownLogin.aspx.cs" Inherits="StatePages_CreatePvtGodownLogin" Title="Pvt. Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />

    <style>
        .main-card { border: none; border-radius: 12px; box-shadow: 0 4px 20px rgba(0,0,0,0.08); margin-top: 20px; }
        .card-header { background: linear-gradient(135deg, #0bb6e6 0%, #0893ba 100%); color: white; border-radius: 12px 12px 0 0 !important; padding: 15px; }
        .form-label { font-weight: 600; color: #444; font-size: 0.9rem; }
        .btn-custom { border-radius: 6px; padding: 8px 20px; font-weight: 500; transition: all 0.3s; }
        .table-container { background: #fff; border-radius: 8px; padding: 15px; margin-top: 20px; }
        .gv-header { background-color: #f8f9fa !important; color: #333 !important; text-transform: uppercase; font-size: 0.8rem; }
        #myspindiv { background: rgba(255,255,255,0.8); backdrop-filter: blur(2px); }
        .select2-container--default .select2-selection--single { height: 38px !important; border: 1px solid #dee2e6 !important; border-radius: 6px !important; }
    </style>

    <div class="container-fluid">
        <div class="card main-card">
            <div class="card-header text-center">
                <h4 class="mb-0"><i class="fas fa-warehouse me-2"></i>Create Private Godown Login</h4>
            </div>
            <div class="card-body p-4">
                
                <div class="row g-3 mb-4">
                    <div class="col-md-3">
                        <label class="form-label">District</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-select select2" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label class="form-label">Branch</label>
                        <asp:DropDownList ID="ddlDepotList" runat="server" CssClass="form-select select2" AutoPostBack="True" OnSelectedIndexChanged="ddlDepotList_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Search Godown ID</label>
                        <div class="input-group">
                            <span class="input-group-text bg-white"><i class="fas fa-search text-muted"></i></span>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Enter Godown ID..." onkeyup="filterGrid()"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2 d-flex align-items-end">
                        <asp:Button ID="btnSearch" runat="server" Text="Fetch Data" CssClass="btn btn-primary w-100 btn-custom" OnClick="btnSearch_Click" OnClientClick="showLoader();" />
                    </div>
                </div>

                <hr class="text-muted opacity-25" />

                <div class="table-responsive table-container">
                    <asp:GridView ID="godown_GridView" runat="server" DataKeyNames="Login_Id" AutoGenerateColumns="False"
                        CssClass="table table-hover align-middle border-0" GridLines="None"
                        OnRowDeleting="godown_GridView_RowDeleting" OnSelectedIndexChanged="godown_GridView_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Select" CssClass="btn btn-sm btn-outline-primary me-1"><i class="fas fa-edit"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Delete this login?');"><i class="fas fa-trash"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate><%#Container.DataItemIndex+1%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Godown_Id" HeaderText="Godown ID" />
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                            <asp:BoundField DataField="Storage_Agency" HeaderText="Agency" />
                            <asp:BoundField DataField="IssueCenter" HeaderText="Issue Center" />
                            <asp:BoundField DataField="W_Reg_No" HeaderText="Reg. No" />
                        </Columns>
                        <HeaderStyle CssClass="gv-header" />
                        <EmptyDataTemplate>
                            <div class="text-center p-4 text-muted">No records found for the selected criteria.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>

                <div class="row g-3 mt-4 bg-light p-3 rounded shadow-sm">
                    <div class="col-lg-2 col-md-12">
                        <label class="form-label">Login ID</label>
                        <asp:TextBox ID="lblGId" runat="server" CssClass="form-control fw-bold bg-white" ReadOnly="true"></asp:TextBox>
                    </div>
                    
                    <div class="col-lg-5 col-md-12" id="trddlgd" runat="server">
                        <label class="form-label">Available Godowns</label>
                        <asp:DropDownList ID="ddlGodown" runat="server" CssClass="form-select select2" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <div class="col-md-5">
                        <label class="form-label">Hired Type</label>
                        <asp:DropDownList ID="ddlGodownType" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>

                    <div class="col-md-6" id="trgodown" runat="server">
                        <label class="form-label">Godown Name</label>
                        <asp:TextBox ID="txtGodownName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-6" id="trICC" runat="server">
                        <label class="form-label">Issue Center ID</label>
                        <asp:TextBox ID="txtIssueCCode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-6">
                        <label class="form-label">Agreement Status</label>
                        <asp:DropDownList ID="ddlAgree" runat="server" CssClass="form-select">
                            <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-6">
                        <label class="form-label">Warehouse Registration No.</label>
                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="text-center mt-4">
                    <asp:Button ID="btnupdate" runat="server" Text="Save" CssClass="btn btn-success btn-custom me-2" OnClick="btnupdate_Click" ValidationGroup="validate" />
                    <asp:Button ID="btn_Close" runat="server" Text="Reset/Close" CssClass="btn btn-secondary btn-custom" OnClick="btn_Close_Click" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdnSearchValue" runat="server" />
    <div id="myspindiv" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; z-index: 9999;">
        <div class="d-flex justify-content-center align-items-center h-100">
            <div class="spinner-border text-primary" role="status" style="width: 3rem; height: 3rem;"></div>
        </div>
    </div>

    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.6.0.min.js"></script>
    <script src="../assets/New/js/select2.min.js"></script>
    <script>
       

        function showLoader() { document.getElementById("myspindiv").style.display = "block"; }

        function filterGrid() {
            var input = document.getElementById('<%= txtSearch.ClientID %>').value.toLowerCase();
            var rows = document.querySelectorAll('#<%= godown_GridView.ClientID %> tr:not(:first-child)');
            rows.forEach(row => {
                row.style.display = row.innerText.toLowerCase().includes(input) ? '' : 'none';
            });
        }
    </script> 
     <script type="text/javascript" src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
 <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
 <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
 <script type="text/javascript">
     function initSelect2() {
         $("[id*=ddlDistrict]").select2();
         $("[id*=ddlDepotList]").select2();
         $("[id*=ddlGodown]").select2();
         $("[id*=ddlGodownType]").select2();
     }
     $(document).ready(function () {
         initSelect2();
     });
 </script>
</asp:Content>