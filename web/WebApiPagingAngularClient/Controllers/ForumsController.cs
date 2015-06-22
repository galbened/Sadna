using System;
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
        private static IApplicationBridge driver = new BridgeReal();

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
                int forumId = driver.CreateForum(forum.name, forum.numOfModerators, forum.degreeOfEnsuring,
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

        // GET: api/forums/createSubForum/forumId/topic
        [Route("createSubForum/{forumId:int}")]
        public HttpResponseMessage Post(int forumId, newSubForumParams topic)
        {
            try
            {
                int subForumId = driver.CreateSubForum(forumId, topic.topic);
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

    }
}
