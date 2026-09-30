<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Gallery.aspx.cs" Inherits="Gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
    <section class="content_wrapper">
        <div class="container">
            <!-- Example row of columns -->
            <div class="row">

                <div class="col-md-12">

                    <div class="row-fluid">
                        <h3 class="red" style="letter-spacing: 1px;">GALLERY</h3>
                        <hr class="line-red" />

                        <ul class="text_content list-unstyled">
                            <%--<asp:Repeater ID="rptGallerList" runat="server">
                 <ItemTemplate>
                    <li>
                       <a href='<%# "GalleryDetails.aspx?Title="+ Eval("Title") %>'> <%# Eval("Title") %> </a> 
                    </li> 
                 </ItemTemplate>
               </asp:Repeater> --%>
                            <asp:GridView ID="GVOfStock" AutoGenerateColumns="false" runat="server"
                                OnRowCommand="GVOfStock_RowCommand">
                                <HeaderStyle
                                    BackColor="#D69758"
                                    Font-Italic="false"
                                    ForeColor="Snow" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <%--<asp:HiddenField ID="hdnTitle" runat="server" Value='<%# Bind("Title") %>' />--%>
                                            <asp:HiddenField runat="server" ID="hdnTitle" Value='<%# Eval("Title") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%--<asp:HiddenField ID="hdnTitle" runat="server" Value='<%# Bind("Title") %>' />--%>
                                            <asp:LinkButton ID="requestID" runat="server" CommandName="COUNT_REQUEST_ID"
                                                CommandArgument='<%# Eval("Title") %>' Text='<%# Eval("Title") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ul>

                    </div>
                </div>

            </div>

        </div>
        <!-- /container -->
    </section>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

