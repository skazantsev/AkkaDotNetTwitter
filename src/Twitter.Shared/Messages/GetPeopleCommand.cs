namespace Twitter.Shared.Messages
{
    public class GetPeopleCommand
    {
        public GetPeopleCommand(string username)
        {
            Username = username;
        }

        public string Username { get; private set; }
    }
}
