<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadNonWdraLicense.aspx.cs" Inherits="JointVentureScheme_UploadNonWdraLicense" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Warehouse Data Portal</title>
    <style>
        body { font-family: 'Segoe UI', Arial; background-color: #f4f7f6; margin: 0; }
        .navbar { background-color: #2c3e50; padding: 10px; color: white; display: flex; justify-content: space-between; align-items: center; }
        .main-content { display: flex; justify-content: center; align-items: center; min-height: 80vh; padding-top: 20px; }
        .upload-container { background: #fff; padding: 35px; border-radius: 12px; box-shadow: 0 10px 25px rgba(0,0,0,0.1); width: 500px; text-align: center; }
        .type-selector { margin-bottom: 25px; padding: 15px; background: #ebf2f7; border-radius: 8px; border: 1px solid #cfe0eb; }
        .file-input-wrapper { margin: 20px 0; border: 2px dashed #3498db; padding: 25px; border-radius: 8px; background: #fafafa; }
        .btn-upload { background-color: #27ae60; color: white; border: none; padding: 14px; border-radius: 6px; font-size: 16px; font-weight: bold; cursor: pointer; width: 100%; transition: 0.3s; }
        .btn-upload:hover { background-color: #219150; transform: translateY(-1px); }
        .status-message { margin-top: 20px; display: block; font-size: 15px; line-height: 1.5; }
        .text-white { color: white !important; text-decoration: none; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar">
            <div>
                <asp:LinkButton ID="btnHome" runat="server" CssClass="text-white" OnClick="btnHome_Click" ToolTip="Home">
                   🏠 Home
                </asp:LinkButton>
                <span style="margin-left: 15px; font-weight: bold;">Warehouse Data Import</span>
            </div>
            <div>
                Welcome, <b><asp:Label ID="lblUser" runat="server"></asp:Label></b> |
                <asp:LinkButton ID="lnkLogout" runat="server" CssClass="text-white" OnClick="lnkLogout_Click" ToolTip="Logout">
                    Logout 🚪
                </asp:LinkButton>
            </div>
        </nav>

        <div class="main-content">
            <div class="upload-container">
                <h2 style="color: #2c3e50; margin-top: 0;">Import Warehouse Data</h2>
                
                <div class="type-selector">
                    <strong style="display:block; margin-bottom:10px; color: #34495e;">Select Category:</strong>
                    <asp:RadioButtonList ID="rbListType" runat="server" RepeatDirection="Horizontal" style="margin: 0 auto;">
                        <asp:ListItem Text="Non-WDRA" Value="NON" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="WDRA" Value="WDRA" style="margin-left:30px;"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>

                <div class="file-input-wrapper">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>

                <asp:Button ID="btnUpload" runat="server" Text="Upload & Process Data" OnClick="btnUpload_Click" CssClass="btn-upload" />
                
                <asp:Label ID="lblMessage" runat="server" CssClass="status-message"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>