﻿using System;
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
        public String[] titles = { "sport", "nature" };
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

        #endregion

        /// <CreateForumTest>
        /// TestId: 10.1
        /// should succeed and get normal forumId
        /// catch any exception in case of reuse of forum topic
        /// </CreateForumTest>
        [TestMethod]
        public void CreateForumTest()
        {
            List<int> forumIdsBefore = bridge.GetForumIds();
            List<string> forumTopicsBefore = bridge.GetForumTopics();
            string currentTopic = topic[0];
            try
            {
                int forumId = bridge.CreateForum(currentTopic,
                                            1, "",
                                            true, false, true,
                                            true, 3);

                List<int> forumIdsAfter = bridge.GetForumIds();
                List<string> forumTopicsAfter = bridge.GetForumTopics();
                Assert.IsTrue(!forumTopicsBefore.Contains(currentTopic));
                Assert.IsTrue((forumId > -1) && (!forumIdsBefore.Contains(forumId)) && (forumIdsAfter.Contains(forumId)));
                forumsIds.Add(forumId);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }



        /// <RegisterTestSuccess>
        /// TestId: 10.2
        /// should succeedd and get normal userId
        /// </RegisterTestSuccess>
        [TestMethod]
        public void RegisterTestSuccess()
        {
            int forumId = CreateForum();
            List<int> registeredUsersBefore = bridge.getRegisteredUsers(forumId);
            try
            {
                int userId = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
                List<int> registeredUsersAfter = bridge.getRegisteredUsers(forumId);
                Assert.IsFalse(registeredUsersBefore.Contains(userId));
                Assert.IsTrue(registeredUsersAfter.Contains(userId));
                Assert.IsTrue(userId > -1);
            }
            catch 
            {
                Assert.IsTrue(true);
            }
        }


        
        /// <RegisterTestFailure>
        /// TestId: 10.3
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
        /// TestId: 10.4
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
                bridge.Register("SetPolicy", "ABCDE", emails[0], forumId);
                Assert.Fail("Exception was expected but not thrown. lowercase letters are required");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }





        /// <LoginTest>
        /// TestId: 10.5
        /// should succeed and get the same userId
        /// </LoginTest>
        [TestMethod]
        public void LoginTest()
        {
            if (forumsIds.Count == 0)
                CreateForumTest();
            List<int> forumMembers = bridge.getRegisteredUsers(forumsIds[forumsIds.Count - 1]);
            int userId = bridge.Register(userNames[0], passwords[0], emails[0], forumsIds[forumsIds.Count-1]);
            int falseId = userId + 1;
            int loggedUser = bridge.Login(userNames[0], passwords[0], forumsIds[forumsIds.Count - 1]);
            Assert.Equals(userId, loggedUser);
            try
            {
                int loggedFalseUser = bridge.Login(userNames[0], passwords[0], forumsIds[forumsIds.Count - 1]);
                Assert.Fail("Unregistered user should not be able to login");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }


        /// <LogoutTest>
        /// TestId: 10.6
        /// test if connected user can logout 
        /// </LogoutTest>
        [TestMethod]
        public void LogoutTest()
        {
            int forumId = CreateForum();
            int userId = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            int falseUserId = userId + 1;
            int loggedUser = bridge.Login(userNames[0], passwords[0], forumId);
            bool disconnected = bridge.Logout(loggedUser, forumId);
            Assert.IsTrue(disconnected);
            Boolean falseUserLogout = bridge.Logout(falseUserId, forumId);
            Assert.IsFalse(falseUserLogout);
            
        }



        /// <LogoutTestFail>
        /// TestId: 10.7
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
        /// TestId: 10.8
        /// test if subForum created
        /// </CreateSubForumTest>
        [TestMethod]
        public void CreateSubForumTest()
        {
            int forumId = CreateForum();
            int subForumId = bridge.CreateSubForum(forumId, topic[0]);
            Assert.IsTrue(subForumId > 0);
            Assert.IsTrue((bridge.GetSubForumsIds(forumId)).Contains(subForumId));     
        }



        /// <CreateSubForumTestFail>
        /// TestId: 10.9
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
            try
            {
                bridge.CreateSubForum(forumId + 1, topic[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot create subForum with null topic");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        /// <ViewTest>
        /// TestId: 10.10
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


        /// <RemoveForumTest>
        /// TestId: 10.11
        /// should catch an exception when trying to view non-existed forum
        /// </RemoveForumTest>
        [TestMethod]
        public void RemoveForumTest()
        {
            int forumId = CreateForum();
            bridge.RemoveForum(forumId);
            List<string> subForumNames = new List<string>();
            List<int> subForumIds = new List<int>();
            try
            {
                bridge.View(forumId, out subForumNames, out subForumIds);
                Assert.Fail("Exception was expected but not thrown. Cannot view non-existed forum");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }


        /// <PublishTestSuccess>
        /// TestId: 10.12
        /// test if message published
        /// </PublishTestSuccess>
        [TestMethod]
        public void PublishTestSuccess()
        {
            int forumId = CreateForum();
            int subForumId = bridge.CreateSubForum(forumId, topic[0]);
            int publisherID = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            int messageId = bridge.Publish(forumId, subForumId, publisherID, titles[0], body[0]);
            List<int> allThreads = bridge.getAllThreads(forumId, subForumId);
            Assert.IsTrue(messageId > 0);
            Assert.IsTrue(allThreads.Contains(messageId));
        }


        /// <PublishTestWrongTitle>
        /// TestId: 10.13
        /// test if published message with empty\null title gets exception
        /// </PublishTestWrongTitle>
        [TestMethod]
        public void PublishTestWrongTitle()
        {
            int forumId = CreateForum();
            int subForumId = bridge.CreateSubForum(forumId, topic[0]);
            int publisherID = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            try
            {
                bridge.Publish(forumId, subForumId, publisherID, "", body[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot publish message with empty title");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
            try
            {
                bridge.Publish(forumId, subForumId, publisherID, null, body[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot publish message with null title");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }



        /// <CommentTestSuccess>
        /// TestId: 10.14
        /// test if comment message published
        /// </CommentTestSuccess>
        [TestMethod]
        public void CommentTestSuccess()
        {
            int forumId = CreateForum();
            int subForumId = bridge.CreateSubForum(forumId, topic[0]);
            int publisherID = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            int firstMessageId = bridge.Publish(forumId, subForumId, publisherID, titles[0], body[0]);
            int responseMessageId = bridge.Comment(firstMessageId, publisherID, titles[1], body[1]);
            List<int> allComments = bridge.getAllComments(forumId, subForumId, firstMessageId);
            Assert.IsTrue(responseMessageId > 0);
            Assert.IsTrue(allComments.Contains(responseMessageId));
        }


        /// <DeleteOnlyThreadTest>
        /// TestId: 10.15
        /// delete thread which doesn't have response messages
        /// </DeleteOnlyThreadTest>
        [TestMethod]
        public void DeleteOnlyThreadTest()
        {
            int forumId = CreateForum();
            int subForumId = bridge.CreateSubForum(forumId, topic[0]);
            int publisherID = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            int threadId = bridge.Publish(forumId, subForumId, publisherID, titles[0], body[0]);
            bridge.DeleteMessage(threadId);
            List<int> allThreads = bridge.getAllThreads(forumId, subForumId);
            Assert.IsFalse(allThreads.Contains(threadId));
            try
            {
                int responseMessageId = bridge.Comment(threadId, publisherID, titles[1], body[1]);
                Assert.Fail("Exception was expected but not thrown. Cannot comment on non-existing thread");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }


        /// <DeleteCommentTest>
        /// TestId: 10.16
        /// delete response message 
        /// </DeleteCommentTest>
        [TestMethod]
        public void DeleteCommentTest()
        {
            int forumId = CreateForum();
            int subForumId = bridge.CreateSubForum(forumId, topic[0]);
            int publisherID = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            int firstMessageId = bridge.Publish(forumId, subForumId, publisherID, titles[0], body[0]);
            int responseMessageId = bridge.Comment(firstMessageId, publisherID, titles[1], body[1]);
            bridge.DeleteMessage(responseMessageId);
            List<int> allComments = bridge.getAllComments(forumId,subForumId,firstMessageId);
            try
            {
                Assert.IsFalse(allComments.Contains(responseMessageId));
                bridge.DeleteMessage(responseMessageId);
                Assert.Fail("Exception was expected but not thrown. Cannot delete on non-existing response message");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }


        /// <DeleteThreadWithCommentsTest>
        /// TestId: 10.17
        /// delete thread with all of its comments 
        /// </DeleteThreadWithCommentsTest>
        [TestMethod]
        public void DeleteThreadWithCommentsTest()
        {
            int forumId = CreateForum();
            int subForumId = bridge.CreateSubForum(forumId, topic[0]);
            int publisherID = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            int firstMessageId = bridge.Publish(forumId, subForumId, publisherID, titles[0], body[0]);
            int responseMessageId = bridge.Comment(firstMessageId, publisherID, titles[1], body[1]);
            bridge.DeleteMessage(firstMessageId);
            List<int> allMessages = bridge.getAllThreads(forumId, subForumId);
            try
            {
                List<int> allComments = bridge.getAllComments(forumId, subForumId, firstMessageId);
                Assert.Fail("Exception was expected but not thrown. Cannot get comments of deleted message");
            }
            catch
            {
                Assert.IsTrue(true);
            }
            try
            {
                Assert.IsFalse(allMessages.Contains(firstMessageId));
                bridge.DeleteMessage(responseMessageId);
                Assert.Fail("Exception was expected but not thrown. First message should be deleted with all of its comments");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }


        

        /// <AddModeratorTest>
        /// TestId: 10.18
        /// test if moderator added to subForum
        /// </AddModeratorTest>
        [TestMethod]
        public void AddModeratorTest()
        {
            int forumId = CreateForum();
            int subForumId = bridge.CreateSubForum(forumId, topic[0]);
            int moderatorId = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            bridge.AddModerator(forumId, subForumId, moderatorId);
            List<int> allModerators = bridge.getModerators(forumId, subForumId);
            Assert.IsTrue(allModerators.Contains(moderatorId));
            try
            {
                bridge.AddModerator(forumId, subForumId, moderatorId);
                Assert.Fail("Exception was expected but not thrown. Cannot add the same moderator twice");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }           
        }


        /// <RemoveModeratorTest>
        /// TestId: 10.19
        /// test if moderator removed from subForum
        /// </RemoveModeratorTest>
        [TestMethod]
        public void RemoveModeratorTest()
        {
            int forumId = CreateForum();
            int subForumId = bridge.CreateSubForum(forumId, topic[0]);
            int moderatorId = bridge.Register(userNames[0], passwords[0], emails[0], forumId);
            bridge.AddModerator(forumId, subForumId, moderatorId);
            bridge.RemoveModerator(forumId, subForumId, moderatorId);
            List<int> allModerators = bridge.getModerators(forumId, subForumId);
            Assert.IsFalse(allModerators.Contains(moderatorId));
            try
            {
                bridge.RemoveModerator(forumId, subForumId, moderatorId);
                Assert.Fail("Exception was expected but not thrown. Cannot remove non-existed moderator");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

           
    }
}
