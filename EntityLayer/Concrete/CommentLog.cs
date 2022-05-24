using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CommentLog
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int? MissionId { get; set; }
        public CommentType commentType { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public Mission Mission { get; set; }

    }
}
