<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Admin_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <h4>Change Password</h4>
    <hr />
    <div class="form-horizontal col-md-6">

        <div class="form-group">
            <div class="col-sm-8 col-sm-offset-4">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
        </div>

        <div class="form-group">
            <label class="col-sm-4 control-label">Current Password</label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtCurPass" TextMode="Password" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtCurPass" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>

            </div>
        </div>


        <div class="form-group">
            <label class="col-sm-4 control-label">New Password</label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtNewPass" TextMode="Password" runat="server" MaxLength="16"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ControlToValidate="txtNewPass" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>--%>
            </div>
            <div>
                <asp:RegularExpressionValidator ID="Regex4" runat="server" ControlToValidate="txtNewPass" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}$" ErrorMessage="Password must contain: Minimum 8 and Maximum 10 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" ForeColor="Red" ValidationGroup="A"/>
            </div>
        </div>


        <div class="form-group">
            <label class="col-sm-4 control-label">Confirm Password</label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtConPass" TextMode="Password" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtConPass" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                <asp:CompareValidator ControlToValidate="txtConPass" ControlToCompare="txtNewPass" ValidationGroup="A" runat="server" ErrorMessage="Password does not match"></asp:CompareValidator>

            </div>
        </div>

        <div class="form-group">
            <div class="col-sm-offset-4 col-sm-8">
                <asp:Button ID="btnSave" CssClass="btn btn-info" ValidationGroup="A" runat="server" Text="SAVE"
                    OnClick="btnSave_Click" />

                <asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server"
                    Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contbootm" runat="Server">
</asp:Content>

