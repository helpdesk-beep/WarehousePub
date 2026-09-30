<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="BMVarificationofGodownCapacatyandcurrentstockposition.aspx.cs" Inherits="Accounting_BMVarificationofGodownCapacatyandcurrentstockposition" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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
            border-radius: 6px;
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
    <script type="text/javascript">  

        function ConfirmOnDelete() {
            if (confirm("Are you sure want to Delete This Inspection ?") == true)
                return true;
            else
                return false;
        }



        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode;
            if ((charCode >= 48 && charCode <= 57)) {
                return true;
            }
            if (charCode == 46) { return true; }
            if (charCode == 8) { return true; }
            if (charCode == 9) { return true; }
            else { return false; }
        }



     <%--            $(document).ready(function () {
                     $('#<%=OfferDistType.ClientID%>').change(function () {
                         var vdist = $('#<%=OfferDistType.ClientID%>').val();
                         if (vdist == "S")
                         {

                         }
                         else
                         {
                         }
            
            });   
        });--%>

    </script>
    <table>
        <tr>
            <td>
                <div style="text-align: center">
                    <h5 style="font-size: 15px; text-transform: uppercase; font-weight: revert; color: blue;">Update Godown Capacity, Storage Capcity, Vacant Capacity,Latitude,Longitude And Godown Status(YES/No)</h5>
                </div>
                <div style="text-align: center">
                    <h5 style="font-size: 15px; text-transform: uppercase; font-weight: revert; color: red;">Note:- ऐसे गोदाम जिनका किराया भुगतान कर दिया गया हैं और वर्तमान में उसमे कोई भी स्कंध नहीं रखा हैं ऐसे गोदामों का स्टेटस "No" करे </h5>
                </div>
                <div style="text-align: center">
                    <h2 style="font-size: 30px; text-transform: uppercase; font-weight: revert; color: red;">
                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    </h2>
                </div>
            </td>

            <%--      <td>
             
                 <div style="text-align:center"> <asp:Button ID="show" runat="server" style="font-size:20px; color:green; font-weight:800" Text="List Of Selected/Rejected Godown For Mapping" OnClick="show_Click"  />  
                </div>
                     </td>--%>
        </tr>
    </table>


    <asp:HiddenField ID="ddd" runat="server" />



    <asp:Panel ID="StoreGrid" runat="server">

        <%--     <table style="text-align:center;margin:0 auto; border:1px solid#000;" >
                <tr>
                    <td>  <asp:DropDownList ID="DdlDist" runat="server" AutoPostBack="true" OnTextChanged="DdlDist_TextChanged"></asp:DropDownList></td>
                  <td>  <asp:DropDownList ID="ddlbranch" runat="server" OnTextChanged="ddlbranch_TextChanged" AutoPostBack="true"></asp:DropDownList></td>
                </tr>
            </table>--%>
        <asp:HiddenField ID="hdngdnid" runat="server" />
        <asp:HiddenField ID="Hiddendistid" runat="server" />
        <asp:HiddenField ID="Hiddenbranch" runat="server" />

        <asp:HiddenField ID="EnterHiddendistid" runat="server" />



        <asp:GridView ID="GridView1" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" Font-Size="18px" Width="100%">

            <Columns>
                <%--<asp:CommandField HeaderText="Delete" ShowDeleteButton="True"
                                                                ItemStyle-ForeColor="red">
                                                                <ItemStyle ForeColor="Red"></ItemStyle>
                                                            </asp:CommandField>
                                                                    <asp:CommandField ShowSelectButton="True" HeaderText="Edit"
                                                                        ItemStyle-ForeColor="blue">
                                                                        <ItemStyle ForeColor="Blue"></ItemStyle>
                                                                    </asp:CommandField>--%>
                <asp:TemplateField HeaderText="S.N.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                        <%--    <asp:Label ID="id" runat="server" Text='<%#(Eval(""),"{0:N0}")) %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" />
                <asp:BoundField DataField="Godown_Name" HeaderText="Godown_Name" />
                <asp:BoundField DataField="Hired_Type" HeaderText="Hired_Type" />

                <asp:TemplateField HeaderText="Godown Max Capacity">
                    <ItemTemplate>

                        <asp:Label ID="Godown_Max_Capacity" runat="server" Text='<%# (Eval("Godown_Capacity","{0:N0}"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Godown Scientific Capacity">
                    <ItemTemplate>

                        <asp:Label ID="Godown_Scientific_Capacity" runat="server" Text='<%# (Eval("Godown_Scintific_Capacity","{0:N0}"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="Godown Avl. Stock">
                                                               <ItemTemplate>
                                                                      
                                                                   <asp:Label ID="AvlStock" runat="server" Text='<%# (Eval("AvlStock","{0:N0}"))%>'></asp:Label>
                                                               </ItemTemplate>
                                                            </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Godown Vacant Capacity">
                    <ItemTemplate>

                        <asp:Label ID="Godown_Vacant_Capacity" runat="server" Text='<%# (Eval("Vacant_Capacity","{0:N0}"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Latitude">
                    <ItemTemplate>
                        <asp:Label ID="Latitude" runat="server" Text='<%# (Eval("Latitude"))%>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Longitude">
                    <ItemTemplate>

                        <asp:Label ID="Longitude" runat="server" Text='<%# (Eval("Longitude"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="IsActive" HeaderText="Status" />

                <asp:TemplateField ItemStyle-Width="30px" HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" Text="Update" OnClick="Edit"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>




        </asp:GridView>
    </asp:Panel>





    <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; width: 50%; height: 90%; overflow: auto;">
        <table align="center">
            <tr align="center">
                <td colspan="2">
                    <asp:Label Font-Bold="true" ID="Label3" Style="text-align: center; font-size: 25px; text-transform: uppercase; color: red;" runat="server" Text="Godown Details"></asp:Label>
                </td>
            </tr>
            <tr>
                <td  style="font-size: 20px;"></td>
            </tr>
            <tr>
                <td style="font-size: 20px;">Godown_Id -:
                    <asp:Label Font-Bold="true" ID="Label8" runat="server" Text="Godown Details"></asp:Label>
                </td>
                </tr>
            <tr>
                <td  style="font-size: 20px;"></td>
            </tr>
                <tr>
                <td style="font-size: 20px;">Godown_Name -:
                    <asp:Label Font-Bold="true" ID="Label9" runat="server" Text="Godown Details"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table align="center">

            <tr>
                <td style="padding: 25px">
                    <%--    Vacant_Capacity_BY_BM--%>
                    <asp:Label ID="Label4" runat="server" Text="Godown Capacity" Font-Bold="true" ForeColor="navy"
                        Font-Size="11pt"></asp:Label>(In MT)</td>
                <td style="padding: 25px">
                    <asp:TextBox ID="txtVacantCapacity" runat="server" Width="150px" AutoComplete="off" onkeypress="return NumberOnly(event)"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtVacantCapacity_FilteredTextBoxExtender"
                        runat="server" TargetControlID="txtVacantCapacity" FilterType="Custom, Numbers" ValidChars=".">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td style="padding: 25px">
                    <%-- Unload_Capacity --%>
                    <asp:Label ID="Label1" runat="server" Text="Godown Scientific Capacity" Font-Bold="true" ForeColor="navy"
                        Font-Size="11pt"></asp:Label>(In  MT)</td>
                <td style="padding: 25px">
                    <asp:TextBox ID="txtUnloadCapacity" runat="server" Width="150px" AutoComplete="off" onkeypress="return NumberOnly(event)"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtUnloadCapacity_FilteredTextBoxExtender"
                        runat="server" TargetControlID="txtUnloadCapacity" FilterType="Custom, Numbers" ValidChars=".">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>



            <tr>
                <td style="padding: 25px">
                    <%-- Unload_Capacity --%>
                    <asp:Label ID="Label10" runat="server" Text="Latitude" Font-Bold="true" ForeColor="navy"
                        Font-Size="11pt"></asp:Label></td>
                <td style="padding: 25px">
                    <asp:TextBox ID="TxtLatitude" runat="server" Width="150px" AutoComplete="off" onkeypress="return NumberOnly(event)"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                        runat="server" TargetControlID="TxtLatitude" FilterType="Custom, Numbers" ValidChars=".">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>


            <tr>
                <td style="padding: 25px">
                    <%-- Unload_Capacity --%>
                    <asp:Label ID="Label11" runat="server" Text="Longitude" Font-Bold="true" ForeColor="navy"
                        Font-Size="11pt"></asp:Label></td>
                <td style="padding: 25px">
                    <asp:TextBox ID="TextLongitude" runat="server" Width="150px" AutoComplete="off" onkeypress="return NumberOnly(event)"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                        runat="server" TargetControlID="TextLongitude" FilterType="Custom, Numbers" ValidChars=".">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>


            <tr>
                <td style="padding: 25px">
                    <asp:Label ID="Label2" runat="server" Text="Godown Status(YES/NO)" Font-Bold="true" ForeColor="navy"
                        Font-Size="11pt"></asp:Label></td>
                <td style="padding: 25px">

                    <asp:DropDownList ID="Godownflag" runat="server" Width="155px" Height="25px" CssClass="tb6">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="NO" Value="N"></asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>
            <%-- <tr>
                                    <td style="padding: 25px">
                                        <asp:Label ID="Label7" runat="server" Text="Avl. Stock (All Commodity) " Font-Bold="true" ForeColor="navy"
                                            Font-Size="11pt"></asp:Label>(In MT)</td>
                                    <td style="padding: 25px">
                                        <asp:TextBox ID="ClosingBalance" runat="server" Width="150px" AutoComplete="off" onkeypress="return NumberOnly(event)"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="ClosingBalance_FilteredTextBoxExtender1"
                                            runat="server" TargetControlID="ClosingBalance" FilterType="Custom, Numbers" ValidChars=".">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>--%>


            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BTNBLUE" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="BTNBLUE" OnClientClick="return Hidepopup()" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
        PopupControlID="pnlAddEdit" TargetControlID="lnkFake"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
</asp:Content>

