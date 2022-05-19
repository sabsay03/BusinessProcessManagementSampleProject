using EntityLayer.Concrete;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Models.Mission
{
    public class MissionCreateViewModel : IValidatableObject
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Öğrenci No gerekli.")]
        [Display(Name = "Öğrenci No:")]
        public string StudentNumber { get; set; }
        public string StudenFullName { get; set; }

        [Required(ErrorMessage = "Başlangıç Tarihi gerekli.")]
        [Display(Name = "Başlangç Tarihi:")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Bitiş Tarihi gerekli.")]
        [Display(Name = "Bitiş Tarihi:")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Proje Başlığı gerekli.")]
        [Display(Name = "Başlık:")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Görev İçeriği gerekli.")]
        [Display(Name = "İçerik:")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Proje seçmeniz gerekli.")]
        [Display(Name = "Proje:")]
        public int ProjectId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult(
                    errorMessage: "Bitiş Tarihi başlangıç tarihinden önce olamaz.",
                    memberNames: new[] { "EndDate" }
               );
            }
        }
    }


}
