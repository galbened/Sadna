using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;

namespace testProject
{
    [TestClass]
    public class UserAcceptanceTests
    {
        private static IApplicationBridge bridge = Driver.GetBridge();


        //pre-made values to ease the testing
        public String[] titles = { "sport", "nature" };
        public String[] subTitels = { "football", "basketball", "animals", "plants" };
        public String[] topic = { "man u", "juve" };
        public String[] body = { "best team in the world" };
        public String[] userNamesList = { "kktddasda.kaks","tomer.belzer", "tomer.s", "gal.b", "gal.p", "osher" };
        public String[] emails = { "tomer.b@gmail.com", "tomer.s@gmail.com", "gal.b@gmail.com", "gal.p@gmail.com", "osher@gmail.com" };
        public String[] passwords = { "A423eda@b3", "123456", "abcdef" };
        public String[] superAdmin = { "admin", "admin" };


        private static List<int> forumsIds;
        private static List<int> subForumsIds;
        private static List<int> messagesIds;

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
           
            bridge = Driver.GetBridge();
            forumsIds = new List<int>();
            subForumsIds = new List<int>();
            messagesIds = new List<int>();
            forumsIds.Add(CreateForum("First Forum"));
        }

        [ClassCleanup]
        public static void TearDown()
        {
            bridge = null;
            forumsIds = null;
            subForumsIds = null;
            messagesIds = null;
        }

        #region HelpFunctions

        //These function should be used only if their relevant use-case test succeeded

        protected static int CreateForum(string forumTopic)
        {
            int ans = bridge.CreateForum(1, forumTopic,
                                        1, "",
                                        false, false, false,
                                        false, 3);

            return ans;
        }

        #endregion

  

        /// <NewUserRegisterTest>
        /// TestId: 10.1
        /// should succeedd and get normal userId
        /// </NewUserRegisterTest>
        [TestMethod]
        public void NewUserRegisterTest()
        {
            int forumId = forumsIds[0];
            int userId = bridge.Register("kartoshke", passwords[1], emails[0], forumId);
            Boolean ans = bridge.isUserRegistered(userId);
            Assert.IsTrue(ans);
            bridge.Deactivate(userId);
        }

        /// <DoubleRegistrationFail>
        /// TestId: 10.2
        /// should throw exception once user
        /// try to register to the same forum more than once 
        /// </DoubleRegistrationFail>
        [TestMethod]
        public void DoubleRegistrationFail()
        {
            int forumId = forumsIds[0];
            int userId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId); //usersIds[0];
            try
            {
                userId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId);
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
            bridge.Deactivate(userId);
        }


        /// <RegistrationReturnsDiffIDTest>
        /// TestId: 10.3
        /// two deifferent users should get 
        /// different userId
        /// </RegistrationReturnsDiffIDTest>
        [TestMethod]
        public void RegistrationReturnsDiffIDTest()
        {
            int forumId = forumsIds[0];
            int firstUserId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId);
            //usersIds.Add(firstUserId);
            int secondUserId = bridge.Register(userNamesList[1], passwords[1], emails[1], forumId);
            Assert.AreNotEqual(firstUserId, secondUserId);
            bridge.Deactivate(firstUserId);
            bridge.Deactivate(secondUserId);
        }
        /// <LogintoRegisteredForum>
        /// TestId: 10.4
        /// should succeed when loging to pre-registered forum 
        /// </LogintoRegisteredForum>
        [TestMethod]
        public void LoginRegisteredToForum()
        {
            int forumId = forumsIds[0];
            int userId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId);
            //int loggedUser = bridge.Login(userNamesList[0], passwords[0], forumId);
            //Assert.IsTrue(userId==loggedUser);
            Assert.IsTrue(bridge.isLoggedin(userId));
            bridge.Deactivate(userId);
        }

        /// <LogintoRegisteredForum>
        /// TestId: 10.5
        /// should fail since unregistered
        /// user can't login
        /// </LogintoRegisteredForum>
        [TestMethod]
        public void LoginUnregisteredToForum()
        {
            int forumId = forumsIds[0];
            try
            {
                // should be redefined 
                int loggedUser = bridge.Login(userNamesList[0], passwords[0], forumId);
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            } 
        }

        /// <LogoutTest>
        /// TestId: 10.6
        /// should 
        /// </LogoutTest>
        [TestMethod]
        public void LogoutTest()
        {
            int forumId = forumsIds[0];
            int userId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId); 
            try
            {
                bridge.Logout(userId, forumId);
                Assert.IsFalse(bridge.isLoggedin(userId));
            }
            catch
            {
                Assert.Fail();
            }
            bridge.Deactivate(userId);
        }
        /// <LogoutLoginTest>
        /// TestId: 10.7
        /// should 
        /// </LogoutLoginTest>
        [TestMethod]
        public void LogoutLoginTest()
        {
            int forumId = forumsIds[0];
            int userId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId);
            try
            {
                bridge.Logout(userId, forumId);
                int loginResponse = bridge.Login(userNamesList[0], passwords[0], forumId);
                Assert.AreEqual(userId, loginResponse);
                Assert.IsTrue(bridge.isLoggedin(userId));
            }
            catch
            {
                bridge.Deactivate(userId);
                Assert.Fail();
            }
            bridge.Deactivate(userId);
        }

        /// <ChangeUsernameTest>
        /// TestId: 10.8
        /// should succeed as long as the password
        /// is correct according to the userId
        /// </ChangeUsernameTest>
        [TestMethod]
        public void ChangeUsernameTest()
        {
            int forumId = forumsIds[0];
            int userId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId);
            string temporaryUsername="EliadMalki";
            try
            {
                int responseId = bridge.ChangeUsername(userId, temporaryUsername, passwords[0]);
                string newUsername = bridge.GetUsername(userId);
                Assert.AreEqual(newUsername, temporaryUsername);

            }
            catch
            {
                bridge.Deactivate(userId);
                Assert.Fail();
            }

            // restoring to previous username, checks if old user name is fully deleted from DB

            try
            {
                int responseId = bridge.ChangeUsername(userId, userNamesList[0], passwords[0]);
                Assert.IsTrue(true);
            }
            catch
            {
                bridge.Deactivate(userId);
                Assert.Fail("Old username is not fully deleted");
            }
            bridge.Deactivate(userId);
        }

        /// <ChangeUsernameIncorrectDetailsTest>
        /// TestId: 10.9.1
        /// should fail due to incorrect login parameters
        /// </ChangeUsernameIncorrectDetailsTest>
        [TestMethod]
        public void ChangeUsernameIncorrectDetailsTest()
        {
            int forumId = forumsIds[0];
            int userId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId);
            string temporaryUsername = "BigTool42";
            //trying to set username with wrong password
            try
            {
                int responseId = bridge.ChangeUsername(userId, temporaryUsername, passwords[1]);
                Assert.Fail("Can't change username with incorrect password");
                // restoring username in case it succeeded
                responseId = bridge.ChangeUsername(userId, userNamesList[0], passwords[0]);
            }
            catch
            {
                
                Assert.IsTrue(true);
            }
            
            // trying to set a null username
            try
            {
                int responseId = bridge.ChangeUsername(userId, null, passwords[0]);
                Assert.Fail("Can't change username to null");
                // restoring username in case it succeeded
                responseId = bridge.ChangeUsername(userId, userNamesList[0], passwords[0]);
            }
            catch
            {

                Assert.IsTrue(true);
            }
            bridge.Deactivate(userId);
        }

        /// <ChangeUsernameToExisting>
        /// TestId: 10.9.2
        /// should fail, can't set a new username 
        /// to one that has already been registered
        /// </ChangeUsernameToExisting>
        [TestMethod]
        public void ChangeUsernameToExisting()
        {
            int forumId = forumsIds[0];
            int userId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId);
            try
            {
                int newUser = bridge.Register(userNamesList[0], passwords[0], emails[1], forumId);
                Assert.Fail("Can't set username to one that has already in use");
            }
            catch
            {
                Assert.IsTrue(true);
            }
            bridge.Deactivate(userId);
        }
        /// <ChangeUsernameTest>
        /// TestId: 10.10
        /// should succeed with correct authentication 
        /// and user name
        /// </ChangeUsernameTest>
        [TestMethod]
        public void ChangePasswordTest()
        {
            int forumId = forumsIds[0];
            int userId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId);
            try
            {
                int ResponseId = bridge.ChangePassword(userId, passwords[0], passwords[1]);
                Assert.IsTrue(bridge.IsPasswordCorrect(userId,passwords[1]));
                Assert.IsFalse(bridge.IsPasswordCorrect(userId, passwords[0]));
            }
            catch
            {
                Assert.Fail();
            }
            bridge.Deactivate(userId);
        }

        /// <ChangeUsernameIncorrectDetailsTest>
        /// TestId: 10.11
        /// should fail due to incorrect params
        /// </ChangeUsernameIncorrectDetailsTest>
        [TestMethod]
        public void ChangePasswordWrongCredentialsTest()
        {
            int forumId = forumsIds[0];
            int userId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId);
            try //wrong password
            {
                int ResponseId = bridge.ChangePassword(userId, passwords[1], passwords[1]);
                Assert.Fail("Old password is not correct");
            }
            catch
            {
                Assert.IsTrue(true);
            }


            try //illegal new password
            {
                int ResponseId = bridge.ChangePassword(userId, passwords[0], null);
                Assert.Fail("New password cannot be empy");
            }
            catch
            {
                Assert.IsTrue(true);
            }
            bridge.Deactivate(userId);
        }

    }
}
