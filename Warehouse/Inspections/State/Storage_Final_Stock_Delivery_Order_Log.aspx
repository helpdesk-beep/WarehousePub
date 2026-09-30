<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="Storage_Final_Stock_Delivery_Order_Log.aspx.cs" Inherits="Inspections_State_Storage_Final_Stock_Delivery_Order_Log" Title="Update WHR Depositer Name" %>


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
            filter:alpha(opacity=65);
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

    <div>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td align="center" valign="top">

                    <center>
                        <div>
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr style="background-color: #0bb6e6; height: 25px">
                                    <td colspan="8" align="center">
                                        <asp:Label ID="lblGodownMaster" runat="server" Text="Update WHR Depositer Name" Font-Bold="true"
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

                                        

                                                &nbsp;&nbsp;&nbsp;&nbsp  Godown : &nbsp;&nbsp;
                                                <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="false" class="form-control">
                                                </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 200px">WHR No.:-
                                                <asp:Button ID="Button1" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                                                    CssClass="BTNBLUE" OnClick="btnSubmit_Click" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td colspan="8" valign="top" align="center">
                                        <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="true" Width="100%" BackColor="White"
                                            BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                            CellSpacing="2">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
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

    </div>

</asp:Content>

