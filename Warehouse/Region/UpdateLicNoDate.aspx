<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="UpdateLicNoDate.aspx.cs" Inherits="Region_UpdateLicNoDate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 0px 0px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-weight: bold;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
        }

        .button1 {
            background-color: white;
            color: black;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button2:hover {
                background-color: #008CBA;
                color: white;
            }
    </style>
    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
    </script>
    <fieldset style="width: 90%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">

                            <center>
                                <div>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr style="background-color: #0bb6e6; height: 25px">
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblGodownMaster" runat="server" Text="Update New Verify Godown Entry" Font-Bold="true"
                                                    Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>


                                        <tr>


                                            <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp;&nbsp;&nbsp;&nbsp  District : &nbsp;&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                                                Height="25px" Width="168px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>

                                                &nbsp;&nbsp;&nbsp;&nbsp  Branch : &nbsp;&nbsp;<asp:DropDownList ID="ddlBranch" runat="server"
                                                    Height="25px" Width="168px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                </asp:DropDownList>


                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>

                                        <tr>
                                            <td colspan="4" valign="top" align="center">
                                                <%--        <asp:GridView ID="Depositor_Gridview"  runat="server"
            DataKeyNames="Godown_ID" 
            AutoGenerateColumns="False" Width="70%"  BackColor="White" 
                BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5" 
    CellSpacing="2" PageSize="50" 
                onselectedindexchanged="Depositor_Gridview_SelectedIndexChanged">
            <Columns>
              <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" ReadOnly="True" SortExpression="Godown_ID"/>
              <asp:BoundField DataField="Godown_Name" HeaderText="Godown_Name" ReadOnly="True" SortExpression="Godown_Name" />
              <asp:BoundField DataField="Hired_Type" HeaderText="Hired_Type" ReadOnly="True" SortExpression="Hired_Type" />
              <asp:BoundField DataField="Storage_Type" HeaderText="Storage_Type" ReadOnly="True" SortExpression="Storage_Type" />
              <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Godown_Scientific_Capacity" ReadOnly="True" SortExpression="Godown_Scientific_Capacity" />
              <asp:BoundField DataField="Premise_capacity" HeaderText="Premise_capacity" ReadOnly="True" SortExpression="Premise_capacity" />
              <asp:BoundField DataField="Closing_Balance" HeaderText="Closing_Balance" ReadOnly="True" SortExpression="Closing_Balance" />
             
              <asp:CommandField SelectText="Select" HeaderText="Delete" ShowSelectButton="True" >
                   <ControlStyle Font-Bold="True" ForeColor="Red" />
             </asp:CommandField>
            </Columns>
            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                            Height="20px" Font-Size="10pt" />
                                                        <AlternatingRowStyle BackColor="#eeeeee" />
          </asp:GridView>
          
                                                --%>



                                                <asp:GridView ID="Depositor_Gridview" runat="server" DataKeyNames="Godown_ID" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                    BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                    CellSpacing="2" OnSelectedIndexChanged="Depositor_Gridview_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" ReadOnly="True" SortExpression="Godown_ID" />
                                                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown_Name" ReadOnly="True" SortExpression="Godown_Name" />
                                                        <asp:BoundField DataField="Hired_Type" HeaderText="Hired_Type" ReadOnly="True" SortExpression="Hired_Type" />
                                                        <asp:BoundField DataField="Storage_Type" HeaderText="Storage_Type" ReadOnly="True" SortExpression="Storage_Type" />
                                                        <asp:BoundField DataField="Godown_Scientific_Capacity" HeaderText="Scientific Capacity" ReadOnly="True" SortExpression="Godown_Scientific_Capacity" />
                                                        <asp:BoundField DataField="Godown_Capacity" HeaderText="Max Capacity" ReadOnly="True" SortExpression="Godown_Capacity" />

                                                        <asp:BoundField DataField="Closing_Balance" HeaderText="Closing_Balance" ReadOnly="True" SortExpression="Closing_Balance" />
                                                        <asp:BoundField DataField="LicNum" HeaderText="LicNum" ReadOnly="True" SortExpression="LicNum" />
                                                        <asp:BoundField DataField="LicDate" HeaderText="Lic Expiry Date" ReadOnly="True" SortExpression="LicDate" />
                                                        <asp:CommandField SelectText="Select" HeaderText="Select for Update" ShowSelectButton="True">
                                                            <ControlStyle Font-Bold="True" ForeColor="Red" />
                                                        </asp:CommandField>

                                                    </Columns>
                                                    <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                        Height="20px" Font-Size="10pt" />
                                                    <AlternatingRowStyle BackColor="#eeeeee" />
                                                </asp:GridView>






                                            </td>
                                        </tr>


                                        <tr id="trmobtxt" runat="server" visible="false">
                                            <td style="height: 70px; font-size: 14px" colspan="4" align="center">
                                                <asp:Label ID="Label1" runat="server" Text="Godown ID : "></asp:Label>
                                                <asp:TextBox ID="txtGdwnID" runat="server" ReadOnly="true"
                                                    Width="150px" Height="20px"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                      &nbsp; Godown Maximum Capacity(In Qntl) &nbsp;
                                                    <asp:TextBox ID="txtMaxCpt" runat="server"
                                                        Width="100px" Height="20px"></asp:TextBox>
                                                &nbsp;
                                                <br />
                                            </td>
                                        </tr>

                                        <tr id="tr2" runat="server" visible="false">
                                            <td style="height: 50px; font-size: 14px" colspan="4" align="center">&nbsp; Scientific Capacity(In Qntl) &nbsp;
                                                    <asp:TextBox ID="txtscieCPT" runat="server"
                                                        Width="100px" Height="20px"></asp:TextBox>
                                                &nbsp;   
                                                   Hired Type   &nbsp;&nbsp;
                                                    <asp:DropDownList ID="ddllst_hired" runat="server" Width="155px" Height="25px">
                                                    </asp:DropDownList>
                                                &nbsp;&nbsp;                                   
                                                  Storage Type &nbsp;&nbsp;
                                                <asp:DropDownList ID="ddllst_storage" runat="server" Width="155px" Height="25px">
                                                    <asp:ListItem Text="Covered" Value="Covered"></asp:ListItem>
                                                    <asp:ListItem Text="Permanent(CAP)" Value="Permanent(CAP)"></asp:ListItem>
                                                    <asp:ListItem Text="Temporary(CAP)" Value="Temporary(CAP)"></asp:ListItem>
                                                    <asp:ListItem Text="Silo Bag" Value="Silo Bag"></asp:ListItem>
                                                    <asp:ListItem Text="Steel Silo" Value="Steel Silo"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr id="tr1" runat="server" visible="false">
                                            <td style="height: 70px; font-size: 14px" colspan="4" align="center">Closing Balance (In Qntl) &nbsp&nbsp
                                                    <asp:TextBox ID="txtclosing" runat="server"
                                                        Width="100px" Height="20px"></asp:TextBox>
                                                &nbsp;&nbsp;
                                                   Licence No. &nbsp&nbsp
                                                    <asp:TextBox ID="txtlicno" runat="server"
                                                        Width="150px" Height="20px"></asp:TextBox>
                                                &nbsp;&nbsp;
                                                   Licence Date &nbsp&nbsp 
                                                <%--                                                    <asp:TextBox ID="txtlicdate" runat="server" 
                                                        Width="100px" Height="20px" ></asp:TextBox>--%>
                                                <asp:TextBox ID="txtlicdate" runat="server" Width="100px" Height="20px"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="height: 5px" colspan="4"></td>
                                        </tr>
                                        <tr id="trbtnhide" runat="server" visible="false">

                                            <td align="Right">
                                                <asp:Button class="button button1" ID="btnAddCompany" Style="width: 100px" runat="server"
                                                    Text="Update" Height="29px" OnClick="btnAddCompany_Click"></asp:Button>&nbsp&nbsp&nbsp&nbsp
                                            </td>
                                            <td align="left">

                                                <asp:Button class="button button2" ID="btnGenerateBill" Style="width: 100px" runat="server" Text="Close" Height="29px"></asp:Button></td>
                                        </tr>

                                    </table>
                                </div>
                            </center>

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>

                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>



