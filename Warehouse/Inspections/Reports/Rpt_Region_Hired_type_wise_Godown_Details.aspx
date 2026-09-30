<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Inspections/Reports/Rpt_Region_Hired_type_wise_Godown_Details.aspx.cs" Inherits="Inspections_Reports_Rpt_Region_Hired_type_wise_Godown_Details" %>

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
      <span style="font-size: 30px; font-family:bold; text-align:center;margin-left: 190px;"> District Hired Type Wise Godown Capacity and Avl. Stock Position in MT</span>
  </div>
  <div><span style="WIDTH: 52.68mm; HEIGHT: 6.35mm;">Date:-</span> <asp:Label ID="labelName" runat="server"></asp:Label> 
      <br />
      <br />
      <br />
      <br />
      <br />
      <span style="font-size: 20px; font-family:bold;">Qty In M.T.</span>

  </div>
</div>
           
            <div class="container py-4">
                <div class="card">

                    <div class="card-body">
                        <asp:Button ID="btnback" runat="server" Text="Back" CssClass="button button2" OnClick="btnback_Click"  />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" Text="ExportToPDF" CssClass="button button2" OnClientClick="printGrid()" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" id="btnExport" value="Exporttoexcel" class="button button2" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<%--<asp:Button ID="btnUpdate" runat="server" CssClass="button button2" Text="Update Pending Months" Visible="false" OnClick="btnUpdate_Click" />--%>
                    </div>
                </div>

            </div>
            <div style="margin-left: 104px;">
                

            </div>
            <div class="Row" style="margin-top: 10px">
                    <div class="col-md-2" style="margin-top: 5px">
                        <label>Division Name</label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:DropDownList CssClass="form-control select2" ID="ddldivision" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2" style="margin-top: 5px">
                        <label>District Name</label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <div class="form-group">
                                <asp:DropDownList CssClass="form-control select2" ID="ddldistrict" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2" style="margin-top: 5px">
                        <label>Branch Name</label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:DropDownList CssClass="form-control select2" ID="ddlbranch" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            <table style="border: solid 5px #e3e3e8; width: 60%; vertical-align: central; margin-left:400px; ">
               <tr id="trgdnlist" runat="server" visible="true">
                        <td align="left">
                            <asp:Label ID="Label1" runat="server" Text="Godown Type" ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                            <asp:DropDownList ID="ddlstorageType" runat="server" Width="305px" Height="25px" TabIndex="4"
                                CssClass="tb6" AutoPostBack="true" OnSelectedIndexChanged="ddlstorageType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                   <td align="left">
                            <asp:Label ID="Label2" runat="server" Text="Storage Type" ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                             <%--<asp:ListBox runat="server" ID="Godownchk"  ClientIDMode="Static" Class="form-control" SelectionMode="Multiple" ></asp:ListBox>--%>
                            <asp:DropDownList ID="ddlGodownType" runat="server" Width="305px" Height="25px" TabIndex="4"
                                CssClass="tb6" AutoPostBack="true" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table id="divdivision" runat="server" visible="false" style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central; ">
                <tr>
                    <td style="text-align: left;" colspan="8">
                        <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>  
                                <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                                <asp:BoundField DataField="Region_ID" HeaderText="Region_ID" />
                                <asp:BoundField DataField="TypeofGodown" HeaderText="Type of Godown" />
                                <asp:TemplateField HeaderText="Total Godown">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalGodown") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Capacity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("GodownCapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Quantity of stock stored in warehouse">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Quantityofstockstoredinwarehouse") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="currently vacant capacity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCVC" runat="server" Text='<%# Eval("currentlyvacantcapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
             <fieldset id="divdistrict" runat="server" visible="false">
                <div class="row" style="margin-top: 15px">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                            CssClass="table-bordered table-hover GridViewScrollHeader"
                            AlternatingRowStyle-CssClass="alt"
                            OnRowDataBound="GridView2_RowDataBound"
                            OnRowCreated="GridView2_RowCreated" 
                            PagerStyle-CssClass="pgr">
                            <Columns>
                                  <asp:BoundField DataField="District_Name" HeaderText="District_Name" />
                                  <asp:BoundField DataField="District_Id" Visible="false" HeaderText="District_Id" />
                                  <asp:BoundField DataField="TypeofGodown" HeaderText="Type of Godown" />
                                  <asp:TemplateField HeaderText="Total Godown">
                                  <ItemTemplate>
                                        <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalGodown") %>'></asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Capacity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("GodownCapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Quantity of stock stored in warehouse">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Quantityofstockstoredinwarehouse") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="currently vacant capacity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCVC" runat="server" Text='<%# Eval("currentlyvacantcapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="20px" Font-Size="12pt" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                        </asp:GridView>
                    </div>
                </div>
            </fieldset>
             <fieldset id="divBranch" runat="server" visible="false">
                <div class="row" style="margin-top: 15px">
                    <div class="table-responsive">
                        <asp:GridView ID="grdbranch" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                            CssClass="table-bordered table-hover GridViewScrollHeader"
                            AlternatingRowStyle-CssClass="alt"
                            OnRowDataBound="grdbranch_RowDataBound"
                            OnRowCreated="grdbranch_RowCreated" 
                            PagerStyle-CssClass="pgr">
                            <Columns>
                                  <asp:BoundField DataField="Branch_Name" HeaderText="Branch" />
                                  <asp:BoundField DataField="BranchId" Visible="false" HeaderText="BranchId" />
                                  <asp:BoundField DataField="TypeofGodown" HeaderText="Type of Godown" />
                                  <asp:TemplateField HeaderText="Total Godown">
                                  <ItemTemplate>
                                        <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalGodown") %>'></asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Capacity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("GodownCapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Quantity of stock stored in warehouse">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Quantityofstockstoredinwarehouse") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="currently vacant capacity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCVC" runat="server" Text='<%# Eval("currentlyvacantcapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="20px" Font-Size="12pt" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                        </asp:GridView>
                    </div>
                </div>
            </fieldset>

            <fieldset id="DivGodown" runat="server" visible="false">
                <div class="row" style="margin-top: 15px">
                    <div class="table-responsive">
                        <asp:GridView ID="GrdGodown" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                            CssClass="table-bordered table-hover GridViewScrollHeader"
                            AlternatingRowStyle-CssClass="alt"
                            OnRowDataBound="GrdGodown_RowDataBound"
                            OnRowCreated="GrdGodown_RowCreated" 
                            PagerStyle-CssClass="pgr">
                            <Columns>
                                 <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                                  <asp:BoundField DataField="Godown_ID" Visible="false" HeaderText="Godown_ID" />
                                  <asp:BoundField DataField="TypeofGodown" HeaderText="Type of Godown" />
                                 <%-- <asp:TemplateField HeaderText="Total Godown">
                                  <ItemTemplate>
                                        <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalGodown") %>'></asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Godown Capacity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("GodownCapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Quantity of stock stored in warehouse">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Quantityofstockstoredinwarehouse") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="currently vacant capacity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCVC" runat="server" Text='<%# Eval("currentlyvacantcapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="20px" Font-Size="12pt" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                        </asp:GridView>
                    </div>
                </div>
            </fieldset>
        </div>
        <div>
            <asp:Label ID="lblMsg" runat="server" BackColor="Red" Font-Size="Large"></asp:Label>
        </div>
       <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js"></script>
        <script src="../../assets/New/js/bootstrap-multiselect.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
                });
            });
        </script>
     <script type="text/javascript">
         $(function () {
             $('[id*=Godownchk]').multiselect({
                 includeSelectAllOption: true,
             });
         });
     </script>
    </form>
</body>
</html>
