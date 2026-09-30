<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/NCCF_Storage_Bill_Status_Branch_And_RM_Fro_State.aspx.cs" Inherits="StatePages_NCCF_Storage_Bill_Status_Branch_And_RM_Fro_State" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />

    <style type="text/css">
        .content-wrapper {
            padding: 20px;
        }

        fieldset {
            border: 2px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 8px;
            padding-left: 20px;
            background-color: #fff;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
        }

        legend {
            padding: 6px 14px;
            border-radius: 20px;
            border: 2px solid #0d6efd;
            font-size: 18px;
            font-weight: 700;
            color: #0d6efd;
            background: #f4f8ff;
        }

        .table-visible {
            font-size: 13.5px;
            border-collapse: collapse;
            width: 100%;
            margin-bottom: 0px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
            border-radius: 4px;
            overflow: hidden;
        }

            /* Direct Grid Header Formatting */
            .table-visible tr th, .table-visible thead th {
                background: linear-gradient(180deg, #3b8dd3 0%, #0f172a 100%) !important;
                color: #ffffff !important;
                text-align: center !important;
                vertical-align: middle !important;
                padding: 14px 10px !important;
                font-weight: 700 !important;
                font-size: 13px !important;
                text-transform: uppercase !important;
                letter-spacing: 0.5px !important;
                border: 1px solid #334155 !important;
            }

            /* Standard Rows Overwrite Settings */
            .table-visible tr td {
                padding: 11px 10px !important;
                vertical-align: middle !important;
                color: #1e293b !important;
                border: 1px solid #cbd5e1 !important;
                background-color: #ffffff;
            }

                .table-visible tr td:nth-child(n+4) {
                    text-align: right !important;
                    font-weight: 600 !important;
                }

            .table-visible tr:hover td {
                background-color: #f8fafc !important;
            }

            /* STRICT OVERWRITE FOR PENDING ROWS AND TOTALS */
            .table-visible tr.pending-row td {
                background-color: #fef2f2 !important;
                border-color: #fca5a5 !important;
                color: #000000 !important;
            }

            .table-visible tr.subtotal-row td {
                background-color: #f1f5f9 !important;
                color: #0d6efd !important;
                border-top: 1.5px solid #0d6efd !important;
                border-bottom: 1.5px solid #0d6efd !important;
                font-weight: 700 !important;
            }

            .table-visible tr.grandtotal-row td {
                background-color: #e2e8f0 !important;
                color: #0f172a !important;
                border-top: 2px solid #0f172a !important;
                border-bottom: 3px double #0f172a !important;
                font-size: 14px !important;
                font-weight: 800 !important;
            }

        .pending-value {
            color: #dc2626 !important;
            font-weight: 700 !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="row" runat="server" id="grdbill" visible="true">
            <div class="col-12">
                <fieldset>
                    <legend><i class="fa fa-file-text-o"></i>NCCF Pending Bills Status (District Wise)</legend>
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="grpendding" AutoGenerateColumns="false" ShowFooter="false"
                            CssClass="table-visible" OnRowDataBound="grpendding_RowDataBound">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#1e3a8a" ForeColor="White" Font-Bold="true" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Region Name" HeaderStyle-Width="160px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District Name" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDistrict" runat="server" Text='<%# Eval("District_Name") %>'
                                            OnClick="lnkDistrict_Click" CommandArgument='<%# Eval("District_Id") %>'
                                            Font-Underline="true" ForeColor="#0d6efd" Font-Bold="true"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Bills Generated" HeaderStyle-Width="140px">
                                    <ItemTemplate><%# Eval("Total_Bills_Generated_At_BM") %></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bills Submitted (BM → RM)" HeaderStyle-Width="170px">
                                    <ItemTemplate><%# Eval("Bills_Submitted_By_BM_To_RM") %></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bills Pending at BM" HeaderStyle-Width="140px">
                                    <ItemTemplate>
                                        <span class='<%# Convert.ToInt32(Eval("Bills_Pending_At_BM")) > 0 ? "pending-value" : "" %>'><%# Eval("Bills_Pending_At_BM") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bills Submitted (RM → NCCF)" HeaderStyle-Width="180px">
                                    <ItemTemplate>
                                        <%# Eval("Bills_Submitted_By_RM_To_NCCF") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bills Pending at RM" HeaderStyle-Width="140px">
                                    <ItemTemplate>
                                        <span class='<%# Convert.ToInt32(Eval("Bills_Pending_At_RM")) > 0 ? "pending-value" : "" %>'>
                                            <%# Eval("Bills_Pending_At_RM") %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
