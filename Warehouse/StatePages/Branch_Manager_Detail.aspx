<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/StatePages/Branch_Manager_Detail.aspx.cs" Inherits="StatePages_Branch_Manager_Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Branch Manager Contact Details</title>
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
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
            background: white !important;
            color: black !important;
        }

        .GridViewHeader th {
            color: white !important; /* header text white */
            background-color: #4CAF50 !important; /* optional background */
            text-align: center;
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
    <script type="text/javascript">
        $(function () {
            $("[id*=DropDownList1]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlBranch]").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="content-wrapper">
            <fieldset style="width: 100%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
                <legend>Branch Manager Details</legend>
                <div class="Row">
                    <div class="col-md-3"></div>
                    <div class="col-md-1" style="margin-top: 4px; text-align: end;">
                        <label id="lbldistrict" runat="server" style="font-size: medium">District :</label>
                    </div>
                    <div class="col-md-2" style="align-items: flex-start">
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                            Height="25px" Width="168px" Font-Size="Medium" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1" style="margin-top: 4px; text-align: end;">
                        <label id="lblbranch" runat="server" style="font-size: medium">Branch :</label>
                    </div>
                    <div class="col-md-2" style="align-items: flex-start">
                        <asp:DropDownList ID="ddlBranch" runat="server"
                            Height="25px" Width="168px" Font-Size="Medium" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </fieldset>
            <fieldset>
                <legend>Details</legend>
                <div class="table-responsive">
                    <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-hover datatable">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDistrictName" Text='<%# Eval("DistrictName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDepotName" Text='<%# Eval("DepotName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Manager Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblNodalOfficeName" Text='<%# Eval("NodalOfficeName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Manager Mobile No">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblNodalOfficerMobile" Text='<%# Eval("NodalOfficerMobile")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operator Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblOperatorName" Text='<%# Eval("OperatorName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operator Mobile No">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblOperatorMobileNo" Text='<%# Eval("OperatorMobileNo")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Font-Size="Medium" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Manager Email">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblNodalOfficerEmail" Text='<%# Eval("NodalOfficerEmail")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Font-Size="Medium" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
