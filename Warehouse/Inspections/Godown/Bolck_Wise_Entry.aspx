<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Godown.master" AutoEventWireup="true" CodeFile="Bolck_Wise_Entry.aspx.cs" Inherits="Inspections_Godown_Bolck_Wise_Entry" %>

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
    
    <div style="background-color: #FDFAF7; width: 100%;">
        <table border="1" width="70%">
            <tbody>
                <th style="text-align: center;">जमाकर्ता का नाम </th>
                <th style="text-align: center;">स्कंध का नाम </th>
                <th style="text-align: center;">स्टैक आईडी</th>
                <th style="text-align: center;">स्टैक नाम </th>
                <th style="text-align: center;">बोरी</th>
                <th style="text-align: center;">वजन</th>
                <th style="text-align: center;">वर्ष</th>
            </tbody>
            <tr align="center">
                <td>
                    <asp:Label ID="lbldepositername" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblcommodityname" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblstackid" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblstackname" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblnoofbags" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblweight" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblcropyear" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
            <tr>
                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="8">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">स्टेक प्लानिंग बिछान </span>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="8">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">कोई भी फील्ड को खाली नहीं छोड़े , यदि कोई जानकारी नहीं हैं तो शून्य अवश्य डाले </span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="लम्बाई : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLendth" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="चौड़ाई : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtwidth" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>

                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="अतिरिक्त : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtextralendth" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtextralendth_TextChanged"></asp:TextBox>
                </td>

                <td>
                    <asp:Label ID="Label3" runat="server" Text="योग (ल. + चौ.+अति.): "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="बोरो के लेयर की ऊंचाई : "></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="txtheight" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="ब्लॉक क्र./संख्या : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtnoofblock" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtnoofblock_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="बोरियो की संख्या (योग*बोरो के लेयर की ऊंचाई*ब्लॉक क्र./संख्या) : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="lbltotalbags" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="8">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">अतिरिक्त पाई गई बोरियो की संख्या </span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="ऊपर : "></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="txtup" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="निचे : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtbelow" runat="server" CssClass="form-control" AutoPostBack="true" onkeypress="return isNumberKey(event)" OnTextChanged="txtbelow_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="टोटल बौरे : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttotalnoofbags" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Spillage Bag : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtspillagebag" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Remark : "></asp:Label>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtremark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="10" align="center">
                    <asp:Button class="button button2" ID="btnsaveprofile" runat="server" Text="Save"
                        TabIndex="11" CssClass="btn btn-warning" OnClick="btnsaveprofile_Click"></asp:Button>
                   
                    <asp:Label ID="Label79" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">

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
                               
                                        <asp:GridView runat="server" ID="GD_StackBal" OnRowCreated="GD_StackBal_RowCreated" OnRowCommand="GD_StackBal_RowCommand"
                                            AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">

                                            <Columns>
                                                <asp:TemplateField HeaderText="1">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1%>
                                                        <asp:HiddenField runat="server" ID="hdnDepositer_ID" Value='<%# Eval("Depositer_ID") %>' />
                                                        <asp:HiddenField runat="server" ID="hdnCommodity_ID" Value='<%# Eval("Commodity_ID") %>' />
                                                        <asp:HiddenField runat="server" ID="hdncropyear" Value='<%# Eval("Crop_Year") %>' />
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
                                                        <asp:Label ID="lblstack_id" runat="server" Text='<%# Eval("stack_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="5">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("Stack_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="6">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="7">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="8">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExtra" runat="server" Text='<%# Eval("Extra") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="9">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotal_L_W_E" runat="server" Text='<%# Eval("Total_L_W_E") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="10">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHeight" runat="server" Text='<%# Eval("Height") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="11">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNumber_Of_Block" runat="server" Text='<%# Eval("Number_Of_Block") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="12">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo_of_Bags" runat="server" Text='<%# Eval("No_of_Bags") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="13">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUp" runat="server" Text='<%# Eval("Up") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="14">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBelow" runat="server" Text='<%# Eval("Below") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="15">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotal_Bags" runat="server" Text='<%# Eval("Total_Bags") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="16">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpillage_bag" runat="server" Text='<%# Eval("Spillage_bag") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="17">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEdit" Text="Edit" runat="server" CssClass="btneditstyle" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remove">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="RemoveRow" CssClass="btneditstyle" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#E6C79D" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" />
                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                        </asp:GridView>
                                   
                            </td>
                        </tr>

                        <tr>
                            <td colspan="6" align="center" style="height: 50px;">
                                <asp:Button CssClass="btn btn-warning" ID="btn_saveInspDate" runat="server" Text="Final Submit" OnClick="btn_saveInspDate_Click" Visible="false"></asp:Button>
                                &nbsp&nbsp&nbsp&nbsp
                                            <asp:Button class="button button6" ID="btnclear" runat="server" Text="Clear All" Visible="false"
                                                TabIndex="12" Width="150px" Height="30px"></asp:Button>
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

    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57)) {
                alert("This field will not accept the alphabet, Please Enter Only number");
                return false;
            }
            return true;
        }
        //
    </script>
</asp:Content>

