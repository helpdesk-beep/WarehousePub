<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Update_Currect_Storage_Bill_With_RentBill.aspx.cs" Inherits="Accounting_Update_Currect_Storage_Bill_With_RentBill" Title="कटोत्रा" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <style type="text/css">
        .style1 {
            width: 346px;
        }

        .auto-style1 {
            width: 539px;
        }
    </style>
    <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlgodown]").select2();

        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddldistrict]").select2();

        });
    </script>
     <script type="text/javascript">
         $(function () {
             $("[id*=ddlbranch]").select2();

         });
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy; background-color: white;">
        <center>
            <div>
                <table width="1000px" style="width: 980px;border: 1px solid navy;height: 184px;">
                    <tr id="msg">
                        <td>
                            <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
                    </tr>
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td align="center">
                            <asp:Label ID="lblheading" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                Text="Update Currect Storage Charges Bill in Deductions"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="1" style="height: 5px"></td>
                    </tr>


                    <tr id="tr1" visible="true" runat="server">
                        <td>
                            <fieldset style="width: 980px;border: 1px solid navy;height: 184px;">
                                <center>
                                    <div id="div2" style="height: 150px;">
                                        <table id="Table1" cellpadding="0" border="0px" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="width: 200px;">
                                                    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label6" runat="server" Text="District"></asp:Label>
                                                </td>
                                                <td class="style1">
                                                    <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="True" TabIndex="1"
                                                        Height="25px" Width="208px" Font-Size="10pt" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                                <td style="width: 200px;">
                                                    <asp:Label ID="Label9" runat="server" Text="Branch" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlbranch" runat="server" Width="208px" Height="25px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td colspan="1" style="height: 10px"></td>
                                            </tr>

                                            <tr style="margin-top:10px">
                                                <td style="width: 200px;">
                                                    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label1" runat="server" Text="Godown"></asp:Label>
                                                </td>
                                                <td class="style1">
                                                    <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" TabIndex="1"
                                                        Height="25px" Width="208px" Font-Size="10pt" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                                <td style="width: 200px;">
                                                    <asp:Label ID="lblcommodity" runat="server" Text="Commodity Name" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlcomodity" runat="server" Width="208px" Height="25px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlcomodity_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" style="height: 10px"></td>
                                            </tr>


                                            <tr style="margin-top:20px">
                                                <td>
                                                    <asp:Label ID="lblCropYear" runat="server" Text="Crop Year" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCropYear" runat="server" AutoPostBack="True"
                                                        TabIndex="1" Height="25px" Width="200px" Font-Size="10pt">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblmonth" runat="server" Text="Month" Font-Bold="True" Font-Size="8pt"
                                                        ForeColor="Navy"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFyear" runat="server" AutoPostBack="false" Visible="true"
                                                        TabIndex="1" Height="25px" Width="90px" Font-Size="10pt" Enabled="true">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="True"
                                                        TabIndex="1" Height="25px" Width="115px" Font-Size="10pt" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="1" style="height: 5px"></td>
                                            </tr>
                                            <tbody id="billdetails" runat="server" visible="false">
                                                <tr>
                                                    <td>
                                                        <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label3" runat="server" Text="Rent Bill No."></asp:Label>
                                                    </td>
                                                    <td class="style1">
                                                        <%--<asp:DropDownList ID="ddlBill" runat="server" AutoPostBack="True" TabIndex="1" 
Height="25px" Width="208px" Font-Size="10pt" onselectedindexchanged="ddlBill_SelectedIndexChanged"
>
</asp:DropDownList>--%>
                                                        <asp:Label runat="server" ID="lblrentbillno" Width="203px" ReadOnly="True"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label5" runat="server" Text="Month"></asp:Label>
                                                    </td>
                                                    <td class="style1">

                                                        <asp:Label runat="server" ID="lblMonth2" Width="203px" ReadOnly="True"></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td colspan="1" style="height: 5px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label7" runat="server" Text="Rate Per Month"></asp:Label>
                                                    </td>
                                                    <td class="style1">
                                                        <asp:Label runat="server" ID="lblRPM" Width="203px" ReadOnly="True"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Rent Bill Amount"></asp:Label>
                                                    </td>
                                                    <td class="style1">

                                                        <asp:Label runat="server" ID="txtBilAmt" Width="203px" ReadOnly="True"></asp:Label>
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td style="height: 36px;">
                                                        <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label2" runat="server" Text="Storage Charges Bill No."></asp:Label>
                                                    </td>
                                                    <td class="style1">
                                                        <%--<asp:DropDownList ID="ddlActualBillNo" runat="server" AutoPostBack="True" TabIndex="1" 
Height="25px" Width="208px" Font-Size="10pt" 
        onselectedindexchanged="ddlActualBillNo_SelectedIndexChanged" >
</asp:DropDownList>--%>
                                                        <asp:Label runat="server" ID="lblActualBillNo" Width="203px" ReadOnly="True"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label4" runat="server" Text="Storage Charges Amount"></asp:Label>
                                                    </td>
                                                    <td class="style1">

                                                        <asp:Label runat="server" ID="txtActAmt" Width="203px" ReadOnly="True"></asp:Label>
                                                    </td>

                                                </tr>
                                            </tbody>
                                            <tr>
                                                <td colspan="1" style="height: 5px"></td>
                                            </tr>
                                            <%--<tr>
                       <td>
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label24" runat="server" Text="Stock in Godown"></asp:Label>
</td>
<td class="style1">
<asp:TextBox runat="server" ID="txtGodownBalance" Width="200px"></asp:TextBox>
</td>
                      <td>
                   
<asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lbl29" runat="server" Text="Closing Balance"></asp:Label>
</td>
<td class="style1">

<asp:TextBox runat="server" ID="txtBillClosingBalance" Width="200px"></asp:TextBox>

</td>

</tr>--%>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="1" style="height: 5px"></td>
                    </tr>

                    <tr id="trJVSGodownRent" visible="true" runat="server">
                        <td>
                            <fieldset style="width: 980px; border: 1px solid navy;">

                                <table width="100%">
                                    <tr>
                                        <td colspan="4" id="GVGodowns" runat="server" visible="true" style="text-align: center; width: 100%;" align="center">

                                            <%--<asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" 
                                        ShowFooter="true" Width="90%"
                                     EnableModelValidation="True" BackColor="White" BorderColor="#E7E7FF" 
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" 
                                        AutoGenerateDeleteButton="false" OnRowDataBound="gvGodown_OnRowDataBound">
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                        <Columns>
                                            <asp:BoundField DataField="Tid" HeaderText="S.No"  ItemStyle-Width="30px"/>

                                            <asp:TemplateField HeaderText="कतोत्र मद का विवरण"  ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                <asp:DropDownList ID="ddlD_Vivran" runat="server"
                                                    Width="200" Height="27px">
                                                </asp:DropDownList>                                     
                                             </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="कतोत्र का कारण"  ItemStyle-Width="200px">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txtK_Karand" runat="server" Width="200px" Text='<%# Eval("K_Karand") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="कतित्रा राशि"  ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txtK_Rashi" runat="server" Width="100px" Text='<%# Eval("K_Rashi") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                
                                            <asp:TemplateField HeaderText="रिमार्क"  ItemStyle-Width="200px">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txtK_Remark" runat="server" Width="200px" Text='<%# Eval("K_Remark") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              
                         <asp:TemplateField HeaderText=""  ItemStyle-Width="100px">                                              
                         <FooterStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                         <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" Width="100px" 
                                onclick="ButtonAdd_Click" />
                        </FooterTemplate>
                                            </asp:TemplateField>
                                                                                                      
                                        </Columns>
                                       
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                       
                                        
                                    </asp:GridView>--%>
                                    
                                    
                                    
                                    
             
                                        </td>
                                    </tr>




                                    <tr>
                                        <td align="center" colspan="4">
                                            <asp:Button ID="btnSumbmitRent" runat="server" Text="Submit" CssClass="BTNBLUE" Width="100px" Enabled="true" OnClick="btnSumbmitRent_Click" ValidationGroup="LoginFrame" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="brnCancel" runat="server" Text="Close" Width="100px" CssClass="BTNBLUE" />&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <asp:Button ID="Button1" runat="server" Text="New" Width="100px"
                            CssClass="BTNBLUE" OnClick="Button1_Click" />
                                        </td>
                                    </tr>
                                    <tr id="trRentBill" visible="false" runat="server">
                                        <td colspan="4">&nbsp;</td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57)) {
                alert("This field will not accept the alphabet, Please Enter Only number");
                return false;
            }
            return true;
        }
        //
    </script>
</asp:Content>

