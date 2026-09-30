<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="UpdateBillPaymentinSCMPSCSC.aspx.cs" Inherits="StatePages_UpdateBillPaymentinSCMPSCSC" Title="Update Bill Amount" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
    </script>
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
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #D69758;
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
    <style type="text/css">
        #popupwin {
            position: fixed;
            top: 0;
            left: 0;
            width: 90%;
            height: 90%;
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
            height: 98%;
            margin: 0 auto;
            position: fixed;
            z-index: 101;
            padding-left: 90px;
        }

        .pop {
            /*min-width: 900px;*/
            width: 80%;
            min-height: 150px;
            margin: 0px auto;
            background: #FFFFFF;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 5px 10px #000;
            /*margin-top:200px;*/
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
                /*left: 22px;*/
                position: relative;
                /*top: -20px;*/
                width: 35px;
            }
    </style>
    <fieldset style="width: 100%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="8" align="center">
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Update Bill Payment Details" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="8"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="width: 200px">Bill No.:-
                                                          <asp:TextBox ID="txttwhrno" runat="server" AutoPostBack="false" Height="25px" Width="250px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp
                                                <asp:Button ID="Button1" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                                                    CssClass="BTNBLUE" OnClick="btnSubmit_Click" />
                                            </td>
                                            <%-- <td align="center" style="width: 200px">
                                                          <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" Width="100px" ValidationGroup="A"
                                                              CssClass="BTNBLUE" OnClick="btnSubmit_Click"/>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td colspan="8" valign="top" align="center">
                                                <asp:GridView ID="Depositor_Gridview" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <asp:HiddenField ID="hdngodownid" runat="server" Value='<%# Eval("Godown_ID") %>' />
                                                                <asp:HiddenField ID="hdncommodoty" runat="server" Value='<%# Eval("Commodity_Id") %>' />
                                                                <asp:HiddenField ID="hdnCrop_Year" runat="server" Value='<%# Eval("Crop_Year") %>' />
                                                                <asp:HiddenField ID="hdnFinancial_Year" runat="server" Value='<%# Eval("Financial_Year") %>' />
                                                                <asp:HiddenField ID="hdnBill_Number" runat="server" Value='<%# Eval("Bill_Number") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:BoundField DataField="Regionnm" HeaderText="District Name" />
                                                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />--%>
                                                        <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                                                        <asp:TemplateField HeaderText="Godown Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Number">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblBill_Number" Width="100%" Text='<%# Eval("Bill_Number")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crop Year">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCropYear" Width="100%" Text='<%# Eval("Crop_Year")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Financial Year">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblFinancial_Year" Width="100%" Text='<%# Eval("Financial_Year")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Month">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblMonth_Name" Width="100%" Text='<%# Eval("Month_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Commodity Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCommodity_Name" Width="100%" Text='<%# Eval("Commodity_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblNet_Amount" Width="100%" Text='<%# Eval("Net_Amount")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Update">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Update" CssClass="btn btn-info"
                                                                    OnClick="Display"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ControlStyle Font-Bold="True" ForeColor="Red" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
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
                <asp:Panel ID="pnllogin" class="popup" runat="server">
                    <div class="pop" style="background-color: white; min-height: 300PX; max-height: 500px; width: 1000px; border: #008CBA; border-style: solid; border-width: 10px;">

                        <div id="divNewInsp" runat="server" visible="true" style="width: 100%;">

                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 70px; font-size: 14px" colspan="4" align="center">
                                        <asp:Label ID="Label2" runat="server" Text="Godown Name : "></asp:Label>
                                        <asp:Label ID="lblgodownname" runat="server" Width="300px" Height="20px"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label1" runat="server" Text="Godown ID : "></asp:Label>
                                        <asp:Label ID="txtGdwnID" runat="server" ReadOnly="true"
                                            Width="150px" Height="20px"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr id="tr2" runat="server" visible="true">
                                    <td style="height: 50px; font-size: 14px" align="center">&nbsp; Bill No. &nbsp;
                                                    <asp:Label ID="txtBillno" runat="server"
                                                        Width="150px" Height="20px"></asp:Label>

                                    </td>
                                    <td style="height: 50px; font-size: 14px" align="center">&nbsp; Crop Year &nbsp;
                                                    <asp:Label ID="txtCropyera" runat="server"
                                                        Width="150px" Height="20px"></asp:Label>

                                    </td>
                                </tr>

                                <tr id="tr1" runat="server" visible="true">
                                    <td style="height: 50px; font-size: 14px" align="center">&nbsp;Finacial Year &nbsp;
                                                    <asp:Label ID="txtFY" runat="server"
                                                        Width="150px" Height="20px"></asp:Label>

                                    </td>
                                    <td style="height: 50px; font-size: 14px" align="center">&nbsp;Month &nbsp;
                                                    <asp:Label ID="txtMonth" runat="server"
                                                        Width="150px" Height="20px"></asp:Label>

                                    </td>
                                </tr>

                                <tr id="tr3" runat="server" visible="true">
                                    <td style="height: 50px; font-size: 14px" align="center">&nbsp;Commodity &nbsp;
                                                    <asp:Label ID="txtCommodity" runat="server"
                                                        Width="150px" Height="20px"></asp:Label>

                                    </td>
                                    <td style="height: 50px; font-size: 14px" align="center">&nbsp;Bill Amount &nbsp;
                                                    <asp:Label ID="txtBillAmount" runat="server"
                                                        Width="150px" Height="20px"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="color:red;">
                                        <h3>whr DMO मार्कफेड में कितनी मात्रा दी गई /देनी है उसका अमाउंट यहाँ प्रविष्ट करे</h3>
                                    </td>
                                </tr>

                                <tr id="tr4" runat="server" visible="true">
                                    <td style="height: 50px; font-size: 14px" align="center">&nbsp;Decution Amount &nbsp;
                                                    <asp:TextBox ID="txtDA" runat="server"
                                                        Width="150px" Height="20px"></asp:TextBox>

                                    </td>
                                    <td style="height: 50px; font-size: 14px" align="center">&nbsp;Resion &nbsp;
                                                    <asp:TextBox ID="txtResion" runat="server" TextMode="MultiLine"
                                                        Width="150px" Height="20px"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 5px" colspan="4"></td>
                                </tr>
                                <tr id="trbtnhide" runat="server" visible="true">

                                    <td align="Right">
                                        <asp:Button class="button button1" ID="btnAddCompany" Style="width: 100px" runat="server"
                                            Text="Update" Height="29px" OnClick="btnAddCompany_Click1"></asp:Button>&nbsp&nbsp&nbsp&nbsp
                                    </td>
                                    <td align="left">

                                        <asp:Button class="button button2" ID="btnGenerateBill" Style="width: 100px" runat="server" Text="Close" Height="29px"></asp:Button></td>
                                </tr>

                            </table>
                            <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

                        </div>


                        <%--------End Of Third Section -------------%>
                        <%-- </div>--%>
                    </div>
                    <img alt="New" src="images/new6.gif" id="new" runat="server" />

                </asp:Panel>
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin">
                </asp:ModalPopupExtender>
            </div>
        </center>
    </fieldset>
</asp:Content>

