using System;
using System.Collections.Generic;
using System.Web.Http;
using Twitter.Web.Business;
using Twitter.Web.Models;

namespace Twitter.Web.Controllers
{
    public class PeopleController : ApiController
    {
        private readonly FollowingService _followingService;

        public PeopleController()
        {
            _followingService = new FollowingService();
        }

        // GET api/people
        public IEnumerable<UserToFollow> Get()
        {
            return _followingService.GetUsersToFollow();
        }

        // POST api/people/{id}
        public void Put(Guid id, [FromBody]UserToFollow user)
        {
            if (user.IsFollowed)
            {
                _followingService.Follow(id);
            }
            else
            {
                _followingService.Unfollow(id);
            }
        }
    }
}