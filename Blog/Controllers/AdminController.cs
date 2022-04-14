using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace Blog.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager; 
        }
        

        public ViewResult Index()
        {
            return View(_userManager.Users);
        }
    }
}
