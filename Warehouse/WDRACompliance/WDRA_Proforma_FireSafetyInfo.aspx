<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/WDRACompliance/WDRA_Proforma_FireSafetyInfo.aspx.cs" Inherits="WDRACompliance_WDRA_Proforma_FireSafetyInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../assets/css/style.css" rel="stylesheet" />

    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap-datepicker.css" rel="stylesheet" />
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

        .auto-style1 {
            height: 22px;
        }

        .auto-style3 {
            height: 22px;
            width: 444px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <asp:Label runat="server" ID="lblMsg"></asp:Label>
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>WDRA Proforma Fire Security Details Info</legend>
            <div class="row">
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWHCapacityInMT" Font-Bold="true" runat="server" ForeColor="Navy">Capacity of Warehouse (In MT):</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWHCapacity" runat="server" CssClass="form-control" onkeypress="return isNumber()" Placeholder="Please Enter Warehouse Capacity in MT"></asp:TextBox>
                </div>
                <div class="col-md-2" style="margin-top: 10px">
                    <div class="form-group">
                        <asp:Label ID="lblWHCapacity" Font-Bold="true" runat="server" ForeColor="Navy">Warehouse Capacity:</asp:Label><strong style="color: red">*</strong>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList CssClass="form-control select2" ID="ddlCapacity" runat="server">
                        <asp:ListItem Value="00">--Select--</asp:ListItem>
                        <asp:ListItem Value="01">Upto 1,500 MTs</asp:ListItem>
                        <asp:ListItem Value="02">Above 1,500 MTs & upto 3,000 MT</asp:ListItem>
                        <asp:ListItem Value="03">Above 3,000 MTs & upto 5,000 MTs</asp:ListItem>
                        <asp:ListItem Value="04">Above 5,000 MTs & upto 10,000 MTs</asp:ListItem>
                        <asp:ListItem Value="05">Above 10,000 MTs & upto 15,000 MTs</asp:ListItem>
                        <asp:ListItem Value="06">Above 15,000 MTs & upto 25,000 MTs</asp:ListItem>
                        <asp:ListItem Value="07">Above 25000 MTs</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
                    <legend>WDRA Fire Security Details </legend>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Label ID="lblFireAlarm" Font-Bold="true" runat="server" ForeColor="Navy">Details of Fire Alarm:</asp:Label><strong style="color: red">*</strong>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:RadioButtonList CssClass="form-control select2" ID="rdlFireAlarm" runat="server">
                            <asp:ListItem Text="Yes" Value="1" />
                            <asp:ListItem Text="No" Value="2" />
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Label ID="lblFireAlarmNo" Font-Bold="true" runat="server" ForeColor="Navy">Number of Fire Alarm:</asp:Label><strong style="color: red">*</strong>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtFireAlarm" runat="server" CssClass="form-control" ></asp:TextBox>
                        <br />
                        <br />
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Label ID="lblFireBucket" Font-Bold="true" runat="server" ForeColor="Navy">Number of Fire Buckets:</asp:Label><strong style="color: red">*</strong>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtFireBucket" runat="server" CssClass="form-control" Placeholder="Please Enter Number of Fire Buckets"></asp:TextBox>
                        <br />
                        <br />
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Label ID="lblFExNo" Font-Bold="true" runat="server" ForeColor="Navy">Number of Fire Extinguisher:</asp:Label><strong style="color: red">*</strong>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtFExNo" runat="server" CssClass="form-control" Placeholder="Please Enter Number of Fire Extinguisher"></asp:TextBox>
                    </div>

                </fieldset>
                <br />
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <div class="form-group">
                        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
                            <legend>WDRA Fire Extinguisher Details </legend>
                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td align="center" valign="top">

                                                <center>
                                                    <div>
                                                        <div class="form-group">
                                                            <table>
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; color: black; font-size: 15px;">SN.</th>
                                                                        <th style="text-align: center; color: black; font-size: 15px;">Fire Extinguisher Category</th>
                                                                        <th style="text-align: center; color: black; font-size: 15px;">Fire Extinguisher Details</th>
                                                                        <th style="text-align: center; color: black; font-size: 15px;">Fire Extinguisher Available</th>
                                                                        <th style="text-align: center; color: black; font-size: 15px;">Type of Fire Extinguisher</th>
                                                                        <th style="text-align: center; color: black; font-size: 15px;">Fire Extinguisher Number</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">1.</td>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">
                                                                            <asp:Label ID="lblFECatA" runat="server" Text="CLASS-A" />
                                                                        </td>
                                                                        <td style="text-align: left; color: black; font-size: 15px;" class="auto-style3">
                                                                            <asp:Label ID="lblTxtA" runat="server" Text="Fires involving solid combustible materials of organic nature such as wood, paper, rubber, plastics, etc, where the cooling effect of water is essential for extinction of fires" />
                                                                        </td>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">
                                                                            <asp:RadioButtonList CssClass="form-control select2" ID="rdlClassA" runat="server">
                                                                                <asp:ListItem Text="Yes" Value="1" />
                                                                                <asp:ListItem Text="No" Value="2" />
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td style="text-align: left; color: black; font-size: 15px;" class="auto-style1">Water, foam, ABC dry power and halocarbons.</td>
                                                                        <td style="text-align: center; color: white; font-size: 15px;">
                                                                            <asp:TextBox ID="txtClassA" runat="server" class="form-control" onkeypress="return NumberOnly(event);"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">2.</td>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">
                                                                            <asp:Label ID="lblFECatB" runat="server" Text="CLASS-B" />
                                                                        </td>
                                                                        <td style="text-align: left; color: black; font-size: 15px;" class="auto-style3">
                                                                            <asp:Label ID="lblTxtB" runat="server" Text="Fires involving flammable liquids or liquefiable solids or the like where a blanketing effect is essential Examples: Oil, Paraffin, Petrol" />
                                                                        </td>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">
                                                                            <asp:RadioButtonList CssClass="form-control select2" ID="rdlClassB" runat="server">
                                                                                <asp:ListItem Text="Yes" Value="1" />
                                                                                <asp:ListItem Text="No" Value="2" />
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td style="text-align: left; color: black; font-size: 15px;" class="auto-style1">Foam, dry powder, clean agent and carbon dioxide extinguishers.</td>
                                                                        <td style="text-align: center; color: white; font-size: 15px;">
                                                                            <asp:TextBox ID="txtClassB" runat="server" class="form-control" onkeypress="return NumberOnly(event);"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">3.</td>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">
                                                                            <asp:Label ID="lblFECatC" runat="server" Text="CLASS-C" />
                                                                        </td>
                                                                        <td style="text-align: left; color: black; font-size: 15px;" class="auto-style3">Fires involving flammable gases under pressure including liquefied gases, where it is necessary to inhibit the burning gas at fast rate with an inert gas, powder or vaporising liquid for extinguishment. Examples: Methane, Butane, Propane.</td>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">
                                                                            <asp:RadioButtonList CssClass="form-control select2" ID="rdlClassC" runat="server">
                                                                                <asp:ListItem Text="Yes" Value="1" />
                                                                                <asp:ListItem Text="No" Value="2" />
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td style="text-align: left; color: black; font-size: 15px;" class="auto-style1">Dry powder, clean agent and carbon dioxide extinguishers</td>
                                                                        <td style="text-align: center; color: white; font-size: 15px;">
                                                                            <asp:TextBox ID="txtClassC" runat="server" class="form-control" onkeypress="return NumberOnly(event);"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">4.</td>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">
                                                                            <asp:Label ID="lblFECatD" runat="server" Text="CLASS-D" />
                                                                        </td>
                                                                        <td style="text-align: left; color: black; font-size: 15px;" class="auto-style3">Fires involving combustible metals, such as magnesium, aluminum, zinc, sodium, potassium, etc, when the burring metals are reactive to water and water containing agents and in certain cases carbon dioxide, halogenated hydrocarbons and ordinary dry powders. These fires require special media and techniques to extinguish. Examples: Magnesium,</td>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">
                                                                            <asp:RadioButtonList CssClass="form-control select2" ID="rdlClassD" runat="server">
                                                                                <asp:ListItem Text="Yes" Value="1" />
                                                                                <asp:ListItem Text="No" Value="2" />
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td style="text-align: left; color: black; font-size: 15px;" class="auto-style1">Extinguishers with special dry powder for metal fires.</td>
                                                                        <td style="text-align: center; color: white; font-size: 15px;">
                                                                            <asp:TextBox ID="txtClassD" runat="server" class="form-control" onkeypress="return NumberOnly(event);"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">5.</td>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">
                                                                            <asp:Label ID="lblFECatE" runat="server" Text="CLASS-E" />
                                                                        </td>
                                                                        <td style="text-align: left; color: black; font-size: 15px;" class="auto-style3">Electrical fires. It is important to decide selection and use of extinguisher on live electrical installations. The extinguisher that have passed electrical conductivity test should only be used.</td>
                                                                        <td style="text-align: center; color: black; font-size: 15px;" class="auto-style1">
                                                                            <asp:RadioButtonList CssClass="form-control select2" ID="rdlClassE" runat="server">
                                                                                <asp:ListItem Text="Yes" Value="1" />
                                                                                <asp:ListItem Text="No" Value="2" />
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td style="text-align: left; color: black; font-size: 15px;" class="auto-style1">Carbon Dioxide</td>
                                                                        <td style="text-align: center; color: white; font-size: 15px;">
                                                                            <asp:TextBox ID="txtClassE" runat="server" class="form-control" onkeypress="return NumberOnly(event);"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <!-- Add more rows as needed -->
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </center>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>
                                    </table>
                                </div>
                            </center>
                        </fieldset>
                        <br />
                        <br />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button ID="btnClkBack" runat="server" Text="Back" CssClass="btn-danger" OnClick="btnClkBack_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-success" Enabled="true" OnClick="btnSubmit_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnClkNext" runat="server" Text="Next" CssClass="btn-danger" OnClick="btnClkNext_Click" />
                </div>
            </div>
        </fieldset>
        <div class="row" id="grdentry" style="margin-top: 20px">
            <fieldset>
                <legend>Warehouse Fire Safety Information as provided</legend>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GV_EntryDone" CellPadding="5" OnRowCommand="GV_EntryDone_RowCommand"
                                CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False" autopostback="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SN" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Capacity of Warehouse">
                                        <ItemTemplate>
                                            <asp:Label ID="txtWHCapacityInMT" Enabled="false" runat="server" Text='<%# Eval("WH_CapacityInMT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Number of Fire Alarms">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFireAlarmCount" Enabled="false" runat="server" Text='<%# Eval("WH_FireAlarmCount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Number of Fire Bucket">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWHFireBucketCount" Enabled="false" runat="server" Text='<%# Eval("WH_FireBucketCount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Number of Fire Extinguishers">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoOfFireExtinguisher" Enabled="false" runat="server" Text='<%# Eval("WH_FireExtinguisherAvlb") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fire Extinguisher Class A">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFireExtinguisherClassA" Enabled="false" runat="server" Text='<%# Eval("WH_FE_ClassA_Cnt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fire Extinguisher Class B">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFireExtinguisherClassB" Enabled="false" runat="server" Text='<%# Eval("WH_FE_ClassB_Cnt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fire Extinguisher Class C">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFireExtinguisherClassC" Enabled="false" runat="server" Text='<%# Eval("WH_FE_ClassC_Cnt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fire Extinguisher Class D">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFireExtinguisherClassD" Enabled="false" runat="server" Text='<%# Eval("WH_FE_ClassA_Cnt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fire Extinguisher Class E">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFireExtinguisherClassE" Enabled="false" runat="server" Text='<%# Eval("WH_FE_ClassE_Cnt") %>'></asp:Label>
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

    <script type="text/javascript" src="../../assets/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript">
        $(".dateAdd").datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            changemonth: true,
            changeyear: true
        });
    </script>
</asp:Content>
