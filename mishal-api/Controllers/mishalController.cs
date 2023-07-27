using System;
using System.Collections.Generic;
using System.Data;
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
        [Route("api/all_elevated/{code}")]
        public IHttpActionResult GetElevateDB(string code)
        {
            var connectionString = new EDBConnectionStringBuilder
            {
                Database = "DirectData",
                CharSet = "UNICODE",
                Type = "LOCAL",
                ConfigPath = "C:\\Program Files\\Atrex\\Shared\\Central",
                UID = "Administrator",
                PWD = "EDBDefault"
            };

            EDBConnection DataConnection = new EDBConnection(connectionString.ToString());
            try
            {
                // Open the connection to the database
                DataConnection.Open();

                // Step 5: Execute a Query
                string sqlQuery = "SELECT * FROM code where StockCode = '" + code + "'";
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
    }
}
