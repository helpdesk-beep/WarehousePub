<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Tachnical.master" AutoEventWireup="true" CodeFile="EmpCorner.aspx.cs" Inherits="EmpCorner" %>



<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">

    <div>
        <div class="panel-header">
            <div class="panel-title">
                <h3>HO Insecticide Entry,Trancfer Details</h3>
            </div>
            <hr />
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="mbox ">
                    <span>
                        <a href="#" target="_blank">
                                <span style="font-size: x-large; margin-left: -45px;">Entry by HO:-
                                     <asp:Label ID="lblAPHOE" runat="server"></asp:Label></span>
                        </a>
                        <br />
                        <a href="#" target="_blank"><span style="color: green; font-size: x-large; margin-left: -75px;">Trnasfer to RM:-
                                    <asp:Label ID="lblHOTRM" runat="server"></asp:Label></span> </a>
                        <br />
                        <a href="#" target="_blank"><span style="color: red; font-size: x-large;">Balance:- 
                                   <asp:Label ID="lblHOBalance" runat="server"></asp:Label></span></a>
                    </span>
                    <h4>Alluminium Phosphide Details</h4>

                </div>
            </div>

            <div class="col-md-4">
                <div class="mbox ">
                    <span>
                        <span style="font-size: x-large; margin-left: -45px;">Entry by HO:-
                                    <asp:Label ID="lblHOEMelaphion" runat="server"></asp:Label></span>
                        <br />
                        <a href="#" target="_blank"><span style="color: green; font-size: x-large; margin-left: -75px;">Trnasfer to RM:-
                                    <asp:Label ID="lblHOTRMMelaphion" runat="server"></asp:Label></span></a>
                        <br />
                        <a href="#" target="_blank"><span style="color: red; font-size: x-large;">Balance:- 
                                    <asp:Label ID="lblBalanceMelaphion" runat="server"></asp:Label></span></a>
                    </span>
                    <h4>Melaphion Details</h4>

                </div>
            </div>
            <div class="col-md-4">
                <div class="mbox ">
                    <span>
                        <span style="font-size: x-large; margin-left: -45px;">Entry by HO:-
                                    <asp:Label ID="lblHOEDeltamethrin" runat="server"></asp:Label></span>
                        <br />

                        <a href="#" target="_blank"><span style="color: green; font-size: x-large; margin-left: -75px;">Trnasfer to RM:-
                                    <asp:Label ID="lblHOTRMDeltamethrin" runat="server"></asp:Label></span></a>
                        <br />
                        <a href="#" target="_blank"><span style="color: red; font-size: x-large;">Balance:- 
                                    <asp:Label ID="lblHOBDeltamethrin" runat="server"></asp:Label></span></a>
                    </span>
                    <h4>Deltamethrin Details</h4>

                </div>
            </div>
        </div>
        <%-- RO Insectiside Details--%>
        <h3>RO Insecticide Entry,Trancfer Details</h3>
         
        <div class="row">
            <div class="col-md-4">
                <div class="mbox ">
                    <span>
                        <a href="#" target="_blank">
                                <span style="font-size: x-large; margin-left: -45px;">Entry by HO:-
                                     <asp:Label ID="lblAPHOE1" runat="server"></asp:Label></span>
                        </a>
                        <br />
                        <a href="#" target="_blank"><span style="color: green; font-size: x-large; margin-left: -75px;">Trnasfer to RM:-
                                    <asp:Label ID="lblHOTRM1" runat="server"></asp:Label></span> </a>
                        <br />
                        <a href="#" target="_blank"><span style="color: red; font-size: x-large;">Balance:- 
                                   <asp:Label ID="lblHOBalance1" runat="server"></asp:Label></span></a>
                    </span>
                    <h4>Alluminium Phosphide Details</h4>

                </div>
            </div>

            <div class="col-md-4">
                <div class="mbox ">
                    <span>
                        <span style="font-size: x-large; margin-left: -45px;">Entry by HO:-
                                    <asp:Label ID="lblHOEMelaphion1" runat="server"></asp:Label></span>
                        <br />
                        <a href="#" target="_blank"><span style="color: green; font-size: x-large; margin-left: -75px;">Trnasfer to RM:-
                                    <asp:Label ID="lblHOTRMMelaphion1" runat="server"></asp:Label></span></a>
                        <br />
                        <a href="#" target="_blank"><span style="color: red; font-size: x-large;">Balance:- 
                                    <asp:Label ID="lblBalanceMelaphion1" runat="server"></asp:Label></span></a>
                    </span>
                    <h4>Melaphion Details</h4>

                </div>
            </div>
            <div class="col-md-4">
                <div class="mbox ">
                    <span>
                        <span style="font-size: x-large; margin-left: -45px;">Entry by HO:-
                                    <asp:Label ID="lblHOEDeltamethrin1" runat="server"></asp:Label></span>
                        <br />

                        <a href="#" target="_blank"><span style="color: green; font-size: x-large; margin-left: -75px;">Trnasfer to RM:-
                                    <asp:Label ID="lblHOTRMDeltamethrin1" runat="server"></asp:Label></span></a>
                        <br />
                        <a href="#" target="_blank"><span style="color: red; font-size: x-large;">Balance:- 
                                    <asp:Label ID="lblHOBDeltamethrin1" runat="server"></asp:Label></span></a>
                    </span>
                    <h4>Deltamethrin Details</h4>

                </div>
            </div>
        </div>

         <h3>Branch Insecticide Receving,Consuption,Trancfer Details</h3>
         
        <div class="row">
            <div class="col-md-4">
                <div class="mbox ">
                    <span>
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank">
                                <span style="font-size: x-large; margin-left: 64px;">Opening Balance:-
                                     <asp:Label ID="lblOB1" runat="server"></asp:Label></span>
                        </a>
                        <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: green; font-size: x-large; margin-left: -75px;">Receipt From RM and Branch:-
                                    <asp:Label ID="lblRI1" runat="server"></asp:Label></span> </a>
                        <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: red; font-size: x-large;margin-left: 87px;">Consumption:- 
                                   <asp:Label ID="lblCon1" runat="server"></asp:Label></span></a>
                         <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: red; font-size: x-large;margin-left: -17px;">Trnasfer to JVS/Branch:- 
                                   <asp:Label ID="lblt1" runat="server"></asp:Label></span></a>
                        <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: red; font-size: x-large; margin-left: 150px;">Balance:- 
                                   <asp:Label ID="lblBalance1" runat="server"></asp:Label></span></a>
                    </span>
                    <h4>Alluminium Phosphide Details</h4>

                </div>
            </div>

            <div class="col-md-4">
                <div class="mbox ">
                    <span>
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank">
                                <span style="font-size: x-large; margin-left: 64px;"">Opening Balance:-
                                     <asp:Label ID="lblOB2" runat="server"></asp:Label></span>
                        </a>
                        <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: green; font-size: x-large; margin-left: -75px;">Receipt From RM and Branch:-
                                    <asp:Label ID="lblRI2" runat="server"></asp:Label></span> </a>
                        <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: red; font-size: x-large;margin-left: 87px;">Consumption:- 
                                   <asp:Label ID="lblCon2" runat="server"></asp:Label></span></a>
                         <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: red; font-size: x-large;margin-left: -17px;">Trnasfer to JVS/Branch:- 
                                   <asp:Label ID="lblt2" runat="server"></asp:Label></span></a>
                         <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: red; font-size: x-large; margin-left: 150px;">Balance:- 
                                   <asp:Label ID="lblbalalnce2" runat="server"></asp:Label></span></a>
                    </span>
                    <h4>Melaphion Details</h4>

                </div>
            </div>

            <div class="col-md-4">
                <div class="mbox ">
                    <span>
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank">
                                <span style="font-size: x-large; margin-left: 64px;">Opening Balance:-
                                     <asp:Label ID="lblOB3" runat="server"></asp:Label></span>
                        </a>
                        <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: green; font-size: x-large; margin-left: -75px;">Receipt From RM and Branch:-
                                    <asp:Label ID="lblRI3" runat="server"></asp:Label></span> </a>
                        <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: red; font-size: x-large;margin-left: 87px;">Consumption:- 
                                   <asp:Label ID="lblCon3" runat="server"></asp:Label></span></a>
                         <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: red; font-size: x-large;margin-left: -17px;">Trnasfer to JVS/Branch:- 
                                   <asp:Label ID="lblt3" runat="server"></asp:Label></span></a>
                         <br />
                        <a href="/Warehouse/Inspections/Technical/View_Region_Wise_Insecticide_report.aspx" target="_blank"><span style="color: red; font-size: x-large; margin-left: 150px;">Balance:- 
                                   <asp:Label ID="lblbalance3" runat="server"></asp:Label></span></a>
                    </span>
                    <h4>Deltamethrin Details</h4>

                </div>
            </div>
        </div>
        <div class="panel box-primary">
            <div class="panel-header">
                <div class="panel-title">
                    <h3>Online Inspection Reports</h3>
                </div>
                <hr />
            </div>
            <!-- /.box-header -->
            <div class="panel-body">

                <div class="row">
                    <div class="col-md-4">
                        <div class="mbox ">
                            <a href="#" target="_blank">
                                <span class="micon">
                                    <span style="font-size: xx-large;">
                                        <asp:Label ID="lbltiarm" runat="server"></asp:Label></span>
                                </span>
                                <h4>Total Inspections Alloted By RM Office</h4>

                            </a>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="mbox ">
                            <a href="#" target="_blank">
                                <span class="micon">
                                    <span style="font-size: xx-large;">
                                        <asp:Label ID="lblinspdone" runat="server"></asp:Label></span>
                                </span>
                                <h4>Total Inspection Done By Inspection Officer</h4>

                            </a>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="mbox ">
                            <a href="#" target="_blank">
                                <span class="micon">
                                    <span style="font-size: xx-large;">
                                        <asp:Label ID="lblpendinginsp" runat="server"></asp:Label></span>
                                </span>
                                <h4>Pending Inspection</h4>

                            </a>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
                <div class="panel-header">
                    <div class="panel-title">
                        <h3>Online Inspection Type Summary Reports</h3>
                    </div>
                    <hr />
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="mbox ">
                            <span>
                                <span style="font-size: x-large;">Total:-
                                    <asp:Label ID="lblgi" runat="server"></asp:Label></span>
                                <br />
                                <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Completed.aspx" target="_blank"><span style="color: green; font-size: x-large;">Complete:-
                                    <asp:Label ID="lblgic" runat="server"></asp:Label></span></a>
                                <br />
                                <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Completed_GIP.aspx" target="_blank"><span style="color: red; font-size: x-large;">Pending:- 
                                    <asp:Label ID="lblgip" runat="server"></asp:Label></span></a>
                            </span>
                            <h4>Total Complete General Inspections</h4>

                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="mbox ">
                            <a href="#" target="_blank">
                                <span>
                                    <span style="font-size: x-large;">Total:-
                                        <asp:Label ID="lblpvi" runat="server"></asp:Label></span>
                                    <br />
                                    <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Completed_PVC.aspx" target="_blank"><span style="color: green; font-size: x-large;">Complete:- 
                                        <asp:Label ID="lblpviC" runat="server"></asp:Label></span></a>
                                    <br />
                                    <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Completed_PVP.aspx" target="_blank"><span style="color: red; font-size: x-large;">Pending:-
                                        <asp:Label ID="lblpviP" runat="server"></asp:Label></span></a>
                                </span>
                                <h4>Total Complete Physical Verification</h4>

                            </a>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="mbox ">
                            <a href="#" target="_blank">
                                <span>
                                    <span style="font-size: x-large;">Total:-
                                        <asp:Label ID="lblboth" runat="server"></asp:Label></span>
                                    <br />
                                    <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Completed_BothC.aspx" target="_blank"><span style="color: green; font-size: x-large;">Complete:- 
                                        <asp:Label ID="lblbothc" runat="server"></asp:Label></span></a>
                                    <br />
                                    <a href="/Warehouse/Inspections/Technical/Rpt_Inspection_Completed_BothP.aspx" target="_blank"><span style="color: red; font-size: x-large;">Pending:-
                                        <asp:Label ID="lblbothP" runat="server"></asp:Label></span></a>
                                </span>
                                <h4>Total Complete Both</h4>

                            </a>
                        </div>
                    </div>
                </div>


            </div>
        </div>
        <!-- /.box-body -->

    </div>

</asp:Content>


