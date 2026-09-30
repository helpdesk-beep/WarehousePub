<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="View_Fill_Inspection_By_Officer.aspx.cs" Inherits="Inspections_RO_View_Fill_Inspection_By_Officer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.3/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.8.3/jquery-ui.js"></script>
    <script src="http://code.jquery.com/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.history.forward();

        function noBack() { window.history.forward(); }
    </script>
    <script type="text/javascript">

        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=300,height=800,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>
    <div style="background-color: #FDFAF7; width: 100%;">
        <input id="btnprint" type="button" onclick="PrintDiv()" value="Print" class="btn btn-warning" />
        <div id="printarea" class="w3-border" style="width: 100%;">
            <asp:Panel ID="pnlContents" runat="server">
                <table align="center" style="width: 100%;">

                    <tr>
                        <td align="center" colspan="3">
                            <table border="1" width="70%">
                                <tbody>
                                    <th>Inspection ID </th>
                                    <th>Inspection Type </th>
                                    <th>Inspection Period</th>
                                    <th>Branch </th>
                                    <th>Inspection Date </th>
                                </tbody>
                                <tr align="center">
                                    <td>
                                        <asp:Label ID="lblinspid" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblinsptype" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInspPeriod" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblbranch" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtinspectiondate" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                </table>
                <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label33" runat="server" Text="निरीक्षण अधिकारी का नाम और पद  : "></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtofficername" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label59" runat="server" Text="भंडार गृह का लाइसेंस नम्बर  : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtlincenceno" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="Label61" runat="server" Text="वैधता दिनांक : "></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtexdate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label60" runat="server" Text="निरक्षण अवधि दिनाँक   : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtinspexdatefrom" runat="server" Width="115px"></asp:TextBox>
                            से
                        </td>

                        <td>
                            <asp:TextBox ID="txtinspexdateto" runat="server" Width="115px"></asp:TextBox>
                            तक
                    <br />
                            <%-- <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Date" ForeColor="Red"
                        runat="server" ControlToValidate="txtinspexdatefrom" ControlToCompare="txtinspexdateto"
                        Operator="LessThan" Type="Date" ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                    <br />--%>

                        </td>
                        <td>
                            <asp:Label ID="Label62" runat="server" Text=" : "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label66" runat="server" Text="पिछले निरिक्षण का दिनांक : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtpriciusinspdate" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label67" runat="server" Text="पिछले निरीक्षण अधिकारी का नाम : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtpriveusinspofficername" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label80" runat="server" Text="पिछले निरीक्षण अधिकारी का पद : "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Desig" runat="server" AutoPostBack="false" Width="222px"
                                Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                <asp:ListItem Value="AQC">AGM</asp:ListItem>
                                <asp:ListItem Value="AQC">AQC</asp:ListItem>
                                <asp:ListItem Value="AQC">AQC(C)</asp:ListItem>
                                <asp:ListItem Value="AQC">QC</asp:ListItem>
                                <asp:ListItem Value="AQC">Manager(QC)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>

                        <td>
                            <asp:Label ID="Label63" runat="server" Text="स्कंध जमा करने के आवेदन क्रमांक  : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtskandhsno" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="Label64" runat="server" Text="दिनाँक से  : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtskandhdatefrom" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label65" runat="server" Text="क्रमांक : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtskandhsnoto" runat="server"></asp:TextBox>

                        </td>
                        <td colspan="2">
                            <asp:Label ID="Label68" runat="server" Text="दिनाँक तक: "></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtskandhdateto" runat="server"></asp:TextBox>

                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label69" runat="server" Text="स्कंध भुगतान पत्र क्र.  : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtskandhpaymentleetersno" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="Label70" runat="server" Text="दिनाँक से  : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtskandhletterdatefrom" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label71" runat="server" Text="क्रमांक : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtskandhpaymentleetersnoto" runat="server"></asp:TextBox>

                        </td>
                        <td>
                            <asp:Label ID="Label72" runat="server" Text="दिनाँक तक: "></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtskandhletterdateto" runat="server"></asp:TextBox>

                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label73" runat="server" Text="अंतिम वेयर हाउस रसीद क्र. : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtlastwarehousereciept" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="Label74" runat="server" Text="दिनाँक : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtwarehouserecieptdate" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label75" runat="server" Text="बाह्यय कीट नाशक विस्तार सेवा योजना (बोरे): "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtschemebore" runat="server"></asp:TextBox>

                        </td>
                        <td>
                            <asp:Label ID="Label76" runat="server" Text="प्राप्त राशि: "></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtamount" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>

                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label77" runat="server" Text="अप्रैल से निरीक्षण तक लाभार्जन की स्थिति : "></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtaprilprofitstatus" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label78" runat="server" Text="वेयरहाउस रसीद रहन का विवरण: "></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtwarerecieptdetaisl" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </td>

                    </tr>

                </table>
                <%-----------------------end of First Gride----------------%>
                <div class="row">
                    <div class="col-lg-12">
                        <h5 class="bd" style="color: Red;">1. शाखा पर ऑनलाईन प्रविष्टियों का विवरण (क्षमता M.T. में)-</h5>
                        <asp:GridView ID="gvCol3" runat="server" AutoGenerateColumns="false" Visible="true">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <tr style="text-align: center;">
                                            <th style="width: 0px"></th>
                                            <th style="width: 0px"></th>
                                            <th style="width: 100px">क्षमता का प्रकार</th>
                                            <th colspan="2" style="text-align: center;">रजिस्ट्रेशन </th>
                                            <th colspan="2" style="text-align: center;">ऑफर्ड</th>
                                            <th colspan="2" style="text-align: center;">अनुबंधित</th>
                                            <th colspan="1" style="text-align: center;">रिमार्क</th>
                                        </tr>
                                        <tr>
                                            <th></th>
                                            <th style="text-align: center;">S.No</th>
                                            <th style="text-align: center;">क्षमता का प्रकार</th>
                                            <th style="text-align: center;">गोदामों की संख्या</th>
                                            <th style="text-align: center;">क्षमता </th>
                                            <th style="text-align: center;">गोदामों की संख्या</th>
                                            <th style="text-align: center;">क्षमता </th>
                                            <th style="text-align: center;">गोदामों की संख्या</th>
                                            <th style="text-align: center;">क्षमता </th>
                                            <th style="text-align: center;"></th>

                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <td><%# Container.DataItemIndex + 1 %></td>
                                        <td><%# Eval("Capacity_Type") %></td>
                                        <asp:HiddenField ID="CT_ID" runat="server" Value='<%#Eval("CT_ID") %>' />
                                        <%--<td>
                                                <asp:Label runat="server" ID="TextBox1" Width="100%" Text='<%# Eval("Capacity_Type")%>'></asp:Label></td>
                                            <td>--%>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="txtReg_No_Of_Godown" Width="100%" placeholder="गोदामों की संख्या" Text='<%# Eval("Reg_No_Of_Godown")%>' onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="txtReg_Storage_Capacity" Width="100%" placeholder="क्षमता" Text='<%# Eval("Reg_Storage_Capacity")%>' onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="txtOffered_No_Of_Godown" Width="100%" placeholder="गोदामों की संख्या" Text='<%# Eval("Offered_No_Of_Godown")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="txtOffered_Storage_Capacity" Width="100%" placeholder="क्षमता" Text='<%# Eval("Offered_Storage_Capacity")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="txtContracted_No_Of_Godown" Width="100%" placeholder="गोदामों की संख्या" Text='<%# Eval("Contracted_No_Of_Godown")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="txtContracted_Storage_Capacity" Width="100%" placeholder="क्षमता" Text='<%# Eval("Contracted_Storage_Capacity")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="txtRemark" Width="100%" placeholder="रिमार्क" Text='<%# Eval("Remark")%>' TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </ItemTemplate>

                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <h5 class="bd" style="color: Red;">2. शाखा की क्षमता एवं उपयोगिता की समीक्षा (क्षमता M.T. में) :-</h5>
                            <asp:GridView ID="gvCol2" runat="server" AutoGenerateColumns="false" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="क्षमता का प्रकार">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="CT_ID" runat="server" Value='<%#Eval("CT_ID") %>' />
                                            <asp:Label runat="server" ID="txtCapacity_Type" Width="100%" Text='<%# Eval("Capacity_Type")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="गोदामों की संख्या">
                                        <ItemTemplate>
                                            <asp:TextBox class="form-control" runat="server" ID="txtnoofgodown" Width="100%" placeholder="गोदामों की संख्या" onkeypress="return isNumberKey(event)" Text='<%# Eval("No_Of_Godown")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="भंडारण क्षमता">
                                        <ItemTemplate>
                                            <asp:TextBox class="form-control" runat="server" ID="txtStorage_Capacity" Width="100%" placeholder="भंडारण क्षमता" Text='<%# Eval("Storage_Capacity")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="भंडारित बोरा">
                                        <ItemTemplate>
                                            <asp:TextBox class="form-control" runat="server" ID="txtStored_Bag" Width="100%" placeholder="भंडारित बोरा" Text='<%# Eval("Stored_Bag")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="भंडारित वजन">
                                        <ItemTemplate>
                                            <asp:TextBox class="form-control" runat="server" ID="txtStored_Weight" Width="100%" placeholder="भंडारित वजन" Text='<%# Eval("Stored_Weight")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="उपयोगिता का %">
                                        <ItemTemplate>
                                            <asp:TextBox class="form-control" runat="server" ID="txtPercentage_Of_Utility" Width="100%" placeholder="उपयोगिता का %" Text='<%# Eval("Percentage_Of_Utility")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="रिमार्क">
                                        <ItemTemplate>
                                            <asp:TextBox class="form-control" runat="server" ID="txtRemark" Width="100%" placeholder="रिमार्क" Text='<%# Eval("Remark")%>' TextMode="MultiLine"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>

                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <h5 class="bd" style="color: Red;">3. शाखा पर उपलब्ध कीटनाशक औषधियों का विवरण :-</h5>
                        <asp:GridView ID="gvCol4" runat="server" AutoGenerateColumns="false" Visible="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="कीटनाशक औषधि का नाम">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="PesticideID" runat="server" Value='<%#Eval("PT_ID") %>' />
                                        <asp:Label runat="server" ID="txtCol2_11" Width="100%" Text='<%# Eval("Pesticides_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="रिकार्ड अनुसार उपलब्ध मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtnoofgodown" Width="100%" placeholder="रिकार्ड अनुसार उपलब्ध मात्रा" Text='<%# Eval("Quantity")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="भौतिक सत्यापन में पाई गई मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtStorage_Capacity" Width="100%" placeholder="भौतिक सत्यापन में पाई गई मात्रा" Text='<%# Eval("Physical_verification_quantity")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="कीटनाषाक की एक्सपायरी दिनांक">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtInsecticideExpiryDate" Width="100%" placeholder="कीटनाषाक की एक्सपायरी दिनांक" Text='<%# Eval("Expiry_Date")%>'></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="आगामी 6 माह के लिए अतिरिक्त आवश्यकता">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtStored_Weight" Width="100%" placeholder="आगामी 6 माह के लिए अतिरिक्त आवश्यकता" Text='<%# Eval("Additional_requirement_for_the_year")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="रिमार्क">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtRemark" Width="100%" placeholder="रिमार्क" Text='<%# Eval("Remark")%>' TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>

                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <h5 class="bd" style="color: Red;">4. केप के निरीक्षण का विवरण :-</h5>
                        <asp:GridView ID="CapeGrd" runat="server" AutoGenerateColumns="false" Visible="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="विवरण">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="CIT_ID" runat="server" Value='<%#Eval("CIT_ID") %>' />
                                        <asp:Label runat="server" ID="txtCol2_11" Width="100%" Text='<%# Eval("Cap_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="कुल संख्या">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtNumber_of_Uses" Width="100%" placeholder="उपयोग की संख्या" Text='<%# Eval("Number_of_Uses")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="शेष उपयोगी संख्या/मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtRemaining_Useful_Number_Quantity" Width="100%" placeholder="शेष उपयोगी संख्या/मात्रा" Text='<%# Eval("Remaining_Useful_Number_Quantity")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="शेष अनुपयोगी संख्या/मात्रा">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtRemaining_Unusable_Number_Quantity" Width="100%" placeholder="शेष अनुपयोगी संख्या/मात्रा" Text='<%# Eval("Remaining_Unusable_Number_Quantity")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="अतिरिक्त आवश्यकता">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtAdditional_Requirement" Width="100%" placeholder="अतिरिक्त आवश्यकता" Text='<%# Eval("Additional_Requirement")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="रिमार्क">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtRemark" Width="100%" placeholder="रिमार्क" Text='<%# Eval("Remark")%>' TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>


                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <h5 class="bd" style="color: Red;">5. कीटोपचार की स्थिति का विवरण :-</h5>
                        <asp:GridView ID="Grdstatusofinsecticide" runat="server" AutoGenerateColumns="false" Visible="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="क्षमता का प्रकार">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="CIT_ID" runat="server" Value='<%#Eval("CT_ID") %>' />
                                        <asp:Label runat="server" ID="txtCol2_11" Width="100%" Text='<%# Eval("Capacity_Type")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="कुल स्टेकों की संख्या">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtNumber_of_total_stakes" Width="100%" placeholder="कुल स्टेकों की संख्या" Text='<%# Eval("Number_of_total_stakes")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="धूमीकरण स्टेको की संख्या">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtNumber_of_fumigation_Stackes" Width="100%" placeholder="धूमीकरण स्टेको की संख्या" Text='<%# Eval("Number_of_fumigation_Stackes")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="धूमीकरण हेतु शेष स्टेको संख्या">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtRemaining_Stake_Number_for_Fumigation" Width="100%" placeholder="धूमीकरण हेतु शेष स्टेको संख्या" Text='<%# Eval("Remaining_Stake_Number_for_Fumigation")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="धूमीकरण की दिनांक">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtFumigation_date" Width="100%" placeholder="धूमीकरण की दिनांक" Text='<%# Eval("Fumigation_date")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="छिड़काव किये गए स्टेको की संख्या">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtNumber_of_Stackes_sprayed" Width="100%" placeholder="छिड़काव किये गए स्टेको की संख्या" Text='<%# Eval("Number_of_Stackes_sprayed")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="छिड़काव के लिए शेष स्टेको की संख्या">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtRemaining_Stake_Number_for_sprayed" Width="100%" placeholder="छिड़काव के लिए शेष स्टेको की संख्या" Text='<%# Eval("Remaining_Stake_Number_for_sprayed")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="छिड़काव की दिनांक">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtsprayed_date" Width="100%" placeholder="छिड़काव की दिनांक" Text='<%# Eval("sprayed_date")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="रिमार्क">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtRemark" Width="100%" placeholder="रिमार्क" Text='<%# Eval("Remark")%>' TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <h5 class="bd" style="color: Red;">6. गोदाम में कीटग्रस्तता की स्थिति का विवरण :-</h5>
                        <asp:GridView ID="Grdinsecticidestatus" runat="server" AutoGenerateColumns="false" Visible="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnid" runat="server" Value='<%#Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="क्षमता का प्रकार">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="CIT_ID" runat="server" Value='<%#Eval("CT_ID") %>' />
                                        <asp:Label runat="server" ID="txtCol2_11" Width="100%" Text='<%# Eval("Capacity_Type")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="गोदाम में कुल स्टेकों की संख्या">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtTotal_number_of_storages_in_the_warehouse" Width="100%" placeholder="गोदाम में कुल स्टेकों की संख्या" Text='<%# Eval("Total_number_of_storages_in_the_warehouse")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="स्टेक C">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtSteak_C" Width="100%" placeholder="स्टेक C" Text='<%# Eval("Steak_C")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="स्टेक F">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtSteak_F" Width="100%" placeholder="स्टेक F" Text='<%# Eval("Steak_C")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="स्टेक H">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtSteak_H" Width="100%" placeholder="स्टेक H" Text='<%# Eval("Steak_C")%>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="कीटग्रस्तता की स्थिति में कार्यवाही">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtAction_in_case_of_insecticide" Width="100%" placeholder="कीटग्रस्तता की स्थिति में कार्यवाही" Text='<%# Eval("Action_in_case_of_insecticide")%>' TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <h5 class="bd" style="color: Red;">7. भंडारण शुल्क देयक प्रस्तुतीकरण की स्थिति का विवरण :-( रेंडम चैक करें )</h5>

                        <asp:GridView ID="Grdstoragedutysubmision" runat="server" AutoGenerateColumns="false" Visible="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="जमाकर्ता का नाम">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="CIT_ID" runat="server" Value='<%#Eval("DN_ID") %>' />
                                        <asp:Label runat="server" ID="txtCol2_11" Width="100%" Text='<%# Eval("Depositor_Name_En")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="देयक क्रमांक">
                                    <ItemTemplate>
                                        <asp:TextBox CssClass="form-control" ID="txtBill_Number" runat="server" placeholder="देयक क्रमांक" Text='<%# Eval("Bill_Number")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="देयक दिनांक">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtBill_Date" placeholder="देयक दिनांक (dd/mm/yyyy)" Text='<%# Eval("Bill_Date")%>'></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="देयक की अवधि दिनांक">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtBill_Pay_date" placeholder="देयक की अवधि(dd/mm/yyyy)" Text='<%# Eval("Bill_Pay_date")%>'></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="देयक की राशि">
                                    <ItemTemplate>
                                        <asp:TextBox CssClass="form-control" ID="txtBill_Payment_Amount" runat="server" placeholder="देयक राशि" Text='<%# Eval("Bill_Payment_Amount")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="भुगतान प्राप्ति की दिनांक">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtBill_Payment_received_Date" placeholder="भुगतान प्राप्ति माह तक (dd/mm/yyyy)" Text='<%# Eval("Bill_Payment_received_Date")%>'></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="भुगतान प्राप्ति की राशि">
                                    <ItemTemplate>
                                        <asp:TextBox CssClass="form-control" ID="txtPayment_Amount" runat="server" placeholder="भुगतान राशि" Text='<%# Eval("Payment_Amount")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="लंबित राशि">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtPending_Amount" placeholder="लंबित राशि" Text='<%# Eval("Pending_Amount")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="भुगतान लंबित रहने का कारण">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtPayment_pending_reason" placeholder="भुगतान लंबित रहने का कारण" Text='<%# Eval("Payment_pending_reason")%>' TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="रिमार्क">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtRemark" placeholder="रिमार्क" Text='<%# Eval("Remark")%>' TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />

                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <h5 class="bd" style="color: Red;">9.कैश बैलेन्स की स्थिति का विवरण :-</h5>
                        <asp:GridView ID="GrdCaseBalance" runat="server" AutoGenerateColumns="false" Visible="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="विवरण">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="MCBSD_ID" runat="server" Value='<%#Eval("MCBSD_ID") %>' />
                                        <asp:Label runat="server" ID="txtName" Width="100%" Text='<%# Eval("Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="	रिकार्ड अनुसार राषि रूपये">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtAmount_according_to_record" Width="100%" placeholder="	रिकार्ड अनुसार राषि रूपये" Text='<%# Eval("Amount_according_to_record")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="भौतिक सत्यापन में पाई गई राषि रूपये">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtAmount_found_in_physical_verification" Width="100%" placeholder="भौतिक सत्यापन में पाई गई राषि रूपये" Text='<%# Eval("Amount_found_in_physical_verification")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="अन्तर">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtDifference" Width="100%" placeholder="अन्तर" Text='<%# Eval("Difference")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="रिमार्क">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtRemark" Width="100%" placeholder="रिमार्क" Text='<%# Eval("Remark")%>' TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />

                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <h5 class="bd" style="color: Red;">10. शाखा पर पदस्थ समस्त कर्मचारियों की सूची का विवरणः-</h5>
                        <asp:GridView ID="GrdEmployeeDetails" runat="server" AutoGenerateColumns="false" Visible="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="पदनाम">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="Dsgn_ID" runat="server" Value='<%#Eval("Dsgn_ID") %>' />
                                        <asp:Label runat="server" ID="txtDepositor_Name" Width="100%" Text='<%# Eval("Designation_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="संख्या">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtNumber_of_Employee" Width="100%" placeholder="संख्या" Text='<%# Eval("Number_of_Employee")%>'></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="पदस्थी दिनाक">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtDate_of_joining" Width="100%" placeholder="पदस्थी दिनाक" Text='<%# Eval("Date_of_joining")%>'></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="पदस्थी अवधि(वर्ष)">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtJoining_Year" Width="100%" placeholder="पदस्थी अवधि(वर्ष)" Text='<%# Eval("Joining_Year")%>'></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="रिमार्क">
                                    <ItemTemplate>
                                        <asp:TextBox class="form-control" runat="server" ID="txtRemark" Width="100%" placeholder="रिमार्क" Text='<%# Eval("Remark")%>' TextMode="MultiLine"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />

                </div>
                <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">

                    <tr>

                        <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                            <span style="color: #cb4e48; font-weight: bold; font-size: 15px">11. शाखा पर गोदामों का विवरण </span>
                            <br />
                            <span style="color: #cb4e48; font-weight: bold; font-size: 15px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; A शाखा में निम्नलिखित पँजियो का अधतन व्यवस्थित संधारण  किया गया हैं </span>

                        </td>
                    </tr>

                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label81" runat="server" Text="आवक पंजी"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlavakyesno" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label82" runat="server" Text="जावक पंजी"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtjavakpanji" runat="server" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"></asp:TextBox>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label83" runat="server" Text="स्थानीय डाक-बुक"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlsthanidakbook" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label84" runat="server" Text="स्टेशनरी पंजी"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlstashnar" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label85" runat="server" Text="डेड स्टॉक पंजी"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddldedstock" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>

                                    <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="6">
                                        <span style="color: #cb4e48; font-weight: bold; font-size: 15px">B. शाखा पर गोदाम का विवरण </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;" colspan="3">
                                        <asp:Label ID="Label87" runat="server" Text="उन गोदामो का विवरण जिनकी स्वीकृति अभी मुख्यालय / क्षेत्रीय कार्यालय से आना बांकी हैं "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ddlGHO" runat="server" Width="222px" onkeypress="return isNumberKey(event)"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"></asp:TextBox>
                                        <%-- <asp:DropDownList ID="ddlGHO" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>--%>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label88" runat="server" Text="शाखा पर गोदाम कितनी पारधी में स्थित हैं(in KM) ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtgodamdistance" runat="server" Width="222px" onkeypress="return isNumberKey(event)"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>

                                    <td align="left" style="border: #E6C79D;" colspan="6">
                                        <span style="color: #cb4e48; font-weight: bold; font-size: 15px">1. क्या सभी गोदाम निम्न बिन्दुओं से उपयुक्त है (यदि उत्तर नकारात्मक हो तो विवरण के साथ सुझाव प्रस्तुत करें।) </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label89" runat="server" Text="(अ) वैज्ञानिक भण्डारण की दृष्टि से"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlA" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label90" runat="server" Text="(ब) व्यवसायिक दृष्टि से"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlB" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label92" runat="server" Text="(स) निगरानी एवं सुरक्षा की दृष्टि से"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlC" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr id="showtxtA" runat="server" style="display: none;">
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label91" runat="server" Text="(अ) विवरण के साथ सुझाव प्रस्तुत करें"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtA" TextMode="MultiLine"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr id="showtxtB" runat="server" style="display: none;">
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label93" runat="server" Text="(ब) विवरण के साथ सुझाव प्रस्तुत करें"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtB" TextMode="MultiLine"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr id="showtxtC" runat="server" style="display: none;">
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label94" runat="server" Text="(स) विवरण के साथ सुझाव प्रस्तुत करें"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtC" TextMode="MultiLine"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;" colspan="2">
                                        <asp:Label ID="Label95" runat="server" Text="2. क्या गोदाम पंजी का संधरण व्यवस्थित पद्धति से अद्यतन किया जाता है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl2" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;" colspan="2">
                                        <asp:Label ID="Label96" runat="server" Text="3. क्या गोदाम लाग बुक का संधारण व्यवस्थित पद्धति से अद्यतन किया जाता है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl3" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;" colspan="3">
                                        <asp:Label ID="Label97" runat="server" Text="4. उन गोदामों की सूची जिनमें निम्न स्तरीय निर्माण/व्यवसाय में कमी और कोई कारण से खाली करने हेतु अनुससित किया गया हो, विस्तृत विवरण के साथ कारणों का उल्लेख करें।"></asp:Label>
                                    </td>
                                    <td colspan="6">
                                        <asp:TextBox ID="txt4" runat="server" Width="500px"
                                            Height="38px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="border: #E6C79D;" colspan="6">
                                        <span style="color: #cb4e48; font-weight: bold; font-size: 15px">क्लेम </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;" colspan="2">
                                        <asp:Label ID="Label98" runat="server" Text="बीमा कम्पनी को यदि क्लेम किया गया हो तो अद्यतन जानकारी।"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtcleimremark" runat="server" Width="500px"
                                            Height="38px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="border: #E6C79D;" colspan="6">
                                        <span style="color: #cb4e48; font-weight: bold; font-size: 15px">वैज्ञानिक भण्डारण </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label99" runat="server" Text="क्या प्रत्येक स्टेक के बीच आवश्यक गलियारा छोड़ा गया है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl8" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label100" runat="server" Text="क्या गोदाम में स्टेक का योजनाबद्ध नियोजन किया गया है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl9" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label101" runat="server" Text="क्या स्टेक को व्यवस्थित एवं वैज्ञानिक ढंग से निर्मित किया गया है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl10" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label102" runat="server" Text="क्या प्रत्येक स्टेक पर कार्ड लगाये हुए है और उनके दोनो ओर अद्यतन प्रविष्टियां अंकित की जा रही है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl11" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label103" runat="server" Text="क्या स्टेक पंजी का संधारण व्यवस्थित ढंग से किया जाता है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl12" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label104" runat="server" Text="क्या स्कंध को कीट रहित रखने हेतु नियतकालिक कीटोपचार धुम्रीकरण समय पर किया जाता है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl13" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>

                                <tr>
                                    <td align="left" style="border: #E6C79D;" colspan="6">
                                        <span style="color: #cb4e48; font-weight: bold; font-size: 15px">प्रतीकात्मक नमूना-वर्गीकरण और विष्लेषण</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;" colspan="2">
                                        <asp:Label ID="Label105" runat="server" Text="क्या जमा करते समय स्कंध का नमूना किया गया है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl14" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-size: 13px; color: Black;" colspan="2">
                                        <asp:Label ID="Label106" runat="server" Text="क्या नमूना पंजी का अद्यतन संधारण किया गया है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl15" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" style="border: #E6C79D;" colspan="6">
                                        <span style="color: #cb4e48; font-weight: bold; font-size: 15px">तौल</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;" colspan="3">
                                        <asp:Label ID="Label107" runat="server" Text="क्या शाखा पर पर्याप्त संख्या में कांटा-वांट उपलब्ध है ? और सभी नाप-तौल विभाग से प्रमाणित है। प्रमाण-पत्र का क्रंमांक व दिनांक (विसंगति होने पर उसका तत्काल किया जावे और वस्तु स्थिति से अवगत कराया जावें।)"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="ddl16" runat="server" Width="500px"
                                            Height="38px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"></asp:TextBox>
                                        <%-- <asp:DropDownList ID="ddl16" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>--%>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" style="border: #E6C79D;" colspan="6">
                                        <span style="color: #cb4e48; font-weight: bold; font-size: 15px">जमा एवं भुगतान वेयरहाउस रसीद </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label108" runat="server" Text="क्या जमाकर्ता के नमूने हस्ताक्षर और अधिकार पत्र निर्धारित प्रक्रियानुसार ठीक से रखे गये है।"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl17" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label109" runat="server" Text="क्या वेयरहाउस रसीद की मूल व कार्यालीन प्रति में आंषिक/पूर्ण भुगतान का इन्द्राज किया गया है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl18" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label110" runat="server" Text="क्या पूर्ण भुगतान के समय जमाकर्ता से मूल वेयरहाउस रसीद लेकर सम्बन्धित भुगतान पत्रक के साथ निरस्त कर लगाई गई है ? "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl19" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label111" runat="server" Text="क्या भुगतान के समय स्कंध उसी गोदाम/थप्पी से दिया गया है, जो वेयर हाउस रसीद के अनुसार है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl20" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label112" runat="server" Text="क्या स्कंध जमा होने व भुगतान होने की प्रविष्टियां स्टाक रजिस्टर/डिपाॅजिट लेजर व अन्य पंजियों में ठीक और सही तरीके से की जाती है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl21" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label113" runat="server" Text="क्या स्टाक रजिस्टर/डिपाॅजिट लेजर वेयर हाउस रसीद जमा आवेदन पत्रों पर पारसपरिक संदर्भ निर्देषानुसार दिये जाते है ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl22" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;" colspan="3">
                                        <asp:Label ID="Label114" runat="server" Text="क्या शेष स्कंध की मात्रा जैसा कि वेयर हाउस रसीद पर दर्शाया गया है स्टाक रजिस्टर और डिपाजिट लेजर के कुल शेष स्कंध की मात्रा से मिलान करते है या नहीं (यदि नहीं तो मिलान कर सूचित करें)"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl23" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label115" runat="server" Text="वेयर हाउस रसीद होल्डर्स पंजी प्रविष्टियों में अद्यतन है या नहीं ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl24" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" style="border: #E6C79D;" colspan="6">
                                        <span style="color: #cb4e48; font-weight: bold; font-size: 15px">सूरक्षा व्यवस्था</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label116" runat="server" Text="महत्वपूर्ण दस्तावेज सुरक्षित रखे जाने है या नहीं ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl25" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label117" runat="server" Text="तालों की दूसरी चाबियां शाखा प्रबंधक द्वारा बैंक में जमा की गई है या नहीं ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl26" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;">
                                        <asp:Label ID="Label118" runat="server" Text="शाखा पर संग्रहित स्कंध व गोदामों की देखभाल व सुरक्षा हेतु पर्याप्त व्यवस्था संतोषजनक है या नहीं ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl27" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" style="border: #E6C79D;" colspan="6">
                                        <span style="color: #cb4e48; font-weight: bold; font-size: 15px">सामान्य</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;" colspan="2">
                                        <asp:Label ID="Label119" runat="server" Text="शाखा प्रबंधक के जमाकर्ताओं/संबंधित व्यक्तियों से सोहार्दपूर्ण सम्बंध है या नहीं ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl28" runat="server" AutoPostBack="false" Width="222px"
                                            Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td style="font-size: 13px; color: Black;" colspan="2">
                                        <asp:Label ID="Label120" runat="server" Text="शाखा को आवष्यकताओं का विभागावार पूर्ण न्यायोचित विवरण ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ddl29" runat="server" Width="222px"
                                            Height="38px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" TextMode="MultiLine"></asp:TextBox>
                                        <%--<asp:DropDownList ID="ddl29" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 13px; color: Black;" colspan="2">
                                        <asp:Label ID="Label121" runat="server" Text="शाखा का व्यवसाय/आर्थिक गतिविधियों को बढ़ाने हेतु निरीक्षण अधिकारी के सुझाव?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ddl30" runat="server" Width="222px"
                                            Height="38px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" TextMode="MultiLine"></asp:TextBox>
                                        <%-- <asp:DropDownList ID="ddl30" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>--%>
                                    </td>
                                    <td style="font-size: 13px; color: Black;" colspan="2">
                                        <asp:Label ID="Label122" runat="server" Text="शाखा के कार्यकलापों के बारे में निरीक्षण अधिकारी की टिप्पणी ?"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ddl31" runat="server" Width="222px"
                                            Height="38px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" TextMode="MultiLine"></asp:TextBox>

                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr>

                        <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                            <span style="color: #cb4e48; font-weight: bold; font-size: 15px">Self Decleration</span>
                        </td>
                    </tr>

                    <tr>
                        <td style="height: 5px;" colspan="6"></td>
                    </tr>
                </table>
                <hr />
            </asp:Panel>
        </div>
    </div>



</asp:Content>

