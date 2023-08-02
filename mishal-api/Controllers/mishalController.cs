using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Elevate.ElevateDB.Data;


namespace mishal_api.Controllers
{
    public class mishalController : ApiController
    {
        // GET: api/mishal
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet]
        [Route("api/all_elevated-1/{code}")]
        public IHttpActionResult GetElevateDB(string code)
        {
            var connectionString = new EDBConnectionStringBuilder
            {
                Database = "DirectData",
                CharSet = "UNICODE",
                Type = "LOCAL",
                ConfigPath = "C:\\Program Files\\Atrex\\Shared\\Central",
                UID = "ADMINISTRATOR",
                PWD = "EDBDefault"
            };

            EDBConnection DataConnection = new EDBConnection(connectionString.ToString());
            try
            {
                // Open the connection to the database
                DataConnection.Open();

                // Step 5: Execute a Query
                //where StockCode = '" + code + "'
                string sqlQuery = "SELECT * FROM code";
                EDBCommand command = new EDBCommand(sqlQuery, DataConnection);

                // Step 6: Retrieve Data
                EDBDataAdapter adapter = new EDBDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                if (ds != null)
                    return Ok(ds.Tables[0]);
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                // Close the Connection
                DataConnection.Close();
            }
        }


        [HttpGet]
        [Route("api/all_elevated/{code}")]
        public IHttpActionResult GetElevateDB2(string code)
        {
            var connectionString = new EDBConnectionStringBuilder
            {
                Database = "DirectData",
                CharSet = "UNICODE",
                Type = "LOCAL",
                ConfigPath = "C:\\Program Files\\Atrex\\Shared\\Central",
                UID = "ADMINISTRATOR",
                PWD = "EDBDefault",
            };

            EDBConnection DataConnection = new EDBConnection(connectionString.ToString());
            string state = DataConnection.State.ToString();
            try
            {
                // Open the connection to the database
                if (state == "Closed")
                {
                    DataConnection.Open();

                    // Step 5: Execute a Query
                    //where StockCode = '" + code + "'
                    string sqlQuery = "SELECT * FROM code";
                    EDBCommand command = new EDBCommand(sqlQuery, DataConnection);

                    // Step 6: Retrieve Data
                    EDBDataAdapter adapter = new EDBDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    if (ds != null)
                        return Ok(ds.Tables[0]);
                    return NotFound();
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                DataConnection.Close();
                return InternalServerError(ex);
            }
            finally
            {
                // Close the Connection
                DataConnection.Close();
            }
        }

        [HttpGet]
        [Route("api/elevate")]
        public IHttpActionResult GetElevated()
        {
            string connectionString = "Data Source=192.168.1.76;Initial Catalog=Inventory;User ID=sa;Password=password";
            EDBConnection connection = new EDBConnection(connectionString);

            try
            {
                connection.Open();
                EDBCommand command = new EDBCommand("SELECT * FROM Audit", connection);
                EDBDataAdapter adapter = new EDBDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                if (ds != null)
                    return Ok(ds.Tables[0]);
                return NotFound();
            }
            catch (Exception ex)
            {
                connection.Close();
                return InternalServerError(ex);
            }
            
        }
    }
}
