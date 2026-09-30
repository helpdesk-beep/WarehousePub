<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="rpt_Insecticide_DateWise.aspx.cs" Inherits="Inspections_State_rpt_Insecticide_DateWise" Title="Account Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    >
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 340px;
        }

        .auto-style2 {
            width: 253px;
        }

        .auto-style3 {
            margin-left: 13px;
            margin-top: 17px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 341px;
        }

        .auto-style2 {
            width: 223px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 232px;
        }

        .auto-style2 {
            width: 233px;
        }

        .auto-style3 {
            width: 235px;
        }

        .auto-style4 {
            width: 385px;
        }

        .auto-style5 {
            height: 23px;
            margin-top: 0px;
        }

        .auto-style6 {
            height: 1px;
        }

        .auto-style7 {
            height: 25px;
        }

        .auto-style8 {
            width: 100%;
        }
    </style>
    <script type="text/javascript">  
 
    function printPartOfPage(elementId) {  
    var printContent = document.getElementById(elementId);  
    var windowUrl = 'about:blank';  
    var uniqueName = new Date();  
    var windowName = 'Print' + uniqueName.getTime();  
    var printWindow = window.open(windowUrl, windowName, 'left=50000,top=50000,width=0,height=0');  
    printWindow.document.write(printContent.innerHTML);  
    printWindow.document.close();  
    printWindow.focus();  
    printWindow.print();    
    printWindow.close();  
}  
    </script>  
    
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src="../../JS/table2excel.js"></script>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.8.0/css/bootstrap-datepicker.min.css" />
        <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
        <script type="text/javascript">
            $("body").on("click", "#btnExport", function () {
                $("[id*=GridView1]").table2excel({
                    filename: "State_Level_District_Branch_Wise_Rent_Summary.xls"
                });
            });
        </script>
     <script type="text/javascript">
         $(function () {
             $('#<%=txtFDate.ClientID %>').datepicker({
                 dateFormat: 'yy/mm/dd',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: '1950:2100'
                });
            });
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div>
            <h4 class="text-info">List Consumption/Transfer During Entry</h4>
             <div style="text-align:right; color:red;  width:auto; height:50px; border-bottom-color:brown">
             <input type="button"  class="btn btn-primary"  value="PRINT" onclick="JavaScript:printPartOfPage('abc');"/>
                </div>
            <div class="row">
                <div class="col-12">

                    <table>
                        <tr>
                            <td style="width: 175px;">
                        <asp:Label ID="Label11" runat="server" Text="From Date (DD/MM/YY): "></asp:Label>
                    </td>
                    <td style="width: 175px;">
                        <asp:TextBox ID="txtFDate" runat="server" ClientIDMode="Static" CssClass="form-control" OnTextChanged="txtFDate_TextChanged" AutoPostBack="true" ></asp:TextBox>
                    </td>
                            <%--<td style="text-align: right;">
                                <asp:Label ID="Label9" runat="server" Text="Region:"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlregion" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>--%>
                            <td style="text-align: right; width: 300px;">
                                <asp:Label ID="Label1" runat="server" Text="District : "></asp:Label>
                            </td>
                            <td style="text-align: left; width: 300px;">
                                <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                             <td style="text-align: right; width: 300px;">
                                <asp:Label ID="Label2" runat="server" Text="Branch:"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 300px;">
                                <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="Label3" runat="server" Text="Insecticide : "></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddl_Ins" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddl_Ins_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Alluminium Phosphide</asp:ListItem>
                                    <asp:ListItem Value="2">Malathion</asp:ListItem>
                                    <asp:ListItem Value="3">Deltamethrin</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div id="abc">
                    <asp:GridView ID="GVOfStock" OnRowEditing="GVOfStock_RowEditing" OnRowUpdating="GVOfStock_RowUpdating"
                        AutoGenerateColumns="false" runat="server" OnRowCancelingEdit="GVOfStock_RowCancelingEdit1" 
                        OnRowCreated="GVOfStock_RowCreated" ShowFooter="true"
                         FooterStyle-Font-Bold="true" FooterStyle-CssClass="alert-danger">
                        <HeaderStyle
                            BackColor="#D69758"
                            Font-Italic="false"
                            ForeColor="Snow"/>
                        <Columns>


                            <%--<asp:TemplateField HeaderText="District Name">
                                <ItemTemplate>
                                    <asp:Label ID="godown_no" runat="server" Text='<%#Eval("District_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name">
                                <ItemTemplate>
                                    <asp:Label ID="godown_no" runat="server" Text='<%#Eval("District_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                             <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <asp:Label ID="lblregion" runat="server" Text='<%#Eval("region") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistricID" runat="server" Text='<%#Eval("District_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Branch Name">
                                <ItemTemplate>
                                    <asp:Label ID="godown_no" runat="server" Text='<%#Eval("DepotName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Insecticide Name">
                                <ItemTemplate>
                                    <asp:Label ID="txtInsecticideName" runat="server" Text='<%#Eval("InsecticideName") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpeningBalancequantity" runat="server" Text='<%#Eval("Opening_Balance_quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpeningBalancevalue" runat="server" Text='<%#Eval("Opening_Balance_value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblReceipt_Balance_quantity" runat="server" Text='<%#Eval("Receipt_Balance_quantity") %>'></asp:Label>
                                </ItemTemplate>
                               
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblReceiptBalancevalue" runat="server" Text='<%#Eval("Receipt_Balance_value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblCBQ1" runat="server" Text='<%#Eval("CBQ1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblCBV1" runat="server" Text='<%#Eval("CBV1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblTBQ" runat="server" Text='<%#Eval("TBQ") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblTBV" runat="server" Text='<%#Eval("TBV") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblValue" runat="server" Text='<%#Eval("Value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                        </div>

                </div>
            </div>
        </div>
    </div>
   
</asp:Content>

