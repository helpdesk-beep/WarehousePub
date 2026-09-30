<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Region/DepositorWise_PaymentStatusInformation_NewProforma.aspx.cs" Inherits="Region_DepositorWise_PaymentStatusInformation_NewProforma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- Bootstrap core CSS -->
    <%--  <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />--%>
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <%-- <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/custome.css" rel="stylesheet" type="text/css" />--%>
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

        </style>

    <style>
        .left, .right {
            float: left;
            width: 20%; /* The width is 20%, by default */
        }

        .main {
            float: left;
            width: 60%; /* The width is 60%, by default */
        }

        /* Use a media query to add a breakpoint at 800px: */
        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
        }
    </style>


    <style>
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
    <style>
        .menu {
            width: 25%;
            float: left;
        }

        .main {
            width: 75%;
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>जमाकर्ता से भुगतान की स्थिति (नया प्रोफोर्मा-केवल MPSCSC, नाफेड, मार्कफेड, NCCF के लिए)</legend>
            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblFinancialYear" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">वित्‍तीय वर्ष:<i style="color:red">*</i></asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    
                    <asp:DropDownList CssClass="form-control select2" ID="ddlFinancialYear" runat="server" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="2010-11">2010-11</asp:ListItem>
                        <asp:ListItem Value="2011-12">2011-12</asp:ListItem>
                        <asp:ListItem Value="2012-13">2012-13</asp:ListItem>
                        <asp:ListItem Value="2013-14">2013-14</asp:ListItem>
                        <asp:ListItem Value="2014-15">2014-15</asp:ListItem>
                        <asp:ListItem Value="2015-16">2015-16</asp:ListItem>
                        <asp:ListItem Value="2016-17">2016-17</asp:ListItem>
                        <asp:ListItem Value="2017-18">2017-18</asp:ListItem>
                        <asp:ListItem Value="2018-19">2018-19</asp:ListItem>
                        <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                        <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                        <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                        <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                        <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                        <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblDepositor" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">जमाकर्ता का नाम:<i style="color:red">*</i></asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlDepositor" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblBillCount" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">वित्‍तीय वर्ष में प्रस्तुत देयकों की संख्या:<i style="color:red">*</i></asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtBillCountFY" runat="server" CssClass="form-control" onkeypress="return isNumber()" ></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 15px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblPresentedBillAmntInFY" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">वित्‍तीय वर्ष में प्रस्तुत देयकों की राशि (in Cr.):<i style="color:red">*</i></asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPresentedBillAmntInFY" runat="server" CssClass="form-control" onkeypress="return isNumber()" ></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblReceivedAmtFromDepositor" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">प्राप्त राशि का विवरण (in Cr.):<i style="color:red">*</i></asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtReceivedAmtFromDepositor" runat="server" CssClass="form-control" onkeypress="return isNumber()" ></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblDepositorRemainingAmt" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">शेष लंबित राशि (in Cr.):<i style="color:red">*</i></asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtDepositorRemainingAmt" runat="server" CssClass="form-control" onkeypress="return isNumber()" ></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 15px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblJVS" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">JVS संचालकों द्वारा प्रस्तुत देयकों की राशि (in Cr):<i style="color:red">*</i></asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPresentedBillJVS" runat="server" CssClass="form-control" onkeypress="return isNumber()" ></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblPaidtoGodownOwner" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">JVS गोदाम संचालकों को भुगतान राशि (in Cr.):<i style="color:red">*</i></asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPaidToGodownOwner" runat="server" CssClass="form-control" onkeypress="return isNumber()" ></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblRemainingJVS" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">JVS गोदाम संचालकों को भुगतान की शेष लंबित राशि (in Cr.):<i style="color:red">*</i></asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtRemainingAmtJVS" runat="server" CssClass="form-control" onkeypress="return isNumber()"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 15px">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblGain" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">1 % Gain के विरुद्ध रोकी गयी राशि( In Cr):<i style="color:red">*</i></asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtGainPercent" runat="server" CssClass="form-control" onkeypress="return isNumber()" ></asp:TextBox>
                    <br />
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblKatotra" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">कटोत्रा की गयी कुल राशि( In Cr):<i style="color:red">*</i></asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtDeduction" runat="server" CssClass="form-control" onkeypress="return isNumber()" ></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblReasonDeduction" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">कटोत्रा की राशि कारण / रिमार्क्स :</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtDeductionRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblRemarks" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">रिमार्क:</asp:Label>
                    </div>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 15px">
                <div style="width: 100%; background-repeat: no-repeat;">
                    <div style="position: relative;">
                        <p>
                            <strong style="color: red">* राशि Cr. में प्रविष्ट करे !!!
                            </strong>
                            <br />
                        </p>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="a" Text="Submit" CssClass="btn-success" Enabled="true" OnClick="btnSubmit_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn-danger" />
                </div>
            </div>
        </fieldset>
        <div class="row" id="grdentry" style="margin-top: 20px">
            <fieldset>
                <legend>लेखा प्रबंधक (RM कार्यालय) द्वारा दर्ज की गई जानकारी</legend>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GV_EntryDone" CellPadding="5" OnRowCommand="GV_EntryDone_RowCommand" OnRowDataBound="GV_EntryDone_RowDataBound"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" autopostback="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY" Enabled="false" runat="server" Text='<%# Eval("FinancialYear") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="जमाकर्ता का नाम">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepositorName" Enabled="false" runat="server" Text='<%# Eval("Depositor_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष में प्रस्तुत देयकों की संख्या">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBillCount" Enabled="false" runat="server" Text='<%# Eval("TotalBillPresentedinFY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वित्‍तीय वर्ष में प्रस्तुत देयकों की राशि (करोड़ रुपये में)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalBillAmtPresented" Enabled="false" runat="server" Text='<%# Eval("TotalBillAmountPresented") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="प्राप्त राशि का विवरण (करोड़ रुपये में)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalBillAmtReceived" Enabled="false" runat="server" Text='<%# Eval("TotalAmountReceivedInFY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="शेष लंबित राशि (करोड़ रुपये में)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_RemainingAmountFromDepositor" Enabled="false" runat="server" Text='<%# Eval("RemainingAmountFromDepositor") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JVS संचालकों द्वारा प्रस्तुत देयक की राशि(करोड़ रुपये में)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_JVSPresentedAmountFromDepositor" Enabled="false" runat="server" Text='<%# Eval("JVSOwnerTotalBillAmountPresented") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="गोदाम संचालकों को भुगतान की राशि (करोड़ रुपये में)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_GodownPaidAmount" Enabled="false" runat="server" Text='<%# Eval("GodownOwnerTotalAmountPaidInFY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="गोदाम संचालकों की शेष लंबित राशि (करोड़ रुपये में)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_JVSRemainingAmountFromDepositor" Enabled="false" runat="server" Text='<%# Eval("JVSOwnerRemainingAmountFromDepositor") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="1 % Gain के विरुद्ध रोकी गयी राशि(करोड़ रुपये में)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_AmountAgainstOnePercentGain" Enabled="false" runat="server" Text='<%# Eval("AmountAgainstOnePercentGain") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="कटोत्रा की गयी राशि(करोड़ रुपये में)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_DeductionAmount" Enabled="false" runat="server" Text='<%# Eval("TotalDeductionAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="कटोत्रा का कारण">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFY_KatotraReason" Enabled="false" runat="server" Text='<%# Eval("DeductionRemarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="रिमार्क">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" Enabled="false" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="RemoveRow" CssClass="btn-danger" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div align="center">No records found.</div>
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                    Height="20px" Font-Size="12pt" />
                                <AlternatingRowStyle BackColor="#eeeeee" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 32 && (charCode < 46 || charCode == 47 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

</asp:Content>
