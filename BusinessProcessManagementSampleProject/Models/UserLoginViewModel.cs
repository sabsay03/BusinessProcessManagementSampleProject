using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Models
{
    public class UserLoginViewModel
    {
        [Display(Name = "Kullanıcı Adı Giriniz")]
        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Giriniz")]
        public string UserName { get; set; }

        [Display(Name = "Şifre Giriniz")]
        [Required(ErrorMessage = "Lütfen Şifre Giriniz")]
        public string Password { get; set; }

    }
}
