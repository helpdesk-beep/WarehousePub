<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Inspections/State/Branch_Manager_Detail.aspx.cs" Inherits="Inspections_State_Branch_Manager_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=DropDownList1]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlBranch]").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                                                <td colspan="8" align="center">
                                                    <asp:Label ID="lblGodownMaster" runat="server" Text="Branch Manager details" Font-Bold="true"
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="8"></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 50px;" colspan="8" align="center">&nbsp;&nbsp;&nbsp;&nbsp  District : &nbsp;&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                                                    Height="25px" Width="168px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                </asp:DropDownList>

                                                    &nbsp;&nbsp;&nbsp;&nbsp  Branch : &nbsp;&nbsp;<asp:DropDownList ID="ddlBranch" runat="server"
                                                        Height="25px" Width="168px" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8" valign="top" align="center">
                                                    <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                        BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                        CellSpacing="2">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="District Name">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblDistrictName" Width="100%" Text='<%# Eval("DistrictName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Branch Name">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblDepotName" Width="100%" Text='<%# Eval("DepotName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Branch Manager Name">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblNodalOfficeName" Width="100%" Text='<%# Eval("NodalOfficeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Branch Manager Mobile No">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblNodalOfficerMobile" Width="100%" Text='<%# Eval("NodalOfficerMobile")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Operator Name">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblOperatorName" Width="100%" Text='<%# Eval("OperatorName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Operator Mobile No">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblOperatorMobileNo" Width="100%" Text='<%# Eval("OperatorMobileNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Branch Manager Email">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblNodalOfficerEmail" Width="100%" Text='<%# Eval("NodalOfficerEmail")%>'></asp:Label>
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
                                        </table>
                                    </div>
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px" colspan="4"></td>
                        </tr>
                    </table>
                    <%-- <asp:Panel ID="pnllogin" class="popup" runat="server">
                        <div class="pop" style="background-color: white; min-height: 300PX; max-height: 500px; width: 1000px; border: #008CBA; border-style: solid; border-width: 10px;">
                            <div id="divNewInsp" runat="server" visible="true" style="width: 100%;">
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
                                            <asp:Label ID="Label1" runat="server" Text="Godown ID : "></asp:Label>
                                            <asp:TextBox ID="txtGdwnID" runat="server" ReadOnly="true"
                                                Width="150px" Height="20px"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                      &nbsp; Depositor Name &nbsp;
                                                    <asp:DropDownList ID="ddlDepositor" runat="server" Width="155px" Height="25px" AutoPostBack="false">
                                                    </asp:DropDownList>
                                            &nbsp;
                                                <br />
                                        </td>
                                    </tr>
                                    <tr id="tr2" runat="server" visible="true">
                                        <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp; WHR No. &nbsp;
                                                    <asp:TextBox ID="txtwhrno" runat="server"
                                                        Width="300px" Height="20px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px" colspan="4"></td>
                                    </tr>
                                </table>
                                <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                            </div>
                        </div>
                        <img alt="New" src="images/new6.gif" id="new" runat="server" />
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin">
                    </asp:ModalPopupExtender>--%>
                </div>
            </center>
        </fieldset>
    </form>
</body>
</html>
