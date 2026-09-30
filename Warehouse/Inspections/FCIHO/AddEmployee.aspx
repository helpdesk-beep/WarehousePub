<%@ Page Title="FCI Registration" Language="C#" MasterPageFile="~/Inspections/Masters/State_FCIHO.master" AutoEventWireup="true" CodeFile="AddEmployee.aspx.cs" Inherits="Inspections_FCIHO_AddEmployee" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="card shadow border-0 mb-4">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0"><i class="fas fa-user-plus me-2"></i>FCI Employee Registration</h4>
            </div>
            <div class="card-body bg-light">
                <asp:HiddenField ID="hfEmployeeID" runat="server" />

                <div class="row">
                    <div class="col-md-4 mb-3">
                        <label class="form-label fw-bold">Employee Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Full Name"></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label fw-bold">Mobile No</label>
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="10 Digit Number" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label fw-bold">Allocated Date</label>
                        <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="row align-items-end">
                    <div class="col-md-4 mb-3">
                        <label class="form-label fw-bold">District</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="form-select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label fw-bold">Branch (Depot)</label>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-select">
                            <asp:ListItem Text="--Select Branch--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label fw-bold">Designation</label>
                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" placeholder="Eneter your Designation"></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="form-check p-2 border rounded bg-white shadow-sm" style="height: 45px; display: flex; align-items: center; padding-left: 35px !important;">
                            <asp:CheckBox ID="chkStatus" runat="server" CssClass="form-check-input me-2" />
                            <label class="form-check-label fw-bold mb-0">Status Active</label>
                        </div>
                    </div>
                </div>

                <div class="row mt-3">
                    <div class="col-12 text-center">
                        <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary px-5 fw-bold shadow-sm">
                            <i class="fas fa-save me-2"></i>Save Employee
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-secondary px-5 fw-bold shadow-sm ms-2" Visible="false">
                            <i class="fas fa-times me-2"></i>Clear/Cancel
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <div class="card shadow border-0">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0"><i class="fas fa-list me-2"></i>Employee List</h5>
            </div>
            <div class="card-body p-0">
                <div class="table-responsive">
                    <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="FCI_ID" OnRowDeleting="gvEmployees_RowDeleting"
                        OnRowCommand="gvEmployees_RowCommand" CssClass="table table-bordered table-hover mb-0">
                        <HeaderStyle CssClass="table-secondary text-center" />
                        <Columns>
                            <asp:BoundField DataField="FCI_ID" HeaderText="ID" ItemStyle-CssClass="text-center" />
                            <asp:BoundField DataField="Emp_Name" HeaderText="Name" />
                            <asp:BoundField DataField="Mobile_No" HeaderText="Mobile" ItemStyle-CssClass="text-center" />
                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                            <asp:BoundField DataField="DepotName" HeaderText="Branch" />
                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                            <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <span class='badge <%# (bool)Eval("Status") ? "bg-success" : "bg-danger" %>'>
                                        <%# (bool)Eval("Status") ? "Active" : "Inactive" %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditEmployee"
                                        CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-warning fw-bold">
                                        <i class="fas fa-edit"></i> Edit
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete"
                                        OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                        CssClass="btn btn-sm btn-danger fw-bold ms-1">
                                        <i class="fas fa-trash"></i> Delete
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>