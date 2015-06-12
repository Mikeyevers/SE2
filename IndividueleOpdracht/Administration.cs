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

        public bool Inloggen(string emailadres, string password)
        {
            // Ophalen van adverteerderNummer bij opgegeven emailadres en wachtwoord.
            string query = "SELECT adverteerdernummer " +
                           "FROM ADVERTEERDER " +
                           "WHERE LOWER(emailadres) = :EMAILADRES " +
                           "AND wachtwoord = :PASSWORD";

            OracleParameter emailadresParameter = new OracleParameter(":EMAILADRES", emailadres.ToLower());
            OracleParameter passwordParameter = new OracleParameter(":PASSWORD", password);
            OracleParameter[] parameters = new OracleParameter[] { emailadresParameter, passwordParameter };

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

        public bool checkEmailIsUnique(string email)
        {
            // Tellen hoeveel records er voor komen in de database met het opgegeven e-mailadres.
            // Indien het resultaat 0 is dan is het e-mailadres unique en is het valide. 
            string correctEmail = email.Replace(" ", "");
            correctEmail = correctEmail.ToLower(); 

            string query = "SELECT EMAILADRES FROM ADVERTEERDER WHERE LOWER(EMAILADRES) = :EMAIL";
            OracleParameter emailParameter = new OracleParameter(":EMAIL", correctEmail);

            OracleDataReader odr = DatabaseConnection.ExecuteQuery(query, emailParameter);

            return odr.Read();
        }

        public bool createAccount(string name, string email, string password, bool emailMarktplaats, bool emailMarktplaatsPartners)
        {
            string emailMarktplaatsString;
            string emailMarktplaatsPartnersString;

            if(emailMarktplaats)
            {
                emailMarktplaatsString = "ja";
            }
            else
            {
                emailMarktplaatsString = "nee";
            }

            if(emailMarktplaatsPartners)
            {
                emailMarktplaatsPartnersString = "ja";
            }
            else
            {
                emailMarktplaatsPartnersString = "nee";
            }

            string query = "INSERT INTO adverteerder VALUES (adverteerderNummer_SEQ.nextval, :EMAIL, :PASSWORD, :NAME, default, default, '" 
                            + emailMarktplaatsString + "', '" + emailMarktplaatsPartnersString + "', default, default, default)";

            OracleParameter emailParameter = new OracleParameter(":EMAIL", email.ToLower());
            OracleParameter passwordParameter = new OracleParameter(":PASSWORD", password);
            OracleParameter nameParameter = new OracleParameter(":NAME", name);
            OracleParameter[] parameters = new OracleParameter[] { emailParameter, passwordParameter, nameParameter};

            int countRows = DatabaseConnection.ExecuteNonQuery(query, parameters);

            if (countRows == 1)
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