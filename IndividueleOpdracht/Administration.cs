using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Web;
using System.Configuration;
using System.Web.Configuration;

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
                // Sluiten van OracleDataReader
                odr.Close();

                // Indien de OracleDataReader uitgelezen kan worden betekent dit dat de opgegeven combinatie - emailadres en wachtwoord - bestaat.
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

        public bool checkEmailIsUnique(string emailadres)
        {
            // Tellen hoeveel records er voor komen in de database met het opgegeven e-mailadres.
            // Indien het resultaat 0 is dan is het e-mailadres unique en is het valide. 
            string correctEmailadres = emailadres.Replace(" ", "");
            correctEmailadres = correctEmailadres.ToLower(); 

            string query = "SELECT COUNT(*) " +
                           "FROM ADVERTEERDER " +
                           "WHERE LOWER(emailadres) = :EMAILADRES";

            OracleParameter emailadresParameter = new OracleParameter(":EMAILADRES", correctEmailadres);
            int recordCount = DatabaseConnection.ExecuteNonQuery(query, emailadresParameter);

            if (recordCount == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void NavigateAfterLogin(HttpResponse response)
        {
            // Open van het Web.config bestand.
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("\\Web.config");

            // Pagina (value) ophalen die door de klant is gekoppeld aan de "pageAfterLogin" (key).
            string pageAfterLogin = configuration.AppSettings.Settings["pageAfterLogin"].Value;

            // Vervolgens navigeren naar de opgehaalde pagina.
            response.Redirect(pageAfterLogin);
        }
    }
}