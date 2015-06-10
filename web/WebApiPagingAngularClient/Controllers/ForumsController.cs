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
        public IHttpActionResult Get()
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

                return Ok(result);
        }

        // GET: api/forums/createForum
        [Route("createForum")]
        public IHttpActionResult Post(newForumParams forum)
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
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: api/forums/createSubForum/forumId/topic
        [Route("createSubForum/{forumId:int}")]
        public IHttpActionResult Post(int forumId, newSubForumParams topic)
        {
            try
            {
                int subForumId = driver.CreateSubForum(forumId, topic.topic);
                var data = new
                {
                    id = subForumId,
                    topic = topic,
                    forumId = forumId
                };

                var result = new
                {
                    data = data
                };

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: api/forums/getForum/forumId
        [Route("getForum/{forumId:int}")]
        public IHttpActionResult Get(int forumId)
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

            return Ok(result);
        }

        // GET: api/forums/signup
        [Route("signup/{forumId}")]
        public IHttpActionResult Post(int forumId, signupParams sign)
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

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }

        }

        // GET: api/forums/addThread
        [Route("addThread")]
        public IHttpActionResult Post(newThreadParams th)
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

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }

        }

        // GET: api/forums/getSubForum/forumId/subForumId
        [Route("getSubForum/{forumId:int}/{subForumId:int}")]
        public IHttpActionResult Get(int forumId,int subForumId)
        {
            var threads = driver.GetAllThreads(forumId, subForumId);
            var subForum = new {
                subForumId = subForumId,
                title = "kashkasha",
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

            return Ok(result);
            
        }

    }
}
