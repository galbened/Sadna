using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiPagingAngularClient.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class ClubPage
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }

    public class ClubQuery
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class ClubQuery1
    {
        public string Name { get; set; }        
    }

    public class Forum
    {
        public int Id { get; set; }
        public string title { get; set; }
        public List<SubForum> SubForums;
    }

    public class SubForum
    {
        public int Id { get; set; }
        public string title { get; set; }
        public List<Thread> Threads;

    }

    public class Thread
    {
        public int Id { get; set; }
        public string topic { get; set; }
        public string content { get; set; }
        public string date { get; set; }
        public string publisher { get; set; }
        public List<Comment> Comments;
    }

    public class Comment
    {
        public int Id { get; set; }
        public string topic { get; set; }
        public string content { get; set; }
        public string date { get; set; }
        public string publisher { get; set; }
    }

    public class signupParams
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    public class loginParams
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class newForumParams
    {
        public string name { get; set; }
        public int numOfModerators { get; set; }
        public string degreeOfEnsuring { get; set; }
        public Boolean uppercase { get; set; }
        public Boolean lowercase { get; set; }
        public Boolean numbers { get; set; }
        public Boolean symbols { get; set; }
        public int minLength { get; set; }
    }

    public class newSubForumParams
    {
        public string topic { get; set; }
    }

    public class newThreadParams
    {
        public int  forumId { get; set; }
        public int  subForumId { get; set; }
        public int  publisherID { get; set; }
        public string publisherName { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    public class newCommentParams
    {
        public int firstMessageId { get; set; }
        public int publisherID { get; set; }
        public string publisherName { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    public class editMessageParams
    {
        public int messageId { get; set; }
        public int publisherID { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}