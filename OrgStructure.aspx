<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="OrgStructure.aspx.cs" Inherits="OrgStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
          table{
                border-collapse: separate !important;
            }
            .google-visualization-orgchart-linebottom,
            .google-visualization-orgchart-lineleft,
            .google-visualization-orgchart-lineright
            {
                border-width:2px !important;
            }
            
            .google-visualization-orgchart-table *{padding:0 !important;}

           .google-visualization-orgchart-node-medium{

              /*box-shadow: 4px 4px 9px -4px rgba(0,0,0,0.4) !important;
              border:4px solid #fff !important;
              border-radius: 0 !important;
              color:#fff;
              padding: 0.5em !important;  
              background: #00B4DB !important; 
              background: -webkit-linear-gradient(to right, #0083B0, #00B4DB) !important;  
              background: linear-gradient(to right, #0083B0, #00B4DB) !important; 
              font-size: 12px;
              font-weight: bold;*/
              
              background-color: transparent !important; 
              background: none !important;  
              border:0 !important;
              box-shadow: none !important;
              
            }
            .dropdown-toggle{padding:0.5em !important;}
            .dropdown-menu
            {
                font-size:12px !important;
                padding:0.5em !important; 
            }
           
        </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
    <section style="min-height:65em;padding:1em 0;">
        <div class="container">
        <!-- Example row of columns -->
          <div class="row">
          <div class="col-md-12">
              <nav aria-label="breadcrumb">
                  <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Default.aspx">Home</a></li>
                   <%-- <li class="breadcrumb-item"><a href="InfoProfile.aspx">Profile</a></li>--%>
                    <li class="breadcrumb-item active" aria-current="page">Organization Structure</li>
                  </ol>
                </nav>
            </div>


           <div class="col-md-12">
                <div class="row-fluid">
                    <h3 class="red" style="letter-spacing:1px;">ORGANIZATION STRUCTURE</h3> <hr class="line-red"/>
                    <div id="chart_div"></div><br />
                    <div id="chart_div1"></div>

                </div>
           </div>
   
          </div>

         </div> <!-- /container -->
   </section>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="script" Runat="Server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        google.charts.load('current', { packages: ["orgchart"] });


        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Name');
            data.addColumn('string', 'Manager');
            data.addColumn('string', 'ToolTip');

            // For each orgchart box, provide the name, manager, and tooltip to show.
            data.addRows([
            // [{'v':'Mike', 'f':'Mike<div style="color:red; font-style:italic">President</div>'},
            //  '', 'The President'],
            // [{'v':'Jim', 'f':'Jim<div style="color:red; font-style:italic">Vice President</div>'},
            //  'Mike', 'VP'],
            // ['Alice', 'Mike', ''],
            // ['Bob', 'Jim', 'Bob Sponge'],
            // ['Carol', 'Bob', '']

                /*[{ 'v': 'BOD', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">BOARD OF DIRECTORS <span class="caret"></span></button> <div class="dropdown-menu text-left"> <h6><strong>Appointed by state govt.</strong></h6> <p class="text-primary">1) Shri Rahul Singh, Chaiman,MPWLC<br/><br/> 2) Shri Umakant Umrao,<br/> Principal  Secretary  (Food Dept. M.P.) </p><br/> <p class="text-primary">3) Shri P. Narhari,<br/> MD, Markfed </p><br/> <p class="text-primary"> 4)  Shri Deepak Kumar Saxena <br/>MD, MPWLC  </p> <br/><p class="text-primary"> 5) Shri Tarun Kumar pithore, MD ,MPSCSC(Bhopal) </p><br/> <p class="text-primary"> 6) Shri Shakti Saran, Dy.Secretary ,M.P.Govt. Finance Dep. </p> <br/>  <p class="text-primary"> 7) Mr. M.K Verma, General Manager (Commercial), CWC, New Delhi<br/><br/></p> <p class="text-primary"> 8) Mr. Devindra S. Uyike, DS (POLICY), Ministry Of Consumers Affairs, Food and Public Distribution,Govt. of india, New-Delhi-110001.<br/><br/></p> <p class="text-primary"> 9) Shri. Pinku Kumar Saw, Regional Manager,CWC Bhopal<br/><br/></p> <p class="text-primary"> 10) Shri. Ravindra Patil, DGM(SBI), HO, Bhopal. <br/><br/></p> <p class="text-primary"> 11) Shri. Kishore Nanabhau Patil, 1602, Blu Kihes CHS Ltd. Plot No-05, Sector-02/A, Coperkherna, Navi-Mumbai-400706</p> </div> </div>' }, '', ''],*/
                /*[{ 'v': 'BOD', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">BOARD OF DIRECTORS <span class="caret"></span></button> <div class="dropdown-menu text-left"> <h6><strong>Appointed by state govt.</strong></h6> <p class="text-primary">1) Shri Sanjay Nagayach, Chairman,MPWLC<br/><br/> 2) Smt. Rashmi Arun Shami,<br/> Principal  Secretary  (Food Dept. M.P.) </p><br/> <p class="text-primary">3) Shri Alok Kumar Singh,<br/> MD, Markfed </p><br/> <p class="text-primary"> 4)  Shri Sibi Chakravarty <br/>MD, MPWLC  </p> <br/><p class="text-primary"> 5) Shri Pratap Narayan Yadav, MD ,MPSCSC(Bhopal) </p><br/> <p class="text-primary"> 6) Shri O.P. Gupta, Dy.Secretary ,M.P.Govt. Finance Dep. </p> <br/>  <p class="text-primary"> 7) Mr. Amit Puri, General Manager (Finance), CWC, New Delhi<br/><br/></p> <p class="text-primary"> 8) Mr. Pankaj singh, DS (POLICY), Ministry Of Consumers Affairs, Food and Public Distribution,Govt. of india, New-Delhi-110001.<br/><br/></p> <p class="text-primary"> 9) Shri. Mukesh Sati, Regional Manager,CWC Bhopal<br/><br/></p> <p class="text-primary"> 10) Shri. Zulfikar Ali Khan, DGM(SBI), HO, Bhopal. <br/><br/></p> <p class="text-primary"> 11) Shri. Kishore Nanabhau Patil, 1602, Blu Kihes CHS Ltd. Plot No-05, Sector-02/A, Coperkherna, Navi-Mumbai-400706</p> </div> </div>' }, '', ''],*/
                [{ 'v': 'BOD', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">BOARD OF DIRECTORS <span class="caret"></span></button> <div class="dropdown-menu text-left"> <h6><strong>Appointed by state govt.</strong></h6> <p class="text-primary">1) Shri Sanjay Nagayach, Chairman,MPWLC<br/><br/> 2) Smt. Rashmi Arun Shami,<br/> Principal  Secretary  (Food Dept. M.P.) </p><br/> <p class="text-primary">3) Shri Abhijeet Agrawal,<br/> MD, Markfed </p><br/> <p class="text-primary"> 4)  Shri Anurag Verma <br/>MD, MPWLC  </p> <br/><p class="text-primary"> 5) Shri Anurag Verma, MD ,MPSCSC(Bhopal) </p><br/> <p class="text-primary"> 6) Shri O.P. Gupta, Dy.Secretary ,M.P.Govt. Finance Dep. </p> <br/>  <p class="text-primary"> 7) Mr. Amit Puri, General Manager (Finance), CWC, New Delhi<br/><br/></p> <p class="text-primary"> 8) Mr. Pankaj singh, DS (POLICY), Ministry Of Consumers Affairs, Food and Public Distribution,Govt. of india, New-Delhi-110001.<br/><br/></p> <p class="text-primary"> 9) Shri. Ram Kumar, Regional Manager,CWC Bhopal<br/><br/></p> <p class="text-primary"> 10) Shri. Zulfikar Ali Khan, DGM(SBI), HO, Bhopal. <br/><br/></p> <p class="text-primary"> 11) Shri. Kishore Nanabhau Patil, 1602, Blu Kihes CHS Ltd. Plot No-05, Sector-02/A, Coperkherna, Navi-Mumbai-400706</p> </div> </div>' }, '', ''],

          //[{ 'v': 'BOD', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">BOARD OF DIRECTORS <span class="caret"></span></button> <div class="dropdown-menu text-left"> <h6><strong>Appointed by state govt.</strong></h6> <p class="text-primary">1) Shri Faiz Ahmed Kidwai, Chaiman,MPWLC<br/>2) Shri Faiz Ahmed Kidwai,<br/> Principal  Secretary  (Food Dept. Bhopal) </p> <p class="text-primary">3) Shri P. Narhari,<br/> MD, Markfed </p> <p class="text-primary"> 4)  Shri Deepak Kumar Saxena <br/>MD, MPWLC  </p> <p class="text-primary"> 5) Shri Tarun Kumar pithore, MD ,MPSCSC(Bhopal) </p> <p class="text-primary"> 6) Shri Shakti Saran, Dy.Secretary ,M.P.Govt. Finance Dep. </p> <br/>  <p class="text-primary"> 7) Mr. M.K Verma, General Manager (Commercial), CWC, New Delhi<br/><br/></p> <p class="text-primary"> 8) Mr. Devindra S. Uyike, DS (POLICY), Ministry Of Consumers Affairs, Food and Public Distribution,Govt. of india, New-Delhi-110001.<br/><br/></p> <p class="text-primary"> 9) Shri. Pinku Kumar Saw, Regional Manager,CWC Bhopal<br/><br/></p> <p class="text-primary"> 10) Shri. Ravindra Patil, DGM(SBI), HO, Bhopal. <br/><br/></p> <p class="text-primary"> 11) Shri. Kishore Nanabhau Patil, 1602, Blu Kihes CHS Ltd. Plot No-05, Sector-02/A, Coperkherna, Navi-Mumbai-400706</p> </div> </div>' }, '', ''],
             //   [{ 'v': 'EV', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">EXECUTIVE COMMITTEE <span class="caret"></span></button> <div class="dropdown-menu text-left"> <h6><strong>Appointed by state govt.</strong></h6> <p class="text-primary">1)Shri Rahul Singh, Chaiman,MPWLC<br/>2) Shri Faiz Ahmed Kidwai,<br/> Principal  Secretary  (Food Dept. Bhopal) </p> <p class="text-primary">3) Shri P. Narhari,<br/> MD, Markfed </p> <p class="text-primary"> 4)  Shri Deepak Kumar Saxena <br/>MD, MPWLC  </p><p class="text-primary"> 5) Shri. Ravindra Patil, DGM(SBI), HO, Bhopal.</p></div> </div>' }, 'BOD', ''],
                [{ 'v': 'EV', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">EXECUTIVE COMMITTEE <span class="caret"></span></button> <div class="dropdown-menu text-left"> <h6><strong>Appointed by state govt.</strong></h6> <p class="text-primary">1)Shri Sanjay Nagayach, Chairman,MPWLC<br/>2) Smt. Rashmi Arun Shami,<br/> Principal  Secretary  (Food Dept. M.P.) </p> <p class="text-primary">3) Shri Abhijeet Agrawal,<br/> MD, Markfed </p> <p class="text-primary"> 4)  Shri Anurag Verma <br/>MD, MPWLC  </p><p class="text-primary"> 5) Shri. Zulfikar Ali Khan, DGM(SBI), HO, Bhopal.</p></div> </div>' }, 'BOD', ''],
        
                [{ 'v': 'CHRM', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">CHAIRMAN</button><div class="dropdown-menu text-left"> <address> <strong>Shri Sanjay Nagayach </strong><br/> Phone: 0755-2600500 (O) </address> </div> </div>' }, 'EV', ''],

                [{ 'v': 'MD', 'f': ' <div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">MANAGING DIRECTOR <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Shri Anurag Verma</strong><br/>Phone: 0755-2600509 (O) </address> </div> </div>' }, 'CHRM', ''],

          [{ 'v': 'AMD', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">AMD <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Shri Brijesh Saxena </strong><br> Phone: 0755-2600148(O)<br/>  </address> </div> </div>' }, 'MD', ''],
                [{ 'v': 'GMPER', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">GM(Personnel) <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Shri Brijesh Saxena</strong><br> Phone: 0755-2600148(O)<br/>  </address> </div> </div>' }, 'MD', ''],
                [{ 'v': 'ED', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">ED(F & AC) <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Shri Rajesh Agarwal </strong><br> Phone: 0755-2600518(O)<br/>  </address> </div> </div>' }, 'MD', ''],
                [{ 'v': 'GMTECH', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">GM(Commercial) <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. S.K. Purohit </strong><br> Phone: 0755-2600287(O)<br/> </address> </div> </div>' }, 'MD', ''],
                [{ 'v': 'ENGCH', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">S.E. <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. S.K. Jain</strong><br> Mob.: 0755-2600520(O)<br/> </address> </div> </div>' }, 'MD', ''],
                [{ 'v': 'GMQC', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">GM(QC) <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. A.K. Dahayat </strong><br> Phone: 0755-2600503(O)<br/>  </address> </div> </div>' }, 'MD', ''],
                [{ 'v': 'DGM', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">GM(Recovery) <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. A.K. Dahayat</strong><br> Phone: 0755-2600507(O)<br/>  </address> </div> </div>' }, 'MD', ''],

        ]);

            // Create the chart.
            var chart = new google.visualization.OrgChart(document.getElementById('chart_div'));
            // Draw the chart, setting the allowHtml option to true for the tooltips.
            chart.draw(data, { 'allowHtml': true });
        }


        google.charts.setOnLoadCallback(drawChart1);
        function drawChart1() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Name');
            data.addColumn('string', 'Manager');
            data.addColumn('string', 'ToolTip');

            // For each orgchart box, provide the name, manager, and tooltip to show.
            data.addRows([
            // [{'v':'Mike', 'f':'Mike<div style="color:red; font-style:italic">President</div>'},
            //  '', 'The President'],
            // [{'v':'Jim', 'f':'Jim<div style="color:red; font-style:italic">Vice President</div>'},
            //  'Mike', 'VP'],
            // ['Alice', 'Mike', ''],
            // ['Bob', 'Jim', 'Bob Sponge'],
            // ['Carol', 'Bob', '']

          [{ 'v': 'RO', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">REGIONAL OFFICE</button> </div>' }, '', ''],

                [{ 'v': 'BPL', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">RM Bhopal <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. Rajesh Agarwal </strong><br> Phone : 0755-2600061(O)<br/> </address> </div> </div>' }, 'RO', ''],
          [{ 'v': 'IND', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">RM Indore <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. B.L. Chouhan </strong><br> Phone : 0731-2411965(O)<br/>  </address> </div> </div>' }, 'RO', ''],
                [{ 'v': 'JBL', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">RM Jabalpur <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. S.K. Solanki </strong><br> Phone : 0761-2641646(O)<br/>  </address> </div> </div>' }, 'RO', ''],
                [{ 'v': 'UJN', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">RM Ujjain <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. Uday Singh More </strong><br> Phone : 0734-2563772(O)<br/>  </address> </div> </div>' }, 'RO', ''],
          [{ 'v': 'GWL', 'f': ' <div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">RM Gwalior <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. Manish Verma </strong><br> Phone : 0751-4076165(O)<br/>  </address> </div> </div>' }, 'RO', ''],
                [{ 'v': 'SAG', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">RM Sagar <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. D.K. Hawaldar </strong><br> Phone : 07582-247721(O)<br/>  </address> </div> </div>' }, 'RO', ''],
          [{ 'v': 'REW', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">RM Rewa <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. C.M. Mishra</strong><br> Phone : 07662-221677(O)<br/>  </address> </div> </div>' }, 'RO', ''],
                [{ 'v': 'NRM', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">RM Narmadapuram <span class="caret"></span></button> <div class="dropdown-menu text-left"> <address> <strong>Mr. Atul Sorte</strong><br> Phone : 07574-257347(O)<br/>  </address> </div> </div>' }, 'RO', ''],

                [{ 'v': 'BPLBC', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">41 Branch</button></div>' }, 'BPL', ''],
                [{ 'v': 'INDBC', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">28 Branch</button></div>' }, 'IND', ''],
                [{ 'v': 'JBLBC', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">50 Branch</button></div>' }, 'JBL', ''],
          [{ 'v': 'UJNBC', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">38 Branch</button></div>' }, 'UJN', ''],
                [{ 'v': 'GWLBC', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">43 Branch</button></div>' }, 'GWL', ''],
                [{ 'v': 'SAGBC', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">36 Branch</button></div>' }, 'SAG', ''],
                [{ 'v': 'REWBC', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">26 Branch</button></div>' }, 'REW', ''],
                [{ 'v': 'NRMBC', 'f': '<div class="dropdown"> <button class="btn btn-info dropdown-toggle" type="button" data-toggle="dropdown">25 Branch</button></div>' }, 'NRM', ''],
          
        ]);

        // Create the chart.
        var chart = new google.visualization.OrgChart(document.getElementById('chart_div1'));
        // Draw the chart, setting the allowHtml option to true for the tooltips.
        chart.draw(data, { 'allowHtml': true });
        }
    </script>

   


</asp:Content>

