using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;

namespace testProject
{
    [TestClass]
    public class AcceptanceTests
    {
        private static IApplicationBridge bridge=Driver.GetBridge();

        // SetUp information
        public String[] titels = { "sport", "nature" };
        public String[] subTitels = { "football", "basketball", "animals", "plants" };
        public String[] topic = { "man u", "juve" };
        public String[] body = { "best team in the world" };
        public String[] userNames = { "tomer.b", "tomer.s", "gal.b", "gal.p", "osher" };
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

        private int CreateForum()
        {
            int ans = bridge.CreateForum(topic[0],
                                        1, "",
                                        true, false, true,
                                        true, 3);
            return ans;
        }

        /*private int RegisterUser(int forumId)
        {
            int userId = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            return userId;
        }*/


        #endregion



             
        /// <CreateForumTest>
        /// should succeed and get normal forumId
        /// </CreateForumTest>
        [TestMethod]
        public void CreateForumTest()
        {
            int forumId = bridge.CreateForum(topic[0],
                                        1, "",
                                        true, false, true,
                                        true, 3);
            Assert.IsTrue(forumId > -1);
            forumsIds.Add(forumId);
        }


        /// <RegisterTestSuccess>
        /// should succeedd and get normal userId
        /// </RegisterTestSuccess>
        [TestMethod]
        public void RegisterTestSuccess()
        {
            int forumId = CreateForum();
            int userId = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            Assert.IsTrue(userId > -1);
        }


        
        /// <RegisterTestFailure>
        /// should fail due to user multiple username
        /// </RegisterTestFailure>
        [TestMethod]
        public void RegisterTestFailure()
        {
            int forumId = CreateForum();
            int userId = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            try
            {
                bridge.Register(userNames[0], passwords[0], emails[0], forumsIds[forumsIds.Count - 1]);
                Assert.Fail("Exception was expected but not thrown. User name with the same name trying to register to the same forum");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }





        /// <SetPolicyTest>
        /// should change policy and fail when trying to register user with invalid password
        /// </SetPolicyTest>
        [TestMethod]
        public void SetPolicyTest()
        {
            int forumId = CreateForum();
            bridge.SetPolicy(forumId,
                                    2, "a",
                                    false, true, false,
                                    false, 4);
            try
            {
                bridge.Register("SetPolicy", "ab", emails[0], forumId);
                Assert.Fail("Exception was expected but not thrown. Password is invalid by new policy");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }





        /// <LoginTest>
        /// should succeed and get the same userId
        /// </LoginTest>
        [TestMethod]
        public void LoginTest()
        {
            if (forumsIds.Count == 0)
                CreateForumTest();
            int userId = bridge.Register(userNames[0], passwords[0], emails[0], forumsIds[forumsIds.Count-1]);
            int loggedUser = bridge.Login(userNames[0], passwords[0], forumsIds[forumsIds.Count - 1]);
            Assert.Equals(userId, loggedUser);
        }


        /// <LogoutTest>
        /// test if connected user can logout 
        /// </LogoutTest>
        [TestMethod]
        public void LogoutTest()
        {
            int forumId = CreateForum();
            int userId = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            int loggedUser = bridge.Login(userNames[0], passwords[0], forumId);
            bool disconnected = bridge.Logout(loggedUser, forumId);
            Assert.IsTrue(disconnected);
        }



        /// <LogoutTestFail>
        /// test if exception thrown when disconnected user try to logout
        /// </LogoutTestFail>
        [TestMethod]
        public void LogoutTestFail()
        {
            int forumId = CreateForum();
            int userId = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            int loggedUser = bridge.Login(userNames[0], passwords[0], forumId);
            try
            {
                bridge.Logout(loggedUser, forumId);
                Assert.Fail("Exception was expected but not thrown. Disconnected user cannot logout");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        /// <CreateSubForumTest>
        /// test if subForum created
        /// </CreateSubForumTest>
        [TestMethod]
        public void CreateSubForumTest()
        {
            int forumId = CreateForum();
            int subForumId = bridge.CreateSubForum(forumId, topic[0]);
            Assert.IsTrue(subForumId > 0);
        }



        /// <CreateSubForumTestFail>
        /// should fail due to empty\null topic
        /// </CreateSubForumTestFail>
        [TestMethod]
        public void CreateSubForumTestFail()
        {
            int forumId = CreateForum();
            try
            {
                bridge.CreateSubForum(forumId, "");
                Assert.Fail("Exception was expected but not thrown. Cannot create subForum with empty string topic");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
             try
            {
                bridge.CreateSubForum(forumId, null);
                Assert.Fail("Exception was expected but not thrown. Cannot create subForum with null topic");
            }
             catch (Exception)
             {
                 Assert.IsTrue(true);
             }
        }

        /// <ViewTest>
        /// test if getting the correct sub-forum Ids & names
        /// </ViewTest>
        [TestMethod]
        public void ViewTest()
        {
            int forumId = CreateForum();
            int subForumId1 = bridge.CreateSubForum(forumId, topic[0]);
            int subForumId2 = bridge.CreateSubForum(forumId, topic[1]);
            List<string> subForumNames = new List<string>();
            List<int> subForumIds = new List<int>();
            bridge.View(forumId, out subForumNames, out subForumIds);
            for (int i = 0; i < subForumNames.Capacity; i++)
            {
                Assert.IsTrue(subForumNames[i].CompareTo(topic[i]) == 0);
            }
            Assert.AreEqual(subForumIds[0], subForumId1);
            Assert.AreEqual(subForumIds[1], subForumId2);
        }

       
    }
}
