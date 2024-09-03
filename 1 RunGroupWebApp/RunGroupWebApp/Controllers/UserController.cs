using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.DTO;
using RunGroupWebApp.Interfaces;

namespace RunGroupWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users= await _userRepository.GetAllUsers();
            List<UserDTO> result = new List<UserDTO>();
            foreach (var user in users)
            {
                var userDTO = new UserDTO() { 
                    Id = user.Id ,
                    UserName = user.UserName ,
                    Pace = user.Pace,
                    Milage = user.Mileage,

                
                };
                result.Add(userDTO);
            }
            return View(result);
        }
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDTO = new UserDetailsDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Milage = user.Mileage,
            };
            return View(userDTO);   
        }

    }
   
}
