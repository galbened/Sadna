using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using Forum;

namespace Forum.UnitTests
{
    [TestClass]
    public class ForumUnitTest
    {
        String[] titels = { "sport", "nature" };
        String[] subTitels = { "football", "basketball", "animals", "plants" };
        IForumManager fm = ForumManager.getInstance();
        String[] user = { "tomer", "tomer@gmail.com", "123456" };

        /*
         * Testing creating forum:
         * creating forums eith two different titles returns two different IDs
         * second creation with same title returns -1
         */
        [TestMethod]
        public void creatingForumReturnsDiffIDTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int id2 = fm.CreateForum(titels[1]);
            Assert.AreNotEqual(id1, id2);
            fm.RemoveForum(id1);
            fm.RemoveForum(id2);
        }

        [TestMethod]
        public void creatingForumWithExistTiltleFailTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int id3 = fm.CreateForum(titels[0]);
            Assert.AreEqual(id3, -1);
            fm.RemoveForum(id1);
        }

        /*
          * Testing creating forum:
          * creating subforums with two different topics in same forum returns two different IDs
          * second creation with same topic in same forum returns -1
          */
        [TestMethod]
        public void creatingSubForumReturnsDiffIDTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int id2 = fm.CreateSubForum(subTitels[1], id1, 1);
            int id3 = fm.CreateSubForum(subTitels[0], id1, 1);
            Assert.AreNotEqual(id1, id2);
            fm.RemoveForum(id1);
            fm.RemoveSubForum(id1, id2, 1);
            fm.RemoveSubForum(id1, id3, 1);
        }

        [TestMethod]
        public void creatingSubForumWithExistTiltleFailTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int id2 = fm.CreateSubForum(subTitels[0], id1, 1);
            int id3 = fm.CreateSubForum(subTitels[0], id1, 1);
            Assert.AreEqual(id3, -1);
            fm.RemoveForum(id1);
            fm.RemoveSubForum(id1, id2, 1);
        }

        /*
         * testing adding and removal of admin to forum
         */
        [TestMethod]
        public void addAdminTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(user[0], user[1], user[2], id1);
            fm.AddAdmin(userId, id1);
            Assert.IsTrue(fm.IsAdmin(userId, id1));
            fm.RemoveAdmin(userId, id1);
            fm.UnRegister(userId, id1);
            fm.RemoveForum(id1);
        }

        [TestMethod]
        public void removeAdminTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(user[0], user[1], user[2], id1);
            Assert.IsFalse(fm.IsAdmin(userId, id1));
            fm.AddAdmin(userId, id1);
            fm.RemoveAdmin(userId, id1);
            Assert.IsFalse(fm.IsAdmin(userId, id1));
            fm.UnRegister(userId, id1);
            fm.RemoveForum(id1);
        }

        /*
         * testing adding and removal of moderator to subforum
         */
        [TestMethod]
        public void addModeratorTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, 1);
            int userId = fm.Register(user[0], user[1], user[2], forumId);
            Console.WriteLine(subForumId);
            fm.AddModerator(userId, forumId, subForumId, 1);
            Assert.IsTrue(fm.IsModerator(userId, forumId, subForumId));
            fm.RemoveModerator(userId, forumId, subForumId);
            fm.UnRegister(userId, forumId);
            fm.RemoveSubForum(forumId, subForumId, 1);
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void removeModeratorTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, 1);
            int userId = fm.Register(user[0], user[1], user[2], forumId);
            Assert.IsFalse(fm.IsModerator(userId, forumId, subForumId));
            fm.AddModerator(userId, forumId, subForumId, 1);
            fm.RemoveModerator(userId, forumId, subForumId);
            Assert.IsFalse(fm.IsModerator(userId, forumId, subForumId));
            fm.UnRegister(userId, forumId);
            fm.RemoveSubForum(forumId, subForumId, 1);
            fm.RemoveForum(forumId);
        }
    }
}
