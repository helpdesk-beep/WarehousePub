<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Godown2.aspx.cs" Inherits="StatePages_Godown2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
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
        <%--<h1> Distance between Change Geo Coordinate of Godown</h1>--%>
    </div>

        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

            <tr>
                <td style="text-align: left; overflow: scroll;">
                    <asp:GridView ID="gvbranch" runat="server" AutoGenerateColumns="False"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
         <Columns>

       
          
                           <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                            <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                            <asp:BoundField DataField="GodownID" HeaderText="Godown ID" />
                                  <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />
                            <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" />
                            <asp:BoundField DataField="Godown_Capacity" HeaderText="Godown Capacity" />
                            <asp:BoundField DataField="Utilized Capacity" HeaderText="Utilized Capacity" />
        
           </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
 
 


</asp:Content>

