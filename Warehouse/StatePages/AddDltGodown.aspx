<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="AddDltGodown.aspx.cs" Inherits="StatePages_AddDltGodown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">

    <link type="text/css" rel="Stylesheet" href="css/style_new.css" />

    <script src="../JS/Jquery.3.6.0.js"></script>
    <link href="../assets/css/style.css" rel="stylesheet" />
    <!-- font awesome -->
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/New/css/bootstrap.min2.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
    <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
    <style>
        .search-box {
            display: block;
            width: 20%;
            max-width: 500px;
            padding: 10px;
            margin: 0 0 0px 4px;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 6px;
            box-sizing: border-box;
            text-align: left;
            margin-bottom: 10px;
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
    <style type="text/css">
        .left, .right {
            float: left;
            width: 20%; /* The width is 20%, by default */
        }

        .main {
            float: left;
            width: 60%; /* The width is 60%, by default */
        }

        /* Use a media query to add a breakpoint at 800px: */
        @media screen and (max-width: 800px) {
            .left, .main, .right {
                width: 100%; /* The width is 100%, when the viewport is 800px or smaller */
            }
        }
    </style>
    <style type="text/css">
        fieldset {
            border: 1px solid #2095A1;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095A1;
            font-size: 17px;
            font-weight: bold;
            color: #030203;
        }

        .content-wrapper {
            padding: 1.75rem 1.25rem;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #030203;
        }

        .form-control {
            border: 1px solid #767B83;
            border-radius: 8px;
        }

        .table th {
            text-align: center;
        }

        .form-inline {
            display: block !important;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: white !important;
            color: black !important;
            /*color: white !important;*/
        }

        .GridViewHeader th {
            color: white !important; /* header text white */
            background-color: #4CAF50 !important; /* optional background */
            text-align: center;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
            /*color: black !important;*/
            color: white !important;
            /*font-size:14px;*/
        }

        element.style {
            font-size: medium !important;
        }

        .auto-style3 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 50%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("[id*=DdlDist]").select2();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlbranch]").select2();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">  
        function check() {

            // alert(a);


            if ($('#<%=DdlDist.ClientID%>').val() == "0") {
                alert("जिला चुने ");
                $('#<%=DdlDist.ClientID%>').focus();
                return false;
            }


            if ($('#<%=ddlbranch.ClientID%>').val() == "0") {
                alert("ब्रांच चुने ");
                $('#<%=ddlbranch.ClientID%>').focus();
                return false;
            }

        }
    </script>

    <%--<style>
        body {
                font-size: 15px;
        }
    </style>--%>
    <fieldset>
        <legend>ऐसे  गोडाउन जो Delete हो गए हो</legend>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-2">
                <label>District</label>
            </div>
            <div class="col-md-2">
                <%--                <asp:DropDownList ID="DdlDist" runat="server" AutoPostBack="true" OnTextChanged="DdlDist_TextChanged"></asp:DropDownList>--%>
                <asp:DropDownList ID="DdlDist" runat="server" AutoPostBack="true"
                    Height="25px" Width="168px" OnSelectedIndexChanged="DdlDist_TextChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label>Branch</label>
            </div>
            <div class="col-md-2">
                <%--<asp:DropDownList ID="ddlbranch" runat="server" OnTextChanged="ddlbranch_TextChanged" AutoPostBack="true"></asp:DropDownList>--%>
                <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true"
                    Height="25px" Width="168px" OnSelectedIndexChanged="ddlbranch_TextChanged">
                </asp:DropDownList>
            </div>
        </div>
    </fieldset>
<fieldset>
    <legend>Details</legend>
    <div class="row">
        <div class="col-md-2">
            <asp:TextBox ID="txtSearch" runat="server"
                CssClass="search-box"
                Width="250px"
                placeholder="Search by Godown Details..."
                onkeyup="filterGrid()" />
        </div>
        <div class="col-md-2" style="padding: 0px 0px 0px 100px;">
            <asp:Button ID="Button2" runat="server"
                Text="Search"
                Width="100px"
                CssClass="BTNBLUE"
                OnClick="btnSearch_Click"
                OnClientClick="$('#myspindiv').show();"
                onmousedown="fnChkEmptyData();" />
            <asp:HiddenField ID="hdnSearchValue" runat="server" />
        </div>
    </div>
      <asp:GridView ID="GridView1" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" OnRowUpdating="GridView1_RowUpdating" Width="100%" Height="140px">
       <Columns>
           <asp:TemplateField>
               <HeaderTemplate>

                   <th style="text-align: center;">क्र.</th>
                   <th style="text-align: center;">Godown_ID</th>
                   <th style="text-align: center;">Godown_Name</th>

                   </tr>
                   
               </HeaderTemplate>
               <ItemTemplate>

                   <td style="text-align: center;"><%# Container.DataItemIndex + 1 %></td>
                   <asp:HiddenField ID="hdngodownid" runat="server" Value='<%#Eval("Godown_ID") %>' />
                   <td style="text-align: center;">
                       <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Godown_ID") %>' />
                   </td>
                   <td style="text-align: center;">
                       <asp:Label ID="Label16" runat="server" Text='<%#Eval("Godown_Name") %>' />
                   </td>
                   <td style="text-align: center;">
                       <asp:Button ID="btn_Update" CssClass="btn btn-info" Style="color: #ffffff" runat="server" Text="ADD" CommandName="Update" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Add this Godown??');" />
                   </td>
               </ItemTemplate>
           </asp:TemplateField>

       </Columns>
   </asp:GridView>
</fieldset>
   <%-- <div style="text-align: center">
        <h2 style="font-size: 25px; text-transform: uppercase; font-weight: revert; color: gray;">ऐसे  गोडाउन जो Delete हो गए हो  </h2>
    </div>--%>

    <asp:Panel ID="StoreGrid" runat="server">

        <table style="text-align: center; margin: 0 auto; border: 1px solid#000;">
            <tr>
                <td></td>
                <td></td>
            </tr>
        </table>




     
    </asp:Panel>
     <script type="text/javascript">
         function filterGrid() {
             var input = document.getElementById('<%= txtSearch.ClientID %>');
         var filter = input.value.toLowerCase();
         var table = document.getElementById('<%= GridView1.ClientID %>');
         var trs = table.getElementsByTagName("tr");

         // hidden field me textbox ka value set karo
         document.getElementById('<%= hdnSearchValue.ClientID %>').value = input.value;

         for (var i = 1; i < trs.length; i++) { // skip header row
             var tds = trs[i].getElementsByTagName("td");
             var show = false;
             for (var j = 0; j < tds.length; j++) {
                 if (tds[j].innerText.toLowerCase().indexOf(filter) > -1) {
                     show = true;
                     break;
                 }
             }
             trs[i].style.display = show ? "" : "none";
         }
     }
     </script>
 <script type="text/javascript">
     function fnChkEmptyData() {
         if (document.getElementById(preid + "txtSearch").value == "") {
             alert("Godown Id is required.");
             document.getElementById(preid + "txtSearch").focus();
             validSubmit = 0;
             return returnFalse();
         }
     }

 </script>
 <script type="text/javascript">
     // Show loader on full postback
     function showLoader() {
         document.getElementById("myspindiv").style.display = "block";
     }

     // For AJAX requests (UpdatePanel, ModalPopup, etc.)
     Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
         showLoader();
     });

     Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
         document.getElementById("myspindiv").style.display = "none";
     });

     // For normal postbacks
     window.onload = function () {
         var theForm = document.forms[0];
         if (theForm.attachEvent) {
             theForm.attachEvent("onsubmit", showLoader);
         } else {
             theForm.addEventListener("submit", showLoader, false);
         }
     };
 </script>
</asp:Content>

