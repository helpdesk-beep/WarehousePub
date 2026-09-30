<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default5.aspx.cs" Inherits="Default5" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Crop Year Data</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            padding: 20px;
            background-color: #f8f9fa;
        }

        .container {
            background: #fff;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
            max-width: 1200px;
            margin: auto;
        }

        .gv-style {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            background-color: white;
        }

            .gv-style th {
                background-color: #4a69bd;
                color: white;
                padding: 12px;
                text-align: left;
                text-transform: uppercase;
                font-size: 13px;
            }

            .gv-style td {
                padding: 10px;
                border-bottom: 1px solid #eee;
                color: #333;
            }

            .gv-style tr:hover {
                background-color: #f1f2f6;
            }

        .btn-print {
            background-color: #1e3799;
            color: white;
            padding: 12px 25px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-weight: bold;
            margin-top: 15px;
        }

            .btn-print:hover {
                background-color: #0c2461;
            }
    </style>

    <script type="text/javascript">
        function SelectAllCheckboxes(headerChk) {
            // Find the GridView by its ID
            var gv = document.getElementById('<%= gvCropData.ClientID %>');
            // Get all input elements inside the grid
            var inputs = gv.getElementsByTagName("input");

            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox" && inputs[i] != headerChk) {
                    inputs[i].checked = headerChk.checked;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 style="color: #1e3799; margin-top: 0;">Crop Year Reports: 2023-24</h2>
            <hr />

            <asp:GridView ID="gvCropData" runat="server" AutoGenerateColumns="False"
                CssClass="gv-style" DataKeyNames="Bill_Number">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="SelectAllCheckboxes(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="District_Name" HeaderText="District" />
                    <asp:BoundField DataField="DepotName" HeaderText="Depot" />
                    <asp:BoundField DataField="Bill_Number" HeaderText="Bill No" />
                    <asp:BoundField DataField="Commodity" HeaderText="Commodity" />
                    <asp:BoundField DataField="Bill_Month" HeaderText="Month" />
                    <asp:BoundField DataField="Rate" HeaderText="Rate" />
                </Columns>
                <EmptyDataTemplate>
                    <div style="padding: 20px; color: #eb4d4b; font-weight: bold;">No records found.</div>
                </EmptyDataTemplate>
            </asp:GridView>

            <asp:Button ID="btnPrint" runat="server" Text="Print Selected Records"
                OnClick="btnPrint_Click" CssClass="btn-print" />

            <div style="margin-top: 10px;">
                <asp:Label ID="lblMsg" runat="server" ForeColor="#eb4d4b" Font-Bold="true"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>

