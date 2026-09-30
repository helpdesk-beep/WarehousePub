<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="main.master"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<asp:Content ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="banner" runat="server">

    <section class="banner_wrapper">
        <div class="overlay_bg">
         <div class="container">
            <div class="col-xs-12 col-sm-6 col-md-8 col-lg-9">
               <div id="carousel-example-generic" class="carousel slide slider" data-ride="carousel">
                  <ol class='carousel-indicators'>
                    <li data-target='#carousel-example-generic' data-slide-to='0' class='active'></li>
                    <li data-target='#carousel-example-generic' data-slide-to='1' class=''></li>
                    <li data-target='#carousel-example-generic' data-slide-to='2' class=''></li>
                    <li data-target='#carousel-example-generic' data-slide-to='3' class=''></li>
                    <li data-target='#carousel-example-generic' data-slide-to='4' class=''></li>
                    <li data-target='#carousel-example-generic' data-slide-to='5' class=''></li>
                    <li data-target='#carousel-example-generic' data-slide-to='6' class=''></li>
                  </ol>
                  <div class="carousel-inner" style="height:360px;">
                      <div class="item active">
                        <img src="assets/New/img/BandaSagar.jpg" style="width:100%;">
                      </div>

                      <div class="item">
                        <img src="assets/New/img/Indore.jpg"  style="width:100%;">
                      </div>

                      <div class="item">
                        <img src="assets/New/img/MajgawanSagar.jpg"  style="width:100%;">
                      </div>

                      <div class="item">
                        <img src="assets/New/img/RehliSagar.jpg"  style="width:100%;">
                      </div>

                      <div class="item">
                        <img src="assets/New/img/Satna.jpg"  style="width:100%;">
                      </div>

                      <div class="item">
                        <img src="assets/New/img/Sehore.jpg"  style="width:100%;">
                      </div>

                      <div class="item">
                        <img src="assets/New/img/Shahnagar.jpg"  style="width:100%;">
                      </div>
                  
                    </div>
                    <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                      <i class="fa fa-angle-left" aria-hidden="true"></i>
                    </a>
                    <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
                        <i class="fa fa-angle-right " aria-hidden="true"></i>
                    </a>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">
              <div class="well minister_wrap">
                <div class="row">
                    <div class="col-md-6 center-block">
                         <div class="thumbnail text-center">
                            <img class="img-center img-circle" title="Food Minister of Food Civil Supplies and Consumer Protection." src="assets/New//img/minister1.jpg" class="img-rounded img-responsive" style="width:80px;height:80px;">
                            <strong>Shri Bisahulal Singh</strong><br>
                           <small class="caps">Food Minister</small> 
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="thumbnail text-center">
                        <img src="assets/New/img/minister2.jpg" class="img-circle img-responsive" style="width:80px;height:80px;">
                        <strong>Shri Tarun Kumar Pithode</strong><br>
                         <small class="caps">MD</small>
                         </div>
                    </div>

                    <div class="col-md-6">
                        <div class="thumbnail text-center">
                        <img src="assets/New/img/Chairman.jpeg" class="img-rounded img-circle" style="width:80px;height:80px;">
                        <strong>Shri Rahul Singh</strong><br>
                        <small class="caps">Chairman</small>
                        </div>
                    </div>

                 
                </div>

              </div>
            </div>
         </div>

         <div id="count">
            <div class="container counters">
            <div class="carousel slide" id="myCarousel">
            <div class="carousel-inner">
            <div class="item active">
            <div class="row-fluid">
            <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
            <div class="count-div">
            <p>Head Office</p>

            <p class="org"><span class="count">AT BHOPAL</span> </p>
            </div>
            </div>

            <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
            <div class="count-div">

            <p class="gree"><span class="count">8</span></p>
            <p>Regional Office</p>
            </div>
            </div>

            <div class="col-xs-12 col-sm-4 col-md-4 text-center">
            <div class="count-div">
            <p class="blu"><span class="count">270</span> </p>
             <p>Branch Office</p>
            </div>
            </div>
            </div>
            </div>

            <%--<div class="item">
            <div class="row-fluid">
              <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
              <div class="count-div">
                <p>MPWLC Godown</p>
                <p class="org"><span class="count"><asp:Label ID="lblowndgdn" runat="server"></asp:Label> </span> </p>
              </div>
              </div>

              <div class="col-xs-12 col-sm-4 col-md-4 text-center ">
              <div class="count-div">
                <p>Private Godown</p>
                <p class="gree"><span class="count"><asp:Label ID="lblpg" runat="server"></asp:Label></span> </p>
              </div>
              </div>

              <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                <div class="count-div">
                  <p>JointVenture(JV)</p>
                  <p class="blu"><span class="count"><asp:Label ID="lblJVS" runat="server"></asp:Label></span> </p>
                </div>
              </div>

                <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                <div class="count-div">
                  <p>Hired</p>
                  <p class="blu"><span class="count"><asp:Label ID="lblHired" runat="server"></asp:Label></span> </p>
                </div>
              </div>
                <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                <div class="count-div">
                  <p>Steel Silo</p>
                  <p class="blu"><span class="count"><asp:Label ID="lblSteelSilo" runat="server"></asp:Label></span> </p>
                </div>
              </div>
                <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                <div class="count-div">
                  <p>Silo Bags</p>
                  <p class="blu"><span class="count"><asp:Label ID="lblSiloBags" runat="server"></asp:Label></span> </p>
                </div>
              </div>
                <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                <div class="count-div">
                  <p>WDRA</p>
                  <p class="blu"><span class="count"><asp:Label ID="lblWDRA" runat="server"></asp:Label></span> </p>
                </div>
              </div>
                <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                <div class="count-div">
                  <p>Markfed</p>
                  <p class="blu"><span class="count"><asp:Label ID="lblMarkfed" runat="server"></asp:Label></span> </p>
                </div>
              </div>

                 <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                <div class="count-div">
                  <p>Tribal Scheme</p>
                  <p class="blu"><span class="count"><asp:Label ID="lblTS" runat="server"></asp:Label></span> </p>
                </div>
              </div>
                 <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                <div class="count-div">
                  <p>FCI</p>
                  <p class="blu"><span class="count"><asp:Label ID="lblFCI" runat="server"></asp:Label></span> </p>
                </div>
              </div>
                 <div class="col-xs-12 col-sm-4 col-md-4 text-center count-div-brd">
                <div class="count-div">
                  <p>CWC</p>
                  <p class="blu"><span class="count"><asp:Label ID="lblCWC" runat="server"></asp:Label></span> </p>
                </div>
              </div>
               
            </div>
            </div>--%>
            </div>
            <a class="left carousel-control" data-slide="prev" href="#myCarousel">‹</a> <a class="right carousel-control" data-slide="next" href="#myCarousel">›</a></div>
        
            </div>
          </div>
        </div>
   </section>
   
</asp:Content>
<asp:Content ContentPlaceHolderID="widget" runat="server">
    <section class="widget_wrapper">
        <div class="container">
         <div class="row-fluid">  
            <div class="col-md-4">
              <img src="assets/New//img/info_red.png" class="icon">
              <h4 class="title">MPWLC Info</h4>

              <p class="text-justify" style="font-size: 15px;">
            MP Warehouse and Logistics Corporation (MPWLC) is one of the oldest State Warehousing Corporation in the country .It was started with 8 regional offices and has now grown up to the extent of 270 Branches as at present with total Godown <b>5694</b> & his total capacity of <b>1,57,85,853</b>.
          </p>
          <p class="text-justify" style="font-size: 15px;">
            MPWLC has own total Godown <b>1516</b> and his total capacity of <b>36,14,554</b>, private Godown <b>4178</b> and his total Capacity of  <b>1,21,71,299</b>.

          </p>
            </div>
            <div class="col-md-4">
              <img src="assets/New//img/services_red.png" class="icon">

              <h4 class="title">Sevices we offer</h4>
              <ul>
                <li><a href="#">Scientific storage facility</a></li>
                <li><a href="#">D.E.S.S.</a></li>
                <li><a href="#">License for Warehousing</a></li>
              </ul>
            </div>
            <div class="col-md-4">
              <img src="assets/New//img/network_red.png" class="icon">

              <h4 class="title">Our Network</h4>
             <ul class="list-unstyled">
                <li><i class="fa fa-caret-right red"></i> <a href="BranchContact.aspx">District wise</a></li>
                <li><i class="fa fa-caret-right red"></i> <a href="RegionalContact.aspx">Region wise</a>
                    <ul>
                        <li>Bhopal</li>
                        <li>Indore</li>
                        <li>Ujjain</li>
                        <li>Jablpur</li>
                        <li>Gwalior</li>
                        <li>Sagar</li>
                        <li>Rewa</li>
                        <li>Narmadapuram</li>
                    </ul>
                </li>
                <li><a href="GeographicalView.aspx">Geography wise</a></li>
              </ul>
            </div>
            </div>
          </div>

   </section>
</asp:Content>