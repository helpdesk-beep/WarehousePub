<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" Async="true" AutoEventWireup="true" CodeFile="Add_Godown_in_AePDS.aspx.cs" Inherits="Masters_Add_Godown_in_AePDS" Title="Add Godown in AePDS" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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
            Width: 210px;
            height: 50px;
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
                height: 230px;
                /*border: 2px dashed red;*/
            }
    </style>
    <fieldset style="width: 100%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
        <center>
            <div>
                <h3 style="color: red;">यदि AePDS में गोडाउन नहीं दिख रह हे तो इस लिंक के माध्यम सें गोडाउन AePDS में Add कर सकते हैं </h3>

                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="lbldist" runat="server" Text="Enter Godown ID" Font-Size="10pt" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtgodownid" runat="server" Height="25px" Width="200px" CssClass="tb6"></asp:TextBox>
                        </td>
                        <td>
                           <%-- <asp:Label ID="Label1" runat="server" Text="Enter Godown ID" Font-Size="10pt" Font-Bold="true"></asp:Label>--%>
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="View Godown Details" CssClass="button button2" OnClick="Button2_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td id="showgrid" align="center" valign="top" runat="server" visible="true" colspan="8">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                       
                                            <tr>
                                                <td colspan="4" valign="top" align="center">

                                                    <asp:GridView ID="godown_GridView" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False"
                                                        CellPadding="2" Width="100%" AllowPaging="True" AllowSorting="True"
                                                        OnSelectedIndexChanged="godown_GridView_SelectedIndexChanged"
                                                        OnPageIndexChanging="godown_GridView_PageIndexChanging" PageSize="20"
                                                        Font-Size="9pt">
                                                        <Columns>
                                                            <%--<asp:CommandField ShowSelectButton="True" HeaderText="Verify"
                                                                ItemStyle-ForeColor="green">
                                                                <ItemStyle ForeColor="green"></ItemStyle>
                                                            </asp:CommandField>--%>

                                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Verify">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Green" Text="" OnClick="Edit">
                                                                    Verify
                                                                    </asp:LinkButton>
                                                                    <%-- <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="Red" Text="" OnClick="Delete">
                                                                    Delete
                                                                    </asp:LinkButton>--%>
                                                                    <asp:HiddenField ID="hdngodownname" runat="server" Value='<%# Eval("Godown_Name") %>' />
                                                                    <asp:HiddenField ID="hdnbranchid" runat="server" Value='<%# Eval("BranchID") %>' />
                                                                    <asp:HiddenField ID="hdnGodown_ID" runat="server" Value='<%# Eval("Godown_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.N.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                                            <asp:BoundField DataField="District_Id" HeaderText="District Id" />
                                                            <asp:BoundField DataField="Branch" HeaderText="Branch" />
                                                            <asp:BoundField DataField="BranchID" HeaderText="Branch ID" />
                                                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                                                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                                            <asp:BoundField DataField="Godown_Capacity" HeaderText="Max Capacity" />
                                                            <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Scientific Capacity" />
                                                            <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />
                                                            <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" />
                                                            <asp:BoundField DataField="Licence_No" HeaderText="Licence No" />
                                                            <asp:BoundField DataField="Licence_Validity" HeaderText="Licence Validity" />
                                                            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date Time" />
                                                            <asp:BoundField DataField="gdnStatus" HeaderText="Godown Status" />
                                                            <asp:BoundField DataField="Godown_ID">
                                                                <HeaderStyle Font-Size="0pt" />
                                                                <ItemStyle Font-Size="0pt" ForeColor="White" />
                                                            </asp:BoundField>
                                                            
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

