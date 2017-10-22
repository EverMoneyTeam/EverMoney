using System;
using System.ComponentModel.DataAnnotations;

namespace Server.WebApi.ViewModel
{
    public class RefreshTokenParameters
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}