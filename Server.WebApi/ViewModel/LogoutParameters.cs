using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.WebApi.ViewModel
{
    public class LogoutParameters
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
