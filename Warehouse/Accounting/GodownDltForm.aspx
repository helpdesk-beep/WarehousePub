<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="GodownDltForm.aspx.cs" Inherits="Accounting_GodownDltForm"  EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    
 <link type="text/css" rel="Stylesheet" href="css/style_new.css" />
    
    <script src="../JS/Jquery.3.6.0.js"></script>

    
<%--    <script language="javascript" type="text/javascript" src="js/MD5.js"></script>

    <script language="javascript" type="text/javascript" src="js/chksql.js"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <script  type="text/javascript">  
         
            function ConfirmOnDelete() {
                if (confirm("Are you sure want to Delete This Inspection ?") == true)
                    return true;
                else
                    return false;
            }
    }
     
        </script>


     

    <style>
        body {
                font-size: 15px;
        }
    </style>

    <div style="text-align:center"> <h2 style="font-size: 25px;text-transform: uppercase;font-weight: revert;color: red;"> ऐसे  गोडाउन जो की कई वर्षो से  उपयोग मे नहीं है एवं जिनका कोई भी किराया भुगतान बाकी नहीं हैं  केवल उन्हे ही हटाए | </h2>
    </div>

    <asp:Panel ID="StoreGrid" runat="server"  >
             <asp:GridView ID="GridView1"  CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" runat="server" AutoGenerateColumns="false" Visible="true" OnRowUpdating="GridView1_RowUpdating" Width="100%"  >
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
                        <asp:HiddenField ID="hdngodownid" runat="server" Value='<%#Eval("Godown_ID") %>'/>
                        <td style="text-align: center;"><asp:Label ID="lblComment" runat="server" Text='<%#Eval("Godown_ID") %>'/> </td>
                        <td style="text-align: center;"><asp:Label ID="Label16" runat="server" Text='<%#Eval("Godown_Name") %>'/> </td>
                         <td style="text-align: center;"><asp:Button ID="btn_Update" runat="server" Text="Remove" OnClientClick="return confirm('Do you want to Remove this Godown?');" CommandName="Update"/>  </td>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
    </asp:Panel>


  


            
    

      
</asp:Content>

