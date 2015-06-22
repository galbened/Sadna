using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using Message;
using Forum;
using System.Collections.Generic;

namespace Message.UnitTests
{
    [TestClass]
    public class MessageUnitTests
    {
        String[] titels = { "sport", "nature" };
        String[] subTitels = { "football", "basketball", "animals", "plants" };
        String[] topic = { "man u", "juve" , "Haifa"};
        String[] body = { "best team in the world", "Forza Juve" , "sharrrlil"};
        String[] userNames = { "tomer.b", "tomer.s", "gal.b", "gal.p", "osher" };
        String[] emails = { "tomer.b@gmail.com", "tomer.s@gmail.com", "gal.b@gmail.com", "gal.p@gmail.com", "osher@gmail.com" };
        String[] passwords = { "123456", "abcdef" };
        IMessageManager mm = MessageManager.Instance();
        IForumManager fm = ForumManager.getInstance();

        /*testing add thread
         * should succeed when title not empty
         */
        [TestMethod]
        public void AddThreadTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int threadId1 = mm.addThread(forumId, subForumId, userId, userNames[0], topic[0], body[0]);
            int threadId2 = mm.addThread(forumId, subForumId, userId, userNames[0], topic[1], body[0]);
            Assert.AreNotEqual(threadId1, threadId2);
            fm.RemoveForum(1, forumId);
        }


        [TestMethod]
        public void AddThreadTitleEmptyTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            try
            {
                int threadId1 = mm.addThread(forumId, subForumId, userId,userNames[0], null, body[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot add thread with null title");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            fm.RemoveForum(1, forumId);
        }

        /*testing add comment
         * should succeed when title not empty
         */
        [TestMethod]
        public void AddCommentTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId,userNames[0], topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId,userNames[0], topic[1], body[0]);
            int commentID2 = mm.addComment(threadId, userId,userNames[0], topic[1], body[0]);
            Assert.AreNotEqual(commentID1, commentID2);
            int numOfComments = mm.GetNumOfComments(threadId);
            Assert.AreEqual(numOfComments, 2);
            fm.RemoveForum(1, forumId);
        }

        [TestMethod]
        public void AddCommentTitleEmptyTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId,userNames[0], topic[0], body[0]);
            try
            {
                 int commentID1 = mm.addComment(threadId, userId,userNames[0], "", body[0]);
                 Assert.Fail("Exception was expected but not thrown. Cannot add comment with empty title");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            fm.RemoveForum(1, forumId);

            
        }

        [TestMethod]
        public void AddCommentThreadNotExistsTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            try
            {
                int commentID1 = mm.addComment(5, userId,userNames[0], topic[1], body[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot add comment to non-existing thread");
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true);
            }

            fm.RemoveForum(1, forumId);
        }

        /*testing edit message
         * should succeed when title not empty and message ID exists
         */
        [TestMethod]
        public void EditMessageTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId,userNames[0], topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, userNames[0], topic[1], body[0]);
            Assert.IsTrue(mm.editMessage(commentID1, topic[0], body[0], userId));
            fm.RemoveForum(1, forumId);
        }

        [TestMethod]
        public void EditMessageTitleEmptyTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId,userNames[0], topic[0], body[0]);
            try
            {
                int commentID1 = mm.addComment(threadId, userId,userNames[0], /*topic[1]*/"", body[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot add comment to non-existing thread");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            fm.RemoveForum(1, forumId);
        }

        [TestMethod]
        public void EditCommentMessageNotExistsTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId,userNames[0], topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId,userNames[0], topic[1], body[0]);
            try
            {
                Assert.IsFalse(mm.editMessage(-200, topic[0], body[0], 1));
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true);
            }
            fm.RemoveForum(1, forumId);           
        }

        /*testing delete message
         * should succeed when message ID exists
         */
        [TestMethod]
        public void DeleteMessageTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId,userNames[0], topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId,userNames[0], topic[1], body[0]);
            Assert.IsTrue(mm.deleteMessage(commentID1));
            fm.RemoveForum(1, forumId);
        }

        [TestMethod]
        public void DeleteCommentMessageNotExistsTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId, userNames[0], topic[0], body[0]);
            try
            {
                int commentID1 = mm.addComment(-5, userId,userNames[0], topic[1], body[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot delete comment to non-existing thread");
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true);
            }
            fm.RemoveForum(1, forumId);
        }

        [TestMethod]
        public void GetAllThreadCommentsTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId, userNames[0], topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, userNames[0], topic[1], body[0]);
            int commentID2 = mm.addComment(threadId, userId, userNames[1], topic[2], body[1]);
            List<CommentInfo> comments = mm.GetAllThreadComments(threadId);
            Assert.AreEqual(comments.Count, 2);
            //Testing the first comment
            Assert.AreEqual(comments[0].Id, commentID1);
            Assert.AreEqual(comments[0].publisher, userNames[0]);
            Assert.AreEqual(comments[0].topic, topic[1]);
            Assert.AreEqual(comments[0].content, body[0]);
            //Testing the second comment
            Assert.AreEqual(comments[1].Id, commentID2);
            Assert.AreEqual(comments[1].publisher, userNames[1]);
            Assert.AreEqual(comments[1].topic, topic[2]);
            Assert.AreEqual(comments[1].content, body[1]);
        }


        [TestMethod]
        public void GetAllThreadsTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int threadIdWithComment = mm.addThread(forumId, subForumId, userId, userNames[0], topic[0], body[0]);
            int commentID1 = mm.addComment(threadIdWithComment, userId, userNames[0], topic[1], body[1]);
            int threadIdNoComments = mm.addThread(forumId, subForumId, userId, userNames[1], topic[2], body[2]);
            List<ThreadInfo> threads = mm.GetAllThreads(forumId, subForumId);
            Assert.AreEqual(threads.Count, 2);
            //Testing the first thread
            Assert.AreEqual(threads[0].id, threadIdWithComment);
            Assert.AreEqual(threads[0].publisher, userNames[0]);
            Assert.AreEqual(threads[0].topic, topic[0]);
            Assert.AreEqual(threads[0].content, body[0]);
            //Testing the comment of the first thread
            Assert.AreEqual(threads[0].comments.Count, 1);
            Assert.AreEqual(threads[0].comments[0].Id, commentID1);
            Assert.AreEqual(threads[0].comments[0].publisher, userNames[0]);
            Assert.AreEqual(threads[0].comments[0].topic, topic[1]);
            Assert.AreEqual(threads[0].comments[0].content, body[1]);

            //Testing the second thread
            Assert.AreEqual(threads[1].id, threadIdNoComments);
            Assert.AreEqual(threads[1].publisher, userNames[1]);
            Assert.AreEqual(threads[1].topic, topic[2]);
            Assert.AreEqual(threads[1].content, body[2]);
            Assert.AreEqual(threads[1].comments.Count, 0);
        }

        [TestMethod]
        public void NumOfMessagesTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int threadIdWithComment = mm.addThread(forumId, subForumId, userId, userNames[0], topic[0], body[0]);
            int commentID1 = mm.addComment(threadIdWithComment, userId, userNames[0], topic[1], body[1]);
            int threadIdNoComments = mm.addThread(forumId, subForumId, userId, userNames[1], topic[2], body[2]);
            int numOfMessages = mm.NumOfMessages(forumId, subForumId);
            Assert.AreEqual(numOfMessages, 3);
        }


        [TestMethod]
        public void AddMessageBadWordTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(1, userId, forumId);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            try
            {
                int threadIdWithComment = mm.addThread(forumId, subForumId, userId, userNames[0], "shirmut", body[0]);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            try
            {
                int threadIdWithComment = mm.addThread(forumId, subForumId, userId, userNames[0], topic[0], "fuck");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
