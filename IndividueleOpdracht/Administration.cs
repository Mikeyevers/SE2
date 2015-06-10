using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            // Veilig ophalen van alle gegevens die horen bij het opgegeven emailadres en wachtwoord.
            string query = "SELECT adverteerdernummer, naam, postcode, telefoonnummer, boolemailmarktplaats, boolemailpartners" +
                           "FROM ADVERTEERDER" +
                           "WHERE emailadres = :EMAILADRES" +
                           "AND wachtwoord = :WACHTWOORD";
            OracleParameter emailadresParameter = new OracleParameter(":EMAILADRES", emailadres);
            OracleParameter wachtwoordParameter = new OracleParameter(":WACHTWOORD", wachtwoord);
            OracleParameter[] parameters = new OracleParameter[] { emailadresParameter, wachtwoordParameter };

            OracleDataReader odr = DatabaseConnection.ExecuteQuery(query, parameters);

            // Aanmaken van een object van type Adverteerder. 
            if (odr.Read())
            {
                Adverteerder adverteerder = new Adverteerder(odr.GetInt32(0), emailadres, odr.GetString(1), odr.GetString(2), odr.GetString(3), odr.GetBoolean(4), odr.GetBoolean(5));
                return true;
            }
            else
            {
                return false;    
            }

            // Sluiten van OracleDataReader
            odr.Close();


        }
    }
}