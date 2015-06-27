using Akka.Actor;

namespace Twitter.Web.Actors
{
    public class SystemActors
    {
        public static IActorRef SignalRActor = ActorRefs.Nobody;

        public static IActorRef RemoteApiActor = ActorRefs.Nobody;
    }
}