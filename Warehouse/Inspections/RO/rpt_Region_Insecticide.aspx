<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="rpt_Region_Insecticide.aspx.cs" Inherits="Inspections_RO_rpt_Region_Insecticide"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


   
        <div>
            <h4 style="font-weight:600;text-align:center; " class="text-info">INSECTICIDE REPORT RM OFFICES</h4>
            
            <div style="text-align:right; color:red;  width:auto; height:50px; border-bottom-color:brown">
             <input type="button"  class="btn btn-primary"  value="PRINT" onclick="JavaScript:printPartOfPage('abc');"/>
                </div>
            <div class="row">
                <div class="col-12">
                     <table>
                        <tr>
                            <td style="text-align: right; width: 200px;">
                                <asp:Label ID="Label1" runat="server" Text="District :"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 200px;">
                                <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                             <td style="text-align: right; width: 200px;">
                                <asp:Label ID="Label2" runat="server" Text="Branch:"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 200px;">
                                <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width:200px;">
                                <asp:label id="label3" runat="server" text="Insecticide : "></asp:label>
                            </td>
                             <td style="text-align: right; width: 200px;">
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
                        OnRowCreated="GVOfStock_RowCreated" ShowFooter="true" FooterStyle-Font-Bold="true" FooterStyle-CssClass="alert-danger">
                        <HeaderStyle
                            BackColor="#D69758"
                            Font-Italic="false"
                            ForeColor="Snow"/>
                        
                        <Columns>
                             <asp:TemplateField HeaderText="District Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrict" runat="server" Text='<%#Eval("District_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("DepotName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Insecticide Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblInsName" runat="server" Text='<%#Eval("InsecticideName") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblOBQ" runat="server" Text='<%#Eval("Opening_Balance_quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblOBV" runat="server" Text='<%#Eval("Opening_Balance_value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblRBQ" runat="server" Text='<%#Eval("Receipt_Balance_quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblRBV" runat="server" Text='<%#Eval("Receipt_Balance_value") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblCBQ1" runat="server" Text='<%#Eval("CBQ1") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblCBMC1" runat="server" Text='<%#Eval("CBMC1") %>'></asp:Label>
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
   
</asp:Content>

