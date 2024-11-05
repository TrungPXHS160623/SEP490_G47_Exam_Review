﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Subjects = new HashSet<Subject>();
        }

        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? DeanId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual User? Dean { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }

    }
}
