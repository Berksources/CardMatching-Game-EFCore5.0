using System.ComponentModel.DataAnnotations;

namespace WebKartOyunu.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string UserEMail { get; set; }
        [Required]
        public string UserPassword { get; set; }
    }
}
