<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Rpt_SearchStatus_By_Final_Bill.aspx.cs" Inherits="Reports_States_Rpt_SearchStatus_By_Final_Bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function SingleCheckboxCheck(ob) {
            var gridvalue = ob.parentNode.parentNode.parentNode;
            var inputs = gridvalue.getElementsByTagName("input");

            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (ob.checked && inputs[i] != ob && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
        }

        function onlyNumbers(evt) {
            var AsciiCode = event.keyCode;
            var txt = evt.value;
            var txt2 = String.fromCharCode(AsciiCode);
            var txt3 = txt2 * 1;
            if ((AsciiCode < 46) || (AsciiCode > 57)) {
                alert('Please enter only numbers.');
                event.cancelBubble = true;
                event.returnValue = false;
            }

            var num = evt.value;
            var len = num.length;
            var indx = -1;
            indx = num.indexOf('.');
            if (indx != -1) {
                var dgt = num.substr(indx, len);
                var count = dgt.length;
                //alert (count);

                if (AsciiCode == 46) {
                    if (num.split(".").length > 1) {
                        alert('दशमलव एक ही बार आ सकता है |');
                        return false;
                    }
                }

                if (count > 9) {
                    alert("Only 9 decimal digits allowed");
                    event.cancelBubble = true;
                    event.returnValue = false;
                }



            }

        }


    </script>


    <script type="text/javascript">

        function CheckRow(objRef) {

            //Get the Row based on checkbox

            var row = objRef.parentNode.parentNode;

            if (objRef.checked) {

                //Change the gridview row color when checkbox checked change

                row.style.backgroundColor = "#5CADFF";

            }

            else {

                
                if (row.rowIndex % 2 == 0) {

                    //Alternating Row Color

                    //                  row.style.backgroundColor = "#AED6FF";
                    row.style.backgroundColor = "#f2f2f2";
                }

                else {

                    row.style.backgroundColor = "white";

                }

            }

            //Get the reference of GridView

            var GridView = row.parentNode;
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                var headerCheckBox = inputList[0];
                var checked = true;

                if (inputList[i].type == "checkbox" && inputList[i]

                                               != headerCheckBox) {

                    if (!inputList[i].checked) {

                        checked = false;

                        break;

                    }

                }

            }

            headerCheckBox.checked = checked;

        }

        function checkAllRow(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef

                                                != inputList[i]) {

                    if (objRef.checked) {



                        row.style.backgroundColor = "#5CADFF";

                        inputList[i].checked = true;

                    }

                    else {

                       
                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            //                          row.style.backgroundColor = "#AED6FF";
                            row.style.backgroundColor = "#f2f2f2";

                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }

    </script>
    <table class="table table-bordered" style="margin: auto; margin-top: 10px; margin-bottom: 5px;"
        width="100%">

        <asp:Panel ID="pnldata" runat="server" Visible="false">
        <table class="table table-bordered" style="margin: auto; margin-top: 10px; margin-bottom: 5px;" width="100%">

            <tr id="tr3" runat="server" align="center">
                <td style="width: 100%" colspan="2" class="list-group-item-danger">
                    <asp:Label ID="Label4" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Verification for Issue Center "></asp:Label>
                    <asp:Panel ID="Panel3" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="grd_details" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>
                                <asp:TemplateField> <ItemTemplate>  <%#Container.DataItemIndex+1 %> </ItemTemplate> </asp:TemplateField>
                                <asp:BoundField DataField="BillNo" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                <asp:BoundField DataField="Crop_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Financial_Year" SortExpression="Date" />
                                <asp:BoundField DataField="Commodity_Name" ControlStyle-BorderWidth="50px" HeaderText="Commodity_Name" SortExpression="Date" />
                                <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month" SortExpression="Date" />
                                <asp:BoundField DataField="WearHouse_NetAmt" ControlStyle-BorderWidth="50px" HeaderText="WharHouse Amount" SortExpression="Date" />
                                <asp:BoundField DataField="CSMS_NetAmt" ControlStyle-BorderWidth="50px" HeaderText="Csms Amount" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Type" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />
                               
                            </Columns>
                            <EmptyDataRowStyle BackColor="Pink" />
                        </asp:GridView>
                    </asp:Panel>

                </td>
            </tr>

            <tr id="tr1" runat="server" align="center">
                <td style="width: 100%" colspan="2" class="list-group-item-danger">
                    <asp:Label ID="Label1" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Digital Sign at Issue Center level "></asp:Label>
                    <asp:Panel ID="Panel1" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="grd_digital_sign" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>
                                 <asp:TemplateField> <ItemTemplate>  <%#Container.DataItemIndex+1 %> </ItemTemplate> </asp:TemplateField>
                                <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Financial Year" SortExpression="Date" />
                                <asp:BoundField DataField="Crop_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                <asp:BoundField DataField="Commodity_Name" ControlStyle-BorderWidth="50px" HeaderText="Commodity" SortExpression="Date" />
                                <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month" SortExpression="Date" />
                                <asp:BoundField DataField="Net_Amount" ControlStyle-BorderWidth="50px" HeaderText="WharHouse Amount" SortExpression="Date" />
                                <asp:BoundField DataField="Sub_Amount" ControlStyle-BorderWidth="50px" HeaderText="Csms Amount" SortExpression="Date" />

                            </Columns>
                            <EmptyDataRowStyle BackColor="Pink" />
                        </asp:GridView>
                    </asp:Panel>

                </td>
            </tr>

            <tr id="tr4" runat="server" align="center">
                <td style="width: 100%" colspan="2" class="list-group-item-danger">
                    <asp:Label ID="Label3" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bill at DM Level "></asp:Label>
                    <asp:Panel ID="Panel4" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="grdDMlevel" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>
                                 <asp:TemplateField> <ItemTemplate>  <%#Container.DataItemIndex+1 %> </ItemTemplate> </asp:TemplateField>
                                <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />
                                <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                <asp:BoundField DataField="Commodity_Name" ControlStyle-BorderWidth="50px" HeaderText="Commodity" SortExpression="Date" />
                                <asp:BoundField DataField="Crop_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Financial Year" SortExpression="Date" />
                                <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month " SortExpression="Date" />
                                <asp:BoundField DataField="CSMS_Net_Amount" ControlStyle-BorderWidth="50px" HeaderText="Net Amount" SortExpression="Date" />
                               
                            </Columns>
                            <EmptyDataRowStyle BackColor="Pink" />
                        </asp:GridView>
                    </asp:Panel>

                </td>
            </tr>

            <tr id="tr2" runat="server" align="center">
                <td style="width: 100%" colspan="2" class="list-group-item-danger">
                    <asp:Label ID="Label2" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Digital Signature at DM Level "></asp:Label>
                    <asp:Panel ID="Panel2" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="grdDMlevelDigital" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>
                                 <asp:TemplateField> <ItemTemplate>  <%#Container.DataItemIndex+1 %> </ItemTemplate> </asp:TemplateField>
                                <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />
                                <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                <asp:BoundField DataField="Commodity_Name" ControlStyle-BorderWidth="50px" HeaderText="Commodity" SortExpression="Date" />
                                <asp:BoundField DataField="Crop_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Financial Year" SortExpression="Date" />
                                <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month" SortExpression="Date" />
                                <asp:BoundField DataField="Net_Amount" ControlStyle-BorderWidth="50px" HeaderText="Net Amount" SortExpression="Date" />
                               
                            </Columns>
                            <EmptyDataRowStyle BackColor="Pink" />
                        </asp:GridView>
                    </asp:Panel>

                </td>
            </tr>

            <tr id="tr5" runat="server" align="center">
                <td style="width: 100%" colspan="2" class="list-group-item-danger">
                    <asp:Label ID="Label5" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bill Payed Via NEFT "></asp:Label>
                    <asp:Panel ID="Panel5" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="grdNeft" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>
                                <asp:TemplateField> <ItemTemplate>  <%#Container.DataItemIndex+1 %> </ItemTemplate> </asp:TemplateField>
                                <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />
                                <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                <asp:BoundField DataField="Commodity_Name" ControlStyle-BorderWidth="50px" HeaderText="Commodity" SortExpression="Date" />
                                <asp:BoundField DataField="Crop_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Financial Year" SortExpression="Date" />
                                <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month" SortExpression="Date" />
                                <asp:BoundField DataField="Net_Amount" ControlStyle-BorderWidth="50px" HeaderText="Net Amount" SortExpression="Date" />
                                <asp:BoundField DataField="DistrictLotId" ControlStyle-BorderWidth="50px" HeaderText="DistrictLotId" SortExpression="Date" />
                                <asp:BoundField DataField="HoLotID" ControlStyle-BorderWidth="50px" HeaderText="HoLotID" SortExpression="Date" />

                            </Columns>
                            <EmptyDataRowStyle BackColor="Pink" />
                        </asp:GridView>
                    </asp:Panel>

                </td>
            </tr>

             <tr id="tr6" runat="server" align="center">
                <td style="width: 100%" colspan="2" class="list-group-item-danger">
                    <asp:Label ID="Label6" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bills Neft Response "></asp:Label>
                    <asp:Panel ID="Panel6" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="gridNeftUTR" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>
                                <asp:TemplateField> <ItemTemplate>  <%#Container.DataItemIndex+1 %> </ItemTemplate> </asp:TemplateField>
                                <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Ref_Bill_No" SortExpression="Date" />
                                <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                <asp:BoundField DataField="Branch_Name" ControlStyle-BorderWidth="50px" HeaderText="Branch_Name" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                <asp:BoundField DataField="Payable_Amount" ControlStyle-BorderWidth="50px" HeaderText="Net Amount" SortExpression="Date" />
                                <asp:BoundField DataField="BranchBill_BankUTRNo" ControlStyle-BorderWidth="50px" HeaderText="UTR No" SortExpression="Date" />
                                <asp:BoundField DataField="BranchBillPaymentDate" ControlStyle-BorderWidth="50px" HeaderText="Payment Date" SortExpression="Date" />
                               

                            </Columns>
                            <EmptyDataRowStyle BackColor="Pink" />
                        </asp:GridView>
                    </asp:Panel>

                </td>
            </tr>

        </table>
    </asp:Panel>
        <br />

    </table>
    
  
    <script src="https://code.jquery.com/jquery-1.12.4.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <%-- <link href="../CSS/FormStyleSheet.css" rel="stylesheet" title="Calendar" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://jqueryui.com/resources/demos/style.css" type="text/css" />
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">--%>
</asp:Content>