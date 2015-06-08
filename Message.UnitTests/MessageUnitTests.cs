using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using Message;
using Forum;

namespace Message.UnitTests
{
    [TestClass]
    public class MessageUnitTests
    {
        String[] titels = { "sport", "nature" };
        String[] subTitels = { "football", "basketball", "animals", "plants" };
        String[] topic = { "man u", "juve" };
        String[] body = { "best team in the world" };
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
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId);
            int threadId1 = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int threadId2 = mm.addThread(forumId, subForumId, userId, topic[1], body[0]);
            Assert.AreNotEqual(threadId1, threadId2);
            fm.RemoveForum(forumId);
        }


        [TestMethod]
        public void AddThreadTitleEmptyTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId);
            try
            {
                int threadId1 = mm.addThread(forumId, subForumId, userId, null, body[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot add thread with null title");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            fm.RemoveForum(forumId);
        }

        /*testing add comment
         * should succeed when title not empty
         */
        [TestMethod]
        public void AddCommentTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, topic[1], body[0]);
            int commentID2 = mm.addComment(threadId, userId, topic[1], body[0]);
            Assert.AreNotEqual(commentID1, commentID2);
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void AddCommentTitleEmptyTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            try
            {
                 int commentID1 = mm.addComment(threadId, userId, "", body[0]);
                 Assert.Fail("Exception was expected but not thrown. Cannot add comment with empty title");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            fm.RemoveForum(forumId);

            
        }

        [TestMethod]
        public void AddCommentThreadNotExistsTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId);
            try
            {
                int commentID1 = mm.addComment(5, userId, topic[1], body[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot add comment to non-existing thread");
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true);
            }
            
            fm.RemoveForum(forumId);
        }

        /*testing edit message
         * should succeed when title not empty and message ID exists
         */
        [TestMethod]
        public void EditMessageTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, topic[1], body[0]);
            Assert.IsTrue(mm.editMessage(commentID1, topic[0], body[0], 1));
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void EditMessageTitleEmptyTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            try
            {
                int commentID1 = mm.addComment(threadId, userId, /*topic[1]*/"", body[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot add comment to non-existing thread");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void EditCommentMessageNotExistsTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, topic[1], body[0]);
            try
            {
                Assert.IsFalse(mm.editMessage(-200, topic[0], body[0], 1));
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true);
            }
            fm.RemoveForum(forumId);           
        }

        /*testing delete message
         * should succeed when message ID exists
         */
        [TestMethod]
        public void DeleteMessageTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, topic[1], body[0]);
            Assert.IsTrue(mm.deleteMessage(commentID1));
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void DeleteCommentMessageNotExistsTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            try
            {
                int commentID1 = mm.addComment(-5, userId, topic[1], body[0]);
                Assert.Fail("Exception was expected but not thrown. Cannot delete comment to non-existing thread");
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true);
            }          
            fm.RemoveForum(forumId);
        }
    }
}
