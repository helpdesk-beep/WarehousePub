<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Secure Password Update</title>
    <style type="text/css">
        .sec-container {
            width: 500px;
            margin: 30px auto;
            font-family: Arial;
            border: 2px solid #DBAF75;
            padding: 20px;
            border-radius: 10px;
        }

        .sec-notice {
            background-color: #fcf8e3;
            border: 1px solid #faebcc;
            padding: 10px;
            font-size: 11px;
            color: #8a6d3b;
            margin-bottom: 20px;
        }

        .form-row {
            margin-bottom: 15px;
        }

        .lbl {
            display: inline-block;
            width: 150px;
            font-weight: bold;
        }

        .txt {
            width: 250px;
            height: 25px;
            border: 1px solid #ccc;
        }

        .btn {
            background-color: #BA8132;
            color: white;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
            width: 100%;
            font-weight: bold;
        }
        /* Container to hold the input and the eye icon */
        .pwd-wrapper {
            position: relative;
            display: inline-block;
        }

        /* Adjust the textbox to make room for the icon */
        .txt-pwd {
            padding-right: 35px; /* Space for the eye icon */
        }

        /* Style for the eye icon/button */
        .toggle-password {
            position: absolute;
            right: 10px;
            top: 50%;
            transform: translateY(-50%);
            cursor: pointer;
            font-size: 14px;
            user-select: none;
        }
    </style>
    <script type="text/javascript">
        function toggleVisibility(id) {
            // In ASP.NET, the ClientID might be different, but for simple setups 'id' works
            var input = document.getElementById(id);
            var icon = input.nextElementSibling;

            if (input.type === "password") {
                input.type = "text";
                icon.textContent = "🙈"; // Icon when password is visible
            } else {
                input.type = "password";
                icon.textContent = "👁️"; // Icon when password is hidden
            }
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="sec-container">
            <div class="sec-notice">
                <strong>🔒 Security Guidelines:</strong>
                <ul>
                    <li>Ensure the URL starts with <strong>https</strong>. </li>
                    <li>Password must be at least <strong>15 characters</strong>. </li>
                    <li>Must be <strong>alphanumeric</strong> and <strong>non-sequential</strong> (no "abc" or "123"). </li>
                    <li>Never share your password or OTP with anyone. </li>
                </ul>
            </div>
            <h2 style="text-align: center; color: #BA8132;">Update Password</h2>

            <%--<div class="form-row">
            <span class="lbl">OTP:</span>
            <asp:TextBox ID="txtOTP" runat="server" CssClass="txt" placeholder="Enter One-Time Password"></asp:TextBox>
        </div>--%>
            <div class="form-row">
                <span class="lbl">New Password:</span>
                <div class="pwd-wrapper">
                    <asp:TextBox ID="txtNewPwd" runat="server" CssClass="txt txt-pwd" TextMode="Password"></asp:TextBox>
                    <span class="toggle-password" onclick="toggleVisibility('txtNewPwd')">👁️</span>
                </div>
            </div>

            <div class="form-row">
                <span class="lbl">Confirm Password:</span>
                <div class="pwd-wrapper">
                    <asp:TextBox ID="txtConfirmPwd" runat="server" CssClass="txt txt-pwd" TextMode="Password"></asp:TextBox>
                    <span class="toggle-password" onclick="toggleVisibility('txtConfirmPwd')">👁️</span>
                </div>
            </div>
            <asp:Button ID="btnUpdate" runat="server" Text="Update Securely" CssClass="btn" OnClick="btnUpdate_Click" />
        </div>
    </form>
</body>
</html>
