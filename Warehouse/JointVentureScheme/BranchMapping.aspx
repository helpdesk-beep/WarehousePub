<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchMapping.aspx.cs" Inherits="JointVentureScheme_BranchMapping" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Branch Block Mapping</title>
    <!-- Bootstrap and Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Select2 for searchable dropdown -->
     <script type="text/javascript" src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
 <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
 <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
 <script type="text/javascript">
     function initSelect2() {
         $("[id*=ddlDistrict]").select2();
         //$("[id*=ddlBranch]").select2();
     }
     $(document).ready(function () {
         initSelect2();
     });
 </script>
    
    <style>
        body { font-family: 'Segoe UI', sans-serif; background-color: #f8f9fa; }
        .navbar { background-color: #1a237e; color: white; }
        .icon-btn { font-size: 1.2rem; cursor: pointer; }
        .card { border: none; box-shadow: 0 2px 4px rgba(0,0,0,0.1); margin-top: 20px; }
        .card-header { background: white; border-bottom: 1px solid #eee; color: #1a237e; font-weight: bold; }
        .grid-header { background-color: #f1f3f4 !important; font-weight: 600; text-align: center; }
        .btn-search { background-color: #1a237e; color: white; border-radius: 4px; padding: 2px 10px; }
        .table-custom td { vertical-align: middle; text-align: center; }
        /* Normal State */
/*.btn-search { 
    background-color: #1a237e !important; 
    color: white !important; 
    border-radius: 4px; 
    padding: 8px 40px; 
    border: none;
    transition: background-color 0.3s ease;
}
*/
/* Hover State (Mouse over) */
.btn-search:hover { 
    background-color: #51A68C !important; /* Slightly darker blue */
    color: #1a237e !important; 
}

/* Active/Focus State (While clicking) */
.btn-search:active, .btn-search:focus { 
    background-color: #51A68C !important; 
    color: #1a237e !important;
    outline: none;
    box-shadow: none; 
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Professional Navbar with Home and Logout Icons -->
        <nav class="navbar p-2">
            <div class="container-fluid d-flex justify-content-between align-items-center">
                <div>
                    <asp:LinkButton ID="btnHome" runat="server" CssClass="text-white me-3 text-decoration-none" OnClick="btnHome_Click" ToolTip="Home">
                        <i class="bi bi-house-door-fill icon-btn"></i>
                    </asp:LinkButton>
                    <span class="h5 m-0 text-white">Branch Block Mapping</span>
                </div>
                <div class="text-white">
                    Welcome, <b><asp:Label ID="lblUser" runat="server"></asp:Label></b> | 
                    <asp:LinkButton ID="lnkLogout" runat="server" CssClass="text-white ms-2 text-decoration-none" OnClick="lnkLogout_Click" ToolTip="Logout">
                        <i class="bi bi-box-arrow-right icon-btn"></i>
                    </asp:LinkButton>
                </div>
            </div>
        </nav>

        <div class="container-fluid px-4">
            <!-- Search Section with Reduced Width Dropdown -->
            <div class="card p-3">
                <div class="card-header border-0 p-0 mb-3">Search Database</div>
                <div class="row g-3 align-items-end">
                    <div class="col-md-3"> <!-- Restricted width for dropdown -->
                        <label class="form-label small fw-bold">District Name</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-select select2" 
                            DataTextField="District_Name" DataValueField="District_ID">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-search w-100" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>

            <!-- GridView Section -->
            <div class="card p-3">
                <div class="card-header border-0 p-0 mb-3">Mapping Results</div>
                <div class="table-responsive">
                    <asp:GridView ID="gvMapping" runat="server" AutoGenerateColumns="False" 
                        CssClass="table table-bordered table-custom" DataKeyNames="District_ID,Block_ID"
                        OnRowEditing="gvMapping_RowEditing" OnRowUpdating="gvMapping_RowUpdating" 
                        OnRowCancelingEdit="gvMapping_RowCancelingEdit">
                        <HeaderStyle CssClass="grid-header" />
                        <Columns>
                            <asp:BoundField DataField="District_Name" HeaderText="District" ReadOnly="true" />
                            <asp:BoundField DataField="Block_Name" HeaderText="Block Name" ReadOnly="true" />
                            <asp:BoundField DataField="Block_ID" HeaderText="Block ID" ReadOnly="true" />
                            <asp:TemplateField HeaderText="Branch ID">
                                <ItemTemplate><%# Eval("Branch_ID") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBranchID" runat="server" Text='<%# Bind("Branch_ID") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name (Hindi)">
                                <ItemTemplate><%# Eval("Branch_Name") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBranchName" runat="server" Text='<%# Bind("Branch_Name") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="text-info fs-5">
                                        <i class="bi bi-pencil-square"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" Text="Save" CssClass="btn btn-sm btn-success me-1" />
                                    <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-sm btn-danger" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
   </body>
</html>