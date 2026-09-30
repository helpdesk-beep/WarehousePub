<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/Accounting/NCCF_StorageBill_BO_Approval.aspx.cs" Inherits="Accounting_NCCF_StorageBill_BO_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #popupwin {
            position: fixed;
            top: 0;
            left: 0;
            width: 90%;
            height: 100%;
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
            height: 500px;
            margin: 0 auto;
            position: fixed;
            z-index: 101;
        }

        .pop {
            min-width: 800px;
            width: 700px;
            min-height: 150px;
            margin: 0px auto;
            background: #f3f3f3;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 2px 5px #000;
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
                left: 22px;
                position: relative;
                top: -20px;
                width: 35px;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy; background-color: white;">
        <center>
            <div>
                <table width="100%">
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="4" align="center">
                            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                Text="NCCF Storage Bill BO Approval - Online Submission To RM"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvBOBillApp" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="Bill_Number"
                                Width="100%" Font-Names="Arial"
                                BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                Font-Size="11px" BorderColor="#CCCCCC"
                                OnSelectedIndexChanged="gvBOBillApp_SelectedIndexChanged">
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:CommandField ButtonType="Button" HeaderStyle-Height="10%" ItemStyle-Width="10%" HeaderText="Submit Bill" ShowHeader="True"
                                        ShowSelectButton="True" SelectText="Submit To RM" ControlStyle-CssClass="BTNBLUE" />
                                    <asp:BoundField DataField="Bill_Number" ItemStyle-Width="10%" HeaderText="Bill Number"></asp:BoundField>
                                    <asp:BoundField DataField="Commodity_Name" ItemStyle-Width="7%" HeaderText="Commodity Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Crop_Year" ItemStyle-Width="7%" HeaderText="Crop Year" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Financial_Year" ItemStyle-Width="7%" HeaderText="Financial Year" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Month" ItemStyle-Width="7%" HeaderText="Month" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Net_Amount" ItemStyle-Width="7%" HeaderText="Total Bill Amount" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>

                                <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                            </asp:GridView>

                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>
