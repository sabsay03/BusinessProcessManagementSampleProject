using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Models
{
    public class UserRegisterViewModel
    {
        [Display(Name="Ad Giriniz")]
        [Required(ErrorMessage ="Lütfen Adınızı Giriniz")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad Giriniz")]
        [Required(ErrorMessage = "Lütfen Soyad Giriniz")]
        public string LastName { get; set; }


        [Display(Name = "Kullanıcı Adı Giriniz")]
        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Giriniz")]
        public string UserName { get; set; }

        [Display(Name = "Mailinizi Giriniz")]
        [Required(ErrorMessage = "Lütfen Mailinizi Giriniz")]
        public string Mail { get; set; }


        [Display(Name = "Şifre Giriniz")]
        [Required(ErrorMessage = "Lütfen Şifre Giriniz")]
        public string Password { get; set; }


        [Display(Name = "Şifre Tekrar Giriniz")]
        [Compare("Password",ErrorMessage = "Şifreyi Tekrar Giriniz")]
        [Required(ErrorMessage = "Lütfen Şifre Giriniz")]
        public string ConfirmPassword { get; set; }
    }
}
