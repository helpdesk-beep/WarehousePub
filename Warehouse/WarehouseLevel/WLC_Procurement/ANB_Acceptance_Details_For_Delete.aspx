<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master"
    AutoEventWireup="true"
    CodeFile="ANB_Acceptance_Details_For_Delete.aspx.cs"
    Inherits="WarehouseLevel_WLC_Procurement_ANB_Acceptance_Details_For_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <!-- JQuery -->
    <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>

    <!-- Select2 -->
    <link href="../../assets/New/css/select2.min.css" rel="stylesheet" />
    <script src="../../assets/New/js/select2.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=ddlGodown.ClientID%>").select2();

            ToggleDeleteButton();

        });

        function ToggleDeleteButton() {

            var isChecked = false;

            $("#<%=gvDetails.ClientID%> input[type=checkbox]").each(function () {

                if ($(this).is(":checked")) {
                    isChecked = true;
                }

            });

            if (isChecked) {

                $("#<%=btnDelete.ClientID%>").show();

            }
            else {

                $("#<%=btnDelete.ClientID%>").hide();

            }
        }

    </script>

    <style type="text/css">

        body {
            font-family: Arial;
        }

        .main-container {
            width: 98%;
            margin: 15px auto;
            background: #fff;
            border: 1px solid #dcdcdc;
            border-radius: 8px;
            box-shadow: 0px 2px 8px rgba(0,0,0,0.10);
        }

        .page-header {
            background: #0d6efd;
            color: #fff;
            padding: 14px 20px;
            font-size: 22px;
            font-weight: bold;
            border-radius: 8px 8px 0 0;
        }

        .filter-section {
            padding: 15px 20px;
            background: #f8f9fa;
            border-bottom: 1px solid #dcdcdc;
        }

        .filter-table {
            width: 100%;
        }

        .filter-table td {
            padding: 6px;
            vertical-align: middle;
        }

        .label {
            font-weight: bold;
            color: #333;
            font-size: 14px;
        }

        .dropdown {
            width: 280px;
            height: 36px;
            border: 1px solid #bfbfbf;
            border-radius: 4px;
            padding-left: 8px;
            font-size: 14px;
        }

        .btnShow {
            background: #198754;
            color: white;
            border: none;
            padding: 8px 20px;
            font-size: 14px;
            font-weight: bold;
            border-radius: 4px;
            cursor: pointer;
        }

        .btnShow:hover {
            background: #146c43;
        }

        .grid-section {
            padding: 15px 20px 20px 20px;
            overflow: auto;
        }

        .grid {
            width: 100%;
            border-collapse: collapse;
        }

        .grid th {
            background: #0d6efd;
            color: #fff;
            padding: 10px;
            border: 1px solid #dcdcdc;
            text-align: center;
            font-size: 14px;
        }

        .grid td {
            padding: 9px;
            border: 1px solid #dcdcdc;
            text-align: center;
            font-size: 13px;
        }

        .grid tr:nth-child(even) {
            background: #f7f7f7;
        }

        .grid tr:hover {
            background: #eef5ff;
        }

        .emptymsg {
            padding: 15px;
            text-align: center;
            color: red;
            font-weight: bold;
            font-size: 15px;
        }

        @media screen and (max-width:768px) {

            .filter-table td {
                display: block;
                width: 100%;
            }

            .dropdown {
                width: 100%;
            }

            .btnShow {
                width: 100%;
                margin-top: 10px;
            }
        }

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-container">

        <div class="page-header">
            ANB Acceptance Details For Delete
        </div>

        <div class="filter-section">

            <table class="filter-table">

                <tr>

                    <td style="width:150px;">
                        <span class="label">Select Godown</span>
                    </td>

                    <td style="width:320px;">

                        <asp:DropDownList ID="ddlGodown"
                            runat="server"
                            CssClass="dropdown"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                        </asp:DropDownList>

                    </td>

                    <td>

                        <asp:Button ID="btnShow"
                            runat="server"
                            Text="Show Details"
                            CssClass="btnShow"
                            OnClick="btnShow_Click" />

                    </td>

                </tr>

            </table>

        </div>

        <div class="grid-section">

            <asp:GridView ID="gvDetails"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="grid"
                Width="100%"
                EmptyDataText="No Record Found">

                <Columns>


                    <asp:TemplateField HeaderText="Select">

                        <HeaderStyle Width="70px" />

                        <ItemTemplate>

                            <asp:CheckBox ID="chkDelete"
                                runat="server"
                                onclick="ToggleDeleteButton();" />

                            <asp:HiddenField ID="hfAcceptanceNo"
                                runat="server"
                                Value='<%# Eval("Acceptance_No") %>' />

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="S.No">

                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>

                        <ItemStyle Width="60px" />

                    </asp:TemplateField>


                    <asp:BoundField DataField="Godown_Name"
                        HeaderText="Godown Name" />

                    <asp:BoundField DataField="Acceptance_No"
                        HeaderText="Acceptance No" />

                    <asp:BoundField DataField="Acceptance_Date"
                        HeaderText="Acceptance Date"
                        DataFormatString="{0:dd/MM/yyyy}" />

                    <asp:BoundField DataField="No_of_Bags"
                        HeaderText="No Of Bags" />

                    <asp:BoundField DataField="Rec_Qty"
                        HeaderText="Received Qty" />

                </Columns>

                <EmptyDataRowStyle CssClass="emptymsg" />

            </asp:GridView>


            <div style="margin-top:15px; text-align:center;">

                <asp:Button ID="btnDelete"
                    runat="server"
                    Text="Delete Selected Acceptance"
                    CssClass="btnShow"
                    Style="background:red; display:none;"
                    OnClientClick="return confirm('Are you sure want to delete selected records ?');"
                    OnClick="btnDelete_Click" />

            </div>

        </div>

    </div>

</asp:Content>