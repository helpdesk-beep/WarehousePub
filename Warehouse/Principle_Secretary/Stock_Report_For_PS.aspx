<%@ Page Title="" Language="C#" MasterPageFile="~/Principle_Secretary/Principle_Secretary.master" AutoEventWireup="true" CodeFile="Stock_Report_For_PS.aspx.cs" Inherits="Principle_Secretary_Stock_Report_For_PS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Inventory/assets/datatable/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../Inventory/assets/datatable/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../../Inventory/DataTable_CssJs/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../../Inventory/DataTable_CssJs/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../Inventory/assets/css/FilpCss.css" rel="stylesheet" />
    <link href="../../Inventory/plugins/css/material-dashboard.css" rel="stylesheet" />
    <style>
        /*Calendar style*/
        th {
            font-weight: 400;
            background-color: #F3DFDF;
        }

        .table-condensed th {
            background-color: #D8F5F5;
        }
        /*Calendar style*/

        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        .select2-container .select2-selection--single {
            box-sizing: border-box;
            cursor: pointer;
            display: block;
            height: 35px;
            user-select: none;
            -webkit-user-select: none;
        }

        .select2-container--default .select2-selection--single {
            /*background-color: #fff;*/
            /*border: 1px solid #aaa;*/
            border-radius: 4px !important;
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

        select.form-control {
            border: 1px solid #767B83;
            border-radius: 8px;
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

        .pagination .paginate_button .active a {
            color: black !important;
            float: left;
            padding: 1px 1px;
            text-decoration: none;
        }
        /* .table-responsive {
            display: block;
        }*/
        .select2 {
            border: 1px solid #767B83;
            border-radius: 8px;
        }

        .content-wrapper {
            padding: 1.75rem 1.25rem;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #90A6D9;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 1050px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green"
            border="1px">
            <tr style="background-color: #90A6D9; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label14" runat="server" Text="Stock Report" ForeColor="Black" Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton202" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton202_Click">GODOWN WISE STOCK POSOTION</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton208" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton208_Click">DISTRICT WISE STOCK POSITION FOR ALL COMMODITY </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton1_Click">BRANCH WISE LAST 10 YEAR STOCK POSITION</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton2_Click">DISTRICT WISE LAST 10 YEAR STOCK POSITION</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton3" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton3_Click">DISTRICT STORAGE WISE LAST 10 YEAR STOCK POSITION</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton4" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton4_Click">REGION STORAGE WISE LAST 10 YEAR STOCK POSITION</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton5" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton5_Click">BRANCH COMMODITY,CROP YEAR,DATE WISE STOCK POSITION</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">8.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton6" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton6_Click">GODOWN COMMODITY,CROP YEAR,DATE WISE STOCK POSITION</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">9.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton7" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton7_Click">COMPLETE JVS DISTRICT WISE STOCK POSITION</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">10.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton8" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton8_Click">SILO BAGS STOCK POSITION CROP YEAR WISE</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">11.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton9" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton9_Click">CAP STOCK POSITION CROP YEAR WISE</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">12.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton10" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton10_Click">CAPACITY WISE GODOWN LIST</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">13.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton11" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton11_Click">VIEW UPDATE GODOWN LIST 2024</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">14.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton12" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton12_Click">DISTRICT COMMODITY,CROPYEAR,DATE WISE STOCK POSITION</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">15.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton13" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton13_Click">GODOWN COMMODITY,CROPYEAR,HIRED TYPE ,STORAGE,DATE WISE STOCK POSITION</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">16.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton14" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton14_Click">DISTRICT HIRED TYPE GODOWN CAPACITY</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">17.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton15" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton15_Click">STEEL SILO</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">18.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton16" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton16_Click">Owned,PVT.PEG,BOT-AUB,CWC,Steel Silo,Hired,Tribal Schemes Godown Capacity and available Stock Position in M.T.</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">29.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton17" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton17_Click">Godown Wise Capacity and Vacant Capacity Details</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <span style="color: Navy; font-weight: bold; font-size: 10pt">30.</span></td>
                <td>
                    <asp:LinkButton ID="LinkButton18" runat="server" ValidationGroup="lik2"
                        ForeColor="navy" Font-Bold="true"
                        Font-Size="10pt" OnClick="LinkButton18_Click">Region,District wise Total Capacity,Available Capacity,Vacant Capacity Report</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

