using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Interfaces;
using Driver;

namespace WebCommunicationClientSoftware.Controllers
{
    [RoutePrefix("api/forums")]
    public class ForumsController : ApiController
    {
        //private static IApplicationBridge driver = new BridgeReal();

        public ForumsController()
        {
        }
        // GET: api/forums/getForumsIds
        [Route("getForumsIds")]
        public List<int> Get()
        {
            Console.Write("!!!!!!!!!!!1\ngot getforums request!\n!!!!!!!!!!!1\n");
            //List<int> ids = driver.GetForumIds();
            List<int> ids = new List<int>();
            ids.Add(3);
            ids.Add(4);
            return ids;
        }

    }
}
