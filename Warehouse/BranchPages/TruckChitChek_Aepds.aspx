<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="TruckChitChek_Aepds.aspx.cs" Inherits="StatePages_TruckChitChek_Aepds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    >
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            $("#EmployeeGridViewList").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
    <style>
        .btnMargin {
            margin-bottom: 10px !important;
        }
    </style>
    <style>
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
            width: 100%;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #00aad2 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #00aad2 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>


    <div>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <%--<asp:Label ID="lblBranch" ForeColor="navy" runat="server" style="margin-left: -200px;" Font-Size="11pt" Font-Bold="true" Text="Branch:"></asp:Label>
        <asp:DropDownList ID="ddlbranch" runat="server" Width="20%" style="margin: -25px -30px 8px 202px;" class="form-control">
        </asp:DropDownList>--%>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTruckChitNo" Font-Size="11pt" style="margin-left: -200px;" Font-Bold="true" ForeColor="navy" runat="server" Text="Truck Chit No:"></asp:Label>
        <asp:TextBox runat="server" class="form-control" Width="20%" style="margin: -25px -30px 8px 313px;" ID="txtTruckChitNo"/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button  ID="btnsearch" runat="server" Text="Search"  Width="80px" style="margin: -34px -18px 21px 979px;"
                        CssClass="BTNBLUE" OnClick="btnsearch_Click" AutoPostBack="true" />



        <%--<h1> Distance between Change Geo Coordinate of Godown</h1>--%>
    </div>

    <table style="border: solid 5px #e3e3e8; width: 100%">
        <%--<table>--%>
    
            <%--<tr>
                <td>
                 <asp:Label ID="lblBranch" ForeColor="navy" runat="server" Font-Size="11pt" Font-Bold="true" Text="Branch:"></asp:Label>
        
            </td>
            <td>
               <asp:DropDownList ID="ddlbranch" runat="server" class="form-control">
        </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblTruckChitNo" Font-Size="11pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Truck Chit No:"></asp:Label>
        
            </td>
            <td>
               <asp:TextBox runat="server" class="form-control" ID="txtTruckChitNo"/>
            </td>
            <td>
                 <asp:Button  ID="btnsearch" runat="server" Text="Search"  Width="80px"
                        CssClass="BTNBLUE" OnClick="btnsearch_Click" AutoPostBack="true" />
            </td>
            </tr>--%>
      

        <tr>
            

            <%--<td style="text-align: left; overflow: scroll;">--%>
            <td style="text-align: left">
                <asp:GridView ID="gvbranch" runat="server" AutoGenerateColumns="False"
                    CssClass="Grid">
                    <columns>

                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                        <asp:BoundField DataField="Truck_Chit_No" HeaderText="Truck Chit No" />
                        <asp:BoundField DataField="Truck_Chit_Date" HeaderText="Truck Chit Date" />
                        <asp:BoundField DataField="Truck_Number" HeaderText="Truck Number" />
                        <asp:BoundField DataField="Challan_Year" HeaderText="Challan Year" />
                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity_Name" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                        <asp:BoundField DataField="Godown_Id" HeaderText="Godown ID" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                        <asp:BoundField DataField="No_of_Bags" HeaderText="No of Bags" />
                        <asp:BoundField DataField="Quantity_Dispatch" HeaderText="Quantity Dispatch" />
                        <%-- <asp:BoundField DataField="Godown_Capacity" HeaderText="Godown Capacity" />
                            <asp:BoundField DataField="Utilized Capacity" HeaderText="Utilized Capacity" />--%>
                    </columns>

                </asp:GridView>
            </td>
        </tr>
    </table>




</asp:Content>

