using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.UI.Models.Project
{
    public class ProjectRequestCreateViewModel
    {

        [Display(Name = "Öğretmen:")]
        [Required(ErrorMessage = "Okul No alanı gereklidir")]
        public int ManagerId { get; set; }

        [Display(Name = "Proje:")]
        [Required(ErrorMessage = "Proje alanı gereklidir")]
        public int ProjecId { get; set; }
    }
}
