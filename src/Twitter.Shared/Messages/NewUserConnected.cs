namespace Twitter.Shared.Messages
{
    public class NewUserConnected
    {
        public NewUserConnected(string username)
        {
            Username = username;
        }

        public string Username { get; private set; }
    }
}
