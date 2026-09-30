<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Login/Nccf_Login.aspx.cs" Inherits="Login_Nccf_Login" %>

<!DOCTYPE html>
<%--<html>
<head runat="server">
    <title>Choose Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background: linear-gradient(120deg, #e6f2ff, #f0f4f8);
            height: 100vh;
            margin: 0;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .container {
            display: flex;
            background: #fff;
            border-radius: 14px;
            box-shadow: 0 8px 25px rgba(0, 0, 0, 0.15);
            overflow: hidden;
            width: 950px;
            max-width: 95%;
            min-height: 520px;
            flex-wrap: wrap; /* Allow wrapping of flex items */
        }

        /* IMAGE SECTION */
        .image-section {
            flex: 1.5;
            background-color: white;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            position: relative;
            overflow: hidden;
            background: transparent;
        }

            .image-section img {
                width: 100%;
                height: 100%;
                object-fit: cover;
                object-position: center center;
            }

        /* Vertical Line (Separate) */
        .vertical-line {
            width: 2px;
            height: 450px;
            border-left: 1px solid #b60e0e;
            border-right: 1px solid #b60e0e;
            border-top: 57px solid transparent;
            border-bottom: 58px solid transparent;
            margin: 30px auto;
        }

        /* LOGIN SECTION */
        .login-section {
            flex: 1;
            padding: 40px 35px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            text-align: center;
        }

        .logo {
            height: 100px;
            margin: 0 auto 20px;
            overflow: hidden;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
            background: transparent;
        }

            .logo img {
                width: 100%;
                height: 100%;
                object-fit: cover;
            }

        h2 {
            margin-bottom: 20px;
            color: #333;
            font-size: 22px;
        }

        .big-btn {
            display: block;
            width: 100%;
            padding: 14px;
            margin: 12px 0;
            border-radius: 8px;
            border: none;
            font-weight: bold;
            font-size: 16px;
            cursor: pointer;
            transition: 0.2s;
        }

        .business {
            background: #1e88e5;
            color: #fff;
        }

        .account {
            background: #43a047;
            color: #fff;
        }

        .big-btn:hover {
            opacity: 0.9;
            transform: scale(1.02);
        }

        /* MODAL */
        .modal-backdrop {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: rgba(0, 0, 0, 0.5);
            z-index: 1000;
            align-items: center;
            justify-content: center;
        }

        .modal {
            background: #fff;
            width: 400px;
            padding: 30px 25px;
            border-radius: 12px;
            box-shadow: 0 10px 35px rgba(0, 0, 0, 0.3);
            text-align: center;
            animation: fadeIn 0.3s ease-in-out;
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(-20px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .modal h3 {
            margin-top: 0;
            margin-bottom: 20px;
            font-size: 22px;
            color: #1e88e5;
        }

        .form-control {
            width: 100%;
            padding: 12px;
            margin: 10px 0 18px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 15px;
            box-sizing: border-box;
        }

        .modal-btn {
            padding: 10px 16px;
            border: none;
            border-radius: 6px;
            font-weight: bold;
            cursor: pointer;
        }

        .login-btn {
            background: #1e88e5;
            color: white;
        }

        .close-btn {
            background: #ddd;
            color: black;
            margin-left: 8px;
        }

        .error {
            color: red;
            font-size: 14px;
            margin-bottom: 10px;
        }

        /* RESPONSIVE */
        @media (max-width: 768px) {
            .container {
                flex-direction: column;
                width: 90%;
            }

            .image-section {
                height: 250px;
            }

            .vertical-line {
                display: none;
            }

            .logo img {
                max-width: 100%;
                height: auto;
            }

            h2 {
                font-size: 20px;
            }

            .big-btn {
                padding: 12px;
                font-size: 14px;
            }
        }

        @media (max-width: 480px) {
            /* For very small devices (320px to 480px) */
            .container {
                min-height: 450px; /* Ensure it fits the small screen */
            }

            .image-section {
                /*height: 180px;*/
                width: 180px;
            }

            .logo {
                /*height: 100px;*/
                width: 100px;
                margin-bottom: 15px;
            }

            .big-btn {
                padding: 10px;
                font-size: 14px;
            }

            .login-section {
                padding: 25px 20px;
            }

            h2 {
                font-size: 18px;
                margin-bottom: 15px;
            }

            .modal {
                width: 90%;
                padding: 20px;
            }

                .modal h3 {
                    font-size: 20px;
                    margin-bottom: 15px;
                }

            .form-control {
                font-size: 14px;
                padding: 10px;
            }

            .modal-btn {
                font-size: 14px;
                padding: 8px 12px;
            }
        }
    </style>
    <style>
        .modal-backdrop {
            display: none;
            position: fixed;
            inset: 0;
            background: rgba(0,0,0,0.45);
            align-items: center;
            justify-content: center;
            z-index: 9999;
        }

        .modal {
            background: #fff;
            padding: 20px;
            border-radius: 8px;
            width: 320px;
            box-shadow: 0 8px 24px rgba(0,0,0,0.15);
        }
    </style>
    <script>
        function openLoginModal(type) {
            document.getElementById('<%= hfLoginType.ClientID %>').value = type;
            document.getElementById('modalTitle').innerText = type + " Login";
            document.getElementById('<%= txtModalUser.ClientID %>').value = '';
            document.getElementById('<%= txtModalPass.ClientID %>').value = '';
            document.getElementById('<%= lblModalError.ClientID %>').innerText = '';
            document.getElementById('modalBackdrop').style.display = 'flex';
        }

        function closeModal() {
            document.getElementById('modalBackdrop').style.display = 'none';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- LEFT IMAGE SECTION -->
            <div class="image-section">
                <img src="../Images/LOGO-removebg-preview.png" alt="Warehouse Image">
            </div>

            <!-- VERTICAL LINE BETWEEN IMAGE AND LOGIN SECTION -->
            <div class="vertical-line"></div>

            <!-- RIGHT LOGIN SECTION -->
            <div class="login-section">
                <h2>NCCF Login</h2>
                <asp:Button ID="btnBusiness" runat="server" Text="Business Login"
                    CssClass="big-btn business"
                    OnClientClick="openLoginModal('Business'); return false;" />
                <asp:Button ID="btnAccount" runat="server" Text="Account Login"
                    CssClass="big-btn account"
                    OnClientClick="openLoginModal('Account'); return false;" />
            </div>
        </div>

        <!-- LOGIN MODAL -->
        <div id="modalBackdrop" class="modal-backdrop">
            <div class="modal" role="dialog" aria-modal="true">
                <h3 id="modalTitle">Login</h3>
                <asp:Label ID="lblModalError" runat="server" CssClass="error" />
                <asp:TextBox ID="txtModalUser" runat="server" CssClass="form-control"
                    Placeholder="User ID" ReadOnly="true" />
                <asp:TextBox ID="txtModalPass" runat="server" CssClass="form-control"
                    TextMode="Password" Placeholder="Password" />
                <asp:HiddenField ID="hfLoginType" runat="server" />
                <asp:Label ID="Label1" runat="server" CssClass="error" />
                <asp:Button ID="btnModalLogin" runat="server" Text="Login"
                    CssClass="modal-btn login-btn" OnClick="btnModalLogin_Click" />
            </div>
        </div>
        <script>
            function openLoginModal(type) {
                // Hidden field set करें
                var hf = document.getElementById('<%= hfLoginType.ClientID %>');
                if (hf) hf.value = type;

                // Error label clear करें ताकि previous server error न दिखे
                var lblErr = document.getElementById('<%= lblModalError.ClientID %>');
                if (lblErr) lblErr.innerText = '';

                // Username textbox fill only if empty
                var txtUser = document.getElementById('<%= txtModalUser.ClientID %>');
                if (txtUser) {
                    if (!txtUser.value || txtUser.value.trim() === '') {
                        txtUser.value = type + " User";
                    }
                    txtUser.readOnly = true;
                }

                // Title update
                var title = document.getElementById('modalTitle');
                if (title) title.innerText = type + " Login";

                // Show modal
                var backdrop = document.getElementById('modalBackdrop');
                if (backdrop) backdrop.style.display = 'flex';

                // Focus password field
                setTimeout(function () {
                    var txtPass = document.getElementById('<%= txtModalPass.ClientID %>');
                   if (txtPass) txtPass.focus();
               }, 100);
            }

            function closeModal() {
                var backdrop = document.getElementById('modalBackdrop');
                if (backdrop) backdrop.style.display = 'none';

                var hf = document.getElementById('<%= hfLoginType.ClientID %>');
                if (hf) hf.value = '';

                var lblErr = document.getElementById('<%= lblModalError.ClientID %>');
                if (lblErr) lblErr.innerText = '';
            }

            // Click outside modal to close
            document.addEventListener('click', function (e) {
                var backdrop = document.getElementById('modalBackdrop');
                var modal = backdrop ? backdrop.querySelector('.modal') : null;
                if (backdrop && modal && e.target === backdrop) {
                    closeModal();
                }
            });
        </script>
    </form>
</body>
</html>--%>
<html>
<head runat="server">
    <title>Choose Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background: linear-gradient(120deg, #e6f2ff, #f0f4f8);
            height: 100vh;
            margin: 0;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .container {
            display: flex;
            background: #fff;
            border-radius: 14px;
            box-shadow: 0 8px 25px rgba(0, 0, 0, 0.15);
            overflow: hidden;
            width: 950px;
            max-width: 95%;
            min-height: 520px;
            flex-wrap: wrap;
        }

        .image-section {
            flex: 1.5;
            background: transparent;
            display: flex;
            justify-content: center;
            align-items: center;
        }

            .image-section img {
                width: 100%;
                height: 100%;
                object-fit: cover;
            }

        .vertical-line {
            width: 2px;
            height: 450px;
            border-left: 1px solid #b60e0e;
            border-right: 1px solid #b60e0e;
            border-top: 57px solid transparent;
            border-bottom: 58px solid transparent;
            margin: 30px auto;
        }

        .login-section {
            flex: 1;
            padding: 40px 35px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            text-align: center;
        }

        h2 {
            margin-bottom: 20px;
            color: #333;
            font-size: 22px;
        }

        .big-btn {
            display: block;
            width: 100%;
            padding: 14px;
            margin: 12px 0;
            border-radius: 8px;
            border: none;
            font-weight: bold;
            font-size: 16px;
            cursor: pointer;
            transition: 0.2s;
        }

        .business {
            background: #1e88e5;
            color: #fff;
        }

        .account {
            background: #43a047;
            color: #fff;
        }

        .big-btn:hover {
            opacity: 0.9;
            transform: scale(1.02);
        }

        /* Modal */
        .modal-backdrop {
            display: none;
            position: fixed;
            inset: 0;
            background: rgba(0,0,0,0.45);
            align-items: center;
            justify-content: center;
            z-index: 9999;
        }

        .modal {
            background: #fff;
            padding: 20px;
            border-radius: 8px;
            width: 320px;
            box-shadow: 0 8px 24px rgba(0,0,0,0.15);
            text-align: center;
        }

            .modal h3 {
                color: #1e88e5;
                margin-bottom: 20px;
            }

        .form-control {
            width: 100%;
            padding: 12px;
            margin: 10px 0 18px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 15px;
            box-sizing: border-box;
        }

        .modal-btn {
            padding: 10px 16px;
            border: none;
            border-radius: 6px;
            font-weight: bold;
            cursor: pointer;
        }

        .login-btn {
            background: #1e88e5;
            color: white;
        }

        .close-btn {
            background: #ddd;
            color: black;
            margin-left: 8px;
        }

        .error {
            color: red;
            font-size: 14px;
            margin-bottom: 10px;
        }
    </style>

    <script>
        // --- Modified to include location ---
        function openLoginModal(type, location) {
            var hfType = document.getElementById('<%= hfLoginType.ClientID %>');
            var hfLoc = document.getElementById('<%= hfLocation.ClientID %>');
            if (hfType) hfType.value = type;
            if (hfLoc) hfLoc.value = location;

            var lblErr = document.getElementById('<%= lblModalError.ClientID %>');
            if (lblErr) lblErr.innerText = '';

            var txtUser = document.getElementById('<%= txtModalUser.ClientID %>');
            if (txtUser) {
                txtUser.value = location + " " + type; // Example: Indore Business
                txtUser.readOnly = true;
            }

            var title = document.getElementById('modalTitle');
            if (title) title.innerText = location + " " + type + " Login";

            var backdrop = document.getElementById('modalBackdrop');
            if (backdrop) backdrop.style.display = 'flex';

            setTimeout(function () {
                var txtPass = document.getElementById('<%= txtModalPass.ClientID %>');
                if (txtPass) txtPass.focus();
            }, 100);
        }

        function closeModal() {
            var backdrop = document.getElementById('modalBackdrop');
            if (backdrop) backdrop.style.display = 'none';

            var hfType = document.getElementById('<%= hfLoginType.ClientID %>');
            var hfLoc = document.getElementById('<%= hfLocation.ClientID %>');
            if (hfType) hfType.value = '';
            if (hfLoc) hfLoc.value = '';

            var lblErr = document.getElementById('<%= lblModalError.ClientID %>');
            if (lblErr) lblErr.innerText = '';

            var txtUser = document.getElementById('<%= txtModalUser.ClientID %>');
            var txtPass = document.getElementById('<%= txtModalPass.ClientID %>');
            if (txtUser) txtUser.value = '';
            if (txtPass) txtPass.value = '';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="image-section">
                <img src="../Images/LOGO-removebg-preview.png" alt="Warehouse Image" />
            </div>

            <div class="vertical-line"></div>

            <div class="login-section">
                <h2>NCCF Login</h2>
                <!-- For Indore -->
                <div id="divIndore" runat="server" visible="false">
                    <asp:Button ID="btnIndoreBusiness" runat="server" Text="Indore Business"
                        CssClass="big-btn business"
                        OnClientClick="openLoginModal('Business','Indore'); return false;" />
                    <asp:Button ID="btnIndoreAccount" runat="server" Text="Indore Account"
                        CssClass="big-btn account"
                        OnClientClick="openLoginModal('Account','Indore'); return false;" />
                </div>
                <!-- For Bhopal -->
                <div id="divBhopal" runat="server" visible="false">
                    <asp:Button ID="btnBhopalBusiness" runat="server" Text="Bhopal Business"
                        CssClass="big-btn business"
                        OnClientClick="openLoginModal('Business','Bhopal'); return false;" />
                    <asp:Button ID="btnBhopalAccount" runat="server" Text="Bhopal Account"
                        CssClass="big-btn account"
                        OnClientClick="openLoginModal('Account','Bhopal'); return false;" />
                </div>
                <div style="text-align: right; margin-top: 10px;">
                    <!-- Back Button -->
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="big-btn back-btn"
                        OnClick="btnBack_Click" />
                </div>
            </div>
        </div>

        <!-- Modal -->
        <div id="modalBackdrop" class="modal-backdrop">
            <div class="modal" role="dialog" aria-modal="true">
                <h3 id="modalTitle">Login</h3>

                <asp:Label ID="lblModalError" runat="server" CssClass="error" />
                <asp:TextBox ID="txtModalUser" runat="server" CssClass="form-control"
                    Placeholder="User ID" />
                <asp:TextBox ID="txtModalPass" runat="server" CssClass="form-control"
                    TextMode="Password" Placeholder="Password" />
                <asp:HiddenField ID="hfLoginType" runat="server" />
                <asp:HiddenField ID="hfLocation" runat="server" />

                <div style="text-align: right; margin-top: 10px;">
                    <!-- Back button -->
                    <%--<input type="button" value="Back" class="modal-btn back-btn" onclick="goBackToPreviousLogin();" />--%>
                    <asp:Button ID="btnCloseModal" runat="server" Text="Close"
                        CssClass="modal-btn close-btn"
                        OnClientClick="closeModal(); return false;" />
                    <!-- Login button -->
                    <asp:Button ID="btnModalLogin" runat="server" Text="Login"
                        CssClass="modal-btn login-btn" OnClick="btnModalLogin_Click" />

                </div>
            </div>
        </div>
    </form>
</body>
</html>

