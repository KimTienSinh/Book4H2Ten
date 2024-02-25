using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Emails.Dtos
{
    public class VerifyEmailRequestDtos
    {
        [Required]
        public string Token { get; set; }
    }
}
