<%@ Page Title="" Language="C#" MasterPageFile="~/Special_PV/Region/Inspection_RO_For_Special_PV.master" AutoEventWireup="true" CodeFile="View_Insp_For_Special_PV.aspx.cs" Inherits="Special_PV_Region_View_Insp_For_Special_PV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.3/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.8.3/jquery-ui.js"></script>
    <style type="text/css">
        .wrap {
            margin: 0 auto;
            width: 960px;
            -moz-box-shadow: 0px 5px 23px #000;
            -webkit-box-shadow: 0px 5px 23px #000;
            box-shadow: 0px 5px 23px #000;
        }

        input.submit {
            color: #fff;
            padding: 7px 10px;
            border: 0;
            font-weight: bold;
            background: #777;
            border-radius: 25px;
        }

        input.text {
            border: 2px solid rgb(173, 204, 204);
            height: 20px;
            width: 223px;
            font-size: 16px;
            box-shadow: 0px 0px 27px rgb(204, 204, 204) inset;
            transition: 500ms all ease;
            padding: 3px 3px 3px 3px;
        }
    </style>
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

        .style1 {
            height: 30px;
        }
    </style>

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
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #D69758;
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
        function PrintPanel() {
            var panel = document.getElementById("<%=divshow.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="background-color: #FDFAF7; width: 100%;">
        <asp:Button ID="Button1" runat="server" Text="Print" OnClientClick="return PrintPanel();" CssClass="btn btn-warning" />

        <div id="divshow" runat="server" visible="true" class="widget-content">
            <asp:GridView runat="server" ID="GD_StackBal" OnRowCreated="GD_StackBal_RowCreated"
                OnRowCommand="GD_StackBal_RowCommand" OnRowDataBound="GD_StackBal_RowDataBound"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
                PagerStyle-CssClass="pgr" ShowFooter="true">

                <Columns>
                    <asp:TemplateField HeaderText="क्रमांक">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                        <ItemStyle Width="1%" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Godown ID">
                        <ItemTemplate>
                            <asp:Label ID="lblGodown_ID" runat="server" Text='<%# Eval("Godown_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="गोदाम का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblGodown_Name" runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="जमाकर्ता का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Depositer_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="स्कंध का नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblcommodity" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="वर्ष">
                        <ItemTemplate>
                            <asp:Label ID="lblCrop_Year" runat="server" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="स्टैक आईडी">
                        <ItemTemplate>
                            <asp:Label ID="lblstack_id" runat="server" Text='<%# Eval("stack_id") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="स्टैक नाम">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("Stack_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="लम्बाई">
                        <ItemTemplate>
                            <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="चौड़ाई">
                        <ItemTemplate>
                            <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="अतिरिक्त">
                        <ItemTemplate>
                            <asp:Label ID="lblExtra" runat="server" Text='<%# Eval("Extra") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="योग (7+8+9)">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_L_W_E" runat="server" Text='<%# Eval("Total_L_W_E") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="बोरो के लेयर की ऊंचाई">
                        <ItemTemplate>
                            <asp:Label ID="lblHeight" runat="server" Text='<%# Eval("Height") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ब्लॉक क्र./संख्या">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber_Of_Block" runat="server" Text='<%# Eval("Number_Of_Block") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="बोरियो की संख्या (10*11*12)">
                        <ItemTemplate>
                            <asp:Label ID="lblNo_of_Bags" runat="server" Text='<%# Eval("No_of_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ऊपर">
                        <ItemTemplate>
                            <asp:Label ID="lblUp" runat="server" Text='<%# Eval("Up") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="निचे">
                        <ItemTemplate>
                            <asp:Label ID="lblBelow" runat="server" Text='<%# Eval("Below") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="टोटल बौरे">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_Bags" runat="server" Text='<%# Eval("Total_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spillage Bag">
                        <ItemTemplate>
                            <asp:Label ID="lblSpillage_bag" runat="server" Text='<%# Eval("Spillage_bag") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#E6C79D" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

