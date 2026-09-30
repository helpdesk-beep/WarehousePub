<%@ Page Title="" Language="C#" MasterPageFile="~/Special_PV/Special_PV.master" AutoEventWireup="true" CodeFile="~/Special_PV/Owned_Block_Wise_Entry_For_Special_PV.aspx.cs" Inherits="Special_PV_Owned_Block_Wise_Entry_For_Special_PV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
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

        .auto-style1 {
            height: 10px;
            width: 558px;
        }

        .auto-style2 {
            width: 558px;
        }
    </style>
    <style type="text/css">
        .left, .right {
            float: left;
            width: 20%; /* The width is 20%, by default */
        }

        .main {
            float: left;
            width: 60%; /* The width is 60%, by default */
        }

        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
        }
    </style>
    <style type="text/css">
        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
        }

        .content-wrapper {
            padding: 1.75rem 1.25rem;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #030203;
        }

        .form-control {
            border: 1px solid #767B83;
            border-radius: 8px;
        }

        .table th {
            text-align: center;
        }

        .form-inline {
            display: block !important;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #647e68 !important;
            color: black !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            color: black !important;
        }

        element.style {
            font-size: medium !important;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <table border="1" width="100%">
            <tbody>
                <th style="text-align: center;">गोदाम का नाम</th>
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
                    <label id="lblgodownname" runat="server"></label>
                </td>
                <td>
                    <label id="lbldepositername" runat="server"></label>
                </td>
                <td>
                    <label id="lblcommodityname" runat="server"></label>
                </td>
                <td>
                    <label id="lblstackid" runat="server"></label>
                </td>
                <td>
                    <label id="lblstackname" runat="server"></label>
                </td>
                <td>
                    <label id="lblnoofbags" runat="server"></label>
                </td>
                <td>
                    <label id="lblweight" runat="server"></label>
                </td>
                <td>
                    <label id="lblcropyear" runat="server"></label>
                </td>
            </tr>
        </table>
        <fieldset style="align-items: center">
            <legend><span style="color: #cb4e48; font-weight: bold; font-size: 17px">स्टेक प्लानिंग बिछान </span></legend>
            <div class="row">
                <div class="col-md-8">
                    <label><span style="color: #cb4e48; font-weight: bold; font-size: 17px">कोई भी फील्ड को खाली नहीं छोड़े , यदि कोई जानकारी नहीं हैं तो शून्य अवश्य डाले </span></label>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <label id="Label9" runat="server">लम्बाई :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtLendth" runat="server" AutoComplete="off" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label id="Label1" runat="server">चौड़ाई :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtwidth" runat="server" AutoComplete="off" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label id="Label2" runat="server">अतिरिक्त :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtextralendth" runat="server" AutoComplete="off" onkeypress="return isNumberKey(event)" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtextralendth_TextChanged"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <label id="Label3" runat="server">योग (ल. + चौ.+अति.):</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label id="Label5" runat="server">बोरो के लेयर की ऊंचाई :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtheight" runat="server" AutoComplete="off" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label id="Label4" runat="server">ब्लॉक क्र./संख्या :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtnoofblock" runat="server" AutoComplete="off" onkeypress="return isNumberKey(event)" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtnoofblock_TextChanged"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <label id="Label6" runat="server">बोरियो की संख्या (योग*बोरो के लेयर की ऊंचाई*ब्लॉक क्र./संख्या):</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="lbltotalbags" runat="server" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-8">
                    <label><span style="color: #cb4e48; font-weight: bold; font-size: 17px">अतिरिक्त पाई गई बोरियो की संख्या </span></label>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <label id="Label8" runat="server">ऊपर :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtup" runat="server" CssClass="form-control" AutoComplete="off" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label id="Label10" runat="server">नीचे :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtbelow" runat="server" CssClass="form-control" AutoComplete="off" AutoPostBack="true" onkeypress="return isNumberKey(event)" OnTextChanged="txtbelow_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label id="Label12" runat="server">टोटल बौरे :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txttotalnoofbags" runat="server" AutoComplete="off" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-2">
                    <label id="Label7" runat="server">Spillage Bag :</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtspillagebag" runat="server" AutoComplete="off" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label id="Label11" runat="server">Remark :</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtremark" runat="server" AutoComplete="off" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-1">
                    <asp:Button class="button button2" ID="btnsaveprofile" runat="server" Text="Save"
                        TabIndex="11" CssClass="btn btn-warning" OnClick="btnsaveprofile_Click"></asp:Button>
                    <label id="Label79" forecolor="Red" runat="server" visible="False"></label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-2">
                    <asp:Button CssClass="btn btn-warning" ID="btn_saveInspDate" runat="server" Text="Final Submit" OnClick="btn_saveInspDate_Click" Visible="false"></asp:Button>
                    &nbsp&nbsp&nbsp&nbsp
                                            <asp:Button class="button button6" ID="btnclear" runat="server" Text="Clear All" Visible="false"
                                                TabIndex="12" Width="150px" Height="30px"></asp:Button>
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend><span style="color: #cb4e48; font-weight: bold; font-size: 17px">Stack wise Balance</span></legend>
            <div class="row" id="tr_griddata" runat="server" visible="false">
                <asp:GridView runat="server" ID="GD_StackBal" OnRowCreated="GD_StackBal_RowCreated" OnRowCommand="GD_StackBal_RowCommand" ShowFooter="true"
                    OnRowDataBound="GD_StackBal_RowDataBound" AutoGenerateColumns="false" CssClass="table table-bordered table-hover" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                    <Columns>
                        <asp:TemplateField HeaderText="1">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                                <asp:HiddenField runat="server" ID="hdnDepositer_id" Value='<%# Eval("Depositer_id") %>' />
                                <asp:HiddenField runat="server" ID="hdnCommodity_id" Value='<%# Eval("Commodity_id") %>' />
                                <asp:HiddenField runat="server" ID="hdncropyear" Value='<%# Eval("Crop_Year") %>' />
                                <asp:HiddenField runat="server" ID="hdnFinalSubmitByIO" Value='<%# Eval("Final_Submit_By_IO") %>' />
                                <asp:HiddenField runat="server" ID="hdnid" Value='<%# Eval("id") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="1%" />
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Depositer_Name" HeaderText="2" />
                        <asp:BoundField DataField="Commodity_Name" HeaderText="3" />
                        <asp:BoundField DataField="stack_id" HeaderText="4" />
                        <asp:BoundField DataField="Stack_Name" HeaderText="5" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="Length" HeaderText="6" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Width" HeaderText="7" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Extra" HeaderText="8" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Total_L_W_E" HeaderText="9" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Height" HeaderText="10" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Number_Of_Block" HeaderText="11" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="No_of_Bags" HeaderText="12" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Up" HeaderText="13" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Below" HeaderText="14" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Total_Bags" HeaderText="15" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Spillage_bag" HeaderText="16" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Remark" HeaderText="17" />
                        <asp:TemplateField HeaderText="18">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CssClass="btneditstyle" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="19">
                            <ItemTemplate>
                                <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="RemoveRow" CssClass="btneditstyle" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="2">
                            <ItemTemplate>
                                <label id="lblDepositor_Name" runat="server" text='<%# Eval("Depositer_Name") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="3">
                            <ItemTemplate>
                                <label id="lblcommodity" runat="server" text='<%# Eval("Commodity_Name") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="4">
                            <ItemTemplate>
                                <label id="lblstack_id" runat="server" text='<%# Eval("stack_id") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5">
                            <ItemTemplate>
                                <label id="lblStack_Name" runat="server" text='<%# Eval("Stack_Name") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="6">
                            <ItemTemplate>
                                <label id="lblLength" runat="server" text='<%# Eval("Length") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7">
                            <ItemTemplate>
                                <label id="lblWidth" runat="server" text='<%# Eval("Width") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="8">
                            <ItemTemplate>
                                <label id="lblExtra" runat="server" text='<%# Eval("Extra") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="9">
                            <ItemTemplate>
                                <label id="lblTotal_L_W_E" runat="server" text='<%# Eval("Total_L_W_E") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10">
                            <ItemTemplate>
                                <label id="lblHeight" runat="server" text='<%# Eval("Height") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="11">
                            <ItemTemplate>
                                <label id="lblNumber_Of_Block" runat="server" text='<%# Eval("Number_Of_Block") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="12">
                            <ItemTemplate>
                                <label id="lblNo_of_Bags" runat="server" text='<%# Eval("No_of_Bags") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="13">
                            <ItemTemplate>
                                <label id="lblUp" runat="server" text='<%# Eval("Up") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="14">
                            <ItemTemplate>
                                <label id="lblBelow" runat="server" text='<%# Eval("Below") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="15">
                            <ItemTemplate>
                                <label id="lblTotal_Bags" runat="server" text='<%# Eval("Total_Bags") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="16">
                            <ItemTemplate>
                                <label id="lblSpillage_bag" runat="server" text='<%# Eval("Spillage_bag") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="17">
                            <ItemTemplate>
                                <label id="lblRemark" runat="server" text='<%# Eval("Remark") %>'></label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="18">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CssClass="btneditstyle" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="19">
                            <ItemTemplate>
                                <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="RemoveRow" CssClass="btneditstyle" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
        <%--  <table align="center" style="width: 100%; border: #000; border-style: solid; border-width: 0px;">
            <tr id="tr_griddata" runat="server" visible="false">
                <td colspan="4">
                    <table align="center" style="width: 100%;">
                        <tr>
                            <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4"></td>
                        </tr>
                        <tr>
                            <td colspan="6" valign="top" align="center"></td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" style="height: 50px;">
                               
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>--%>
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
    </div>
</asp:Content>

