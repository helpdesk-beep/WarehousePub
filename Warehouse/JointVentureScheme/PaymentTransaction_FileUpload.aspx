<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentTransaction_FileUpload.aspx.cs" Inherits="JointVentureScheme_PaymentTransaction_FileUpload" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Payment Upload | Joint Venture Scheme</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    
    <style>
        body { background-color: #f4f7f6; font-family: 'Segoe UI', Arial, sans-serif; margin: 0; padding: 0; }
        .header-strip { background: #008CBA; color: white; padding: 10px 20px; display: flex; justify-content: space-between; align-items: center; box-shadow: 0 2px 5px rgba(0,0,0,0.1); }
        .nav-link { color: white; text-decoration: none; font-weight: bold; padding: 5px 10px; border-radius: 4px; transition: 0.3s; }
        .nav-link:hover { background: rgba(255,255,255,0.2); }
        
        .main-container { max-width: 600px; margin: 50px auto; padding: 20px; }
        .upload-card { background: white; border-radius: 8px; box-shadow: 0 4px 15px rgba(0,0,0,0.1); padding: 30px; text-align: center; border-top: 5px solid #008CBA; }
        .upload-card h2 { color: #333; margin-bottom: 25px; font-size: 20px; }
        
        .file-input-wrapper { margin: 20px 0; padding: 20px; border: 2px dashed #ddd; border-radius: 8px; background: #fafafa; }
        .btn-upload { background-color: #008CBA; color: white; border: none; padding: 12px 40px; border-radius: 5px; cursor: pointer; font-size: 16px; font-weight: bold; transition: 0.3s; }
        .btn-upload:hover { background-color: #005f7a; transform: translateY(-1px); }
        
        .msg-label { display: block; margin-top: 20px; padding: 10px; border-radius: 4px; font-size: 14px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header-strip">
            <div>
                <asp:LinkButton ID="LinkButton10" runat="server" PostBackUrl="~/JointVentureScheme/DistrictWiseJVSOffer.aspx" CssClass="nav-link">
                    <i class="fa fa-home"></i> Home
                </asp:LinkButton>
            </div>
            <div>
                <i class="fa fa-user-circle"></i> Welcome <asp:Label ID="lbluser" runat="server" Font-Bold="true"></asp:Label>
            </div>
            <div>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="nav-link">
                    <i class="fa fa-sign-out"></i> Logout
                </asp:LinkButton>
            </div>
        </div>

        <div class="main-container">
            <div class="upload-card">
                <h2><i class="fa fa-cloud-upload"></i> Payment Status File Upload</h2>
                
                <asp:Label ID="lblmsg" runat="server" CssClass="msg-label"></asp:Label>

                <div class="file-input-wrapper">
                    <asp:FileUpload ID="upFile" runat="server" />
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="upFile" 
                        ValidationGroup="A" ErrorMessage="Please select a file" ForeColor="Red" runat="server" Display="Dynamic" />
                </div>

                <asp:Button ID="btnUpload" runat="server" Text="Process Upload" 
                    CssClass="btn-upload" OnClick="btnUpload_Click" ValidationGroup="A" />
            </div>
        </div>
    </form>
</body>
</html>