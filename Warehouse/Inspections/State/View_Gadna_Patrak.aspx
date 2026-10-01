<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="View_Gadna_Patrak.aspx.cs" Inherits="Inspections_State_View_Annaxure_B" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.3/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/NEW_CSS/js/jquery-ui.js") %>"></script>
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
        function printGrid() {
            var gridData = document.getElementById('<%= GD_StackBal.ClientID %>');
            var windowUrl = 'about:blank';

            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
                'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
        }
    </script>

    <div style="background-color: #FDFAF7; width: 100%;">
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
           <tr>
                 <td style="text-align: center"; colspan="6">
                    <asp:Label ID="lblInspOfficerName" runat="server" ForeColor="Red" ></asp:Label>
                     </td>
            </tr>
            <tr>
                <td style="text-align:left;width: 300px;">
                    <asp:Label ID="Label2" runat="server" Text="Order Date : "></asp:Label>
                </td>
                <td style="text-align:right;width: 300px;">
                    <asp:TextBox ID="txt_inspdate" runat="server" class="form-control"></asp:TextBox>
                   
                </td>
                <td align="right">Crop Year :
                                          
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="true"
                        Height="30px" CssClass="form-control" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged">
                        <asp:ListItem Value="2010-11">2010-11</asp:ListItem>
                        <asp:ListItem Value="2012-13">2012-13</asp:ListItem>
                        <asp:ListItem Value="2013-14">2013-14</asp:ListItem>
                        <asp:ListItem Value="2014-15">2014-15</asp:ListItem>
                        <asp:ListItem Value="2015-16">2015-16</asp:ListItem>
                        <asp:ListItem Value="2016-17">2016-17</asp:ListItem>
                        <asp:ListItem Value="2017-18">2017-18</asp:ListItem>
                        <asp:ListItem Value="2018-19">2018-19</asp:ListItem>
                        <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                        <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                        <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                        <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                        <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                    </asp:DropDownList>
                </td>
            
           
                
                 <td style="text-align: right; width: 300px;">
                    <asp:Label ID="Label1" runat="server" Text="District : "></asp:Label>
                </td>
                <td style="text-align: left; width: 300px;">
                    <asp:DropDownList ID="ddl_dist" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
               
                <td style="text-align: right; width: 300px;">
                    <asp:Label ID="Label3" runat="server" Text="Branch : "></asp:Label>
                </td>
                <td style="text-align: left; width: 300px;">
                    <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
                <td style="text-align: left; width: 300px;">
                    <asp:Label ID="Label9" runat="server" Text="Godown : "></asp:Label>
                </td>
                <td style="text-align: right; width: 300px;">
                    <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="false" class="form-control">
                    </asp:DropDownList>
                </td>
                <td colspan="2" align="center" style="height: 50px;">
                    <asp:Button CssClass="btn btn-warning" ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click"></asp:Button>

                </td>
            </tr>
            
        </table>
        <div id="divshow" runat="server" visible="false">
            <div class="card-body">
                <%--<asp:Button ID="btnExportToWord" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="ExportToWord"  />--%>
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-warning" Text="Print" OnClientClick="printGrid()" />
            </div>
            <asp:GridView runat="server" ID="GD_StackBal" OnRowCreated="GD_StackBal_RowCreated" 
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" ShowFooter="true">

                <Columns>
                    <asp:TemplateField HeaderText="1">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                            <asp:HiddenField runat="server" ID="hdnDepositer_ID" Value='<%# Eval("Depositer_ID") %>' />
                            <asp:HiddenField runat="server" ID="hdnCommodity_ID" Value='<%# Eval("Commodity_ID") %>' />
                            <asp:HiddenField runat="server" ID="hdncropyear" Value='<%# Eval("Crop_Year") %>' />
                            <asp:HiddenField runat="server" ID="hdnFinalsubmit" Value='<%# Eval("Final_Submit_By_IO") %>' />
                            <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="1%" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="2">
                        <ItemTemplate>
                            <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Depositer_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="3">
                        <ItemTemplate>
                            <asp:Label ID="lblcommodity" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="4">
                        <ItemTemplate>
                            <asp:Label ID="lblCrop_Year" runat="server" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="5">
                        <ItemTemplate>
                            <asp:Label ID="lblstack_id" runat="server" Text='<%# Eval("stack_id") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="6">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("Stack_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="7">
                        <ItemTemplate>
                            <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="8">
                        <ItemTemplate>
                            <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="9">
                        <ItemTemplate>
                            <asp:Label ID="lblExtra" runat="server" Text='<%# Eval("Extra") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="10">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_L_W_E" runat="server" Text='<%# Eval("Total_L_W_E") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="11">
                        <ItemTemplate>
                            <asp:Label ID="lblHeight" runat="server" Text='<%# Eval("Height") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="12">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber_Of_Block" runat="server" Text='<%# Eval("Number_Of_Block") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="13">
                        <ItemTemplate>
                            <asp:Label ID="lblNo_of_Bags" runat="server" Text='<%# Eval("No_of_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="14">
                        <ItemTemplate>
                            <asp:Label ID="lblUp" runat="server" Text='<%# Eval("Up") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="15">
                        <ItemTemplate>
                            <asp:Label ID="lblBelow" runat="server" Text='<%# Eval("Below") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="16">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_Bags" runat="server" Text='<%# Eval("Total_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="17">
                        <ItemTemplate>
                            <asp:Label ID="lblSpillage_bag" runat="server" Text='<%# Eval("Spillage_bag") %>'></asp:Label>
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

