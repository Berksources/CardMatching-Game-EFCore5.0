using System.ComponentModel.DataAnnotations;

namespace WebKartOyunu.Models
{
    public class ForgotMyPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
