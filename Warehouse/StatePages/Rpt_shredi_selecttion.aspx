<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Rpt_shredi_selecttion.aspx.cs" Inherits="StatePages_Rpt_shredi_selecttion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
             text-align: center;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                 text-align: center;
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
                     text-align: center;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                     text-align: center;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                     text-align: center;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                         text-align: center;
                    }
    </style>
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=GridView1.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }
    </script>
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= GridView1.ClientID %>');
            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
                'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
        }
    </script>
    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
            Width: 200px;
            height: 50px;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }
    </style>

    <div>
        <div class="container py-4">
            <div class="card">
                <div class="card-body">
                    <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                </div>
            </div>

        </div>
        <br />
        <br />

        <table>
            <tr>

                       <td>
            <asp:Label ID="Label3" runat="server" Text="Select Region"></asp:Label>
            <asp:DropDownList ID="ddlregion" Height="25px" Width="300px" runat="server" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                </td>

                <td>
            <asp:Label ID="Label2" runat="server" Text="Select District"></asp:Label>
            <asp:DropDownList ID="ddldist" Height="25px" Width="300px" runat="server" ></asp:DropDownList>
                </td>
       <%--         <td>
             <asp:Label ID="Label1" runat="server" Text="">Select Branch</asp:Label>
            <asp:DropDownList ID="ddlbranch" Height="25px" Width="300px" runat="server" ></asp:DropDownList>
                </td>--%>
                <td>
                    <asp:Button ID="SearchID" runat="server" Text="Search" OnClick="SearchID_Click" />
                </td>
            </tr>
        </table>


        <div style="text-align: center; font-size: large;">

        

        </div>
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

            <tr>
                <td style="text-align: left; overflow: scroll;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  ShowFooter="true"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField  DataField="Regionnm" HeaderText="Region Name" />
                            <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                           <%-- <asp:BoundField DataField="DepotName" HeaderText="Branch_Name" />--%>
                            <asp:BoundField DataField="TotalRegistrasion" HeaderText="Total_Registrasion" />
                            <asp:BoundField DataField="TotalChoice" HeaderText="Total_Choice_Filling" />
                            <asp:BoundField DataField="ChoiceA" HeaderText=" श्रेणी अ " />
                             <asp:BoundField DataField="ChoiceB" HeaderText=" श्रेणी ब  " />
                              <asp:BoundField DataField="ChoiceBR" HeaderText=" Branch Reject " />
                            <asp:BoundField DataField="RamaningForChoices" HeaderText="Ramaning_For_Choices" />


<%--                             <asp:TemplateField HeaderText="View Deatils">
                    <ItemTemplate >     

                   <a id="Edit" visible="true" href="DetailGodownCapForState.aspx?TYPE=<%#Eval("Hired_Type")%>&BID=<%#Eval("BranchID")%>">View Deatils</a>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                
                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script src="../../JS/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=GridView1]").table2excel({
                filename: "ABC.xls"
            });
        });
    </script>
</asp:Content>

