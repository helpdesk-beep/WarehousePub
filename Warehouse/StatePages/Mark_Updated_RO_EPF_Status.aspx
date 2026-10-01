<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Mark_Updated_RO_EPF_Status.aspx.cs" Inherits="StatePages_Mark_Updated_RO_EPF_Status" %>


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
    <%--<script type="text/javascript">
        $(function() {
        $('[id*=chk_Delete]').on('change', function() {
                var value = 0;
                $('[id*=chk_Delete]:checked').each(function() {
                var row = $(this).closest('tr');
                alert(value);
                    value = value + parseInt(row.find('[id*=txtbagnumber]').html());
                });
                alert(value);
                $('[id*=lblTotalBags]').html(value);
            });
        });
    </script>--%>
    <script type="text/javascript">
        function calculateOld() {
            alert("hiiiii");
            var sum = 0.00;
            var itemsum = 0.00;
            // alert(itemsum);
            var gridview = document.getElementById('<%=gvBOBillApp.ClientID %>');
            //alert(gridview);
            for (var row = 1; row < gridview.rows.length; row++) {

                //                var quantity = document.getElementById(gridview.rows[row].cells[7].all[0].id); //gridview.rows[row].cells[4].innerText;
                var quantity = gridview.rows[row].cells[7].innerHTML;
                //                alert(gridview.rows[row].cells[5].innerText);   //working
                var txtAmountReceive = $("input[id*=txtQty]")
                alert(gridview.rows[row].cells[5].innerText);
                //var rows = cntrlname.getElementsByTagName("tr");
                //alert(quantity);
                var chkBox = document.getElementById(gridview.rows[row].cells[10].all[1].id); // + (row - 1).toString());

                alert(gridview.rows[row].cells[7].innerText);
                if (!isNaN(gridview.rows[row].cells[3].innerText)) {
                    if (!isNaN(txtbagnumber.value)) {
                        if (chk_Delete.checked) {
                            itemsum = parseFloat(gridview.rows[row].cells[3].innerText) * parseFloat(txtbagnumber.value);
                            sum += itemsum;
                        }
                    }
                }
            }

            lblTotalBags.innerText = sum.toString();
            //alert(sum.toString());
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
            //Get Data for all Check
            //        function calculate() {
            //            alert("hii");
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

                //var txtBagSend = $("input[id*=sendb]")
                //var txtQtySend = 0.0;
                var txtAmount = 0.0;
                var txtcharges = 0.0;
                var txtGSTAmt = 0.0;
                var txtSupcharges = 0.0;
                var txtGSTPer = 0.0;
                //txtAmount = $("input[id*=txtweight]")
                txtcharges = $("input[id*=txtcharges]")
                //txtGSTAmt = $("input[id*=txtGSTAmt]")
                //txtSupcharges = $("input[id*=txtSupcharges]")
                //txtGSTPer = $("input[id*=txtGSTPer]")

                //alert(txtcharges);
                //var txtBagReceive = $("input[id*=txtbagnumber]")
                var txtQtyReceive = 0.0;
                //txtQtyReceive = $("input[id*=txtweight]")

                //if (txtBagReceive[i].value != '' && txtQtyReceive[i].value != '') {
                var checkBoxes = $("input[id*=chk_Sum]")
                if (checkBoxes[i].checked == true) {
                    //if ((parseInt(txtBagReceive[i].value) <= parseInt(txtBagSend[i].value)) && (parseFloat(txtQtyReceive[i].value) <= parseFloat(txtQtySend[i].value))) {
                    //txtTotalRecBags = txtTotalRecBags + parseInt(txtBagReceive[i].value);
                    //txtTotalRecQty = txtTotalRecQty + parseFloat(txtAmount[i].value);
                    txtTotalcharges = txtTotalcharges + parseFloat(txtcharges[i].value);
                    //txttTotalGSTAmt = txttTotalGSTAmt + parseFloat(txtGSTAmt[i].value);
                    //txttTotalSupcharges = txttTotalSupcharges + parseFloat(txtSupcharges[i].value);
                    //txttTotalGSTPer = txttTotalGSTPer + parseFloat(txtGSTPer[i].value);

                    CheckCount = grid.rows.length;

                    //alert("hhhhhhhhhhh");
                    //}
                    //else {
                    //    //                        Check == "Y";
                    //    alert('You Can not Receive Greater Bags/Qty.then Send Bags/Qty.');
                    //}
                }

                //}
            }
            //        alert(Check);
            //        if (Check == "N") {

            //        var num = 5.78805;
            //        var QtyValue = parseFloat(txtTotalRecQty);
            //        var n = (55.78805).toFixed(4);
            //        var num = parseFloat("55.78805").toFixed(5);
            //        alert(num);

            //        }
            //        else if (Check == "Y") {
            //        alert('You Can not Receive Greater Bags/Qty.then Send Bags/Qty.');
            //        }


            //        }
            //End
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
                //var txtQtySend = 0.0;
                var txtNetQty = 0.0;
                var txtcharges = 0.0;
                var txtGSTAmt = 0.0;
                var txtSupcharges = 0.0;
                var txtGSTPer = 0.0;
                //txtNetQty = $("input[id*=txtweight]")
                txtcharges = $("input[id*=txtcharges]")
                //txtGSTAmt = $("input[id*=txtGSTAmt]")
                //txtSupcharges = $("input[id*=txtSupcharges]")
                //txtGSTPer = $("input[id*=txtGSTPer]")

                //var txtBagReceive = $("input[id*=txtbagnumber]")
                var txtQtyReceive = 0.0;
                //txtQtyReceive = $("input[id*=txtweight]")
                //                 if (i == 0) {
                //                     alert(parseFloat($("input[id*=txtweight]")[0].value));
                //                 }

                //if (txtBagReceive[i].value != '' && txtQtyReceive[i].value != '') {
                var checkBoxes = $("input[id*=chk_Sum]")
                if (checkBoxes[i].checked == true) {
                    //if ((parseInt(txtBagReceive[i].value) <= parseInt(txtBagSend[i].value)) && (parseFloat(txtQtyReceive[i].value) <= parseFloat(txtQtySend[i].value))) {
                    //txtTotalRecBags = txtTotalRecBags + parseInt(txtBagReceive[i].value);
                    //txtTotalRecQty = txtTotalRecQty + parseFloat(txtQtyReceive[i].value);
                    //alert("hiiiii");
                    //txtTotalRecQty = txtTotalRecQty + parseFloat(txtNetQty[i].value);
                    txtTotalcharges = txtTotalcharges + parseFloat(txtcharges[i].value);
                    //txttTotalGSTAmt = txttTotalGSTAmt + parseFloat(txtGSTAmt[i].value);
                    //txttTotalSupcharges = txttTotalSupcharges + parseFloat(txtSupcharges[i].value);
                    //txttTotalGSTPer = txttTotalGSTPer + parseFloat(txtGSTPer[i].value);
                    CheckCount = CheckCount + 1;
                    //}
                    //else {
                    //    alert('You Can not Receive Greater Bags/Qty.then Send Bags/Qty.');
                    //}
                }
                //}
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
                                                    <asp:Label ID="lblDepositDetail" runat="server" Text="Mark Updated Electronic Payment File" Font-Size="17px"
                                                        Font-Bold="true" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 150px" align="left">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="200px" AutoPostBack="True"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
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
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Payment Instructions Detail
                                                                 <asp:Label ID="lblcropyr" runat="server" Text=""></asp:Label></span></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <div style="height: 340px; widows: 100%; overflow: scroll;" id="toexportDist" runat="server">
                                                        <asp:GridView ID="gvBOBillApp" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="Reference_No" AllowPaging="False" Width="100%"
                                                            Font-Size="10pt" BorderColor="Navy" BorderWidth="1px"
                                                            TabIndex="4" CellPadding="4" CellSpacing="2">
                                                            <Columns>
                                                                <asp:BoundField DataField="Branch" HeaderText="Branch" SortExpression="Branch">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Reference_No" HeaderText="Reference No" SortExpression="Reference_No">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Debit_Amount" HeaderText="Debit Amount" SortExpression="Debit_Amount">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreatedDate" HeaderText="Transaction Date" SortExpression="CreatedDate">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="UploadedDate" HeaderText="Updated Date" SortExpression="UploadedDate">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="BankType" HeaderText="Bank Type" SortExpression="BankType">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkBxHeader" Enabled="false" Text="All" onclick="javascript:HeaderClick(this);" runat="server" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chk_Sum" runat="server" />
                                                                        <asp:HiddenField ID="hdnRefno" runat="server" Value='<%# Eval("Reference_No") %>' />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                                        Width="80px" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                                    <ControlStyle Width="15px" />
                                                                </asp:TemplateField>
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
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px" align="center">
                                                    <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="button1 button2" OnClick="Save" />
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
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
    <script type="text/javascript">
        function HeaderClick(CheckBox) {

            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.gvBOBillApp.ClientID %>');

            var TargetChildControl = "chk_Sum";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                    Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
            //Get Data for all Check
            //        function calculate() {
            //            alert("hii");
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

        }
    </script>
</asp:Content>

