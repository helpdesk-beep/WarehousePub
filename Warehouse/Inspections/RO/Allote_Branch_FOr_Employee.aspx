<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="~/Inspections/RO/Allote_Branch_FOr_Employee.aspx.cs" Inherits="Inspections_RO_Allote_Branch_FOr_Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Inventory/assets/plugins/select2/css/select2-bootstrap4.css" rel="stylesheet" />
    <script src="../../assets/plugins/select2/js/select2.min.js"></script>
    <script src="../../Inventory/assets/plugins/select2/js/select2.min.js"></script>
    <script type="text/javascript">
        window.history.forward();

        function noBack() { window.history.forward(); }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <fieldset>
            <legend>Allot Branch Inspection</legend>
            <div class="row" style="margin-top: 20px">
                <div class="col-md-2">
                    <asp:Label ID="Label13" runat="server" Text="Inspection Type : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtinspectiontype" runat="server" CssClass="form-control" class="text" type="text" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label14" runat="server" Text="Allot Month :"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtallotMonth" runat="server" class="text" CssClass="form-control" type="text" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label7" runat="server" Text="Verification Type : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtverificationtype" runat="server" CssClass="form-control" class="text" type="text" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 20px">
                <div class="col-md-2">
                    <asp:Label ID="Label6" runat="server" Text="Region : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtregion" runat="server" CssClass="form-control" class="text" type="text" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label15" runat="server" Text="District : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtdistrict" runat="server" CssClass="form-control" class="text" type="text" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label16" runat="server" Text="Branch : "></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtbranch" runat="server" CssClass="form-control" class="text" type="text" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 20px">
                <div class="col-md-2">
                    <asp:Label ID="lbloffi" runat="server">Officer Name :<i style="color: red;">*</i></asp:Label>
                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                        ErrorMessage="Select Employe Name" ToolTip="Enter Employee Name" Text="<i class='fa fa-exclamation-circle' title='Enter Employee Name !'></i>"
                        ControlToValidate="ddlemployee" InitialValue="0" CssClass="fa fa-pull-right" ForeColor="Red" Display="Dynamic" runat="server">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlemployee" runat="server" AutoPostBack="true"
                        ForeColor="Navy" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" Text="Financial Year :"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtfinancial" runat="server" CssClass="form-control" class="text" type="text" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 20px">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button CssClass="btn btn-success btn-block" ValidationGroup="a" ID="btn_saveAllote_Branch" runat="server" Text="Submit"
                        OnClick="btn_saveAllote_Branch_Click"></asp:Button>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

