<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Administration.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administration_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="Server">
    <link href="assets/css/Bracket_dashboard.css" rel="stylesheet" type="text/css" />
    <style>
        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content {
            background-color: #fefefe;
            margin: auto;
            padding: 20px;
            border: 1px solid #888;
            width: 100%;
        }

        /* The Close Button */
        .close {
            color: #aaaaaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover, .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }

        .count {
            font-size: 32px;
            font-weight: bold;
            color: white;
        }
    </style>
    <div class="row-text-center">
        <hr />
        <div class="row" style="">
            <%--<div class="col-md-12 Heading Counters_Heading">
                <center>
                    <label style="font-size: 18pt; background-color: Orange; border-radius: 20px; padding: 3px;">
                        <b style="margin: 5px 20px 5px 20px;">स्वछागृही एवं ग्राम स्वच्छता</b></label></center>
            </div>--%>
            <div class="alert" style="font-size: 20pt; font-weight: bold; background-color: #b5799e; color: White; border: 1px solid #b5799e; border-radius: 4px; margin-bottom: 20px; padding: 8px 35px 8px 14px; text-shadow: 0 1px 0 rgba(255, 255, 255, 0.5);">
                Billing Dasboard <a class="ui-dialog-titlebar-close ui-corner-all"
                    style="float: right; cursor: pointer;" href="/Reports.aspx"></a>
            </div>
        </div>
        <hr />
        <div class="row" style="margin-bottom: 20px;">
            <div class="col-md-4 mg-t-20">
                <div class="bg-royal rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            No of Bill Generated with Amount</label>
                        <hr />
                        <div class="mg-l-20">
                            <a href="Public/RegionGeneratedBillSummary.aspx" target="_blank">
                                <center>
                                    <u>

                                        <span class="count" id="NoOfBill" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white;">00</span>

                                    </u>
                                    <div>
                                        <span class="count1" id="NoOfBillAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: white;"></span>

                                    </div>
                                </center>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mg-t-20">
                <div class="bg-flickr rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            No of Bill Submitted to ICM with Amount</label>
                        <hr />
                        <div class="mg-l-20">
                            <center>
                                <a href="Public/ICM/Bill_Sub_RegionGeneratedBillSummary.aspx" target="_blank">
                                    <u>
                                        <span class="count" id="ICMSubmit" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white;">00</span>
                                    </u>
                                    <div>
                                        <span class="count1" id="ICMSubmitAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white; font-weight: bold;">00</span> <span style="font-familyigital-7 !important; font-size: 30px; color: white;"></span>
                                    </div>
                                </a>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
            <!-- col-3 -->
            <div class="col-md-4 mg-t-20">
                <div class="bg-crystal-clear rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            No of Bill Submitted by ICM with Amount
                        </label>
                        <hr />
                        <div class="mg-l-20 ">
                            <center>
                                <a href="Public/ICM/SubmittedByIcm_RegionGeneratedBillSummary.aspx" target="_blank">
                                    <u>

                                        <span class="count" id="SubmittedICM" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white;">00</span>
                                    </u>
                                    <div>

                                        <span class="count1" id="SubmittedICMAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: white;"></span>

                                    </div>
                                </a>
                            </center>
                        </div>
                    </div>

                </div>
            </div>
            <!-- col-3 -->

            <!-- col-3 -->
        </div>
        <!-- col-3 -->
        <div class="row" style="margin-bottom: 20px;">
            <div class="col-md-4 mg-t-20">
                <div class="bg-crystal-Opal rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            Pending for Bill Generation
                        </label>
                        <hr />
                        <div class="mg-l-20 blink">
                            <center>
                                <a href="Public/PendingRegionGeneratedBillSummary.aspx" target="_blank">
                                    <u>

                                        <span class="count" id="PendingBill" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span></u>
                                </a>
                                <div>
                                    <br />
                                    <br />
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mg-t-20 ">
                <div class="bg-mojito rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            Pending Bill for ICM Submission with Amount</label>
                        <hr />
                        <div class="mg-l-20 blink">
                            <center>
                                <a href="Public/ICM/PendencyForIcm_RegionGeneratedBillSummary.aspx" target="_blank"><u>

                                    <span class="count" id="PendingICMSubmit" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span>
                                </u>
                                    <div>
                                        <span class="count1" id="PendingICMSubmitAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: red;"></span>

                                    </div>
                                </a>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
            <!-- col-3 -->
            <div class="col-md-4 mg-t-20">
                <div class="bg-grandeur rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            Pending Bill at ICM Submission with Amount</label>
                        <hr />
                        <div class="mg-l-20 blink">
                            <center>
                                <a href="Public/ICM/PendencyAtIcm_RegionGeneratedBillSummary.aspx" target="_blank">
                                    <u>

                                        <span class="count" id="PendingSubmittedICM" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span>
                                    </u>
                                    <div>
                                        <span class="count1" id="PendingSubmittedICMAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: red;"></span>

                                    </div>
                                </a>
                            </center>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <hr />
        <!-- col-3 -->
        <div class="row" style="margin-bottom: 20px;">
            <div class="col-md-4 mg-t-20">
                <div class="bg-neon rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            Payment by DM NAN</label>
                        <hr />
                        <div class="mg-l-20">
                            <center>
                                <a href="#" target="_blank">
                                    <u>

                                        <span class="count" id="DMBill" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white;">00</span>
                                    </u></a>
                                <div>


                                    <span class="count1" id="DMAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: white;"></span>
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mg-t-20">
                <div class="bg-mixup rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            DSC Bill by RM with Amount (Rent Bill)
                        </label>
                        <hr />
                        <div class="mg-l-20">
                            <center>
                                <a href="Public/RM/Dsc_By_Rm_RegionGeneratedBillSummary.aspx" target="_blank">
                                    <span class="count" id="RMDSCBill" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white;">00</span>
                                    <div>

                                        <span class="count1" id="RMDSCAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: white;"></span>

                                    </div>
                                </a>
                            </center>
                        </div>
                    </div>

                </div>
            </div>

            <!-- col-3 -->
            <div class="col-md-4 mg-t-20">
                <div class="bg-reef rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            Created EPF Bill (No of Bills & Amount)
                        </label>
                        <hr />
                        <div class="mg-l-20">
                            <center>
                                <a href="#" target="_blank"><u>

                                    <span class="count" id="CreatedEPFBill" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white;">00</span>
                                </u></a>
                                <div>

                                    <span class="count1" id="CreatedEPFAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: white;"></span>
                                </div>
                            </center>
                        </div>
                    </div>

                </div>
            </div>
            <!-- col-3 -->

        </div>
        <!-- col-3 -->
        <div class="row" style="margin-bottom: 20px;">
            <div class="col-md-4 mg-t-20">
                <div class="bg-orangelite rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            Pending Pending by DM NAN (No of Bills & Amount)
                        </label>
                        <hr />
                        <div class="mg-l-20 blink">
                            <center>
                                <a href="#"
                                    target="_blank"><u>

                                        <span class="count" id="PendingDMBill" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span>
                                    </u></a>
                                <div>

                                    <span class="count1" id="PendingDMAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: red;"></span>
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
            <!-- col-3 -->
            <div class="col-md-4 mg-t-20">
                <div class="bg-cyan rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            Pending for RM DSC (No of Bills & Amount)
                        </label>
                        <hr />
                        <div class="mg-l-20 blink">
                            <center>
                                <a href="Public/RM/PendencyForRmDsc_RegionGeneratedBillSummary.aspx" target="_blank"><u>

                                    <span class="count" id="PendingRMDSCBill" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span>
                                </u>
                                    <div>

                                        <span class="count1" id="PendingRMDSCAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: red;"></span>
                                    </div>
                                </a>
                            </center>
                        </div>
                    </div>
                    <%--<div id="Div13" class="ht-50 tr-y-1 rickshaw_graph">
                        <svg width="249" height="50"><g><path d="M0,25Q17.983333333333334,19.291666666666664,20.75,19.374999999999996C24.9,19.499999999999996,37.35,25.0625,41.5,26.25S58.1,30.875,62.25,31.25S78.85000000000001,30.625,83,30S99.59999999999998,24.25,103.74999999999999,25S120.35000000000001,35.625,124.5,37.5S141.09999999999997,43.75,145.24999999999997,43.75S161.85,38.4375,166,37.5S182.6,35.3125,186.75,34.375S203.34999999999997,27.8125,207.49999999999997,28.125S224.1,37.8125,228.25,37.5Q231.01666666666668,37.291666666666664,249,25L249,50Q231.01666666666668,50,228.25,50C224.1,50,211.64999999999998,50,207.49999999999997,50S190.9,50,186.75,50S170.15,50,166,50S149.39999999999998,50,145.24999999999997,50S128.65,50,124.5,50S107.89999999999999,50,103.74999999999999,50S87.14999999999999,50,83,50S66.4,50,62.25,50S45.65,50,41.5,50S24.9,50,20.75,50Q17.983333333333334,50,0,50Z" class="area" fill="rgba(255,255,255,0.5)"></path></g></svg>
                    </div>--%>
                </div>
            </div>
            <div class="col-md-4 mg-t-20">
                <div class="bg-reef rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            Pending for EPF Creation (No of Bills & Amount)
                        </label>
                        <hr />
                        <div class="mg-l-20">
                            <center>
                                <a href="#" target="_blank"><u>

                                    <span class="count" id="PendingCreatedEPFBill" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span>
                                </u></a>
                                <div>

                                    <span class="count1" id="PendingCreatedEPFAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: white;"></span>
                                </div>
                            </center>
                        </div>
                    </div>

                </div>
            </div>

        </div>
        <div class="row" style="margin-bottom: 20px;">
            <div class="col-md-4 mg-t-20">
                <div class="bg-reef rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            Payment by NEFT Godown (No of Bills & Amount)
                        </label>
                        <hr />
                        <div class="mg-l-20">
                            <center>
                                <a href="Public/NEFT/Payment_RegionGeneratedBillSummary.aspx" target="_blank"><u>

                                    <span class="count" id="NEFTBill" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white;">00</span>
                                </u></a>
                                <div>

                                    <span class="count1" id="NEFTAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: white; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: white;"></span>
                                </div>
                            </center>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-md-4 mg-t-20">
                <div class="bg-mantle rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex
        align-items-center">
                        <label style="color: White">
                            Pending for NEFT to Godown (No of Bills & Amount)
                        </label>
                        <hr />
                        <div class="mg-l-20 blink">
                            <center>
                                <a href="#"
                                    target="_blank"><u>

                                        <span class="count" id="PendingNEFTBill" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span>
                                    </u></a>
                                <div>

                                    <span class="count1" id="PendingNEFTAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: red;"></span>
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
            <!-- col-3 -->
            <div class="col-md-4 mg-t-20">
                <div class="bg-mojito-mix rounded overflow-hidden zoom">
                    <div class="pd-x-20 pd-t-20 d-flex align-items-center">
                        <label style="color: White">
                            Pending Bill for Payment at HO MPSCSC
                        </label>
                        <hr />
                        <div class="mg-l-20 blink">
                            <center>
                                <a href="#"
                                    target="_blank"><u>

                                        <span class="count" id="PendingHOBill" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red;">00</span>
                                    </u></a>
                                <div>

                                    <span class="count1" id="PendingHOAmt" runat="server" style="font-familyigital-7 !important; font-size: 30px; color: red; font-weight: bold;">00</span><span style="font-familyigital-7 !important; font-size: 30px; color: red;"></span>
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>




        </div>
        <!-- col-3 -->
        <!-- col-3 -->

        <!-- col-3 -->
        <hr />
        <script>
            $('.count').each(function () {
                $(this).prop('Counter', 0).animate({
                    Counter: $(this).text()
                }, {
                    duration: 4000, easing: 'swing', step: function (now) {
                        $(this).text(Math.ceil(now));
                    }
                });
            }); </script>
        <style type="text/css">
            .Circle {
                height: 150px;
                width: 50%;
                background-color: #ceccb966;
                border-radius: 50%;
                display: inline-block;
            }

            .blink {
                animation: blinker 1s linear infinite;
            }

            @keyframes blinker {
                50% {
                    opacity: 0;
                }
            }
        </style>
</asp:Content>
