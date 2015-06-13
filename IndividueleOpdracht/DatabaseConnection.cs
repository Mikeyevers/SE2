using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;

namespace IndividueleOpdracht
{
    public sealed class DatabaseConnection
    {
        // Deze class gemaakt samen met Teun Willems ©.

        private string connection_settings;

        public OracleConnection Connection
        {
            get;
            set;
        }

        public string DatabaseConnectionArgs = null;

        public DatabaseConnection()
        {
            connection_settings = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            Connection = new OracleConnection(connection_settings);
            Connection.Open();
        }

        public OracleCommand ExecuteCommand(string query, OracleParameter[] parameters)
        {
            OracleCommand command = new OracleCommand(query, Connection);

            command.CommandType = System.Data.CommandType.Text;

            if (parameters.Length > 0)
            {
                foreach (OracleParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            command.Prepare();

            return command;
        }

        public int ExecuteNonQuery(string query, OracleParameter[] parameters)
        {
            return ExecuteCommand(query, parameters).ExecuteNonQuery();
        }

        public int ExecuteNonQuery(string query, OracleParameter parameter)
        {
            return ExecuteNonQuery(query, new OracleParameter[] { parameter });
        }

        public OracleDataReader ExecuteQuery(string query, OracleParameter[] parameters)
        {
            return ExecuteCommand(query, parameters).ExecuteReader();
        }

        public OracleDataReader ExecuteQuery(string query, OracleParameter parameter)
        {
            return ExecuteQuery(query, new OracleParameter[] { parameter });
        }

        public OracleDataReader ExecuteQuery(string query)
        {
            return ExecuteQuery(query, new OracleParameter[] {});
        }
    }
}