<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertLicenceDetailsForRabi2026_27.aspx.cs" Inherits="StatePages_InsertLicenceDetailsForRabi2026_27" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Insert Licence Details for Rabi 2026-27</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f7f8;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 600px;
            margin: 80px auto;
            background-color: #fff;
            padding: 50px;
            box-shadow: 0 6px 18px rgba(0,0,0,0.15);
            border-radius: 16px;
            text-align: center;
        }
        h2 {
            color: #333;
            margin-bottom: 40px;
            font-size: 26px;
        }
        .btn-primary {
            background-color: #007bff;
            border: none;
            color: white;
            padding: 18px 40px;
            font-size: 18px;
            font-weight: bold;
            border-radius: 12px;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 4px 8px rgba(0,0,0,0.2);
        }
        .btn-primary:hover {
            background-color: #0056b3;
            transform: translateY(-2px);
            box-shadow: 0 6px 12px rgba(0,0,0,0.25);
        }
        #lblMessage {
            display: block;
            margin-top: 25px;
            font-weight: bold;
            font-size: 16px;
            color: #28a745; /* green for success */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Insert Licence Details for Rabi 2026-27</h2>
            <asp:Button ID="btnInsertLicence" runat="server" CssClass="btn-primary" Text="Insert Licence Details" OnClick="btnInsertLicence_Click" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>