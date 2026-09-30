<%@ Page Title="" Language="C#" ViewStateEncryptionMode="Always" MasterPageFile="../MasterPages/adminMaster.master" AutoEventWireup="true" CodeFile="InsertSubCategory.aspx.cs" Inherits="Admin_InsertSubCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PM" runat="Server">
    <div class="alert dv_head">
        <label class="pg_head"><b>&nbsp;&nbsp;Insert Category</b></label>
    </div>
    <br />
    <div style="text-align: center" runat="server" id="dvErr">
        <asp:Label ID="lblErr" runat="server" CssClass="alert-danger" Font-Bold="true" Font-Size="Large"></asp:Label>
    </div>
    <br />
    <div>
        <div class=" container">
            <div class="row">
                <div class="col-lg-4" style="text-align: right;">
                    <span id="Label18" style="display: inline-block; font-size: medium">Select Category :</span>
                </div>
                <div class="col-lg-6">
                    <asp:DropDownList ID="ddlProgram" Width="100%" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="ddlProgram" ErrorMessage="Please Select Cetegory" ForeColor="#CC3300"
                        ValidationGroup="myValidator"></asp:RequiredFieldValidator>
                </div>
                <div class="col-lg-2"></div>
            </div>
            <div class="row">
                <div class="col-lg-4" style="text-align: right;">
                    <span id="Label9" style="display: inline-block; font-size: medium">Insert Sub Category in English :</span>
                </div>
                <div class="col-lg-6">
                    <asp:TextBox runat="server" ID="txtCategoryE" Width="100%" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-lg-2"></div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <center>
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" OnClick="btnSave_Click" Text="Save"
                        ValidationGroup="myValidator" /></center>
                </div>
            </div>
            <br />
            <br />
        </div>

    </div>
</asp:Content>

