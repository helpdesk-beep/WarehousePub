<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="RO_Generate_EPF_For_Left.aspx.cs" Inherits="Reports_Region_RO_Generate_EPF_For_Left" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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

        .button3 {
            background-color: white;
            color: black;
            border: 2px solid #f44336;
        }

            .button3:hover {
                background-color: #f44336;
                color: white;
            }

        .button6 {
            background-color: white;
            color: black;
            border: 2px solid #008CBA;
        }

            .button6:hover {
                background-color: #008CBA;
                color: white;
            }

        .style3 {
            width: 200px;
           
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function PrintDiv_Actual() {
            var divContents = document.getElementById("printActualBill").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000');
            //   printWindow.document.write('<html><head><title>WHR</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        }
    </script>

    <fieldset style="width: 1100px; border: 1px solid navy; box-shadow: 1px 2px 8px; border-radius: 10px 10px 10px 10px; padding-left: 0px; margin-left: 15px">
        <center>
            <%--     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <div style="background-color: white">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="4" align="center" valign="top">
                            <fieldset style="width: 1000px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblDepositDetail" runat="server" Text="Pendinding Generate Electronic Payment File Details" Font-Size="17px"
                                                        Font-Bold="true" ForeColor="whitesmoke"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px"></td>
                    </tr>
                    <tr id="trnewproc" runat="server" visible="false">
                        <td colspan="4" align="center" valign="top">
                            <fieldset style="width: 930px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">

                                            <tr>
                                                <td style="height: 5px"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <div style="widows: 100%;" id="toexportDist" runat="server">
                                                        <asp:GridView ID="gvBOBillApp" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="Ref_Bill_No" AllowPaging="False" Width="100%"
                                                            Font-Size="10pt" BorderColor="Navy" BorderWidth="1px"
                                                            TabIndex="4" CellPadding="4" CellSpacing="2">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="DepotName" HeaderText="Branch Name" SortExpression="DepotName">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" SortExpression="Godown_Name">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Godown_Id" HeaderText="Godown Id" SortExpression="Godown_Id">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Ref_Bill_No" HeaderText="Bill No" SortExpression="Ref_Bill_No">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Account_No" HeaderText="Credit Account No" SortExpression="Account_No">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSC_Code" HeaderText="IFSC Code" SortExpression="IFSC_Code">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Transaction_Date" HeaderText="Transaction Date" SortExpression="Transaction_Date">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year" SortExpression="Crop_Year">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" SortExpression="Financial_Year">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Month_Name" HeaderText="Month" SortExpression="Month_Name">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Net_Amount" HeaderText="Credit Amount" SortExpression="Net_Amount">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                
                                                                <%-- <asp:BoundField DataField="Net_Amount" HeaderText="Credit Amount" SortExpression="Net_Amount">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>--%>
                                                                <asp:BoundField DataField="Party_Name" HeaderText="Party Name" SortExpression="Party_Name">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Beneficiary_Id" HeaderText="Beneficiary Id" SortExpression="Beneficiary_Id">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>


                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCC99" />
                                                            <PagerStyle BackColor="#719cb6" ForeColor="Black" HorizontalAlign="center" />
                                                            <SelectedRowStyle BackColor="#cc3399" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#ff6600" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                                Height="20px" Font-Size="10pt" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 5px"></td>
                                            </tr>


                                            <asp:Label ID="Label8" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                            <asp:Label ID="Label24" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>


                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>



                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>

