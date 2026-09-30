<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Rpt_Search_HO_StorageBillVerification_N.aspx.cs" Inherits="Reports_States_Rpt_Search_HO_StorageBillVerification_N" %>

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

                //If checkbox not checked change default row color

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

            //Get all input elements in Gridview

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //The First element is the Header Checkbox

                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes

                //are checked check/uncheck Header Checkbox

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

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

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


    <div class="container" style="clip: rect(0px, auto, auto, auto); top: 0px;">

           <div class="panel-heading clearfix" style="background-color: #FF9900">
              
                    <div class="row">
                        <div class="col-sm-1">
                            <a href="State_Welcome2019.aspx"><span class="glyphicon glyphicon-home danger"></span></a>
                        </div>
                        <div class="col-sm-10">
                            <h4 class="panel-title pull-center text-center">Storage Bill After August 2020 Search Godown Wise</h4>
                        </div>
                        <div class="col-sm-1">
                            <a href="Search_HO_StorageBillVerification.aspx"><span class="glyphicon glyphicon-refresh danger"></span></a>
                        </div>

                    </div>
                
              </div>

        <table class="table table-bordered table-striped">
            <tr runat="server" visible="false">
                <td class="text-left">
                    <h4 class="panel-title pull-center">Search Bill Number 
                      <asp:RadioButton ID="rbtnbillNo" runat="server" GroupName="abc" AutoPostBack="true" OnCheckedChanged="rbtnbillNo_CheckedChanged"></asp:RadioButton>
 </h4>
                </td>
                <td class="text-left">
                   Godown  <asp:RadioButton ID="rbtngodown" runat="server" GroupName="abc" AutoPostBack="true" OnCheckedChanged="rbtngodown_CheckedChanged"></asp:RadioButton>

                </td>
                <td class="text-left">
                </td>
                <td class="text-left">
                   
                </td>
            </tr>
            <tr id="pnlbillno" runat="server" visible="false">
                <td class="text-left">
                    <h4 class="panel-title pull-center">Enter Bill Number </h4>
                </td>
                <td class="text-left">
                    <asp:TextBox ID="txtBillNumber" runat="server"></asp:TextBox>

                </td>
                <td class="text-left"></td>
                <td class="text-left"></td>
            </tr>

            <tr id="pnlgodwnno" runat="server" visible="true">
                <td class="text-left">
                    <h4 class="panel-title pull-center">District </h4>
                </td>
                <td class="text-left">
                    <asp:DropDownList ID="ddldistrict" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged"></asp:DropDownList>

                </td>
                <td class="text-left" id="god" runat="server" visible="false" >Godown
                </td>
                <td class="text-left" id="god1" runat="server" visible="false">

                    <asp:DropDownList ID="ddlgodown"  runat="server"></asp:DropDownList>
                </td>
            </tr>


              <tr id="pnlyear" runat="server" visible="false">
                  <td></td>
                  <td></td>
                <td class="text-left" id="yeartd" runat="server" >Year
                </td>
                <td class="text-left" id="yeartd1" runat="server" >

                    <asp:DropDownList ID="ddlyear"  runat="server"></asp:DropDownList>

                </td>
            </tr>

            <tr id="pnlsave" runat="server" visible="true">
                <td class="text-left">&nbsp;</td>
                <td colspan="2" align="center">
                    <asp:Button ID="btnsave" Text="Search" runat="server" OnClick="btnsave_Click"></asp:Button>

                </td>
                <td class="text-left">&nbsp;</td>
            </tr>
            <tr>

                <td style="width: 100%" colspan="4" class="list-group-item-danger">
                    


                    <br />
                </td>

            </tr>
        </table>
    </div>


    <table class="table table-bordered" style="margin: auto; margin-top: 10px; margin-bottom: 5px;"
        width="100%">

        <asp:Panel ID="pnldata" runat="server" Visible="false">
        <table class="table table-bordered" style="margin: auto; margin-top: 10px; margin-bottom: 5px;"
            width="100%">


            <tr id="tr3" runat="server" align="center">
                <td style="width: 100%" colspan="2" class="list-group-item-danger">
                    <asp:Label ID="Label4" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bill Verification from Issue Center "></asp:Label>
                    <asp:Panel ID="Panel3" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="grd_details" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>

                                <asp:BoundField DataField="BillNo" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />

                                <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                <asp:BoundField DataField="year" ControlStyle-BorderWidth="50px" HeaderText="year" SortExpression="Date" />
                                <asp:BoundField DataField="Crop_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month" SortExpression="Date" />
                                <asp:BoundField DataField="WearHouse_TotAmt" ControlStyle-BorderWidth="50px" HeaderText="WharHouse Amount" SortExpression="Date" />
                                <asp:BoundField DataField="CSMS_TotAmt" ControlStyle-BorderWidth="50px" HeaderText="Csms Amount" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Type" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />
                                <asp:BoundField DataField="Status" ControlStyle-BorderWidth="50px" HeaderText="Status" SortExpression="Date" />

                            </Columns>
                            <EmptyDataRowStyle BackColor="Pink" />
                        </asp:GridView>
                    </asp:Panel>

                </td>
            </tr>

            <tr id="tr1" runat="server" align="center">
                <td style="width: 100%" colspan="2" class="list-group-item-danger">
                    <asp:Label ID="Label1" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bill DSC At IC level "></asp:Label>
                    <asp:Panel ID="Panel1" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="grd_digital_sign" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>

                                <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />

                                <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                <asp:BoundField DataField="year" ControlStyle-BorderWidth="50px" HeaderText="year" SortExpression="Date" />
                                <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
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
                    <asp:Label ID="Label3" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bill Verification At DM Level "></asp:Label>
                    <asp:Panel ID="Panel4" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="grdDMlevel" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>
                                <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />

                                <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />

                                <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                <asp:BoundField DataField="year" ControlStyle-BorderWidth="50px" HeaderText="year" SortExpression="Date" />

                                <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />

                                <asp:BoundField DataField="Party_Name" ControlStyle-BorderWidth="50px" HeaderText="Party Name" SortExpression="Date" />

                                <asp:BoundField DataField="Net_Amount" ControlStyle-BorderWidth="50px" HeaderText="WharHouse Amount" SortExpression="Date" />
                                <asp:BoundField DataField="Sub_Amount" ControlStyle-BorderWidth="50px" HeaderText="Csms Amount" SortExpression="Date" />


                            </Columns>
                            <EmptyDataRowStyle BackColor="Pink" />
                        </asp:GridView>
                    </asp:Panel>

                </td>
            </tr>

            
            <tr id="tr2" runat="server" align="center">
                <td style="width: 100%" colspan="2" class="list-group-item-danger">
                    <asp:Label ID="Label2" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bill DSC At DM Level"></asp:Label>
                    <asp:Panel ID="Panel2" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="grd_DMDSC_Verification" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>
                                <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />

                                <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />

                                <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                <asp:BoundField DataField="year" ControlStyle-BorderWidth="50px" HeaderText="year" SortExpression="Date" />
                                <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month" SortExpression="Date" />

                                <asp:BoundField DataField="Party_Name" ControlStyle-BorderWidth="50px" HeaderText="Csms Amount" SortExpression="Date" />


                                <asp:BoundField DataField="Net_Amount" ControlStyle-BorderWidth="50px" HeaderText="WharHouse Amount" SortExpression="Date" />
                                <asp:BoundField DataField="Sub_Amount" ControlStyle-BorderWidth="50px" HeaderText="Csms Amount" SortExpression="Date" />


                            </Columns>
                            <EmptyDataRowStyle BackColor="Pink" />
                        </asp:GridView>
                    </asp:Panel>

                </td>
            </tr>

            <tr id="tr5" runat="server" align="center">
                <td style="width: 100%" colspan="2" class="list-group-item-danger">
                    <asp:Label ID="Label5" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bill Payment Via NEFT"></asp:Label>
                    <asp:Panel ID="Panel5" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="grdNeft" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>
                                <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Godown Type" SortExpression="Date" />

                                <asp:BoundField DataField="Bill_Number" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />
                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />
                                <asp:BoundField DataField="Financial_Year" ControlStyle-BorderWidth="50px" HeaderText="Crop Year" SortExpression="Date" />
                                <asp:BoundField DataField="year" ControlStyle-BorderWidth="50px" HeaderText="year" SortExpression="Date" />
                                <asp:BoundField DataField="district_name" ControlStyle-BorderWidth="50px" HeaderText="District" SortExpression="Date" />
                                <asp:BoundField DataField="MonthName" ControlStyle-BorderWidth="50px" HeaderText="Month" SortExpression="Date" />
                                <asp:BoundField DataField="Party_Name" ControlStyle-BorderWidth="50px" HeaderText="Party Name" SortExpression="Date" />
                                <asp:BoundField DataField="Net_Amount" ControlStyle-BorderWidth="50px" HeaderText="WharHouse Amount" SortExpression="Date" />
                                <asp:BoundField DataField="Sub_Amount" ControlStyle-BorderWidth="50px" HeaderText="Csms Amount" SortExpression="Date" />
                            </Columns>
                            <EmptyDataRowStyle BackColor="Pink" />
                        </asp:GridView>
                    </asp:Panel>

                </td>
            </tr>

           <%-- <tr id="tr6" runat="server" align="center">
                <td style="width: 100%" colspan="2" class="list-group-item-danger">
                    <asp:Label ID="Label6" runat="server" Style="font-size: 16px; font-weight: bold;" Text="Storage Bill Verification Response Status "></asp:Label>
                    <asp:Panel ID="Panel6" runat="server" Style="width: 1110px; height: 200px; overflow: scroll">
                        <asp:GridView ID="grdResponse" runat="server" AutoGenerateColumns="False"
                            EmptyDataRowStyle-BackColor="pink"
                            EmptyDataText="Record Not Found"
                            CssClass="table table-striped table-hover" ShowFooter="True" EnableModelValidation="True">
                            <Columns>

                                <asp:BoundField DataField="UPID" ControlStyle-BorderWidth="50px" HeaderText="Bill Number" SortExpression="Date" />

                                <asp:BoundField DataField="Ref_Bill_No" ControlStyle-BorderWidth="50px" HeaderText="Ref Bill No" SortExpression="Date" />


                                <asp:BoundField DataField="Godown_Name" ControlStyle-BorderWidth="50px" HeaderText="Godown" SortExpression="Date" />

                                <asp:BoundField DataField="Bank_UTR_No" ControlStyle-BorderWidth="50px" HeaderText="UTR No" SortExpression="Date" />


                                <asp:BoundField DataField="AH_Amount" ControlStyle-BorderWidth="50px" HeaderText="Amount" SortExpression="Date" />

                                <asp:BoundField DataField="Remark" ControlStyle-BorderWidth="50px" HeaderText="Credit_Remark" SortExpression="Date" />


                            </Columns>
                            <EmptyDataRowStyle BackColor="Pink" />
                        </asp:GridView>
                    </asp:Panel>

                </td>
            </tr>--%>

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
