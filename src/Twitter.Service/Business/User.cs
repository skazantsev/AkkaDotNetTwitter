using Seterlund.CodeGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using Twitter.Shared.Business;

namespace Twitter.Service.Business
{
    public class User
    {
        private readonly List<UserSession> _sessions;

        private readonly List<string> _followedUsers;

        public User(string username)
            : this(username, new List<UserSession>(), new List<string>())
        { }

        public User(string username, List<UserSession> sessions, List<string> followedUsers)
        {
            Guard.That(() => username).IsNotNull();
            Guard.That(() => sessions).IsNotNull();
            Guard.That(() => followedUsers).IsNotNull();

            Username = username;
            _sessions = sessions;
            _followedUsers = followedUsers;
        }

        public string Username { get; private set; }

        public IEnumerable<UserSession> Sessions
        {
            get { return _sessions; }
        }

        public IEnumerable<string> FollowedUsers
        {
            get { return _followedUsers; }
        }

        public void AddSession(UserSession session)
        {
            _sessions.Add(session);
        }

        public void Follow(string username)
        {
            if (!_followedUsers.Any(x => string.Equals(x, username, StringComparison.OrdinalIgnoreCase)))
                _followedUsers.Add(username);
        }

        public void Unfollow(string username)
        {
            _followedUsers.RemoveAll(x => string.Equals(x, username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
