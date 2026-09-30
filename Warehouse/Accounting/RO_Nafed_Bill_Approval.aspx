<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="~/Accounting/RO_Nafed_Bill_Approval.aspx.cs" Inherits="Accounting_RO_Nafed_Bill_Approval" %>

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
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table width="100%">
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="4" align="center">
                            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                Text="ब्रांच मैनेजर के द्वारा NAFED के ऑनलाइन बिलो का अवलोकन करने के पश्चयात ही NAFED और HOMPWLC को सबमिट करे"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvBOBillApp" runat="server"
                                            CellPadding="5"
                                            CellSpacing="10" OnSelectedIndexChanged="gvBOBillApp_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ButtonType="Button" HeaderText="Submit TO NAFED/HOMPWLC" ShowHeader="True"
                                                    ShowSelectButton="True" SelectText="Submit TO NAFED/HOMPWLC" ControlStyle-CssClass="BTNBLUE"/>
                                            </Columns>
                                            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                Height="20px" Font-Size="10pt" />
                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                </table>
            </div>
        </center>
    </fieldset>  
   
</asp:Content>

