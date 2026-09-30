<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteGodownInspection.aspx.cs" Inherits="JointVentureScheme_DeleteGodownInspection" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Godown Inspection Management</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
    <style type="text/css">
        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            margin: 0;
            background-color: #f4f7f9;
            color: #333;
        }

        .nav-bar {
            background-color: #2c3e50;
            color: #ffffff;
            padding: 12px 25px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }

            .nav-bar a {
                color: #ffffff;
                text-decoration: none;
                font-weight: 600;
                font-size: 14px;
                margin: 0 10px;
            }

                .nav-bar a:hover {
                    color: #3498db;
                }

        .main-card {
            width: 96%;
            margin: 25px auto;
            background: #ffffff;
            padding: 30px;
            border-radius: 4px;
            box-shadow: 0 1px 3px rgba(0,0,0,0.1);
            border-top: 5px solid #2c3e50;
        }

        .official-title {
            text-align: center;
            color: #2c3e50;
            font-size: 22px;
            font-weight: bold;
            margin-bottom: 25px;
            text-transform: uppercase;
            border-bottom: 1px solid #eee;
            padding-bottom: 15px;
        }

        .warning-strip {
            background-color: #aa9bd1a8;
            border: 1px solid #607d8b00;
            color: #14226ead;
            padding: 12px;
            margin-bottom: 25px;
            border-radius: 4px;
            font-weight: 600;
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .search-area {
            background: #9abcdde8;
            border: 1px solid #9bbbdb;
            padding: 20px;
            border-radius: 4px;
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 15px;
            margin-bottom: 30px;
        }

        .form-input {
            padding: 8px 12px;
            border: 1px solid #ced4da;
            border-radius: 4px;
            font-size: 14px;
        }

        .btn-action {
            background-color: #1a73e8;
            color: white;
            border: none;
            padding: 9px 25px;
            border-radius: 4px;
            cursor: pointer;
            font-weight: 600;
            transition: background 0.2s;
        }

            .btn-action:hover {
                background-color: #1557b0;
            }

        /* Grid Styling */
        .official-grid {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }

            .official-grid th {
                background-color: #34dbca87;
                color: #495057;
                font-weight: 1000;
                text-align: center;
                padding: 12px;
                border-bottom: 2px solid#36c784;
                text-transform: uppercase;
                font-size: 12px;
            }

            .official-grid td {
                padding: 12px;
                border-bottom: 1px solid #eee;
                text-align: center;
                font-size: 13px;
                font-weight: 700;
                color: #333;
            }

            .official-grid tr:hover {
                background-color: #f8f9fa;
            }

        .delete-link {
            color: #d93025;
            text-decoration: none;
            font-weight: 600;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 5px;
        }

            .delete-link:hover {
                color: #a50e0e;
            }
    </style>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
</head>
<body onload="noBack();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm1" runat="server" />

        <div class="nav-bar">
            <div>
                <asp:LinkButton ID="linkHome" runat="server" PostBackUrl="~/JointVentureScheme/DistrictWiseJVSOffer.aspx">
                    <i class="fas fa-landmark"></i> HOME
                </asp:LinkButton>
            </div>
            <div>
                <i class="fas fa-user-tie"></i>
                <asp:Label ID="lblUser" runat="server" />
                <asp:LinkButton ID="lnkLogout" runat="server" OnClick="lnkLogout_Click" Style="margin-left: 25px; color: #ff7675;">
                    <i class="fas fa-power-off"></i> LOGOUT
                </asp:LinkButton>
            </div>
        </div>

        <div class="main-card">
            <h2 class="official-title">Godown Inspection Records Management</h2>

            <div class="warning-strip">
                <i class="fas fa-exclamation-circle" style="font-size: 18px;"></i>
                सावधानी: यदि आप गोदाम का निरिक्षण डिलीट करते हैं, तो उस गोदाम का एग्रीमेंट भी स्वतः डिलीट हो जाएगा।
            </div>

            <div class="search-area">
                <label class="label-bold" style="font-weight:bold">Season:</label>
                <asp:DropDownList ID="ddl_session" runat="server" CssClass="form-input" Width="220px">
                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                    <asp:ListItem Value="Previous">Previous</asp:ListItem>
                    <asp:ListItem Value="Rab2026_27">Rabi_2026_27</asp:ListItem>
                    <asp:ListItem Value="Rab2025_26">Rabi_2025_26</asp:ListItem>
                    <asp:ListItem Value="Kharif2024_25">Kharif2024_25</asp:ListItem>
                    <asp:ListItem Value="Rab2024_25">Rabi_2024_25</asp:ListItem>
                    <asp:ListItem Value="Kharif2023_24">Kharif2023_24</asp:ListItem>
                    <asp:ListItem Value="Rab2023_24">Rabi_2023_24</asp:ListItem>
                    <asp:ListItem Value="Kharif_2022_23">Kharif_2022_23</asp:ListItem>
                </asp:DropDownList>

                <label class="label-bold" style="font-weight:bold">Registration ID:</label>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-input" Width="180px" placeholder="Enter ID"></asp:TextBox>

                <asp:Button ID="btnSearch" runat="server" Text="VIEW RECORDS" OnClick="btnSearch_Click" CssClass="btn-action" />
            </div>

            <div style="overflow-x: auto;">
                <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False"
                    CssClass="official-grid" GridLines="None" OnRowCommand="gvGodown_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S.NO.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                <asp:HiddenField ID="hdnInspectionID" runat="server" Value='<%# Eval("Inspection_Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Inspection_Id" HeaderText="Insp ID" />
                        <asp:BoundField DataField="Registration_Id" HeaderText="Reg ID" />
                        <asp:BoundField DataField="GodownId" HeaderText="Godown ID" />
                        <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse Name" />
                        <asp:BoundField DataField="Godown_No" HeaderText="G.No" />
                        <asp:BoundField DataField="G_OfferCapacity" HeaderText="Offer Cap" />
                        <asp:BoundField DataField="Vacant_Capacity" HeaderText="Vacant" />
                        <asp:BoundField DataField="Fit_Unfit" HeaderText="Status" />
                        <asp:BoundField DataField="Remark" HeaderText="Remark" />
                        <asp:BoundField DataField="Agree_Capacity" HeaderText="Agmt Cap" />
                        <asp:BoundField DataField="Insp_Date" HeaderText="Insp Date" DataFormatString="{0:dd/MM/yyyy}" />

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="delete-link"
                                    CommandName="DeleteRecord" CommandArgument='<%# Eval("Inspection_Id") %>'
                                    OnClientClick="return confirm('Are you sure you want to delete this inspection? This will also remove the agreement.');">
                                    <i class="fas fa-trash-alt"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="grid-header" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
