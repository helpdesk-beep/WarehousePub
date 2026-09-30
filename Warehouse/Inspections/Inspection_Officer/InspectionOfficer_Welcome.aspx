<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_Officer.master" AutoEventWireup="true" CodeFile="InspectionOfficer_Welcome.aspx.cs" Inherits="Inspections_Inspection_Officer_InspectionOfficer_Welcome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#Panel1").hide();

            $("#Button1").click(function () {

                $("#Panel1").show();

            });

        });
                         </script>


<div  style="width: 100%; ">
                   
                    <br /><br />
                                 <asp:Button class="button button2" ID="btnAddOfficer" runat="server" Text="Fill Inspection" Font-Size="15px" Font-Bold="false"
                                 TabIndex="1" Width="200px" Height="50px" onclick="btnAddOfficer_Click"></asp:Button>    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp   
                                             
                                <asp:Button class="button button6" ID="btnAllotInsp" 
                        runat="server" Text="View Filled Annexure B" Font-Size="15px" Font-Bold="false"   
                                 TabIndex="2" Width="200px" Height="50px" 
                            onclick="btnAllotInsp_Click" ></asp:Button>
              <%--                    
                                 <asp:Button class="button button3" ID="btnviewInsp" runat="server" Text="View Inspection"   Font-Size="15px" Font-Bold="false"
                                 TabIndex="3" Width="200px" Height="50px"></asp:Button> --%>                                                                   
                    <br /><br /><br />
            </div>
</asp:Content>

