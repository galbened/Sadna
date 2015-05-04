using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;

namespace testProject
{
    [TestClass]
    public class ProjectTest
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


        /*
      * Registration Test - should succeedd and get normal userId
      */
        [TestMethod]
        public void RegisterTestSuccess()
        {
            if (forumsIds.Count == 0)
                CreateForumTest();
            int userId = bridge.Register(userNames[0], passwords[0], emails[0],forumsIds[forumsIds.Count-1]);
            Assert.IsTrue(userId > -1);
        }

        /*
         * Registration Test - should fail due to user multiple username
         */
        [TestMethod]
        public void RegisterTestFailure()
        {
            if (forumsIds.Count == 0)
                CreateForumTest();
            int userId = bridge.Register(userNames[0], passwords[0], emails[0], forumsIds[forumsIds.Count - 1]);
            try
            {
                bridge.Register(userNames[0], passwords[0], emails[0], forumsIds[forumsIds.Count - 1]);
                Assert.Fail("Exception was expected but not thrown");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }



        /*
        * CreateForum Test - should succeed and get normal forumId
        */
        [TestMethod]
        public void CreateForumTest()
        {
            //int forumAdmin = bridge.Register(userNames[0], passwords[0], emails[0]);
           // Assert.IsTrue(forumAdmin > -1);
            int forumId = bridge.CreateForum(/*forumAdmin,*/ topic[0],
                                        1, "",
                                        true, false, true,
                                        true, 3);           
            Assert.IsTrue(forumId > -1);
            forumsIds.Add(forumId);
        }


        /*
        * Login Test - should succeed and get the same userId
        */
        [TestMethod]
        public void LoginTest()
        {
            if (forumsIds.Count == 0)
                CreateForumTest();
            int userId = bridge.Register(userNames[0], passwords[0], emails[0], forumsIds[forumsIds.Count-1]);
            int loggedUser = bridge.Login(userNames[0], passwords[0], forumsIds[forumsIds.Count - 1]);
            Assert.Equals(userId, loggedUser);
        }

       
    }
}
