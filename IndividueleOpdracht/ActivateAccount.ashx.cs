using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace IndividueleOpdracht
{
    /// <summary>
    /// Summary description for ActivateAccount1
    /// </summary>
    public class ActivateAccount1 : IHttpHandler
    {
        private DatabaseConnection connection;
        public void ProcessRequest(HttpContext context)
        {
            connection = new DatabaseConnection();

            ActivationResult activationResult = ActivationResult.niet_gevonden;

            context.Response.ContentType = "application/json";

            string ActivationCode = context.Request.QueryString["ActivationCode"];

            if (ActivationCode != null)
            {

                string query = "DELETE FROM UserActivation WHERE ActivationCode = :ACTIVATIONCODE";

                OracleParameter activationCodeParameter = new OracleParameter(":activationCode", ActivationCode);
                int rowCount = connection.ExecuteNonQuery(query, activationCodeParameter);

                if (rowCount == 1)
                {
                    activationResult = ActivationResult.geactiveerd;
                }
            }

            
            // wat ga je op het scherm laten zien mike bij foutmeldingen?

            if (activationResult != ActivationResult.niet_gevonden)
            {
                context.Response.Redirect("Login.aspx");
            }
            else
            {
                context.Response.Write("ERROR 404! Deze pagina is niet (meer) beschikbaar.");
            }
        }


     
        public enum ActivationResult
        {
            geactiveerd,
            niet_gevonden
        }

            
        bool IHttpHandler.IsReusable
        {
            get { return false; ; }
        }

      
    }
}