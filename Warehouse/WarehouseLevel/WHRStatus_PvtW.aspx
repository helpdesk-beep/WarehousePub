<%@ Page Language="C#" MasterPageFile="~/MasterPage/PrivateWarehouse.master" AutoEventWireup="true" CodeFile="WHRStatus_PvtW.aspx.cs" Inherits="WarehouseLevel_WHRStatus_PvtW" Title="WHR Status" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <style type="text/css">
       
           .fixedHeader
{

font-weight:bold;

position:absolute;

background-color: #006699;

color: #ffffff;

height: 25px;

top: expression(Sys.UI.DomElement.getBounds(document.getElementById("panelContainer")).y-25);

}
        }     
    </style>  
     
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="6" align="center" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="6" align="center">
                                                            <asp:Label ID="lblheadnew" runat="server" Text="WHR Details" ForeColor="whitesmoke"
                                                                Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="6"><p>
                                                            जिन WHR मे balance है केबल उन्ही के records यहा दिखाये गए हैं nill हो चुके WHR 
                                                            की detail देखने के लिए ब्रांच रिपोर्ट में 
                                                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#6600FF" 
                                                                onclick="LinkButton1_Click">whr issued and cancelled register</asp:LinkButton> 
                                                            देखें</p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" align="left">
                                                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                                                Font-Size="10pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="6">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" align="center">
                                                            <asp:Label ID="lblmsg" runat="server" Font-Size="10pt" ForeColor="Red" EnableViewState="False"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" align="center">
                                                            <asp:Panel ID="panelContainer" runat="server" Height="500px" ScrollBars="Vertical">
                                                                <asp:GridView ID="GV_WHRDETAILS" runat="server" CellPadding="2"
                                                                    Width="80%" Font-Size="10pt" HorizontalAlign="Center" 
                                                                    AutoGenerateColumns="False" >
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="WHR ID" />
                                                                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name" />
                                                                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                                                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" />
                                                                        <asp:BoundField DataField="Bags" HeaderText="Bags" />
                                                                        <asp:BoundField DataField="Weight" HeaderText="Weight" />
                                                                        <asp:BoundField DataField="Stack_Name" HeaderText="Stack" />
                                                                        <asp:BoundField DataField="WHRDATE" HeaderText="WHRDATE" />
                                                                        <asp:BoundField DataField="Issuebags" HeaderText="Issuebags" />
                                                                        <asp:BoundField DataField="Issueweigt" HeaderText="Issueweigt" />
                                                                        <asp:BoundField DataField="avilableQty" HeaderText="avilableQty" />
                                                                        <asp:BoundField DataField="avilableBags" HeaderText="avilableBags" />
                                                                    </Columns>
                                                                    <EditRowStyle Font-Strikeout="False" HorizontalAlign="Center" 
                                                                        VerticalAlign="Middle" />
                                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                        Height="20px" Font-Size="10pt" />
                                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </fieldset>
</asp:Content>

