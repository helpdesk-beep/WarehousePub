<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/Accounting/Nafed_BO_BillApproval.aspx.cs" Inherits="Accounting_Nafed_BO_BillApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #popupwin {
            position: fixed;
            top: 0;
            left: 0;
            width: 90%;
            height: 100%;
            background-color: #000;
            filter: alpha(opacity=65);
            -moz-opacity: 0.7;
            display: none;
            opacity: 0.7;
            z-index: 100;
        }

        .pop a {
            text-decoration: none;
        }

        .popup {
            width: 100%;
            height: 500px;
            margin: 0 auto;
            position: fixed;
            z-index: 101;
        }

        .pop {
            min-width: 800px;
            width: 700px;
            min-height: 150px;
            margin: 0px auto;
            background: #f3f3f3;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 2px 5px #000;
        }

            .pop p {
                color: #555555;
                text-align: justify;
                font-size: medium;
            }

                .pop p a {
                    color: #d91900;
                }

            .pop .x {
                float: right;
                height: 35px;
                left: 22px;
                position: relative;
                top: -20px;
                width: 35px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy; background-color: white;">
        <center>
            <div>
                <table width="100%">
                    <%--<tr>
                        <td align="left" class="style1" style="font-size: 11px; color: Red">Note :&nbsp; Submission हेतु ऐसे बिल ही दिखेंगे जिन Final Bill एवं संबधित गोडाउन बिलो पर DSC की जा चुकी है। :
                            <asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>--%>
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="4" align="center">
                            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                Text="Nafed Storage Charges Bill Online Submission To RM"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvBOBillApp" runat="server" AutoGenerateColumns="False" Width="100%" Font-Names="Arial" OnSelectedIndexChanged="gvBOBillApp_SelectedIndexChanged"
                                            BorderStyle="Double" align="center" CellPadding="3" CellSpacing="5" ShowFooter="true"
                                            Font-Size="11px" BorderColor="#CCCCCC">
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:CommandField ButtonType="Button" HeaderStyle-Height="10%" ItemStyle-Width="10%" HeaderText="Submit Bill" ShowHeader="True"
                                                    ShowSelectButton="True" SelectText="Submit To RM" ControlStyle-CssClass="BTNBLUE" />
                                                <asp:BoundField DataField="Bill_Number" ItemStyle-Width="10%" HeaderText="Bill Number"></asp:BoundField>
                                                <asp:BoundField DataField="Commodity_Name" ItemStyle-Width="7%" HeaderText="Commodity Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Crop_Year" ItemStyle-Width="7%" HeaderText="Crop Year" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Financial_Year" ItemStyle-Width="7%" HeaderText="Financial Year" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Month" ItemStyle-Width="7%" HeaderText="Month" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Net_Amount" ItemStyle-Width="7%" HeaderText="Total Bill Amount" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle ForeColor="#C70039" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="11px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
    <img alt="" src="" id="new" runat="server" />
    <asp:Panel ID="pnllogin" class="popup" runat="server">
        <div class="popup">
            <div class="pop" style="background-color: #FFFFCC0">
                <table cellspacing="1" cellpadding="3">
                    <tr>
                        <td align="center">
                            <center>
                                <h3>Verify Bill by OTP</h3>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style1" style="font-size: 11px; color: Red">Note :&nbsp; Kindly Verify - OTP इस सीयूजी मोबाइल नंबर :
                            <asp:Label ID="lblcug" runat="server" Font-Bold="true"></asp:Label>
                            &nbsp;पर भेजा जाएगा । यदि यह दिया गया नंबर गलत है तो कृप्या शाखा प्रोफाइल मे सीयूजी मोबाइल नंबर अपडेट करें ।
                        </td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnOTP" runat="server" Text="Generate OTP" Height="30px"
                Width="142px" OnClick="btnOTP_Click" />
                        </td>
                    </tr>
                    <tr id="trbtn" runat="server">
                        <td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtCheckOTP" runat="server" Height="22px" Width="140px" placeholder="Enter OTP Here"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btn_Submit" runat="server" Text="Submit"
                Height="30px" Width="80px" OnClick="Btn_Submit_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_close" runat="server" Text="Close" Height="30px"
                    Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <input id="hdfOTP" type="hidden" runat="server" />
                            <asp:TextBox ID="txtMobNum" runat="server" Height="22px" Width="140px" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </div>
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin">
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
</asp:Content>

