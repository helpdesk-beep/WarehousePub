using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class BasePage : System.Web.UI.Page
{
    // यह फ़ंक्शन पूरे प्रोजेक्ट के हर पेज के Render होने से ठीक पहले ऑटोमैटिक चलेगा
    protected override void Render(HtmlTextWriter writer)
    {
        AutoEncodeControls(this);
        base.Render(writer);
    }

    private void AutoEncodeControls(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            Label lbl = c as Label;
            TextBox txt = c as TextBox;

            if (lbl != null)
            {
                lbl.Text = HttpUtility.HtmlEncode(lbl.Text);
            }
            else if (txt != null && txt.ReadOnly)
            {
                txt.Text = HttpUtility.HtmlEncode(txt.Text);
            }

            if (c.HasControls())
            {
                AutoEncodeControls(c); // Recurse through all child controls
            }
        }
    }
}