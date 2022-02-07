using System.ComponentModel.DataAnnotations;

namespace AuthIS4_Core3_1.IdentityServer4Service.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string ReturnUrl { get; set; } = string.Empty;
    }
}