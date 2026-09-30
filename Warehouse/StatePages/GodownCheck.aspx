<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GodownCheck.aspx.cs" Inherits="StatePages_GodownCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Godown</title>
    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <div>
                                <div class="auto-style1">
                                    <a href="State_Welcome_DashBoard.aspx">Go Back</a>
                                </div>

                                <center>

                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Godown Details" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 50px;" colspan="8" align="center">&nbsp;&nbsp;&nbsp;&nbsp<b>Godown Id : </b>&nbsp;&nbsp;<%--<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                                                Height="25px" Width="168px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtGodownID"></asp:TextBox>

                                                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<%--<asp:DropDownList ID="ddlBranch" runat="server"
                                                    Height="25px" Width="168px" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                </asp:DropDownList>--%>

                                                <%--<asp:DropDownList ID="ddl_session" runat="server" Width="150px"  Height="25px" 
                                                onselectedindexchanged="ddl_session_SelectedIndexChanged" AutoPostBack="true" >
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                <asp:ListItem Value="Previous">Previous</asp:ListItem>
                                                <asp:ListItem Value="Rab2023_24">Rabi_2023_24</asp:ListItem>
                                                <asp:ListItem Value="Kharif_2022_23">Kharif_2022_23</asp:ListItem>
                                                </asp:DropDownList>--%>  
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button runat="server" Text="Check" ID="btnCheck" OnClick="btnCheck_Click" Height="25px" Width="168px" Font-Bold="true" />


                                            </td>

                                        </tr>
                                        <%--<tr>
                                            <td align="center" style="width: 200px">
                                                <asp:Button ID="Button1" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                                                    CssClass="BTNBLUE" OnClick="Button1_Click" />
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" Font-Size="15pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDistrict_Name" Width="100%" Text='<%# Eval("District_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Branch Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDepotName" Width="100%" Text='<%# Eval("DepotName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("Godown_ID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Registration No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownReg_ID" Width="100%" Text='<%# Eval("GodownReg_No")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JVS Registration No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblJVS_RegNo" Width="100%" Text='<%# Eval("JVS_RegNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Password">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownPwd" Width="100%" Text='<%# Eval("GodownPassword")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Capacity" Width="100%" Text='<%# Eval("Godown_Capacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%-- <ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Scientific Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Width="100%" Text='<%# Eval("Godown_Scientific_Capacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hired Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHired_Type" Width="100%" Text='<%# Eval("Hired_Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Active">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblIsActive" Width="100%" Text='<%# Eval("IsActive")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%-- <ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CreatedDate">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCreatedDate" Width="100%" Text='<%# Eval("CreatedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<ItemStyle HorizontalAlign="Left" Width="30%" />--%>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="12pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
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
                <%--  <asp:Panel ID="pnllogin" class="popup" runat="server">
                    <div class="pop" style="background-color: white; min-height: 300PX; max-height: 500px; width: 1000px; border: #008CBA; border-style: solid; border-width: 10px;">
                       


                        <div id="divNewInsp" runat="server" visible="true" style="width: 100%;">

                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 70px; font-size: 14px" colspan="4" align="center">
                                        <asp:Label ID="Label2" runat="server" Text="Godown Name : "></asp:Label>
                                        <asp:TextBox ID="lblgodownname" runat="server" Width="300px" Height="20px"></asp:TextBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr id="trmobtxt" runat="server" visible="true">
                                    <td style="height: 70px; font-size: 14px" colspan="4" align="center">
                                        <asp:Label ID="Label1" runat="server" Text="Godown ID : "></asp:Label>
                                        <asp:TextBox ID="txtGdwnID" runat="server" ReadOnly="true"
                                            Width="150px" Height="20px"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                      &nbsp; Depositor Name &nbsp;
                                                    <asp:DropDownList ID="ddlDepositor" runat="server" Width="155px" Height="25px" AutoPostBack="false">
                                                    </asp:DropDownList>
                                        &nbsp;
                                                <br />
                                    </td>
                                </tr>

                                <tr id="tr2" runat="server" visible="true">
                                    <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp; WHR No. &nbsp;
                                                    <asp:TextBox ID="txtwhrno" runat="server"
                                                        Width="300px" Height="20px"></asp:TextBox>

                                    </td>
                                </tr>


                                <tr>
                                    <td style="height: 5px" colspan="4"></td>
                                </tr>
                             
                            </table>
                            <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

                        </div>


                       
                    </div>
                    <img alt="New" src="images/new6.gif" id="new" runat="server" />

                </asp:Panel>--%>
                <%--<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin">
                </asp:ModalPopupExtender>--%>
            </div>
        </div>
        <%--    </form>--%>

        <%--    <form id="form2" runat="server">--%>

        

        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="lblGodownTxndtls" runat="server" Text="Godown Transaction Details" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>


                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="GV_Capacity" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" Font-Size="15pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("Godown_ID")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Godown Vacant Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Vacant_Capacity" Width="100%" Text='<%# Eval("Godown_Vacant_Capacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crop Year">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCropYear" Width="100%" Text='<%# Eval("CropYear")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="WHR Quantity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCropYear" Width="100%" Text='<%# Eval("WHR_Quantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="12pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
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
        </div>
        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="Label1" runat="server" Text="Godown Beneficiary Details" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>


                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="grdBeneficiarydetails" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" Font-Size="15pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("Godown_ID")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="JVS_RegNo">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblJVS_RegNo" Width="100%" Text='<%# Eval("JVS_RegNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Registration_Id">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblRegistration_Id" Width="100%" Text='<%# Eval("Registration_Id")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblMobile" Width="100%" Text='<%# Eval("Mobile")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Beneficiary_Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBeneficiary_Name" Width="100%" Text='<%# Eval("Beneficiary_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="12pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
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
        </div>
        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin-top: 50px">
                                        <tr style="background-color: #5da7a7; height: 25px; margin-top: 50px;">
                                            <td colspan="8" align="center ">
                                                <asp:Label ID="lblGAgreementDtls" runat="server" Text="Godown Agreement Details" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 50px;" colspan="8" align="center">&nbsp;&nbsp;&nbsp;&nbsp<b>Registration Id: </b>&nbsp;&nbsp;
                                                <asp:TextBox runat="server" ID="txtRegistrationID"></asp:TextBox>

                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblSeason" runat="server" Font-Bold="true" Text="Season:"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlSeason" runat="server" Width="150px" Height="25px"
                                                    OnSelectedIndexChanged="ddlSeason_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="Previous">Previous</asp:ListItem>
                                                    <asp:ListItem Value="Kharif2023_24">Kharif2023_24</asp:ListItem>
                                                    <asp:ListItem Value="Rab2023_24">Rabi_2023_24</asp:ListItem>
                                                    <asp:ListItem Value="Kharif_2022_23">Kharif_2022_23</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button runat="server" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" Height="25px" Width="168px" Font-Bold="true" />
                                        </tr>

                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="grdGodownOtherDetails" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" Font-Size="15pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Godown ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("GodownID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Registration No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownReg_ID" Width="100%" Text='<%# Eval("GodownReg_No")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Offer Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownOfferCapacity" Width="100%" Text='<%# Eval("GodownOfferCapacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Vacant Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownVacantCapacity" Width="100%" Text='<%# Eval("GodownVacantCapacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Agreement Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownAgreementCapacity" Width="100%" Text='<%# Eval("GodownAgreementCapacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Fit_Unfit">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownAgreementCapacity" Width="100%" Text='<%# Eval("Fit_Unfit")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insp_Created_Date">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodownAgreementCapacity" Width="100%" Text='<%# Eval("Insp_Created_Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Godown Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Capacity" Width="100%" Text='<%# Eval("Godown_Capacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                           <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="12pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
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
        </div>

        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">
                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin-top: 50px">
                                        <tr style="background-color: #ac8abd; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="lblPaymentDetails" runat="server" Text="Godown Payment Details" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>

                                        <tr>
                                            <td colspan="8" valign="top" align="center" style="padding-top: 22px">
                                                <asp:GridView ID="grdPaymentRegDtls" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    Width="900px" DataKeyNames="Registration_Id" Font-Size="15pt">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="क्रमांक">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Registration_Id" HeaderText="रजिस्ट्रेशन आईडी" SortExpression="Registration_Id" />
                                                        <asp:BoundField DataField="RegCapacity" HeaderText="रजिस्ट्रेशन की छमता" SortExpression="RegCapacity" />
                                                        <asp:BoundField DataField="RegAmt" HeaderText="रजिस्ट्रेशन की जमा की जाने वाली राशि" SortExpression="RegAmt" />
                                                        <asp:BoundField DataField="DepositedRegAmt" HeaderText="रजिस्ट्रेशन की जमा की गई राशि" SortExpression="DepositedRegAmt" />
                                                        <asp:BoundField DataField="RegPaymentStatus" HeaderText="रजिस्ट्रेशन की स्थिति" SortExpression="RegPaymentStatus" />
                                                        <asp:BoundField DataField="Offer_Capacity" HeaderText="आफर की गई छमता" SortExpression="Offer_Capacity" />
                                                        <asp:BoundField DataField="OfferAmt" HeaderText="आफर की जमा की जाने वाली राशि" SortExpression="OfferAmt" />
                                                        <asp:BoundField DataField="ofrdepositedamt" HeaderText="आफर की जमा की गई राशि" SortExpression="ofrdepositedamt" />
                                                        <asp:BoundField DataField="OfferPaymentStatus" HeaderText="आफर की स्थिति" SortExpression="OfferPaymentStatus" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Height="25px" Font-Size="12pt" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="30px" Font-Size="12pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                        </td>
                </table>
            </div>
        </div>

        <div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="lblGdnStock" runat="server" Text="Godown Stock details" Font-Bold="true"
                                                    Font-Size="15pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 50px;" colspan="8" align="center">&nbsp;&nbsp;&nbsp;&nbsp<b>  Registration Id : </b>&nbsp;&nbsp;
                                                <asp:TextBox runat="server" ID="txtWRegID"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;
                                                <asp:Button runat="server" Text="Check" ID="btnCheckWHCapacity" OnClick="btnCheckWHCapacity_Click" Height="25px" Width="168px" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="GV_GdnCapacity" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDistrict_Name" Width="100%" Text='<%# Eval("DistrictID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Branch ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDepotName" Width="100%" Text='<%# Eval("BranchID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_ID" Text='<%# Eval("GodownID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JVS RegistrationID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblRegistrationID" Text='<%# Eval("JVS_RegNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("GodownName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Godown Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Capacity" Text='<%# Eval("GodownCapacity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Weight Balance">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Text='<%# Eval("WeightBalance")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Filled Percentage">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Text='<%# Eval("Percentage")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
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
        </div>
    </form>
</body>
</html>
