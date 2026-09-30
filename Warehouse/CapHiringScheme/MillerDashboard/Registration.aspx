<%@ Page Title="" Language="C#" MasterPageFile="~/CapHiringScheme/MillerDashboard/Miller.master"
    AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="JVSMiller_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 80%;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }
        .modalPopup .header
        {
            background-color: #2FBDF1;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
            border-top-left-radius: 6px;
            border-top-right-radius: 6px;
        }
        .modalPopup .body
        {
            min-height: 50px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .footer
        {
            padding: 6px;
        }
        .modalPopup .yes, .modalPopup .no
        {
            height: 23px;
            color: White;
            line-height: 23px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            border-radius: 4px;
        }
        .modalPopup .yes
        {
            background-color: #2FBDF1;
            border: 1px solid #0DA9D0;
        }
        .modalPopup .no
        {
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <!-- page-wrapper -->
    <div id="page-wrapper">
        <div class="row">
            <div class="col-md-12">
                <%-------------------------------Model Registration----------------------------------------------------%>
                <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </cc1:ToolkitScriptManager>
                <asp:Label ID="Label8" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlCofirmmsg"
                    TargetControlID="Label8" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlCofirmmsg" runat="server" CssClass="modalPopup" Height="465px"
                    Width="800px" Style="display: none">
                    <div class="pane panel-info">
                        <div class="panel panel-heading">
                            <div class="panel-title">
                                रजिस्ट्रेशन करने के लिए आवश्यक निर्देश
                                <asp:Button ID="btnNo" runat="server" Style="display: none;" Text="Close" CssClass="no "
                                    align="left" /></div>
                        </div>
                        <%--<table style="width:100%;">
            <tr>
                   <td style="color:White; font-weight:bold; font-size:14px;" align="center">रजिस्ट्रेशन करने के लिए आवश्यक निर्देश</td>
                   <td style="width:50px">  </td>
            </tr> 
                                         
        </table> --%>
                    </div>
                    <div class="panel-body">
                    <table class="table table-bordered">
                                        <thead>
                                          <tr>
                                            <th>क्रमांक</th>
                                            <th>निर्देश</th>
                                            <th>चुने</th>
                                          </tr>
                                        </thead>
                                        <tbody>
                                           <tr align="center">
                                            <td>
                                                <asp:Label ID="Label5" runat="server">1.</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label6" runat="server">क्‍या जिला उघोग द्वारा मिलिंग हेतु रजिस्ट्रेशन प्रदान किया गया है ?</asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkDistrictIndustry" runat="server" />
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label10" runat="server">2.</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label11" runat="server">क्‍या MPSCSC/MARKFED से मिलिंग हेतु Agreement किया गया है ? </asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkMpscscAgree" runat="server" />
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label7" runat="server">3.</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label9" runat="server">क्‍या धान (Paddy) को MPSCSC/MARKFED में स्‍वतंत्र रूप से रखने की अनुमति प्राप्‍त है  ?</asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ckhPaddy" runat="server" />
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="lblinspid" runat="server">4.</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblinsptype" runat="server">(*) वाले कॉलम अनिवार्य हैं।</asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkreg1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label1" runat="server">5.</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label2" runat="server">मिल का नाम, मोबाइल नंबर एवं अन्य जानकारी सावधानीपूर्वक प्रविष्ट करे, प्रविष्ट की गई जानकारी मे किसी भी प्रकार की त्रुटि के लिए आवेदक स्वयं जिम्मेदार होंगा |</asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkreg2" runat="server" />
                                            </td>
                                        </tr>
                                         <tr>
                                <td style="height: 15px;" colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <asp:Button class="btn btn-info" ID="btnAgree" runat="server" Text="Agree All Conditions"
                                        OnClick="btnAgree_Click" align="Center" />
                                </td>
                            </tr>


                                        </tbody>
                                      </table>



                        
                    </div>
                </asp:Panel>
                <asp:Panel ID="panelRegForm" runat="server">
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
                                        Registration Id
                                    </label>
                                    <span class="text-primary">
                                        <asp:Literal ID="litRegId" runat="server"></asp:Literal></span>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Miller Name</label>
                                    <span class="text-primary">
                                        <asp:Literal ID="litMillName" runat="server"></asp:Literal></span>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Mobile No.
                                    </label>
                                    <span class="text-primary">
                                        <asp:Literal ID="litMobile" runat="server"></asp:Literal></span>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Email</label>
                                    <span class="text-primary">
                                        <asp:Literal ID="litEmail" runat="server"></asp:Literal></span>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Aadhar No.
                                    </label>
                                    <span class="text-primary">
                                        <asp:Literal ID="litAdhar" runat="server"></asp:Literal></span>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        Pan No.
                                    </label>
                                    <span class="text-primary">
                                        <asp:Literal ID="litPan" runat="server"></asp:Literal></span>
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
                                    <label> Mill Name</label>
                                    <asp:TextBox ID="txtMill" class="form-control" runat="server"></asp:TextBox>
                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtMill"
                                        ValidationGroup="A" runat="server" ErrorMessage="* required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                
                                <div class="form-group col-md-4">
                                    <label>Capacity of Rice Miller (in MT per day)</label>
                                    <asp:TextBox ID="txtRiceCap" Text="0" class="allow-numeric form-control" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtRiceCap"
                                        ValidationGroup="A" runat="server" ErrorMessage="* required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Office Contact No./ Mobile No.</label>
                                    <asp:TextBox ID="txtOfficeNo" class="allow-numeric form-control" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtOfficeNo"
                                        ValidationGroup="A" runat="server" ErrorMessage="* required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Landmark Near Mills</label>
                                    <asp:TextBox ID="txtMLandmark" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label> District</label>
                                    <asp:DropDownList ID="ddlDistrict" class="form-control" AutoPostBack="true" runat="server"
                                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlDistrict"
                                        InitialValue="0" ErrorMessage="* required" ForeColor="Red" ValidationGroup="A"
                                        runat="server"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Tehsil</label>
                                    <asp:DropDownList ID="ddlTehsil" class="form-control" runat="server">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlTehsil"
                                        InitialValue="0" ErrorMessage="* required" ForeColor="Red" ValidationGroup="A"
                                        runat="server"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label> Block</label>
                                    <asp:DropDownList ID="ddlTehsilBlock" class="form-control" AutoPostBack="true" runat="server"
                                        OnSelectedIndexChanged="ddlTehsilBlock_SelectedIndexChanged">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlTehsilBlock"
                                        InitialValue="0" ErrorMessage="* required" ForeColor="Red" ValidationGroup="A"
                                        runat="server"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Nearest Branch of MPWLC</label>
                                    <asp:DropDownList ID="ddlBranch" class="form-control" runat="server">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="ddlBranch"
                                        InitialValue="0" ErrorMessage="* required" ForeColor="Red" ValidationGroup="A"
                                        runat="server"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Distance from nearest branch of MPWLC (in KM):</label>
                                    <asp:TextBox ID="txtNDistance"  Text="0" class="allow-numeric form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Mills/ Office Address with Postal Address</label>
                                    <asp:TextBox ID="txtOfficeAddr" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtOfficeAddr"
                                        ErrorMessage="* required" ForeColor="Red" ValidationGroup="A" runat="server"></asp:RequiredFieldValidator>
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
                                    <label>Registration No</label>
                                    <asp:TextBox ID="txtRegNoDistInd" class="form-control" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtRegNoDistInd"
                                        ValidationGroup="A" runat="server" ErrorMessage="* required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Issue Date</label>
                                    <asp:TextBox ID="txtRegNoIssueDt" autocomplete="off" class="form-control" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtRegNoIssueDt"
                                        ValidationGroup="A" runat="server" ErrorMessage="* required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Upload Document</label>
                                    <asp:FileUpload ID="fuRegDoc" class="form-control" runat="server" />
                                    <asp:CustomValidator ID="customValidatorUpload" runat="server" ErrorMessage="" ControlToValidate="fuRegDoc" ClientValidationFunction="setUploadButtonState();" />

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="fuRegDoc"
                                        ValidationGroup="A" runat="server" ErrorMessage="* required" ForeColor="Red"></asp:RequiredFieldValidator>

                               <asp:RegularExpressionValidator ID="RegExValFileUploadFileType" runat="server"
                        ControlToValidate="fuRegDoc"  ValidationGroup="A"
                        ErrorMessage="Only .jpg,.png,.jpeg,.gif Files are allowed" ForeColor="Red"
                        ValidationExpression="(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$"></asp:RegularExpressionValidator>


                                </div>
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <div class="panel box-primary">
                        <div class="panel-header with-border">
                            <h3 class="panel-title">
                                MPSCSC/MARKFED से मिलिंग हेतु किये गए एग्रीमेंट का विवरण</h3>
                            <hr />
                        </div>
                        <!-- /.box-header -->
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label> MPSCSC / MARKFED</label>
                                    <asp:DropDownList ID="ddlmpscsc_markfed" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="MPSCSC" Value="MPSCSC"></asp:ListItem>
                                        <asp:ListItem Text="MARKFED" Value="MARKFED"></asp:ListItem>
                                    </asp:DropDownList>
                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="ddlmpscsc_markfed"
                                        InitialValue="0" ErrorMessage="* required" ForeColor="Red" ValidationGroup="A"
                                        runat="server"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4">
                                    <label> Miller Registration Id</label>
                                    <asp:TextBox ID="txtAgreeMilerId" class="form-control" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtAgreeMilerId"
                                        ValidationGroup="A" runat="server" ErrorMessage="* required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label> Agreement Date</label>
                                    <asp:TextBox ID="txtAgreeDate" autocomplete="off" class="form-control" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtAgreeDate"
                                        ValidationGroup="A" runat="server" ErrorMessage="* required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Agreement Capacity (in MT)</label>
                                    <asp:TextBox ID="txtAgreeCap" class="allow-numeric form-control" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtAgreeCap"
                                        ValidationGroup="A" runat="server" ErrorMessage="* required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                </div>

                                <div class="row">
                                <div class="col-md-12">
                                    <h3 class="panel-title">
                                        धान (Paddy) को स्‍वतंत्र रूप से रखने की क्षमता का विवरण
                                    </h3><hr />
                                    <div class="form-group col-md-4">
                                        <label>Paddy Capacity (in MT)</label>
                                        <asp:TextBox ID="txtPaddyCap" class="allow-numeric form-control" runat="server"></asp:TextBox>

                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtPaddyCap"
                                            ValidationGroup="A" runat="server" ErrorMessage="* required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.box-body -->
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
                                    <asp:TextBox ID="txtInchPerson" class="form-control" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtInchPerson"
                                        ValidationGroup="A" runat="server" ErrorMessage="* required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Designation</label>
                                    <asp:TextBox ID="txtInchPost" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Email ID :</label>
                                    <asp:TextBox ID="txtInchEmail" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Mobile No. :</label>
                                    <asp:TextBox ID="txtInchMobile" class="allow-numeric form-control" MaxLength="10" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Authorised Person Address with Postal Address:</label>
                                    <asp:TextBox ID="txtInchAddr" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group col-md-4">
                                    &nbsp;
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
                                <div class="form-group col-md-3">
                                    <label>
                                        Mills Latitude</label>
                                    <asp:TextBox ID="txtMLat" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-3">
                                    <label>
                                        Mills Longitude
                                    </label>
                                    <asp:TextBox ID="txtMLong" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <h4 class="panel-title text-center red">
                        पंजीयन राशि - प्रत्येक राईस मिलर द्वारा कैप किराये पर लेने हेतु ऑनलाइन आवेदक करने
                        पर पंजीयन शुल्क राशि रुपए १०० /- ऑनलाइन भुगतान करना होगा |
                    </h4>
                    <br />
                    <asp:Button ID="btnSubmit" class="btn btn-info center-block" Text="Submit" ValidationGroup="A"
                        runat="server" OnClick="btnSubmit_Click"></asp:Button>
                </asp:Panel>
            </div>
    <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    </div>
    <!-- /page-wrapper -->
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">

    <script src="../assets/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
        <script type="text/javascript">
        
            $(function () {
                $('[id*=txtRegNoIssueDt]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd/mm/yyyy",
                    language: "tr"
                });

                $('[id*=txtAgreeDate]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd/mm/yyyy",
                    language: "tr"
                });
            });
        </script>


       <script type="text/javascript">

           $(document).ready(function () {
               $(".allow-numeric").bind("keypress", function (e) {
                   var keyCode = e.which ? e.which : e.keyCode

                   if (!(keyCode >= 48 && keyCode <= 57)) {
                       // $(".error").css("display", "inline");
                       return false;
                   } else {
                       //  $(".error").css("display", "none");
                   }
               });
           });


           function setUploadButtonState() {

               var maxFileSize = 101000; // 101KB 
               var fileUpload = $('#<%= fuRegDoc.ClientID %>');

               if (fileUpload.val() == '') {
                   return false;
               }
               else {
                   if (fileUpload[0].files[0].size < maxFileSize) {
                      
                       return true;
                   } else {
                 
                   alert('File Size not more than 100 KB');
                       fileUpload.val('');
                       return false;
                   }
               }
           }


     
</script>
</asp:Content>
