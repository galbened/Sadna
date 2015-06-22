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
        //private static IApplicationBridge driver = new BridgeReal();

        public ForumsController()
        {
        }
        // GET: api/forums/getForumsIds
        [Route("getForumsIds")]
        public List<int> GetIds()
        {
            Console.Write("!!!!!!!!!!!1\ngot getforums request!\n!!!!!!!!!!!1\n");
            //List<int> ids = driver.GetForumIds();
            List<int> ids = new List<int>();
            ids.Add(3);
            ids.Add(4);
            /*List<string> names = driver.GetForumTopics();
            List<string> names = new List<string>();
            names.Add("third");
            names.Add("forth");
            Dictionary<int, string> result = new Dictionary<int, string>();
            for (int i = 0; i < ids.Count(); i++)
                  result.Add(ids.ElementAt(i), names.ElementAt(i));*/
            return ids;
        }

        [Route("getForumsNames")]
        public List<string> GetNames()
        {
            Console.Write("!!!!!!!!!!!1\ngot getforums request!\n!!!!!!!!!!!1\n");
            //List<int> ids = driver.GetForumIds();
            /*List<int> ids = new List<int>();
            ids.Add(3);
            ids.Add(4);*/
            //List<string> names = driver.GetForumTopics();
            List<string> names = new List<string>();
            names.Add("third");
            names.Add("forth");
            /*Dictionary<int, string> result = new Dictionary<int, string>();
            for (int i = 0; i < ids.Count(); i++)
                result.Add(ids.ElementAt(i), names.ElementAt(i));*/
            return names;
            
        }
    }
}
