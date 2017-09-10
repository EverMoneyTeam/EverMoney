using System.ComponentModel.DataAnnotations;

namespace Server.WebApi.ViewModel
{
    public class LoginParameters
    {
        [Required]
        [MinLength(5)]
        public string Login { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
