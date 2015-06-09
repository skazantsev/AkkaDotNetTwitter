using Topshelf;

namespace Twitter.Service
{
    public class Program
    {
        public static void Main()
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceHost>();
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.EnableServiceRecovery(r => r.RestartService(1));

                x.SetServiceName("TwitterService");
                x.SetDisplayName("Twitter Service");
            });
        }
    }
}
