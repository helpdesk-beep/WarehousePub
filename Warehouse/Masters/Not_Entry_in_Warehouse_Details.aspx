<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Not_Entry_in_Warehouse_Details.aspx.cs" Inherits="Masters_Not_Entry_in_Warehouse_Details" Title="Premice Details Reports" %>


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

    <script type="text/javascript">
        function HeaderClick(CheckBox) {

            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.Depositor_Gridview.ClientID %>');

            var TargetChildControl = "chk_Sum";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.er
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                    Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;

            var txtTotalRecBags = 0;
            var txtTotalRecQty = 0;
            var txtTotalcharges = 0;
            var txttTotalGSTAmt = 0.0;
            var txttTotalSupcharges = 0.0;
            var txttTotalGSTPer = 0.0;
            var Check = "N";
            var checkBoxes = 0;
            //checkBoxes = (1.toString();
            var grid = document.getElementById("<%= Depositor_Gridview.ClientID%>");
            var CheckCount = 1;
            for (var i = 0; i < grid.rows.length - 1; i++) {


                var txtAmount = 0.0;
                var txtcharges = 0.0;
                var txtGSTAmt = 0.0;
                var txtSupcharges = 0.0;
                var txtGSTPer = 0.0;
                txtcharges = $("input[id*=txtcharges]")

                var txtQtyReceive = 0.0;
                var checkBoxes = $("input[id*=chk_Sum]")
                if (checkBoxes[i].checked == true) {
                    txtTotalcharges = txtTotalcharges + parseFloat(txtcharges[i].value);
                    CheckCount = grid.rows.length;


                }

            }



        }
        function calculate() {

            var txtTotalRecBags = 0;
            var txtTotalRecQty = 0;
            var CheckCount = 0;
            var txtTotalcharges = 0;
            var txttTotalGSTAmt = 0.0;
            var txttTotalSupcharges = 0.0;
            var txttTotalGSTPer = 0.0;
            var grid = document.getElementById("<%= Depositor_Gridview.ClientID%>");
            for (var i = 0; i < grid.rows.length - 1; i++) {

                var txtBagSend = $("input[id*=sendb]")
                var txtNetQty = 0.0;
                var txtcharges = 0.0;
                var txtGSTAmt = 0.0;
                var txtSupcharges = 0.0;
                var txtGSTPer = 0.0;
                txtcharges = $("input[id*=txtcharges]")

                var txtQtyReceive = 0.0;

                var checkBoxes = $("input[id*=chk_Sum]")
                if (checkBoxes[i].checked == true) {

                    txtTotalcharges = txtTotalcharges + parseFloat(txtcharges[i].value);

                    CheckCount = CheckCount + 1;

                }
            }

        }
    </script>

    <fieldset style="width: 100%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
        <center>
            <div>
                 <h3 style="color: red;">यहाँ पर ऐसे गोडाउन की जानकारी दिखेगी जिनकी एंट्री वेयरहाउस डिटेल्स टेबल में दर्ज नहीं हुई हैं
                     <br />
                     अपडेट बटन पर क्लिक करने से गोडाउन मैपिंग के लिए फिर से दिखने लगेगे |
                 </h3>
               
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="width: 150px" align="left">
                            <asp:Label ID="Label2" runat="server" Text="District Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 150px" align="left">
                            <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                CssClass="tb6">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 150px" align="left">
                            <asp:Label ID="Label1" runat="server" Text="Branch Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="1" style="width: 150px" align="left">
                            <asp:DropDownList ID="ddlbranch" runat="server" Height="25px" Width="200px" AutoPostBack="true"
                                CssClass="tb6" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td id="showgrid" align="center" valign="top" runat="server" visible="false" colspan="8">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tbody id="grdshow" runat="server" visible="true">
                                            <tr>
                                                <td colspan="4" valign="top" align="center">

                                                    <asp:GridView ID="Depositor_Gridview" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                        BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                        CellSpacing="2" OnRowCommand="Depositor_Gridview_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="क्रमांक">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1%>
                                                                  
                                                                </ItemTemplate>
                                                                <ItemStyle Width="1%" />
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Branch">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblDepotName" Width="100%" Text='<%# Eval("DepotName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>--%>
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
                                                          <asp:TemplateField HeaderText="Select">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkBxHeader" Text="All" onclick="javascript:HeaderClick(this);" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_Sum" runat="server" />
                                                                    <asp:HiddenField ID="hdncheckID" runat="server" Value='<%# Eval("Godown_ID") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                                    Width="80px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                                <ControlStyle Width="50px" Height="50px" />
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
                                        </tbody>
                                        <tr>
                                            <td style="height: 5px; text-align: center;" colspan="8">
                                                <%--  <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="MAP" CssClass="btn btn-info"
                                                    OnClick="Display"></asp:LinkButton>--%>
                                                <asp:Button ID="btnmap" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnmap_Click1" />
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

            </div>
        </center>
    </fieldset>
</asp:Content>

