using System;
using Akka.Actor;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Twitter.Shared.Messages;
using Twitter.Web.Hubs;

namespace Twitter.Web.Actors
{
    public class SignalRActor : ReceiveActor
    {
        private readonly IActorRef _remoteApiActor;

        private UserHub _userHub;

        public SignalRActor(IActorRef remoteApiActor)
        {
            _remoteApiActor = remoteApiActor;

            Receive<StartNewSessionCommand>(x => _remoteApiActor.Tell(x));
            Receive<NewUserConnected>(x => _userHub.NewUserConnected(x.Username));
        }

        protected override void PreStart()
        {
            var hubManager = new DefaultHubManager(GlobalHost.DependencyResolver);
            _userHub = hubManager.ResolveHub("userHub") as UserHub;
        }
    }
}