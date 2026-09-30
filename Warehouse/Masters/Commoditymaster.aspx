<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Commoditymaster.aspx.cs"
    Inherits="Masters_pCommoditymaster" MasterPageFile="~/MasterPage/StateMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<fieldset style="width: 620px; border: 1px solid navy; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px;
      padding-left: 0px; margin-left: 5px ; padding-right:0px; margin-right:5px">
        <center>
            <div>
    <table style="width: 580px;">
                        <tr>
                        <td align="center" valign="top">
                            <fieldset style="width:580px; border: 1px solid navy; margin-top:2px">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr style="background-color: #0bb6e6; height: 25px">
            <td colspan="2" style="text-align: center">
                <strong><span style="font-size: 8pt; color: #990033">
                    <asp:Label ID="lblCommodityMas" runat="server" Text="Commodity Master" Font-Bold="true" 
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label></span></strong>
            </td>
        </tr>
                                                    <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
        <tr>
            <td colspan="2" style="height: 148px">
                <asp:GridView ID="Commodity_GridView" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" DataKeyNames="Commodity_Id" Width="580px" BackColor="White" BorderColor="#CC9966"
                    BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView1_RowDataBound"
                    OnPreRender="Commodity_GridView_PreRender" OnSelectedIndexChanged="Commodity_GridView_SelectedIndexChanged"
                    OnRowEditing="Commodity_GridView_RowEditing" OnRowUpdating="Commodity_GridView_RowUpdated"
                    OnRowDeleting="Commodity_GridView_RowDeleting" OnRowCancelingEdit="Commodity_GridView_RowCancelingEdit"
                    AllowPaging="True" OnPageIndexChanging="Commodity_GridView_PageIndexChanging">
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <Columns>
                        <asp:TemplateField HeaderText="Action" ShowHeader="False" Visible="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="Update"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serial Number" InsertVisible="False">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Commodity Name" InsertVisible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_Commodity_Name" runat="server" Text='<%#Bind("Commodity_Name")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_Commodity_Name" runat="server" Text='<%#Bind("Commodity_Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddl_status1" runat="server" DataTextField="ShowStatus" DataValueField="Status"
                                    AutoPostBack="True" SelectedValue='<%#Bind("Status")%>'>
                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="custom_comm" runat="server" Display="Dynamic" ControlToValidate="ddl_status1"
                                    OnServerValidate="custom_validate" ValidationGroup="validation"></asp:CustomValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("ShowStatus")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Commodity Group">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddl_Commoditygroup" runat="server" DataTextField="Group_name"
                                    DataValueField="Comm_Group_id" AutoPostBack="True" SelectedValue='<%#Bind("Comm_Group_id")%>'
                                    DataSourceID="sqldatasourse1">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="custom_comm_group" runat="server" Display="Dynamic" ControlToValidate="ddl_Commoditygroup"
                                    OnServerValidate="custom_validate_commgroup" ValidationGroup="validation"></asp:CustomValidator>
                                <asp:SqlDataSource ID="sqldatasourse1" runat="server" ConnectionString="<%$ ConnectionStrings:FCIConnectionString %>"
                                    SelectCommand="select Comm_Group_id,Group_name from tbl_MetaData_STORAGE_COMMODITY_Group">
                                </asp:SqlDataSource>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Lbl_commgroup" runat="server" Text='<%#Bind("Group_name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" HorizontalAlign="Center" />
                </asp:GridView>
                <asp:Label ID="Label2" runat="server" Font-Size="X-Small" ForeColor="#400040"></asp:Label>
            </td>
                                                   <tr>
                                          <td style="height: 10px" colspan="2">
                                          </td>
                                     </tr>
        </tr>
                                                    <tr>
                                                <td style="height: 10px" colspan="4">
            <asp:Panel ID="paneladdcmd" runat="server" Visible="false">
                <table style="width: 572px">
                    <tr>
                        <td colspan="2" style="text-align:left">
                            <asp:DetailsView ID="Commodity_detailsinsert" runat="server" AutoGenerateRows="False"
                                DefaultMode="Insert" Height="50px" Visible="False" Width="575px">
                                <Fields>
                                    <asp:TemplateField HeaderText="Commodity Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:TextBox ID="TextBox1" onblur="Spc_character(this)" runat="server" MaxLength="30"></asp:TextBox><span
                                                style="color: #990000">*<asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                                    runat="server" ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="Commodity Name field cannot be empty"
                                                    SetFocusOnError="True">></asp:RequiredFieldValidator></span>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddl_status" runat="server" DataTextField="Status" DataValueField="Status">
                                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="custom_comm1" runat="server" Display="Dynamic" ControlToValidate="ddl_status"
                                                OnServerValidate="custom_validate" ValidationGroup="validation"></asp:CustomValidator>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commodity Group">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:DropDownList ID="ddl_Commgroup" runat="server" DataTextField="Group_name" DataValueField="Comm_Group_id"
                                                DataSourceID="sqldatasourse2">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="custom_comm_group1" runat="server" Display="Dynamic" ControlToValidate="ddl_Commgroup"
                                                OnServerValidate="custom_validate_commgroup" ValidationGroup="validation"></asp:CustomValidator>
                                            <asp:SqlDataSource ID="sqldatasourse2" runat="server" ConnectionString="<%$ ConnectionStrings:FCIConnectionString %>"
                                                SelectCommand="select Comm_Group_id,Group_name from tbl_MetaData_STORAGE_COMMODITY_Group">
                                            </asp:SqlDataSource>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Lbl_commgroup" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="height: 5px" colspan="4">
                                                </td>
                                            </tr>
                            <tr>
                            <td align="center">
                            <asp:Button ID="btninsert" runat="server"  OnClick="btninsert_Click" CssClass="BTNBLUE" Height="33px" Width="70px"
                                Text="Insert" Visible="False" />
                            &nbsp;
                            <asp:Button ID="btncancel" runat="server"  OnClick="btncancel_Click" CssClass="BTNBLUE" Height="33px" Width="70px"
                                Text="Cancel" Visible="False" CausesValidation="False" />
                        </td>
                        </tr>
                        </table>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                                                </td>
                                            </tr>
        
                    <tr>
                        <td style="text-align:center">
                            <asp:Button ID="btnaddnew" runat="server" OnClick="Button1_Click" CssClass="BTNBLUE" Height="33px" Width="70px"
                                Text="Add New" CausesValidation="False" />
                        </td>
                    </tr>        
    </table>
    </div>
    </center>
    </fieldset>
    </td>
    </tr>
</table>
   
    &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
         </div>
        </center>
      </fieldset>
</asp:Content>
