using Akka.Actor;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Twitter.Shared.Business;
using Twitter.Shared.Messages;

namespace Twitter.Service.Actors
{
    public class ApiActor : ReceiveActor
    {
        public static readonly ConcurrentDictionary<string, List<UserSession>> Users = new ConcurrentDictionary<string, List<UserSession>>();

        public ApiActor()
        {
            Receive<StartNewSessionCommand>(x => ConnectUserImpl(x));

            Receive<GetUsersCommand>(x => Sender.Tell(Users.Keys.ToList()));
        }

        #region private messages

        private void ConnectUserImpl(StartNewSessionCommand command)
        {
            var username = command.UserSession.Username;
            var userSessions = Users.GetOrAdd(command.UserSession.Username, new List<UserSession>());
            userSessions.Add(new UserSession(username, command.UserSession.Requestor));

            if (userSessions.Count <= 1)
            {
                BroadcastNewUserConnected(username);
            }
        }

        private void BroadcastNewUserConnected(string username)
        {
            var broadcastTo = Users.SelectMany(x => x.Value).Where(x => !x.Requestor.Equals(ActorRefs.Nobody));

            foreach (var session in broadcastTo)
            {
                session.Requestor.Tell(new NewUserConnected(username));
            }
        }

        #endregion
    }
}
