<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Branch_wise_FIFO.aspx.cs" Inherits="StatePages_Rpt_Branch_wise_FIFO" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title style="color: white;">State Level Rerport</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.css" rel="stylesheet" />
    <script src="scripts/dataTables.bootstrap4.min.js"></script>
    <script src="scripts/jquery.dataTables.min.js"></script>


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
            font-size: xx-large;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #557db0 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 25px;
                text-align: center;
                font-size: xx-large;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #557db0 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                    font-size: xx-large;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                    font-size: xx-large;
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
            background-color: #467d9b; /* Green */
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
            Width: 110px;
            height: 20px;
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

        #container {
            display: flex; /* establish flex container */
            flex-direction: row; /* default value; can be omitted */
            flex-wrap: nowrap; /* default value; can be omitted */
            justify-content: space-between; /* switched from default (flex-start, see below) */
            background-color: lightyellow;
        }

            #container > div {
                /*width: 140px;*/
                height: 130px;
                /*border: 2px dashed red;*/
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="container" runat="server">
  <div><img src="../Warehouse/images/mpwlc.png" width: 75px;/></div>
  <div ><span style="font-size: 39px; font-family:bold;"> M.P. WAREHOUSING & LOGISTICS CORPORATION</span>
        <br />
      <span style="font-size: 30px; font-family:bold; text-align:center;margin-left: 190px;"> गोदाम वार FIFO नीति से स्टॉक के उठाव  की स्थिति </span> <asp:Label ID="lblsyncdate" runat="server"></asp:Label></span>
  </div>
  <div><span style="WIDTH: 52.68mm; HEIGHT: 6.35mm;">Date:-</span> <asp:Label ID="labelName" runat="server"></asp:Label> 
     <%-- <br />
      <br />
      <br />
      <br />
      <br />
      <span style="font-size: 20px; font-family:bold;">Qty In M.T.</span>--%>

  </div>
</div>
           <div style="text-align: center; font-size: large;">

                <asp:Label ID="Label1" runat="server" Text="Region"></asp:Label>
                <asp:DropDownList ID="ddlRegion" Height="25px" Width="300px" runat="server" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>

                <asp:Label ID="Label2" runat="server" Text="District"></asp:Label>
                <asp:DropDownList ID="ddldistrict" Height="25px" Width="300px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                </asp:DropDownList>

                <asp:Label ID="Label3" runat="server" Text="Branch"></asp:Label>
                <asp:DropDownList ID="ddlbranch" Height="25px" Width="300px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Button ID="tbnview" runat="server" Text="View" CssClass="button button2" Width="165px" Height="50px" OnClick="tbnview_Click" />
               <br />
               <asp:Button ID="btnback" runat="server" Text="Back" CssClass="button button2" OnClick="btnback_Click"  />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
            </div>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central; ">
               
                <tr>
                    <td style="text-align: left;" colspan="8">

                        <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>  
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnBranchId" runat="server" Value='<%#Eval("BranchId")%>' />
                                         <asp:HiddenField runat="server" ID="hdndiffirence" Value='<%# Eval("FIFOFrizwedStock") %>' />
                                     <asp:HiddenField runat="server" ID="hdnQty" Value='<%# Eval("Qty") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                               
                                <asp:TemplateField HeaderText="Godown Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Oldest Stock Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFirstWHRDate" runat="server" Text='<%# Eval("FirstWHRDate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProb_Remark" runat="server" Text='<%# Eval("Prob_Remark") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="View Details" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Button ID="btnAnnexure_B" Text="View Details" runat="server" CommandName="Annexure_B" CssClass=".button" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Label ID="lblMsg" runat="server" BackColor="Red" Font-Size="Large"></asp:Label>
        </div>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "Rpt_Procurement_Rabi2022_District.xls"
                });
            });
        </script>
       
    </form>
</body>
</html>
