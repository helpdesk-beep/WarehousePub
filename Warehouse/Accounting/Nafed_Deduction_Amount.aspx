<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMasterNafed.master" AutoEventWireup="true" CodeFile="~/Accounting/Nafed_Deduction_Amount.aspx.cs" Inherits="Accounting_Nafed_Deduction_Amount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <%-- <script>
        function previewUserImage() {
            debugger
            let preview = document.querySelector('#<%=imgUpload.ClientID %>');
            let file = document.querySelector('#<%=Nurseryphoto.ClientID %>').files[0];
            let reader = new FileReader();

            reader.onloadend = function () {

                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>--%>
    <div class="content-wrapper">
        <asp:Label runat="server" Visible="false" ID="lblmsg"></asp:Label>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>Nafed Deduction For Storage Bills</legend>
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
                            CssClass="table table-bordered table-striped" AutoGenerateColumns="False" Autopostback="true">
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
                                        <asp:TextBox ID="txtDeduction_Amount" runat="server" Width="125" CssClass="form-control" Text='<%# Eval("Deduction_Amount") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Upload" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <%--    <asp:FileUpload ID="IdFileUpload" runat="server" />--%>
                                        <%--<asp:FileUpload ID="IdFileUpload" runat="server" CssClass="form-control" />--%>
                                        <asp:Image ID="imgUpload" ClientIDMode="Static" runat="server" Visible="false" Width="125" Height="149" src="../image/NurserRegImage.jpg" />
                                        <asp:FileUpload ID="IdFileUpload" onchange="previewUserImage()" Width="200" runat="server" CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update UTR" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtUpdate_UTR" runat="server" Width="125" CssClass="form-control" Text='<%# Eval("UTR_No") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Payment Date" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPayment_Date" runat="server" Width="125" MaxLength="10" CssClass="form-control" Text='<%# Eval("Payment_Date") %>' />
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
</asp:Content>

