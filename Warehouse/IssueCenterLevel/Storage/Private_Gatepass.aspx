<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="Private_Gatepass.aspx.cs" Inherits="IssueCenterLevel_Storage_Private_Gatepass"
    Title="Private Gatepass" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function popMe(url) {
            var newWindow;
            newWindow = window.open(url, 'MyWin', 'width=275,height=390,top=1,left=1');
        }
        function OpenWindow(Sno) {
            window.open("Gate_Pass.aspx?src=RO&vu=" + Sno, "_new", "height=800,width=780");
        }
        //percentage
        function validate() {
            // Percent = document.frmPost.percent.value
            if ((document.ctl00_ContentPlaceHolder1_txtmoisture.value.indexOf(".") == -1) && (document.ctl00_ContentPlaceHolder1_txtmoisture.value.length >= 3)) {
                alert("Percentage is not in Correct Format");
                document.ctl00_ContentPlaceHolder1_txtmoisture.value = "";
                document.ctl00_ContentPlaceHolder1_txtmoisture.focus();
                return false;
            }
            if ((document.ctl00_ContentPlaceHolder1_txtmoisture.value.indexOf(".")) == 4 || (document.ctl00_ContentPlaceHolder1_txtmoisture.value.indexOf(".")) == 3 || (document.ctl00_ContentPlaceHolder1_txtmoisture.value.indexOf(".")) == 0) {
                alert("Invalid Percentage");
                document.ctl00_ContentPlaceHolder1_txtmoisture.value = "";
                document.ctl00_ContentPlaceHolder1_txtmoisture.focus();
                return false;
            }
            if (isNaN(document.ctl00_ContentPlaceHolder1_txtmoisture.value) == true) {
                alert("Enter Numeric values");
                document.ctl00_ContentPlaceHolder1_txtmoisture.value = "";
                document.ctl00_ContentPlaceHolder1_txtmoisture.focus();
                return false;
            }
            return true;
        }
        function validateDate1() {
            var input = document.getElementById('Ddpaymentparameters1_txtdddate')
            alert(input);
            var validformat = /^\d{1,2}\/\d{1,2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input.value))
                alert('Invalid Date Format. Please correct.')
            else { //Detailed check for valid date ranges
                var dayfield = input.value.split("/")[0]
                var monthfield = input.value.split("/")[1]
                var yearfield = input.value.split("/")[2]

                var dayobj = new Date(yearfield, monthfield - 1, dayfield)
                if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield))
                    alert('Invalid Day, Month, or Year range detected. Please correct.')
                else {
                    returnval = true
                }
            }
            if (returnval == false)
                input.value = ""
            return returnval
        }
        function FillIssueQty(abc) {
            document.getElementById('txtIssuedQty').value = abc.value;
        }
        function FillValueStock() {
            val = parseFloat((document.getElementById('txtIssuedQty').value) * (document.getElementById('txtROrate').value));
            //alert(val.toFixed(2));
            document.getElementById('txtROvalue').value = val.toFixed(2); //parseFloat((document.getElementById('txtIssuedQty').value)*(document.getElementById('txtROrate').value));
        }
        function SelectAll(id) {
            var frm = document.forms[0];
            for (i = 0; i < frm.elements.length; i++) {
                if (frm.elements[i].type == "checkbox") {
                    frm.elements[i].checked = document.getElementById(id).checked;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 960px; border: 1px solid navy; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px;
        padding-left: 0px; margin-left: 15px">
        <center>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td colspan="6" align="center" valign="top">
                                <fieldset style="width: 980px; border: 1px solid navy;">
                                    <center>
                                        <div>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr style="background-color: #0bb6e6; height: 25px">
                                                    <td colspan="3" align="center">
                                                        <asp:Label ID="lblDeliveryOrderOfStock" runat="server" Text="Delivery GatePass Details(Private Commodity)"
                                                            ForeColor="whitesmoke" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <a href="javascript:popMe('../../Help/DeleveryGatePassHelp.htm');" style="text-decoration: underline;
                                                            color: White; font-size: 10pt; font-weight: bold">(FOR HELP)</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <asp:Label ID="lblmsg" runat="server" Font-Size="10pt" ForeColor="Red" EnableViewState="False"
                                                            Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="left">
                                                        <asp:Label ID="lblInstruction" runat="server" Text="Note :- (*) Fields are Mandatory ( Values in Rs. ) and "
                                                            Font-Bold="True" ForeColor="red"></asp:Label><a href="javascript:popMe('../../SampleQuantity.htm');"
                                                                style="text-decoration: underline; color: Red">(Qty. in Qtls.kgsgms)</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 120px">
                                                        <asp:Label ID="lblDepositorType" runat="server" Text="Depositor Type " Font-Size="8pt"
                                                            Font-Bold="true" ForeColor="navy"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 200px" valign="middle">
                                                        <asp:DropDownList ID="ddlDepositorType" runat="server" Font-Size="10pt" AutoPostBack="True"
                                                            TabIndex="1" Height="30px" Width="200px" CssClass="tb6" OnSelectedIndexChanged="ddlDepositorType_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left" style="width: 150px">
                                                        <asp:Label ID="lblDepositorName" runat="server" Text="Depositor Name " Font-Size="8pt"
                                                            Font-Bold="true" ForeColor="navy"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 200px" valign="middle">
                                                        <asp:DropDownList ID="ddlDepositor" runat="server" Font-Size="10pt" TabIndex="2"
                                                            AutoPostBack="True" Height="30px" Width="200px" CssClass="tb6" OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5px" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100px">
                                                        <asp:Label ID="lblIssuedTo" runat="server" Text="Issued To" Font-Size="8pt" Font-Bold="true"
                                                            ForeColor="navy"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 120px">
                                                        <asp:DropDownList ID="ddlDeliveredAgnt" runat="server" Font-Size="10pt" TabIndex="3"
                                                            Height="30px" Width="200px" CssClass="tb6" AutoPostBack="true" 
                                                            OnSelectedIndexChanged="ddlDeliveredAgnt_SelectedIndexChanged" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblGodownNo" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                            Text="Godown -"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlGodown" runat="server" Font-Size="10pt" Height="30px" Width="200px"
                                                            TabIndex="6" AutoPostBack="true" CssClass="tb6" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5px" colspan="4">
                                                        <asp:Label ID="lblcom" runat="server" Font-Bold="true" Font-Size="8pt" ForeColor="Transparent"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="trdo" runat="server">
                                                    <td align="left">
                                                        <asp:Label ID="lblComm" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                                            Text="Commodity"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:DropDownList ID="ddlcomm" CssClass="tb6" runat="server" Height="30px" Width="200px"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlcomm_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtTransId" runat="server" BackColor="Transparent" BorderStyle="None"
                                                            MaxLength="10" Width="0px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="Stockdetails" runat="server" visible="false">
                                                    <td colspan="6" align="left" valign="top">
                                                        <fieldset style="width: 980px; border: 1px solid navy;">
                                                            <center>
                                                                <div>
                                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                        <tr style="background-color: #0bb6e6; height: 25px">
                                                                            <td align="center">
                                                                                <asp:Label ID="lblStockforIssue" runat="server" Text="WHR Details & Available Stock For Issue"
                                                                                    ForeColor="whitesmoke" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" align="center">
                                                                                <asp:Label ID="lblnotfound" runat="server" Text="" ForeColor="red" Font-Bold="true"
                                                                                    Font-Size="12pt" Visible="false"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 10px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span style="color: Red; font-weight: bold; font-size: 10pt">Note - जिस WHR से स्टॉक
                                                                                    Issue करना है,उसमे बोरी की मात्रा नंबर में,वजन (Qty. in Qtls.kgsgms) में प्रविष्ट
                                                                                    करें ! </span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 10px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" align="center">
                                                                                <div style="overflow: scroll; height: 200px; overflow-x: hidden">
                                                                                    <asp:GridView ID="gdstackdetail" runat="server" CellPadding="2" TabIndex="10" Width="100%"
                                                                                        ForeColor="navy" GridLines="Both" AutoGenerateColumns="false">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="SNo" HeaderText="S.No.">
                                                                                                <ItemStyle HorizontalAlign="center" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Depositor_Name" HeaderText="Depositer Name">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Depositor_whr_id" HeaderText="Whr No">
                                                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Stack_Name" HeaderText="StackNo">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="AvailBags" HeaderText="Available Bags">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="AvailQty" HeaderText="Available Weight">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Lot_no" HeaderText="Lot No.">
                                                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:TemplateField HeaderText="Bags">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtbagnumber" runat="server" Width="60px" MaxLength="5" onblur="Spc_validatorInt(this)">0</asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="80px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Weight">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtweight" runat="server" Width="65px" MaxLength="12" onkeyup="NumericDecimalCheck(this,5)"
                                                                                                        onblur="Spc_validatornumeric(this)">0</asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Select">
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="ckstack" runat="server" AutoPostBack="True" OnCheckedChanged="ckstack_CheckedChanged" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Stack_ID" HeaderText="stackid">
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" BorderColor="#FFC080" HorizontalAlign="Center" />
                                                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" BorderColor="White" />
                                                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                                            Height="20px" Font-Size="10pt" />
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </center>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                                <tr id="Issuedetails" runat="server" visible="false">
                                                    <td colspan="6" align="center" valign="top">
                                                        <fieldset style="width: 980px; border: 1px solid navy;">
                                                            <center>
                                                                <div>
                                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                        <tr style="background-color: #0bb6e6; height: 25px">
                                                                            <td colspan="4" align="center">
                                                                                <asp:Label ID="Label4" runat="server" Text="Issuing Details" ForeColor="whitesmoke"
                                                                                    Font-Bold="true" Font-Size="12pt"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 10px" colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="width: 200px">
                                                                                <asp:Label ID="lblIssuedBags" runat="server" Text="Issued Bags" Font-Size="8pt" Font-Bold="true"
                                                                                    ForeColor="navy"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="width: 200px">
                                                                                <asp:TextBox ID="txtissuedbags" runat="server" Width="150px" Height="20px" Enabled="False"
                                                                                    BackColor="LemonChiffon" TabIndex="9" CssClass="tb6"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left" style="width: 200px">
                                                                                <asp:Label ID="lblIssuedweight" runat="server" Style="position: static" Text="Issued Weight -"
                                                                                    Font-Bold="true" ForeColor="navy"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtissuedwt" runat="server" Width="170px" Height="20px" Enabled="False"
                                                                                    BackColor="LemonChiffon" TabIndex="10" CssClass="tb6"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 5px" colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblPercentMoisture" runat="server" Text="Moisture Content (%) -" Font-Size="8pt"
                                                                                    Font-Bold="true" ForeColor="navy"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtmoisture" runat="server" Width="150px" Height="20px" MaxLength="13"
                                                                                    TabIndex="11" CssClass="tb6"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left" style="width: 150px">
                                                                                <asp:Label ID="lblTrans" runat="server" Text="Name of Transporter -" Font-Size="8pt"
                                                                                    Font-Bold="true" ForeColor="navy"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="UxTrans" runat="server" Font-Size="8pt" Height="30px" Width="175px"
                                                                                    TabIndex="14" CssClass="tb6">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 5px" colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblTruckNumber" runat="server" Text="Vehicle No." Font-Size="8pt"
                                                                                    Font-Bold="true" ForeColor="navy"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txttruckno" runat="server" Width="150px" Height="20px" MaxLength="20"
                                                                                    TabIndex="16" CssClass="tb6"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblArrivalTime" runat="server" Text="Departure Time" Font-Size="8pt"
                                                                                    Font-Bold="true" ForeColor="navy"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="ddl1" runat="server" Font-Size="8pt" Height="30px" TabIndex="17"
                                                                                    CssClass="tb6" Width="50px">
                                                                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                                                    <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <strong>: </strong>
                                                                                <asp:DropDownList ID="ddl2" runat="server" Font-Size="8pt" Height="30px" Width="50px"
                                                                                    TabIndex="18" CssClass="tb6">
                                                                                </asp:DropDownList>
                                                                                <strong>: </strong>
                                                                                <asp:DropDownList ID="ddl3" runat="server" Font-Size="8pt" Height="30px" CssClass="tb6"
                                                                                    TabIndex="19" Width="50px">
                                                                                    <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                                    <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 5px" colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblTypeVehicle" runat="server" Text="Type of Vehicle" Font-Size="8pt"
                                                                                    Font-Bold="true" ForeColor="navy"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="ddlVehicleType" runat="server" Font-Size="8pt" onChange="DisabledDetails();"
                                                                                    Width="155px" AutoPostBack="false" TabIndex="15" Height="30px" CssClass="tb6">
                                                                                    <asp:ListItem Text="Bullock Cart" Value="Bullock Cart"></asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Text="Truck" Value="Truck"></asp:ListItem>
                                                                                    <asp:ListItem Text="Tractor" Value="Tractor"></asp:ListItem>
                                                                                    <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lbIssusedQty" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                                                                    Text="Issued Qty : " Visible="False"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblIssusedQty" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                                                                    Text="0" Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 5px" colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                        <tr visible="false" runat="server" id="trOtherDepot">
                                                                            <td align="left">
                                                                                <asp:Label ID="lblRecDistrict" runat="server" Text="Receipient District" Font-Size="8pt"
                                                                                    Font-Bold="true" ForeColor="navy"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="ddlRecDistrict" runat="server" AutoPostBack="True" DataTextField="District_Name"
                                                                                    Width="155px" Height="25px" DataValueField="District_Id" TabIndex="12" Font-Size="8pt"
                                                                                    CssClass="tb6">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblRecDepot" runat="server" Text="Receipient Depot" Font-Size="8pt"
                                                                                    Font-Bold="true" ForeColor="navy"></asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="ddlRecDepot" runat="server" DataTextField="DepotName" DataValueField="DepotID"
                                                                                    TabIndex="13" Font-Size="8pt" Width="155px" Height="25px" CssClass="tb6">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 5px" colspan="4">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 5px" colspan="4">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 5px" align="left" >
                                                                                <asp:Label ID="lblRecDistrict0" runat="server" Text="GatePass Date" Font-Size="8pt"
                                                                                    Font-Bold="True" ForeColor="Navy"></asp:Label>
                                                                            </td>
                                                                                <td align="left">
                                                                                
                                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" 
                                                                    Enabled="True" TargetControlID="TextBox2" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                            
                                                                                </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="left" visible="true">
                                                                                <asp:Label ID="lblDesc" runat="server" Font-Size="8pt" Font-Bold="true" ForeColor="navy"
                                                                                    Text="Description of Available Papers of Gatepass in Truck">
                                                                                </asp:Label>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtGpid" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                                    Width="0px" BorderStyle="None" CssClass="tb6"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4" visible="true">
                                                                                <asp:TextBox ID="txtTruckDetails" runat="server" Height="50px" MaxLength="1000" TextMode="MultiLine"
                                                                                    Width="750px" TabIndex="20" CssClass="tb6"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </center>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                                <tr visible="true" id="Trbutton" runat="server">
                                                    <td align="center" colspan="6">
                                                        <asp:Button ID="btnsave" runat="server" CausesValidation="False" Text="Save Details"
                                                            TabIndex="21" CssClass="BTNBLUE" OnClientClick="ShowBalance();" Enabled="False"
                                                            ValidationGroup="SaveValid,WLC_OD,FPS_LS" OnClick="btnsave_Click" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnNewMC" runat="server" CausesValidation="False" Text="New Delivery GatePass"
                                                            TabIndex="22" CssClass="BTNBLUE" Enabled="false" OnClick="btnNewMC_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5px" colspan="6">
                                                    </td>
                                                </tr>
                                                <tr id="trlnk" runat="server" visible="false">
                                                    <td runat="server" align="center" colspan="6" visible="true" id="Td1">
                                                        <span style="font-size: 8pt; color: blue; text-decoration: underline;"><a href="#"
                                                            onclick="OpenWindow(<%= txtGpid.Text %>);"><u><b>Issue GatePass</b></u></a></span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </center>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
              <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </center>
    </fieldset>
</asp:Content>
