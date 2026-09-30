<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="UpdateFlag_N_to_Y_in_Godown.aspx.cs" Inherits="StatePages_UpdateFlag_N_to_Y_in_Godown" Title="Lic Update" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--Update New--%>
    <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=DropDownList1]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlBranch]").select2();
        });
    </script>
    <%--<script type="text/javascript">
      $(function () {
          $("[id*=ddlParty]").select2();
      });
  </script>--%>
    <script type="text/javascript">
        function filterGrid() {

            var input = document.getElementById("<%= txtSearch.ClientID %>");
          var filter = input.value.toLowerCase();

          var table = document.getElementById("<%= Depositor_Gridview.ClientID %>");
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
    <fieldset style="width: 90%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td align="center">
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Update New Verify Godown Entry" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px"></td>
                                        </tr>


                                        <tr>


                                            <td style="height: 50px; font-size: 14px" align="center">&nbsp;&nbsp;&nbsp;&nbsp  District : &nbsp;&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                                                Height="25px" Width="168px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>

                                                &nbsp;&nbsp;&nbsp;&nbsp  Branch : &nbsp;&nbsp;<asp:DropDownList ID="ddlBranch" runat="server"
                                                    Height="25px" Width="168px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                </asp:DropDownList>


                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="height: 5px"></td>
                                        </tr>

                                        <tr>
                                            <td valign="top" align="center">
                                                <tr>

                                                    <td align="left">
                                                        <asp:TextBox ID="txtSearch" runat="server"
                                                            placeholder="Search here..."
                                                            Style="width: 380px; height: 36px; margin-bottom: 10px; padding: 0 15px; font-size: 15px; border: 1px solid #000; border-radius: 8px; outline: none; transition: all 0.25s ease; box-shadow: 0 2px 6px rgba(0,0,0,0.08);"
                                                            onkeyup="filterGrid();" />

                                                    </td>
                                                </tr>
                                                <tr>

                                                    <asp:GridView ID="Depositor_Gridview" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                        BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                        CellSpacing="2">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Godown_ID">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblGodown_ID" Width="100%" Text='<%# Eval("Godown_ID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Godown_Name">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Hired_Type">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblHired_Type" Width="100%" Text='<%# Eval("Hired_Type")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Storage_Type">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblStorage_Type" Width="100%" Text='<%# Eval("Storage_Type")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Scientific Capacity">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblGodown_Scientific_Capacity" Width="100%" Text='<%# Eval("Godown_Scientific_Capacity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Max Capacity">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblGodown_Capacity" Width="100%" Text='<%# Eval("Godown_Capacity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Closing_Balance">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblClosing_Balance" Width="100%" Text='<%# Eval("Closing_Balance")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="LicNum">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblLicNum" Width="100%" Text='<%# Eval("LicNum")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="LicDate">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblLicDate" Width="100%" Text='<%# Eval("LicDate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update Lincance Details">
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
                        <%-- <div class="col-sm-12 col-md-12 col-xs-12">--%>


                        <div id="divNewInsp" runat="server" visible="false" style="width: 100%;">
                            <h3 style="color: red;">Only Update N to Y/ Y to N in Godown Details </h3>
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 70px; font-size: 14px" colspan="4" align="center">
                                        <asp:Label ID="Label2" runat="server" Text="Godown Name : "></asp:Label>
                                        <asp:TextBox ID="lblgodownname" runat="server" Width="300px" Height="20px"></asp:TextBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr id="trmobtxt" runat="server" visible="false">
                                    <td style="height: 70px; font-size: 14px" colspan="4" align="center">
                                        <asp:Label ID="Label1" runat="server" Text="Godown ID : "></asp:Label>
                                        <asp:TextBox ID="txtGdwnID" runat="server" ReadOnly="true"
                                            Width="150px" Height="20px"></asp:TextBox>

                                        &nbsp;&nbsp;                                   
                                                  Update N to Y/ Y to N &nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlytonandntoy" runat="server" Width="155px" Height="25px">
                                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="N" Value="N"></asp:ListItem>

                                                </asp:DropDownList>
                                        <br />
                                    </td>
                                </tr>

                                <tr id="tr2" runat="server" visible="false">
                                    <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp; Scientific Capacity(In Qntl) &nbsp;
                                                    <asp:TextBox ID="txtscieCPT" runat="server"
                                                        Width="100px" Height="20px"></asp:TextBox>
                                        &nbsp;   
                                                   Hired Type   &nbsp;&nbsp;
                                                    <asp:DropDownList ID="ddllst_hired" runat="server" Width="155px" Height="25px">
                                                    </asp:DropDownList>
                                        &nbsp;&nbsp;                                   
                                                  Storage Type &nbsp;&nbsp;
                                                <asp:DropDownList ID="ddllst_storage" runat="server" Width="155px" Height="25px">
                                                    <asp:ListItem Text="Covered" Value="Covered"></asp:ListItem>
                                                    <asp:ListItem Text="Permanent(CAP)" Value="Permanent(CAP)"></asp:ListItem>
                                                    <asp:ListItem Text="Temporary(CAP)" Value="Temporary(CAP)"></asp:ListItem>
                                                    <asp:ListItem Text="Silo Bag" Value="Silo Bag"></asp:ListItem>
                                                    <asp:ListItem Text="Steel Silo" Value="Steel Silo"></asp:ListItem>
                                                </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr id="tr1" runat="server" visible="false">
                                    <td style="height: 70px; font-size: 14px" colspan="4" align="center">Closing Balance (In Qntl) &nbsp&nbsp
                                                    <asp:TextBox ID="txtclosing" runat="server"
                                                        Width="100px" Height="20px"></asp:TextBox>
                                        &nbsp;&nbsp;
                                                   Licence No. &nbsp&nbsp
                                                    <asp:TextBox ID="txtlicno" runat="server"
                                                        Width="150px" Height="20px"></asp:TextBox>
                                        &nbsp;&nbsp;
                                                   Licence Date &nbsp&nbsp 
                                                <%--                                                    <asp:TextBox ID="txtlicdate" runat="server" 
                                                        Width="100px" Height="20px" ></asp:TextBox>--%>
                                        <asp:TextBox ID="txtlicdate" runat="server" Width="100px" Height="20px"></asp:TextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="height: 5px" colspan="4"></td>
                                </tr>
                                <tr id="trbtnhide" runat="server" visible="false">

                                    <td align="Right">
                                        <asp:Button class="button button1" ID="btnAddCompany" Style="width: 100px" runat="server"
                                            Text="Update" Height="29px" OnClick="btnAddCompany_Click"></asp:Button>&nbsp&nbsp&nbsp&nbsp
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


