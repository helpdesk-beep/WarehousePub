<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="UpdateVacantCapacity.aspx.cs" Inherits="BranchPages_UpdateVacantCapacity" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
            border-radius: 6px;
            padding: 0
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>
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

    </script>

    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <%--<asp:GridView ID="GridView1" runat="server" Width="550px"
                            AutoGenerateColumns="false" AlternatingRowStyle-BackColor="#C2D69B"
                            HeaderStyle-BackColor="green" AllowPaging="true"
                            OnPageIndexChanging="godown_GridView_SelectedIndexChanged"
                            PageSize="10">
                            <Columns>
                                <asp:BoundField DataField="GodownID" HeaderText="Godown ID" />
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                <asp:BoundField DataField="Godown_Capacity" HeaderText="Max Capacity" />
                                <asp:BoundField DataField="Vacant_Capacity" HeaderText="Vacant Capacity" />
                                <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />
                                <asp:TemplateField ItemStyle-Width="30px" HeaderText="GodownID">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#C2D69B" />
                        </asp:GridView>--%>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="center" valign="top">
                                    <fieldset style="width: 980px; border: 1px solid navy;">
                                        <center>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblGodownMaster" runat="server" Text="Verified Godown  Master" Font-Bold="true"
                                                                Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                                                Font-Size="10pt"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <a href="javascript:popMe('../SampleQuantity.htm');" style="text-decoration: underline">(Qty. in Qtls.kgsgms)</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top" colspan="2">
                                                            <%--OnRowDataBound="godown_GridView_RowDataBound" 
                                                        OnRowDeleting="godown_GridView_RowDeleting"
                                                            --%>
                                                            <asp:GridView ID="godown_GridView" runat="server" DataKeyNames="GodownID" AutoGenerateColumns="False"
                                                                CellPadding="2" Width="100%" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="godown_GridView_PageIndexChanging" OnSelectedIndexChanged="godown_GridView_SelectedIndexChanged"
                                                                PageSize="20" Font-Size="9pt">
                                                                <Columns>
                                                                    <%--<asp:CommandField HeaderText="Delete" ShowDeleteButton="True"
                                                                ItemStyle-ForeColor="red">
                                                                <ItemStyle ForeColor="Red"></ItemStyle>
                                                            </asp:CommandField>
                                                                    <asp:CommandField ShowSelectButton="True" HeaderText="Edit"
                                                                        ItemStyle-ForeColor="blue">
                                                                        <ItemStyle ForeColor="Blue"></ItemStyle>
                                                                    </asp:CommandField>--%>
                                                                    <asp:TemplateField HeaderText="S.N.">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1%>
                                                                            <asp:HiddenField ID="hdnGodownID" runat="server" Value='<%# Eval("GodownID") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="GodownID" HeaderText="Godown ID" />
                                                                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                                                    <asp:BoundField DataField="Godown_Capacity" HeaderText="Max Capacity" />
                                                                    <asp:BoundField DataField="Vacant_Capacity" HeaderText="Vacant Capacity" />
                                                                    <asp:BoundField DataField="Unload_Capacity" HeaderText="Unload Capacity" />
                                                                    <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" />
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
                            <tr>
                                <td style="height: 10px" colspan="2"></td>
                            </tr>
                            <%--<tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnaddnew" runat="server" Width="100px" CssClass="BTNBLUE" OnClick="btnaddnew_Click"
                                Text="Add New" CausesValidation="False" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE"
                                CausesValidation="false" OnClick="btn_Close_Click" />
                            <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>--%>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                    <asp:ValidationSummary ID="godown_Validationerror" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" />
                                </td>
                            </tr>
                        </table>
                        <%--<asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Add" />--%>

                        <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 50%">
                            <asp:Label Font-Bold="true" ID="Label3" runat="server" Text="Godown Details"></asp:Label>
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
                                        <asp:Label ID="Label4" runat="server" Text="Vacant Capacity" Font-Bold="true" ForeColor="navy"
                                            Font-Size="8pt"></asp:Label>(Qty. in Qtls.kgsgms)</td>
                                    <td>
                                        <asp:TextBox ID="txtVacantCapacity" runat="server" Width="150px" AutoComplete="off" onkeypress="return isNumberKey2(event)"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtVacantCapacity_FilteredTextBoxExtender"
                                            runat="server" TargetControlID="txtVacantCapacity" FilterType="Custom, Numbers" ValidChars=".">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Unload Capacity" Font-Bold="true" ForeColor="navy"
                                            Font-Size="8pt"></asp:Label>(Qty. in Qtls.kgsgms)</td>
                                    <td>
                                        <asp:TextBox ID="txtUnloadCapacity" runat="server" Width="150px" AutoComplete="off" onkeypress="return isNumberKey2(event)"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtUnloadCapacity_FilteredTextBoxExtender"
                                            runat="server" TargetControlID="txtUnloadCapacity" FilterType="Custom, Numbers" ValidChars=".">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BTNBLUE" OnClick = "Save"/>
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
    <asp:HiddenField ID="hdnGodownID" runat="server" Value="0" />
</asp:Content>

