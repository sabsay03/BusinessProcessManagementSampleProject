using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Models.Project
{
    public class ProjectCreateViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Proje Başlığı gerekli.")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Proje Açıklaması boş bırakılamaz.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bitiş Tarihi boş bırakılamaz.")]
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Bitiş Tarihi boş bırakılamaz.")]
        [Display(Name = "Bitiş Tarihi")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Dosya Linki")]
        public string FilePath { get; set; }
    }
}
