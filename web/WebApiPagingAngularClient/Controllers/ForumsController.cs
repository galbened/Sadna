﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiPagingAngularClient.Models;
using WebApiPagingAngularClient.Utility;
using Interfaces;
using Driver;

namespace WebApiPagingAngularClient.Controllers
{
    [RoutePrefix("api/forums")]
    public class ForumsController : ApiController
    {
        private static IApplicationBridge driver = BridgeReal.GetInstance();
        private static int SUPER_ADMIN = 1;

        public ForumsController()
        {
        }

        // GET: api/forums/getForums
        [Route("getForums")]
        public HttpResponseMessage Get()
            {
                var ids = driver.GetForumIds();
                var names = driver.GetForumTopics();

                var data =  new
                {
                    ids = ids,
                    names = names
                };
            
                var result = new
                {
                    data = data
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
        }

        // GET: api/forums/createForum
        [Route("createForum")]
        public HttpResponseMessage Post(newForumParams forum)
        {
            try
            {
                int forumId = driver.CreateForum(1,forum.name, forum.numOfModerators, forum.degreeOfEnsuring,
                    forum.uppercase, forum.lowercase, forum.numbers, forum.symbols, forum.minLength);
                var data = new
                {
                    Id = forumId,
                    title = forum.name,
                };
                var result = new
                {
                    data = data
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
            catch(Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }
        }

        // GET: api/forums/setPolicy/forumId/userId
        [Route("setPolicy/{forumId:int}/{userId:int}")]
        public HttpResponseMessage Post(int forumId,int userId,newForumParams forum)
        {
            try
            {
                driver.SetPolicy(userId, forumId, forum.numOfModerators, forum.degreeOfEnsuring,
                    forum.uppercase, forum.lowercase, forum.numbers, forum.symbols, forum.minLength);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }
        }

        // GET: api/forums/createSubForum/userId/forumId/
        [Route("createSubForum/{userId}/{forumId:int}")]
        public HttpResponseMessage Post(int userId,int forumId, newSubForumParams topic)
        {
            try
            {
                int subForumId = driver.CreateSubForum(userId, forumId, topic.topic);
                var data = new
                {
                    id = subForumId,
                    title = topic.topic
                };

                var result = new
                {
                    data = data
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }
        }

        // GET: api/forums/getForum/forumId
        [Route("getForum/{forumId:int}")]
        public HttpResponseMessage Get(int forumId)
        {
            var name = driver.GetForumName(forumId);
            var subForumIds = driver.GetSubForumsIds(forumId);
            var subForumTopics = driver.GetSubForumsTopics(forumId);

            var data = new
            {
                id = forumId,
                title = name,
                subForumIds = subForumIds,
                subForumTopics = subForumTopics
            };

            var result = new
            {
                data = data
            };

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        // GET: api/forums/getUser/forumId/userId
        [Route("getUser/{forumId:int}/{userId:int}")]
        public HttpResponseMessage Get_user(int forumId, int userId)
        {
            if (userId == SUPER_ADMIN)
            {
                var data = new
                {
                    id = 1,
                    username = "Admin",
                    type = "admin"
                };
                var result = new
                {
                    data = data
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
            else
            {
                var username = driver.GetUsername(forumId, userId);
                var type = driver.GetUserType(forumId, userId);
                var data = new
                {
                    id = userId,
                    username = username,
                    type = type
                };
                var result = new
                {
                    data = data
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
        }

        // GET: api/forums/signup
        [Route("signup/{forumId}")]
        public HttpResponseMessage Post(int forumId, signupParams sign)
        {
            try
            {
                var userId = driver.Register(sign.username, sign.password, sign.email, forumId);
                var data = new
                {
                    id = userId,
                    username = sign.username,
                    type = "member"
                };

                var result = new
                {
                    data = data
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }

        }

        // GET: api/forums/login/forumId
        [Route("login/{forumId:int}")]
        public HttpResponseMessage Post(int forumId, loginParams log)
        {
            try
            {
                if (log.username.CompareTo("Admin") == 0 && log.password.CompareTo("Admin") == 0)
                {
                    var data = new
                    {
                        id = 1,
                        username = "Admin",
                        type = "admin"
                    };

                    var result = new
                    {
                        data = data
                    };

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                    return response;
                }
                else
                {
                    var userId = driver.Login(log.username, log.password, forumId);
                    var data = new
                    {
                        id = userId,
                        username = log.username,
                        type = driver.GetUserType(forumId, userId)
                    };

                    var result = new
                    {
                        data = data
                    };

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                    return response;
                }
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }

        }

        // GET: api/forums/removeForum/forumId
        [Route("removeForum/{forumId:int}")]
        public HttpResponseMessage Post(int forumId)
        {
            try
            {
                driver.RemoveForum(1, forumId);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }

        }

        // GET: api/forums/removeSubForum/forumId/subForumId/userId
        [Route("removeSubForum/{forumId:int}/{subForumId:int}/{userId:int}")]
        public HttpResponseMessage Post_remove_subforum(int forumId, int subForumId, int userId)
        {
            try
            {
                driver.RemoveSubForum(userId, forumId, subForumId);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }

        }

        // GET: api/forums/logout/forumId/userId
        [Route("logout/{forumId:int}/{userId:int}")]
        public HttpResponseMessage Get_logout(int forumId, int userId)
        {
            if (userId == SUPER_ADMIN)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            HttpResponseMessage response;
            var succ = driver.Logout(userId, forumId);
            if(succ){
                response = Request.CreateResponse(HttpStatusCode.OK);
            }else{
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            
            return response;

        }

        // GET: api/forums/addThread
        [Route("addThread")]
        public HttpResponseMessage Post(newThreadParams th)
        {
            try
            {
                var threadId = driver.Publish(th.forumId, th.subForumId, th.publisherID, th.publisherName, th.title,th.body);
                var data = new
                {
                    threadId = threadId
                };

                var result = new
                {
                    data = data
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response; ;
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }

        }

        // GET: api/forums/addComment
        [Route("addComment")]
        public HttpResponseMessage Post(newCommentParams cm)
        {
            try
            {
                var commentId = driver.Comment(cm.firstMessageId, cm.publisherID, cm.publisherName, cm.title, cm.body);
                var data = new
                {
                    commentId = commentId
                };

                var result = new
                {
                    data = data
                };

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }

        }

        // GET: api/forums/editComment
        [Route("editMessage")]
        public HttpResponseMessage Post_editmessage(editMessageParams cm)
        {
            try
            {
                var commentId = driver.EditMessage(cm.publisherID, cm.messageId, cm.title, cm.body);


                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }

        }

        // GET: api/forums/getSubForum/forumId/subForumId
        [Route("getSubForum/{forumId:int}/{subForumId:int}")]
        public HttpResponseMessage Get_subforum(int forumId, int subForumId)
        {
            var threads = driver.GetAllThreads(forumId, subForumId);
            var subForum = new {
                subForumId = subForumId,
                title = driver.GetSubForumTopic(forumId, subForumId),
                threads = threads
            };
            var data = new
            {
                forumId = forumId,
                title = driver.GetForumName(forumId),
                subForum = subForum
            };
            var result = new
            {
                data = data
            };

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
            
        }

        // GET: api/forums/DeleteMessage/userId/messageId
        [Route("DeleteMessage/{userId:int}/{messageId:int}")]
        public HttpResponseMessage Post(int userId, int messageId)
        {
            var succ = driver.DeleteMessage(userId, messageId);
            HttpResponseMessage response;
            if (succ)
            {
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return response;

        }

        // GET: api/forums/getNotAdmins/forumId
        [Route("getNotAdmins/{forumId:int}")]
        public HttpResponseMessage Get_notadmins(int forumId)
        {
            var ids = driver.GetMembersNoAdminIds(forumId);
            var usernames = driver.GetMembersNoAdminNames(forumId);

            var data = new
            {
                ids = ids,
                usernames = usernames
            };

            var result = new
            {
                data = data
            };
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);

            return response;

        }

        // GET: api/forums/addAdmin/forumId/userId
        [Route("addAdmin/{forumId:int}/{userId:int}/{adminId:int}")]
        public HttpResponseMessage Post_addAdmin(int forumId, int userId,int adminId)
        {
            try{
                driver.AddAdmin(userId, forumId, adminId);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                return response;
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }

        }

        // GET: api/forums/getNotModerators/forumId/subForumId
        [Route("getNotModerators/{forumId:int}/{subForumId:int}")]
        public HttpResponseMessage Get_notadmins(int forumId, int subForumId)
        {
            var ids = driver.GetMembersNoModeratorIds(forumId, subForumId);
            var usernames = driver.GetMembersNoModeratorNames(forumId, subForumId);

            var data = new
            {
                ids = ids,
                usernames = usernames
            };

            var result = new
            {
                data = data
            };
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);

            return response;

        }

        // GET: api/forums/getModerators/forumId/subForumId
        [Route("getModerators/{forumId:int}/{subForumId:int}")]
        public HttpResponseMessage Get_moderators(int forumId, int subForumId)
        {
            var ids = driver.GetModeratorIds(forumId, subForumId);
            var usernames = driver.GetModeratorNames(forumId, subForumId);

            var data = new
            {
                ids = ids,
                usernames = usernames
            };

            var result = new
            {
                data = data
            };
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);

            return response;

        }

        // GET: api/forums/addModerator/forumId/subForumId/userId/moderetorId
        [Route("addModerator/{forumId:int}/{subForumId:int}/{userId:int}/{moderatorId:int}")]
        public HttpResponseMessage Post_addModerator(int forumId, int subForumId, int userId, int moderatorId)
        {
            try
            {
                driver.AddModerator(userId, forumId, subForumId, moderatorId);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                return response;
            }
            catch(Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }

        }

        // GET: api/forums/complainModerator/forumId/subForumId/userId/moderetorId
        [Route("complainModerator/{forumId:int}/{subForumId:int}/{userId:int}/{moderatorId:int}")]
        public HttpResponseMessage Post_complainModerator(int forumId, int subForumId, int userId, int moderatorId)
        {
            try
            {
                driver.ComplainModerator(userId,moderatorId, forumId, subForumId);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                return response;
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }

        }

        // GET: api/forums/addModerator/forumId/subForumId/userId/moderetorId
        [Route("removeModerator/{forumId:int}/{subForumId:int}/{userId:int}/{moderatorId:int}")]
        public HttpResponseMessage Post_removeModerator(int forumId, int subForumId, int userId, int moderatorId)
        {
            try
            {
                driver.RemoveModerator(userId, moderatorId, forumId, subForumId);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                return response;
            }
            catch (Exception e)
            {
                var data = new
                {
                    message = e.Message
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, data);
                return response;
            }

        }



    }
}
