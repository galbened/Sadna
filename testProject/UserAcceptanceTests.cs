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
        public String[] userNamesList = { "kktuka.kaks","tomer.belzer", "tomer.s", "gal.b", "gal.p", "osher" };
        public String[] emails = { "tomer.b@gmail.com", "tomer.s@gmail.com", "gal.b@gmail.com", "gal.p@gmail.com", "osher@gmail.com" };
        public String[] passwords = { "Ab3", "123456", "abcdef" };
        public String[] superAdmin = { "admin", "admin" };


        private static List<int> usersIds;
        private static List<int> forumsIds;
        private static List<int> subForumsIds;
        private static List<int> messagesIds;

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
           
            bridge = Driver.GetBridge();
            usersIds = new List<int>();
            forumsIds = new List<int>();
            subForumsIds = new List<int>();
            messagesIds = new List<int>();
            forumsIds.Add(CreateForum("First Forum"));
        }

        [ClassCleanup]
        public static void TearDown()
        {

            bridge = null;
            usersIds = null;
            forumsIds = null;
            subForumsIds = null;
            messagesIds = null;
        }

        #region HelpFunctions

        //These function should be used only if their relevant use-case test succeeded

        protected static int CreateForum(string forumTopic)
        {
            int ans = bridge.CreateForum(forumTopic,
                                        1, "",
                                        false, false, false,
                                        false, 3);

            return ans;
        }

        #endregion

        /// <TestMethod1>
        /// TestId: 10.
        /// should 
        /// </TestMethod1>
        [TestMethod]
        public void TestMethod1()
        {
        }

        /// <NewUserRegisterTest>
        /// TestId: 10.1
        /// should succeedd and get normal userId
        /// </NewUserRegisterTest>
        [TestMethod]
        public void NewUserRegisterTest()
        {
            int forumId = forumsIds[0];
            int userId = bridge.Register(userNamesList[0], passwords[1], emails[0], forumId);
            Boolean ans = bridge.isUserRegistered(userId);
            Assert.IsTrue(ans);
            //usersIds.Add(userId);

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
            Assert.IsTrue(bridge.isRegisteredUser(forumId, userId));
            try
            {
                userId = bridge.Register(userNamesList[0], passwords[0], emails[0], forumId);
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
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
            int firstUserId = usersIds[0];
            //usersIds.Add(firstUserId);
            int secondUserId = bridge.Register(userNamesList[1], passwords[1], emails[1], forumId);
            usersIds.Add(secondUserId);
            Assert.AreNotEqual(firstUserId, secondUserId);
        }
        /// <LogintoRegisteredForum>
        /// TestId: 10.4
        /// should succeed when loging to pre-registered forum 
        /// </LogintoRegisteredForum>
        [TestMethod]
        public void LoginRegisteredToForum()
        {
            int forumId = forumsIds[0];
            int userId = usersIds[0];
            int loggedUser = bridge.Login(userNamesList[0], passwords[0], forumId);
            Assert.Equals(userId, loggedUser);
        }
        /// <LogintoRegisteredForum>
        /// TestId: 10.5
        /// should fail since unregistered
        /// user can't login
        /// </LogintoRegisteredForum>
        [TestMethod]
        public void LoginUnregisteredToForum()
        {
            int forumId = CreateForum("Rap battle hype men");
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

        /// <TestMethod1>
        /// TestId: 10.
        /// should 
        /// </TestMethod1>
        [TestMethod]
        public void LogoutTest()
        {
            int forumId = forumsIds[0];
            int userId = usersIds[0];
            try
            {
                bridge.Logout(userId, forumId);
                Assert.IsFalse(bridge.isLoggedin(userId));
                // restoring login information
                int userIdAfterLogin = bridge.Login(userNamesList[0], passwords[0], forumId);
            }
            catch
            {
                Assert.Fail();
            }
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
            int userId = usersIds[0];
            string temporaryUsername="EliadMalki";
            try
            {
                int responseId = bridge.ChangeUsername(userId, temporaryUsername, passwords[0]);
                Assert.Equals(responseId, userId);
                string newUsername = bridge.GetUsername(responseId);
                Assert.Equals(newUsername, temporaryUsername);

            }
            catch
            {
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
                Assert.Fail("Old username is not fully deleted");
            }
        }

        /// <ChangeUsernameIncorrectDetailsTest>
        /// TestId: 10.9.1
        /// should fail due to incorrect login parameters
        /// </ChangeUsernameIncorrectDetailsTest>
        [TestMethod]
        public void ChangeUsernameIncorrectDetailsTest()
        {
            int forumId = forumsIds[0];
            int userId = usersIds[0];
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
            int userId = usersIds[0];
            try
            {
                int newUser = bridge.Register(userNamesList[0], passwords[0], emails[1], forumId);
                Assert.Fail("Can't set username to one that has already in use");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
        /// <ChangeUsernameTest>
        /// TestId: 10.10
        /// should succeed with correct authentication 
        /// and user name
        /// </ChangeUsernameTest>
        [TestMethod]
        public void ChangeUsernameTestUserAcceptance()
        {
            int forumId = forumsIds[0];
            int userId = usersIds[0];
            int loggedUser = bridge.Login(userNamesList[0], passwords[0], forumId);
            try
            {
                int ResponseId = bridge.ChangePassword(userId, passwords[0], passwords[1]);
                Assert.Equals(ResponseId, userId);
                // restoring password
                ResponseId = bridge.ChangePassword(userId, passwords[1], passwords[0]);
            }
            catch
            {
                Assert.Fail();
            }
        }

        /// <ChangeUsernameIncorrectDetailsTest>
        /// TestId: 10.11
        /// should fail due to incorrect params
        /// </ChangeUsernameIncorrectDetailsTest>
        [TestMethod]
        public void ChangeUsernameIncorrectDetailsTestUserAcceptance()
        {
            int forumId = forumsIds[0];
            int userId = usersIds[0];
            int loggedUser = bridge.Login(userNamesList[0], passwords[0], forumId);
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
        }

    }
}
