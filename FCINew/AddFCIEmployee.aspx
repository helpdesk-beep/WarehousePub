<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FCI_Master_New.master" AutoEventWireup="true" CodeFile="AddFCIEmployee.aspx.cs" Inherits="FCI_AddFCIEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">

<div class="container-fluid">

    <!-- FORM CARD -->
    <div class="content-card">

        <h4 class="mb-4 text-primary fw-bold">
            <i class="fa fa-user-plus mr-2"></i> FCI Employee Registration
        </h4>

        <asp:HiddenField ID="hfEmployeeID" runat="server" />

        <div class="row">

            <div class="col-md-4 mb-3">
                <label class="form-label fw-bold">Employee Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Full Name"></asp:TextBox>
            </div>

            <div class="col-md-4 mb-3">
                <label class="form-label fw-bold">Mobile No</label>
                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10" placeholder="10 Digit Number"></asp:TextBox>
            </div>

            <div class="col-md-4 mb-3">
                <label class="form-label fw-bold">Allocated Date</label>
                <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4 mb-3">
                <label class="form-label fw-bold">District</label>
                <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                    CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="col-md-4 mb-3">
                <label class="form-label fw-bold">Branch (Depot)</label>
                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control">
                    <asp:ListItem Text="--Select Branch--" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-4 mb-3">
                <label class="form-label fw-bold">Designation</label>
                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" placeholder="Enter Designation"></asp:TextBox>
            </div>

            <div class="col-md-4 mb-3 d-flex align-items-center">
                <div class="form-check p-2 border rounded bg-white shadow-sm w-100">
                    <asp:CheckBox ID="chkStatus" runat="server" CssClass="form-check-input mr-2" />
                    <label class="form-check-label fw-bold">Status Active</label>
                </div>
            </div>

        </div>

        <!-- BUTTONS -->
        <div class="text-center mt-3">
            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="return validateForm();"
                CssClass="btn btn-primary px-5 fw-bold">
                <i class="fa fa-save mr-2"></i> Save
            </asp:LinkButton>

            <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                CssClass="btn btn-secondary px-5 fw-bold ml-2" Visible="false">
                <i class="fa fa-times mr-2"></i> Cancel
            </asp:LinkButton>
        </div>

    </div>

    <!-- GRID CARD -->
    <div class="content-card">

        <h5 class="mb-3 fw-bold text-dark">
            <i class="fa fa-list mr-2"></i> Employee List
        </h5>

        <div class="table-container">

            <asp:GridView ID="gvEmployees" runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="FCI_ID"
                CssClass="table table-bordered table-hover"
                OnRowDeleting="gvEmployees_RowDeleting"
                OnRowCommand="gvEmployees_RowCommand">

                <Columns>

                    <asp:BoundField DataField="FCI_ID" HeaderText="ID" />
                    <asp:BoundField DataField="Emp_Name" HeaderText="Name" />
                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile" />
                    <asp:BoundField DataField="District_Name" HeaderText="District" />
                    <asp:BoundField DataField="DepotName" HeaderText="Branch" />
                    <asp:BoundField DataField="Designation" HeaderText="Designation" />

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class='badge <%# (bool)Eval("Status") ? "bg-success" : "bg-danger" %>'>
                                <%# (bool)Eval("Status") ? "Active" : "Inactive" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>

                            <asp:LinkButton ID="lnkEdit" runat="server"
                                CommandName="EditEmployee"
                                CommandArgument='<%# Container.DataItemIndex %>'
                                CssClass="btn btn-sm btn-warning">
                                <i class="fa fa-edit"></i>
                            </asp:LinkButton>

                            <asp:LinkButton ID="lnkDelete" runat="server"
                                CommandName="Delete"
                                CssClass="btn btn-sm btn-danger ml-1"
                                OnClientClick="return confirm('Delete this record?');">
                                <i class="fa fa-trash"></i>
                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

    </div>

</div>


   <script type="text/javascript">
       // Client-side validation function - One by one validation
       function validateForm() {
           // Get all form field values
           var txtName = document.getElementById('<%= txtName.ClientID %>');
        var txtMobile = document.getElementById('<%= txtMobile.ClientID %>');
        var txtDate = document.getElementById('<%= txtDate.ClientID %>');
        var ddlDistrict = document.getElementById('<%= ddlDistrict.ClientID %>');
        var ddlBranch = document.getElementById('<%= ddlBranch.ClientID %>');
        var txtDesignation = document.getElementById('<%= txtDesignation.ClientID %>');
        
        // Check 1: Employee Name
        if (txtName.value.trim() == "" || txtName.value.trim() == "0" || txtName.value.trim().toUpperCase() == "NULL") {
            alert("Please Enter Employee Name");
            txtName.focus();
            txtName.style.borderColor = "red";
            txtName.style.borderWidth = "2px";
            return false;
        } else {
            txtName.style.borderColor = "";
        }
        
        // Check 2: Mobile Number
        if (txtMobile.value.trim() == "" || txtMobile.value.trim() == "0" || txtMobile.value.trim().toUpperCase() == "NULL") {
            alert("Please Enter Mobile Number");
            txtMobile.focus();
            txtMobile.style.borderColor = "red";
            txtMobile.style.borderWidth = "2px";
            return false;
        } else if (txtMobile.value.trim().length != 10) {
            alert("Mobile Number must be exactly 10 digits");
            txtMobile.focus();
            txtMobile.style.borderColor = "red";
            txtMobile.style.borderWidth = "2px";
            return false;
        } else if (isNaN(txtMobile.value.trim())) {
            alert("Mobile Number must contain only digits");
            txtMobile.focus();
            txtMobile.style.borderColor = "red";
            txtMobile.style.borderWidth = "2px";
            return false;
        } else {
            txtMobile.style.borderColor = "";
        }
        
        // Check 3: Allocated Date
        if (txtDate.value.trim() == "" || txtDate.value.trim() == "0" || txtDate.value.trim().toUpperCase() == "NULL") {
            alert("Please Select Allocated Date");
            txtDate.focus();
            txtDate.style.borderColor = "red";
            txtDate.style.borderWidth = "2px";
            return false;
        } else {
            txtDate.style.borderColor = "";
        }
        
        // Check 4: District
        if (ddlDistrict.value == "" || ddlDistrict.value == "0" || ddlDistrict.value.toUpperCase() == "NULL") {
            alert("Please Select District");
            ddlDistrict.focus();
            ddlDistrict.style.borderColor = "red";
            ddlDistrict.style.borderWidth = "2px";
            return false;
        } else {
            ddlDistrict.style.borderColor = "";
        }
        
        // Check 5: Branch (Depot)
        if (ddlBranch.value == "" || ddlBranch.value == "0" || ddlBranch.value.toUpperCase() == "NULL") {
            alert("Please Select Branch (Depot)");
            ddlBranch.focus();
            ddlBranch.style.borderColor = "red";
            ddlBranch.style.borderWidth = "2px";
            return false;
        } else {
            ddlBranch.style.borderColor = "";
        }
        
        // Check 6: Designation
        if (txtDesignation.value.trim() == "" || txtDesignation.value.trim() == "0" || txtDesignation.value.trim().toUpperCase() == "NULL") {
            alert("Please Enter Designation");
            txtDesignation.focus();
            txtDesignation.style.borderColor = "red";
            txtDesignation.style.borderWidth = "2px";
            return false;
        } else {
            txtDesignation.style.borderColor = "";
        }
        
        // If all validations pass
        return true;
    }
    
    // Function to clear border color when user starts typing
    function clearBorder(element) {
        element.style.borderColor = "";
        element.style.borderWidth = "";
    }
    
    // Add event listeners when page loads
    window.onload = function() {
        // Get all form fields
        var txtName = document.getElementById('<%= txtName.ClientID %>');
        var txtMobile = document.getElementById('<%= txtMobile.ClientID %>');
        var txtDate = document.getElementById('<%= txtDate.ClientID %>');
        var ddlDistrict = document.getElementById('<%= ddlDistrict.ClientID %>');
        var ddlBranch = document.getElementById('<%= ddlBranch.ClientID %>');
        var txtDesignation = document.getElementById('<%= txtDesignation.ClientID %>');

           // Add onfocus event to clear borders
           if (txtName) txtName.onfocus = function () { clearBorder(this); };
           if (txtMobile) txtMobile.onfocus = function () { clearBorder(this); };
           if (txtDate) txtDate.onfocus = function () { clearBorder(this); };
           if (ddlDistrict) ddlDistrict.onfocus = function () { clearBorder(this); };
           if (ddlBranch) ddlBranch.onfocus = function () { clearBorder(this); };
           if (txtDesignation) txtDesignation.onfocus = function () { clearBorder(this); };
       };
   </script>

</asp:Content>