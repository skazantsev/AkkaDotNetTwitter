using Akka.Actor;

namespace Twitter.Shared.Business
{
    public class UserSession
    {
        public UserSession(string username, IActorRef requestor)
        {
            Username = username;
            Requestor = requestor;
        }

        public string Username { get; private set; }

        public IActorRef Requestor { get; private set; }
    }
}
