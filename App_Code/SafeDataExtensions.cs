using System;
using System.Data;
using System.Web;

public static class SafeDataExtensions
{
    // 1. DataRow से डेटा सुरक्षित तरीके से निकालने के लिए
    public static string ToSafeText(this DataRow row, string columnName)
    {
        if (row != null && row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
        {
            return HttpUtility.HtmlEncode(row[columnName].ToString());
        }
        return string.Empty;
    }

    // 2. सामान्य string/object के लिए
    public static string ToSafeText(this object value)
    {
        if (value == null || value == DBNull.Value)
            return string.Empty;

        return HttpUtility.HtmlEncode(value.ToString());
    }
}