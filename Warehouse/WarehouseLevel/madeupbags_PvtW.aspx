<%@ Page Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="madeupbags_PvtW.aspx.cs" Inherits="WarehouseLevel_madeupbags_PvtW" Title="Madeup Bags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="divsave" style="text-align: left">        
        <table style="background-image: url(../../images/images[26].jpg);margin-left:20px; width: 575px;">
            <tr>
                <td style="width: 100px; height: 15px; background-color: dimgray">
                </td>
            </tr>
            <tr>
                <td style="TEXT-ALIGN: center">
                    <span style="font-size: 8pt; color: #660000"><strong>&nbsp;<asp:Label ID="lblMadeupheader" runat="server" Text="Made UpBags Form" Width="177px"></asp:Label></strong></span></td>
            </tr>
            <tr>
                <td style="width: 100px;" align="center">
        <asp:GridView ID="GridView_MadeUpBags" runat="server"  Width="644px" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" EnableTheming="True" DataKeyNames="Bagsid"  OnRowUpdating="GridView_MadeUpBags_RowUpdating" OnRowEditing="GridView_MadeUpBags_RowEditing" >
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <Columns>
                 <asp:TemplateField HeaderText="Edit" ShowHeader="False">
               <ItemTemplate>
                   <asp:LinkButton ID="btnedit" runat="server" 
			CommandName="Edit" Text="Edit" CausesValidation="false"></asp:LinkButton>
               </ItemTemplate>
               <EditItemTemplate>
                   <asp:LinkButton ID="btnupdate" runat="server" 
			CommandName="Update" Text="Update" CausesValidation="true"></asp:LinkButton>
                   
               </EditItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="S. No">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSerial" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" SortExpression="Godown_Name" ReadOnly="True" >
                    <ItemStyle Wrap="True" />
                </asp:BoundField>
                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" SortExpression="Commodity_Name" ReadOnly="True" />
                <asp:BoundField DataField="Stack_Name" HeaderText="Stack Name" SortExpression="Stack_Name" ReadOnly="True" />
                <asp:TemplateField HeaderText="Made Up Bags" SortExpression="No_Made_Up_Bags">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("No_Made_Up_Bags") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("No_Made_Up_Bags") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Collection Date" SortExpression="Collection_Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"  Width="167px" Text='<%# Bind("Collection_Date") %>'></asp:TextBox>&nbsp;
                       <span id="spnDateofIssueEdit" runat="server"></span><a onclick="ShowCalendar(ctl00$ContentPlaceHolder1$GridView_MadeUpBags$ctl03$TextBox2 , ctl00$ContentPlaceHolder1$GridView_MadeUpBags$ctl03$TextBox2);"
																href="javascript:;"><IMG height="16" alt="Click Here to Pick up the date" src="../../images/cal.gif" width="16"
																	border="0"></a>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Collection_Date") %>'></asp:Label>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="WHR_NO" HeaderText="WHR NO" SortExpression="WHR_NO" ReadOnly="True" />
            </Columns>
            <RowStyle BackColor="White" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
        </asp:GridView>
                   
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                <table style="width:400px">          
                    <tr>
                        <td style="width: 147px; height: 22px;">
                            <asp:Label ID="lblWHRNumber" runat="server" Font-Size="X-Small" Text="WHR No." Width="111px"></asp:Label></td>
                        <td style="width: 97px; height: 22px;">
                            <asp:DropDownList ID="ddlwhrlist" runat="server" Font-Size="XX-Small"  AutoPostBack="True"  TabIndex="1" OnSelectedIndexChanged="ddlwhrlist_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CVWhr" runat="server" ErrorMessage="WHR" ControlToValidate="ddlwhrlist" OnServerValidate="CVWhr_ServerValidate" ValidationGroup="SaveValid"></asp:CustomValidator></td>
                    </tr>
            
            
            <tr>
                <td style="width: 147px">
                    <span style="font-size: 8pt">
                        <asp:Label ID="lblCommodity" runat="server" Text="Commodity" Width="77px"></asp:Label></span></td>
                <td style="width: 97px">
                   
                    <asp:DropDownList ID="ddlcommodity" runat="server" DataValueField="Commodity_Id" Width="146px" AutoPostBack="True" Font-Size="XX-Small" Enabled="False">
                    <asp:ListItem> Select</asp:ListItem>
                    </asp:DropDownList>
                            <asp:CustomValidator ID="CVcomm" runat="server" ErrorMessage="Comm" ControlToValidate="ddlcommodity" OnServerValidate="CVcomm_ServerValidate" ValidationGroup="SaveValid"></asp:CustomValidator>
                        
                            
                       
                </td>
            </tr>
            <tr>
                <td style="width: 147px">
                    <span style="font-size: 8pt">
                        <asp:Label ID="lblGodownNo" runat="server" Text="Gododwn No" Width="82px"></asp:Label></span></td>
                <td style="width: 97px">
                  
                    <asp:DropDownList ID="ddlgodown" runat="server" DataValueField="Godown_ID" Width="146px" AutoPostBack="True" Font-Size="XX-Small" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged" TabIndex="2">
                    <asp:ListItem> Select</asp:ListItem></asp:DropDownList>
                            <asp:CustomValidator ID="CVGodown" runat="server" ErrorMessage="Godown" ControlToValidate="ddlgodown" OnServerValidate="CVGodown_ServerValidate" ValidationGroup="SaveValid"></asp:CustomValidator>
                       
                   
                </td>
            </tr>
            <tr>
                <td style="width: 147px;">
                    <span style="font-size: 8pt">
                        <asp:Label ID="lblStackNo" runat="server" Text="Stack No" Width="75px"></asp:Label></span></td>
                <td style="width: 97px; height: 27px;">
                    
                    <asp:DropDownList ID="ddlstack" runat="server" DataValueField="Stack_ID" Width="146px" Font-Size="XX-Small" TabIndex="3">
                    <asp:ListItem> Select</asp:ListItem>
                    </asp:DropDownList>
                                    <asp:CustomValidator ID="CVstack" runat="server" ErrorMessage="Stack" ControlToValidate="ddlstack" OnServerValidate="CVstack_ServerValidate" ValidationGroup="SaveValid"></asp:CustomValidator>
                              
                        
                </td>
            </tr>
            <tr>
                <td style="width: 147px">
                    <span style="font-size: 8pt">
                        <asp:Label ID="lblCollectionDate" runat="server" Text="Date Of Collection"></asp:Label></span></td>
                <td>
                    <asp:TextBox ID="txtcollectiondate" runat="server" Width="86px" Font-Size="XX-Small" ></asp:TextBox><span
                        style="font-size: 7pt; color: #990000">*</span><a onclick="ShowCalendar(ctl00_ContentPlaceHolder1_txtcollectiondate, ctl00_ContentPlaceHolder1_txtcollectiondate);"
																href="javascript:;">
                            <img height="16" alt="Click Here to Pick up the date" src="../../images/cal.gif" width="16"
																	border="0" /></a></td>
            </tr>
                    <tr id="trgunnybag_type_category" runat="server" visible="false">
                        <td style="width: 147px; height: 18px">
                            <asp:Label ID="lbl_gunnybag_type_category" runat="server" Text="Gunny Bags Type/Category" Width="164px"></asp:Label></td>
                        <td style="height: 18px">
                            <asp:DropDownList ID="ddl_gunnybag_type_category" runat="server" 
                                Font-Size="XX-Small" Width="146px" TabIndex="4">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CVgunny" runat="server" ErrorMessage="Gunny" ControlToValidate="ddl_gunnybag_type_category" OnServerValidate="CVgunny_ServerValidate" ValidationGroup="SaveValid"></asp:CustomValidator></td>
                    </tr>
            <tr>
                <td style="width: 147px;">
                    <span style="font-size: 8pt">
                        <asp:Label ID="lblmadeupBagsNo" runat="server" Text="No. Of Made Up Bags"></asp:Label></span></td>
                <td style="width: 97px; height: 34px;">
                    <asp:TextBox ID="txtmadebags" runat="server" Width="132px" MaxLength="5" onblur="Spc_validatornumeric(this)" Font-Size="XX-Small" TabIndex="5" AutoComplete="off"></asp:TextBox><span
                        style="font-size: 7pt; color: #990000">*<asp:RequiredFieldValidator ID="RFVbags"
                            runat="server" ControlToValidate="txtmadebags" ErrorMessage=">" ValidationGroup="SaveValid"></asp:RequiredFieldValidator></span></td>
            </tr>
                    <tr>
                        <td style="width: 147px;">Quantity</td>
                        <td style="width: 97px; height: 34px;">
                            <asp:TextBox ID="txt_quantity"   runat="server" Font-Size="XX-Small" style="text-align:right" MaxLength="5" onblur="Spc_validatornumeric(this)" TabIndex="5" Width="132px" AutoComplete="off"></asp:TextBox>
                            Qtls.</td>
                    </tr>
                    <tr>
                        <td style="width: 147px; height: 19px;">
                            <asp:Label ID="lblMadeForDo" runat="server" Text="Made For Delivery"></asp:Label></td>
                        <td style="width: 97px; height: 19px;">
                            <asp:DropDownList ID="ddlMadeForDo" runat="server" Font-Size="XX-Small" Font-Bold="True" Width="62px" TabIndex="6">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Selected="True" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CVDel" runat="server" ErrorMessage="Delivery" ControlToValidate="txtmadebags" OnServerValidate="CVDel_ServerValidate" ValidationGroup="SaveValid"></asp:CustomValidator></td>
                    </tr>
            <tr>
                <td style="text-align: center; height: 32px;" colspan="2">
                    <asp:ImageButton ID="btnsave" runat="server" ImageUrl="~/images/SUBMIT.GIF"  TabIndex="7" ValidationGroup="SaveValid" OnClick="btnsave_Click" /></td>
            </tr>
        </table>
                            </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnsave" />
                        </Triggers>
                            </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 15px; background-color: dimgray">
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
    
    </div>

</asp:Content>

