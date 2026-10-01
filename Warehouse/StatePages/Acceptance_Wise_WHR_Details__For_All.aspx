<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Reports/Branch/Acceptance_Wise_WHR_Details__For_All.aspx.cs" Inherits="Reports_Branch_Acceptance_Wise_WHR_Details__For_All" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title style="color: white;">State Level Rerport</title>
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link href="../../assets/New/css/bootstrap-multiselect .css" rel="stylesheet" />

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
                background: #557db0 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 25px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #557db0 url(Images/grid-pgr.png) repeat-x top;
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
    <style>
        li.header {
            font-size: 16px !important;
        }

        span#ctl00_spnUsername {
            text-transform: uppercase;
            color: white;
            font-weight: 600;
            font-size: 16px;
        }

        li.dropdown.tasks-menu.classhide a {
            padding: 4px 10px 0px 0px;
        }

        .datepicker.datepicker-dropdown.dropdown-menu.datepicker-orient-left.datepicker-orient-top {
            z-index: 9999 !important;
        }

        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>
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
  <div><img src="../../images/mpwlc.png" width: 75px;/></div>
  <div ><span style="font-size: 39px; font-family:bold;"> M.P. WAREHOUSING & LOGISTICS CORPORATION</span>
        <br />
      <span style="font-size: 30px; font-family:bold; text-align:center;margin-left: 190px;"> Acceptance Wise WHR Details</span>
  </div>
  <div><span style="WIDTH: 52.68mm; HEIGHT: 6.35mm;">Date:-</span> <asp:Label ID="labelName" runat="server"></asp:Label> 
      <br />
      <br />
      <br />
      <br />
      <br />
      <span style="font-size: 20px; font-family:bold;">Qty In Qtl.</span>

  </div>
</div>
           
            <div class="container py-4">
                <div class="card">

                    <div class="card-body">
                        <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<%--<asp:Button ID="btnUpdate" runat="server" CssClass="button button2" Text="Update Pending Months" Visible="false" OnClick="btnUpdate_Click" />--%>
                    </div>
                </div>

            </div>           
           
             <table style="border: solid 5px #e3e3e8; width: 60%; vertical-align: central; margin-left:400px; ">
              <tr id="tr1" runat="server" visible="true">
                        <td align="left">
                            <asp:Label ID="Label3" runat="server" Text="Division Name" ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                            <asp:DropDownList CssClass="form-control select2" ID="ddldivision" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                   <td align="left">
                            <asp:Label ID="Label4" runat="server" Text="District Name" ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                             <%--<asp:ListBox runat="server" ID="Godownchk"  ClientIDMode="Static" Class="form-control" SelectionMode="Multiple" ></asp:ListBox>--%>
                           <asp:DropDownList CssClass="form-control select2" ID="ddldistrict" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                        </td>
                   <td align="left">
                            <asp:Label ID="Label5" runat="server" Text="Branch Name" ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                             <%--<asp:ListBox runat="server" ID="Godownchk"  ClientIDMode="Static" Class="form-control" SelectionMode="Multiple" ></asp:ListBox>--%>
                          <asp:DropDownList CssClass="form-control select2" ID="ddlbranch" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                 <tr id="trgdnlist" runat="server" visible="true">
                        <td align="left">
                            <asp:Label ID="Label1" runat="server" Text="Session" ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                            <asp:DropDownList ID="ddlcropyear" runat="server" Width="305px" Height="25px" TabIndex="4"
                                CssClass="tb6" AutoPostBack="false">
                                 <asp:ListItem Value="0">Select</asp:ListItem>
                              <asp:ListItem Value="Rabi202425">Rabi 2024-25</asp:ListItem>
                              <asp:ListItem Value="Kharif202425">Kharif 2024-25</asp:ListItem>                          
                            </asp:DropDownList>
                        </td>
                   <td align="left">
                            <asp:Label ID="Label2" runat="server" Text="Commodity" ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                             <%--<asp:ListBox runat="server" ID="Godownchk"  ClientIDMode="Static" Class="form-control" SelectionMode="Multiple" ></asp:ListBox>--%>
                            <asp:DropDownList ID="ddlcommodity" runat="server" Width="305px" Height="25px" TabIndex="4"
                                CssClass="tb6" AutoPostBack="true" OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged">
                             <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="22">Wheat-PSS</asp:ListItem>
                            <asp:ListItem Value="63">GRAM</asp:ListItem>
                              <asp:ListItem Value="64">LENTIL</asp:ListItem>
                              <asp:ListItem Value="33">Mustard-Sarason</asp:ListItem>
                            <asp:ListItem Value="92">Moong</asp:ListItem>
                            <asp:ListItem Value="27">Urad</asp:ListItem>
                            <asp:ListItem Value="26">Soya-Beans</asp:ListItem>
                            <asp:ListItem Value="13">Paddy-Common</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central; ">
               
                <tr>
                    <td style="text-align: left;" colspan="8">

                        <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>                              
                               <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                           
                                <asp:TemplateField HeaderText="Acceptance No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAN" runat="server" Text='<%# Eval("Acceptance_No") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="WHR Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWHR_Id" runat="server" Text='<%# Eval("WHR_Id") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acceptance Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAD" runat="server" Text='<%# Eval("Acceptance_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="WHR Issue Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWID" runat="server" Text='<%# Eval("WHR_Issue_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Acceptance Bags">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAB" runat="server" Text='<%# Eval("No_of_Bags") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WHR Bags">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWB" runat="server" Text='<%# Eval("TotalBags_Received") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Acceptance Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("Rec_Qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                              
                                 <asp:TemplateField HeaderText="WHR Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWQ" runat="server" Text='<%# Eval("Total_Qty_Received") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
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
         <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
        <script src="../../JS/table2excel.js"></script>
        <script src="../../assets/New/js/bootstrap-multiselect.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
                });
            });
        </script>
       
    </form>
</body>
</html>
