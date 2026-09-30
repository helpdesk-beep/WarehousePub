<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="Rpt_Fill_Delevery_Order_Form_Details.aspx.cs" Inherits="Inspections_Inspection_Officer_Rpt_Fill_Delevery_Order_Form_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <div style="background-color: #FDFAF7; width: 100%;">
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="6">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">भुगतान फार्म के साथ आवश्यक दस्तावेज एवं निरिक्षण</span>
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
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Godown : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="false" class="form-control">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Last Inspection Date From : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_inspdate" runat="server" class="form-control"></asp:TextBox>
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
                    <asp:GridView runat="server" ID="GD_StackBal" OnRowDataBound="GD_StackBal_RowDataBound"
                        OnRowEditing="GD_StackBal_RowEditing" OnRowUpdating="GD_StackBal_RowUpdating" OnRowCancelingEdit="GD_StackBal_RowCancelingEdit"
                        AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="क्रमांक">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                    <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_ID") %>' />
                                    <asp:HiddenField runat="server" ID="hdngodownid" Value='<%# Eval("Godown_ID") %>' />
                                    <asp:HiddenField runat="server" ID="hdncommodityid" Value='<%# Eval("Commodity_Id") %>' />
                                    <%--<asp:HiddenField runat="server" ID="hdnid" Value='<%# Eval("ID") %>' />--%>
                                    <asp:HiddenField runat="server" ID="hdnInspDate" Value='<%# Eval("InspDate") %>' />
                                    <asp:HiddenField runat="server" ID="hdnDO_No" Value='<%# Eval("DO_No") %>' />
                                    <asp:HiddenField runat="server" ID="hdnSignature_of_depositorID" Value='<%# Eval("Signature_of_depositorID") %>' />
                                    <asp:HiddenField runat="server" ID="hdnSignature_of_BMID" Value='<%# Eval("Signature_of_BMID") %>' />
                                    <asp:HiddenField runat="server" ID="hdnDeposit_Gate_PassID" Value='<%# Eval("Deposit_Gate_PassID") %>' />
                                    <asp:HiddenField runat="server" ID="hdnKata_ParchiID" Value='<%# Eval("Kata_ParchiID") %>' />
                                    <asp:HiddenField runat="server" ID="hdnTruck_ChalanID" Value='<%# Eval("Truck_ChalanID") %>' />
                                     <asp:HiddenField runat="server" ID="hdnCancil_WHRID" Value='<%# Eval("Cancil_WHRID") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="भुगतान फार्म नंबर">
                                <ItemTemplate>
                                    <asp:Label ID="GtxtdepositformNo" runat="server" Width="100px" Text='<%# Eval("DO_form_No") %>'></asp:Label>
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:TextBox ID="txt_DO_form_No" runat="server" Text='<%#Eval("DO_form_No") %>'></asp:TextBox>
                                </EditItemTemplate>--%>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="भुगतान फार्म दिनांक">
                                <ItemTemplate>
                                    <asp:Label ID="Gtxtdepositformdate" Width="100px" Text='<%# Eval("Date_of_DO_form") %>'
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                               <%-- <EditItemTemplate>
                                    <asp:TextBox ID="txt_DO_Date" runat="server" Text='<%#Eval("Date_of_DO_form") %>'></asp:TextBox>
                                </EditItemTemplate>--%>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="भुगतान आदेश क्र.">
                                <ItemTemplate>
                                    <asp:Label ID="lblDO_No" runat="server" Text='<%# Eval("DO_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="भुगतान आदेश का दिनांक">
                                <ItemTemplate>
                                    <asp:Label ID="lblDO_Date" runat="server" Text='<%# Eval("DO_Date") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="कमोडिटी का नाम">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity_Name" runat="server" Text='<%# Eval("Commodity_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="जारी किए गए बैग संख्या">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalBags_Issued" runat="server" Text='<%# Eval("No_of_Bags_Issued") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="वजन">
                                <ItemTemplate>
                                    <asp:Label ID="lblQty_Issued_Weight" runat="server" Text='<%# Eval("Qty_Issued_Weight") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="भुगतान प्राप्त कर्ता के हस्ताक्षर">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("Signature_of_depositor") %>'></asp:Label>
                                </ItemTemplate>
                               
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="शाखा प्रबंधक के हस्ताक्षर">
                                <ItemTemplate>
                                    <asp:Label ID="lblBS" runat="server" Text='<%# Eval("Signature_of_BM") %>'></asp:Label>
                                </ItemTemplate>
                                
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="भुगतान गेट पास">
                                <ItemTemplate>
                                    <asp:Label ID="lblGP" runat="server" Text='<%# Eval("Deposit_Gate_Pass") %>'></asp:Label>
                                </ItemTemplate>
                               
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="काँटा पर्ची">
                                <ItemTemplate>
                                    <asp:Label ID="lblKP" runat="server" Text='<%# Eval("Kata_Parchi") %>'></asp:Label>
                                </ItemTemplate>
                               
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="भुगतान संबंधी आदेश (DO,RO,परिवहन आदेश) ">
                                <ItemTemplate>
                                    <asp:Label ID="lblTC" runat="server" Text='<%# Eval("Truck_Chalan") %>'></asp:Label>
                                </ItemTemplate>
                               
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="निरस्त WHR संलग्न हे या नहीं">
                                <ItemTemplate>
                                    <asp:Label ID="lblcancilwhr" runat="server" Text='<%# Eval("Cancil_WHR") %>'></asp:Label>
                                </ItemTemplate>
                                
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="GtxtRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                              
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Edit" CssClass="btn btn-info"
                                OnClick="Display"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                           <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" />
                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#E6C79D" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
               <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
             <asp:Panel ID="pnllogin" class="popup" runat="server">
                <div class="pop" style="background-color: #FFFFCC0; min-height: 600PX; max-height: 500px; overflow: auto;">
                    <%-- <div class="col-sm-12 col-md-12 col-xs-12">--%>
                    <img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />


                    <div id="divNewInsp" runat="server" visible="false" style="width: 100%;">
                        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">

                            <tr>
                                <td style="height: 5px;"></td>
                            </tr>

                           <%-- <tr>
                                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="6">
                                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Godown Name:- 
                                        <asp:Label ID="lblgdnname" runat="server"></asp:Label></span>
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="height: 10px;"></td>
                            </tr>

                            <tr>
                                <td>&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="Label17" runat="server" Text="भुगतान फार्म नंबर : "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="GtxtdepositformNo" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label19" runat="server" Text="भुगतान फार्म दिनांक:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Gtxtdepositformdate" runat="server"></asp:TextBox>
                                </td>
                               
                            </tr>

                            <tr>
                                <td>&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="Label13" runat="server" Text="भुगतान आदेश क्र. : "></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDONO" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Text="भुगतान आदेश का दिनांक:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDODATE" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="Label7" runat="server" Text="कमोडिटी का नाम : "></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCommodity" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="Label4" runat="server" Text="जारी किए गए बैग संख्या : "></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblBages" runat="server" class="text" type="text" Height="25px" Width="222px" ReadOnly="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="वजन :"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWeight" runat="server" class="text" type="text" Height="25px" Width="222px" ReadOnly="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="भुगतान प्राप्त कर्ता के हस्ताक्षर :"></asp:Label>
                                </td>
                                <td>
                                     <asp:DropDownList ID="ddlsgndepositer" runat="server" class="form-control" Width="80px">
                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                        <asp:ListItem Value="2">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="Label3" runat="server" Text="शाखा प्रबंधक के हस्ताक्षर : "></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBS" runat="server" class="form-control" Width="80px">
                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                        <asp:ListItem Value="2">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="भुगतान गेट पास : "></asp:Label>
                                </td>
                                <td>
                                     <asp:DropDownList ID="ddlgatrpass" runat="server" class="form-control" Width="80px">
                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                        <asp:ListItem Value="2">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="काँटा पर्ची : "></asp:Label>
                                </td>
                                <td>
                                   <asp:DropDownList ID="ddltollslip" runat="server" class="form-control" Width="80px">
                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                        <asp:ListItem Value="2">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp&nbsp&nbsp&nbsp<asp:Label ID="Label11" runat="server" Text="भुगतान संबंधी आदेश (DO,RO,परिवहन आदेश) : "></asp:Label></td>
                                <td>
                                     <asp:DropDownList ID="ddltruckparchi" runat="server" class="form-control" Width="80px">
                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                        <asp:ListItem Value="2">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                                <td>&nbsp&nbsp&nbsp&nbsp<asp:Label ID="Label16" runat="server" Text="निरस्त WHR संलग्न हे या नहीं : "></asp:Label></td>
                                <td>
                                     <asp:DropDownList ID="ddlcancilWHR" runat="server" class="form-control" Width="80px">
                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                        <asp:ListItem Value="2">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                </tr>
                            <tr>
                                <td>&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="Label12" runat="server" Text="Remark : "></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>

                            </tr>


                            <tr>

                                <td style="height: 10px;"></td>
                            </tr>
                             <tr>
                                <td colspan="6" align="center">
                                    <asp:Button class="button button6" ID="btn_saveInspDate" runat="server" Text="Submit"
                                        TabIndex="11" Width="222px" Height="30px" OnClick="btn_saveInspDate_Click"></asp:Button>
                                    &nbsp&nbsp&nbsp&nbsp
                                            <asp:Button class="button button6" ID="btnclear" runat="server" Text="Clear All"
                                                TabIndex="12" Width="222px" Height="30px" OnClick="btnclear_Click"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px;"></td>
                            </tr>
                        </table>
                        <asp:Label ID="Label15" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

                    </div>


                    <%--------End Of Third Section -------------%>
                    <%-- </div>--%>
                </div>
                <img alt="New" src="images/new6.gif" id="new" runat="server" />

            </asp:Panel>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="new" BackgroundCssClass="popup" PopupControlID="pnllogin" CancelControlID="x">
            </asp:ModalPopupExtender>

            <asp:AnimationExtender ID="popupAnimation" runat="server" TargetControlID="new">
                <Animations>
                <OnClick>
         <Parallel AnimationTarget="pnllogin" Duration="0.4" Fps="10">
            <FadeIn />
          </Parallel>
       </OnClick>
                </Animations>
            </asp:AnimationExtender>
        </div>

    </div>
    <script>
            $(document).ready(function () {
                $("[id$=txt_inspdate]").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: 'dd/mm/yy',
                });
            });
    </script>
</asp:Content>

