<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_BO.master" AutoEventWireup="true" CodeFile="InspOfficer_FillOverall_PVInsp.aspx.cs" Inherits="Inspections_BO_InspOfficer_FillOverall_PVInsp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
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
    <script src="http://code.jquery.com/jquery-1.11.1.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=ddlA.ClientID %>').change(function () {
                //Get DropDownList selected value
                var selectedValue = $('#<%=ddlA.ClientID %>').val();
                //Hide Controls
                if (selectedValue == 1) {
                    $('#<%=showtxtA.ClientID %>').hide();
                }
                    //Show Controls
                else {
                    $('#<%=showtxtA.ClientID %>').show();
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=ddlB.ClientID %>').change(function () {
                //Get DropDownList selected value
                var selectedValue = $('#<%=ddlB.ClientID %>').val();
                //Hide Controls
                if (selectedValue == 1) {
                    $('#<%=showtxtB.ClientID %>').hide();
                }
                    //Show Controls
                else {
                    $('#<%=showtxtB.ClientID %>').show();
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=ddlC.ClientID %>').change(function () {
                //Get DropDownList selected value
                var selectedValue = $('#<%=ddlC.ClientID %>').val();
                //Hide Controls
                if (selectedValue == 1) {
                    $('#<%=showtxtC.ClientID %>').hide();
                }
                    //Show Controls
                else {
                    $('#<%=showtxtC.ClientID %>').show();
                }
            });
        });
    </script>
    <div style="background-color: #FDFAF7; width: 100%;">
        <table align="center" style="width: 100%;">

            <tr>
                <td align="center" colspan="3">
                    <table border="1" width="70%">
                        <tbody>
                            <th>Inspection ID </th>
                            <th>Inspection Type </th>
                            <th>Inspection Period</th>
                            <th>Branch </th>
                            <th>Inspection Date </th>
                        </tbody>
                        <tr align="center">
                            <td>
                                <asp:Label ID="lblinspid" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblinsptype" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInspPeriod" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblbranch" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtinspectiondate" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>
        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label33" runat="server" Text="निरीक्षण अधिकारी का नाम और पद  : "></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtofficername" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label59" runat="server" Text="भंडार गृह का लाइसेंस नम्बर  : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtlincenceno" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label61" runat="server" Text="वैधता दिनांक : "></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtexdate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label60" runat="server" Text="निरक्षण अवधि दिनक : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtinspexdatefrom" runat="server"></asp:TextBox>

                </td>
                <td>
                    <asp:Label ID="Label62" runat="server" Text="तक : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtinspexdateto" runat="server"></asp:TextBox>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Date" ForeColor="Red"
                        runat="server" ControlToValidate="txtinspexdatefrom" ControlToCompare="txtinspexdateto"
                        Operator="LessThan" Type="Date" ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                    <br />
                </td>

                <td>
                    <asp:Label ID="Label66" runat="server" Text="पिछले निरिक्षण का दिनांक : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtpriciusinspdate" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label67" runat="server" Text="पिछले निरीक्षण अधिकारी का नाम : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtpriveusinspofficername" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label80" runat="server" Text="पिछले निरीक्षण अधिकारी का पद : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_Desig" runat="server" AutoPostBack="false" Width="222px"
                        Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                        <asp:ListItem Selected="True">--Select--</asp:ListItem>
                        <asp:ListItem Value="AQC">AGM</asp:ListItem>
                        <asp:ListItem Value="AQC">AQC</asp:ListItem>
                        <asp:ListItem Value="AQC">AQC(C)</asp:ListItem>
                        <asp:ListItem Value="AQC">QC</asp:ListItem>
                        <asp:ListItem Value="AQC">Manager(QC)</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>

                <td>
                    <asp:Label ID="Label63" runat="server" Text="स्कंध जमा करने के आवेदन क्रमांक  : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtskandhsno" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label64" runat="server" Text="दिनाँक से  : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtskandhdatefrom" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label65" runat="server" Text="क्रमांक : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtskandhsnoto" runat="server"></asp:TextBox>

                </td>
                <td colspan="2">
                    <asp:Label ID="Label68" runat="server" Text="दिनाँक तक: "></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtskandhdateto" runat="server"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label69" runat="server" Text="स्कंध भुगतान पत्र क्र.  : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtskandhpaymentleetersno" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label70" runat="server" Text="दिनाँक से  : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtskandhletterdatefrom" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label71" runat="server" Text="क्रमांक : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtskandhpaymentleetersnoto" runat="server"></asp:TextBox>

                </td>
                <td>
                    <asp:Label ID="Label72" runat="server" Text="दिनाँक तक: "></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtskandhletterdateto" runat="server"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label73" runat="server" Text="अंतिम वेयर हाउस रसीद क्र. : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtlastwarehousereciept" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label74" runat="server" Text="दिनाँक : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtwarehouserecieptdate" runat="server" CssClass="form-control" Height="25px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label75" runat="server" Text="बाह्यय कीट नाशक विस्तार सेवा योजना (बोरे): "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtschemebore" runat="server"></asp:TextBox>

                </td>
                <td>
                    <asp:Label ID="Label76" runat="server" Text="प्राप्त राशि: "></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtamount" runat="server"></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label77" runat="server" Text="अप्रैल से निरीक्षण तक लाभार्जन की स्थिति : "></asp:Label>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtaprilprofitstatus" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label78" runat="server" Text="वेयरहाउस रसीद रहने का विवरण: "></asp:Label>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtwarerecieptdetaisl" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td colspan="10" align="center">
                    <asp:Button class="button button2" ID="btnsaveprofile" runat="server" Text="Save"
                        TabIndex="11" CssClass="btn btn-info" ValidationGroup="Date" OnClick="btnsaveprofile_Click"></asp:Button>
                    <asp:Label ID="Label79" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-size: 13px;" colspan="10">
                    <p style="color: Red;">
                        नोट :-  1.  सभी डेटा क्रम सें प्रविस्ट करे ।
                    </p>
                </td>
            </tr>
        </table>
        <%-----------------------end of First Gride----------------%>

        <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">

            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">1. शाखा की क्षमता एवं उपयोगिता की समीक्षा (क्षमता Qntl. में) :-</span>
                </td>
            </tr>

            <tr>
                <td align="center" colspan="4">
                    <table border="1" width="82%">
                        <tbody style="font-size: 12px; color: Black;">
                            <th>क्षमता का प्रकार</th>
                            <th>गोदामों की संख्या</th>
                            <th>भंडारण क्षमता</th>
                            <th>भंडारित बोरा</th>
                            <th>भंडारित वजन</th>
                            <th>उपयोगिता का %</th>
                            <th>रिमार्क</th>
                        </tbody>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label1" runat="server" Text="स्वनिर्मित + स्व0पीईजी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Ogdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_OgdwnCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_OABags" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_OAQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_OUtil" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_ORemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>

                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label7" runat="server" Text="निजी पीईजी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Pgdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_PgdwnCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_PABags" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_PAQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_PUtil" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_PRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label6" runat="server" Text="जेव्हीएस"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Jgdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_JgdwnCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_JABags" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_JAQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_JUtil" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_JRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label2" runat="server" Text="किराया/अधिग्रहण"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Hgdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_HgdwnCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_HABags" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_HAQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_HUtil" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_HRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label3" runat="server" Text="केप"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Cgdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_CgdwnCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_CABags" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_CAQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_CUtil" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_CRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label4" runat="server" Text="अन्य"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Othgdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_OthgdwnCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_OthABags" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_OthAQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_OthUtil" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_OthRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="center">
                                <asp:Button class="button button2" ID="btn_addnewoff" runat="server" Text="Save"
                                    TabIndex="11" CssClass="btn btn-info" OnClick="btn_addnewoff_Click"></asp:Button>
                                <asp:Label ID="lblcptmessage" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td style="font-size: 13px;">
                    <p style="color: Red;">
                        नोट :-  1.स्वनिर्मित भण्डारण क्षमता की उपयोगिता किराये/JVS से कम होने पर वस्तुस्थिति पृथक से कारण सहित उल्लेखित करें ।
                    </p>
                </td>
            </tr>
            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">2. शाखा पर ऑनलाईन प्रविष्टियों का विवरण (क्षमता M.T. में)-</span>
                </td>
            </tr>

            <tr>
                <td align="center" colspan="4">
                    <table border="1" width="93%">
                        <tbody style="font-size: 12px; color: Black;">
                            <th>क्षमता का प्रकार</th>
                            <th colspan="2">रजिस्ट्रेशन</th>
                            <th colspan="2">ऑफर्ड</th>
                            <th colspan="2">अनुबंधित</th>
                            <th>रिमार्क</th>
                        </tbody>
                        <tbody style="font-size: 12px; color: Black;">
                            <th></th>
                            <th>S.no.</th>
                            <th>गोदामों की संख्या</th>
                            <th>क्षमता</th>
                            <th>गोदामों की संख्या</th>
                            <th>क्षमता</th>
                            <th>गोदामों की संख्या</th>
                            <th>क्षमता</th>
                            <th></th>
                        </tbody>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label8" runat="server" Text="स्वनिर्मित + स्व0पीईजी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegOwnNoOfGdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegOwnCapacity" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox45" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox46" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox47" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox85" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegOwnRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr align="center">
                                    <td style="font-size:12px; color:Black;">
                                     <asp:Label ID="Label9" runat="server" Text="स्व0पीईजी"></asp:Label>
                                    </td>
                                    <td>
                                     <asp:TextBox ID="TextBox49" runat="server" Width="100px" Height="25px" align="Center" >0</asp:TextBox>
                                    </td>  
                                    <td>
                                     <asp:TextBox ID="TextBox50" runat="server" Width="100px" Height="25px" align="Center" >0</asp:TextBox>
                                    </td>                                    
                                    <td>
                                     <asp:TextBox ID="TextBox51" runat="server" Width="100px" Height="25px" align="Center" >0</asp:TextBox>
                                    </td>
                                    <td>
                                     <asp:TextBox ID="TextBox52" runat="server" Width="100px" Height="25px" align="Center" >0</asp:TextBox>
                                    </td>
                                    <td>
                                     <asp:TextBox ID="TextBox53" runat="server" Width="100px" Height="25px" align="Center" >0</asp:TextBox>
                                    </td>
                                    <td>
                                     <asp:TextBox ID="TextBox86" runat="server" Width="100px" Height="25px" align="Center" >0</asp:TextBox>
                                    </td>                                    
                                    <td>
                                     <asp:TextBox ID="TextBox54" runat="server" Width="150px" Height="25px" align="Center" 
                                            TextMode="MultiLine" ></asp:TextBox>
                                    </td>                                                                                                                                                                                   
                            </tr>--%>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label10" runat="server" Text="निजी पीईजी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegPEGNoOfGdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegPEGCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegPEGNoOfOfferedGdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegPEGOfferCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegPEGAgreeNoofgdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegPEGAgreeCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegPEGRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label11" runat="server" Text="जेव्हीएस"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegJvsNoOfGdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegJVSCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegJVSNoOfOfferedGdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegJVSOfferCPT" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegJVSAgreeNoOfGdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegJVSAgreeCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegJVSRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label12" runat="server" Text="किराया/अधिग्रहण"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegHNoOfGdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegHCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox69" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox70" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegHAgreeNoOf" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegHAgreeCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegHAgreeRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label13" runat="server" Text="केप"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegCAPNoOdGdwnReg" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegCAPCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox75" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox76" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegAgreeNoOfGdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegAgreeCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegCAPRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label14" runat="server" Text="अन्य"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegOthNoOfCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegOthRegCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegOthOfrNoOfCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegOthOfrCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegOthNoOfGdwn" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegOthAgreeCpt" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_RegOthRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">
                                <asp:Button class="button button2" ID="Button1" runat="server" Text="Save"
                                    TabIndex="11" CssClass="btn btn-info" OnClick="Button1_Click"></asp:Button>
                                <asp:Label ID="lblOnlineRegCptMessage" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="font-size: 13px;">
                    <p style="color: Red;">
                        नोट :-  1. जिन गोदामों का स्थानीय स्तर पर ऑनलाईन पंजीयन नहीं हुआ है उनका विवरण निम्नानुसार उल्लेखित करें एवं यदि कोई शासकीय गोदाम का पंजीयन शेष है तो उसका निरीक्षण के 
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; दौरान पंजीयन/संशोधन करावें ।
                    </p>
                </td>
            </tr>
            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">3. शाखा पर उपलब्ध कीटनाशक औषधियों का विवरण :-</span>
                </td>
            </tr>


            <tr>
                <td align="center" colspan="4">
                    <table border="1" width="80%">
                        <tbody style="font-size: 12px; color: Black;">
                            <th>कीटनाशक औषधि का नाम</th>
                            <th>मात्रा</th>
                            <th>भौतिक सत्यापन मात्रा</th>
                            <th>कीटनाषाक की एक्सपायरी/अनुपयोगी मात्रा</th>
                            <th>वर्ष हेतु अतिरिक्त आवश्यकता</th>
                            <th>रिमार्क</th>
                        </tbody>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label15" runat="server" Text="एल्यु0फास्फाईड"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlumiQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlumi_PVQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlum_Exp" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlum_Req" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlum_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label16" runat="server" Text="मेलाथियान"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMeth_Qty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMeth_PVQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMeth_Exp" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMeth_Req" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMeth_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label17" runat="server" Text="डी0डी0व्ही0पी0"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDDVP_Qty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDDVP_PVQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDDVP_Exp" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDDVP_Req" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDDVP_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label18" runat="server" Text="डेल्टामेथ्रिन"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDelta_Qty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDelta_PVQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDelta_Exp" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDelta_Req" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDelta_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label19" runat="server" Text="अन्य"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOther_Qty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOther_PVQty" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOther_Exp" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOther_Req" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOther_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="center">
                                <asp:Button class="button button2" ID="Button3" runat="server" Text="Save"
                                    TabIndex="11" CssClass="btn btn-info" OnClick="Button3_Click"></asp:Button>
                                <asp:Label ID="lblKitnasakmesage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td style="font-size: 13px;">
                    <p style="color: Red;">
                        नोट :-  1. शाखा पर भण्डारित स्कंध के अनुपात में कीटनाशक औषधियों की उपलब्धता की समीक्षा करें एवं यदि कीटनाशक औषधियां कम हैं तो विवरण उल्लेखित करें ।
                    </p>
                </td>
            </tr>

            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">4. केप के निरीक्षण का विवरण :-</span>
                </td>
            </tr>


            <tr>
                <td align="center" colspan="4">
                    <table border="1" width="80%">
                        <tbody style="font-size: 12px; color: Black;">
                            <th>विवरण</th>
                            <th>उपयोग की संख्या</th>
                            <th>शेष उपयोगी संख्या/मात्रा</th>
                            <th>शेष अनुपयोगी संख्या/मात्रा</th>
                            <th>अतिरिक्त आवश्यकता</th>
                            <th>रिमार्क</th>
                        </tbody>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label20" runat="server" Text="केप स्टेकों की संख्या"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_Upyog" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_BalUpyog" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_UnUpyog" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_MoreReq" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_RemarkS" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label21" runat="server" Text="केप कवर "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_NoOfCover" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_BalCover" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_UnUsedCover" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_ReqCover" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_RemarkCover" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label22" runat="server" Text="केप कवर टाप"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_NoOfTop" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_BalUsedTop" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_UnusedTOP" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_ReqTop" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_RemarkTop" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label23" runat="server" Text="रस्सी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_NoOfRasi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_BalRassi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_UnUsedRassi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_ReqRassi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_RemarkRassi" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label24" runat="server" Text="हुक"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_NoOfHuk" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_BalHuk" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_UnUsedHuk" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_ReqHuk" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAP_RemarkHuk" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>

                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label5" runat="server" Text="अग्निशामक व्यवस्ता"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_AgniUse" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Agniseshmatra" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_agniunusedmatra" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_agnimorereq" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_agniremark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>

                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label9" runat="server" Text="अग्निशामक व्यवस्ता"></asp:Label>
                            </td>
                            <td colspan="4" align="center">

                                <asp:TextBox ID="txtSecurity" runat="server" Width="450px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="center">
                                <asp:Button class="button button2" ID="Button5" runat="server" Text="Save"
                                    TabIndex="11" CssClass="btn btn-info" OnClick="Button5_Click"></asp:Button>
                                <asp:Label ID="lblcapmessage" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td style="font-size: 13px;">
                    <p style="color: Red;">
                        नोट :-  1. निरीक्षण के दौरान कोई भी कैप असुरक्षित होने पर तत्काल कैप कव्हर ढंकवाऐ जावें एवं विवरण उल्लेखित करें ।
                    </p>
                </td>
            </tr>

            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">5. कीटोपचार की स्थिति का विवरण :- </span>
                </td>
            </tr>


            <tr>
                <td align="center" colspan="4">
                    <table border="1" width="70%">
                        <tbody style="font-size: 12px; color: Black;">
                            <th>क्षमता का प्रकार </th>
                            <th>कुल स्टेकों की संख्या</th>
                            <th>ध्रुर्मीकृत स्टेक संख्या</th>
                            <th>ध्रुर्मी. हेतु शेष स्टेक संख्या</th>
                            <th>कीटनाशक छिड़काव</th>
                            <th>रिमार्क</th>
                        </tbody>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label25" runat="server" Text="स्वनिर्मित + स्व0पीईजी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOFMG_TotalStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOFMG_fmgStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOFMG_BalFmgStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOFMG_Chml" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOFMG_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr align="center">
                                    <td style="font-size:12px; color:Black;">
                                     <asp:Label ID="Label26" runat="server" Text="स्व0पीईजी "></asp:Label>
                                    </td>
                                    <td>
                                     <asp:TextBox ID="TextBox147" runat="server" Width="100px" Height="25px" align="Center" >0</asp:TextBox>
                                    </td>  
                                    <td>
                                     <asp:TextBox ID="TextBox148" runat="server" Width="100px" Height="25px" align="Center" >0</asp:TextBox>
                                    </td>                                    
                                    <td>
                                     <asp:TextBox ID="TextBox149" runat="server" Width="100px" Height="25px" align="Center" >0</asp:TextBox>
                                    </td>
                                    <td>
                                     <asp:TextBox ID="TextBox150" runat="server" Width="100px" Height="25px" align="Center" >0</asp:TextBox>
                                    </td>                                  
                                    <td>
                                     <asp:TextBox ID="TextBox151" runat="server" Width="150px" Height="25px" align="Center" 
                                            TextMode="MultiLine" ></asp:TextBox>
                                    </td>                                                                                                                                                                                   
                            </tr>--%>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label27" runat="server" Text="निजी पीईजी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPEGFMG_NoOfStakc" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPEGFMG_fmgstack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPEGFMG_Balfmgstack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPEGFMG_chml" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPEGFMG_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label28" runat="server" Text="जेव्हीएस"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJVSFMG_TotalStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJVSFMG_fmgstack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJVSFMG_BalFmgStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJVSFMG_chml" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJVSFMG_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label29" runat="server" Text="किराया/अधिग्रहण"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHFMG_TotalStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHFMG_fmgstack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHFMG_BalmgStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHFMG_chml" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHFMG_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label30" runat="server" Text="केप"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAPFMG_TotalStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAPFMG_fmgStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAPFMG_BalFmgStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAPFMG_chml" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAPFMG_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label31" runat="server" Text="अन्य"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOthFMG_TotalStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOthFMG_FmgStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOthFMG_BalFmgStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOthFMG_Chml" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOthFMG_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="center">
                                <asp:Button class="button button2" ID="Button7" runat="server" Text="Save"
                                    TabIndex="11" CssClass="btn btn-info" OnClick="Button7_Click"></asp:Button>
                                <asp:Label ID="lblkitopcharmessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>

            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">6. गोदाम में कीटग्रस्तता की स्थिति का विवरण :- </span>
                </td>
            </tr>


            <tr>
                <td align="center" colspan="4">
                    <table border="1" width="60%">
                        <tbody style="font-size: 12px; color: Black;">
                            <th>क्षमता का प्रकार </th>
                            <th>गोदाम में कुल स्टेकों की संख्या</th>
                            <th>स्टेक C</th>
                            <th>स्टेक F</th>
                            <th>स्टेक H</th>
                            <th>कीटग्रस्तता की स्थिति में कार्यवाही</th>
                        </tbody>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label32" runat="server" Text="स्वनिर्मित + स्व0पीईजी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOCl_TotalStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOCl_C" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOCl_F" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOCl_H" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOCl_Process" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr align="center">
                                    <td style="font-size:12px; color:Black;">
                                     <asp:Label ID="Label33" runat="server" Text="स्व0पीईजी "></asp:Label>
                                    </td>
                                    <td>
                                     <asp:TextBox ID="TextBox182" runat="server" Width="100px" Height="25px" align="Center" >0</asp:TextBox>
                                    </td>  
                                    <td>
                                         <asp:DropDownList ID="DropDownList1" runat="server"  Width="100px"> 
                                            <asp:ListItem Value="--Select--">Select</asp:ListItem>
                                            <asp:ListItem Value="C">क्लीयर</asp:ListItem>
                                            <asp:ListItem Value="F">फ्यू</asp:ListItem>
                                            <asp:ListItem Value="H">हेवी</asp:ListItem>                                         
                                        </asp:DropDownList>
                                    </td>                                                                      
                                    <td>
                                     <asp:TextBox ID="TextBox186" runat="server" Width="150px" Height="25px" align="Center" 
                                            TextMode="MultiLine" ></asp:TextBox>
                                    </td>                                                                                                                                                                                   
                            </tr>--%>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label34" runat="server" Text="निजी पीईजी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPEGCl_TotalStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPEGCl_C" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPEGCl_F" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPEGCl_H" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPEGCl_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label35" runat="server" Text="जेव्हीएस"></asp:Label>
                            </td>

                            <td>
                                <asp:TextBox ID="txtJVSCl_TotalStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJVSCl_C" runat="server" Width="50" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJVSCl_F" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJVSCl_H" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJVSCl_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label36" runat="server" Text="किराया/अधिग्रहण"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHCl_TotalStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHCl_C" runat="server" Width="50" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHCl_F" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHCl_H" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHCl_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label37" runat="server" Text="केप"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAPCl_TotalStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAPCl_C" runat="server" Width="50" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAPCl_F" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAPCl_H" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCAPCl_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label38" runat="server" Text="अन्य"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOthCl_TotalStack" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOthCl_C" runat="server" Width="50" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOthCl_F" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOthCl_H" runat="server" Width="50px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOthCl_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <asp:Button class="button button2" ID="Button9" runat="server" Text="Save"
                                    TabIndex="11" CssClass="btn btn-info" OnClick="Button9_Click"></asp:Button>
                                <asp:Label ID="lblkigrastamessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">7. भंडारण शुल्क देयक प्रस्तुतीकरण की स्थिति का विवरण :-( रेडण्म चैक करें ) </span>
                </td>
            </tr>

            <tr>
                <td style="font-size: 13px;">
                    <p style="color: Red;">
                        नोट :-  1.  माह प्रविस्ट करने कें लिये इस प्रकार प्रविस्ट करे (Example - July 2019)।
                    </p>
                </td>
            </tr>


            <tr>
                <td align="center" colspan="4">
                    <table border="1" width="70%">
                        <tbody style="font-size: 12px; color: Black;">
                            <th>जमाकर्ता का नाम</th>
                            <th>देयक प्रस्तुत माह</th>
                            <th>भुगतान प्राप्ति माह तक</th>
                            <th>भुगतान लंबित माह</th>
                            <th>भुगतान लंबित रहने का कारण</th>
                            <th>रिमार्क</th>
                        </tbody>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label39" runat="server" Text="MPSCSC"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMPSC_Prastut" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMPSC_Prapti" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMPSC_Bal" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMPSC_Reason" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMPSC_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label40" runat="server" Text="NAFED"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNFD_Prastus" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNFD_Prapti" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNFD_Bal" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNFD_Resone" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNFD_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label41" runat="server" Text="MARKFED"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMFD_Prastut" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMFD_Prapti" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMFD_Bal" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMFD_Reasone" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMFD_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label42" runat="server" Text="OTHER"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOth_prastut" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOth_Prapit" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOth_Bal" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOth_Reasone" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOth_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="center">
                                <asp:Button class="button button2" ID="Button11" runat="server" Text="Save"
                                    TabIndex="11" CssClass="btn btn-info" OnClick="Button11_Click"></asp:Button>
                                <asp:Label ID="lblbhandarnsulkmessage" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>



                    </table>
                </td>
            </tr>
            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">8. जेव्हीएस देयक प्रस्तुतीकरण की स्थिति का विवरण :-( रेडण्म चैक करें ) </span>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label54" runat="server" Text="जेव्हीएस देयक प्रस्तुत माह तक"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtprastutmahtuk" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label55" runat="server" Text="गोदाम संचालकों को भुगतान माह तक"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbhugtanmahtuk" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label56" runat="server" Text="भुगतान लंबित रहने का कारण"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbhuktanlambitkaran" runat="server" Width="180px" Height="40px"
                                    align="Center" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label57" runat="server" Text="वर्ष 2018-19 जेव्हीएस देयकों से कटौत्रा परीक्षण"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtkatotraparichan" runat="server" Width="180px" Height="40px"
                                    align="Center" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label58" runat="server" Text="कमी/अधिक के विरूद्ध कटौत्रा राषि एवं समायोजन की स्थिति"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtlossgainsamayojan" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="center">
                                <asp:Button class="button button2" ID="Button18" runat="server" Text="Save"
                                    TabIndex="11" CssClass="btn btn-info" OnClick="Button18_Click"></asp:Button>
                                <asp:Label ID="lvljvskatotramessage" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">9.कैश बैलेन्स की स्थिति का विवरण :-</span>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <table border="1" width="70%">
                        <tbody style="font-size: 12px; color: Black;">
                            <th>विवरण</th>
                            <th>रिकार्ड अनुसार राषि रूपये</th>
                            <th>भौतिक सत्यापन में पाई गई राषि रूपये</th>
                            <th>अन्तर</th>
                            <th>रिमार्क</th>
                        </tbody>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label43" runat="server" Text="मुख्य कैश बुक"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtM_Recordrashi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtM_PVRashi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtM_Anter" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtM_remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label44" runat="server" Text="इम्प्रैस्ट केष बुक"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtS_RecordRashi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtS_PVRashi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtS_Anter" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtS_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label45" runat="server" Text="निर्माण इम्प्रैस्ट केष बुक"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtN_RecordRashi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtN_PVRashi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtN_Anter" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtN_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label46" runat="server" Text="राजस्व टिकिट"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtT_RecordRashi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtT_PVRashi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtT_Anter" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtT_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label47" runat="server" Text="अन्य"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtO_RecordRashi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtO_PVRashi" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtO_Amter" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOCash_Remark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="center">
                                <asp:Button class="button button2" ID="Button13" runat="server" Text="Save"
                                    TabIndex="11" CssClass="btn btn-info" OnClick="Button13_Click"></asp:Button>
                                <asp:Label ID="lblcasemesaage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                &nbsp;
                                                                                              
                            </td>
                        </tr>



                    </table>
                </td>
            </tr>

            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">10. शाखा पर पदस्थ समस्त कर्मचारियों की सूची का विवरणः-</span>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <table border="1" width="60%">
                        <tbody style="font-size: 12px; color: Black;">
                            <th>पदनाम</th>
                            <th>संख्या</th>
                            <th>पदस्थी दिनाक</th>
                            <th>पदस्थी अवधि(वर्ष)</th>
                            <th>रिमार्क</th>
                        </tbody>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label48" runat="server" Text="शाखा प्रबंधक"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBMNoOfPer" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBMInchDate" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtBMInchDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBMInchYear" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBMRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label49" runat="server" Text="सहा0गुण0निय0"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAQCNoOfPer" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAQCInchDate" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtAQCInchDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAQCIncgYear" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAQCRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label50" runat="server" Text="कनि0सहायक"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQCNoOfPer" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQCInchDate" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtQCInchDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQCInchYear" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQCInchRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label51" runat="server" Text="सी0सी0एच0"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCCHNoOfPer" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCCHInchDate" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtCCHInchDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCCHInchYear" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCCHReamrk" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label52" runat="server" Text="स्थायी कर्मी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSthaiKarmiNoOfPer" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSthaiKarmiInchDate" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtSthaiKarmiInchDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSthaiKarmiInchYear" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSthaiKarmiRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 12px; color: Black;">
                                <asp:Label ID="Label53" runat="server" Text="दैनिक वेतन भोगी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDainikNoOfPer" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDainikInchDate" runat="server" Width="100px" Height="25px" align="Center"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDainikInchDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDainikInchYear" runat="server" Width="100px" Height="25px" align="Center">0</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDainikRemark" runat="server" Width="150px" Height="25px" align="Center"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="center">
                                <asp:Button class="button button2" ID="Button15" runat="server" Text="Save"
                                    TabIndex="11" CssClass="btn btn-info" OnClick="Button15_Click"></asp:Button>
                                <asp:Label ID="lblpadsttmessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>





            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">11. शाखा पर गोदामों का विवरण </span>
                    <br />
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; A क्या शाखा में निम्नलिखित पँजियो का अधतन व्यवस्थित संधारण  किया गया हैं </span>

                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label81" runat="server" Text="आवक पंजी"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlavakyesno" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label82" runat="server" Text="जावक पंजी"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtjavakpanji" runat="server" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"></asp:TextBox>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label83" runat="server" Text="स्थानीय डाक-बुक"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlsthanidakbook" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label84" runat="server" Text="स्टेशनरी पंजी"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlstashnar" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label85" runat="server" Text="डेड स्टॉक पंजी"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddldedstock" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>

                            <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 15px">B. शाखा पर गोदाम का विवरण </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label87" runat="server" Text="उन गोदामो का विवरण जिनकी स्वीकृति अभी मुख्यालय / क्षेत्रीय कार्यालय से आना बांकी हैं "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGHO" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label88" runat="server" Text="शाखा पर गोदाम कितनी पारधी में स्थित हैं ?"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtgodamdistance" runat="server" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>

                            <td align="left" style="border: #E6C79D;" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 15px">1. क्या सभी गोदाम निम्न बिन्दुओं से उपयुक्त है (यदि उत्तर नकारात्मक हो तो विवरण के साथ सुझाव प्रस्तुत करें।) </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label89" runat="server" Text="(अ) वैज्ञानिक भण्डारण की दृष्टि से"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlA" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label90" runat="server" Text="(ब) व्यवसायिक दृष्टि से"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlB" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label92" runat="server" Text="(स) निगरानी एवं सुरक्षा की दृष्टि से"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlC" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr id="showtxtA" runat="server" style="display: none;">
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label91" runat="server" Text="(अ) विवरण के साथ सुझाव प्रस्तुत करें"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtA" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>
                        <tr id="showtxtB" runat="server" style="display: none;">
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label93" runat="server" Text="(ब) विवरण के साथ सुझाव प्रस्तुत करें"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtB" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>
                        <tr id="showtxtC" runat="server" style="display: none;">
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label94" runat="server" Text="(स) विवरण के साथ सुझाव प्रस्तुत करें"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtC" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label95" runat="server" Text="2. क्या गोदाम पंजी का संधरण व्यवस्थित पद्धति से अद्यतन किया जाता है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl2" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;" colspan="2">
                                <asp:Label ID="Label96" runat="server" Text="3. क्या गोदाम लाग बुक का संधारण व्यवस्थित पद्धति से अद्यतन किया जाता है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl3" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;" colspan="3">
                                <asp:Label ID="Label97" runat="server" Text="4. उन गोदामों की सूची जिनमें निम्न स्तरीय निर्माण/व्यवसाय में कमी और कोई कारण से खाली करने हेतु अनुससित किया गया हो, विस्तृत विवरण के साथ कारणों का उल्लेख करें।"></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txt4" runat="server" Width="500px"
                                    Height="38px" Font-Bold="true" ForeColor="Navy" CssClass="tb6" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border: #E6C79D;" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 15px">क्लेम </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;" colspan="2">
                                <asp:Label ID="Label98" runat="server" Text="बीमा कम्पनी को यदि क्लेम किया गया हो तो अद्यतन जानकारी।"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtcleimremark" runat="server" Width="500px"
                                    Height="38px" Font-Bold="true" ForeColor="Navy" CssClass="tb6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border: #E6C79D;" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 15px">वैज्ञानिक भण्डारण </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label99" runat="server" Text="क्या प्रत्येक स्टेक के बीच आवष्यक गलियारा छोड़ा गया है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl8" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label100" runat="server" Text="क्या गोदाम में स्टेक का योजनाबद्ध नियोजन किया गया है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl9" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label101" runat="server" Text="क्या स्टेक को व्यवस्थित एवं वैज्ञानिक ढंग से निर्मित किया गया है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl10" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label102" runat="server" Text="क्या प्रत्येक स्टेक पर कार्ड लगाये हुए है और उनके दोनो ओर अद्यतन प्रविष्टियां अंकित की जा रही है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl11" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label103" runat="server" Text="क्या स्टेक पंजी का संधारण व्यवस्थित ढंग से किया जाता है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl12" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label104" runat="server" Text="क्या स्कंध को कीट रहित रखने हेतु नियतकालिक कीटोपचार धुम्रीकरण समय पर किया जाता है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl13" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>

                        <tr>
                            <td align="left" style="border: #E6C79D;" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 15px">प्रतीकात्मक नमूना-वर्गीकरण और विष्लेषण</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;" colspan="2">
                                <asp:Label ID="Label105" runat="server" Text="क्या जमा करते समय स्कंध का नमूना किया गया है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl14" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="font-size: 13px; color: Black;" colspan="2">
                                <asp:Label ID="Label106" runat="server" Text="क्या नमूना पंजी का अद्यतन संधारण किया गया है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl15" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td align="left" style="border: #E6C79D;" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 15px">तौल</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;" colspan="3">
                                <asp:Label ID="Label107" runat="server" Text="क्या शाखा पर पर्याप्त संख्या में कांटा-वांट उपलब्ध है ? और सभी नाप-तौल विभाग से प्रमाणित है। प्रमाण-पत्र का क्रंमांक व दिनांक (विसंगति होने पर उसका तत्काल किया जावे और वस्तु स्थिति से अवगत कराया जावें।)"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddl16" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td align="left" style="border: #E6C79D;" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 15px">जमा एवं भुगतान वेयरहाउस रसीद </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label108" runat="server" Text="क्या जमाकर्ता के नमूने हस्ताक्षर और अधिकार पत्र निर्धारित प्रक्रियानुसार ठीक से रखे गये है।"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl17" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label109" runat="server" Text="क्या वेयरहाउस रसीद की मूल व कार्यालीन प्रति में आंषिक/पूर्ण भुगतान का इन्द्राज किया गया है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl18" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label110" runat="server" Text="क्या पूर्ण भुगतान के समय जमाकर्ता से मूल वेयरहाउस रसीद लेकर सम्बन्धित भुगतान पत्रक के साथ निरस्त कर लगाई गई है ? "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl19" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label111" runat="server" Text="क्या भुगतान के समय स्कंध उसी गोदाम/थप्पी से दिया गया है, जो वेयर हाउस रसीद के अनुसार है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl20" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label112" runat="server" Text="क्या स्कंध जमा होने व भुगतान होने की प्रविष्टियां स्टाक रजिस्टर/डिपाॅजिट लेजर व अन्य पंजियों में ठीक और सही तरीके से की जाती है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl21" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label113" runat="server" Text="क्या स्टाक रजिस्टर/डिपाॅजिट लेजर वेयर हाउस रसीद जमा आवेदन पत्रों पर पारसपरिक संदर्भ निर्देषानुसार दिये जाते है ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl22" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;" colspan="3">
                                <asp:Label ID="Label114" runat="server" Text="क्या शेष स्कंध की मात्रा जैसा कि वेयर हाउस रसीद पर दर्शाया गया है स्टाक रजिस्टर और डिपाजिट लेजर के कुल शेष स्कंध की मात्रा से मिलान करते है या नहीं (यदि नहीं तो मिलान कर सूचित करें)"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl23" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label115" runat="server" Text="वेयर हाउस रसीद होल्डर्स पंजी प्रविष्टियों में अद्यतन है या नहीं ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl24" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td align="left" style="border: #E6C79D;" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 15px">सूरक्षा व्यवस्था</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label116" runat="server" Text="महत्वपूर्ण दस्तावेज सुरक्षित रखे जाने है या नहीं ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl25" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label117" runat="server" Text="तालों की दूसरी चाबियां शाखा प्रबंधक द्वारा बैंक में जमा की गई है या नहीं ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl26" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;">
                                <asp:Label ID="Label118" runat="server" Text="शाखा पर संग्रहित स्कंध व गोदामों की देखभाल व सुरक्षा हेतु पर्याप्त व्यवस्था संतोषजनक है या नहीं ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl27" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td align="left" style="border: #E6C79D;" colspan="6">
                                <span style="color: #cb4e48; font-weight: bold; font-size: 15px">सामान्य</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;" colspan="2">
                                <asp:Label ID="Label119" runat="server" Text="शाखा प्रबंधक के जमाकर्ताओं/संबंधित व्यक्तियों से सोहार्दपूर्ण सम्बंध है या नहीं ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl28" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td style="font-size: 13px; color: Black;" colspan="2">
                                <asp:Label ID="Label120" runat="server" Text="शाखा को आवष्यकताओं का विभागावार पूर्ण न्यायोचित विवरण ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl29" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 13px; color: Black;" colspan="2">
                                <asp:Label ID="Label121" runat="server" Text="शाखा का व्यवसाय/आर्थिक गतिविधियों को बढ़ाने हेतु निरीक्षण अधिकारी के सुझाव?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl30" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="font-size: 13px; color: Black;" colspan="2">
                                <asp:Label ID="Label122" runat="server" Text="शाखा के कार्यकलापों के बारे में निरीक्षण अधिकारी की टिप्पणी ?"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl31" runat="server" AutoPostBack="false" Width="222px"
                                    Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
            <tr>
                <td colspan="7" align="center">
                    <asp:Button class="button button2" ID="Button2" runat="server" Text="Save"
                        TabIndex="11" CssClass="btn btn-info" OnClick="btnsaveGodownDetilasonBranches_Click"></asp:Button>
                    <asp:Label ID="Label86" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            </table>
                </td>
            </tr>
            <tr>

                <td align="left" style="border: #E6C79D; border-style: solid; border-width: 2px;" colspan="4">
                    <span style="color: #cb4e48; font-weight: bold; font-size: 15px">Self Decleration</span>
                </td>
            </tr>
            <tr>
                <td style="font-size: 14px; color: black;">
                    <p style="font-size: 14px; color: black;">
                        <asp:CheckBox ID="chkDec" runat="server" />
                        भौतिक सत्यापन अधिकारी द्वारा प्रमाणित किया जाता हे को मेरे द्वारा उपरोक्त विवरणनुसार भौतिक सत्यापन किया एवं काॅलम जमाकर्ता का नाम,भौतिक सत्यापन में पाये गये बोरो की संख्या एवं कीट रहित/कीटग्रहस्त में वर्णानुसार सही पाया है । 
                                     
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="7" align="center">
                    <asp:Button class="button button2" ID="Button17" runat="server" Text="Final Submittion"
                        TabIndex="11" CssClass="btn btn-info" OnClick="Button17_Click"></asp:Button>
                </td>
            </tr>


            <tr>
                <td style="height: 5px;" colspan="6"></td>
            </tr>
        </table>
        <asp:Label ID="Label26" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlofferpopup" TargetControlID="Label26"
            BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlofferpopup" runat="server" CssClass="modalPopup" Height="150px" Width="250px" Visible="false">
            <div class="header">
                <table style="width: 100%;">
                    <tr>
                        <td style="color: White; font-weight: bold" align="center">Confirmation Message</td>
                        <td></td>
                    </tr>

                </table>
            </div>
            <div class="body">
                <table align="center" style="width: 100%; border: #008CBA; border-style: solid; border-width: 0px;">
                    <tr>
                        <td style="height: 10px;"></td>
                    </tr>

                    <tr>
                        <td align="center">Successfully Save
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;"></td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button class="button button2" Width="100px" Height="30px" ID="btncnfrmmessage"
                                runat="server" Text="Ok" align="Center" OnClick="btncnfrmmessage_Click" />

                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            $("[id$=txtexdate]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $("[id$=txtinspexdatefrom]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $("[id$=txtinspexdateto]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });

    </script>
    <script>
        $(document).ready(function () {
            $("[id$=txtpriciusinspdate]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $("[id$=txtinspectiondate]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $("[id$=txtskandhdatefrom]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $("[id$=txtskandhdateto]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $("[id$=txtskandhletterdatefrom]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $("[id$=txtskandhletterdateto]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $("[id$=txtwarehouserecieptdate]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>
</asp:Content>

