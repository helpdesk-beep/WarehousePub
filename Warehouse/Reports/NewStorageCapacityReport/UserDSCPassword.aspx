<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDSCPassword.aspx.cs" Inherits="UserDSCPassword" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>User Login Finder</title>
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600&display=swap" rel="stylesheet">

    <style>
        * {
            box-sizing: border-box;
            font-family: 'Poppins', sans-serif;
        }

        body {
            margin: 0;
            background: #eef2f7;
        }

        /* Top Header */
        .header {
            background: linear-gradient(90deg, #1e3c72, #2a5298);
            padding: 15px 30px;
            color: white;
            font-size: 20px;
            font-weight: 600;
            box-shadow: 0 4px 10px rgba(0,0,0,0.2);
        }

        /* Main Container */
        .container {
            padding: 30px;
            max-width: 1200px;
            margin: auto;
        }

        .card {
            background: white;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.08);
        }

        h2 {
            margin-top: 0;
            color: #1e3c72;
            font-weight: 600;
            text-align: center;
            margin-bottom: 25px;
        }

        .form-row {
            display: flex;
            gap: 20px;
            flex-wrap: wrap;
        }

        .input-box {
            flex: 1;
            min-width: 250px;
        }

        label {
            font-size: 14px;
            font-weight: 500;
            margin-bottom: 5px;
            display: block;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            border-radius: 6px;
            border: 1px solid #ccc;
            transition: 0.2s;
        }

            .form-control:focus {
                border-color: #2a5298;
                outline: none;
                box-shadow: 0 0 5px rgba(42,82,152,0.3);
            }

        .btn {
            background: #2a5298;
            color: white;
            border: none;
            padding: 10px 25px;
            border-radius: 6px;
            cursor: pointer;
            font-weight: 500;
            transition: 0.3s;
            margin-top: 23px;
        }

            .btn:hover {
                background: #1e3c72;
            }

        /* Grid Styling */
        .grid {
            margin-top: 30px;
            overflow-x: auto;
        }

            .grid table {
                width: 100%;
                border-collapse: collapse;
                font-size: 14px;
            }

            .grid th {
                background: #2a5298;
                color: white;
                padding: 10px;
                text-align: left;
            }

            .grid td {
                padding: 9px;
                border-bottom: 1px solid #ddd;
            }

        .highlightCell {
            background-color: #fff3cd !important;
            color: #b30000;
            font-weight: 600;
        }

        .grid tr:nth-child(even) {
            background: #f7f9fc;
        }

        /* Highlight Password & User_Id */
        .highlight {
            background: #fff3cd !important;
            font-weight: 600;
            color: #b30000;
        }

        @media (max-width: 768px) {
            .form-row {
                flex-direction: column;
            }

            .btn {
                width: 100%;
            }
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <div class="header">
            🔐 DSC Login Management Panel
        </div>

        <div class="container">
            <div class="card">
                <h2>DSC Login Finder</h2>

                <div class="form-row">
                    <div class="input-box">
                        <label>Select User Type</label>
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="-- Select --" Value="" />
                            <asp:ListItem Text="Godown" Value="Godown" />
                            <asp:ListItem Text="Branch" Value="Branch" />
                            <asp:ListItem Text="RO / AC" Value="ROAC" />
                            <asp:ListItem Text="RM" Value="RM" />
                        </asp:DropDownList>
                    </div>

                    <div class="input-box">
                        <label>Enter User ID</label>
                        <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" placeholder="Enter ID..." />
                    </div>

                    <div class="input-box" style="flex: 0;">
                        <asp:Button ID="btnSearch" runat="server" Text="🔍 Search" CssClass="btn" OnClick="btnSearch_Click" />
                    </div>
                </div>

                <div class="grid">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"
                        OnRowDataBound="GridView1_RowDataBound" />
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
