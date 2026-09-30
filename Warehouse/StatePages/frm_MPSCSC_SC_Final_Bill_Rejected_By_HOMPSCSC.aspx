<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="frm_MPSCSC_SC_Final_Bill_Rejected_By_HOMPSCSC.aspx.cs" Inherits="Accounting_frm_MPSCSC_SC_Final_Bill_Rejected_By_HOMPSCSC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .divWaiting {
            position: absolute;
            background-color: aqua;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>

    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        .style2 {
            height: 20px;
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
             for (var i = 0; i < grid.rows.length - 1; i++) {

                 var txtBagSend = $("input[id*=sendb]")
                 //var txtQtySend = 0.0;
                 var txtAmount = 0.0;
                 var txtcharges = 0.0;
                 var txtGSTAmt = 0.0;
                 var txtSupcharges = 0.0;
                 var txtGSTPer = 0.0;
                 txtAmount = $("input[id*=txtweight]")
                 txtcharges = $("input[id*=txtcharges]")
                 txtGSTAmt = $("input[id*=txtGSTAmt]")
                 txtSupcharges = $("input[id*=txtSupcharges]")
                 txtGSTPer = $("input[id*=txtGSTPer]")

                 //alert(txtcharges);
                 var txtBagReceive = $("input[id*=txtbagnumber]")
                 var txtQtyReceive = 0.0;
                 txtQtyReceive = $("input[id*=txtweight]")

                 //if (txtBagReceive[i].value != '' && txtQtyReceive[i].value != '') {
                 var checkBoxes = $("input[id*=chk_Sum]")
                 if (checkBoxes[i].checked == true) {
                     //if ((parseInt(txtBagReceive[i].value) <= parseInt(txtBagSend[i].value)) && (parseFloat(txtQtyReceive[i].value) <= parseFloat(txtQtySend[i].value))) {
                     //txtTotalRecBags = txtTotalRecBags + parseInt(txtBagReceive[i].value);
                     txtTotalRecQty = txtTotalRecQty + parseFloat(txtAmount[i].value);
                     txtTotalcharges = txtTotalcharges + parseFloat(txtcharges[i].value);
                     txttTotalGSTAmt = txttTotalGSTAmt + parseFloat(txtGSTAmt[i].value);
                     txttTotalSupcharges = txttTotalSupcharges + parseFloat(txtSupcharges[i].value);
                     txttTotalGSTPer = txttTotalGSTPer + parseFloat(txtGSTPer[i].value);

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
             document.getElementById('<%=lblTotalBags.ClientID%>').innerHTML = (CheckCount - 1).toString();
        <%--document.getElementById('<%=lblTotalQty.ClientID%>').innerHTML = (txtTotalRecQty.toFixed(5)).toString();--%>
             document.getElementById('<%=lblTotalQty.ClientID%>').innerHTML = (txtTotalRecQty.toFixed(0)).toString();
             document.getElementById('<%= hdnLabelState.ClientID %>').value = (CheckCount - 1).toString();
        <%--document.getElementById('<%= hdnLabelStateQty.ClientID %>').value = (txtTotalRecQty.toFixed(5)).toString();--%>
             document.getElementById('<%= hdnLabelStateQty.ClientID %>').value = (txtTotalRecQty.toFixed(0)).toString();

             document.getElementById('<%=lblTotalCharges.ClientID%>').innerHTML = (txtTotalcharges.toFixed(0)).toString();
             document.getElementById('<%=lblGSTAmt.ClientID%>').innerHTML = (txttTotalGSTAmt.toFixed(0)).toString();
             document.getElementById('<%=lblSupCharge.ClientID%>').innerHTML = (txttTotalSupcharges.toFixed(0)).toString();
             document.getElementById('<%=lblGSTonSup.ClientID%>').innerHTML = (txttTotalGSTPer.toFixed(0)).toString();

             document.getElementById('<%= HiddenField11.ClientID %>').value = (txtTotalcharges.toFixed(0)).toString();
             document.getElementById('<%= HiddenField12.ClientID %>').value = (txttTotalGSTAmt.toFixed(0)).toString();
             document.getElementById('<%= HiddenField13.ClientID %>').value = (txttTotalSupcharges.toFixed(0)).toString();
             document.getElementById('<%= HiddenField14.ClientID %>').value = (txttTotalGSTPer.toFixed(0)).toString();

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
                 txtNetQty = $("input[id*=txtweight]")
                 txtcharges = $("input[id*=txtcharges]")
                 txtGSTAmt = $("input[id*=txtGSTAmt]")
                 txtSupcharges = $("input[id*=txtSupcharges]")
                 txtGSTPer = $("input[id*=txtGSTPer]")

                 var txtBagReceive = $("input[id*=txtbagnumber]")
                 var txtQtyReceive = 0.0;
                 txtQtyReceive = $("input[id*=txtweight]")
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
                     txtTotalRecQty = txtTotalRecQty + parseFloat(txtNetQty[i].value);
                     txtTotalcharges = txtTotalcharges + parseFloat(txtcharges[i].value);
                     txttTotalGSTAmt = txttTotalGSTAmt + parseFloat(txtGSTAmt[i].value);
                     txttTotalSupcharges = txttTotalSupcharges + parseFloat(txtSupcharges[i].value);
                     txttTotalGSTPer = txttTotalGSTPer + parseFloat(txtGSTPer[i].value);
                     CheckCount = CheckCount + 1;
                     //}
                     //else {
                     //    alert('You Can not Receive Greater Bags/Qty.then Send Bags/Qty.');
                     //}
                 }
                 //}
             }
             //alert(txtcharges);
             document.getElementById('<%=lblTotalBags.ClientID%>').innerHTML = (CheckCount).toString();
             document.getElementById('<%=lblTotalQty.ClientID%>').innerHTML = (txtTotalRecQty.toFixed(0)).toString();
             document.getElementById('<%= hdnLabelState.ClientID %>').value = (CheckCount).toString();
             document.getElementById('<%= hdnLabelStateQty.ClientID %>').value = (txtTotalRecQty.toFixed(0)).toString();

             document.getElementById('<%=lblTotalCharges.ClientID%>').innerHTML = (txtTotalcharges.toFixed(0)).toString();
             document.getElementById('<%=lblGSTAmt.ClientID%>').innerHTML = (txttTotalGSTAmt.toFixed(0)).toString();
             document.getElementById('<%=lblSupCharge.ClientID%>').innerHTML = (txttTotalSupcharges.toFixed(0)).toString();
             document.getElementById('<%=lblGSTonSup.ClientID%>').innerHTML = (txttTotalGSTPer.toFixed(0)).toString();

             document.getElementById('<%= HiddenField11.ClientID %>').value = (txtTotalcharges.toFixed(0)).toString();
             document.getElementById('<%= HiddenField12.ClientID %>').value = (txttTotalGSTAmt.toFixed(0)).toString();
             document.getElementById('<%= HiddenField13.ClientID %>').value = (txttTotalSupcharges.toFixed(0)).toString();
             document.getElementById('<%= HiddenField14.ClientID %>').value = (txttTotalGSTPer.toFixed(0)).toString();
        }
    </script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
            for (var i = 0; i < grid.rows.length - 1; i++) {

                var txtBagSend = $("input[id*=sendb]")
                //var txtQtySend = 0.0;
                var txtAmount = 0.0;
                var txtcharges = 0.0;
                var txtGSTAmt = 0.0;
                var txtSupcharges = 0.0;
                var txtGSTPer = 0.0;
                txtAmount = $("input[id*=txtweight]")
                txtcharges = $("input[id*=txtcharges]")
                txtGSTAmt = $("input[id*=txtGSTAmt]")
                txtSupcharges = $("input[id*=txtSupcharges]")
                txtGSTPer = $("input[id*=txtGSTPer]")

                //alert(txtcharges);
                var txtBagReceive = $("input[id*=txtbagnumber]")
                var txtQtyReceive = 0.0;
                txtQtyReceive = $("input[id*=txtweight]")

                //if (txtBagReceive[i].value != '' && txtQtyReceive[i].value != '') {
                var checkBoxes = $("input[id*=chk_Sum]")
                if (checkBoxes[i].checked == true) {
                    //if ((parseInt(txtBagReceive[i].value) <= parseInt(txtBagSend[i].value)) && (parseFloat(txtQtyReceive[i].value) <= parseFloat(txtQtySend[i].value))) {
                    //txtTotalRecBags = txtTotalRecBags + parseInt(txtBagReceive[i].value);
                    txtTotalRecQty = txtTotalRecQty + parseFloat(txtAmount[i].value);
                    txtTotalcharges = txtTotalcharges + parseFloat(txtcharges[i].value);
                    txttTotalGSTAmt = txttTotalGSTAmt + parseFloat(txtGSTAmt[i].value);
                    txttTotalSupcharges = txttTotalSupcharges + parseFloat(txtSupcharges[i].value);
                    txttTotalGSTPer = txttTotalGSTPer + parseFloat(txtGSTPer[i].value);

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
            document.getElementById('<%=lblTotalBags.ClientID%>').innerHTML = (CheckCount - 1).toString();
        <%--document.getElementById('<%=lblTotalQty.ClientID%>').innerHTML = (txtTotalRecQty.toFixed(5)).toString();--%>
            document.getElementById('<%=lblTotalQty.ClientID%>').innerHTML = (txtTotalRecQty.toFixed(0)).toString();
            document.getElementById('<%= hdnLabelState.ClientID %>').value = (CheckCount - 1).toString();
        <%--document.getElementById('<%= hdnLabelStateQty.ClientID %>').value = (txtTotalRecQty.toFixed(5)).toString();--%>
            document.getElementById('<%= hdnLabelStateQty.ClientID %>').value = (txtTotalRecQty.toFixed(0)).toString();

            document.getElementById('<%=lblTotalCharges.ClientID%>').innerHTML = (txtTotalcharges.toFixed(0)).toString();
            document.getElementById('<%=lblGSTAmt.ClientID%>').innerHTML = (txttTotalGSTAmt.toFixed(0)).toString();
            document.getElementById('<%=lblSupCharge.ClientID%>').innerHTML = (txttTotalSupcharges.toFixed(0)).toString();
            document.getElementById('<%=lblGSTonSup.ClientID%>').innerHTML = (txttTotalGSTPer.toFixed(0)).toString();

            document.getElementById('<%= HiddenField11.ClientID %>').value = (txtTotalcharges.toFixed(0)).toString();
            document.getElementById('<%= HiddenField12.ClientID %>').value = (txttTotalGSTAmt.toFixed(0)).toString();
            document.getElementById('<%= HiddenField13.ClientID %>').value = (txttTotalSupcharges.toFixed(0)).toString();
            document.getElementById('<%= HiddenField14.ClientID %>').value = (txttTotalGSTPer.toFixed(0)).toString();

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
                txtNetQty = $("input[id*=txtweight]")
                txtcharges = $("input[id*=txtcharges]")
                txtGSTAmt = $("input[id*=txtGSTAmt]")
                txtSupcharges = $("input[id*=txtSupcharges]")
                txtGSTPer = $("input[id*=txtGSTPer]")

                var txtBagReceive = $("input[id*=txtbagnumber]")
                var txtQtyReceive = 0.0;
                txtQtyReceive = $("input[id*=txtweight]")
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
                    txtTotalRecQty = txtTotalRecQty + parseFloat(txtNetQty[i].value);
                    txtTotalcharges = txtTotalcharges + parseFloat(txtcharges[i].value);
                    txttTotalGSTAmt = txttTotalGSTAmt + parseFloat(txtGSTAmt[i].value);
                    txttTotalSupcharges = txttTotalSupcharges + parseFloat(txtSupcharges[i].value);
                    txttTotalGSTPer = txttTotalGSTPer + parseFloat(txtGSTPer[i].value);
                    CheckCount = CheckCount + 1;
                    //}
                    //else {
                    //    alert('You Can not Receive Greater Bags/Qty.then Send Bags/Qty.');
                    //}
                }
                //}
            }
            //alert(txtcharges);
            document.getElementById('<%=lblTotalBags.ClientID%>').innerHTML = (CheckCount).toString();
            document.getElementById('<%=lblTotalQty.ClientID%>').innerHTML = (txtTotalRecQty.toFixed(0)).toString();
            document.getElementById('<%= hdnLabelState.ClientID %>').value = (CheckCount).toString();
            document.getElementById('<%= hdnLabelStateQty.ClientID %>').value = (txtTotalRecQty.toFixed(0)).toString();

            document.getElementById('<%=lblTotalCharges.ClientID%>').innerHTML = (txtTotalcharges.toFixed(0)).toString();
            document.getElementById('<%=lblGSTAmt.ClientID%>').innerHTML = (txttTotalGSTAmt.toFixed(0)).toString();
            document.getElementById('<%=lblSupCharge.ClientID%>').innerHTML = (txttTotalSupcharges.toFixed(0)).toString();
            document.getElementById('<%=lblGSTonSup.ClientID%>').innerHTML = (txttTotalGSTPer.toFixed(0)).toString();

            document.getElementById('<%= HiddenField11.ClientID %>').value = (txtTotalcharges.toFixed(0)).toString();
            document.getElementById('<%= HiddenField12.ClientID %>').value = (txttTotalGSTAmt.toFixed(0)).toString();
            document.getElementById('<%= HiddenField13.ClientID %>').value = (txttTotalSupcharges.toFixed(0)).toString();
            document.getElementById('<%= HiddenField14.ClientID %>').value = (txttTotalGSTPer.toFixed(0)).toString();
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
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
     <div class="divWaiting">            
	<asp:Label ID="lblWait" runat="server" 
	Text=" " />
	<asp:Image ID="imgWait" runat="server" 
	ImageAlign="Middle" ImageUrl="~/images/mpwlc3.gif" />
  </div>
    
    </ProgressTemplate>
    </asp:UpdateProgress>--%>

    <fieldset style="width: 1100px; border: 1px solid navy; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px; padding-left: 0px; margin-left: 15px">
        <center>
            <%--     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
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
                                                    <asp:Label ID="lblDepositDetail" runat="server" Text="Storage Charges Bill Generation " Font-Size="17px"
                                                        Font-Bold="true" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="4" align="center">
                                                    <table>
                                                        <%-- <tr>
                                                           <td>
                                                           <span style="color: #FF0000">
                                                           Important instructions
                                                           </span>

                                                           </td>
                                                           
                                                           </tr>--%>
                                                        <%--<tr>
                                                           <td style="font-size: small; font-weight: bold; font-style: normal; color: #FF0000; text-decoration: blink">
                                                           1. 
                                                               Other depot के case मे रिसीविंग लेने के लिए Date wise&nbsp; ऑप्शन का use किया जा सकता है जिसमे एक डेट की सारी रिसीविंग एक साथ दिख जाएगी</td>
                                                           </tr>--%>
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
                                            <tr align="center">
                                                <td align="left" style="width: 200px">
                                                    <asp:Label ID="lblDepositorType" runat="server" Font-Size="12px" Font-Bold="true"
                                                        Text="District" ForeColor="navy"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" Width="200px" Enabled="true"
                                                        Height="25px" CssClass="tb6" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <asp:Label ID="Label11" runat="server" Font-Size="12px" Font-Bold="true"
                                                        Text="Branch" ForeColor="navy"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="True" Width="200px" Enabled="true"
                                                        Height="25px" CssClass="tb6" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="left" style="width: 200px">
                                                    <asp:Label ID="Label7" runat="server" Font-Size="12px" Font-Bold="true"
                                                        Text="Godown" ForeColor="navy"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <asp:DropDownList ID="ddlGodown" runat="server" AutoPostBack="True" Width="200px" Enabled="true"
                                                        Height="25px" CssClass="tb6" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 200px">
                                                    <asp:Label ID="lblcmd" runat="server" Font-Size="12px" Font-Bold="true"
                                                        Text="Select Final Number" ForeColor="navy"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <asp:DropDownList ID="ddlbill" runat="server" AutoPostBack="True"
                                                        Height="25px" Width="200px" Enabled="true" OnSelectedIndexChanged="ddlbill_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>

                                            <tr>
                                                <td colspan="4" style="height: 5px">&nbsp;</td>
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
                                            <tr style="background-color: #ff9966; height: 25px">
                                                <td valign="Center">
                                                    <span style="color: White; font-size: 10pt; font-weight: bold;">Total Record:
                                                                 <asp:Label ID="lblNoofAC" runat="server" Text=""></asp:Label></span>
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                 &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                 &nbsp; &nbsp; &nbsp; &nbsp;
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Storage Bill Detail
                                                                 <asp:Label ID="lblcropyr" runat="server" Text=""></asp:Label></span></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <div style="height: 340px; widows: 100%; overflow: scroll;">
                                                        <%--<asp:GridView ID="gvBOBillApp" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="Bill_Number" AllowPaging="False" Width="100%"
                                                            Font-Size="10pt" BorderColor="Navy" BorderWidth="1px"
                                                            TabIndex="4" CellPadding="4" CellSpacing="2">
                                                            <Columns>


                                                                <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number" SortExpression="Bill_Number">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Godown_ID" HeaderText="Godown_Id" SortExpression="Godown_ID">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown" SortExpression="Godown_Name">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                
                                                             
                                                                
                                                                <asp:TemplateField HeaderText="Total Bill Amount">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtweight" Font-Bold="true" Enabled="false" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,6)"
                                                                            Text='<%# Eval("Net_Amount") %>'>0</asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Select">
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
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCC99" />
                                                            <PagerStyle BackColor="#719cb6" ForeColor="Black" HorizontalAlign="center" />
                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                Height="20px" Font-Size="10pt" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>--%>
                                                        <asp:GridView ID="gvBOBillApp" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="Bill_Number" AllowPaging="False" Width="100%"
                                                            Font-Size="10pt" BorderColor="Navy" BorderWidth="1px"
                                                            TabIndex="4" CellPadding="4" CellSpacing="2">
                                                            <Columns>


                                                                <asp:BoundField DataField="Bill_Number" HeaderText="Bill Number" SortExpression="Bill_Number">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Godown_Id" HeaderText="Godown_Id" SortExpression="Godown_Id">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Godown" HeaderText="Godown" SortExpression="Godown">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Billing_Date" HeaderText="Billing Date" SortExpression="Billing_Date">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <%-- <asp:BoundField DataField="Charges_Amount" SortExpression="Charges_Amount" HeaderText="Charges Amount">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>--%>
                                                                <asp:TemplateField HeaderText="Charges Amount">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtcharges" Font-Bold="true" Enabled="false" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,6)"
                                                                            Text='<%# Eval("Charges_Amount") %>'>0</asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- <asp:BoundField DataField="GST_AMT" SortExpression="GST_AMT" HeaderText="GST Amount">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>--%>
                                                                <asp:TemplateField HeaderText="GST Amount">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtGSTAmt" Font-Bold="true" Enabled="false" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,6)"
                                                                            Text='<%# Eval("GST_AMT") %>'>0</asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <%-- <asp:BoundField DataField="Sup_Charges_Amt" SortExpression="Sup_Charges_Amt" HeaderText="Supervision Charges">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>--%>
                                                                <asp:TemplateField HeaderText="Supervision Charges">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtSupcharges" Font-Bold="true" Enabled="false" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,6)"
                                                                            Text='<%# Eval("Sup_Charges_Amt") %>'>0</asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <%-- <asp:BoundField DataField="GST_Sup_Amt" SortExpression="GST_Sup_Amt" HeaderText="GST(18%) on Supervision Charges">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>--%>
                                                                <asp:TemplateField HeaderText="GST(18%) on Supervision Charges">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtGSTPer" Font-Bold="true" Enabled="false" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,6)"
                                                                            Text='<%# Eval("GST_Sup_Amt") %>'>0</asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <%-- <asp:BoundField DataField="Net_Amount" HeaderText="Net Amount" SortExpression="Net_Amount">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>--%>
                                                                <%-- <asp:BoundField DataField="Bill_Month" HeaderText="Bill Month" SortExpression="Bill_Month">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>--%>


                                                                <%--  <asp:TemplateField HeaderText="Send Bags">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="sendb" BackColor="Transparent" Enabled="false" Font-Bold="true" runat="server" Width="60px" MaxLength="5" onblur="Spc_validatorInt(this)" Text='<%# Eval("Recd_Bags") %>'>0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="80px" />
                                                                        </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Send Qty.">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="sendq"  BackColor="Transparent"  Enabled="false" Font-Bold="true" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,5)"
                                                                                    onblur="Spc_validatornumeric(this)" Text='<%# Eval("Recd_Qty") %>'>0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>

                                                                <%--<asp:TemplateField HeaderText="Receive Bags">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtbagnumber" BackColor="#ffe09f" Font-Bold="true" runat="server" Width="60px" MaxLength="5" onblur="Spc_validatorInt(this)" Text='<%# Eval("Recd_Bags2") %>'>0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="80px" />
                                                                        </asp:TemplateField>
                                                                --%>
                                                                <asp:TemplateField HeaderText="Total Bill Amount">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtweight" Font-Bold="true" Enabled="false" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,6)"
                                                                            Text='<%# Eval("Net_Amount") %>'>0</asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Select">
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="tblbtn" runat="server" visible="false">
                                                        <%--<tr align="center">
                                                        <td align="left" class="style3">
                                                              <asp:Label ID="Label7" runat="server" Font-Bold="True" 
                                                                Font-Size="14px" ForeColor="Navy" Text="Total Bags Send"></asp:Label>
                                                        </td>
                                                        <td align="left" class="style3">
                                                             <asp:Label ID="lblTotalBagSend" runat="server" Text="0" Font-Size="14px" Font-Bold="true"
                                                               ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" class="style3">
                                                            <asp:Label ID="Label10" runat="server" Font-Size="14px" Font-Bold="true"
                                                                Text="Total Qty. Send" ForeColor="navy"></asp:Label>
                                                        </td>
                                                        <td align="left" class="style3">
                                                               <asp:Label ID="lblTotalQtySend" runat="server" Font-Size="14px" Font-Bold="true"
                                                                Text="0" ForeColor="navy"></asp:Label>
                                                         
                                                        </td>
                                                    </tr>--%>

                                                        <%-- Summary table Details--%>

                                                        <tr>
                                                            <td colspan="4" style="height: 10px"></td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="left" style="width: 200px; color: #0066CC;">
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Green"
                                                                    Font-Size="14px" Text="Final Bill Number"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblBill_Number" runat="server" Text="0" Font-Size="14px" Font-Bold="true" ForeColor="Red"></asp:Label>


                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="Label10" runat="server" Font-Size="14px" Font-Bold="true"
                                                                    Text="Bill_Count" ForeColor="Green"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblBill_Count" runat="server" Font-Size="14px" Font-Bold="true"
                                                                    Text="0" ForeColor="Red"></asp:Label>

                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td colspan="4" style="height: 10px"></td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="left" style="width: 200px; color: #0066CC;">
                                                                <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="Green"
                                                                    Font-Size="14px" Text="Net_Amount"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblNet_Amount" runat="server" Text="0" Font-Size="14px" Font-Bold="true" ForeColor="Red"></asp:Label>


                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="Label14" runat="server" Font-Size="14px" Font-Bold="true"
                                                                    Text="Sub_Amount" ForeColor="Green"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblSub_Amount" runat="server" Font-Size="14px" Font-Bold="true"
                                                                    Text="0" ForeColor="Red"></asp:Label>

                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td colspan="4" style="height: 10px"></td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="left" style="width: 200px; color: #0066CC;">
                                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#0066cc"
                                                                    Font-Size="14px" Text="Selected Record"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblTotalBags" EnableViewState="false" ViewStateMode="Disabled" runat="server" Text="0" Font-Size="14px" Font-Bold="true" ForeColor="#0066cc"></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hdnLabelState" />

                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="Label4" runat="server" Font-Size="14px" Font-Bold="true"
                                                                    Text="Total Charges" ForeColor="#0066cc"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblTotalCharges" runat="server" Font-Size="14px" Font-Bold="true"
                                                                    Text="0" ForeColor="#0066cc"></asp:Label>
                                                                <asp:HiddenField runat="server" ID="HiddenField11" />
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="4" style="height: 20px"></td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#0066cc"
                                                                    Font-Size="14px" Text="GST Amount"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblGSTAmt" EnableViewState="false" ViewStateMode="Disabled" runat="server" Text="0" Font-Size="14px" Font-Bold="true" ForeColor="#0066cc"></asp:Label>
                                                                <asp:HiddenField runat="server" ID="HiddenField12" />

                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="Label9" runat="server" Font-Size="14px" Font-Bold="true"
                                                                    Text="Supervision Charges" ForeColor="#0066cc"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblSupCharge" runat="server" Font-Size="14px" Font-Bold="true"
                                                                    Text="0" ForeColor="#0066cc"></asp:Label>
                                                                <asp:HiddenField runat="server" ID="HiddenField13" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="height: 20px"></td>
                                                        </tr>

                                                        <tr align="center">
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#0066cc"
                                                                    Font-Size="14px" Text="GST on Supervision"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblGSTonSup" EnableViewState="false" ViewStateMode="Disabled" runat="server" Text="0" Font-Size="14px" Font-Bold="true" ForeColor="#0066cc"></asp:Label>
                                                                <asp:HiddenField runat="server" ID="HiddenField14" />

                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="Label6" runat="server" Font-Size="14px" Font-Bold="true"
                                                                    Text="Total Bill Amount" ForeColor="#0066cc"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblTotalQty" runat="server" Font-Size="14px" Font-Bold="true"
                                                                    Text="0" ForeColor="#0066cc"></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hdnLabelStateQty" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="height: 20px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="center">
                                                                <asp:Button ID="btn_save" runat="server" Text="Proceed" CssClass="BTNBLUE" Width="100px"
                                                                    Enabled="False" TabIndex="13" ValidationGroup="SaveValid" OnClick="btn_save_Click" />
                                                                &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btn_Close" runat="server" Text="Cancel"
                                                                CssClass="BTNBLUE" Width="100px" CausesValidation="false" />


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlCofirmmsg" TargetControlID="Label8"
                                                                    BackgroundCssClass="modalBackground">
                                                                </asp:ModalPopupExtender>
                                                                <asp:Panel ID="pnlCofirmmsg" runat="server" CssClass="modalPopup" Height="360px" Width="700px">
                                                                    <div class="header">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="color: White; font-weight: bold; font-size: large;" align="center">Bill Summary</td>
                                                                                <td style="width: 50px">
                                                                                    <asp:Button ID="btnNo" runat="server" Text="Close" CssClass="no" align="left" OnClick="btnNo_Click" />
                                                                                </td>
                                                                            </tr>

                                                                        </table>
                                                                    </div>
                                                                    <div class="body">
                                                                        <table cellspacing="1" style="width: 100%; height: 317px;">
                                                                            <tr>
                                                                                <td style="height: 15px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="font-size: 12px; font-family: Arial; font-weight: bold" align="left">
                                                                                    <table width="100%">
                                                                                        <tr style="height: 15px;">
                                                                                            <td style="width: 130px;">&nbsp;Bill Category :
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblBillCategory" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 150px;">Commodity Name :
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblCommodity" runat="server"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 15px;">
                                                                                            <td>&nbsp;Crop Year :
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblCropYear" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td>Month :</td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblMonths" runat="server"></asp:Label>
                                                                                            </td>

                                                                                        </tr>
                                                                                        <tr style="height: 15px;">
                                                                                            <td>&nbsp;Rate Per Month :
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblRPM" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td>Rate Per Day :</td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblRPD" runat="server"></asp:Label>
                                                                                            </td>

                                                                                        </tr>
                                                                                        <tr style="height: 15px;">
                                                                                            <td>&nbsp;No of Godown :
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td>Total Charges	 :</td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblSTotalCharges" runat="server"></asp:Label>
                                                                                            </td>

                                                                                        </tr>
                                                                                        <tr style="height: 15px;">
                                                                                            <td>&nbsp;GST Amount :
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblSGSTAmt" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td>Supervision Charges :</td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblSSupCharges" runat="server"></asp:Label>
                                                                                            </td>

                                                                                        </tr>
                                                                                        <tr style="height: 15px;">
                                                                                            <td>&nbsp;GST on Supervision :
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblSGSTonSupChar" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td>Bill Amount :</td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblBillAmount" runat="server"></asp:Label>
                                                                                            </td>

                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td align="center">
                                                                                    <asp:Button class="button button2" Width="150px" Height="30px" ID="Button2" Visible="false"
                                                                                        runat="server" Text="Generate Bill" align="Center" OnClick="Button2_Click" />

                                                                                    <%--  <asp:Button class="button button2" id="Button1" runat="server" Text="Print Bill"  
                                       Width="150px" Height="30px" align="Center" Visible="false"/>  --%>

                                                                                    <asp:Button class="button button2" Width="150px" Height="30px" ID="btnNewBill" Visible="false"
                                                                                        runat="server" Text="New Bill" align="Center" OnClick="btnNewBill_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <asp:Label ID="hdnBillCategoryID" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                       <asp:Label ID="hdnDepositorID" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                       <asp:Label ID="hdnCommodityID" runat="server" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblrmsg" runat="server" Visible="false" ForeColor="#339933" Font-Bold="true" Font-Size="Large"></asp:Label>

                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </asp:Panel>
                                                            </td>
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

