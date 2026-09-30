<%@ Page Title="" Language="C#" MasterPageFile="~/Special_PV/Special_PV.master" AutoEventWireup="true" CodeFile="Godown_wise_Stock_Details_For_Special_PV.aspx.cs" Inherits="Special_PV_Godown_wise_Stock_Details_For_Special_PV" %>

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
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= GD_StackBal.ClientID %>');
            var windowUrl = 'about:blank';

            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();
            location.reload();
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <fieldset>
            <legend>Godown Stock Details</legend>
            <div class="row">
                <div class="col-md-2" style="margin-top: 10PX">
                    <label>Godown :</label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddl_gdwn" runat="server" AutoPostBack="false" class="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button class="button button2" ID="btnshow" runat="server" Text="Show"
                        TabIndex="11" CssClass="btn btn-warning" OnClick="btnshow_Click"></asp:Button>
                </div>
            </div>
            <div class="row" id="divshow" runat="server" visible="false">
                <asp:GridView runat="server" ID="GD_StackBal" OnRowCreated="GD_StackBal_RowCreated" OnRowCommand="GD_StackBal_RowCommand"
                    OnRowDataBound="GD_StackBal_RowDataBound"
                    AutoGenerateColumns="false" CssClass="table table-bordered table-hover" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" ShowFooter="true">
                    <Columns>
                        <asp:TemplateField HeaderText="क्रमांक">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                                <asp:HiddenField runat="server" ID="hdnDepositer_ID" Value='<%# Eval("Depositer_ID") %>' />
                                <asp:HiddenField runat="server" ID="hdnCommodity_ID" Value='<%# Eval("Commodity_ID") %>' />
                                <asp:HiddenField runat="server" ID="hdncropyear" Value='<%# Eval("Crop_Year") %>' />
                                <asp:HiddenField runat="server" ID="hdnFinalsubmit" Value='<%# Eval("Final_Bubmit") %>' />
                                <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="1%" />
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
            <div class="row" id="divremark" runat="server" visible="false" style="text-align: center; margin-top: 10px">
                <div class="col-md-2" style="margin-top: 10px">
                    <lable>Remark</lable>
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtremark" Visible="false" runat="server"  TextMode="MultiLine" AutoComplete="off" placeholder="Enter Remark"></asp:TextBox>
                </div>
            </div>
            <div class="row" id="divbtn" runat="server" visible="false" style="text-align: center; margin-top: 10PX">
                <div class="col-md-4"></div>
                <div class="col-md-2">
                    <asp:Button CssClass="btn btn-warning" ID="btn_saveInspDate" runat="server" Text="Verify" Visible="true" OnClick="btn_saveInspDate_Click"></asp:Button>
                </div>
                <div class="col-md-1">
                    <asp:Button class="btn btn-danger" ID="btnclear" runat="server" Text="Clear All" Visible="true"></asp:Button>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

