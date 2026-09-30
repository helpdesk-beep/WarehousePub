<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Stock_Reconcilation_2022.aspx.cs" Inherits="Reports_Branch_Stock_Reconcilation_2022" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }
    </style>
    <fieldset style="width: 800px; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
        <center>
             <div style="text-align: center; font-size: x-large; color: red;">
            31 August 2022 की स्थति में Online स्टॉक Position का फिजिकल उपलब्ध स्टॉक से परीक्षण
            <br />
           
        </div>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>

                                        <tr>
                                            <td colspan="4" valign="top" align="center">

                                                <asp:GridView ID="Depositor_Gridview" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" 
                                                    OnRowUpdating="Depositor_Gridview_RowUpdating">
                                                    <Columns>
                                                        <asp:TemplateField>
                                         <HeaderTemplate>  
                                        
                                               <th style="text-align: center;">क्र.</th>
                                               <th style="text-align: center;">Godown Name</th>
                                               <th style="text-align: center;">Godown ID</th>  
                                              <th style="text-align: center;">Bag Balance</th>  
                                              <th style="text-align: center;">Weight Balance</th>  
                                              
                                             <th style="text-align: center;">Physical Weight Balance</th>
                                             <th style="text-align: center;">Remark</th>
                                               <th style="text-align: center;">Update </th> 
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>                                                    
                       
                        <td style="text-align: center;"><%# Container.DataItemIndex + 1 %></td>
                        
                        <td style="text-align: center;"><asp:Label ID="Godown_Name" runat="server" Text='<%#Eval("Godown_Name") %>'/> </td>
                        <td style="text-align: center;"><asp:Label ID="Godown_ID" runat="server" Text='<%#Eval("Godown_ID") %>'/> </td>
                        <td style="text-align: center;"><asp:Label ID="BagBalance" runat="server" Text='<%#Eval("BagBalance") %>'/> </td>
                          <td style="text-align: center;"><asp:Label ID="WeightBalance" runat="server" Text='<%#Eval("WeightBalance") %>'/> </td>
                         
                                                <td style="text-align: center;">
                                                 <asp:TextBox runat="server" ID="txtPhysical_Waight_Balances"  ></asp:TextBox>
                                             </td>
                                            <td style="text-align: center;">
                                                 <asp:TextBox runat="server" ID="txtRemark"  TextMode="MultiLine"></asp:TextBox>
                                             </td>
                             </td>
                         <td style="text-align: center;"><asp:Button ID="btn_Update" runat="server" Text="Update"  CommandName="Update"/></td>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                       <%-- <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <%--<asp:BoundField DataField="District_Name" HeaderText="District" SortExpression="District_Name">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Branch" HeaderText="Branch" SortExpression="Branch">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>--%>
                                                       <%-- <asp:BoundField DataField="Godown_Name" HeaderText="Godown" SortExpression="Godown_Name">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" SortExpression="DepositerNo">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="BagBalance" HeaderText="Bag Balance" SortExpression="Acceptance_No">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="WeightBalance" HeaderText="Weight Balance" SortExpression="Acceptance_Date">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>--%>
                                                         
                                                        <%--<asp:BoundField DataField="Online_Avl_Balances" HeaderText="Online Avl Balances" SortExpression="TC_Number">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>--%>
                                                       <%-- <asp:BoundField DataField="Truck_Number" HeaderText="Truck Number" SortExpression="Truck_Number">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Send Bags">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="sendb" BackColor="Transparent" Enabled="false" Font-Bold="true" runat="server" Width="60px" MaxLength="5" onblur="Spc_validatorInt(this)" Text='<%# Eval("Recd_Bags") %>'>0</asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="80px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Send Qty.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="sendq" BackColor="Transparent" Enabled="false" Font-Bold="true" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,5)"
                                                                    onblur="Spc_validatornumeric(this)" Text='<%# Eval("Recd_Qty") %>'>0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <%--<td>
                                                        <asp:TextBox runat="server" ID="txtRemark"  TextMode="MultiLine"></asp:TextBox></td>--%>
                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center" Wrap="true"
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
        </center>
    </fieldset>
</asp:Content>

