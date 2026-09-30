<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Geo_map_for_Distance_Prct_Godown.aspx.cs" Inherits="District_Geo_map_for_Distance_Prct_Godown" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Geo godown</title>
     <script type="text/javascript" style="width:800px">
         function flasher() {
             if (document.getElementById("lbl_h1")) {
                 var h1 = document.getElementById("lbl_h1");
                 var h2 = document.getElementById("lbl_h2");
                 h1.style.color = (h1.style.color == 'purple' ? 'green' : 'purple');
                 h2.style.color = (h2.style.color == 'purple' ? 'green' : 'purple');
                 setTimeout('flasher()', 2000);
             }
         }
    </script>
    <style type="text/css">
        .auto-style3 {
            width: 1607px;
        }
        .auto-style6 {
            width: 98%;
            height: 397px;
        }
        .auto-style7 {
            width: 1422px;
        }
        .auto-style9 {
            margin-left: 0px;
        }
        .auto-style10 {
            width: 1277px;
            height: 155px;
        }
        .auto-style11 {
            height: 155px;
            width: 745px;
        }
        .auto-style12 {
            width: 1277px;
        }
        .auto-style13 {
            width: 471px;
        }
    </style>
</head>
<body onload="flasher()">
    <form id="form1" runat="server">
        <table style="border:double">
            <tr >
                <td align="Centre" class="auto-style3" style="text-align:center" >
                    <asp:Label ID="lbl_Heading" runat="server" Text="Nearest Vacant Godown From Procurement Centre" BackColor="#FF99FF" Font-Bold="True" Font-Size="X-Large" ForeColor="#000099" ></asp:Label>
                    

                    <br />
                    

                </td>

            </tr>
            <tr>
                <td align="left" class="auto-style3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="Procurement Id :  "></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:TextBox ID="txt_pcid" runat="server" AutoPostBack="true" OnTextChanged="txt_pcid_TextChanged"></asp:TextBox>
                    <br />
                </tr>
                    </td>
            <tr>
                <td align="left">   
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                      <asp:Label ID="Label2" runat="server" Text="PRC_Name :"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbl_prcname" runat="server" Text="" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                     
           <tr>
                    <br />
                <td align="left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label4" runat="server" Text="PRC_District Id :"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbl_distid" runat="server" Text="" Font-Bold="True" ForeColor="Red"></asp:Label>
                   
                    </td>
               </tr>
                    <br />

            <tr>        
                <td align="left">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label6" runat="server" Text="PRC District Name :"></asp:Label>
                    &nbsp;&nbsp;<asp:Label ID="lbl_distname" runat="server" Text="" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                    <br />
                    <br />
            <tr>
                <td align="center">
                    <br />
&nbsp;<asp:Label ID="lbl_VactRange" runat="server" Text="Entre Required Vacant Capacity Greater than(in MT)  :"></asp:Label>
                    &nbsp; &nbsp;<asp:TextBox ID="txt_vctrange" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;
                     <asp:Label ID="lbl_Distance" runat="server" Text="Enter Required Distance within(in km) :"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txt_distance" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    </td>
                </tr>
                    &nbsp;
                 <tr>
                    <td align="center">
                        <asp:Button ID="btn_dist" runat="server" Text="Within District" OnClick="btn_dist_Click" Width="122px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Button ID="btn_interdist" runat="server" Text="Inter District" OnClick="btn_interdist_Click" />
                        <br />
                    </td>
                    
                </tr>
                    
               
            </table>
          <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <div class="auto-style7">


        <table style="background-color: #fefbea" class="auto-style6"  width="100%" >
            <tr>

                <td align="left" valign="top" class="auto-style10">
                    <table  border="1" cellspacing="0" cellpadding="0" style="border-color: Navy" class="auto-style13">
                        <tr>
                            <td colspan="2" align="center">
                                <span style="font-size: 12pt; font-weight: bolder; color: Green">
                                    Nearest Vacant Godowns </span></td>
                        </tr>
                         <tr>
                            <td align="center">
                                <img alt="" src="icons/Yellow.png" width="20px" height="30px" />
                            </td>
                            <td align="left">
                                <span style="font-size: 10pt; font-weight: bolder; color: Navy">Procurement Centre</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img alt="" src="icons/Green.png" width="20px" height="30px" />
                            </td>
                            <td align="left">
                                <span style="font-size: 10pt; font-weight: bolder; color: Navy">Storage in Godown is
                                    Empty</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img alt="" src="icons/Red.png" width="20px" height="30px" />
                            </td>
                            <td align="left">
                                <span style="font-size: 10pt; font-weight: bolder; color: Navy">More Than 80% Storage
                                    in Godwon</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img alt="" src="icons/Blue.png" width="20px" height="30px" />
                            </td>
                            <td align="left">
                                <span style="font-size: 10pt; font-weight: bolder; color: Navy">Space Available For
                                    Storage in Godown</span>
                            </td>
                        </tr>
                                                                   

                    </table>
                    <br />
                   
                    <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server"  />
                            
                </td >
                <td align="right" class="auto-style11" style="text-align:center">
                    

                    <asp:Label ID="lbl_interdist" runat="server"  align="centre" Text="Inter District Distance Between Procuremnet to Godown" Font-Bold="True" Font-Size="Large" ForeColor="Red" ></asp:Label>
&nbsp;<asp:Label ID="lbl_withindist" runat="server" align="centre" Text="Distance Between Procuremnet to Godown Within District" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
                    

                    <br />
                    

                 <asp:GridView ID="grd_Distance" runat="server" AutoGenerateColumns="false" Width="553px" CssClass="auto-style9">
                             <Columns>

                                 <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-BackColor="#009933">
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 
                                  <asp:BoundField HeaderText="District Name" DataField="District_Name" HeaderStyle-BackColor="#009933" />
                                                         
                                                                 
                                  <asp:BoundField HeaderText="Godown ID" DataField="GodownID" HeaderStyle-BackColor="#009933" />                            
                                <asp:BoundField HeaderText="Godown Name" DataField="Godown_Name" HeaderStyle-BackColor="#009933" />
                                 <asp:BoundField HeaderText="Godown Capacity" DataField="Godown_Capacity" DataFormatString="{0:0.00}" HeaderStyle-BackColor="#009933" /> 
                                 <asp:BoundField HeaderText="Utilized Capacity" DataField="Utilized_Capacity" DataFormatString="{0:0.00}" HeaderStyle-BackColor="#009933" /> 
                                 <asp:BoundField HeaderText="Vacant Capacity" DataField="Vacant_Capacity" DataFormatString="{0:0.00}" HeaderStyle-BackColor="#009933" /> 
                                 <asp:BoundField HeaderText="Arial_Distance" DataField="Arial_Distance" DataFormatString="{0:0.0}" HeaderStyle-BackColor="#009933"/>                                                    

                             </Columns>
                         </asp:GridView>
                    <br />
                    

                    </td>
            </tr>
            <tr>
                <td align="left" class="auto-style12">
                    &nbsp;</td>
            </tr>     
                        
            

        </table>
    </div>
         


    </form>
</body>
</html>
