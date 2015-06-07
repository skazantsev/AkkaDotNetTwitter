using System;
using System.Web.Mvc;
using Twitter.Web.Business;

namespace Twitter.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly FollowingService _followingService;

        public HomeController()
        {
            _followingService = new FollowingService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UsersToFollow()
        {
            var model = _followingService.GetUsersToFollow();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Follow(Guid userId)
        {
            _followingService.Follow(userId);
            return null;
        }
    }
}