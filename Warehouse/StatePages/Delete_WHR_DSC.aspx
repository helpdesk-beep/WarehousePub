<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="~/StatePages/Delete_WHR_DSC.aspx.cs" Inherits="StatePages_Delete_WHR_DSC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
          .button {
    padding: 6px 20px;
    font-size: 14px;
    font-weight: bold;
    border-radius: 6px;
    border: none;
    cursor: pointer;
    transition: 0.3s;
}

.button.green {
    background-color: #28a745;
    color: white;
}

.button.green:hover {
    background-color: #218838;
}

.button.blue {
    background-color: #007bff;
    color: white;
}

.button.blue:hover {
    background-color: #0056b3;
}

  </style>
  <script type="text/javascript">
      function preventInput(evnt) {
          //Checked In IE9,Chrome,FireFox
          if (evnt.which != 9) evnt.preventDefault();
      }
  </script>
  <style type="text/css">
      .modalBackground {
          background-color: Black;
          filter: alpha(opacity=60);
          opacity: 0.6;
      }

      .modalPopup {
          background-color: #FFFFFF;
          width: 80%;
          border: 3px solid #0DA9D0;
          border-radius: 12px;
          padding: 0;
      }

          .modalPopup .header {
              background-color: #D69758;
              height: 30px;
              color: White;
              line-height: 30px;
              text-align: center;
              font-weight: bold;
              border-top-left-radius: 6px;
              border-top-right-radius: 6px;
          }

          .modalPopup .body {
              min-height: 50px;
              line-height: 30px;
              text-align: center;
              font-weight: bold;
          }

          .modalPopup .footer {
              padding: 6px;
          }

          .modalPopup .yes, .modalPopup .no {
              height: 23px;
              color: White;
              line-height: 23px;
              text-align: center;
              font-weight: bold;
              cursor: pointer;
              border-radius: 4px;
          }

          .modalPopup .yes {
              background-color: #2FBDF1;
              border: 1px solid #0DA9D0;
          }

          .modalPopup .no {
              background-color: #9F9F9F;
              border: 1px solid #5C5C5C;
          }
  </style>
  <style type="text/css">
      #popupwin {
          position: fixed;
          top: 0;
          left: 0;
          width: 90%;
          height: 90%;
          background-color: #000;
          filter: alpha(opacity=65);
          -moz-opacity: 0.7;
          display: none;
          opacity: 0.7;
          z-index: 100;
      }

      .pop a {
          text-decoration: none;
      }

      .popup {
          width: 100%;
          height: 98%;
          margin: 0 auto;
          position: fixed;
          z-index: 101;
          padding-left: 90px;
      }

      .pop {
          /*min-width: 900px;*/
          width: 80%;
          min-height: 150px;
          margin: 0px auto;
          background: #FFFFFF;
          position: relative;
          z-index: 103;
          padding: 10px;
          border-radius: 5px;
          box-shadow: 0 5px 10px #000;
          /*margin-top:200px;*/
      }

          .pop p {
              color: #555555;
              text-align: justify;
              font-size: medium;
          }

              .pop p a {
                  color: #d91900;
              }

          .pop .x {
              float: right;
              height: 35px;
              /*left: 22px;*/
              position: relative;
              /*top: -20px;*/
              width: 35px;
          }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <fieldset style="width: 100%; border: 2px solid navy; margin-left: 10px; margin-right: 10px">
     <center>
         <div>
             <table cellpadding="0" cellspacing="0" style="width: 100%">
                 <tr>
                     <td align="center" valign="top">

                         <center>
                             <div>
                                 <table cellpadding="0" cellspacing="0" style="width: 100%">
                                     <tr style="background-color: #0bb6e6; height: 25px">
                                         <td colspan="8" align="center">
                                             <asp:Label ID="lblGodownMaster" runat="server" Text="Delete Whr Digital Sign" Font-Bold="true"
                                                 Font-Size="12pt" ForeColor="whitesmoke"></asp:Label>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="height: 5px" colspan="8"></td>
                                     </tr>
                                     <tr>
                                         <td align="center" style="width: 200px; font-size:large"><b>Whr ID :-</b>
                                                       <asp:TextBox ID="txttjvsbillno" runat="server" AutoPostBack="false" Height="25px" Width="250px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp
                                           
                                              <asp:Button ID="Button2" runat="server" Text="SEARCH" Visible="true" Width="100px" ValidationGroup="A"
                                                  CssClass="BTNBLUE" OnClick="Button2_Click" />


                                         </td>
                                         <%-- <td align="center" style="width: 200px">
                                                       <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" Width="100px" ValidationGroup="A"
                                                           CssClass="BTNBLUE" OnClick="btnSubmit_Click"/>
                                         </td>--%>
                                     </tr>
                                     <tr>
                                         <td colspan="8" valign="top" align="center" style="padding-top:10px">
                                             <asp:GridView ID="Passing_Gridview" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                                 BorderColor="#CC9966" BorderStyle="Double" BorderWidth="1px" CellPadding="5"
                                                 CellSpacing="2">
                                                 <Columns>
                                                     <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                                         <ItemTemplate>
                                                             <%# Container.DataItemIndex + 1 %>
                                                             <asp:HiddenField ID="hdnWhr_No" runat="server" Value='<%# Eval("Whr_No") %>' />
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                                     <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                                                     <asp:TemplateField HeaderText="Godown Name">
                                                         <ItemTemplate>
                                                             <asp:Label runat="server" ID="lblGodown_Name" Width="100%" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Whr No">
                                                         <ItemTemplate>
                                                             <asp:Label runat="server" ID="lblWhr_No" Width="100%" Text='<%# Eval("Whr_No")%>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="BM Submission Date">
                                                         <ItemTemplate>
                                                             <asp:Label runat="server" ID="lblBMSubmissionDate" Width="100%" Text='<%# Eval("BMSubmissionDate")%>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Center" Width="30%" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Godown Submission Date">
                                                         <ItemTemplate>
                                                             <asp:Label runat="server" ID="lblGodownSubmissionDate" Width="100%" Text='<%# Eval("GodownSubmissionDate")%>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Center" Width="30%" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Digitally Signed Date">
                                                         <ItemTemplate>
                                                             <asp:Label runat="server" ID="lblDigitallySignedWhrDate" Width="100%" Text='<%# Eval("DigitallySignedWhrDate")%>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Center" Width="30%" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Digitally Signature Date">
                                                         <ItemTemplate>
                                                             <asp:Label runat="server" ID="lblDigitallySignaturedDate" Width="100%" Text='<%# Eval("DigitallySignaturedDate")%>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="DSC WHR XML Date">
                                                         <ItemTemplate>
                                                             <asp:Label runat="server" ID="lblDSCWHRXMLDate" Width="100%" Text='<%# Eval("DSCWHRXMLDate")%>'></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                     </asp:TemplateField>
                                                 </Columns>
                                                 <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                                 <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                                 <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                 <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                                     Height="20px" Font-Size="10pt" />
                                                 <AlternatingRowStyle BackColor="#eeeeee" />
                                             </asp:GridView>
                                         </td>
                                     </tr>
                                     <tr id="divdelete" runat="server" visible="false">
                                         <td align="center" style="margin-top:10px; padding: 10px;">
                                             <asp:Button ID="Button1" runat="server" Text="Delete" Visible="true" Width="100px" ValidationGroup="A"
                                                 CssClass="BTNBLUE" OnClick="btnSubmit_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                         </td>
                                     </tr>
                                 </table>


                             </div>
                         </center>

                     </td>
                 </tr>

             </table>

         </div>
     </center>
 </fieldset>
</asp:Content>

