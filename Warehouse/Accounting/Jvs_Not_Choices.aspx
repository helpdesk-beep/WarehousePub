<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Jvs_Not_Choices.aspx.cs" Inherits="Accounting_Jvs_Not_Choices" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">

    <link type="text/css" rel="Stylesheet" href="css/style_new.css" />

    <script src="../JS/Jquery.3.6.0.js"></script>


    <%--    <script language="javascript" type="text/javascript" src="js/MD5.js"></script>

    <script language="javascript" type="text/javascript" src="js/chksql.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">  

        function ConfirmOnDelete() {
            if (confirm("Are you sure want to Delete This Inspection ?") == true)
                return true;
            else
                return false;
        }


    </script>




    <style>
        body {
            font-size: 15px;
        }
    </style>

    <div style="text-align: center">
        <h2 style="font-size: 25px; text-transform: uppercase; font-weight: revert; color: red;">ऐसे jVS  Registation जिन्होने Choice Filling नहीं की हैं , केवल उनका रिमार्क डालें  | </h2>
    </div>

    <asp:Panel ID="StoreGrid" runat="server">
        <asp:GridView ID="GridView1" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" OnRowUpdating="GridView1_RowUpdating" Width="100%">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        
                        <th style="text-align: center;">क्र.</th>
                        <th style="text-align: center;">Registration ID</th>
                        <th style="text-align: center;">Warehouse Name</th>
                        <th style="text-align: center;">Mobile No</th>
                        <th style="text-align: center;">Warehouse Capacity</th>
                        <th style="text-align: center;">Warehouse License No</th>
                        <th style="text-align: center;">Incharge Name</th>
                        <th style="text-align: center;">चयन करे</th>
                        <th style="text-align: center;">Save</th>
                        </tr>
                        
                    </HeaderTemplate>
                    <ItemTemplate>

                        <td style="text-align: center;"><%# Container.DataItemIndex + 1 %></td>
                        <td style="text-align: center;">
                   
                                 <asp:HiddenField ID="hdnregid" runat="server" Value='<%#Eval("Registration_Id") %>'/>
                                 <asp:HiddenField ID="HiddenDist" runat="server" Value='<%#Eval("DistrictId") %>'/>
                            
                            <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Registration_Id") %>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Warehouse_Name") %>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Mobile_No") %>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Warehouse_Capacity") %>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("Warehouse_LicenseNo") %>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("Incharge_Name") %>' />
                        </td>
                         <td style="text-align: center;">
                            <asp:DropDownList runat="server" ID="ddlchouse" >
                                <asp:ListItem Text="चयन करे" Value="0"></asp:ListItem>
                                <asp:ListItem Text="यह Registration उपयोग में नही है" Value="यह Regitration उपयोग में नही है"></asp:ListItem>
                                <asp:ListItem Text="संचालक Offer नही करना चहता  हैं" Value="संचालक Offer नही करना चाह रहा हैं"></asp:ListItem>
                                <asp:ListItem Text="गोदाम भरा हैं" Value="गोदाम भरा हैं"></asp:ListItem>
                                <asp:ListItem Text="अन्‍य" Value="अन्‍य"></asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td style="text-align: center;">
                            <asp:Button ID="btn_Update" runat="server" CssClass="yelloCell" Text="Save" OnClientClick="return confirm('Do you want to Choice Filling?');" CommandName="Update" />
                        </td>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </asp:Panel>









</asp:Content>

