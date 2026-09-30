<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/WDRACompliance/WDRA_Proforma_EquipmentDetails.aspx.cs" Inherits="WDRACompliance_WDRA_Proforma_EquipmentDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../assets/css/style.css" rel="stylesheet" />

    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }
    </style>

    <style>
        .left, .right {
            float: left;
            width: 20%; /* The width is 20%, by default */
        }

        .main {
            float: left;
            width: 60%; /* The width is 60%, by default */
        }

        /* Use a media query to add a breakpoint at 800px: */
        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
        }
    </style>


    <style>
        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
        }

        .content-wrapper {
            padding: 1.75rem 1.25rem;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #030203;
        }

        .form-control {
            border: 1px solid #767B83;
            border-radius: 8px;
        }

        .table th {
            text-align: center;
        }

        .form-inline {
            display: block !important;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #647e68 !important;
            color: black !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            color: black !important;
        }

        element.style {
            font-size: medium !important;
        }
    </style>
    <style>
        .menu {
            width: 25%;
            float: left;
        }

        .main {
            width: 75%;
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 15px">
                <div style="width: 100%; background-repeat: no-repeat;">
                    <div style="position: relative;">
                        <p style="text-align: start;">
                            <strong style="color: red">*सभी सामग्री की जानकारी/संख्या दर्ज करें| जो सामग्री उपलब्ध नहीं है, उसे ०/शुन्य दर्ज करें| :-
                            </strong>
                            <br />
                        </p>
                    </div>
                </div>
            </div>
    <div class="content-wrapper">
        <asp:Label runat="server" ID="lblMsg"></asp:Label>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>WDRA Proforma Equipment Details CheckList </legend>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblNewGodownName" Font-Bold="true" runat="server" ForeColor="Navy">गोदाम का नाम लिखें एवं Add Godown बटन पर क्लिक करें:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox CssClass="form-control select2" ID="txtNewGodownName" runat="server">
                    </asp:TextBox>
                </div>
                <div class="col-md-1">
                </div>
            </div>

            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblPhysicalBalance" Font-Bold="true" runat="server" ForeColor="Navy">Number of Physical Balance:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPhysicalBalance" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblCounterBalance" Font-Bold="true" runat="server" ForeColor="Navy">Number of Counter Balance:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtCounterBalance" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>



            </div>

            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblDigitalMoistureMeter" Font-Bold="true" runat="server" ForeColor="Navy">Number of Digital Moisture Meter:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtDigitalMoistureMeter" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblSieveSet" Font-Bold="true" runat="server" ForeColor="Navy">Number of SieveSet:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSieveSet" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>



            </div>

            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblScoops" Font-Bold="true" runat="server" ForeColor="Navy">Number of Scoops:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtScoops" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblForCepsBrushes" Font-Bold="true" runat="server" ForeColor="Navy">Number of ForCeps Brushes:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtForCepsBrushes" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>

                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWeightBox" Font-Bold="true" runat="server" ForeColor="Navy">Number of WeightBox:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWeightBox" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>

            </div>

            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblEnamelPlates" Font-Bold="true" runat="server" ForeColor="Navy">Number of EnamelPlates:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtEnamelPlates" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblSampleBagsPolythene" Font-Bold="true" runat="server" ForeColor="Navy">Number of SampleBags Polythene:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSampleBagsPolythene" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>

                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblSampleBagsCloth" Font-Bold="true" runat="server" ForeColor="Navy">Number of SampleBags Cloths:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSampleBagsCloth" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>

            </div>

            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblParkhiBagTrier" Font-Bold="true" runat="server" ForeColor="Navy">Number of Parkhi BagTrier:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtParkhiBagTrier" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblSampleSeal" Font-Bold="true" runat="server" ForeColor="Navy">Number of Sample Seal:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSampleSeal" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>

                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblMagnifyingGlassMagnification" Font-Bold="true" runat="server" ForeColor="Navy">Number of Magnifying Glass Magnification:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtMagnifyingGlassMagnification" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>

            </div>

            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblPetriDish" Font-Bold="true" runat="server" ForeColor="Navy">Number of Petri Dish:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPetriDish" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblMeasuringCylinders" Font-Bold="true" runat="server" ForeColor="Navy">Number of Measuring Cylinders:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtMeasuringCylinders" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblRecommendedPesticides" Font-Bold="true" runat="server" ForeColor="Navy">Number of Recommended Pesticides:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtRecommendedPesticides" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>

            </div>

            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblRatControl" Font-Bold="true" runat="server" ForeColor="Navy">Number of Rat Control:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtRatControl" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblRatCages" Font-Bold="true" runat="server" ForeColor="Navy">Number of Rat Cages:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtRatCages" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblThermoplasticFumigationCovers" Font-Bold="true" runat="server" ForeColor="Navy">Number of Thermoplastic Fumigation Covers:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtThermoplasticFumigationCovers" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblMultilayeredCLFumigationCovers" Font-Bold="true" runat="server" ForeColor="Navy">Number of Multilayered Cross Layered Fumigation Covers:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtMultilayeredCLFumigationCovers" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblFootHandSprayers" Font-Bold="true" runat="server" ForeColor="Navy">Number of Foot Hand Sprayers:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtFootHandSprayers" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblSandsnakes" Font-Bold="true" runat="server" ForeColor="Navy">Number of Sandsnakes:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSandsnakes" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblAdhesiveTape" Font-Bold="true" runat="server" ForeColor="Navy">Number of Adhesive Tape:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtAdhesiveTape" runat="server" CssClass="form-control" Placeholder="अनुबंधित क्षमता दर्ज करे"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblTarpaulin" Font-Bold="true" runat="server" ForeColor="Navy">Number of Tarpaulin:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtTarpaulin" runat="server" CssClass="form-control" Placeholder="अनुबंध दिनांक दर्ज करे" AutoPostBack="true"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblLadder" Font-Bold="true" runat="server" ForeColor="Navy">Number of Ladder:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtLadder" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblFirstAidbox" Font-Bold="true" runat="server" ForeColor="Navy">Number of First Aid box:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtFirstAidbox" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblPlatformScales" Font-Bold="true" runat="server" ForeColor="Navy">Number of Platform Scales:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPlatformScales" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblGumBoots" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Gum Boots:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtGumBoots" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblGoggles" Font-Bold="true" runat="server" ForeColor="Navy">Number of Goggles:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtGoggles" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblGasMask" Font-Bold="true" runat="server" ForeColor="Navy">Number of Gas Mask:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtGasMask" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblCanister" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Canister:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtCanister" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblPolytheneFilm" Font-Bold="true" runat="server" ForeColor="Navy">Number of PolytheneFilm:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPolytheneFilm" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblBambooMats" Font-Bold="true" runat="server" ForeColor="Navy">Number of Bamboo Mats:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtBambooMats" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWoodenCrates" Font-Bold="true" runat="server" ForeColor="Navy">Number of Wooden Crates:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWoodenCrates" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>


                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblHectoliterWeightApparatus" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Hectoliter Weight Apparatus:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtHectoliterWeightApparatus" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>


            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblSampleDivider" Font-Bold="true" runat="server" ForeColor="Navy">Number of Sample Divider:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSampleDivider" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblVernierCaliper" Font-Bold="true" runat="server" ForeColor="Navy">Number of Vernier Caliper:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtVernierCaliper" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblThermohygrometer" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Thermohygrometer:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtThermohygrometer" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblFilterPapers" Font-Bold="true" runat="server" ForeColor="Navy">Number of Filter Papers:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtFilterPapers" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblSpecimenTubes" Font-Bold="true" runat="server" ForeColor="Navy">Number of Specimen Tubes:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSpecimenTubes" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblMetalProbe" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Metal Probe:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtMetalProbe" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>


            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblPhosphineAlertPersonalMonitor" Font-Bold="true" runat="server" ForeColor="Navy">Number of Phosphine Alert Personal Monitor:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPhosphineAlertPersonalMonitor" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblPhosphineGasMonitor" Font-Bold="true" runat="server" ForeColor="Navy">Number of Phosphine Gas Monitor:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPhosphineGasMonitor" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblToolBox" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of ToolBox:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtToolBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblDustMask" Font-Bold="true" runat="server" ForeColor="Navy">Number of DustMask:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtDustMask" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblAprons" Font-Bold="true" runat="server" ForeColor="Navy">Number of Aprons:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtAprons" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblResuscitator" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Resuscitator:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtResuscitator" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblSCBA" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of SCBA:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSCBA" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-success" Enabled="true" OnClick="btnSubmit_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn-danger" />
                </div>
            </div>
        </fieldset>
        <div class="row" id="grdentry" style="margin-top: 20px">
            <fieldset>
                <legend>गोदाम प्रबंधक द्वारा दर्ज की गई जानकारी</legend>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GV_EntryDone" CellPadding="5" OnRowCommand="GV_EntryDone_RowCommand"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" autopostback="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष">
                                        <ItemTemplate>
                                            <asp:Label ID="txtFY" Enabled="false" runat="server" Text='<%# Eval("FinancialYear") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="गोदाम का नाम">
                                        <ItemTemplate>
                                            <asp:Label ID="txtEVGodownName" Enabled="false" runat="server" Text='<%# Eval("GodownName") %>'>0</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="गोदाम का प्रकार">
                                        <ItemTemplate>
                                            <asp:Label ID="txtGodownType" Enabled="false" runat="server" Text='<%# Eval("GodownType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अनुबंधित क्षमता">
                                        <ItemTemplate>
                                            W                               
                                            <asp:Label ID="lblAgreementCapacity" Enabled="false" runat="server" Text='<%# Eval("GodownAgreementCapacity") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अनुबंध दिनांक">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAgreementDate" Enabled="false" runat="server" Text='<%# Eval("GodownAgreementDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष में किराये की कुल राशि">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_GodownRentAmount" Enabled="false" runat="server" Text='<%# Eval("TotalAmountInRentInFY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष में भुगतान राशि">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_AmountPaymentToGodown" Enabled="false" runat="server" Text='<%# Eval("TotalAmountPaidToGodownInFY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="गोदाम संचालक की शेष लंबित राशि">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGodownOwnerPendingAmount" Enabled="false" runat="server" Text='<%# Eval("RemainingAmountOfGodownOwner") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अनुबंध समाप्ति दिनांक">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAgreementEndDate" Enabled="false" runat="server" Text='<%# Eval("AgreementEndDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" Enabled="false" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="RemoveRow" CssClass="btn-danger" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div align="center">No records found.</div>
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                    Height="20px" Font-Size="12pt" />
                                <AlternatingRowStyle BackColor="#eeeeee" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 32 && (charCode < 46 || charCode == 47 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript" src="../../assets/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript">
        $(".dateAdd").datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            changemonth: true,
            changeyear: true
        });
    </script>
</asp:Content>
