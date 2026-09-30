using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public class CheckInput
{
    
    HttpCookie mCookie = null;
    int check = 0;
    int check_ddlcropyear = 0;
    int s = 0;
	public CheckInput()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     #region To Check Input for child controls of a Panel
    public int CheckInputForPanelChildControls(System.Web.UI.Page pg)
    {
        bool ck;
    //int check = 0;
    //int check_ddlcropyear = 0;
    //Page p = pg;
        try
        {

            foreach (System.Web.UI.Control ctrl in pg.Form.Controls)
            {
                if (ctrl.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    if (ctrl.Visible == true)
                    {
                        for (int j = 0; j < (ctrl as Panel).Controls.Count; j++)
                        {
                            //for (int k = 0; k < (ctl00_ContentPlaceHolder1_Controls[i] as GridView).Controls[j].Controls.Count; k++)
                            //{
                            if (ctrl.Controls[j].GetType() == typeof(System.Web.UI.WebControls.TextBox))
                            {
                                TextBox txt = (TextBox)ctrl.Controls[j];
                                if (txt.Visible == true)
                                {
                                    ck = BlackListSqlCheck(txt.Text.ToString());
                                    if (ck == true)
                                    {
                                        s = 1;
                                        //break;
                                        return s;
                                    }
                                    else
                                    {
                                        s = 0;
                                    }
                                }
                                //return 0;
                            }

                            else if (ctrl.Controls[j].GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                            {
                                DropDownList ddl = (DropDownList)ctrl.Controls[j];
                                if (ddl.Visible == true)
                                {
                                    ck = BlackListSqlCheck(ddl.SelectedItem.Text.ToString());
                                    if (ck == true)
                                    {
                                        s = 1;
                                        //break;
                                        return s;
                                    }
                                    else
                                    {
                                        s = 0;
                                    }
                                }
                            }
                            //else
                            //{
                            //    s = 0;
                            //}
                        }
                    }
                    
                }
                
            }
            return s;
        }
        catch (Exception ex)
        {
            return 1;
        }
    
    }
    #endregion
    #region To Check Input for login controls
    public int CheckInputforLoginControls(System.Web.UI.Page pg)
    {
        //Page p = pg;
        int val = 0;
        bool s;
        try
        {
            //for (int i = 0; i < pg.Controls.Count; i++)
            foreach (System.Web.UI.Control ctrl in pg.Form.Controls)
            {
                if (ctrl.GetType() == typeof(System.Web.UI.WebControls.HiddenField))
                {
                    HiddenField HD = (HiddenField)ctrl;
                    s = BlackListSqlCheck(HD.Value.ToString());
                    if (s == true)
                    {
                        val = 1;
                        break;
                    }
                    else
                    {
                        val = 0;
                    }
                }
                else
                {
                    val = 0;
                }
                if (ctrl.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    TextBox txt = (TextBox)ctrl;
                    s = BlackListSqlCheck(txt.Text.ToString());
                    if (s == true)
                    {
                        val = 1;
                        break;
                    }
                    else
                    {
                        val = 0;
                    }
                }
                else
                {
                    val = 0;
                }
                if (ctrl.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    DropDownList ddl = (DropDownList)ctrl;
                    s = BlackListSqlCheck(ddl.SelectedItem.Text.ToString());
                    if (s == true)
                    {
                        val = 1;
                        break;
                    }
                    else
                    {
                        val = 0;
                    }
                }
                else
                {
                    val = 0;
                }

            }
            return val;
        }
        catch (Exception ex)
        {
            return val;
        }
    }
    #endregion
    #region To Check Input for web controls 
    public int CheckInputforWebControls(System.Web.UI.Page pg)
    {
        //Page p = pg;
        int val = 0;
        bool s;
        try
        {
            //for (int i = 0; i < pg.Controls.Count; i++)
            foreach (System.Web.UI.Control ctrl in pg.Form.Controls)
            {
                
                if (ctrl.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    TextBox txt = (TextBox)ctrl;
                    s = BlackListSqlCheck(txt.Text.ToString());
                    if (s == true)
                    {
                        val = 1;
                        break;
                    }
                    else
                    {
                        val = 0;
                    }
                }
                else
                {
                    val = 0;
                }
                if (ctrl.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    DropDownList ddl = (DropDownList)ctrl;
                    s = BlackListSqlCheck(ddl.SelectedItem.Text.ToString());
                    if (s == true)
                    {
                        val = 1;
                        break;
                    }
                    else
                    {
                        val = 0;
                    }
                }
                else
                {
                    val = 0;
                }

            }
            return val;
        }
        catch (Exception ex)
        {
            return val;
        }
    }
    #endregion


    #region To Check Input for particular controls
    public int CheckInputforAControls(System.Web.UI.WebControls.TextBox txt)
    {
        //Page p = pg;
        int val = 0;
        bool s;
        try
        {
            //for (int i = 0; i < pg.Controls.Count; i++)
                         
                    s = BlackListSqlCheck(txt.Text.ToString());
                    if (s == true)
                    {
                        val = 1;
                        
                    }
                    else
                    {
                        val = 0;
                    }
               
                
            
            return val;
        }
        catch (Exception ex)
        {
            return val;
        }
    }
    #endregion


    
    public bool BlackListSqlCheck(string str_input)
    {
        string[] BalckList ={"=","#","$","%","^","&","*","|",";","<",">","'","(",")", "/*", "*/", "@@","cursor","exec","execute","nchar", "varchar", "nvarchar", "iframe"};
        if (str_input == "")
        {
            return false;
            
        }
        else
        {
            string lstr = str_input.ToLower();
            foreach (String bl in BalckList)
            {
                if(lstr.Contains(bl))
                {
                    return true;
                    break;
                }
                                
            }
            return false; 
        }

    }

   
    
}


