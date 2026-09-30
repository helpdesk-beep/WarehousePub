<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="DeleteAcceptanceNoKharif2021.aspx.cs" Inherits="StatePages_DeleteAcceptanceNoKharif2021" %>

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
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Primarry Key Constraint Duplicate Key Value की एरर आने पर यहाँ से acceptance डिलीट करे(Kharif)|" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>


                                        <tr>
                                            <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp;&nbsp;&nbsp;&nbsp  Acceptance Number : &nbsp;&nbsp;<asp:TextBox ID="txtacceptanceno" runat="server" AutoPostBack="true"
                                                Height="25px" Width="168px" OnTextChanged="txtacceptanceno_TextChanged" >
                                            </asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>

                                        <tr>
                                            <td colspan="4" valign="top" align="center">

                                               <asp:GridView ID="gdnacceptance" runat="server" DataKeyNames="Acceptance_No" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                    CellSpacing="2" OnRowCommand="gdnacceptance_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="क्रमांक">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                    <asp:HiddenField ID="hdnGodownId" runat="server" Value='<%# Bind("Godown") %>' />
                                                    <asp:HiddenField ID="hdnAcceptance_No" runat="server" Value='<%# Bind("Acceptance_No") %>' />
                                                    <asp:HiddenField ID="hdnTC_Number" runat="server" Value='<%# Bind("TC_Number") %>' />
                                                    <asp:HiddenField ID="hdnTruck_Number" runat="server" Value='<%# Bind("Truck_Number") %>' />
                                                    <asp:HiddenField ID="hdnCommodity_Id" runat="server" Value='<%# Bind("Commodity_Id") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="1%" />
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                        <asp:BoundField DataField="DepotName" HeaderText="Branch Name" ReadOnly="True" SortExpression="DepotName" />
                                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ReadOnly="True" SortExpression="Godown_Name" />
                                        <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No" ReadOnly="True" SortExpression="Acceptance_No" />
                                        <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" ReadOnly="True" SortExpression="Acceptance_Date" />
                                        <asp:BoundField DataField="Depositor_Form_No" HeaderText="Depositor Form No" ReadOnly="True" SortExpression="Depositor_Form_No" />
                                        <asp:BoundField DataField="TC_Number" HeaderText="TC Number" ReadOnly="True" SortExpression="TC_Number" />
                                        <asp:BoundField DataField="Truck_Number" HeaderText="Truck Number" ReadOnly="True" SortExpression="Truck_Number" />

                                        <asp:BoundField DataField="Bags" HeaderText="Receive Bags" ReadOnly="True" SortExpression="Bags" />
                                        <asp:BoundField DataField="QtyWeight" HeaderText="Receive Qty Weight" ReadOnly="True" SortExpression="QtyWeight" />
                                       <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnRemove" Text="Delete" runat="server" CommandName="RemoveRow" CssClass="btneditstyle" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Delete this row?');" />
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
              
            </div>
        </center>
    </fieldset>
</asp:Content>
