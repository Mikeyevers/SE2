using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace IndividueleOpdracht
{
    public class Administration
    {
        public DatabaseConnection DatabaseConnection;
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
                           "AND wachtwoord = :PASSWORD " +
                           "AND LOWER(emailadres) NOT IN (SELECT emailadres FROM UserActivation)";

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

        public bool CheckEmailIsUnique(string email)
        {
            // Tellen hoeveel records er voor komen in de database met het opgegeven e-mailadres.
            // Indien het resultaat 0 is dan is het e-mailadres unique en is het valide. 

            string query = "SELECT EMAILADRES FROM ADVERTEERDER WHERE LOWER(EMAILADRES) = :EMAIL";
            OracleParameter emailParameter = new OracleParameter(":EMAIL", email);

            OracleDataReader odr = DatabaseConnection.ExecuteQuery(query, emailParameter);

            return odr.Read();
        }

        public bool CreateAccount(string name, string email, string password, bool emailMarktplaats, bool emailMarktplaatsPartners)
        {
            string emailMarktplaatsString;
            string emailMarktplaatsPartnersString;

            if (emailMarktplaats)
            {
                emailMarktplaatsString = "ja";
            }
            else
            {
                emailMarktplaatsString = "nee";
            }

            if (emailMarktplaatsPartners)
            {
                emailMarktplaatsPartnersString = "ja";
            }
            else
            {
                emailMarktplaatsPartnersString = "nee";
            }

            string query = "INSERT INTO adverteerder VALUES (adverteerderNummer_SEQ.nextval, :EMAIL, :PASSWORD, :NAME, default, default, '"
                            + emailMarktplaatsString + "', '" + emailMarktplaatsPartnersString + "', default, default)";

            OracleParameter emailParameter = new OracleParameter(":EMAIL", email);
            OracleParameter passwordParameter = new OracleParameter(":PASSWORD", password);
            OracleParameter nameParameter = new OracleParameter(":NAME", name);
            OracleParameter[] parameters = new OracleParameter[] { emailParameter, passwordParameter, nameParameter };

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

        public bool SendActivationMail(string email, string name)
        {
            try
            {
                string activationCode = Guid.NewGuid().ToString();

                string query = "INSERT INTO UserActivation VALUES (:EMAIL, '" + activationCode + "')";
                OracleParameter emailParameter = new OracleParameter(":EMAIL", email);
                int rowCount = DatabaseConnection.ExecuteNonQuery(query, emailParameter);

                if (rowCount == 1)
                {
                    // Instellen afzender en ontvanger van de te versturen e-mail.
                    MailAddress from = new MailAddress("marktplaats_software@hotmail.com", "Mike Evers - Developer");
                    MailAddress to = new MailAddress(email);
                    MailMessage mailMessage = new MailMessage(from, to);

                    //  Instellen onderwerp e-mail en vullen van de inhoud.
                    mailMessage.Subject = "Bevestigingsmail van Marktplaats voor uw nieuwe account!";
                    string mailBody = "Hoi " + name.Trim() + ",";
                    mailBody += "<br /><br />Welkom bij Marktplaats! Voor dat je gebruik kunt maken van je account, willen we graag dat je even je account bevestigd. ";
                    // activationLink maken met activationCode als parameter. 
                    string activationLink = string.Format("http://localhost:1348/ActivateAccount.ashx?ActivationCode={0}", activationCode);
                    mailBody += "<br /><a href = '" + activationLink + "'>Klik hier om je account te bevestigen.</a>";
                    mailBody += "<br /><br /> Bedankt!";

                    mailMessage.Body = mailBody;
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.live.com";
                    smtp.EnableSsl = true;
                    // I.v.m. de tijdsdruk heb ik de credentials niet voorzien van encryptie in de web.config, in praktijk gebeurt dit natuurlijk wel.
                    NetworkCredential networkCredential = new NetworkCredential("marktplaats_software@hotmail.com", "SoftwareOpdracht");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mailMessage);

                    return true;
                }
                else
                {
                    throw new System.ArgumentException("UserActivation kon niet worden toegevoegd aan de database.");
                }
            }
            catch
            {
                return false;
            }
        }

        public Adverteerder GetUserByEmail(string email)
        {

            string query = "SELECT adverteerdernummer, naam, postcode, telefoonnummer, boolemailmarktplaats, boolemailpartners " +
                           "FROM ADVERTEERDER " +
                           "WHERE LOWER(emailadres) = :EMAILADRES";

            OracleParameter emailadresParameter = new OracleParameter(":EMAILADRES", email);
            OracleDataReader odr = DatabaseConnection.ExecuteQuery(query, emailadresParameter);

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

                if (stringEmailMarktplaats.Trim() == "ja")
                {
                    boolEmailMarktplaats = true;
                }
                if (stringEmailMarktplaatsPartners.Trim() == "ja")
                {
                    boolEmailMarktplaatsPartners = true;
                }

                Adverteerder adverteerder = new Adverteerder(adverteerderNummer, email, naam, postcode, telefoonnummer, boolEmailMarktplaats, boolEmailMarktplaatsPartners);
                return adverteerder;
            }
            else
            {
                throw new System.ArgumentException("Er bestaat geen gebruiker met het opgegeven e-mailadres.");
            }
        }

        public List<string> GetMainGroups()
        {
            List<string> groups = new List<string>();

            string query = "SELECT naam FROM groep WHERE groepnummer IN (SELECT a.groepnummera FROM groep_groep a, groep_groep b WHERE a.groepnummera <> b.groepnummerb)";
            OracleDataReader odr = DatabaseConnection.ExecuteQuery(query);

            while (odr.Read())
            {
                groups.Add(odr.GetString(0));
            }

            return groups;
        }

        public List<string> GetSubGroups(string mainGroup)
        {
            List<string> subgroups = new List<string>();

            string query = "SELECT naam FROM groep WHERE groepnummer IN (SELECT groepnummerb FROM groep_groep WHERE groepnummera = (SELECT groepnummer FROM groep WHERE naam = :MAINGROUP))";
            OracleParameter mainGroupParameter = new OracleParameter(":MAINGROUP", mainGroup);
            OracleDataReader odr = DatabaseConnection.ExecuteQuery(query, mainGroupParameter);

            while (odr.Read())
            {
                subgroups.Add(odr.GetString(0));
            }

            return subgroups;
        }

        public List<string> GetRubrics(string subGroup)
        {
            List<string> rubrics = new List<string>();

            string query = "SELECT naam FROM rubriek WHERE rubrieknummer IN (SELECT rubrieknummer FROM rubriek_groep WHERE groepnummer = (SELECT groepnummer FROM groep WHERE naam = :SUBGROUP))";
            OracleParameter subGroupParameter = new OracleParameter(":SUBGROUP", subGroup);
            OracleDataReader odr = DatabaseConnection.ExecuteQuery(query, subGroupParameter);

            while (odr.Read())
            {
                rubrics.Add(odr.GetString(0));
            }

            return rubrics;
        }

        public int GetRubricNumber(string rubricName)
        {
            string query = "SELECT rubrieknummer FROM rubriek WHERE naam = :RUBRICNAME";
            OracleParameter rubricNameParameter = new OracleParameter(":RUBRICNAME", rubricName);

            OracleDataReader odr = DatabaseConnection.ExecuteQuery(query, rubricNameParameter);


            if (odr.Read())
            {
                int number = odr.GetInt32(0);

                return number;
            }
            else return -9999;
        }

        public List<Product> GetAllProductAdvertisements()
        {
            string query = "SELECT a.rubriekNummer, a.adverteerderNummer, a.advertentieTekst, a.websiteUrl, a.naamBijAdvertentie, a.telefoonBijAdvertentie, a.postcode, a.plaatsdatum, " +
                            "       p.titel, p.prijsType, p.vraagPrijsOptie, p.biedenVanafBedrag, p.prijsBedrag, p.boolPaypal, p.land, p.woonplaats " +
                            "FROM advertentie a, product p " +
                            "WHERE a.advertentieNummer = p.advertentieNummer " +
                            "ORDER BY plaatsDatum DESC";

            OracleDataReader odr = DatabaseConnection.ExecuteQuery(query);
            List<Product> producten = new List<Product>();

            while(odr.Read())
            {
                // Eerst uitlezen en controleren van de data uit de odr.
                int rubriekNummer = odr.GetInt32(0);
                int adverteerderNummer = odr.GetInt32(1);
                string advertentieTekst = odr.GetString(2);
                string naam = odr.GetString(4);
                DateTime plaatsDatum = odr.GetDateTime(7);
                string titel = odr.GetString(8);
                string prijsType = odr.GetString(9);

                string websiteUrl;
                if(odr.IsDBNull(3))
                {
                    websiteUrl = null;
                }
                else
                {
                    websiteUrl = odr.GetString(3);
                }
  
                int telefoon;
                if (odr.IsDBNull(5))
                {
                    // Geef een onmogelijke waarden.
                    telefoon = -999;
                }
                else
                {
                    telefoon = odr.GetInt32(5);
                }

                string postcode;
                if (odr.IsDBNull(6))
                {
                    postcode = null;
                }
                else
                {
                    postcode = odr.GetString(6);
                }
                
                string vraagPrijsOptie;
                if (odr.IsDBNull(10))
                {
                    vraagPrijsOptie = null;
                }
                else
                {
                    vraagPrijsOptie = odr.GetString(10);
                }

                decimal biedenVanafBedrag;
                if (odr.IsDBNull(11))
                {
                    // Geef een onmogelijke waarden.
                    biedenVanafBedrag = -999M;
                }
                else
                {
                    biedenVanafBedrag = odr.GetDecimal(11);
                }

                decimal prijsBedrag;
                if (odr.IsDBNull(12))
                {
                    // Geef een onmogelijke waarden.
                    prijsBedrag = -999M;
                }
                else
                {
                    prijsBedrag = odr.GetDecimal(12);
                }
                string payPalString = odr.GetString(13);
                bool payPal = false;
                if (payPalString.Trim() == "ja")
                {
                    payPal = true;
                }     
         
                string land;
                if (odr.IsDBNull(14))
                {
                    land = null;
                }
                else
                {
                    land = odr.GetString(14);
                }
                string woonplaats;
                if(odr.IsDBNull(15))
                {
                    woonplaats = null;
                }
                else
                {
                    woonplaats = odr.GetString(15);
                }


                if (vraagPrijsOptie == null && biedenVanafBedrag == -999M && prijsBedrag == -999M)
                {
                    Product product = new Product(titel, prijsType, payPal, rubriekNummer, advertentieTekst, websiteUrl, naam, telefoon, postcode, plaatsDatum, adverteerderNummer);
                    producten.Add(product);
                }
                else
                {
                    Product product = new Product(titel, prijsType, vraagPrijsOptie, biedenVanafBedrag, prijsBedrag, payPal, rubriekNummer, advertentieTekst, websiteUrl, naam, telefoon, postcode, plaatsDatum, adverteerderNummer);
                    producten.Add(product);
                }
            }

            return producten;                   
        }
    }
}