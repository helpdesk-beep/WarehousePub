<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/Inspection_RO_TQ.master" AutoEventWireup="true" CodeFile="ViewUploadedDocument.aspx.cs" Inherits="Inspections_TQRO_ViewUploadedDocument" %>

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
            <div style="width: 100%;">
                <asp:GridView runat="server" ID="GridView1" OnRowCommand="GridView1_RowCommand"
                    AutoGenerateColumns="false" CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                    <columns>
                        <asp:TemplateField HeaderText="S.No.">
                            <itemtemplate>
                                <%#Container.DataItemIndex+1%>
                                <asp:HiddenField ID="hdnid" runat="server" Value='<%# Bind("id") %>' />
                                <asp:HiddenField ID="hdbuid" runat="server" Value='<%# Bind("BranchID") %>' />
                                <asp:HiddenField ID="hdnvcid" runat="server" Value='<%# Bind("EmployeeID") %>' />
                            </itemtemplate>
                            <itemstyle width="1%" />
                            <headerstyle horizontalalign="Center"></headerstyle>
                            <itemstyle horizontalalign="Center"></itemstyle>
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Branch">
                            <itemtemplate>
                                <asp:Label ID="lblDepotName" runat="server" Text='<%# Eval("DepotName") %>'></asp:Label>
                            </itemtemplate>
                            <itemstyle horizontalalign="Left"></itemstyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Quater">
                            <itemtemplate>
                                <asp:Label ID="lblQuaterID" runat="server" Text='<%# Eval("Quater") %>'></asp:Label>
                            </itemtemplate>
                            <itemstyle horizontalalign="Left"></itemstyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Inspection type">
                            <itemtemplate>
                                <asp:Label ID="lblInspectionType" runat="server" Text='<%# Eval("InspectionType") %>'></asp:Label>
                            </itemtemplate>
                            <itemstyle horizontalalign="Left"></itemstyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Financial Year">
                            <itemtemplate>
                                <asp:Label ID="lblFinancial_Year" runat="server" Text='<%# Eval("Financial_Year") %>'></asp:Label>
                            </itemtemplate>
                            <itemstyle horizontalalign="Left"></itemstyle>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Document Type">
                            <itemtemplate>
                                <asp:Label ID="lbldoctype" runat="server" Text='<%# Eval("Doc_Type") %>'></asp:Label>
                            </itemtemplate>
                            <itemstyle horizontalalign="Left"></itemstyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Document Name">
                            <itemtemplate>
                                <asp:Label ID="lblqualificatio" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </itemtemplate>
                            <itemstyle horizontalalign="Left"></itemstyle>
                        </asp:TemplateField>
                   
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="View Upleaded Document">
                            <itemtemplate>
                                <a href='<%#Eval("Intermarksheet") %>' target="_blank">View</a>
                            </itemtemplate>
                        </asp:TemplateField>
                     
                    </columns>
                </asp:GridView>
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

