using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CalculatorAPI.DataLayer
{
    public  class SqlHelper
    {
        static string connString = "Server=SPL1666;Initial Catalog=DataOperations;Integrated Security=True;";

        public  string ExecuteProcedureReturnString(params SqlParameter[] paramters)
        {
            string result = "";
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "AddDataToOperation";
                    if (paramters != null)
                    {
                        command.Parameters.AddRange(paramters);
                    }
                    sqlConnection.Open();
                    var ret = command.ExecuteScalar();
                    if (ret != null)
                        result = Convert.ToString(ret);
                }
            }
            return result;
        }

        public DataTable ExtecuteProcedureReturnData()
        {
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SelectTop10Operations";
                  
                    sqlConnection.Open();

                    DataTable dt = new DataTable();
                    dt.Load(sqlCommand.ExecuteReader());
                    sqlConnection.Close();
                    return dt;
                }
            }
        }

    }
}
