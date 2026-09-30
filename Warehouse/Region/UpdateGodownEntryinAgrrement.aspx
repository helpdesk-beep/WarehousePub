<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="UpdateGodownEntryinAgrrement.aspx.cs" Inherits="Region_UpdateGodownEntryinAgrrement" Title="Pvt. Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 100%; border: 2px solid navy;">
        <center>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="8" align="center" valign="top">
                            <fieldset style="border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="8" align="center">
                                                    <span style="color: White; font-size: 12pt; font-weight: bold">Update Private Godown in IWMS Software</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="8"></td>
                                            </tr>

                                            <tr>
                                                <td align="left" colspan="8">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="8" align="center">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 150px" align="left">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 100px" align="left">
                                                    <asp:Label ID="lblBranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        CssClass="tb6"
                                                        OnSelectedIndexChanged="ddlDepotList_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                  <td style="width: 100px" align="left">
                                                    <asp:Label ID="Label3" runat="server" Text="Select Godown" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlgodown" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        CssClass="tb6"
                                                        OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="8"></td>
                                            </tr>
                                            <tbody id="showdetails" runat="server" visible="false">
                                            <tr id="trddlgd" runat="server">
                                                <td align="left">
                                                  <asp:Label ID="Label2" runat="server" Text="Registration Id :- " Font-Size="10pt" Font-Bold="true"></asp:Label>  </td>
                                                <td align="left" >
                                                   <asp:Label ID="lblregid" runat="server" Font-Size="10pt" Font-Bold="true"></asp:Label>

                                                </td>

                                                <td align="left">
                                                   <asp:Label ID="Label1" runat="server" Text="Warehouse Name:-" Font-Size="10pt" Font-Bold="true"></asp:Label> </td>
                                                <td align="left">
                                                   <asp:Label ID="lblwhname" runat="server" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="8"></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label5" runat="server" Text="Priority :- " Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="left">
                                                   <asp:Label ID="lblpriority" runat="server" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>

                                          
                                                <td align="left">
                                                    <asp:Label ID="Label6" runat="server" Text="Maintain_By :- " Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblMaintainby" runat="server" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>

                                           
                                                <td align="left">
                                                    <asp:Label ID="Label7" runat="server" Text="Offer Capacity :- " Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="left" colspan="3">
                                                   <asp:Label ID="lbloffercapacity" runat="server" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>

                                            </tr>

                                                 <tr>
                                                <td style="height: 10px" colspan="8"></td>
                                            </tr>
                                                <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label4" runat="server" Text="Select Godown :- " Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="left" colspan="6">
                                                   <asp:DropDownList ID="ddlintgodown" runat="server" Height="25px" Width="500px" AutoPostBack="True"
                                                        CssClass="tb6">
                                                    </asp:DropDownList>
                                                </td>

                                          
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                                
                                            <tr>
                                                <td colspan="8" align="center">
                                                    <asp:Button ID="btnupdate" runat="server" Text="Save" Width="100px" CssClass="BTNBLUE"
                                                        ValidationGroup="validate" OnClick="btnupdate_Click" />
                                                    &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                CausesValidation="false" OnClick="btn_Close_Click" />
                                                </td>
                                            </tr>
                                                </tbody>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="8"></td>
                    </tr>
                </table>
            </div>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </fieldset>
</asp:Content>

