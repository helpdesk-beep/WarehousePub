<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="PrintGodownMasterByID.aspx.cs" Inherits="Masters_PrintGodownMasterByID" %>

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
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblGodownMaster" runat="server" Text="Print Godown  Master" Font-Bold="true"
                                                        Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2">
                            <img alt="New" src="images/new6.gif" id="new" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="2"></td>

                    </tr>

                    <tr id="PanelGodown" runat="server" visible="True">
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td style="height: 5px" colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_mobile" runat="server" Text="Godown Name" Font-Bold="True" ForeColor="Navy"
                                                                Font-Size="8pt"></asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddllGodown" runat="server" Width="155px" Height="25px" OnSelectedIndexChanged="ddllGodown_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="height: 5px" colspan="2"></td>

                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="2"></td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="2"></td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="2">
                            <asp:ValidationSummary ID="godown_Validationerror" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin" CancelControlID="x">
    </asp:ModalPopupExtender>

    <asp:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="new">
        <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
        </Animations>
    </asp:AnimationExtender>
    <asp:HiddenField ID="hdnJVS_RegNo" runat="server" Value="0" />
</asp:Content>


