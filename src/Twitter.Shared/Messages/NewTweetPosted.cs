namespace Twitter.Shared.Messages
{
    public class NewTweetPosted
    {
        public NewTweetPosted(string username, string tweetText)
        {
            Username = username;
            TweetText = tweetText;
        }

        public string Username { get; private set; }

        public string TweetText { get; private set; }
    }
}
