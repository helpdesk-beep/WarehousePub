<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Rpt_Delete_Uploaded_DSC.aspx.cs" Inherits="Reports_States_Rpt_Delete_Uploaded_DSC" Title="Delete DSC Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset style="width: 100%; border: 2px solid navy;">
        <center>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="6" align="center" valign="top">
                                    <fieldset style="width: 100%; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Create Private Godown Login</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td align="left" colspan="4">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4" align="center">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 150px" align="left">
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                                CssClass="tb6" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                      <td style="width: 100px" align="left">
                                                            <asp:Label ID="lblBranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true" Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="width: 200px" align="left">
                                                            <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px" AutoPostBack="True" Visible="false"
                                                                 CssClass="tb6" 
                                                                onselectedindexchanged="ddlDepotList_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td align="center" valign="top" colspan="4">
                                                    <asp:GridView ID="godown_GridView" runat="server" DataKeyNames="Aid" AutoGenerateColumns="False"
                                                        CellPadding="2" Width="100%" AllowSorting="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.N.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="SerialNumber" HeaderText="Serial Number" />
                                                            <asp:BoundField DataField="Branch" HeaderText="Branch"/>
                                                            <asp:BoundField DataField="Branch_Godown_Name" HeaderText="Branch_Godown_Name" />
                                                            <asp:BoundField DataField="DSC_HOLDER_NAME" HeaderText="DSC_HOLDER_NAME" />
                                                            <asp:BoundField DataField="DSC_IssuerName" HeaderText="DSC_IssuerName" />
                                                            <asp:BoundField DataField="ValidUpto" HeaderText="Valid Upto" />
                                                            <asp:BoundField DataField="DSC_UploadDate" HeaderText="DSC_UploadDate" />
                                                            <asp:BoundField DataField="User_Type" HeaderText="User_Type" />
                                                             <asp:BoundField DataField="Verification_Status" HeaderText="Verification" />
                                                             <asp:BoundField DataField="DeleteDate" HeaderText="Delete Date" />
                                                        </Columns>
                                                        <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
                                                    </asp:GridView>
                                                    <asp:Label ID="Label3" runat="server" Font-Size="X-Small" ForeColor="#400040"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                          
                                                    <tr>
                                                    <td>
                                                    &nbsp;
                                                    </td>
                                                    </tr>
                                                    <tr>
                        
                    </tr>
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="4">
                                </td>
                            </tr>
                        </table>
                    </div>
              <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </fieldset>
</asp:Content>