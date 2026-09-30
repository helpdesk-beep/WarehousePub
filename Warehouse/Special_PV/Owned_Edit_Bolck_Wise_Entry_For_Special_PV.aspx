<%@ Page Title="" Language="C#" MasterPageFile="~/Special_PV/Special_PV.master" AutoEventWireup="true" CodeFile="~/Special_PV/Owned_Edit_Bolck_Wise_Entry_For_Special_PV.aspx.cs" Inherits="Special_PV_Owned_Edit_Bolck_Wise_Entry_For_Special_PV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
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

        .auto-style1 {
            height: 10px;
            width: 558px;
        }

        .auto-style2 {
            width: 558px;
        }
    </style>
    <style type="text/css">
        .left, .right {
            float: left;
            width: 20%; /* The width is 20%, by default */
        }

        .main {
            float: left;
            width: 60%; /* The width is 60%, by default */
        }

        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
        }
    </style>
    <style type="text/css">
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
    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="background-color: #FDFAF7; width: 100%;">
        <table border="1" width="70%">
            <tbody>
                <th style="text-align: center;">गोदाम का नाम</th>
                <th style="text-align: center;">जमाकर्ता का नाम </th>
                <th style="text-align: center;">स्कंध का नाम </th>
                <th style="text-align: center;">स्टैक आईडी</th>
                <th style="text-align: center;">स्टैक नाम </th>
                <th style="text-align: center;">बोरी</th>
                <th style="text-align: center;">वजन</th>
                <th style="text-align: center;">वर्ष</th>
            </tbody>
            <tr align="center">
                <td>
                    <asp:Label ID="lblgodownname" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbldepositername" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblcommodityname" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblstackid" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblstackname" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblnoofbags" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblweight" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblcropyear" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
            <tr style="margin-top: 10px">
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="8">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">स्टेक प्लानिंग बिछान </span>
                </td>
            </tr>
            <tr style="margin-top: 10px">
                <td align="left" colspan="8">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">कोई भी फील्ड को खाली नहीं छोड़े , यदि कोई जानकारी नहीं हैं तो शून्य अवश्य डाले </span>
                </td>
            </tr>
            <tr style="margin-top: 10px">
                <td>
                    <asp:Label ID="Label9" runat="server" Text="लम्बाई : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLendth" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" OnTextChanged="txtLendth_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="चौड़ाई : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtwidth" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" OnTextChanged="txtwidth_TextChanged" AutoPostBack="true"></asp:TextBox>

                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="अतिरिक्त : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtextralendth" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtextralendth_TextChanged"></asp:TextBox>
                </td>

                <td>
                    <asp:Label ID="Label3" runat="server" Text="योग (ल. + चौ.+अति.): "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr style="margin-top: 10px">
                <td>
                    <asp:Label ID="Label5" runat="server" Text="बोरो के लेयर की ऊंचाई : "></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="txtheight" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" OnTextChanged="txtheight_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="ब्लॉक क्र./संख्या : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtnoofblock" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtnoofblock_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="बोरियो की संख्या (योग*बोरो के लेयर की ऊंचाई*ब्लॉक क्र./संख्या) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="lbltotalbags" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr style="margin-top: 10px">
                <td align="left" colspan="8">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">अतिरिक्त पाई गई बोरियो की संख्या </span>
                </td>
            </tr>
            <tr style="margin-top: 10px">
                <td>
                    <asp:Label ID="Label8" runat="server" Text="ऊपर : "></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="txtup" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" OnTextChanged="txtup_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="निचे : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtbelow" runat="server" CssClass="form-control" AutoPostBack="true" onkeypress="return isNumberKey(event)" OnTextChanged="txtbelow_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="टोटल बौरे : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotalnoofbags" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr style="margin-top: 10px">
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Spillage Bag : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtspillagebag" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Remark : "></asp:Label>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtremark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr style="margin-top: 10px">
                <td colspan="10" align="center">
                    <asp:Button class="button button2" ID="btnupdate" runat="server" Text="Update"
                        TabIndex="11" CssClass="btn btn-warning" OnClick="btnupdate_Click"></asp:Button>
                    <asp:Label ID="Label79" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="row">
            <div class="col-lg-12">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57)) {
                alert("This field will not accept the alphabet, Please Enter Only number");
                return false;
            }
            return true;
        }
        //
    </script>
</asp:Content>

