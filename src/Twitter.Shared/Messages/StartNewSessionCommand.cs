using Twitter.Shared.Business;

namespace Twitter.Shared.Messages
{
    public class StartNewSessionCommand
    {
        public StartNewSessionCommand(UserSession userSession)
        {
            UserSession = userSession;
        }

        public UserSession UserSession { get; private set; }
    }
}
