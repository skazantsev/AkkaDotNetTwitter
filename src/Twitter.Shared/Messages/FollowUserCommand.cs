namespace Twitter.Shared.Messages
{
    public class FollowUserCommand
    {
        public FollowUserCommand(string follower, string followee)
        {
            Follower = follower;
            Followee = followee;
        }

        public string Follower { get; private set; }

        public string Followee { get; private set; }
    }
}
