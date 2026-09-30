<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Inspections/State/Login_Special_PV.aspx.cs" Inherits="Inspections_State_Login_Special_PV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@3.4.1/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-1.12.4.min.js"></script>
</head>
<body class="bg-light">
    <form id="form1" runat="server">
      <div class="container" style="margin-top:80px; max-width:420px;">
    <div class="panel panel-primary">
        <div class="panel-heading text-center">
            <h4>Login</h4>
        </div>

        <div class="panel-body">

            <div class="form-group">
                <label>User Type</label>
                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                    <asp:ListItem Text="-- Select --" Value="" />
                    <asp:ListItem Text="Admin" Value="Admin" />
                    <asp:ListItem Text="Inspection Officer" Value="Officer" />
                </asp:DropDownList>
            </div>

            <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                <div class="form-group">
                    <label>Admin Password</label>
                    <asp:TextBox ID="txtAdminPassword" runat="server"
                        CssClass="form-control" TextMode="Password"
                        placeholder="Enter admin password" />
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlOfficer" runat="server" Visible="false">
                <div class="form-group">
                    <label>Mobile Number</label>
                    <asp:TextBox ID="txtMobile" runat="server"
                        CssClass="form-control"
                        placeholder="Enter mobile number"
                        MaxLength="10" />
                </div>
            </asp:Panel>

            <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

            <br />

            <asp:Button ID="btnLogin" runat="server"
                Text="Login"
                CssClass="btn btn-primary btn-block"
                OnClick="btnLogin_Click" />

        </div>
    </div>
</div>
    </form>
</body>
</html>

