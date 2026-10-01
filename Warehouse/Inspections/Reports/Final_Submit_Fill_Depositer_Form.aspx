<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="Final_Submit_Fill_Depositer_Form.aspx.cs" Inherits="Inspections_Reports_Final_Submit_Fill_Depositer_Form" %>

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

    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you sure want to Delete This Inspection ?") == true)
                return true;
            else
                return false;
        }
    </script>



    <div style="background-color: #FDFAF7; width: 100%;">
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="6">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">जमा फार्म के साथ आवश्यक दस्तावेज एवं निरिक्षण</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Branch : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
               <%-- <td>
                    <asp:Label ID="Label9" runat="server" Text="Godown : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="false" class="form-control">
                    </asp:DropDownList>
                </td>--%>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Inspection Date : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_inspdate" runat="server" class="form-control"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Godown : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="false" class="form-control">
                    </asp:DropDownList>
                </td>
                 <td>
                    <asp:Label ID="Label3" runat="server" Text="Financial Year : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlfinancialyear" runat="server" AutoPostBack="false" Width="222px"
                        Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                        <asp:ListItem Value="0">--Select Financial Year--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center" style="height: 50px;">
                    <asp:Button CssClass="btn btn-warning" ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click"></asp:Button>

                </td>
            </tr>
        </table>
        <div id="tr_griddata" runat="server" visible="false" class="row" style="overflow: auto;">
            <%--<div class="col-lg-12">--%>
           <%-- <asp:UpdatePanel runat="server">
                <ContentTemplate>--%>
                    <asp:GridView runat="server" ID="GD_StackBal" AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="क्रमांक">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                    <asp:HiddenField runat="server" ID="hdnid" Value='<%# Eval("ID") %>' />
                                     <asp:HiddenField runat="server" ID="hdngodownid" Value='<%# Eval("Godown_ID") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="गोदाम का नाम">
                                <ItemTemplate>
                                    <asp:Label ID="lblGodown_Name" runat="server" Width="100px" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="जमा फार्म नंबर">
                                <ItemTemplate>
                                    <asp:Label ID="GtxtdepositformNo" runat="server" Width="100px" Text='<%# Eval("deposit_form_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="जमा फार्म दिनांक">
                                <ItemTemplate>
                                    <asp:Label ID="Gtxtdepositformdate" Width="100px" Text='<%# Eval("Date_of_deposit_form") %>'
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="वेयरहाउस रसीद क्र.">
                                <ItemTemplate>
                                    <asp:Label ID="lblWhr_No" runat="server" Text='<%# Eval("Whr_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="वेयरहाउस रसीद का दिनांक">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Eval("WHR_Date") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="कमोडिटी का नाम">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity_Name" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="बोरे की संख्या">
                                <ItemTemplate>
                                    <asp:Label ID="lblAvlBags" runat="server" Text='<%# Eval("No_of_Bags") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="उपलब्ध मात्रा">
                                <ItemTemplate>
                                    <asp:Label ID="lblAvlQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="कमोडिटी मार्केट वैल्यू">
                                <ItemTemplate>
                                    <asp:Label ID="lblMktValue_of_Commodity" runat="server" Text='<%# Eval("Mkt_value") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ग्रेड">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("Grade") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="जमाकर्ता के हस्ताक्षर">
                                <ItemTemplate>
                                    <asp:Label ID="lblSignature_of_depositor" runat="server" Text='<%# Eval("Signature_of_depositor") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="शाखा प्रबंधक के हस्ताक्षर">
                                <ItemTemplate>
                                    <asp:Label ID="lblSignature_of_BM" runat="server" Text='<%# Eval("Signature_of_BM") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="जमा गेट पास">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeposit_Gate_Pass" runat="server" Text='<%# Eval("Deposit_Gate_Pass") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="काँटा पर्ची(वजन का पत्रक तोल संबंधी)">
                                <ItemTemplate>
                                    <asp:Label ID="lblKata_Parchi" runat="server" Text='<%# Eval("Kata_Parchi") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ट्रक चालान">
                                <ItemTemplate>
                                    <asp:Label ID="lblTruck_Chalan" runat="server" Text='<%# Eval("Truck_Chalan") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="GtxtRemark" runat="server" TextMode="MultiLine" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#E6C79D" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
             <div id="divbtn" runat="server" visible="false" style="text-align: center;">
                <asp:Button runat="server" ID="btnfinalsubmit" Text="Final Submit" CssClass="btn btn-success" OnClick="Submit"/>
                &nbsp&nbsp&nbsp&nbsp
                                            <asp:Button class="button button6" ID="btnclear" runat="server" Text="Clear All" Visible="true"
                                                TabIndex="12" Width="150px" Height="30px"></asp:Button>
            </div>
        </div>
    </div>

</asp:Content>

