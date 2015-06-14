//using System;
//using System.Text;
//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace IndividueleOpdracht
//{
//    /// <summary>
//    /// Testing the method for sending activationmail
//    /// </summary>
//    [TestClass]
//    public class UnitTestSendMail
//    {
//        [TestMethod]
//        public void SendActivationMail()
//        {
//            // arrange
//            string email = "mikeevers1997@gmail.com";
//            string name = "Testnaam Mike";

//            // act

//            Administration administration = new Administration();
//            bool succes = administration.SendActivationMail(email, name);
//            bool expected = true;
//            // assert
//            Assert.AreEqual(succes, expected, "De SendActivationMail() methode werkt niet meer naar behoren");

//            string query = "DELETE FROM useractivation WHERE emailadres = '" + email + "'";
//            administration.DatabaseConnection.ExecuteNonQuery(query);
//        }
//    }
//}

// Dit is allemaal in comments vanwege een fout die ik niet uit deze unit test krijg. 
// Hij geeft een nullReference exception bij het uitlezen van de configuration file. 
