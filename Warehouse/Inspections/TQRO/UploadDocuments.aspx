<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO_TQ.master" AutoEventWireup="true" CodeFile="UploadDocuments.aspx.cs" Inherits="Inspections_TQRO_UploadDocuments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
            width: 100%;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #00aad2 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 15px;
                text-align: center;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #00aad2 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        .btnstyle {
            Height: 42px;
            Width: 150px;
            color: #04c;
            align-items: center;
            background-color: #0183bf;
            color: white;
        }

        .otherbtnstyle {
            align-items: center;
            border-color: #0183bf;
            color: #0183bf;
            background-color: white;
        }

        .btneditstyle {
            background-color: white;
            color: #00aad2;
            border-color: #00aad2;
            width: 100px;
            font-size: 14px;
        }
    </style>
    <style type="text/css">
        .buttonClass {
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px black;
            background-color: #ababab;
        }

            .buttonClass:hover {
                border: solid 1px Black;
                background-color: #ffffff;
            }
    </style>
    <div style="width: 100%;" id="divdocument" runat="server" visible="true">
        <div style="background-color: #00aad2; width: 100%; vertical-align: middle;">
            <h3 class="text-left waves-effect" style="vertical-align: text-top; margin-left: 10px; padding-top: 5px; font-size: 18px; color: white;">Upload Documents</h3>
        </div>
        <div class="row">
            <%--<div class="col-sm-12 col-md-12 col-xs-12">
                <div class="row">
                  
                    <div class="col-sm-6 col-md-6 col-xs-12">
                        <div class="form-group">
                            <label class="text-left">Select Branch<span style="color: red;">*</span></label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class=""></i></span>
                                </div>
                                <asp:DropDownList ID="ddlbranch" runat="server" class="form-control" Width="250px" Height="32px">
                                    <asp:ListItem Text="Select Branch" Value="0"></asp:ListItem>
                                    
                                </asp:DropDownList>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlbranch" ValidationGroup="G" InitialValue="0" CssClass="text-danger" runat="server" ErrorMessage="Select Branch."></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-6 col-xs-12">
                        <div class="form-group">
                            <label class="text-left">Select Document Type<span style="color: red;">*</span></label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class=""></i></span>
                                </div>
                                <asp:DropDownList ID="ddldoctype" runat="server" class="form-control">
                                    <asp:ListItem Text="Select Document Type" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Lien Certificate" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="SR/DL Certificate" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Other Certificate" Value="3"></asp:ListItem>
                                    
                                </asp:DropDownList>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddldoctype" ValidationGroup="G" InitialValue="0" CssClass="text-danger" runat="server" ErrorMessage="Choose Document Type."></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4 col-xs-12">
                        <div class="form-group">
                            <label class="text-left">Select Financial Year<span style="color: red;">*</span></label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class=""></i></span>
                                </div>
                                <asp:DropDownList ID="ddlfinancialyear" runat="server" class="form-control">
                                    <asp:ListItem Value="0">--Select Financial Year--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlfinancialyear" ValidationGroup="G" InitialValue="0" CssClass="text-danger" runat="server" ErrorMessage="Choose Year."></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4 col-xs-12">
                        <div class="form-group">
                            <label class="text-left">Upload Document(Upload Only PDF File)<span style="color: red;">*</span></label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class=""></i></span>
                                </div>
                                <asp:FileUpload ID="upload12thmarksheet" runat="server" />
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="upload12thmarksheet" ValidationGroup="G" CssClass="text-danger" runat="server" ErrorMessage="Please Upload Document."></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4 col-xs-12">
                        <div class="input-group-prepend " style="padding-top: 34px;">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload Document" CssClass="buttonClass form-control" ValidationGroup="G" OnClick="Upload" />
                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="col-sm-12 col-md-12 col-xs-12">
                 <div class="row">
                     
                     <div class="col-sm-4 col-md-4 col-xs-12">
                        <div class="form-group">
                            <label class="text-left">Select Branch<span style="color: red;">*</span></label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class=""></i></span>
                                </div>
                                <asp:DropDownList ID="ddl_dist" runat="server" class="form-control" AutoPostBack="true" Width="250px" Height="32px" OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                                    <asp:ListItem Text="Select District" Value="0"></asp:ListItem>
                                    
                                </asp:DropDownList>
                            </div>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlbranch" ValidationGroup="G" InitialValue="0" CssClass="text-danger" runat="server" ErrorMessage="Select Branch."></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    
                    <%--  <div class="col-sm-4 col-md-4 col-xs-12">
                        </div>--%>
                    <div class="col-sm-4 col-md-4 col-xs-12">
                        <div class="form-group">
                            <label class="text-left">Select Branch<span style="color: red;">*</span></label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class=""></i></span>
                                </div>
                                <asp:DropDownList ID="ddlbranch" runat="server" class="form-control" Width="250px" Height="32px">
                                    <asp:ListItem Text="Select Branch" Value="0"></asp:ListItem>
                                    
                                </asp:DropDownList>
                            </div>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlbranch" ValidationGroup="G" InitialValue="0" CssClass="text-danger" runat="server" ErrorMessage="Select Branch."></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                     
                    
                    <div class="col-sm-4 col-md-4 col-xs-12">
                        <div class="form-group">
                            <label class="text-left">Select Financial Year<span style="color: red;">*</span></label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class=""></i></span>
                                </div>
                                <asp:DropDownList ID="ddlfinancialyear" runat="server" class="form-control">
                                    <asp:ListItem Value="0">--Select Financial Year--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlfinancialyear" ValidationGroup="G" InitialValue="0" CssClass="text-danger" runat="server" ErrorMessage="Choose Year."></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                      </div>
                <div class="row">
                    <div class="col-sm-4 col-md-4 col-xs-12">
                        <div class="form-group">
                            <label class="text-left">Select Inspection Type<span style="color: red;">*</span></label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class=""></i></span>
                                </div>
                                <asp:DropDownList ID="ddlverification" runat="server" class="form-control">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">General Inspection</asp:ListItem>
                                    <asp:ListItem Value="2">Physical Verification</asp:ListItem>
                                    <asp:ListItem Value="3">Both</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddldoctype" ValidationGroup="G" InitialValue="0" CssClass="text-danger" runat="server" ErrorMessage="Choose Document Type."></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4 col-xs-12">
                        <div class="form-group">
                            <label class="text-left">Select Quater<span style="color: red;">*</span></label>
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text"><i class=""></i></span>
                                </div>
                                <asp:DropDownList ID="ddlquater" runat="server" class="form-control">
                                   <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">1st Quarter</asp:ListItem>
                                    <asp:ListItem Value="2">2nd Quarter</asp:ListItem>
                                    <asp:ListItem Value="3">3rd Quarter</asp:ListItem>
                                    <asp:ListItem Value="4">4th Quarter</asp:ListItem>
                                    <asp:ListItem Value="5">Half Yearly Inspection</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddldoctype" ValidationGroup="G" InitialValue="0" CssClass="text-danger" runat="server" ErrorMessage="Choose Document Type."></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    
                    <div class="col-sm-3 col-md-3 col-xs-12">
                        <div class="input-group-prepend " style="padding-top: 34px;">
                            <asp:Button ID="btnUpload" runat="server" Text="View Upload Document" CssClass="buttonClass form-control" OnClick="btnUpload_Click" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="row">
            <div style="width: 100%;">
                 <asp:GridView runat="server" ID="GrdOfficerPreviousInsp" OnRowCommand="GrdOfficerPreviousInsp_RowCommand"
                        AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">                       
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                    <asp:HiddenField runat="server" ID="hdndistrictid" Value='<%# Eval("District_ID") %>' />
                                    <asp:HiddenField runat="server" ID="hdnbranchid" Value='<%# Eval("Branch_ID") %>' />
                                    <asp:HiddenField runat="server" ID="hdnVerificationType" Value='<%# Eval("Verification_TypeID") %>' />
                                    <%--<asp:HiddenField runat="server" ID="hdnTAPID" Value='<%# Eval("TAP_ID") %>' />--%>
                                    <asp:HiddenField runat="server" ID="hdnInspection_ID" Value='<%# Eval("Inspection_ID") %>' />
                                     <asp:HiddenField runat="server" ID="hdnquatertype" Value='<%# Eval("Inspection_type_ID") %>' />
                                    <asp:HiddenField runat="server" ID="hdnfinancialyear" Value='<%# Eval("Financial_year") %>' />
                                    <asp:HiddenField runat="server" ID="hdnemployeeid" Value='<%# Eval("PF_ID") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="1%" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblpfid" runat="server" Text='<%# Eval("PF_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Officer Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblOfficer_Name" runat="server" Text='<%# Eval("Officer_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="District">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistirct_name" runat="server" Text='<%# Eval("Distirct_name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepotname" runat="server" Text='<%# Eval("Depotname") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manager Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchManagerName" runat="server" Text='<%# Eval("BranchManagerName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>    
                             <asp:TemplateField HeaderText="Inspection Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblInsp_Type" runat="server" Text='<%# Eval("VerificationType") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>   
                             <asp:TemplateField HeaderText="Quater">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuater" runat="server" Text='<%# Eval("Quater") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Order No">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrder_No" runat="server" Text='<%# Eval("Order_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrder_Date" runat="server" Text='<%# Eval("Order_Date") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                          <%--  <asp:TemplateField HeaderText="Inspection Period">
                                <ItemTemplate>
                                    <asp:Label ID="lblInsp_Period" runat="server" Text='<%# Eval("Inspection_Status") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>   --%>                        
                            <asp:TemplateField HeaderText="Inspection Month">
                                <ItemTemplate>
                                    <asp:Label ID="lblInsp_Month" runat="server" Text='<%# Eval("Inspection_Month") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                                                    
                            <asp:TemplateField HeaderText="View Document" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Button ID="btnfilloverallinsp" Text="View Document" runat="server" CommandName="Overallinsp" CssClass="btn btn-info" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>

                        <%--<FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />--%>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" Height="20px" Font-Size="10pt" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#F7ECDD" Font-Bold="True" ForeColor="#cb4e48" HorizontalAlign="center"
                            Height="20px" Font-Size="10pt" />
                        <AlternatingRowStyle BackColor="#eeeeee" />
                    </asp:GridView>
               <%-- <asp:GridView runat="server" ID="GridView1" OnRowCommand="GridView1_RowCommand"
                    AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                                <asp:HiddenField ID="hdnid" runat="server" Value='<%# Bind("id") %>' />
                                <asp:HiddenField ID="hdbuid" runat="server" Value='<%# Bind("BranchID") %>' />
                                <asp:HiddenField ID="hdnvcid" runat="server" Value='<%# Bind("EmployeeID") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="1%" />
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Document Type">
                            <ItemTemplate>
                                <asp:Label ID="lbldoctype" runat="server" Text='<%# Eval("Doc_Type") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Document Name">
                            <ItemTemplate>
                                <asp:Label ID="lblqualificatio" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Download">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                    CommandArgument='<%# Eval("Id") %>' CssClass="buttonClass"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                            <ItemTemplate>
                                <asp:Button ID="btnRemove" Text="Remove" runat="server" CommandName="RemoveRow" CssClass="btneditstyle" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Do you want to Remove this row?');" />
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>
            </div>
        </div>

        <hr />

    </div>
    
     <%--<div id="divmsg" runat="server" visible="false" style="text-align: center;">
            <h2>You have already Final submitted to your Application,Please Print Your Application</h2>
            <br />
             <asp:Button ID="btneReceipt" Height="34px" Width="360px" runat="server" Text="Click here to print e-Receipt" OnClick="btneReceipt_Click" />
            <asp:Button ID="btngotoprintpage" Height="34px" Width="360px" runat="server" Text="Click here to print your application" OnClick="btngotoprintpage_Click" />
        </div>--%>
</asp:Content>

