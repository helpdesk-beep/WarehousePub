<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Business_Prapatra_III.aspx.cs" Inherits="Accounting_frm_Business_Prapatra_III" Title="Prapatra III" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-control {
            width: 50%;
            height: 30px;
            padding: 6px 12px;
            background-color: #fff;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <div>
                    <h3>मध्य प्रदेश वेयरहाउसिंग एंड लॉजिस्टिक्स कॉर्पोरेशन, शाखा :-
                        <asp:Label ID="lblbranchname" runat="server"></asp:Label>
                    </h3>
                    <h5>खरीफ विपणन वर्ष 
                         <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="false"
                             Width="80px">
                             <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                             <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                             <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                             <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                             <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                         </asp:DropDownList>
                        में कैप की क्षमता एवं भंडारित मात्रा की जानकारी प्रतिदिवास</h5>
                </div>
                <table style="width: 100%; border: 1px solid navy;">
                    <tr id="msg">
                        <td colspan="4">
                            <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
                    </tr>


                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="कैप /शेड स्थल का नाम "></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:DropDownList ID="ddlcapshedname" runat="server" Height="20px" AutoPostBack="false">
                            </asp:DropDownList></td>
                    </tr>
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="8" align="left">
                            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke"
                                Text="केप / शेड की वास्तविक कुल क्षमता (MT)"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="पक्का केप(A)"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtpakkacap1" runat="server" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtpakkacap1_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="मंडी शेड(B)"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtmandished1" Text="0" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtmandished1_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="कच्चा केप(C)"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtkachchacap1" Text="0" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtkachchacap1_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="कुल केप क्षमता (A+B+C)"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txttotal123" runat="server" Text="0" class="form-control" onkeypress="return NumberOnly(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="8" align="left">                           
                            <asp:Label runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke">कल दिनांक</asp:Label>
                            <asp:TextBox ID="txtIntimationRegDate" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtIntimationRegDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                ErrorMessage="*" ValidationGroup="A" ForeColor="WhiteSmoke" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIntimationRegDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                            <asp:Label runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke">को भंडारित मात्रा</asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="पक्का केप(A)"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtpakkacap2" runat="server" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtpakkacap2_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="मंडी शेड(B)"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtmandished2" Text="0" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtmandished2_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="कच्चा केप(C)"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtkachchacap2" Text="0" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtkachchacap2_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="कुल केप क्षमता (A+B+C)"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txttotal456" runat="server" Text="0" class="form-control" onkeypress="return NumberOnly(event);"></asp:TextBox>
                        </td>
                    </tr>

                     <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="8" align="left">                           
                            <asp:Label runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke">आज दिनांक</asp:Label>
                            <asp:TextBox ID="txttodaydate" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txttodaydate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                ErrorMessage="*" ValidationGroup="A" ForeColor="WhiteSmoke" />
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttodaydate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                            <asp:Label runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke">को भंडारित मात्रा</asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="पक्का केप(A)"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtpakkacap3" runat="server" Text="0" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtpakkacap3_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="मंडी शेड(B)"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtmandished3" Text="0" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtmandished3_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="कच्चा केप(C)"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtkachchacap3" Text="0" runat="server" class="form-control" onkeypress="return NumberOnly(event);" OnTextChanged="txtkachchacap3_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="कुल केप क्षमता (A+B+C)"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txttotal789" runat="server" Text="0" class="form-control" onkeypress="return NumberOnly(event);"></asp:TextBox>
                        </td>
                    </tr>
                  
                   
                    <tr>
                        <td colspan="8" align="center">
                            <asp:Button ID="btnSumbmitRent" runat="server" ValidationGroup="A" Text="Save" CssClass="BTNBLUE" Width="100px" OnClick="btnSumbmitRent_Click" />
                        </td>
                    </tr>

                </table>
                <div style="width: 1000px; overflow: auto;">

                    <asp:GridView ID="GrdPrapatraI" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Arial"
                        BorderStyle="Double" CellPadding="3" CellSpacing="5" ShowFooter="true"
                        Font-Size="11px" BorderColor="#CCCCCC" OnRowCreated="GrdPrapatraI_RowCreated">
                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText="1">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Commodity_Name" HeaderText="1"></asp:BoundField>--%>

                            <asp:BoundField DataField="Godown_Name" HeaderText="2" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Pakkacap1" HeaderText="3" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Mandished1" HeaderText="4" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Kachchacap1" HeaderText="5" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Total1" HeaderText="6" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Pakkacap2" HeaderText="7" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Mandished2" HeaderText="8" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Kachchacap2" HeaderText="9" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Total2" HeaderText="10" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Pakkacap3" HeaderText="11" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Mandished3" HeaderText="12" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Kachchacap3" HeaderText="13" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Total3" HeaderText="14" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            
                        </Columns>
                        <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                    </asp:GridView>

                </div>
            </div>
        </center>
    </fieldset>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript">
        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode;
            if ((charCode >= 48 && charCode <= 57)) {
                return true;
            }
            if (charCode == 46) { return true; }
            if (charCode == 8) { return true; }
            if (charCode == 9) { return true; }
            else { return false; }
        }
    </script>
</asp:Content>

