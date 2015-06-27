using Akka.Actor;

namespace Twitter.Shared.Actors
{
    public class RemoteApiActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            Context.ActorSelection("/user/api").Tell(message, Sender);
        }
    }
}
