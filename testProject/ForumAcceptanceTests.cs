using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;

namespace testProject
{
    [TestClass]
    public class ForumAcceptanceTests
    {
        private static IApplicationBridge bridge = Driver.GetBridge();


        //pre-made values to ease the testing
        public String[] titles = { "sport", "nature" };
        public String[] subTitels = { "football", "basketball", "animals", "plants" };
        public String[] topic = { "man u", "juve" };
        public String[] body = { "best team in the world" };
        public String[] userNamesList = { "tomer.b", "tomer.s", "gal.b", "gal.p", "osher" };
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

        protected static int CreateForum(string forumTopic)
        {
            int ans = bridge.CreateForum(1, "galpopopo",
                                        1, "",
                                        false, false, false,
                                        false, 1);

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
        /// <creatingSubForum>
        /// TestId: 10.0.1
        /// should add succefully the new forum
        /// </creatingSubForum>
        [TestMethod]
        public void creatingForumTest()
        {
            List<int> allForumsPreCreation = bridge.GetForumIds();
            int newForumId = CreateForum("First Forum");
            forumsIds.Add(newForumId);
            List<int> allForumsPostCreation = bridge.GetForumIds();
            Assert.IsTrue(allForumsPostCreation.Contains(newForumId) && !allForumsPreCreation.Contains(newForumId));

        }
        /// <TestMethod1>
        /// TestId: 10.
        /// should add succefully the new sub forum
        /// </TestMethod1>
        [TestMethod]
        public void creatingSubForumTest()
        {
            int currentForum = forumsIds[0];
            int subForumId = bridge.CreateSubForum(1, currentForum, "first sub-forum");
            subForumsIds.Add(subForumId);
            Assert.IsTrue(subForumId > 0);
            Assert.IsTrue((bridge.GetSubForumsIds(currentForum)).Contains(subForumId));  
        }
        /// <creatingForumReturnsDiffIDTest>
        /// TestId: 10.1
        /// should create a new forum with a different id 
        /// than the first forum
        /// </creatingForumReturnsDiffIDTest>
        [TestMethod]
        public void creatingForumReturnsDiffIDTest()
        {
            int newForumId = CreateForum("Second Forum");
            Assert.AreNotEqual(forumsIds[0], newForumId);
        }
        /// <creatingSubForumReturnsDiffIDTest>
        /// TestId: 10.2
        /// should create a new sub-forum with a different id 
        /// than the first sub forums 
        /// </creatingSubForumReturnsDiffIDTest>
        [TestMethod]
        public void creatingSubForumReturnsDiffIDTest()
        {
            int mainForumId=forumsIds[0];
            int firstSubForum = subForumsIds[0];
            int secondSubForum = bridge.CreateSubForum(1, mainForumId, "second sub forum");
            Assert.AreNotEqual(firstSubForum, secondSubForum);
        }
         /// <CreateSubForumTestFail>
        /// TestId: 10.8
        /// should fail due to empty\null topic
        /// </CreateSubForumTestFail>
        [TestMethod]
        public void CreateSubForumTestFail()
        {
            try
            {
                int temporaryForum = CreateForum(null);
                Assert.Fail("Can't create forum with a null topic");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        /// <CreateSubForumTestFail>
        /// TestId: 10.9
        /// should fail due to empty\null topic
        /// or illegal forumId
        /// </CreateSubForumTestFail>
        [TestMethod]
        public void CreateSubForumTestFailForumAcceptance()
        {
            int forumId = forumsIds[0];
            try
            {
                bridge.CreateSubForum(1, forumId, "");
                Assert.Fail("Exception was expected but not thrown. Cannot create subForum with empty string topic");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
            try
            {
                bridge.CreateSubForum(1, forumId, null);
                Assert.Fail("Exception was expected but not thrown. Cannot create subForum with null topic");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
            try
            {
                bridge.CreateSubForum(1, -1, topic[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot create subForum with illegal forumId");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

    }
}
