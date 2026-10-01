<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Account_MPWLC_Master.master" AutoEventWireup="true" CodeFile="Generate_Beneficiary.aspx.cs" Inherits="Accounting_Generate_Beneficiary" %>

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
            border: 2px solid #008CBA;
        }

            .button6:hover {
                background-color: #008CBA;
                color: white;
            }

        .style3 {
            width: 200px;
            height: 11px;
        }
    </style>



    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $(document).on("submit", "form", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvBOBillApp.Rows.Count %>');

            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }



        function ChildClick(CheckBox, HCheckBox) {
            //        alert("hii");
            //get target control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter; 
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

            //Change state of the header CheckBox.
            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;

        }
    </script>

    <script type="text/javascript">
        function HeaderClick(CheckBox) {

            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.gvBOBillApp.ClientID %>');

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
            var grid = document.getElementById("<%= gvBOBillApp.ClientID%>");
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
            var grid = document.getElementById("<%= gvBOBillApp.ClientID%>");
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
    <script type="text/javascript">
        function PrintDiv_Actual() {
            var divContents = document.getElementById("printActualBill").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>


    <fieldset style="width: 1100px; border: 1px solid navy; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px; padding-left: 0px; margin-left: 15px">
        <center>

            <div style="background-color: white">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="4" align="center" valign="top">
                            <fieldset style="width: 1000px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblDepositDetail" runat="server" Text="Generate Beneficiary File" Font-Size="17px"
                                                        Font-Bold="true" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="4" align="center">
                                                    <table>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lbldist" runat="server" Text="District Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                        CssClass="tb6">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label1" runat="server" Text="Branch Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlbranch" runat="server" Height="25px" Width="200px" AutoPostBack="true" CssClass="tb6"
                                                        OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>

                                                <td align="left">
                                                    <asp:Label ID="Label2" runat="server" Text="Beneficiary/Bank Type" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td colspan="1" style="width: 150px" align="left">
                                                    <asp:DropDownList ID="ddlBankType" runat="server" Height="25px" Width="200px" AutoPostBack="true"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlBankType_SelectedIndexChanged">
                                                        <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Same Bank(SBI)" Value="S"></asp:ListItem>
                                                        <asp:ListItem Text="Other Bank(None SBI)" Value="O"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px">
                                                    <b style="color: red;">OR</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>

                                                <td align="left">
                                                    <asp:Label ID="lblPartyName" runat="server" Text="Party Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td colspan="1" style="width: 150px" align="left">
                                                    <asp:DropDownList ID="ddlPartyName" runat="server" Height="25px" Width="200px" AutoPostBack="True"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>

                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr id="trnewproc" runat="server" visible="false">
                        <td colspan="4" align="center" valign="top">
                            <fieldset style="width: 930px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: forestgreen; height: 25px">
                                                <td valign="Center">
                                                    <span style="color: White; font-size: 10pt; font-weight: bold;">Total Record:
                                                                 <asp:Label ID="lblNoofAC" runat="server" Text=""></asp:Label></span>
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                 &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                 &nbsp; &nbsp; &nbsp; &nbsp;
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Beneficiary Instructions Detail
                                                                 <asp:Label ID="lblcropyr" runat="server" Text=""></asp:Label></span></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <div style="height: 340px; widows: 100%; overflow: scroll;" id="toexportDist" runat="server">
                                                        <asp:GridView ID="gvBOBillApp" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="Beneficiary_Id" AllowPaging="False" Width="100%"
                                                            Font-Size="10pt" BorderColor="Navy" BorderWidth="1px"
                                                            TabIndex="4" CellPadding="4" CellSpacing="2">
                                                            <Columns>

                                                                <asp:BoundField DataField="Beneficiary_Id" HeaderText="Beneficiary Id" SortExpression="Beneficiary_Id">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Beneficiary_Name" HeaderText="Beneficiary Name" SortExpression="Beneficiary_Name">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Account_No" HeaderText="Account No" SortExpression="Account_No">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSC_Code" HeaderText="IFSC Code" SortExpression="IFSC_Code">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>

                                                                <%-- <asp:TemplateField HeaderText="Select">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkBxHeader" Text="All" onclick="javascript:HeaderClick(this);" runat="server" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>

                                                                        <asp:CheckBox ID="chk_Sum" runat="server" onclick="calculate();" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                                        Width="80px" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                                    <ControlStyle Width="15px" />
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCC99" />
                                                            <PagerStyle BackColor="#719cb6" ForeColor="Black" HorizontalAlign="center" />
                                                            <SelectedRowStyle BackColor="#cc3399" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#ff6600" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                Height="20px" Font-Size="10pt" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="tblbtn" runat="server" visible="false">

                                                        <tr>
                                                            <td colspan="4" align="center">&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnDownload" runat="server" Text="Download File" class="button button2" Width="150px" Height="30px"
                                                                Enabled="False" Visible="false" TabIndex="13" ValidationGroup="SaveValid" OnClick="btnDownload_Click" />
                                                                &nbsp;&nbsp;&nbsp;
                                                       <asp:Button ID="btn_Sbi_Link" runat="server" Text="Go to SBI Portal for File Upload" Visible="true"
                                                           class="button button2" Width="200px" Height="30px" OnClick="btn_Sbi_Link_Click" OnClientClick="window.open('https://yonobusiness.sbi/login/yonobusinesslogin', 'OtherPage');" />
                                                                &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btn_Close" runat="server" Text="Cancel"
                                                                class="button button2" Width="100px" Height="30px" CausesValidation="false" OnClick="btn_Close_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td colspan="4" align="center">
                                                                <asp:Label ID="lblRespMsg" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#3366ff" Visible="true"></asp:Label>
                                                                <asp:Label ID="lblRespMsgNo" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#3366ff" Visible="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>

                                                    </table>
                                                </td>
                                            </tr>

                                            <asp:Label ID="Label8" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                            <asp:Label ID="Label24" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>


                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>



                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>



