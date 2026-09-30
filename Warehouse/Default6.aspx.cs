using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    [System.Web.Services.WebMethod]
    public static object GetAllBillsData()
    {
        string billNos = HttpContext.Current.Session["BillNos"].ToString();
        if (string.IsNullOrEmpty(billNos)) return null;

        string constr = System.Configuration.ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        List<object> billList = new List<object>();

        using (SqlConnection con = new SqlConnection(constr))
        {
            string[] billArray = billNos.Split(',');
            foreach (string billNo in billArray)
            {
                if (string.IsNullOrWhiteSpace(billNo)) continue;

                // 1. Fetch Main Bill Data
                DataTable dtBill = new DataTable();
                using (SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_For_Print_New_New", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Bill_Number", billNo.Trim());
                    new SqlDataAdapter(cmd).Fill(dtBill);
                }

                // 2. Fetch DSC Details
                DataTable dtDsc = new DataTable();
                string dscQry = "SELECT DSC_User_Type, DSC_Serial_No, DSC_Holder_Name, Client_Ip, " +
                                "Convert(varchar(10),CreatedDate,103) as CreatedDate " +
                                "FROM tbl_Digitally_Signed_Bill_Details_NAFED " +
                                "WHERE Ref_Bill_No=@BillNo AND DSC_User_Type IN ('B','R')";
                using (SqlCommand cmd = new SqlCommand(dscQry, con))
                {
                    cmd.Parameters.AddWithValue("@BillNo", billNo.Trim());
                    new SqlDataAdapter(cmd).Fill(dtDsc);
                }

                if (dtBill.Rows.Count > 0)
                {
                    billList.Add(new
                    {
                        Header = dtBill.AsEnumerable().First().Table.Columns.Cast<DataColumn>()
                                     .ToDictionary(c => c.ColumnName, c => dtBill.Rows[0][c.ColumnName]),
                        Grid = dtBill.AsEnumerable().Select(r => new {
                            Commodity = r["Commodity"],
                            Bill_Month = r["Bill_Month"],
                            Dates_Period = r["Dates_Period"],
                            Opening = r["Opening_Balance"],
                            In = r["Receive_Bags"],
                            Out = r["Issue_Bags"],
                            Closing = r["Closing_Balance"],
                            Total = r["Total_Charges"]
                        }).ToList(),
                        DSC = dtDsc.AsEnumerable().Select(r => new {
                            Type = r["DSC_User_Type"],
                            Name = r["DSC_Holder_Name"],
                            Serial = r["DSC_Serial_No"],
                            IP = r["Client_Ip"],
                            Date = r["CreatedDate"]
                        }).ToList()
                    });
                }
            }
        }
        return billList;
    }
}