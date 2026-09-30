using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;




public class DBAccess
{
    public  MySqlConnection moSqlConnection;
    public MySqlCommand moSqlCommand;
    public MySqlDataReader moSqlDataReader;
    public MySqlTransaction moSqlTransaction;
    public MySqlDataAdapter moSqlDataAdapter;
    public DataSet moDataSet;
  //  component c = new component();
    public DBAccess()
    {
        moSqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
        moSqlCommand = new MySqlCommand();
        moSqlCommand.Connection = moSqlConnection;
    }

    public DBAccess(string connectionString)
    {
        moSqlConnection = new MySqlConnection(connectionString);
        moSqlCommand = new MySqlCommand();
        moSqlCommand.Connection = moSqlConnection;
    }

    public DataSet records()
    {
        moSqlDataAdapter = new MySqlDataAdapter(moSqlCommand);
        moDataSet = new DataSet();
        moSqlDataAdapter.Fill(moDataSet);
        moSqlDataAdapter.Dispose();
        return moDataSet;
    }

    public DataTable records1()
    {
        moSqlDataAdapter = new MySqlDataAdapter(moSqlCommand);
        DataTable moDataSet1 = new DataTable();
        moSqlDataAdapter.Fill(moDataSet1);
        moSqlDataAdapter.Dispose();
        return moDataSet1;
    }

    public void openConnection()
    {
        if (moSqlConnection.State == ConnectionState.Closed)
            moSqlConnection.Open();
    }

    public object getParameter(String fsParameter)
    {
        return moSqlCommand.Parameters[fsParameter].Value;
    }

    public object getParameter(int fiParameterIndex)
    {
        return moSqlCommand.Parameters[fiParameterIndex].Value;
    }

    public int executeMyQuery()
    {
        try
        {
            openConnection();
            int result = moSqlCommand.ExecuteNonQuery();
            if (moSqlConnection.State == ConnectionState.Open)
                moSqlConnection.Close();
            return result;
        }
        catch (Exception ex)
        {
            rollBackTransaction();
            throw ex;
        }
    }

    public object executeMyScalar()
    {
        try
        {
            openConnection();
            object obj = moSqlCommand.ExecuteScalar();
            if (moSqlConnection.State == ConnectionState.Open)
                moSqlConnection.Close();
            return obj;
        }
        catch (Exception ex)
        {
            rollBackTransaction();
            throw ex;
        }
    }

    public DataSet returnSet()
    {
        moSqlDataAdapter = new MySqlDataAdapter(moSqlCommand);
        DataSet dsSet = new DataSet();
        moSqlDataAdapter.Fill(dsSet);
        moSqlDataAdapter.Dispose();
        if (moSqlConnection.State == ConnectionState.Open)
            moSqlConnection.Close();
        return dsSet;

    }

    public DataTable returnTable()
    {
        moSqlDataAdapter = new MySqlDataAdapter(moSqlCommand);
        DataTable dtTable = new DataTable();
        moSqlDataAdapter.Fill(dtTable);
        moSqlDataAdapter.Dispose();
        if (moSqlConnection.State == ConnectionState.Open)
            moSqlConnection.Close();
        return dtTable;
    }

    public void execute(String fsSQL, SQLType foSQLType)
    {
        openConnection();
        //moSqlCommand = new SqlCommand(fsSQL, moSqlConnection, moSqlTransaction);
        moSqlCommand = new MySqlCommand(fsSQL, moSqlConnection);
        moSqlCommand.CommandTimeout = 300;
        if (foSQLType == SQLType.IS_PROC)
            moSqlCommand.CommandType = CommandType.StoredProcedure;
    }

    public void addParam(String fsParameterName, MySqlDbType foSqlDbType, object foValue, SqlDirection foSqlDirection)
    {
        MySqlParameter loSqlParameter = new MySqlParameter();
        loSqlParameter.ParameterName = fsParameterName;
        loSqlParameter.MySqlDbType = foSqlDbType;
        if (foSqlDirection == SqlDirection.IN)
            loSqlParameter.Direction = ParameterDirection.Input;
        else if (foSqlDirection == SqlDirection.OUT)
            loSqlParameter.Direction = ParameterDirection.Output;
        loSqlParameter.Value = foValue;
        moSqlCommand.Parameters.Add(loSqlParameter);
    }

    public void addParam(String fsParameterName, MySqlDbType foSqlDbType, SqlDirection foSqlDirection)
    {
        MySqlParameter loSqlParameter = new MySqlParameter();
        loSqlParameter.ParameterName = fsParameterName;
        loSqlParameter.MySqlDbType = foSqlDbType;
        loSqlParameter.Size = 10;

        if (foSqlDirection == SqlDirection.IN)
            loSqlParameter.Direction = ParameterDirection.Input;
        else if (foSqlDirection == SqlDirection.OUT)
            loSqlParameter.Direction = ParameterDirection.Output;

        moSqlCommand.Parameters.Add(loSqlParameter);
    }

    public void addParam(String fsParameterName, MySqlDbType foSqlDbType, SqlDirection foSqlDirection, int fiLength)
    {
        MySqlParameter loSqlParameter = new MySqlParameter();
        loSqlParameter.ParameterName = fsParameterName;
        loSqlParameter.MySqlDbType = foSqlDbType;
        loSqlParameter.Size = fiLength;
        if (foSqlDirection == SqlDirection.IN)
            loSqlParameter.Direction = ParameterDirection.Input;
        else if (foSqlDirection == SqlDirection.OUT)
        {
            loSqlParameter.Direction = ParameterDirection.Output;
            loSqlParameter.Size = fiLength;
        }
        moSqlCommand.Parameters.Add(loSqlParameter);
    }

    public void addParam(String fsParameterName, object foValue, SqlDirection foSqlDirection)
    {
        MySqlParameter loSqlParameter = moSqlCommand.Parameters.AddWithValue(fsParameterName, foValue);
        if (foSqlDirection == SqlDirection.IN)
            loSqlParameter.Direction = ParameterDirection.Input;
        else if (foSqlDirection == SqlDirection.OUT)
            loSqlParameter.Direction = ParameterDirection.Output;
    }

    public void addParam(String fsParameterName, object foValue)
    {
        addParam(fsParameterName, foValue, SqlDirection.IN);
    }

    public void addParam(MySqlParameter foSqlParameter)
    {
        moSqlCommand.Parameters.Add(foSqlParameter);
    }

    public void closeConnection()
    {
        if (moSqlConnection.State == ConnectionState.Open)
            moSqlConnection.Close();


     
        if (moSqlCommand != null)
        {
            moSqlCommand.Dispose();
        }
        if (moSqlConnection != null)
        {
           // SqlConnection.ClearPool(moSqlConnection);
            moSqlConnection.Dispose();
        }
    }

    public enum SQLType
    {
        IS_QUERY = 1,
        IS_PROC = 2
    }

    public enum SqlDirection
    {
        IN = 1,
        OUT = 2
    }

    public void rollBackTransaction()
    {
        try
        {
            if (moSqlTransaction != null)
                moSqlTransaction.Rollback();
        }
        catch (Exception feException)
        { }
    }

    public void releasedCommand()
    {
        if (moSqlCommand != null)
        {
            moSqlCommand.Dispose();
            moSqlCommand = null;
        }
        //closeConnection();
    }
}
