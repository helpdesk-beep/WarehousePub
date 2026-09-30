<%@ Page Title="" Language="C#" MasterPageFile="~/CapHiringScheme/MillerDashboard/Miller.master" AutoEventWireup="true" CodeFile="CapBookPrint.aspx.cs" Inherits="CapHiringScheme_MillerDashboard_CapBookPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    
     <!--invoice wrapper -->
     <div id="page-wrapper">
      <div class="row">
                <div class="col-md-12 ">
                    <input type="button" class="btn btn-warning" id="btnPrint" value="Print" />
                </div>
            </div>
      <br />
      <div class="row">
         <div class="panel box-primary" id="printData">
          
        <div class="invoice_wrapper">
          
          <div class="row">
            
            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 company_details">
            
             <img src="../assets/img/logo_with_text.png" style="width:300px;margin-bottom:1em;">
             <address>
                <strong>MP Warehousing and Logistics Corporation,</strong><br>Office Complex Block 'A' Gautam Nagar Bhopal<br>Email : helpdeskmpwlc@gmail.com<br>
                <span>Phone: 91-755-2600505 Fax: 2600384</span>
             </address>
            </div>

            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 text-right invoice_details">
              <h3>Cap Receipt</h3>
               <span>Cap Reserve Date: <asp:Literal ID="ltlResrvDt" runat="server"></asp:Literal> </span>
              <%--<span>Invoice: #456</span><br>
              <span>Invoice Date: 15/01/2015</span><br>
              <span>Due Date: 15/02/201</span>--%>
            </div>
          </div>

          <div class="row">
            <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 pull-left">
              <div class="panel panel-default height">
                <div class="panel-heading caps">Miller Details</div>
                  <div class="panel-body">
                    <strong>Miller : </strong><asp:Literal ID="litMill" runat="server"></asp:Literal> <br>
                    <strong>Registration ID : </strong> <asp:Literal ID="litRegId" runat="server"></asp:Literal><br>
                    <%--<strong>Address:</strong> 104 Folsom Ave, Suite 600<br>--%>
                    <strong>Email : </strong> <asp:Literal ID="ltlEmail" runat="server"> </asp:Literal>
                    <strong>Phone : </strong> <asp:Literal ID="litPhone" runat="server"> </asp:Literal>
                  </div>
              </div>
            </div>

            <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 pull-right">
              <div class="panel panel-default height">
                <div class="panel-heading caps">Payment Information</div>
                  <div class="panel-body">
                    <strong>Payment :</strong> Pending<br>
                  </div>
              </div>
            </div>

            
            
          </div>


          <table class="table table-striped order_table">
            <thead>
              <tr>
                <th>#</th>
                <th>CAP NAME</th>
                <th>FULL / PARTIAL</th>
                <th>RESERVE CAPACITY (MT)</th>
                <th class="col-md-1 col-lg-1" align="right" >AMOUNT</th>                                          
              </tr>
            </thead>   
            <tbody>
            <asp:Repeater ID="rptBookGDCap" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#Container.ItemIndex+1%></td>
                        <td><%#Eval("Godown_Name")%></td>
                        <td><%#Eval("BookCapacityType")%></td>
                        <td>
                       <%-- <%#Eval("BookGodownCapacity")%>--%>
                       <%# Eval("BookGodownCapacity", "{0:#}")%>
                      
                        </td>
                        <td align="right">
                       <%-- <%#Eval("BookGodownAmount")%>--%>
                         <i class="fa fa-inr" aria-hidden="true"></i>  <%# Eval("BookGodownAmount", "{0:#}")%> 
                        </td>
                                                              
                      </tr>
                </ItemTemplate>
            
            </asp:Repeater>
              

                

              

              <tr class="amount_row">
                <td colspan="3" class="brd0"><big><b>Total</b></big></td>
                
                <td class="text-left brd0"><big><b>
                    <asp:Literal ID="ltTtlCap" runat="server"></asp:Literal></b></big></td>
                <td class="text-right brd0"><big><b>
                    <i class="fa fa-inr" aria-hidden="true"></i>&nbsp;<asp:Literal ID="ltTtlAmt" runat="server"></asp:Literal> </b></big></td>
              </tr>


              <tr style="background: #fff;text-align: right;">
                <td colspan="4" class="brd0">
                 <%-- <button type="submit" class="btn btn-success"><i class="fa fa-download"></i>&nbsp;Download PDF</button>--%>
                </td>
                <td class="text-left brd0">
                  <%--<button type="submit" class="btn btn-warning"  onclick="printDiv()"><i class="fa fa-print"></i>&nbsp;Print</button>--%>
                
                </td>
              </tr>

            </tbody>
        </table>

       


         <div class="row">
         <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-left">
            <h4 class="panel-title text-center red"> अमानती राशि-प्रत्येक राईस मिलर को निगम की स्वनिर्मित कैप क्षमता ऑनलाइन आवेदन करने के दौरान राशि रुपए १०/- प्रति मे.टन अमानती राशि के रूप में जमा करनी होगी |  </h4><br />
            
              <div class="invoice_footer">
                <span class="text-muted">If you have any question about this invoice, please contact</span><br>
                <p class="text-muted">[MPWLC, 91-755-2600505, helpdeskmpwlc@gmail.com]</p>
               <strong>Thank you for your business !</strong>
             </div>
            

           </div>

         </div>

         



        
          
        </div>
        </div>
      </div>
     
   
    </div>
    
  <!-- invoice wrapper -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">

<script type="text/javascript">
    $(function () {
        $("#btnPrint").click(function () {
            var contents = $("#printData").html();
            var frame1 = $('<iframe />');
            frame1[0].name = "frame1";
            frame1.css({ "position": "absolute", "top": "-1000000px" });
            $("body").append(frame1);
            var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
            frameDoc.document.open();
            //Create a new HTML document.
            frameDoc.document.write('<html><head><title>Reserve Capacity Reciept</title>');
            frameDoc.document.write('</head><body>');
            //Append the external CSS file.
            frameDoc.document.write('<link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.css">');
            frameDoc.document.write('<link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">');
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
</script>

</asp:Content>

