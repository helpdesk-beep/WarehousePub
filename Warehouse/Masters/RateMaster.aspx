<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master"
    AutoEventWireup="true" CodeFile="RateMaster.aspx.cs" Inherits="Masters_RateMaster"
    Title="Rate Master ::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="2" align="center">
                                    <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Storage Rate Master"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="lblmsg" runat="server" Font-Italic="True" ForeColor="Maroon" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 200px">
                                    <asp:Label ID="Label9" runat="server" Text="Commodity Category" ForeColor="Navy"
                                        Font-Bold="true" Font-Size="8pt"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlverity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlverity_SelectedIndexChanged"
                                        Width="250px" Height="25px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="Label12" runat="server" ForeColor="Navy" Font-Bold="true" Font-Size="8pt"
                                        Text="Select Commodity"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="170px"
                                        ScrollBars="Vertical" Width="250px">
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderStyle="Double" Width="209px">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label1" runat="server" Text="Packing Type" ForeColor="Navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlpacktype" runat="server" Width="250px" Height="25px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label2" runat="server" Text="Weight" ForeColor="Navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlweight" runat="server" Width="250px" Height="25px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label3" runat="server" Text="Max Height of Stack" ForeColor="Navy"
                                        Font-Bold="true" Font-Size="8pt"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlstackheight" runat="server" Width="250px" Height="25px">
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label5" runat="server" Text="Effective From" ForeColor="Navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="fromdate" runat="server" Width="150px"></asp:TextBox>
                                    <asp:ImageButton ID="Imgpop" runat="server" ImageUrl="~/images/cal.gif" CausesValidation="false" />
                                    <asp:CalendarExtender ID="tctrcdate_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="fromdate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"
                                        PopupButtonID="Imgpop">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label6" runat="server" Text="Rate" ForeColor="Navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtrate" runat="server" Width="150px"></asp:TextBox>
                                    <asp:Label ID="Label4" runat="server" ForeColor="Navy" Font-Bold="true" Font-Size="8pt"
                                        Text="(Rs.)"></asp:Label>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtrate"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label7" runat="server" Text="Revised Date" ForeColor="Navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="revdate" runat="server" Width="150px"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal.gif" CausesValidation="false" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="revdate"
                                        Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="ImageButton1">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label10" runat="server" Text="Rate" ForeColor="Navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtrevrate" runat="server" Width="152px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtrevrate"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:Label ID="Label11" runat="server" ForeColor="Navy" Font-Bold="true" Font-Size="8pt"
                                        Text="(Rs.)"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label13" runat="server" Text="Remarks" ForeColor="Navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnnew" runat="server" OnClick="btnnew_Click" Text="New" Width="96px" />
                                    <asp:Button ID="btnsave" runat="server" Text="Save" Width="141px" OnClick="btnsave_Click" />
                                    <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close" Width="137px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </fieldset>
</asp:Content>
