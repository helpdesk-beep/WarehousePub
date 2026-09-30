<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Markfed_Branch_Print_Bill.aspx.cs" Inherits="BranchPages_Markfed_Branch_Print_Bill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Markfed Rent Bill</title>

    <!-- jQuery first, then Select2 -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>

    <!-- CSS Files -->
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />

    <!-- Internal Styles -->
    <style type="text/css">
        /* Global Styles */
        body {
            background: #f8f9fa;
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
        }

        .content-wrapper {
            padding: 1.5rem;
            background: #fff;
            min-height: 100vh;
        }

        /* Fieldset Styling */
        fieldset {
            border: 2px solid #1e4d7c;
            padding: 20px;
            margin: 20px 0;
            border-radius: 10px;
            background: #fff;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }

        legend {
            width: auto;
            padding: 8px 20px;
            border: 2px solid #1e4d7c;
            border-radius: 25px;
            font-size: 16px;
            font-weight: bold;
            color: #1e4d7c;
            background: #fff;
            margin-bottom: 0;
        }

        /* Button Styling */
        .button {
            background-color: #4CAF50;
            border: none;
            color: white;
            padding: 8px 16px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 13px;
            font-weight: bold;
            margin: 4px 2px;
            cursor: pointer;
            border-radius: 5px;
            transition: all 0.3s ease;
        }

        .button1 {
            background-color: white;
            color: #1e4d7c;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
                border-color: #4CAF50;
            }

        .button2 {
            background-color: white;
            color: #1e4d7c;
            border: 2px solid #1e4d7c;
        }

            .button2:hover {
                background-color: #1e4d7c;
                color: white;
                border-color: #1e4d7c;
            }

        /* Select2 Customization */
        .select2-container--default .select2-selection--single {
            height: 38px;
            border: 2px solid #e1e5e9;
            border-radius: 6px;
        }

            .select2-container--default .select2-selection--single .select2-selection__rendered {
                line-height: 34px;
                padding-left: 12px;
            }

            .select2-container--default .select2-selection--single .select2-selection__arrow {
                height: 34px;
            }

        /* Ensure search box is always visible and properly styled */
        .select2-container--default .select2-search--dropdown .select2-search__field {
            border: 1px solid #aaa;
            padding: 8px;
            width: 100%;
            box-sizing: border-box;
            font-size: 14px;
            outline: none;
        }

            .select2-container--default .select2-search--dropdown .select2-search__field:focus {
                border-color: #1e4d7c;
                box-shadow: 0 0 5px rgba(30,77,124,0.3);
            }

        /* Prevent focus issues */
        .select2-container--open .select2-dropdown--below {
            z-index: 9999;
        }

        /* Ensure dropdown doesn't cause page jumps */
        .select2-container {
            z-index: 9999;
        }

        /* Form Controls */
        .form-control {
            border: 2px solid #e1e5e9;
            border-radius: 6px;
            height: 38px;
            font-size: 14px;
            transition: border-color 0.3s ease;
        }

            .form-control:focus {
                border-color: #1e4d7c;
                box-shadow: 0 0 5px rgba(30,77,124,0.2);
                outline: none;
            }

        /* Readonly Textbox Style */
        .readonly-textbox {
            background-color: #e9ecef !important;
            color: #2c3e50 !important;
            border: 2px solid #ced4da !important;
            cursor: default !important;
            opacity: 1 !important;
            pointer-events: none;
            user-select: none;
        }

            .readonly-textbox:hover {
                border-color: #adb5bd !important;
            }

        /* Labels */
        label {
            font-weight: 600;
            color: #2c3e50;
            margin-top: 8px;
            font-size: 14px;
        }

        /* GridView Styling */
        .gridview-container {
            overflow-x: auto;
            margin: 20px 0;
            border-radius: 8px;
            border: 1px solid #dee2e6;
        }

        .gridview-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 12px;
        }

            .gridview-table th {
                background: #1e4d7c;
                color: white;
                padding: 10px;
                font-weight: 600;
                text-align: center;
                border: 1px solid #2c5a8c;
            }

            .gridview-table td {
                padding: 8px;
                border: 1px solid #dee2e6;
                text-align: center;
            }

            .gridview-table tr:nth-child(even) {
                background: #f8f9fa;
            }

            .gridview-table tr:hover {
                background: #e9ecef;
            }

        .gridview-footer {
            background: #e8f0fe;
            font-weight: bold;
            color: #c00;
        }

        /* Responsive Layout */
        @media screen and (max-width: 768px) {
            .content-wrapper {
                padding: 1rem;
            }

            fieldset {
                padding: 15px;
                margin: 10px 0;
            }

            .col-md-1, .col-md-2, .col-md-3, .col-md-6 {
                margin-bottom: 10px;
            }

            label {
                margin-top: 0;
                margin-bottom: 5px;
            }

            .gridview-table {
                font-size: 11px;
            }

                .gridview-table th,
                .gridview-table td {
                    padding: 6px 4px;
                }

            .button {
                width: 100% !important;
                margin: 5px 0;
            }
        }

        @media screen and (max-width: 480px) {
            legend {
                font-size: 14px;
                padding: 6px 12px;
            }

            .form-control {
                font-size: 13px;
            }

            label {
                font-size: 13px;
            }
        }

        /* Print Bill Container */
        #PrintDiv {
            background: #fff;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 8px;
        }

            #PrintDiv table {
                width: 100%;
                border-collapse: collapse;
            }

            #PrintDiv td {
                padding: 5px;
            }

        /* Modal Styling */
        .modalBackground {
            background-color: rgba(0,0,0,0.6);
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            max-width: 500px;
            border: 3px solid #1e4d7c;
            border-radius: 10px;
            padding: 0;
            box-shadow: 0 5px 15px rgba(0,0,0,0.3);
        }

            .modalPopup .header {
                background: linear-gradient(135deg, #1e4d7c 0%, #2a5f94 100%);
                height: 40px;
                color: black;
                line-height: 40px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 7px;
                border-top-right-radius: 7px;
            }

            .modalPopup .body {
                min-height: 60px;
                padding: 20px;
                text-align: center;
                font-size: 16px;
            }

            .modalPopup .footer {
                padding: 15px;
                text-align: center;
                border-top: 1px solid #dee2e6;
            }

            .modalPopup .yes, .modalPopup .no {
                display: inline-block;
                padding: 8px 25px;
                margin: 0 5px;
                border-radius: 5px;
                cursor: pointer;
                font-weight: bold;
                border: none;
                transition: all 0.3s ease;
            }

            .modalPopup .yes {
                background: linear-gradient(135deg, #28a745 0%, #34ce57 100%);
                color: white;
            }

                .modalPopup .yes:hover {
                    background: linear-gradient(135deg, #218838 0%, #28a745 100%);
                    transform: translateY(-2px);
                }

            .modalPopup .no {
                background: linear-gradient(135deg, #dc3545 0%, #e4606d 100%);
                color: white;
            }

                .modalPopup .no:hover {
                    background: linear-gradient(135deg, #c82333 0%, #dc3545 100%);
                    transform: translateY(-2px);
                }

        /* Utility Classes */
        .text-center {
            text-align: center;
        }

        .text-right {
            text-align: right;
        }

        .mt-3 {
            margin-top: 1rem;
        }

        .mb-3 {
            margin-bottom: 1rem;
        }

        .p-3 {
            padding: 1rem;
        }

        /* Row/Column Spacing */
        .row {
            margin: 0 -10px;
        }

            .row > [class*="col-"] {
                padding: 0 10px;
            }

        /* Button Container */
        .button-container {
            margin-top: 20px;
            text-align: center;
        }

            .button-container .btn {
                margin: 0 5px;
            }

        /* Back Button Container */
        .back-button-container {
            margin-bottom: 15px;
            padding: 10px 0;
            border-bottom: 1px solid #dee2e6;
        }

        /* Print Specific Styles */
        @media print {
            .content-wrapper {
                padding: 0;
                background: white;
            }

            fieldset {
                border: none;
                box-shadow: none;
                padding: 0;
                margin: 0;
            }

            legend {
                display: none;
            }

            .button, .btn, #Button1, .button-container, .back-button-container {
                display: none !important;
            }

            #PrintDiv {
                border: none;
                padding: 0;
            }

            .gridview-table th {
                background: #f2f2f2;
                color: black;
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
            }

            .form-control, select, input {
                border: none;
                background: transparent;
            }
        }
    </style>

    <!-- JavaScript Functions -->
    <script type="text/javascript">
        // Initialize Select2 on document ready
        $(document).ready(function () {
            initializeSelect2();

            // Re-initialize Select2 after every postback
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        initializeSelect2();
                    }
                });
            }
        });

        function initializeSelect2() {
            // Destroy existing Select2 instances first to prevent conflicts
            setTimeout(function () {
                // Check and destroy existing instances
                if ($("[id*=ddlgdwn]").hasClass("select2-hidden-accessible")) {
                    $("[id*=ddlgdwn]").select2("destroy");
                }
                if ($("[id*=ddlactualbillno]").hasClass("select2-hidden-accessible")) {
                    $("[id*=ddlactualbillno]").select2("destroy");
                }

                // Initialize Godown dropdown with focus handling
                $("[id*=ddlgdwn]").select2({
                    placeholder: "--Select Godown--",
                    allowClear: true,
                    width: '100%',
                    dropdownParent: $('body'),
                    minimumResultsForSearch: 0, // Always show search box
                    selectOnClose: false,
                    closeOnSelect: true
                }).on('select2:open', function () {
                    // Force focus to search box when dropdown opens
                    setTimeout(function () {
                        var searchField = document.querySelector('.select2-search__field');
                        if (searchField) {
                            searchField.focus();
                            searchField.select();
                        }
                    }, 100);

                    setTimeout(function () {
                        var searchField = document.querySelector('.select2-search__field');
                        if (searchField) {
                            searchField.focus();
                        }
                    }, 200);
                }).on('select2:close', function () {
                    $(this).focus();
                });

                // Initialize Bill No dropdown with focus handling
                $("[id*=ddlactualbillno]").select2({
                    placeholder: "--Select Bill--",
                    allowClear: true,
                    width: '100%',
                    dropdownParent: $('body'),
                    minimumResultsForSearch: 0,
                    selectOnClose: false,
                    closeOnSelect: true
                }).on('select2:open', function () {
                    // Multiple attempts to ensure focus
                    setTimeout(function () {
                        var searchField = document.querySelector('.select2-search__field');
                        if (searchField) {
                            searchField.focus();
                            searchField.select();
                        }
                    }, 100);

                    setTimeout(function () {
                        var searchField = document.querySelector('.select2-search__field');
                        if (searchField) {
                            searchField.focus();
                        }
                    }, 200);
                }).on('select2:close', function () {
                    $(this).focus();
                });
            }, 100);
        }

        // Handle focus after any partial postback
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            initializeSelect2();
        });

        // Additional fix for IE/Edge browsers
        if (navigator.userAgent.match(/MSIE|Trident|Edge/)) {
            $(document).on('focus', '.select2-search__field', function () {
                $(this).trigger('click');
            });
        }

        // Function to manually trigger focus (can be called if needed)
        function focusSelect2Search(selectElement) {
            var select2Instance = $(selectElement).data('select2');
            if (select2Instance) {
                select2Instance.open();
                setTimeout(function () {
                    $('.select2-search__field').focus();
                }, 100);
            }
        }

        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            printWindow.document.write('<html><head>');
            printWindow.document.write('<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">');
            printWindow.document.write('<style>body { font-family: Arial, sans-serif; padding: 20px; } table { border-collapse: collapse; width: 100%; } th { background: #f2f2f2;color:#000; } td, th { border: 1px solid #ddd; padding: 4px; } .text-center { text-align: center; }</style>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
        }

        function PrintDiv_det() {
            var divContents = document.getElementById("PrintDiv_Det").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            printWindow.document.write('<html><head>');
            printWindow.document.write('<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">');
            printWindow.document.write('<style>body { font-family: Arial, sans-serif; padding: 20px; }</style>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
        }

        function PrintDiv_Actual() {
            var divContents = document.getElementById("printActualBill").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            printWindow.document.write('<html><head>');
            printWindow.document.write('<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">');
            printWindow.document.write('</head><body>');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
        }

        function PrintDiv_Epo() {
            var divContents = document.getElementById("PrintDivEpo").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            printWindow.document.write('<html><head>');
            printWindow.document.write('<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">');
            printWindow.document.write('</head><body>');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
        }

        function goBack() {
            window.location.href = 'https://mpwarehousing.mp.gov.in/Warehouse/Branch_Welcome.aspx';
        }

        function goBackWithSession() {
            // Alternative: Check if previous page exists
            var previousPage = document.referrer;
            if (previousPage && previousPage.includes('mpwarehousing.mp.gov.in')) {
                window.location.href = previousPage;
            } else {
                window.location.href = 'https://mpwarehousing.mp.gov.in/Warehouse/Branch_Welcome.aspx';
            }
        }

        // Debug function to check Select2 state (can be called from browser console)
        function debugSelect2() {
            console.log('Godown Select2:', $("[id*=ddlgdwn]").data('select2'));
            console.log('Bill Select2:', $("[id*=ddlactualbillno]").data('select2'));
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="content-wrapper">
            <!-- Back Button Row - Aligned to Right using Flexbox -->
            <div class="back-button-container">
                <div class="row">
                    <div class="col-md-12" style="display: flex; justify-content: flex-end; align-items: center;">
                        <button type="button" class="button button2" onclick="goBack();" style="min-width: 120px; padding: 8px 20px;">
                            <i class="fa fa-arrow-left"></i>&nbsp;&nbsp;Back
                        </button>
                        <span style="margin-left: 15px; color: #6c757d; font-size: 13px;">
                            <%--<i class="fa fa-home"></i> https://mpwarehousing.mp.gov.in/Warehouse/Branch_Welcome.aspx--%>
                        </span>
                    </div>
                </div>
            </div>

            <!-- Main Search Section -->
            <fieldset>
                <legend>Markfed Rent Bills Print</legend>
                <div class="row">
                    <div class="col-md-1">
                        <label runat="server">Branch</label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox runat="server"
                            ID="txtbranch"
                            CssClass="form-control readonly-textbox"
                            ReadOnly="true"
                            Font-Bold="true"
                            Font-Size="Medium">
                        </asp:TextBox>
                    </div>

                    <div class="col-md-1">
                        <label>Godown Name</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlgdwn" runat="server" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlgdwn_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-1">
                        <label>Bill No.</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlactualbillno" runat="server" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlactualbillno_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 text-center">
                        <asp:Button class="button button2" ID="btnviewbill" runat="server"
                            Text="View Bill" OnClick="btnviewbill_Click" Style="min-width: 150px;"></asp:Button>
                    </div>
                </div>
            </fieldset>

            <!-- Bill Display Section -->
            <fieldset>
                <legend>Bill Details</legend>
                <div class="row">
                    <div class="col-md-12">
                        <div id="divrent" runat="server" visible="false">
                            <!-- Print Container -->
                            <div id="PrintDiv" class="print-container">
                                <table style="width: 100%;" class="bill-table">
                                    <tr>
                                        <td colspan="2" class="text-center" style="padding: 10px;">
                                            <strong style="font-size: 16px;">मध्य प्रदेश वेयर हाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन, क्षेत्रीय कार्यालय -</strong>
                                            <asp:Label ID="lblP_regionnm" runat="server" Font-Size="16px" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="text-center" style="padding: 5px;">
                                            <asp:Label ID="Label2" runat="server" Font-Size="14px"
                                                Text="-: वास्तविक भंडारित मात्रा अंतर्गत J.V. योजना मे लिए गए गोदामो के देयक :-">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 15px;"></td>
                                    </tr>
                                    <tr>
                                        <td width="60%" style="padding: 3px;">
                                            <asp:Label ID="Label5" runat="server" Text="शाखा का नाम :- " Font-Size="13px"></asp:Label>
                                            <asp:Label ID="lblbranch" runat="server" Font-Size="13px" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td width="40%" style="padding: 3px;">
                                            <asp:Label ID="Label3" runat="server" Text="माह :- " Font-Size="13px"></asp:Label>
                                            <asp:Label ID="lblbillmonth" runat="server" Font-Size="13px" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px;">
                                            <asp:Label ID="Label9" runat="server" Text="वेयरहाउस का नाम :- " Font-Size="13px"></asp:Label>
                                            <asp:Label ID="lblgdwnname" runat="server" Font-Size="13px" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="padding: 3px;">
                                            <asp:Label ID="Label7" runat="server" Text="गोदाम क्र. :- " Font-Size="13px"></asp:Label>
                                            <asp:Label ID="lblGdnum" runat="server" Font-Size="13px" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px;">
                                            <asp:Label ID="Label11" runat="server" Text="बिल क्रमांक :- " Font-Size="13px"></asp:Label>
                                            <asp:Label ID="lblbillno" runat="server" Font-Size="13px" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="padding: 3px;">
                                            <asp:Label ID="Label13" runat="server" Text="भंडारित स्कंध का नाम :- " Font-Size="13px"></asp:Label>
                                            <asp:Label ID="lblcmd" runat="server" Font-Size="13px" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px;">
                                            <asp:Label ID="Label15" runat="server" Text="GSTN : 23AADCM7742B1ZU" Font-Size="13px" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="padding: 3px;">
                                            <asp:Label ID="Label17" runat="server" Text="PAN : AADCM7742B" Font-Size="13px" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px;">
                                            <asp:Label ID="Label21" runat="server" Text="मात्रा :- मेट्रिक टन मे" Font-Size="13px" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="padding: 3px;">
                                            <asp:Label ID="Label1" runat="server" Text="स्कंध की दर :" Font-Size="13px" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="lblCRate" runat="server" Font-Size="13px" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 15px;"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 10px 0;">
                                            <div class="gridview-container">
                                                <asp:GridView ID="GD1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    CssClass="gridview-table" ShowFooter="true" BorderStyle="None"
                                                    GridLines="Both">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="क्र." ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="Date" HeaderText="दिनाँक" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="Opening_Weight" HeaderText="प्रारंभिक मात्रा" ItemStyle-Width="12%" />
                                                        <asp:BoundField DataField="Receive_Weight" HeaderText="जमा मात्रा" ItemStyle-Width="12%" />
                                                        <asp:BoundField DataField="Issue_Weight" HeaderText="भुगतान मात्रा" ItemStyle-Width="12%" />
                                                        <asp:BoundField DataField="Closing_Weight" HeaderText="शेष मात्रा" ItemStyle-Width="12%" />
                                                        <asp:BoundField DataField="Per_Day_Rate" HeaderText="शुल्क दर प्रति दिन" ItemStyle-Width="12%" />
                                                        <asp:BoundField DataField="Total_Charges" HeaderText="राशि (6x7)" ItemStyle-Width="15%" />
                                                    </Columns>
                                                    <FooterStyle CssClass="gridview-footer" Font-Bold="True" ForeColor="#C70039"
                                                        HorizontalAlign="Center" Font-Size="12px" />
                                                    <HeaderStyle BackColor="#1e4d7c" ForeColor="White" Font-Bold="True"
                                                        HorizontalAlign="Center" Font-Size="12px" />
                                                    <RowStyle HorizontalAlign="Center" Font-Size="12px" />
                                                    <AlternatingRowStyle BackColor="#f8f9fa" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <!-- Action Buttons -->
                            <div class="button-container">
                                <asp:Button class="button button2" Width="150px" Height="35px" ID="btncloseconfrm"
                                    runat="server" Text="Close" OnClientClick="divrent.style.display='none'; return false;" />
                                <input id="Button1" name="Print" type="button" style="height: 35px; width: 150px;"
                                    class="button button2" value="Print Bill" onclick="PrintDiv();" />
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
    </form>

    <!-- Script for handling postbacks -->
    <script type="text/javascript">
        // Ensure Select2 works after partial postbacks
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            initializeSelect2();
        });
    </script>
</body>
</html>
