﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using System.Collections.Generic;
using Forum;

namespace Forum.UnitTests
{
    [TestClass]
    public class ForumUnitTest
    {
        String[] titels = { "sport", "nature" };
        String[] subTitels = { "football", "basketball", "animals", "plants" };
        IForumManager fm = ForumManager.getInstance();
        String[] user = { "tomer", "123456", "tomer@gmail.com"};

        /*
         * Testing creating forum:
         * creating forums eith two different titles returns two different IDs
         * second creation with same title returns -1
         */
        [TestMethod]
        public void creatingForumReturnsDiffIDTest()
        {
            int id1 = fm.CreateForum(1,titels[0]);
            int id2 = fm.CreateForum(1,titels[1]);
            Assert.AreNotEqual(id1, id2);
            fm.RemoveForum(1,id1);
            fm.RemoveForum(1,id2);
        }

        [TestMethod]
        public void creatingForumWithExistTiltleFailTest()
        {
            int id1 = fm.CreateForum(1,titels[0]);
            try
            {
                int id3 = fm.CreateForum(1,titels[0]);
                Assert.Fail("succeed to create Forum with already exist title");
            }
            catch(ArgumentException){
            Assert.IsTrue(true);
            }
            fm.RemoveForum(1,id1);
        }

        /*
          * Testing creating forum:
          * creating subforums with two different topics in same forum returns two different IDs
          * second creation with same topic in same forum returns -1
          */
        [TestMethod]
        public void creatingSubForumReturnsDiffIDTest()
        {
            int id1 = fm.CreateForum(1,titels[0]);
            int id2 = fm.CreateSubForum(1,subTitels[1], id1);
            int id3 = fm.CreateSubForum(1, subTitels[0], id1);
            Assert.AreNotEqual(id1, id2);
            fm.RemoveForum(1, id1);
            fm.RemoveSubForum(1, id1, id2);
            fm.RemoveSubForum(1, id1, id3);
        }

        [TestMethod]
        public void creatingSubForumWithExistTiltleFailTest()
        {
            int id1 = fm.CreateForum(1, titels[0]);
            int id2 = fm.CreateSubForum(1,subTitels[0], id1);
            try
            {
                int id3 = fm.CreateSubForum(1, subTitels[0], id1);
                Assert.Fail("succeed to create SubForum with already exist title");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            fm.RemoveForum(1, id1);
            fm.RemoveSubForum(1, id1, id2);
        }

        /*
         * testing adding and removal of admin to forum
         */
        [TestMethod]
        public void addAdminTest()
        {
            int id1 = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(user[0], user[1], user[2], id1);
            fm.AddAdmin(1, userId, id1);
            Assert.IsTrue(fm.IsAdmin(userId, id1));
            fm.RemoveAdmin(1, userId, id1);
            fm.UnRegister(userId, id1);
            fm.RemoveForum(1, id1);
        }

        [TestMethod]
        public void removeAdminTest()
        {
            int id1 = fm.CreateForum(1, titels[0]);
            int userId = fm.Register(user[0], user[1], user[2], id1);
            Assert.IsFalse(fm.IsAdmin(userId, id1));
            fm.AddAdmin(1, userId, id1);
            fm.RemoveAdmin(1, userId, id1);
            Assert.IsFalse(fm.IsAdmin(userId, id1));
            fm.UnRegister(userId, id1);
            fm.RemoveForum(1, id1);
        }

        /*
         * testing adding and removal of moderator to subforum
         */
        [TestMethod]
        public void addModeratorTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int userId = fm.Register(user[0], user[1], user[2], forumId);
            Console.WriteLine(subForumId);
            fm.AddModerator(1, forumId, subForumId, userId);
            Assert.IsTrue(fm.IsModerator(userId, forumId, subForumId));
            fm.RemoveModerator(1, userId, forumId, subForumId);
            fm.UnRegister(userId, forumId);
            fm.RemoveSubForum(1, forumId, subForumId);
            fm.RemoveForum(1, forumId);
        }

        [TestMethod]
        public void removeModeratorTest()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int userId = fm.Register(user[0], user[1], user[2], forumId);
            Assert.IsFalse(fm.IsModerator(userId, forumId, subForumId));
            fm.AddModerator(1, forumId, subForumId, userId);
            fm.RemoveModerator(1, userId, forumId, subForumId);
            Assert.IsFalse(fm.IsModerator(userId, forumId, subForumId));
            fm.UnRegister(userId, forumId);
            fm.RemoveSubForum(1, forumId, subForumId);
            fm.RemoveForum(1, forumId);
        }


        [TestMethod]
        public void UnregisteredUserLogToForumTest()
        {
            int forumId1 = fm.CreateForum(1, titels[0]);
            int forumId2 = fm.CreateForum(1, titels[1]);
            int userId = fm.Register(user[0], user[1], user[2], forumId1);
            Assert.IsTrue(fm.isRegisteredUser(forumId1, userId));
            fm.Logout(userId, forumId1);
            Assert.IsFalse(fm.isLoggedUser(forumId1, userId));
            try
            {
                fm.Login(user[0], user[1], forumId2);
                Assert.Fail("Exception was expected but not thrown. Unregistered user cannot log forum");
            }
            catch (Exception ex)
            {
                Assert.IsFalse(fm.isLoggedUser(forumId2, userId));
            }
            fm.UnRegister(userId, forumId1);
            fm.RemoveForum(1, forumId1);
            fm.RemoveForum(1, forumId2);
        }

        [TestMethod]
        public void ComplainModerator()
        {
            int forumId = fm.CreateForum(1, titels[0]);
            int subForumId = fm.CreateSubForum(1, subTitels[0], forumId);
            int userRequesterId = fm.Register(user[0], user[1], user[2], forumId);
            int moderator = fm.Register("moderator", user[1], user[2], forumId);
            try
            {
                fm.ComplainModerator(userRequesterId, moderator, forumId, subForumId);
                Assert.Fail("Exception was expected but not thrown. Cannot complain on non-moderator user");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            fm.AddModerator(1, forumId, subForumId ,moderator);
            List<int> moderators = fm.GetModeratorIds(forumId, subForumId);
            Assert.IsTrue(moderators.Contains(moderator));
            fm.ComplainModerator(userRequesterId, moderator, forumId, subForumId);
        }
    }
}
