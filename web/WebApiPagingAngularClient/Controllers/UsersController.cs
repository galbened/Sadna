using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiPagingAngularClient.Models;
using WebApiPagingAngularClient.Utility;

namespace WebApiPagingAngularClient.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        // GET: api/users/getUser
        [Route("getUser")]
        public IHttpActionResult Get()
        {
            var data = new
            {
                username = "tomer",
                id = 5
            };

            var result = new
            {
                data = data
            };

            return Ok(result);
        }

        // GET: api/users/login/username/password/forumId
        [Route("login/{username}/{password}/{forumId}")]
        public IHttpActionResult Get(string username, string password,int forumId)
        {
            var data = new
            {
                username = "tomer",
                id = 5
            };

            var result = new
            {
                data = data
            };

            return Ok(result);
        }

        // GET: api/users/login/userId/forumId
        [Route("logout/{userId}/{forumId}")]
        public IHttpActionResult Get(int userId, int forumId)
        {
            var data = new
            {
                username = "tomer",
                id = 5
            };

            var result = new
            {
                data = data
            };

            return Ok(result);
        }

        // GET: api/users/signup
        [Route("signup/{forumId}")]
        public IHttpActionResult Post(int forumId, signupParams sign)
        {
            var data = new
            {
                username = sign.username,
                email = sign.email,
                password = sign.password,
                forumId = forumId,
                id = 5
            };

            var result = new
            {
                data = data
            };

            return Ok(result);
        }
    }


}
