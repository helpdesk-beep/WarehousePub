<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="DeleteAcceptanceNoteRabi.aspx.cs" Inherits="StatePages_DeleteAcceptanceNoteRabi" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
            border-radius: 6px;
            padding: 0
        }

            .modalPopup .header {
                background-color: #2FBDF1;
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

        }
    </style>
    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
    </script>
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
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Delete Acceptance" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>


                                        <tr>
                                            <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp;&nbsp;&nbsp;&nbsp  District : &nbsp;&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                                                Height="25px" Width="168px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>

                                                &nbsp;&nbsp;&nbsp;&nbsp  Branch : &nbsp;&nbsp;<asp:DropDownList ID="ddlBranch" runat="server"
                                                    Height="25px" Width="168px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                </asp:DropDownList>


                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>

                                        <tr>
                                            <td colspan="4" valign="top" align="center">

                                                <asp:GridView ID="Depositor_Gridview" runat="server" DataKeyNames="Acpt_FCIRO_No" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" OnSelectedIndexChanged="Depositor_Gridview_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="FCIRO No">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" Text="" OnClick="Edit">
                                                                    <%# Eval("Acpt_FCIRO_No") %>
                                                                </asp:LinkButton>
                                                                <asp:HiddenField ID="hdnAcpt_FCIRO_No" runat="server" Value='<%# Eval("Acpt_FCIRO_No") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="Acpt_FCIRO_No" HeaderText="FCIRO No" ReadOnly="True" SortExpression="Acpt_FCIRO_No" />--%>
                                                        <asp:BoundField DataField="Acpt_FCIRO_Date" HeaderText="FCIRO Date" ReadOnly="True" SortExpression="Acpt_FCIRO_Date" />
                                                        <asp:BoundField DataField="Bags" HeaderText="Receive Bags" ReadOnly="True" SortExpression="Bags" />
                                                        <asp:BoundField DataField="QtyWeight" HeaderText="Receive Qty Weight" ReadOnly="True" SortExpression="QtyWeight" />
                                                        <%--<asp:CommandField SelectText="Delete" HeaderText="Delete" ShowSelectButton="True">
                                                            <ControlStyle Font-Bold="True" ForeColor="Red" />
                                                        </asp:CommandField>--%>
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
                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
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
                <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 80%; overflow: scroll; height: 90%">
                    <asp:Label Font-Bold="true" ID="Label3" runat="server" Text="Godown Details"></asp:Label>
                    <br />
                    <table align="center">
                        <tr>
                            <td colspan="4" valign="top" align="center">

                                <asp:GridView ID="GridView1" runat="server" DataKeyNames="Acceptance_No" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                    CellSpacing="2" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Delete" runat="server" />
                                                <asp:HiddenField ID="hdnAcceptance_No" runat="server" Value='<%#Eval("Acceptance_No") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                Width="40px" />
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <ControlStyle Width="15px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Acpt_FCIRO_No" HeaderText="FCIRO No" ReadOnly="True" SortExpression="Acpt_FCIRO_No" />
                                        <asp:BoundField DataField="WhrNo" HeaderText="Whr No" ReadOnly="True" SortExpression="WhrNo" />
                                        <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No" ReadOnly="True" SortExpression="Acceptance_No" />
                                        <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" ReadOnly="True" SortExpression="Acceptance_Date" />
                                        <asp:BoundField DataField="Depositor_Form_No" HeaderText="Depositor Form No" ReadOnly="True" SortExpression="Depositor_Form_No" />
                                        <asp:BoundField DataField="TC_Number" HeaderText="TC Number" ReadOnly="True" SortExpression="TC_Number" />
                                        <asp:BoundField DataField="Truck_Number" HeaderText="Truck Number" ReadOnly="True" SortExpression="Truck_Number" />

                                        <asp:BoundField DataField="Bags" HeaderText="Receive Bags" ReadOnly="True" SortExpression="Bags" />
                                        <asp:BoundField DataField="QtyWeight" HeaderText="Receive Qty Weight" ReadOnly="True" SortExpression="QtyWeight" />
                                        <%--<asp:TemplateField ItemStyle-Width="30px" HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" Text="Edit" OnClick="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:CommandField SelectText="Delete" HeaderText="Delete" ShowSelectButton="True">
                                            <ControlStyle Font-Bold="True" ForeColor="Red" />
                                        </asp:CommandField>--%>
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
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Delete" CssClass="BTNBLUE" OnClick="Delete" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button button2" OnClientClick="return Hidepopup()" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                <cc1:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
                    PopupControlID="pnlAddEdit" TargetControlID="lnkFake"
                    BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
            </div>
        </center>
    </fieldset>
</asp:Content>
