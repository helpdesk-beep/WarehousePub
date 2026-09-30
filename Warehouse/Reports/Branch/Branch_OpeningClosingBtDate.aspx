<%@ Page Language="C#"  EnableEventValidation="false" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Branch_OpeningClosingBtDate.aspx.cs" Inherits="IssueCenterLevel_Storage_Branch_OpeningClosingBtDate" Title="Branch Opening Closing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 10px;
        }
    </style>
</asp:Content>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset style="width: 900px; border: 2px solid navy;  margin-left: 15px ; margin-right:15px">
        <center>
       
                    <div style="margin-top:5px">
                        <table cellpadding="0" cellspacing="0" style="width: 99%; ">
                            <tr style="background-color: #0bb6e6; height: 25px; ">
                                <td colspan="4" align="center" style="border:1px ; border-style:solid ; border-width :1px; border-color:Navy">
                                    <asp:Label ID="lblhead" runat="server" Text="Opening Closing" Font-Size="10pt"
                                        ForeColor="whitesmoke" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td style="height:10px"></td>
                            </tr>
                             <tr>
                                <td align="left">
                                  <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Note : MPSCSC Stock Opening Closing"
                                  Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>                                                        
                            <tr>
                               <td class="style1"></td>
                            </tr>
                            <tr>
                                <td align="center" >
                            <table>
                            <tr>
                               <td align="left">
                                   <asp:Label ID="Label2" runat="server" Text="Commodity" Font-Size="8pt"
                                    ForeColor="navy" Font-Bold="true"></asp:Label>
                               </td >  
                               <td align="left">
                                    <asp:DropDownList ID="dprlst_Commodity" runat="server" Width="150px" 
                                        AutoPostBack="true" 
                                        onselectedindexchanged="dprlst_Commodity_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="22">Wheat-PSS</asp:ListItem>
                                        <asp:ListItem Value="3">Rice-Row-Common</asp:ListItem>
                                        <asp:ListItem Value="13">Paddy-Common</asp:ListItem>
                                </asp:DropDownList>
                                </td>                                                         
                               <td align="left">
                                     <asp:Label ID="lbldate" runat="server" Text="From Date" Font-Size="8pt"
                                     ForeColor="navy" Font-Bold="true"></asp:Label>
                               </td >
                <td style="width: 149px;" align="left">
                <asp:TextBox ID="fromDate" runat="server" Width="111px"></asp:TextBox>
                    <a onclick="ShowCalendar(form1.fromDate,form1.fromDate);" href="javascript:;">
                    <img height="16" id="imgProc" runat="server" alt="Click Here to Pick up the date" 
                    src="../../images/cal.gif" width="16"	border="0" /></a>
                                            <cc1:CalendarExtender ID="txtDepositDate_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="fromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                            </cc1:CalendarExtender>                
                </td>
                                                                                                    <td align="left">
                                                        <asp:Label ID="Label1" runat="server" Text="To Date" Font-Size="8pt"
                                                                ForeColor="navy" Font-Bold="true"></asp:Label>
                                                        </td >
                <td style="width: 149px;" align="left">
                <asp:TextBox ID="todate" runat="server" Width="111px"></asp:TextBox>
                    <a onclick="ShowCalendar(form1.fromDate,form1.fromDate);" href="javascript:;">
                    <img height="16" id="img1" runat="server" alt="Click Here to Pick up the date" 
                    src="../../images/cal.gif" width="16"	border="0" /></a>
                  <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                       Enabled="True" TargetControlID="todate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                  </cc1:CalendarExtender>                     
                
                </td> 
                
                                <td align="left" style="width:100px; height:15px">
                    &nbsp;<asp:Button ID="btnViewReport" runat="server" Text="View Report" 
                                 onclick="btnViewReport_Click" /></td>
                          <td align="left" style=" height:15px">
                    &nbsp;<asp:Button ID="Button1" runat="server" Text="Export To Excel" 
                                  Width="108px" /></td>
                
                            </tr>
                            </table>
                            </td>
                            </tr>
                             <tr>
                                <td style="height:10px"></td>
                            </tr>
                            <tr>
                               <td colspan="4" align="center" valign="top">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:GridView ID="gv_whr" runat="server" AutoGenerateColumns="False" Width="100%"
                                        Font-Size="10pt" DataKeyNames="DateCorrect">
                                        <Columns>
                                            <asp:BoundField DataField="DateCorrect" HeaderText="Date">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                           <asp:BoundField DataField="OpneBal" HeaderText="Opening Balance">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RecWeight" HeaderText="Receive Quantity">
                                                <ItemStyle  HorizontalAlign="Center" VerticalAlign="Bottom" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IssueWeight" HeaderText="Issue Quantity">
                                                <ItemStyle  HorizontalAlign="Center" VerticalAlign="Bottom" />
                                            </asp:BoundField> 
                                            <asp:BoundField DataField="ClosingBl" HeaderText="Closing Balance">
                                                <ItemStyle  HorizontalAlign="Center" VerticalAlign="Bottom" />
                                            </asp:BoundField>                                                                                     
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                            Height="25px" Font-Size="9pt" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                              <td class="style1"></td>
                           </tr>

                       </table>
                   </div>
           </center>
   </fieldset>
</asp:Content>

