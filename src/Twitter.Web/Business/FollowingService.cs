using System;
using System.Collections.Generic;
using System.Linq;
using Twitter.Web.Business.Exceptions;
using Twitter.Web.Models;

namespace Twitter.Web.Business
{
    public class FollowingService
    {
        private static List<UserToFollow> _usersToFollow;
 
        public List<UserToFollow> GetUsersToFollow()
        {
            _usersToFollow = _usersToFollow ?? new List<UserToFollow>
            {
                new UserToFollow(Guid.NewGuid(), "John", false),
                new UserToFollow(Guid.NewGuid(), "Alexander", false),
                new UserToFollow(Guid.NewGuid(), "Mia", false),
                new UserToFollow(Guid.NewGuid(), "Anna", false),
                new UserToFollow(Guid.NewGuid(), "Mike", false),
                new UserToFollow(Guid.NewGuid(), "Tom", false)
            };
            return _usersToFollow;
        }

        public UserToFollow Get(Guid userId)
        {
            var user = _usersToFollow.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
                throw new UserNotFoundException();
            return user;
        }

        public void Follow(Guid userId)
        {
            Get(userId).Follow();
        }

        public void Unfollow(Guid userId)
        {
            Get(userId).Unfollow();
        }
    }
}