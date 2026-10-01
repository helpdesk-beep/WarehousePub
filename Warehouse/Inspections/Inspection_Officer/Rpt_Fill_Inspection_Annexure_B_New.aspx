<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="Rpt_Fill_Inspection_Annexure_B_New.aspx.cs" Inherits="Inspections_Rpt_Fill_Inspection_Annexure_B_New" %>

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

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>
    <script type="text/javascript">
        function preventInput(evnt) {
            //Checked In IE9,Chrome,FireFox
            if (evnt.which != 9) evnt.preventDefault();
        }
    </script>
    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you sure want to Delete This Inspection ?") == true)
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        function test() {
            //$('[id*=GtxtLastFDate]').datepicker({
            //    changeMonth: true,
            //    changeYear: true,
            //    format: "dd/mm/yyyy",
            //    language: "tr"
            //});
        }
        //$(function () {

        //});
    </script>
    <script type="text/javascript">
        // if you use jQuery, you can load them when dom is read.
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the DatePicker
            $(".clDate").datepicker();
        });

        function InitializeRequest(sender, args) {
            // make unbind to avoid memory leaks.
            $(".clDate").unbind();
        }

        function EndRequest(sender, args) {
            // after update occur on UpdatePanel re-init the DatePicker
            $(".clDate").datepicker();
        }
    </script>
    <script type="text/javascript">
        function SUM(input) {
            // female's number
            var txtAmount_according_to_record = $(input).val() == '' ? 0 : parseInt($(input).val());
            // Current <td> which contains input for female number
            var txtAmount_according_to_recordCell = $(input).parent();
            // total <td> which contains input for total number
            var txtDifference = txtAmount_according_to_recordCell.next();
            // Male <td> which contains input for male number
            var txtAmount_found_in_physical_verificationCell = txtAmount_according_to_recordCell.prev();
            // Get Male number from input
            var txtAmount_found_in_physical_verification = txtAmount_found_in_physical_verificationCell.find('input').val() == '' ? 0 : parseInt(txtAmount_found_in_physical_verificationCell.find('input').val());
            // Do addtion 
            var total = txtAmount_found_in_physical_verification - txtAmount_according_to_record;
            //Change the content of the total
            txtDifference.find('input').val(total);
        }
    </script>
    <div style="background-color: #FDFAF7; width: 100%;">
        <table border="1" width="70%">
            <tbody>
                <th>Inspection ID </th>
                <th>Inspection Type </th>
                <th>Inspection Period</th>
                <th>Branch </th>
            </tbody>
            <tr align="center">
                <td>
                    <asp:Label ID="lblinspid" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblinsptype" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblInspPeriod" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblbranch" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Fill Godown Inspection ( Annexure 'B' ) </span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Godown : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="true" class="form-control"
                        OnSelectedIndexChanged="ddl_gdwn_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Inspection Date : "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txt_inspdate" runat="server" class="form-control"></asp:Label>
                   
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Scientific Capacity (In Qntl.) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtsci_CPT" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Max. Capacity (In Qntl.) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmaxcpt" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Godown Type : "></asp:Label>
                </td>

                <td>
                    <asp:DropDownList ID="ddlhiredtype" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Storage Type : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStorageType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--Select--"></asp:ListItem>
                        <asp:ListItem Text="Covered" Value="Covered"></asp:ListItem>
                        <asp:ListItem Text="Permanent(CAP)" Value="Permanent(CAP)"></asp:ListItem>
                        <asp:ListItem Text="Temporary(CAP)" Value="Temporary(CAP)"></asp:ListItem>
                        <asp:ListItem Text="Silo Bag" Value="Silo Bag"></asp:ListItem>
                        <asp:ListItem Text="Steel Silo" Value="Steel Silo"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="tr_griddata" runat="server" visible="false">
                <td colspan="4">
                    <table align="center" style="width: 100%;">
                        <tr>
                            <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Stack wise Balance</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" valign="top" align="center">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:GridView runat="server" ID="GD_StackBal"
                                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1%>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" />
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stack_ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStack_ID" runat="server" Text='<%# Eval("Stack_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stack Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("Stack_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stack capacity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStack_capacity" runat="server" Text='<%# Eval("Stack_capacity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Commodity Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommodity_Name" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AvlBags">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAvlBags" runat="server" Text='<%# Eval("AvlBags") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AvlQty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAvlQty" runat="server" Text='<%# Eval("AvlQty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Avlailable Bags As Per PV">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GtxtbagsActual" runat="server" Text='<%# Eval("Avlailable_Bags_As_Per_PV") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Difference of Bags">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GtxtDiffBags" runat="server" Text='<%# Eval("Difference_of_Bags") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="GtxtRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                            </Columns>

                                            <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#E6C79D" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" />
                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>


        <div class="row">
            <div class="col-lg-12">
            </div>
        </div>
    </div>
</asp:Content>

