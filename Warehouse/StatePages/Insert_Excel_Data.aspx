<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Insert_Excel_Data.aspx.cs" Inherits="StatePages_Insert_Excel_Data" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Excel and Insert to SQL</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 40px;
            background-color: #f5f6f8;
        }

        .container {
            background: white;
            width: 500px;
            margin: auto;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 10px #ccc;
        }

        h2 {
            text-align: center;
            color: #333;
        }

        .BTNBLUE {
            background-color: #007bff;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

            .BTNBLUE:hover {
                background-color: #0056b3;
            }

        .message {
            margin-top: 20px;
            font-weight: bold;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Upload Excel & Insert Data</h2>
            <asp:FileUpload ID="FileUpload1" runat="server" /><br />
            <br />
            <asp:Button ID="btnUpload" runat="server" Text="Upload & Insert"
                CssClass="BTNBLUE" OnClick="btnUpload_Click" /><br />
            <asp:Label ID="lblMsg" runat="server" CssClass="message" ForeColor="Green"></asp:Label>
        </div>
    </form>
</body>
</html>
