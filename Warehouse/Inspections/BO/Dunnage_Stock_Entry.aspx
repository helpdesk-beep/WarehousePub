<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="Dunnage_Stock_Entry.aspx.cs" Inherits="Inspections_BO_Dunnage_Stock_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.3/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.8.3/jquery-ui.js"></script>
    <script src="http://code.jquery.com/jquery-1.11.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        .modal-dialog {
            width: 1000px;
            margin: 30px auto;
        }

        .btn-info {
            color: #fff;
            background-color: #5bc0de;
            border-color: #46b8da;
        }

        .btn {
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            background-color: #D69758;
            color: #fff;
        }

        .btn-info:hover {
            color: black;
            background-color: #31b0d5;
            border-color: #269abc;
        }

        .btn.active, .btn:active {
            background-image: none;
            outline: 0;
            -webkit-box-shadow: inset 0 3px 5px rgba(0,0,0,.125);
            box-shadow: inset 0 3px 5px rgba(0,0,0,.125);
        }

        .td {
            width: 175px;
        }
    </style>
    <div runat="server" style="background-color: #FDFAF7; width: 100%;">
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;  ">
            <tr>
                <td style="width: 175px;">
                    <asp:Label ID="Label3" runat="server" Text="Godown No. : "></asp:Label>
                </td>
                <td style="width: 175px;" colspan="3">
                     <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="false" class="form-control">
                    </asp:DropDownList>
                    <%--<asp:TextBox ID="txtGodownNo" runat="server" CssClass="form-control" MaxLength="30" onkeypress="return NumberOnly(event);"></asp:TextBox>--%>
                </td>
<td style="width: 175px;">
                    <asp:Label ID="lbltod" runat="server" Text="Type of Dunnage : "></asp:Label>
                </td>
                <td style="width: 175px;">
 <asp:DropDownList ID="ddltypeofdunnage" Height="35px" Class="form-control" runat="server">
                        <asp:ListItem Value="0">-----Select------</asp:ListItem>
                        <asp:ListItem Value="1">Mats</asp:ListItem>
                        <asp:ListItem Value="2">B.P. Sheet</asp:ListItem>
                        <asp:ListItem Value="3">Blue Dunnage Sheet</asp:ListItem>
                        <asp:ListItem Value="4">Unservicable as Dunnage Sheet</asp:ListItem>
                        <asp:ListItem Value="5">Wooden crates Sheet</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>

                <td style="width: 175px;">
                    <asp:Label ID="Label11" runat="server" Text="Date (DD/MM/YY): "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                </td>
                <td style="width: 175px;">
                    <asp:Label ID="Label12" runat="server" Text="Opening Balance : "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:TextBox ID="txtOpening" runat="server" CssClass="form-control" MaxLength="30" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td style="width: 176px;">
                    <asp:Label ID="Label13" runat="server" Text="Serviceable (No. of mats in hand) : "></asp:Label>
                </td>
                <td style="width: 176px;">
                    <asp:TextBox ID="txtServiceable" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 175px;">
                    <asp:Label ID="Label33" runat="server" Text="Unserviceable (No. of mats in hand) : "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:TextBox ID="txtUnserviceable" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label59" runat="server" Text="No. of mats Quantity Received  : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQtyRec" runat="server" CssClass="form-control" MaxLength="4" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td>No. of mats size Quantity Received :
                </td>
                <td>
                    <asp:TextBox ID="txtQtySizeRec" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 175px;">
                    <asp:Label ID="Label14" runat="server" Text="No. of mats value Quantity Received : "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:TextBox ID="txtQtyValueRec" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="From Date (DD/MM/YY): "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtFromDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                </td>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="No. of mats Serviceable (Total of CI No. 3a & 4) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCIServiceable" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width: 175px;">
                    <asp:Label ID="Label17" runat="server" Text="No. of mats Unserviceable (Total of CI No. 3a & 4) : "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:TextBox ID="txtCIUnserviceable" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="No. of mats Value (Total of CI No. 3a & 4) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCIValue" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="Quantity Used (No. of mats) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQtyUsed" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width: 175px;">
                    <asp:Label ID="Label20" runat="server" Text="Quantity Used (No. of mats size) : "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:TextBox ID="txtQtyUsedSize" runat="server" CssClass="form-control" MaxLength="300" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Quantity Used (No. of mats value) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQtyUsedValue" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Floor Area : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFloorArea" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
            </tr>
            <tr>

                <td>
                    <asp:Label ID="Label4" runat="server" Text="Quantity Transferred (No of mats) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQtyTran" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Quantity Transferred (No of mats size) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQtyTranSize" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
            
                <td style="width: 175px;">
                    <asp:Label ID="Label6" runat="server" Text="Quantity Transferred (No of mats value)  : "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:TextBox ID="txtQtyTranValue" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Name of Centre which transferred : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCentreName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="No.of mats Under Use (Closing Balance) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCBUnderUse" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
            
                <td style="width: 175px;">
                    <asp:Label ID="Label9" runat="server" Text="No.of mats Serviceable (Closing Balance) : "></asp:Label>
                </td>
                <td style="width: 175px;">
                    <asp:TextBox ID="txtCBServiceable" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="No.of mats Unserviceable (Closing Balance) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCBUnserviceable" runat="server" CssClass="form-control" MaxLength="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="Remarks : "></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Height="80px" MaxLength="300" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <center>
                  <asp:Button class="btn" ID="btn_addnewoff" runat="server" Text="Submit"  OnClick="btnsaveprofile_Click"
                                    TabIndex="11" Width="150px" Height="30px" ValidationGroup="A" OnClientClick=" return validate()"></asp:Button>
                                &nbsp;
                                 <asp:Button class="btn-info" ID="btn_clear" runat="server" Text="Clear"
                                     TabIndex="11" Width="150px" Height="30px" OnClick="btn_clear_Click"></asp:Button>
                    </center>
                </td>
            </tr>
        </table>
        <div style="overflow: scroll;">
            <asp:GridView runat="server" ID="GrdOfficerPreviousInsp" OnRowCommand="GrdOfficerPreviousInsp_RowCommand"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowDataBound="GrdOfficerPreviousInsp_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                            <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("ID") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="1%" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Opening Balance">
                        <ItemTemplate>
                            <asp:Label ID="lblOpeningBalance" runat="server" Text='<%# Eval("OB_No_of_Mats_Under_Use") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Serviceable (No. of mats in hand)">
                        <ItemTemplate>
                            <asp:Label ID="lblServiceable" runat="server" Text='<%# Eval("Serviceable_Mats_in_Hand") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unserviceable (No. of mats in hand)">
                        <ItemTemplate>
                            <asp:Label ID="lblUnserviceable" runat="server" Text='<%# Eval("Unserviceable_Mats_in_Hand") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No. of mats Quantity Received">
                        <ItemTemplate>
                            <asp:Label ID="lblReceived" runat="server" Text='<%# Eval("Qty_Received_Mats") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No. of mats size Quantity Received">
                        <ItemTemplate>
                            <asp:Label ID="lblSizeReceived" runat="server" Text='<%# Eval("Qty_Received_Mats_Size") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No. of mats value Quantity Received">
                        <ItemTemplate>
                            <asp:Label ID="lblValueReceived" runat="server" Text='<%# Eval("Qty_Received_Mats_Value") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From Date">
                        <ItemTemplate>
                            <asp:Label ID="lblFrom_Date" runat="server" Text='<%# Eval("From_Date") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No. of mats Serviceable (Total of CI No. 3a & 4)">
                        <ItemTemplate>
                            <asp:Label ID="lblCI_No_of_Mats_Seal" runat="server" Text='<%# Eval("CI_No_of_Mats_Seal") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No. of mats Unserviceable (Total of CI No. 3a & 4)">
                        <ItemTemplate>
                            <asp:Label ID="lblCI_No_of_Mats_Unseal" runat="server" Text='<%# Eval("CI_No_of_Mats_Unseal") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No. of mats Value (Total of CI No. 3a & 4)">
                        <ItemTemplate>
                            <asp:Label ID="lblCI_No_of_Mats_Value" runat="server" Text='<%# Eval("CI_No_of_Mats_Value") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity Used (No. of mats)">
                        <ItemTemplate>
                            <asp:Label ID="lblQty_Used_No_of_Mats" runat="server" Text='<%# Eval("Qty_Used_No_of_Mats") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity Used (No. of mats size)">
                        <ItemTemplate>
                            <asp:Label ID="lblQty_Used_No_of_Mats_Size" runat="server" Text='<%# Eval("Qty_Used_No_of_Mats_Size") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity Used (No. of mats value)">
                        <ItemTemplate>
                            <asp:Label ID="lblQty_Used_No_of_Mats_Value" runat="server" Text='<%# Eval("Qty_Used_No_of_Mats_Value") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Floor Area">
                        <ItemTemplate>
                            <asp:Label ID="lblFloor_Area" runat="server" Text='<%# Eval("Floor_Area") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Godown No. ">
                        <ItemTemplate>
                            <asp:Label ID="lblGodown_No" runat="server" Text='<%# Eval("Godown_No") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity Transferred (No of mats)">
                        <ItemTemplate>
                            <asp:Label ID="lblTransfered_No_of_Mats" runat="server" Text='<%# Eval("Transfered_No_of_Mats") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity Transferred (No of mats size)">
                        <ItemTemplate>
                            <asp:Label ID="lblTransfered_No_of_Mats_Size" runat="server" Text='<%# Eval("Transfered_No_of_Mats_Size") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity Transferred (No of mats value)">
                        <ItemTemplate>
                            <asp:Label ID="lblTransfered_No_of_Mats_Value" runat="server" Text='<%# Eval("Transfered_No_of_Mats_Value") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name of Centre which transferred">
                        <ItemTemplate>
                            <asp:Label ID="lblName_of_Centre_to_Which_Transferred" runat="server" Text='<%# Eval("Name_of_Centre_to_Which_Transferred") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No.of mats Under Use (Closing Balance)">
                        <ItemTemplate>
                            <asp:Label ID="lblCB_No_of_Mats_Under_Use" runat="server" Text='<%# Eval("CB_No_of_Mats_Under_Use") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No.of mats Serviceable (Closing Balance)">
                        <ItemTemplate>
                            <asp:Label ID="lblCB_No_of_Mats_Serviceable" runat="server" Text='<%# Eval("CB_No_of_Mats_Serviceable") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No.of mats Unserviceable (Closing Balance)">
                        <ItemTemplate>
                            <asp:Label ID="lblCB_No_of_Mats_In_Hand_Unserviceable" runat="server" Text='<%# Eval("CB_No_of_Mats_In_Hand_Unserviceable") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit Details" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Button ID="btnfilloverallinsp" Text="Edit" runat="server" CommandName="Overallinsp" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                </Columns>

                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                    Height="20px" Font-Size="10pt" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>
        </div>


        <div>
        </div>

    </div>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript">  
    function validate() {
        if (document.getElementById("<%=txtDate.ClientID%>").value == "") {
                alert("Date can not be blank");
                document.getElementById("<%=txtDate.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtOpening.ClientID %>").value == "") {
                alert("Opening Balance can not be blank");
                document.getElementById("<%=txtOpening.ClientID %>").focus();
                return false;
            }

            if (document.getElementById("<%=txtServiceable.ClientID %>").value == "") {
                alert("Serviceable (No. of mats in hand) can not be blank");
                document.getElementById("<%=txtServiceable.ClientID %>").focus();
                return false;
            }

            if (document.getElementById("<%=txtUnserviceable.ClientID%>").value == "") {
                alert("Unserviceable (No. of mats in hand) can not be blank");
                document.getElementById("<%=txtUnserviceable.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtQtyRec.ClientID%>").value == "") {
                alert("No. of mats Quantity Received can not be blank");
                document.getElementById("<%=txtQtyRec.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtQtySizeRec.ClientID%>").value == "") {
                alert("No. of mats size Quantity Received can not be blank");
                document.getElementById("<%=txtQtySizeRec.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtQtyValueRec.ClientID%>").value == "") {
                alert("No. of mats value Quantity Received can not be blank");
                document.getElementById("<%=txtQtyValueRec.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtFromDate.ClientID%>").value == "") {
                alert("From Date can not be blank");
                document.getElementById("<%=txtFromDate.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtCIServiceable.ClientID%>").value == "") {
                alert("No. of mats Serviceable (Total of CI No. 3a & 4) can not be blank");
                document.getElementById("<%=txtCIServiceable.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtCIUnserviceable.ClientID%>").value == "") {
                alert("No. of mats Unserviceable (Total of CI No. 3a & 4) can not be blank");
                document.getElementById("<%=txtCIUnserviceable.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtCIValue.ClientID%>").value == "") {
                alert("No. of mats Value (Total of CI No. 3a & 4) can not be blank");
                document.getElementById("<%=txtCIValue.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtQtyUsed.ClientID%>").value == "") {
                alert("Quantity Used (No. of mats) can not be blank");
                document.getElementById("<%=txtQtyUsed.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtQtyUsedSize.ClientID%>").value == "") {
                alert("Quantity Used (No. of mats size) can not be blank");
                document.getElementById("<%=txtQtyUsedSize.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtQtyUsedValue.ClientID%>").value == "") {
                alert("Quantity Used (No. of mats value) can not be blank");
                document.getElementById("<%=txtQtyUsedValue.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtFloorArea.ClientID%>").value == "") {
                alert("Floor Area can not be blank");
                document.getElementById("<%=txtFloorArea.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=ddl_gdwn.ClientID%>").value == "") {
                alert("Godown No. can not be blank");
                document.getElementById("<%=ddl_gdwn.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtQtyTran.ClientID%>").value == "") {
                alert("Quantity Transferred (No of mats) can not be blank");
                document.getElementById("<%=txtQtyTran.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtQtyTranSize.ClientID%>").value == "") {
                alert("Quantity Transferred (No of mats size) can not be blank");
                document.getElementById("<%=txtQtyTranSize.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtQtyTranValue.ClientID%>").value == "") {
                alert("Quantity Transferred (No of mats value) can not be blank");
                document.getElementById("<%=txtQtyTranValue.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtCentreName.ClientID%>").value == "") {
                alert("Name of Centre which transferred can not be blank");
                document.getElementById("<%=txtCentreName.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtCBUnderUse.ClientID%>").value == "") {
                alert("No.of mats Under Use (Closing Balance) can not be blank");
                document.getElementById("<%=txtCBUnderUse.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtCBServiceable.ClientID%>").value == "") {
                alert("No.of mats Serviceable (Closing Balance) can not be blank");
                document.getElementById("<%=txtCBServiceable.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtCBUnserviceable.ClientID%>").value == "") {
                alert("No.of mats Unserviceable (Closing Balance) can not be blank");
                document.getElementById("<%=txtCBUnserviceable.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtRemarks.ClientID%>").value == "") {
                alert("Remarks can not be blank");
                document.getElementById("<%=txtRemarks.ClientID%>").focus();
            return false;
        }

        return true;
    }
    function NumberOnly(e) {
        var charCode = (e.which) ? e.which : e.keyCode;
        if ((charCode >= 48 && charCode <= 57)) {
            return true;
        }
        if (charCode == 46) { return true; }
        if (charCode == 8) { return true; }
        if (charCode == 9) { return true; }
        else { return false; }
    }
    </script>
    <script>
    $(document).ready(function () {
        $("[id$=txtDate]").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            dateFormat: 'dd/mm/yy',
        });
    });
    </script>
    <script>
    $(document).ready(function () {
        $("[id$=txtReceiptDate]").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            dateFormat: 'dd/mm/yy',
        });
        $("[id$=txtBillDate]").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            dateFormat: 'dd/mm/yy',
        });
    });
    </script>
</asp:Content>


