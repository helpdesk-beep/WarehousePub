<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="JIT_Bill_Process_MPWLCHO_TO-MPSCSCHO.aspx.cs" Inherits="StatePages_JIT_Bill_Process_MPWLCHO_TO_MPSCSCHO" %>

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
            $("[id*=ddlregion]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddldistrict]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlbranch]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlgodown]").select2();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend>JIT Bill Process MPWLC HO TO MPSCSC HO</legend>
            <div class="row">
                <div class="col-md-2">
                    <label>Region Name</label>
                    <asp:DropDownList CssClass="form-control select2" ID="ddlregion" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>District Name</label>
                    <asp:DropDownList CssClass="form-control select2" ID="ddldistrict" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Branch Name</label>
                    <asp:DropDownList CssClass="form-control select2" ID="ddlbranch" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>Godown Name</label>
                    <asp:DropDownList CssClass="form-control select2" ID="ddlgodown" AutoPostBack="true" runat="server">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>No OF Bill</label>
                    <asp:TextBox runat="server" ID="txtnoofbill" Height="27px" Width="100%" AutoComplete="off"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label>Bill Amount</label>
                    <asp:TextBox runat="server" ID="txtBillAmount" Height="27px" Width="100%" AutoComplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <label>Upload PDF</label>
                    <asp:Image ID="imgUpload" ClientIDMode="Static" runat="server" Visible="false" Width="125" Height="149" src="../Moisture_Document" />
                    <asp:FileUpload ID="IdFileUpload" onchange="previewUserImage()" Width="200" Height="27" runat="server" CssClass="fa-border" />
                </div>
                <div class="col-md-2" style="text-align: center; margin-top: 17px">
                    <asp:Button runat="server" CssClass="btn btn-success btn-sm" ValidationGroup="a" Text="SUBMIT" ID="btnsubmit" OnClick="btnsubmit_Click" autopostback="true" />
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Details</legend>
            <div class="row" style="align-content: center; margin-top: 10px" runat="server" id="Div1" visible="false">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="grdJitbill" CssClass="table table-bordered table-hover datatable" runat="server" AutoGenerateColumns="false"
                            OnRowCommand="grdJitbill_RowCommand" autopostback="true" ShowFooter="true" DataKeyNames="id" OnRowDeleting="grdJitbill_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("id") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Region Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRegionnm" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnRegion_ID" Value='<%# Eval("Region_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnDistrict_ID" Value='<%# Eval("District_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnBranch_ID" Value='<%# Eval("Branch_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnGodown_ID" Value='<%# Eval("Godown_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No Of Bill" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNo_Of_Bill" Text='<%# Eval("No_Of_Bill") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Amount" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBill_Amount" Text='<%# Eval("Bill_Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jit Bill PDF Document" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyppdfdoc" runat="server" Target="_blank" CssClass="fa fa-eye" Visible='<%# Eval("Jit_Bill_PDF_Document").ToString() !=""?true:false %>' NavigateUrl='<%# "../JIT_Bill_PDF_Upload/" + Eval("Jit_Bill_PDF_Document")%>'></asp:HyperLink>
                                        <asp:HiddenField runat="server" ID="hdnDoc" Value='<%# Eval("Jit_Bill_PDF_Document")%>' />
                                        <asp:Label ID="lblfileuploadName" runat="server" Visible='<%# Eval("Jit_Bill_PDF_Document").ToString() !=""?true:false %>' Text='<%# Eval("Jit_Bill_PDF_Document").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve/Reject reason By RM MPSCSC" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRemark" Text='<%# Eval("Remark") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JIT Bill Payment Received From MPSCSC" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPaymentrMPSCSC" Text='<%# Eval("PaymentStatusFromMPSCSC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HO Remark" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtHO_Remark" CssClass="form-control" AutoComplete="off" Text='<%# Bind("HO_Remark") %>' TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Submit To MPSCSC HO" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" CausesValidation="false" CommandName="EditRow" CommandArgument='<%# Eval("Godown_ID")%>' Text="Send To MPSCSC" CssClass="btn btn-primary btn-sm" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" CausesValidation="false" CommandName="updateRow" CommandArgument='<%# Eval("Godown_ID")%>' Text="Update Record" CssClass="btn btn-success btn-sm" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="false" Text="Delete" CssClass="btn btn-danger btn-sm"
                                            CommandName="Delete" CommandArgument='<%# Eval("id") %>' OnClientClick="return confirm('Do you want to Delete this Record?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

