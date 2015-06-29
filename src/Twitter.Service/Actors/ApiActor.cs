using System;
using System.Collections.Generic;
using Akka.Actor;
using System.Collections.Concurrent;
using System.Linq;
using Twitter.Service.Business;
using Twitter.Shared.Business;
using Twitter.Shared.Messages;

namespace Twitter.Service.Actors
{
    public class ApiActor : ReceiveActor
    {
        public static readonly ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();

        public ApiActor()
        {
            Receive<StartNewSessionCommand>(x => StartNewSessionImpl(x.UserSession));

            Receive<GetPeopleCommand>(x => GetPeopleImpl(x.Username));

            Receive<FollowUserCommand>(x => ChangeFolloweeImpl(x.Follower, x.Followee, true));

            Receive<UnfollowUserCommand>(x => ChangeFolloweeImpl(x.Follower, x.Followee, false));

            Receive<NewMessagePosted>(x => BroadcastToFollowers(x, x.Username));
        }

        #region private messages

        private void StartNewSessionImpl(UserSession userSession)
        {
            var user = Users.GetOrAdd(userSession.Username, new User(userSession.Username));
            user.AddSession(userSession);

            if (user.Sessions.Count() <= 1)
            {
                BroadcastToEveryone(new NewUserConnected(userSession.Username));
            }
        }        

        private void GetPeopleImpl(string username)
        {
            User user;
            if (Users.TryGetValue(username, out user))
            {
                var userDataList = Users.Values
                    .Where(x => !string.Equals(x.Username, user.Username, StringComparison.OrdinalIgnoreCase))
                    .Select(
                        x =>
                            new UserData
                            {
                                Username = x.Username,
                                IsFollowed =
                                    user.FollowedUsers.Any(
                                        _ => string.Equals(_, x.Username, StringComparison.OrdinalIgnoreCase))
                            })
                    .ToList();

                Sender.Tell(userDataList);
            }
            else
            {
                Sender.Tell(new List<UserData>());
            }
        }

        // ReSharper disable RedundantIfElseBlock
        private void ChangeFolloweeImpl(string follower, string followee, bool isFollowed)
        {
            User followerUser, followeeUser;
            if (Users.TryGetValue(follower, out followerUser) && Users.TryGetValue(followee, out followeeUser))
            {
                if (isFollowed)
                {
                    followerUser.Follow(followee);
                    followeeUser.AddFollower(follower);
                }
                else
                {
                    followerUser.Unfollow(followee);
                    followeeUser.RemoveFollower(follower);
                }
                Sender.Tell(new Results.SuccessResult());
            }
            else
            {
                Sender.Tell(new Results.FailureResult());
            }
        }
        // ReSharper restore RedundantIfElseBlock

        private void BroadcastToEveryone<T>(T message)
        {
            var broadcastTo = Users.SelectMany(x => x.Value.Sessions).Where(x => !x.Requestor.Equals(ActorRefs.Nobody));
            foreach (var session in broadcastTo)
            {
                session.Requestor.Tell(message);
            }
        }

        private void BroadcastToFollowers<T>(T message, string followee)
        {
            User followeeUser;
            if (!Users.TryGetValue(followee, out followeeUser))
                return;

            var broadcastTo = Users
                .Join(followeeUser.Followers, user => user.Key, follower => follower, (user, followers) => user)
                .SelectMany(x => x.Value.Sessions)
                .Where(x => !x.Requestor.Equals(ActorRefs.Nobody));

            foreach (var session in broadcastTo)
            {
                session.Requestor.Tell(message);
            }
        }

        #endregion
    }
}
