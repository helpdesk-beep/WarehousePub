using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for JSON_Helper
/// </summary>
public class JSON_Helper
{

    public class LabelValue
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class TwoValues
    {
        public string y { get; set; }
        public string a { get; set; }
    }

    public class FourValues
    {
        public string y { get; set; }
        public string a { get; set; }
        public int b { get; set; }
        public int c { get; set; }
    }
    public class FiveValues
    {
        public string y { get; set; }
        public string a { get; set; }
        public int b { get; set; }
        public int c { get; set; }
        public string d { get; set; }
    }
    public class ThreeValues
    {
        public string y { get; set; }
        public string a { get; set; }
        public int b { get; set; }
    }

    public static string JsonSerializer<T>(T t)
    {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream();
        ser.WriteObject(ms, t);
        string jsonString = Encoding.UTF8.GetString(ms.ToArray());
        ms.Close();
        return jsonString;
    }
    /// <summary>
    /// JSON Deserialization
    /// </summary>
    public static T JsonDeserialize<T>(string jsonString)
    {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        T obj = (T)ser.ReadObject(ms);
        return obj;
    }
    public static List<LabelValue> GetJsonResult(DataTable dt)
    {
        List<LabelValue> ObjJsonResult = new List<LabelValue>();
        foreach (DataRow row in dt.Rows)
        {
            ObjJsonResult.Add(new LabelValue
            {
                label = row["label"].ToString(),
                value = row["value"].ToString(),
            });
        }
        return ObjJsonResult;
    }

    public static List<TwoValues> GetBarChartJsonResult(DataTable dt)
    {
        List<TwoValues> ObjJsonResult = new List<TwoValues>();
        foreach (DataRow row in dt.Rows)
        {
            ObjJsonResult.Add(new TwoValues
            {
                y = row["label"].ToString(),
                a = row["value"].ToString(),
            });
        }
        return ObjJsonResult;
    }

    public static List<FourValues> GetLineChartJsonResult(DataTable dt)
    {
        List<FourValues> ObjJsonResult = new List<FourValues>();
        foreach (DataRow row in dt.Rows)
        {
            ObjJsonResult.Add(new FourValues
            {
                y = row["Date"].ToString(),
                a = row["present"].ToString(),
                b = Convert.ToInt32(row["absent"]),
                c = Convert.ToInt32(row["others"]),
            });
        }
        return ObjJsonResult;
    }
    public static List<FourValues> GetLineChartJsonResultRTE(DataTable dt)
    {
        List<FourValues> ObjJsonResult = new List<FourValues>();
        foreach (DataRow row in dt.Rows)
        {
            ObjJsonResult.Add(new FourValues
            {
                y = row["Date"].ToString(),
                a = row["UnLockedProposals"].ToString(),
                b = Convert.ToInt32(row["LockedProposals"]),
                c = Convert.ToInt32(row["Students_proposed"]),
            });
        }
        return ObjJsonResult;
    }

    public static List<FourValues> GetLineChartJsonResultForShaladarpan(DataTable dt)
    {
        List<FourValues> ObjJsonResult = new List<FourValues>();
        foreach (DataRow row in dt.Rows)
        {
            ObjJsonResult.Add(new FourValues
            {
                y = row["Month"].ToString(),
                a = row["Level1"].ToString(),
                b = Convert.ToInt32(row["Level2"]),
                c = Convert.ToInt32(row["Level3"]),
            });
        }
        return ObjJsonResult;
    }

    public static List<FiveValues> GetJsonResultLineChart(DataTable dt)
    {
        List<FiveValues> ObjLineJsonResult = new List<FiveValues>();
        foreach (DataRow row in dt.Rows)
        {
            ObjLineJsonResult.Add(new FiveValues
            {
                y = row["Date"].ToString(),
                a = row["Male"].ToString(),
                b = Convert.ToInt32(row["Female"]),
                c = Convert.ToInt32(row["Total"]),
                //d = Convert.ToInt32(row["MSQ2"]),
            });
        }
        return ObjLineJsonResult;
    }
    public static List<FiveValues> GetJsonResultLineChartformat(DataTable dt)
    {
        List<FiveValues> ObjLineJsonResult = new List<FiveValues>();
        foreach (DataRow row in dt.Rows)
        {
            ObjLineJsonResult.Add(new FiveValues
            {
                y = row["label"].ToString(),
                a = row["Male"].ToString(),
                b = Convert.ToInt32(row["Female"]),
                c = Convert.ToInt32(row["Total"]),
                d = Convert.ToString(row["percentage"]),
                //d = Convert.ToInt32(row["MSQ2"]),
            });
        }
        return ObjLineJsonResult;
    }

    public static List<FiveValues> GetJsonResultBarChart(DataTable dt)
    {
        List<FiveValues> ObjLineJsonResult = new List<FiveValues>();
        foreach (DataRow row in dt.Rows)
        {
            ObjLineJsonResult.Add(new FiveValues
            {
                y = row["label"].ToString(),
                a = row["Male"].ToString(),
                b = Convert.ToInt32(row["Female"]),
                c = Convert.ToInt32(row["Total"]),
                d = Convert.ToString(row["percentage"]),
            });
        }
        return ObjLineJsonResult;
    }
   
    public static List<FourValues> GetStackBarJsonResult(DataTable dt)
    {
        List<FourValues> ObjJsonResult = new List<FourValues>();
        foreach (DataRow row in dt.Rows)
        {
            ObjJsonResult.Add(new FourValues
            {
                y = row["District"].ToString(),
                a = row["Employees"].ToString(),
                b = Convert.ToInt32(row["Present"]),
            });
        }
        return ObjJsonResult;
    }

    public static List<FourValues> GetStackBarJsonResultRTE(DataTable dt)
    {
        List<FourValues> ObjJsonResult = new List<FourValues>();
        foreach (DataRow row in dt.Rows)
        {
            ObjJsonResult.Add(new FourValues
            {
                y = row["District"].ToString(),
                a = row["TotalSchools"].ToString(),
                b = Convert.ToInt32(row["TotalProposals"]),
            });
        }
        return ObjJsonResult;
    }
}