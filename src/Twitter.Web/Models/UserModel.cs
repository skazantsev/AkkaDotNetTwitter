using Twitter.Shared.Business;

namespace Twitter.Web.Models
{
    public class UserModel
    {
        public string Username { get; set; }

        public bool IsFollowed { get; set; }

        public static UserModel From(UserData data)
        {
            return new UserModel {Username = data.Username, IsFollowed = data.IsFollowed};
        }
    }
}