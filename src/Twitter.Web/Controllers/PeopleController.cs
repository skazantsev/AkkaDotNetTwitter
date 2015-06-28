using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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
            var users =
                SystemActors.RemoteApiActor.Ask<List<string>>(new GetUsersCommand(), TimeSpan.FromSeconds(5)).Result;
            return
                users.Except(new[] {User.Identity.Name})
                    .Select(x => new UserModel {Username = x, IsFollowed = false})
                    .ToList();
        }

        // POST api/people/{id}
        public void Put(Guid id, [FromBody]UserModel user)
        {
            // do nothing
        }
    }
}