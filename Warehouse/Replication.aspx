<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Replication.aspx.cs" Inherits="Replication.Replication" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Replicate Table Data</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" />
</head>
<body style="background-color: #e37272">
    <div class="p-2" style="background-color: #58c5b1;">
        <div class="container text-center">
            <h3>Welcome to Replicate Table service</h3>
        </div>
    </div>
    <form id="form1" runat="server" class="m-auto h-100 w-75">
        <div class="jumbotron text-center mt-5" style="background-color: #58c5b1; border-radius: 20px">
            <asp:Label ID="msg" CssClass="alert-info" runat="server" Text=""></asp:Label>

            <h2>Replicate Table Data From Intergrated_MP_STORAGE Database</h2>
            <br />
            <br />


            <asp:Label ID="ddltable" runat="server" Text="Choose Table" Style="text-align: left"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList1" runat="server" Height="30px" CssClass="text-bold">
                <asp:ListItem Enabled="true" Text="tbl_storage_Depositor_WHR_Relation" Value="tbl_storage_Depositor_WHR_Relation"></asp:ListItem>
                <asp:ListItem Text="tbl_storage_Stacking_Details" Value="tbl_storage_Stacking_Details"></asp:ListItem>
                <asp:ListItem Text="tbl_Storage_Receipt_Details" Value="tbl_Storage_Receipt_Details"></asp:ListItem>
                <asp:ListItem Text="tbl_Storage_GatePass_Enrty" Value="tbl_Storage_GatePass_Enrty"></asp:ListItem>
                <asp:ListItem Text="tbl_Delivery_Stacking_Details_GatePass" Value="tbl_Delivery_Stacking_Details_GatePass"></asp:ListItem>
                <asp:ListItem Text="tbl_RO_Details" Value="tbl_RO_Details"></asp:ListItem>
                <asp:ListItem Text="tbl_Storage_Arrival_Stock" Value="tbl_Storage_Arrival_Stock"></asp:ListItem>
                <asp:ListItem Text="tbl_Storage_Receipt_Details" Value="tbl_Storage_Receipt_Details"></asp:ListItem>
                <asp:ListItem Text="tbl_Aepds_Truckchit_Data_DisGodown" Value="tbl_Aepds_Truckchit_Data_DisGodown"></asp:ListItem>
                <asp:ListItem Text="tbl_Storage_Final_Stock_Delivery_Order" Value="tbl_Storage_Final_Stock_Delivery_Order"></asp:ListItem>
                <asp:ListItem Text="tbl_Storage_Final_Stock_Delivery_GatePass" Value="tbl_Storage_Final_Stock_Delivery_GatePass"></asp:ListItem>
                <asp:ListItem Text="tbl_MetaData_GODOWN_2018" Value="tbl_MetaData_GODOWN_2018"></asp:ListItem>
                <asp:ListItem Text="tbl_MetaData_DEPOSITOR" Value="tbl_MetaData_DEPOSITOR"></asp:ListItem>
                <asp:ListItem Text="Tbl_Truck_Chit_Getpass_Entry" Value="Tbl_Truck_Chit_Getpass_Entry"></asp:ListItem>
            </asp:DropDownList>

            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="No. Of Records"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="noOfRecord" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="fromDate" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="To Date"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="toDate" runat="server"></asp:TextBox>
            <br />
            <br />

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-sm" Text="Replicate" Height="30px" OnClick="Button1_Click" />
            <br />
        </div>
    </form>
    <div>
        <hr />
        <footer class="text-center">
            <p>&copy; - Replicate Table Data Application</p>
        </footer>
    </div>

    <%--    bootstrap cdn--%>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js"></script>
</body>
</html>
