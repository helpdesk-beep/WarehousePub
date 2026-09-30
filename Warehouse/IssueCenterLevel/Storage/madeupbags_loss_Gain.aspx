<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true"
    CodeFile="madeupbags_loss_Gain.aspx.cs" Inherits="IssueCenterLevel_Storage_madeupbags_loss_Gain" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function CheckIsNumeric(tx) {
            var AsciiCode = event.keyCode;
            var txt = tx.value;
            var txt2 = String.fromCharCode(AsciiCode);
            var txt3 = txt2 * 1;
            if ((AsciiCode < 46) || (AsciiCode > 57)) {
                alert('Please enter only numbers.');
                event.cancelBubble = true;
                event.returnValue = false;
            }

            var num = tx.value;
            var len = num.length;
            var indx = -1;
            indx = num.indexOf('.');
            if (indx != -1) {
                var coda = event.keyCode
                if (coda == 46) {
                    alert('Decimal cannot come twice');
                    event.cancelBubble = true;
                    event.returnValue = false;
                }
                var dgt = num.substr(indx, len);
                var count = dgt.length;
                //alert (count);
                if (count > 5) {
                    alert("Only 5 decimal digits allowed");
                    event.cancelBubble = true;
                    event.returnValue = false;
                }
            }

        }
        function IsNumericProcQty(key, txt) {
            var keycode = (key.which) ? key.which : key.keyCode;
            var num = txt.value;
            var len = num.length;
            var indx = -1;
            indx = num.indexOf('.');

            var dgt = num.substr(indx, len);
            var count = dgt.length;

            if (keycode == 08) {
                return true;
            }
            else if (keycode == 59 || keycode == 32) {
                alert('Semi Colon (;) & Blank Space Not Allowed ...');
                return false;
            }
            else if (keycode == "36" || keycode == "37" || keycode == "38" || keycode == "40" || keycode == "41" || keycode == "43" || keycode == "92" || keycode == "124" || keycode == "34" || keycode == "39" || keycode == "60" || keycode == "62" || keycode == "44" || keycode == "64" || keycode == "61" || keycode == "63") {
                alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
                return false;
            }

            else if (keycode == 09) {
                if (num > 999) {
                    alert(' मात्रा 1000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                    txt.value = "0";
                    return false;
                }
            }

            else if (num > 999) {
                alert('मात्रा 1000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                txt.value = "0";
                return false;
            }
            else if (count > 5) {
                alert("दशमलव के बाद 5 अंक ही आ सकते है");
                return false;
            }

            else if (keycode == 46) {
                if (num.split(".").length > 1) {
                    alert('दशमलव एक ही बार आ सकता है |');
                    return false;
                }
            }
            else if (keycode >= 48 && keycode <= 58) {
                return true;
            }
            else {
                alert('कृपया संख्या ही प्रविष्ट करें |');
                return false;
            }
        }

    </script>

    <fieldset style="height: 550px; width: 960px; border: 1px solid navy; box-shadow: 1px 2px 8px;
        border-radius: 10px 10px 10px 10px; padding-left: 0px; margin-left: 15px">
        <center>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="center" valign="top">
                                    <fieldset style="width: 930px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 30px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblMadeupheader" runat="server" Text="Loss Gain Entry Form (WHR Wise)"
                                                                Font-Size="15px" Font-Bold="true" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 20px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 150px">
                                                            <asp:Label ID="lblDepositorType" runat="server" ForeColor="navy" Font-Size="12px"
                                                                Font-Bold="true" Text="Commodity"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:DropDownList ID="ddl_cmdty" runat="server" Width="200px" OnSelectedIndexChanged="ddl_cmdty_SelectedIndexChanged"
                                                                AutoPostBack="True" Height="30px" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" style="width: 150px">
                                                            <asp:Label ID="Label1" runat="server" ForeColor="navy" Font-Size="12px" Font-Bold="true"
                                                                Text="Godown No."></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:DropDownList ID="ddl_godown" runat="server" Width="200px" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged"
                                                                AutoPostBack="True" Height="30px" CssClass="tb6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label2" runat="server" ForeColor="navy" Font-Size="12px" Font-Bold="true"
                                                                Text="Stack No."></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddl_stackno" runat="server" Width="200px" Height="30px" CssClass="tb6"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddl_stackno_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label3" runat="server" ForeColor="navy" Font-Size="12px" Font-Bold="true"
                                                                Text="WHR No."></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddl_whrno" runat="server" Width="200px" Height="30px" 
                                                                CssClass="tb6" AutoPostBack="true" onselectedindexchanged="ddl_whrno_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label5" runat="server" ForeColor="navy" Font-Size="12px" Font-Bold="true"
                                                                Text="Type"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:RadioButtonList ID="rbtflag" runat="server" Font-Size="8pt" Font-Bold="true"
                                                                ForeColor="Navy" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="L">Loss</asp:ListItem>
                                                                <asp:ListItem Value="G">Gain</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label4" runat="server" ForeColor="navy" Font-Size="12px" Font-Bold="true"
                                                                Text="Date Of Collection"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_collectiondate" runat="server" Width="195px" CssClass="tb6"
                                                                Height="20px" MaxLength="10"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txt_collectiondate_CalendarExtender" runat="server" Enabled="True"
                                                                TargetControlID="txt_collectiondate" Format="dd/MM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label7" runat="server" ForeColor="navy" Font-Size="12px" Font-Bold="true"
                                                                Text="Bags"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_bags" runat="server" onblur="Spc_validatornumeric(this)" Height="20px"
                                                                Width="100px" CssClass="tb6"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label6" runat="server" ForeColor="navy" Font-Size="12px" Font-Bold="true"
                                                                Text="Quantity"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_Gain" runat="server" onblur="Spc_validatornumeric(this)" Height="20px"
                                                                Width="100px" CssClass="tb6"></asp:TextBox>
                                                            Qtls.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:GridView ID="gvwhr" runat="server" DataKeyNames="Depositor_WHR_Id" AutoGenerateColumns="False"
                                                                CellPadding="4" CellSpacing="2" Width="100%" GridLines="Both" Font-Size="9pt"
                                                                BackColor="LemonChiffon" CssClass="Gridview" ForeColor="Navy">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="WHR ID">
                                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor">
                                                                        <ItemStyle HorizontalAlign="Left" Width="350px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown No.">
                                                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Stack_Name" HeaderText="Stack No.">
                                                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Recied_Bags" HeaderText="Received Bags">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Recivedqty" HeaderText="Received Qty">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DelBags" HeaderText="Delivered Bags">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DelQty" HeaderText="Delivered Qty">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="loss" HeaderText="Loss Qty">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="gain" HeaderText="Gain Qty">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Availwet" HeaderText="Available Qty">
                                                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <AlternatingRowStyle BackColor="#dff0d8" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 50px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="4">
                                                            <asp:Button ID="btnsave" runat="server" Text="Save" Width="100px" OnClick="btnsave_Click"
                                                                CssClass="BTNBLUE" />
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                                                CausesValidation="false" OnClick="btn_Close_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="4">
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                                ShowSummary="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 5px">
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
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnsave" />
                </Triggers>
            </asp:UpdatePanel>
        </center>
    </fieldset>
</asp:Content>
