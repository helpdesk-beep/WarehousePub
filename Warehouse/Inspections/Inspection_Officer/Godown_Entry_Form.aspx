<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="Godown_Entry_Form.aspx.cs" Inherits="Inspections_Inspection_Officer_Godown_Entry_Form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <%--Dropdown New--%>
<script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
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
<%--Dropdown End--%>

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

        .button4 {
            background-color: white;
            color: black;
            border: 2px solid #FC00B3;
        }

            .button4:hover {
                background-color: #FC00B3;
                color: white;
            }

        .button5 {
            background-color: white;
            color: black;
            border: 2px solid #CBE555;
        }

            .button5:hover {
                background-color: #CBE555;
                color: white;
            }

        .button7 {
            background-color: white;
            color: black;
            border: 2px solid #AEB6BF;
        }

            .button7:hover {
                background-color: #AEB6BF;
                color: white;
            }

        .button8 {
            background-color: white;
            color: black;
            border: 2px solid #F4D03F;
        }

            .button8:hover {
                background-color: #F4D03F;
                color: white;
            }

        .button9 {
            background-color: white;
            color: black;
            border: 2px solid #117A65;
        }

            .button9:hover {
                background-color: #117A65;
                color: white;
            }

        .auto-style1 {
            width: 145px;
        }

        .auto-style2 {
            width: 215px;
        }

        .auto-style3 {
            width: 117px;
        }

        .auto-style4 {
            width: 299px;
        }
      /* // Date picker//*/
       .datepicker-dropdown {
    border-radius: 1rem !important;
    padding: 10px !important;
    border: 2px solid #0d6efd !important;
}

/* 🔹 Month & Year Header */
.datepicker .datepicker-switch {
    font-weight: 600;
    color: #161718 !important;
    background: #e9f2ff;
    border-radius: 8px;
    padding: 6px;
}

/* 🔹 Day Names (Sun, Mon, Tue...) */
.datepicker thead th.dow {
    color: #198754 !important;
    font-weight: 600;
    background: #e9f9f0;
    border-radius: 6px;
    padding: 5px;
}

/* 🔹 Today's date */
.datepicker table tr td.today {
    background: #0d6efd !important;
    color: #fff !important;
    border-radius: 50%;
}

/* 🔹 Active (selected) date */
.datepicker table tr td.active,
.datepicker table tr td.active:hover {
    background: #198754 !important;
    color: #fff !important;
    border-radius: 50%;
}
    </style>

   <%-- <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>--%>
    <div runat="server">
        <%-- <table align="center" style="width: 100%;">        
                      <tr >
                            <td colspan="4" style="font-size: medium; font-weight:bolder;">
                            <table  style="width: 100%; height:30px; font-size: medium;" >
                            <tr>
                            <td  style="background-color: #E6C79D; width:150PX; font-size: medium; color: #cb4e48; font-weight:bolder; " 
                                    align="center"> 
                             <asp:Label ID="lbl_date" runat="server"></asp:Label>
                            </td>
                            <td colspan="2" 
                                    style="background-color: #D69758 ; font-size: medium; color: White; width:100px;font-weight:bolder;" 
                                    align="center" >
                            Welcome&nbsp;<asp:Label ID="lbl_user" runat="server" ForeColor="White"></asp:Label></td>
                            </tr>
                            </table>
                            </td>
                        </tr>  
                        
                                                                  
                    </table>--%>
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
            निरीक्षण अधिकारी को Closing बैलेंस देने के लिए दिये गए सभी विकल्प अच्छे से जाँच कर लेवे ,
            <br />
            क्योकि इसी बैलेंस के अनुसार आपकी ब्रांच का निरीक्षण/भौतिक सत्यापन किया जायेगा
        </div>
        <table align="center" style="width: 80%; margin-left: 100px;">
            <tr>
                 <td style="width: 20px; text-align: right;">

                    <asp:Label ID="Label5" runat="server" Text="Branch : "></asp:Label>
                </td>
                <td style="width: 50px; text-align: left;">
                    <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="false" Width="222px"
                        Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                        <asp:ListItem Value="0">--Select Branch--</asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td style="width: 20px; text-align: right;" >

                    <asp:Label ID="Label4" runat="server" Text="Financial Year : "></asp:Label>
                </td>
                <td style="width: 50px; text-align: left;">
                    <asp:DropDownList ID="ddlfinancialyear" runat="server" AutoPostBack="false" Width="222px"
                        Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                        <asp:ListItem Value="0">--Select Financial Year--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 20px; text-align: right;">

                    <asp:Label ID="Label2" runat="server" Text="Employees : "></asp:Label>
                </td>
                <td style="width: 50px; text-align: left;">
                    <asp:DropDownList ID="ddlemp" runat="server" AutoPostBack="false" Width="222px"
                        Height="25px" Font-Bold="true" ForeColor="Navy" CssClass="tb6">
                    </asp:DropDownList>
                </td>
                <td style="width: 20px; text-align: right;">
                    <asp:Label ID="Label1" runat="server" Text="Inspection Closing Date : "></asp:Label>
                </td>
                <td style="width: 50px; text-align: left;">
                    <asp:TextBox ID="txtdob" placeholder="DD/MM/YY" onclick="return ValidateDOB()" CssClass="form-control datepicker" runat="server" Height="24px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtdob" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtdob" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                        ErrorMessage="Invalid date format." ValidationGroup="A" ForeColor="Red" />

                </td>
            </tr>
            <tr>
                <td style="width: 20px; text-align: right;">
                    <asp:Label ID="Label7" runat="server" Text="Inspection Type : "></asp:Label>
                </td>
                <td style="text-align: left; width: 50px;">
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
            </tr>
            <tr>
                <td colspan="4" style="text-align: center;">
                    <%--<asp:Button class="button button2" ID="btnNewReg" runat="server"
                        Text="Add Inspection Officer" Font-Size="15px" Font-Bold="false"
                        TabIndex="1" Width="260px" Height="60px" OnClick="btnNewReg_Click"></asp:Button>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   
                                                     
                                         <asp:Button class="button button6" ID="btnPaymentReg"
                                             runat="server" Text="Schedule Inspection" Font-Size="15px" Font-Bold="false"
                                             TabIndex="2" Width="260px" Height="60px" OnClick="btnPaymentReg_Click"></asp:Button>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp--%>

                    <asp:Button class="button button3" ID="btnupdatereg" runat="server"
                        Text="Godown Entry" Font-Size="15px" Font-Bold="false" ValidationGroup="A"
                        TabIndex="3" Width="260px" Height="60px" OnClick="btnupdatereg_Click"></asp:Button>
                </td>
            </tr>

            <%-- <tr>
                <td>
                    <asp:Button class="button button9" ID="Button1" runat="server"
                        Text="View Filled Annexure B" Font-Size="15px" Font-Bold="false"
                        TabIndex="1" Width="260px" Height="60px" OnClick="Button1_Click"></asp:Button>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   
                                         <asp:Button class="button button4" ID="Button2" runat="server"
                                             Text="Comparision of PV Bags" Font-Size="15px" Font-Bold="false"
                                             TabIndex="1" Width="260px" Height="60px" OnClick="Button2_Click"></asp:Button>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp                                            
                                                                    
                </td>

            </tr>--%>
        </table>
    </div>
        <!-- Bootstrap DatePicker CSS -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" />
<!-- Bootstrap DatePicker JS -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('.datepicker').datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            todayHighlight: true,

        });
    });
</script>
   <%-- <script>
        $(document).ready(function () {
            $("[id$=txt_inspdate]").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
            });
        });
    </script>--%>
</asp:Content>

