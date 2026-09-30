<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Storage_Position_Branch_Wise.aspx.cs" Inherits="BranchPages_Storage_Position_Branch_Wise" %>

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

        .auto-style1 {
            height: 10px;
            width: 558px;
        }

        .auto-style2 {
            width: 558px;
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
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>गोदामो में भण्‍डारित स्‍कंध की अवधि अनुसार जानकारी</legend>
            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblDepositor" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">जमाकर्ता का नाम:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlDepositor" runat="server" OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblCommodity" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">जिंस (Commodity):</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlCommodity" runat="server" OnSelectedIndexChanged="ddlcomodity_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblGodownType" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">गोदाम का प्रकार:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlGodownType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblGodownName" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">गोदाम का नाम:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlGodownName" runat="server" OnSelectedIndexChanged="ddlGodownName_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="Label3" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">क्षेत्रीय कार्यालय:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <asp:Label ID="lblRegionName" runat="server" Height="25px" Width="208px" Font-Size="10pt" />
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="Label11" Font-Bold="true" runat="server" Style="margin-top: 10px" ForeColor="Navy">जिला:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <asp:Label runat="server" ID="lblDistrictName" Height="25px" Width="208px" ReadOnly="True" Font-Size="10pt"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Style="margin-top: 10px" ForeColor="Navy">शाखा का नाम	:</asp:Label>
                    </div>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <asp:Label ID="lblBranchName" runat="server" Height="25px" Width="208px" Font-Size="10pt" />
                </div>
                <div class="col-md-4"></div>
                <div class="col-md-4" style="margin-top: 25px">
                    <h1>मात्रा मे.टन में प्रविष्ट करे</h1>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="GV_CommodityInfo" 
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" OnRowDataBound="GV_CommodityInfo_OnRowDataBound" autopostback="true">
                            <Columns>
                                <asp:BoundField DataField="RowNumber" HeaderText="क्रमांक" ItemStyle-Width="10px" />
                                <asp:TemplateField HeaderText="क्रॉप ईयर">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCropYear" runat="server">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="2017-18">2017-18</asp:ListItem>
                                            <asp:ListItem Value="2018-19">2018-19</asp:ListItem>
                                            <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                                            <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                                            <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                                            <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                                            <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                                            <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="6 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSixMonth" runat="server" Width="70px" onkeypress="return isNumber()" Text='<%# Eval("StockPositionIn6Months") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="9 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNineMonth" runat="server" Width="70px" onkeypress="return isNumber()" Text='<%# Eval("StockPositionIn9Months") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="12 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTwelveMonth" runat="server" Width="70px" onkeypress="return isNumber()" Text='<%# Eval("StockPositionIn12Months") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="18 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEighteenMonth" runat="server" Width="70px" onkeypress="return isNumber()" Text='<%# Eval("StockPositionIn18Months") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="24 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTwentyFourMonth" runat="server" Width="70px" onkeypress="return isNumber()" Text='<%# Eval("StockPositionIn24Months") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="30 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtThirtyMonth" runat="server" Width="70px" onkeypress="return isNumber()" Text='<%# Eval("StockPositionIn30Months") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="36 माह से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtThirtySixMonth" runat="server" Width="70px" onkeypress="return isNumber()" Text='<%# Eval("StockPositionIn30Months") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="5 वर्ष से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFiveYear" runat="server" Width="70px" onkeypress="return isNumber()" Text='<%# Eval("StockPositionFiveYear") %>'>0</asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="5 वर्ष से अधिक भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGreaterthanFiveYear" runat="server" Width="70px" onkeypress="return isNumber()" Text='<%# Eval("StockPositionGreaterthanFiveYear") %>'>0</asp:TextBox>
                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Button ID="btnAddRow" runat="server" Visible="false" Text="Add New Row" CssClass="btn-success"
                                            OnClick="ButtonAdd_Click" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
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
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-success" Enabled="true" OnClick="btnSubmit_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn-danger" />
                </div>
            </div>
        </fieldset>
        <div class="row" id="grdentry" style="margin-top: 20px">
            <fieldset>
                <legend>शाखा प्रबंधक द्वारा दर्ज  की गई जानकारी </legend>
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
                                 <asp:TemplateField HeaderText="गोदाम का नाम">
                                     <ItemTemplate>
                                         <asp:Label ID="txtEVGodownName" Enabled="false" runat="server" Text='<%# Eval("GodownName") %>'>0</asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="जिंस (Commodity)">
                                     <ItemTemplate>
                                         <asp:Label ID="txtEVCommodityName" Enabled="false" runat="server" Text='<%# Eval("CommodityName") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="जमाकर्ता का नाम">
                                     <ItemTemplate>
                                         <asp:Label ID="txtEVDepositorName" Enabled="false" runat="server" Text='<%# Eval("DepositorName") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="क्रॉप ईयर">
                                     <ItemTemplate>
                                         <asp:Label ID="txtEVCropYear" Enabled="false" runat="server" Text='<%# Eval("CropYear") %>'>>
                                         </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="6 माह से भण्‍डारित मात्रा">
                                     <ItemTemplate>
                                         <asp:Label ID="txtEVSixMonth" Enabled="false" runat="server" Text='<%# Eval("StockPositionIn6Months") %>'>0</asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="9 माह से भण्‍डारित मात्रा">
                                     <ItemTemplate>
                                         <asp:Label ID="txtEVNineMonth" Enabled="false" runat="server" Text='<%# Eval("StockPositionIn9Months") %>'>0</asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="12 माह से भण्‍डारित मात्रा">
                                     <ItemTemplate>
                                         <asp:Label ID="txtEVTwelveMonth" Enabled="false" runat="server" Text='<%# Eval("StockPositionIn12Months") %>'>0</asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="18 माह से भण्‍डारित मात्रा">
                                     <ItemTemplate>
                                         <asp:Label ID="txtEVEighteenMonth" Enabled="false" runat="server" Text='<%# Eval("StockPositionIn18Months") %>'>0</asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="24 माह से भण्‍डारित मात्रा">
                                     <ItemTemplate>
                                         <asp:Label ID="txtEVTwentyFourMonth" Enabled="false" runat="server" Text='<%# Eval("StockPositionIn24Months") %>'>0</asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="30 माह से भण्‍डारित मात्रा">
                                     <ItemTemplate>
                                         <asp:Label ID="txtEVThirtyMonth" Enabled="false" runat="server" Text='<%# Eval("StockPositionIn30Months") %>'>0</asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="36 माह से भण्‍डारित मात्रा">
                                     <ItemTemplate>
                                         <asp:Label ID="txtEVThirtySixMonth" Enabled="false" runat="server" Text='<%# Eval("StockPositionIn36Months") %>'>0</asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                  <asp:TemplateField HeaderText="5 वर्ष से भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:Label ID="txtFiveYear" runat="server" Width="70px" onkeypress="return isNumber()" Text='<%# Eval("StockPositionFiveYear") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="5 वर्ष से अधिक भण्‍डारित मात्रा">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGreaterthanFiveYear" runat="server" Width="70px" onkeypress="return isNumber()" Text='<%# Eval("StockPositionGreaterthanFiveYear") %>'>0</asp:Label>
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
