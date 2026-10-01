<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/ViewLicenceNo.aspx.cs" Inherits="StatePages_ViewLicenceNo" Title="Licence Update" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--Drop Down Filter--%>
<script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
<link href="../assets/New/css/select2.min.css" rel="stylesheet" />
<script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=ddldst2]").select2();
    });
</script>
<script type="text/javascript">
    $(function () {
        $("[id*=ddlWDRADst]").select2();
    });
</script>
<%----End------%>

    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

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
    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
    </script>
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
         <style type="text/css">
     .select2-dropdown {
         z-index: 9999999 !important;
     }
 </style>
      <fieldset style="width: 100%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Licence Update For Inspection" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px; padding-left: 212px;" colspan="2">
                                                <asp:Button ID="lnkBtnEdit" runat="server" style="color: white;background: #0da9d0;padding: 10px;border: 0;border-radius: 5px;" Text="NON WDRA" 
                                                    OnClick="Display"></asp:Button>
                                            </td>
                                            <td style="height: 5px" colspan="6">
                                                <asp:Button ID="lnkwdra" runat="server" Text="WDRA"  style="color: white;background: #0da9d0;padding: 10px;border: 0;border-radius: 5px;"
                                                    OnClick="Display2"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp;&nbsp;&nbsp;&nbsp  Licence Number : &nbsp;&nbsp;
                                                 <asp:TextBox ID="txtLicenceNo" runat="server" AutoPostBack="false" Height="25px" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 50px; font-size: 14px" colspan="4" align="center">
                                                <asp:Button class="button button1" ID="btnshow" Style="width: 100px" runat="server"
                                                    Text="View Details" Height="40px" OnClick="btnshow_Click"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>
                                        <tbody id="grdshow" runat="server" visible="false">
                                            <tr style="background-color: #0094ff; height: 25px">
                                                <td colspan="8" align="center">
                                                    <asp:Label ID="lblGodownTxndtls" runat="server" Text="NON WDRA" Font-Bold="true"
                                                        Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" valign="top" align="center">

                                                    <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                        BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                        CellSpacing="2">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="District">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblDistrict" Width="100%" Text='<%# Eval("District")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Warehouse Name">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblWHN" Width="100%" Text='<%# Eval("Warehouse_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Warehouse Man Name">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblWHMN" Width="100%" Text='<%# Eval("Warehouse_Man_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Licensed Capacity (MT)">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblLCMT" Width="100%" Text='<%# Eval("Licensed_Capacity_MT")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="License Code">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblLicense_Code" Width="100%" Text='<%# Eval("License_Code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Application_Code">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblAC" Width="100%" Text='<%# Eval("Application_Code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Issuance Date">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblIssuance_Date" Width="100%" Text='<%# Eval("Issuance_Date")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valid Till">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblValid_Till" Width="100%" Text='<%# Eval("Valid_Till")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </tbody>

                                        <tbody id="grdwdra" runat="server" visible="false">
                                            <tr style="background-color: #008CBA; height: 25px">
                                                <td colspan="8" align="center">
                                                    <asp:Label ID="Label20" runat="server" Text="WDRA" Font-Bold="true"
                                                        Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" valign="top" align="center">
                                                    <asp:GridView ID="WDRA_GridView" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                        BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                        CellSpacing="2">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="District">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblDistrict" Width="100%" Text='<%# Eval("District")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Warehouse Name">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblWHN" Width="100%" Text='<%# Eval("WarehouseName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Warehouse Man Name">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblWHMN" Width="100%" Text='<%# Eval("WarehousemanName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Licensed Capacity (MT)">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblLCMT" Width="100%" Text='<%# Eval("Licensed_Capacity_MT")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="License Code">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblLicense_Code" Width="100%" Text='<%# Eval("License_Code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mobile No">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblAC" Width="100%" Text='<%# Eval("Mobile_No")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Issue Date">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblIssuance_Date" Width="100%" Text='<%# Eval("Issue_date")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valid date">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblValid_Till" Width="100%" Text='<%# Eval("Valid_date")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>


                                </div>
                            </center>

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>

                    </tr>
                </table>
                <asp:Panel ID="pnllogin" class="popup" runat="server">
                    <div class="pop" style="background-color: white; min-height: 300PX; max-height: 500px; width: 1000px; border: #008CBA; border-style: solid; border-width: 10px;">
                        <%-- <div class="col-sm-12 col-md-12 col-xs-12">--%>
                        <%-- <div id="div1" runat="server" visible="true" style="width: 100%;">
                            <h3>WDRA or non-WDRA wharehosue Licence Entry Select</h3>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="false"
                                class="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                <asp:ListItem Value="0">-----Select------</asp:ListItem>
                                <asp:ListItem Value="1">Non-WDRA</asp:ListItem>
                                <asp:ListItem Value="2">WDRA</asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>

                        <div id="divNewInsp" runat="server" visible="false" style="width: 100%;">
                            <div>
                                <h3>Non WDRA Entry Form</h3>
                            </div>
                            <table cellpadding="0" cellspacing="0" style="width: 100%; border: 1px solid black;">
                                <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label3" runat="server" Text="District : "></asp:Label>

                                        <br />
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:DropDownList ID="ddldst2" runat="server" AutoPostBack="false"
                                            class="form-control">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label8" runat="server" Text="Application Code : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtApplicationCode" runat="server"
                                            class="form-control"></asp:TextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label2" runat="server" Text="Warehouse Name : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="lblgodownname" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label7" runat="server" Text="Warehouse Man Name : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtWMN" runat="server"
                                            class="form-control"></asp:TextBox>

                                    </td>

                                </tr>
                                <tr id="trmobtxt" runat="server" visible="true">
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label1" runat="server" Text="License Code : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtLicenseCode" runat="server"
                                            class="form-control"></asp:TextBox>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label6" runat="server" Text="Licensed Capacity (MT) : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtLCMT" runat="server"
                                            class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label4" runat="server" Text="Issuance Date : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtIssuDate" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label5" runat="server" Text="Valid Till : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtValidTill" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>

                                </tr>

                                <tr>
                                    <td style="height: 5px" colspan="4"></td>
                                </tr>
                                <tr id="trbtnhide" runat="server" visible="true">

                                    <td align="Right">
                                        <asp:Button class="button button1" ID="btnAddCompany" Style="width: 100px" runat="server"
                                            Text="Save" Height="29px" OnClick="btnAddCompany_Click"></asp:Button>&nbsp&nbsp&nbsp&nbsp
                                    </td>
                                    <td align="left">

                                        <asp:Button class="button button2" ID="btnGenerateBill" Style="width: 100px" runat="server" Text="Close" Height="29px"></asp:Button></td>
                                </tr>

                            </table>
                            <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

                        </div>


                        <div id="divWDRA" runat="server" visible="false" style="width: 100%;">
                            <div>
                                <h3>WDRA Entry Form</h3>
                            </div>
                            <table cellpadding="0" cellspacing="0" style="width: 100%; border: 1px solid black;">
                                <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label9" runat="server" Text="District : "></asp:Label>

                                        <br />
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:DropDownList ID="ddlWDRADst" runat="server" AutoPostBack="false"
                                            class="form-control">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label19" runat="server" Text="Mobile No. : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtwdramobileno" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>


                                </tr>
                                <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label11" runat="server" Text="Warehouse Name : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtwdrawhn" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label13" runat="server" Text="Warehouse Man Name : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtwdrawhmn" runat="server"
                                            class="form-control"></asp:TextBox>

                                    </td>

                                </tr>
                                <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label10" runat="server" Text="Name and Address : "></asp:Label>
                                    </td>
                                    <td colspan="4" style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtwdraapplicationcode" runat="server"
                                            class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="tr1" runat="server" visible="true">
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label14" runat="server" Text="License Code : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtwdralicencecode" runat="server"
                                            class="form-control"></asp:TextBox>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label15" runat="server" Text="Licensed Capacity (MT) : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtwdralicencecapacity" runat="server"
                                            class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label16" runat="server" Text="Issuance Date : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtwdraissuedate" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label17" runat="server" Text="Valid Till : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtwdravaliddate" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>

                                </tr>

                                <tr>
                                    <td style="height: 5px" colspan="4"></td>
                                </tr>
                                <tr id="tr2" runat="server" visible="true">

                                    <td align="Right">
                                        <asp:Button class="button button1" ID="btnwdra" Style="width: 100px" runat="server"
                                            Text="Save" Height="29px" OnClick="btnwdra_Click"></asp:Button>&nbsp&nbsp&nbsp&nbsp
                                    </td>
                                    <td align="left">

                                        <asp:Button class="button button2" ID="Button2" Style="width: 100px" runat="server" Text="Close" Height="29px"></asp:Button></td>
                                </tr>

                            </table>
                            <asp:Label ID="Label18" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

                        </div>


                        <%--------End Of Third Section -------------%>
                        <%-- </div>--%>
                    </div>
                    <img alt="New" src="images/new6.gif" id="new" runat="server" />

                </asp:Panel>
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin">
                </asp:ModalPopupExtender>
            </div>
        </center>
    </fieldset>
</asp:Content>

