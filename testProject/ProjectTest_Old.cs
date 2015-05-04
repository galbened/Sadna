using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using Message;
using Forum;

namespace testProject
{
    [TestClass]
    public class ProjectTest_Old
    {

        // SetUp information
        String[] titels = { "sport", "nature" };
        String[] subTitels = { "football", "basketball", "animals", "plants" };
        String[] topic = { "man u", "juve" };
        String[] body = { "best team in the world" };
        String[] userNames = { "tomer.b", "tomer.s", "gal.b", "gal.p", "osher" };
        String[] emails = { "tomer.b@gmail.com", "tomer.s@gmail.com", "gal.b@gmail.com", "gal.p@gmail.com", "osher@gmail.com" };
        String[] passwords = { "Ab3","123456", "abcdef" };
        String[] superAdmin = { "admin", "admin" };
        IMessageManager mm = MessageManager.Instance();
        IForumManager fm = ForumManager.getInstance();

         

        /*
         * use case - forum creation
         */

        [TestMethod]
        public void forumCreationTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            Assert.AreNotEqual(id1, -1);
            fm.RemoveForum(id1);
        }

        [TestMethod]
        public void forumCreationFailTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int id3 = fm.CreateForum(titels[0]);
            Assert.AreEqual(id3, -1);
            fm.RemoveForum(id1);
        }

        /*
         * use case - set policy
         */
        [TestMethod]
        public void setPolicyTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            fm.SetPolicy(4, "", true, true, true, false, 3, id1);
            Assert.AreNotEqual(-1,fm.Register(userNames[0], passwords[0], emails[0],id1));//password valid
            fm.RemoveForum(id1);
        }
        [TestMethod]
        public void setPolicyIncorrectPasswordTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            fm.SetPolicy(4, "", true, true, true, false, 3, id1);
            Assert.AreEqual(-1, fm.Register(userNames[0], passwords[1], emails[0], id1));//password only lowercase
            fm.RemoveForum(id1);
        }

        /*
         * use case - user registration
         */
        [TestMethod]
        public void userRegistrationToForumTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            Assert.AreNotEqual(-1, fm.Register(userNames[0], passwords[0], emails[0], id1));
            fm.RemoveForum(id1);
        }

        /*
         * use case - user login
         */
        [TestMethod]
        public void userLoginForumTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], id1);
            Assert.AreEqual(userId, fm.Login(userNames[0], passwords[0], id1));
            fm.RemoveForum(id1);
        }

        /*
         * use case - user logout
         */
        [TestMethod]
        public void userLogoutForumTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], id1);
            userId = fm.Login(userNames[0], passwords[0], id1);
            Assert.IsTrue(fm.Logout(userId,id1));
            fm.RemoveForum(id1);
        }

        [TestMethod]
        public void userLogoutNotLogedInForumTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], id1);
            Assert.IsFalse(fm.Logout(userId, id1));
            fm.RemoveForum(id1);
        }

        /*
         * use case - creation of sub forum
         */
        [TestMethod]
        public void subForumCreationTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], id1);
            fm.AddAdmin(userId, id1);
            Assert.AreNotEqual(-1, fm.CreateSubForum(subTitels[0], id1, userId));
            fm.RemoveForum(id1);
        }

        [TestMethod]
        public void subForumCreationUserNoAdminTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], id1);
            Assert.AreEqual(-2, fm.CreateSubForum(subTitels[0], id1, userId));
            fm.RemoveForum(id1);
        }

        /*
         * use case - publish message in sub forum
         */
        [TestMethod]
        public void publishMessageTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            int threadId1 = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            Assert.AreNotEqual(threadId1, -1);
            fm.RemoveForum(forumId);
        }

        /*
         * use case - publish response message in sub forum
         */
        [TestMethod]
        public void publishResponseTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            int threadId1 = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId1, userId, topic[1], body[0]);
            Assert.AreNotEqual(commentID1, -1);
            fm.RemoveForum(forumId);
        }

        /*
         * use case - remove sub forum
         */
        [TestMethod]
        public void removeSubForumTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], id1);
            fm.AddAdmin(userId, id1);
            int id2 = fm.CreateSubForum(subTitels[1], id1, userId);
            Assert.AreNotEqual(id1, id2);
            Assert.IsTrue(fm.RemoveSubForum(id1, id2,userId));
            fm.RemoveForum(id1);
        }

        [TestMethod]
        public void removeSubForumNotAdminTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], id1);
            int id2 = fm.CreateSubForum(subTitels[1], id1, userId);
            Assert.AreNotEqual(id1, id2);
            Assert.IsFalse(fm.RemoveSubForum(id1, id2, userId));
            fm.RemoveForum(id1);
        }
    }
}
