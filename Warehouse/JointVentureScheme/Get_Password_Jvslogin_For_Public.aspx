<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Get_Password_Jvslogin_For_Public.aspx.cs" Inherits="JointVentureScheme_Get_Password_Jvslogin_For_Public" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registration Search</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
    <style type="text/css">
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 0; padding: 0; background-color: #f4f7f6; }
        
        /* Navigation Header */
        .nav-header { background-color: #008CBA; padding: 12px 25px; color: white; display: flex; justify-content: space-between; align-items: center; box-shadow: 0 2px 5px rgba(0,0,0,0.1); }
        .nav-header a { color: white; text-decoration: none; font-weight: bold; margin: 0 10px; font-size: 14px; }
        
        /* Search Area */
        .search-container { background: #a2d9df; padding: 35px; border-radius: 8px; box-shadow: 0 4px 6px rgba(0,0,0,0.05); margin: 30px auto; width: fit-content; border-top: 5px solid #008CBA; text-align: center; }
        .custom-textbox { padding: 10px 15px; border: 1px solid #ccc; border-radius: 4px; width: 350px; outline: none; font-size: 14px; }
        .btn-search { background-color: #008CBA; color: white; border: none; padding: 10px 25px; border-radius: 4px; font-weight: bold; cursor: pointer; transition: 0.3s; }
        .btn-search:hover { background-color: #005f7a; }

        /* Grid Layout & Centering */
        .grid-wrapper { display: flex; justify-content: center; width: 100%; padding-bottom: 60px; }
        .grid-container { width: 100%; max-width: 650px; } /* Increased size */

        .card-wrapper { background: #ffffff; border-radius: 10px; overflow: hidden; box-shadow: 0 8px 16px rgba(0,0,0,0.1); border: 1px solid #d1d9e6;  border:1px solid green; width:650px}

        /* Grid Table Styling */
        .vertical-table { 
            width: 100%; /* */
            border-collapse: collapse; /* */
             background-color: #f8fafc; 
        }
           
        /* Official Themed Grid */
        .vertical-table { width: 100%; border-collapse: collapse; background-color: #f8fafc; }
        
        .vertical-table th { 
            background-color: #eef2f7; /* Official background */
            color: #6174d5fc; 
            padding: 18px; 
            text-align: left; 
            width: 20%; 
            border: 1px solid #cbd5e1; 
            font-weight: bold; /* Bold Header */
            text-transform: uppercase;
            font-size: 13px;
           
        }

        .vertical-table td { 
            padding: 18px; 
            text-align: left; 
            border: 1px solid #cbd5e1; 
            color: #1e293b; 
            background-color: #eef2f7; 
            font-weight: bold; /* Bold Values */
            font-size: 15px;
            
            
        }

        /* Red Password Highlight */
        .password-text { color: #d9534f !important; font-size: 16px; }
        
        .copy-footer { text-align: center; padding: 20px; background: #b9abcd; border-top: 1px solid #cbd5e1; }
        .btn-copy { background-color: #ffffff; color: #28a745; border: 2px solid #28a745; padding: 10px 40px; border-radius: 25px; font-weight: bold; cursor: pointer; transition: 0.3s; }
        .btn-copy:hover { background-color: #28a745; color: white; }
    </style>
    
    <script type="text/javascript">
        function copyRegistrationDetails(btn) {
            var container = btn.closest('.card-wrapper');
            var table = container.querySelector('.vertical-table');
            var rows = table.rows;

            // Logic updated for 6 rows (since S.No is removed)
            var details = "Registration Details\n" +
                "Reg No : " + rows[0].cells[1].innerText.trim() + "\n" +
                "Email ID : " + rows[1].cells[1].innerText.trim() + "\n" +
                "Mobile No : " + rows[2].cells[1].innerText.trim() + "\n" +
                "Auth Person : " + rows[3].cells[1].innerText.trim() + "\n" +
                "Date : " + rows[4].cells[1].innerText.trim() + "\n" +
                "Password : " + rows[5].cells[1].innerText.trim();

            var temp = document.createElement("textarea");
            document.body.appendChild(temp);
            temp.value = details;
            temp.select();
            document.execCommand("copy");
            document.body.removeChild(temp);
            alert("Success: Details copied to clipboard!");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
        
        <div class="nav-header">
            <div>
                <asp:LinkButton ID="link1" runat="server" PostBackUrl="https://mpwarehousing.mp.gov.in/warehouse/JointVentureScheme/DistrictWiseJVSOffer.aspx">
                    <i class="fa-solid fa-house"></i> Home
                </asp:LinkButton>
            </div>
            <div>
                <span style="margin-right:20px;"><i class="fa-solid fa-user"></i> Welcome: <asp:Label ID="lbluser" runat="server"></asp:Label></span>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                    <i class="fa-solid fa-right-from-bracket"></i> Logout
                </asp:LinkButton>
            </div>
        </div>

        <div class="search-container">
            <b style="font-size:16px;">Search Detail:</b>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="custom-textbox" placeholder="Enter Reg No, Mobile or Email"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="SEARCH" CssClass="btn-search" OnClick="btnSearch_Click" />
        </div>

        <div class="grid-wrapper">
            <div class="grid-container">
                <asp:GridView ID="RegGrid" runat="server" AutoGenerateColumns="False" GridLines="None" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="card-wrapper">
                                    <table class="vertical-table">
                                        <tr>
                                            <th>Reg Id</th>
                                            <td><%# Eval("Reg_No") %></td>
                                        </tr>
                                        <tr>
                                            <th>Email ID</th>
                                            <td><%# Eval("EmailID") %></td>
                                        </tr>
                                        <tr>
                                            <th>Mobile No</th>
                                            <td><%# Eval("MobileNo") %></td>
                                        </tr>
                                        <tr>
                                            <th>Auth Person</th>
                                            <td><%# Eval("Auth_Person") %></td>
                                        </tr>
                                        <tr>
                                            <th>Date</th>
                                            <td><%# Eval("CreatedDate") %></td>
                                        </tr>
                                        <tr>
                                            <th>Password</th>
                                            <td class="password-text"><%# Eval("Password") %></td>
                                        </tr>
                                    </table>
                                    <div class="copy-footer">
                                        <button type="button" class="btn-copy" onclick="copyRegistrationDetails(this)">
                                            <i class="fa-solid fa-copy"></i> Copy Details
                                        </button>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div style="padding: 30px; text-align: center; color: #666; background:white; border-radius:8px; border: 1px dashed #ccc;">
                            No records found. Please enter valid search criteria.
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>