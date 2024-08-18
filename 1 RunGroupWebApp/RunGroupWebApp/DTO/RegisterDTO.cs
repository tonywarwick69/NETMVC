using System.ComponentModel.DataAnnotations;

namespace RunGroupWebApp.DTO
{
    public class RegisterDTO
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Email address is required")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Poassword do not match")]
        public string ConfirmPassword { get; set; }

    }
}
