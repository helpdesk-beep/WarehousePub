<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="Delete_Annaxure_A_Remark.aspx.cs" Inherits="Inspections_State_Delete_Annaxure_A_Remark" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <%--DropDown New--%>
   <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
   <link href="../../assets/New/css/select2.min.css" rel="stylesheet" />
   <script type="text/javascript" src="../../assets/New/js/select2.min.js"></script>
   <script type="text/javascript">
       $(function () {
           $("[id*=ddl_dist]").select2();
       });
   </script>
   <script type="text/javascript">
       $(function () {
           $("[id*=ddlbranch]").select2();
       });
   </script>
   <script type="text/javascript">
       $(function () {
           $("[id*=ddlemp]").select2();
       });
   </script>
   <script type="text/javascript">
       $(function () {
           $("[id*=ddlverification]").select2();
       });
   </script>
   <script type="text/javascript">
       $(function () {
           $("[id*=ddlquater]").select2();
       });
   </script>
   <script type="text/javascript">
       $(function () {
           $("[id*=ddlfinancialyear]").select2();
       });
   </script>
   <%--DropDown End--%>
   
 <%--Search New --%> 
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

    <div runat="server">

       <%--<%--<%-- <div class="pop" style="background-color: #FFFFCC0; min-height: 600PX; max-height: 500px; overflow: auto;">
            <%-- <div class="col-sm-12 col-md-12 col-xs-12">--%>
         <%--   <img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />--%>
        <div class="row">

            <div id="divNewInsp" runat="server" visible="true" style="width: 100%;">
                <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                    <tr>
                        <td style="height: 5px;"></td>
                    </tr>



                    <tr>
                        <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px; width: 100%" colspan="6">
                            <span style="color: #cb4e48; font-weight: bold; font-size: 17px">Delete Annaxure A Remark Fill By Inspection Offiser</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;" colspan="6"></td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="font-size: small;">Total Record :
                                            <asp:Label ID="lblTotalInsp" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="Label9" runat="server" Text="District : "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_dist" runat="server" AutoPostBack="true" Width="222px"
                                Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"
                                OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="Label2" runat="server" Text="Branch : "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true" Width="222px"
                                Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"
                                OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="Label1" runat="server" Text="Employee : "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlemp" runat="server" AutoPostBack="false" Width="222px"
                                Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                               <td style="text-align: right;">
                    <asp:Label ID="Label7" runat="server" Text="Verification Type : "></asp:Label>
                </td>
                               <td style="text-align: left;">
                    <asp:DropDownList ID="ddlverification" runat="server" AutoPostBack="false" Width="222px"
                                Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">General Inspection</asp:ListItem>
                        <asp:ListItem Value="2">Physical Verification</asp:ListItem>
                        <asp:ListItem Value="3">Both</asp:ListItem>

                    </asp:DropDownList>
                </td>
                         <td style="text-align: right;">
                    <asp:Label ID="Label3" runat="server" Text="Quarter : "></asp:Label>
                </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlquater" runat="server" AutoPostBack="false" Width="222px"
                                Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">1st Quarter</asp:ListItem>
                                <asp:ListItem Value="2">2nd Quarter</asp:ListItem>
                                <asp:ListItem Value="3">3rd Quarter</asp:ListItem>
                                <asp:ListItem Value="4">4th Quarter</asp:ListItem>
                                <asp:ListItem Value="5">Half Yearly Inspection</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align:right">
                    <asp:Label ID="Label4" runat="server" Text="Financial Year : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlfinancialyear" runat="server" AutoPostBack="false" Width="222px"
                        Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                        <asp:ListItem Value="0">--Select Financial Year--</asp:ListItem>
                    
                    </asp:DropDownList>
                </td>
                          </tr>
                    <tr>
                                <td colspan="6" align="center">
                                    <asp:Button class="button button6" ID="btn_show" runat="server" Text="Show"
                                        TabIndex="11" Width="222px" Height="30px" OnClick="btn_show_Click" ></asp:Button>
                                  
                                </td>
                            </tr>
                       <%--Search Textbox--%>
   <tr>
       <td colspan="10" align="left">
           <asp:TextBox ID="txtSearch" runat="server"
               placeholder="Search here..."
               Style="width: 380px; height: 36px;margin-left: 88px; padding: 0 15px; font-size: 15px; border: 1px solid #000; border-radius: 8px; outline: none; transition: all 0.25s ease; box-shadow: 0 2px 6px rgba(0,0,0,0.08);"
               onkeyup="filterGrid();" Visible="false" />

       </td>
   </tr>

   <%--Search Textbox End--%>
                    <tr>

                        <td colspan="6" valign="top" align="center">
                            <asp:GridView ID="Gridview_OfficerPreviousInsp" runat="server"
                                AutoGenerateColumns="False" Width="90%" Font-Size="10pt" Font-Bold="true" BackColor="White"
                                BorderColor="#008CBA" BorderStyle="Double"
                                BorderWidth="1px" CellPadding="2" CellSpacing="2" OnRowCommand="Gridview_OfficerPreviousInsp_RowCommand">

                                <Columns>
                                    <asp:TemplateField HeaderText="क्रमांक">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                            <asp:HiddenField ID="hdnempid" runat="server" Value='<%# Bind("EmployeeID") %>' />
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("BranchId") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BranchId" HeaderText="Branch ID" />
                                    <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />

                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="RemoveRow" CssClass="btneditstyle" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                                    Height="20px" Font-Size="10pt" />
                                <AlternatingRowStyle BackColor="#eeeeee" />
                            </asp:GridView>
                        </td>
                    </tr>

                    <%-------------End of Second Gride --------%>

                    <tr>
                        <td style="height: 5px;"></td>
                    </tr>

                    <tr>
                        <td style="height: 10px;"></td>
                    </tr>
                </table>
                <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

            </div>
        </div>
    </div>
</asp:Content>

