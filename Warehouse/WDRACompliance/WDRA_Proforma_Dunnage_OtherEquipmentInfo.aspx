<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/WDRACompliance/WDRA_Proforma_Dunnage_OtherEquipmentInfo.aspx.cs" Inherits="WDRACompliance_WDRA_Proforma_Dunnage_OtherEquipmentInfo" %>

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
            <legend>WDRA Proforma Dunnage and Other Equipment Details CheckList </legend>
            <div class="row" style="margin-top: 10px">

                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblTarpaulin" Font-Bold="true" runat="server" ForeColor="Navy">Number of Tarpaulin:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtTarpaulin" runat="server" CssClass="form-control" Placeholder="अनुबंध दिनांक दर्ज करे" AutoPostBack="true"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblLadder" Font-Bold="true" runat="server" ForeColor="Navy">Number of Ladder:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtLadder" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblFirstAidbox" Font-Bold="true" runat="server" ForeColor="Navy">Number of First Aid box:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtFirstAidbox" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblPlatformScales" Font-Bold="true" runat="server" ForeColor="Navy">Number of Platform Scales:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPlatformScales" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblGumBoots" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Gum Boots:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtGumBoots" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblGoggles" Font-Bold="true" runat="server" ForeColor="Navy">Number of Goggles:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtGoggles" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblGasMask" Font-Bold="true" runat="server" ForeColor="Navy">Number of Gas Mask:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtGasMask" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblCanister" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Canister:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtCanister" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblPolytheneFilm" Font-Bold="true" runat="server" ForeColor="Navy">Number of PolytheneFilm:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPolytheneFilm" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblBambooMats" Font-Bold="true" runat="server" ForeColor="Navy">Number of Bamboo Mats:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtBambooMats" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblWoodenCrates" Font-Bold="true" runat="server" ForeColor="Navy">Number of Wooden Crates:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWoodenCrates" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>


                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblHectoliterWeightApparatus" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Hectoliter Weight Apparatus:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtHectoliterWeightApparatus" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>


            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblSampleDivider" Font-Bold="true" runat="server" ForeColor="Navy">Number of Sample Divider:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSampleDivider" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblVernierCaliper" Font-Bold="true" runat="server" ForeColor="Navy">Number of Vernier Caliper:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtVernierCaliper" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblThermohygrometer" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Thermohygrometer:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtThermohygrometer" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblFilterPapers" Font-Bold="true" runat="server" ForeColor="Navy">Number of Filter Papers:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtFilterPapers" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblSpecimenTubes" Font-Bold="true" runat="server" ForeColor="Navy">Number of Specimen Tubes:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSpecimenTubes" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblMetalProbe" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Metal Probe:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtMetalProbe" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>


            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblPhosphineAlertPersonalMonitor" Font-Bold="true" runat="server" ForeColor="Navy">Number of Phosphine Alert Personal Monitor:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPhosphineAlertPersonalMonitor" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblPhosphineGasMonitor" Font-Bold="true" runat="server" ForeColor="Navy">Number of Phosphine Gas Monitor:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPhosphineGasMonitor" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblToolBox" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of ToolBox:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtToolBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblDustMask" Font-Bold="true" runat="server" ForeColor="Navy">Number of DustMask:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtDustMask" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblAprons" Font-Bold="true" runat="server" ForeColor="Navy">Number of Aprons:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtAprons" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblResuscitator" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of Resuscitator:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtResuscitator" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblSCBA" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">Number of SCBA:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSCBA" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>


            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button ID="btnClkBack" runat="server" Text="Back" CssClass="btn-danger" OnClick="btnClkBack_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-success" Enabled="true" OnClick="btnSubmit_Click" />
                </div>
               <div class="col-md-1">
                    <asp:Button ID="btnClkNext" runat="server" Text="Next" CssClass="btn-danger" OnClick="btnClkNext_Click" />
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
                                    <asp:TemplateField HeaderText="SN" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tarpaulin">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTarpaulin" Enabled="false" runat="server" Text='<%# Eval("Tarpaulin") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ladder">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLadder" Enabled="false" runat="server" Text='<%# Eval("Ladder") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FirstAidbox">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFirstAidbox" Enabled="false" runat="server" Text='<%# Eval("FirstAidbox") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PlatformScales">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlatformScales" Enabled="false" runat="server" Text='<%# Eval("PlatformScales") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GumBoots">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGumBoots" Enabled="false" runat="server" Text='<%# Eval("GumBoots") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Goggles">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGoggles" Enabled="false" runat="server" Text='<%# Eval("Goggles") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GasMask">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGasMask" Enabled="false" runat="server" Text='<%# Eval("GasMask") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Canister">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCanister" Enabled="false" runat="server" Text='<%# Eval("Canister") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PolytheneFilm">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPolytheneFilm" Enabled="false" runat="server" Text='<%# Eval("PolytheneFilm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="BambooMats">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBambooMats" Enabled="false" runat="server" Text='<%# Eval("BambooMats") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WoodenCrates">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWoodenCrates" Enabled="false" runat="server" Text='<%# Eval("WoodenCrates") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HectoliterWeightApparatus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHectoliterWeightApparatus" Enabled="false" runat="server" Text='<%# Eval("HectoliterWeightApparatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SampleDivider">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampleDivider" Enabled="false" runat="server" Text='<%# Eval("SampleDivider") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VernierCaliper">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVernierCaliper" Enabled="false" runat="server" Text='<%# Eval("VernierCaliper") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Thermohygrometer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblThermohygrometer" Enabled="false" runat="server" Text='<%# Eval("Thermohygrometer") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FilterPapers">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFilterPapers" Enabled="false" runat="server" Text='<%# Eval("FilterPapers") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SpecimenTubes">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecimenTubes" Enabled="false" runat="server" Text='<%# Eval("SpecimenTubes") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MetalProbe">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMetalProbe" Enabled="false" runat="server" Text='<%# Eval("MetalProbe") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PhosphineAlertPersonalMonitor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhosphineAlertPersonalMonitor" Enabled="false" runat="server" Text='<%# Eval("PhosphineAlertPersonalMonitor") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PhosphineGasMonitor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhosphineGasMonitor" Enabled="false" runat="server" Text='<%# Eval("PhosphineGasMonitor") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ToolBox">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToolBox" Enabled="false" runat="server" Text='<%# Eval("ToolBox") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DustMask">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDustMask" Enabled="false" runat="server" Text='<%# Eval("DustMask") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aprons">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAprons" Enabled="false" runat="server" Text='<%# Eval("Aprons") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resuscitator">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResuscitator" Enabled="false" runat="server" Text='<%# Eval("Resuscitator") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SCBA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSCBA" Enabled="false" runat="server" Text='<%# Eval("SCBA") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" Enabled="false" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
