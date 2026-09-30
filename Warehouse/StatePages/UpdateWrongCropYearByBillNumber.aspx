<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="UpdateWrongCropYearByBillNumber.aspx.cs" Inherits="BranchPages_UpdateWrongCropYearByBillNumber" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function CheckNumeric(e, tx) {
            var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
            if ((AsciiCode < 46 && AsciiCode != 8 && AsciiCode != 9) || (AsciiCode > 57)) {
                alert('Please enter only numbers !');
                return false;
            }

        }

        function isNumberKey(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes

            if (keycode > 31 && (keycode < 48 || keycode > 57) && keycode != 47) {
                alert(" You can enter only characters 0 to 9 ");
                return false;
            }
            else return true;
        }

        function isNumberKey2(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#inputfield").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function popMe(url) {
            var newWindow;
            newWindow = window.open(url, 'MyWin', 'width=275,height=390,top=1,left=1');

        }

        function chkgod() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_dprlst_Godown").value == "--Select--") {

                alert('Please Select Godown No')

            }
        }

    </script>
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="center" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblGodownMaster" runat="server" Text="Update Missing Crop Year" Font-Bold="true"
                                                                Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px; color: red; font-weight: bold;" colspan="4">जिन गोदाम के बिलों में DSC करते समय क्रॉप ईयर नही दिख रहा हो उसे यहाँ से अपडेट करें 
                                                            (क्रॉप ईयर अपडेट करते समय यह सुनिश्चित कर लें की जो क्रॉप ईयर अपडेट किया जा रहा है वह सही है|
                                                            क्रॉप ईयर गलत अपडेट होने की स्थिति में शाखा प्रबंधक जिम्मेदार होंगे|)</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label6" runat="server" Text="Crop Year : "></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>                                                               
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label7" runat="server" Text="Bill Month : "></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="false" class="form-control">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>                                                               
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td style="height: 5px" colspan="2"></td>
                                                        <td style="height: 5px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px; color:red; text-align:center;" colspan="2"> OR </td>
                                                        <td style="height: 5px" colspan="2"></td>
                                                    </tr>
                                                     <tr>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label8" runat="server" Text="Bill Number : "></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                           
                                                            <asp:TextBox ID="txtbillnumber" runat="server" class="form-control"></asp:TextBox>
                                                        </td>
                                                        
                                                    </tr>
                                                      <tr>
                                                        <td style="height: 5px" colspan="4"></td>
                                                    </tr>
                                                     <tr>
                                                         <td style="height: 5px; text-align:center;" colspan="4">
                                                              <asp:Button ID="Button1" runat="server" Text="Show" OnClick="Button1_Click" />
                                                        <%-- <asp:Button ID="Button1" runat="server" Text="View Details" Width="100px"
                                                    OnClick="Button1_Click" /></td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top" colspan="4">
                                                            <asp:GridView ID="godown_GridView" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False"
                                                                CellPadding="2" Width="100%" AllowPaging="True" AllowSorting="True"
                                                                OnSelectedIndexChanged="godown_GridView_SelectedIndexChanged"
                                                                OnPageIndexChanging="godown_GridView_PageIndexChanging" PageSize="100"
                                                                Font-Size="9pt">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.N.">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1%>
                                                                            <asp:HiddenField ID="hdnBillNumber" runat="server" Value='<%# Eval("Bill_Number") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                                                                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                                                    <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number" />
                                                                    <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" />
                                                                    <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" />
                                                                    <asp:BoundField DataField="Month" HeaderText="Month" />
                                                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="Edit">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" Text="Edit" OnClick="Edit"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                    Height="20px" Font-Size="10pt" />
                                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                                            </asp:GridView>
                                                            <asp:Label ID="Label2" runat="server" Font-Size="X-Small" ForeColor="#400040"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </center>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" colspan="2"></td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 50%; background-color: #FFFFE6;">
                            <asp:Label Font-Bold="true" ID="Label3" runat="server" Text="Bill Details"></asp:Label>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Godown ID" Font-Bold="true" ForeColor="navy"
                                            Font-Size="8pt"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblGodownID" runat="server" Text="Godown ID" Font-Bold="true" ForeColor="navy"
                                            Font-Size="8pt"></asp:Label></td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Bill Number" Font-Bold="true" ForeColor="navy"
                                            Font-Size="8pt"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblBillNumber" runat="server" Text="Bill Number" Font-Bold="true" ForeColor="navy"
                                            Font-Size="8pt"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Crop Year for Update" Font-Bold="true" ForeColor="navy"
                                            Font-Size="8pt"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlChangeCropYear" runat="server">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Value="2021-2022">2021-22</asp:ListItem>
                                            <asp:ListItem Value="2020-2021">2020-21</asp:ListItem>
                                            <asp:ListItem Value="2019-2020">2019-20</asp:ListItem>
                                            <asp:ListItem Value="2018-2019">2018-19</asp:ListItem>
                                            <asp:ListItem Value="2017-2018">2017-18</asp:ListItem>
                                            <asp:ListItem Value="2016-2017">2016-17</asp:ListItem>
                                            <asp:ListItem Value="2015-2016">2015-16</asp:ListItem>
                                            <asp:ListItem Value="2014-2015">2014-15</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="BTNBLUE" OnClick="Save" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button button2" OnClientClick="return Hidepopup()" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                        <asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
                            PopupControlID="pnlAddEdit" TargetControlID="lnkFake"
                            BackgroundCssClass="modalBackground">
                        </asp:ModalPopupExtender>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="godown_GridView" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </center>
    </fieldset>
    <asp:HiddenField ID="hdnBillNumber" runat="server" Value="0" />
</asp:Content>


