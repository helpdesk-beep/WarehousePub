<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GodownCapecityFormWithoutMaster.aspx.cs" Inherits="Accounting_GodownCapecityFormWithoutMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title style="color: white;">Godown Capacity Report</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <link href="Content/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.css" rel="stylesheet" />
    <script src="scripts/dataTables.bootstrap4.min.js"></script>
    <script src="scripts/jquery.dataTables.min.js"></script>
   
    <style>
        .btnMargin {
            margin-bottom: 10px !important;
        }
    </style>
    <style>
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
            width: 100%;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #00aad2 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #00aad2 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
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
            Width: 200px;
            height: 50px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
     
                <asp:Panel ID="Givdata" runat="server" >

        
                                                <table cellpadding="0" cellspacing="0" style="width: 100% ; text-align:center; ">
                                                    <tr style="background-color: #0bb6e6; height: 25px">
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="Label1" runat="server" Text="गोडाउन की कुल छमता " Font-Bold="true"
                                                                Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td colspan="4" style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="Label3" runat="server" Text="Depositer" ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                              <asp:DropDownList ID="DdlDepositer"  runat="server"  Width="155px" Height="25px" CssClass="tb6"  >
                                                               <%--   <asp:ListItem Text="Select-Depositer" Value="0"></asp:ListItem>--%>
                                                                  <asp:ListItem Text="MPSCSC" Value="MPSCSC"></asp:ListItem>
                                                                  <asp:ListItem Text="MOCKFED" Value="MOCKFED"></asp:ListItem>
                                                                  <asp:ListItem Text="FCI" Value="FCI"></asp:ListItem>
                                                                  <asp:ListItem Text="NEFED" Value="NEFED"></asp:ListItem>
                                                                  <asp:ListItem Text="OTHER" Value="OTHER"></asp:ListItem>
                                                              </asp:DropDownList>
                                                                                                                       
                                                        </td>
                                                        <td align="left" style="width: 200px">
                                                            <asp:Label ID="Label4" runat="server" Text="Currodity" ForeColor="navy" Font-Bold="true"
                                                                Font-Size="8pt"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="3">                                                            
                                                      <asp:DropDownList ID="DdlCurrodity"  runat="server"  Width="155px" Height="25px" CssClass="tb6" OnSelectedIndexChanged="DdlCurrodity_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                               
                                                          
                                                        </td>

                                                    </tr>
                                            
                                                    </table>
        <br />

<fieldset style="width: 1000px;">
        <center>
            <div >
                <div >
                    <div >
                    
                        <div class="row">
                            <div class="col-12">
                                <asp:GridView ID="gvCol3"  CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" OnRowUpdating="gvCol3_RowUpdating">
                                    <Columns>
                 


                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <tr style="text-align: center;">
                                                    <th style="width: 0px"></th>
                                                    <th style="width: 0px"></th>
                                                    <th style="width: 100px">Godown Name</th>
                                                    <th colspan="3" style="text-align: center;">2010-11 </th>
                                                    <th colspan="3" style="text-align: center;">2011-12</th>
                                                    <th colspan="3" style="text-align: center;">2012-13</th>
                                                    <th colspan="3" style="text-align: center;">2013-14</th>
                                                    <th colspan="3" style="text-align: center;">2014-15</th>

                                                       <th colspan="3" style="text-align: center;">2015-16 </th>
                                                    <th colspan="3" style="text-align: center;">2016-17</th>
                                                    <th colspan="3" style="text-align: center;">2017-18</th>
                                                    <th colspan="3" style="text-align: center;">2018-19</th>
                                                    <th colspan="3" style="text-align: center;">2019-20</th>
                                                    <th colspan="3" style="text-align: center;">2020-21</th>
                                                       <th colspan="3" style="text-align: center;">2021-22</th>

                                                </tr>
                                                <tr>
                                                   
                                                      <th style="text-align: center;">Update</th>
                                                    <th style="text-align: center;">S.No</th>
                                                    <th style="text-align: center;">Godown Nam</th>

                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>
                                                    
                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>
                                                    
                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>
                                                    
                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>
                                                    
                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>

                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>

                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>

                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>

                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>

                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>

                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>

                                                    <th style="text-align: center;">कुल भण्डारण मात्रा </th>
                                                    <th style="text-align: center;">कुल भुगतान मात्रा  </th>
                                                    <th style="text-align: center;">शेष मात्रा </th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                   <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>     


                                                <td><%# Container.DataItemIndex + 1 %></td>
                                                <td><%# Eval("Godown_Name") %></td>
                                                <asp:HiddenField ID="hdngodownid" runat="server" Value='<%#Eval("Godown_ID") %>'/>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity1" Text='<%# Eval("Godown_Total_Capacity_2010_11")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity1" Text='<%# Eval("Godown_Total_Use_Capacity_2010_11")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                     <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity1" Text='<%# Eval("Godown_Remaining_Capacity_2010_11")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity2" Text='<%# Eval("Godown_Total_Capacity_2011_12")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity2" Text='<%# Eval("Godown_Total_Use_Capacity_2011_12")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                   
                                                 <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity2" Text='<%# Eval("Godown_Remaining_Capacity_2011_12")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity3" Text='<%# Eval("Godown_Total_Capacity_2012_13")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity3" Text='<%# Eval("Godown_Total_Use_Capacity_2012_13")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>                                             
                                              
                                                     <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity3" Text='<%# Eval("Godown_Remaining_Capacity_2012_13")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                    
                                                </td>

                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity4" Text='<%# Eval("Godown_Total_Capacity_2013_14")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity4" Text='<%# Eval("Godown_Total_Use_Capacity_2013_14")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>                                          
                                              
                                                     <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity4" Text='<%# Eval("Godown_Remaining_Capacity_2013_14")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity5" Text='<%# Eval("Godown_Total_Capacity_2014_15")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity5" Text='<%# Eval("Godown_Total_Use_Capacity_2014_15")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                   
                                                 <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity5" Text='<%# Eval("Godown_Remaining_Capacity_2014_15")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                </td>


                                                     <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity6" Text='<%# Eval("Godown_Total_Capacity_2015_16")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity6" Text='<%# Eval("Godown_Total_Use_Capacity_2015_16")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                   
                                                 <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity6" Text='<%# Eval("Godown_Remaining_Capacity_2015_16")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                </td>

                                                     <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity7" Text='<%# Eval("Godown_Total_Capacity_2016_17")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity7" Text='<%# Eval("Godown_Total_Use_Capacity_2016_17")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                   
                                                 <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity7" Text='<%# Eval("Godown_Remaining_Capacity_2016_17")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                </td>

                                                     <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity8" Text='<%# Eval("Godown_Total_Capacity_2017_18")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity8" Text='<%# Eval("Godown_Total_Use_Capacity_2017_18")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                   
                                                 <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity8" Text='<%# Eval("Godown_Remaining_Capacity_2017_18")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                </td>

                                                     <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity9" Text='<%# Eval("Godown_Total_Capacity_2018_19")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity9" Text='<%# Eval("Godown_Total_Use_Capacity_2018_19")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                   
                                                 <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity9" Text='<%# Eval("Godown_Remaining_Capacity_2018_19")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                </td>

                                                     <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity10" Text='<%# Eval("Godown_Total_Capacity_2019_20")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity10" Text='<%# Eval("Godown_Total_Use_Capacity_2019_20")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                   
                                                 <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity10" Text='<%# Eval("Godown_Remaining_Capacity_2019_20")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                </td>

                                                     <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity11" Text='<%# Eval("Godown_Total_Capacity_2020_21")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity11" Text='<%# Eval("Godown_Total_Use_Capacity_2020_21")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                   
                                                 <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity11" Text='<%# Eval("Godown_Remaining_Capacity_2020_21")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                </td>

                                                     <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalCapacity12" Text='<%# Eval("Godown_Total_Capacity_2021_22")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox class="form-control" runat="server" ID="GodownTotalUseCapacity12" Text='<%# Eval("Godown_Total_Use_Capacity_2021_22")%>' onkeypress="return AllowNumber(event)"></asp:TextBox></td>
                                                <td>
                                                   
                                                 <asp:Label class="form-control" runat="server" ID="GodownRemainingCapacity12" Text='<%# Eval("Godown_Remaining_Capacity_2021_22")%>' onkeypress="return AllowNumber(event)"></asp:Label>
                                                </td>
                                                
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <br />
                                <br />
                                
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </center>
    </fieldset>





               
</asp:Panel>
     
    </form>
</body>

        <script>
            function AllowAlphabet(e) {
                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : event.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
                    return true;
                else {
                    alert('Please Enter Only Character values.');
                    return false;
                }
            }

            function AllowNumber(evt) {
                if ((evt.which != 46 || self.val().indexOf('.') != -1) && (evt.which < 48 || evt.which > 57)) {
                    alert("Allow Only Numbers");
                    return false;
                }
            }

        </script>
</html>
