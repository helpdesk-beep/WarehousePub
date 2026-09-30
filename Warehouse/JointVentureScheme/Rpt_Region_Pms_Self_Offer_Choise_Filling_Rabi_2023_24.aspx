<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Region_Pms_Self_Offer_Choise_Filling_Rabi_2023_24.aspx.cs" Inherits="JointVentureScheme_Rpt_Region_Pms_Self_Offer_Choise_Filling_Rabi_2023_24" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>JVS Report</title>
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

        .style3 {
            height: 22px;
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
            border: 2px solid #E47D21;
        }

            .button6:hover {
                background-color: #E47D21;
                color: white;
            }
    </style>
</head>
<body>

    <div id="bg">
        <div class="wrap">
            <img src="../images/CH.jpg" style="width: 100%" alt="" height="150" />

            <div>
                <form id="form1" runat="server">
                    <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
                    <center>
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="4" style="font-size: medium;">
                                    <table style="width: 100%; height: 32px; font-size: medium;">
                                        <tr>
                                            <td style="background-color: #008CBA; width: 70PX;" align="center">
                                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" ForeColor="White">Home</asp:LinkButton>
                                            </td>
                                            <td colspan="2" style="background-color: #008CBA; font-size: medium; color: White; width: 100px" align="center">&nbsp;</td>
                                            <td style="background-color: #008CBA; width: 70px;" align="center">
                                                <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="White"
                                                    OnClick="LinkButton2_Click">Log out</asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
             

                            <tr id="Tr4" runat="server">
                               
                            </tr>
                           

                          
  
                  </tr>
                         
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                           


                          
                            <tr>
                                <td colspan="12" style="border-color: #008CBA; height: 20px; border-style: solid; border-width: 2px; background-color: White; font-size: 14px; color: Black;" align="center">Region Wise Offered Capacity and Choise Filling
                                </td>
                            </tr>
                            <tr>
                                
                                <td colspan="3" style="border-color: #008CBA; height: 20px; background-color: White; font-size: 14px; color: Black;" align="center">
                                    <asp:DropDownList ID="ddlChoise" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChoise_SelectedIndexChanged">
                                        <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="PMS" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="SELF" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr visible="false" id="GPhase1" runat="server">
                                <td align="center" colspan="4">
                                    <div style="height: 200px; width: 900px; overflow: auto;" id="toexportDist" runat="server">
                                        <p>
                                            <asp:GridView ID="GridDist" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                                Width="50%" DataKeyNames="District_Name" Font-Size="10pt" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="District_Name" HeaderText="District"/>--%>
                                                    <%--<asp:BoundField Visible="True" DataField="Regionnm" HeaderText="Region" SortExpression="Regionnm" />--%>
                                                    <asp:BoundField Visible="True" DataField="District_Name" HeaderText="District Name" SortExpression="District_Name" />
                                                    <asp:BoundField Visible="True" DataField="DepotName" HeaderText="Branch Name" SortExpression="DepotName" />
                                                    <asp:BoundField Visible="True" DataField="Warehouse_Name" HeaderText="Warehouse Name" SortExpression="Warehouse_Name" />
                                                    <asp:BoundField Visible="True" DataField="Registration_Id" HeaderText="Registration No" SortExpression="Registration_Id" />
                                                    <asp:BoundField DataField="Offer_Capacity" HeaderText="Offered Capacity" SortExpression="Offer_Capacity" />
                                                    <asp:BoundField DataField="OfferedDate" HeaderText="Offered Date" SortExpression="OfferedDate" />
                                                    <asp:BoundField DataField="Choise_Filling" HeaderText="Choise" SortExpression="Choise_Filling" />
                                                </Columns>
                                                <FooterStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" Height="25px" Font-Size="10pt" />
                                                <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                    Height="30px" Font-Size="10pt"/>
                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                            </asp:GridView>

                                        </p>
                                    </div>
                                </td>

                            </tr>

                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="Button1" runat="server" Text="Export In Excel" class="button button2" Width="120px" Height="28px"
                                        OnClick="Button1_Click1" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4"></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4" style="font-weight: bold">
                                    <p>
                                        <asp:Label ID="Label2" runat="server" Text="Total No. Of Registered Warehouse : " Visible="false"></asp:Label>&nbsp
                         <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
                                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                         <asp:Label ID="Label4" runat="server" Visible="false" Text="Total Offered Capacity (In M.T) :"></asp:Label>&nbsp
                         <asp:Label ID="Label5" runat="server" Visible="false"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                            <%--<tr id="Tr1" runat="server">
                      <td colspan="4" >
                          <p style="font-size: medium; color: #008080;">  &nbsp;&nbsp;&nbsp;<asp:LinkButton 
                                  ID="LinkButton5" Text="Phase-1 Offer Capacity Detail Report" runat="server" 
                                  PostBackUrl="~/JointVentureScheme/JVSStateReport.aspx" 
                                   Font-Underline="True"></asp:LinkButton></p>
                        
                      </td>

                  </tr>
                
            <tr id="Tr2" runat="server">
                      <td colspan="4" >
                          <p style="font-size: medium; color: #008080;">  &nbsp;&nbsp;&nbsp;<asp:LinkButton 
                                  ID="LinkButton3" Text="Phase-2 Offer Capacity Detail Report" runat="server" 
                                  PostBackUrl="~/JointVentureScheme/JVSSecondOfferReport.aspx" 
                                   Font-Underline="True"></asp:LinkButton></p>
                        
                      </td>

                  </tr>
                
            <tr id="Tr3" runat="server">
                      <td colspan="4" >
                          <p style="font-size: medium; color: #008080;">  &nbsp;&nbsp;&nbsp;<asp:LinkButton 
                                  ID="LinkButton4" Text="Phase-3 Offer Capacity Detail Report" runat="server" 
                                  PostBackUrl="~/JointVentureScheme/JVSThirdOfferReport.aspx" 
                                   Font-Underline="True"></asp:LinkButton></p>
                        
                      </td>

                  </tr> --%>
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
                            <b>© 2018 &nbsp;National Informatics Centre.All Rights Reserved
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
