using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signManager = signInManager;
            _userManager = userManager; 
        }
        

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(details.Email);
                if(user != null)
                {
                    await _signManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await _signManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            }
            return View(details);
        }

    }
}
