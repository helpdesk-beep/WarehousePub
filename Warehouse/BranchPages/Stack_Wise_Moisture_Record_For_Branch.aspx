<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/BranchPages/Stack_Wise_Moisture_Record_For_Branch.aspx.cs" Inherits="BranchPages_Stack_Wise_Moisture_Record_For_Branch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <link href="../Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="../Assets/js/bootstrap-datepicker.js"></script>
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
    <style type="text/css">
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

        .auto-style3 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 50%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
    <style type="text/css">
        /* Custom date picker colors */
        .datepicker {
            background-color: #f0f8ff; /* Light blue background */
        }

            .datepicker table tbody tr td.active,
            .datepicker table tbody tr td.active:hover {
                background-color: #ff6347; /* Tomato color for selected date */
                color: white; /* White text color for selected date */
            }

            .datepicker table tbody tr td:hover {
                background-color: #87ceeb; /* Sky blue color on hover */
            }

            .datepicker .datepicker-days .datepicker-switch {
                color: #008080; /* Teal color for the month/year switch */
            }

            .datepicker .datepicker-days .prev,
            .datepicker .datepicker-days .next {
                color: #008080; /* Teal arrows */
            }

            .datepicker table {
                border: 2px solid #008080; /* Teal border around the calendar */
            }

                .datepicker table tbody tr td {
                    color: #333; /* Dark text color for the dates */
                }
    </style>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.1.0-beta.1/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.1.0-beta.1/js/select2.min.js"></script>
    <!-- Bootstrap DatePicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <!-- Bootstrap DatePicker JS -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true,
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlgodowntype]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlGodown]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlmonth]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlfinancialyear]").select2();
        });
    </script>
    <%-- <script>
        function previewUserImage() {
            debugger
            let preview = document.querySelector('#<%=imgUpload.ClientID %>');
            let file = document.querySelector('#<%=IdFileUpload.ClientID %>').files[0];
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label runat="server" Visible="false" ID="lblmsg"></asp:Label>
    <div class="content-wrapper">
        <fieldset>
            <legend>Stack Wise Moisture Record</legend>
            <div class="row">
                <div class="col-md-2" style="margin-top: 8px">
                    <label>Branch Name</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtbranch" CssClass="form-control" ReadOnly="true" AutoComplete="off"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 8px">
                    <label>Godown Type</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlgodowntype" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlgodowntype_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 8px">
                    <label>Godown Name</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlGodown" AutoPostBack="true" runat="server">
                        <asp:ListItem Value="0 ">Select</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2" style="margin-top: 8px">
                    <label>Financial Year</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlfinancialyear" AutoPostBack="false" runat="server">
                        <asp:ListItem Value="0">Select Financial Year</asp:ListItem>
                        <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                        <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                        <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                        <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                        <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                        <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                        <asp:ListItem Value="2025-26">2025-26</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2" style="margin-top: 8px">
                    <label>Month</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlmonth" AutoPostBack="false" runat="server">
                        <asp:ListItem Value="0">Select Month</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-2" style="text-align: center; margin-top: 35px">
                    <asp:Button runat="server" CssClass="btn btn-success btn-sm" ValidationGroup="a" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" autopostback="true" />
                </div>
            </div>
            <div class="row" style="align-content: center; margin-top: 10px" runat="server" id="Div1" visible="false">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grdMoisture" CssClass="table table-bordered table-hover datatable" runat="server" AutoGenerateColumns="false"
                            OnRowCommand="grdMoisture_RowCommand" autopostback="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stack ID" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStack_ID" Text='<%# Eval("Stack_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stack Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStack_Name" Text='<%# Eval("Stack_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Commodity Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommodity_Name" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnCommodity_Id" Value='<%# Eval("Commodity_Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="स्टैक पूर्ण होने पर नमी का प्रतिशत" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtStack_Moisture_Per" autocomplete="off" runat="server" Width="125" CssClass="form-control" Text='<%# Eval("Stack_Moisture_Per") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Moisture लेने की दिनाँक" ItemStyle-Width="200px" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDate" placeholder="dd/mm/yyyy" autocomplete="off" onpaste="return false ;" onkeypress="return false;" CssClass="form-control datepicker" runat="server" Text='<%# Bind("DM_Record_Date") %>'></asp:TextBox>
                                        <%--<asp:TextBox ID="txtDate" placeholder="dd/mm/yyyy" runat="server" Text='<%# Bind("DM_Record_Date") %>'></asp:TextBox>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upload Moisture Document </br>(Upload Only PDF)" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Image ID="imgUpload" ClientIDMode="Static" runat="server" Visible="false" Width="125" Height="149" src="../Moisture_Document" />
                                        <asp:FileUpload ID="IdFileUpload" onchange="previewUserImage()" Width="200" runat="server" CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" CausesValidation="false" CommandName="EditRow" CommandArgument='<%# Eval("Stack_ID")%>' Text="Update" CssClass="BTNBLUE" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="false" Text="Delete" CssClass="btn btn-danger btn-sm"
                                            CommandName="Delete" CommandArgument='<%# Eval("Stack_ID") %>' OnClientClick="return confirm('Do you want to Delete this Record?');" />
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

