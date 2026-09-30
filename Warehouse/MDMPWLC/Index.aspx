<%@ Page Title="" Language="C#" MasterPageFile="~/MDMPWLC/MasterPages//MD.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
            width: 100%;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #00aad2 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #00aad2 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dashboard-main-wrapper">
        <!-- ============================================================== -->
        <!-- navbar -->
        <!-- ============================================================== -->
        <div class="dashboard-header">
            <nav class="navbar navbar-expand-lg bg-#ef172c fixed-top" style="background-color: white;">
                <a class="navbar-brand" href="index.aspx">
                    <img src="../assets/New/img/logo_with_text.png" class="img-responsive logo"></a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse " id="navbarSupportedContent">
                    <ul class="navbar-nav ml-auto navbar-right-top">
                        <%-- <li class="nav-item">
                            <div id="custom-search" class="top-search-bar">
                                <input class="form-control" type="text" placeholder="Search..">
                            </div>
                        </li>--%>
                        <li class="nav-item dropdown notification"></li>
                        <li class="nav-item dropdown connection">
                            <a class="nav-link" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fas fa-fw fa-th"></i></a>
                            <ul class="dropdown-menu dropdown-menu-right connection-dropdown">

                                <li>
                                    <div class="conntection-footer"><a href="#">More</a></div>
                                </li>
                            </ul>
                        </li>
                        <li class="nav-item dropdown nav-user">
                            <a class="nav-link nav-user-img" href="#" id="navbarDropdownMenuLink2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <%--<img src="assets/images/avatar-1.jpg" alt="" class="user-avatar-md rounded-circle">--%>
                                <i class="fas fa-user"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right nav-user-dropdown" aria-labelledby="navbarDropdownMenuLink2">
                                <div class="nav-user-info">
                                    <h5 class="mb-0 text-#ef172c nav-user-name">
                                        <asp:Label ID="lblusername" runat="server"></asp:Label>
                                    </h5>
                                    <span class="status"></span><span class="ml-2">Available</span>
                                </div>
                                <a class="dropdown-item" href="#"><i class="fas fa-user mr-2"></i>MD MPWLC</a>
                                <a class="dropdown-item" href="#"><i class="fas fa-cog mr-2"></i>Setting</a>
                                <a class="dropdown-item" href="#"><i class="fas fa-power-off mr-2"></i>Logout</a>
                            </div>
                        </li>
                    </ul>
                </div>
            </nav>
        </div>
        <!-- ============================================================== -->
        <!-- end navbar -->
        <!-- ============================================================== -->
        <!-- ============================================================== -->
        <!-- left sidebar -->
        <!-- ============================================================== -->
        <div class="nav-left-sidebar sidebar-dark" style="overflow: auto; padding-bottom: 150px;">
            <div class="menu-list">
                <nav class="navbar navbar-expand-lg navbar-light">
                    <a class="d-xl-none d-lg-none" href="index.aspx">Dashboard</a>
                    <%-- <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>--%>
                    <div class="collapse navbar-collapse" id="navbarNav">
                        <ul class="navbar-nav flex-column">
                            <li class="nav-divider">Menu
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link active" href="/MDMPWLC/Index.aspx"><i class="fa fa-fw fa-user-circle"></i>Dashboard <span class="badge badge-success">6</span></a>
                                <%--<div id="submenu-1" class="collapse submenu" style="">
                                            <ul class="nav flex-column">
                                                <li class="nav-item">
                                                    <a class="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-1-2" aria-controls="submenu-1-2">E-Commerce</a>
                                                    <div id="submenu-1-2" class="collapse submenu" style="">
                                                        <ul class="nav flex-column">
                                                            <li class="nav-item">
                                                                <a class="nav-link" href="index.html">E Commerce Dashboard</a>
                                                            </li>
                                                            <li class="nav-item">
                                                                <a class="nav-link" href="ecommerce-product.html">Product List</a>
                                                            </li>
                                                            <li class="nav-item">
                                                                <a class="nav-link" href="ecommerce-product-single.html">Product Single</a>
                                                            </li>
                                                            <li class="nav-item">
                                                                <a class="nav-link" href="ecommerce-product-checkout.html">Product Checkout</a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" href="dashboard-finance.html">Finance</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" href="dashboard-sales.html">Sales</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-1-1" aria-controls="submenu-1-1">Infulencer</a>
                                                    <div id="submenu-1-1" class="collapse submenu" style="">
                                                        <ul class="nav flex-column">
                                                            <li class="nav-item">
                                                                <a class="nav-link" href="dashboard-influencer.html">Influencer</a>
                                                            </li>
                                                            <li class="nav-item">
                                                                <a class="nav-link" href="influencer-finder.html">Influencer Finder</a>
                                                            </li>
                                                            <li class="nav-item">
                                                                <a class="nav-link" href="influencer-profile.html">Influencer Profile</a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>--%>
                            </li>

                            <li class="nav-item ">
                                <a class="nav-link" href="../../StockPosition_Depot.aspx" target="_blank"><i class="fa fa-map-marker"></i>GMap Location Branch <span class="badge badge-success">6</span></a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="../../GioStock_Godown.aspx" target="_blank"><i class="fa fa-map-marker"></i>GMap Location godown <span class="badge badge-success">6</span></a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="../../Geo_Stock_with_Branch.aspx" target="_blank"><i class="fa fa-map-marker"></i>GMap Location godown with branch <span class="badge badge-success">6</span></a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="../../State_Godown_dtl.aspx" target="_blank"><i class="fa fa-map-marker"></i>GMap Location godown Current stock <span class="badge badge-success">6</span></a>
                            </li>
                            <li class="nav-item ">
                                <a class="nav-link" href="/MDMPWLC/Public/GenerateBill/Report_Region.aspx"><i class="fa fa-fw fa-user-circle"></i>Report Page </a>

                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-2" aria-controls="submenu-2"><i class="fa fa-fw fa-rocket"></i>Billing Reports</a>
                                <div id="submenu-2" class="collapse submenu" style="">
                                    <ul class="nav flex-column">
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Rpt_Get_Pending_Bill_Detail_With_Amount.aspx" target="_blank">1. Billing Pendency report at Various Level(From August)</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Rpt_Get_Pending_Bill_Detail_With_Amount.aspx" target="_blank">2. Received Payment from MPSCSC(From August)</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Rpt_Get_Except_and_Received_Payment_From_MPSCSC_Details_From_Aug.aspx" target="_blank">3. Region wise Summary Report from August</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Rpt_Get_Except_and_Received_Payment_From_MPSCSC_Details_From_Aug.aspx" target="_blank">4. Storage Bill After August 2020 Search Godown Wise</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Rpt_Search_HO_StorageBillVerification_N.aspx" target="_blank">5. Search Storage Bill After August 2020 Status</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Rpt_Region_And_Month_Wise_Pending_Amount.aspx" target="_blank">6. Pending Storage Bills Report for Payment</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Payment_BillsFromCSMStoMPWLC_Status_Rept_Region_Wise.aspx" target="_blank">7. Received Payment from MPSCSC Region/District/Branch Wise(After August)</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Payment_Credit_From_MPWLC_To_Godown.aspx" target="_blank">8.	Payment Credit From MPWLC To Godown NEW</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Rpt_Get_UnexpiredAmount_From_Aug.aspx" target="_blank">9. Payment August To December</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Rpt_Get_UnexpiredAmount_From_Jan.aspx" target="_blank">10. Payment Jan To March</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Rpt_Get_Recived_and_Pending_Amount_From_Aug.aspx" target="_blank">11. Pending Payment From MPSCSC</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/States/Rpt_Pending_for_DSC_and_Submission.aspx" target="_blank">12. Pending Bill Details Before August</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/Rpt_Bill_Detail_After_District_Difference_Wise_With_Amount.aspx" target="_blank">13. District Wise Pendancy at Verius Level From August</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="../../Reports/Rpt_Pendancy_At_Verius_Level.aspx" target="_blank">14. District Wise Pendancy at Verius Level From August(Only Amount)</a>
                                        </li>
                                    </ul>
                                </div>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-3" aria-controls="submenu-3"><i class="fas fa-fw fa-chart-pie"></i>Rabi Procurement</a>
                                <div id="submenu-3" class="collapse submenu" style="">
                                    <ul class="nav flex-column">
                                        <li class="nav-item">
                                            <asp:LinkButton ID="LinkButton" runat="server" CssClass="nav-link"
                                                OnClick="LinkButton_Click">1. Wheat Procurement 2021-22 </asp:LinkButton>
                                        </li>
                                        <li class="nav-item">
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="nav-link">1. Wheat Procurement 2021-22 </asp:LinkButton>
                                        </li>
                                        <li class="nav-item">
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="nav-link">Dalhan e-WHR Submission Procurement 2021-22</asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </li>
                            <%-- <li class="nav-item ">
                                <a class="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-4" aria-controls="submenu-4"><i class="fab fa-fw fa-wpforms"></i>Forms</a>
                                <div id="submenu-4" class="collapse submenu" style="">
                                    <ul class="nav flex-column">
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/form-elements.html">Form Elements</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/form-validation.html">Parsely Validations</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/multiselect.html">Multiselect</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/datepicker.html">Date Picker</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/bootstrap-select.html">Bootstrap Select</a>
                                        </li>
                                    </ul>
                                </div>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-5" aria-controls="submenu-5"><i class="fas fa-fw fa-table"></i>Tables</a>
                                <div id="submenu-5" class="collapse submenu" style="">
                                    <ul class="nav flex-column">
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/general-table.html">General Tables</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/data-tables.html">Data Tables</a>
                                        </li>
                                    </ul>
                                </div>
                            </li>--%>
                            <%-- <li class="nav-divider">Features
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-6" aria-controls="submenu-6"><i class="fas fa-fw fa-file"></i>Pages </a>
                                <div id="submenu-6" class="collapse submenu" style="">
                                    <ul class="nav flex-column">
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/blank-page.html">Blank Page</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/blank-page-header.html">Blank Page Header</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/login.html">Login</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/404-page.html">404 page</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/sign-up.html">Sign up Page</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/forgot-password.html">Forgot Password</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/pricing.html">Pricing Tables</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/timeline.html">Timeline</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/calendar.html">Calendar</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/sortable-nestable-lists.html">Sortable/Nestable List</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/widgets.html">Widgets</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/media-object.html">Media Objects</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/cropper-image.html">Cropper</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/color-picker.html">Color Picker</a>
                                        </li>
                                    </ul>
                                </div>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-7" aria-controls="submenu-7"><i class="fas fa-fw fa-inbox"></i>Apps <span class="badge badge-secondary">New</span></a>
                                <div id="submenu-7" class="collapse submenu" style="">
                                    <ul class="nav flex-column">
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/inbox.html">Inbox</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/email-details.html">Email Detail</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/email-compose.html">Email Compose</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/message-chat.html">Message Chat</a>
                                        </li>
                                    </ul>
                                </div>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-8" aria-controls="submenu-8"><i class="fas fa-fw fa-columns"></i>Icons</a>
                                <div id="submenu-8" class="collapse submenu" style="">
                                    <ul class="nav flex-column">
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/icon-fontawesome.html">FontAwesome Icons</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/icon-material.html">Material Icons</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/icon-simple-lineicon.html">Simpleline Icon</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/icon-themify.html">Themify Icon</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/icon-flag.html">Flag Icons</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/icon-weather.html">Weather Icon</a>
                                        </li>
                                    </ul>
                                </div>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-9" aria-controls="submenu-9"><i class="fas fa-fw fa-map-marker-alt"></i>Maps</a>
                                <div id="submenu-9" class="collapse submenu" style="">
                                    <ul class="nav flex-column">
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/map-google.html">Google Maps</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="pages/map-vector.html">Vector Maps</a>
                                        </li>
                                    </ul>
                                </div>
                            </li>--%>
                        </ul>

                    </div>
                </nav>
            </div>
        </div>
        <!-- ============================================================== -->
        <!-- end left sidebar -->
        <!-- ============================================================== -->
        <!-- ============================================================== -->
        <!-- wrapper  -->
        <!-- ============================================================== -->

        <div class="dashboard-wrapper">
            <div class="dashboard-ecommerce">
                <div class="container-fluid dashboard-content " style="background-color: #888484;">
                    <!-- ============================================================== -->
                    <!-- pageheader  -->
                    <!-- ============================================================== -->

                    <!-- ============================================================== -->
                    <!-- end pageheader  -->
                    <!-- ============================================================== -->
                    <div class="ecommerce-widget" style="padding-top: 60px;">
                        <div class="row">
                            <!-- ============================================================== -->
                            <!-- sales  -->
                            <!-- ============================================================== -->
                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">No of Bill Generated with Amount</h5>
                                        <center>
                                            <a href="Public/GenerateBill/RegionGeneratedBillSummary.aspx">
                                
                                <u>
                               
                                    <span class="count" id="NoOfBill" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span>

                                </u>
                                <div>
                                    <span class="count1" id="NoOfBillAmt" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c;font-weight:bold; font-family:'Times New Roman';">00</span><span style="font-familyigital-7 !important;
                                            font-size: 30px; color: #ef172c;"></span>                                    
                                </div>
                            </a>
                            </center>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">No of Bill Submitted to ICM with Amount</h5>
                                        <center>
                                <a href="Public/ICM/Bill_Sub_RegionGeneratedBillSummary.aspx" >
                                    <u>
                                        <span class="count" id="ICMSubmit" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span>
                                    </u> 
                                <div>
                                    <span class="count1" id="ICMSubmitAmt" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c;font-weight:bold; font-family:'Times New Roman';">00</span> <span style="font-familyigital-7 !important;
                                            font-size: 30px; color: #ef172c; font-family:'Times New Roman';"></span>
                                </div>
                                    </a>
                            </center>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">No of Bill Submitted by ICM with Amount</h5>
                                        <center>
                                <a href="Public/ICM/SubmittedByIcm_RegionGeneratedBillSummary.aspx" >
                                    <u>
                                   
                                        <span class="count" id="SubmittedICM" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span>
                                    </u>
                                <div>
                                    
                                     <span class="count1" id="SubmittedICMAmt" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c;font-weight:bold; font-family:'Times New Roman';">00</span><span style="font-familyigital-7 !important;
                                            font-size: 30px; color: #ef172c; font-family:'Times New Roman';"></span>
                                   
                                </div>
                                    </a>
                            </center>
                                    </div>
                                </div>
                            </div>
                            <!-- ============================================================== -->
                            <!-- end sales  -->
                            <!-- ============================================================== -->
                            <!-- ============================================================== -->
                            <!-- new customer  -->
                            <!-- ============================================================== -->
                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">Pending for Bill Generation
                                            <br />
                                            <br />
                                        </h5>
                                        <center>
                                <a href= "Public/GenerateBill/PendingRegionGeneratedBillSummary.aspx">
                                    <u>
                                       
                                    <span class="count" id="PendingBill" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span></u>
                                </a>
                                <div>
                                    <br />
                                    <br />
                                </div>
                            </center>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">Pending Bill for ICM Submission with Amount</h5>
                                        <center>
                                <a href="Public/ICM/PendencyForIcm_RegionGeneratedBillSummary.aspx" ><u>
                                    
                                    <span class="count" id="PendingICMSubmit" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span>
                                                                                                            </u> 
                                <div>
                                    <span class="count1" id="PendingICMSubmitAmt" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c;font-weight:bold; font-family:'Times New Roman';">00</span><span style="font-familyigital-7 !important;
                                            font-size: 30px; color: #ef172c; font-family:'Times New Roman';"></span>
                                    
                                </div>
                                    </a>
                            </center>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">Pending Bill at ICM Submission with Amount</h5>
                                        <center>
                                <a href="Public/ICM/PendencyAtIcm_RegionGeneratedBillSummary.aspx" >
                                    <u>
                                        
                                        <span class="count" id="PendingSubmittedICM" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span>
                                    </u> 
                                <div>
                                     <span class="count1" id="PendingSubmittedICMAmt" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c;font-weight:bold; font-family:'Times New Roman';">00</span><span style="font-familyigital-7 !important;
                                            font-size: 30px; color: #ef172c; font-family:'Times New Roman';"></span>
                                    
                                </div>
                                    </a>
                            </center>
                                    </div>
                                </div>
                            </div>
                            <!-- ============================================================== -->
                            <!-- end new customer  -->
                            <!-- ============================================================== -->
                            <!-- ============================================================== -->
                            <!-- visitor  -->
                            <!-- ============================================================== -->
                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">Payment by DM NAN
                                            <br />
                                            <br />
                                        </h5>
                                        <center>
                                <a href="Public/DM_NAN/RegionBillSummary.aspx" >
                                    <u>
                                        
                                        <span class="count" id="DMBill" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span>
                                    </u></a>
                                <div>
                                    
                                  
                                        <span class="count1" id="DMAmt" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c;font-weight:bold; font-family:'Times New Roman';">00</span><span style="font-familyigital-7 !important;
                                            font-size: 30px; color: #ef172c; font-family:'Times New Roman';"></span>
                                </div>
                            </center>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">DSC Bill by RM with Amount</h5>
                                        <center>
                                <a href="Public/RM/Dsc_By_Rm_RegionGeneratedBillSummary.aspx" >
                                     <u>
                                 <span class="count" id="RMDSCBill" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span>
                                         </u>
                                <div>
                                    
                                    <span class="count1" id="RMDSCAmt" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c;font-weight:bold; font-family:'Times New Roman';">00</span><span style="font-familyigital-7 !important;
                                            font-size: 30px; color: #ef172c; font-family:'Times New Roman';"></span>
                                    
                                </div>
                                    </a>
                            </center>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">Payment by NEFT Godown (No of Bills & Amount)
                                            <br />
                                            <br />
                                        </h5>
                                        <center>
                                <a href="Public/NEFT/Payment_RegionGeneratedBillSummary.aspx" ><u>
                                       
                                        <span class="count" id="NEFTBill" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span>
                                    </u></a>
                                <div>
                               
                                   <span class="count1" id="NEFTAmt" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c;font-weight:bold; font-family:'Times New Roman';">00</span><span style="font-familyigital-7 !important;
                                            font-size: 30px; color: #ef172c; font-family:'Times New Roman';"></span>
                                </div>
                            </center>
                                    </div>
                                </div>
                            </div>
                            <!-- ============================================================== -->
                            <!-- end visitor  -->
                            <!-- ============================================================== -->
                            <!-- ============================================================== -->
                            <!-- total orders  -->
                            <!-- ============================================================== -->
                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">Pending by DM NAN (No of Bills & Amount)
                                            <br />
                                            <br />
                                        </h5>
                                        <center>
                                <a href="#"
                                    ><u>
                                   
                                         <span class="count" id="PendingDMBill" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span>
                                    </u></a>
                                <div>
                                    
                                    <span class="count1" id="PendingDMAmt" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c;font-weight:bold; font-family:'Times New Roman';">00</span><span style="font-familyigital-7 !important;
                                            font-size: 30px; color: #ef172c; font-family:'Times New Roman';"></span>
                                </div>
                            </center>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">Pending for RM DSC (No of Bills & Amount)
                                            <br />
                                            <br />
                                        </h5>
                                        <center>
                                <a href="Public/RM/PendencyForRmDsc_RegionGeneratedBillSummary.aspx" ><u>
                                      
                                        <span class="count" id="PendingRMDSCBill" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span>
                                    </u>
                                <div>
                                    
                                    <span class="count1" id="PendingRMDSCAmt" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c;font-weight:bold; font-family:'Times New Roman';">00</span><span style="font-familyigital-7 !important;
                                            font-size: 30px; color: #ef172c; font-family:'Times New Roman';"></span>
                                </div>
                                    </a>
                            </center>
                                    </div>
                                </div>
                            </div>
                            </hr>
                           
                            <div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12">
                                <div class="card border-3 border-top border-top-primary">
                                    <div class="card-body">
                                        <h5 class="text-muted">Pending for NEFT to Godown (No of Bills & Amount)</h5>
                                        <center>
                                <a href="#"
                                    ><u>
                                        
                                        <span class="count" id="PendingNEFTBill" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c; font-family:'Times New Roman';">00</span>
                                    </u></a>
                                <div>
                                    
                                   <span class="count1" id="PendingNEFTAmt" runat="server" style="font-familyigital-7 !important;
                                        font-size: 30px; color: #ef172c;font-weight:bold; font-family:'Times New Roman';">00</span><span style="font-familyigital-7 !important;
                                            font-size: 30px; color: #ef172c; font-family:'Times New Roman';"></span>
                                </div>
                            </center>

                                    </div>
                                </div>
                            </div>

                            <!-- ============================================================== -->
                            <!-- end total orders  -->
                            <!-- ============================================================== -->
                        </div>

                        <div class="row">
                            <!-- ============================================================== -->
                            <!-- data table  -->
                            <!-- ============================================================== -->
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                <div class="card">
                                    <div class="card-header">
                                        <%--<h5 class="mb-0">Data Tables - Print, Excel, CSV, PDF Buttons</h5>--%>
                                        <h5>Region Wise Payment Received Details From MPSCSC</h5>
                                    </div>
                                    <div class="card-body">

                                        <div class="table-responsive">
                                            <asp:GridView ID="example" runat="server" AutoGenerateColumns="false" Visible="true" class="Grid table table-striped table-borde#ef172c second" Style="width: 100%"
                                                ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <tr>
                                                                <th>जिले का नाम</th>
                                                                <th>माह अगस्त 2020 से प्रस्तुत देयकों की संख्या</th>
                                                                <th>प्रस्तुत देयकों की राशि</th>
                                                                <th>MPSCSC द्वारा काटी गई TDS राशि</th>
                                                                <th>MPSCSC द्वारा एकतरफा  काटी गई राशि</th>
                                                                <th>MPSCSC से प्राप्त राशि रूपये में</th>
                                                                <th>MPSCSC से अप्राप्त राशि</th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("District_Name")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("NoOfSUBBill")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("SUBBillAmt")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("TDSDeduction")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label5" Text='<%# Eval("OtherDeduction")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("PaymentReceivedTilldate")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="TextBox1" Text='<%# Eval("PendingatMPSCSC")%>'></asp:Label>
                                                                </td>

                                                            </tr>

                                                            <%--  <td><%# Container.DataItemIndex + 1 %></td>--%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- ============================================================== -->
                            <!-- end data table  -->
                            <!-- ============================================================== -->
                        </div>

                    </div>
                    <!-- ============================================================== -->
                    <!-- footer -->
                    <!-- ============================================================== -->
                    <div class="footer">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 col-12">
                                    Madhya Pradesh warehouse logistics corporation Bhopal <a href="http://mpwarehousing.com/">MPWLC</a>.
                       
                                </div>
                                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 col-12">
                                    <div class="text-md-right footer-links d-none d-sm-block">
                                        <a href="javascript: void(0);">About</a>
                                        <a href="javascript: void(0);">Support</a>
                                        <a href="javascript: void(0);">Contact Us</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- ============================================================== -->
                    <!-- end footer -->
                    <!-- ============================================================== -->
                </div>
                <!-- ============================================================== -->
                <!-- end wrapper  -->
                <!-- ============================================================== -->
            </div>
        </div>
    </div>
    <!-- jquery 3.3.1 -->
    <script src="assets/vendor/jquery/jquery-3.3.1.min.js"></script>
    <!-- bootstap bundle js -->
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.js"></script>
    <!-- slimscroll js -->
    <script src="assets/vendor/slimscroll/jquery.slimscroll.js"></script>
    <!-- main js -->
    <script src="assets/libs/js/main-js.js"></script>
    <!-- chart chartist js -->
    <script src="assets/vendor/charts/chartist-bundle/chartist.min.js"></script>
    <!-- sparkline js -->
    <script src="assets/vendor/charts/sparkline/jquery.sparkline.js"></script>
    <!-- morris js -->
    <script src="assets/vendor/charts/morris-bundle/raphael.min.js"></script>
    <script src="assets/vendor/charts/morris-bundle/morris.js"></script>
    <!-- chart c3 js -->
    <script src="assets/vendor/charts/c3charts/c3.min.js"></script>
    <script src="assets/vendor/charts/c3charts/d3-5.4.0.min.js"></script>
    <script src="assets/vendor/charts/c3charts/C3chartjs.js"></script>
    <script src="assets/libs/js/dashboard-ecommerce.js"></script>
    <!-- Optional JavaScript -->
    <!--savan--->
    <%--    <script src="../assets/vendor/jquery/jquery-3.3.1.min.js"></script>
    <script src="../assets/vendor/bootstrap/js/bootstrap.bundle.js"></script>
    <script src="../assets/vendor/slimscroll/jquery.slimscroll.js"></script>
    <script src="../assets/vendor/multi-select/js/jquery.multi-select.js"></script>
    <script src="../assets/libs/js/main-js.js"></script>--%>
    <!--savan-->
    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="../assets/vendor/datatables/js/dataTables.bootstrap4.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>
    <script src="../assets/vendor/datatables/js/buttons.bootstrap4.min.js"></script>
    <script src="../assets/vendor/datatables/js/data-table.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.print.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.colVis.min.js"></script>
    <script src="https://cdn.datatables.net/rowgroup/1.0.4/js/dataTables.rowGroup.min.js"></script>
    <script src="https://cdn.datatables.net/select/1.2.7/js/dataTables.select.min.js"></script>
    <script src="https://cdn.datatables.net/fixedheader/3.1.5/js/dataTables.fixedHeader.min.js"></script>

</asp:Content>

