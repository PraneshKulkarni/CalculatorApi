using CalculatorAPI.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        SqlHelper helper = new SqlHelper();
        // GET: api/<DataController>
        [HttpGet]
        public string Get()
        {
            DataTable dt = helper.ExtecuteProcedureReturnData();
            var result = ConvertToList(dt);
            return JsonConvert.SerializeObject(result);
        }

        // POST api/<DataController>
        [HttpPost]
        public void Post([FromBody] int input1, int input2, string action)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Input1"; // Defining Name  
            parameter.SqlDbType = System.Data.SqlDbType.Int; // Defining DataType  
            parameter.Direction = ParameterDirection.Input; // Setting the direction  
            
            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@Input2"; // Defining Name  
            parameter1.SqlDbType = System.Data.SqlDbType.Int; // Defining DataType  
            parameter1.Direction = ParameterDirection.Input; // Setting the direction  

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Result"; // Defining Name  
            parameter2.SqlDbType = System.Data.SqlDbType.Int; // Defining DataType  
            parameter2.Direction = ParameterDirection.Input; // Setting the direction  

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Action"; // Defining Name  
            parameter3.SqlDbType = System.Data.SqlDbType.NVarChar; // Defining DataType  
            parameter3.Direction = ParameterDirection.Input; // Setting the direction  

            // Setting values of Parameter  
            parameter.Value = input1;
            parameter1.Value = input2;
            parameter2.Value = Performm(input1, input2, action);
            parameter3.Value = "Add";

            string result = helper.ExecuteProcedureReturnString(parameter, parameter1, parameter2, parameter3);
        }

        // Calculates result as per given action and returns result
        public float Performm(int left, int right, string action)
        {
            float sum = 0;
            switch (action)
            {
                case "Add":
                    sum = left + right;
                    return sum;

                case "Subtract":
                    sum = left - right;
                    return sum;

                case "Multiply":
                    sum = left * right;
                    return sum;

                case "Diviide":
                    sum = (int)left / right;
                    return sum;

                default:
                    return sum;
            }
        }

        // converts data table object into list array
        public List<DataOperation> ConvertToList(DataTable table)
        {
            List<DataOperation> dataOperations = new List<DataOperation>();
            foreach (DataRow row in table.Rows)
            {
                DataOperation data = new DataOperation();
                data.Id = (int)row["Id"];
                data.Input1 = (int)row["Input1"];
                data.Input2 = (int)row["Input2"];
                data.Result = (Double)row["Result"];
                data.Perform = (string)row["Action"];
                data.Delete = (bool)row["Delete"];
                data.dateTime = (DateTime)row["dateTime"];
                dataOperations.Add(data);
            }
            return dataOperations;
        }
    }
}
