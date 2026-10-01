<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DSC_Update.aspx.cs" Inherits="Inspections_State_DSC_Update" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=yes" />
    <title>Update DSC | Responsive</title>
    
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link href="../Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <script type="text/javascript" src="../Assets/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    
    <style type="text/css">
        /* ----- ORIGINAL STYLES PRESERVED ----- */
        .button {
            background-color: #4CAF50;
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s;
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
        
        /* ----- RESPONSIVE LAYOUT UPDATES ----- */
        * {
            box-sizing: border-box;
        }
        
        body {
            margin: 0;
            padding: 20px;
            background: #f0f2f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        /* Responsive Fieldset */
        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 0 auto;
            border-radius: 5px;
            padding-left: 20px;
            padding-right: 20px;
            background: #ffffff;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            max-width: 1400px;
            width: 100% !important;
            border: 2px solid navy !important;
            margin-left: auto !important;
            margin-right: auto !important;
        }
        
        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
            background: white;
        }
        
        /* Responsive Button Container */
        .dsc-button-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            align-items: center;
            min-height: 60vh;
            padding: 40px 20px;
        }
        
        /* Responsive Update Button - WIDER VERSION */
        .btn-responsive-update {
            font-size: 2rem !important;
            font-weight: bold !important;
            padding: 70px 50px !important;
            width: 320px !important;
            max-width: 800px !important;
            height: auto !important;
            min-height: 200px;
            border-radius: 25px !important;
            transition: all 0.3s ease;
            box-shadow: 0 12px 28px rgba(0,0,0,0.25);
            background: linear-gradient(135deg, #28a745, #1e7e34);
            border: none;
            color: white;
            cursor: pointer;
            text-transform: uppercase;
            letter-spacing: 3px;
        }
        
        .btn-responsive-update:hover {
            transform: translateY(-4px);
            box-shadow: 0 18px 40px rgba(0,0,0,0.3);
            background: linear-gradient(135deg, #1e7e34, #166b2c);
        }
        
        /* Hide empty row */
        .row:empty {
            display: none;
        }
        
        /* Responsive Breakpoints - maintaining wider button */
        @media screen and (max-width: 1200px) {
            .btn-responsive-update {
                font-size: 1.8rem !important;
                padding: 60px 45px !important;
                max-width: 750px !important;
                min-height: 180px;
            }
        }
        
        @media screen and (max-width: 992px) {
            body {
                padding: 15px;
            }
            fieldset {
                padding-left: 15px;
                padding-right: 15px;
            }
            .btn-responsive-update {
                font-size: 1.6rem !important;
                padding: 55px 40px !important;
                max-width: 700px !important;
                min-height: 170px;
                letter-spacing: 2px;
            }
            .dsc-button-container {
                min-height: 50vh;
                padding: 30px 15px;
            }
        }
        
        @media screen and (max-width: 768px) {
            body {
                padding: 10px;
            }
            legend {
                font-size: 15px;
            }
            .btn-responsive-update {
                font-size: 1.4rem !important;
                padding: 45px 30px !important;
                max-width: 95% !important;
                min-height: 150px;
                letter-spacing: 2px;
                border-radius: 20px !important;
            }
            .dsc-button-container {
                min-height: 45vh;
                padding: 20px 10px;
            }
        }
        
        @media screen and (max-width: 576px) {
            .btn-responsive-update {
                font-size: 1.2rem !important;
                padding: 35px 25px !important;
                max-width: 100% !important;
                min-height: 130px;
                border-radius: 18px !important;
                letter-spacing: 1px;
            }
            .dsc-button-container {
                min-height: 70vh;
            }
            fieldset {
                padding: 0.35em 0.5em 0.75em;
                padding-left: 12px;
                padding-right: 12px;
            }
        }
        
        @media screen and (max-width: 400px) {
            .btn-responsive-update {
                font-size: 1rem !important;
                padding: 28px 18px !important;
                min-height: 110px;
                border-radius: 15px !important;
            }
            legend {
                font-size: 13px;
            }
        }
        
        /* Preserve original classes */
        .left, .right {
            float: left;
            width: 20%;
        }
        .main {
            float: left;
            width: 60%;
        }
        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%;
            }
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
        .auto-style3 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 50%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
        
        /* Ensure button inherits btn-success styling plus responsive overrides */
        .btn-success {
            background-color: #28a745 !important;
            border-color: #28a745 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset style="width: 100%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
            <legend>Update DSC</legend>
            
            <!-- Original empty row - preserved but hidden via CSS -->
            <div class="row"></div>
            
            <!-- Responsive Button Container - Wider Button -->
            <div class="dsc-button-container">
                <div class="text-center w-100">
                    <asp:Button runat="server" ID="btnshow" 
                        CssClass="btn btn-success btn-responsive-update" 
                        Text="UPDATE DSC" 
                        OnClick="btnshow_Click"
                        Font-Size="X-Large" />
                </div>
            </div>
            
            <!-- Original row with fixed margins is hidden to ensure responsiveness -->
            <!-- But we keep it in the DOM for any server-side references, just hide it -->
            <div class="row" style="margin-top:300px; display: none;">
                <div class="col-md-2"></div>
                <div class="col-md-8" style="margin-left: 600px">
                    <asp:Button runat="server" ID="btnshow_original" Font-Size="X-Large" Height="200" Width="500" CssClass="btn btn-success" Text="Update DSC" OnClick="btnshow_Click" />
                </div>
            </div>
        </fieldset>
    </form>
    
    <script type="text/javascript">
        // Ensure noBack functionality works
        if (typeof noBack === 'function') {
            noBack();
        }

        // Remove any inline styles that might break responsiveness
        (function () {
            // Fix any potential fieldset inline styles
            var fieldset = document.querySelector('fieldset');
            if (fieldset) {
                fieldset.style.marginLeft = 'auto';
                fieldset.style.marginRight = 'auto';
                fieldset.style.width = '100%';
            }

            // Remove any problematic inline styles from button if needed
            var btn = document.getElementById('btnshow');
            if (btn) {
                btn.style.height = 'auto';
                btn.style.width = 'auto';
                // Ensure Font-Size from server is overridden by CSS
                btn.style.fontSize = '';
            }

            // Handle window resize for any dynamic adjustments
            function adjustResponsive() {
                var btn = document.getElementById('btnshow');
                if (btn) {
                    if (window.innerWidth <= 576) {
                        btn.style.padding = '35px 25px';
                    } else if (window.innerWidth <= 768) {
                        btn.style.padding = '45px 30px';
                    } else if (window.innerWidth <= 992) {
                        btn.style.padding = '55px 40px';
                    } else {
                        btn.style.padding = '70px 50px';
                    }
                }
            }

            window.addEventListener('load', adjustResponsive);
            window.addEventListener('resize', adjustResponsive);
            adjustResponsive();
        })();
    </script>
</body>
</html>