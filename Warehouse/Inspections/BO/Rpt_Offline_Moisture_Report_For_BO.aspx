<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="~/Inspections/BO/Rpt_Offline_Moisture_Report_For_BO.aspx.cs" Inherits="Inspections_BO_Rpt_Offline_Moisture_Report_For_BO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Godown Wise Offline Moisture Entry Report</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }

        .btn {
            padding: 8px 15px;
            margin-right: 10px;
            font-weight: bold;
            cursor: pointer;
        }

        .btn-excel {
            background-color: #217346;
            color: white;
            border: none;
        }

        .btn-pdf {
            background-color: #d9534f;
            color: white;
            border: none;
        }

        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

            .grid-view th {
                background-color: #1c5a96;
                color: white;
                padding: 8px;
                text-align: left;
            }

            .grid-view td {
                padding: 8px;
                border: 1px solid #ddd;
            }

        .subtotal-row {
            background-color: #eaf2f8;
            font-weight: bold;
        }

        .grandtotal-row {
            background-color: #d4e6f1;
            font-weight: bold;
            border-top: 2px solid #1c5a96;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <h2>Region Wise Offline Moisture Entry For HO</h2>

        <asp:Button ID="btnExcel" runat="server" Text="Export To Excel" OnClick="btnExcel_Click" CssClass="btn btn-excel" />
        <asp:Button ID="btnPDF" runat="server" Text="Print To PDF" OnClick="btnPDF_Click" CssClass="btn btn-pdf" />

        <br />
        <br />

        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False"
            CssClass="grid-view" OnRowDataBound="gvReport_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                <asp:BoundField DataField="Total_Stack" HeaderText="Total Stack" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Sent to FCI/DM" HeaderText="Sent to FCI/DM" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Inspected By FCI" HeaderText="Inspected By FCI" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Pending As FCI" HeaderText="Pending As FCI" ItemStyle-HorizontalAlign="Right" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

