<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Storage_Bills_Pendding_And_Received_Imformation.aspx.cs" Inherits="BranchPages_Storage_Bills_Pendding_And_Received_Imformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />

    <link href="../assets/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/New/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="../StatePages/Assets/js/bootstrap-datepicker.js"></script>
    <link href="../assets/css/custome.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
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
            border: 1px solid black;
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
            border: 1px solid black;
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
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=txtDate]").datepicker({
                //showOn: 'button',
                //buttonImageOnly: true,
                //buttonImage: 'images/calendar.png'
                format: 'dd/mm/yyyy',
                autoclose: true,
                changemonth: true,
                changeyear: true
            });
        });
    </script>
    <%--<link href="../Administration/assets/Resources/Validation/jquery-ui.css" rel="stylesheet" />
    <script src="../CapHiringScheme/assets/js/jquery-1.11.3.min.js"></script>
    <script src="../WarehouseLicense/css/jquery-ui.js"></script>--%>
    <%--<script type="text/javascript">
        $("#txtDate").datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            changemonth: true,
            changeyear: true
        });
    </script>--%>
    <%--  <script>
        $(function () {
            $('#txtDate').datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                yearRange: '1950:2100'
            });
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper" style="background-color: darkseagreen">
        <fieldset>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <asp:Label ID="Label18" runat="server" Font-Size="20pt" ForeColor="Blue"
                        Text="भंडारण शुल्‍क देयकों के प्रस्‍तुतिकरण,प्राप्‍त राशि एवं लंबित राशि का MPWLC एवं MPSCSC का मिलान उपरांत 
                       संयुक्‍त जानकारी पत्रक -"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <label id="Label19" style="font-size: 20px" runat="server">शाखा का नाम</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtbranch" ReadOnly="true" Font-Bold="true" Font-Size="Medium" runat="server" AutoComplete="off" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
                <div class="col-md-3"></div>
                <div class="col-md-2">
                    <label id="Label1" style="font-size: 20px" runat="server">वित्तीय वर्ष</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="false" class="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">2008-09</asp:ListItem>
                        <asp:ListItem Value="2">2009-10</asp:ListItem>
                        <asp:ListItem Value="3">2010-11</asp:ListItem>
                        <asp:ListItem Value="4">2011-12</asp:ListItem>
                        <asp:ListItem Value="5">2012-13</asp:ListItem>
                        <asp:ListItem Value="6">2013-14</asp:ListItem>
                        <asp:ListItem Value="7">2014-15</asp:ListItem>
                        <asp:ListItem Value="8">2015-16</asp:ListItem>
                        <asp:ListItem Value="9">2016-17</asp:ListItem>
                        <asp:ListItem Value="10">2017-18</asp:ListItem>
                        <asp:ListItem Value="11">2018-19</asp:ListItem>
                        <asp:ListItem Value="12">2019-20</asp:ListItem>
                        <asp:ListItem Value="13">2020-21</asp:ListItem>
                        <asp:ListItem Value="14">2021-22</asp:ListItem>
                        <asp:ListItem Value="15">2022-23</asp:ListItem>
                        <asp:ListItem Value="16">2023-24</asp:ListItem>
                        <asp:ListItem Value="17">2024-25</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <fieldset>
                    <legend>MPWLC द्वारा प्रस्तुत देयको का विवरण</legend>
                    <div class="row">
                        <div class="col-md-2">
                            <label id="Label21" style="font-size: 20px" runat="server">स्कंध का नाम :</label>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlComodity" runat="server" AutoPostBack="false" class="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <label id="Label9" style="font-size: 20px" runat="server">देयक क्रमांक :</label>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="txtbillnumber" CssClass="form-control" AutoComplete="off" placeholder="देयक क्रमांक दर्ज करे"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <label id="Label22" style="font-size: 20px" runat="server">दिनांक :</label>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:TextBox ID="txtDate" runat="server" placeholder="MM/dd/YYYY"
                                    CssClass="form-control dateAdd" autocomplete="off" data-provide="datepicker"
                                    data-date-format="dd/mm/yyyy"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <label id="Label23" style="font-size: 20px" runat="server">देयक राशि :</label>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="txtMpwlcAmount" CssClass="form-control" AutoComplete="off" placeholder="देयक राशि दर्ज करे"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="row" style="margin-top: 10px">
                <fieldset>
                    <legend>MPSCSC द्वारा किये गये भुगतान का विवरण</legend>
                    <div class="row">
                        <div class="col-md-4">
                            <fieldset>
                                <legend>कटोत्रा का विवरण</legend>
                                <div class="col-md-6">
                                    <label id="Label2" style="font-size: 20px" runat="server">राशि :</label>
                                    <asp:TextBox runat="server" ID="txtkatoraamount" CssClass="form-control" AutoComplete="off" placeholder="राशि दर्ज करे"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label id="Label3" style="font-size: 20px" runat="server">विवरण :</label>
                                    <asp:TextBox runat="server" ID="txtinformation" CssClass="form-control" AutoComplete="off" placeholder="विवरण दर्ज करे"></asp:TextBox>
                                </div>
                            </fieldset>
                        </div>
                        <div class="col-md-2" style="margin-top: 62px">
                            <label id="Label4" style="font-size: 20px" runat="server">शुद्ध भुगतान :</label>
                            <asp:TextBox runat="server" ID="txtbhugtan" CssClass="form-control" AutoComplete="off" placeholder="शुद्ध भुगतान दर्ज करे"></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="margin-top: 62px">
                            <label id="Label5" style="font-size: 20px" runat="server">चेक क्र./एडवाइस क्र. :</label>
                            <asp:TextBox runat="server" ID="txtadwise" CssClass="form-control" AutoComplete="off" placeholder="चेक क्र./एडवाइस क्र दर्ज करे"></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="margin-top: 62px">
                            <label id="Label6" style="font-size: 20px" runat="server">भुगतान हेतु लंबित राशि :</label>
                            <asp:TextBox runat="server" ID="txtUnpaidamount" CssClass="form-control" AutoComplete="off" placeholder="भुगतान हेतु लंबित राशि दर्ज करे"></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="margin-top: 62px">
                            <label id="Label7" style="font-size: 20px" runat="server">लंबित का कारण :</label>
                            <asp:TextBox runat="server" ID="txtdelyreason" CssClass="form-control" AutoComplete="off" placeholder="लंबित का कारण दर्ज करे"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 20px">
                        <div class="col-md-2"></div>
                        <div class="col-md-1">
                            <label id="Label8" style="font-size: 20px" runat="server">रिमार्क :</label>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="txtremark" TextMode="MultiLine" CssClass="form-control" AutoComplete="off" placeholder="देयक राशि दर्ज करे"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4"></div>
                        <div class="col-md-1" style="margin-top: 25px">
                            <asp:Button runat="server" ID="btnsave" CssClass="btn btn-success btn-block" ValidationGroup="a" Text="SUBMIT" OnClick="btnsave_Click" />
                        </div>
                        <div class="col-md-1" style="margin-top: 25px">
                            <a href="Storage_Bills_Pendding_And_Received_Imformation.aspx" class="btn btn-warning btn-block">Clear</a>
                        </div>
                    </div>
                </fieldset>
                <div class="row" style="align-content: center">
                    <div class="col-md-12">
                        <fieldset>
                            <legend>Details</legend>
                            <div class="table-responsive">
                                <asp:GridView runat="server" ID="GridView1" HeaderStyle-Font-Size="Large" OnDataBound="OnDataBound"
                                    CssClass="table" BorderColor="Black" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="क्र." HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ID").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="शाखा का नाम" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblDepotName" Text='<%# Eval("DepotName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="वित्तीय वर्ष" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblFinacial_Year" Text='<%# Eval("Finacial_Year") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="स्कंध का नाम" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblCommodity_Name" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblCommodity_ID" Visible="false" Text='<%# Eval("Commodity_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="देयक क्रमांक" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblBIll_Number" Text='<%# Eval("BIll_Number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="देयक दिनांक" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblPaid_Date" Text='<%# Eval("Paid_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="देयक राशि" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblPaid_Amount" Text='<%# Eval("Paid_Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="कटोत्रा राशि" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblKatotra_amount" Text='<%# Eval("Katotra_amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="कटोत्रा विवरण" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblKatotra_Information" Text='<%# Eval("Katotra_Information") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="शुद्ध भुगतान" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblShudh_Paid" Text='<%# Eval("Shudh_Paid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="चेक क्र./एडवाइस क्र." HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblCheck_Number" Text='<%# Eval("Check_Number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="भुगतान हेतु लंबित राशि" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblMPSCSC_Pending_Amount" Text='<%# Eval("MPSCSC_Pending_Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="लंबित का कारण" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblPendency_Reason" Text='<%# Eval("Pendency_Reason") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="रिमार्क" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Font-Size="Medium" ID="lblRemark" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Action" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" Font-Size="Medium" runat="server" CommandName="DeleteRecord" CommandArgument='<%# Eval("ID").ToString()%>' Text="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

