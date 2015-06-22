using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using WebServerForGui.Models;
using Interfaces;
using Driver;

namespace WebCommunicationClientSoftware.Controllers
{
    [RoutePrefix("api/forums")]
    public class ForumsController : ApiController
    {
        private static IApplicationBridge driver = new BridgeReal();

        // GET: api/forums/getForums
        [Route("getForums")]
        public IHttpActionResult Get()
        {
            Console.Write("!!!!!!!!!!!1\ngot getforums request!\n!!!!!!!!!!!1\n");
            List<int> ids = driver.GetForumIds();
            List<string> names = driver.GetForumTopics();
            Dictionary<int, string> result = new Dictionary<int, string>();
            for (int i = 0; i < ids.Count(); i++)
                result.Add(ids.ElementAt(i), names.ElementAt(i));

            return Ok(result);
        }
    }
}
