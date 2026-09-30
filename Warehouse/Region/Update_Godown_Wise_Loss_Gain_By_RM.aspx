<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Region/Update_Godown_Wise_Loss_Gain_By_RM.aspx.cs" Inherits="Region_Update_Godown_Wise_Loss_Gain_By_RM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Inspections/Assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Administration/assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/datatable/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="assets/datatable/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/custome.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script src="../assets/New/js/bootstrap-datepicker.js"></script>
    <script src="../assets/New/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(".dateAdd").datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            changemonth: true,
            changeyear: true
        });
    </script>

    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("PrintDiv").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
    <script type="text/javascript">
        function PrintDiv_det() {
            var divContents = document.getElementById("PrintDiv_Det").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>
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
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
            /*transform: translate3d(0px, 0px, 0px) !important;*/
            padding: 2px !important;
            max-height: 10em !important;
            overflow-y: auto !important;
            /*#ec8712*/
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }

        .form-controlSearchBox {
            display: block;
            width: 100%;
            height: calc(2.25rem + 2px);
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            /*border-radius: 0.25rem;*/
            box-shadow: inset 0 0 0 transparent;
            /*transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;*/
        }

        .multiselect-native-select button {
            border-radius: 0.5em;
            border-color: #767b83 !important;
        }

        .form-check {
            margin-top: -3px;
            margin-bottom: -4px;
            padding-left: 0;
        }

            .form-check .form-check-label {
                margin-left: 0.5rem;
            }


        .row {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            margin-right: 0px;
            margin-left: -15px;
        }
    </style>
    <style type="text/css">
        ul.svertical {
            width: 220px; /* width of menu */
            overflow: auto;
            background: #f4f4f4; /* background of menu */
            margin: 0;
            padding: 0;
            padding-top: 7px; /* top padding */
            list-style-type: none;
        }

            ul.svertical li {
                text-align: right; /* right align menu links */
            }

                ul.svertical li a {
                    position: relative;
                    display: inline-block;
                    text-indent: 5px;
                    overflow: hidden;
                    background: rgb(1, 138, 180); /* initial background color of links */
                    font: bold 16px Germand;
                    text-decoration: none;
                    padding: 5px;
                    margin-bottom: 5px; /* spacing between links */
                    color: White;
                    -moz-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8); /* inner right shadow added to each link */
                    -webkit-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    -moz-transition: all 0.2s ease-in-out; /* CSS3 transition of hover properties */
                    -webkit-transition: all 0.2s ease-in-out;
                    -o-transition: all 0.2s ease-in-out;
                    -ms-transition: all 0.2s ease-in-out;
                    transition: all 0.2s ease-in-out;
                }

                    ul.svertical li a:hover {
                        padding-right: 30px; /* add right padding to expand link horizontally to the left */
                        color: Black;
                        background: rgb(153,249,75);
                        -moz-box-shadow: inset -3px 0 2px rgba(114,114,114, 0.8); /* contract inner right shadow */
                        -webkit-box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                        box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                    }

                    ul.svertical li a:before { /* CSS generated content: slanted right edge */
                        content: "";
                        position: absolute;
                        left: 0;
                        top: 0;
                        border-style: solid;
                        border-width: 70px 0 0 20px; /* Play around with 1st and 4th value to change slant degree */
                        border-color: transparent transparent transparent #f4f4f4; /* change black to match the background color of the menu UL */
                    }

        /*print*/


        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }

        .page {
            width: 21cm;
            min-height: 29.7cm;
            padding: 2cm;
            margin: 1cm auto;
            border: 1px #D3D3D3 solid;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

        .subpage {
            padding: 1cm;
            border: 5px red solid;
            height: 237mm;
            outline: 2cm #FFEAEA solid;
        }

        @page {
            size: A4;
            margin: 0;
            font-size: smaller;
        }

        @media print {
            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
                font-size: smaller;
            }
        }

        @media print {
            html, body {
                width: 210mm;
                height: 297mm;
                font-size: smaller;
            }
            /* ... the rest of the rules ... */
        }
        /*td{font-size:smaller;}*/
        page[size="A4"] {
            background: white;
            width: 21cm;
            height: 29.7cm;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
        }

        @media print {
            body, page[size="A4"] {
                margin: 0;
                box-shadow: 0;
                font-size: smaller;
            }
        }

        .style7 {
            height: 15px;
        }

        .style8 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <legend>क्षेत्रीय कार्यालय स्तर से 1% आधिक्य के विरुद्ध गोदाम संचालको के देयकों से 20% रोकी गई राशि की जानकारी अपडेट करना |</legend>
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    <span class="fa-pull-right">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Select Inspection Quarter" ID="RequiredFieldValidator6" Display="Dynamic" InitialValue="0" ControlToValidate="ddlDistrict" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Select Select !'></i>" ForeColor="Red"></asp:RequiredFieldValidator>
                    </span>
                    <asp:DropDownList ID="ddlDistrict" CssClass="form-control" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblbranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    <asp:DropDownList CssClass="form-control" ID="ddlBranch" runat="server" AutoPostBack="false">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblCropYear" runat="server" Text="Crop Year" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    <asp:DropDownList CssClass="form-control" ID="ddlcropyear" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged" >
                        <asp:ListItem Value="0">Select</asp:ListItem>
                      <%--  <asp:ListItem Value="2019-2020">2019-2020</asp:ListItem>
                        <asp:ListItem Value="2020-2021">2020-2021</asp:ListItem>
                        <asp:ListItem Value="2021-2022">2021-2022</asp:ListItem>
                        <asp:ListItem Value="2022-2023">2022-2023</asp:ListItem>
                        <asp:ListItem Value="2023-2024">2023-2024</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
            </div>
             <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblCommodity" runat="server" Text="Commodity" Font-Size="10pt" Font-Bold="true"></asp:Label>
                    <asp:DropDownList CssClass="form-control" ID="ddlCommodity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCommodity_SelectedIndexChanged" >
                        <asp:ListItem Value="0">Select</asp:ListItem>
                      <%--  <asp:ListItem Value="2019-2020">2019-2020</asp:ListItem>
                        <asp:ListItem Value="2020-2021">2020-2021</asp:ListItem>
                        <asp:ListItem Value="2021-2022">2021-2022</asp:ListItem>
                        <asp:ListItem Value="2022-2023">2022-2023</asp:ListItem>
                        <asp:ListItem Value="2023-2024">2023-2024</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        
    </fieldset>
    <div class="row" style="align-content: center">
        <div class="col-md-12">
            <fieldset>
                <legend>Details</legend>
                <div class="table-responsive">
                    <asp:GridView runat="server" DataKeyNames="Godown_ID" ID="gv_whr" HeaderStyle-Font-Size="Large"
                        CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" OnRowUpdating="gv_whr_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightBlue" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Godown_ID").ToString()%>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Godown Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>' />
                                    <asp:Label ID="lblGodown_ID" Visible="false" runat="server" Text='<%#Eval("Godown_ID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="क्षेत्रीय कार्यालय द्वारा गोदाम संचालको के देयकों से 20% रोकी गई राशि">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDeductionAmount" runat="server" Text='<%# Eval("DeductionAmount") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="क्षेत्रीय कार्यालय द्वारा गोदाम संचालको के देयकों से 20% रोकी गई राशि का भुगतान">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("Amount") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="मूल वजन में कमी एवं गेन में कमी के विरुद्ध काटी गई राशि">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount_DALG" runat="server" Text='<%# Eval("Amount_DALG") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="अन्य कारणों से काटी गई राशि">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOther_Deduction" runat="server" Text='<%# Eval("Other_Deduction") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="btn_Update" runat="server" Text="Update" OnClientClick="return confirm('Do you want to Update this Record?');" CommandName="Update" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
