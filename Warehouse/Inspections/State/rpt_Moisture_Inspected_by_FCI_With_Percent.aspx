<%@ Page Title="" Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="~/Inspections/State/rpt_Moisture_Inspected_by_FCI_With_Percent.aspx.cs" Inherits="Inspections_State_rpt_Moisture_Inspected_by_FCI_With_Percent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Godown Moisture Report with District Subtotal</title>
   <style>
    /* ग्रिडव्यू का बेसिक फॉन्ट और बॉर्डर सेटिंग्स */
    .grid-view { 
        font-family: 'Segoe UI', Arial, sans-serif; 
        border-collapse: collapse; 
        width: 100%; 
        margin-top: 15px; 
        box-shadow: 0 2px 5px rgba(0,0,0,0.1);
    }
    
    /* MPWLC थीम के अनुसार हेडर का मुख्य मैरून (Maroon) कलर */
    .grid-view th { 
        background-color: #cc0000; /* इमेज का मुख्य रेड/मैरून शेड */
        color: #ffffff; 
        padding: 12px 10px; 
        text-align: left; 
        font-size: 14px; 
        font-weight: 600;
        border: 1px solid #b30000; 
        text-transform: uppercase;
        letter-spacing: 0.5px;
    }
    
    /* डेटा सेल्स की स्टाइलिंग */
    .grid-view td { 
        padding: 10px 8px; 
        border: 1px solid #e0e0e0; 
        font-size: 13px; 
        color: #333333;
    }
    
    /* वेलकम बार से मैच करता हुआ 'District Subtotal' का गोल्डन/टैन (Gold/Tan) लुक */
    .subtotal-row { 
        background-color: #dfab6c; /* इमेज के वेलकम बार का बैकग्राउंड कलर */
        font-weight: bold; 
        color: #000000; /* स्पष्ट दिखने के लिए ब्लैक टेक्स्ट */
    }
    
    /* सबटोटल के ऊपर और नीचे एक हल्की डार्क लाइन ताकि यह अलग से हाइलाइट हो */
    .subtotal-row td {
        border-top: 2px solid #b37424 !important;
        border-bottom: 2px solid #b37424 !important;
    }
    
    /* डेटा को साफ़ दिखाने के लिए अल्टरनेटिंग रो (Alternate Row) का बहुत हल्का शेड */
    .grid-view tr:nth-child(even) { 
        background-color: #f9f9f9; 
    }
    
    /* जब यूजर डेटा रो पर माउस ले जाए तो हल्का सा इफ़ेक्ट (Hover) */
    .grid-view tr:hover:not(.subtotal-row):not(:first-child) {
        background-color: #f1f1f1;
    }
</style></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding: 20px;">
        <h2>Godown Moisture Inspection Report</h2>
        <hr />

        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False"
            CssClass="grid-view" OnRowDataBound="gvReport_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Region Name" HeaderText="Region Name" />
                <asp:BoundField DataField="District Name" HeaderText="District Name" />
                <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                <asp:BoundField DataField="Total Moisture" HeaderText="Total Moisture" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Stack Moisture Sent to DM MPSCSC/FCI" HeaderText="Moisture Sent to DM" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FCI Inspected Stack" HeaderText="FCI Inspected Stack" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Percantage" HeaderText="Percentage (%)" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

