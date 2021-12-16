﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Relations
{
    public class ProjectMember
    {
        public int MemberId { get; set; }
        public User Member { get; set; }

        public int ProjecId { get; set; }
        public Project Project { get; set; }
    }
}
