<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Private_Warehouse_Related_Information.aspx.cs" Inherits="BranchPages_Private_Warehouse_Related_Information" Title="Privateb Warehouse Related Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table {
            border-collapse: collapse;
        }

        /* td {
            padding-top: .5em;
            padding-bottom: .5em;
        }*/

        td {
            padding-top: 10px;
            padding-bottom: 10px;
        }
        /*table {
            border-collapse: collapse;
        }

        th {
            background-color: green;
            Color: white;
        }

        th, td {
            width: 150px;
            text-align: center;
            border: 1px solid black;
            padding: 5px
        }

        .geeks {
            border-right: hidden;
        }

        .gfg {
            border-collapse: separate;
            border-spacing: 0 15px;
        }

        h1 {
            color: green;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <div>
            <table width="1000px" class="gfg">
                <tr id="msg">
                    <td>
                        <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
                </tr>
                <tr style="background-color: #0bb6e6; height: 25px">
                    <td align="center">
                        <asp:Label ID="lblheading" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                            Text="गोदाम संचालक द्वारा उत्पन्न अवरोध / लापरवाही की जानकारी"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 5px"></td>
                </tr>

                <tr id="trJVSGodownRent" visible="false" runat="server">
                    <td>

                        <table cellpadding="0" cellspacing="0" style="width: 100%">

                            <tr align="center">
                                <td align="center">
                                    <asp:Label ID="Label2" runat="server" Text="Godown Name" Font-Bold="True" Font-Size="8pt"
                                        ForeColor="Navy"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:DropDownList ID="ddl_gdwn" AutoPostBack="true" runat="server" Width="200px" Height="25px">
           <%--  <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddl_gdwn_SelectedIndexChanged">
                    </asp:DropDownList>
           <asp:ListItem Value="0">--Select--</asp:ListItem>
           <asp:ListItem Value="1" Selected="True">MPWLC Godowns</asp:ListItem>
           <asp:ListItem Value="2">JVS Godowns</asp:ListItem>
           <asp:ListItem Value="3">Hired Godowns</asp:ListItem>
           <asp:ListItem Value="4">Silo Bags</asp:ListItem>
           <asp:ListItem Value="5">MPWLC Owned Cap</asp:ListItem>
           <asp:ListItem Value="6">Tribal Scheme</asp:ListItem>
           <asp:ListItem Value="7">CAP-PMS</asp:ListItem>--%>
       </asp:DropDownList>
                                </td>
                                <td align="center" colspan="2">
                                    <asp:Label ID="lblRegID" runat="server" Text="JVS Registration ID" Font-Bold="True" Font-Size="8pt"
                                        ForeColor="Navy"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:DropDownList ID="ddlRegID" runat="server" Width="200px" Height="25px">
       </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px"></td>
                            </tr>
                            <tr>
                                <td colspan="6" align="center" style="background-color: #0bb6e6; height: 25px">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px"></td>
                            </tr>

                            <tr>
                                <td colspan="2">
                                    <asp:Label Font-Size="10pt" Font-Bold="true" ForeColor="navy" ID="lblgodown" runat="server" Text="1.क्या गोदाम संचालक द्वारा सकंध के जमा भुगतान में किसी प्रकार का अवरोध उत्पन्न किया गया है ? "></asp:Label>
                                </td>


                                <td colspan="2">
                                    <asp:DropDownList ID="ddlObstruction_BYGO" Height="20px" Class="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDepositPayment_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="Y">YES</asp:ListItem>
                                        <asp:ListItem Value="N">NO</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tbody id="show1" runat="server" visible="false">
                                <td>
                                    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label4" runat="server" Text="अवरोध उत्पन्न की दिनाक"></asp:Label>
                                </td>
                                <%--<td>--%>
                                <%--  <asp:TextBox ID="txtDateBlocking" runat="server" class="form-control" placeholder="DD/MM/YYYY"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDateBlocking" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" Text="DD/MM/YYYY" />--%>
                                <td valign="middle">
                                    <asp:TextBox ID="txtDate_Of_Obstruction" placeholder="DD/MM/YYYY" onclick="return ValidateDOB()" CssClass="form-control" runat="server" AutoComplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtDate_Of_Obstruction" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDate_Of_Obstruction" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate_Of_Obstruction" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                </td>
                                <%--</td>--%>
                                <td>
                                    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lblObstruction_Remark" runat="server" Text="Remark"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtObstruction_Remark" TextMode="MultiLine" runat="server" class="form-control" placeholder="Remark"></asp:TextBox>
                                </td>
                            </tbody>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label Font-Size="10pt" Font-Bold="true" ForeColor="navy" ID="lblWarehouseFoundInfested" runat="server" Text="2.
क्या गोदाम संचालक द्वारा स्कंध का वैज्ञानिक ढ़ंग  से  रख-रखाव नहीं किया गया फलस्वरूप किटग्रस्ता पाई गई |
"></asp:Label>
                    </td>

                    <td colspan="2">
                        <asp:DropDownList ID="ddlWarehouse_Infested_Status" OnSelectedIndexChanged="ddlWarehouseFoundInfested_SelectedIndexChanged" AutoPostBack="true" Height="20px" Class="form-control" runat="server">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="Y">YES</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>
                <tbody id="Tbody1" runat="server" visible="false">
                    <tr>
                        <td>
                            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label6" runat="server" Text="किटग्रस्ता पाये जाने की दिनाक"></asp:Label>
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtInfestedDate" runat="server" class="form-control" placeholder="DD/MM/YYYY"></asp:TextBox>
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInfestedDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                            ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" Text="DD/MM/YYYY" />--%>
                            <asp:TextBox ID="txtWarehouse_Infested_Date" placeholder="DD/MM/YYYY" onclick="return ValidateDOB()" CssClass="form-control" runat="server" AutoComplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtWarehouse_Infested_Date" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtWarehouse_Infested_Date" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtWarehouse_Infested_Date" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                        </td>
                        <td>
                            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lblInfested_Remark" runat="server" Text="Remark"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInfested_Remark" TextMode="MultiLine" runat="server" class="form-control" placeholder="Remark"></asp:TextBox>

                        </td>
                    </tr>

                </tbody>
                <tr>
                    <td colspan="4" style="height: 5px"></td>
                </tr>

            </table>

        </div>
        <table width="100%">
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSumbmi" OnClick="btnSumbmi_Click" runat="server" Text="Sumbit"
                        CssClass="BTNBLUE" Enabled="true" />

                </td>

            </tr>
            <tr>
               <%-- <div class="container pt-2">
                    <h4 class="text-info">List Insecticide Opening Balance Entry</h4>
                    <div class="row">
                        <div class="col-12">--%>
                            <%--<asp:GridView ID="GVOfStock" OnRowEditing="GVOfStock_RowEditing" OnRowUpdating="GVOfStock_RowUpdating"
                        AutoGenerateColumns="false" runat="server" 
                        OnRowCancelingEdit="GVOfStock_RowCancelingEdit1"
                        OnRowCommand="GVOfStock_RowCommand">--%>
                            <headerstyle
                                backcolor="#D69758"
                                font-italic="false"
                                forecolor="Snow" />
                            <%--<td colspan="4">
                    <fieldset style="width: 980px; border: 1px solid navy;">
                        <center>
                            <div style="overflow: scroll; height: 400px; overflow-x: hidden">
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <%--<td colspan="6" align="center" style="background-color: #0bb6e6; height: 25px">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                                Text="Storage Bill Detail"></asp:Label>
                                        </td>--%>
                            <%--</tr>
                                    <tr>
                                        <td colspan="6" align="center">--%>
                                           <%-- <asp:GridView ID="gvshow" runat="server">
                                                <Columns>
                                                    <asp:boundfield headertext="date" dataformatstring="{0:dd/mm/yyyy}" datafield="deposit_date" />
                                                    <asp:BoundField HeaderText="Godown Id" DataField="Godown_Id" />
                                                    <asp:BoundField HeaderText="receive bags" DataField="Obstruction_BYGO" />
                                                    <asp:BoundField HeaderText="issue bags" DataField="Obstruction_Remark" />
                                                    <asp:BoundField HeaderText="closing bag balance" DataField="Date_Of_Obstruction" />
                                                    <asp:BoundField HeaderText="per day rate(for bag)" DataField="Warehouse_Infested_Status" />
                                                    <asp:BoundField HeaderText="per day charges" DataField="Infested_Remark" />
                                                    <asp:BoundField HeaderText="godown_id" DataField="Warehouse_Infested_Date" />
                                                </Columns>
                                            </asp:GridView>--%>
                <%--</td>--%>
            </tr>


        </table>

        </div>
                               
                            <table width="100%">
                                <tr>
                                    <td align="center" colspan="2">



                                        <asp:Button ID="Button1" runat="server" Text="New Verification" Visible="false" Width="110px"
                                            CssClass="BTNBLUE" />
                                    </td>

                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="Lblmsg2" runat="server" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </td>

                                </tr>
                            </table>

        </td>
            </tr>
        </table>
    </center>
    </td>
                    </tr>
                </table>
         
    <asp:HiddenField ID="hdnAmt" runat="server" Value="0" />
</asp:Content>

