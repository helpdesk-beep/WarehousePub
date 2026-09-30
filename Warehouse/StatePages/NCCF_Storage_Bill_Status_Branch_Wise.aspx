<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="NCCF_Storage_Bill_Status_Branch_Wise.aspx.cs" Inherits="StatePages_NCCF_Storage_Bill_Status_Branch_Wise" %>

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
            padding: 6px 18px;
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

            .table-visible tr td {
                padding: 11px 10px !important;
                vertical-align: middle !important;
                color: #1e293b !important;
                border: 1px solid #cbd5e1 !important;
                background-color: #ffffff;
            }

                .table-visible tr td:nth-child(n+5) {
                    text-align: right !important;
                    font-weight: 600 !important;
                }

            .table-visible tr:hover td {
                background-color: #f8fafc !important;
            }

            .table-visible tr.pending-row td {
                background-color: #fef2f2 !important;
                border-color: #fca5a5 !important;
                color: #000000 !important;
            }

        .grandtotal-row {
            background-color: #e2e8f0 !important;
            font-weight: 800 !important;
            color: #0f172a !important;
        }

            .grandtotal-row td {
                border-top: 2px solid #0f172a !important;
                border-bottom: 3px double #0f172a !important;
                font-size: 14px !important;
                font-weight: 800 !important;
                background-color: #e2e8f0 !important;
                padding: 12px 10px !important;
            }

        .btn-back {
            background-color: #475569 !important;
            color: #ffffff !important;
            font-weight: 600 !important;
            padding: 8px 18px !important;
            border: none !important;
            border-radius: 6px;
            cursor: pointer;
            transition: all 0.2s ease;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }

            .btn-back:hover {
                background-color: #334155 !important;
                transform: translateY(-1px);
            }

        .pending-value {
            color: #dc2626 !important;
            font-weight: 700 !important;
        }

        .subtotal-row {
            background-color: #f1f5f9 !important; /* Light Grey background */
            font-weight: 700 !important;
            color: #1e293b !important;
        }

            .subtotal-row td {
                border-top: 1px solid #94a3b8 !important;
                border-bottom: 1px solid #94a3b8 !important;
                padding: 10px !important;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend><i class="fa fa-university"></i>Branch Wise Pending Status for:-
                <span style="color: #000; font-weight: 700;">
                    <asp:Literal ID="litDistrictHeading" runat="server"></asp:Literal></span>
            </legend>

            <div class="mb-3 mt-2">
                <asp:Button ID="btnBack" runat="server" Text="Back to District Report"
                    CssClass="btn-back" OnClick="btnBack_Click" UseSubmitBehavior="false" />
            </div>

            <div class="table-responsive" style="margin-top: 20px">
                <asp:GridView runat="server" ID="grdBranch" AutoGenerateColumns="false"
                    CssClass="table-visible" OnRowDataBound="grdBranch_RowDataBound" ShowFooter="true">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="60px">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Regionnm" HeaderText="Region Name" HeaderStyle-Width="150px" Visible="false" />
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" HeaderStyle-Width="150px" Visible="false" />

                        <%--    <asp:TemplateField HeaderText="Branch Name" HeaderStyle-Width="200px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBranch" runat="server" Text='<%# Eval("Branch_Name") %>'
                                    OnClick="lnkBranch_Click" CommandArgument='<%# Eval("Branch_Id") %>'
                                    Font-Underline="true" ForeColor="#0d6efd" Font-Bold="true"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Branch Name" HeaderStyle-Width="200px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBranch" runat="server"
                                    Text='<%# Eval("Branch_Name") %>'
                                    OnClick="lnkBranch_Click"
                                    CommandArgument='<%# Eval("Branch_Id") + "|" + Eval("Commodity_Id") + "|" + Eval("Crop_Year") + "|" + Eval("Financial_Year") %>'
                                    Font-Underline="true" ForeColor="#0d6efd" Font-Bold="true">
                              </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" />
                        <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" />
                        <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" />
                        <asp:BoundField DataField="Total_Bills_Generated_At_BM" HeaderText="Total Bills Generated" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" />
                        <asp:BoundField DataField="Bills_Submitted_By_BM_To_RM" HeaderText="Bills Submitted (BM → RM)" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" />
                        <asp:BoundField DataField="Bills_Pending_At_BM" HeaderText="Bills Pending at BM" />
                        <asp:BoundField DataField="Bills_Submitted_By_RM_To_NCCF" HeaderText="Bills Submitted (RM → NCCF)" />
                        <asp:BoundField DataField="Bills_Pending_At_RM" HeaderText="Bills Pending at RM" />
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
</asp:Content>
