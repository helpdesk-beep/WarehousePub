<%@ Page Title="Register Employee for Moisture/Fumigation" Language="C#" 
    MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true"
    CodeFile="~/BranchPages/RegisterEmployeeMostureFumigation.aspx.cs"
    Inherits="BranchPages_RegisterEmployeeMostureFumigation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        fieldset {
            border: 1px solid #007bff;
            border-radius: 8px;
            padding: 10px;
        }
        legend {
            font-weight: bold;
            color: #007bff;
        }
        .btn {
            border-radius: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper col-md-12">
        <fieldset>
            <legend>Register Employee for Moisture / Fumigation</legend>

            <asp:HiddenField ID="hfID" runat="server" />

            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblName" runat="server" Text="Name:" Font-Bold="true"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>

                <div class="col-md-4">
                    <asp:Label ID="lblMobile" runat="server" Text="Mobile No:" Font-Bold="true"></asp:Label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                </div>

                <div class="col-md-4">
                    <asp:Label ID="lblDesignation" runat="server" Text="Designation:" Font-Bold="true"></asp:Label>
                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col-md-2">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success w-100" Text="Save" OnClick="btnSave_Click" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary w-100" Text="
                        
                        
                        " Visible="false" OnClick="btnUpdate_Click" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary w-100" Text="Cancel" Visible="false" OnClick="btnCancel_Click" />
                </div>
            </div>
        </fieldset>

        <fieldset class="mt-4">
            <legend>Existing Employee Records</legend>
            <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False"
                CssClass="table table-bordered table-hover" OnRowCommand="gvEmployee_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditRecord"
                                CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-info" />
                            &nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteRecord"
                                CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-danger"
                                OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
