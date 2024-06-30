using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } 
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
    }
}
