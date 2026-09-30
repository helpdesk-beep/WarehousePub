<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="State_Reports_Procurement_201920.aspx.cs" Inherits="StatePages_State_Reports_Procurement_201920" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            background-color: #4CAF50;
            color: white;
            border: 2px solid #4CAF50;
        }

            .button1:hover {
                background-color: #4CAF50;
                color: white;
            }

        .button2 {
            background-color: #F1B92D;
            color: white;
            border: 2px solid #F1B92D;
        }

            .button2:hover {
                background-color: #F1B92D;
                color: white;
            }

        .button3 {
            background-color: #f44336;
            color: white;
            border: 2px solid #f44336;
        }

            .button3:hover {
                background-color: #f44336;
                color: white;
            }

        .button6 {
            background-color: #E47D21;
            color: white;
            border: 2px solid #E47D21;
        }

            .button6:hover {
                background-color: #E47D21;
                color: white;
            }

        .auto-style1 {
            height: 22px;
        }
    </style>
    <fieldset style="width: 1000px; height: 470px; border: 2px solid navy;">
        <center>
            <div>

                <table style="width: 100%;">
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #008CBA; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Procurement Reports 
                        </td>
                    </tr>

                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Rabi Procurement 2026-27
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                                <asp:LinkButton ID="LinkButton66" runat="server" ForeColor="navy"
                                    Font-Bold="true" Font-Size="10pt" OnClick="LinkButton66_Click"> 1. &nbsp;Wheat Procurement 2026-27 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                                <asp:LinkButton ID="LinkButton65" runat="server" ForeColor="navy"
                                    Font-Bold="true" Font-Size="10pt" OnClick="LinkButton65_Click"> 2. &nbsp;Wheat Procurement District Wise 2026-27 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                             <asp:LinkButton ID="LinkButton67" runat="server" ForeColor="navy"
                                 Font-Bold="true" Font-Size="10pt" OnClick="LinkButton67_Click"> 3. &nbsp;Dalhan Procurement 2026-27 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                                <asp:LinkButton ID="LinkButton68" runat="server" ForeColor="navy"
                                    Font-Bold="true" Font-Size="10pt" OnClick="LinkButton68_Click"> 4. &nbsp;Dalhan Procurement District Wise 2026-27 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton69" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton69_Click"> 5. &nbsp;Moong Urad Procurement  2025-26</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Kharif Procurement 2025-26
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton64" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton64_Click"> 1. &nbsp;Paddy Procurement 2025-26</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Rabi Procurement 2025-26
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton59" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton59_Click"> 1. &nbsp;Wheat Procurement 2025-26 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                              <asp:LinkButton ID="LinkButton60" runat="server" ForeColor="navy"
                                  Font-Bold="true" Font-Size="10pt" OnClick="LinkButton60_Click"> 2. &nbsp;Wheat Procurement District Wise 2025-26 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                             <asp:LinkButton ID="LinkButton61" runat="server" ForeColor="navy"
                                 Font-Bold="true" Font-Size="10pt" OnClick="LinkButton61_Click"> 3. &nbsp;Dalhan Procurement 2025-26</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton62" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton62_Click"> 4. &nbsp;Dalhan Procurement District Wise 2025-26</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton63" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton63_Click"> 6. &nbsp;Moong Urad Procurement  2025-26</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Kharif Procurement 2024-25
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton58" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton58_Click"> 1. &nbsp;Paddy Procurement 2024-25</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Rabi Procurement 2024-25
                        </td>
                    </tr>


                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton50" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton50_Click"> 1. &nbsp;Wheat Procurement 2024-25 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton51" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton51_Click"> 2. &nbsp;Wheat Procurement District Wise 2024-25 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton52" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton52_Click"> 3. &nbsp;Dalhan Procurement 2024-25</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton53" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton53_Click"> 4. &nbsp;Dalhan Procurement District Wise 2024-25</asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton54" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton54_Click"> 5. &nbsp;Wheat Procurement 2024-25 With PMS</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton55" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton55_Click"> 6. &nbsp;Moong Urad Procurement  2024-25</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton56" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton56_Click"> 7. &nbsp;Dalhan e-WHR Submission Procurement 2024-25</asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton57" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton57_Click"> 8. &nbsp;Soya-Beens Procurement  2024-25</asp:LinkButton>
                        </td>
                    </tr>


                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Kharif Procurement 2023-24
                        </td>
                        <tr>
                            <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton47" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton37_Click"> 1. &nbsp;Paddy Procurement 2023-24</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton48" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton38_Click"> 2. &nbsp;Paddy Procurement 2023-24 With PMS</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton49" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton39_Click"> 3. &nbsp; Bajra Jowar Procurement 2023-24</asp:LinkButton>
                            </td>
                        </tr>
                    </tr>

                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Rabi Procurement 2023-24
                        </td>
                    </tr>


                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton40" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton40_Click"> 1. &nbsp;Wheat Procurement 2023-24 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton41" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton41_Click"> 2. &nbsp;Wheat Procurement District Wise 2023-24 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton42" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton42_Click"> 3. &nbsp;Dalhan Procurement 2023-24</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton43" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton43_Click"> 4. &nbsp;Dalhan Procurement District Wise 2023-24</asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton45" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton45_Click"> 5. &nbsp;Wheat Procurement 2023-24 With PMS</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton44" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton44_Click"> 6. &nbsp;Moong Urad Procurement  2023-24</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton46" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton46_Click"> 7. &nbsp;Dalhan e-WHR Submission Procurement 2023-24</asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Kharif Procurement 2022-23
                        </td>
                        <tr>
                            <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton37" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton37_Click"> 1. &nbsp;Paddy Procurement 2022-23</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton38" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton38_Click"> 2. &nbsp;Paddy Procurement 2022-23 With PMS</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton39" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton39_Click"> 3. &nbsp; Bajra Jowar Procurement 2022-23</asp:LinkButton>
                            </td>
                        </tr>
                    </tr>
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Rabi Procurement 2022-23
                        </td>
                    </tr>


                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton32" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton32_Click"> 1. &nbsp;Wheat Procurement 2022-23 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton33" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton33_Click"> 2. &nbsp;Wheat Procurement District Wise 2022-23 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton34" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton34_Click"> 3. &nbsp;Dalhan Procurement 2022-23</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton35" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton35_Click"> 4. &nbsp;Dalhan Procurement District Wise 2022-23</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton36" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton36_Click"> 5. &nbsp;Moong Urad Procurement  2022-23</asp:LinkButton>
                        </td>
                    </tr>


                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Kharif Procurement 2021-22
                        </td>
                    </tr>


                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton31" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton31_Click"> 1. &nbsp;District_by_Paddy Procurement 2021-22 [MPWLC] </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton26" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton26_Click"> 2. &nbsp;Paddy Procurement 2021-22 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton27" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton27_Click"> 3. &nbsp;Bajra Procurement 2021-22 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton28" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton28_Click"> 4. &nbsp;Jowar Procurement 2021-22 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton29" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton29_Click"> 5. &nbsp;Paddy Procurement 2021-22[Cap Pms] </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton30" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton30_Click"> 6. &nbsp;Paddy Procurement 2021-22 [MPWLC] </asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Rabi Procurement 2021-22
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton21" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton21_Click"> 1. &nbsp;Wheat Procurement 2021-22 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton22" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton22_Click"> 2. &nbsp;Dalhan Procurement 2021-22 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton23" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton23_Click"> 3. &nbsp;Dalhan e-WHR Submission Procurement 2021-22 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton24" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton24_Click"> 4. &nbsp;Moong,Udad Procurement WHR 2021-22 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton25" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton25_Click"> 5. &nbsp;Moong,Udad e-WHR Submission Procurement 2021-22</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Kharif Procurement 2020-21
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton18" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton18_Click"> 1. &nbsp;Paddy Procurement 2020-21 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton19" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton19_Click"> 2. &nbsp;Bajra Procurement 2020-21 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Left" style="border: 1px solid Gray;" class="auto-style1">&nbsp;
                    <asp:LinkButton ID="LinkButton20" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton20_Click"> 3. &nbsp;Jowar Procurement 2020-21 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Wheat Procurement 2020-21
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton15" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton15_Click"> 1. &nbsp;Wheat Procurement 2020-21 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton16" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton16_Click"> 2. &nbsp;Dalhan Procurement 2020-21 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton17" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton17_Click"> 3. &nbsp;Dalhan e-WHR Submission 2020-21 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Paddy Procurement 2019-20
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton12" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton12_Click"> 1. &nbsp;Paddy Procurement 2019-20 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton13" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton13_Click"> 2. &nbsp;Paddy Procurement 2019-20 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton14" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton14_Click"> 3. &nbsp;Paddy Procurement 2019-20 (MPWLC Only) </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E47D21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Wheat Procurement 2019-20
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton1_Click"> 1. &nbsp;Wheat Procurement 2019-20 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton4_Click"> 2. &nbsp;Wheat Procurement 2019-20 (District Wise) </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton9_Click"> 3. &nbsp;Wheat e-WHR 2019-20 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton11" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton11_Click"> 4. &#160;Wheat e-WHR 2019-20(MPWLC Only) </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="border: #FFFFFF; border-style: solid; border-width: 2px; background-color: #E49A21; height: 25px; color: #FFFFFF; font-weight: bolder; font-size: 16px" colspan="4">Dalhan Procurement 2019-20
                
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton2_Click"> 1. &nbsp;Dalhan e-WHR 2019-20 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton3_Click"> 2. &nbsp;Dalhan e-WHR 2019-20 (District Wise) </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton5_Click"> 3. &nbsp;Dalhan e-WHR 2019-20 (District Wise and Godown Type Wise) </asp:LinkButton>
                        </td>
                    </tr>

                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton6_Click"> 4. &nbsp;Dalhan e-WHR Online Submission Report 2019-20 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton7" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton7_Click"> 5. &nbsp;Arahar e-WHR 2019-20 (District Wise) </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton8_Click"> 6. &nbsp;Arhar e-WHR Online Submission Report 2019-20 </asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="border: border: 1px solid navy;">
                        <td colspan="4" align="Left" colspan="4" align="Left" style="border: #FFFFFF; border-style: solid; border-width: 1px; border-color: Gray; height: 22px;">&nbsp;
                    <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt" OnClick="LinkButton10_Click"> 7. &nbsp;Pending e-WHR for Print </asp:LinkButton>
                        </td>
                    </tr>
                </table>

            </div>
        </center>
    </fieldset>
</asp:Content>

