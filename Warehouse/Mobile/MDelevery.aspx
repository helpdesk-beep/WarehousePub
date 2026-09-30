<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/MMaster.master" AutoEventWireup="true" CodeFile="MDelevery.aspx.cs" Inherits="Mobile_MDelevery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
<div class="row-fluid">
<div class="span5">
    <div style="background-color: #CC0000">
<p> <label for="REciving" class="bebas" style="font-size: medium">Delevery Details</label></p>
    </div>
    

    
     <p>
       
                	<label for="password" class="bebas">Challan No/चालान नं</label>
         <input id="Text1" type="text" />
                </p>
        
      <p>
                	<label for="password" class="bebas">Truck No/ट्रक नं</label>
                    
          <input id="Text2" type="text" />
                </p>
   <p>
                	<label for="password" class="bebas">Commodity</label>
                    
       <input id="Text3" type="text" />
                </p>
      <p>
                	<label for="password" class="bebas">Date of Deposit</label>
                    <input type="date" class="radius2" id="txtdod"/>
                    <%--<asp:TextBox ID="txtdod" runat="server" class="radius2" Width="95%" ></asp:TextBox>--%>
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
                	<label for="password" class="bebas">WCM No</label>
                    
         <input id="Text4" type="text" />
                </p>
     <p>
                	<label for="password" class="bebas">Mode of Weigment</label>
                    
         <asp:dropdownlist runat="server"></asp:dropdownlist>
    
                     </p>
    </div>
    <div class="span4">
     <p>
                	<label for="password" class="bebas">Number of Bags Received</label>
                    <input id="Text5" type="text" />
                </p>
     <p>
                	<label for="password" class="bebas">Qty Received</label>
                    
                    <input id="Text6" type="text" />
                </p>
     <p>
                	<label for="password" class="bebas">Godown</label>
                    
                    <input id="Text7" type="text" />
                </p>
    <input id="btnsubmit" type="button" class="btn-primary" value="Receive" />&nbsp;&nbsp;&nbsp;&nbsp;
     <input id="btncancel" type="button" class="btn-danger" value="Cancel" />

    </div>

  
    </div>
        </div>
</asp:Content>

