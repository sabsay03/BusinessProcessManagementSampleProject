using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int MemberId { get; set; }
        public User Member { get; set; }
        public DateTime Date { get; set; }
        public int TaskId { get; set; }
        public Mission Mission { get; set; }

    }
}
