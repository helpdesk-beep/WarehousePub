<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="PaymentStatus_int.aspx.cs" Inherits="StatePages_PaymentStatus_int" Title="Licence Update" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

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
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Licence Update For Inspection" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px; padding-left:212px;" colspan="2">
                                                <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Update Payment" CssClass="btn btn-info"
                                                    OnClick="Display"></asp:LinkButton>
                                            </td>
                                           
                                        </tr>


                                        <tr>


                                            <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp;&nbsp;&nbsp;&nbsp  Licence Number : &nbsp;&nbsp;
                                                 <asp:TextBox ID="txtLicenceNo" runat="server" AutoPostBack="false" Height="25px" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
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
                        <%-- <div class="col-sm-12 col-md-12 col-xs-12">--%>
                       <%-- <div id="div1" runat="server" visible="true" style="width: 100%;">
                            <h3>WDRA or non-WDRA wharehosue Licence Entry Select</h3>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="false"
                                class="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                <asp:ListItem Value="0">-----Select------</asp:ListItem>
                                <asp:ListItem Value="1">Non-WDRA</asp:ListItem>
                                <asp:ListItem Value="2">WDRA</asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>

                        <div id="divNewInsp" runat="server" visible="false" style="width: 100%;">
                            <div>
                                <h3>Non WDRA Entry Form</h3>
                            </div>
                            <table cellpadding="0" cellspacing="0" style="width: 100%; border: 1px solid black;">
                                <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label3" runat="server" Text="CategoryName : "></asp:Label>

                                        <br />
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:DropDownList ID="ddlCategoryName" runat="server" AutoPostBack="false"
                                            class="form-control">
                                            <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="REGISTRATION FEE" Text="REGISTRATION FEE"></asp:ListItem>
                                            <asp:ListItem Value="OFFER FEES" Text="OFFER FEES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label8" runat="server" Text="PaymentMode : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                       <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="false"
                                            class="form-control">
                                           
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label2" runat="server" Text="BankReferenceNo : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="lblBankReferenceNo" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label7" runat="server" Text="Amount : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txAmount" runat="server"
                                            class="form-control"></asp:TextBox>

                                    </td>

                                </tr>
                                <tr id="trmobtxt" runat="server" visible="true">
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label1" runat="server" Text="Status : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtStatus" runat="server"
                                            class="form-control"></asp:TextBox>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label6" runat="server" Text="REGISTRATIONID : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtREGISTRATIONID" runat="server"
                                            class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label4" runat="server" Text="TransactionDate : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtTransactionDate" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label5" runat="server" Text="NAMEOFDEPOSITOR : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtNAMEOFDEPOSITOR" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>

                                </tr>

                                 <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label20" runat="server" Text="CONTACTNO : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtCONTACTNO" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label21" runat="server" Text="EMAILID : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtEMAILID" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>

                                </tr>

                               
                                <tr>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label24" runat="server" Text="FEE : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtFEE" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:Label ID="Label25" runat="server" Text="Remarks : "></asp:Label>
                                    </td>
                                    <td style="height: 70px; font-size: 14px; border: 1px solid black;" align="left">
                                        <asp:TextBox ID="txtRemarks" runat="server" class="form-control"></asp:TextBox>
                                        <br />
                                    </td>

                                </tr>

                                <tr>
                                    <td style="height: 5px" colspan="4"></td>
                                </tr>
                                <tr id="trbtnhide" runat="server" visible="true">

                                    <td align="Right">
                                        <asp:Button class="button button1" ID="btnAddCompany" Style="width: 100px" runat="server"
                                            Text="Save" Height="29px" OnClick="btnAddCompany_Click"></asp:Button>&nbsp&nbsp&nbsp&nbsp
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

