using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Models.Project
{
    public class ProjectMemberCreateViewModel
    {
        [Display(Name = "Öğrencinin İsmi")]
        public string FullName { get; set; }
        [Display(Name = "Okul No")]
        [Required(ErrorMessage = "Okul No alanı gereklidir")]
        public string StudentNo { get; set; }
        [Display(Name = "Proje")]
        [Required(ErrorMessage = "Proje alanı gereklidir")]
        public int ProjecId { get; set; }
    }
}
