using System;

namespace Twitter.Web.Models
{
    [Serializable]
    public class UserToFollow
    {
        public UserToFollow()
        { }

        public UserToFollow(Guid userId, string nickname, bool isFollowed)
        {
            UserId = userId;
            Nickname = nickname;
            IsFollowed = isFollowed;
        }

        public Guid UserId { get; set; }

        public string Nickname { get; set; }

        public bool IsFollowed { get; set; }

        public void Follow()
        {
            IsFollowed = true;
        }

        public void Unfollow()
        {
            IsFollowed = false;
        }
    }
}