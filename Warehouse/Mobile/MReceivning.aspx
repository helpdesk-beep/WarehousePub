<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/MMaster.master" AutoEventWireup="true" CodeFile="MReceivning.aspx.cs" Inherits="Mobile_MReceivning" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
  <div id="divContainer">
    <script src="assets/js/jquery.js" type="text/javascript"></script>
      <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
<script src="http://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.2/modernizr.js"></script>
      <script type="text/javascript">
          $(window).load(function () {
              // Animate loader off screen
              $(".se-pre-con").fadeOut("slow");;
          });
          </script>
      <script type="text/javascript">
          var datefield = document.createElement("input")
          datefield.setAttribute("type", "date")
          if (datefield.type != "date") { //if browser doesn't support input type="date", load files for jQuery UI Date Picker
              document.write('<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />\n')
              document.write('<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"><\/script>\n')
              document.write('<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"><\/script>\n')
          }
</script>
      <script>
          if (datefield.type != "date") { //if browser doesn't support input type="date", initialize date picker widget:
              jQuery(function ($) { //on document.ready
                  $('#txtdod').datepicker();
              })
          }
</script>
      <style type="text/css">
          .no-js #loader { display: none;  }
.js #loader { display: block; position: absolute; left: 100px; top: 0; }
.se-pre-con {
	position: fixed;
	left: 0px;
	top: 0px;
	width: 100%;
	height: 100%;
	z-index: 9999;
	background: url(images/Preloader_8.gif) center no-repeat #fff;
}

      </style>
      <div class="se-pre-con">
           </div>
    <div style="background-color: #CC0000">
<p> <label for="REciving" class="bebas" style="font-size: medium">Receiving Details</label></p>
    </div>
    
      <p>
       
                	<label for="password" class="bebas">Depositor Type</label>
          <asp:DropDownList ID="ddldepositortype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldepositortype_SelectedIndexChanged"></asp:DropDownList>
                </p>
      <p>
       
                	<label for="password" class="bebas">Depositor/जमाकर्ता</label>
          <asp:DropDownList ID="ddlDepositor" runat="server" ></asp:DropDownList>
                </p>
      <p>
       
                	<label for="password" class="bebas">Deposit from/डिपॉज़िट फ्रोम</label>
          <asp:DropDownList ID="ddlArrival_Source" runat="server"  >
              <asp:ListItem Value="01">Procurement</asp:ListItem>
              <asp:ListItem Selected="True" Value="02">Other Depot</asp:ListItem>
              <asp:ListItem Value="03">From FCI</asp:ListItem>
              <asp:ListItem Value="04">Levy Rice</asp:ListItem>
              <asp:ListItem Value="05">CMR</asp:ListItem>
              <asp:ListItem Value="06">Other Source</asp:ListItem>
              <asp:ListItem Value="07">From RailHead</asp:ListItem>
              <asp:ListItem Value="08">Loss / Gain</asp:ListItem>
              <asp:ListItem Value="09">Tender Purchase(by Rack)-Sugar/Salt</asp:ListItem>
              <asp:ListItem Value="10">Tender Purchase(by Road)-Sugar/Salt</asp:ListItem>
              <asp:ListItem Value="11">From Marketing Federation</asp:ListItem>
              <asp:ListItem Value="12">Transfer By Road</asp:ListItem>
              <asp:ListItem Value="13">Transfer By Rack</asp:ListItem>
              <asp:ListItem Value="14">D.G.S. &amp; D.(Gunny bags reciept)</asp:ListItem>
                    </asp:DropDownList>
                </p>
     <p>
       
                	<label for="password" class="bebas">Challan No/चालान नं</label>
                    <input id="txtchallan" type="text" runat="server" class="radius2"/>
        
                </p>
    <p>
         
        <asp:Button ID="btnsrch" CssClass="btn-primary" runat="server" Text="Search" OnClick="btnsrch_Click" OnClientClick="this.disabled = true; this.value='Please wait'" UseSubmitBehavior="false" />
    </p>

        
      <p>
                	<label for="password" class="bebas">Truck No/ट्रक नं</label>
                    
                     <input id="txttrucknum" type="text" runat="server" class="radius2"/>
                </p>
   <p>
                	<label for="password" class="bebas">Commodity</label>
                    
       <asp:DropDownList ID="ddlCommodity" runat="server"></asp:DropDownList>
                </p>
      <p>
                	<label for="Date" class="bebas">Date of Deposit</label>
                  <%--  <input type="date" id="txtdod"  class="radius2" />--%>

                    <%--<asp:TextBox ID="txtdod"  runat="server" class="radius2"  ></asp:TextBox>--%>
         <input type="text" required autofocus placeholder="Date"
    class="txt-input txt-input-username" ID="txtdod" runat="server"/>
                  <%--  <asp:CalendarExtender ID="txtdod_CalendarExtender" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" runat="server" Enabled="True" TargetControlID="txtdod">
                    </asp:CalendarExtender>--%>

                </p>

    <script>
        if (!Modernizr.touch || !Modernizr.inputtypes.date) {
            $('input[type=date]')
                .attr('type', 'text')
                .datepicker({
                    // Consistent format with the HTML5 picker
                    dateFormat: 'dd-mm-yyyy'
                });
        }
</script>
       <p>
                	<label for="Category" class="bebas">Category</label>
                    
           <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
                </p>
      <p>
                	<label for="cropyear" class="bebas">Crop Year</label>
                    
           <asp:DropDownList ID="ddlcropyear" runat="server"></asp:DropDownList>
                </p>
      <p>
                	<label for="transpoter" class="bebas">Transpoter</label>
                    
           <asp:DropDownList ID="ddlTransporter" runat="server"></asp:DropDownList>
                </p>
     <p>

                	<label for="password"  class="bebas">WCM No</label>
                    
                    <input id="txtwcmno" type="text" runat="server" class="radius2"/>
                </p>
     <p>
                	<label for="password" class="bebas">Mode of Weigment</label>
                    
         <asp:DropDownList ID="ddlmow" runat="server">
               <asp:ListItem>10%</asp:ListItem>
                                                <asp:ListItem>100%</asp:ListItem>
         </asp:DropDownList>
                </p>
     <p>
                	<label for="number" class="bebas">Number of Bags Received</label>
                    
         <input id="txtnumbags" runat="server"  type="text" />
                </p>
     <p>
                	<label for="password" class="bebas">Qty Received</label>
                    
                    <input id="qtyrec" runat="server"  type="text" />
                </p>
     <p>
                	<label for="password" class="bebas">Godown</label>
                    
         <asp:DropDownList ID="ddlgodown" runat="server"></asp:DropDownList>
                </p>
   
      <asp:Button ID="btnsubmit" CssClass="btn-primary" runat="server" Text="Receive" OnClick="btnsubmit_Click" OnClientClick="this.disabled = true; this.value='Please wait'" UseSubmitBehavior="false"  />
      &nbsp;&nbsp;&nbsp;&nbsp;
     
      <asp:Button ID="btncancel" runat="server" class="btn-danger" Text="Cancel" OnClick="btncancel_Click" />
       
      </div>
     <script type="text/javascript">
         $(function () {
             $('#btnsubmit').click(function () {
                 var Depositortype = $('#ddldepositortype').val();
                 var Depositor = $('#ddlDepositor').val();
                 var RecQty = $('#qtyrec').val();
                 if (name != '' && subject != '' && body) {
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         url: "MReceivning.aspx/InsertData",
                         data: "{'DepositorType':'" + Depositortype + "','DepositorID':'" + Depositor + "','QtyRec':'" + RecQty + "'}",
                         dataType: "json",
                         success: function (data) {
                             var obj = data.d;
                             if (obj == 'true') {
                                 $('#ddldepositortype').val('');
                                 $('#ddlDepositor').val('');
                                 $('#qtyrec').val('');
                                 $('#lblmsg').html("Details Submitted Successfully");
                                 window.location.reload();
                             }
                         },
                         error: function (result) {
                             alert("Error");
                         }
                     });
                 }
                 else {
                     alert('Please enter all the fields')
                     return false;
                 }
             })
         });
</script>
</asp:Content>

