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
                        return RedirectToAction("Index", "Dashboard");
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
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerModel)
        {
            if (!ModelState.IsValid) return View(registerModel);
            var user = await _userManager.FindByEmailAsync(registerModel.Email);

            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerModel);
            }
            //if user doesn't already exist => create new user
            var newUser = new AppUser()
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,

            };
             var newUserResponse = await _userManager.CreateAsync(newUser, registerModel.Password);
            if (newUserResponse.Succeeded) {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
               
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

    }
}
