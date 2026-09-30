<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="Insert_Gadna_Patrak_Not_Show_In_Summary.aspx.cs" Inherits="Inspections_RO_Insert_Gadna_Patrak_Not_Show_In_Summary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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

    <div class="pop" style="background-color: #FFFFCC0; min-height:600PX;max-height: 500px; overflow: auto;">
               <%-- <div class="col-sm-12 col-md-12 col-xs-12">--%>
                <img visible="true" runat="server" src="~/images/close-icon.png" alt="quit" class="x" id="x" />


                    <div id="divNewInsp" runat="server" visible="true" style="width: 100%;">
                        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                            <tr>
                                <td style="height: 5px;"></td>
                            </tr>
                            <tr>
                                <td align="center" style="border: #E6C79D; border-style: solid; border-width: 2px; width: 100%" colspan="6">
                                    <span style="color: #cb4e48; font-weight: bold; font-size: 17px">यह से ऐसे निरिक्षण अधिकारी जिनके गड़ना पत्रक नहीं दिखाई दे रहे हैं ,उसको देखने के लिए यहाँ से शाखा एवं निरिक्षण अधिकारी का नाम चुने, और "Insert Gadna Patrak Data" बटन पर क्लीक करे क्लिक करने के बाद चेक करे गड़ना पत्रक दिखने लगेगा यदि गड़ना पत्रक की जानकारी को निरिक्षण अधिकारी के द्वारा सत्यापित किया हैं तभी गड़ना पत्रक दिखाई देगा  |</span>
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
                        <asp:Label ID="Label1" runat="server" Text="Branch : "></asp:Label>
                        </td>
                         <td>
                                <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true" Width="222px"
                                     Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"
                                    OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                        <td>
                                 
                            <asp:Label ID="Label2" runat="server" Text="Employees : "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlemp" runat="server" AutoPostBack="false" Width="222px"
                                Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" >
                            </asp:DropDownList>
                        </td>
                            </tr>
                            
                            <tr>
                                <td colspan="6" align="center">
                                    <asp:Button class="button button6" ID="btn_show" runat="server" Text="Insert Gadna Patrak Data"
                                        TabIndex="11" Width="222px" Height="30px" OnClick="btn_show_Click" ></asp:Button>
                                    
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

