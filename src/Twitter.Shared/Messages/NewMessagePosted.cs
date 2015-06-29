namespace Twitter.Shared.Messages
{
    public class NewMessagePosted
    {
        public NewMessagePosted(string username, string message)
        {
            Username = username;
            Message = message;
        }

        public string Username { get; private set; }

        public string Message { get; private set; }
    }
}
