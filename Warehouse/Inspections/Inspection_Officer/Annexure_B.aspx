<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="Annexure_B.aspx.cs" Inherits="Inspections_Officer_Godown_wise_Stock_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        window.history.forward();

        function noBack() { window.history.forward(); }
    </script>
    <script type="text/javascript">
        Cufon.replace('h1,h2,h3,h4,h5,#menu,#copy,.blog-date');
    </script>
    <style type="text/css">
        ul.svertical {
            width: 220px; /* width of menu */
            overflow: auto;
            background: #f4f4f4; /* background of menu */
            margin: 0;
            padding: 0;
            padding-top: 7px; /* top padding */
            list-style-type: none;
        }

            ul.svertical li {
                text-align: right; /* right align menu links */
            }

                ul.svertical li a {
                    position: relative;
                    display: inline-block;
                    text-indent: 5px;
                    overflow: hidden;
                    background: rgb(1, 138, 180); /* initial background color of links */
                    font: bold 16px Germand;
                    text-decoration: none;
                    padding: 5px;
                    margin-bottom: 5px; /* spacing between links */
                    color: White;
                    -moz-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8); /* inner right shadow added to each link */
                    -webkit-box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    box-shadow: inset -7px 0 5px rgba(114,114,114, 0.8);
                    -moz-transition: all 0.2s ease-in-out; /* CSS3 transition of hover properties */
                    -webkit-transition: all 0.2s ease-in-out;
                    -o-transition: all 0.2s ease-in-out;
                    -ms-transition: all 0.2s ease-in-out;
                    transition: all 0.2s ease-in-out;
                }

                    ul.svertical li a:hover {
                        padding-right: 30px; /* add right padding to expand link horizontally to the left */
                        color: Black;
                        background: rgb(153,249,75);
                        -moz-box-shadow: inset -3px 0 2px rgba(114,114,114, 0.8); /* contract inner right shadow */
                        -webkit-box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                        box-shadow: inset -3px 0 5px rgba(114,114,114, 0.8);
                    }

                    ul.svertical li a:before { /* CSS generated content: slanted right edge */
                        content: "";
                        position: absolute;
                        left: 0;
                        top: 0;
                        border-style: solid;
                        border-width: 70px 0 0 20px; /* Play around with 1st and 4th value to change slant degree */
                        border-color: transparent transparent transparent #f4f4f4; /* change black to match the background color of the menu UL */
                    }

        /*print*/


        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }

        .page {
            width: 21cm;
            min-height: 29.7cm;
            padding: 2cm;
            margin: 1cm auto;
            border: 1px #D3D3D3 solid;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

        .subpage {
            padding: 1cm;
            border: 5px red solid;
            height: 237mm;
            outline: 2cm #FFEAEA solid;
        }

        @page {
            size: A4;
            margin: 0;
            font-size: smaller;
        }

        @media print {
            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
                font-size: smaller;
            }
        }

        @media print {
            html, body {
                width: 210mm;
                height: 297mm;
                font-size: smaller;
            }
            /* ... the rest of the rules ... */
        }
        /*td{font-size:smaller;}*/
        page[size="A4"] {
            background: white;
            width: 21cm;
            height: 29.7cm;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
        }

        @media print {
            body, page[size="A4"] {
                margin: 0;
                box-shadow: 0;
                font-size: smaller;
            }
        }

        .style7 {
            height: 15px;
        }

        .style8 {
            height: 20px;
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

    <div runat="server">

        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">

            <tr>

                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="6">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Godown Stock Details</span>
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
                    <asp:Label ID="Label1" runat="server" Text="Financial Year : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlfinancialyear" runat="server" AutoPostBack="false" Width="222px"
                        Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                        <asp:ListItem Value="0">--Select Financial Year--</asp:ListItem>

                    </asp:DropDownList>
                </td>
                <%-- <td align="right">Crop Year :
                                          
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="false"
                        Height="30px" CssClass="form-control">
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
                </td>--%>
            </tr>
        </table>
        <div div="div" runat="server" style="text-align: center;">

            <asp:Button class="button button2" ID="btnshow" runat="server" Text="Show"
                TabIndex="11" CssClass="btn btn-warning" OnClick="btnshow_Click"></asp:Button>

        </div>
        <div id="divshow" runat="server" visible="false">
            <asp:GridView runat="server" ID="GD_StackBal" OnRowCreated="GD_StackBal_RowCreated" OnRowCommand="GD_StackBal_RowCommand" OnRowDataBound="GD_StackBal_RowDataBound"
                AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" ShowFooter="true">

                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                            <asp:HiddenField runat="server" ID="hdnDepositer_ID" Value='<%# Eval("DepositorID") %>' />
                            <asp:HiddenField runat="server" ID="hdnCommodity_ID" Value='<%# Eval("Commodity_Id") %>' />
                            <asp:HiddenField runat="server" ID="hdncropyear" Value='<%# Eval("CropYear") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="1%" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stack ID">
                        <ItemTemplate>
                            <asp:Label ID="lblstack_id" runat="server" Text='<%# Eval("Stack_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stack Name">
                        <ItemTemplate>
                            <asp:Label ID="lblStack_Name" runat="server" Text='<%# Eval("Stack_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Depositer Name">
                        <ItemTemplate>
                            <asp:Label ID="lblDepositor_Name" runat="server" Text='<%# Eval("Depositor_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Commodity Name">
                        <ItemTemplate>
                            <asp:Label ID="lblcommodity" runat="server" Text='<%# Eval("Commodity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Crop Year">
                        <ItemTemplate>
                            <asp:Label ID="lblCrop_Year" runat="server" Text='<%# Eval("CropYear") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Available Bags">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal_Bags" runat="server" Text='<%# Eval("Total_Bags") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Available Bags as Per PV">
                        <ItemTemplate>
                            <asp:TextBox ID="lblPV_Bags" runat="server" onkeypress="return isNumberKey(event)" Text='<%# Eval("PV_Bags") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Classification">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlclassifi" runat="server">
                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                <asp:ListItem Value="C" Selected="True">C</asp:ListItem>
                                <asp:ListItem Value="F">F</asp:ListItem>
                                <asp:ListItem Value="H">H</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stack Open/Covered (Fumigated/CAP)" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStackType" runat="server" Width="80px">
                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                <asp:ListItem Value="Open">Open</asp:ListItem>
                                <asp:ListItem Value="Covered">Covered</asp:ListItem>

                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlCategory" runat="server">
                                <asp:ListItem Value="A" Selected="True">A</asp:ListItem>
                                <asp:ListItem Value="B">B</asp:ListItem>
                                <asp:ListItem Value="C">C</asp:ListItem>
                                <asp:ListItem Value="D">D</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Moisture" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <asp:TextBox ID="txtmoisture" runat="server" Width="120px" align="Center" Height="25px" Text='<%# Eval("Moisture") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Last Fumigation Date">
                        <ItemTemplate>
                            <asp:TextBox ID="Gtxtdepositformdate" Width="100px"
                                runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="Gtxtdepositformdate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spillage bags">
                        <ItemTemplate>
                            <asp:TextBox ID="lblSpillage_bag" runat="server" onkeypress="return isNumberKey(event)" Text='<%# Eval("Spillage_bag") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="120px">
                        <ItemTemplate>
                            <asp:TextBox ID="GtxtRemark" runat="server" Width="120px" align="Center" Height="25px" TextMode="MultiLine"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#E6C79D" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center" />
                <AlternatingRowStyle BackColor="#eeeeee" />
            </asp:GridView>
            <div id="divbtn" runat="server" visible="false" style="text-align: center;">
                <asp:Button CssClass="btn btn-warning" ID="btn_saveInspDate" runat="server" Text="Submit" Visible="true" OnClick="btn_saveInspDate_Click"
                    ValidationGroup="A"></asp:Button>
                &nbsp&nbsp&nbsp&nbsp
                                            <asp:Button class="button button6" ID="btnclear" runat="server" Text="Clear All" Visible="true"
                                                TabIndex="12" Width="150px" Height="30px"></asp:Button>
            </div>
            <div>
                <p style="color: Red;">
                    नोट :-
                    <br />
                    1.गोदाम में यदि फिजिकल स्कंध भण्डारित है और WHR एवम्‌ स्टेक भी बनी है परन्तु स्टेक ऑनलाइन प्रदर्शित नहि हो रह हे तो सम्पर्क करें ।
                    <br />
                    2. यदि ऑनलाइन ओर फिजिकल स्टेक बैलेंस में अंतर हे तो रिमार्क में विवरण अवस्य डालें ।<br />
                    3. यदि फ़्युमिगेसन नहीं किया गया है तो क्रिप्या फ़्युमिगेसन दिनांक ब्लेंक छोड़ें ।
                </p>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnBranchID" Value="0" runat="server" />
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

