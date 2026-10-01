<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master"
    AutoEventWireup="true"
    CodeFile="~/Region/NCCF_Payment_Details.aspx.cs"
    Inherits="Region_NCCF_Payment_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $("[id*=ddlDistrict]").select2();
        });

    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlBranch]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlGodown]").select2();
        });
    </script>
    <style>
        .panel {
            width: 95%;
            margin: auto;
            border: 2px solid #0bb6e6;
            padding: 15px;
            border-radius: 6px;
            font-family: Arial;
            background: #f9f9f9;
        }

        .title {
            text-align: center;
            background: #0bb6e6;
            color: white;
            padding: 12px;
            font-size: 20px;
            font-weight: bold;
            border-radius: 4px;
        }

        /* FILTER BOX */
        .filter-box {
            width: 98%;
            margin-top: 15px;
            background: white;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0px 1px 4px rgba(0,0,0,0.1);
        }

        .filter-table {
            width: 100%;
        }

            .filter-table td {
                padding: 8px;
                vertical-align: middle;
                text-align: left;
            }

        label {
            font-weight: bold;
        }

        select, input {
            width: 180px;
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .btn {
            padding: 7px 20px;
            background: #0bb6e6;
            border: none;
            color: white;
            cursor: pointer;
            border-radius: 4px;
            font-weight: bold;
        }

            .btn:hover {
                background: #088bb5;
            }

        /* GRID */
        .grid {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
            background: white;
        }

            .grid th {
                background: #0bb6e6;
                color: white;
                padding: 10px;
                text-align: center;
                font-size: 13px;
            }

            .grid td {
                padding: 7px;
                text-align: center;
                border: 1px solid #ddd;
                font-size: 12px;
            }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel">

        <div class="title">
            NCCF Payment Received Details
        </div>

        <!-- FILTER SECTION -->
        <div class="filter-box">

            <table class="filter-table">
                <tr>

                    <td>
                        <label>District</label></td>
                    <td>
                        <asp:DropDownList ID="ddlDistrict" runat="server"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>

                    <td>
                        <label>Branch</label></td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>

                    <td>
                        <label>Godown</label></td>
                    <td>
                        <asp:DropDownList ID="ddlGodown" runat="server"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>


                </tr>
            </table>

        </div>

        <!-- GRID -->
        <asp:GridView ID="gvPayment" runat="server"
            CssClass="grid"
            AutoGenerateColumns="False"
            OnRowDataBound="gvPayment_RowDataBound"
            ShowFooter="True">

            <Columns>
                <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>

                <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                <asp:BoundField DataField="District_Name" HeaderText="District" />
                <asp:BoundField DataField="DepotName" HeaderText="Depot" />
                <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" />

                <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" />
                <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" />
                <asp:BoundField DataField="Month_Name" HeaderText="Month" />

                <asp:BoundField DataField="StorageBillNumber" HeaderText="Storage Bill No" />
                <asp:BoundField DataField="RentBillNumber" HeaderText="Rent Bill No" />

                <asp:BoundField DataField="PaymentReceivedDate" HeaderText="Payment Date"
                    DataFormatString="{0:dd/MM/yyyy}" />

                <asp:BoundField DataField="ReceivedPayment" HeaderText="Received Payment" />

            </Columns>

        </asp:GridView>

    </div>

</asp:Content>
