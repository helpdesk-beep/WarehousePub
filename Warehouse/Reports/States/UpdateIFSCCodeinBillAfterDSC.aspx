<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateIFSCCodeinBillAfterDSC.aspx.cs" Inherits="Reports_States_Payment_BillsFromCSMStoMPWLC_Status_Rept_Region_Wise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title style="color: white;">Payment Received From MPSCSC</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.css" rel="stylesheet" />
    <script src="scripts/dataTables.bootstrap4.min.js"></script>
    <script src="scripts/jquery.dataTables.min.js"></script>

    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
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
            Width: 90px;
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
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #D69758;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>
    <style type="text/css">
        #popupwin {
            position: fixed;
            top: 0;
            left: 0;
            width: 90%;
            height: 90%;
            background-color: #000;
            filter: alpha(opacity=65);
            -moz-opacity: 0.7;
            display: none;
            opacity: 0.7;
            z-index: 100;
        }

        .pop a {
            text-decoration: none;
        }

        .popup {
            width: 100%;
            height: 98%;
            margin: 0 auto;
            position: fixed;
            z-index: 101;
            padding-left: 90px;
        }

        .pop {
            /*min-width: 900px;*/
            width: 80%;
            min-height: 150px;
            margin: 0px auto;
            background: #FFFFFF;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 5px 10px #000;
            /*margin-top:200px;*/
        }

            .pop p {
                color: #555555;
                text-align: justify;
                font-size: medium;
            }

                .pop p a {
                    color: #d91900;
                }

            .pop .x {
                float: right;
                height: 35px;
                /*left: 22px;*/
                position: relative;
                /*top: -20px;*/
                width: 35px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <div>
            <div style="text-align: center; font-size: large;">
                <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
                <h4 class="header">received Payment from MPSCSC through NEFT Payment System</h4>
            </div>
            <div style="text-align: center; font-size: large;">

                <asp:Label ID="Label1" runat="server" Text="Bill Number"></asp:Label>
                <asp:TextBox ID="txtbillnumber" runat="server"></asp:TextBox>

                &nbsp;&nbsp;
                <asp:Button ID="tbnview" runat="server" Text="View" CssClass="button button2" Width="165px" Height="50px" OnClick="tbnview_Click" />

            </div>
            <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

                <tr>
                    <td style="text-align: left;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowDataBound="GridView1_RowDataBound">
                            <%--<Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Regionnm" HeaderText="Region Name" />
                                <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                                <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number" />
                                <asp:BoundField DataField="Ref_Bill_No" HeaderText="Ref Bill No" />
                                <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" />
                                <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" />
                                <asp:BoundField DataField="Month_Name" HeaderText="Month" />
                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" />
                                <asp:BoundField DataField="Account_No" HeaderText="Account_No" />
                                <asp:BoundField DataField="IFSC_Code" HeaderText="IFSC_Code" />
                                <asp:BoundField DataField="Re_Push" HeaderText="Re_Push" />
                            </Columns>--%>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Regionnm">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRegionnm" Width="100%" Text='<%# Eval("Regionnm")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District_Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistrict_Name" Width="100%" Text='<%# Eval("District_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DepotName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDepotName" Width="100%" Text='<%# Eval("DepotName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill_Number">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBill_Number" Width="100%" Text='<%# Eval("Bill_Number")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref_Bill_No">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRef_Bill_No" Width="100%" Text='<%# Eval("Ref_Bill_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown_ID">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("Godown_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown_Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop_Year">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCrop_Year" Width="100%" Text='<%# Eval("Crop_Year")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Financial_Year">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFinancial_Year" Width="100%" Text='<%# Eval("Financial_Year")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Month_Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMonth_Name" Width="100%" Text='<%# Eval("Month_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commodity_Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommodity_Name" Width="100%" Text='<%# Eval("Commodity_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account_No">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAccount_No" Width="100%" Text='<%# Eval("Account_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IFSC_Code">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblIFSC_Code" Width="100%" Text='<%# Eval("IFSC_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Re_Push">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRe_Push" Width="100%" Text='<%# Eval("Re_Push")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Update Bill Details">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Update" CssClass="btn btn-info"
                                            OnClick="Display"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ControlStyle Font-Bold="True" ForeColor="Red" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="Black" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
              <asp:Panel ID="pnllogin" class="popup" runat="server">
                    <div class="pop" style="background-color: white; min-height: 300PX; max-height: 500px; width: 1000px; border: #008CBA; border-style: solid; border-width: 10px;">
                        <%-- <div class="col-sm-12 col-md-12 col-xs-12">--%>


                        <div id="divNewInsp" runat="server" visible="false" style="width: 100%;">
                            <h3 style="color:red;">कृप्या सभी शाखा प्रबंधक यह जानकारी बहुत ही ध्यान से सबमिट  करे ,एवं जो JVS की Registration एआईडी दी हुई हें उसे ध्यान से सिलेक्ट करे | </h3>
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 70px; font-size: 14px" colspan="4" align="center">
                                        <asp:Label ID="Label2" runat="server" Text="Godown Name : "></asp:Label>
                                        <asp:TextBox ID="lblgodownname" runat="server" Width="300px" Height="20px"></asp:TextBox>
                                      
                                        <br />
                                    </td>
                                </tr>
                                <tr id="trmobtxt" runat="server" visible="true">
                                    <td style="height: 70px; font-size: 14px" colspan="4" align="center">
                                        <asp:Label ID="Label3" runat="server" Text="Godown ID : "></asp:Label>
                                        <asp:TextBox ID="txtGdwnID" runat="server" ReadOnly="true"
                                            Width="150px" Height="20px"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                      &nbsp; Account Number &nbsp;
                                                    <asp:TextBox ID="txtAccountno" runat="server"
                                                        Width="200px" Height="20px"></asp:TextBox>
                                        &nbsp;
                                                <br />
                                    </td>
                                </tr>

                                <tr id="tr2" runat="server" visible="true">
                                    <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp; IFSC Code &nbsp;
                                                    <asp:TextBox ID="txtIFSC" runat="server"
                                                        Width="200px" Height="20px"></asp:TextBox>
                                        &nbsp;   
                                                   Re-Push Status   &nbsp;&nbsp;
                                                    <asp:TextBox ID="txtRepush" runat="server"
                                                        Width="100px" Height="20px"></asp:TextBox>
                                        
                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 5px" colspan="4"></td>
                                </tr>
                                <tr id="trbtnhide" runat="server" visible="true">

                                    <td align="Right">
                                        <asp:Button class="button button1" ID="btnAddCompany" Style="width: 100px" runat="server"
                                            Text="Update" Height="29px" OnClick="btnAddCompany_Click"></asp:Button>&nbsp&nbsp&nbsp&nbsp
                                    </td>
                                    <td align="left">

                                        <asp:Button class="button button2" ID="btnGenerateBill" Style="width: 100px" runat="server" Text="Close" Height="29px"></asp:Button></td>
                                </tr>

                            </table>
                            <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

                        </div>


                        <%--------End Of Third Section -------------%>
                        <%-- </div>--%>
                    </div>
                    <img alt="New" src="images/new6.gif" id="new" runat="server" />

                </asp:Panel>
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin">
                </asp:ModalPopupExtender>
        </div>
    </form>
    <!--Java Script -->

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
</body>
</html>
