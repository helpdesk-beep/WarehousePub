using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
/// <summary>
/// Summary description for Sqldatalayer
/// </summary>
public class Sqldatalayer
{
    DataTable dt;
    SqlDataAdapter adp;


    //Return Connection String     
    private string connString(string conName)
    {
        string str = string.Empty;

        try
        {
            str = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString();
        }
        catch { }
        return str;
    }


    public DataTable SelectData(string cnm, string text, ArrayList lstParam, bool isSp)
    {
        string str = connString(cnm);
        if (str != null)
        {
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if (isSp == true)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = text;

                if (lstParam != null)
                {
                    for (int i = 0; i < lstParam.Count; i++)
                    {
                        cmd.Parameters.Add(lstParam[i]);
                    }
                }
            }

            else
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = text;
            }

            try
            {
                con.Open();
                adp = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adp.Fill(dt);

            }
            catch (Exception ex)
            {

            }

            finally
            {
                con.Close();
                con.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }
        return dt;

    }


    public int ExecuteScalar(string cnm, string text, ArrayList lstParam, bool isSp)
    {
        string str = connString(cnm);
        int rvalue = 0;

        if (str != null)
        {
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if (isSp == true)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = text;

                if (lstParam != null)
                {
                    for (int i = 0; i < lstParam.Count; i++)
                    {
                        cmd.Parameters.Add(lstParam[i]);
                    }
                }
            }

            else
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = text;
            }

            try
            {
                con.Open();
                rvalue = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

            }

            finally
            {
                con.Close();
                con.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }

        }

        return rvalue;


    }


    public int ExecuteNonQuery(string cnm, string text, ArrayList lstParam, bool isSp)
    {
        string str = connString(cnm);
        int rvalue = 0;

        if (str != null)
        {
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if (isSp == true)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = text;

                if (lstParam != null)
                {
                    for (int i = 0; i < lstParam.Count; i++)
                    {
                        cmd.Parameters.Add(lstParam[i]);
                    }
                }
            }

            else
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = text;
            }

            try
            {
                con.Open();
                rvalue = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }

            finally
            {
                con.Close();
                con.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }

        }

        return rvalue;


    }



}