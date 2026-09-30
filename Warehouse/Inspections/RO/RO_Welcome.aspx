<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO.master" AutoEventWireup="true" CodeFile="RO_Welcome.aspx.cs" Inherits="Inspections_RO_State_Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .wrap {
            margin: 0 auto;
            width: 960px;
            -moz-box-shadow: 0px 5px 23px #000;
            -webkit-box-shadow: 0px 5px 23px #000;
            box-shadow: 0px 5px 23px #000;
        }

        input.submit {
            color: #fff;
            padding: 7px 10px;
            border: 0;
            font-weight: bold;
            background: #777;
            border-radius: 25px;
        }

        input.text {
            border: 2px solid rgb(173, 204, 204);
            height: 20px;
            width: 223px;
            font-size: 16px;
            box-shadow: 0px 0px 27px rgb(204, 204, 204) inset;
            transition: 500ms all ease;
            padding: 3px 3px 3px 3px;
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
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
    </script>
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
        <table align="center" style="width: 100%; text-align: center; height: 300px">
            <tr>
                <td>
                    <asp:Button class="button button2" ID="btnNewReg" runat="server"
                        Text="Add Inspection Officer For Third Party" Font-Size="15px" Font-Bold="false"
                        TabIndex="1" Width="260px" Height="60px" OnClick="btnNewReg_Click"></asp:Button>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   
                                                     
                                         <asp:Button class="button button6" ID="btnPaymentReg"
                                             runat="server" Text="Schedule Inspection For Third Party" Font-Size="15px" Font-Bold="false"
                                             TabIndex="2" Width="260px" Height="60px" OnClick="btnPaymentReg_Click"></asp:Button>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                         
                                        <%-- <asp:Button class="button button3" ID="btnupdatereg" runat="server"
                                             Text="View Filled Inspection Summary" Font-Size="15px" Font-Bold="false"
                                             TabIndex="3" Width="260px" Height="60px" OnClick="btnupdatereg_Click"></asp:Button>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button class="button button2" ID="btnmpwlcaddemp" runat="server"
                        Text="Add Inspection Officer For MPWLC Employee" Font-Size="15px" Font-Bold="false"
                        TabIndex="1" Width="325px" Height="60px" OnClick="btnmpwlcaddemp_Click"></asp:Button>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   
                                                     
                                         <asp:Button class="button button6" ID="btnmpwlcsc"
                                             runat="server" Text="Schedule Inspection For MPWLC Employee" Font-Size="15px" Font-Bold="false"
                                             TabIndex="2" Width="325px" Height="60px" OnClick="btnmpwlcsc_Click"></asp:Button>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                         
                                        <%-- <asp:Button class="button button3" ID="btnupdatereg" runat="server"
                                             Text="View Filled Inspection Summary" Font-Size="15px" Font-Bold="false"
                                             TabIndex="3" Width="260px" Height="60px" OnClick="btnupdatereg_Click"></asp:Button>--%>
                </td>
            </tr>
            <%--<tr>
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
</asp:Content>

