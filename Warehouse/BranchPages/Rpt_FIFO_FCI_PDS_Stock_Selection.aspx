<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Rpt_FIFO_FCI_PDS_Stock_Selection.aspx.cs" Inherits="BranchPages_Rpt_FIFO_FCI_PDS_Stock_Selection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function HeaderClick(CheckBox) {

            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.GV_Fifo.ClientID %>');

            var TargetChildControl = "chk_Sum";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.er
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                    Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;

            var txtTotalRecBags = 0;
            var txtTotalRecQty = 0;
            var txtTotalcharges = 0;
            var txttTotalGSTAmt = 0.0;
            var txttTotalSupcharges = 0.0;
            var txttTotalGSTPer = 0.0;
            var Check = "N";
            var checkBoxes = 0;
            //checkBoxes = (1.toString();
            var grid = document.getElementById("<%= GV_Fifo.ClientID%>");
            var CheckCount = 1;
            for (var i = 0; i < grid.rows.length - 1; i++) {


                var txtAmount = 0.0;
                var txtcharges = 0.0;
                var txtGSTAmt = 0.0;
                var txtSupcharges = 0.0;
                var txtGSTPer = 0.0;
                txtcharges = $("input[id*=txtcharges]")

                var txtQtyReceive = 0.0;
                var checkBoxes = $("input[id*=chk_Sum]")
                if (checkBoxes[i].checked == true) {
                    txtTotalcharges = txtTotalcharges + parseFloat(txtcharges[i].value);
                    CheckCount = grid.rows.length;


                }

            }



        }
        function calculate() {

            var txtTotalRecBags = 0;
            var txtTotalRecQty = 0;
            var CheckCount = 0;
            var txtTotalcharges = 0;
            var txttTotalGSTAmt = 0.0;
            var txttTotalSupcharges = 0.0;
            var txttTotalGSTPer = 0.0;
            var grid = document.getElementById("<%= GV_Fifo.ClientID%>");
            for (var i = 0; i < grid.rows.length - 1; i++) {

                var txtBagSend = $("input[id*=sendb]")
                var txtNetQty = 0.0;
                var txtcharges = 0.0;
                var txtGSTAmt = 0.0;
                var txtSupcharges = 0.0;
                var txtGSTPer = 0.0;
                txtcharges = $("input[id*=txtcharges]")

                var txtQtyReceive = 0.0;

                var checkBoxes = $("input[id*=chk_Sum]")
                if (checkBoxes[i].checked == true) {

                    txtTotalcharges = txtTotalcharges + parseFloat(txtcharges[i].value);

                    CheckCount = CheckCount + 1;

                }
            }

        }
    </script>
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>

            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="4" align="center">
                            <asp:Label ID="lblhead" runat="server" Text="Selection of Stock for FIFO(FCI/PDS)" Font-Size="12pt"
                                ForeColor="whitesmoke" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4" style="height: 5px">
                            <h4 style="color:red; font-size:larger;">नोट:- आपके द्वारा फ्रिज किये गए स्टॉक के आधार पर AePDS/CSMS सॉफ्टवेयर में भुगतान संबंधित ऑनलाइन कार्य किया जा रहा हैं ,कृप्या ध्यान पूर्वक डिलीट करे |</h4>
                        </td>
                    </tr>
                    <tr id="tr1" runat="server" visible="true">
                        <td align="left">
                            <asp:Label ID="Label1" runat="server" Text="Dispatch Category" ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                            <asp:DropDownList ID="ddlDispatchCategory" runat="server" Width="305px" Height="25px" TabIndex="4"
                                CssClass="tb6" OnSelectedIndexChanged="ddlDispatchCategory_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem>
                                <%-- <asp:ListItem Value="F" Text="FCI"></asp:ListItem>
                                <asp:ListItem Value="P" Text="PDS"></asp:ListItem>--%>
                                <asp:ListItem Value="F" Text="Freeze for FCI Dispatch"></asp:ListItem>
                                <asp:ListItem Value="P" Text="Freeze for PDS Dispatch"></asp:ListItem>
                                <asp:ListItem Value="E" Text="Excess Stock"></asp:ListItem>
                                <asp:ListItem Value="D" Text="Dead Stock"></asp:ListItem>
                                <asp:ListItem Value="S" Text="Sellout Stock"></asp:ListItem>
                                <asp:ListItem Value="O" Text="Other Stock"></asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <%-- </tr>
                               <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr id="trgdnlist" runat="server" visible="true">--%>

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
                        <td colspan="4" style="height: 5px"></td>
                    </tr>

                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text=""
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px"></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" valign="top">
                            <asp:GridView ID="GV_Fifo" runat="server" AutoGenerateColumns="False" Width="100%"
                                Font-Size="10pt" DataKeyNames="whr_id">
                                <Columns>
                                    <asp:TemplateField HeaderText="क्रमांक">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("whr_id") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name">
                                        <ItemStyle Width="120px" HorizontalAlign="center" />
                                        <HeaderStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID">
                                        <ItemStyle Width="120px" HorizontalAlign="center" />
                                        <HeaderStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="whr_id" HeaderText="WHR No.">
                                        <ItemStyle Width="120px" HorizontalAlign="center" />
                                        <HeaderStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name">
                                        <ItemStyle Width="100px" HorizontalAlign="center" />
                                        <HeaderStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="SStatus" HeaderText="Dispatch Category">
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkBxHeader" Text="All" onclick="javascript:HeaderClick(this);" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_Sum" runat="server" />
                                            <asp:HiddenField ID="hdncheckID" runat="server" Value='<%# Eval("whr_id") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                            Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        <ControlStyle Width="15px" />
                                    </asp:TemplateField>
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
                        <td style="height: 15px"></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnsave" runat="server" Text="Delete" Width="120px" CssClass="BTNBLUE"
                                ValidationGroup="SaveValid" OnClick="btnsave_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="120px" CssClass="BTNBLUE"
                                        CausesValidation="false" Visible="false" OnClick="btnPrint_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="SaveValid" />
                        </td>
                    </tr>
                </table>
            </div>

        </center>
    </fieldset>
</asp:Content>

