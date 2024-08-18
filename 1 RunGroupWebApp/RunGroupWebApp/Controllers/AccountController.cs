using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.DTO;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;
        public readonly ApplicationDBContext _dbContext;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
            , ApplicationDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            _dbContext = context;
        }
        public IActionResult Login()
        {
            var response = new LoginDTO();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginModel)
        {
            if(!ModelState.IsValid) return View(loginModel);
            
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
            {
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginModel);
            }
            else
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user,loginModel.Password, false, false);
                    if (result.Succeeded) {
                        return RedirectToAction("Index", "Home");
                    } 
                }
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginModel);
            }

        }

        [HttpGet]
        public IActionResult Register() { 
            var response = new RegisterDTO();
            return View(response);
        }

    }
}
