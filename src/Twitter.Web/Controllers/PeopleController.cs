using System.Net;
using System.Net.Http;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Twitter.Shared.Business;
using Twitter.Shared.Messages;
using Twitter.Web.Actors;
using Twitter.Web.Models;

namespace Twitter.Web.Controllers
{
    public class PeopleController : ApiController
    {
        // GET api/people
        public IEnumerable<UserModel> Get()
        {
            return GetUsers().Select(UserModel.From).ToList();
        }

        // POST api/people/{username}
        public void Put(string username, [FromBody]UserModel user)
        {
            var existingUser = GetUsers().FirstOrDefault(x => string.Equals(x.Username, user.Username, StringComparison.OrdinalIgnoreCase));

            if (existingUser == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Can't find this user"));

            if (existingUser.IsFollowed == user.IsFollowed) return;

            var command = user.IsFollowed
                ? (object)new FollowUserCommand(User.Identity.Name, user.Username)
                : new UnfollowUserCommand(User.Identity.Name, user.Username);

            var result = SystemActors.RemoteApiActor.Ask<Results.BaseResult>(command, TimeSpan.FromSeconds(2)).Result;
            var failureResult = result as Results.FailureResult;
            if (failureResult != null)
            {
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError, failureResult.Error));
            }
        }

        private IEnumerable<UserData> GetUsers()
        {
            return
                SystemActors.RemoteApiActor.Ask<List<UserData>>(new GetPeopleCommand(User.Identity.Name),
                    TimeSpan.FromSeconds(2)).Result;
        }
    }
}