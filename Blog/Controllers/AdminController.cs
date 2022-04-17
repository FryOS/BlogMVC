using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyBlog.Models;
using System;
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

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                IdentityResult identityResult = await _userManager.DeleteAsync(user);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorFromResult(identityResult);
                }
            }else
            {
                ModelState.AddModelError("", "User not found");
            }
            return View("Index", _userManager.Users);
        }

        private void AddErrorFromResult(IdentityResult identityResult)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            { 
                return View(user);
            }else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string password)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email; 
                user.PasswordHash = password;
            } else
            {
                ModelState.AddModelError("", "User not found");
            }
            return View(user);
        }

    }
}
