<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/JointVentureScheme/BrnchInspectionReport2023_24_Kharif_JVS.aspx.cs" Inherits="JointVentureScheme_BrnchInspectionReport2023_24_Kharif_JVS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>JVS Inspection Report</title>
    <meta charset="urf-8" />
    <meta name="viewport" content="width=device-width" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="css/main.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="js/menu.js"></script>
    <script type="text/javascript" src="js/slideshow.js"></script>
    <script type="text/javascript" src="js/cufon-yui.js"></script>
    <script type="text/javascript" src="js/Arial.font.js"></script>
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

        .style1 {
            height: 10px;
        }
    </style>
</head>
<body>
    <div id="bg" style="background-color: White">
        <div class="wrap">
            <img src="../images/CH.jpg" style="width: 100%" alt="" height="160" />
            <div>
                <form id="form1" runat="server">
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
                    <center>
                        <table>
                            <tr>
                                <td colspan="4" style="font-size: medium; width: 1000px;">
                                    <table style="width: 100%; height: 32px; font-size: medium;">
                                        <tr>
                                            <td style="background-color: #008CBA; width: 70PX;" align="center">
                                                <asp:LinkButton ID="LinkButton10" Text="Home" runat="server" PostBackUrl="~/JointVentureScheme/BranchHome.aspx" ForeColor="White"></asp:LinkButton>
                                            </td>
                                            <td colspan="2" style="background-color: #008CBA; font-size: medium; color: White; width: 100px" align="center">Welcome&nbsp;<asp:Label ID="lbluser" runat="server" ForeColor="White"></asp:Label></td>
                                            <td style="background-color: #008CBA; width: 70px;" align="center">
                                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="White"
                                                    OnClick="LinkButton1_Click">Log out</asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="border-color: #008CBA; border-style: solid; border-width: 2px; background-color: White" align="center">
                                    <p style="font-size: 14px; color: Black;">
                                        Inspection Report For Offered Godown(JVS 2023-24)
                                    </p>
                                </td>

                            </tr>
                            <tr>
                                <td align="center" style="height: 30px; font-weight: bold;">
                                    <p>
                                        Inspection Report &nbsp&nbsp&nbsp&nbsp
                                        <asp:DropDownList ID="ddl_session" runat="server" Width="150px" AutoPostBack="true" Height="25px" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="0">ALL</asp:ListItem>
                                            <asp:ListItem Value="FIT">FIT</asp:ListItem>
                                            <asp:ListItem Value="UNFIT">UNFIT</asp:ListItem>
                                        </asp:DropDownList>
                                    </p>
                                </td>
                            </tr>
                            <tr id="GAll" runat="server">
                                <td align="center" colspan="4">
                                    <div style="height: 270px; width: 950px; overflow: auto;" id="toexport" runat="server">
                                        <p>
                                            <asp:GridView ID="RegGrid" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                                Width="940px" DataKeyNames="Registration_Id" Font-Size="10pt" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="District_Name" HeaderText="District" SortExpression="District_Name" />
                                                    <asp:BoundField DataField="DepotName" HeaderText="DepotName" SortExpression="DepotName" />
                                                    <asp:BoundField DataField="Warehouse_Name" HeaderText="Warehouse Name" SortExpression="Warehouse_Name" />
                                                    <asp:BoundField DataField="Registration_Id" HeaderText="Registration ID" SortExpression="Registration_Id" />
                                                    <asp:BoundField DataField="Godown_No" HeaderText="Godown No" SortExpression="Godown_No" />
                                                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No." SortExpression="Mobile_No" />
                                                    <asp:BoundField DataField="OfferCapacity" HeaderText="Offered Capacity" SortExpression="OfferCapacity" />
                                                    <asp:BoundField DataField="InspectedCpt" HeaderText="Inspected Vacant Capacity" SortExpression="InspectedCpt" />
                                                    <asp:BoundField DataField="Offer_Scheme" HeaderText="Offer Scheme" SortExpression="G_Scheme" />
                                                    <asp:BoundField DataField="Insp_Offer_Scheme" HeaderText="Inspected Scheme" SortExpression="Insp_Offer_Scheme" />
                                                    <asp:BoundField DataField="Fit_Unfit" HeaderText="FIT/UNFIT" SortExpression="Fit_Unfit" />
                                                    <asp:BoundField DataField="Maintain_By" HeaderText="Maintain_By" SortExpression="Maintain_By" />
                                                    <asp:BoundField DataField="Select_Priority" HeaderText="Priority" SortExpression="Select_Priority" />
                                                    <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
                                                </Columns>
                                                <FooterStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Height="25px" Font-Size="10pt" />
                                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                    Height="30px" Font-Size="10pt" />
                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                            </asp:GridView>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Button ID="Button1" runat="server" Text="Export In Excel"
                                        OnClick="Button1_Click1" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4" style="font-weight: bold"></td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </center>
                </form>
            </div>
            <div style="background-image: url('../images/div_bg.png')">
                <table style="width: 100%">
                    <tr>
                        <td style="height: 20px;" colspan="5">
                            <img id="Img2" src="../Images/line.png" height="30px" width="100%" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" align="center">
                            <a href="http://www.mp.nic.in/">
                                <img src="../Images/NIC-logo.png" width="200px" height="50px" alt="" />
                            </a>
                        </td>
                        <td style="width: 1%" align="center">
                            <img id="Img1" src="../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                        </td>
                        <td style="color: Navy; font-size: 8pt; width: 58%;" align="center">
                            <b>© 2015 &nbsp;National Informatics Centre.All Rights Reserved
                            <br />
                                Developed By : National Informatics Centre
                            <br />
                                Madhya Pradesh, Ministry of Communications and Information Technology</b>
                        </td>
                        <td style="width: 1%" align="center">
                            <img id="Logo" src="/../Images/Linevertical.png" style="width: 20px; height: 50px" alt="" />
                        </td>
                        <td style="width: 20%" align="center">
                            <table>
                                <tr>
                                    <td><a href="http://india.gov.in/">
                                        <img src="../Images/natindialogo.png" width="100px" height="50px" alt="" />
                                    </a>
                                    </td>
                                    <td><a href="http://www.digitalindia.gov.in/">
                                        <img src="../Images/di.png" width="100px" height="50px" alt="" />
                                    </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</body>
</html>
