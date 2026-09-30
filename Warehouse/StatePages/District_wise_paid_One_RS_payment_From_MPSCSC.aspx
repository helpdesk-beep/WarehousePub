<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/District_wise_paid_One_RS_payment_From_MPSCSC.aspx.cs" Inherits="StatePages_District_wise_paid_One_RS_payment_From_MPSCSC" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend>Godown Wise details paid One payment From MPSCSC to MPWLC</legend>
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group ">
                        <label style="margin-top: 10px">District Name</label>
                        <asp:DropDownList CssClass="form-control" ID="ddldistrict" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </fieldset>
        <fieldset id="grdone" runat="server" visible="false">
            <legend>Details</legend>
            <div class="row">
                <div class="col-md-2">
                    <asp:Button runat="server" CssClass="btn button2 btn-sm" Text="Exporttoexcel" ID="btnExport" OnClick="btnExport_Click" />
                </div>
            </div>
            <div class="row" style="margin-top: 20px">
                <div class="table-responsive">
                   <asp:GridView runat="server" ShowFooter="true" FooterStyle-Font-Bold="true" DataKeyNames="Bill_Number" ID="GrdBills"
                        CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Region" HeaderStyle-BackColor="LightBlue">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name" HeaderStyle-BackColor="LightBlue">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDepotName" Text='<%# Eval("DepotName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Bill" HeaderStyle-BackColor="LightBlue">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblBill_Number" Text='<%# Eval("Bill_Number") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Bill Amount" HeaderStyle-BackColor="LightBlue">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Bill Amount Received From MPSCSC" HeaderStyle-BackColor="LightBlue">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPayable_Amount" Text='<%# Eval("Payable_Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deduction by HO MPSCSC" HeaderStyle-BackColor="LightBlue">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDeduction_by_HO_MPSCSC" Text='<%# Eval("Deduction_by_HO_MPSCSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TDS Deduction by HO MPSCSC" HeaderStyle-BackColor="LightBlue">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblTDS_Deduction_by_HO_MPSCSC" Text='<%# Eval("TDS_Deduction_by_HO_MPSCSC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

