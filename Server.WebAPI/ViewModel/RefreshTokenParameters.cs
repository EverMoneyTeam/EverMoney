using System;
using System.ComponentModel.DataAnnotations;

namespace Server.WebApi.ViewModel
{
    public class RefreshTokenParameters
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        [MinLength(5)]
        public string Login { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}