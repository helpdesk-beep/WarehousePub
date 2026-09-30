<%@ Page Title="Godown Bill Wise Deduction Report" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Godown_Bill_Wise_Deduction_Report.aspx.cs" Inherits="Region_Reports_Godown_Bill_Wise_Deduction_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />

    <style type="text/css">
        .form-group {
            margin-bottom: 15px;
        }

        .form-control {
            width: 100%;
            padding: 6px 12px;
            height: 34px;
        }

        .btn-search {
            margin-top: 24px;
        }
        /* Select2 की विड्थ को ठीक करने के लिए */
        .select2-container {
            width: 100% !important;
        }

        .select2-selection--single {
            height: 34px !important;
            padding-top: 2px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding: 20px;">
        <h2>Godown Bill Wise Deduction Report</h2>
        <hr />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="display: flex; gap: 20px; align-items: center; flex-wrap: wrap;">

                    <div class="form-group" style="flex: 1; min-width: 200px;">
                        <label><b>Select District:</b></label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group" style="flex: 1; min-width: 200px;">
                        <label><b>Select Branch/Depot:</b></label>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group" style="flex: 1; min-width: 200px;">
                        <label><b>Select Godown:</b></label>
                        <asp:DropDownList ID="ddlGodown" runat="server" CssClass="form-control select2">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <asp:Button ID="btnSearch" runat="server" Text="Show Report" CssClass="btn-search" OnClick="btnSearch_Click" Style="padding: 7px 20px; background-color: #007bff; color: white; border: none; cursor: pointer;" />
                    </div>
                </div>

                <br />
                <hr />
                <br />

                <div style="width: 100%; overflow-x: auto;">
                    <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="true" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                        <EmptyDataTemplate>
                            <div style="text-align: center; color: red; padding: 10px; font-weight: bold;">No Data Found for Selected Criteria.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <script type="text/javascript">
        // पहली बार पेज लोड होने पर सर्च लागू करने के लिए
        $(document).ready(function () {
            BindSelect2();
        });

        // UpdatePanel के पोस्टबैक (AJAX) के बाद दोबारा सर्च लागू करने के लिए महत्वपूर्ण लॉजिक
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    BindSelect2();
                }
            });
        }

        // Select2 को इनिशियलाइज करने का कॉमन फंक्शन
        function BindSelect2() {
            $('.select2').select2({
                placeholder: "--Select Option--",
                allowClear: false
            });
        }
    </script>
</asp:Content>
