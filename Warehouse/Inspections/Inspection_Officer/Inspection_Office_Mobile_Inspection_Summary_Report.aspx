<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="~/Inspections/Inspection_Officer/Inspection_Office_Mobile_Inspection_Summary_Report.aspx.cs" Inherits="Inspections_Inspection_Officer_Inspection_Office_Mobile_Inspection_Summary_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .custom-grid {
            font-size: 13px;
            border-collapse: collapse !important;
        }

            .custom-grid th {
                background-color: #4b4b4b !important;
                color: white !important;
                padding: 12px 8px !important;
                font-weight: 500;
            }

            .custom-grid td {
                padding: 10px 8px !important;
            }

        .btn-info {
            background-color: #5bc0de;
            border-color: #46b8da;
            font-size: 12px;
        }

            .btn-info:hover {
                background-color: #31b0d5;
                color: white;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid mt-4">
        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white d-flex justify-content-between align-items-center">
                <h6 class="mb-0"><i class="fa-solid fa-list-check me-2"></i>Inspection Summary Records</h6>
                <span class="badge bg-primary">Total Records:
                    <asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>
                </span>
            </div>
            <div class="card-body p-0">
                <div class="table-responsive">
                    <asp:GridView ID="gvInspectionSummary" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-hover mb-0 custom-grid"
                        GridLines="None" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                    <asp:HiddenField runat="server" ID="hdnDistrict_Id" Value='<%# Eval("District_Id") %>' />
                                    <asp:HiddenField runat="server" ID="hdnemployeeid" Value='<%# Eval("Emp_ID") %>' />
                                    <asp:HiddenField runat="server" ID="hdninsp_type_id" Value='<%# Eval("Inspection_Quarter") %>' />
                                    <asp:HiddenField runat="server" ID="hdnVerificationType" Value='<%# Eval("Verification_Type") %>' />
                                    <asp:HiddenField runat="server" ID="hdnFinancial_Year" Value='<%# Eval("Financial_Year") %>' />
                                    <asp:HiddenField runat="server" ID="hdnOrder_no" Value='<%# Eval("Order_no") %>' />
                                    <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_ID") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="OfficerName" HeaderText="Officer Name" />
                            <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                            <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                            <asp:BoundField DataField="Order_no" HeaderText="Order No" />
                            <asp:BoundField HeaderText="Financial Year" DataField="Financial_Year" />
                            <asp:TemplateField HeaderText="Inspection Period">
                                <ItemTemplate>
                                    <span class="badge rounded-pill bg-info text-dark">
                                        <%# Eval("Inspection_Status") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inspection Type">
                                <ItemTemplate>
                                    <span class="badge rounded-pill bg-info text-dark">
                                        <%# Eval("VerificationType") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View Summary">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkViewSummary" runat="server"  CssClass="btn btn-sm btn-info text-white" CommandName="ViewSummary"
                                        OnCommand="lnkViewSummary_Command" > <i class="fa-solid fa-eye me-1"></i> View Summary Godown Wise
                                         </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="table-secondary text-dark text-center fw-bold" />
                        <RowStyle CssClass="align-middle text-center" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

