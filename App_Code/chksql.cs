using System.Collections;
public class chksql
{
    public bool chksql_server(ArrayList ctrllist)
    {
        ArrayList sqlkeys = new ArrayList();
        sqlkeys.Add("drop");
        sqlkeys.Add("select");
        sqlkeys.Add("delete");
        sqlkeys.Add("truncate");
        sqlkeys.Add("alter");
        sqlkeys.Add(";");
        sqlkeys.Add("update");
        sqlkeys.Add("create");
        sqlkeys.Add(" ,");
        sqlkeys.Add("%");
        sqlkeys.Add("@");
        sqlkeys.Add("&");
        sqlkeys.Add("'");
        sqlkeys.Add("\"");
        sqlkeys.Add("\\\"");
        sqlkeys.Add("\\'");
        sqlkeys.Add("<");
        sqlkeys.Add(">");
        sqlkeys.Add("\\");
        sqlkeys.Add("$");
        sqlkeys.Add("+");
        sqlkeys.Add("(");
        sqlkeys.Add(")");
        sqlkeys.Add("|");
        sqlkeys.Add("lf ");
        sqlkeys.Add("cr ");
        sqlkeys.Add("=");
        bool chkstr = false;
        ArrayList ctrllst = ctrllist;
        int indx = 0;
        while (indx < ctrllst.Count)
        {
            if (chkstr == true)
            {
                break; // TODO: might not be correct. Was : Exit While
            }
            int i = 0;
            while (i < sqlkeys.Count)
            {
                string st = ctrllst[indx].ToString();
                string aray = st.ToLower();

                if (aray.Contains(sqlkeys[i].ToString()))

                {

                    chkstr = true;
                    break; // TODO: might not be correct. Was : Exit While
                }
                i = i + 1;
            }
            indx = indx + 1;
        }
        return chkstr;
    }
}




