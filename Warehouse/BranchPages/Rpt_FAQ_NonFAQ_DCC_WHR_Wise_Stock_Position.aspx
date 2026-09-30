<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Rpt_FAQ_NonFAQ_DCC_WHR_Wise_Stock_Position.aspx.cs" Inherits="BranchPages_WHR_Wise_Stock_Position" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
         
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="4" align="center">
                                    <asp:Label ID="lblhead" runat="server" Text="FAQ Non-FAQ And DCC Stock Details Submit by Branch Manager " Font-Size="12pt"
                                        ForeColor="whitesmoke" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 10px">
                                </td>
                            </tr>                           
                               <tr id="tr1" runat="server" visible="true">  
                                    <td align="left">
                                    <asp:Label ID="lblCommodity" runat="server" Text="Commodity" ForeColor="navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td align="left" valign="middle">
                                    <asp:DropDownList ID="ddlcommodity" runat="server" Width="305px" Height="25px" TabIndex="4"
                                        CssClass="tb6">
                                         
                                    </asp:DropDownList>
                                </td>
                                    <td align="left">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                        Text="Crop Year"></asp:Label>
                                </td>
                                <td align="left" valign="middle">
                                   <td align="left" valign="middle">
                            <asp:DropDownList ID="ddlcropyear" runat="server" Width="305px" Height="25px" TabIndex="4"
                                CssClass="tb6" AutoPostBack="true" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="2016-17">2016-17</asp:ListItem>
                                <asp:ListItem Value="2017-18">2017-18</asp:ListItem>
                                <asp:ListItem Value="2018-19">2018-19</asp:ListItem>
                                <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                                <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                                <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                                <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                                <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                                <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                                </td>
                             <td align="left">
                                    <asp:Label ID="lblGodown" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                        Text="Godown Name"></asp:Label>
                                </td>
                                <td align="left" valign="middle">
                                    <asp:DropDownList ID="ddl_godown" runat="server" Width="205px" AutoPostBack="True"
                                        CssClass="tb6" Height="25px" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                   
                            </tr>
                               <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>                            
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                         
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text=""
                                        Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px">
                                </td>
                            </tr>
                           
                            <tr>
                                <td colspan="8" align="center" valign="top">
                                    <asp:GridView ID="gv_whr" runat="server" AutoGenerateColumns="false" Width="100%"
                                        Font-Size="10pt" DataKeyNames="Whr_No" OnRowUpdating="gv_whr_RowUpdating">
                                        <Columns>  
                                            <asp:TemplateField HeaderText="Godown Name" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGodown_Nameo" runat="server" Text='<%# Eval("Godown_Name") %>' />
                                            </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Whr No" ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWhr_No" runat="server" Text='<%# Eval("Whr_No") %>' />
                                            </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="WHR Issue Date" ItemStyle-Width="10">
                                            <ItemTemplate>
                                             <asp:Label ID="lblWHR_Issue_Date" runat="server" Text='<%# Eval("WHR_Issue_Date") %>' />
                                            </ItemTemplate>
                                                 </asp:TemplateField>
                                          <%--   <asp:TemplateField HeaderText="Stack ID" ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStack_ID" runat="server" Text='<%# Eval("Stack_ID") %>' />
                                            </ItemTemplate>
                                              </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Stack Name" ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("Stack_Name") %>' />
                                            </ItemTemplate>
                                              </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="No. of Bags" ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBagBalance" runat="server" Text='<%# Eval("BagBalance") %>' />
                                            </ItemTemplate>
                                               </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity (in Qtls.)" ItemStyle-Width="10">
                                            <ItemTemplate>
                                             <asp:Label ID="lblWeightBalance" runat="server" Text='<%# Eval("WeightBalance") %>' />
                                            </ItemTemplate>
                                               </asp:TemplateField>
                                             
                                            <asp:TemplateField HeaderText="FAQ Stock Quantity (in Qtls.)" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Label ID="txtFAQ_Stock" runat="server" Text='<%# Eval("FAQ_Stock") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Non FAQ Stock Quantity (in Qtls.)" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Label ID="txtNon_FAQ_Stock" runat="server" Text='<%# Eval("Non_FAQ_Stock") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="DCC Stock Quantity (in Qtls.)" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Label ID="txtDCC_Stock" runat="server" Text='<%# Eval("DCC_Stock") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="कीटग्रस्‍त स्‍कंध की मात्रा (in Qtls.)" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Label ID="txtinfestedstock" runat="server" Text='<%# Eval("infestedstock") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="आटा फारमेशन की मात्रा (in Qtls.)" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Label ID="txtdoughformation" runat="server" Text='<%# Eval("doughformation") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="फारेन मेटर युक्‍त स्‍कंध की मात्रा (in Qtls.)" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Label ID="txtforeignmatter" runat="server" Text='<%# Eval("foreignmatter") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="खराब बारदाने में भंडारित स्‍कंध की मात्रा (in Qtls.)" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Label ID="txtbadgunnybags" runat="server" Text='<%# Eval("badgunnybags") %>' />
                                            </ItemTemplate>
                                            </asp:TemplateField>

                                           <%-- <asp:TemplateField HeaderText="DCC Stock" ItemStyle-Width="10">
                                            <ItemTemplate>
                                            <asp:Button ID="btn_Update" runat="server" Text="Update" OnClientClick="return confirm('Do you want to Update this Stock Balance?');" CommandName="Update"/>
                                            </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="20px" Font-Size="10pt" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                         
                            <tr>
                                <td style="height: 15px">
                                </td>
                            </tr>
                            <%--<tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnsave" runat="server" Text="Submit" Width="120px" CssClass="BTNBLUE"
                                        ValidationGroup="SaveValid" OnClick="btnsave_Click"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="120px" CssClass="BTNBLUE"
                                        CausesValidation="false" Visible="false" onclick="btnPrint_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="SaveValid" />
                                </td>
                            </tr>--%>
                        </table>
                    </div>
               
        </center>
    </fieldset>
</asp:Content>

