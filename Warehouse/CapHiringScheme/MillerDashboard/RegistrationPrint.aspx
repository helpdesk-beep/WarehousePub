<%@ Page Title="" Language="C#" MasterPageFile="~/CapHiringScheme/MillerDashboard/Miller.master" AutoEventWireup="true" CodeFile="RegistrationPrint.aspx.cs" Inherits="JVSMiller_RegistrationPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .bg-info{padding:5px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
   
    <!-- page-wrapper -->
        <div id="page-wrapper">
            <div class="row">
                <div class="col-md-12 ">
                   <input type="button" class="btn btn-warning" id="btnPrint" value="Print" />
                    <asp:Button ID="btnMakePayment" OnClick="btnMakePayment_OnClick" runat="server" class="btn btn-info" Text="Click Here To Make Payement" />
                </div>
            </div>
            <br />

            <div class="row" >
                <div class="col-md-12">
                    <asp:HiddenField ID="hdnMRegId" runat="server" />
                    
                    <asp:Repeater ID="rptRegPrint" runat="server">
                        <ItemTemplate>
                             <div class="panel box-primary">
                        <div class="panel-header with-border">
                            <h3 class="panel-title">
                                Personal Details</h3>
                            <hr />
                        </div>
                        <!-- /.box-header -->
                        <div class="panel-body">
                            <div class="row">
                                <asp:HiddenField ID="hdnMRegId" runat="server" />
                                <div class="form-group col-md-4">
                                    <label>
                                        Registration Id :
                                    </label>
                                    <span class="text-primary">
                                        <label class="text-primary"><%#Eval("Registration_ID")%></label>
                                        </span>
                                </div>
                                 <div class="form-group col-md-4">
                                    <label>
                                        Registration Date :
                                    </label>
                                    <span class="text-primary">
                                        <label class="text-primary"><%#Eval("CreateOn", "{0:dd/M/yyyy}")%></label>
                                        </span>
                                </div>


                                <div class="form-group col-md-4">
                                    <label>
                                        Miller Name :</label>
                                    <span class="text-primary">
                                        <label class="text-primary"><%#Eval("Miller")%></label>
                                        </span>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Mobile No. :
                                    </label>
                                    <span class="text-primary">
                                        <label class="text-primary"><%#Eval("Mobile")%></label>
                                        </span>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Email :</label>
                                    <span class="text-primary">
                                        <label class="text-primary"><%#Eval("Email")%></label>
                                        </span>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Aadhar No. :
                                    </label>
                                    <span class="text-primary">
                                        <label class="text-primary"><%#Eval("Aadhar")%></label>
                                        </span>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Pan No. :
                                    </label>
                                    <span class="text-primary">
                                        <label class="text-primary"><%#Eval("Pan")%></label>
                                    </span>
                                </div>
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <div class="panel box-primary">
                        <div class="panel-header with-border">
                            <h3 class="panel-title">
                                मिलर के मिल का विवरण
                            </h3>
                            <hr />
                        </div>
                        <!-- /.box-header -->
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label>
                                        Mill Name :</label>
                                    <label class="text-primary"><%#Eval("Mill_Name")%></label>
                                </div>
                                
                                <div class="form-group col-md-4">
                                    <label>
                                        Capacity of Rice Miller (in MT per day) :</label>
                                    <label class="text-primary"><%#Eval("Rice_Capacity", "{0:#}")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Office Contact No./ Mobile No. :</label>
                                    <label class="text-primary"><%#Eval("Office_Contact")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Landmark Near Mills :</label>
                                    <label class="text-primary"><%#Eval("Mill_Landmark")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>District :</label>
                                    <label class="text-primary"><%#Eval("District_Name")%></label>
                                    
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Tehsil :</label>
                                    <label class="text-primary"><%#Eval("Tehsil_Name")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Block :</label>
                                    <label class="text-primary"><%#Eval("Block_Name")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Nearest Branch of MPWLC :
                                    </label>
                                    <label class="text-primary"><%#Eval("BranchName")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Distance from nearest branch of MPWLC (in KM):</label>
                                    <label class="text-primary"><%#Eval("Near_Distance")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Mills/ Office Address with Postal Address :</label>
                                    <label class="text-primary"><%#Eval("Mill_Office_Address")%></label>
                                </div>
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <div class="panel box-primary">
                        <div class="panel-header with-border">
                            <h3 class="panel-title">
                                 जिला उद्योग से मिलिंग हेतु प्राप्त रजिस्ट्रेशन का विवरण
                            </h3>
                            <hr />
                        </div>
                        <!-- /.box-header -->
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label>
                                        Registration No :</label>
                                    <label class="text-primary"><%#Eval("Dist_Ind_RegNo")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Issue Date :
                                    </label>
                                    <label class="text-primary"><%#Eval("RegNo_Issue_Date", "{0:dd/M/yyyy}")%> </label>
                                </div>
                                <div class="form-group col-md-4">
                                    <%--<label>
                                        Attached Document
                                    </label>--%>
                                    
                                </div>
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <div class="panel box-primary">
                        <div class="panel-header with-border">
                            <h3 class="panel-title">
                                <%#Eval("Mpscsc_Markfed")%> से मिलिंग हेतु किये गए एग्रीमेंट का विवरण</h3>
                            <hr />
                        </div>
                        <!-- /.box-header -->
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label>
                                         Miller Registration Id  :</label>
                                    <label class="text-primary"><%#Eval("Agree_Miller_Id")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Agreement Date :
                                    </label>
                                    <label class="text-primary"><%#Eval("Agree_Date", "{0:dd/M/yyyy}")%> </label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Agreement Capacity (in MT) :</label>
                                    <label class="text-primary"><%#Eval("Agree_Capacity", "{0:#}")%></label>
                                </div>
                                
                                 </div>
                                 
                                <h3 class="panel-title col-md-12">
                                
                                  <%#Eval("Mpscsc_Markfed")%> से धान (Paddy) को स्‍वतंत्र रूप से रखने की क्षमता का विवरण
                                </h3>
                                <hr />
                                <div class="form-group col-md-4">
                                    <label>Paddy Capacity (in MT) :</label>
                                    <label class="text-primary"><%#Eval("Paddy_Capacity", "{0:#}")%> </label>
                                </div>
                           </div>
                            <!-- /.row -->
                        </div>
                       
                    <div class="panel box-primary">
                        <div class="panel-header with-border">
                            <h3 class="panel-title">
                               Miller Incharge/Manager Details
                            </h3>
                            <hr />
                        </div>
                        <!-- /.box-header -->
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label>
                                        Authorised Person :</label>
                                     <label class="text-primary"><%#Eval("Incharge_Peson")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Designation :
                                    </label>
                                    <label class="text-primary"><%#Eval("Incharge_Post")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Email ID :</label>
                                    <label class="text-primary"><%#Eval("Incharge_Email")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Mobile No. :</label>
                                    <label class="text-primary"><%#Eval("Incharge_Mobile")%></label>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Authorised Person Address with Postal Address :</label>
                                    <label class="text-primary"><%#Eval("Incharge_Address")%></label>
                                </div>
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <div class="panel box-primary">
                        <div class="panel-header with-border">
                            <h3 class="panel-title">
                                Geogrophical Information
                            </h3>
                            <hr />
                        </div>
                        <!-- /.box-header -->
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group col-md-6">
                                    <label>
                                        Mills Latitude :</label>
                                   <label class="text-primary"><%#Eval("Mill_Lat")%></label>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>
                                        Mills Longitude :
                                    </label>
                                    <label class="text-primary"><%#Eval("Mill_Long")%></label>
                                </div>
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.box-body -->
                    </div>
                     
                     <div class="panel box-primary">
                        <div class="panel-header with-border">
                            <h3 class="panel-title">
                                Registration Fee Payment
                            </h3>
                            <hr />
                        </div>
                        <!-- /.box-header -->
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label>Registration Charge :</label>
                                    <label class="text-primary">100 /- </label>
                                  </div>
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.box-body -->
                    </div>   

                        </ItemTemplate>
                    
                    </asp:Repeater>

                    
                    <h4 class=" text-center red"> पंजीयन राशि - प्रत्येक राईस मिलर द्वारा कैप किराये पर लेने हेतु ऑनलाइन आवेदक करने पर पंजीयन शुल्क राशि रुपए १०० /- ऑनलाइन भुगतान करना होगा |  </h4>
                         
                        


                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /page-wrapper --> 




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">

    <script type="text/javascript">

        $(function () {
            $("#btnPrint").click(function () {
                var contents = $("#page-wrapper").html();
                var frame1 = $('<iframe />');
                frame1[0].name = "frame1";
                frame1.css({ "position": "absolute", "top": "-1000000px" });
                $("body").append(frame1);
                var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
                frameDoc.document.open();
                //Create a new HTML document.
                frameDoc.document.write('<html><head><title>Book Capacity Details</title>');
                frameDoc.document.write('</head><body>');
                //Append the external CSS file.
                frameDoc.document.write('<link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.css">');
                //Append the DIV contents.
                frameDoc.document.write(contents);
                frameDoc.document.write('</y></html>');
                frameDoc.document.close();
                setTimeout(function () {
                    window.frames["frame1"].focus();
                    window.frames["frame1"].print();
                    frame1.remove();
                }, 500);
            });
        });

//        function printDiv() {

//            var divContents = document.getElementById("page-wrapper").innerHTML;

//            var a = window.open('', '', 'height=500, width=500');

//            a.document.write('<html>');

//            a.document.write('<body > <h1>Miller Registration Details</h1> <br>');

//            a.document.write(divContents);

//            a.document.write('</body></html>');

//            a.document.close();

//            a.print();

        //        } 


    </script> 
</asp:Content>

