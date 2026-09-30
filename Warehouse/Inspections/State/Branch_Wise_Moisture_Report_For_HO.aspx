<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master"
    AutoEventWireup="true"
    CodeFile="Branch_Wise_Moisture_Report_For_HO.aspx.cs"
    Inherits="Inspections_State_Branch_Wise_Moisture_Report_For_HO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

        body {
            font-family: Arial;
            margin: 0;
            padding: 0;
        }

        .mainbox {
            width: 99%;
            margin: 10px auto;
            background: #ffffff;
            border-radius: 10px;
            padding: 12px;
            box-shadow: 0px 0px 8px #cccccc;
        }

        .heading {
            background-color: #0b6d90;
            color: #fff;
            padding: 12px;
            font-size: 24px;
            font-weight: bold;
            text-align: center;
            border-radius: 6px;
            margin-bottom: 15px;
        }

        .btnbox {
            text-align: right;
            margin-bottom: 12px;
        }

        .btn {
            background-color: #198754;
            color: white;
            border: none;
            padding: 8px 18px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            font-weight: bold;
        }

        .btn:hover {
            background-color: #146c43;
        }

        .grid {
            width: 100%;
            border-collapse: collapse !important;
            border: 1px solid #000 !important;
        }

        .grid th {
            background-color: #0b6d90 !important;
            color: White !important;
            padding: 10px;
            text-align: center;
            font-size: 14px;
            border: 1px solid #000 !important;
        }

        .grid td {
            padding: 8px;
            text-align: center;
            font-size: 13px;
            border: 1px solid #000 !important;
        }

        .grid tr:nth-child(even) {
            background-color: #f5f5f5;
        }

        .grid tr:hover {
            background-color: #e8f4ff;
        }

        @media print {

            body * {
                visibility: hidden;
            }

            #printArea,
            #printArea * {
                visibility: visible;
            }

            #printArea {
                position: absolute;
                left: 0;
                top: 0;
                width: 100%;
            }

            .btnbox {
                display: none;
            }
        }

    </style>

    <script type="text/javascript">

        function PrintPanel() {
            window.print();
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="printArea" class="mainbox">

        <div class="heading">
            Branch Wise Moisture Report For HO
        </div>

        <div class="btnbox">

            <%--<asp:Button ID="btnBack"
                runat="server"
                Text="Back"
                CssClass="btn"
                OnClick="btnBack_Click" />--%>

            &nbsp;

            <asp:Button ID="btnPrint"
                runat="server"
                Text="Print"
                CssClass="btn"
                OnClientClick="PrintPanel(); return false;" />

            &nbsp;

            <%--<asp:Button ID="btnExport"
                runat="server"
                Text="Export To Excel"
                CssClass="btn"
                OnClick="btnExport_Click" />--%>

        </div>

        <asp:GridView ID="gvDetails"
            runat="server"
            AutoGenerateColumns="false"
            CssClass="grid"
            ShowFooter="true"
            EmptyDataText="No Record Found"
            OnRowDataBound="gvDetails_RowDataBound">

            <Columns>

                <asp:BoundField DataField="S.No" HeaderText="S.No" />

                <asp:TemplateField HeaderText="Branch Name">
                    <ItemTemplate>

                        <asp:LinkButton ID="lnkBranch"
                            runat="server"
                            Text='<%# Eval("Branch Name") %>'
                            CommandArgument='<%# Eval("BranchId") %>'
                            ForeColor="Blue"
                            Font-Bold="true"
                            OnClick="lnkBranch_Click">
                        </asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Prev Stack" HeaderText="Prev Stack" />
                <asp:BoundField DataField="Current Stack" HeaderText="Current Stack" />
                <asp:BoundField DataField="Total Stack" HeaderText="Total Stack" />
                <asp:BoundField DataField="Sent_DM" HeaderText="Sent DM" />
                <asp:BoundField DataField="Submit To FCI" HeaderText="Submit To FCI" />
                <asp:BoundField DataField="FCI Inspected Stack" HeaderText="FCI Inspected Stack" />

            </Columns>

        </asp:GridView>

    </div>

</asp:Content>