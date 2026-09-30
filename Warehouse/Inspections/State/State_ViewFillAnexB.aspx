<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="State_ViewFillAnexB.aspx.cs" Inherits="Inspections_State_State_ViewFillAnexB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .wrap {
            margin: 0 auto;
            width: 960px;
            -moz-box-shadow: 0px 5px 23px #000;
            -webkit-box-shadow: 0px 5px 23px #000;
            box-shadow: 0px 5px 23px #000;
        }

        input.submit {
            color: #fff;
            padding: 7px 10px;
            border: 0;
            font-weight: bold;
            background: #777;
            border-radius: 25px;
        }

        input.text {
            border: 2px solid rgb(173, 204, 204);
            height: 20px;
            width: 223px;
            font-size: 16px;
            box-shadow: 0px 0px 27px rgb(204, 204, 204) inset;
            transition: 500ms all ease;
            padding: 3px 3px 3px 3px;
        }
    </style>
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

        .button3 {
            background-color: white;
            color: black;
            border: 2px solid #f44336;
        }

            .button3:hover {
                background-color: #f44336;
                color: white;
            }

        .button6 {
            background-color: white;
            color: black;
            border: 2px solid #E47D21;
        }

            .button6:hover {
                background-color: #E47D21;
                color: white;
            }
    </style>
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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
    $("[src*=plus]").live("click", function() {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../images/minus.jpg");
    });
    $("[src*=minus]").live("click", function() {
    $(this).attr("src", "../images/Plus.jpg");
        $(this).closest("tr").next().remove();
    });
    </script>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%;">
      
        <table align="center" style="width: 100%; border: #E6C79D; border-style: solid; border-width: 0px;">
            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">View Filled Annexure B</span>
                    <%--   <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td align="center">District Wise
                    <asp:RadioButton ID="rdoDist" runat="server"
                        GroupName="StateLicence" Width="70px" AutoPostBack="true"
                        OnCheckedChanged="rdoDist_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 Inspection Officer Name Wise
                    <asp:RadioButton ID="rdoInspOff" runat="server"
                        GroupName="StateLicence" AutoPostBack="true" Width="70px"
                        OnCheckedChanged="rdoInspOff_CheckedChanged" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     
                                 <asp:DropDownList ID="ddl_dist" runat="server" AutoPostBack="true" Width="222px"
                                     Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"
                                     OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                                 </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td style="width: 100%;" align="center">
                    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False"
                        Width="100%" Font-Size="10pt" Font-Bold="true"
                        BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"
                        CellPadding="2" CellSpacing="2"
                        DataKeyNames="Godown_ID"
                        OnSelectedIndexChanged="gvCustomers_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                            <asp:BoundField DataField="Officer_Name" HeaderText="Officer Name" />
                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                            <asp:BoundField DataField="Depotname" HeaderText="Branch" />
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                            <asp:BoundField DataField="Hired_type" HeaderText="Hired Type" />
                            <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" />
                            <asp:BoundField DataField="InspDate" HeaderText="Insp Date" />
                            <asp:BoundField DataField="Total_Bags_AsPerOnline" HeaderText="Total Bags AsPer Online" />
                            <asp:BoundField DataField="Total_Bags_AsPerPV" HeaderText="Total Bags AsPer PV" />
                            <asp:BoundField DataField="DiffBags" HeaderText="Difference Bags" />
                            <asp:CommandField SelectText="Select" HeaderText="View In Details " ShowSelectButton="True">
                                <ControlStyle Font-Bold="True" ForeColor="#008CBA" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlofferpopup" TargetControlID="Label12"
            BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlofferpopup" runat="server" CssClass="modalPopup" ScrollBars="Vertical" Height="70%" Width="70%" Visible="false">
            <div class="header">
                <table style="width: 100%;">
                    <tr>
                        <td style="color: White; font-weight: bold; width: 95%;" align="center">Stack Wise Fill PV Details</td>
                        <td style="color: White; font-weight: bold; height: 25px; width: 40px; border-radius: 5px">
                            <asp:Button ID="btncancelpwd" runat="server" Text="Close" Font-Bold="true" Font-Size="12px" Height="25px" ForeColor="White" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="body">
                <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" Font-Size="10pt" Font-Bold="true" FooterStyle-Wrap="true" ShowFooter="true"
                                BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="godownid">
                                <Columns>
                                    <asp:BoundField DataField="StackName" HeaderText="StackName" />
                                    <asp:BoundField DataField="StackCommodity" HeaderText="Stack Commodity" />
                                    <asp:BoundField DataField="Avl_Bags" HeaderText="Avl Bags AsPer Online" />
                                    <asp:BoundField DataField="Avl_Bags_AsPer_PV" HeaderText="Avl Bags AsPer PV" />
                                    <asp:BoundField DataField="Diff_Bags" HeaderText="Diff Bags" />
                                    <asp:BoundField DataField="Type_Of_DiffBags" HeaderText="Type Of DiffBags" />
                                    <asp:BoundField DataField="Stack_Classification" HeaderText="Stack Classification" />
                                    <asp:BoundField DataField="Stack_PVType" HeaderText="Stack Classification" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remark" />
                                </Columns>
                                <FooterStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" Height="20px" Font-Size="10pt" />
                                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                                    Height="20px" Font-Size="10pt" />
                                <AlternatingRowStyle BackColor="#eeeeee" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;"></td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
   
</asp:Content>

