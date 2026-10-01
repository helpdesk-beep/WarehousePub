<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="UpdateJVSRegID_WHMSLogin.aspx.cs" Inherits="StatePages_UpdateJVSRegID_WHMSLogin" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>'></script>
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
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
    <style type="text/css">
        .left, .right {
            float: left;
            width: 20%; /* The width is 20%, by default */
        }

        .main {
            float: left;
            width: 60%; /* The width is 60%, by default */
        }

        /* Use a media query to add a breakpoint at 800px: */
        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
        }
    </style>
    <style type="text/css">
        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
        }

        .content-wrapper {
            padding: 1.75rem 1.25rem;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #030203;
        }

        .form-control {
            border: 1px solid #767B83;
            border-radius: 8px;
        }

        .table th {
            text-align: center;
        }

        .form-inline {
            display: block !important;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: white !important;
            color: black !important;
        }

        .GridViewHeader th {
            color: white !important; /* header text white */
            background-color: #4CAF50 !important; /* optional background */
            text-align: center;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            color: black !important;
        }

        element.style {
            font-size: medium !important;
        }

        .auto-style3 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 50%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
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

        .BTNRED {
            border: 1px solid #7eb9d0;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            font-size: 12px;
            font-family: arial, helvetica, sans-serif;
            padding: 10px 10px 10px 10px;
            text-decoration: none;
            display: inline-block;
            text-shadow: -1px -1px 0 rgba(0,0,0,0.3);
            font-weight: bold;
            color: #FFFFFF;
            background-color: #dfa7aa;
            background-image: linear-gradient(to bottom, #dfa7b7, #d7333b);
        }

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
        $(function () {
            $("[id*=ddlYear]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlRegID]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=DDLDistrict]").select2();
        });
    </script>
     <script type="text/javascript">
         $(function () {
             $("[id*=ddlBranch]").select2();
         });
     </script>
         <%--Grid Filter--%>
     <script type="text/javascript">
         function filterGrid() {

             var input = document.getElementById("<%= txtSearch.ClientID %>");
         var filter = input.value.toLowerCase();

         var table = document.getElementById("<%= GridView1.ClientID %>");
             var trs = table.getElementsByTagName("tr");

             for (var i = 1; i < trs.length; i++) { // skip header row
                 var display = false;
                 var tds = trs[i].getElementsByTagName("td");

                 for (var j = 0; j < tds.length; j++) {
                     var cell = tds[j];
                     if (cell && cell.textContent.toLowerCase().indexOf(filter) > -1) {
                         display = true;
                         break;
                     }
                 }

                 trs[i].style.display = display ? "" : "none";
             }
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1300px; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">

                    <%--      ----------Start  JVS Lic -----------------------%>
                    <tr id="trjvslic" runat="server">
                        <td align="center" valign="top">
                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Link JVS Registration ID to WHMS Private Godown Login ID" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="left">
                                                <p style="color: Red; font-size: 14px">
                                                    नोट :-
                                                    <br />
                                                    1.यह ऑपरेशन सिर्फ WHR बनाते समय यदि यह मेसेज आ रहा हे (You Cannot Deposite Greater Then Allow Capacity) तभी उपयोग करें ।<br />
                                                    2.यदि  एग्रीमेंट छमता क़े विरुध 125% तक WHR बनाई जा चुकी हे  तो उक्त गोदाम से WHR बनाना संभव नहीं हैं | यदि गलत गोदाम लिंक हे तो HO से unlink कराए ।
                                                    <br />
                                                    3.JVS मॉडुल में जिस ब्रांच में ऑफर किया गया हे , ड्रॉपडाउन मे वह ब्रांच एवम्‌ रजिस्ट्रेशन आईडी सेलेक्ट करें ।
                                                    <br />
                                                    4.जिन  गोदाम का JVS में निरीक्षण एवम्‌ एग्रीमेंट अपडेट कर लिया गया है वहि रजिस्ट्रेशन आईडी प्रदर्शित होगी ।
                                                </p>
                                            </td>
                                        </tr>


                                        <tr>

                                            <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp;
                                                 Select Year : 
                                                &nbsp;<asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" Height="25px" Width="168px"
                                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                    <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                                    <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                                    <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                                    <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                                    <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                                    <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                                </asp:DropDownList>
                                                District : &nbsp;<asp:DropDownList
                                                    ID="DDLDistrict" runat="server"
                                                    Height="25px" Width="150px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="DDLDistrict_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp  Branch : &nbsp;&nbsp;&nbsp;&nbsp<asp:DropDownList
                                                    ID="ddlBranch" runat="server" AutoPostBack="true"
                                                    Height="25px" Width="168px" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>

                                                &nbsp;&nbsp;&nbsp; JVS Registration ID : &nbsp;&nbsp;<asp:DropDownList
                                                    ID="ddlRegID" runat="server"
                                                    Height="25px" Width="250px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlRegID_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>
                                        <tr id="trhide1" runat="server" visible="false">
                                            <td colspan="4" align="center">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <%-- ----------Start New Table Here---------------%>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblgdrowcount" runat="server" Font-Size="10pt"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="4" valign="top" align="center">
                                                            <asp:GridView ID="Depositor_Gridview" runat="server" DataKeyNames="Godownid" AutoGenerateColumns="False" Width="70%" BackColor="White"
                                                                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                                CellSpacing="2" PageSize="50">
                                                                <Columns>

                                                                    <asp:BoundField DataField="Godownid" HeaderText="Godown ID" ReadOnly="True" SortExpression="Godownid" />
                                                                    <asp:BoundField DataField="Godown_name" HeaderText="Godown Name" ReadOnly="True" SortExpression="Godown_name" />
                                                                    <asp:BoundField DataField="Hired_Type" HeaderText="Hired Type" ReadOnly="True" SortExpression="Hired Type" />
                                                                    <asp:BoundField DataField="WHRQty" HeaderText="WHRQty" ReadOnly="True" SortExpression="WHRQty" />

                                                                </Columns>
                                                                <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center" Wrap="true"
                                                                    Height="20px" Font-Size="11px" />
                                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px"></td>
                                                    </tr>

                                                    <tr id="tr1" runat="server">

                                                        <td style="height: 30px; font-size: 14px" colspan="4" align="left">
                                                            <asp:Label ID="Label2" runat="server" Text="उक्त रजिस्ट्रेशन  आईडि क़े विरुध कुल ऑनलाइन एग्रीमेंट क्षमता (In M.T): "></asp:Label>
                                                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp 
                                                    <asp:TextBox ID="txtagrcpt" runat="server" ReadOnly="true"
                                                        Width="150px" Height="20px" MaxLength="10"></asp:TextBox>

                                                        </td>

                                                    </tr>



                                                    <tr id="tr22" runat="server">
                                                        <td style="height: 30px; font-size: 14px" colspan="4" align="left">उक्त रजिस्ट्रेशन आईडि सें लिंक WHMS गोडाउन आईडि से जारी कुल WHR की क्षमता (M.T) : &nbsp&nbsp&nbsp 
                                                    <asp:TextBox ID="txtWHRCpt" runat="server" ReadOnly="true"
                                                        Width="150px" Height="20px" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="left">
                                                            <p style="color: Red; font-size: 14px">
                                                                नोट :-
                                                                <br />
                                                                1.जिस गोडाउन से यह रजिस्ट्रेशन आईडी लिंक करना हे  जिसमें WHR बनाते समय  यह मेसेज आ रहा हे (You Cannot Deposite Greater Then Allow Capacity) वह WHMS गोडाउन आईडी सेळेक्ट करें ।<br />
                                                                2.WHMS मे WHR जारी करने के लिये लोगिन उपलब्ध (लोगिन) बना हे  वहि आईडी लिंक करने क़े लिये प्रदर्शित होगी ।<br />
                                                        </td>
                                                    </tr>
                                                    <tr id="trmobtxt" runat="server">

                                                        <td style="height: 40px; font-size: 14px" colspan="4" align="center">&nbsp;&nbsp;Branch :&nbsp;<asp:DropDownList
                                                            ID="DDLBRANCH2" runat="server"
                                                            Height="25px" Width="150px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="DDLBRANCH2_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                            &nbsp;&nbsp;
                                                    <asp:Label ID="Label1" runat="server" Text="WHMS Godown ID: "></asp:Label>&nbsp;
                                                    <asp:DropDownList
                                                        ID="ddlWHMSGdwnID" runat="server"
                                                        Height="25px" Width="250px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlWHMSGdwnID_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                            &nbsp;
                                                    <asp:Label ID="lblgdid" runat="server"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px" colspan="4"></td>
                                                    </tr>
                                                    <tr id="trbtnhide" runat="server">

                                                        <td align="Right">
                                                            <asp:Button class="button button1" ID="btnsubmit" Style="width: 100px" runat="server"
                                                                Text="Update" Height="29px" OnClick="btnsubmit_Click"></asp:Button>&nbsp&nbsp&nbsp&nbsp
                                                        </td>
                                                        <td align="left">

                                                            <asp:Button class="button button2" ID="btnGenerateBill" Style="width: 100px" runat="server" Text="Close" Height="29px"></asp:Button></td>
                                                    </tr>

                                                    <%-----------------End New Table Here----------------%>
                                                </table>
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
                    <tr>
                        <center>
                            <tr id="tr2" runat="server" visible="false">
                                <td colspan="4" align="center">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <%-- ----------Start New Table Here---------------%>
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="4" align="center">
                                                <asp:Label ID="Label4" runat="server" Text="Update Agreement capacity" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                                <asp:Label ID="Label3" runat="server" Font-Size="10pt">&nbsp;</asp:Label>
                                            </td>
                                        </tr>
                                               <tr>

    <td align="left">
        <asp:TextBox ID="txtSearch" runat="server"
            placeholder="Search here..."
            Style="width: 380px; height: 36px; margin-bottom: 10px; padding: 0 15px; font-size: 15px; border: 1px solid #000; border-radius: 8px; outline: none; transition: all 0.25s ease; box-shadow: 0 2px 6px rgba(0,0,0,0.08);"
            onkeyup="filterGrid();" />

    </td>
</tr>
                                        <tr>
                                            <td colspan="4" valign="top" align="center">

                                                <br />
                                                <asp:GridView ID="GridView1" runat="server" DataKeyNames="Agreement_Id" AutoGenerateColumns="False" Width="70%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" PageSize="50" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Update Agreement Capacity">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" Text="" OnClick="Edit">
                                                                    Update
                                                                </asp:LinkButton>
                                                                <asp:HiddenField ID="hdnAgreement_Id" runat="server" Value='<%# Eval("Agreement_Id") %>' />
                                                                <asp:HiddenField ID="hdnAgree_Capacity" runat="server" Value='<%# Eval("Agree_Capacity") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID" ReadOnly="True" SortExpression="Godownid" />
                                                        <asp:BoundField DataField="Godown_name" HeaderText="Godown Name" ReadOnly="True" SortExpression="Godown_name" />
                                                        <asp:BoundField DataField="Registration_Id" HeaderText="Registration Id" ReadOnly="True" SortExpression="Registration_Id" />
                                                        <asp:BoundField DataField="Agreement_Id" HeaderText="Agreement Id" ReadOnly="True" SortExpression="Agreement_Id" />
                                                        <asp:BoundField DataField="GodownNo_JVS" HeaderText="GodownNo JVS" ReadOnly="True" SortExpression="GodownNo_JVS" />
                                                        <asp:BoundField DataField="Agree_Capacity" HeaderText="Agreement Capacity" ReadOnly="True" SortExpression="Agree_Capacity" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center" Wrap="true"
                                                        Height="20px" Font-Size="11px" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>


                                        <%-----------------End New Table Here----------------%>
                                    </table>
                                </td>
                            </tr>
                        </center>
                    </tr>

                </table>
                <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 50%; overflow: scroll;">
                    <asp:Label Font-Bold="true" ID="Label5" runat="server" Text="Update Agreement Capacity"></asp:Label>
                    <br />
                    <table align="center">
                        <tr>
                            <td colspan="4" valign="top" align="center"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Agreement ID" Font-Bold="true" ForeColor="navy"
                                    Font-Size="8pt"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblAgreementID" runat="server" Text="Agreement ID" Font-Bold="true" ForeColor="navy"
                                    Font-Size="8pt"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Agreement Capacity" Font-Bold="true" ForeColor="navy"
                                    Font-Size="8pt"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblAgree_Capacity" runat="server" Text="Agreement Capacity" Font-Bold="true" ForeColor="navy"
                                    Font-Size="8pt"></asp:Label></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Update Agreement Capacity" Font-Bold="true" ForeColor="navy"
                                    Font-Size="8pt"></asp:Label>
                                : </td>
                            <td>
                                <asp:TextBox ID="txtAgreeCapacity" runat="server" Width="150px" AutoComplete="off" onkeypress="return isNumberKey2(event)"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtAgreeCapacity_FilteredTextBoxExtender"
                                    runat="server" TargetControlID="txtAgreeCapacity" FilterType="Custom, Numbers" ValidChars=".">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BTNBLUE" OnClick="Save" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="BTNRED" OnClientClick="return Hidepopup()" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                <cc1:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
                    PopupControlID="pnlAddEdit" TargetControlID="lnkFake"
                    BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
            </div>
        </center>
    </fieldset>

    <asp:HiddenField ID="hdnAgrId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnYearID" runat="server" Value="0" />
</asp:Content>
