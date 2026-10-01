<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Total_Sulk_Opening.aspx.cs" Inherits="Region_Total_Sulk_Opening" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>

    
     <script type="text/javascript" src="https://www.google.com/jsapi">
    </script>
    <script type="text/javascript">

        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                [google.elements.transliteration.LanguageCode.HINDI],
                transliterationEnabled: true
            };

            // Create an instance on TransliterationControl with the required
            // options.
            var control =
            new google.elements.transliteration.TransliterationControl(options);

            // Enable transliteration in the textbox with id
            // 'transliterateTextarea'.
            control.makeTransliteratable(['txtremark']);

        }
        google.setOnLoadCallback(onLoad);
    </script>
    <table align="center">
        <tr>
            <td colspan="4" align="center">
<h3>लंबित राशि विवरण</h3>
            </td>
        </tr>
        <tr>
           <td>
               District:
           </td> 
            <td>
                <asp:dropdownlist runat="server" id="ddldist" AutoPostBack="True" OnSelectedIndexChanged="ddldist_SelectedIndexChanged"></asp:dropdownlist>
            </td>
       
       
           <td>
               जमाकर्ता:
           </td> 
            <td>
                <asp:dropdownlist runat="server" id="ddldeposiotr" AutoPostBack="True" OnSelectedIndexChanged="ddldeposiotr_SelectedIndexChanged"></asp:dropdownlist>
            </td>
        </tr>
        <tr>
            <td>

            </td>
            <td>

            </td>

            <td>पूर्व की लंबित राशि:</td>
           <td>
               <asp:Label ID="lbllambitrashi" runat="server" Text="0"></asp:Label>
           </td>
        </tr>
        <tr>
           <td>
              Date:
           </td> 
            <td>
                
                <asp:TextBox ID="txtdate" runat="server"></asp:TextBox>
                
                <asp:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtdate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                </asp:CalendarExtender>
                
            </td>
       
       
           <td>
               लंबित राशि(यदि कोई नया बिल है):
           </td> 
            <td>
                <asp:TextBox ID="txtoldlambitrashi" runat="server">0</asp:TextBox>
                <asp:FilteredTextBoxExtender ID="txtoldlambitrashi_FilteredTextBoxExtender" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtoldlambitrashi">
                </asp:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td>

            </td>
        </tr>
         <tr>
           <td>
               प्रस्तुत राशि: </td> 
            <td>
                
                <asp:TextBox ID="txtprastutrashi" runat="server">0</asp:TextBox>
                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtprastutrashi">
                </asp:FilteredTextBoxExtender>
            </td>
       
       
           <td>
               प्राप्त राशि:
           </td> 
            <td>
                <asp:TextBox ID="txtpraptrashi" runat="server">0</asp:TextBox>
                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtpraptrashi">
                </asp:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td>

            </td>
        </tr>
        <tr>
            <td>रोकी गई राशि </td>
           <td>
               <asp:TextBox ID="txtrokigairashi" runat="server" MaxLength="100" class="text"></asp:TextBox>
           </td>
            <td>

                रोकी गई राशि का विवरण:</td>
            <td>
                <asp:TextBox ID="txtremark" runat="server" MaxLength="250" class="text" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>

                &nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:button runat="server" text="Submit" id="btnsubmit" OnClick="btnsubmit_Click" />
            </td>
            <td colspan="2">
                <asp:label runat="server" text="" id="lblmsg"></asp:label>
            </td>
        </tr>
    </table>
</asp:Content>

