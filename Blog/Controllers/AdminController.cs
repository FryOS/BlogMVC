using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyBlog.Models;
using System.Threading.Tasks;

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

        public ViewResult Create()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateModel createModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = createModel.Name,
                    Email = createModel.Email
                };
                IdentityResult identityResult = await _userManager.CreateAsync(user, createModel.Password);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var item in identityResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(createModel);   
        }
    }
}
