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
        Adverteerder LoggedInUser;
        public Administration()
        {
            DatabaseConnection = new DatabaseConnection();
        }

        public bool Inloggen(string emailadres, string wachtwoord) 
        {
            // Veilig ophalen van alle gegevens die horen bij het opgegeven emailadres en wachtwoord.
            string query = "SELECT adverteerdernummer, naam, postcode, telefoonnummer, boolemailmarktplaats, boolemailpartners " +
                           "FROM ADVERTEERDER " +
                           "WHERE LOWER(emailadres) = :EMAILADRES " +
                           "AND wachtwoord = :WACHTWOORD";
            OracleParameter emailadresParameter = new OracleParameter(":EMAILADRES", emailadres.ToLower());
            OracleParameter wachtwoordParameter = new OracleParameter(":WACHTWOORD", wachtwoord);
            OracleParameter[] parameters = new OracleParameter[] { emailadresParameter, wachtwoordParameter };

            OracleDataReader odr = DatabaseConnection.ExecuteQuery(query, parameters);

            // Aanmaken van een object van type Adverteerder. 
            if (odr.Read())
            {
                int adverteerderNummer = odr.GetInt32(0);
                string naam = odr.GetString(1);
                string postcode = odr.GetString(2);
                string telefoonnummer = odr.GetString(3);
                string stringEmailMarktplaats = odr.GetString(4);
                string stringEmailMarktplaatsPartners = odr.GetString(5);
                bool boolEmailMarktplaats = false;
                bool boolEmailMarktplaatsPartners = false;
                
                if(stringEmailMarktplaats == "ja")
                {
                    boolEmailMarktplaats = true; 
                }
                if(stringEmailMarktplaatsPartners == "ja")
                {
                    boolEmailMarktplaatsPartners = true;
                }

                LoggedInUser = new Adverteerder(adverteerderNummer, emailadres.ToLower(), naam, postcode, telefoonnummer, boolEmailMarktplaats, boolEmailMarktplaatsPartners);             
                 

                // Sluiten van OracleDataReader
                odr.Close();
                return true;
            }
            else
            {
                // Sluiten van OracleDataReader
                odr.Close();
                return false;    
            }
        }
    }
}