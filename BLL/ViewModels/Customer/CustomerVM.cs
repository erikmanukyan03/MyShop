using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class CustomerVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public CustomerType CustomerType { get; set; }
        public string? Message { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [Display(Name = "Введите число с картинки")]
        public string Captcha { get; set; }
    }
}