<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Branch_Wise_Godown_Mapping_With_Issue_Center_Details.aspx.cs" Inherits="BranchPages_Branch_Wise_Godown_Mapping_With_Issue_Center_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" rel="stylesheet">

    <style>
        body {
            background: linear-gradient(135deg,#eef2ff,#f8fafc);
        }

        .main-card {
            max-width: 1000px;
            margin: 40px auto;
            border: none;
            border-radius: 22px;
            overflow: hidden;
            background: #fff;
            box-shadow: 0 15px 40px rgba(0,0,0,.10);
        }

        .card-header-custom {
            background: linear-gradient(135deg,#0d6efd,#0047b3);
            padding: 25px;
            color: white;
        }

            .card-header-custom h3 {
                margin: 0;
                font-weight: 700;
                text-align: center;
                letter-spacing: .5px;
            }

        .card-body-custom {
            padding: 35px;
        }

        .form-label {
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 8px;
        }

        .input-group-text {
            background: #0d6efd;
            color: white;
            border: none;
        }

        .form-control,
        .form-select {
            height: 50px;
            border-radius: 10px;
            border: 1px solid #dce3ec;
        }

            .form-control:focus,
            .form-select:focus {
                box-shadow: 0 0 0 0.2rem rgba(13,110,253,.15);
                border-color: #0d6efd;
            }

        .note-box {
            background: #fff5f5;
            border-left: 6px solid #dc3545;
            padding: 15px 20px;
            border-radius: 10px;
            margin-top: 25px;
            margin-bottom: 25px;
        }

            .note-box span {
                color: #dc3545;
                font-weight: 700;
                font-size: 16px;
            }

        .btn-save {
            background: linear-gradient(135deg,#0d6efd,#0047b3);
            border: none;
            border-radius: 12px;
            min-width: 180px;
            height: 52px;
            font-size: 18px;
            font-weight: 600;
            transition: .3s;
        }

            .btn-save:hover {
                transform: translateY(-2px);
                box-shadow: 0 10px 20px rgba(13,110,253,.25);
            }

        .required {
            color: red;
        }

        .section-title {
            font-size: 18px;
            font-weight: 700;
            color: #0d6efd;
            margin-bottom: 20px;
            border-bottom: 2px solid #e9ecef;
            padding-bottom: 10px;
        }

        fieldset {
            border: 1px solid #d6e4f0;
            border-radius: 15px;
            padding: 25px 15px 15px 15px;
            margin-top: 25px;
            background: #ffffff;
            box-shadow: 0 4px 12px rgba(0,0,0,0.05);
        }

        legend {
            float: none;
            width: auto;
            margin-left: 15px;
            margin-bottom: 0;
            padding: 8px 18px;
            border: none;
            border-radius: 30px;
            background: linear-gradient(135deg,#0d6efd,#0047b3);
            color: #fff;
            font-size: 16px;
            font-weight: 600;
            box-shadow: 0 3px 8px rgba(13,110,253,.25);
        }

        .table {
            margin-bottom: 0px;
        }

            .table thead th {
                background: #eaf4ff;
                color: #1f2937;
                font-weight: 700;
                text-align: center;
                vertical-align: middle;
            }

            .table tbody td {
                vertical-align: middle;
            }

        .table-hover tbody tr:hover {
            background-color: #f5faff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="main-card">
            <div class="card-header-custom">
                <h3><i class="fas fa-map-marked-alt me-2"></i>Branch Wise Godown Mapping With Issue Center</h3>
            </div>
            <div class="card-body-custom">
                <div class="row">
                    <div class="col-md-6 mb-4">
                        <label class="form-label">Issue Center Name <span class="required">*</span></label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-building"></i></span>
                            <asp:DropDownList ID="ddlIssueCenter" runat="server" CssClass="form-select">
                                <asp:ListItem Text="-- Select Issue Center --" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6 mb-4">
                        <label class="form-label">Godown Name <span class="required">*</span></label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-warehouse"></i></span>
                            <asp:DropDownList ID="ddlGodown" runat="server" CssClass="form-select">
                                <asp:ListItem Text="-- Select Godown --" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="note-box">
                    <span>⚠️ * Issue Center जिस गोडाउन से संचालित है सिर्फ उसी गोडाउन को मैप करे।</span>
                </div>
                <div class="section-title">
                    <i class="fas fa-location-dot me-2"></i>Geo Location Details
                </div>
                <div class="row">
                    <div class="col-md-6 mb-4">
                        <label class="form-label">Latitude <span class="required">*</span></label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-map-pin"></i></span>
                            <asp:TextBox ID="txtLatitude" runat="server" CssClass="form-control" placeholder="Enter Latitude" onkeypress="return AllowLatLong(event)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6 mb-4">
                        <label class="form-label">Longitude <span class="required">*</span></label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-earth-asia"></i></span>
                            <asp:TextBox ID="txtLongitude" runat="server" CssClass="form-control" placeholder="Enter Longitude" onkeypress="return AllowLatLong(event)"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="text-center mt-4">
                    <asp:Button ID="btnSubmit" runat="server" Text="💾 Save Mapping" CssClass="btn btn-primary btn-save" OnClientClick="return validateForm();" OnClick="btnSubmit_Click" />
                </div>
                <div class="mt-5">
                    <fieldset>
                        <legend>Godown Mapped With Issue Centers</legend>
                        <asp:GridView ID="gvMapping" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="IssueCenterName" HeaderText="Issue Center" />
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
                                <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function AllowLatLong(evt) {

            var charCode = evt.which ? evt.which : evt.keyCode;
            var value = evt.target.value;

            // Numbers 0-9
            if (charCode >= 48 && charCode <= 57)
                return true;

            // Allow one decimal point
            if (charCode == 46 && value.indexOf('.') == -1)
                return true;

            // Allow Backspace, Tab, Delete, Arrow Keys
            if (charCode == 8 || charCode == 9 || charCode == 46 ||
                charCode == 37 || charCode == 39)
                return true;

            return false;
        }

    </script>
    <script type="text/javascript">

        function validateForm() {

            var issueCenter = document.getElementById('<%=ddlIssueCenter.ClientID%>').value;
            var godown = document.getElementById('<%=ddlGodown.ClientID%>').value;
            var lat = document.getElementById('<%=txtLatitude.ClientID%>').value.trim();
            var lng = document.getElementById('<%=txtLongitude.ClientID%>').value.trim();

            if (issueCenter == "0") {
                alert("Please select Issue Center.");
                document.getElementById('<%=ddlIssueCenter.ClientID%>').focus();
                return false;
            }

            if (godown == "0") {
                alert("Please select Godown.");
                document.getElementById('<%=ddlGodown.ClientID%>').focus();
                return false;
            }

            if (lat == "") {
                alert("Please enter Latitude.");
                document.getElementById('<%=txtLatitude.ClientID%>').focus();
                return false;
            }

            if (lng == "") {
                alert("Please enter Longitude.");
                document.getElementById('<%=txtLongitude.ClientID%>').focus();
                return false;
            }

            if (isNaN(lat)) {
                alert("Latitude must be numeric.");
                document.getElementById('<%=txtLatitude.ClientID%>').focus();
                return false;
            }

            if (isNaN(lng)) {
                alert("Longitude must be numeric.");
                document.getElementById('<%=txtLongitude.ClientID%>').focus();
                return false;
            }

            if (parseFloat(lat) < -90 || parseFloat(lat) > 90) {
                alert("Latitude should be between -90 and 90.");
                document.getElementById('<%=txtLatitude.ClientID%>').focus();
                return false;
            }

            if (parseFloat(lng) < -180 || parseFloat(lng) > 180) {
                alert("Longitude should be between -180 and 180.");
                document.getElementById('<%=txtLongitude.ClientID%>').focus();
                return false;
            }

            return confirm('Are you sure you want to save this mapping?');
        }

    </script>
</asp:Content>
