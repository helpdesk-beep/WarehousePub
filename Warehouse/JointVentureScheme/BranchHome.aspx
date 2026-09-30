<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/JointVentureScheme/BranchHome.aspx.cs" Inherits="JointVentureScheme_BranchHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Warehouse Home</title>
    <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <%--<link href="css/bootstrap.css" rel="stylesheet"/>
<link href="css/bootstrap-responsive.css" rel="stylesheet"/>--%>
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="js/menu.js"></script>
    <script type="text/javascript" src="js/slideshow.js"></script>
    <script type="text/javascript" src="js/cufon-yui.js"></script>
    <script type="text/javascript" src="js/Arial.font.js"></script>
    <style>
        ul.svertical {
            width: 220px; /* width of menu */
            overflow: auto;
            background: #f4f4f4; /* background of menu */
            margin: 0;
            padding: 0;
            padding-top: 7px; /* top padding */
            list-style-type: none;
        }

            ul.svertical li {
                text-align: right; /* right align menu links */
            }

                ul.svertical li a {
                    position: relative;
                    display: inline-block;
                    text-indent: 5px;
                    overflow: hidden;
                    background: rgb(1, 138, 180); /* initial background color of links */
                    font: bold 16px Germand;
                    text-decoration: none;
                    padding: 5px;
                    margin-bottom: 5px; /* spacing between links */
                    color: White;
                    -moz-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8); /* inner right shadow added to each link */
                    -webkit-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    -moz-transition: all 0.2s ease-in-out; /* CSS3 transition of hover properties */
                    -webkit-transition: all 0.2s ease-in-out;
                    -o-transition: all 0.2s ease-in-out;
                    -ms-transition: all 0.2s ease-in-out;
                    transition: all 0.2s ease-in-out;
                }

                    ul.svertical li a:hover {
                        padding-right: 30px; /* add right padding to expand link horizontally to the left */
                        color: Black;
                        background: rgb(153,249,75);
                        -moz-box-shadow: inset -3px 0 2px rgba(114,114,114, 0.8); /* contract inner right shadow */
                        -webkit-box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                        box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                    }

                    ul.svertical li a:before { /* CSS generated content: slanted right edge */
                        content: "";
                        position: absolute;
                        left: 0;
                        top: 0;
                        border-style: solid;
                        border-width: 70px 0 0 20px; /* Play around with 1st and 4th value to change slant degree */
                        border-color: transparent transparent transparent #f4f4f4; /* change black to match the background color of the menu UL */
                    }

        .style4 {
            height: 25px;
        }
    </style>
</head>
<body style="background-color: White">
    <form id="form1" runat="server">
        <div id="bg">
            <div class="wrap">

                <img src="../images/CH.jpg" style="width: 100%" alt="" height="150" />
                <%--            
            <div style="background-color: #66CCFF" align="Right">
                 <p style="font-size: medium; color: #008080;"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Welcome&nbsp;<asp:Label ID="lbluser" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" align="left">Log out</asp:LinkButton>&nbsp;&nbsp;</p>
            </div>--%>
                <table style="width: 100%; font-size: medium; font-weight: bold;">
                    <tr>
                        <td style="font-size: medium;" colspan="2">
                            <table style="width: 100%; height: 30px; font-size: medium;">
                                <tr>
                                    <td style="background-color: #008CBA; width: 70PX;" align="center"></td>
                                    <td style="background-color: #008CBA; font-size: medium; color: White; width: 80%" align="center">Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                                    <td style="background-color: #008CBA; width: 70px" align="center">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="White">Log out</asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: White; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: Black;">
                                JVS Godown Operations
                            </p>
                        </td>

                    </tr>

                    <tr id="Tr3" runat="server" style="height: 30px">
                        <td class="style4" style="color: #008080">&nbsp;&nbsp;1. <a style="font-size: medium; color: #008080;"
                            font-underline="True" id="a6" forecolor="#008080" href="UM_InspectionPPF.pdf">User Manual to Generate Inspection Pre Printed Form</a>
                        </td>
                        <td class="style4" style="color: #008080">&nbsp;&nbsp;2. <a style="font-size: medium; color: #008080;"
                            font-underline="True" forecolor="#008080" id="a1"
                            href="UM_InspectionDataEntryFormVer1.0.pdf">User Manual to Fill Inspected Detail in Inspection Form</a>
                        </td>
                    </tr>
                    


                    <tr id="TrReg" runat="server" style="height: 30px">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3. 
                              <asp:LinkButton
                                  ID="link1" Text="Fill Warehouse Inspection Details" runat="server" Visible="true" Enabled="false"
                                  PostBackUrl="~/JointVentureScheme/Branch_FillInspectionNew.aspx" ForeColor="#008080"
                                  Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton7" Text="Fill Godown Agreement Detail" runat="server" Visible="true" Enabled="false"
                                    PostBackUrl="~/JointVentureScheme/Agreement.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                        
                    </tr>
                    <tr id="Tr2" runat="server" style="height: 30px">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;5.
                                <asp:LinkButton
                                    ID="LinkButton8" Text="Re-Inspection for Pending Inspected Godown" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillPendingInspection.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                         <td style="font-size: medium; color: #008080;">&nbsp;&nbsp;6.
                            <asp:LinkButton
                                ID="LinkButton6" Text="Print Inspection Format for Offered Godowns " runat="server"
                                PostBackUrl="~/JointVentureScheme/Branch_PrintNewInspFormate.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                        </td>
                    </tr>


                    <tr id="Tr4" runat="server" style="height: 30px">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;7.
                                <asp:LinkButton
                                    ID="LinkButton13" Text="Agree/Disagreeable report for fumigation work" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FumigationWorkReport.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                         <td>
                          <p style="font-size: medium; color: #008080;">&nbsp;&nbsp;8. <asp:LinkButton 
                            ID="LinkButton26" Text="Insurance For Offered Godown(JVS 2021-22)" runat="server" Visible="true" 
                            PostBackUrl="~/JointVentureScheme/Godown_insurance_JVS21_22.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton></p>
                     </td> 
                    </tr>
                     <tr id="Tr21" runat="server" style="height: 30px">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;9.
                                <asp:LinkButton
                                    ID="LinkButton34" Text="FIFO wise Godown Priority List for Rabi 2023-24 Mapping" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FIFO_wise_Godown_Inflow.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    
                    </tr>
                    <tr id="Tr22" runat="server" style="height: 30px">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;10.
                                <asp:LinkButton
                                    ID="LinkButton35" Text="Report For Self/PMS 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Rpt_Region_Pms_Self_Offer_Choise_Filling_For_Branch.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    
                    </tr>

                    <tr id="Tr13" runat="server" style="height: 30px">
                    </tr>
                    <tr id="Tr11" runat="server" style="height: 10px">
                    </tr>
                     <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #6a6a6a; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Branch Wise Godown Reports and Format
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr1" runat="server" style="height: 30px">
                        <td style="font-size: medium; color: #008080;">&nbsp;&nbsp;1.
                            <asp:LinkButton
                                ID="LinkButton5" Text="Offered Warehouse Report" runat="server" ForeColor="#008080"
                                PostBackUrl="~/JointVentureScheme/JVSBranchReport.aspx" Font-Underline="True"></asp:LinkButton>
                        </td>
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton10" Text="Check Payment Status For Offered Godown" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_CheckOffer_WH_Status.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>
                    <tr id="Tr17" runat="server" style="height: 30px">

                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton3" Text="Warehouse Inspection Report" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/BranchInspectionReport.aspx" ForeColor="#008080"
                                    Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>

                       <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton9" Text="Godown Agreemented Report" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/BranchAgreementedReport.aspx" ForeColor="#008080"
                                    Font-Underline="True"></asp:LinkButton>
                            </p>

                        </td>

                    </tr>


                    <tr id="Tr24" runat="server" style="height: 30px">

                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;5.
                                <asp:LinkButton
                                    ID="LinkButton32" Text="Check Payment Status For Offered Godown Rabi 2023-24" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_CheckOffer_WH_Status_Rabi_2023_24.aspx" ForeColor="#008080"
                                    Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>

                    </tr>
                 
                    <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #d58a8a; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Inspection Agreement 2020-21
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr10" runat="server" style="height: 30px;">

                        <td>
                          <%--  <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton14" Text="Fill Warehouse Inspection Details JVS 2020-21" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillInspection_JVS20_21.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>--%>
                                <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton14" Text="Fill Warehouse Inspection Details JVS 2020-21" runat="server" Visible="true"
                                    PostBackUrl="#" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>

                        </td>
                        <td>
                            <%--<p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton16" Text="Fill Godown Agreement Detail JVS 2020-21" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_JVS2020_21.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>--%>
                             <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton16" Text="Fill Godown Agreement Detail JVS 2020-21" runat="server" Visible="true"
                                    PostBackUrl="#" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr6" runat="server" style="height: 30px;">

                        <td>
                           <%-- <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton15" Text="Warehouse Inspection Report 2020-21" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/BranchInspectionReport2020_21.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>--%>
                               <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton15" Text="Warehouse Inspection Report 2020-21" runat="server" Visible="true"
                                    PostBackUrl="#" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>

                        </td>
                        <td>
                            <%--<p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton17" Text="Warehouse Agreement Report 2020-21" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2020_21.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>--%>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton17" Text="Warehouse Agreement Report 2020-21" runat="server" Visible="true"
                                    PostBackUrl="#" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #c75353; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Inspection Agreement 2021-22
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr14" runat="server" style="height: 30px;">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton18" Text="Fill Warehouse Inspection Details JVS 2021-22" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillInspection_JVS21_22.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                            <%--<p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton18" Text="Fill Warehouse Inspection Details JVS 2021-22" runat="server" Visible="true"
                                    PostBackUrl="#" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>--%>
                        </td>
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton19" Text="Fill Godown Agreement Detail JVS 2021-22" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_JVS2021_22.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                             <%--<p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton19" Text="Fill Godown Agreement Detail JVS 2021-22" runat="server" Visible="true"
                                    PostBackUrl="#" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>--%>
                        </td>
                    </tr>
                    <tr id="Tr12" runat="server" style="height: 30px; ">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton20" Text="Warehouse Inspection Report 2021-22" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/BranchInspectionReport2021_22.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                           <%-- <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton20" Text="Warehouse Inspection Report 2021-22" runat="server" Visible="true"
                                    PostBackUrl="#" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>--%>
                        </td>
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton21" Text="Warehouse Agreement Report 2021-22" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2021_22.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                            <%--<p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton21" Text="Warehouse Agreement Report 2021-22" runat="server" Visible="true"
                                    PostBackUrl="#" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>--%>
                        </td>

                    </tr>


                      <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #c75353; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Inspection Agreement Rabi 2022-23
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr9" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton22" Text="Fill Warehouse Inspection Details JVS 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillInspection_JVS22_23.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton23" Text="Fill Godown Agreement Detail JVS 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_JVS2022_23.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>
                    <tr id="Tr15" runat="server" style="height: 30px">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton24" Text="Warehouse Inspection Report 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/BranchInspectionReport2022_23.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                             <%--   ~/JointVentureScheme/BranchInspectionReport2021_22.aspx--%>
                                <%--~/JointVentureScheme/JvsInspectionReportBranch22_23.aspx--%>
                            </p>
                        </td>
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton25" Text="Warehouse Agreement Report 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2022_23.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                              <%--  ~/JointVentureScheme/Agreement_Report_JVS2022_23.aspx--%>
                            </p>
                        </td>

                    </tr>
                       <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #c75353; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Warehouse Facilities Inspection 2022-23
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr16" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton27" Text="Print Inspection Format for Offered Godowns" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_PrintInspFormate_2022_23.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                          <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton28" Text="Fill Warehouse Inspection Details JVS 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillInspection_Kharif_JVS22_23.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>
                    <tr id="Tr18" runat="server" style="height: 30px">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton29" Text="Warehouse Inspection Report Kharif 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/BranchInspectionReport2022_23_Kharif.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                             <%--   ~/JointVentureScheme/BranchInspectionReport2021_22.aspx--%>
                                <%--~/JointVentureScheme/JvsInspectionReportBranch22_23.aspx--%>
                            </p>
                        </td>
                        </tr>
                      <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #c75353; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Inspection Agreement Kharif 2022-23
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr19" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton30" Text="Warehouse Inspection  Kharif 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillInspection_JVS22_23_Kharif.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton31" Text="Fill Godown Agreement Detail JVS 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_JVS2022_23_Kharif.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>
                    <tr id="Tr20" runat="server" style="height: 30px">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                
                                <asp:LinkButton
                                    ID="LinkButton37" Text="Warehouse Inspection Report Kharif 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/BranchInspectionReport2022_23_Kharif_JVS.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                                
                             <%--   ~/JointVentureScheme/BranchInspectionReport2021_22.aspx--%>
                                <%--~/JointVentureScheme/JvsInspectionReportBranch22_23.aspx--%>
                            </p>
                        </td>
                        <%--<td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton32" Text="Warehouse Inspection Report 2022-23" runat="server" Visible="true"
                                    PostBackUrl="#" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                                ~/JointVentureScheme/BranchInspectionReport2021_22.aspx
                                ~/JointVentureScheme/JvsInspectionReportBranch22_23.aspx
                            </p>
                        </td>--%>
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton33" Text="Warehouse Agreement Report 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2022_23_Kharif.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                              <%--  ~/JointVentureScheme/Agreement_Report_JVS2022_23.aspx--%>
                            </p>
                        </td>

                    </tr>
                     <tr id="Tr23" runat="server" style="height: 30px">
                        
                       <%-- <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton37" Text="Warehouse Agreement Report 2022-23" runat="server" Visible="true"
                                    PostBackUrl="#" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>--%>

                    </tr>


                     <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #c75353; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Inspection Agreement Rabi 2023-24
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr25" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton36" Text="Print Inspection Format for Offered Godowns Rabi 2023-24" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_PrintInspFormate_2023_24.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton38" Text="Warehouse Inspection Rabi 2023-24" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillInspection_Rabi_JVS23_24.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>

                    <tr id="Tr28" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton40" Text="Fill Godown Agreement Detail Rabi 2023-24" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_JVS2023_24_Rabi.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton41" Text="Warehouse Agreement Report Rabi 2023-24" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2023_24_Rabi.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>


                     <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #c75353; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Inspection Agreement Kahrif 2023-24
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr29" runat="server" style="height: 30px">
                       
                       <%-- <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton42" Text="Print Inspection Format for Offered Godowns Rabi 2023-24" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_PrintInspFormate_2023_24.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>--%>
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton43" Text="Warehouse Inspection Kharif 2023-24" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillInspection_Kharif_JVS23_24.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton44" Text="Fill Godown Agreement Detail Kharif 2023-24" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_JVS2023_24_Kharif.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                        </tr>
                    <%--BEGIN--%>
                     <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #c75353; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Inspection Agreement Rabi 2024-25
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr26" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton39" Text="Print Inspection Format for Offered Godowns Rabi 2024-25" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_PrintInspFormate_2024_25.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton42" Text="Warehouse Inspection Rabi 2024-25" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillInspection_Rabi_JVS24_25.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>

                    <tr id="Tr30" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton45" Text="Fill Godown Agreement Detail Rabi 2024-25" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_JVS2024_25_Rabi.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton46" Text="Warehouse Agreement Report Rabi 2024-25" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2024_25_Rabi.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>

                    <tr id="Tr31" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;5.
                                <asp:LinkButton
                                    ID="LinkButton47" Text="Warehouse Inspection Report 2024-25" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/BranchInspectionReport2024_25.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                        
                    </tr>

                    <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #c75353; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Inspection Agreement Kahrif 2024-25
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr32" runat="server" style="height: 30px">                      
                     
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton48" Text="Warehouse Inspection Kharif 2024-25" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillInspection_Kharif_JVS24_25.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton49" Text="Fill Godown Agreement Detail Kharif 2024-25" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_JVS2024_25_Kharif.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                        </tr>
                     <tr id="Tr33" runat="server" style="height: 30px">                      
                     
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton50" Text="Warehouse Inspection Report 2024-25 Kharif" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/BranchInspectionReport2024_25_Kharif.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton51" Text="Warehouse Agreement Report 2024-25 Kharif" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2024_25_Kharif.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                        </tr>
                     <%--END--%>
                    <%--BEGIN--%>
                     <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #c75353; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Inspection Agreement Rabi 2025-26
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr34" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton52" Text="Print Inspection Format for Offered Godowns Rabi 2025-26" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_PrintInspFormate_2025_26.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton53" Text="Warehouse Inspection Rabi 2025-26" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillInspection_Rabi_JVS25_26.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>

                    <tr id="Tr35" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton54" Text="Fill Godown Agreement Detail Rabi 2025-26" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_JVS2025_26_Rabi.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton55" Text="Warehouse Agreement Report Rabi 2025-26" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2025_26_Rabi.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>

                    <tr id="Tr36" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;5.
                                <asp:LinkButton
                                    ID="LinkButton56" Text="Warehouse Inspection Report 2025-26" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/BranchInspectionReport2025_26.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                        
                    </tr>


                     <tr>
    <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #c75353; width: 100%;" align="center" colspan="2">
        <p style="font-size: 20px; color: white;">
            Inspection Agreement Rabi 2026-27
        </p>
    </td>

</tr>
<tr id="Tr37" runat="server" style="height: 30px">
   
    <td>
        <p style="font-size: medium; color: #008080;">
            &nbsp;&nbsp;1.
            <asp:LinkButton
                ID="LinkButton57" Text="Print Inspection Format for Offered Godowns Rabi 2026-27" runat="server" Visible="true"
                PostBackUrl="~/JointVentureScheme/Branch_PrintInspFormate_2026_27.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
        </p>
    </td>
     <td>
        <p style="font-size: medium; color: #008080;">
            &nbsp;&nbsp;2.
            <asp:LinkButton
                ID="LinkButton58" Text="Warehouse Inspection Rabi 2026-27" runat="server" Visible="true"
                PostBackUrl="~/JointVentureScheme/Branch_FillInspection_Rabi_JVS26_27.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
        </p>
    </td>
</tr>

<tr id="Tr38" runat="server" style="height: 30px">
   
    <td>
        <p style="font-size: medium; color: #008080;">
            &nbsp;&nbsp;3.
            <asp:LinkButton
                ID="LinkButton59" Text="Fill Godown Agreement Detail Rabi 2026-27" runat="server" Visible="true"
                PostBackUrl="~/JointVentureScheme/Agreement_JVS2026_27_Rabi.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
        </p>
    </td>
     <td>
        <p style="font-size: medium; color: #008080;">
            &nbsp;&nbsp;4.
            <asp:LinkButton
                ID="LinkButton60" Text="Warehouse Agreement Report Rabi 2026-27" runat="server" Visible="true"
                PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2026_27_Rabi.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
        </p>
    </td>
</tr>

<tr id="Tr39" runat="server" style="height: 30px">
   
    <td>
        <p style="font-size: medium; color: #008080;">
            &nbsp;&nbsp;5.
            <asp:LinkButton
                ID="LinkButton61" Text="Warehouse Inspection Report 2026-27" runat="server" Visible="true"
                PostBackUrl="~/JointVentureScheme/BranchInspectionReport2026_27.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
        </p>
    </td>
    
</tr>
                    <%--<tr id="Tr30" runat="server" style="height: 30px">
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton45" Text="Warehouse Agreement Report Rabi 2023-24" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2023_24_Rabi.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>

                   <tr id="Tr26" runat="server" style="height: 30px">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;5.
                                
                                <asp:LinkButton
                                    ID="LinkButton39" Text="Warehouse Inspection Report Rabi 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/BranchInspectionReport2023_24_Rabi.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                          
                            </p>
                        </td>  --%>                     
                        <%--<td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton40" Text="Warehouse Agreement Report 2022-23" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2022_23_Kharif.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>--%>

                    </tr>--%>
                     <tr id="Tr27" runat="server" style="height: 30px">
                        
                       <%-- <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton37" Text="Warehouse Agreement Report 2022-23" runat="server" Visible="true"
                                    PostBackUrl="#" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>--%>

                    </tr>
                   <%-- <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #009688; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Inspection Agreement 2022-23
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr15" runat="server" style="height: 30px">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton22" Text="Fill Warehouse Inspection Details JVS 2020-21" runat="server" Visible="true" Enabled="false"
                                    PostBackUrl="~/JointVentureScheme/Branch_FillInspection_JVS20_21.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                        
                         <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton24" Text="Fill Godown Agreement Detail JVS 2021-22" runat="server" Visible="true" Enabled="false"
                                    PostBackUrl="~/JointVentureScheme/Agreement_JVS2021_22.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>
                    <tr id="Tr16" runat="server" style="height: 30px">
                       
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;3.
                                <asp:LinkButton
                                    ID="LinkButton25" Text="Warehouse Inspection Report 2021-22" runat="server" Visible="true" Enabled="false"
                                    PostBackUrl="~/JointVentureScheme/BranchInspectionReport2021_22.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;4.
                                <asp:LinkButton
                                    ID="LinkButton23" Text="Warehouse Agreement Report 2021-22" runat="server" Visible="true" Enabled="false"
                                    PostBackUrl="~/JointVentureScheme/Agreement_Report_JVS2021_22.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>--%>

                    <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: #1dd3d3; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: white;">
                                Update Inspection and Agreement Capacity
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr5" runat="server" style="height: 30px">
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;1.
                                <asp:LinkButton
                                    ID="LinkButton11" Text="Re-Inspection of Vacant Capacity" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_ReInspectionForVacantCPT.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                        <td>
                            <p style="font-size: medium; color: #008080;">
                                &nbsp;&nbsp;2.
                                <asp:LinkButton
                                    ID="LinkButton12" Text="Update Agreemente Capacity" runat="server" Visible="true"
                                    PostBackUrl="~/JointVentureScheme/Branch_UpdateReAgreemetn.aspx" ForeColor="#008080" Font-Underline="True"></asp:LinkButton>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: White; width: 100%;" align="center" colspan="2">
                            <p style="font-size: 20px; color: Black;">
                                Owned Godown Operations
                            </p>
                        </td>

                    </tr>
                    <tr id="Tr7" runat="server" style="height: 30px; color: #008080;">
                        <td class="style4">
                            <%-- <p style="font-size: medium; color: #008080;">  &nbsp;&nbsp;&nbsp;<asp:LinkButton 
                                  ID="LinkButton2" Text="User Manual to Generate Inspection Pre Printed Form" runat="server" 
                                  PostBackUrl="~/JointVentureScheme/UM_InspectionPPF.pdf"
                                   Font-Underline="True"></asp:LinkButton></p>--%>
                                    &nbsp;&nbsp;1. <a style="font-size: medium; color: #008080;"
                                        font-underline="True" id="a2" href="UM_MPWLCGodownRegistartionVer1.0.pdf">User Manual to Fill Warehouse Registration Form</a>

                        </td>
                        <td colspan="4"
                            style="font-size: medium; color: #008080;">&nbsp;&nbsp;2.  
                          <asp:LinkButton
                              ID="LinkButton2" Text="Warehouse Registration " runat="server"
                              PostBackUrl="~/JointVentureScheme/OtherLoginRegistration.aspx"
                              Font-Underline="True" ForeColor="#408080"></asp:LinkButton>
                        </td>

                    </tr>
                    <tr id="Tr8" runat="server" style="height: 30px">
                        <td colspan="4"
                            style="font-size: medium; color: #008080;">&nbsp;&nbsp;3.  
                          <asp:LinkButton
                              ID="LinkButton4" Text="Preview Warehouse Registration Detail" runat="server"
                              PostBackUrl="~/JointVentureScheme/Gov_RegistrationPreview.aspx"
                              Font-Underline="True" ForeColor="Teal"></asp:LinkButton>
                        </td>

                    </tr>
                </table>
                <hr />
                <hr />

                <tr>
                    <td colspan="4">
                        <%--                            <br />
                            <p style="color:Red;">
                                  &nbsp;&nbsp;&nbsp; 1. Warehouse Registration Detail के लिए  Warehouse Registration & Offer Detail लिंक पर click करें|     
 <br />
                                </p>
                                 <p style="color:Red;">
                                  &nbsp;&nbsp;&nbsp; 2. Warehouse Registration Detail Update के लिए  Update Warehouse Registration Detail लिंक पर click करें|     
 <br />
                                </p>
                                <p style="color:Red;">
                                  &nbsp;&nbsp;&nbsp; 3. कृपया Warehouse Registration के लिए Warehouse Registration लिंक पर click करें|     
 <br />
                                </p>
                                 <p style="color:Red;">
                                 &nbsp;&nbsp;&nbsp;  4. भुगतान करने की स्थति में Payment for Registration of Warehouse लिंक पर क्लिक करे।
 <br />
                                </p>--%>
                        <%-- <p style="color:Red;">
                                   3.डुप्लीकेट रसीद की प्राप्ति हेतु कृपया लिंक "Duplicate Receipt for Warehouse Registration  " पर क्लिक करे। 
 <br />
                                </p>
                                <p style="color:Red;">
                                   4.JV/Rental scheme आवेदन हेतु लिंक "Application Form For JV Scheme" पर क्लिक करे।
 <br />
                                </p>--%>
                    </td>

                </tr>
                <div style="background-image: url('../images/div_bg.png')">
                    <table style="width: 100%">
                        <tr>
                            <td style="height: 20px;" colspan="5">
                                <img id="Img2" src="../Images/line.png" height="30px" width="100%" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" align="center">
                                <a href="http://www.mp.nic.in/">
                                    <img src="../Images/NIC-logo.png" width="200px" height="50px" alt="" />
                                </a>
                            </td>
                            <td style="width: 1%" align="center">
                                <img id="Img1" src="../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                            </td>
                            <td style="color: Navy; font-size: 8pt; width: 58%;" align="center">
                                <b>© 2018 &nbsp;National Informatics Centre.All Rights Reserved
                                                <br />
                                    Developed By : National Informatics Centre
                                                <br />
                                    Madhya Pradesh, Ministry of Communications and Information Technology</b>
                            </td>
                            <td style="width: 1%" align="center">
                                <img id="Logo" src="/../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                            </td>
                            <td style="width: 20%" align="center">
                                <table>
                                    <tr>
                                        <td><a href="http://india.gov.in/">
                                            <img src="../Images/natindialogo.png" width="100px" height="50px" alt="" />
                                        </a>
                                        </td>
                                        <td>
                                            <a href="http://www.digitalindia.gov.in/">
                                                <img src="../Images/di.png" width="100px" height="50px" alt="" />
                                            </a>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
