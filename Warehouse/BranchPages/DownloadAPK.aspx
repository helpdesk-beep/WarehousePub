<%@ Page Title="APK Download" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master"
    AutoEventWireup="true" CodeFile="~/BranchPages/DownloadAPK.aspx.cs" Inherits="BranchPages_DownloadAPK" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>APK Download</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
                background: linear-gradient(135deg, #fecd4f2b, #e8e9aec7);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            min-height: 100vh;
        }

        .card-custom {
            max-width: 900px;
            width: 100%;
            border-radius: 25px;
            background: rgba(255,255,255,0.95);
            padding: 40px;
            box-shadow: 0 12px 30px rgba(0,0,0,0.25);
            margin-top: 50px;
        }

        h3 {
            font-weight: 700;
            color: #2c3e50;
            text-align: center;
            margin-bottom: 30px;
        }

        .form-label {
            width: 180px;
            font-weight: 600;
        }

        .form-control {
            max-width: 350px;
            border-radius: 10px;
        }

        .radio-group {
            display: flex;
            gap: 30px;
        }

        .btn-modern {
            border-radius: 30px;
            padding: 12px 0;
            font-size: 18px;
            font-weight: 600;
            transition: 0.3s ease;
        }

        .btn-modern:hover {
            transform: scale(1.05);
        }

        .alert {
            border-radius: 12px;
        }

        .loader-overlay {
            position: fixed;
            top: 0; left: 0;
            width: 100%; height: 100%;
            background: rgba(0,0,0,0.65);
            display: none;
            z-index: 9999;
            backdrop-filter: blur(3px);
        }

        .loader-box {
            position: absolute;
            top: 50%; left: 50%;
            transform: translate(-50%, -50%);
            color: #fff;
            text-align: center;
            font-size: 20px;
        }

        .loader-spinner {
            border: 6px solid #eee;
            border-top: 6px solid #00d1ff;
            border-radius: 50%;
            width: 70px;
            height: 70px;
            animation: spin 1s linear infinite;
            margin: 0 auto 15px auto;
        }

        @keyframes spin { 100% { transform: rotate(360deg); } }

        /* Responsive Image */
        .apk-image {
            max-width: 100%;
            border-radius: 20px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.2);
        }

        @media (max-width: 992px) {
            .row-custom {
                flex-direction: column-reverse;
            }
            .apk-image {
                margin-bottom: 30px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container d-flex justify-content-center">
        <div class="card-custom">

            <h3>📱 APK Download Portal</h3>

            <div class="row row-custom align-items-center">

                <!-- Left Side: Form -->
                <div class="col-lg-6">

                    <asp:Label runat="server" ID="lblMessage"
                        CssClass="d-block mb-3 text-center fw-bold text-danger"></asp:Label>

                    <!-- Login Panel -->
                    <asp:Panel runat="server" ID="pnlLogin">

                        <div class="mb-4 d-flex align-items-center">
                            <label class="form-label">Select App Type</label>
                            <div class="radio-group">
                                <asp:RadioButton runat="server" ID="radioInsp"
                                    Text="Inspection App" GroupName="radiobutton"
                                    Checked="true" AutoPostBack="true"
                                    OnCheckedChanged="radioInsp_CheckedChanged" />

                                <asp:RadioButton runat="server" ID="radioOther"
                                    Text="Godown App" GroupName="radiobutton"
                                    AutoPostBack="true"
                                    OnCheckedChanged="radioInsp_CheckedChanged" />
                            </div>
                        </div>

                        <div class="mb-4 d-flex align-items-center" id="phoneSection" runat="server">
                            <label class="form-label">Mobile Number</label>
                            <asp:TextBox runat="server" ID="txt_phoneNumber"
                                CssClass="form-control" placeholder="Enter Mobile Number"
                                TextMode="Number" MaxLength="10"
                                oninput="if(this.value.length > 10) this.value = this.value.slice(0,10);"></asp:TextBox>
                        </div>

                        <div class="mb-4 d-flex align-items-center" id="otpSection" runat="server" visible="false">
                            <label class="form-label">Enter OTP</label>
                            <asp:TextBox runat="server" ID="txt_otp"
                                CssClass="form-control" TextMode="Number"
                                MaxLength="4" placeholder="Enter OTP"></asp:TextBox>
                        </div>

                        <div class="d-grid mt-4 gap-3">
                            <asp:Button runat="server" ID="btnOtp" Text="Get OTP"
                                CssClass="btn btn-primary fw-bold btn-modern"
                                OnClick="btnOtp_Click" OnClientClick="showLoader();" />

                            <asp:Button runat="server" ID="btnVerify" Text="Verify OTP"
                                CssClass="btn btn-success fw-bold btn-modern"
                                Visible="false" OnClick="btnVerify_Click"
                                OnClientClick="showLoader();" />
                        </div>

                    </asp:Panel>

                    <!-- Inspection APK Panel -->
                    <asp:Panel runat="server" ID="pnlDownload" Visible="false" class="mt-4">

                        <div class="alert alert-success text-center mt-2 fw-bold">
                            ✔ Login Successful — Download Your APK
                        </div>

                        <h5 class="fw-bold">📌 App: Warehouse Inspection Module</h5>
                        <p>This APK is used for physical verification of godowns.</p>

                        <div class="d-grid">
                            <asp:Button runat="server" ID="btnInspDownload" Text="⬇ Download Inspection APK"
                                CssClass="btn btn-warning fw-bold btn-modern" OnClick="btnInspDownload_Click" />
                        </div>

                    </asp:Panel>

                    <!-- Godown APK Panel -->
                    <asp:Panel runat="server" ID="panelgodownapk" Visible="false" class="mt-4">

                        <div class="alert alert-success text-center mt-2 fw-bold">
                            ✔ Login Successful — Download Your APK
                        </div>

                        <h5 class="fw-bold">📌 App: Warehouse / Godown Module</h5>
                        <p>This APK is used for Godown Management operations.</p>

                        <div class="d-grid">
                            <asp:Button runat="server" ID="btn_godowndownload"
                                Text="⬇ Download Godown APK"
                                CssClass="btn btn-info fw-bold btn-modern" OnClick="btn_godowndownload_Click" />
                        </div>

                    </asp:Panel>

                </div>

                <!-- Right Side: APK Image -->
                <div class="col-lg-6 text-center">
                    <img src="../images/mobileAPK.png" alt="APK Download" class="apk-image">
                </div>

            </div>
        </div>
    </div>

    <!-- Loader -->
    <div id="loader" class="loader-overlay">
        <div class="loader-box">
            <div class="loader-spinner"></div>
            Processing...
        </div>
    </div>

    <script>
        function showLoader() {
            document.getElementById("loader").style.display = "block";
        }
    </script>

</asp:Content>
