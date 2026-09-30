<%@ Page Title="" Language="C#" MasterPageFile="~/Special_PV/Special_PV.master" AutoEventWireup="true" CodeFile="~/Special_PV/BO_Special_PV.aspx.cs" Inherits="Special_PV_BO_Special_PV" %>

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
        <div class="row">
            <asp:Label ID="lbl_user" runat="server" ForeColor="White" Visible="false"></asp:Label>
            <div style="text-align: center; font-size: x-large; color: green;">
                निरीक्षण/भौतिक सत्यापन करने के लिए  31/12/2024 का Closing बैलेंस लिया गया हैं 
          <br />
                ऑनलाइन स्टॉक पोजीशन के आधार पर लिया गया हैं  
          </div>
        </div>
        <br />
        <div class="row" id="divshow1" runat="server" visible="false" style="margin-top: 10px">
            <div class="col-md-1"></div>
            <div class="col-md-10">
                <asp:GridView runat="server" ID="GrdOfficerPreviousInsp" OnRowCommand="GrdOfficerPreviousInsp_RowCommand" ShowFooter="true"
                    AutoGenerateColumns="false" CssClass="table table-bordered table-hover" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowDataBound="GrdOfficerPreviousInsp_RowDataBound"
                    OnRowCreated="GrdOfficerPreviousInsp_RowCreated">
                    <Columns>
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" HeaderStyle-BackColor="#D69758" />
                        <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" HeaderStyle-BackColor="#D69758" />
                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name" HeaderStyle-BackColor="#D69758" />
                        <asp:BoundField DataField="commodity" HeaderText="commodity" HeaderStyle-BackColor="#D69758" />
                        <asp:BoundField DataField="CropYear" HeaderText="Crop Year" HeaderStyle-BackColor="#D69758" />
                        <asp:TemplateField HeaderText="recbags" HeaderStyle-BackColor="#D69758">
                            <ItemTemplate>
                                <asp:Label ID="lblrecbags" runat="server" Text='<%# Eval("recbags") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="delbags" HeaderStyle-BackColor="#D69758">
                            <ItemTemplate>
                                <asp:Label ID="lbldelbags" runat="server" Text='<%# Eval("delbags") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rec_Weight" HeaderStyle-BackColor="#D69758">
                            <ItemTemplate>
                                <asp:Label ID="lblRec_Weight" runat="server" Text='<%# Eval("Rec_Weight") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Del_Weight" HeaderStyle-BackColor="#D69758">
                            <ItemTemplate>
                                <asp:Label ID="lblDel_Weight" runat="server" Text='<%# Eval("Del_Weight") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bag Balance" HeaderStyle-BackColor="#D69758">
                            <ItemTemplate>
                                <asp:Label ID="lblBag_Balance" runat="server" Text='<%# Eval("Bag_Balance") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Weight Balance" HeaderStyle-BackColor="#D69758">
                            <ItemTemplate>
                                <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <%-- <div class="content-wrapper">
        <fieldset style="border: 1px solid navy; border-radius: 10px 10px 10px 10px;">
            <legend>Special PV</legend>
            <div class="row">
                <asp:Label ID="lbl_user" runat="server" ForeColor="White" Visible="false"></asp:Label>
                <div style="text-align: center; font-size: x-large; color: green;">
                    निरीक्षण/भौतिक सत्यापन करने के लिए Closing दिनांक चयन करने का विकल्प ऑनलाइन 
          <br />
                    सॉफ्टवेयर में प्रदान कर दिया गया है ! आप जिस दिनाँक की Closing अपने निरीक्षण अधिकारी को 
          <br />
                    देना चाहते हो उस दिनाँक तक का पूरा ऑनलाइन डाटा आपके निरिक्षण अधिकारी को दिखने लगेगा
                </div>
                <br />
                <div style="text-align: center; font-size: x-large; color: red;">
                    निरीक्षण अधिकारी को Closing बैलेंस देने के लिए दिये गए सभी विकल्प अच्छे से जाँच कर लेवे
            <br />
                    क्योकि इसी बैलेंस के अनुसार आपकी ब्रांच का निरीक्षण/भौतिक सत्यापन किया जायेगा
                </div>
            </div>
            
            <div class="row" style="margin-top: 10px">
               <div class="col-md-2">
                    <label style="margin-top: 10px">Financial</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlfinancialyear" CssClass="form-control" runat="server" AutoPostBack="false"
                        Font-Bold="true" ForeColor="Navy">
                        <asp:ListItem Value="0">--Select Financial Year--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label style="margin-top: 10px">Inspection Closing Date</label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtdob" placeholder="DD/MM/YY" onclick="return ValidateDOB()" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtdob" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtdob" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                </div>
                <div class="col-md-2">
                    <label style="margin-top: 10px">Inspection Type</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlverification" runat="server" CssClass="form-control" AutoPostBack="false"
                        Font-Bold="true" ForeColor="Navy">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="2">Physical Verification</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px">
                <div class="col-md-5"></div>
                <div class="col-md-2">
                    <asp:Button class="button button3" ID="btnupdatereg" runat="server"
                        Text="Godown Entry" Font-Size="15px" Font-Bold="false" ValidationGroup="A"
                        TabIndex="3" Width="260px" Height="60px" OnClick="btnupdatereg_Click"></asp:Button>
                </div>
            </div>
        </fieldset>
    </div>
    <script>
        $(document).ready(function () {
            $("[id$=txtdob]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>--%>
</asp:Content>

