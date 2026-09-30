<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="GetGodownDetails_DistrictBranchWise.aspx.cs" Inherits="StatePages_GetGodownDetails_DistrictBranchWise" Title="Godown Details" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <fieldset style="width: 90%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
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
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="District and Branch wise Godown Details" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="row" style="margin-top: 15px">
                                        <div class="col-md-2" style="margin-top: 10px">
                                            <div class="form-group">
                                                <asp:Label ID="lblDistrict" Font-Bold="true" runat="server" ForeColor="Navy">District:</asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true"
                                                        Height="25px" Width="168px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                        <asp:Label ID="lblBranch" Font-Bold="true" runat="server" ForeColor="Navy">Branch:</asp:Label>

                                                &nbsp;
                                                <asp:DropDownList ID="ddlBranch" runat="server"
                                                    Height="25px" Width="168px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                            <asp:Label ID="lblGodownType" Font-Bold="true" runat="server" ForeColor="Navy">Godown Type:</asp:Label>

                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:DropDownList CssClass="form-control select2" ID="ddlGodownType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="row" style="margin-top: 15px">

                                    <div class="col-md-2">
                                                        <tr>
                                                            <td colspan="8" style="text-align: center;">
                                                                <asp:Button ID="btnSearch" CssClass="btn btn-success btn-sm" ValidationGroup="A"
                                                                    runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                            </td>
                                                        </tr>
                                    </div>
                                </div>
                            </center>
                        </td>
                    </tr>

                    <tr>
                        <td style="height: 5px" colspan="4"></td>
                    </tr>

                    <tr>
                        <td colspan="4" valign="top" align="center">
                            <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" ShowFooter="true"
                                CellSpacing="2" OnRowDataBound="Depositor_Gridview_OnRowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GodownID">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("GodownID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GodownName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("GodownName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HiredType">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblHired_Type" Width="100%" Text='<%# Eval("HiredType")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Storage Type">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblStorage_Type" Width="100%" Text='<%# Eval("StorageType")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scientific Capacity In MT">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Width="100%" Text='<%# Eval("ScientificCapacityInMT")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max Capacity In MT">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGodown_Capacity" Width="100%" Text='<%# Eval("MaxCapacityInMT")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Closing_Balance">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblClosing_Balance" Width="100%" Text='<%# Eval("Closing_Balance")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="LicNum">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblLicNum" Width="100%" Text='<%# Eval("LicNum")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LicDate">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblLicDate" Width="100%" Text='<%# Eval("LicDate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Update Lincance Details">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Update" CssClass="btn btn-info"
                                                                    OnClick="Display"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ControlStyle Font-Bold="True" ForeColor="Red" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>--%>
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


            </div>
        </center>
    </fieldset>
</asp:Content>

