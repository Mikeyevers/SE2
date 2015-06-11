using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace IndividueleOpdracht
{
    public class Administration
    {
        DatabaseConnection DatabaseConnection;
        public Administration()
        {
            DatabaseConnection = new DatabaseConnection();
        }

        public bool Inloggen(string emailadres, string wachtwoord)
        {
            // Ophalen van adverteerderNummer bij opgegeven emailadres en wachtwoord.
            string query = "SELECT adverteerdernummer " +
                           "FROM ADVERTEERDER " +
                           "WHERE LOWER(emailadres) = :EMAILADRES " +
                           "AND wachtwoord = :WACHTWOORD";
            OracleParameter emailadresParameter = new OracleParameter(":EMAILADRES", emailadres.ToLower());
            OracleParameter wachtwoordParameter = new OracleParameter(":WACHTWOORD", wachtwoord);
            OracleParameter[] parameters = new OracleParameter[] { emailadresParameter, wachtwoordParameter };

            OracleDataReader odr = DatabaseConnection.ExecuteQuery(query, parameters);

            if (odr.Read())
            {
                // Indien de OracleDataReader uitgelezen kan worden betekent dit dat de opgegeven combinatie - emailadres en wachtwoord - bestaat.

                // Sluiten van OracleDataReader
                odr.Close();

                return true;
            }
            else
            {
                // Sluiten van OracleDataReader
                odr.Close();

                // Indien er geen account wordt gevonden bij de opgegeven gegevens dan is het inloggen mislukt.

                return false;
            }
        }
    }
}