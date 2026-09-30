<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Nccf_Master.master" AutoEventWireup="true" CodeFile="~/NCCF/NCCF_Deduction_For_Final_Storage_Bill.aspx.cs" Inherits="NCCF_NCCF_Deduction_For_Final_Storage_Bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <style>
        .custom-grid {
            width: 100%;
            border-collapse: collapse;
            background: #fff;
            font-size: 14px;
        }
            /*#8ea9c5*/
            .custom-grid th {
                background: #8ea9c5 !important;
                color: white;
                text-align: center;
                padding: 10px !important;
                font-weight: 600;
            }

            .custom-grid td {
                padding: 8px !important;
                vertical-align: middle !important;
            }

            .custom-grid tr:nth-child(even) {
                background: #f2f6fc;
            }

            .custom-grid tr:hover {
                background: #d9ebff;
                transition: 0.2s;
            }



        /*#6063ad*/
        .BTNBLUE {
            background-color: #6063ad !important;
            color: #fff !important;
            border-radius: 50%;
            width: 45px;
            height: 45px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 18px;
            border: none;
            transition: 0.2s ease-in-out;
        }

            .BTNBLUE:hover {
                background: #46cb63;
            }

        .approve-dropdown {
            width: 100px;
            height: 30px;
        }

        .remark-box {
            width: 120px;
            height: 30px;
            margin-top: 5px;
        }
    </style>

    <style>
        /* Sticky Header */
        .sticky-header th {
            position: sticky;
            top: 0;
            z-index: 10;
            background: #0066cc !important;
            color: #fff !important;
        }

        /* Theme */
        .grid-theme {
            border-collapse: collapse;
            width: 100%;
            font-family: Arial;
        }

            .grid-theme td, .grid-theme th {
                padding: 8px;
                border: 1px solid #ddd;
            }

            .grid-theme tr:nth-child(even) {
                background-color: #f9f9f9;
            }

            .grid-theme tr:hover {
                background-color: #e2f1ff;
            }

        #searchBox {
            padding: 8px;
            width: 250px;
            margin-bottom: 12px;
            border-radius: 4px;
            border: 1px solid #aaa;
        }

        .final-amount {
            color: #d9534f; /* RED */
            font-weight: bold;
        }
    </style>
    <style>
        .highlight-total {
            background-color: #ffc107; /* Yellow background */
            color: #000; /* Black text */
            font-weight: bold;
            padding: 5px 10px;
            border-radius: 5px;
            border: 1px solid #ff9800; /* Orange border */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content_wrapper">
        <asp:Label runat="server" Visible="false" ID="lblmsg"></asp:Label>
        <fieldset>
            <legend>NCCF Deduction For Storage Bills</legend>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <div class="row" style="margin-bottom: 10px">
                            <div style="width: 100%; background-repeat: no-repeat;">
                                <div style="position: relative;">
                                    <p>
                                        <strong style="color: red; font-size: medium; margin-left: 50px">* UPLOAD FILE IN PDF FORMATE
                                        </strong>
                                        <br />
                                    </p>
                                </div>
                            </div>
                        </div>
                        <asp:GridView runat="server" DataKeyNames="Net_Amount" ID="GrdBills" OnRowCommand="GrdBills_RowCommand"
                            CssClass="custom-grid" AutoGenerateColumns="False" Autopostback="true" OnRowDataBound="GrdBills_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <asp:HiddenField runat="server" ID="hdnBill_Number" Value='<%#Eval("Bill_Number")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commodity Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommodity_Name" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnCommodityId" Value='<%#Eval("Commodity_ID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCrop_Year" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Count" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBill_Count" Text='<%# Eval("Bill_Count") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Bill Amount" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNet_Amount" Text='<%# Eval("Net_Amount") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Process Amount" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPass_Amount" runat="server" Text='<%# Eval("Pass_Amount") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deduction Amount" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDeduction_Amount" AutoComplete="off" Enabled="false" runat="server" Width="125" onkeypress="return isDecimalNumber(event)" CssClass="form-control" Text='<%# Eval("Deduction_Amount") %>' onkeyup="calculateFinal(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Amount" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFinalAmount" runat="server" CssClass="final-amount"></asp:Label>
                                        <asp:HiddenField ID="hdnFinalAmount" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Upload" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Image ID="imgUpload" ClientIDMode="Static" runat="server" Visible="false" Width="125" Height="149" src="../image/NurserRegImage.jpg" />
                                        <asp:FileUpload ID="IdFileUpload" onchange="previewUserImage()" Width="200" runat="server" CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update UTR" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtUpdate_UTR" AutoComplete="off" runat="server" Width="125" CssClass="form-control" Text='<%# Eval("UTR_No") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Payment Date" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <%-- <asp:TextBox ID="txtPayment_Date" runat="server" Width="125" MaxLength="10" CssClass="form-control" Text='<%# Eval("Payment_Date") %>' />--%>
                                        <div class="input-group" style="max-width: 160px">
                                            <asp:TextBox ID="txtPayment_Date" runat="server"
                                                CssClass="form-control datepicker"
                                                AutoComplete="off" />

                                            <span class="input-group-text btnCalendar" style="cursor: pointer">
                                                <i class="fa fa-calendar"></i>
                                            </span>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="10" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Button ID="btn_Save" ValidationGroup="fleUpload" runat="server" Text="Submit" OnClientClick="return confirm('Do you want to Submit This Record?');" CommandName="Submit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
    <script>
        function isDecimalNumber(evt) {
            var ch = String.fromCharCode(evt.which);

            // ✅ Allow only numbers and one decimal point
            if (!(/[0-9.]/.test(ch))) {
                return false;
            }

            // ✅ Only one dot allowed
            if (ch === "." && evt.target.value.indexOf('.') !== -1) {
                return false;
            }

            return true;
        }
    </script>

    <script type="text/javascript">
        function initCalendar() {

            if ($('.datepicker').length > 0) {

                $('.datepicker').datepicker('destroy');

                $('.datepicker').datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    todayHighlight: true,
                    endDate: new Date(),   // ✅ future disabled
                    orientation: "bottom"      // ✅ Calendar Neeche Khulega
                });
            }

            $(document).on('click', '.btnCalendar', function () {
                $(this).closest('.input-group').find('.datepicker').focus();
            });
        }

        $(document).ready(function () {
            initCalendar();
        });

        if (typeof Sys !== "undefined") {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                initCalendar();
            });
        }
    </script>

</asp:Content>

