<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Registered_Warehouse_Mapping.aspx.cs" Inherits="Registered_Warehouse_Mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

        //For searching name in gridview
        function Search_Gridview(strKey) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblDataGV1 = document.getElementById("<%= gvGodown.ClientID %>");
            var tblDataGV2 = document.getElementById("<%= gvWarehouse.ClientID %>");
            var rowData;
            if (tblDataGV1 === null || tblDataGV2 === null) {
                alert("No Values Available for Searching");
                document.getElementById("txtSearch").value = "";
            }
            else {
                for (var i = 1; i < tblDataGV1.rows.length; i++) {
                    // rowData = tblDataGV1.rows[i].cells[2].innerHTML;
                    rowData = tblDataGV1.rows[i].innerHTML;
                    var styleDisplay = 'none';
                    for (var j = 0; j < strData.length; j++) {
                        if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                            styleDisplay = '';
                        else {
                            styleDisplay = 'none';
                            break;
                        }
                    }
                    tblDataGV1.rows[i].style.display = styleDisplay;
                }

                for (var i = 1; i < tblDataGV2.rows.length; i++) {
                    // rowData = tblDataGV2.rows[i].cells[2].innerHTML;
                    rowData = tblDataGV2.rows[i].innerHTML;
                    var styleDisplay = 'none';
                    for (var j = 0; j < strData.length; j++) {
                        if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                            styleDisplay = '';
                        else {
                            styleDisplay = 'none';
                            break;
                        }
                    }
                    tblDataGV2.rows[i].style.display = styleDisplay;
                }
            }
        }


        //To check only one checkbox is checked
        function CheckGridListGodown() {
            var count = 0;
            for (i = 0; i < document.forms[0].elements.length; i++) {
                if ((document.forms[0].elements[i].type == 'checkbox') &&
                    (document.forms[0].elements[i].name.indexOf('chbGodown') > -1)) {
                    if (document.forms[0].elements[i].checked == true) {
                        count++;
                        if (count > 1) {
                            document.forms[0].elements[i].checked = false;
                            break;
                        }
                    }
                }
            }
            if (count > 1) {
                alert('Please select only one checkbox');
                return false;
            }
            else { return true; }
        }

        //To check only one checkbox is checked
        function CheckGridListWarehouse() {
            var count = 0;
            for (i = 0; i < document.forms[0].elements.length; i++) {
                if ((document.forms[0].elements[i].type == 'checkbox') &&
                    (document.forms[0].elements[i].name.indexOf('chbWarehouse') > -1)) {
                    if (document.forms[0].elements[i].checked == true) {
                        count++;
                        if (count > 1) {
                            document.forms[0].elements[i].checked = false;
                            break;
                        }
                    }
                }
            }
            if (count > 1) {
                alert('Please select only one checkbox');
                return false;
            }
            else { return true; }
        }

        //To clear the search textbox
        function ClearTxtSearch() {
            document.getElementById("<%= txtSearch.ClientID %>").value = "";
        }
</script>

     <style type="text/css">

       #leftPanel 
        {
            width: 565px;
            float: left;
            position: relative;
            height: auto;
            overflow: scroll;
            max-height: 390px;
             /*margin-right: 20px;*/
            /*margin-left: 5px;*/
            /*top: 0px;
            left: 0px;*/
        }

        #rightPanel
    {
       width: 565px;
       float: left;
       position: relative;
       height: auto;
       overflow: scroll;
       max-height: 390px;
       /*margin-left: 80px;
            top: 0px;
            left: 0px;*/
        }

      
        .auto-style2 {
            font-weight:bold;
            width: 118px;
        }
   
        .auto-style3 {
            width: 77px;
        }
   
        #divBranch {
            height: 35px;
        }
   
   
         .auto-style4 {
             width: 118px;
         }
   
   
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <div>
            <table>
                    <tr>
                        <td class="auto-style4">
                            <asp:Label ID="lblGodownType" runat="server" Text="Godown Type : " Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGodownType" runat="server" AutoPostBack="true" Height="25px" Width="176px" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                     </tr>
                 
                </table>
        </div>
        <div id="divrblist" runat="server">
            <table>
                <tr>
                    <td class="auto-style2">Select Criteria: </td>
                    <td>
                        <asp:RadioButtonList ID="rbCriteria" runat="server" RepeatDirection="Horizontal" Width="336px" AutoPostBack="true" OnSelectedIndexChanged="rbCriteria_SelectedIndexChanged">
                            <asp:ListItem> District</asp:ListItem>
                            <asp:ListItem> Branch</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>

        <div style="background-color:aliceblue;" id="divDistrict" runat="server">
              <table>
                    <tr>
                        <td class="auto-style3">
                            <asp:Label ID="Label3" runat="server" Text="Region"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRegion" runat="server" onSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true" Height="25px" Width="176px"></asp:DropDownList>
                        </td>
                        <td></td>
                        <td class="auto-style3">
                            <asp:Label ID="Label4" runat="server" Text="District"></asp:Label>
                        </td>
                        <td>  
                            <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" Height="25px" Width="176px"></asp:DropDownList>

                        </td>

           
                     </tr>
                 
                </table>
            </div>

        <div id="divBranch" runat="server" style="background-color:aliceblue;" >
            <table>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label5" runat="server" Text="Branch"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" Height="20px" Width="176px" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" style="margin-top:5px;"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>

        <div id="divMsg" runat="server">
            <table>
                 <tr>
                      <td style="text-decoration-color:red;">
                          <asp:Label ID="Label2" runat="server" Text="ConfirmMsg" Visible="False" ForeColor="Red"></asp:Label> 
                      </td>
                  </tr>
            </table>
        </div>

        <div>
           <table>
               <tr>
                   <td>
        <div style="margin-top:0px;margin-left:0px; width: 665px;" id="divSearch" runat="server">
                <table style="margin-left:0px;">
                  <tr>
                       <td>
                            <asp:Label ID="Label7" runat="server" Text="Enter Godown Name/Address/APN to Search : "></asp:Label>
                        </td>
                      <td></td>
                       <td> 
                            <asp:TextBox ID="txtSearch" runat="server" Width="222px" onkeyup="Search_Gridview(this);" onkeydown="clear()"></asp:TextBox>
                        </td>
                    
                    </tr>
                </table>
            </div>
                       </td>
                   <td>
        <div  style="width: 168px; float: left; position: relative;margin-left:150px;" id="divBtnMap" runat="server">
                <asp:Button ID="btnMapRecords" runat="server" Text="Map Godown" OnClick="btnMapRecords_Click"   Width="157px" OnClientClick="ClearTxtSearch()"/>
            </div>
                       </td>
      </tr>
                    </table>
                </div>

        <div style="margin-top:10px; margin-right: 0px;" id="divGrid" runat="server">
            <table>
                <tr>
                    <td>
                        <div id="leftPanel">
                            <asp:Label ID="lblGridGMsg" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:GridView ID="gvGodown" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" 
                            style="margin-top: 0px" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Small">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                                <Columns>
                                         <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate> 
                                            <asp:CheckBox ID="chbGodown" runat="server" OnClick="CheckGridListGodown();"></asp:CheckBox>
                                        </ItemTemplate>              
                                    </asp:TemplateField>
                                       
                                    <asp:BoundField DataField="Godown_ID" HeaderText="Godown Id" />
                                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                                    <asp:BoundField DataField="Godown_Address" HeaderText="Address" />
                                    <asp:BoundField DataField="Godown_APN" HeaderText="Godown_APN" />
                                 <%--<asp:BoundField DataField="Godown_Mobile" HeaderText="Mobile" />
                                    <asp:BoundField DataField="Godown_Email" HeaderText="Email" />
                                    <asp:BoundField DataField="Godown_Capacity" HeaderText="Capacity" />
                                    <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Scientific Capacity" />--%>
                                         
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                        </div>
                    </td>
                    <td>
                        <div id="rightPanel">
                            <asp:Label ID="lblGridWMsg" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:GridView ID="gvWarehouse" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"  style="margin-top: 0px" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Small">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate> 
                                            <asp:CheckBox ID="chbWarehouse" runat="server" OnClick="CheckGridListWarehouse();"></asp:CheckBox>
                                        </ItemTemplate>              
                                    </asp:TemplateField>                                
                                    <asp:BoundField DataField="Registration_No" HeaderText="Registration No." />
                                    <asp:BoundField DataField="Warehouse_Name" HeaderText="Warehouse Name" />
                                    <asp:BoundField DataField="Address" HeaderText="Address" />
                                   <asp:BoundField DataField="Authorized_PName" HeaderText="Whr_APN" />
                                   <%--  <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No." />
                                    <asp:BoundField DataField="Email_Id" HeaderText="Email Id" />
                                    <asp:BoundField DataField="Capacity(MT)" HeaderText="Capacity (MT)" />
                                    <asp:BoundField DataField="Registration_Date" HeaderText="Registration Date" />--%>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                        </div>

                    </td>
                </tr>
            </table>
        </div>

        </div>
</asp:Content>

