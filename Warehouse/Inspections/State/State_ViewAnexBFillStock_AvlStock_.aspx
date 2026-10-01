<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="State_ViewAnexBFillStock_AvlStock_.aspx.cs" Inherits="Inspections_State_State_ViewAnexBFillStock_AvlStock_" %>

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
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript">
    $(document).on("click", "[src*=plus]", function() {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../images/minus.jpg");
    });
    $(document).on("click", "[src*=minus]", function() {
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
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">View Filled Annexure B Summary</span></td>
            </tr>
            <tr>
                <td align="center">District
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                 <asp:DropDownList ID="ddl_dist" runat="server" AutoPostBack="true" Width="222px"
                                     Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"
                                     OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                                 </asp:DropDownList>

                </td>
            </tr>


            <tr>
                <td style="width: 100%;" align="center" visible="false">
                    <asp:GridView ID="gridbranch" runat="server" AutoGenerateColumns="False"
                        Width="90%" Font-Size="10pt" Font-Bold="true" FooterStyle-Wrap="true" ShowFooter="true"
                        BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"
                        CellPadding="2" CellSpacing="2" DataKeyNames="Branch_ID"
                        OnSelectedIndexChanged="gridbranch_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="Branch_ID" HeaderText="Branch_ID" />
                            <asp:BoundField DataField="OfficeName" HeaderText="Office Name" />
                            <asp:BoundField DataField="District_Name" HeaderText="District" />
                            <asp:BoundField DataField="Depotname" HeaderText="Branch" />
                            <asp:BoundField DataField="NoOfGdwn" HeaderText="No Of Godown Online" />
                            <asp:BoundField DataField="NoOfPVGdwn" HeaderText="No Of Fill PV Godown" />
                            <asp:BoundField DataField="Total_Bags_AsPerOnline" HeaderText="Total Available Bags As Per Online at time of PV" />
                            <asp:BoundField DataField="Total_Bags_AsPerPV" HeaderText="Total Available Bags As Per PV" />
                            <asp:BoundField DataField="DiffBags" HeaderText="Difference Of Bags" />
                            <asp:BoundField DataField="AvlBagsCur" HeaderText="Avl Bags After PV" />
                            <asp:BoundField DataField="AvlBagsCurDiff" HeaderText="Diff Bags After PV Avl Bags" />
                            <asp:CommandField SelectText="Select" HeaderText="View Godown Wise Details" ShowSelectButton="True">
                                <ControlStyle Font-Bold="True" ForeColor="#008CBA" />
                            </asp:CommandField>
                        </Columns>
                        <FooterStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" Height="20px" Font-Size="10pt" />
                        <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
                </td>
            </tr>

            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Branch
                        <asp:Label ID="lblbName" runat="server" ForeColor="#cb4e48" Font-Bold="true" Font-Size="17px"></asp:Label>
                        Godown Wise Balance Details</span></td>
            </tr>
            <tr>
                <td style="width: 100%;" align="center">
                    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False"
                        Width="100%" Font-Size="10pt" Font-Bold="true" FooterStyle-Wrap="true" ShowFooter="true"
                        BackColor="White" BorderColor="#008CBA" BorderStyle="Double" BorderWidth="1px"
                        CellPadding="2" CellSpacing="2"
                        DataKeyNames="Godown_ID">
                        <Columns>
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" />
                            <asp:BoundField DataField="Depotname" HeaderText="Branch" />
                            <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                            <asp:BoundField DataField="Hired_type" HeaderText="Hired Type" />
                            <asp:BoundField DataField="Storage_Type" HeaderText="Storage Type" />
                            <asp:BoundField DataField="InspDate" HeaderText="Insp Date" />
                            <asp:BoundField DataField="Total_Bags_AsPerOnline" HeaderText="Total Bags AsPer Online at time of PV" />
                            <asp:BoundField DataField="Total_Bags_AsPerPV" HeaderText="Total Bags AsPer PV" />
                            <asp:BoundField DataField="DiffBags" HeaderText="DiffBags" />
                            <asp:BoundField DataField="AvlBagsCur" HeaderText="Avalable Bags Online After PV" />
                            <asp:BoundField DataField="AvlBagsCurDiff" HeaderText="Diff Of Avalable Bags Online After PV" />
                        </Columns>
                        <FooterStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" Height="20px" Font-Size="10pt" />
                        <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

