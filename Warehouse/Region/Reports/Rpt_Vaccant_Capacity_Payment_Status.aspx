<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Region/Reports/Rpt_Vaccant_Capacity_Payment_Status.aspx.cs" Inherits="Region_Reports_Rpt_Vaccant_Capacity_Payment_Status" %>

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

        .status {
            font-weight: 600;
            padding: 4px 8px;
            border-radius: 6px;
            display: inline-block;
        }

            .status.pending {
                background: #dc3545;
                color: #fff;
            }

            .status.completed {
                background: #198754;
                color: #fff;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">

        <fieldset>
            <legend>Vaccant Capacity Payment Status</legend>
            <div class="row">
                <div class="table-responsive">
                    <asp:GridView ID="grdbill" runat="server" CssClass="table table-bordered table-hover"
                        AutoGenerateColumns="false" ShowFooter="true" FooterStyle-Font-Bold="true"
                        OnRowDataBound="grdbill_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%# ((grdbill.PageIndex * grdbill.PageSize) + Container.DataItemIndex + 1) %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                            <asp:BoundField DataField="Branch_Name" HeaderText="Branch Name" />
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                            <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number" />
                            <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" />
                            <asp:BoundField DataField="Month" HeaderText="Month" />
                            <asp:BoundField DataField="Net_Amount" HeaderText="Net Amount"
                                DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="TDS_Amount" HeaderText="TDS Amount"
                                DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="Other_Deduction" HeaderText="Other Deduction"
                                DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="Credit_Amount" HeaderText="Credit Amount to Godown Owner"
                                DataFormatString="{0:N2}" />
                             <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date"
                                DataFormatString="{0:N2}" />
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Reference">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" OnClick="Edit">
                                                                                <%# Eval("Reference_No") %>
                                    </asp:LinkButton>
                                    <asp:HiddenField ID="hdnDistrict_Id" runat="server" Value='<%# Eval("District_Id") %>' />
                                    <asp:HiddenField ID="hdnBranch_Id" runat="server" Value='<%# Eval("Branch_Id") %>' />
                                    <asp:HiddenField ID="hdnBank_Type" runat="server" Value='<%# Eval("Bank_Type") %>' />
                                    <asp:HiddenField ID="hdnRefno" runat="server" Value='<%# Eval("Reference_No") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Payment Status">
                                <ItemTemplate>
                                    <span class='<%# (DataBinder.Eval(Container.DataItem, "Payment_Status").ToString() == "Pending" ? "status pending" : "status completed") %>'>
                                        <%# Eval("Payment_Status") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

