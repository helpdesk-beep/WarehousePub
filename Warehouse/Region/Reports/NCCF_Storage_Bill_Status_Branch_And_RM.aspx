<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master"
    AutoEventWireup="true"
    CodeFile="~/Region/Reports/NCCF_Storage_Bill_Status_Branch_And_RM.aspx.cs"
    Inherits="Region_Reports_NCCF_Storage_Bill_Status_Branch_And_RM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <!-- Bootstrap -->
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Font Awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />

    <style>
        .content-wrapper {
            padding: 20px;
        }

        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        legend {
            padding: 6px 14px;
            border-radius: 20px;
            border: 1px solid #0d6efd;
            font-size: 18px;
            font-weight: 600;
            color: #0d6efd;
            background: #f4f8ff;
        }

        .table {
            font-size: 13px;
        }

            .table th {
                background-color: #e7f0ff;
                background-color: #e7f0ff;
                color: #000;
                text-align: center;
                vertical-align: middle;
                font-weight: 600;
            }

            .table td {
                vertical-align: middle;
                color: #000;
            }

        .table-bordered th,
        .table-bordered td {
            border: 1px solid #000;
        }

        .table tfoot td {
            background-color: #f3f3f3;
            font-weight: bold;
            text-align: left !important;
        }

        .table-hover tbody tr:hover {
            background-color: #f8fbff;
        }

        .grid-title {
            font-size: 20px;
            font-weight: 600;
            color: #0d6efd;
            margin-bottom: 10px;
        }
        /* Table overall */
        .table {
            font-size: 13px;
            border-collapse: collapse;
        }

            /* Header */
            .table thead th {
                background: linear-gradient(to bottom, #eaf2ff, #d6e6ff) !important;
                color: #000;
                text-align: center;
                vertical-align: middle;
                padding: 10px 8px;
                font-weight: 600;
                font-size: 13px;
                border: 1px solid #000;
            }

            /* Body cells */
            .table tbody td {
                padding: 8px 8px;
                font-size: 13px;
            }

                /* Text alignment */
                .table tbody td:nth-child(n+5),
                .table tfoot td:nth-child(n+5) {
                    text-align: right;
                }

        /* Pending rows – soft highlight */
        .pending-row {
            background-color: #fff1f1 !important;
        }

        /* Footer (Grand Total) */
        .table tfoot td {
            background: #d9d9d9 !important;
            font-weight: 700;
            font-size: 13.5px;
            border-top: 3px double #000;
        }

            /* GRAND TOTAL text */
            .table tfoot td:first-child {
                text-align: right !important;
            }

        /* Responsive scroll */
        .table-responsive {
            max-height: none; /* height unlimited */
            overflow-y: visible; /* no scrollbar */
        }

        .pending-value {
            color: #c70000;
            font-weight: 600;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="row" runat="server" id="grdbill" visible="false">
            <div class="col-12">
                <fieldset>
                    <legend><i class="fa fa-file-text-o"></i>NCCF Pending Bills Status (Branch & RM)</legend>
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="grpendding" AutoGenerateColumns="false" ShowFooter="true"
                            CssClass="table table-bordered table-hover" OnRowDataBound="grpendding_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="60px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Region Name" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <%# Eval("Regionnm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="District Name" HeaderStyle-Width="140px">
                                    <ItemTemplate>
                                        <%# Eval("District_Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Branch Name" HeaderStyle-Width="180px">
                                    <ItemTemplate>
                                        <%# Eval("Branch_Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commodity Name" HeaderStyle-Width="180px">
                                    <ItemTemplate>
                                        <%# Eval("Commodity_Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <%# Eval("Crop_Year") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Financial Year" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <%# Eval("Financial_Year") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Bills Generated" HeaderStyle-Width="140px">
                                    <ItemTemplate>
                                        <%# Eval("Total_Bills_Generated_At_BM") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bills Submitted (BM → RM)" HeaderStyle-Width="170px">
                                    <ItemTemplate>
                                        <%# Eval("Bills_Submitted_By_BM_To_RM") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bills Pending at BM" HeaderStyle-Width="140px">
                                    <ItemTemplate>
                                        <%# Eval("Bills_Pending_At_BM") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bills Submitted (RM → NCCF)" HeaderStyle-Width="180px">
                                    <ItemTemplate>
                                        <%# Eval("Bills_Submitted_By_RM_To_NCCF") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bills Pending at RM" HeaderStyle-Width="140px">
                                    <ItemTemplate>
                                        <%# Eval("Bills_Pending_At_RM") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>
                    </div>

                </fieldset>

            </div>
        </div>
    </div>
</asp:Content>
