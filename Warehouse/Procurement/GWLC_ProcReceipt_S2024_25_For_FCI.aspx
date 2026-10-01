<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="~/Procurement/GWLC_ProcReceipt_S2024_25_For_FCI.aspx.cs" Inherits="GWLC_ProcReceipt_S2024_25_For_FCI" %>

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
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <link href="../Assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="../Assets/js/bootstrap-datepicker.js"></script>
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
            background: #647e68 !important;
            color: black !important;
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
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;
        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gdnewproc.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.gdnewproc.ClientID %>');
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
            var txtTotalRecBags = 0;
            var txtTotalRecQty = 0.0;
            var Check = "N";
            var grid = document.getElementById("<%= gdnewproc.ClientID%>");
            for (var i = 0; i < grid.rows.length - 1; i++) {

                var txtBagSend = $("input[id*=sendb]")
                var txtQtySend = 0.0;
                txtQtySend = $("input[id*=sendq]")

                var txtBagReceive = $("input[id*=txtbagnumber]")
                var txtQtyReceive = 0.0;
                txtQtyReceive = $("input[id*=txtweight]")

                if (txtBagReceive[i].value != '' && txtQtyReceive[i].value != '') {
                    var checkBoxes = $("input[id*=chk_Sum]")
                    if (checkBoxes[i].checked == true) {
                        if ((parseInt(txtBagReceive[i].value) <= parseInt(txtBagSend[i].value)) && (parseFloat(txtQtyReceive[i].value) <= parseFloat(txtQtySend[i].value))) {
                            txtTotalRecBags = txtTotalRecBags + parseInt(txtBagReceive[i].value);
                            txtTotalRecQty = txtTotalRecQty + parseFloat(txtQtyReceive[i].value);
                        }
                        else {
                            //                        Check == "Y";
                            alert('You Can not Receive Greater Bags/Qty.then Send Bags/Qty.');
                        }
                    }
                }
            }
            document.getElementById('<%=lblTotalBags.ClientID%>').innerHTML = txtTotalRecBags.toString();
            document.getElementById('<%=lblTotalQty.ClientID%>').innerHTML = (txtTotalRecQty.toFixed(5)).toString();
            document.getElementById('<%= hdnLabelState.ClientID %>').value = txtTotalRecBags.toString()
            document.getElementById('<%= hdnLabelStateQty.ClientID %>').value = (txtTotalRecQty.toFixed(5)).toString();
        }
        function ChildClick(CheckBox, HCheckBox) {
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
        function calculateOld() {

            var sum = 0.00;
            var itemsum = 0.00;
            // alert(itemsum);
            var gridview = document.getElementById('<%=gdnewproc.ClientID %>');
            //alert(gridview);
            for (var row = 1; row < gridview.rows.length; row++) {
                // var quantity = document.getElementById(gridview.rows[row].cells[7].all[0].id); //gridview.rows[row].cells[4].innerText;
                var quantity = gridview.rows[row].cells[7].innerHTML;
                // alert(gridview.rows[row].cells[5].innerText);   //working
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
        function calculate() {
            var lbltext = document.getElementById('<%=lblTotalQty.ClientID%>').innerHTML;
            //alert('111111111');
            //alert(lbltext);
            if (lbltext == '' || lbltext == '0') {
                var txtTotalRecBags = 0;
                var txtTotalRecQty = 0;
                var grid = document.getElementById("<%= gdnewproc.ClientID%>");
                for (var i = 0; i < grid.rows.length - 1; i++) {

                    var txtBagSend = $("input[id*=sendb]")
                    var txtQtySend = 0.0;
                    txtQtySend = $("input[id*=sendq]")

                    var txtBagReceive = $("input[id*=txtbagnumber]")
                    var txtQtyReceive = 0.0;
                    txtQtyReceive = $("input[id*=txtweight]")
                    if (txtBagReceive[i].value != '' && txtQtyReceive[i].value != '') {
                        var checkBoxes = $("input[id*=chk_Sum]")
                        if (checkBoxes[i].checked == true) {
                            if ((parseInt(txtBagReceive[i].value) <= parseInt(txtBagSend[i].value)) && (parseFloat(txtQtyReceive[i].value) <= parseFloat(txtQtySend[i].value))) {
                                txtTotalRecBags = txtTotalRecBags + parseInt(txtBagReceive[i].value);
                                txtTotalRecQty = txtTotalRecQty + parseFloat(txtQtyReceive[i].value);
                            }
                            else {
                                alert('You Can not Receive Greater Bags/Qty.then Send Bags/Qty.');
                            }

                        }
                    }
                }
                document.getElementById('<%=lblTotalBags.ClientID%>').innerHTML = txtTotalRecBags.toString();
                document.getElementById('<%=lblTotalQty.ClientID%>').innerHTML = (txtTotalRecQty.toFixed(5)).toString();
                document.getElementById('<%= hdnLabelState.ClientID %>').value = txtTotalRecBags.toString()
                document.getElementById('<%= hdnLabelStateQty.ClientID %>').value = (txtTotalRecQty.toFixed(5)).toString();
            }
            else {
                /*  alert('आपके द्वारा पहले से ही एक ट्रक चालान सिलैक्ट किया जा चुका है, आपको ट्रक वाइज़ WHR जारी करना होगा.');*/
                alert('आपके द्वारा केवल एक ही ट्रक चालान सिलैक्ट किया जा सकता है, आपको ट्रक वाइज़ WHR जारी करनी होगी|');
            }
        }

    </script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    <!-- Bootstrap Datepicker plugin -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.10.0/css/bootstrap-datepicker.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.10.0/js/bootstrap-datepicker.min.js"></script>
    <script>
        $(function () {
            $('.datepicker').datepicker({
                format: "dd/mm/yyyy",
                todayHighlight: true,
                autoclose: true,
                orientation: "bottom"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
    <div class="content-wrapper">
        <fieldset>
            <legend>Truck wise Stock Receiving</legend>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <label style="margin-top: 6px; font-size: medium">Type of Depositor</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddldepositortype" Font-Bold="true" runat="server" AutoPostBack="True" Enabled="false"
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label style="margin-top: 6px; font-size: medium">Depositor Name</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlDepositor" Font-Bold="true" runat="server" AutoPostBack="false" Enabled="true"
                        CssClass="form-control" readonly="true">
                        <asp:ListItem Value="181">FCI</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <label style="margin-top: 6px; font-size: medium">Procurement Commodity</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlProcCmd" runat="server"
                        CssClass="form-control" Font-Bold="true" Enabled="true">
                        <asp:ListItem Value="92">Moong</asp:ListItem>
                        <asp:ListItem Value="27">Urad</asp:ListItem>
                        <asp:ListItem Value="13">Paddy-Common</asp:ListItem>
                        <asp:ListItem Value="8">Bajra</asp:ListItem>
                        <asp:ListItem Value="11">Jowar</asp:ListItem>
                        <asp:ListItem Value="3">Rice-Raw-Common</asp:ListItem>
                        <asp:ListItem Value="129">Fortified_Rice</asp:ListItem>
                        <asp:ListItem Value="131">Fortified Rice Kerne</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label style="margin-top: 6px; font-size: medium">Crop Year</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlcropyear" Font-Bold="true" runat="server" CssClass="form-control" Enabled="true">
                        <asp:ListItem Value="2024-2025">2024-2025</asp:ListItem>
                        <asp:ListItem Value="2025-2026">2025-2026</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <label style="margin-top: 6px; font-size: medium">Deposit Date</label>
                </div>
                <div class="col-md-2">

                    <%-- <asp:TextBox ID="txtDate" placeholder="dd/mm/yyyy" runat="server" CssClass="form-control"
                            onpaste="return false ;"
                            onkeypress="return false;" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" />--%>
                    <%-- <span class="input-group-text">
                            <i class="bi bi-calendar-date"></i>
                        </span>--%>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker"
                        placeholder="dd/mm/yyyy" Font-Bold="true" autocomplete="off" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate"
                        Display="Dynamic" ErrorMessage="Deposite Date is required" SetFocusOnError="True"></asp:RequiredFieldValidator>

                    <%-- <asp:TextBox ID="txtDate" placeholder="dd/mm/yyyy" autocomplete="off" onpaste="return false ;"
                        onkeypress="return false;" CssClass="form-control datepicker" runat="server" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>--%>
                    <%-- <asp:TextBox ID="txtDate" runat="server" MaxLength="18" AutoPostBack="true"
                        CssClass="form-control" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                    <asp:ImageButton ID="Imgpop" runat="server" ImageUrl="~/images/cal.gif" CausesValidation="false" />
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtDate"
                        Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="Imgpop">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate"
                        Display="Dynamic" ErrorMessage="Deposite Date is required" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-md-2">
                    <label style="margin-top: 6px; font-size: medium">Godown</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="True"
                        CssClass="form-control" Enabled="true"
                        OnClientClick="$('#myspindiv').show();"
                        OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </fieldset>
        <fieldset id="trnewproc" runat="server" visible="false">
            <div class="row">
                <div class="col-md-6">
                    <span style="color: black; font-size: 12pt; font-weight: bold;">Total Record:
                        <asp:Label ID="lblNoofAC" runat="server" Text=""></asp:Label></span>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <%--&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp;--%>
                    <span style="color: black; font-size: 12pt; font-weight: bold">Depositor Form Detail
                        <asp:Label ID="lblcropyr" runat="server" Text=""></asp:Label></span>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="table-responsive">
                    <asp:GridView ID="gdnewproc" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="Acceptance_No,DepositerNo" AllowPaging="false" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="DepositerNo" HeaderText="Depositor Form No." SortExpression="DepositerNo">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Acceptance_No" HeaderText="Acceptance No" SortExpression="Acceptance_No">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Acceptance_Date" HeaderText="Acceptance Date" SortExpression="Acceptance_Date">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TC_Number" HeaderText="TC Number" SortExpression="TC_Number">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Truck_Number" HeaderText="Truck Number" SortExpression="Truck_Number">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Send Bags">
                                <ItemTemplate>
                                    <asp:TextBox ID="sendb" BackColor="Transparent" Enabled="false" Font-Bold="true" runat="server" Width="60px" MaxLength="5" onblur="Spc_validatorInt(this)" Text='<%# Eval("Recd_Bags") %>'>0</asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Send Qty.">
                                <ItemTemplate>
                                    <asp:TextBox ID="sendq" BackColor="Transparent" Enabled="false" Font-Bold="true" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,5)"
                                        onblur="Spc_validatornumeric(this)" Text='<%# Eval("Recd_Qty") %>'>0</asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receive Bags">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtbagnumber" BackColor="#ffe09f" Font-Bold="true" runat="server" Width="60px" MaxLength="5" onblur="Spc_validatorInt(this)" Text='<%# Eval("Recd_Bags2") %>'>0</asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receive Qty.">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtweight" BackColor="#ffe09f" Font-Bold="true" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,6)"
                                        Text='<%# Eval("Recd_Qty2") %>'>0</asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="RecdBags_JuteNew" HeaderText="Recd_Bags_JuteNew" SortExpression="RecdBags_JuteNew">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RecdBags_PP" HeaderText="Recd_Bags_PP" SortExpression="RecdBags_PP">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RecdBags_JuteOld" HeaderText="Recd_Bags_JuteOld" SortExpression="RecdBags_JuteOld">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Moisture" HeaderText="Moisture" SortExpression="Moisture">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Select">
                                <HeaderTemplate>
                                    <%--  <asp:CheckBox ID="chkBxHeader" Text="All"  onclick="javascript:HeaderClick(this);" runat="server" />--%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_Sum" runat="server" onclick="calculate();" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                    Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                <ControlStyle Width="15px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <PagerStyle BackColor="#719cb6" ForeColor="Black" HorizontalAlign="center" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </div>
            <div style="margin-top: 30px" id="tblbtn" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-2">
                        <label style="margin-top: 6px; font-size: medium">Total Bags Send</label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblTotalBagSend" runat="server" Text="0" Font-Size="14px" Font-Bold="true"
                            ForeColor="navy"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <label style="margin-top: 6px; font-size: medium">Total Qty. Send</label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblTotalQtySend" runat="server" Font-Size="14px" Font-Bold="true"
                            Text="0" ForeColor="navy"></asp:Label>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px">
                    <div class="col-md-1"></div>
                    <div class="col-md-2">
                        <label style="margin-top: 6px; font-size: medium">Total Bags Received</label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblTotalBags" EnableViewState="false" ViewStateMode="Disabled" runat="server" Text="0" Font-Size="14px" Font-Bold="true" ForeColor="#c64f00"></asp:Label>
                        <asp:HiddenField runat="server" ID="hdnLabelState" />
                    </div>
                    <div class="col-md-2">
                        <label style="margin-top: 6px; font-size: medium">Total Qty. Received</label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblTotalQty" runat="server" Font-Size="14px" Font-Bold="true"
                            Text="0" ForeColor="#c64f00"></asp:Label>
                        <asp:HiddenField runat="server" ID="hdnLabelStateQty" />
                    </div>
                </div>
                <div class="row" style="margin-top: 10px">
                    <div class="col-md-4"></div>
                    <div class="col-md-1">
                        <asp:Button ID="btn_save" runat="server" Text="Submit" CssClass="BTNBLUE" Width="100px"
                            Enabled="False" TabIndex="13" ValidationGroup="SaveValid"
                            OnClientClick="$('#myspindiv').show();"
                            OnClick="btn_save_Click" />
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btn_Close" runat="server" Text="Cancel"
                            CssClass="BTNBLUE" Width="100px" CausesValidation="false"
                            OnClick="btn_Close_Click" />
                    </div>
                </div>
            </div>
        </fieldset>
        <tr>
            <td>
                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlCofirmmsg" TargetControlID="Label8"
                    CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlCofirmmsg" runat="server" CssClass="modalPopup" Height="330px" Width="700px">
                    <div class="header">
                        <table style="width: 100%;">
                            <tr>
                                <td style="color: White; font-weight: bold; font-size: large;" align="center">Receiving Detail</td>
                                <td style="width: 50px">
                                    <asp:Button ID="btnNo" runat="server" Text="Close" CssClass="no" align="left" />
                                </td>
                            </tr>

                        </table>
                    </div>
                    <div class="body">
                        <table cellspacing="1" style="width: 100%;">
                            <tr>
                                <td style="height: 15px"></td>
                            </tr>
                            <tr>
                                <td style="font-size: 12px; font-family: Arial; font-weight: bold" align="left">
                                    <table width="100%">
                                        <tr style="height: 15px;">
                                            <td style="width: 130px;">&nbsp;Date of Deposit :
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDepostDate" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 150px;">Godown Name :
                                            </td>
                                            <td>
                                                <asp:Label ID="lblGodown" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="height: 15px;">
                                            <td>&nbsp;Send Bags :
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSendBags" runat="server"></asp:Label>
                                            </td>
                                            <td>Send Qty.(In Qtl.) :</td>
                                            <td>
                                                <asp:Label ID="lblSendQty" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="height: 15px;">
                                            <td>&nbsp;Received Bags :
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRcdBags" runat="server"></asp:Label>
                                            </td>
                                            <td>Received Qty.(In Qtl.) :</td>
                                            <td>
                                                <asp:Label ID="lblRecdQty" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="height: 15px;">
                                            <td>&nbsp;Depositor :
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDepositor" runat="server"></asp:Label>
                                            </td>
                                            <td>Commodity :</td>
                                            <td>
                                                <asp:Label ID="lblCommodity" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">&nbsp;Crop Year :</td>
                                            <td class="style2">
                                                <asp:Label ID="lblCropYear" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td align="center">
                                    <asp:Button class="button button2" Width="150px" Height="30px" ID="Button2"
                                        runat="server" Text="Proceed" align="Center"
                                        OnClientClick="$('#myspindiv').show();"
                                        OnClick="Button2_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="hdnGodownID" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Label ID="hdnDepositorID" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Label ID="hdnCommodityID" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <asp:Label ID="Label8" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
        <div id="myspindiv" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(255, 255, 255, 0.7); z-index: 9999; text-align: center;">
            <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
                <img src="../images/mpwlc3.gif" alt="Loading..." />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        // Show loader on full postback
        function showLoader() {
            document.getElementById("myspindiv").style.display = "block";
        }

        // For AJAX requests (UpdatePanel, ModalPopup, etc.)
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
            showLoader();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            document.getElementById("myspindiv").style.display = "none";
        });

        // For normal postbacks
        window.onload = function () {
            var theForm = document.forms[0];
            if (theForm.attachEvent) {
                theForm.attachEvent("onsubmit", showLoader);
            } else {
                theForm.addEventListener("submit", showLoader, false);
            }
        };
    </script>
</asp:Content>

