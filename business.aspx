<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true"
    CodeFile="business.aspx.cs" Inherits="business" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="banner" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    <section class="content_wrapper">
        <div class="container">
            <!-- Example row of columns -->
            <div class="row">
                <div class="col-md-3">
                    <div class="list-group">
                        <h4 class=" list-group-item active">BUSINESS INFORMATION</h4>
                        <a href="business.aspx" class="list-group-item">Capacity and Utilization</a>
                        <a href="https://mpwarehousing.mp.gov.in//Warehouse/StatePages/Rpt_Commodity_wise_Stock_Position.aspx" target="_blank" class="list-group-item">Commodity Wise Stock Position</a>
                        <a href="https://mpwarehousing.mp.gov.in//Warehouse/StatePages/Rpt_District_wise_Stock_Position.aspx" target="_blank" class="list-group-item">District Wise Stock Position</a>
                        <a href="https://mpwarehousing.mp.gov.in//Warehouse/StatePages/Rpt_Godown_wise_Stock_Position.aspx" target="_blank" class="list-group-item">Branch, Godown Wise Stock Position</a>
                        <%--<a href="#finance" class="list-group-item">Financial Performance</a>--%>
                    </div>
                </div>
                <div class="col-md-9">

                    <div class="row-fluid">


                        <h3 class="red" style="letter-spacing: 1px;">BUSINESS</h3>
                        <hr class="line-red" />

                        <div class="text_content">
                            <h4><strong>Storage Capacity and Utilization for Month : September - 2021</strong></h4>
                            <p></p>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; height: 18.2pt">
                                        <td valign="top" style="width: 44; background: #999999; padding: 0in; height: 18.2pt"
                                            class="style12">
                                            <p class="MsoNormal21" align="center" style="text-align: center; mso-line-height-alt: 9.0pt">
                                                <span class="SpellE11"><strong><span style="font-size: 10.0pt; color: white"
                                                    class="style12">Sr</span></strong></span><strong><span style="font-size: 10.0pt; color: white" class="style12"> No. </span></strong>
                                            </p>
                                        </td>
                                        <td valign="top" style="width: 60; background: #999999; padding: 0in; height: 18.2pt">
                                            <p class="MsoNormal21" align="center" style="text-align: center; mso-line-height-alt: 9.0pt">
                                                <strong><span style="font-size: 10.0pt; color: white" class="style12">Region</span></strong>
                                            </p>
                                        </td>
                                        <td valign="top" style="width: 88; background: #999999; padding: 0in; height: 18.2pt">
                                            <p class="MsoNormal21" align="center" style="text-align: center; mso-line-height-alt: 9.0pt">
                                                <strong><span style="font-size: 10.0pt; color: white" class="style12">No. of Branches </span></strong>
                                            </p>
                                        </td>
                                        <td valign="top" style="width: 73px; background: #999999; padding: 0in; height: 18.2pt">
                                            <p class="MsoNormal21" align="center" style="text-align: center; mso-line-height-alt: 9.0pt">
                                                <strong><span style="font-size: 10.0pt; color: white" class="style12">No of <span class="SpellE11">Godown</span></span></strong>
                                            </p>
                                        </td>
                                        <td valign="top" style="width: 145; background: #999999; padding: 0in; height: 18.2pt">
                                            <p class="MsoNormal21" align="center" style="text-align: center; mso-line-height-alt: 9.0pt">
                                                <strong><span style="font-size: 10.0pt; color: white" class="style12">Total Capacity Covered</span></strong>
                                            </p>
                                        </td>
                                        <td valign="top" style="width: 91; background: #999999; padding: 0in; height: 18.2pt">
                                            <p class="MsoNormal21" align="center" style="text-align: center; mso-line-height-alt: 9.0pt">
                                                <strong><span style="font-size: 10.0pt; color: white" class="style12">Total Utilization</span></strong>
                                            </p>
                                        </td>
                                        <td valign="top" style="width: 88; background: #999999; padding: 0in; height: 18.2pt">
                                            <p class="MsoNormal21" align="center" style="text-align: center; mso-line-height-alt: 9.0pt">
                                                <strong><span style="font-size: 10.0pt; color: white" class="style12">% of Utilization</span></strong>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 1; height: 15.75pt">
                                        <td style="width: 44; background: #fefbea; padding: 0in; height: 15.75pt"
                                            class="style19">
                                            <p align="center" class="MsoNormal2 style43" style="text-align: center; font-family: Verdana;">1</p>
                                        </td>
                                        <td class="style45" style="width: 60; background: #fefbea; padding: 0in; height: 15.75pt">
                                            <p>Bhopal</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 15.75pt">
                                            <p><span>40</span></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 15.75pt">
                                            <p>1232</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 84; background: #fefbea; padding: 0in; height: 15.75pt">
                                            <p><span>50,97,013</span></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 62; background: #fefbea; padding: 0in; height: 15.75pt">
                                            <p>40,26,523</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 15.75pt">
                                            <p>79</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 44; background: #fefbea; padding: 0in; height: .25in"
                                            class="style19">
                                            <p align="center" class="MsoNormal2 style43" style="text-align: center; font-family: Verdana;">2</p>
                                        </td>
                                        <td class="style45" style="width: 60; background: #fefbea; padding: 0in; height: .25in">
                                            <p>Narmadapuram</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p><span>23</span></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p>748</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 84; background: #fefbea; padding: 0in; height: .25in">
                                            <p><span>30,15,646</span></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 62; background: #fefbea; padding: 0in; height: .25in">
                                            <p>23,09,351</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p>77</p>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 2; height: .25in">
                                        <td style="width: 44; background: #fefbea; padding: 0in; height: .25in"
                                            class="style19">
                                            <p align="center" class="MsoNormal2 style43" style="text-align: center; font-family: Verdana;">3</p>
                                        </td>
                                        <td class="style45" style="width: 60; background: #fefbea; padding: 0in; height: .25in">
                                            <p>Gwalior</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p><span>43</span></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p>678</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 84; background: #fefbea; padding: 0in; height: .25in">
                                            <p>19,46,647</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 62; background: #fefbea; padding: 0in; height: .25in">
                                            <p>13,25,163</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p>68</p>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 3; height: .25in">
                                        <td style="width: 44; background: #fefbea; padding: 0in; height: 20"
                                            class="style19">
                                            <p align="center" class="MsoNormal2 style43" style="text-align: center; font-family: Verdana;">4</p>
                                        </td>
                                        <td class="style45" style="width: 60; background: #fefbea; padding: 0in; height: 20">
                                            <p>Jabalpur<span class="MsoNormal2 style46 style43">&nbsp;</span></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 20">
                                            <p><span>49</span></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 20">
                                            <p>1460</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 84; background: #fefbea; padding: 0in; height: 20">
                                            <p>44,22,291</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 62; background: #fefbea; padding: 0in; height: 20">
                                            <p>31,42,255</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 20">
                                            <p> 71</p>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 4; height: .25in">
                                        <td style="width: 44; background: #fefbea; padding: 0in; height: 23"
                                            class="style19">
                                            <p align="center" class="MsoNormal2 style43" style="text-align: center; font-family: Verdana;">5</p>
                                        </td>
                                        <td class="style45" style="width: 60; background: #fefbea; padding: 0in; height: 23">
                                            <p>Indore</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 23">
                                            <p><span>28</span></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 23">
                                            <p>498</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 84; background: #fefbea; padding: 0in; height: .25in">
                                            <p>12,61,882</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 62; background: #fefbea; padding: 0in; height: 23">
                                            <p> 10,29,568</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 23">
                                            <p> 82</p>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 5; height: .25in">
                                        <td style="width: 44; background: #fefbea; padding: 0in; height: .25in"
                                            class="style19">
                                            <p align="center" class="MsoNormal2 style43" style="text-align: center; font-family: Verdana;">6</p>
                                        </td>
                                        <td class="style45" style="width: 60; background: #fefbea; padding: 0in; height: .25in">
                                            <p>Ujjain</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p><span>33</span></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p>1013</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 84; background: #fefbea; padding: 0in; height: 23">
                                            <p>31,54,146</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 62; background: #fefbea; padding: 0in; height: .25in">
                                            <p> 21,11,516</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p>67</p>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 6; height: .25in">
                                        <td style="width: 44; background: #fefbea; padding: 0in; height: .25in"
                                            class="style19">
                                            <p align="center" class="MsoNormal2 style43" style="text-align: center; font-family: Verdana;">7</p>
                                        </td>
                                        <td class="style45" style="width: 60; background: #fefbea; padding: 0in; height: .25in">
                                            <p>Sagar</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p><span>36</span></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p>792</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 84; background: #fefbea; padding: 0in; height: .25in">
                                            <p>20,05,127</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 62; background: #fefbea; padding: 0in; height: .25in">
                                            <p>16,11,935</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: .25in">
                                            <p>80</p>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 7; height: .25in">
                                        <td style="background: #fefbea; padding: 0in 0in 0in 0in; height: .25in" width="44" class="style19">
                                            <p align="center" class="MsoNormal2 style43" style="text-align: center; font-family: Verdana;">8</p>
                                        </td>
                                        <td width="60" class="style45" style="background: #fefbea; padding: 0in 0in 0in 0in; height: .25in">
                                            <p>Rewa</p>
                                        </td>
                                        <td width="88" align="right" class="style45" style="background: #fefbea; padding: 0in; height: .25in; width: 73px">
                                            <p><span>26</span></p>
                                        </td>
                                        <td align="right" class="style45" style="background: #fefbea; padding: 0in 0in 0in 0in; height: .25in; width: 73px">
                                            <p>358</p>
                                        </td>
                                        <td align="right" class="style45" style="width: 84; background: #fefbea; padding: 0in; height: .25in">
                                            <p>19,61,448</p>
                                        </td>
                                        <td align="right" class="style45" style="background: #fefbea; padding: 0in; height: .25in; width: 62">
                                            <p>12,24,139</p>
                                        </td>
                                        <td width="88" align="right" class="style45" style="background: #fefbea; padding: 0in; height: .25in; width: 73px">
                                            <p>62</p>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 8; mso-yfti-lastrow: yes;">
                                        <td style="width: 44; background: #fefbea; padding: 0in; height: 12pt"
                                            class="style19">
                                            <p class="MsoNormal21" style="font-family: 'Times New Roman', Times, serif">
                                                <o:p><span class="style21"><strong><font size="3">&nbsp;</font></strong></span></o:p>
                                                <span class="style21"><strong><font size="3"></font></strong></span>
                                            </p>
                                        </td>
                                        <td class="style45" style="width: 60; background: #fefbea; padding: 0in; height: 12pt">
                                            <p><strong>Total</strong></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 12pt">
                                            <p><b>278</b></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 12pt">
                                            <p><strong>6779</strong></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 84; background: #fefbea; padding: 0in; height: 12pt">
                                            <p><b>2,28,64,200</b></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 62; background: #fefbea; padding: 0in; height: 12pt">
                                            <p><b>1,67,80,450</b></p>
                                        </td>
                                        <td align="right" class="style45" style="width: 73px; background: #fefbea; padding: 0in; height: 12pt">
                                            <p><strong>73</strong></p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                            <div id="container" style="width: 600px; height: 600px; margin: 0 auto"></div>

                            <!-- <h4><strong>Graphical Presentation</strong></h4>
                <img src="assets/img/graph/graph3.jpg" class="img-responsive"/> -->
                        </div>
                        <hr />


                        <div class="text_content">
                            <h4><strong>Depositor wise Warehouse Position of Commodity</strong></h4>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes">
                                        <td align="center" bgcolor="#FEFBEA" class="style19" width="61">
                                            <p class="style42">Sr. No.</p>
                                        </td>
                                        <td align="center" bgcolor="#FEFBEA" width="187">
                                            <p class="style42">Depositor</p>
                                        </td>
                                        <td align="center" bgcolor="#FEFBEA" width="110">
                                            <p align="center" class="style42">2012-13</p>
                                        </td>
                                        <td align="center" bgcolor="#FEFBEA" width="108">
                                            <div align="center"><span class="style42">2013-14 </span></div>
                                        </td>
                                        <td align="center" bgcolor="#FEFBEA" width="103">
                                            <div align="center"><span class="style42">2014-15 </span></div>
                                        </td>
                                        <td align="center" bgcolor="#FEFBEA" width="203">
                                            <div align="center"><span class="style42">2015-16 </span></div>
                                        </td>
                                        <td align="center" bgcolor="#FEFBEA" width="203">
                                            <div align="center"><span class="style42">2016-17</span></div>
                                        </td>
                                        <td align="center" bgcolor="#FEFBEA" width="203">
                                            <div align="center"><span class="style42">2017-18 </span></div>
                                        </td>
                                        <td align="center" bgcolor="#FEFBEA" width="203">
                                            <div align="center"><span class="style42">2018-19 </span></div>
                                        </td>
                                        <td align="center" bgcolor="#FEFBEA" width="203">
                                            <div align="center"><span class="style42">2019-20</span></div>
                                        </td>
                                         <td align="center" bgcolor="#FEFBEA" width="203">
                                            <div align="center"><span class="style42">2020-21</span></div>
                                        </td>
                                        <td align="center" bgcolor="#FEFBEA" width="203">
                                            <div align="center"><span class="style42">2021-22 (upto Sep.-2021) </span></div>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 1">
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="61" class="style19">
                                            <p align="center" class="style30">1</p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="187">
                                            <p align="center" class="style31">Cultivator</p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="110" class="style15">
                                            <p align="center" class="style30">00.63</p>
                                        </td>
                                        <td width="108" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.82</span></strong></div>
                                        </td>
                                        <td width="103" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.65</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.45</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.56</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.40</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.23</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.17</span></strong></div>
                                        </td>
                                          <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.10</span></strong></div>
                                        </td>
                                          <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.06</span></strong></div>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 2">
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="61" class="style19">
                                            <p align="center" class="style31"><strong>2</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="187">
                                            <p align="center" class="style31">
                                                <strong style="font-weight: 400">Co-operative
              Societies</strong>
                                            </p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="110" class="style15">
                                            <p align="center" class="style31"><strong>03.21</strong></p>
                                        </td>
                                        <td width="108" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">07.84</span></strong></div>
                                        </td>
                                        <td width="103" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">08.99</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">06.68</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">08.83</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">08.57</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">19.13</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">20.19</span></strong></div>
                                        </td>
                                         <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">13.03</span></strong></div>
                                        </td>
                                         <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">8.73</span></strong></div>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 3">
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="61" class="style19">
                                            <p align="center" class="style31"><strong>3</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="187">
                                            <p align="center" class="style31"><strong style="font-weight: 400">FCI</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="110" class="style15">
                                            <p align="center" class="style31"><strong>00.01</strong></p>
                                        </td>
                                        <td width="108" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.01</span></strong></div>
                                        </td>
                                        <td width="103" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.04</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.10</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.02</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.42</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.29</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.64</span></strong></div>
                                        </td>
                                         <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.27</span></strong></div>
                                        </td>
                                         <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.06</span></strong></div>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 4">
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="61" class="style19">
                                            <p align="center" class="style31"><strong>4</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="187">
                                            <p align="center" class="style31">
                                                <strong style="font-weight: 400">MP
              Civil Supplies Corp.</strong>
                                            </p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="110" class="style15">
                                            <p align="center" class="style31"><strong>93.24</strong></p>
                                        </td>
                                        <td width="108" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">88.67</span></strong></div>
                                        </td>
                                        <td width="103" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">87.34</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">89.60</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">88.46</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">88.17</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">73.93</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">73.65</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">83.33</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">89.88</span></strong></div>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 5">
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="61" class="style19">
                                            <p align="center" class="style31"><strong>5</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="187">
                                            <p align="center" class="style31">
                                                <strong style="font-weight: 400">Govt.
              Institutions</strong>
                                            </p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="110" class="style15">
                                            <p align="center" class="style31"><strong>00.96</strong></p>
                                        </td>
                                        <td width="108" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.59</span></strong></div>
                                        </td>
                                        <td width="103" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">01.50</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">02.08</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.49</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">01.35</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">05.88</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">04.92</span></strong></div>
                                        </td>
                                         <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">02.93</span></strong></div>
                                        </td>
                                         <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">01.07</span></strong></div>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 6">
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="61" class="style19">
                                            <p align="center" class="style31"><strong>6</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="187">
                                            <p align="center" class="style31"><strong style="font-weight: 400">Laghu Vanopaj Sangh</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="110" class="style15">
                                            <p align="center" class="style31"><strong>00.01</strong></p>
                                        </td>
                                        <td width="108" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.01</span></strong></div>
                                        </td>
                                        <td width="103" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.04</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.05</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.01</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.01</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.02</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.01</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.02</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.01</span></strong></div>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 7">
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="61" class="style19">
                                            <p align="center" class="style31"><strong>7</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="187">
                                            <p align="center" class="style31"><strong style="font-weight: 400">Traders</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="110" class="style15">
                                            <p align="center" class="style31"><strong>01.78</strong></p>
                                        </td>
                                        <td width="108" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">01.98</span></strong></div>
                                        </td>
                                        <td width="103" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">01.41</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">01.02</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">01.47</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.95</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.48</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.40</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.24</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.17</span></strong></div>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 8">
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="61" class="style19">
                                            <p align="center" class="style31"><strong>8</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="187">
                                            <p align="center" class="style31"><strong style="font-weight: 400">Others</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="110" class="style15">
                                            <p align="center" class="style31"><strong>00.16</strong></p>
                                        </td>
                                        <td width="108" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.08</span></strong></div>
                                        </td>
                                        <td width="103" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.03</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.02</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.16</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.03</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.07</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.02</span></strong></div>
                                        </td>
                                         <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.08</span></strong></div>
                                        </td>
                                         <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">00.02</span></strong></div>
                                        </td>
                                    </tr>


                                    <tr style="mso-yfti-irow: 9; mso-yfti-lastrow: yes">
                                        <td width="61" class="style32" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"></div>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="187">
                                            <p align="center" class="style31"><strong style="font-weight: 400">Total</strong></p>
                                        </td>
                                        <td style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt" width="110" class="style15">
                                            <p align="center" class="style31"><strong>100.00</strong></p>
                                        </td>
                                        <td width="108" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">100.00</span></strong></div>
                                        </td>
                                        <td width="103" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">100.00</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">100.00</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">100.00</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">100.00</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">100.00</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">100.00</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">100.00</span></strong></div>
                                        </td>
                                        <td width="203" class="style15" style="background: #f4f4f4; padding: 1.5pt 1.5pt 1.5pt 1.5pt">
                                            <div align="center"><strong><span class="style31">100.00</span></strong></div>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>


                            <h4><strong>Graphical Presentation</strong></h4>
                            <img src="assets/img/graph/graph2.jpg" class="img-responsive" />
                        </div>
                        <hr />

                        <div class="text_content">
                            <h4><strong>Capacity and Utilization</strong></h4>
                            <p>Average Capacity and Occupancy for the last fifteen Year. Figures in Mts.</p>
                            <table class="table table-bordered">
                                <tbody>
                                    <tr>
                                        <td width="216" rowspan="2" bgcolor="#FEFBEA" class="style22" style="width: 61; background: #999999; padding: 0in">
                                            <p class="MsoNormal41" style="text-align: center"><strong><span style="font-size: 10.0pt; color: white" class="style12">Year</span></strong></p>
                                        </td>
                                        <td colspan="3" bgcolor="#FEFBEA" style="background: #999999; padding: 0in 0in 0in 0in" class="style23">
                                            <p class="MsoNormal41" align="center" style="text-align: center"><strong><span style="font-size: 10.0pt; color: white" class="style12">Capacity</span></strong></p>
                                        </td>
                                        <td width="77" rowspan="2" bgcolor="#FEFBEA" class="style23" style="width: 75; background: #999999; padding: 0in">
                                            <p class="MsoNormal41" align="center" style="text-align: center"><span class="style12"><strong><font size="2" color="#FFFFFF">Total</font></strong></span></p>
                                        </td>
                                        <td width="106" rowspan="2" bgcolor="#FEFBEA" class="style23" style="width: 103; background: #999999; padding: 0in">
                                            <p class="MsoNormal41" align="center" style="text-align: center"><strong><span style="font-size: 10.0pt; color: white" class="style12">Occupancy</span></strong></p>
                                        </td>
                                        <td width="106" rowspan="2" bgcolor="#FEFBEA" class="style23" style="width: 103; background: #999999; padding: 0in">
                                            <p class="MsoNormal41" align="center" style="text-align: center"><strong><span style="font-size: 10.0pt; color: white" class="style12">Percentage</span></strong></p>
                                        </td>

                                    </tr>
                                    <tr style="mso-yfti-irow: 1">
                                        <td width="127" bgcolor="#FEFBEA" class="style22" style="width: 99; background: #999999; padding: 0in">
                                            <p class="MsoNormal41" style="text-align: center"><strong><span style="font-size: 10.0pt; color: white" class="style12">Owned</span></strong></p>
                                        </td>
                                        <td width="86" bgcolor="#FEFBEA" class="style23" style="width: 84; background: #999999; padding: 0in">
                                            <p class="MsoNormal41" align="center" style="text-align: center"><strong><span style="font-size: 10.0pt; color: white" class="style12">Hired</span></strong></p>
                                        </td>
                                        <td width="77" bgcolor="#FEFBEA" class="style23" style="width: 75; background: #999999; padding: 0in"><span class="style12"><strong><font size="2" color="#FFFFFF">JVS</font></strong></span></td>
                                    </tr>


                                    <tr style="mso-yfti-irow: 14; height: 15.0pt">
                                        <td style="background: #fefbea; padding: 0in; height: 20; text-align: center" class="style30">
                                            <p class="style47">2004-05</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">0964788</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">213012</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20; text-align: center" class="style30"><span class="style47">-</span></td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">1177800</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">922336</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">78.00</p>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 15; height: 15.0pt">
                                        <td style="background: #fefbea; padding: 0in; height: 20; text-align: center" class="style30">
                                            <p class="style47">2005-06</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">0977835</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">210081</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20; text-align: center" class="style30"><span class="style47">-</span></td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">1187916</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">945694</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">80.00</p>
                                        </td>
                                    </tr>
                                    <tr style="mso-yfti-irow: 16; height: 15.0pt">
                                        <td style="background: #fefbea; padding: 0in; height: 20; text-align: center" class="style30">
                                            <p class="style47">2006-07</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">1053028</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">117498</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20; text-align: center" class="style30"><span class="style47">-</span></td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">1170526</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">825948</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">71.00</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 20; text-align: center" class="style30">
                                            <p class="style47">2007-08</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">1102960</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">088377</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20; text-align: center" class="style30"><span class="style47">-</span></td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">1191337</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">890772</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">75.00</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 21; text-align: center" class="style30">
                                            <p class="style47">2008-09</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 21" class="style30">
                                            <p class="style47">1141145</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 21" class="style30">
                                            <p class="style47">256620</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 21; text-align: center" class="style30"><span class="style47">-</span></td>
                                        <td style="background: #fefbea; padding: 0in; height: 21" class="style30">
                                            <p class="style47">1397765</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 21" class="style30">
                                            <p class="style47">1181510</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 21" class="style30">
                                            <p class="style47">85.00</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 20; text-align: center" class="style30">
                                            <p class="style47">2009-10</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">1167110</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">245118</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">639047</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">2051275</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">1688013</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">82.00</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25" class="style30" style="background: #fefbea; padding: 0in; height: 20; text-align: center">
                                            <p class="style47">2010-11</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">1186619</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">503584</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">1259304</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">2949507</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">2535101</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 20" class="style30">
                                            <p class="style47">86.00</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 19; text-align: center" class="style30"><span class="style47">2011-12</span></td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">1311038</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">612496</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">1729557</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">3653091</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">2966361</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">81.00</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 19; text-align: center" class="style30"><span class="style47">2012-13</span></td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">1481129</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">624628</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">3368088</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">5473845</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">4631555</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 19" class="style30">
                                            <p class="style47">85.00</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p align="center" class="style47">2013-14</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p class="style47">1496636</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p class="style47">291400</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p class="style47">4070082</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p class="style47">5858118</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p class="style47">4761493</p>
                                        </td>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p class="style47">81.00</p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p align="center" class="style47">
                                                2014-15 (Own + Cap) 
                                            </p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">1818299</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">274622</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">5083642</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">7176562</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">5473418</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">76.00</p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p align="center" class="style47">
                                                2015-16 (Own + Cap + PEG) 
                                            </p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">2211127</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">210144</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">4900674</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">7321945</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">5095923</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">70.00</p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p align="center" class="style47">
                                                2016-17 (Own + Cap + PEG) 
                                            </p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">2276917</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">120235</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">3523858</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">5921010</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">3177225</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">54.00</p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p align="center" class="style47">
                                                2017-18 (Own + Cap + PEG) 
                                            </p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">2291779</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">415549</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">4609885</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">7317213</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">5897663</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">74.00</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p align="center" class="style47">
                                                2018-19 (Own + Cap + PEG) 
                                            </p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">2552073</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">871894</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">6192333</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">9616300</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">7890838</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">82.00</p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p align="center" class="style47">
                                                2019-20 (Own + Cap + PEG) 
                                            </p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">2757542</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">1433067</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">6845953</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">11036563</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">9261073</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">84.00</p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p align="center" class="style47">
                                                2020-21 (Own + Cap + PEG) 
                                            </p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">3387996</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">1779732</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">9811926</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">14979654</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">12445364</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">83.00</p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="background: #fefbea; padding: 0in; height: 23" class="style30">
                                            <p align="center" class="style47">
                                                2021-22 (Own + Cap + PEG) [upto Sep.-2021] 
                                            </p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">3680483</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">1481480</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">15018577</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">20480540</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">17904065</p>
                                        </td>
                                        <td class="style30" style="background: #fefbea; padding: 0in; height: 23">
                                            <p class="style47">87.00</p>
                                        </td>
                                    </tr>


                                </tbody>
                            </table>


                            <h4><strong>Graphical Presentation</strong></h4>
                            <img src="assets/img/graph/graph.jpg" class="img-responsive" />
                        </div>
                        <hr />






                        <%--<div class="text_content" id="finance">
                 <h4><strong>Financial Performance</strong></h4>
                    <img src="assets/img/graph/FinancialGraph.jpg" class="img-responsive" />
             </div>
                        --%>
                    </div>
                </div>

            </div>

        </div>
        <!-- /container -->
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="widget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript" src="https://code.highcharts.com/highcharts.js"></script>
    <script type="text/javascript" src="https://code.highcharts.com/modules/data.js"></script>
    <script type="text/javascript" src="https://code.highcharts.com/modules/drilldown.js"></script>

    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            // Create the chart

            var chart;

            chart = Highcharts.chart('container', {
                chart: {
                    type: 'pie'
                },
                setTitle: {
                    text: 'Subtitle'
                },
                //main series = as level 0
                series: [{
                    name: "Region",
                    colorByPoint: true,
                    data: [{
                        name: "BHOPAL",
                        y: 40,
                        drilldown: "BHOPAL"
                    },
                    {
                        name: "NARMADAPURAM",
                        y: 23,
                        drilldown: "NARMADAPURAM"
                    },
                    {
                        name: "GWAWLIOR",
                        y: 43,
                        drilldown: "GWAWLIOR"
                    },
                    {
                        name: "JABALPUR",
                        y: 49,
                        drilldown: "JABALPUR"
                    },
                    {
                        name: "INDORE",
                        y: 28,
                        drilldown: "INDORE"
                    },

                    {
                        name: "UJJAIN",
                        y: 33,
                        drilldown: "UJJAIN"
                    },

                    {
                        name: "SAGAR",
                        y: 36,
                        drilldown: "SAGAR"
                    },
                    {
                        name: "REWA",
                        y: 26,
                        drilldown: "REWA"
                    }
                    ]
                }],
                drilldown: {
                    series: [{

                        name: "BHOPAL",
                        id: "BHOPAL",
                        data: [{
                            name: "Total Capacity Covered",
                            y: 5097013,
                            color: 'gray'

                        },
                        {
                            name: "Vacant Capacity",
                            y: 1070490,
                            color: 'green'
                        }
                        ]
                    },
                    {

                        name: "NARMADAPURAM",
                        id: "NARMADAPURAM",
                        data: [{
                            name: "Total Capacity Covered",
                            y: 3015646,
                            color: 'gray'
                        },
                        {
                            name: "Vacant Capacity",
                            y: 706295,
                            color: 'green'
                        }
                        ]
                    },

                    {

                        name: "GWAWLIOR",
                        id: "GWAWLIOR",
                        data: [{
                            name: "Total Capacity Covered",
                            y: 1946647,
                            color: 'gray'
                        },
                        {
                            name: "Vacant Capacity",
                            y: 621484,
                            color: 'green'
                        }
                        ]
                    },

                    {

                        name: "JABALPUR",
                        id: "JABALPUR",
                        data: [{
                            name: "Total Capacity Covered",
                            y: 4422291,
                            color: 'gray'
                        },
                        {
                            name: "Vacant Capacity",
                            y: 1280036,
                            color: 'red'
                        }
                        ]
                    },

                    {

                        name: "INDORE",
                        id: "INDORE",
                        data: [{
                            name: "Total Capacity Covered",
                            y: 1261882,
                            color: 'gray'
                        },
                        {
                            name: "Vacant Capacity",
                            y: 232314,
                            color: 'red'
                        }
                        ]
                    },
                    {

                        name: "UJJAIN",
                        id: "UJJAIN",
                        data: [{
                            name: "Total Capacity Covered",
                            y: 3154146,
                            color: 'gray'
                        },
                        {
                            name: "Vacant Capacity",
                            y: 1042630,
                            color: 'green'
                        }
                        ]
                    },
                    {

                        name: "SAGAR",
                        id: "SAGAR",
                        data: [{
                            name: "Total Capacity Covered 1753650",
                            y: 2005127,
                            color: 'gray'
                        },
                        {
                            name: "Vacant Capacity 67710",
                            y: 393192,
                            color: 'green'
                        }
                        ]
                    },
                    {

                        name: "REWA",
                        id: "REWA",
                        data: [{
                            name: "Total Capacity Covered",
                            y: 1961448,
                            color: 'gray'
                        },
                        {
                            name: "Vacant Capacity",
                            y: 737309,
                            color: 'red'
                        }
                        ]
                    }

                    ]
                }
            });

            chart.setTitle({ text: 'Storage Capacity and Utilization Chart' });

        });
    </script>
</asp:Content>
