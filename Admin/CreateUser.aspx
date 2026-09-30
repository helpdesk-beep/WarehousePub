<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="Admin_Agreement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <h4>Create New User</h4>
    <hr />
   <%-- <asp:UpdatePanel runat="server">--%>
        <%--<ContentTemplate>--%>
            <div class="col-md-12">
                <div class="form-group">
                    <div class="col-sm-8 col-sm-offset-4">
                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        <asp:HiddenField ID="hfId" Value="0" runat="server" />
                        <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
                    </div>
                </div>
                 <div class="form-group">
                    <div class="col-sm-3">
                        <label class="control-label">Role</label>
                        <asp:DropDownList ID="ddlrole" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlrole_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>                           
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" id="divregion" runat="server" visible="false">
                    <div class="col-sm-3">
                        <label class="control-label">Region</label>
                        <asp:DropDownList ID="ddlregion" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlSection" ValidationGroup="A" ErrorMessage="* required" InitialValue="0" runat="server"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                <div class="form-group" id="divdistrict" runat="server" visible="false">
                    <div class="col-sm-3">
                        <label class="control-label">District</label>
                        <asp:DropDownList ID="ddldistrict" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlSection" ValidationGroup="A" ErrorMessage="* required" InitialValue="0" runat="server"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                <div class="form-group" id="divbranch" runat="server" visible="false">
                    <div class="col-sm-3">
                        <label class="control-label">Branch</label>
                        <asp:DropDownList ID="ddlbranch" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlSection" ValidationGroup="A" ErrorMessage="* required" InitialValue="0" runat="server"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                <div class="form-group" id="divgodown" runat="server" visible="false">
                    <div class="col-sm-3">
                        <label class="control-label">Godown</label>
                        <asp:DropDownList ID="ddlgodown" CssClass="form-control" runat="server">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlSection" ValidationGroup="A" ErrorMessage="* required" InitialValue="0" runat="server"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3">
                        <label class="control-label">Name</label>
                        <asp:TextBox ID="txtname" placeholder="Name" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtname" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3">
                        <label class="control-label">Mobile No.</label>
                        <asp:TextBox ID="txtmobileno" placeholder="Mobile Number" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtmobileno" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3">
                        <label class="control-label">User Name</label>
                        <asp:TextBox ID="txtusername" placeholder="User Name" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtusername" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                    </div>
                </div>
               <div class="form-group">
                    <div class="col-sm-3">
                        <label class="control-label">Password</label>
                        <asp:TextBox ID="txtpassword" placeholder="Password" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtpassword" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="Regex4" runat="server" ControlToValidate="txtpassword" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}$" ErrorMessage="Password must contain: Minimum 8 and Maximum 10 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" ForeColor="Red" ValidationGroup="A" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3">
                        <label class="control-label">Emailc</label>
                        <asp:TextBox ID="txtemail" placeholder="Email" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtemail" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <%--<div class="form-group">
                    <div class="col-sm-3">
                        <label class="control-label">Title</label>
                        <asp:TextBox ID="txtTitle" placeholder="Title" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTitle" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-3">
                        <label class="control-label">Select File</label>
                        <asp:FileUpload ID="upFile" runat="server" />
                        <small class="help-block">Please Upload Only pdf,xls,xlsx,zip,rar,jpg,jpeg,txt,doc,docx File</small>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-3">
                        <label class="control-label">Expiry Date</label>
                        <asp:TextBox ID="txtExpDate" autocomplete="off" placeholder="Expiry Date" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtExpDate" ValidationGroup="A" ErrorMessage="* required" runat="server"></asp:RequiredFieldValidator>
                    </div>
                </div>--%>


                <div class="form-group">
                    <div class="col-sm-offset-4 col-sm-8">
                        <asp:Button ID="btnSave" CssClass="btn btn-info" ValidationGroup="A"
                            runat="server" Text="SAVE" OnClick="btnSave_Click" />

                        <asp:Button ID="btnCancel" CssClass="btn btn-default" Text="Cancel"
                            runat="server" OnClick="btnCancel_Click" />
                    </div>
                </div>

            </div>
            <div class="col-md-12">
                <div class="table-responsive">

                   <div style="overflow-x: scroll; width: 1020px;">
                            <asp:GridView ID="grdalreadyattended" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" rules="all" ForeColor="#333333">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="White" CssClass="ADFieldsGridText" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="5%" HeaderText="Sr No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5px" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName1" runat="server" Text='<%# Bind("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Username")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Password">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpassword" runat="server" Text='<%# Bind("Password")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile_No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("Mobile_No")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Role">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStateName" runat="server" Text='<%# Bind("Roll_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp_ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDOB" runat="server" Text='<%# Bind("Emp_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" HorizontalAlign="Left" />
                                    </asp:TemplateField>                                   
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <a href='/Admin/CreateUser.aspx?ID=<%# Eval("ID") %>' class="buttonStyle">Edit</a>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="#e1f3ff" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                </div>
            </div>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="contbootm" runat="Server">
    <script src="../assets/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=txtExpDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                language: "tr"
            });
        });
    </script>
</asp:Content>

