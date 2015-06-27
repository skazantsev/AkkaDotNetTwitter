using Akka.Actor;

namespace Twitter.Service.Actors
{
    public class SystemActors
    {
        public static IActorRef ApiActor = ActorRefs.Nobody;
    }
}
