using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
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

        public bool checkEmailIsUnique(string email)
        {
            // Tellen hoeveel records er voor komen in de database met het opgegeven e-mailadres.
            // Indien het resultaat 0 is dan is het e-mailadres unique en is het valide. 

            string query = "SELECT EMAILADRES FROM ADVERTEERDER WHERE LOWER(EMAILADRES) = :EMAIL";
            OracleParameter emailParameter = new OracleParameter(":EMAIL", email);

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
                            + emailMarktplaatsString + "', '" + emailMarktplaatsPartnersString + "', default, default)";

            OracleParameter emailParameter = new OracleParameter(":EMAIL", email);
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
                    NetworkCredential NetworkCredential = new NetworkCredential("marktplaats_software@hotmail.com", "SoftwareOpdracht");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCredential;
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
    }
}